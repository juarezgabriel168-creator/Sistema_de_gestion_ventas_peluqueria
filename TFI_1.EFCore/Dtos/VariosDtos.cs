using System;

namespace TFI_1.EFCore.Dtos;

public class CajaListadoDto
{
	public int Id { get; set; }
	public DateTime Apertura { get; set; }
	public string UsuarioApertura { get; set; } = string.Empty;
	public decimal Inicial { get; set; }
	public DateTime? Cierre { get; set; }
	public string? UsuarioCierre { get; set; }
	public decimal? Esperado { get; set; }
	public decimal? Contado { get; set; }
	public decimal? Diferencia { get; set; }
	public string Estado { get; set; } = string.Empty;
	public string? Observacion { get; set; }
}

public class CajaMovimientoDto
{
	public int Id { get; set; }
	public DateTime Fecha { get; set; }
	public string Tipo { get; set; } = string.Empty;
	public string Concepto { get; set; } = string.Empty;
	public decimal Monto { get; set; }
	public string Usuario { get; set; } = string.Empty;
}

public class ResumenVentasDto
{
	public string MedioPago { get; set; } = string.Empty;
	public int Cantidad { get; set; }
	public decimal Total { get; set; }
}

public class MovimientoCuentaCorrienteDto
{
	public DateTime Fecha { get; set; }
	public string Tipo { get; set; } = string.Empty;
	public decimal Monto { get; set; }
	public string? Concepto { get; set; }
	public int? IdVenta { get; set; }
	public string NombreUsuario { get; set; } = string.Empty;
}

public class IngresoResumenDto
{
	public DateTime Fecha { get; set; }
	public int Cantidad { get; set; }
	public decimal Total { get; set; }
}

public class GastoCategoriaDto
{
	public string Categoria { get; set; } = string.Empty;
	public int Cantidad { get; set; }
	public decimal Total { get; set; }
}

public class ProductoStockDto
{
	public string Producto { get; set; } = string.Empty;
	public int? Stock { get; set; }
}

public class UltimaVentaDto
{
	public int Venta { get; set; }
	public DateTime Fecha { get; set; }
	public string Cliente { get; set; } = string.Empty;
	public string? Empleado { get; set; }
	public string Usuario { get; set; } = string.Empty;
	public string Pago { get; set; } = string.Empty;
	public decimal Total { get; set; }
}

public class AuditoriaListadoDto
{
	public int Id { get; set; }
	public DateTime Fecha { get; set; }
	public string Usuario { get; set; } = string.Empty;
	public string Codigo { get; set; } = string.Empty;
	public string Accion { get; set; } = string.Empty;
	public string Entidad { get; set; } = string.Empty;
	public int? IdEntidad { get; set; }
	public string? Detalle { get; set; }
}

public class AccionAuditoriaDto
{
	public string Codigo { get; set; } = string.Empty;
	public string Nombre { get; set; } = string.Empty;
}
