using System;
using System.Text;

namespace TFI_1.Models;

public class Gasto
{
	public int Id { get; set; }
	public DateTime Fecha { get; set; } = DateTime.Today;
	public string Concepto { get; set; } = string.Empty;
	public decimal Monto { get; set; }
	public string? Categoria { get; set; }
	public string? Observacion { get; set; }
	public int IdUsuario { get; set; }
	public string? NombreUsuario { get; set; }
	public DateTime FechaAlta { get; set; } = DateTime.Now;

}
