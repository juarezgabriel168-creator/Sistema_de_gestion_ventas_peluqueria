using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Dtos;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class DashboardRepositorio
{
	public decimal TotalVendidoHoy()
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = (DateTime.Today, DateTime.Today.AddDays(1));
		return db.Ventas.AsNoTracking()
			.Where(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada)
			.Sum(v => (decimal?)v.Total) ?? 0m;
	}

	public int CantidadVentasHoy()
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = (DateTime.Today, DateTime.Today.AddDays(1));
		return db.Ventas.AsNoTracking().Count(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada);
	}

	public int TotalClientes()
	{
		using var db = new PeluqueriaDbContext();
		return db.Clientes.AsNoTracking().Count(c => c.Activo);
	}

	public int TotalItems()
	{
		using var db = new PeluqueriaDbContext();
		return db.Items.AsNoTracking().Count(i => i.Activo);
	}

	public int ProductosStockBajo()
	{
		using var db = new PeluqueriaDbContext();
		return db.Items.AsNoTracking().Count(i =>
			i.Activo && i.Tipo == TipoItem.Producto && i.Stock <= i.StockMinimo);
	}

	public List<ProductoStockDto> ProductoMayorStock()
	{
		using var db = new PeluqueriaDbContext();
		return db.Items.AsNoTracking()
			.Where(i => i.Activo && i.Tipo == TipoItem.Producto)
			.OrderByDescending(i => i.Stock)
			.Select(i => new ProductoStockDto { Producto = i.Nombre, Stock = i.Stock })
			.Take(1)
			.ToList();
	}

	public List<ProductoStockDto> ProductoMenorStock()
	{
		using var db = new PeluqueriaDbContext();
		return db.Items.AsNoTracking()
			.Where(i => i.Activo && i.Tipo == TipoItem.Producto && i.Stock > 0)
			.OrderBy(i => i.Stock)
			.Select(i => new ProductoStockDto { Producto = i.Nombre, Stock = i.Stock })
			.Take(1)
			.ToList();
	}

	public List<UltimaVentaDto> UltimasVentas(int cantidad)
	{
		using var db = new PeluqueriaDbContext();
		return db.Ventas.AsNoTracking()
			.Where(v => !v.Anulada)
			.OrderByDescending(v => v.Fecha)
			.ThenByDescending(v => v.Id)
			.Select(v => new UltimaVentaDto
			{
				Venta = v.Id,
				Fecha = v.Fecha,
				Cliente = v.Cliente.Apellido + ", " + v.Cliente.Nombre,
				Empleado = v.Empleado == null ? null : v.Empleado.Apellido + ", " + v.Empleado.Nombre,
				Usuario = v.Usuario.NombreUsuario,
				Pago = v.MedioPago,
				Total = v.Total,
			})
			.Take(cantidad)
			.ToList();
	}
}
