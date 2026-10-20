using System;

namespace TFI_1.Models;

public class CuentaCorriente
{
	public int Id { get; set; }
	public int IdCliente { get; set; }
	public decimal Saldo { get; set; }
	public DateTime FechaActualizacion { get; set; } = DateTime.Now;

}
