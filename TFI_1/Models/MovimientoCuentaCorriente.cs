using System;

namespace TFI_1.Models;

public enum TipoMovimientoCC { Cargo, Abono }

public class MovimientoCuentaCorriente
{
	public int Id { get; set; }
	public int IdCuenta { get; set; }
	public DateTime Fecha { get; set; } = DateTime.Now;
	public TipoMovimientoCC Tipo { get; set; }
	public decimal Monto { get; set; }
	public string? Concepto { get; set; }
	public int? IdVenta { get; set; }
	public int IdUsuario { get; set; }

	public string TipoTexto => Tipo == TipoMovimientoCC.Cargo ? "Cargo" : "Abono";

}
