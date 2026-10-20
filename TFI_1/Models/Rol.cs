using System;

namespace TFI_1.Models;

public class Rol
{
	public int Id { get; set; }
	public string Nombre { get; set; } = string.Empty;

	public Rol() { }
	public Rol(int id, string nombre)
	{
		Id = id;
		Nombre = nombre;
	}
}
