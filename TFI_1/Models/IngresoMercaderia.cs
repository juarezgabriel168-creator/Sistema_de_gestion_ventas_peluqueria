using System;
using System.Collections.Generic;
using System.Text;

namespace TFI_1.Models;

public class IngresoMercaderia
{
	public int Id { get; set; }
	public int? IdProveedor { get; set; }
	public DateTime Fecha { get; set; } = DateTime.Now;
	public string? Proveedor { get; set; }
	public decimal Total { get; set; }
	public int IdUsuario { get; set; }
	public string? NombreUsuario { get; set; }
	public string? Observacion { get; set; }
	public bool Anulada { get; set; }
	public DateTime? FechaAnulacion { get; set; }
	public int? IdUsuarioAnulacion { get; set; }
	public string? NombreUsuarioAnulacion { get; set; }
	public string? MotivoAnulacion { get; set; }
	public string Estado => Anulada ? "Anulada" : "Vigente";
	public List<IngresoMercaderiaDetalle> Detalles { get; } = new();

	public void AgregarDetalle(Item item, int cantidad, decimal costoUnitario)
	{
		if (item == null)
			throw new ArgumentException("El producto no existe.");
		if (cantidad <= 0)
			throw new ArgumentException("La cantidad debe ser mayor a cero.");
		if (costoUnitario < 0)
			throw new ArgumentException("El costo unitario no puede ser negativo.");

		Detalles.Add(new IngresoMercaderiaDetalle(item, cantidad, costoUnitario));
		Total += Detalles[^1].Subtotal;
	}

}
