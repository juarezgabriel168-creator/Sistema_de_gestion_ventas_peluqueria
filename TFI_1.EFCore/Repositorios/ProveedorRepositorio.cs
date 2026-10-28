using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class ProveedorRepositorio
{
	public List<Proveedor> Listar(string? filtro = null)
	{
		using var db = new PeluqueriaDbContext();
		IQueryable<Proveedor> consulta = db.Proveedores.AsNoTracking();

		if (!string.IsNullOrEmpty(filtro))
			consulta = consulta.Where(p =>
				p.Nombre.Contains(filtro) ||
				(p.Cuit ?? string.Empty).Contains(filtro));

		return consulta.OrderBy(p => p.Nombre).ToList();
	}

	public Proveedor? ObtenerPorId(int id)
	{
		using var db = new PeluqueriaDbContext();
		return db.Proveedores.AsNoTracking().SingleOrDefault(p => p.Id == id);
	}

	public bool ExisteNombre(string nombre, int? exceptoId = null)
	{
		using var db = new PeluqueriaDbContext();
		return db.Proveedores.Any(p =>
			p.Nombre == nombre &&
			(exceptoId == null || p.Id != exceptoId));
	}

	public bool ExisteCuit(string cuit, int? exceptoId = null)
	{
		using var db = new PeluqueriaDbContext();
		return db.Proveedores.Any(p =>
			p.Cuit == cuit &&
			(exceptoId == null || p.Id != exceptoId));
	}

	public int Insertar(Proveedor proveedor)
	{
		using var db = new PeluqueriaDbContext();
		var nuevo = new Proveedor
		{
			Nombre = proveedor.Nombre,
			Cuit = Comun.Limpiar(proveedor.Cuit),
			Telefono = Comun.Limpiar(proveedor.Telefono),
			Email = Comun.Limpiar(proveedor.Email),
			Direccion = Comun.Limpiar(proveedor.Direccion),
			Activo = proveedor.Activo,
		};

		db.Proveedores.Add(nuevo);
		db.SaveChanges();
		proveedor.Id = nuevo.Id;
		return nuevo.Id;
	}

	public void Actualizar(Proveedor proveedor)
	{
		using var db = new PeluqueriaDbContext();
		Proveedor actual = db.Proveedores.Find(proveedor.Id)
			?? throw new InvalidOperationException("El proveedor no existe.");

		actual.Nombre = proveedor.Nombre;
		actual.Cuit = Comun.Limpiar(proveedor.Cuit);
		actual.Telefono = Comun.Limpiar(proveedor.Telefono);
		actual.Email = Comun.Limpiar(proveedor.Email);
		actual.Direccion = Comun.Limpiar(proveedor.Direccion);
		actual.Activo = proveedor.Activo;
		db.SaveChanges();
	}

	public void Desactivar(int id)
	{
		using var db = new PeluqueriaDbContext();
		db.Proveedores.Where(p => p.Id == id).ExecuteUpdate(s => s.SetProperty(p => p.Activo, false));
	}

	public void Reactivar(int id)
	{
		using var db = new PeluqueriaDbContext();
		db.Proveedores.Where(p => p.Id == id).ExecuteUpdate(s => s.SetProperty(p => p.Activo, true));
	}
}
