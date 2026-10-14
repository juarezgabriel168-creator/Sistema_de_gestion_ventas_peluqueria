using System;
using System.Collections.Generic;
using System.Text;

namespace TFI_1.Models
{
    public class Categoria
    {
        private string _nombre;

        public int Id { get; set; }
        public bool Activo { get; set; } = true;

        public string Nombre
        {
            get => _nombre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre de la categoría es obligatorio.");
                _nombre = value.Trim();
            }
        }

        public Categoria() { }
        public Categoria(string nombre) { Nombre = nombre; }
    }
}
