using System;
using System.Collections.Generic;
using TFI_1.Datos;
using TFI_1.Models;

namespace TFI_1.Negocio;

public class CategoriaNegocio
{
	private readonly CategoriaDatos _datos = new CategoriaDatos();

	public List<Categoria> Listar(string? filtro = null)
	{
		return _datos.Listar(filtro);
	}

	public Categoria Guardar(Categoria categoria)
	{
		if (categoria == null)
		{
			throw new ArgumentException("La categoría es obligatoria.");
		}
		if (string.IsNullOrWhiteSpace(categoria.Nombre))
		{
			throw new ArgumentException("El nombre de la categoría es obligatorio.");
		}
		int? exceptoId = ((categoria.Id == 0) ? ((int?)null) : new int?(categoria.Id));
		if (_datos.ExisteNombre(categoria.Nombre, exceptoId))
		{
			throw new ArgumentException("Ya existe una categoría llamada '" + categoria.Nombre + "'.");
		}
		if (categoria.Id == 0)
		{
			categoria.Id = _datos.Insertar(categoria);
		}
		else
		{
			_datos.Actualizar(categoria);
		}
		return categoria;
	}

	public void Desactivar(int id)
	{
		if (id <= 0)
		{
			throw new ArgumentException("Debe seleccionar una categoría.");
		}
		_datos.Desactivar(id);
	}

	public void Reactivar(int id)
	{
		if (id <= 0)
		{
			throw new ArgumentException("Debe seleccionar una categoría.");
		}
		_datos.Reactivar(id);
	}
}
