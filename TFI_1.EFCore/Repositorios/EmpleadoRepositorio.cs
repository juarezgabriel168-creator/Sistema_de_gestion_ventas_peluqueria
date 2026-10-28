using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class EmpleadoRepositorio
{
	public List<Empleado> Listar(string? filtro = null)
	{
		using var db = new PeluqueriaDbContext();
		IQueryable<Empleado> consulta = db.Empleados.AsNoTracking();

		if (!string.IsNullOrEmpty(filtro))
			consulta = consulta.Where(e => e.Apellido.Contains(filtro) || e.Nombre.Contains(filtro));

		return consulta.OrderBy(e => e.Apellido).ThenBy(e => e.Nombre).ToList();
	}

	public int Insertar(Empleado empleado)
	{
		using var db = new PeluqueriaDbContext();
		var nuevo = new Empleado
		{
			Nombre = empleado.Nombre,
			Apellido = empleado.Apellido,
			Modalidad = empleado.Modalidad,
			Activo = empleado.Activo,
		};

		db.Empleados.Add(nuevo);
		db.SaveChanges();
		empleado.Id = nuevo.Id;
		return nuevo.Id;
	}

	public void Actualizar(Empleado empleado)
	{
		using var db = new PeluqueriaDbContext();
		Empleado actual = db.Empleados.Find(empleado.Id)
			?? throw new InvalidOperationException("El empleado no existe.");

		actual.Nombre = empleado.Nombre;
		actual.Apellido = empleado.Apellido;
		actual.Modalidad = empleado.Modalidad;
		actual.Activo = empleado.Activo;
		db.SaveChanges();
	}

	public void Desactivar(int id)
	{
		using var db = new PeluqueriaDbContext();
		db.Empleados.Where(e => e.Id == id).ExecuteUpdate(s => s.SetProperty(e => e.Activo, false));
	}

	public void Reactivar(int id)
	{
		using var db = new PeluqueriaDbContext();
		db.Empleados.Where(e => e.Id == id).ExecuteUpdate(s => s.SetProperty(e => e.Activo, true));
	}
}
