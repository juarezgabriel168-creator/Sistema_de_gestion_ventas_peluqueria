using System;
using System.Collections.Generic;
using System.Text;

namespace TFI_1.Models
{
    public enum TipoItem { Servicio, Producto }

    public class Item
    {
        private decimal _precio;

        public int Id { get; set; }
        public string Nombre { get; set; }
        public TipoItem Tipo { get; set; }
        public int? DuracionMin { get; set; }   // solo servicios
        public int? Stock { get; set; }         // solo productos
        public int? StockMinimo { get; set; }   // solo productos
        public Categoria Categoria { get; set; } = null!;
        public string CategoriaNombre => Categoria?.Nombre ?? string.Empty;
        public bool Activo { get; set; } = true;

        public decimal Precio
        {
            get => _precio;
            set
            {
                if (value < 0)
                    throw new ArgumentException("El precio no puede ser negativo.");
                _precio = value;
            }
        }

        public decimal? Costo { get; set; }   // costo de compra (solo productos)

        public bool EsProducto => Tipo == TipoItem.Producto;

        public bool StockBajo => EsProducto && Stock <= StockMinimo;

        public bool HayStockPara(int cantidad) => !EsProducto || Stock >= cantidad;

        public void DescontarStock(int cantidad)
        {
            if (!EsProducto) return;
            if (!HayStockPara(cantidad))
                throw new InvalidOperationException($"Stock insuficiente de {Nombre}.");
            Stock -= cantidad;
        }

        public Item() { }
    }
}
