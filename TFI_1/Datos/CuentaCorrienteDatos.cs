using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

public class CuentaCorrienteDatos
{
	public CuentaCorriente? ObtenerPorCliente(int idCliente)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT id_cuenta_corriente, id_cliente, saldo, fecha_actualizacion " +
			"FROM cuentas_corrientes WHERE id_cliente = @cliente;", conexion);
		cmd.Parameters.AddWithValue("@cliente", idCliente);
		conexion.Open();
		using MySqlDataReader rd = cmd.ExecuteReader();
		if (!rd.Read())
			return null;
		return new CuentaCorriente
		{
			Id = rd.GetInt32("id_cuenta_corriente"),
			IdCliente = rd.GetInt32("id_cliente"),
			Saldo = rd.GetDecimal("saldo"),
			FechaActualizacion = rd.GetDateTime("fecha_actualizacion")
		};
	}

	public int CrearSiNoExiste(int idCliente)
	{
		var existente = ObtenerPorCliente(idCliente);
		if (existente != null)
			return existente.Id;

		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"INSERT INTO cuentas_corrientes (id_cliente, saldo) VALUES (@cliente, 0); " +
			"SELECT LAST_INSERT_ID();", conexion);
		cmd.Parameters.AddWithValue("@cliente", idCliente);
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar());
	}

	public decimal ObtenerSaldo(int idCliente)
	{
		var cuenta = ObtenerPorCliente(idCliente);
		return cuenta?.Saldo ?? 0m;
	}

	public void ActualizarSaldo(int idCuenta, decimal nuevoSaldo)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"UPDATE cuentas_corrientes SET saldo = @saldo WHERE id_cuenta_corriente = @id;", conexion);
		cmd.Parameters.AddWithValue("@saldo", nuevoSaldo);
		cmd.Parameters.AddWithValue("@id", idCuenta);
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public List<MovimientoCuentaCorriente> ListarMovimientos(int idCuenta, DateTime? desde = null, DateTime? hasta = null)
	{
		List<MovimientoCuentaCorriente> lista = new();
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT m.id_movimiento, m.id_cuenta, m.fecha, m.tipo, m.monto, m.concepto, " +
			"       m.id_venta, m.id_usuario " +
			"FROM movimientos_cuenta_corriente AS m " +
			"WHERE m.id_cuenta = @cuenta " +
			"  AND (@desde IS NULL OR DATE(m.fecha) >= @desde) " +
			"  AND (@hasta IS NULL OR DATE(m.fecha) <= @hasta) " +
			"ORDER BY m.fecha DESC, m.id_movimiento DESC;", conexion);
		cmd.Parameters.AddWithValue("@cuenta", idCuenta);
		cmd.Parameters.AddWithValue("@desde", ((object?)desde?.Date) ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@hasta", ((object?)hasta?.Date) ?? DBNull.Value);
		conexion.Open();
		using MySqlDataReader rd = cmd.ExecuteReader();
		while (rd.Read())
			lista.Add(Leer(rd));
		return lista;
	}

	public int InsertarMovimiento(MovimientoCuentaCorriente movimiento)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"INSERT INTO movimientos_cuenta_corriente (id_cuenta, fecha, tipo, monto, concepto, id_venta, id_usuario) " +
			"VALUES (@cuenta, @fecha, @tipo, @monto, @concepto, @idVenta, @idUsuario); " +
			"SELECT LAST_INSERT_ID();", conexion);
		cmd.Parameters.AddWithValue("@cuenta", movimiento.IdCuenta);
		cmd.Parameters.AddWithValue("@fecha", movimiento.Fecha);
		cmd.Parameters.AddWithValue("@tipo", movimiento.Tipo == TipoMovimientoCC.Cargo ? "cargo" : "abono");
		cmd.Parameters.AddWithValue("@monto", movimiento.Monto);
		cmd.Parameters.AddWithValue("@concepto", (object?)movimiento.Concepto ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@idVenta", (object?)movimiento.IdVenta ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@idUsuario", movimiento.IdUsuario);
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar());
	}

	// Registra un pago (abono) y descuenta el saldo adeudado dentro de una transacción,
	// para que el movimiento y el saldo queden siempre consistentes.
	public int RegistrarAbono(int idCliente, decimal monto, string? concepto, int idUsuario, DateTime fecha)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		conexion.Open();
		using MySqlTransaction transaccion = conexion.BeginTransaction();
		try
		{
			int idCuenta;
			using (MySqlCommand cmdCuenta = new MySqlCommand(
				"SELECT id_cuenta_corriente FROM cuentas_corrientes WHERE id_cliente = @cliente FOR UPDATE;",
				conexion, transaccion))
			{
				cmdCuenta.Parameters.AddWithValue("@cliente", idCliente);
				object? resultado = cmdCuenta.ExecuteScalar();
				if (resultado == null || resultado == DBNull.Value)
				{
					using MySqlCommand cmdNueva = new MySqlCommand(
						"INSERT INTO cuentas_corrientes (id_cliente, saldo) VALUES (@cliente, 0); " +
						"SELECT LAST_INSERT_ID();", conexion, transaccion);
					cmdNueva.Parameters.AddWithValue("@cliente", idCliente);
					idCuenta = Convert.ToInt32(cmdNueva.ExecuteScalar());
				}
				else
				{
					idCuenta = Convert.ToInt32(resultado);
				}
			}

			int idMovimiento;
			using (MySqlCommand cmdMov = new MySqlCommand(
				"INSERT INTO movimientos_cuenta_corriente (id_cuenta, fecha, tipo, monto, concepto, id_venta, id_usuario) " +
				"VALUES (@cuenta, @fecha, 'abono', @monto, @concepto, NULL, @usuario); " +
				"SELECT LAST_INSERT_ID();", conexion, transaccion))
			{
				cmdMov.Parameters.AddWithValue("@cuenta", idCuenta);
				cmdMov.Parameters.AddWithValue("@fecha", fecha);
				cmdMov.Parameters.AddWithValue("@monto", monto);
				cmdMov.Parameters.AddWithValue("@concepto", (object?)concepto ?? DBNull.Value);
				cmdMov.Parameters.AddWithValue("@usuario", idUsuario);
				idMovimiento = Convert.ToInt32(cmdMov.ExecuteScalar());
			}

			using (MySqlCommand cmdSaldo = new MySqlCommand(
				"UPDATE cuentas_corrientes SET saldo = saldo - @monto, fecha_actualizacion = NOW() " +
				"WHERE id_cuenta_corriente = @cuenta;", conexion, transaccion))
			{
				cmdSaldo.Parameters.AddWithValue("@monto", monto);
				cmdSaldo.Parameters.AddWithValue("@cuenta", idCuenta);
				cmdSaldo.ExecuteNonQuery();
			}

			transaccion.Commit();
			return idMovimiento;
		}
		catch
		{
			transaccion.Rollback();
			throw;
		}
	}

	private static MovimientoCuentaCorriente Leer(MySqlDataReader rd)
	{
		return new MovimientoCuentaCorriente
		{
			Id = rd.GetInt32("id_movimiento"),
			IdCuenta = rd.GetInt32("id_cuenta"),
			Fecha = rd.GetDateTime("fecha"),
			Tipo = rd.GetString("tipo") == "cargo" ? TipoMovimientoCC.Cargo : TipoMovimientoCC.Abono,
			Monto = rd.GetDecimal("monto"),
			Concepto = rd.IsDBNull(rd.GetOrdinal("concepto")) ? null : rd.GetString("concepto"),
			IdVenta = rd.IsDBNull(rd.GetOrdinal("id_venta")) ? null : rd.GetInt32("id_venta"),
			IdUsuario = rd.GetInt32("id_usuario")
		};
	}

	public DataTable MovimientosResumen(int idCliente, DateTime desde, DateTime hasta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT m.fecha, m.tipo, m.monto, m.concepto, m.id_venta, u.nombre_usuario " +
			"FROM movimientos_cuenta_corriente AS m " +
			"INNER JOIN cuentas_corrientes AS c ON c.id_cuenta_corriente = m.id_cuenta " +
			"INNER JOIN usuarios AS u ON u.id_usuario = m.id_usuario " +
			"WHERE c.id_cliente = @cliente " +
			"  AND DATE(m.fecha) BETWEEN @desde AND @hasta " +
			"ORDER BY m.fecha DESC;", conexion);
		cmd.Parameters.AddWithValue("@cliente", idCliente);
		cmd.Parameters.AddWithValue("@desde", desde.Date);
		cmd.Parameters.AddWithValue("@hasta", hasta.Date);
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new();
		adaptador.Fill(tabla);
		return tabla;
	}
}
