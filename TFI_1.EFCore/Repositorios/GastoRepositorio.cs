using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Dtos;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class GastoRepositorio
{
	public List<Gasto> Listar(DateTime? desde = null, DateTime? hasta = null, string? filtro = null)
	{
		using var db = new PeluqueriaDbContext();
		IQueryable<Gasto> consulta = db.Gastos.AsNoTracking().Include(g => g.Usuario);

		// Igual que el original: no usa DATE(), compara contra la medianoche.
		if (desde.HasValue)
			consulta = consulta.Where(g => g.Fecha >= desde.Value.Date);
		if (hasta.HasValue)
			consulta = consulta.Where(g => g.Fecha <= hasta.Value.Date);

		if (!string.IsNullOrEmpty(filtro))
			consulta = consulta.Where(g =>
				g.Concepto.Contains(filtro) ||
				(g.Categoria ?? string.Empty).Contains(filtro));

		return consulta.OrderByDescending(g => g.Fecha).ThenByDescending(g => g.Id).ToList();
	}

	public Gasto? ObtenerPorId(int id)
	{
		using var db = new PeluqueriaDbContext();
		return db.Gastos.AsNoTracking()
			.Include(g => g.Usuario)
			.SingleOrDefault(g => g.Id == id);
	}

	public int Insertar(Gasto gasto)
	{
		using var db = new PeluqueriaDbContext();
		var nuevo = new Gasto
		{
			Fecha = gasto.Fecha,
			Concepto = gasto.Concepto,
			Monto = gasto.Monto,
			Categoria = Comun.Limpiar(gasto.Categoria),
			Observacion = Comun.Limpiar(gasto.Observacion),
			IdUsuario = gasto.IdUsuario,
		};

		db.Gastos.Add(nuevo);
		db.SaveChanges();
		gasto.Id = nuevo.Id;
		return nuevo.Id;
	}

	public void Actualizar(Gasto gasto)
	{
		using var db = new PeluqueriaDbContext();
		Gasto actual = db.Gastos.Find(gasto.Id)
			?? throw new InvalidOperationException("El gasto no existe.");

		actual.Fecha = gasto.Fecha;
		actual.Concepto = gasto.Concepto;
		actual.Monto = gasto.Monto;
		actual.Categoria = Comun.Limpiar(gasto.Categoria);
		actual.Observacion = Comun.Limpiar(gasto.Observacion);
		db.SaveChanges();
	}

	public decimal TotalGastos(DateTime desde, DateTime hasta)
	{
		using var db = new PeluqueriaDbContext();
		return db.Gastos.AsNoTracking()
			.Where(g => g.Fecha >= desde.Date && g.Fecha <= hasta.Date)
			.Sum(g => (decimal?)g.Monto) ?? 0m;
	}

	public List<GastoCategoriaDto> GastosPorCategoria(DateTime desde, DateTime hasta)
	{
		using var db = new PeluqueriaDbContext();
		return db.Gastos.AsNoTracking()
			.Where(g => g.Fecha >= desde.Date && g.Fecha <= hasta.Date)
			.GroupBy(g => g.Categoria)
			.Select(g => new GastoCategoriaDto
			{
				Categoria = g.Key ?? "Sin categoría",
				Cantidad = g.Count(),
				Total = g.Sum(x => x.Monto),
			})
			.OrderByDescending(x => x.Total)
			.ToList();
	}
}
