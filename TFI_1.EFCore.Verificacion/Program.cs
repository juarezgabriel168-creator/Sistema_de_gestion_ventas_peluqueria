using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore;
using TFI_1.EFCore.Dtos;
using TFI_1.EFCore.Entidades;
using TFI_1.EFCore.Repositorios;
using TFI_1.EFCore.Utilidades;

Console.OutputEncoding = Encoding.UTF8;

var fallos = new List<string>();
DateTime desde = new(2000, 1, 1);
DateTime hasta = DateTime.Today;

Console.WriteLine("Verificación de la capa EF Core sobre MySQL");
Console.WriteLine(new string('=', 70));
Console.WriteLine($"Cadena: {OcultarPassword(ConfiguracionEF.CadenaConexion)}");
Console.WriteLine($"Versión de servidor declarada: {ConfiguracionEF.VersionServidor}");

using var db = new PeluqueriaDbContext();

Console.WriteLine();
Console.WriteLine("1) Conexión y proveedor");
Verificar("CanConnect", () => db.Database.CanConnect());
Verificar("Tablas visibles en el esquema actual", () =>
	db.Database.SqlQueryRaw<int>(
		"SELECT COUNT(*) AS `Value` FROM information_schema.tables WHERE table_schema = DATABASE()").Single());

Console.WriteLine();
Console.WriteLine("2) Conteos por DbSet");
var conteos = new (string Nombre, Func<int> Cantidad)[]
{
	("roles", () => db.Roles.Count()),
	("usuarios", () => db.Usuarios.Count()),
	("clientes", () => db.Clientes.Count()),
	("empleados", () => db.Empleados.Count()),
	("categorias", () => db.Categorias.Count()),
	("items", () => db.Items.Count()),
	("bancos", () => db.Bancos.Count()),
	("tarjetas_credito", () => db.TarjetasCredito.Count()),
	("ventas", () => db.Ventas.Count()),
	("detalle_venta", () => db.DetallesVenta.Count()),
	("cuentas_corrientes", () => db.CuentasCorrientes.Count()),
	("movimientos_cuenta_corriente", () => db.MovimientosCuentaCorriente.Count()),
	("gastos", () => db.Gastos.Count()),
	("proveedores", () => db.Proveedores.Count()),
	("ingresos_mercaderia", () => db.IngresosMercaderia.Count()),
	("ingresos_mercaderia_detalle", () => db.IngresosMercaderiaDetalle.Count()),
	("atenciones", () => db.Atenciones.Count()),
	("auditoria", () => db.Auditorias.Count()),
	("cajas", () => db.Cajas.Count()),
	("caja_movimientos", () => db.CajaMovimientos.Count()),
};

foreach ((string nombre, Func<int> cantidad) in conteos)
	Verificar(nombre, () => cantidad());

int? ventaId = db.Ventas.AsNoTracking().OrderBy(v => v.Id).Select(v => (int?)v.Id).FirstOrDefault();
var cuenta = db.CuentasCorrientes.AsNoTracking()
	.Select(c => new { c.Id, c.IdCliente })
	.FirstOrDefault();
int? cajaId = db.Cajas.AsNoTracking().OrderBy(c => c.Id).Select(c => (int?)c.Id).FirstOrDefault();
int? empleadoId = db.Empleados.AsNoTracking().OrderBy(e => e.Id).Select(e => (int?)e.Id).FirstOrDefault();
int? itemId = db.Items.AsNoTracking().OrderBy(i => i.Id).Select(i => (int?)i.Id).FirstOrDefault();
int? categoriaId = db.Categorias.AsNoTracking().OrderBy(c => c.Id).Select(c => (int?)c.Id).FirstOrDefault();
int? proveedorId = db.Proveedores.AsNoTracking().OrderBy(p => p.Id).Select(p => (int?)p.Id).FirstOrDefault();
int? clienteId = db.Clientes.AsNoTracking().OrderBy(c => c.Id).Select(c => (int?)c.Id).FirstOrDefault();
int? usuarioId = db.Usuarios.AsNoTracking().OrderBy(u => u.Id).Select(u => (int?)u.Id).FirstOrDefault();
var atencion = db.Atenciones.AsNoTracking()
	.Where(a => a.Estado != EstadoAtencion.Cancelada)
	.OrderBy(a => a.Id)
	.Select(a => new { a.Id, a.IdEmpleado, a.FechaHora })
	.FirstOrDefault();

Console.WriteLine();
Console.WriteLine("3) Repositorios de catálogo y personas");
Verificar("Usuario.Listar", () => new UsuarioRepositorio().Listar());
Verificar("Usuario.ObtenerRoles", () => new UsuarioRepositorio().ObtenerRoles());
if (usuarioId.HasValue)
{
	int id = usuarioId.Value;
	Verificar("Usuario.TieneDependencias", () => new UsuarioRepositorio().TieneDependencias(id));
	Verificar("Usuario.EsUltimoAdministrador", () => new UsuarioRepositorio().EsUltimoAdministrador(id));
}
Verificar("Cliente.Listar", () => new ClienteRepositorio().Listar());
Verificar("Cliente.Listar(filtro)", () => new ClienteRepositorio().Listar("a"));
if (clienteId.HasValue)
{
	int id = clienteId.Value;
	Verificar("Cliente.EsConsumidorFinal", () => new ClienteRepositorio().EsConsumidorFinal(id));
	Verificar("Cliente.TieneDependencias", () => new ClienteRepositorio().TieneDependencias(id));
}
Verificar("Categoria.Listar", () => new CategoriaRepositorio().Listar());
if (categoriaId.HasValue)
	Verificar("Categoria.TieneDependencias", () => new CategoriaRepositorio().TieneDependencias(categoriaId.Value));
Verificar("Item.Listar", () => new ItemRepositorio().Listar());
Verificar("Item.Listar(servicios)", () => new ItemRepositorio().Listar(tipo: TipoItem.Servicio));
Verificar("Item.Listar(stock bajo)", () => new ItemRepositorio().Listar(soloStockBajo: true));
if (itemId.HasValue)
	Verificar("Item.TieneDependencias", () => new ItemRepositorio().TieneDependencias(itemId.Value));
Verificar("Empleado.Listar", () => new EmpleadoRepositorio().Listar());
if (empleadoId.HasValue)
	Verificar("Empleado.TieneDependencias", () => new EmpleadoRepositorio().TieneDependencias(empleadoId.Value));
Verificar("Proveedor.Listar", () => new ProveedorRepositorio().Listar());
if (proveedorId.HasValue)
	Verificar("Proveedor.TieneDependencias", () => new ProveedorRepositorio().TieneDependencias(proveedorId.Value));
Verificar("Banco.Listar(incluye inactivos)", () => new BancoRepositorio().Listar(false));
Verificar("TarjetaCredito.Listar(incluye inactivas)", () => new TarjetaCreditoRepositorio().Listar(false));

Console.WriteLine();
Console.WriteLine("4) Ventas");
Verificar("Venta.Contar", () => new VentaRepositorio().Contar(desde, hasta));
Verificar("Venta.Listar(limite 5)", () => new VentaRepositorio().Listar(desde, hasta, limite: 5));
Verificar("Venta.Listar (coincide con Contar)", () =>
{
	VentaRepositorio repo = new();
	int cantidad = repo.Contar(desde, hasta);
	int filas = repo.Listar(desde, hasta).Count;
	if (cantidad != filas)
		throw new InvalidOperationException($"Contar={cantidad}, Listar={filas}");
	return $"{filas} filas";
});

if (ventaId.HasValue)
{
	int id = ventaId.Value;
	Verificar("Venta.ObtenerCabecera", () => new VentaRepositorio().ObtenerCabecera(id));
	Verificar("Venta.ObtenerDetalle", () => new VentaRepositorio().ObtenerDetalle(id));
}
else
{
	Console.WriteLine("  [OMITIDO] No hay ventas para probar cabecera/detalle.");
}

Console.WriteLine();
Console.WriteLine("5) Cuenta corriente");
if (cuenta != null)
{
	int idCliente = cuenta.IdCliente;
	int idCuenta = cuenta.Id;
	Verificar("CuentaCorriente.ObtenerPorCliente", () => new CuentaCorrienteRepositorio().ObtenerPorCliente(idCliente));
	Verificar("CuentaCorriente.ObtenerSaldo", () => new CuentaCorrienteRepositorio().ObtenerSaldo(idCliente));
	Verificar("CuentaCorriente.ListarMovimientos", () => new CuentaCorrienteRepositorio().ListarMovimientos(idCuenta));
	Verificar("CuentaCorriente.MovimientosResumen", () =>
		new CuentaCorrienteRepositorio().MovimientosResumen(idCliente, desde, hasta));
}
else
{
	Console.WriteLine("  [OMITIDO] No hay cuentas corrientes.");
}

Console.WriteLine();
Console.WriteLine("6) Caja");
Verificar("Caja.ObtenerAbierta", () => new CajaRepositorio().ObtenerAbierta());
Verificar("Caja.Listar", () => new CajaRepositorio().Listar(desde, hasta));
Verificar("Caja.Contar", () => new CajaRepositorio().Contar(desde, hasta));
Verificar("Caja.ResumenVentas", () => new CajaRepositorio().ResumenVentas(desde, null));
Verificar("Caja.VentasEfectivo", () => new CajaRepositorio().VentasEfectivo(desde, null));
Verificar("Caja.ListarAnulaciones", () => new CajaRepositorio().ListarAnulaciones(desde, null));
if (cajaId.HasValue)
{
	int idCaja = cajaId.Value;
	Verificar("Caja.ListarMovimientos", () => new CajaRepositorio().ListarMovimientos(idCaja));
	Verificar("Caja.TotalMovimientos(ingreso)", () => new CajaRepositorio().TotalMovimientos(idCaja, "ingreso"));
}
else
{
	Console.WriteLine("  [OMITIDO] No hay cajas.");
}

Console.WriteLine();
Console.WriteLine("7) Gastos e ingresos de mercadería");
Verificar("Gasto.Listar", () => new GastoRepositorio().Listar());
Verificar("Gasto.TotalGastos", () => new GastoRepositorio().TotalGastos(desde, hasta));
Verificar("Gasto.GastosPorCategoria", () => new GastoRepositorio().GastosPorCategoria(desde, hasta));
Verificar("IngresoMercaderia.Listar", () => new IngresoMercaderiaRepositorio().Listar());
Verificar("IngresoMercaderia.Contar", () => new IngresoMercaderiaRepositorio().Contar(desde, hasta));
Verificar("IngresoMercaderia.Sumar", () => new IngresoMercaderiaRepositorio().Sumar(desde, hasta));
Verificar("IngresoMercaderia.IngresosResumen", () =>
	new IngresoMercaderiaRepositorio().IngresosResumen(desde, hasta));

int? ingresoId = db.IngresosMercaderia.AsNoTracking()
	.OrderBy(i => i.Id)
	.Select(i => (int?)i.Id)
	.FirstOrDefault();
if (ingresoId.HasValue)
	Verificar("IngresoMercaderia.ObtenerPorId", () =>
		new IngresoMercaderiaRepositorio().ObtenerPorId(ingresoId.Value));

Console.WriteLine();
Console.WriteLine("8) Atenciones y auditoría");
Verificar("Atencion.Listar", () => new AtencionRepositorio().Listar());
Verificar("Atencion.Listar(estado)", () => new AtencionRepositorio().Listar(estado: "pendiente"));
Verificar("Atencion.CantidadHoy", () => new AtencionRepositorio().CantidadHoy());

if (atencion != null)
{
	Verificar("Atencion.ExisteConflictoHorario (GREATEST)", () =>
	{
		bool conflicto = new AtencionRepositorio()
			.ExisteConflictoHorario(atencion.IdEmpleado, atencion.FechaHora, 30);
		if (!conflicto)
			throw new InvalidOperationException("se esperaba conflicto con la misma atención");
		return "true";
	});
}
else if (empleadoId.HasValue)
{
	Verificar("Atencion.ExisteConflictoHorario (GREATEST)", () =>
		new AtencionRepositorio().ExisteConflictoHorario(empleadoId.Value, DateTime.Today.AddHours(9), 30));
}
else
{
	Console.WriteLine("  [OMITIDO] No hay atenciones ni empleados.");
}

Verificar("Auditoria.Listar(limite 5)", () => new AuditoriaRepositorio().Listar(desde, hasta, null, null, 5));
Verificar("Auditoria.Contar", () => new AuditoriaRepositorio().Contar(desde, hasta, null, null));
Verificar("Auditoria.Acciones", () => new AuditoriaRepositorio().Acciones());

Console.WriteLine();
Console.WriteLine("9) Dashboard");
Verificar("Dashboard.TotalVendidoHoy", () => new DashboardRepositorio().TotalVendidoHoy());
Verificar("Dashboard.CantidadVentasHoy", () => new DashboardRepositorio().CantidadVentasHoy());
Verificar("Dashboard.TotalClientes", () => new DashboardRepositorio().TotalClientes());
Verificar("Dashboard.TotalItems", () => new DashboardRepositorio().TotalItems());
Verificar("Dashboard.ProductosStockBajo", () => new DashboardRepositorio().ProductosStockBajo());
Verificar("Dashboard.ProductoMayorStock", () => new DashboardRepositorio().ProductoMayorStock());
Verificar("Dashboard.ProductoMenorStock", () => new DashboardRepositorio().ProductoMenorStock());
Verificar("Dashboard.UltimasVentas(5)", () => new DashboardRepositorio().UltimasVentas(5));

Console.WriteLine();
Console.WriteLine("10) Reportes");
Verificar("Reporte.Resumen", () => new ReporteRepositorio().Resumen(desde, hasta));
Verificar("Reporte.Resumen (total coincide con la suma LINQ)", () =>
{
	decimal esperado = db.Ventas.AsNoTracking()
		.Where(v => v.Fecha >= desde && v.Fecha < hasta.AddDays(1) && !v.Anulada)
		.Sum(v => (decimal?)v.Total) ?? 0m;

	ResumenDto fila = new ReporteRepositorio().Resumen(desde, hasta).Single();
	if (fila.Total != esperado)
		throw new InvalidOperationException($"total {fila.Total} != {esperado}");
	return $"total {fila.Total:0.00}, ticket {fila.TicketPromedio:0.00}";
});
Verificar("Reporte.VentasPorMedioPago", () => new ReporteRepositorio().VentasPorMedioPago(desde, hasta));
Verificar("Reporte.ProductosMasVendidos", () => new ReporteRepositorio().ProductosMasVendidos(desde, hasta));
Verificar("Reporte.VentasPorDia", () => new ReporteRepositorio().VentasPorDia(desde, hasta));
Verificar("Reporte.ClientesConMasCompras", () => new ReporteRepositorio().ClientesConMasCompras(desde, hasta));
Verificar("Reporte.VentasPorUsuario", () => new ReporteRepositorio().VentasPorUsuario(desde, hasta));
Verificar("Reporte.ServiciosMasSolicitados", () => new ReporteRepositorio().ServiciosMasSolicitados(desde, hasta));
Verificar("Reporte.VentasPorEmpleado", () => new ReporteRepositorio().VentasPorEmpleado(desde, hasta));
Verificar("Reporte.Anulaciones", () => new ReporteRepositorio().Anulaciones(desde, hasta));

Console.WriteLine();
Console.WriteLine("11) Compatibilidad con DataTable");
Verificar("TablaExtensiones.ToDataTable<VentaListadoDto>", () =>
{
	DataTable tabla = new VentaRepositorio().Listar(desde, hasta, limite: 5).ToDataTable();
	if (tabla.Columns.Count == 0)
		throw new InvalidOperationException("la tabla quedó sin columnas");
	return $"{tabla.Rows.Count} filas, {tabla.Columns.Count} columnas";
});

Console.WriteLine();
Console.WriteLine(new string('=', 70));
if (fallos.Count == 0)
{
	Console.WriteLine("RESULTADO: todas las comprobaciones pasaron.");
	return 0;
}

Console.WriteLine($"RESULTADO: {fallos.Count} comprobación(es) fallaron:");
foreach (string fallo in fallos)
	Console.WriteLine($" - {fallo}");
return 1;

void Verificar(string nombre, Func<object?> prueba)
{
	try
	{
		object? valor = prueba();
		Console.WriteLine($"  [OK]    {nombre} -> {Describir(valor)}");
	}
	catch (Exception ex)
	{
		fallos.Add($"{nombre}: {ex.GetType().Name}: {ex.Message}");
		Console.WriteLine($"  [FALLO] {nombre} -> {ex.GetType().Name}: {ex.Message}");
	}
}

static string Describir(object? valor) => valor switch
{
	null => "null",
	bool booleano => booleano ? "true" : "false",
	string texto => texto,
	int entero => entero.ToString(),
	long largo => largo.ToString(),
	decimal numero => numero.ToString("0.00"),
	DateTime fecha => fecha.ToString("yyyy-MM-dd HH:mm"),
	DataTable tabla => $"{tabla.Rows.Count} filas, {tabla.Columns.Count} columnas",
	ICollection coleccion => $"{coleccion.Count} fila(s)",
	IEnumerable secuencia => $"{secuencia.Cast<object?>().Count()} fila(s)",
	_ => valor.ToString() ?? string.Empty,
};

static string OcultarPassword(string cadena) =>
	Regex.Replace(cadena, "(?i)(Pwd|Password)=[^;]*", "$1=***");
