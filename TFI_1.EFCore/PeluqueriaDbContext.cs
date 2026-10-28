using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore;

/// <summary>
/// Mapea el esquema MySQL existente (BaseDatos/peluqueria.sql) sin migraciones.
/// </summary>
public class PeluqueriaDbContext : DbContext
{
	public PeluqueriaDbContext()
	{
	}

	public PeluqueriaDbContext(DbContextOptions<PeluqueriaDbContext> options) : base(options)
	{
	}

	public DbSet<Rol> Roles => Set<Rol>();
	public DbSet<Usuario> Usuarios => Set<Usuario>();
	public DbSet<Cliente> Clientes => Set<Cliente>();
	public DbSet<Empleado> Empleados => Set<Empleado>();
	public DbSet<Categoria> Categorias => Set<Categoria>();
	public DbSet<Item> Items => Set<Item>();
	public DbSet<Banco> Bancos => Set<Banco>();
	public DbSet<TarjetaCredito> TarjetasCredito => Set<TarjetaCredito>();
	public DbSet<Venta> Ventas => Set<Venta>();
	public DbSet<DetalleVenta> DetallesVenta => Set<DetalleVenta>();
	public DbSet<CuentaCorriente> CuentasCorrientes => Set<CuentaCorriente>();
	public DbSet<MovimientoCuentaCorriente> MovimientosCuentaCorriente => Set<MovimientoCuentaCorriente>();
	public DbSet<Gasto> Gastos => Set<Gasto>();
	public DbSet<Proveedor> Proveedores => Set<Proveedor>();
	public DbSet<IngresoMercaderia> IngresosMercaderia => Set<IngresoMercaderia>();
	public DbSet<IngresoMercaderiaDetalle> IngresosMercaderiaDetalle => Set<IngresoMercaderiaDetalle>();
	public DbSet<Atencion> Atenciones => Set<Atencion>();
	public DbSet<Auditoria> Auditorias => Set<Auditoria>();
	public DbSet<Caja> Cajas => Set<Caja>();
	public DbSet<CajaMovimiento> CajaMovimientos => Set<CajaMovimiento>();

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
			optionsBuilder.UseMySql(ConfiguracionEF.CadenaConexion, ConfiguracionEF.VersionServidor);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		ConfigurarRolesUsuarios(modelBuilder);
		ConfigurarPersonas(modelBuilder);
		ConfigurarCatalogo(modelBuilder);
		ConfigurarVentas(modelBuilder);
		ConfigurarCuentaCorriente(modelBuilder);
		ConfigurarGastosIngresos(modelBuilder);
		ConfigurarAtencionesAuditoria(modelBuilder);
		ConfigurarCajas(modelBuilder);
	}

	private static void ConfigurarRolesUsuarios(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Rol>(e =>
		{
			e.ToTable("roles");
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_rol");
			e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(50).IsRequired();
			e.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("uq_roles_nombre");
		});

		modelBuilder.Entity<Usuario>(e =>
		{
			e.ToTable("usuarios");
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_usuario");
			e.Property(x => x.NombreUsuario).HasColumnName("nombre_usuario").HasMaxLength(50).IsRequired();
			e.Property(x => x.Contrasena).HasColumnName("contrasena").HasMaxLength(100).IsRequired();
			e.Property(x => x.IdRol).HasColumnName("id_rol");
			e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);
			e.HasIndex(x => x.NombreUsuario).IsUnique().HasDatabaseName("uq_usuarios_nombre");
			e.HasOne(x => x.Rol)
				.WithMany()
				.HasForeignKey(x => x.IdRol)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_usuarios_rol");
		});
	}

	private static void ConfigurarPersonas(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Cliente>(e =>
		{
			e.ToTable("clientes");
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_cliente");
			e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
			e.Property(x => x.Apellido).HasColumnName("apellido").HasMaxLength(100).IsRequired();
			e.Property(x => x.Dni).HasColumnName("dni").HasColumnType("char(8)").HasMaxLength(8).IsRequired();
			e.Property(x => x.Telefono).HasColumnName("telefono").HasMaxLength(30);
			e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);
			e.HasIndex(x => x.Dni).IsUnique().HasDatabaseName("uq_clientes_dni");
		});

		modelBuilder.Entity<Empleado>(e =>
		{
			e.ToTable("empleados");
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_empleado");
			e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
			e.Property(x => x.Apellido).HasColumnName("apellido").HasMaxLength(100).IsRequired();
			e.Property(x => x.Modalidad)
				.HasColumnName("modalidad")
				.HasColumnType("enum('turnos', 'orden_llegada')")
				.HasConversion(
					v => v == Modalidad.Turnos ? "turnos" : "orden_llegada",
					v => v == "turnos" ? Modalidad.Turnos : Modalidad.OrdenLlegada)
				.IsRequired();
			e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);
		});
	}

	private static void ConfigurarCatalogo(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Categoria>(e =>
		{
			e.ToTable("categorias");
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_categoria");
			e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
			e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);
		});

		modelBuilder.Entity<Item>(e =>
		{
			e.ToTable("items", t =>
			{
				t.HasCheckConstraint("chk_precio", "precio >= 0");
				t.HasCheckConstraint("chk_stock",
					"(tipo = 'servicio' AND stock IS NULL AND stock_minimo IS NULL) OR " +
					"(tipo = 'producto' AND stock IS NOT NULL AND stock >= 0 AND stock_minimo IS NOT NULL AND stock_minimo >= 0)");
				t.HasCheckConstraint("chk_duracion",
					"(tipo = 'servicio' AND duracion_min IS NOT NULL AND duracion_min > 0) OR " +
					"(tipo = 'producto' AND duracion_min IS NULL)");
				t.HasCheckConstraint("chk_costo", "costo IS NULL OR costo >= 0");
			});
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_item");
			e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();
			e.Property(x => x.Tipo)
				.HasColumnName("tipo")
				.HasColumnType("enum('producto', 'servicio')")
				.HasConversion(
					v => v == TipoItem.Producto ? "producto" : "servicio",
					v => v == "producto" ? TipoItem.Producto : TipoItem.Servicio)
				.IsRequired();
			e.Property(x => x.Precio).HasColumnName("precio").HasPrecision(12, 2).HasDefaultValue(0m);
			e.Property(x => x.DuracionMin).HasColumnName("duracion_min");
			e.Property(x => x.Stock).HasColumnName("stock");
			e.Property(x => x.StockMinimo).HasColumnName("stock_minimo");
			e.Property(x => x.Costo).HasColumnName("costo").HasPrecision(12, 2);
			e.Property(x => x.IdCategoria).HasColumnName("id_categoria");
			e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);
			e.HasIndex(x => x.IdCategoria).HasDatabaseName("ix_items_categoria");
			e.HasOne(x => x.Categoria)
				.WithMany()
				.HasForeignKey(x => x.IdCategoria)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_items_categoria");
		});

		modelBuilder.Entity<Banco>(e =>
		{
			e.ToTable("bancos");
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_banco");
			e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
			e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);
		});

		modelBuilder.Entity<TarjetaCredito>(e =>
		{
			e.ToTable("tarjetas_credito");
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_tarjeta_credito");
			e.Property(x => x.IdBanco).HasColumnName("id_banco");
			e.Property(x => x.TipoTarjeta).HasColumnName("tipo_tarjeta").HasMaxLength(50).IsRequired();
			e.Property(x => x.PorcentajeRecargo).HasColumnName("porcentaje_recargo").HasPrecision(5, 2).HasDefaultValue(0m);
			e.Property(x => x.CuotasMax).HasColumnName("cuotas_max").HasDefaultValue(1);
			e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);
			e.Property(x => x.FechaAlta).HasColumnName("fecha_alta").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
			e.HasIndex(x => new { x.IdBanco, x.TipoTarjeta }).IsUnique().HasDatabaseName("uq_tarjetas_banco_tipo");
			e.HasOne(x => x.Banco)
				.WithMany()
				.HasForeignKey(x => x.IdBanco)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_tarjetas_banco");
		});

		modelBuilder.Entity<Proveedor>(e =>
		{
			e.ToTable("proveedores");
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_proveedor");
			e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();
			e.Property(x => x.Cuit).HasColumnName("cuit").HasMaxLength(20);
			e.Property(x => x.Telefono).HasColumnName("telefono").HasMaxLength(40);
			e.Property(x => x.Email).HasColumnName("email").HasMaxLength(120);
			e.Property(x => x.Direccion).HasColumnName("direccion").HasMaxLength(200);
			e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);
			e.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("ux_proveedores_nombre");
		});
	}

	private static void ConfigurarVentas(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Venta>(e =>
		{
			e.ToTable("ventas");
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_venta");
			e.Property(x => x.IdCliente).HasColumnName("id_cliente");
			e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
			e.Property(x => x.IdEmpleado).HasColumnName("id_empleado");
			e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
			e.Property(x => x.MedioPago)
				.HasColumnName("medio_pago")
				.HasColumnType("enum('efectivo', 'tarjeta', 'transferencia', 'cuenta_corriente')")
				.HasDefaultValue("efectivo")
				.IsRequired();
			e.Property(x => x.IdTarjetaCredito).HasColumnName("id_tarjeta_credito");
			e.Property(x => x.CantidadCuotas).HasColumnName("cantidad_cuotas");
			e.Property(x => x.Descuento).HasColumnName("descuento").HasPrecision(12, 2).HasDefaultValue(0m);
			e.Property(x => x.Recargo).HasColumnName("recargo").HasPrecision(12, 2).HasDefaultValue(0m);
			e.Property(x => x.Total).HasColumnName("total").HasPrecision(12, 2).HasDefaultValue(0m);
			e.Property(x => x.Anulada).HasColumnName("anulada").HasColumnType("tinyint(1)").HasDefaultValue(false);
			e.Property(x => x.FechaAnulacion).HasColumnName("fecha_anulacion").HasColumnType("datetime");
			e.Property(x => x.IdUsuarioAnulacion).HasColumnName("id_usuario_anulacion");
			e.Property(x => x.MotivoAnulacion).HasColumnName("motivo_anulacion").HasMaxLength(255);

			e.HasIndex(x => x.IdCliente).HasDatabaseName("ix_ventas_cliente");
			e.HasIndex(x => x.Fecha).HasDatabaseName("ix_ventas_fecha");
			e.HasIndex(x => x.Anulada).HasDatabaseName("ix_ventas_anulada");

			e.HasOne(x => x.Cliente)
				.WithMany()
				.HasForeignKey(x => x.IdCliente)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_ventas_cliente");
			e.HasOne(x => x.Usuario)
				.WithMany()
				.HasForeignKey(x => x.IdUsuario)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_ventas_usuario");
			e.HasOne(x => x.Empleado)
				.WithMany()
				.HasForeignKey(x => x.IdEmpleado)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_ventas_empleado");
			e.HasOne(x => x.TarjetaCredito)
				.WithMany()
				.HasForeignKey(x => x.IdTarjetaCredito)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_ventas_tarjeta");
			e.HasOne(x => x.UsuarioAnulacion)
				.WithMany()
				.HasForeignKey(x => x.IdUsuarioAnulacion)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_ventas_usuario_anulacion");
		});

		modelBuilder.Entity<DetalleVenta>(e =>
		{
			e.ToTable("detalle_venta");
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_detalle");
			e.Property(x => x.IdVenta).HasColumnName("id_venta");
			e.Property(x => x.IdItem).HasColumnName("id_item");
			e.Property(x => x.Cantidad).HasColumnName("cantidad");
			e.Property(x => x.Precio).HasColumnName("precio").HasPrecision(12, 2);
			e.Property(x => x.Subtotal).HasColumnName("subtotal").HasPrecision(12, 2);
			e.HasIndex(x => x.IdVenta).HasDatabaseName("ix_detalle_venta");
			e.HasIndex(x => x.IdItem).HasDatabaseName("ix_detalle_item");
			e.HasOne(x => x.Venta)
				.WithMany(x => x.Detalles)
				.HasForeignKey(x => x.IdVenta)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("fk_detalle_venta");
			e.HasOne(x => x.Item)
				.WithMany()
				.HasForeignKey(x => x.IdItem)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_detalle_item");
		});
	}

	private static void ConfigurarCuentaCorriente(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<CuentaCorriente>(e =>
		{
			e.ToTable("cuentas_corrientes");
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_cuenta_corriente");
			e.Property(x => x.IdCliente).HasColumnName("id_cliente");
			e.Property(x => x.Saldo).HasColumnName("saldo").HasPrecision(12, 2).HasDefaultValue(0m);
			e.Property(x => x.FechaActualizacion).HasColumnName("fecha_actualizacion").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
			e.HasIndex(x => x.IdCliente).IsUnique().HasDatabaseName("uq_cuentas_cliente");
			e.HasOne(x => x.Cliente)
				.WithMany()
				.HasForeignKey(x => x.IdCliente)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_cuentas_cliente");
		});

		modelBuilder.Entity<MovimientoCuentaCorriente>(e =>
		{
			e.ToTable("movimientos_cuenta_corriente");
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_movimiento");
			e.Property(x => x.IdCuenta).HasColumnName("id_cuenta");
			e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
			e.Property(x => x.Tipo)
				.HasColumnName("tipo")
				.HasColumnType("enum('cargo', 'abono')")
				.HasConversion(
					v => v == TipoMovimientoCuentaCorriente.Cargo ? "cargo" : "abono",
					v => v == "cargo" ? TipoMovimientoCuentaCorriente.Cargo : TipoMovimientoCuentaCorriente.Abono)
				.IsRequired();
			e.Property(x => x.Monto).HasColumnName("monto").HasPrecision(12, 2);
			e.Property(x => x.Concepto).HasColumnName("concepto").HasMaxLength(255);
			e.Property(x => x.IdVenta).HasColumnName("id_venta");
			e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
			e.HasIndex(x => x.IdCuenta).HasDatabaseName("ix_mov_cuenta");
			e.HasOne(x => x.Cuenta)
				.WithMany(x => x.Movimientos)
				.HasForeignKey(x => x.IdCuenta)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("fk_mov_cuenta");
			e.HasOne(x => x.Venta)
				.WithMany()
				.HasForeignKey(x => x.IdVenta)
				.OnDelete(DeleteBehavior.SetNull)
				.HasConstraintName("fk_mov_venta");
			e.HasOne(x => x.Usuario)
				.WithMany()
				.HasForeignKey(x => x.IdUsuario)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_mov_usuario");
		});
	}

	private static void ConfigurarGastosIngresos(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Gasto>(e =>
		{
			e.ToTable("gastos");
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_gasto");
			e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
			e.Property(x => x.Concepto).HasColumnName("concepto").HasMaxLength(150).IsRequired();
			e.Property(x => x.Monto).HasColumnName("monto").HasPrecision(12, 2);
			e.Property(x => x.Categoria).HasColumnName("categoria").HasMaxLength(100);
			e.Property(x => x.Observacion).HasColumnName("observacion").HasMaxLength(255);
			e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
			e.Property(x => x.FechaAlta).HasColumnName("fecha_alta").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
			e.HasIndex(x => x.Fecha).HasDatabaseName("ix_gastos_fecha");
			e.HasOne(x => x.Usuario)
				.WithMany()
				.HasForeignKey(x => x.IdUsuario)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_gastos_usuario");
		});

		modelBuilder.Entity<IngresoMercaderia>(e =>
		{
			e.ToTable("ingresos_mercaderia");
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_ingreso");
			e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
			e.Property(x => x.IdProveedor).HasColumnName("id_proveedor");
			e.Property(x => x.Proveedor).HasColumnName("proveedor").HasMaxLength(150);
			e.Property(x => x.Total).HasColumnName("total").HasPrecision(12, 2).HasDefaultValue(0m);
			e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
			e.Property(x => x.Observacion).HasColumnName("observacion").HasMaxLength(255);
			e.HasIndex(x => x.Fecha).HasDatabaseName("ix_ingresos_fecha");
			e.HasIndex(x => x.IdProveedor).HasDatabaseName("ix_ingresos_proveedor");
			e.HasOne(x => x.ProveedorEntidad)
				.WithMany()
				.HasForeignKey(x => x.IdProveedor)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_ingresos_proveedor");
			e.HasOne(x => x.Usuario)
				.WithMany()
				.HasForeignKey(x => x.IdUsuario)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_ingresos_usuario");
		});

		modelBuilder.Entity<IngresoMercaderiaDetalle>(e =>
		{
			e.ToTable("ingresos_mercaderia_detalle");
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_detalle_ingreso");
			e.Property(x => x.IdIngreso).HasColumnName("id_ingreso");
			e.Property(x => x.IdItem).HasColumnName("id_item");
			e.Property(x => x.Cantidad).HasColumnName("cantidad");
			e.Property(x => x.CostoUnitario).HasColumnName("costo_unitario").HasPrecision(12, 2);
			e.Property(x => x.Subtotal).HasColumnName("subtotal").HasPrecision(12, 2);
			e.HasIndex(x => x.IdIngreso).HasDatabaseName("ix_ingdet_ingreso");
			e.HasIndex(x => x.IdItem).HasDatabaseName("ix_ingdet_item");
			e.HasOne(x => x.Ingreso)
				.WithMany(x => x.Detalles)
				.HasForeignKey(x => x.IdIngreso)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("fk_ingdet_ingreso");
			e.HasOne(x => x.Item)
				.WithMany()
				.HasForeignKey(x => x.IdItem)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_ingdet_item");
		});
	}

	private static void ConfigurarAtencionesAuditoria(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Atencion>(e =>
		{
			e.ToTable("atenciones");
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_atencion");
			e.Property(x => x.IdCliente).HasColumnName("id_cliente");
			e.Property(x => x.IdEmpleado).HasColumnName("id_empleado");
			e.Property(x => x.IdItem).HasColumnName("id_item");
			e.Property(x => x.Tipo)
				.HasColumnName("tipo")
				.HasColumnType("enum('turno', 'llegada')")
				.HasDefaultValue(TipoAtencion.Llegada)
				.HasConversion(
					v => v == TipoAtencion.Turno ? "turno" : "llegada",
					v => v == "turno" ? TipoAtencion.Turno : TipoAtencion.Llegada)
				.IsRequired();
			e.Property(x => x.FechaHora).HasColumnName("fecha_hora").HasColumnType("datetime");
			e.Property(x => x.Estado)
				.HasColumnName("estado")
				.HasColumnType("enum('pendiente', 'en_atencion', 'finalizada', 'cancelada')")
				.HasDefaultValue(EstadoAtencion.Pendiente)
				.HasConversion(
					v => v == EstadoAtencion.Pendiente ? "pendiente"
						: v == EstadoAtencion.EnAtencion ? "en_atencion"
						: v == EstadoAtencion.Finalizada ? "finalizada" : "cancelada",
					v => v == "en_atencion" ? EstadoAtencion.EnAtencion
						: v == "finalizada" ? EstadoAtencion.Finalizada
						: v == "cancelada" ? EstadoAtencion.Cancelada : EstadoAtencion.Pendiente)
				.IsRequired();
			e.HasIndex(x => new { x.IdEmpleado, x.FechaHora }).HasDatabaseName("ix_atenciones_empleado");
			e.HasIndex(x => x.IdCliente).HasDatabaseName("ix_atenciones_cliente");
			e.HasOne(x => x.Cliente)
				.WithMany()
				.HasForeignKey(x => x.IdCliente)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_atenciones_cliente");
			e.HasOne(x => x.Empleado)
				.WithMany()
				.HasForeignKey(x => x.IdEmpleado)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_atenciones_empleado");
			e.HasOne(x => x.Servicio)
				.WithMany()
				.HasForeignKey(x => x.IdItem)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_atenciones_item");
		});

		modelBuilder.Entity<Auditoria>(e =>
		{
			e.ToTable("auditoria");
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_auditoria");
			e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
			e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
			e.Property(x => x.Accion).HasColumnName("accion").HasMaxLength(40).IsRequired();
			e.Property(x => x.Entidad).HasColumnName("entidad").HasMaxLength(40).IsRequired();
			e.Property(x => x.IdEntidad).HasColumnName("id_entidad");
			e.Property(x => x.Detalle).HasColumnName("detalle").HasMaxLength(255);
			e.HasIndex(x => x.Fecha).HasDatabaseName("ix_auditoria_fecha");
			e.HasIndex(x => x.IdUsuario).HasDatabaseName("ix_auditoria_usuario");
			e.HasIndex(x => x.Accion).HasDatabaseName("ix_auditoria_accion");
			e.HasOne(x => x.Usuario)
				.WithMany()
				.HasForeignKey(x => x.IdUsuario)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_auditoria_usuario");
		});
	}

	private static void ConfigurarCajas(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Caja>(e =>
		{
			e.ToTable("cajas", t =>
			{
				t.HasCheckConstraint("chk_cajas_montos",
					"monto_inicial >= 0 AND (monto_esperado IS NULL OR monto_esperado >= 0) " +
					"AND (monto_contado IS NULL OR monto_contado >= 0)");
			});
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_caja");
			e.Property(x => x.IdUsuarioApertura).HasColumnName("id_usuario_apertura");
			e.Property(x => x.FechaApertura).HasColumnName("fecha_apertura").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
			e.Property(x => x.MontoInicial).HasColumnName("monto_inicial").HasPrecision(12, 2).HasDefaultValue(0m);
			e.Property(x => x.IdUsuarioCierre).HasColumnName("id_usuario_cierre");
			e.Property(x => x.FechaCierre).HasColumnName("fecha_cierre").HasColumnType("datetime");
			e.Property(x => x.MontoEsperado).HasColumnName("monto_esperado").HasPrecision(12, 2);
			e.Property(x => x.MontoContado).HasColumnName("monto_contado").HasPrecision(12, 2);
			e.Property(x => x.Diferencia).HasColumnName("diferencia").HasPrecision(12, 2);
			e.Property(x => x.Observacion).HasColumnName("observacion").HasMaxLength(255);
			e.Property(x => x.Estado)
				.HasColumnName("estado")
				.HasColumnType("enum('abierta', 'cerrada')")
				.HasDefaultValue("abierta")
				.IsRequired();
			e.HasIndex(x => x.Estado).HasDatabaseName("ix_cajas_estado");
			e.HasIndex(x => x.FechaApertura).HasDatabaseName("ix_cajas_apertura");
			e.HasOne(x => x.UsuarioApertura)
				.WithMany()
				.HasForeignKey(x => x.IdUsuarioApertura)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_cajas_usuario_apertura");
			e.HasOne(x => x.UsuarioCierre)
				.WithMany()
				.HasForeignKey(x => x.IdUsuarioCierre)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_cajas_usuario_cierre");
		});

		modelBuilder.Entity<CajaMovimiento>(e =>
		{
			e.ToTable("caja_movimientos", t =>
			{
				t.HasCheckConstraint("chk_caja_movimientos_monto", "monto > 0");
			});
			e.HasKey(x => x.Id);
			e.Property(x => x.Id).HasColumnName("id_movimiento_caja");
			e.Property(x => x.IdCaja).HasColumnName("id_caja");
			e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
			e.Property(x => x.Tipo)
				.HasColumnName("tipo")
				.HasColumnType("enum('ingreso', 'egreso')")
				.IsRequired();
			e.Property(x => x.Monto).HasColumnName("monto").HasPrecision(12, 2);
			e.Property(x => x.Concepto).HasColumnName("concepto").HasMaxLength(150).IsRequired();
			e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
			e.HasIndex(x => new { x.IdCaja, x.Fecha }).HasDatabaseName("ix_caja_movimientos_caja");
			e.HasOne(x => x.Caja)
				.WithMany(x => x.Movimientos)
				.HasForeignKey(x => x.IdCaja)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_caja_movimientos_caja");
			e.HasOne(x => x.Usuario)
				.WithMany()
				.HasForeignKey(x => x.IdUsuario)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("fk_caja_movimientos_usuario");
		});
	}
}
