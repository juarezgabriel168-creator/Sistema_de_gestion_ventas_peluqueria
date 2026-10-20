using System;
using System.Collections.Generic;
using TFI_1.Datos;
using TFI_1.Models;

namespace TFI_1.Negocio;

public class ItemNegocio
{
	private readonly ItemDatos _datos = new ItemDatos();

	private readonly CategoriaNegocio _categoriaNegocio = new CategoriaNegocio();

	public List<Item> Listar(string? filtro = null, int? categoriaId = null, TipoItem? tipo = null)
	{
		return _datos.Listar(filtro, categoriaId, tipo);
	}

	public List<Categoria> ListarCategorias()
	{
		return _categoriaNegocio.Listar();
	}

	public Item Guardar(Item item)
	{
		if (item == null)
		{
			throw new ArgumentException("El ítem es obligatorio.");
		}
		if (string.IsNullOrWhiteSpace(item.Nombre))
		{
			throw new ArgumentException("El nombre del ítem es obligatorio.");
		}
		if (item.Precio < 0m)
		{
			throw new ArgumentException("El precio no puede ser negativo.");
		}
		if (item.Costo.HasValue && item.Costo.Value < 0m)
		{
			throw new ArgumentException("El costo no puede ser negativo.");
		}
		if (item.Categoria == null || item.Categoria.Id <= 0)
		{
			throw new ArgumentException("Debe seleccionar una categoría.");
		}
		if (item.Tipo == TipoItem.Producto)
		{
			if (!item.Stock.HasValue || item.Stock < 0)
			{
				throw new ArgumentException("El stock es obligatorio y no puede ser negativo.");
			}
			if (!item.StockMinimo.HasValue || item.StockMinimo < 0)
			{
				throw new ArgumentException("El stock mínimo es obligatorio y no puede ser negativo.");
			}
			item.DuracionMin = null;
		}
		else
		{
			if (!item.DuracionMin.HasValue || item.DuracionMin <= 0)
			{
				throw new ArgumentException("La duración es obligatoria para los servicios y debe ser mayor a cero.");
			}
			item.Stock = null;
			item.StockMinimo = null;
			// Un servicio no tiene costo de compra: ese dato es de los productos.
			item.Costo = null;
		}
		int? exceptoId = ((item.Id == 0) ? ((int?)null) : new int?(item.Id));
		if (_datos.ExisteNombre(item.Nombre, exceptoId))
		{
			throw new ArgumentException("Ya existe un ítem llamado '" + item.Nombre + "'.");
		}
		if (item.Id == 0)
		{
			item.Id = _datos.Insertar(item);
		}
		else
		{
			_datos.Actualizar(item);
		}
		return item;
	}

	public void Desactivar(int id)
	{
		if (id <= 0)
		{
			throw new ArgumentException("Debe seleccionar un ítem.");
		}
		_datos.Desactivar(id);
	}

	public void Reactivar(int id)
	{
		if (id <= 0)
		{
			throw new ArgumentException("Debe seleccionar un ítem.");
		}
		_datos.Reactivar(id);
	}
}
