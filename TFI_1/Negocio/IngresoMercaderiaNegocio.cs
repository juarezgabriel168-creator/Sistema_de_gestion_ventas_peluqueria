using System;
using System.Collections.Generic;
using System.Data;
using TFI_1.Datos;
using TFI_1.Models;

namespace TFI_1.Negocio;

public class IngresoMercaderiaNegocio
{
	private readonly IngresoMercaderiaDatos _datos = new();

	public List<IngresoMercaderia> Listar(DateTime? desde = null, DateTime? hasta = null, string? filtro = null) =>
		_datos.Listar(desde, hasta, filtro);

	public IngresoMercaderia? ObtenerPorId(int id) => _datos.ObtenerPorId(id);

	public int Insertar(IngresoMercaderia ingreso)
	{
		if (ingreso == null)
			throw new ArgumentException("El ingreso es obligatorio.");
		if (ingreso.IdProveedor == null || ingreso.IdProveedor <= 0)
			throw new ArgumentException("Debe seleccionar un proveedor.");
		if (string.IsNullOrWhiteSpace(ingreso.Proveedor))
			throw new ArgumentException("Debe ingresar el proveedor.");
		if (ingreso.Detalles.Count == 0)
			throw new ArgumentException("Debe agregar al menos un producto al ingreso.");
		if (ingreso.IdUsuario <= 0)
			throw new ArgumentException("No hay un usuario en sesión.");

		foreach (var detalle in ingreso.Detalles)
		{
			if (detalle.Item == null || detalle.Item.Id <= 0)
				throw new ArgumentException("Producto inválido en el detalle.");
			if (!detalle.Item.EsProducto)
				throw new ArgumentException($"'{detalle.Item.Nombre}' es un servicio: solo se puede ingresar mercadería de productos.");
			if (detalle.Cantidad <= 0)
				throw new ArgumentException($"La cantidad de {detalle.Item.Nombre} debe ser mayor a cero.");
			if (detalle.CostoUnitario < 0)
				throw new ArgumentException($"El costo de {detalle.Item.Nombre} no puede ser negativo.");
		}

		return _datos.Insertar(ingreso);
	}

	public DataTable IngresosResumen(DateTime desde, DateTime hasta) => _datos.IngresosResumen(desde, hasta);
}
