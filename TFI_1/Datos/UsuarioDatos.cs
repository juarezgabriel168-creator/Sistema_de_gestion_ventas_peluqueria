using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

public class UsuarioDatos
{
	// Igual que en AuditoriaDatos: la base puede mezclar colaciones y las
	// comparaciones de texto deben hacerse con una colación explícita.
	private const string Collation = "utf8mb4_unicode_ci";

	public Usuario? ObtenerPorNombre(string nombreUsuario)
	{
		string sql = $@"
                SELECT u.id_usuario, u.nombre_usuario, u.contrasena, u.activo,
                       r.id_rol, r.nombre AS rol
                FROM usuarios AS u
                INNER JOIN roles AS r ON r.id_rol = u.id_rol
                WHERE u.nombre_usuario = @usuario COLLATE {Collation};";

		using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
		using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
		{
			cmd.Parameters.Add(Parametros.Texto("@usuario", nombreUsuario));
			conexion.Open();

			using (MySqlDataReader rd = cmd.ExecuteReader())
			{
				if (!rd.Read()) return null;

				return new Usuario
				{
					Id = rd.GetInt32("id_usuario"),
					NombreUsuario = rd.GetString("nombre_usuario"),
					Contrasena = rd.GetString("contrasena"),
					Activo = rd.GetBoolean("activo"),
					Rol = new Rol(rd.GetInt32("id_rol"), rd.GetString("rol"))
				};
			}
		}
	}

	public List<Usuario> Listar(string? filtro = null)
	{
		string sql = $@"
                SELECT u.id_usuario, u.nombre_usuario, u.contrasena, u.activo,
                       r.id_rol, r.nombre AS rol
                FROM usuarios AS u
                INNER JOIN roles AS r ON r.id_rol = u.id_rol
                WHERE (@filtro = ''
                       OR u.nombre_usuario COLLATE {Collation}
                          LIKE CONCAT('%', @filtro, '%') COLLATE {Collation})
                ORDER BY u.nombre_usuario;";

		List<Usuario> lista = new();

		using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
		using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
		{
			cmd.Parameters.Add(Parametros.Texto("@filtro", filtro ?? string.Empty));
			conexion.Open();

			using (MySqlDataReader rd = cmd.ExecuteReader())
			{
				while (rd.Read())
				{
					lista.Add(new Usuario
					{
						Id = rd.GetInt32("id_usuario"),
						NombreUsuario = rd.GetString("nombre_usuario"),
						Contrasena = rd.GetString("contrasena"),
						Activo = rd.GetBoolean("activo"),
						Rol = new Rol(rd.GetInt32("id_rol"), rd.GetString("rol"))
					});
				}
			}
		}

		return lista;
	}

	public List<Rol> ObtenerRoles()
	{
		const string sql = "SELECT id_rol, nombre FROM roles ORDER BY nombre;";

		List<Rol> lista = new();

		using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
		using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
		{
			conexion.Open();

			using (MySqlDataReader rd = cmd.ExecuteReader())
			{
				while (rd.Read())
					lista.Add(new Rol(rd.GetInt32("id_rol"), rd.GetString("nombre")));
			}
		}

		return lista;
	}

	public bool ExisteNombre(string nombreUsuario, int? exceptoId = null)
	{
		string sql = $@"
                SELECT COUNT(*)
                FROM usuarios
                WHERE nombre_usuario = @usuario COLLATE {Collation}
                  AND (@exceptoId IS NULL OR id_usuario <> @exceptoId);";

		using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
		using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
		{
			cmd.Parameters.Add(Parametros.Texto("@usuario", nombreUsuario));
			cmd.Parameters.Add(Parametros.EnteroNulo("@exceptoId", exceptoId));
			conexion.Open();
			return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
		}
	}

	public int Insertar(Usuario usuario)
	{
		const string sql = @"
                INSERT INTO usuarios (nombre_usuario, contrasena, id_rol, activo)
                VALUES (@usuario, @contrasena, @rol, @activo);
                SELECT LAST_INSERT_ID();";

		using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
		using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
		{
			cmd.Parameters.Add(Parametros.Texto("@usuario", usuario.NombreUsuario));
			cmd.Parameters.Add(Parametros.Texto("@contrasena", usuario.Contrasena));
			cmd.Parameters.Add(Parametros.Entero("@rol", usuario.Rol.Id));
			cmd.Parameters.Add(Parametros.Booleano("@activo", usuario.Activo));
			conexion.Open();
			return Convert.ToInt32(cmd.ExecuteScalar());
		}
	}

	public void Actualizar(Usuario usuario)
	{
		const string sql = @"
                UPDATE usuarios
                SET nombre_usuario = @usuario,
                    contrasena = @contrasena,
                    id_rol = @rol,
                    activo = @activo
                WHERE id_usuario = @id;";

		using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
		using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
		{
			cmd.Parameters.Add(Parametros.Texto("@usuario", usuario.NombreUsuario));
			cmd.Parameters.Add(Parametros.Texto("@contrasena", usuario.Contrasena));
			cmd.Parameters.Add(Parametros.Entero("@rol", usuario.Rol.Id));
			cmd.Parameters.Add(Parametros.Booleano("@activo", usuario.Activo));
			cmd.Parameters.Add(Parametros.Entero("@id", usuario.Id));
			conexion.Open();
			cmd.ExecuteNonQuery();
		}
	}

	public void Desactivar(int id)
	{
		const string sql = "UPDATE usuarios SET activo = 0 WHERE id_usuario = @id;";

		using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
		using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
		{
			cmd.Parameters.Add(Parametros.Entero("@id", id));
			conexion.Open();
			cmd.ExecuteNonQuery();
		}
	}

	public void Reactivar(int id)
	{
		const string sql = "UPDATE usuarios SET activo = 1 WHERE id_usuario = @id;";

		using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
		using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
		{
			cmd.Parameters.Add(Parametros.Entero("@id", id));
			conexion.Open();
			cmd.ExecuteNonQuery();
		}
	}
}
