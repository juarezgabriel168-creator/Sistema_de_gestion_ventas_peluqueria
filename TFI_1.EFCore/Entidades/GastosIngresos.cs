using System;
using System.Collections.Generic;

namespace TFI_1.EFCore.Entidades;

public class Gasto
{
	public int Id { get; set; }
	public DateTime Fecha { get; set; }
	public string Concepto { get; set; } = string.Empty;
	public decimal Monto { get; set; }
	public string? Categoria { get; set; }
	public string? Observacion { get; set; }
	public int IdUsuario { get; set; }
	public Usuario Usuario { get; set; } = null!;
	public DateTime FechaAlta { get; set; }

	public string? NombreUsuario => Usuario?.NombreUsuario;
}

public class IngresoMercaderia
{
	public int Id { get; set; }
	public DateTime Fecha { get; set; }
	public int? IdProveedor { get; set; }
	public Proveedor? ProveedorEntidad { get; set; }
	public string? Proveedor { get; set; }
	public decimal Total { get; set; }
	public int IdUsuario { get; set; }
	public Usuario Usuario { get; set; } = null!;
	public string? Observacion { get; set; }

	public List<IngresoMercaderiaDetalle> Detalles { get; set; } = new();

	public string? NombreUsuario => Usuario?.NombreUsuario;
}

public class IngresoMercaderiaDetalle
{
	public int Id { get; set; }
	public int IdIngreso { get; set; }
	public IngresoMercaderia Ingreso { get; set; } = null!;
	public int IdItem { get; set; }
	public Item Item { get; set; } = null!;
	public int Cantidad { get; set; }
	public decimal CostoUnitario { get; set; }
	public decimal Subtotal { get; set; }
}
