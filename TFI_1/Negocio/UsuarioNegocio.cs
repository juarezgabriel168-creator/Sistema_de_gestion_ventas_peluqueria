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

            if (string.IsNullOrWhiteSpace(usuario.Contrasena))
                throw new UnauthorizedAccessException(
                    "La contraseña almacenada de este usuario no es válida. Contacte al administrador.");

            if (!VerificarContrasena(contrasena, usuario.Contrasena))
                throw new UnauthorizedAccessException("Contraseña incorrecta.");

            if (!usuario.Activo)
                throw new UnauthorizedAccessException("El usuario está inactivo.");

            return usuario;
        }

        public List<Usuario> Listar(string? filtro = null)
        {
            return _datos.Listar(filtro);
        }

        public List<Rol> ObtenerRoles()
        {
            return _datos.ObtenerRoles();
        }

        public Usuario Guardar(Usuario usuario, string contrasenaPlano, bool esNuevo)
        {
            if (usuario == null)
                throw new ArgumentException("El usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario))
                throw new ArgumentException("El nombre de usuario es obligatorio.");

            if (usuario.Rol == null || usuario.Rol.Id <= 0)
                throw new ArgumentException("Debe seleccionar un rol.");

            int? exceptoId = esNuevo ? null : usuario.Id;
            if (_datos.ExisteNombre(usuario.NombreUsuario, exceptoId))
                throw new ArgumentException($"Ya existe un usuario con el nombre '{usuario.NombreUsuario}'.");

            if (esNuevo)
            {
                if (string.IsNullOrWhiteSpace(contrasenaPlano))
                    throw new ArgumentException("La contraseña es obligatoria para un usuario nuevo.");

                usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(contrasenaPlano);
                usuario.Id = _datos.Insertar(usuario);
            }
            else
            {
                // Si no se ingresó una contraseña nueva, se conserva el hash actual.
                if (!string.IsNullOrWhiteSpace(contrasenaPlano))
                    usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(contrasenaPlano);

                _datos.Actualizar(usuario);
            }

            return usuario;
        }

        public void Desactivar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Debe seleccionar un usuario.");

            if (Sesion.UsuarioActual != null && Sesion.UsuarioActual.Id == id)
                throw new InvalidOperationException("No puede desactivar su propio usuario.");

            _datos.Desactivar(id);
        }

        public void Reactivar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Debe seleccionar un usuario.");

            _datos.Reactivar(id);
        }

        private static bool VerificarContrasena(string contrasena, string hashAlmacenado)
        {
            if (string.IsNullOrWhiteSpace(hashAlmacenado))
                throw new UnauthorizedAccessException(
                    "La contraseña almacenada de este usuario no es válida. Contacte al administrador.");
            try
            {
                return BCrypt.Net.BCrypt.Verify(contrasena, hashAlmacenado);
            }
            catch (BCrypt.Net.SaltParseException)
            {
                throw new UnauthorizedAccessException(
                    "La contraseña almacenada de este usuario es inválida. Contacte al administrador.");
            }
            catch (ArgumentException)
            {
                throw new UnauthorizedAccessException(
                    "La contraseña almacenada de este usuario es inválida. Contacte al administrador.");
            }
        }
    }
}
