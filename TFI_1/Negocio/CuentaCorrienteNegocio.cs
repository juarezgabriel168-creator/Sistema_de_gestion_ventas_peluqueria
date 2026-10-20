using System;
using System.Collections.Generic;
using System.Data;
using TFI_1.Datos;
using TFI_1.Models;

namespace TFI_1.Negocio;

public class CuentaCorrienteNegocio
{
	/// <summary>
	/// Saldo a partir del cual se avisa (no se bloquea) antes de seguir
	/// cargando deuda en la cuenta corriente de un cliente. Se configura
	/// en App.config (clave LimiteDeudaCuentaCorriente).
	/// </summary>
	public static decimal LimiteDeudaAviso => Configuracion.LimiteDeudaCuentaCorriente;

	private readonly CuentaCorrienteDatos _datos = new();

	private readonly AuditoriaNegocio _auditoria = new();

	public CuentaCorriente? ObtenerPorCliente(int idCliente) => _datos.ObtenerPorCliente(idCliente);

	public int CrearSiNoExiste(int idCliente) => _datos.CrearSiNoExiste(idCliente);

	public decimal ObtenerSaldo(int idCliente) => _datos.ObtenerSaldo(idCliente);

	public void ActualizarSaldo(int idCuenta, decimal nuevoSaldo) => _datos.ActualizarSaldo(idCuenta, nuevoSaldo);

	public List<MovimientoCuentaCorriente> ListarMovimientos(int idCuenta, DateTime? desde = null, DateTime? hasta = null) =>
		_datos.ListarMovimientos(idCuenta, desde, hasta);

	public int InsertarMovimiento(MovimientoCuentaCorriente movimiento)
	{
		if (movimiento == null)
			throw new ArgumentException("El movimiento es obligatorio.");
		if (movimiento.IdCuenta <= 0)
			throw new ArgumentException("Cuenta corriente inválida.");
		if (movimiento.Monto <= 0)
			throw new ArgumentException("El monto debe ser mayor a cero.");
		if (movimiento.IdUsuario <= 0)
			throw new ArgumentException("No hay un usuario en sesión.");

		return _datos.InsertarMovimiento(movimiento);
	}

	// Registra el pago de un cliente. Valida que exista deuda y que el pago no la supere.
	public int RegistrarAbono(int idCliente, decimal monto, string? concepto)
	{
		if (idCliente <= 0)
			throw new ArgumentException("Seleccione un cliente.");
		if (monto <= 0)
			throw new ArgumentException("El monto del abono debe ser mayor a cero.");
		if (Sesion.UsuarioActual == null)
			throw new ArgumentException("No hay un usuario en sesión.");

		decimal saldo = _datos.ObtenerSaldo(idCliente);
		if (saldo <= 0)
			throw new InvalidOperationException("El cliente no tiene saldo adeudado.");
		if (monto > saldo)
			throw new ArgumentException($"El abono no puede superar el saldo adeudado ({saldo:C2}).");

		string conceptoFinal = string.IsNullOrWhiteSpace(concepto) ? "Pago a cuenta" : concepto.Trim();
		int idMovimiento = _datos.RegistrarAbono(idCliente, monto, conceptoFinal, Sesion.UsuarioActual.Id, DateTime.Now);
		_auditoria.Registrar("abono", "movimientos_cuenta_corriente", idMovimiento,
			$"Pago a cuenta del cliente #{idCliente} por {monto:C2}.", Sesion.UsuarioActual.Id);
		return idMovimiento;
	}

	public DataTable MovimientosResumen(int idCliente, DateTime desde, DateTime hasta) =>
		_datos.MovimientosResumen(idCliente, desde, hasta);
}
