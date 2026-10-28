using System;
using System.Collections.Generic;

namespace TFI_1.EFCore.Entidades;

public class Caja
{
	public int Id { get; set; }
	public int IdUsuarioApertura { get; set; }
	public Usuario UsuarioApertura { get; set; } = null!;
	public DateTime FechaApertura { get; set; }
	public decimal MontoInicial { get; set; }
	public int? IdUsuarioCierre { get; set; }
	public Usuario? UsuarioCierre { get; set; }
	public DateTime? FechaCierre { get; set; }
	public decimal? MontoEsperado { get; set; }
	public decimal? MontoContado { get; set; }
	public decimal? Diferencia { get; set; }
	public string? Observacion { get; set; }
	public string Estado { get; set; } = "abierta";

	public bool Abierta => Estado == "abierta";
	public List<CajaMovimiento> Movimientos { get; set; } = new();
}

public class CajaMovimiento
{
	public int Id { get; set; }
	public int IdCaja { get; set; }
	public Caja Caja { get; set; } = null!;
	public DateTime Fecha { get; set; }
	public string Tipo { get; set; } = string.Empty;
	public decimal Monto { get; set; }
	public string Concepto { get; set; } = string.Empty;
	public int IdUsuario { get; set; }
	public Usuario Usuario { get; set; } = null!;
}
