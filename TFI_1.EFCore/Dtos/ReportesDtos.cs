using System;

namespace TFI_1.EFCore.Dtos;

public class ResumenDto
{
	public decimal Total { get; set; }
	public int Cantidad { get; set; }
	public decimal TicketPromedio { get; set; }
}

public class VentasPorMedioPagoDto
{
	public string Pago { get; set; } = string.Empty;
	public int Cantidad { get; set; }
	public decimal Total { get; set; }
}

public class ItemVendidoDto
{
	public string Item { get; set; } = string.Empty;
	public string Tipo { get; set; } = string.Empty;
	public int Cantidad { get; set; }
	public decimal Total { get; set; }
}

public class VentasPorDiaDto
{
	public DateTime Fecha { get; set; }
	public int Cantidad { get; set; }
	public decimal Total { get; set; }
}

public class ClientesComprasDto
{
	public string Cliente { get; set; } = string.Empty;
	public string DNI { get; set; } = string.Empty;
	public int Compras { get; set; }
	public decimal Total { get; set; }
}

public class VentasPorUsuarioDto
{
	public string Usuario { get; set; } = string.Empty;
	public string Rol { get; set; } = string.Empty;
	public int Cantidad { get; set; }
	public decimal Total { get; set; }
}

public class ServiciosSolicitadosDto
{
	public string Servicio { get; set; } = string.Empty;
	public int Veces { get; set; }
	public int Cantidad { get; set; }
	public decimal Total { get; set; }
}

public class VentasPorEmpleadoDto
{
	public string Empleado { get; set; } = string.Empty;
	public int Cantidad { get; set; }
	public decimal Total { get; set; }
}

public class AnulacionDto
{
	public int Venta { get; set; }
	public DateTime FechaVenta { get; set; }
	public DateTime? FechaAnulacion { get; set; }
	public string Cliente { get; set; } = string.Empty;
	public string Pago { get; set; } = string.Empty;
	public decimal Total { get; set; }
	public string Usuario { get; set; } = string.Empty;
	public string? Motivo { get; set; }
}
