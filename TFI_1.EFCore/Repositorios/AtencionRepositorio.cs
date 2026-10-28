using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class AtencionRepositorio
{
	public List<Atencion> Listar(string? filtro = null, string? estado = null)
	{
		using var db = new PeluqueriaDbContext();
		IQueryable<Atencion> consulta = db.Atenciones.AsNoTracking()
			.Include(a => a.Cliente)
			.Include(a => a.Empleado)
			.Include(a => a.Servicio);

		if (!string.IsNullOrEmpty(filtro))
			consulta = consulta.Where(a =>
				a.Cliente.Nombre.Contains(filtro) ||
				a.Cliente.Apellido.Contains(filtro) ||
				a.Servicio.Nombre.Contains(filtro));

		if (!string.IsNullOrWhiteSpace(estado))
		{
			EstadoAtencion valor = estado switch
			{
				"en_atencion" => EstadoAtencion.EnAtencion,
				"finalizada" => EstadoAtencion.Finalizada,
				"cancelada" => EstadoAtencion.Cancelada,
				_ => EstadoAtencion.Pendiente,
			};
			consulta = consulta.Where(a => a.Estado == valor);
		}

		return consulta.OrderByDescending(a => a.FechaHora).ToList();
	}

	public int Insertar(Atencion atencion)
	{
		using var db = new PeluqueriaDbContext();
		var nueva = new Atencion
		{
			IdCliente = atencion.IdCliente != 0 ? atencion.IdCliente : atencion.Cliente.Id,
			IdEmpleado = atencion.IdEmpleado != 0 ? atencion.IdEmpleado : atencion.Empleado.Id,
			IdItem = atencion.IdItem != 0 ? atencion.IdItem : atencion.Servicio.Id,
			Tipo = atencion.Tipo,
			FechaHora = atencion.FechaHora,
			Estado = atencion.Estado,
		};

		db.Atenciones.Add(nueva);
		db.SaveChanges();
		atencion.Id = nueva.Id;
		return nueva.Id;
	}

	public void Actualizar(Atencion atencion)
	{
		using var db = new PeluqueriaDbContext();
		Atencion actual = db.Atenciones.Find(atencion.Id)
			?? throw new InvalidOperationException("La atención no existe.");

		actual.IdCliente = atencion.IdCliente != 0 ? atencion.IdCliente : atencion.Cliente.Id;
		actual.IdEmpleado = atencion.IdEmpleado != 0 ? atencion.IdEmpleado : atencion.Empleado.Id;
		actual.IdItem = atencion.IdItem != 0 ? atencion.IdItem : atencion.Servicio.Id;
		actual.Tipo = atencion.Tipo;
		actual.FechaHora = atencion.FechaHora;
		actual.Estado = atencion.Estado;
		db.SaveChanges();
	}

	public void CambiarEstado(int id, EstadoAtencion estado)
	{
		using var db = new PeluqueriaDbContext();
		db.Atenciones.Where(a => a.Id == id).ExecuteUpdate(s => s.SetProperty(a => a.Estado, estado));
	}

	public bool ExisteConflictoHorario(int idEmpleado, DateTime fechaHora, int duracionMin, int? exceptoId = null)
	{
		using var db = new PeluqueriaDbContext();
		DateTime fin = fechaHora.AddMinutes(Math.Max(duracionMin, 1));

		return db.Atenciones.Any(a =>
			a.IdEmpleado == idEmpleado &&
			a.Estado != EstadoAtencion.Cancelada &&
			(exceptoId == null || a.Id != exceptoId) &&
			fechaHora < a.FechaHora.AddMinutes(Math.Max(a.Servicio.DuracionMin ?? 0, 1)) &&
			a.FechaHora < fin);
	}

	public int CantidadHoy()
	{
		using var db = new PeluqueriaDbContext();
		DateTime inicio = DateTime.Today;
		DateTime fin = inicio.AddDays(1);

		return db.Atenciones.Count(a =>
			a.FechaHora >= inicio &&
			a.FechaHora < fin &&
			a.Estado != EstadoAtencion.Cancelada);
	}

	public void Eliminar(int id)
	{
		using var db = new PeluqueriaDbContext();
		Atencion? atencion = db.Atenciones.Find(id);
		if (atencion == null)
			return;

		db.Atenciones.Remove(atencion);
		db.SaveChanges();
	}
}
