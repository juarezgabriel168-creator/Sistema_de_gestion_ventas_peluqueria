using System;
using System.Collections.Generic;
using System.Text;
using TFI_1.Models;

namespace TFI_1.Negocio
{
    public static class Sesion
    {
        public const int RolAdministrador = 1;
        public const int RolVendedor = 2;
        public const int RolSupervisor = 3;

        public static Usuario? UsuarioActual { get; set; }

        public static bool HaySesion => UsuarioActual != null;

        public static int RolId => UsuarioActual?.Rol?.Id ?? 0;

        public static string NombreUsuario => UsuarioActual?.NombreUsuario ?? string.Empty;

        public static string RolNombre => UsuarioActual?.Rol?.Nombre ?? string.Empty;

        public static bool EsAdministrador => RolId == RolAdministrador;

        public static bool EsSupervisor => RolId == RolSupervisor;

        public static bool PuedeVender => RolId == RolAdministrador || RolId == RolVendedor;

        public static bool PuedeVerReportes => RolId == RolAdministrador || RolId == RolSupervisor;

        public static void CerrarSesion()
        {
            UsuarioActual = null;
        }
    }
}
