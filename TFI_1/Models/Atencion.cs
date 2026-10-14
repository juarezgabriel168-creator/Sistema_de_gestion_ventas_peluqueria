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
        public EstadoAtencion Estado { get; set; } = EstadoAtencion.Pendiente;

        public void Cancelar()
        {
            if (Estado == EstadoAtencion.Finalizada)
                throw new InvalidOperationException("No se puede cancelar una atención finalizada.");
            Estado = EstadoAtencion.Cancelada;
        }
    }
}
