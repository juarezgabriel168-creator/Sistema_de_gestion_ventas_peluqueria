using System;
using System.Collections.Generic;
using System.Text;

namespace TFI_1.Models
{
    public class DetalleVenta
    {
        public int Id { get; set; }
        public Item Item { get; private set; }
        public int Cantidad { get; private set; }
        public decimal Precio { get; private set; }      // precio al momento de la venta

        public decimal Subtotal => Precio * Cantidad;

        public DetalleVenta() { }

        public DetalleVenta(Item item, int cantidad)
        {
            if (item == null)
                throw new ArgumentException("El producto o servicio no existe.");
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.");
            if (!item.HayStockPara(cantidad))
                throw new InvalidOperationException($"Stock insuficiente de {item.Nombre}.");

            Item = item;
            Cantidad = cantidad;
            Precio = item.Precio;
        }
    }
}
