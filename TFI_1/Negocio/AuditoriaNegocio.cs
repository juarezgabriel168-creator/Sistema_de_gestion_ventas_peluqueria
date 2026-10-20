using System;
using System.Data;
using TFI_1.Datos;

namespace TFI_1.Negocio;

/// <summary>
/// Registra en la auditoría las operaciones sensibles del sistema.
/// Si no hay usuario en sesión (o la acción viene vacía) no hace nada:
/// nunca debe hacer fallar la operación real.
/// </summary>
public class AuditoriaNegocio
{
	// Largo de la columna detalle en la base de datos.
	private const int LargoDetalle = 255;

	private readonly AuditoriaDatos _datos = new();

	public void Registrar(string accion, string entidad, int? idEntidad, string? detalle, int? usuarioId = null)
	{
		int idUsuario = usuarioId ?? Sesion.UsuarioActual?.Id ?? 0;
		if (idUsuario <= 0 || string.IsNullOrWhiteSpace(accion))
		{
			return;
		}

		if (detalle != null && detalle.Length > LargoDetalle)
		{
			detalle = detalle.Substring(0, LargoDetalle);
		}

		_datos.Registrar(idUsuario, accion.Trim(), entidad, idEntidad, detalle);
	}

	public DataTable Listar(DateTime desde, DateTime hasta, int? usuarioId = null, string? accion = null) =>
		_datos.Listar(desde, hasta, usuarioId, accion);

	public DataTable Acciones() => _datos.Acciones();
}
