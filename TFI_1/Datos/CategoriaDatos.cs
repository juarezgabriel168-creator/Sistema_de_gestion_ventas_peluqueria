using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

public class CategoriaDatos
{
	public List<Categoria> Listar(string? filtro = null)
	{
		List<Categoria> lista = new List<Categoria>();
		using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
		{
			using MySqlCommand cmd = new MySqlCommand("\n                SELECT id_categoria, nombre, activo\n                FROM categorias\n                WHERE (@filtro = '' OR nombre LIKE CONCAT('%', @filtro, '%'))\n                ORDER BY nombre;", conexion);
			cmd.Parameters.Add(Parametros.Texto("@filtro", filtro ?? string.Empty));
			conexion.Open();
			using MySqlDataReader rd = cmd.ExecuteReader();
			while (rd.Read())
			{
				lista.Add(new Categoria
				{
					Id = rd.GetInt32("id_categoria"),
					Nombre = rd.GetString("nombre"),
					Activo = rd.GetBoolean("activo")
				});
			}
		}
		return lista;
	}

	public bool ExisteNombre(string nombre, int? exceptoId = null)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("\n                SELECT COUNT(*)\n                FROM categorias\n                WHERE nombre = @nombre\n                  AND (@exceptoId IS NULL OR id_categoria <> @exceptoId);", conexion);
		cmd.Parameters.Add(Parametros.Texto("@nombre", nombre));
		cmd.Parameters.Add(Parametros.EnteroNulo("@exceptoId", exceptoId));
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
	}

	public int Insertar(Categoria categoria)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("\n                INSERT INTO categorias (nombre, activo)\n                VALUES (@nombre, @activo);\n                SELECT LAST_INSERT_ID();", conexion);
		cmd.Parameters.Add(Parametros.Texto("@nombre", categoria.Nombre));
		cmd.Parameters.Add(Parametros.Booleano("@activo", categoria.Activo));
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar());
	}

	public void Actualizar(Categoria categoria)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("\n                UPDATE categorias\n                SET nombre = @nombre, activo = @activo\n                WHERE id_categoria = @id;", conexion);
		cmd.Parameters.Add(Parametros.Texto("@nombre", categoria.Nombre));
		cmd.Parameters.Add(Parametros.Booleano("@activo", categoria.Activo));
		cmd.Parameters.Add(Parametros.Entero("@id", categoria.Id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public void Desactivar(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("UPDATE categorias SET activo = 0 WHERE id_categoria = @id;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public void Reactivar(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("UPDATE categorias SET activo = 1 WHERE id_categoria = @id;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public bool TieneDependencias(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT COUNT(*) FROM items WHERE id_categoria = @id;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
	}

	public void Eliminar(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("DELETE FROM categorias WHERE id_categoria = @id;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}
}
