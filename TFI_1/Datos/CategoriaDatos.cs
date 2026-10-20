using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

public class CategoriaDatos
{
	public List<Categoria> Listar(string? filtro = null)
	{
		List<Categoria> list = new List<Categoria>();
		using (MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion())
		{
			using MySqlCommand mySqlCommand = new MySqlCommand("\n                SELECT id_categoria, nombre, activo\n                FROM categorias\n                WHERE (@filtro = '' OR nombre LIKE CONCAT('%', @filtro, '%'))\n                ORDER BY nombre;", mySqlConnection);
			mySqlCommand.Parameters.AddWithValue("@filtro", filtro ?? string.Empty);
			mySqlConnection.Open();
			using MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
			while (mySqlDataReader.Read())
			{
				list.Add(new Categoria
				{
					Id = mySqlDataReader.GetInt32("id_categoria"),
					Nombre = mySqlDataReader.GetString("nombre"),
					Activo = mySqlDataReader.GetBoolean("activo")
				});
			}
		}
		return list;
	}

	public bool ExisteNombre(string nombre, int? exceptoId = null)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("\n                SELECT COUNT(*)\n                FROM categorias\n                WHERE nombre = @nombre\n                  AND (@exceptoId IS NULL OR id_categoria <> @exceptoId);", mySqlConnection);
		mySqlCommand.Parameters.AddWithValue("@nombre", nombre);
		mySqlCommand.Parameters.AddWithValue("@exceptoId", ((object)exceptoId) ?? DBNull.Value);
		mySqlConnection.Open();
		return Convert.ToInt32(mySqlCommand.ExecuteScalar()) > 0;
	}

	public int Insertar(Categoria categoria)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("\n                INSERT INTO categorias (nombre, activo)\n                VALUES (@nombre, @activo);\n                SELECT LAST_INSERT_ID();", mySqlConnection);
		mySqlCommand.Parameters.AddWithValue("@nombre", categoria.Nombre);
		mySqlCommand.Parameters.AddWithValue("@activo", categoria.Activo);
		mySqlConnection.Open();
		return Convert.ToInt32(mySqlCommand.ExecuteScalar());
	}

	public void Actualizar(Categoria categoria)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("\n                UPDATE categorias\n                SET nombre = @nombre, activo = @activo\n                WHERE id_categoria = @id;", mySqlConnection);
		mySqlCommand.Parameters.AddWithValue("@nombre", categoria.Nombre);
		mySqlCommand.Parameters.AddWithValue("@activo", categoria.Activo);
		mySqlCommand.Parameters.AddWithValue("@id", categoria.Id);
		mySqlConnection.Open();
		mySqlCommand.ExecuteNonQuery();
	}

	public void Desactivar(int id)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("UPDATE categorias SET activo = 0 WHERE id_categoria = @id;", mySqlConnection);
		mySqlCommand.Parameters.AddWithValue("@id", id);
		mySqlConnection.Open();
		mySqlCommand.ExecuteNonQuery();
	}

	public void Reactivar(int id)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("UPDATE categorias SET activo = 1 WHERE id_categoria = @id;", mySqlConnection);
		mySqlCommand.Parameters.AddWithValue("@id", id);
		mySqlConnection.Open();
		mySqlCommand.ExecuteNonQuery();
	}
}
