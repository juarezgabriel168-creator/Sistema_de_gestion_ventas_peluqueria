using System;
using TFI_1.Models;
using Xunit;

namespace TFI_1.Tests;

public class DetalleVentaTests
{
	private static Item Producto(int stock, decimal precio) =>
		new Item
		{
			Nombre = "Shampoo",
			Tipo = TipoItem.Producto,
			Stock = stock,
			StockMinimo = 1,
			Precio = precio,
		};

	private static Item Servicio(decimal precio) =>
		new Item
		{
			Nombre = "Corte",
			Tipo = TipoItem.Servicio,
			DuracionMin = 30,
			Precio = precio,
		};

	[Fact]
	public void Item_Nulo_Lanza()
	{
		Assert.Throws<ArgumentException>(() => new DetalleVenta(null!, 1));
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	public void Cantidad_NoPositiva_Lanza(int cantidad)
	{
		Assert.Throws<ArgumentException>(() => new DetalleVenta(Producto(10, 100m), cantidad));
	}

	[Fact]
	public void Stock_Insuficiente_Lanza()
	{
		Assert.Throws<InvalidOperationException>(
			() => new DetalleVenta(Producto(stock: 2, precio: 100m), 3));
	}

	[Fact]
	public void Subtotal_EsPrecioPorCantidad()
	{
		DetalleVenta detalle = new DetalleVenta(Producto(stock: 10, precio: 150m), 3);

		Assert.Equal(450m, detalle.Subtotal);
	}

	[Fact]
	public void Precio_SeCongelaAlMomentoDeLaVenta()
	{
		Item item = Producto(stock: 10, precio: 100m);
		DetalleVenta detalle = new DetalleVenta(item, 2);

		item.Precio = 999m;

		Assert.Equal(100m, detalle.Precio);
		Assert.Equal(200m, detalle.Subtotal);
	}

	[Fact]
	public void Servicio_NoControlaStock()
	{
		DetalleVenta detalle = new DetalleVenta(Servicio(500m), 5);

		Assert.Equal(2500m, detalle.Subtotal);
	}
}
