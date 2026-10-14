using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace TFI_1.Datos
{
    public static class ConexionBD
    {
        private const string Cadena =
            "Server=localhost;Port=3306;Database=peluqueria;Uid=root;Pwd=Gabriel1234/;CharSet=utf8mb4;";

        public static MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(Cadena);
        }
    }
}
