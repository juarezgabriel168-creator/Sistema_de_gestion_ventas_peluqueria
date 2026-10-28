using System;

namespace TFI_1.EFCore.Dtos;

public class VentaListadoDto
{
	public int Venta { get; set; }
	public DateTime Fecha { get; set; }
	public string Cliente { get; set; } = string.Empty;
	public string Usuario { get; set; } = string.Empty;
	public string? Empleado { get; set; }
	public string Pago { get; set; } = string.Empty;
	public string? Tarjeta { get; set; }
	public int? Cuotas { get; set; }
	public decimal Subtotal { get; set; }
	public decimal Descuento { get; set; }
	public decimal Recargo { get; set; }
	public decimal Total { get; set; }
	public bool Anulada { get; set; }
}

public class VentaCabeceraDto
{
	public int Venta { get; set; }
	public DateTime Fecha { get; set; }
	public string Cliente { get; set; } = string.Empty;
	public string Usuario { get; set; } = string.Empty;
	public string? Empleado { get; set; }
	public string Pago { get; set; } = string.Empty;
	public int? IdTarjeta { get; set; }
	public string? Banco { get; set; }
	public string? TipoTarjeta { get; set; }
	public int? Cuotas { get; set; }
	public decimal Subtotal { get; set; }
	public decimal Descuento { get; set; }
	public bool Anulada { get; set; }
	public DateTime? FechaAnulacion { get; set; }
	public string? MotivoAnulacion { get; set; }
	public string? UsuarioAnulacion { get; set; }
	public decimal Recargo { get; set; }
	public decimal Total { get; set; }
}

public class VentaDetalleDto
{
	public string Item { get; set; } = string.Empty;
	public string Tipo { get; set; } = string.Empty;
	public int Cantidad { get; set; }
	public decimal Precio { get; set; }
	public decimal Subtotal { get; set; }
}
