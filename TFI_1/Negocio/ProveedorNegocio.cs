using System;
using System.Collections.Generic;
using TFI_1.Datos;
using TFI_1.Models;

namespace TFI_1.Negocio;

public class ProveedorNegocio
{
	private readonly ProveedorDatos _datos = new();

	public List<Proveedor> Listar(string? filtro = null)
	{
		return _datos.Listar(filtro);
	}

	public Proveedor? ObtenerPorId(int id)
	{
		return _datos.ObtenerPorId(id);
	}

	public Proveedor Guardar(Proveedor proveedor)
	{
		if (proveedor == null)
		{
			throw new ArgumentException("El proveedor es obligatorio.");
		}

		proveedor.Nombre = (proveedor.Nombre ?? string.Empty).Trim();
		proveedor.Cuit = Limpiar(proveedor.Cuit);
		proveedor.Telefono = Limpiar(proveedor.Telefono);
		proveedor.Email = Limpiar(proveedor.Email);
		proveedor.Direccion = Limpiar(proveedor.Direccion);

		if (string.IsNullOrWhiteSpace(proveedor.Nombre))
		{
			throw new ArgumentException("El nombre del proveedor es obligatorio.");
		}
		if (proveedor.Email != null && (!proveedor.Email.Contains('@') || !proveedor.Email.Contains('.')))
		{
			throw new ArgumentException("El email no parece válido.");
		}

		int? exceptoId = (proveedor.Id == 0) ? null : new int?(proveedor.Id);
		if (_datos.ExisteNombre(proveedor.Nombre, exceptoId))
		{
			throw new ArgumentException("Ya existe un proveedor llamado '" + proveedor.Nombre + "'.");
		}

		if (proveedor.Id == 0)
		{
			proveedor.Id = _datos.Insertar(proveedor);
		}
		else
		{
			_datos.Actualizar(proveedor);
		}
		return proveedor;
	}

	public void Desactivar(int id)
	{
		_datos.Desactivar(id);
	}

	public void Reactivar(int id)
	{
		_datos.Reactivar(id);
	}

	private static string? Limpiar(string? valor)
	{
		return string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
	}
}
