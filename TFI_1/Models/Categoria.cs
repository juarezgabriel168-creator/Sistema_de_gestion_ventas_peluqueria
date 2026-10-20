using System;

namespace TFI_1.Models
{
    public class Categoria
    {
        private string _nombre = string.Empty;

        public int Id { get; set; }
        public bool Activo { get; set; } = true;

        public string Nombre
        {
            get => _nombre;
            set
            {
                Validaciones.ExigirTexto(value, "El nombre de la categoría es obligatorio.");
                _nombre = value.Trim();
            }
        }

        public Categoria() { }
        public Categoria(string nombre) { Nombre = nombre; }
    }
}
