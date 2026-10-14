using System;
using TFI_1.Models;

namespace TFI_1.Models
{
    public class Usuario
    {
        private string _nombreUsuario;

        public int Id { get; set; }

        public string NombreUsuario
        {
            get => _nombreUsuario;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre de usuario es obligatorio.");
                _nombreUsuario = value.Trim();
            }
        }

        public string Contrasena { get; set; }   // guarda el hash, no el texto plano
        public Rol Rol { get; set; }
        public bool Activo { get; set; } = true;

        public Usuario() { }

        public Usuario(string nombreUsuario, string contrasena, Rol rol)
        {
            NombreUsuario = nombreUsuario;
            Contrasena = contrasena;
            Rol = rol;
        }

        public bool EsAdministrador() => Rol != null && Rol.Nombre == "Administrador";
    }
}