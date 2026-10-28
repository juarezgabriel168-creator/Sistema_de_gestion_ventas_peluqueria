using System;
using System.Collections.Generic;
using System.Data;
using TFI_1.Datos;
using TFI_1.Models;

namespace TFI_1.Negocio;

public class GastoNegocio
{
	private readonly GastoDatos _datos = new();

	private readonly AuditoriaNegocio _auditoria = new();

	public List<Gasto> Listar(DateTime? desde = null, DateTime? hasta = null, string? filtro = null) =>
		_datos.Listar(desde, hasta, filtro);

	public Gasto? ObtenerPorId(int id) => _datos.ObtenerPorId(id);

	public int Insertar(Gasto gasto)
	{
		if (gasto == null)
			throw new ArgumentException("El gasto es obligatorio.");
		Validaciones.ExigirTexto(gasto.Concepto, "Debe ingresar un concepto.");
		Validaciones.ExigirLongitudMaxima(gasto.Concepto, 150,
			"El concepto no puede superar los 150 caracteres.");
		Validaciones.ExigirLongitudMaxima(gasto.Categoria, 100,
			"La categoría no puede superar los 100 caracteres.");
		Validaciones.ExigirLongitudMaxima(gasto.Observacion, 255,
			"La observación no puede superar los 255 caracteres.");
		Validaciones.ExigirImportePositivo(gasto.Monto, "El monto debe ser mayor a cero.");
		Validaciones.ExigirFechaNoFutura(gasto.Fecha, "La fecha no puede ser futura.");

		int id = _datos.Insertar(gasto);
		_auditoria.Registrar("gasto_alta", "gastos", id,
			$"Gasto '{gasto.Concepto}' por {gasto.Monto:C2}.", gasto.IdUsuario);
		return id;
	}

	public void Actualizar(Gasto gasto)
	{
		if (gasto == null || gasto.Id <= 0)
			throw new ArgumentException("Gasto inválido.");
		Validaciones.ExigirTexto(gasto.Concepto, "Debe ingresar un concepto.");
		Validaciones.ExigirLongitudMaxima(gasto.Concepto, 150,
			"El concepto no puede superar los 150 caracteres.");
		Validaciones.ExigirLongitudMaxima(gasto.Categoria, 100,
			"La categoría no puede superar los 100 caracteres.");
		Validaciones.ExigirLongitudMaxima(gasto.Observacion, 255,
			"La observación no puede superar los 255 caracteres.");
		Validaciones.ExigirImportePositivo(gasto.Monto, "El monto debe ser mayor a cero.");

		_datos.Actualizar(gasto);
		_auditoria.Registrar("gasto_edicion", "gastos", gasto.Id,
			$"Gasto '{gasto.Concepto}' actualizado a {gasto.Monto:C2}.", gasto.IdUsuario);
	}

	/// <summary>
	/// Elimina físicamente un gasto. Solo un administrador puede hacerlo y
	/// queda registrado en la auditoría.
	/// </summary>
	public void Eliminar(int id)
	{
		if (id <= 0)
			throw new ArgumentException("Debe seleccionar un gasto.");
		if (!Sesion.EsAdministrador)
			throw new InvalidOperationException("Solo un administrador puede eliminar gastos.");

		Gasto? gasto = _datos.ObtenerPorId(id);
		if (gasto == null)
			throw new InvalidOperationException("El gasto ya no existe.");

		_datos.Eliminar(id);
		_auditoria.Registrar("gasto_eliminacion", "gastos", id,
			$"Gasto '{gasto.Concepto}' del {gasto.Fecha:dd/MM/yyyy} por {gasto.Monto:C2} eliminado.");
	}

	public decimal TotalGastos(DateTime desde, DateTime hasta) => _datos.TotalGastos(desde, hasta);

	public DataTable GastosPorCategoria(DateTime desde, DateTime hasta) => _datos.GastosPorCategoria(desde, hasta);
}
