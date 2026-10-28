using System;
using System.Collections.Generic;
using TFI_1.Datos;
using TFI_1.Models;

namespace TFI_1.Negocio;

public class EmpleadoNegocio
{
	private readonly EmpleadoDatos _datos = new EmpleadoDatos();
	private readonly AuditoriaNegocio _auditoria = new();

	public List<Empleado> Listar(string? filtro = null)
	{
		return _datos.Listar(filtro);
	}

	public Empleado Guardar(Empleado empleado)
	{
		if (empleado == null)
		{
			throw new ArgumentException("El empleado es obligatorio.");
		}
		if (string.IsNullOrWhiteSpace(empleado.Nombre))
		{
			throw new ArgumentException("El nombre es obligatorio.");
		}
		if (string.IsNullOrWhiteSpace(empleado.Apellido))
		{
			throw new ArgumentException("El apellido es obligatorio.");
		}
		if (empleado.Id == 0)
		{
			empleado.Id = _datos.Insertar(empleado);
		}
		else
		{
			_datos.Actualizar(empleado);
		}
		return empleado;
	}

	public void Desactivar(int id)
	{
		if (id <= 0)
		{
			throw new ArgumentException("Debe seleccionar un empleado.");
		}
		_datos.Desactivar(id);
	}

	public void Reactivar(int id)
	{
		if (id <= 0)
		{
			throw new ArgumentException("Debe seleccionar un empleado.");
		}
		_datos.Reactivar(id);
	}

	/// <summary>
	/// Elimina físicamente un empleado. Solo se permite si no tiene ventas
	/// ni atenciones asociadas, para no romper el historial.
	/// </summary>
	public void Eliminar(int id)
	{
		if (id <= 0)
		{
			throw new ArgumentException("Debe seleccionar un empleado.");
		}
		if (!Sesion.EsAdministrador)
		{
			throw new InvalidOperationException("Solo un administrador puede eliminar empleados.");
		}
		if (_datos.TieneDependencias(id))
		{
			throw new InvalidOperationException(
				"No se puede eliminar el empleado porque tiene ventas o atenciones asociadas. " +
				"Desactívelo en su lugar.");
		}

		_datos.Eliminar(id);
		_auditoria.Registrar("empleado_eliminacion", "empleados", id, $"Empleado con id {id} eliminado.");
	}
}
