using System;

namespace TFI_1.Models
{
    public enum Modalidad { Turnos, OrdenLlegada }

    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public Modalidad Modalidad { get; set; }
        public bool Activo { get; set; } = true;

        public bool AtiendeConTurno() => Modalidad == Modalidad.Turnos;

        public string ModalidadTexto => Modalidad == Modalidad.Turnos ? "Turnos" : "Orden de llegada";

        public string NombreCompleto => $"{Apellido}, {Nombre}";

        public Empleado() { }
        public Empleado(string nombre, string apellido, Modalidad modalidad)
        {
            Nombre = nombre;
            Apellido = apellido;
            Modalidad = modalidad;
        }
    }
}
