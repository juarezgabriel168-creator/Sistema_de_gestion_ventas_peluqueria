using System;

namespace TFI_1.Models;

public class IngresoMercaderiaDetalle
{
	public int Id { get; set; }
	public Item Item { get; set; } = null!;
	public int Cantidad { get; set; }
	public decimal CostoUnitario { get; set; }
	public decimal Subtotal => CostoUnitario * Cantidad;

	public IngresoMercaderiaDetalle() { }

	public IngresoMercaderiaDetalle(Item item, int cantidad, decimal costoUnitario)
	{
		Item = item ?? throw new ArgumentNullException(nameof(item));
		if (cantidad <= 0)
			throw new ArgumentException("La cantidad debe ser mayor a cero.");
		if (costoUnitario < 0)
			throw new ArgumentException("El costo unitario no puede ser negativo.");

		Cantidad = cantidad;
		CostoUnitario = costoUnitario;
	}
}
