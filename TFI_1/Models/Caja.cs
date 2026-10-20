using System;

namespace TFI_1.Models;

/// <summary>
/// Turno de caja: se abre con un fondo inicial, registra ingresos y egresos,
/// y se cierra con el efectivo contado y la diferencia contra lo esperado.
/// </summary>
public class Caja
{
	public int Id { get; set; }

	public int IdUsuarioApertura { get; set; }

	public string? UsuarioApertura { get; set; }

	public DateTime FechaApertura { get; set; }

	public decimal MontoInicial { get; set; }

	public int? IdUsuarioCierre { get; set; }

	public string? UsuarioCierre { get; set; }

	public DateTime? FechaCierre { get; set; }

	public decimal? MontoEsperado { get; set; }

	public decimal? MontoContado { get; set; }

	public decimal? Diferencia { get; set; }

	public string? Observacion { get; set; }

	public string Estado { get; set; } = "abierta";

	public bool Abierta => Estado == "abierta";

	public Caja() { }
}
