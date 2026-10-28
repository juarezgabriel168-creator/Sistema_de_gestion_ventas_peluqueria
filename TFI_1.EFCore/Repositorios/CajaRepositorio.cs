using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Dtos;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class CajaRepositorio
{
	/// <summary>Abre una caja solo si no hay otra abierta; devuelve 0 si ya había una.</summary>
	public int Abrir(int usuarioId, decimal montoInicial)
	{
		using var db = new PeluqueriaDbContext();
		using var tx = db.Database.BeginTransaction(IsolationLevel.Serializable);

		if (db.Cajas.Any(c => c.Estado == "abierta"))
			return 0;

		var caja = new Caja
		{
			IdUsuarioApertura = usuarioId,
			FechaApertura = DateTime.Now,
			MontoInicial = montoInicial,
			Estado = "abierta",
		};

		db.Cajas.Add(caja);
		db.SaveChanges();
		tx.Commit();
		return caja.Id;
	}

	public Caja? ObtenerAbierta()
	{
		using var db = new PeluqueriaDbContext();
		return db.Cajas.AsNoTracking()
			.Include(c => c.UsuarioApertura)
			.Include(c => c.UsuarioCierre)
			.Where(c => c.Estado == "abierta")
			.OrderByDescending(c => c.Id)
			.FirstOrDefault();
	}

	public Caja? ObtenerPorId(int id)
	{
		using var db = new PeluqueriaDbContext();
		return db.Cajas.AsNoTracking()
			.Include(c => c.UsuarioApertura)
			.Include(c => c.UsuarioCierre)
			.SingleOrDefault(c => c.Id == id);
	}

	public bool Cerrar(int idCaja, int usuarioId, decimal montoEsperado, decimal montoContado, string? observacion)
	{
		using var db = new PeluqueriaDbContext();
		int filas = db.Cajas
			.Where(c => c.Id == idCaja && c.Estado == "abierta")
			.ExecuteUpdate(s => s
				.SetProperty(c => c.IdUsuarioCierre, (int?)usuarioId)
				.SetProperty(c => c.FechaCierre, (DateTime?)DateTime.Now)
				.SetProperty(c => c.MontoEsperado, (decimal?)montoEsperado)
				.SetProperty(c => c.MontoContado, (decimal?)montoContado)
				.SetProperty(c => c.Diferencia, (decimal?)(montoContado - montoEsperado))
				.SetProperty(c => c.Observacion, Comun.Limpiar(observacion))
				.SetProperty(c => c.Estado, "cerrada"));

		return filas == 1;
	}

	public int RegistrarMovimiento(int idCaja, string tipo, decimal monto, string concepto, int usuarioId)
	{
		using var db = new PeluqueriaDbContext();
		var movimiento = new CajaMovimiento
		{
			IdCaja = idCaja,
			Fecha = DateTime.Now,
			Tipo = tipo,
			Monto = monto,
			Concepto = concepto,
			IdUsuario = usuarioId,
		};

		db.CajaMovimientos.Add(movimiento);
		db.SaveChanges();
		return movimiento.Id;
	}

	public List<CajaMovimientoDto> ListarMovimientos(int idCaja)
	{
		using var db = new PeluqueriaDbContext();
		return db.CajaMovimientos.AsNoTracking()
			.Where(m => m.IdCaja == idCaja)
			.OrderBy(m => m.Fecha)
			.ThenBy(m => m.Id)
			.Select(m => new CajaMovimientoDto
			{
				Id = m.Id,
				Fecha = m.Fecha,
				Tipo = m.Tipo,
				Concepto = m.Concepto,
				Monto = m.Monto,
				Usuario = m.Usuario.NombreUsuario,
			})
			.ToList();
	}

	// Anulaciones registradas durante el turno. No se suman al efectivo:
	// las ventas en efectivo anuladas ya se excluyen de VentasEfectivo.
	public List<AnulacionDto> ListarAnulaciones(DateTime desde, DateTime? hasta)
	{
		using var db = new PeluqueriaDbContext();
		IQueryable<Venta> consulta = db.Ventas.AsNoTracking()
			.Where(v => v.Anulada && v.FechaAnulacion != null && v.FechaAnulacion >= desde);

		if (hasta.HasValue)
			consulta = consulta.Where(v => v.FechaAnulacion <= hasta.Value);

		return consulta
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

	public List<ResumenVentasDto> ResumenVentas(DateTime desde, DateTime? hasta)
	{
		using var db = new PeluqueriaDbContext();
		IQueryable<Venta> consulta = db.Ventas.AsNoTracking()
			.Where(v => v.Fecha >= desde && !v.Anulada);

		if (hasta.HasValue)
			consulta = consulta.Where(v => v.Fecha <= hasta.Value);

		return consulta
			.GroupBy(v => v.MedioPago)
			.Select(g => new ResumenVentasDto
			{
				MedioPago = g.Key,
				Cantidad = g.Count(),
				Total = g.Sum(v => v.Total),
			})
			.OrderBy(x => x.MedioPago)
			.ToList();
	}

	public decimal VentasEfectivo(DateTime desde, DateTime? hasta)
	{
		using var db = new PeluqueriaDbContext();
		IQueryable<Venta> consulta = db.Ventas.AsNoTracking()
			.Where(v => v.Fecha >= desde && !v.Anulada && v.MedioPago == "efectivo");

		if (hasta.HasValue)
			consulta = consulta.Where(v => v.Fecha <= hasta.Value);

		return consulta.Sum(v => (decimal?)v.Total) ?? 0m;
	}

	public decimal TotalMovimientos(int idCaja, string tipo)
	{
		using var db = new PeluqueriaDbContext();
		return db.CajaMovimientos.AsNoTracking()
			.Where(m => m.IdCaja == idCaja && m.Tipo == tipo)
			.Sum(m => (decimal?)m.Monto) ?? 0m;
	}

	public List<CajaListadoDto> Listar(DateTime desde, DateTime hasta, int? limite = null, int? desplazamiento = null)
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

		IQueryable<CajaListadoDto> consulta = db.Cajas.AsNoTracking()
			.Where(c => c.FechaApertura >= inicio && c.FechaApertura < fin)
			.OrderByDescending(c => c.FechaApertura)
			.ThenByDescending(c => c.Id)
			.Select(c => new CajaListadoDto
			{
				Id = c.Id,
				Apertura = c.FechaApertura,
				UsuarioApertura = c.UsuarioApertura.NombreUsuario,
				Inicial = c.MontoInicial,
				Cierre = c.FechaCierre,
				UsuarioCierre = c.UsuarioCierre == null ? null : c.UsuarioCierre.NombreUsuario,
				Esperado = c.MontoEsperado,
				Contado = c.MontoContado,
				Diferencia = c.Diferencia,
				Estado = c.Estado,
				Observacion = c.Observacion,
			});

		return Comun.Paginar(consulta, limite, desplazamiento).ToList();
	}

	public int Contar(DateTime desde, DateTime hasta)
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);
		return db.Cajas.Count(c => c.FechaApertura >= inicio && c.FechaApertura < fin);
	}
}
