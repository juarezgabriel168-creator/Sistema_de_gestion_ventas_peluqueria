using System;
using System.Data;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

/// <summary>
/// Acceso a las cajas y a sus movimientos (ingresos y egresos de efectivo).
/// </summary>
public class CajaDatos
{
	private const string SelectCaja =
		"SELECT c.id_caja, c.id_usuario_apertura, ua.nombre_usuario AS usuario_apertura, " +
		"       c.fecha_apertura, c.monto_inicial, " +
		"       c.id_usuario_cierre, uc.nombre_usuario AS usuario_cierre, " +
		"       c.fecha_cierre, c.monto_esperado, c.monto_contado, c.diferencia, " +
		"       c.observacion, c.estado " +
		"FROM cajas AS c " +
		"INNER JOIN usuarios AS ua ON ua.id_usuario = c.id_usuario_apertura " +
		"LEFT JOIN usuarios AS uc ON uc.id_usuario = c.id_usuario_cierre ";

	// Abre una caja solo si no hay otra abierta. Devuelve 0 si ya había una.
	public int Abrir(int usuarioId, decimal montoInicial)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"INSERT INTO cajas (id_usuario_apertura, fecha_apertura, monto_inicial, estado) " +
			"SELECT @usuario, NOW(), @monto, 'abierta' " +
			"WHERE NOT EXISTS (SELECT 1 FROM cajas WHERE estado = 'abierta'); " +
			"SELECT IF(ROW_COUNT() = 1, LAST_INSERT_ID(), 0);", conexion);
		cmd.Parameters.AddWithValue("@usuario", usuarioId);
		cmd.Parameters.AddWithValue("@monto", montoInicial);
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar());
	}

	public Caja? ObtenerAbierta()
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			SelectCaja + "WHERE c.estado = 'abierta' ORDER BY c.id_caja DESC LIMIT 1;", conexion);
		conexion.Open();
		using MySqlDataReader rd = cmd.ExecuteReader();
		return rd.Read() ? Leer(rd) : null;
	}

	public Caja? ObtenerPorId(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(SelectCaja + "WHERE c.id_caja = @id;", conexion);
		cmd.Parameters.AddWithValue("@id", id);
		conexion.Open();
		using MySqlDataReader rd = cmd.ExecuteReader();
		return rd.Read() ? Leer(rd) : null;
	}

	public bool Cerrar(int idCaja, int usuarioId, decimal montoEsperado, decimal montoContado, string? observacion)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"UPDATE cajas SET id_usuario_cierre = @usuario, fecha_cierre = NOW(), " +
			"       monto_esperado = @esperado, monto_contado = @contado, " +
			"       diferencia = @contado - @esperado, observacion = @observacion, estado = 'cerrada' " +
			"WHERE id_caja = @id AND estado = 'abierta';", conexion);
		cmd.Parameters.AddWithValue("@usuario", usuarioId);
		cmd.Parameters.AddWithValue("@esperado", montoEsperado);
		cmd.Parameters.AddWithValue("@contado", montoContado);
		cmd.Parameters.AddWithValue("@observacion",
			string.IsNullOrWhiteSpace(observacion) ? DBNull.Value : ((object?)observacion));
		cmd.Parameters.AddWithValue("@id", idCaja);
		conexion.Open();
		return cmd.ExecuteNonQuery() == 1;
	}

	public int RegistrarMovimiento(int idCaja, string tipo, decimal monto, string concepto, int usuarioId)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"INSERT INTO caja_movimientos (id_caja, fecha, tipo, monto, concepto, id_usuario) " +
			"VALUES (@caja, NOW(), @tipo, @monto, @concepto, @usuario); " +
			"SELECT LAST_INSERT_ID();", conexion);
		cmd.Parameters.AddWithValue("@caja", idCaja);
		cmd.Parameters.AddWithValue("@tipo", tipo);
		cmd.Parameters.AddWithValue("@monto", monto);
		cmd.Parameters.AddWithValue("@concepto", concepto);
		cmd.Parameters.AddWithValue("@usuario", usuarioId);
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar());
	}

	public DataTable ListarMovimientos(int idCaja)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT m.id_movimiento_caja AS Id, m.fecha AS Fecha, m.tipo AS Tipo, " +
			"       m.concepto AS Concepto, m.monto AS Monto, u.nombre_usuario AS Usuario " +
			"FROM caja_movimientos AS m " +
			"INNER JOIN usuarios AS u ON u.id_usuario = m.id_usuario " +
			"WHERE m.id_caja = @caja " +
			"ORDER BY m.fecha, m.id_movimiento_caja;", conexion);
		cmd.Parameters.AddWithValue("@caja", idCaja);
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new DataTable();
		adaptador.Fill(tabla);
		return tabla;
	}

	// Ventas del turno agrupadas por medio de pago (las anuladas no cuentan).
	public DataTable ResumenVentas(DateTime desde, DateTime? hasta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT v.medio_pago AS MedioPago, COUNT(*) AS Cantidad, " +
			"       COALESCE(SUM(v.total), 0) AS Total " +
			"FROM ventas AS v " +
			"WHERE v.fecha >= @desde " +
			"  AND (@hasta IS NULL OR v.fecha <= @hasta) " +
			"  AND v.anulada = 0 " +
			"GROUP BY v.medio_pago " +
			"ORDER BY v.medio_pago;", conexion);
		cmd.Parameters.AddWithValue("@desde", desde);
		cmd.Parameters.AddWithValue("@hasta", ((object?)hasta) ?? DBNull.Value);
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new DataTable();
		adaptador.Fill(tabla);
		return tabla;
	}

	public decimal VentasEfectivo(DateTime desde, DateTime? hasta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT COALESCE(SUM(total), 0) FROM ventas " +
			"WHERE fecha >= @desde " +
			"  AND (@hasta IS NULL OR fecha <= @hasta) " +
			"  AND anulada = 0 AND medio_pago = 'efectivo';", conexion);
		cmd.Parameters.AddWithValue("@desde", desde);
		cmd.Parameters.AddWithValue("@hasta", ((object?)hasta) ?? DBNull.Value);
		conexion.Open();
		return Convert.ToDecimal(cmd.ExecuteScalar());
	}

	public decimal TotalMovimientos(int idCaja, string tipo)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT COALESCE(SUM(monto), 0) FROM caja_movimientos " +
			"WHERE id_caja = @caja AND tipo = @tipo;", conexion);
		cmd.Parameters.AddWithValue("@caja", idCaja);
		cmd.Parameters.AddWithValue("@tipo", tipo);
		conexion.Open();
		return Convert.ToDecimal(cmd.ExecuteScalar());
	}

	public DataTable Listar(DateTime desde, DateTime hasta, int? limite = null, int? desplazamiento = null)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT c.id_caja AS Id, c.fecha_apertura AS Apertura, " +
			"       ua.nombre_usuario AS UsuarioApertura, c.monto_inicial AS Inicial, " +
			"       c.fecha_cierre AS Cierre, uc.nombre_usuario AS UsuarioCierre, " +
			"       c.monto_esperado AS Esperado, c.monto_contado AS Contado, " +
			"       c.diferencia AS Diferencia, c.estado AS Estado, c.observacion AS Observacion " +
			"FROM cajas AS c " +
			"INNER JOIN usuarios AS ua ON ua.id_usuario = c.id_usuario_apertura " +
			"LEFT JOIN usuarios AS uc ON uc.id_usuario = c.id_usuario_cierre " +
			"WHERE DATE(c.fecha_apertura) BETWEEN @desde AND @hasta " +
			"ORDER BY c.fecha_apertura DESC, c.id_caja DESC " +
			"LIMIT @limite OFFSET @desplazamiento;", conexion);
		cmd.Parameters.AddWithValue("@desde", desde.Date);
		cmd.Parameters.AddWithValue("@hasta", hasta.Date);
		cmd.Parameters.AddWithValue("@limite", limite ?? int.MaxValue);
		cmd.Parameters.AddWithValue("@desplazamiento", desplazamiento ?? 0);
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new DataTable();
		adaptador.Fill(tabla);
		return tabla;
	}

	/// <summary>Cuenta los turnos de caja del período.</summary>
	public int Contar(DateTime desde, DateTime hasta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT COUNT(*) FROM cajas AS c " +
			"WHERE DATE(c.fecha_apertura) BETWEEN @desde AND @hasta;", conexion);
		cmd.Parameters.AddWithValue("@desde", desde.Date);
		cmd.Parameters.AddWithValue("@hasta", hasta.Date);
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar());
	}

	private static bool EsNulo(MySqlDataReader rd, string columna) =>
		rd.IsDBNull(rd.GetOrdinal(columna));

	private static Caja Leer(MySqlDataReader rd)
	{
		return new Caja
		{
			Id = rd.GetInt32("id_caja"),
			IdUsuarioApertura = rd.GetInt32("id_usuario_apertura"),
			UsuarioApertura = rd.GetString("usuario_apertura"),
			FechaApertura = rd.GetDateTime("fecha_apertura"),
			MontoInicial = rd.GetDecimal("monto_inicial"),
			IdUsuarioCierre = EsNulo(rd, "id_usuario_cierre") ? null : rd.GetInt32("id_usuario_cierre"),
			UsuarioCierre = EsNulo(rd, "usuario_cierre") ? null : rd.GetString("usuario_cierre"),
			FechaCierre = EsNulo(rd, "fecha_cierre") ? null : rd.GetDateTime("fecha_cierre"),
			MontoEsperado = EsNulo(rd, "monto_esperado") ? null : rd.GetDecimal("monto_esperado"),
			MontoContado = EsNulo(rd, "monto_contado") ? null : rd.GetDecimal("monto_contado"),
			Diferencia = EsNulo(rd, "diferencia") ? null : rd.GetDecimal("diferencia"),
			Observacion = EsNulo(rd, "observacion") ? null : rd.GetString("observacion"),
			Estado = rd.GetString("estado")
		};
	}
}
