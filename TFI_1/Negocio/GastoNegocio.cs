using System;
using System.Collections.Generic;
using System.Data;
using TFI_1.Datos;
using TFI_1.Models;

namespace TFI_1.Negocio;

public class GastoNegocio
{
	private readonly GastoDatos _datos = new();

	public List<Gasto> Listar(DateTime? desde = null, DateTime? hasta = null, string? filtro = null) =>
		_datos.Listar(desde, hasta, filtro);

	public Gasto? ObtenerPorId(int id) => _datos.ObtenerPorId(id);

	public int Insertar(Gasto gasto)
	{
		if (gasto == null)
			throw new ArgumentException("El gasto es obligatorio.");
		if (string.IsNullOrWhiteSpace(gasto.Concepto))
			throw new ArgumentException("Debe ingresar un concepto.");
		if (gasto.Monto <= 0)
			throw new ArgumentException("El monto debe ser mayor a cero.");
		if (gasto.Fecha > DateTime.Today)
			throw new ArgumentException("La fecha no puede ser futura.");

		return _datos.Insertar(gasto);
	}

	public void Actualizar(Gasto gasto)
	{
		if (gasto == null || gasto.Id <= 0)
			throw new ArgumentException("Gasto inválido.");
		if (string.IsNullOrWhiteSpace(gasto.Concepto))
			throw new ArgumentException("Debe ingresar un concepto.");
		if (gasto.Monto <= 0)
			throw new ArgumentException("El monto debe ser mayor a cero.");

		_datos.Actualizar(gasto);
	}

	public decimal TotalGastos(DateTime desde, DateTime hasta) => _datos.TotalGastos(desde, hasta);

	public DataTable GastosPorCategoria(DateTime desde, DateTime hasta) => _datos.GastosPorCategoria(desde, hasta);
}
