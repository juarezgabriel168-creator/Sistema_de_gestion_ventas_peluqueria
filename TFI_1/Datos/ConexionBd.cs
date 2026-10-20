using System.Configuration;
using MySql.Data.MySqlClient;

namespace TFI_1.Datos
{
    public static class ConexionBD
    {
        private static readonly string Cadena = ObtenerCadena();

        private static string ObtenerCadena()
        {
            ConnectionStringSettings configuracion =
                ConfigurationManager.ConnectionStrings["Peluqueria"];

            if (configuracion == null || string.IsNullOrWhiteSpace(configuracion.ConnectionString))
                throw new ConfigurationErrorsException(
                    "No se encontró la cadena de conexión 'Peluqueria' en App.config.");

            return configuracion.ConnectionString;
        }

        public static MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(Cadena);
        }
    }
}
