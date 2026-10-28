using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class BancoRepositorio
{
	public List<Banco> Listar(bool soloActivos = true)
	{
		using var db = new PeluqueriaDbContext();
		IQueryable<Banco> consulta = db.Bancos.AsNoTracking();

		if (soloActivos)
			consulta = consulta.Where(b => b.Activo);

		return consulta.OrderBy(b => b.Nombre).ToList();
	}

	public bool ExisteNombre(string nombre, int? exceptoId = null)
	{
		using var db = new PeluqueriaDbContext();
		return db.Bancos.Any(b =>
			b.Nombre == nombre &&
			(exceptoId == null || b.Id != exceptoId));
	}

	public int Insertar(Banco banco)
	{
		using var db = new PeluqueriaDbContext();
		var nuevo = new Banco { Nombre = banco.Nombre, Activo = banco.Activo };
		db.Bancos.Add(nuevo);
		db.SaveChanges();
		banco.Id = nuevo.Id;
		return nuevo.Id;
	}

	public void Actualizar(Banco banco)
	{
		using var db = new PeluqueriaDbContext();
		using var tx = db.Database.BeginTransaction();
		Banco actual = db.Bancos.Find(banco.Id)
			?? throw new InvalidOperationException("El banco no existe.");

		// Si el estado cambia, las tarjetas acompañan (igual que Desactivar/Reactivar).
		bool cambioEstado = actual.Activo != banco.Activo;

		actual.Nombre = banco.Nombre;
		actual.Activo = banco.Activo;

		if (cambioEstado)
			db.TarjetasCredito
				.Where(t => t.IdBanco == banco.Id)
				.ExecuteUpdate(s => s.SetProperty(t => t.Activo, banco.Activo));

		db.SaveChanges();
		tx.Commit();
	}

	public void Desactivar(int id)
	{
		using var db = new PeluqueriaDbContext();
		using var tx = db.Database.BeginTransaction();
		db.Bancos.Where(b => b.Id == id).ExecuteUpdate(s => s.SetProperty(b => b.Activo, false));
		db.TarjetasCredito.Where(t => t.IdBanco == id).ExecuteUpdate(s => s.SetProperty(t => t.Activo, false));
		tx.Commit();
	}

	public void Reactivar(int id)
	{
		using var db = new PeluqueriaDbContext();
		using var tx = db.Database.BeginTransaction();
		db.Bancos.Where(b => b.Id == id).ExecuteUpdate(s => s.SetProperty(b => b.Activo, true));
		db.TarjetasCredito.Where(t => t.IdBanco == id).ExecuteUpdate(s => s.SetProperty(t => t.Activo, true));
		tx.Commit();
	}

	public bool TieneTarjetas(int id)
	{
		using var db = new PeluqueriaDbContext();
		return db.TarjetasCredito.Any(t => t.IdBanco == id);
	}

	// Solo se elimina un banco sin tarjetas: las tarjetas pueden estar usadas en ventas.
	public void Eliminar(int id)
	{
		using var db = new PeluqueriaDbContext();
		Banco banco = db.Bancos.Find(id)
			?? throw new InvalidOperationException("El banco no existe.");
		if (db.TarjetasCredito.Any(t => t.IdBanco == id))
			throw new InvalidOperationException(
				"No se puede eliminar el banco porque tiene tarjetas asociadas. Desactívelo en su lugar.");
		db.Bancos.Remove(banco);
		db.SaveChanges();
	}
}
