using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

public class TarjetaCreditoDatos
{
	private const string Select =
		"SELECT t.id_tarjeta_credito, t.id_banco, b.nombre AS nombre_banco, t.tipo_tarjeta, " +
		"       t.porcentaje_recargo, t.cuotas_max, t.activo, t.fecha_alta " +
		"FROM tarjetas_credito AS t " +
		"INNER JOIN bancos AS b ON b.id_banco = t.id_banco ";

	// Todas las tarjetas (para el formulario de ventas).
	public List<TarjetaCredito> Listar(bool soloActivas = true) =>
		Consultar("WHERE (@soloActivas = 0 OR (t.activo = 1 AND b.activo = 1)) " +
				  "ORDER BY b.nombre, t.tipo_tarjeta;",
				  Parametros.Entero("@soloActivas", soloActivas ? 1 : 0));

	public List<TarjetaCredito> ListarPorBanco(int idBanco, bool soloActivas = true) =>
		Consultar("WHERE t.id_banco = @banco AND (@soloActivas = 0 OR t.activo = 1) " +
				  "ORDER BY t.tipo_tarjeta;",
				  Parametros.Entero("@banco", idBanco),
				  Parametros.Entero("@soloActivas", soloActivas ? 1 : 0));

	public TarjetaCredito? ObtenerPorId(int id) =>
		Consultar("WHERE t.id_tarjeta_credito = @id;", Parametros.Entero("@id", id)).FirstOrDefault();

	public bool ExisteTipo(int idBanco, string tipo, int? exceptoId = null)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT COUNT(*) FROM tarjetas_credito " +
			"WHERE id_banco = @banco AND tipo_tarjeta = @tipo " +
			"  AND (@exceptoId IS NULL OR id_tarjeta_credito <> @exceptoId);", conexion);
		cmd.Parameters.Add(Parametros.Entero("@banco", idBanco));
		cmd.Parameters.Add(Parametros.Texto("@tipo", tipo));
		cmd.Parameters.Add(Parametros.EnteroNulo("@exceptoId", exceptoId));
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
	}

	public int Insertar(TarjetaCredito tarjeta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"INSERT INTO tarjetas_credito (id_banco, tipo_tarjeta, porcentaje_recargo, cuotas_max, activo) " +
			"VALUES (@banco, @tipo, @recargo, @cuotas, @activo); SELECT LAST_INSERT_ID();", conexion);
		Cargar(cmd, tarjeta);
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar());
	}

	public void Actualizar(TarjetaCredito tarjeta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"UPDATE tarjetas_credito SET id_banco = @banco, tipo_tarjeta = @tipo, " +
			"porcentaje_recargo = @recargo, cuotas_max = @cuotas, activo = @activo " +
			"WHERE id_tarjeta_credito = @id;", conexion);
		Cargar(cmd, tarjeta);
		cmd.Parameters.Add(Parametros.Entero("@id", tarjeta.Id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public void Desactivar(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"UPDATE tarjetas_credito SET activo = 0 WHERE id_tarjeta_credito = @id;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public void Reactivar(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"UPDATE tarjetas_credito SET activo = 1 WHERE id_tarjeta_credito = @id;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	private static void Cargar(MySqlCommand cmd, TarjetaCredito tarjeta)
	{
		cmd.Parameters.Add(Parametros.Entero("@banco", tarjeta.IdBanco));
		cmd.Parameters.Add(Parametros.Texto("@tipo", tarjeta.TipoTarjeta));
		cmd.Parameters.Add(Parametros.Importe("@recargo", tarjeta.PorcentajeRecargo));
		cmd.Parameters.Add(Parametros.Entero("@cuotas", tarjeta.CuotasMax));
		cmd.Parameters.Add(Parametros.Booleano("@activo", tarjeta.Activo));
	}

	private static List<TarjetaCredito> Consultar(string condicion, params MySqlParameter[] parametros)
	{
		List<TarjetaCredito> lista = new();
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(Select + condicion, conexion);
		cmd.Parameters.AddRange(parametros);
		conexion.Open();
		using MySqlDataReader rd = cmd.ExecuteReader();
		while (rd.Read())
			lista.Add(new TarjetaCredito
			{
				Id = rd.GetInt32("id_tarjeta_credito"),
				IdBanco = rd.GetInt32("id_banco"),
				NombreBanco = rd.GetString("nombre_banco"),
				TipoTarjeta = rd.GetString("tipo_tarjeta"),
				PorcentajeRecargo = rd.GetDecimal("porcentaje_recargo"),
				CuotasMax = rd.GetInt32("cuotas_max"),
				Activo = rd.GetBoolean("activo"),
				FechaAlta = rd.GetDateTime("fecha_alta")
			});
		return lista;
	}
}
