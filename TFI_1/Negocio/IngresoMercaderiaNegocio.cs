using System;
using System.Collections.Generic;
using System.Data;
using TFI_1.Datos;
using TFI_1.Models;

namespace TFI_1.Negocio;

public class IngresoMercaderiaNegocio
{
	private readonly IngresoMercaderiaDatos _datos = new();

	private readonly AuditoriaNegocio _auditoria = new();

	public List<IngresoMercaderia> Listar(DateTime? desde = null, DateTime? hasta = null, string? filtro = null,
		int? limite = null, int? desplazamiento = null) =>
		_datos.Listar(desde, hasta, filtro, limite, desplazamiento);

	public int Contar(DateTime? desde = null, DateTime? hasta = null, string? filtro = null) =>
		_datos.Contar(desde, hasta, filtro);

	public decimal Sumar(DateTime? desde = null, DateTime? hasta = null, string? filtro = null) =>
		_datos.Sumar(desde, hasta, filtro);

	public IngresoMercaderia? ObtenerPorId(int id) => _datos.ObtenerPorId(id);

	public int Insertar(IngresoMercaderia ingreso)
	{
		if (ingreso == null)
			throw new ArgumentException("El ingreso es obligatorio.");
		if (ingreso.IdProveedor == null || ingreso.IdProveedor <= 0)
			throw new ArgumentException("Debe seleccionar un proveedor.");
		if (string.IsNullOrWhiteSpace(ingreso.Proveedor))
			throw new ArgumentException("Debe ingresar el proveedor.");
		Validaciones.ExigirLongitudMaxima(ingreso.Proveedor, 150,
			"El nombre del proveedor no puede superar los 150 caracteres.");
		Validaciones.ExigirLongitudMaxima(ingreso.Observacion, 255,
			"La observación no puede superar los 255 caracteres.");
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

		int id = _datos.Insertar(ingreso);
		_auditoria.Registrar("ingreso_mercaderia", "ingresos_mercaderia", id,
			$"Ingreso #{id} de {ingreso.Proveedor} por {ingreso.Total:C2}.", ingreso.IdUsuario);
		return id;
	}

	/// <summary>
	/// Anula un ingreso de mercadería. No se borra: se descuenta el stock y
	/// queda registrado el motivo, la fecha y el usuario que lo anuló.
	/// </summary>
	public void Anular(int id, int usuarioId, string motivo)
	{
		if (id <= 0)
			throw new ArgumentException("Debe seleccionar un ingreso.");
		if (!Sesion.EsAdministrador)
			throw new InvalidOperationException("Solo un administrador puede anular ingresos de mercadería.");
		if (usuarioId <= 0)
			throw new ArgumentException("No hay un usuario en sesión.");
		if (string.IsNullOrWhiteSpace(motivo))
			throw new ArgumentException("Indique el motivo de la anulación.");

		motivo = motivo.Trim();
		Validaciones.ExigirLongitudMaxima(motivo, 255,
			"El motivo no puede superar los 255 caracteres.");

		_datos.Anular(id, usuarioId, motivo);
		_auditoria.Registrar("anulacion_ingreso_mercaderia", "ingresos_mercaderia", id,
			$"Ingreso #{id} anulado. Motivo: {motivo}", usuarioId);
	}

	public DataTable IngresosResumen(DateTime desde, DateTime hasta) => _datos.IngresosResumen(desde, hasta);
}
