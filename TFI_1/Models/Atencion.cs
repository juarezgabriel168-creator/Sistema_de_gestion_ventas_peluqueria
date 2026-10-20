using System;
using System.Collections.Generic;
using System.Text;

namespace TFI_1.Models
{
    public enum TipoAtencion { Turno, Llegada }
    public enum EstadoAtencion { Pendiente, EnAtencion, Finalizada, Cancelada }

    public class Atencion
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public Empleado Empleado { get; set; }
        public Item Servicio { get; set; }
        public TipoAtencion Tipo { get; set; }
        public DateTime FechaHora { get; set; }
        public string Fecha => FechaHora.ToString("dd/MM/yyyy");
        public string Hora => FechaHora.ToString("HH:mm");
        public string ClienteNombre => Cliente?.NombreCompleto ?? string.Empty;
        public string EmpleadoNombre => Empleado?.NombreCompleto ?? string.Empty;
        public string ServicioNombre => Servicio?.Nombre ?? string.Empty;
        public EstadoAtencion Estado { get; set; } = EstadoAtencion.Pendiente;

        public string TipoNombre => Tipo == TipoAtencion.Turno ? "Turno" : "Orden de llegada";
        public string EstadoNombre => Estado switch
        {
            EstadoAtencion.Pendiente => "Pendiente",
            EstadoAtencion.EnAtencion => "En atención",
            EstadoAtencion.Finalizada => "Finalizada",
            _ => "Cancelada",
        };

        public void Cancelar()
        {
            if (Estado == EstadoAtencion.Finalizada)
                throw new InvalidOperationException("No se puede cancelar una atención finalizada.");
            Estado = EstadoAtencion.Cancelada;
        }
    }
}
