using System;
using System.Collections.Generic;
using System.Linq;
using TFI_1.Datos;
using TFI_1.Models;

namespace TFI_1.Negocio;

public class ClienteNegocio
{
	private readonly ClienteDatos _datos = new ClienteDatos();

	private readonly AuditoriaNegocio _auditoria = new();

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

	/// <summary>
	/// Elimina físicamente un cliente. No se permite borrar al Consumidor
	/// Final ni a clientes con ventas, cuenta corriente o turnos asociados.
	/// </summary>
	public void Eliminar(int id)
	{
		if (id <= 0)
		{
			throw new ArgumentException("Debe seleccionar un cliente.");
		}
		if (!Sesion.EsAdministrador)
		{
			throw new InvalidOperationException("Solo un administrador puede eliminar clientes.");
		}
		if (_datos.EsConsumidorFinal(id))
		{
			throw new InvalidOperationException(
				"No se puede eliminar el cliente 'Consumidor Final' porque se usa en las ventas sin cliente identificado.");
		}
		if (_datos.TieneDependencias(id))
		{
			throw new InvalidOperationException(
				"No se puede eliminar el cliente porque tiene ventas, cuenta corriente o turnos asociados. " +
				"Desactívelo en su lugar.");
		}

		_datos.Eliminar(id);
		_auditoria.Registrar("cliente_eliminacion", "clientes", id, $"Cliente con id {id} eliminado.");
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
