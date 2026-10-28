namespace TFI_1.EFCore.Entidades;

public class Categoria
{
	public int Id { get; set; }
	public string Nombre { get; set; } = string.Empty;
	public bool Activo { get; set; } = true;
}

public class Item
{
	public int Id { get; set; }
	public string Nombre { get; set; } = string.Empty;
	public TipoItem Tipo { get; set; }
	public decimal Precio { get; set; }
	public int? DuracionMin { get; set; }
	public int? Stock { get; set; }
	public int? StockMinimo { get; set; }
	public decimal? Costo { get; set; }
	public int IdCategoria { get; set; }
	public Categoria Categoria { get; set; } = null!;
	public bool Activo { get; set; } = true;

	public string CategoriaNombre => Categoria?.Nombre ?? string.Empty;
	public bool EsProducto => Tipo == TipoItem.Producto;
	public bool StockBajo => EsProducto && Stock <= StockMinimo;
	public bool HayStockPara(int cantidad) => !EsProducto || Stock >= cantidad;
}

public class Banco
{
	public int Id { get; set; }
	public string Nombre { get; set; } = string.Empty;
	public bool Activo { get; set; } = true;

	public override string ToString() => Nombre;
}

public class TarjetaCredito
{
	public int Id { get; set; }
	public int IdBanco { get; set; }
	public Banco Banco { get; set; } = null!;
	public string TipoTarjeta { get; set; } = string.Empty;
	public decimal PorcentajeRecargo { get; set; }
	public int CuotasMax { get; set; } = 1;
	public bool Activo { get; set; } = true;
	public DateTime FechaAlta { get; set; } = DateTime.Now;

	public string DescripcionCorta => $"{Banco?.Nombre} - {TipoTarjeta}";
}

public class Proveedor
{
	public int Id { get; set; }
	public string Nombre { get; set; } = string.Empty;
	public string? Cuit { get; set; }
	public string? Telefono { get; set; }
	public string? Email { get; set; }
	public string? Direccion { get; set; }
	public bool Activo { get; set; } = true;

	public override string ToString() => Nombre;
}
