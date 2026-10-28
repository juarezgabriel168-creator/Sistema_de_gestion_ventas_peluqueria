using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

public class ClienteDatos
{
	public List<Cliente> Listar(string? filtro = null)
	{
		List<Cliente> lista = new List<Cliente>();
		using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
		{
			using MySqlCommand cmd = new MySqlCommand("\n                SELECT id_cliente, nombre, apellido, dni, telefono, activo\n                FROM clientes\n                WHERE (@filtro = ''\n                       OR dni LIKE CONCAT('%', @filtro, '%')\n                       OR apellido LIKE CONCAT('%', @filtro, '%')\n                       OR nombre LIKE CONCAT('%', @filtro, '%'))\n                ORDER BY apellido, nombre;", conexion);
			cmd.Parameters.Add(Parametros.Texto("@filtro", filtro ?? string.Empty));
			conexion.Open();
			using MySqlDataReader rd = cmd.ExecuteReader();
			while (rd.Read())
			{
				lista.Add(Leer(rd));
			}
		}
		return lista;
	}

	public bool ExisteDni(string dni, int? exceptoId = null)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("\n                SELECT COUNT(*)\n                FROM clientes\n                WHERE dni = @dni\n                  AND (@exceptoId IS NULL OR id_cliente <> @exceptoId);", conexion);
		cmd.Parameters.Add(Parametros.Texto("@dni", dni));
		cmd.Parameters.Add(Parametros.EnteroNulo("@exceptoId", exceptoId));
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
	}

	public Cliente? ObtenerPorDni(string dni)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT id_cliente, nombre, apellido, dni, telefono, activo " +
			"FROM clientes WHERE dni = @dni LIMIT 1;", conexion);
		cmd.Parameters.Add(Parametros.Texto("@dni", dni));
		conexion.Open();
		using MySqlDataReader rd = cmd.ExecuteReader();
		if (rd.Read())
			return Leer(rd);
		return null;
	}

	public int Insertar(Cliente cliente)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("\n                INSERT INTO clientes (nombre, apellido, dni, telefono, activo)\n                VALUES (@nombre, @apellido, @dni, @telefono, @activo);\n                SELECT LAST_INSERT_ID();", conexion);
		AgregarParametros(cmd, cliente);
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar());
	}

	public void Actualizar(Cliente cliente)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("\n                UPDATE clientes\n                SET nombre = @nombre,\n                    apellido = @apellido,\n                    dni = @dni,\n                    telefono = @telefono,\n                    activo = @activo\n                WHERE id_cliente = @id;", conexion);
		AgregarParametros(cmd, cliente);
		cmd.Parameters.Add(Parametros.Entero("@id", cliente.Id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public void Desactivar(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("UPDATE clientes SET activo = 0 WHERE id_cliente = @id;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public void Reactivar(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("UPDATE clientes SET activo = 1 WHERE id_cliente = @id;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public bool EsConsumidorFinal(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT COUNT(*) FROM clientes WHERE id_cliente = @id AND dni = @dni;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		cmd.Parameters.Add(Parametros.Texto("@dni", Cliente.DniConsumidorFinal));
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
	}

	/// <summary>
	/// Indica si el cliente está referenciado por ventas, cuenta corriente
	/// o turnos. En ese caso no se puede borrar sin romper el historial.
	/// </summary>
	public bool TieneDependencias(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT (SELECT COUNT(*) FROM ventas WHERE id_cliente = @id) " +
			"     + (SELECT COUNT(*) FROM cuentas_corrientes WHERE id_cliente = @id) " +
			"     + (SELECT COUNT(*) FROM atenciones WHERE id_cliente = @id);", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
	}

	public void Eliminar(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("DELETE FROM clientes WHERE id_cliente = @id;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	private static void AgregarParametros(MySqlCommand cmd, Cliente cliente)
	{
		cmd.Parameters.Add(Parametros.Texto("@nombre", cliente.Nombre));
		cmd.Parameters.Add(Parametros.Texto("@apellido", cliente.Apellido));
		cmd.Parameters.Add(Parametros.Texto("@dni", cliente.Dni));
		cmd.Parameters.Add(Parametros.Texto("@telefono", string.IsNullOrWhiteSpace(cliente.Telefono) ? null : cliente.Telefono));
		cmd.Parameters.Add(Parametros.Booleano("@activo", cliente.Activo));
	}

	private static Cliente Leer(MySqlDataReader rd)
	{
		int ordinal = rd.GetOrdinal("telefono");
		return new Cliente
		{
			Id = rd.GetInt32("id_cliente"),
			Nombre = rd.GetString("nombre"),
			Apellido = rd.GetString("apellido"),
			Dni = rd.GetString("dni"),
			Telefono = (rd.IsDBNull(ordinal) ? string.Empty : rd.GetString(ordinal)),
			Activo = rd.GetBoolean("activo")
		};
	}
}
