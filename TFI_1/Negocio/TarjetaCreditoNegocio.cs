using System;
using System.Collections.Generic;
using TFI_1.Datos;
using TFI_1.Models;

namespace TFI_1.Negocio;

public class TarjetaCreditoNegocio
{
	private readonly TarjetaCreditoDatos _datos = new();

	public List<TarjetaCredito> Listar(bool soloActivas = true) => _datos.Listar(soloActivas);

	public List<TarjetaCredito> ListarPorBanco(int idBanco, bool soloActivas = true) =>
		_datos.ListarPorBanco(idBanco, soloActivas);

	public TarjetaCredito? ObtenerPorId(int id) => _datos.ObtenerPorId(id);

	public int Insertar(TarjetaCredito tarjeta)
	{
		Validar(tarjeta);
		return _datos.Insertar(tarjeta);
	}

	public void Actualizar(TarjetaCredito tarjeta)
	{
		if (tarjeta == null || tarjeta.Id <= 0) throw new ArgumentException("Tarjeta inválida.");
		Validar(tarjeta);
		_datos.Actualizar(tarjeta);
	}

	public void Desactivar(int id)
	{
		if (id <= 0) throw new ArgumentException("Tarjeta inválida.");
		_datos.Desactivar(id);
	}

	public void Reactivar(int id)
	{
		if (id <= 0) throw new ArgumentException("Tarjeta inválida.");
		_datos.Reactivar(id);
	}

	private void Validar(TarjetaCredito t)
	{
		if (t == null) throw new ArgumentException("La tarjeta es obligatoria.");
		if (t.IdBanco <= 0) throw new ArgumentException("Debe seleccionar un banco.");
		if (string.IsNullOrWhiteSpace(t.TipoTarjeta))
			throw new ArgumentException("Debe ingresar el tipo de tarjeta.");
		t.TipoTarjeta = t.TipoTarjeta.Trim();
		if (t.PorcentajeRecargo < 0 || t.PorcentajeRecargo > 100)
			throw new ArgumentException("El interés debe estar entre 0 y 100.");
		if (t.CuotasMax < 1)
			throw new ArgumentException("La cantidad máxima de cuotas debe ser al menos 1.");
		if (_datos.ExisteTipo(t.IdBanco, t.TipoTarjeta, t.Id > 0 ? t.Id : null))
			throw new ArgumentException($"Este banco ya tiene una tarjeta '{t.TipoTarjeta}'.");
	}
}
