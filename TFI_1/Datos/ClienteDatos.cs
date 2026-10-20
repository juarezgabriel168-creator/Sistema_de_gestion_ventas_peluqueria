using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

public class ClienteDatos
{
	public List<Cliente> Listar(string? filtro = null)
	{
		List<Cliente> list = new List<Cliente>();
		using (MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion())
		{
			using MySqlCommand mySqlCommand = new MySqlCommand("\n                SELECT id_cliente, nombre, apellido, dni, telefono, activo\n                FROM clientes\n                WHERE (@filtro = ''\n                       OR dni LIKE CONCAT('%', @filtro, '%')\n                       OR apellido LIKE CONCAT('%', @filtro, '%')\n                       OR nombre LIKE CONCAT('%', @filtro, '%'))\n                ORDER BY apellido, nombre;", mySqlConnection);
			mySqlCommand.Parameters.AddWithValue("@filtro", filtro ?? string.Empty);
			mySqlConnection.Open();
			using MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
			while (mySqlDataReader.Read())
			{
				list.Add(Leer(mySqlDataReader));
			}
		}
		return list;
	}

	public bool ExisteDni(string dni, int? exceptoId = null)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("\n                SELECT COUNT(*)\n                FROM clientes\n                WHERE dni = @dni\n                  AND (@exceptoId IS NULL OR id_cliente <> @exceptoId);", mySqlConnection);
		mySqlCommand.Parameters.AddWithValue("@dni", dni);
		mySqlCommand.Parameters.AddWithValue("@exceptoId", ((object?)exceptoId) ?? DBNull.Value);
		mySqlConnection.Open();
		return Convert.ToInt32(mySqlCommand.ExecuteScalar()) > 0;
	}

	public Cliente? ObtenerPorDni(string dni)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand(
			"SELECT id_cliente, nombre, apellido, dni, telefono, activo " +
			"FROM clientes WHERE dni = @dni LIMIT 1;", mySqlConnection);
		mySqlCommand.Parameters.AddWithValue("@dni", dni);
		mySqlConnection.Open();
		using MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
		if (mySqlDataReader.Read())
			return Leer(mySqlDataReader);
		return null;
	}

	public int Insertar(Cliente cliente)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("\n                INSERT INTO clientes (nombre, apellido, dni, telefono, activo)\n                VALUES (@nombre, @apellido, @dni, @telefono, @activo);\n                SELECT LAST_INSERT_ID();", mySqlConnection);
		AgregarParametros(mySqlCommand, cliente);
		mySqlConnection.Open();
		return Convert.ToInt32(mySqlCommand.ExecuteScalar());
	}

	public void Actualizar(Cliente cliente)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("\n                UPDATE clientes\n                SET nombre = @nombre,\n                    apellido = @apellido,\n                    dni = @dni,\n                    telefono = @telefono,\n                    activo = @activo\n                WHERE id_cliente = @id;", mySqlConnection);
		AgregarParametros(mySqlCommand, cliente);
		mySqlCommand.Parameters.AddWithValue("@id", cliente.Id);
		mySqlConnection.Open();
		mySqlCommand.ExecuteNonQuery();
	}

	public void Desactivar(int id)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("UPDATE clientes SET activo = 0 WHERE id_cliente = @id;", mySqlConnection);
		mySqlCommand.Parameters.AddWithValue("@id", id);
		mySqlConnection.Open();
		mySqlCommand.ExecuteNonQuery();
	}

	public void Reactivar(int id)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("UPDATE clientes SET activo = 1 WHERE id_cliente = @id;", mySqlConnection);
		mySqlCommand.Parameters.AddWithValue("@id", id);
		mySqlConnection.Open();
		mySqlCommand.ExecuteNonQuery();
	}

	private static void AgregarParametros(MySqlCommand cmd, Cliente cliente)
	{
		cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
		cmd.Parameters.AddWithValue("@apellido", cliente.Apellido);
		cmd.Parameters.AddWithValue("@dni", cliente.Dni);
		cmd.Parameters.AddWithValue("@telefono", string.IsNullOrWhiteSpace(cliente.Telefono) ? ((IConvertible)DBNull.Value) : ((IConvertible)cliente.Telefono));
		cmd.Parameters.AddWithValue("@activo", cliente.Activo);
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
