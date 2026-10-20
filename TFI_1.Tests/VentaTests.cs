using System;
using TFI_1.Models;
using Xunit;

namespace TFI_1.Tests;

public class VentaTests
{
	private static Item Producto(decimal precio, int stock = 100) =>
		new Item
		{
			Nombre = "Producto",
			Tipo = TipoItem.Producto,
			Stock = stock,
			StockMinimo = 1,
			Precio = precio,
		};

	[Fact]
	public void VentaVacia_TotalCero()
	{
		Venta venta = new Venta();

		Assert.Equal(0m, venta.Subtotal);
		Assert.Equal(0m, venta.Total);
	}

	[Fact]
	public void AgregarDetalle_AcumulaElSubtotal()
	{
		Venta venta = new Venta();

		venta.AgregarDetalle(Producto(100m), 2);
		venta.AgregarDetalle(Producto(50m), 3);

		Assert.Equal(2, venta.Detalles.Count);
		Assert.Equal(350m, venta.Subtotal);
	}

	[Fact]
	public void Total_RestaElDescuentoYSumaElRecargo()
	{
		Venta venta = new Venta
		{
			Descuento = 50m,
			Recargo = 120m,
		};
		venta.AgregarDetalle(Producto(100m), 2);

		Assert.Equal(200m, venta.Subtotal);
		Assert.Equal(270m, venta.Total);
	}

	[Fact]
	public void AgregarDetalle_SinStock_Lanza()
	{
		Venta venta = new Venta();

		Assert.Throws<InvalidOperationException>(
			() => venta.AgregarDetalle(Producto(100m, stock: 1), 2));
		Assert.Empty(venta.Detalles);
	}
}
