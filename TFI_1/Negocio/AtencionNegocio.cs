using System;
using System.Collections.Generic;
using TFI_1.Datos;
using TFI_1.Models;

namespace TFI_1.Negocio;

public class AtencionNegocio
{
	private readonly AtencionDatos _datos = new AtencionDatos();

	public List<Atencion> Listar(string? filtro = null, string? estado = null)
	{
		return _datos.Listar(filtro, estado);
	}

	public Atencion Guardar(Atencion atencion)
	{
		if (atencion == null)
		{
			throw new ArgumentException("La atención es obligatoria.");
		}
		if (atencion.Cliente == null || atencion.Cliente.Id <= 0)
		{
			throw new ArgumentException("Debe seleccionar un cliente.");
		}
		if (atencion.Empleado == null || atencion.Empleado.Id <= 0)
		{
			throw new ArgumentException("Debe seleccionar un peluquero.");
		}
		if (atencion.Servicio == null || atencion.Servicio.Id <= 0)
		{
			throw new ArgumentException("Debe seleccionar un servicio.");
		}

		// El tipo de atención lo define la modalidad del peluquero:
		// con turnos -> turno con fecha-hora; por orden de llegada -> llegada (ahora).
		if (atencion.Empleado.AtiendeConTurno())
		{
			atencion.Tipo = TipoAtencion.Turno;
		}
		else
		{
			atencion.Tipo = TipoAtencion.Llegada;
			atencion.FechaHora = DateTime.Now;
		}

		// Advertencia: no permitir dos atenciones del mismo empleado en el mismo horario.
		if (_datos.ExisteConflictoHorario(atencion.Empleado.Id, atencion.FechaHora, atencion.Id))
		{
			throw new ArgumentException("El empleado ya tiene una atención registrada en ese horario.");
		}

		if (atencion.Id == 0)
		{
			atencion.Id = _datos.Insertar(atencion);
		}
		else
		{
			_datos.Actualizar(atencion);
		}
		return atencion;
	}

	public void Cancelar(Atencion atencion)
	{
		if (atencion == null || atencion.Id <= 0)
		{
			throw new ArgumentException("Debe seleccionar una atención.");
		}
		atencion.Cancelar();
		_datos.CambiarEstado(atencion.Id, atencion.Estado);
	}

	public void CambiarEstado(Atencion atencion)
	{
		if (atencion == null || atencion.Id <= 0)
		{
			throw new ArgumentException("Debe seleccionar una atención.");
		}
		_datos.CambiarEstado(atencion.Id, atencion.Estado);
	}

	public int CantidadHoy()
	{
		return _datos.CantidadHoy();
	}
}
