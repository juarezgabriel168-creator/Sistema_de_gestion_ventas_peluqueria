using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TFI_1.Models;
using TFI_1.Datos;

namespace TFI_1.Negocio
{
    public class UsuarioNegocio
    {
        private readonly UsuarioDatos _datos = new UsuarioDatos();

        private readonly AuditoriaNegocio _auditoria = new();

        /// <summary>Largo mínimo exigido a las contraseñas nuevas.</summary>
        public const int LargoMinimoContrasena = 8;

        // Contraseñas habituales que no se aceptan aunque cumplan el largo.
        private static readonly string[] ContrasenasDebiles =
        {
            "12345678", "123456789", "1234567890", "password", "contraseña",
            "contrasena", "admin123", "administrador", "qwerty123", "abc12345",
            "peluqueria", "peluquería", "vendedor123", "supervisor123"
        };

        /// <summary>
        /// Devuelve la lista de requisitos que no cumple la contraseña.
        /// Lista vacía = contraseña aceptada. Se usa tanto en la pantalla
        /// como en la capa de negocio para que la regla sea una sola.
        /// </summary>
        public static List<string> ValidarContrasena(string? nombreUsuario, string? contrasena)
        {
            List<string> problemas = new List<string>();

            if (string.IsNullOrWhiteSpace(contrasena))
            {
                problemas.Add("La contraseña es obligatoria.");
                return problemas;
            }

            if (contrasena.Length < LargoMinimoContrasena)
            {
                problemas.Add($"Debe tener al menos {LargoMinimoContrasena} caracteres.");
            }
            if (!contrasena.Any(char.IsUpper))
            {
                problemas.Add("Debe incluir al menos una letra mayúscula.");
            }
            if (!contrasena.Any(char.IsLower))
            {
                problemas.Add("Debe incluir al menos una letra minúscula.");
            }
            if (!contrasena.Any(char.IsDigit))
            {
                problemas.Add("Debe incluir al menos un número.");
            }
            if (!string.IsNullOrWhiteSpace(nombreUsuario)
                && nombreUsuario.Trim().Length >= 3
                && contrasena.Contains(nombreUsuario.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                problemas.Add("No puede contener el nombre de usuario.");
            }
            if (ContrasenasDebiles.Contains(contrasena.Trim().ToLowerInvariant()))
            {
                problemas.Add("Es una contraseña demasiado común: elija otra.");
            }

            return problemas;
        }

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

            // La política se aplica solo cuando se define o cambia la contraseña:
            // las claves viejas siguen funcionando hasta que se cambien.
            if (!string.IsNullOrWhiteSpace(contrasenaPlano))
            {
                List<string> problemas = ValidarContrasena(usuario.NombreUsuario, contrasenaPlano);
                if (problemas.Count > 0)
                {
                    throw new ArgumentException(
                        "La contraseña no cumple la política de seguridad:" + Environment.NewLine +
                        "- " + string.Join(Environment.NewLine + "- ", problemas));
                }
            }

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

            string accion = esNuevo ? "usuario_alta" : "usuario_edicion";
            _auditoria.Registrar(accion, "usuarios", usuario.Id,
                $"Usuario '{usuario.NombreUsuario}' (rol {usuario.Rol.Nombre}).");

            return usuario;
        }

        public void Desactivar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Debe seleccionar un usuario.");

            if (Sesion.UsuarioActual != null && Sesion.UsuarioActual.Id == id)
                throw new InvalidOperationException("No puede desactivar su propio usuario.");

            _datos.Desactivar(id);
            _auditoria.Registrar("usuario_desactivacion", "usuarios", id,
                $"Usuario con id {id} desactivado.");
        }

        public void Reactivar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Debe seleccionar un usuario.");

            _datos.Reactivar(id);
            _auditoria.Registrar("usuario_reactivacion", "usuarios", id,
                $"Usuario con id {id} reactivado.");
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
