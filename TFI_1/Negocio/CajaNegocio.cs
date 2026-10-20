using System;
using System.Data;
using TFI_1.Datos;
using TFI_1.Models;

namespace TFI_1.Negocio;

/// <summary>
/// Reglas de la caja: una sola caja abierta, movimientos con monto positivo y
/// cierre con el efectivo esperado (fondo inicial + ventas en efectivo +
/// ingresos - egresos) contra el efectivo contado.
/// </summary>
public class CajaNegocio
{
	private readonly CajaDatos _datos = new();

	private readonly AuditoriaNegocio _auditoria = new();

	public Caja? ObtenerAbierta() => _datos.ObtenerAbierta();

	public Caja? ObtenerPorId(int id) => _datos.ObtenerPorId(id);

	public Caja Abrir(decimal montoInicial)
	{
		if (montoInicial < 0m)
		{
			throw new ArgumentException("El monto inicial no puede ser negativo.");
		}
		if (Sesion.UsuarioActual == null)
		{
			throw new InvalidOperationException("No hay un usuario en sesión.");
		}
		if (_datos.ObtenerAbierta() != null)
		{
			throw new InvalidOperationException("Ya hay una caja abierta. Ciérrela antes de abrir otra.");
		}

		int id = _datos.Abrir(Sesion.UsuarioActual.Id, montoInicial);
		if (id <= 0)
		{
			throw new InvalidOperationException("Ya hay una caja abierta. Ciérrela antes de abrir otra.");
		}

		_auditoria.Registrar("caja_apertura", "cajas", id,
			$"Caja abierta con un fondo inicial de {montoInicial:C2}.");

		return _datos.ObtenerPorId(id) ?? throw new InvalidOperationException("No se pudo leer la caja recién abierta.");
	}

	public void Cerrar(int idCaja, decimal montoContado, string? observacion)
	{
		if (montoContado < 0m)
		{
			throw new ArgumentException("El efectivo contado no puede ser negativo.");
		}
		if (Sesion.UsuarioActual == null)
		{
			throw new InvalidOperationException("No hay un usuario en sesión.");
		}

		Caja caja = _datos.ObtenerPorId(idCaja)
			?? throw new ArgumentException("La caja no existe.");
		if (!caja.Abierta)
		{
			throw new InvalidOperationException("La caja ya está cerrada.");
		}

		if (observacion != null && observacion.Length > 255)
		{
			observacion = observacion.Substring(0, 255);
		}

		decimal esperado = CalcularEsperado(caja);
		if (!_datos.Cerrar(idCaja, Sesion.UsuarioActual.Id, esperado, montoContado, observacion))
		{
			throw new InvalidOperationException("La caja ya está cerrada.");
		}

		decimal diferencia = montoContado - esperado;
		_auditoria.Registrar("caja_cierre", "cajas", idCaja,
			$"Caja cerrada. Esperado {esperado:C2}, contado {montoContado:C2}, diferencia {diferencia:C2}.");
	}

	public int RegistrarMovimiento(int idCaja, string tipo, decimal monto, string? concepto)
	{
		if (tipo != "ingreso" && tipo != "egreso")
		{
			throw new ArgumentException("El tipo de movimiento no es válido.");
		}
		if (monto <= 0m)
		{
			throw new ArgumentException("El monto debe ser mayor a cero.");
		}
		if (string.IsNullOrWhiteSpace(concepto))
		{
			throw new ArgumentException("Debe ingresar un concepto.");
		}
		if (concepto.Trim().Length > 150)
		{
			concepto = concepto.Trim().Substring(0, 150);
		}
		if (Sesion.UsuarioActual == null)
		{
			throw new InvalidOperationException("No hay un usuario en sesión.");
		}

		Caja caja = _datos.ObtenerPorId(idCaja)
			?? throw new ArgumentException("La caja no existe.");
		if (!caja.Abierta)
		{
			throw new InvalidOperationException("La caja está cerrada: no admite movimientos.");
		}

		int id = _datos.RegistrarMovimiento(idCaja, tipo, monto, concepto.Trim(), Sesion.UsuarioActual.Id);
		string texto = tipo == "ingreso" ? "Ingreso" : "Egreso";
		_auditoria.Registrar("caja_" + tipo, "caja_movimientos", id,
			$"{texto} de caja por {monto:C2}: {concepto.Trim()}.");
		return id;
	}

	// Efectivo que debería haber en la caja: fondo inicial + ventas en efectivo
	// del turno + ingresos - egresos. Las ventas anuladas no se cuentan.
	public decimal CalcularEsperado(Caja caja)
	{
		if (caja == null)
		{
			throw new ArgumentNullException(nameof(caja));
		}

		decimal ventas = _datos.VentasEfectivo(caja.FechaApertura, caja.FechaCierre);
		decimal ingresos = _datos.TotalMovimientos(caja.Id, "ingreso");
		decimal egresos = _datos.TotalMovimientos(caja.Id, "egreso");
		return caja.MontoInicial + ventas + ingresos - egresos;
	}

	public decimal VentasEfectivo(Caja caja) => _datos.VentasEfectivo(caja.FechaApertura, caja.FechaCierre);

	public decimal TotalMovimientos(int idCaja, string tipo) => _datos.TotalMovimientos(idCaja, tipo);

	public DataTable ResumenVentas(Caja caja) => _datos.ResumenVentas(caja.FechaApertura, caja.FechaCierre);

	public DataTable ListarMovimientos(int idCaja) => _datos.ListarMovimientos(idCaja);

	public DataTable Listar(DateTime desde, DateTime hasta) => _datos.Listar(desde, hasta);
}
