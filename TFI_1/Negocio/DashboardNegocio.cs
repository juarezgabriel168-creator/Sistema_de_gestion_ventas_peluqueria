using System.Data;
using TFI_1.Datos;

namespace TFI_1.Negocio;

public class DashboardNegocio
{
	private readonly DashboardDatos _datos = new DashboardDatos();

	public decimal TotalVendidoHoy()
	{
		return _datos.TotalVendidoHoy();
	}

	public int CantidadVentasHoy()
	{
		return _datos.CantidadVentasHoy();
	}

	public int TotalClientes()
	{
		return _datos.TotalClientes();
	}

	public int TotalItems()
	{
		return _datos.TotalItems();
	}

	public int ProductosStockBajo()
	{
		return _datos.ProductosStockBajo();
	}

	public DataTable ProductoMayorStock()
	{
		return _datos.ProductoMayorStock();
	}

	public DataTable ProductoMenorStock()
	{
		return _datos.ProductoMenorStock();
	}

	public DataTable UltimasVentas(int cantidad)
	{
		return _datos.UltimasVentas(cantidad);
	}
}
