using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class TarjetaCreditoRepositorio
{
	public List<TarjetaCredito> Listar(bool soloActivas = true)
	{
		using var db = new PeluqueriaDbContext();
		IQueryable<TarjetaCredito> consulta = db.TarjetasCredito
			.AsNoTracking()
			.Include(t => t.Banco);

		if (soloActivas)
			consulta = consulta.Where(t => t.Activo && t.Banco.Activo);

		return consulta.OrderBy(t => t.Banco.Nombre).ThenBy(t => t.TipoTarjeta).ToList();
	}

	public List<TarjetaCredito> ListarPorBanco(int idBanco, bool soloActivas = true)
	{
		using var db = new PeluqueriaDbContext();
		IQueryable<TarjetaCredito> consulta = db.TarjetasCredito
			.AsNoTracking()
			.Include(t => t.Banco)
			.Where(t => t.IdBanco == idBanco);

		if (soloActivas)
			consulta = consulta.Where(t => t.Activo);

		return consulta.OrderBy(t => t.TipoTarjeta).ToList();
	}

	public TarjetaCredito? ObtenerPorId(int id)
	{
		using var db = new PeluqueriaDbContext();
		return db.TarjetasCredito
			.AsNoTracking()
			.Include(t => t.Banco)
			.SingleOrDefault(t => t.Id == id);
	}

	public bool ExisteTipo(int idBanco, string tipo, int? exceptoId = null)
	{
		using var db = new PeluqueriaDbContext();
		return db.TarjetasCredito.Any(t =>
			t.IdBanco == idBanco &&
			t.TipoTarjeta == tipo &&
			(exceptoId == null || t.Id != exceptoId));
	}

	public int Insertar(TarjetaCredito tarjeta)
	{
		using var db = new PeluqueriaDbContext();
		var nueva = new TarjetaCredito
		{
			IdBanco = tarjeta.IdBanco,
			TipoTarjeta = tarjeta.TipoTarjeta,
			PorcentajeRecargo = tarjeta.PorcentajeRecargo,
			CuotasMax = tarjeta.CuotasMax,
			Activo = tarjeta.Activo,
			FechaAlta = DateTime.Now,
		};

		db.TarjetasCredito.Add(nueva);
		db.SaveChanges();
		tarjeta.Id = nueva.Id;
		return nueva.Id;
	}

	public void Actualizar(TarjetaCredito tarjeta)
	{
		using var db = new PeluqueriaDbContext();
		TarjetaCredito actual = db.TarjetasCredito.Find(tarjeta.Id)
			?? throw new InvalidOperationException("La tarjeta no existe.");

		actual.IdBanco = tarjeta.IdBanco;
		actual.TipoTarjeta = tarjeta.TipoTarjeta;
		actual.PorcentajeRecargo = tarjeta.PorcentajeRecargo;
		actual.CuotasMax = tarjeta.CuotasMax;
		actual.Activo = tarjeta.Activo;
		db.SaveChanges();
	}

	public void Desactivar(int id)
	{
		using var db = new PeluqueriaDbContext();
		db.TarjetasCredito.Where(t => t.Id == id).ExecuteUpdate(s => s.SetProperty(t => t.Activo, false));
	}

	public void Reactivar(int id)
	{
		using var db = new PeluqueriaDbContext();
		db.TarjetasCredito.Where(t => t.Id == id).ExecuteUpdate(s => s.SetProperty(t => t.Activo, true));
	}
}
