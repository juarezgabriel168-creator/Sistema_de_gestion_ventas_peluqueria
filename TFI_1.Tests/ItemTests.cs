using System;
using TFI_1.Models;
using Xunit;

namespace TFI_1.Tests;

public class ItemTests
{
	private static Item Producto(int stock = 10, int stockMinimo = 2, decimal precio = 100m) =>
		new Item
		{
			Nombre = "Shampoo",
			Tipo = TipoItem.Producto,
			Stock = stock,
			StockMinimo = stockMinimo,
			Precio = precio,
		};

	private static Item Servicio(decimal precio = 500m) =>
		new Item
		{
			Nombre = "Corte",
			Tipo = TipoItem.Servicio,
			DuracionMin = 30,
			Precio = precio,
		};

	[Fact]
	public void Precio_Negativo_Lanza()
	{
		Assert.Throws<ArgumentException>(() => new Item { Precio = -1m });
	}

	[Fact]
	public void Precio_Cero_EsValido()
	{
		Item item = new Item { Precio = 0m };

		Assert.Equal(0m, item.Precio);
	}

	[Fact]
	public void EsProducto_DistingueProductoDeServicio()
	{
		Assert.True(Producto().EsProducto);
		Assert.False(Servicio().EsProducto);
	}

	[Theory]
	[InlineData(2, 2, true)]
	[InlineData(1, 2, true)]
	[InlineData(3, 2, false)]
	public void StockBajo_ComparaContraElMinimo(int stock, int minimo, bool esperado)
	{
		Assert.Equal(esperado, Producto(stock, minimo).StockBajo);
	}

	[Fact]
	public void StockBajo_EnServicio_EsFalso()
	{
		Assert.False(Servicio().StockBajo);
	}

	[Fact]
	public void HayStockPara_Producto()
	{
		Item item = Producto(stock: 3);

		Assert.True(item.HayStockPara(3));
		Assert.False(item.HayStockPara(4));
	}

	[Fact]
	public void HayStockPara_Servicio_SiempreEsVerdadero()
	{
		Assert.True(Servicio().HayStockPara(999));
	}

	[Fact]
	public void DescontarStock_Producto_Descuenta()
	{
		Item item = Producto(stock: 10);

		item.DescontarStock(4);

		Assert.Equal(6, item.Stock);
	}

	[Fact]
	public void DescontarStock_SinStock_NoModifica()
	{
		Item item = Producto(stock: 2);

		Assert.Throws<InvalidOperationException>(() => item.DescontarStock(3));
		Assert.Equal(2, item.Stock);
	}

	[Fact]
	public void DescontarStock_Servicio_NoHaceNada()
	{
		Item item = Servicio();

		item.DescontarStock(5);

		Assert.Null(item.Stock);
	}

	[Fact]
	public void CategoriaNombre_SinCategoria_EsVacio()
	{
		Assert.Equal(string.Empty, new Item().CategoriaNombre);
	}
}
