using System;
using System.Collections.Generic;
using System.Text;
using TFI_1.Models;
using TFI_1.Datos;

namespace TFI_1.Negocio
{
    public class UsuarioNegocio
    {
        private readonly UsuarioDatos _datos = new UsuarioDatos();

        public Usuario Autenticar(string nombreUsuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(contrasena))
                throw new ArgumentException("Ingrese usuario y contraseña.");

            var usuario = _datos.ObtenerPorNombre(nombreUsuario.Trim());

            if (usuario == null)
                throw new UnauthorizedAccessException("El usuario no existe.");

            if (!BCrypt.Net.BCrypt.Verify(contrasena, usuario.Contrasena))
                throw new UnauthorizedAccessException("Contraseña incorrecta.");

            if (!usuario.Activo)
                throw new UnauthorizedAccessException("El usuario está inactivo.");

            return usuario;
        }
    }
}
