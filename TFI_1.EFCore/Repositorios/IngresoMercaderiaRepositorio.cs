using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Dtos;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class IngresoMercaderiaRepositorio
{
	public List<IngresoMercaderia> Listar(DateTime? desde = null, DateTime? hasta = null,
		string? filtro = null, int? limite = null, int? desplazamiento = null)
	{
		using var db = new PeluqueriaDbContext();
		IQueryable<IngresoMercaderia> consulta = db.IngresosMercaderia
			.AsNoTracking()
			.Include(i => i.Usuario)
			.Include(i => i.UsuarioAnulacion);

		if (desde.HasValue)
			consulta = consulta.Where(i => i.Fecha >= desde.Value.Date);
		if (hasta.HasValue)
			consulta = consulta.Where(i => i.Fecha < hasta.Value.Date.AddDays(1));

		if (!string.IsNullOrEmpty(filtro))
			consulta = consulta.Where(i => (i.Proveedor ?? string.Empty).Contains(filtro));

		consulta = consulta.OrderByDescending(i => i.Fecha).ThenByDescending(i => i.Id);
		return Comun.Paginar(consulta, limite, desplazamiento).ToList();
	}

	public int Contar(DateTime? desde = null, DateTime? hasta = null, string? filtro = null)
	{
		using var db = new PeluqueriaDbContext();
		return Filtrar(db.IngresosMercaderia.AsNoTracking(), desde, hasta, filtro).Count();
	}

	public decimal Sumar(DateTime? desde = null, DateTime? hasta = null, string? filtro = null)
	{
		using var db = new PeluqueriaDbContext();
		return Filtrar(db.IngresosMercaderia.AsNoTracking(), desde, hasta, filtro)
			.Where(i => !i.Anulada)
			.Sum(i => (decimal?)i.Total) ?? 0m;
	}

	public IngresoMercaderia? ObtenerPorId(int id)
	{
		using var db = new PeluqueriaDbContext();
		return db.IngresosMercaderia.AsNoTracking()
			.Include(i => i.Usuario)
			.Include(i => i.UsuarioAnulacion)
			.Include(i => i.Detalles)
				.ThenInclude(d => d.Item)
			.SingleOrDefault(i => i.Id == id);
	}

	public int Insertar(IngresoMercaderia ingreso)
	{
		using var db = new PeluqueriaDbContext();
		using var tx = db.Database.BeginTransaction();

		try
		{
			var nuevo = new IngresoMercaderia
			{
				Fecha = ingreso.Fecha,
				IdProveedor = ingreso.IdProveedor,
				Proveedor = ingreso.Proveedor,
				Total = ingreso.Total,
				IdUsuario = ingreso.IdUsuario,
				Observacion = ingreso.Observacion,
			};

			db.IngresosMercaderia.Add(nuevo);
			db.SaveChanges();

			foreach (IngresoMercaderiaDetalle detalle in ingreso.Detalles)
			{
				int idItem = detalle.IdItem != 0 ? detalle.IdItem : detalle.Item?.Id ?? 0;
				decimal subtotal = detalle.CostoUnitario * detalle.Cantidad;

				db.IngresosMercaderiaDetalle.Add(new IngresoMercaderiaDetalle
				{
					IdIngreso = nuevo.Id,
					IdItem = idItem,
					Cantidad = detalle.Cantidad,
					CostoUnitario = detalle.CostoUnitario,
					Subtotal = subtotal,
				});

				Item item = db.Items.Find(idItem)
					?? throw new InvalidOperationException("El producto no existe.");

				item.Stock = (item.Stock ?? 0) + detalle.Cantidad;
				if (detalle.Item != null)
					item.Precio = detalle.Item.Precio;
				item.Costo = detalle.CostoUnitario;
			}

			db.SaveChanges();
			tx.Commit();
			ingreso.Id = nuevo.Id;
			return nuevo.Id;
		}
		catch
		{
			tx.Rollback();
			throw;
		}
	}

	public void Anular(int id, int usuarioId, string motivo)
	{
		using var db = new PeluqueriaDbContext();
		using var tx = db.Database.BeginTransaction();

		try
		{
			IngresoMercaderia ingreso = db.IngresosMercaderia
				.Include(i => i.Detalles)
				.SingleOrDefault(i => i.Id == id)
				?? throw new InvalidOperationException("El ingreso no existe.");

			if (ingreso.Anulada)
				throw new InvalidOperationException("El ingreso ya fue anulado.");

			foreach (IngresoMercaderiaDetalle detalle in ingreso.Detalles)
			{
				Item item = db.Items.Find(detalle.IdItem)
					?? throw new InvalidOperationException("El producto del ingreso no existe.");
				item.Stock = (item.Stock ?? 0) - detalle.Cantidad;
			}

			ingreso.Anulada = true;
			ingreso.FechaAnulacion = DateTime.Now;
			ingreso.IdUsuarioAnulacion = usuarioId;
			ingreso.MotivoAnulacion = motivo;

			db.SaveChanges();
			tx.Commit();
		}
		catch
		{
			tx.Rollback();
			throw;
		}
	}

	public List<IngresoResumenDto> IngresosResumen(DateTime desde, DateTime hasta)
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

		return db.IngresosMercaderia.AsNoTracking()
			.Where(i => i.Fecha >= inicio && i.Fecha < fin && !i.Anulada)
			.GroupBy(i => i.Fecha.Date)
			.Select(g => new IngresoResumenDto
			{
				Fecha = g.Key,
				Cantidad = g.Count(),
				Total = g.Sum(x => x.Total),
			})
			.OrderBy(x => x.Fecha)
			.ToList();
	}

	private static IQueryable<IngresoMercaderia> Filtrar(IQueryable<IngresoMercaderia> consulta,
		DateTime? desde, DateTime? hasta, string? filtro)
	{
		if (desde.HasValue)
			consulta = consulta.Where(i => i.Fecha >= desde.Value.Date);
		if (hasta.HasValue)
			consulta = consulta.Where(i => i.Fecha < hasta.Value.Date.AddDays(1));
		if (!string.IsNullOrEmpty(filtro))
			consulta = consulta.Where(i => (i.Proveedor ?? string.Empty).Contains(filtro));

		return consulta;
	}
}
