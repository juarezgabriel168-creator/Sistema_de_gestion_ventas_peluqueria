using System;
using System.Collections.Generic;
using System.Text;

namespace TFI_1.Models
{
    public class Cliente
    {
        private string _dni;

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; } = true;

        public string Dni
        {
            get => _dni;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length != 8 || !value.All(char.IsDigit))
                    throw new ArgumentException("El DNI debe tener 8 dígitos numéricos.");
                _dni = value;
            }
        }

        public string NombreCompleto => $"{Apellido}, {Nombre}";

        public const string DniConsumidorFinal = "00000000";

        public bool EsConsumidorFinal => _dni == DniConsumidorFinal;

        public Cliente() { }
        public Cliente(string nombre, string apellido, string dni, string telefono = null)
        {
            Nombre = nombre;
            Apellido = apellido;
            Dni = dni;
            Telefono = telefono;
        }
    }
}
