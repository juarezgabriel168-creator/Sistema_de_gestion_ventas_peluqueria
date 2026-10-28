using System.Collections.Generic;

namespace TFI_1.EFCore.Entidades;

public class Rol
{
	public int Id { get; set; }
	public string Nombre { get; set; } = string.Empty;
}

public class Usuario
{
	public int Id { get; set; }
	public string NombreUsuario { get; set; } = string.Empty;
	public string Contrasena { get; set; } = string.Empty;
	public int IdRol { get; set; }
	public Rol Rol { get; set; } = null!;
	public bool Activo { get; set; } = true;

	public string RolNombre => Rol?.Nombre ?? string.Empty;
}

public class Cliente
{
	public int Id { get; set; }
	public string Nombre { get; set; } = string.Empty;
	public string Apellido { get; set; } = string.Empty;
	public string Dni { get; set; } = string.Empty;
	public string? Telefono { get; set; }
	public bool Activo { get; set; } = true;

	public string NombreCompleto => $"{Apellido}, {Nombre}";
	public bool EsConsumidorFinal => Dni == "00000000";
}

public class Empleado
{
	public int Id { get; set; }
	public string Nombre { get; set; } = string.Empty;
	public string Apellido { get; set; } = string.Empty;
	public Modalidad Modalidad { get; set; }
	public bool Activo { get; set; } = true;

	public string NombreCompleto => $"{Apellido}, {Nombre}";
	public string ModalidadTexto => Modalidad == Modalidad.Turnos ? "Turnos" : "Orden de llegada";
}
