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

		Validaciones.ExigirTexto(proveedor.Nombre, "El nombre del proveedor es obligatorio.");
		Validaciones.ExigirEmail(proveedor.Email);
		Validaciones.ExigirLongitudMaxima(proveedor.Nombre, 150,
			"El nombre del proveedor no puede superar los 150 caracteres.");
		Validaciones.ExigirLongitudMaxima(proveedor.Cuit, 20,
			"El CUIT no puede superar los 20 caracteres.");
		Validaciones.ExigirLongitudMaxima(proveedor.Telefono, 40,
			"El teléfono no puede superar los 40 caracteres.");
		Validaciones.ExigirLongitudMaxima(proveedor.Email, 120,
			"El email no puede superar los 120 caracteres.");
		Validaciones.ExigirLongitudMaxima(proveedor.Direccion, 200,
			"La dirección no puede superar los 200 caracteres.");

		int? exceptoId = (proveedor.Id == 0) ? null : new int?(proveedor.Id);
		if (_datos.ExisteNombre(proveedor.Nombre, exceptoId))
		{
			throw new ArgumentException("Ya existe un proveedor llamado '" + proveedor.Nombre + "'.");
		}
		if (!Validaciones.TextoVacio(proveedor.Cuit) && _datos.ExisteCuit(proveedor.Cuit, exceptoId))
		{
			throw new ArgumentException("Ya existe un proveedor con el CUIT '" + proveedor.Cuit + "'.");
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
