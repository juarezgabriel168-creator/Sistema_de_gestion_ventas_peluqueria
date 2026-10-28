using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class ItemRepositorio
{
	public List<Item> Listar(string? filtro = null, int? categoriaId = null, TipoItem? tipo = null,
		bool soloStockBajo = false)
	{
		using var db = new PeluqueriaDbContext();
		IQueryable<Item> consulta = db.Items.AsNoTracking().Include(i => i.Categoria);

		if (!string.IsNullOrEmpty(filtro))
			consulta = consulta.Where(i => i.Nombre.Contains(filtro));
		if (categoriaId.HasValue)
			consulta = consulta.Where(i => i.IdCategoria == categoriaId.Value);
		if (tipo.HasValue)
			consulta = consulta.Where(i => i.Tipo == tipo.Value);
		if (soloStockBajo)
			consulta = consulta.Where(i => i.Tipo == TipoItem.Producto && i.Stock <= i.StockMinimo);

		return consulta.OrderBy(i => i.Nombre).ToList();
	}

	public Item? ObtenerPorId(int id)
	{
		using var db = new PeluqueriaDbContext();
		return db.Items
			.AsNoTracking()
			.Include(i => i.Categoria)
			.SingleOrDefault(i => i.Id == id);
	}

	public bool ExisteNombre(string nombre, int? exceptoId = null)
	{
		using var db = new PeluqueriaDbContext();
		return db.Items.Any(i =>
			i.Nombre == nombre &&
			(exceptoId == null || i.Id != exceptoId));
	}

	public int Insertar(Item item)
	{
		using var db = new PeluqueriaDbContext();
		var nuevo = new Item
		{
			Nombre = item.Nombre,
			Tipo = item.Tipo,
			Precio = item.Precio,
			IdCategoria = item.IdCategoria != 0 ? item.IdCategoria : item.Categoria.Id,
			Activo = item.Activo,
			Costo = item.Costo,
			DuracionMin = item.Tipo == TipoItem.Servicio ? item.DuracionMin : null,
			Stock = item.Tipo == TipoItem.Producto ? item.Stock : null,
			StockMinimo = item.Tipo == TipoItem.Producto ? item.StockMinimo : null,
		};

		db.Items.Add(nuevo);
		db.SaveChanges();
		item.Id = nuevo.Id;
		return nuevo.Id;
	}

	public void Actualizar(Item item)
	{
		using var db = new PeluqueriaDbContext();
		Item actual = db.Items.Find(item.Id)
			?? throw new InvalidOperationException("El ítem no existe.");

		actual.Nombre = item.Nombre;
		actual.Tipo = item.Tipo;
		actual.Precio = item.Precio;
		actual.Costo = item.Costo;
		actual.IdCategoria = item.IdCategoria != 0 ? item.IdCategoria : item.Categoria.Id;
		actual.Activo = item.Activo;
		actual.DuracionMin = item.Tipo == TipoItem.Servicio ? item.DuracionMin : null;
		actual.Stock = item.Tipo == TipoItem.Producto ? item.Stock : null;
		actual.StockMinimo = item.Tipo == TipoItem.Producto ? item.StockMinimo : null;
		db.SaveChanges();
	}

	public void Desactivar(int id)
	{
		using var db = new PeluqueriaDbContext();
		db.Items.Where(i => i.Id == id).ExecuteUpdate(s => s.SetProperty(i => i.Activo, false));
	}

	public void Reactivar(int id)
	{
		using var db = new PeluqueriaDbContext();
		db.Items.Where(i => i.Id == id).ExecuteUpdate(s => s.SetProperty(i => i.Activo, true));
	}

	public bool TieneDependencias(int id)
	{
		using var db = new PeluqueriaDbContext();
		return db.DetallesVenta.Any(d => d.IdItem == id)
			|| db.IngresosMercaderiaDetalle.Any(d => d.IdItem == id)
			|| db.Atenciones.Any(a => a.IdItem == id);
	}

	public void Eliminar(int id)
	{
		using var db = new PeluqueriaDbContext();
		Item? item = db.Items.Find(id);
		if (item == null)
			return;

		db.Items.Remove(item);
		db.SaveChanges();
	}
}
