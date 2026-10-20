using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TFI_1.Models;


namespace TFI_1.Datos
{
    public class UsuarioDatos
    {
        public Usuario? ObtenerPorNombre(string nombreUsuario)
        {
            const string sql = @"
                SELECT u.id_usuario, u.nombre_usuario, u.contrasena, u.activo,
                       r.id_rol, r.nombre AS rol
                FROM usuarios AS u
                INNER JOIN roles AS r ON r.id_rol = u.id_rol
                WHERE u.nombre_usuario = @usuario;";

            using (var cn = ConexionBD.ObtenerConexion())
            using (var cmd = new MySqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@usuario", nombreUsuario);
                cn.Open();

                using (var rd = cmd.ExecuteReader())
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
            const string sql = @"
                SELECT u.id_usuario, u.nombre_usuario, u.contrasena, u.activo,
                       r.id_rol, r.nombre AS rol
                FROM usuarios AS u
                INNER JOIN roles AS r ON r.id_rol = u.id_rol
                WHERE (@filtro = '' OR u.nombre_usuario LIKE CONCAT('%', @filtro, '%'))
                ORDER BY u.nombre_usuario;";

            var lista = new List<Usuario>();

            using (var cn = ConexionBD.ObtenerConexion())
            using (var cmd = new MySqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@filtro", filtro ?? string.Empty);
                cn.Open();

                using (var rd = cmd.ExecuteReader())
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

            var lista = new List<Rol>();

            using (var cn = ConexionBD.ObtenerConexion())
            using (var cmd = new MySqlCommand(sql, cn))
            {
                cn.Open();

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                        lista.Add(new Rol(rd.GetInt32("id_rol"), rd.GetString("nombre")));
                }
            }

            return lista;
        }

        public bool ExisteNombre(string nombreUsuario, int? exceptoId = null)
        {
            const string sql = @"
                SELECT COUNT(*)
                FROM usuarios
                WHERE nombre_usuario = @usuario
                  AND (@exceptoId IS NULL OR id_usuario <> @exceptoId);";

            using (var cn = ConexionBD.ObtenerConexion())
            using (var cmd = new MySqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@usuario", nombreUsuario);
                cmd.Parameters.AddWithValue("@exceptoId", (object?)exceptoId ?? DBNull.Value);
                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public int Insertar(Usuario usuario)
        {
            const string sql = @"
                INSERT INTO usuarios (nombre_usuario, contrasena, id_rol, activo)
                VALUES (@usuario, @contrasena, @rol, @activo);
                SELECT LAST_INSERT_ID();";

            using (var cn = ConexionBD.ObtenerConexion())
            using (var cmd = new MySqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@usuario", usuario.NombreUsuario);
                cmd.Parameters.AddWithValue("@contrasena", usuario.Contrasena);
                cmd.Parameters.AddWithValue("@rol", usuario.Rol.Id);
                cmd.Parameters.AddWithValue("@activo", usuario.Activo);
                cn.Open();
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

            using (var cn = ConexionBD.ObtenerConexion())
            using (var cmd = new MySqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@usuario", usuario.NombreUsuario);
                cmd.Parameters.AddWithValue("@contrasena", usuario.Contrasena);
                cmd.Parameters.AddWithValue("@rol", usuario.Rol.Id);
                cmd.Parameters.AddWithValue("@activo", usuario.Activo);
                cmd.Parameters.AddWithValue("@id", usuario.Id);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Desactivar(int id)
        {
            const string sql = "UPDATE usuarios SET activo = 0 WHERE id_usuario = @id;";

            using (var cn = ConexionBD.ObtenerConexion())
            using (var cmd = new MySqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Reactivar(int id)
        {
            const string sql = "UPDATE usuarios SET activo = 1 WHERE id_usuario = @id;";

            using (var cn = ConexionBD.ObtenerConexion())
            using (var cmd = new MySqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
