using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Dtos;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class VentaRepositorio
{
	/// <summary>Registra cabecera, detalle, stock y cuenta corriente en una transacción.</summary>
	public int Registrar(Venta venta)
	{
		using var db = new PeluqueriaDbContext();
		using var tx = db.Database.BeginTransaction();

		try
		{
			int idCliente = venta.IdCliente != 0 ? venta.IdCliente : venta.Cliente?.Id ?? 0;
			int idUsuario = venta.IdUsuario != 0 ? venta.IdUsuario : venta.Usuario?.Id ?? 0;

			decimal total = venta.Detalles.Sum(d => d.Precio * d.Cantidad)
				- venta.Descuento + venta.Recargo;

			var nueva = new Venta
			{
				IdCliente = idCliente,
				IdUsuario = idUsuario,
				IdEmpleado = venta.IdEmpleado,
				Fecha = venta.Fecha,
				MedioPago = venta.MedioPago,
				IdTarjetaCredito = venta.IdTarjetaCredito,
				CantidadCuotas = venta.CantidadCuotas,
				Descuento = venta.Descuento,
				Recargo = venta.Recargo,
				Total = total,
				Anulada = false,
			};

			db.Ventas.Add(nueva);
			db.SaveChanges();

			foreach (DetalleVenta detalle in venta.Detalles)
			{
				int idItem = detalle.IdItem != 0 ? detalle.IdItem : detalle.Item?.Id ?? 0;
				db.DetallesVenta.Add(new DetalleVenta
				{
					IdVenta = nueva.Id,
					IdItem = idItem,
					Cantidad = detalle.Cantidad,
					Precio = detalle.Precio,
					Subtotal = detalle.Precio * detalle.Cantidad,
				});
			}

			db.SaveChanges();

			var ids = venta.Detalles
				.Select(d => d.IdItem != 0 ? d.IdItem : d.Item?.Id ?? 0)
				.Distinct()
				.ToList();

			var datosItems = db.Items.AsNoTracking()
				.Where(i => ids.Contains(i.Id))
				.Select(i => new { i.Id, i.Tipo, i.Nombre })
				.ToDictionary(i => i.Id);

			foreach (DetalleVenta detalle in venta.Detalles)
			{
				int idItem = detalle.IdItem != 0 ? detalle.IdItem : detalle.Item?.Id ?? 0;
				if (!datosItems.TryGetValue(idItem, out var datos) || datos.Tipo != TipoItem.Producto)
					continue;

				int filas = db.Items
					.Where(i => i.Id == idItem && i.Stock >= detalle.Cantidad)
					.ExecuteUpdate(s => s.SetProperty(
						i => i.Stock,
						i => (i.Stock ?? 0) - detalle.Cantidad));

				if (filas == 0)
					throw new InvalidOperationException($"Stock insuficiente de {datos.Nombre}.");
			}

			if (venta.MedioPago == "cuenta_corriente")
			{
				CuentaCorriente? cuenta = db.CuentasCorrientes
					.FromSqlInterpolated($"SELECT * FROM cuentas_corrientes WHERE id_cliente = {idCliente} FOR UPDATE")
					.SingleOrDefault();

				if (cuenta == null)
				{
					cuenta = new CuentaCorriente
					{
						IdCliente = idCliente,
						Saldo = 0m,
						FechaActualizacion = DateTime.Now,
					};
					db.CuentasCorrientes.Add(cuenta);
					db.SaveChanges();
				}

				db.MovimientosCuentaCorriente.Add(new MovimientoCuentaCorriente
				{
					IdCuenta = cuenta.Id,
					Fecha = venta.Fecha,
					Tipo = TipoMovimientoCuentaCorriente.Cargo,
					Monto = total,
					Concepto = $"Venta #{nueva.Id}",
					IdVenta = nueva.Id,
					IdUsuario = idUsuario,
				});

				cuenta.Saldo += total;
				cuenta.FechaActualizacion = DateTime.Now;
				db.SaveChanges();
			}

			tx.Commit();
			venta.Id = nueva.Id;
			venta.Total = total;
			return nueva.Id;
		}
		catch
		{
			tx.Rollback();
			throw;
		}
	}

	public List<VentaListadoDto> Listar(DateTime desde, DateTime hasta, int? usuarioId = null,
		int? empleadoId = null, int? ventaId = null, int? limite = null, int? desplazamiento = null)
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

		IQueryable<Venta> consulta = db.Ventas.AsNoTracking()
			.Where(v => v.Fecha >= inicio && v.Fecha < fin);

		if (usuarioId.HasValue)
			consulta = consulta.Where(v => v.IdUsuario == usuarioId.Value);
		if (empleadoId.HasValue)
			consulta = consulta.Where(v => v.IdEmpleado == empleadoId.Value);
		if (ventaId.HasValue)
			consulta = consulta.Where(v => v.Id == ventaId.Value);

		IQueryable<VentaListadoDto> proyeccion = consulta
			.OrderByDescending(v => v.Fecha)
			.ThenByDescending(v => v.Id)
			.Select(v => new VentaListadoDto
			{
				Venta = v.Id,
				Fecha = v.Fecha,
				Cliente = v.Cliente.Apellido + ", " + v.Cliente.Nombre,
				Usuario = v.Usuario.NombreUsuario,
				Empleado = v.Empleado == null ? null : v.Empleado.Apellido + ", " + v.Empleado.Nombre,
				Pago = v.MedioPago,
				Tarjeta = v.TarjetaCredito == null
					? null
					: v.TarjetaCredito.Banco.Nombre + " - " + v.TarjetaCredito.TipoTarjeta,
				Cuotas = v.CantidadCuotas,
				Subtotal = v.Detalles.Sum(d => (decimal?)d.Subtotal) ?? 0m,
				Descuento = v.Descuento,
				Recargo = v.Recargo,
				Total = v.Total,
				Anulada = v.Anulada,
			});

		return Comun.Paginar(proyeccion, limite, desplazamiento).ToList();
	}

	public int Contar(DateTime desde, DateTime hasta, int? usuarioId = null,
		int? empleadoId = null, int? ventaId = null)
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

		IQueryable<Venta> consulta = db.Ventas.AsNoTracking()
			.Where(v => v.Fecha >= inicio && v.Fecha < fin);

		if (usuarioId.HasValue)
			consulta = consulta.Where(v => v.IdUsuario == usuarioId.Value);
		if (empleadoId.HasValue)
			consulta = consulta.Where(v => v.IdEmpleado == empleadoId.Value);
		if (ventaId.HasValue)
			consulta = consulta.Where(v => v.Id == ventaId.Value);

		return consulta.Count();
	}

	public VentaCabeceraDto? ObtenerCabecera(int ventaId)
	{
		using var db = new PeluqueriaDbContext();
		return db.Ventas.AsNoTracking()
			.Where(v => v.Id == ventaId)
			.Select(v => new VentaCabeceraDto
			{
				Venta = v.Id,
				Fecha = v.Fecha,
				Cliente = v.Cliente.Apellido + ", " + v.Cliente.Nombre,
				Usuario = v.Usuario.NombreUsuario,
				Empleado = v.Empleado == null ? null : v.Empleado.Apellido + ", " + v.Empleado.Nombre,
				Pago = v.MedioPago,
				IdTarjeta = v.IdTarjetaCredito,
				Banco = v.TarjetaCredito == null ? null : v.TarjetaCredito.Banco.Nombre,
				TipoTarjeta = v.TarjetaCredito == null ? null : v.TarjetaCredito.TipoTarjeta,
				Cuotas = v.CantidadCuotas,
				Subtotal = v.Detalles.Sum(d => (decimal?)d.Subtotal) ?? 0m,
				Descuento = v.Descuento,
				Anulada = v.Anulada,
				FechaAnulacion = v.FechaAnulacion,
				MotivoAnulacion = v.MotivoAnulacion,
				UsuarioAnulacion = v.UsuarioAnulacion == null ? null : v.UsuarioAnulacion.NombreUsuario,
				Recargo = v.Recargo,
				Total = v.Total,
			})
			.SingleOrDefault();
	}

	public List<VentaDetalleDto> ObtenerDetalle(int ventaId)
	{
		using var db = new PeluqueriaDbContext();
		return db.DetallesVenta.AsNoTracking()
			.Where(d => d.IdVenta == ventaId)
			.OrderBy(d => d.Id)
			.Select(d => new VentaDetalleDto
			{
				Item = d.Item.Nombre,
				Tipo = d.Item.Tipo == TipoItem.Producto ? "producto" : "servicio",
				Cantidad = d.Cantidad,
				Precio = d.Precio,
				Subtotal = d.Subtotal,
			})
			.ToList();
	}

	/// <summary>Anula sin borrar: devuelve stock, revierte la cuenta corriente y marca la venta.</summary>
	public void Anular(int ventaId, int usuarioId, string motivo)
	{
		using var db = new PeluqueriaDbContext();
		using var tx = db.Database.BeginTransaction();

		try
		{
			Venta venta = db.Ventas
				.FromSqlInterpolated($"SELECT * FROM ventas WHERE id_venta = {ventaId} AND anulada = 0 FOR UPDATE")
				.AsNoTracking()
				.SingleOrDefault()
				?? throw new InvalidOperationException("La venta no existe o ya fue anulada.");

			var detalles = db.DetallesVenta.AsNoTracking()
				.Where(d => d.IdVenta == ventaId)
				.Select(d => new { d.IdItem, d.Cantidad })
				.ToList();

			var ids = detalles.Select(d => d.IdItem).Distinct().ToList();
			var idsProducto = db.Items.AsNoTracking()
				.Where(i => i.Tipo == TipoItem.Producto && ids.Contains(i.Id))
				.Select(i => i.Id)
				.ToList();

			foreach (var detalle in detalles.Where(d => idsProducto.Contains(d.IdItem)))
			{
				db.Items
					.Where(i => i.Id == detalle.IdItem)
					.ExecuteUpdate(s => s.SetProperty(
						i => i.Stock,
						i => (i.Stock ?? 0) + detalle.Cantidad));
			}

			if (venta.MedioPago == "cuenta_corriente")
			{
				CuentaCorriente cuenta = db.CuentasCorrientes
					.FromSqlInterpolated($"SELECT * FROM cuentas_corrientes WHERE id_cliente = {venta.IdCliente} FOR UPDATE")
					.SingleOrDefault()
					?? throw new InvalidOperationException("No se encontró la cuenta corriente del cliente.");

				db.MovimientosCuentaCorriente.Add(new MovimientoCuentaCorriente
				{
					IdCuenta = cuenta.Id,
					Fecha = DateTime.Now,
					Tipo = TipoMovimientoCuentaCorriente.Abono,
					Monto = venta.Total,
					Concepto = $"Anulación venta #{ventaId}: {motivo}",
					IdVenta = ventaId,
					IdUsuario = usuarioId,
				});

				cuenta.Saldo -= venta.Total;
				cuenta.FechaActualizacion = DateTime.Now;
				db.SaveChanges();
			}

			int filas = db.Ventas
				.Where(v => v.Id == ventaId && !v.Anulada)
				.ExecuteUpdate(s => s
					.SetProperty(v => v.Anulada, true)
					.SetProperty(v => v.FechaAnulacion, (DateTime?)DateTime.Now)
					.SetProperty(v => v.IdUsuarioAnulacion, (int?)usuarioId)
					.SetProperty(v => v.MotivoAnulacion, motivo));

			if (filas == 0)
				throw new InvalidOperationException("La venta ya fue anulada.");

			tx.Commit();
		}
		catch
		{
			tx.Rollback();
			throw;
		}
	}
}
