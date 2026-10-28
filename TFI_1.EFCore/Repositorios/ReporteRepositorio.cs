using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Dtos;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class ReporteRepositorio
{
	public List<ResumenDto> Resumen(DateTime desde, DateTime hasta)
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

		ResumenDto? resumen = db.Ventas.AsNoTracking()
			.Where(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada)
			.GroupBy(_ => 1)
			.Select(g => new ResumenDto
			{
				Total = g.Sum(v => v.Total),
				Cantidad = g.Count(),
				TicketPromedio = g.Average(v => (decimal?)v.Total) ?? 0m,
			})
			.SingleOrDefault();

		// El original devuelve una fila con ceros cuando no hay ventas en el período.
		return new List<ResumenDto> { resumen ?? new ResumenDto() };
	}

	public List<VentasPorMedioPagoDto> VentasPorMedioPago(DateTime desde, DateTime hasta)
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

		return db.Ventas.AsNoTracking()
			.Where(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada)
			.GroupBy(v => v.MedioPago)
			.Select(g => new VentasPorMedioPagoDto
			{
				Pago = g.Key,
				Cantidad = g.Count(),
				Total = g.Sum(v => v.Total),
			})
			.OrderByDescending(x => x.Total)
			.ToList();
	}

	public List<ItemVendidoDto> ProductosMasVendidos(DateTime desde, DateTime hasta)
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

		return (from d in db.DetallesVenta.AsNoTracking()
				join v in db.Ventas on d.IdVenta equals v.Id
				join i in db.Items on d.IdItem equals i.Id
				where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada
				group d by new { i.Id, i.Nombre, i.Tipo } into g
				select new ItemVendidoDto
				{
					Item = g.Key.Nombre,
					Tipo = g.Key.Tipo == TipoItem.Producto ? "producto" : "servicio",
					Cantidad = g.Sum(x => x.Cantidad),
					Total = g.Sum(x => x.Subtotal),
				})
			.OrderByDescending(x => x.Cantidad)
			.ThenByDescending(x => x.Total)
			.Take(10)
			.ToList();
	}

	public List<VentasPorDiaDto> VentasPorDia(DateTime desde, DateTime hasta)
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

		return db.Ventas.AsNoTracking()
			.Where(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada)
			.GroupBy(v => v.Fecha.Date)
			.Select(g => new VentasPorDiaDto
			{
				Fecha = g.Key,
				Cantidad = g.Count(),
				Total = g.Sum(v => v.Total),
			})
			.OrderBy(x => x.Fecha)
			.ToList();
	}

	public List<ClientesComprasDto> ClientesConMasCompras(DateTime desde, DateTime hasta)
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

		return (from v in db.Ventas.AsNoTracking()
				join c in db.Clientes on v.IdCliente equals c.Id
				where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada
				group v by new { c.Id, c.Apellido, c.Nombre, c.Dni } into g
				select new ClientesComprasDto
				{
					Cliente = g.Key.Apellido + ", " + g.Key.Nombre,
					DNI = g.Key.Dni,
					Compras = g.Count(),
					Total = g.Sum(v => v.Total),
				})
			.OrderByDescending(x => x.Compras)
			.ThenByDescending(x => x.Total)
			.Take(20)
			.ToList();
	}

	public List<VentasPorUsuarioDto> VentasPorUsuario(DateTime desde, DateTime hasta)
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

		return (from v in db.Ventas.AsNoTracking()
				join u in db.Usuarios on v.IdUsuario equals u.Id
				join r in db.Roles on u.IdRol equals r.Id
				where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada
				group v by new { u.Id, u.NombreUsuario, r.Nombre } into g
				select new VentasPorUsuarioDto
				{
					Usuario = g.Key.NombreUsuario,
					Rol = g.Key.Nombre,
					Cantidad = g.Count(),
					Total = g.Sum(v => v.Total),
				})
			.OrderByDescending(x => x.Total)
			.ToList();
	}

	public List<ServiciosSolicitadosDto> ServiciosMasSolicitados(DateTime desde, DateTime hasta)
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

		return (from d in db.DetallesVenta.AsNoTracking()
				join v in db.Ventas on d.IdVenta equals v.Id
				join i in db.Items on d.IdItem equals i.Id
				where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada && i.Tipo == TipoItem.Servicio
				group d by new { i.Id, i.Nombre } into g
				select new ServiciosSolicitadosDto
				{
					Servicio = g.Key.Nombre,
					Veces = g.Count(),
					Cantidad = g.Sum(x => x.Cantidad),
					Total = g.Sum(x => x.Subtotal),
				})
			.OrderByDescending(x => x.Cantidad)
			.ThenByDescending(x => x.Total)
			.Take(20)
			.ToList();
	}

	public List<VentasPorEmpleadoDto> VentasPorEmpleado(DateTime desde, DateTime hasta)
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

		return (from v in db.Ventas.AsNoTracking()
				join e in db.Empleados on v.IdEmpleado equals (int?)e.Id into empleados
				from e in empleados.DefaultIfEmpty()
				where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada
				group v by new { v.IdEmpleado, e.Apellido, e.Nombre } into g
				select new VentasPorEmpleadoDto
				{
					Empleado = g.Key.Apellido == null
						? "Sin empleado"
						: g.Key.Apellido + ", " + g.Key.Nombre,
					Cantidad = g.Count(),
					Total = g.Sum(v => v.Total),
				})
			.OrderByDescending(x => x.Total)
			.ToList();
	}

	// Ventas anuladas dentro del período, por fecha de anulación.
	public List<AnulacionDto> Anulaciones(DateTime desde, DateTime hasta)
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

		return db.Ventas.AsNoTracking()
			.Where(v => v.Anulada
				&& v.FechaAnulacion != null
				&& v.FechaAnulacion >= inicio
				&& v.FechaAnulacion < fin)
			.OrderByDescending(v => v.FechaAnulacion)
			.ThenByDescending(v => v.Id)
			.Select(v => new AnulacionDto
			{
				Venta = v.Id,
				FechaVenta = v.Fecha,
				FechaAnulacion = v.FechaAnulacion,
				Cliente = v.Cliente.Apellido + ", " + v.Cliente.Nombre,
				Pago = v.MedioPago,
				Total = v.Total,
				Usuario = v.UsuarioAnulacion != null
					? v.UsuarioAnulacion.NombreUsuario
					: "Sin usuario",
				Motivo = v.MotivoAnulacion,
			})
			.ToList();
	}
}
