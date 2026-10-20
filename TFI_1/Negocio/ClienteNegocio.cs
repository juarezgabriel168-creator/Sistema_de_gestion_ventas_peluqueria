using System;
using System.Collections.Generic;
using System.Linq;
using TFI_1.Datos;
using TFI_1.Models;

namespace TFI_1.Negocio;

public class ClienteNegocio
{
	private readonly ClienteDatos _datos = new ClienteDatos();

	public List<Cliente> Listar(string? filtro = null)
	{
		return _datos.Listar(filtro);
	}

	public Cliente Guardar(Cliente cliente)
	{
		if (cliente == null)
		{
			throw new ArgumentException("El cliente es obligatorio.");
		}
		Validaciones.ExigirTexto(cliente.Nombre, "El nombre es obligatorio.");
		Validaciones.ExigirTexto(cliente.Apellido, "El apellido es obligatorio.");
		Validaciones.ExigirDni(cliente.Dni);
		int? exceptoId = ((cliente.Id == 0) ? ((int?)null) : new int?(cliente.Id));
		if (_datos.ExisteDni(cliente.Dni, exceptoId))
		{
			throw new ArgumentException("Ya existe un cliente con el DNI " + cliente.Dni + ".");
		}
		if (cliente.Id == 0)
		{
			cliente.Id = _datos.Insertar(cliente);
		}
		else
		{
			_datos.Actualizar(cliente);
		}
		return cliente;
	}

	public void Desactivar(int id)
	{
		if (id <= 0)
		{
			throw new ArgumentException("Debe seleccionar un cliente.");
		}
		_datos.Desactivar(id);
	}

	public void Reactivar(int id)
	{
		if (id <= 0)
		{
			throw new ArgumentException("Debe seleccionar un cliente.");
		}
		_datos.Reactivar(id);
	}

	public Cliente ObtenerOCrearConsumidorFinal()
	{
		const string dniCf = "00000000";
		var existente = _datos.ObtenerPorDni(dniCf);
		if (existente != null && existente.Activo)
			return existente;
		if (existente != null && !existente.Activo)
		{
			existente.Activo = true;
			_datos.Actualizar(existente);
			return existente;
		}
		var cf = new Cliente
		{
			Nombre = "Consumidor",
			Apellido = "Final",
			Dni = dniCf,
			Telefono = string.Empty,
			Activo = true
		};
		return Guardar(cf);
	}
}
