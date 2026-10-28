using System;

namespace TFI_1.EFCore.Entidades;

public class Atencion
{
	public int Id { get; set; }
	public int IdCliente { get; set; }
	public Cliente Cliente { get; set; } = null!;
	public int IdEmpleado { get; set; }
	public Empleado Empleado { get; set; } = null!;
	public int IdItem { get; set; }
	public Item Servicio { get; set; } = null!;
	public TipoAtencion Tipo { get; set; }
	public DateTime FechaHora { get; set; }
	public EstadoAtencion Estado { get; set; } = EstadoAtencion.Pendiente;

	public string Fecha => FechaHora.ToString("dd/MM/yyyy");
	public string Hora => FechaHora.ToString("HH:mm");
	public string ClienteNombre => Cliente?.NombreCompleto ?? string.Empty;
	public string EmpleadoNombre => Empleado?.NombreCompleto ?? string.Empty;
	public string ServicioNombre => Servicio?.Nombre ?? string.Empty;
	public string TipoNombre => Tipo == TipoAtencion.Turno ? "Turno" : "Orden de llegada";
	public string EstadoNombre => Estado switch
	{
		EstadoAtencion.Pendiente => "Pendiente",
		EstadoAtencion.EnAtencion => "En atención",
		EstadoAtencion.Finalizada => "Finalizada",
		_ => "Cancelada",
	};
}

public class Auditoria
{
	public int Id { get; set; }
	public DateTime Fecha { get; set; }
	public int IdUsuario { get; set; }
	public Usuario Usuario { get; set; } = null!;
	public string Accion { get; set; } = string.Empty;
	public string Entidad { get; set; } = string.Empty;
	public int? IdEntidad { get; set; }
	public string? Detalle { get; set; }
}
