using System;

namespace TFI_1.Models;

public class TarjetaCredito
{
	public int Id { get; set; }
	public int IdBanco { get; set; }
	public string NombreBanco { get; set; } = string.Empty;   // viene del JOIN, no se guarda
	public string TipoTarjeta { get; set; } = string.Empty;   // Crédito, Débito, etc.
	public decimal PorcentajeRecargo { get; set; }            // interés %
	public int CuotasMax { get; set; } = 1;
	public bool Activo { get; set; } = true;
	public DateTime FechaAlta { get; set; } = DateTime.Now;

	public string DescripcionCorta => $"{NombreBanco} - {TipoTarjeta}";
}
