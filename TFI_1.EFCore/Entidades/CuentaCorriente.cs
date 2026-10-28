using System;
using System.Collections.Generic;

namespace TFI_1.EFCore.Entidades;

public class CuentaCorriente
{
	public int Id { get; set; }
	public int IdCliente { get; set; }
	public Cliente Cliente { get; set; } = null!;
	public decimal Saldo { get; set; }
	public DateTime FechaActualizacion { get; set; } = DateTime.Now;

	public List<MovimientoCuentaCorriente> Movimientos { get; set; } = new();
}

public class MovimientoCuentaCorriente
{
	public int Id { get; set; }
	public int IdCuenta { get; set; }
	public CuentaCorriente Cuenta { get; set; } = null!;
	public DateTime Fecha { get; set; } = DateTime.Now;
	public TipoMovimientoCuentaCorriente Tipo { get; set; }
	public decimal Monto { get; set; }
	public string? Concepto { get; set; }
	public int? IdVenta { get; set; }
	public Venta? Venta { get; set; }
	public int IdUsuario { get; set; }
	public Usuario Usuario { get; set; } = null!;

	public string TipoTexto => Tipo == TipoMovimientoCuentaCorriente.Cargo ? "Cargo" : "Abono";
}
