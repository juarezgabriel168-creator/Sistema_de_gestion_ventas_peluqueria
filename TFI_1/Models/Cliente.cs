using System;

namespace TFI_1.Models;

public class Cliente
{
	private string _dni = string.Empty;

	public int Id { get; set; }
	public string Nombre { get; set; } = string.Empty;
	public string Apellido { get; set; } = string.Empty;
	public string Telefono { get; set; } = string.Empty;
	public bool Activo { get; set; } = true;

	public string Dni
	{
		get => _dni;
		set
		{
			Validaciones.ExigirDni(value);
			_dni = value;
		}
	}

	public string NombreCompleto => $"{Apellido}, {Nombre}";

	public const string DniConsumidorFinal = "00000000";

	public bool EsConsumidorFinal => _dni == DniConsumidorFinal;

	public Cliente() { }
	public Cliente(string nombre, string apellido, string dni, string? telefono = null)
	{
		Nombre = nombre;
		Apellido = apellido;
		Dni = dni;
		Telefono = telefono ?? string.Empty;
	}
}
