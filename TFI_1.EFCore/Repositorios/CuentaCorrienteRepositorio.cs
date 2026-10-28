using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Dtos;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class CuentaCorrienteRepositorio
{
	public CuentaCorriente? ObtenerPorCliente(int idCliente)
	{
		using var db = new PeluqueriaDbContext();
		return db.CuentasCorrientes.AsNoTracking().SingleOrDefault(c => c.IdCliente == idCliente);
	}

	public int CrearSiNoExiste(int idCliente)
	{
		using var db = new PeluqueriaDbContext();
		CuentaCorriente? existente = db.CuentasCorrientes.AsNoTracking()
			.SingleOrDefault(c => c.IdCliente == idCliente);

		if (existente != null)
			return existente.Id;

		var nueva = new CuentaCorriente
		{
			IdCliente = idCliente,
			Saldo = 0m,
			FechaActualizacion = DateTime.Now,
		};
		db.CuentasCorrientes.Add(nueva);
		db.SaveChanges();
		return nueva.Id;
	}

	public decimal ObtenerSaldo(int idCliente)
	{
		using var db = new PeluqueriaDbContext();
		return db.CuentasCorrientes
			.Where(c => c.IdCliente == idCliente)
			.Select(c => (decimal?)c.Saldo)
			.SingleOrDefault() ?? 0m;
	}

	public void ActualizarSaldo(int idCuenta, decimal nuevoSaldo)
	{
		using var db = new PeluqueriaDbContext();
		db.CuentasCorrientes
			.Where(c => c.Id == idCuenta)
			.ExecuteUpdate(s => s.SetProperty(c => c.Saldo, nuevoSaldo));
	}

	public List<MovimientoCuentaCorriente> ListarMovimientos(int idCuenta, DateTime? desde = null, DateTime? hasta = null)
	{
		using var db = new PeluqueriaDbContext();
		IQueryable<MovimientoCuentaCorriente> consulta = db.MovimientosCuentaCorriente
			.AsNoTracking()
			.Where(m => m.IdCuenta == idCuenta);

		if (desde.HasValue)
			consulta = consulta.Where(m => m.Fecha >= desde.Value.Date);
		if (hasta.HasValue)
			consulta = consulta.Where(m => m.Fecha < hasta.Value.Date.AddDays(1));

		return consulta.OrderByDescending(m => m.Fecha).ThenByDescending(m => m.Id).ToList();
	}

	public int InsertarMovimiento(MovimientoCuentaCorriente movimiento)
	{
		using var db = new PeluqueriaDbContext();
		var nuevo = new MovimientoCuentaCorriente
		{
			IdCuenta = movimiento.IdCuenta,
			Fecha = movimiento.Fecha,
			Tipo = movimiento.Tipo,
			Monto = movimiento.Monto,
			Concepto = movimiento.Concepto,
			IdVenta = movimiento.IdVenta,
			IdUsuario = movimiento.IdUsuario,
		};

		db.MovimientosCuentaCorriente.Add(nuevo);
		db.SaveChanges();
		movimiento.Id = nuevo.Id;
		return nuevo.Id;
	}

	/// <summary>Registra el abono y descuenta el saldo dentro de una transacción.</summary>
	public int RegistrarAbono(int idCliente, decimal monto, string? concepto, int idUsuario, DateTime fecha)
	{
		using var db = new PeluqueriaDbContext();
		using var tx = db.Database.BeginTransaction();

		try
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

			var movimiento = new MovimientoCuentaCorriente
			{
				IdCuenta = cuenta.Id,
				Fecha = fecha,
				Tipo = TipoMovimientoCuentaCorriente.Abono,
				Monto = monto,
				Concepto = concepto,
				IdVenta = null,
				IdUsuario = idUsuario,
			};

			db.MovimientosCuentaCorriente.Add(movimiento);
			cuenta.Saldo -= monto;
			cuenta.FechaActualizacion = DateTime.Now;
			db.SaveChanges();

			tx.Commit();
			return movimiento.Id;
		}
		catch
		{
			tx.Rollback();
			throw;
		}
	}

	public List<MovimientoCuentaCorrienteDto> MovimientosResumen(int idCliente, DateTime desde, DateTime hasta)
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

		return db.MovimientosCuentaCorriente.AsNoTracking()
			.Where(m => m.Cuenta.IdCliente == idCliente && m.Fecha >= inicio && m.Fecha < fin)
			.OrderByDescending(m => m.Fecha)
			.Select(m => new MovimientoCuentaCorrienteDto
			{
				Fecha = m.Fecha,
				Tipo = m.Tipo == TipoMovimientoCuentaCorriente.Cargo ? "cargo" : "abono",
				Monto = m.Monto,
				Concepto = m.Concepto,
				IdVenta = m.IdVenta,
				NombreUsuario = m.Usuario.NombreUsuario,
			})
			.ToList();
	}
}
