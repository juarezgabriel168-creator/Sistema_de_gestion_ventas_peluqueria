using System;
using System.Collections.Generic;
using System.Linq;

namespace TFI_1.EFCore.Entidades;

public class Venta
{
	public int Id { get; set; }
	public int IdCliente { get; set; }
	public Cliente Cliente { get; set; } = null!;
	public int IdUsuario { get; set; }
	public Usuario Usuario { get; set; } = null!;
	public int? IdEmpleado { get; set; }
	public Empleado? Empleado { get; set; }
	public DateTime Fecha { get; set; }
	public string MedioPago { get; set; } = string.Empty;
	public int? IdTarjetaCredito { get; set; }
	public TarjetaCredito? TarjetaCredito { get; set; }
	public int? CantidadCuotas { get; set; }
	public decimal Descuento { get; set; }
	public decimal Recargo { get; set; }
	public decimal Total { get; set; }
	public bool Anulada { get; set; }
	public DateTime? FechaAnulacion { get; set; }
	public int? IdUsuarioAnulacion { get; set; }
	public Usuario? UsuarioAnulacion { get; set; }
	public string? MotivoAnulacion { get; set; }

	public List<DetalleVenta> Detalles { get; set; } = new();

	public decimal Subtotal => Detalles.Sum(d => d.Subtotal);
}

public class DetalleVenta
{
	public int Id { get; set; }
	public int IdVenta { get; set; }
	public Venta Venta { get; set; } = null!;
	public int IdItem { get; set; }
	public Item Item { get; set; } = null!;
	public int Cantidad { get; set; }
	public decimal Precio { get; set; }
	public decimal Subtotal { get; set; }
}
