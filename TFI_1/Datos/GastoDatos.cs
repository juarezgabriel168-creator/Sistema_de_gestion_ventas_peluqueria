using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

public class GastoDatos
{
	public List<Gasto> Listar(DateTime? desde = null, DateTime? hasta = null, string? filtro = null)
	{
		List<Gasto> lista = new();
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT g.id_gasto, g.fecha, g.concepto, g.monto, g.categoria, g.observacion, " +
			"       g.id_usuario, g.fecha_alta, u.nombre_usuario " +
			"FROM gastos AS g " +
			"INNER JOIN usuarios AS u ON u.id_usuario = g.id_usuario " +
			"WHERE (@desde IS NULL OR g.fecha >= @desde) " +
			"  AND (@hasta IS NULL OR g.fecha <= @hasta) " +
			"  AND (@filtro = '' OR g.concepto LIKE CONCAT('%', @filtro, '%') " +
			"               OR g.categoria LIKE CONCAT('%', @filtro, '%')) " +
			"ORDER BY g.fecha DESC, g.id_gasto DESC;", conexion);
		cmd.Parameters.AddWithValue("@desde", ((object?)desde?.Date) ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@hasta", ((object?)hasta?.Date) ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@filtro", filtro ?? string.Empty);
		conexion.Open();
		using MySqlDataReader rd = cmd.ExecuteReader();
		while (rd.Read())
			lista.Add(Leer(rd));
		return lista;
	}

	public Gasto? ObtenerPorId(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT g.id_gasto, g.fecha, g.concepto, g.monto, g.categoria, g.observacion, " +
			"       g.id_usuario, g.fecha_alta, u.nombre_usuario " +
			"FROM gastos AS g " +
			"INNER JOIN usuarios AS u ON u.id_usuario = g.id_usuario " +
			"WHERE g.id_gasto = @id;", conexion);
		cmd.Parameters.AddWithValue("@id", id);
		conexion.Open();
		using MySqlDataReader rd = cmd.ExecuteReader();
		return rd.Read() ? Leer(rd) : null;
	}

	public int Insertar(Gasto gasto)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"INSERT INTO gastos (fecha, concepto, monto, categoria, observacion, id_usuario) " +
			"VALUES (@fecha, @concepto, @monto, @categoria, @observacion, @idUsuario); " +
			"SELECT LAST_INSERT_ID();", conexion);
		cmd.Parameters.AddWithValue("@fecha", gasto.Fecha);
		cmd.Parameters.AddWithValue("@concepto", gasto.Concepto);
		cmd.Parameters.AddWithValue("@monto", gasto.Monto);
		cmd.Parameters.AddWithValue("@categoria", (object?)gasto.Categoria ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@observacion", (object?)gasto.Observacion ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@idUsuario", gasto.IdUsuario);
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar());
	}

	public void Actualizar(Gasto gasto)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"UPDATE gastos SET fecha = @fecha, concepto = @concepto, monto = @monto, " +
			"    categoria = @categoria, observacion = @observacion " +
			"WHERE id_gasto = @id;", conexion);
		cmd.Parameters.AddWithValue("@id", gasto.Id);
		cmd.Parameters.AddWithValue("@fecha", gasto.Fecha);
		cmd.Parameters.AddWithValue("@concepto", gasto.Concepto);
		cmd.Parameters.AddWithValue("@monto", gasto.Monto);
		cmd.Parameters.AddWithValue("@categoria", (object?)gasto.Categoria ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@observacion", (object?)gasto.Observacion ?? DBNull.Value);
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public void Desactivar(int id)
	{
		// Los gastos no se borran físicamente; se deja un registro histórico.
		// Si se necesita, se podría agregar una columna 'activo' a la tabla gastos.
		throw new NotSupportedException("Los gastos no se desactivan; se conservan como historial.");
	}

	private static Gasto Leer(MySqlDataReader rd)
	{
		return new Gasto
		{
			Id = rd.GetInt32("id_gasto"),
			Fecha = rd.GetDateTime("fecha"),
			Concepto = rd.GetString("concepto"),
			Monto = rd.GetDecimal("monto"),
			Categoria = rd.IsDBNull(rd.GetOrdinal("categoria")) ? null : rd.GetString("categoria"),
			Observacion = rd.IsDBNull(rd.GetOrdinal("observacion")) ? null : rd.GetString("observacion"),
			IdUsuario = rd.GetInt32("id_usuario"),
			NombreUsuario = rd.IsDBNull(rd.GetOrdinal("nombre_usuario")) ? null : rd.GetString("nombre_usuario"),
			FechaAlta = rd.GetDateTime("fecha_alta")
		};
	}

	public decimal TotalGastos(DateTime desde, DateTime hasta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT COALESCE(SUM(monto), 0) FROM gastos WHERE fecha BETWEEN @desde AND @hasta;", conexion);
		cmd.Parameters.AddWithValue("@desde", desde.Date);
		cmd.Parameters.AddWithValue("@hasta", hasta.Date);
		conexion.Open();
		return Convert.ToDecimal(cmd.ExecuteScalar());
	}

	public DataTable GastosPorCategoria(DateTime desde, DateTime hasta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT COALESCE(categoria, 'Sin categoría') AS Categoria, COUNT(*) AS Cantidad, " +
			"       COALESCE(SUM(monto), 0) AS Total " +
			"FROM gastos WHERE fecha BETWEEN @desde AND @hasta " +
			"GROUP BY categoria ORDER BY Total DESC;", conexion);
		cmd.Parameters.AddWithValue("@desde", desde.Date);
		cmd.Parameters.AddWithValue("@hasta", hasta.Date);
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new();
		adaptador.Fill(tabla);
		return tabla;
	}
}
