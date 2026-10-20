using System;
using System.Collections.Generic;
using System.Linq;

namespace TFI_1.Models;

public class Venta
{
	public int Id { get; set; }
	public decimal Recargo { get; set; }
	public Cliente Cliente { get; set; } = null!;

	public Usuario Usuario { get; set; } = null!;

	public int? IdEmpleado { get; set; }

	public DateTime Fecha { get; set; } = DateTime.Now;

	public string MedioPago { get; set; } = string.Empty;

	public int? IdTarjetaCredito { get; set; }

	public int? CantidadCuotas { get; set; }

	public decimal Descuento { get; set; }

	public List<DetalleVenta> Detalles { get; } = new List<DetalleVenta>();

	public decimal Subtotal => Detalles.Sum((DetalleVenta d) => d.Subtotal);

	public decimal Total => Subtotal - Descuento + Recargo;

	public void AgregarDetalle(Item item, int cantidad)
	{
		Detalles.Add(new DetalleVenta(item, cantidad));
	}
}
