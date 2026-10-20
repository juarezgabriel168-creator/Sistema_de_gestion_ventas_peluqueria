using System;
using System.Data;
using TFI_1.Datos;

namespace TFI_1.Negocio;

public class ReporteNegocio
{
	private readonly ReporteDatos _datos = new ReporteDatos();

	public DataTable Resumen(DateTime desde, DateTime hasta)
	{
		ValidarRango(desde, hasta);
		return _datos.Resumen(desde, hasta);
	}

	public DataTable VentasPorMedioPago(DateTime desde, DateTime hasta)
	{
		ValidarRango(desde, hasta);
		return _datos.VentasPorMedioPago(desde, hasta);
	}

	public DataTable ProductosMasVendidos(DateTime desde, DateTime hasta)
	{
		ValidarRango(desde, hasta);
		return _datos.ProductosMasVendidos(desde, hasta);
	}

	public DataTable VentasPorDia(DateTime desde, DateTime hasta)
	{
		ValidarRango(desde, hasta);
		return _datos.VentasPorDia(desde, hasta);
	}
	private static void ValidarRango(DateTime desde, DateTime hasta)
	{
		if (desde.Date > hasta.Date)
		{
			throw new ArgumentException("La fecha 'desde' no puede ser posterior a la fecha 'hasta'.");
		}
	}
	public DataTable VentasPorEmpleado(DateTime desde, DateTime hasta)
	{
		ValidarRango(desde, hasta);
		return _datos.VentasPorEmpleado(desde, hasta);
	}
}
