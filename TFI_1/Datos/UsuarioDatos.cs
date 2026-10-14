using MySql.Data.MySqlClient;
using TFI_1.Models;


namespace TFI_1.Datos
{
    public class UsuarioDatos
    {
        public Usuario ObtenerPorNombre(string nombreUsuario)
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
    }
}