# Código completo de la migración a EF Core

Este documento contiene, en un solo archivo, todo el código fuente de la
migración de la capa de acceso a datos del proyecto `TFI_1` a Entity Framework
Core.

- **Proyecto de la capa migrada:** `TFI_1.EFCore`
- **Proyecto de verificación:** `TFI_1.EFCore.Verificacion`
- **Framework:** `net10.0-windows`
- **Proveedor:** `Pomelo.EntityFrameworkCore.MySql` 9.0.0

> El proyecto original `TFI_1` no está incluido porque no fue modificado.
>
> **Nota de seguridad:** `appsettings.example.json` contiene solo una plantilla;
> la cadena real se configura en `appsettings.json`, que está ignorado por Git.

---

## Índice

Total: 36 archivos.

- [`TFI_1.EFCore/TFI_1.EFCore.csproj`](#tfi_1efcoretfi_1efcorecsproj)
- [`TFI_1.EFCore/appsettings.example.json`](#tfi_1efcoreappsettingsexamplejson)
- [`TFI_1.EFCore/ConfiguracionEF.cs`](#tfi_1efcoreconfiguracionefcs)
- [`TFI_1.EFCore/PeluqueriaDbContext.cs`](#tfi_1efcorepeluqueriadbcontextcs)
- [`TFI_1.EFCore/Entidades/AtencionesAuditoria.cs`](#tfi_1efcoreentidadesatencionesauditoriacs)
- [`TFI_1.EFCore/Entidades/Cajas.cs`](#tfi_1efcoreentidadescajascs)
- [`TFI_1.EFCore/Entidades/Catalogo.cs`](#tfi_1efcoreentidadescatalogocs)
- [`TFI_1.EFCore/Entidades/CuentaCorriente.cs`](#tfi_1efcoreentidadescuentacorrientecs)
- [`TFI_1.EFCore/Entidades/Enumeraciones.cs`](#tfi_1efcoreentidadesenumeracionescs)
- [`TFI_1.EFCore/Entidades/GastosIngresos.cs`](#tfi_1efcoreentidadesgastosingresoscs)
- [`TFI_1.EFCore/Entidades/Personas.cs`](#tfi_1efcoreentidadespersonascs)
- [`TFI_1.EFCore/Entidades/Ventas.cs`](#tfi_1efcoreentidadesventascs)
- [`TFI_1.EFCore/Dtos/ReportesDtos.cs`](#tfi_1efcoredtosreportesdtoscs)
- [`TFI_1.EFCore/Dtos/VariosDtos.cs`](#tfi_1efcoredtosvariosdtoscs)
- [`TFI_1.EFCore/Dtos/VentasDtos.cs`](#tfi_1efcoredtosventasdtoscs)
- [`TFI_1.EFCore/Repositorios/AtencionRepositorio.cs`](#tfi_1efcorerepositoriosatencionrepositoriocs)
- [`TFI_1.EFCore/Repositorios/AuditoriaRepositorio.cs`](#tfi_1efcorerepositoriosauditoriarepositoriocs)
- [`TFI_1.EFCore/Repositorios/BancoRepositorio.cs`](#tfi_1efcorerepositoriosbancorepositoriocs)
- [`TFI_1.EFCore/Repositorios/CajaRepositorio.cs`](#tfi_1efcorerepositorioscajarepositoriocs)
- [`TFI_1.EFCore/Repositorios/CategoriaRepositorio.cs`](#tfi_1efcorerepositorioscategoriarepositoriocs)
- [`TFI_1.EFCore/Repositorios/ClienteRepositorio.cs`](#tfi_1efcorerepositoriosclienterepositoriocs)
- [`TFI_1.EFCore/Repositorios/Comun.cs`](#tfi_1efcorerepositorioscomuncs)
- [`TFI_1.EFCore/Repositorios/CuentaCorrienteRepositorio.cs`](#tfi_1efcorerepositorioscuentacorrienterepositoriocs)
- [`TFI_1.EFCore/Repositorios/DashboardRepositorio.cs`](#tfi_1efcorerepositoriosdashboardrepositoriocs)
- [`TFI_1.EFCore/Repositorios/EmpleadoRepositorio.cs`](#tfi_1efcorerepositoriosempleadorepositoriocs)
- [`TFI_1.EFCore/Repositorios/GastoRepositorio.cs`](#tfi_1efcorerepositoriosgastorepositoriocs)
- [`TFI_1.EFCore/Repositorios/IngresoMercaderiaRepositorio.cs`](#tfi_1efcorerepositoriosingresomercaderiarepositoriocs)
- [`TFI_1.EFCore/Repositorios/ItemRepositorio.cs`](#tfi_1efcorerepositoriositemrepositoriocs)
- [`TFI_1.EFCore/Repositorios/ProveedorRepositorio.cs`](#tfi_1efcorerepositoriosproveedorrepositoriocs)
- [`TFI_1.EFCore/Repositorios/ReporteRepositorio.cs`](#tfi_1efcorerepositoriosreporterepositoriocs)
- [`TFI_1.EFCore/Repositorios/TarjetaCreditoRepositorio.cs`](#tfi_1efcorerepositoriostarjetacreditorepositoriocs)
- [`TFI_1.EFCore/Repositorios/UsuarioRepositorio.cs`](#tfi_1efcorerepositoriosusuariorepositoriocs)
- [`TFI_1.EFCore/Repositorios/VentaRepositorio.cs`](#tfi_1efcorerepositoriosventarepositoriocs)
- [`TFI_1.EFCore/Utilidades/TablaExtensiones.cs`](#tfi_1efcoreutilidadestablaextensionescs)
- [`TFI_1.EFCore.Verificacion/Program.cs`](#tfi_1efcoreverificacionprogramcs)
- [`TFI_1.EFCore.Verificacion/TFI_1.EFCore.Verificacion.csproj`](#tfi_1efcoreverificaciontfi_1efcoreverificacioncsproj)

---

## `TFI_1.EFCore/TFI_1.EFCore.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net10.0-windows</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <RootNamespace>TFI_1.EFCore</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.21" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Relational" Version="9.0.21" />
    <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="9.0.0" />
  </ItemGroup>

  <ItemGroup>
    <None Update="appsettings.json">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
  </ItemGroup>

</Project>
```

## `TFI_1.EFCore/appsettings.example.json`

```json
{
  "ConnectionStrings": {
    "Peluqueria": "Server=localhost;Port=3306;Database=peluqueria;Uid=peluqueria_app;Pwd=CAMBIAR_ESTA_CONTRASENA;CharSet=utf8mb4;"
  }
}
```

## `TFI_1.EFCore/ConfiguracionEF.cs`

```csharp
using System;
using System.IO;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace TFI_1.EFCore;

/// <summary>
/// Resuelve la cadena de conexión: variable de entorno, appsettings.json o
/// una cadena de ejemplo sin credenciales reales.
/// </summary>
public static class ConfiguracionEF
{
    public const string VariableConexion = "TFI_PELUQUERIA_CONNECTION";
    public const string VariableVersion = "TFI_MYSQL_VERSION";

    private const string CadenaOriginal =
        "Server=localhost;Port=3306;Database=peluqueria;Uid=peluqueria_app;" +        "Server=localhost;Port=3306;Database=peluqueria;Uid=peluqueria_app;" +
        "Pwd=CAMBIAR_ESTA_CONTRASENA;CharSet=utf8mb4;";        "Pwd=CAMBIAR_ESTA_CONTRASENA;CharSet=utf8mb4;";

    private static readonly Lazy<string> _cadena = new(LeerCadena);
    private static readonly Lazy<MySqlServerVersion> _version = new(LeerVersion);

    public static string CadenaConexion => _cadena.Value;

    public static MySqlServerVersion VersionServidor => _version.Value;

    public static DbContextOptions<PeluqueriaDbContext> CrearOpciones() =>
        new DbContextOptionsBuilder<PeluqueriaDbContext>()        new DbContextOptionsBuilder<PeluqueriaDbContext>()
            .UseMySql(CadenaConexion, VersionServidor)            .UseMySql(CadenaConexion, VersionServidor)            .UseMySql(CadenaConexion, VersionServidor)
            .Options;            .Options;            .Options;

    private static string LeerCadena()
    {
        string? desdeEntorno = Environment.GetEnvironmentVariable(VariableConexion);        string? desdeEntorno = Environment.GetEnvironmentVariable(VariableConexion);
        if (!string.IsNullOrWhiteSpace(desdeEntorno))        if (!string.IsNullOrWhiteSpace(desdeEntorno))
            return desdeEntorno.Trim();            return desdeEntorno.Trim();            return desdeEntorno.Trim();

        foreach (string archivo in ArchivosCandidatos())        foreach (string archivo in ArchivosCandidatos())
        {        {
            if (!File.Exists(archivo))            if (!File.Exists(archivo))            if (!File.Exists(archivo))
                continue;                continue;                continue;                continue;

            try            try            try
            {            {            {
                using JsonDocument documento = JsonDocument.Parse(File.ReadAllText(archivo));                using JsonDocument documento = JsonDocument.Parse(File.ReadAllText(archivo));                using JsonDocument documento = JsonDocument.Parse(File.ReadAllText(archivo));                using JsonDocument documento = JsonDocument.Parse(File.ReadAllText(archivo));
                if (documento.RootElement.TryGetProperty("ConnectionStrings", out JsonElement cadenas)                if (documento.RootElement.TryGetProperty("ConnectionStrings", out JsonElement cadenas)                if (documento.RootElement.TryGetProperty("ConnectionStrings", out JsonElement cadenas)                if (documento.RootElement.TryGetProperty("ConnectionStrings", out JsonElement cadenas)
                    && cadenas.TryGetProperty("Peluqueria", out JsonElement valor)                    && cadenas.TryGetProperty("Peluqueria", out JsonElement valor)                    && cadenas.TryGetProperty("Peluqueria", out JsonElement valor)                    && cadenas.TryGetProperty("Peluqueria", out JsonElement valor)                    && cadenas.TryGetProperty("Peluqueria", out JsonElement valor)
                    && !string.IsNullOrWhiteSpace(valor.GetString()))                    && !string.IsNullOrWhiteSpace(valor.GetString()))                    && !string.IsNullOrWhiteSpace(valor.GetString()))                    && !string.IsNullOrWhiteSpace(valor.GetString()))                    && !string.IsNullOrWhiteSpace(valor.GetString()))
                {                {                {                {
                    return valor.GetString()!.Trim();                    return valor.GetString()!.Trim();                    return valor.GetString()!.Trim();                    return valor.GetString()!.Trim();                    return valor.GetString()!.Trim();
                }                }                }                }
            }            }            }
            catch (JsonException)            catch (JsonException)            catch (JsonException)
            {            {            {
                // Si el archivo está mal formado se prueba el siguiente candidato.                // Si el archivo está mal formado se prueba el siguiente candidato.                // Si el archivo está mal formado se prueba el siguiente candidato.                // Si el archivo está mal formado se prueba el siguiente candidato.
            }            }            }
        }        }

        return CadenaOriginal;        return CadenaOriginal;
    }

    private static MySqlServerVersion LeerVersion()
    {
        string? texto = Environment.GetEnvironmentVariable(VariableVersion);        string? texto = Environment.GetEnvironmentVariable(VariableVersion);
        if (!string.IsNullOrWhiteSpace(texto) && Version.TryParse(texto, out Version? version))        if (!string.IsNullOrWhiteSpace(texto) && Version.TryParse(texto, out Version? version))
            return new MySqlServerVersion(version);            return new MySqlServerVersion(version);            return new MySqlServerVersion(version);

        return new MySqlServerVersion(new Version(8, 0, 46));        return new MySqlServerVersion(new Version(8, 0, 46));
    }

    private static string[] ArchivosCandidatos() =>
        new[]        new[]
        {        {
            Path.Combine(AppContext.BaseDirectory, "appsettings.json"),            Path.Combine(AppContext.BaseDirectory, "appsettings.json"),            Path.Combine(AppContext.BaseDirectory, "appsettings.json"),
            Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"),            Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"),            Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"),
        };        };
}
```

## `TFI_1.EFCore/PeluqueriaDbContext.cs`

```csharp
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
        if (!optionsBuilder.IsConfigured)        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseMySql(ConfiguracionEF.CadenaConexion, ConfiguracionEF.VersionServidor);            optionsBuilder.UseMySql(ConfiguracionEF.CadenaConexion, ConfiguracionEF.VersionServidor);            optionsBuilder.UseMySql(ConfiguracionEF.CadenaConexion, ConfiguracionEF.VersionServidor);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigurarRolesUsuarios(modelBuilder);        ConfigurarRolesUsuarios(modelBuilder);
        ConfigurarPersonas(modelBuilder);        ConfigurarPersonas(modelBuilder);
        ConfigurarCatalogo(modelBuilder);        ConfigurarCatalogo(modelBuilder);
        ConfigurarVentas(modelBuilder);        ConfigurarVentas(modelBuilder);
        ConfigurarCuentaCorriente(modelBuilder);        ConfigurarCuentaCorriente(modelBuilder);
        ConfigurarGastosIngresos(modelBuilder);        ConfigurarGastosIngresos(modelBuilder);
        ConfigurarAtencionesAuditoria(modelBuilder);        ConfigurarAtencionesAuditoria(modelBuilder);
        ConfigurarCajas(modelBuilder);        ConfigurarCajas(modelBuilder);
    }

    private static void ConfigurarRolesUsuarios(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rol>(e =>        modelBuilder.Entity<Rol>(e =>
        {        {
            e.ToTable("roles");            e.ToTable("roles");            e.ToTable("roles");
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_rol");            e.Property(x => x.Id).HasColumnName("id_rol");            e.Property(x => x.Id).HasColumnName("id_rol");
            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(50).IsRequired();            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(50).IsRequired();            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(50).IsRequired();
            e.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("uq_roles_nombre");            e.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("uq_roles_nombre");            e.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("uq_roles_nombre");
        });        });

        modelBuilder.Entity<Usuario>(e =>        modelBuilder.Entity<Usuario>(e =>
        {        {
            e.ToTable("usuarios");            e.ToTable("usuarios");            e.ToTable("usuarios");
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_usuario");            e.Property(x => x.Id).HasColumnName("id_usuario");            e.Property(x => x.Id).HasColumnName("id_usuario");
            e.Property(x => x.NombreUsuario).HasColumnName("nombre_usuario").HasMaxLength(50).IsRequired();            e.Property(x => x.NombreUsuario).HasColumnName("nombre_usuario").HasMaxLength(50).IsRequired();            e.Property(x => x.NombreUsuario).HasColumnName("nombre_usuario").HasMaxLength(50).IsRequired();
            e.Property(x => x.Contrasena).HasColumnName("contrasena").HasMaxLength(100).IsRequired();            e.Property(x => x.Contrasena).HasColumnName("contrasena").HasMaxLength(100).IsRequired();            e.Property(x => x.Contrasena).HasColumnName("contrasena").HasMaxLength(100).IsRequired();
            e.Property(x => x.IdRol).HasColumnName("id_rol");            e.Property(x => x.IdRol).HasColumnName("id_rol");            e.Property(x => x.IdRol).HasColumnName("id_rol");
            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);
            e.HasIndex(x => x.NombreUsuario).IsUnique().HasDatabaseName("uq_usuarios_nombre");            e.HasIndex(x => x.NombreUsuario).IsUnique().HasDatabaseName("uq_usuarios_nombre");            e.HasIndex(x => x.NombreUsuario).IsUnique().HasDatabaseName("uq_usuarios_nombre");
            e.HasOne(x => x.Rol)            e.HasOne(x => x.Rol)            e.HasOne(x => x.Rol)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdRol)                .HasForeignKey(x => x.IdRol)                .HasForeignKey(x => x.IdRol)                .HasForeignKey(x => x.IdRol)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_usuarios_rol");                .HasConstraintName("fk_usuarios_rol");                .HasConstraintName("fk_usuarios_rol");                .HasConstraintName("fk_usuarios_rol");
        });        });
    }

    private static void ConfigurarPersonas(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(e =>        modelBuilder.Entity<Cliente>(e =>
        {        {
            e.ToTable("clientes");            e.ToTable("clientes");            e.ToTable("clientes");
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_cliente");            e.Property(x => x.Id).HasColumnName("id_cliente");            e.Property(x => x.Id).HasColumnName("id_cliente");
            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
            e.Property(x => x.Apellido).HasColumnName("apellido").HasMaxLength(100).IsRequired();            e.Property(x => x.Apellido).HasColumnName("apellido").HasMaxLength(100).IsRequired();            e.Property(x => x.Apellido).HasColumnName("apellido").HasMaxLength(100).IsRequired();
            e.Property(x => x.Dni).HasColumnName("dni").HasColumnType("char(8)").HasMaxLength(8).IsRequired();            e.Property(x => x.Dni).HasColumnName("dni").HasColumnType("char(8)").HasMaxLength(8).IsRequired();            e.Property(x => x.Dni).HasColumnName("dni").HasColumnType("char(8)").HasMaxLength(8).IsRequired();
            e.Property(x => x.Telefono).HasColumnName("telefono").HasMaxLength(30);            e.Property(x => x.Telefono).HasColumnName("telefono").HasMaxLength(30);            e.Property(x => x.Telefono).HasColumnName("telefono").HasMaxLength(30);
            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);
            e.HasIndex(x => x.Dni).IsUnique().HasDatabaseName("uq_clientes_dni");            e.HasIndex(x => x.Dni).IsUnique().HasDatabaseName("uq_clientes_dni");            e.HasIndex(x => x.Dni).IsUnique().HasDatabaseName("uq_clientes_dni");
        });        });

        modelBuilder.Entity<Empleado>(e =>        modelBuilder.Entity<Empleado>(e =>
        {        {
            e.ToTable("empleados");            e.ToTable("empleados");            e.ToTable("empleados");
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_empleado");            e.Property(x => x.Id).HasColumnName("id_empleado");            e.Property(x => x.Id).HasColumnName("id_empleado");
            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
            e.Property(x => x.Apellido).HasColumnName("apellido").HasMaxLength(100).IsRequired();            e.Property(x => x.Apellido).HasColumnName("apellido").HasMaxLength(100).IsRequired();            e.Property(x => x.Apellido).HasColumnName("apellido").HasMaxLength(100).IsRequired();
            e.Property(x => x.Modalidad)            e.Property(x => x.Modalidad)            e.Property(x => x.Modalidad)
                .HasColumnName("modalidad")                .HasColumnName("modalidad")                .HasColumnName("modalidad")                .HasColumnName("modalidad")
                .HasColumnType("enum('turnos', 'orden_llegada')")                .HasColumnType("enum('turnos', 'orden_llegada')")                .HasColumnType("enum('turnos', 'orden_llegada')")                .HasColumnType("enum('turnos', 'orden_llegada')")
                .HasConversion(                .HasConversion(                .HasConversion(                .HasConversion(
                    v => v == Modalidad.Turnos ? "turnos" : "orden_llegada",                    v => v == Modalidad.Turnos ? "turnos" : "orden_llegada",                    v => v == Modalidad.Turnos ? "turnos" : "orden_llegada",                    v => v == Modalidad.Turnos ? "turnos" : "orden_llegada",                    v => v == Modalidad.Turnos ? "turnos" : "orden_llegada",
                    v => v == "turnos" ? Modalidad.Turnos : Modalidad.OrdenLlegada)                    v => v == "turnos" ? Modalidad.Turnos : Modalidad.OrdenLlegada)                    v => v == "turnos" ? Modalidad.Turnos : Modalidad.OrdenLlegada)                    v => v == "turnos" ? Modalidad.Turnos : Modalidad.OrdenLlegada)                    v => v == "turnos" ? Modalidad.Turnos : Modalidad.OrdenLlegada)
                .IsRequired();                .IsRequired();                .IsRequired();                .IsRequired();
            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);
        });        });
    }

    private static void ConfigurarCatalogo(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(e =>        modelBuilder.Entity<Categoria>(e =>
        {        {
            e.ToTable("categorias");            e.ToTable("categorias");            e.ToTable("categorias");
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_categoria");            e.Property(x => x.Id).HasColumnName("id_categoria");            e.Property(x => x.Id).HasColumnName("id_categoria");
            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);
        });        });

        modelBuilder.Entity<Item>(e =>        modelBuilder.Entity<Item>(e =>
        {        {
            e.ToTable("items", t =>            e.ToTable("items", t =>            e.ToTable("items", t =>
            {            {            {
                t.HasCheckConstraint("chk_precio", "precio >= 0");                t.HasCheckConstraint("chk_precio", "precio >= 0");                t.HasCheckConstraint("chk_precio", "precio >= 0");                t.HasCheckConstraint("chk_precio", "precio >= 0");
                t.HasCheckConstraint("chk_stock",                t.HasCheckConstraint("chk_stock",                t.HasCheckConstraint("chk_stock",                t.HasCheckConstraint("chk_stock",
                    "(tipo = 'servicio' AND stock IS NULL AND stock_minimo IS NULL) OR " +                    "(tipo = 'servicio' AND stock IS NULL AND stock_minimo IS NULL) OR " +                    "(tipo = 'servicio' AND stock IS NULL AND stock_minimo IS NULL) OR " +                    "(tipo = 'servicio' AND stock IS NULL AND stock_minimo IS NULL) OR " +                    "(tipo = 'servicio' AND stock IS NULL AND stock_minimo IS NULL) OR " +
                    "(tipo = 'producto' AND stock IS NOT NULL AND stock >= 0 AND stock_minimo IS NOT NULL AND stock_minimo >= 0)");                    "(tipo = 'producto' AND stock IS NOT NULL AND stock >= 0 AND stock_minimo IS NOT NULL AND stock_minimo >= 0)");                    "(tipo = 'producto' AND stock IS NOT NULL AND stock >= 0 AND stock_minimo IS NOT NULL AND stock_minimo >= 0)");                    "(tipo = 'producto' AND stock IS NOT NULL AND stock >= 0 AND stock_minimo IS NOT NULL AND stock_minimo >= 0)");                    "(tipo = 'producto' AND stock IS NOT NULL AND stock >= 0 AND stock_minimo IS NOT NULL AND stock_minimo >= 0)");
                t.HasCheckConstraint("chk_duracion",                t.HasCheckConstraint("chk_duracion",                t.HasCheckConstraint("chk_duracion",                t.HasCheckConstraint("chk_duracion",
                    "(tipo = 'servicio' AND duracion_min IS NOT NULL AND duracion_min > 0) OR " +                    "(tipo = 'servicio' AND duracion_min IS NOT NULL AND duracion_min > 0) OR " +                    "(tipo = 'servicio' AND duracion_min IS NOT NULL AND duracion_min > 0) OR " +                    "(tipo = 'servicio' AND duracion_min IS NOT NULL AND duracion_min > 0) OR " +                    "(tipo = 'servicio' AND duracion_min IS NOT NULL AND duracion_min > 0) OR " +
                    "(tipo = 'producto' AND duracion_min IS NULL)");                    "(tipo = 'producto' AND duracion_min IS NULL)");                    "(tipo = 'producto' AND duracion_min IS NULL)");                    "(tipo = 'producto' AND duracion_min IS NULL)");                    "(tipo = 'producto' AND duracion_min IS NULL)");
                t.HasCheckConstraint("chk_costo", "costo IS NULL OR costo >= 0");                t.HasCheckConstraint("chk_costo", "costo IS NULL OR costo >= 0");                t.HasCheckConstraint("chk_costo", "costo IS NULL OR costo >= 0");                t.HasCheckConstraint("chk_costo", "costo IS NULL OR costo >= 0");
            });            });            });
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_item");            e.Property(x => x.Id).HasColumnName("id_item");            e.Property(x => x.Id).HasColumnName("id_item");
            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();
            e.Property(x => x.Tipo)            e.Property(x => x.Tipo)            e.Property(x => x.Tipo)
                .HasColumnName("tipo")                .HasColumnName("tipo")                .HasColumnName("tipo")                .HasColumnName("tipo")
                .HasColumnType("enum('producto', 'servicio')")                .HasColumnType("enum('producto', 'servicio')")                .HasColumnType("enum('producto', 'servicio')")                .HasColumnType("enum('producto', 'servicio')")
                .HasConversion(                .HasConversion(                .HasConversion(                .HasConversion(
                    v => v == TipoItem.Producto ? "producto" : "servicio",                    v => v == TipoItem.Producto ? "producto" : "servicio",                    v => v == TipoItem.Producto ? "producto" : "servicio",                    v => v == TipoItem.Producto ? "producto" : "servicio",                    v => v == TipoItem.Producto ? "producto" : "servicio",
                    v => v == "producto" ? TipoItem.Producto : TipoItem.Servicio)                    v => v == "producto" ? TipoItem.Producto : TipoItem.Servicio)                    v => v == "producto" ? TipoItem.Producto : TipoItem.Servicio)                    v => v == "producto" ? TipoItem.Producto : TipoItem.Servicio)                    v => v == "producto" ? TipoItem.Producto : TipoItem.Servicio)
                .IsRequired();                .IsRequired();                .IsRequired();                .IsRequired();
            e.Property(x => x.Precio).HasColumnName("precio").HasPrecision(12, 2).HasDefaultValue(0m);            e.Property(x => x.Precio).HasColumnName("precio").HasPrecision(12, 2).HasDefaultValue(0m);            e.Property(x => x.Precio).HasColumnName("precio").HasPrecision(12, 2).HasDefaultValue(0m);
            e.Property(x => x.DuracionMin).HasColumnName("duracion_min");            e.Property(x => x.DuracionMin).HasColumnName("duracion_min");            e.Property(x => x.DuracionMin).HasColumnName("duracion_min");
            e.Property(x => x.Stock).HasColumnName("stock");            e.Property(x => x.Stock).HasColumnName("stock");            e.Property(x => x.Stock).HasColumnName("stock");
            e.Property(x => x.StockMinimo).HasColumnName("stock_minimo");            e.Property(x => x.StockMinimo).HasColumnName("stock_minimo");            e.Property(x => x.StockMinimo).HasColumnName("stock_minimo");
            e.Property(x => x.Costo).HasColumnName("costo").HasPrecision(12, 2);            e.Property(x => x.Costo).HasColumnName("costo").HasPrecision(12, 2);            e.Property(x => x.Costo).HasColumnName("costo").HasPrecision(12, 2);
            e.Property(x => x.IdCategoria).HasColumnName("id_categoria");            e.Property(x => x.IdCategoria).HasColumnName("id_categoria");            e.Property(x => x.IdCategoria).HasColumnName("id_categoria");
            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);
            e.HasIndex(x => x.IdCategoria).HasDatabaseName("ix_items_categoria");            e.HasIndex(x => x.IdCategoria).HasDatabaseName("ix_items_categoria");            e.HasIndex(x => x.IdCategoria).HasDatabaseName("ix_items_categoria");
            e.HasOne(x => x.Categoria)            e.HasOne(x => x.Categoria)            e.HasOne(x => x.Categoria)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdCategoria)                .HasForeignKey(x => x.IdCategoria)                .HasForeignKey(x => x.IdCategoria)                .HasForeignKey(x => x.IdCategoria)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_items_categoria");                .HasConstraintName("fk_items_categoria");                .HasConstraintName("fk_items_categoria");                .HasConstraintName("fk_items_categoria");
        });        });

        modelBuilder.Entity<Banco>(e =>        modelBuilder.Entity<Banco>(e =>
        {        {
            e.ToTable("bancos");            e.ToTable("bancos");            e.ToTable("bancos");
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_banco");            e.Property(x => x.Id).HasColumnName("id_banco");            e.Property(x => x.Id).HasColumnName("id_banco");
            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);
        });        });

        modelBuilder.Entity<TarjetaCredito>(e =>        modelBuilder.Entity<TarjetaCredito>(e =>
        {        {
            e.ToTable("tarjetas_credito");            e.ToTable("tarjetas_credito");            e.ToTable("tarjetas_credito");
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_tarjeta_credito");            e.Property(x => x.Id).HasColumnName("id_tarjeta_credito");            e.Property(x => x.Id).HasColumnName("id_tarjeta_credito");
            e.Property(x => x.IdBanco).HasColumnName("id_banco");            e.Property(x => x.IdBanco).HasColumnName("id_banco");            e.Property(x => x.IdBanco).HasColumnName("id_banco");
            e.Property(x => x.TipoTarjeta).HasColumnName("tipo_tarjeta").HasMaxLength(50).IsRequired();            e.Property(x => x.TipoTarjeta).HasColumnName("tipo_tarjeta").HasMaxLength(50).IsRequired();            e.Property(x => x.TipoTarjeta).HasColumnName("tipo_tarjeta").HasMaxLength(50).IsRequired();
            e.Property(x => x.PorcentajeRecargo).HasColumnName("porcentaje_recargo").HasPrecision(5, 2).HasDefaultValue(0m);            e.Property(x => x.PorcentajeRecargo).HasColumnName("porcentaje_recargo").HasPrecision(5, 2).HasDefaultValue(0m);            e.Property(x => x.PorcentajeRecargo).HasColumnName("porcentaje_recargo").HasPrecision(5, 2).HasDefaultValue(0m);
            e.Property(x => x.CuotasMax).HasColumnName("cuotas_max").HasDefaultValue(1);            e.Property(x => x.CuotasMax).HasColumnName("cuotas_max").HasDefaultValue(1);            e.Property(x => x.CuotasMax).HasColumnName("cuotas_max").HasDefaultValue(1);
            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);
            e.Property(x => x.FechaAlta).HasColumnName("fecha_alta").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.FechaAlta).HasColumnName("fecha_alta").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.FechaAlta).HasColumnName("fecha_alta").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.HasIndex(x => new { x.IdBanco, x.TipoTarjeta }).IsUnique().HasDatabaseName("uq_tarjetas_banco_tipo");            e.HasIndex(x => new { x.IdBanco, x.TipoTarjeta }).IsUnique().HasDatabaseName("uq_tarjetas_banco_tipo");            e.HasIndex(x => new { x.IdBanco, x.TipoTarjeta }).IsUnique().HasDatabaseName("uq_tarjetas_banco_tipo");
            e.HasOne(x => x.Banco)            e.HasOne(x => x.Banco)            e.HasOne(x => x.Banco)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdBanco)                .HasForeignKey(x => x.IdBanco)                .HasForeignKey(x => x.IdBanco)                .HasForeignKey(x => x.IdBanco)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_tarjetas_banco");                .HasConstraintName("fk_tarjetas_banco");                .HasConstraintName("fk_tarjetas_banco");                .HasConstraintName("fk_tarjetas_banco");
        });        });

        modelBuilder.Entity<Proveedor>(e =>        modelBuilder.Entity<Proveedor>(e =>
        {        {
            e.ToTable("proveedores");            e.ToTable("proveedores");            e.ToTable("proveedores");
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_proveedor");            e.Property(x => x.Id).HasColumnName("id_proveedor");            e.Property(x => x.Id).HasColumnName("id_proveedor");
            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();            e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();
            e.Property(x => x.Cuit).HasColumnName("cuit").HasMaxLength(20);            e.Property(x => x.Cuit).HasColumnName("cuit").HasMaxLength(20);            e.Property(x => x.Cuit).HasColumnName("cuit").HasMaxLength(20);
            e.Property(x => x.Telefono).HasColumnName("telefono").HasMaxLength(40);            e.Property(x => x.Telefono).HasColumnName("telefono").HasMaxLength(40);            e.Property(x => x.Telefono).HasColumnName("telefono").HasMaxLength(40);
            e.Property(x => x.Email).HasColumnName("email").HasMaxLength(120);            e.Property(x => x.Email).HasColumnName("email").HasMaxLength(120);            e.Property(x => x.Email).HasColumnName("email").HasMaxLength(120);
            e.Property(x => x.Direccion).HasColumnName("direccion").HasMaxLength(200);            e.Property(x => x.Direccion).HasColumnName("direccion").HasMaxLength(200);            e.Property(x => x.Direccion).HasColumnName("direccion").HasMaxLength(200);
            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);            e.Property(x => x.Activo).HasColumnName("activo").HasColumnType("tinyint(1)").HasDefaultValue(true);
            e.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("ux_proveedores_nombre");            e.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("ux_proveedores_nombre");            e.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("ux_proveedores_nombre");
        });        });
    }

    private static void ConfigurarVentas(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Venta>(e =>        modelBuilder.Entity<Venta>(e =>
        {        {
            e.ToTable("ventas");            e.ToTable("ventas");            e.ToTable("ventas");
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_venta");            e.Property(x => x.Id).HasColumnName("id_venta");            e.Property(x => x.Id).HasColumnName("id_venta");
            e.Property(x => x.IdCliente).HasColumnName("id_cliente");            e.Property(x => x.IdCliente).HasColumnName("id_cliente");            e.Property(x => x.IdCliente).HasColumnName("id_cliente");
            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
            e.Property(x => x.IdEmpleado).HasColumnName("id_empleado");            e.Property(x => x.IdEmpleado).HasColumnName("id_empleado");            e.Property(x => x.IdEmpleado).HasColumnName("id_empleado");
            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.Property(x => x.MedioPago)            e.Property(x => x.MedioPago)            e.Property(x => x.MedioPago)
                .HasColumnName("medio_pago")                .HasColumnName("medio_pago")                .HasColumnName("medio_pago")                .HasColumnName("medio_pago")
                .HasColumnType("enum('efectivo', 'tarjeta', 'transferencia', 'cuenta_corriente')")                .HasColumnType("enum('efectivo', 'tarjeta', 'transferencia', 'cuenta_corriente')")                .HasColumnType("enum('efectivo', 'tarjeta', 'transferencia', 'cuenta_corriente')")                .HasColumnType("enum('efectivo', 'tarjeta', 'transferencia', 'cuenta_corriente')")
                .HasDefaultValue("efectivo")                .HasDefaultValue("efectivo")                .HasDefaultValue("efectivo")                .HasDefaultValue("efectivo")
                .IsRequired();                .IsRequired();                .IsRequired();                .IsRequired();
            e.Property(x => x.IdTarjetaCredito).HasColumnName("id_tarjeta_credito");            e.Property(x => x.IdTarjetaCredito).HasColumnName("id_tarjeta_credito");            e.Property(x => x.IdTarjetaCredito).HasColumnName("id_tarjeta_credito");
            e.Property(x => x.CantidadCuotas).HasColumnName("cantidad_cuotas");            e.Property(x => x.CantidadCuotas).HasColumnName("cantidad_cuotas");            e.Property(x => x.CantidadCuotas).HasColumnName("cantidad_cuotas");
            e.Property(x => x.Descuento).HasColumnName("descuento").HasPrecision(12, 2).HasDefaultValue(0m);            e.Property(x => x.Descuento).HasColumnName("descuento").HasPrecision(12, 2).HasDefaultValue(0m);            e.Property(x => x.Descuento).HasColumnName("descuento").HasPrecision(12, 2).HasDefaultValue(0m);
            e.Property(x => x.Recargo).HasColumnName("recargo").HasPrecision(12, 2).HasDefaultValue(0m);            e.Property(x => x.Recargo).HasColumnName("recargo").HasPrecision(12, 2).HasDefaultValue(0m);            e.Property(x => x.Recargo).HasColumnName("recargo").HasPrecision(12, 2).HasDefaultValue(0m);
            e.Property(x => x.Total).HasColumnName("total").HasPrecision(12, 2).HasDefaultValue(0m);            e.Property(x => x.Total).HasColumnName("total").HasPrecision(12, 2).HasDefaultValue(0m);            e.Property(x => x.Total).HasColumnName("total").HasPrecision(12, 2).HasDefaultValue(0m);
            e.Property(x => x.Anulada).HasColumnName("anulada").HasColumnType("tinyint(1)").HasDefaultValue(false);            e.Property(x => x.Anulada).HasColumnName("anulada").HasColumnType("tinyint(1)").HasDefaultValue(false);            e.Property(x => x.Anulada).HasColumnName("anulada").HasColumnType("tinyint(1)").HasDefaultValue(false);
            e.Property(x => x.FechaAnulacion).HasColumnName("fecha_anulacion").HasColumnType("datetime");            e.Property(x => x.FechaAnulacion).HasColumnName("fecha_anulacion").HasColumnType("datetime");            e.Property(x => x.FechaAnulacion).HasColumnName("fecha_anulacion").HasColumnType("datetime");
            e.Property(x => x.IdUsuarioAnulacion).HasColumnName("id_usuario_anulacion");            e.Property(x => x.IdUsuarioAnulacion).HasColumnName("id_usuario_anulacion");            e.Property(x => x.IdUsuarioAnulacion).HasColumnName("id_usuario_anulacion");
            e.Property(x => x.MotivoAnulacion).HasColumnName("motivo_anulacion").HasMaxLength(255);            e.Property(x => x.MotivoAnulacion).HasColumnName("motivo_anulacion").HasMaxLength(255);            e.Property(x => x.MotivoAnulacion).HasColumnName("motivo_anulacion").HasMaxLength(255);

            e.HasIndex(x => x.IdCliente).HasDatabaseName("ix_ventas_cliente");            e.HasIndex(x => x.IdCliente).HasDatabaseName("ix_ventas_cliente");            e.HasIndex(x => x.IdCliente).HasDatabaseName("ix_ventas_cliente");
            e.HasIndex(x => x.Fecha).HasDatabaseName("ix_ventas_fecha");            e.HasIndex(x => x.Fecha).HasDatabaseName("ix_ventas_fecha");            e.HasIndex(x => x.Fecha).HasDatabaseName("ix_ventas_fecha");
            e.HasIndex(x => x.Anulada).HasDatabaseName("ix_ventas_anulada");            e.HasIndex(x => x.Anulada).HasDatabaseName("ix_ventas_anulada");            e.HasIndex(x => x.Anulada).HasDatabaseName("ix_ventas_anulada");

            e.HasOne(x => x.Cliente)            e.HasOne(x => x.Cliente)            e.HasOne(x => x.Cliente)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdCliente)                .HasForeignKey(x => x.IdCliente)                .HasForeignKey(x => x.IdCliente)                .HasForeignKey(x => x.IdCliente)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ventas_cliente");                .HasConstraintName("fk_ventas_cliente");                .HasConstraintName("fk_ventas_cliente");                .HasConstraintName("fk_ventas_cliente");
            e.HasOne(x => x.Usuario)            e.HasOne(x => x.Usuario)            e.HasOne(x => x.Usuario)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ventas_usuario");                .HasConstraintName("fk_ventas_usuario");                .HasConstraintName("fk_ventas_usuario");                .HasConstraintName("fk_ventas_usuario");
            e.HasOne(x => x.Empleado)            e.HasOne(x => x.Empleado)            e.HasOne(x => x.Empleado)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdEmpleado)                .HasForeignKey(x => x.IdEmpleado)                .HasForeignKey(x => x.IdEmpleado)                .HasForeignKey(x => x.IdEmpleado)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ventas_empleado");                .HasConstraintName("fk_ventas_empleado");                .HasConstraintName("fk_ventas_empleado");                .HasConstraintName("fk_ventas_empleado");
            e.HasOne(x => x.TarjetaCredito)            e.HasOne(x => x.TarjetaCredito)            e.HasOne(x => x.TarjetaCredito)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdTarjetaCredito)                .HasForeignKey(x => x.IdTarjetaCredito)                .HasForeignKey(x => x.IdTarjetaCredito)                .HasForeignKey(x => x.IdTarjetaCredito)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ventas_tarjeta");                .HasConstraintName("fk_ventas_tarjeta");                .HasConstraintName("fk_ventas_tarjeta");                .HasConstraintName("fk_ventas_tarjeta");
            e.HasOne(x => x.UsuarioAnulacion)            e.HasOne(x => x.UsuarioAnulacion)            e.HasOne(x => x.UsuarioAnulacion)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdUsuarioAnulacion)                .HasForeignKey(x => x.IdUsuarioAnulacion)                .HasForeignKey(x => x.IdUsuarioAnulacion)                .HasForeignKey(x => x.IdUsuarioAnulacion)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ventas_usuario_anulacion");                .HasConstraintName("fk_ventas_usuario_anulacion");                .HasConstraintName("fk_ventas_usuario_anulacion");                .HasConstraintName("fk_ventas_usuario_anulacion");
        });        });

        modelBuilder.Entity<DetalleVenta>(e =>        modelBuilder.Entity<DetalleVenta>(e =>
        {        {
            e.ToTable("detalle_venta");            e.ToTable("detalle_venta");            e.ToTable("detalle_venta");
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_detalle");            e.Property(x => x.Id).HasColumnName("id_detalle");            e.Property(x => x.Id).HasColumnName("id_detalle");
            e.Property(x => x.IdVenta).HasColumnName("id_venta");            e.Property(x => x.IdVenta).HasColumnName("id_venta");            e.Property(x => x.IdVenta).HasColumnName("id_venta");
            e.Property(x => x.IdItem).HasColumnName("id_item");            e.Property(x => x.IdItem).HasColumnName("id_item");            e.Property(x => x.IdItem).HasColumnName("id_item");
            e.Property(x => x.Cantidad).HasColumnName("cantidad");            e.Property(x => x.Cantidad).HasColumnName("cantidad");            e.Property(x => x.Cantidad).HasColumnName("cantidad");
            e.Property(x => x.Precio).HasColumnName("precio").HasPrecision(12, 2);            e.Property(x => x.Precio).HasColumnName("precio").HasPrecision(12, 2);            e.Property(x => x.Precio).HasColumnName("precio").HasPrecision(12, 2);
            e.Property(x => x.Subtotal).HasColumnName("subtotal").HasPrecision(12, 2);            e.Property(x => x.Subtotal).HasColumnName("subtotal").HasPrecision(12, 2);            e.Property(x => x.Subtotal).HasColumnName("subtotal").HasPrecision(12, 2);
            e.HasIndex(x => x.IdVenta).HasDatabaseName("ix_detalle_venta");            e.HasIndex(x => x.IdVenta).HasDatabaseName("ix_detalle_venta");            e.HasIndex(x => x.IdVenta).HasDatabaseName("ix_detalle_venta");
            e.HasIndex(x => x.IdItem).HasDatabaseName("ix_detalle_item");            e.HasIndex(x => x.IdItem).HasDatabaseName("ix_detalle_item");            e.HasIndex(x => x.IdItem).HasDatabaseName("ix_detalle_item");
            e.HasOne(x => x.Venta)            e.HasOne(x => x.Venta)            e.HasOne(x => x.Venta)
                .WithMany(x => x.Detalles)                .WithMany(x => x.Detalles)                .WithMany(x => x.Detalles)                .WithMany(x => x.Detalles)
                .HasForeignKey(x => x.IdVenta)                .HasForeignKey(x => x.IdVenta)                .HasForeignKey(x => x.IdVenta)                .HasForeignKey(x => x.IdVenta)
                .OnDelete(DeleteBehavior.Cascade)                .OnDelete(DeleteBehavior.Cascade)                .OnDelete(DeleteBehavior.Cascade)                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_detalle_venta");                .HasConstraintName("fk_detalle_venta");                .HasConstraintName("fk_detalle_venta");                .HasConstraintName("fk_detalle_venta");
            e.HasOne(x => x.Item)            e.HasOne(x => x.Item)            e.HasOne(x => x.Item)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdItem)                .HasForeignKey(x => x.IdItem)                .HasForeignKey(x => x.IdItem)                .HasForeignKey(x => x.IdItem)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detalle_item");                .HasConstraintName("fk_detalle_item");                .HasConstraintName("fk_detalle_item");                .HasConstraintName("fk_detalle_item");
        });        });
    }

    private static void ConfigurarCuentaCorriente(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CuentaCorriente>(e =>        modelBuilder.Entity<CuentaCorriente>(e =>
        {        {
            e.ToTable("cuentas_corrientes");            e.ToTable("cuentas_corrientes");            e.ToTable("cuentas_corrientes");
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_cuenta_corriente");            e.Property(x => x.Id).HasColumnName("id_cuenta_corriente");            e.Property(x => x.Id).HasColumnName("id_cuenta_corriente");
            e.Property(x => x.IdCliente).HasColumnName("id_cliente");            e.Property(x => x.IdCliente).HasColumnName("id_cliente");            e.Property(x => x.IdCliente).HasColumnName("id_cliente");
            e.Property(x => x.Saldo).HasColumnName("saldo").HasPrecision(12, 2).HasDefaultValue(0m);            e.Property(x => x.Saldo).HasColumnName("saldo").HasPrecision(12, 2).HasDefaultValue(0m);            e.Property(x => x.Saldo).HasColumnName("saldo").HasPrecision(12, 2).HasDefaultValue(0m);
            e.Property(x => x.FechaActualizacion).HasColumnName("fecha_actualizacion").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.FechaActualizacion).HasColumnName("fecha_actualizacion").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.FechaActualizacion).HasColumnName("fecha_actualizacion").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.HasIndex(x => x.IdCliente).IsUnique().HasDatabaseName("uq_cuentas_cliente");            e.HasIndex(x => x.IdCliente).IsUnique().HasDatabaseName("uq_cuentas_cliente");            e.HasIndex(x => x.IdCliente).IsUnique().HasDatabaseName("uq_cuentas_cliente");
            e.HasOne(x => x.Cliente)            e.HasOne(x => x.Cliente)            e.HasOne(x => x.Cliente)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdCliente)                .HasForeignKey(x => x.IdCliente)                .HasForeignKey(x => x.IdCliente)                .HasForeignKey(x => x.IdCliente)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cuentas_cliente");                .HasConstraintName("fk_cuentas_cliente");                .HasConstraintName("fk_cuentas_cliente");                .HasConstraintName("fk_cuentas_cliente");
        });        });

        modelBuilder.Entity<MovimientoCuentaCorriente>(e =>        modelBuilder.Entity<MovimientoCuentaCorriente>(e =>
        {        {
            e.ToTable("movimientos_cuenta_corriente");            e.ToTable("movimientos_cuenta_corriente");            e.ToTable("movimientos_cuenta_corriente");
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_movimiento");            e.Property(x => x.Id).HasColumnName("id_movimiento");            e.Property(x => x.Id).HasColumnName("id_movimiento");
            e.Property(x => x.IdCuenta).HasColumnName("id_cuenta");            e.Property(x => x.IdCuenta).HasColumnName("id_cuenta");            e.Property(x => x.IdCuenta).HasColumnName("id_cuenta");
            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.Property(x => x.Tipo)            e.Property(x => x.Tipo)            e.Property(x => x.Tipo)
                .HasColumnName("tipo")                .HasColumnName("tipo")                .HasColumnName("tipo")                .HasColumnName("tipo")
                .HasColumnType("enum('cargo', 'abono')")                .HasColumnType("enum('cargo', 'abono')")                .HasColumnType("enum('cargo', 'abono')")                .HasColumnType("enum('cargo', 'abono')")
                .HasConversion(                .HasConversion(                .HasConversion(                .HasConversion(
                    v => v == TipoMovimientoCuentaCorriente.Cargo ? "cargo" : "abono",                    v => v == TipoMovimientoCuentaCorriente.Cargo ? "cargo" : "abono",                    v => v == TipoMovimientoCuentaCorriente.Cargo ? "cargo" : "abono",                    v => v == TipoMovimientoCuentaCorriente.Cargo ? "cargo" : "abono",                    v => v == TipoMovimientoCuentaCorriente.Cargo ? "cargo" : "abono",
                    v => v == "cargo" ? TipoMovimientoCuentaCorriente.Cargo : TipoMovimientoCuentaCorriente.Abono)                    v => v == "cargo" ? TipoMovimientoCuentaCorriente.Cargo : TipoMovimientoCuentaCorriente.Abono)                    v => v == "cargo" ? TipoMovimientoCuentaCorriente.Cargo : TipoMovimientoCuentaCorriente.Abono)                    v => v == "cargo" ? TipoMovimientoCuentaCorriente.Cargo : TipoMovimientoCuentaCorriente.Abono)                    v => v == "cargo" ? TipoMovimientoCuentaCorriente.Cargo : TipoMovimientoCuentaCorriente.Abono)
                .IsRequired();                .IsRequired();                .IsRequired();                .IsRequired();
            e.Property(x => x.Monto).HasColumnName("monto").HasPrecision(12, 2);            e.Property(x => x.Monto).HasColumnName("monto").HasPrecision(12, 2);            e.Property(x => x.Monto).HasColumnName("monto").HasPrecision(12, 2);
            e.Property(x => x.Concepto).HasColumnName("concepto").HasMaxLength(255);            e.Property(x => x.Concepto).HasColumnName("concepto").HasMaxLength(255);            e.Property(x => x.Concepto).HasColumnName("concepto").HasMaxLength(255);
            e.Property(x => x.IdVenta).HasColumnName("id_venta");            e.Property(x => x.IdVenta).HasColumnName("id_venta");            e.Property(x => x.IdVenta).HasColumnName("id_venta");
            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
            e.HasIndex(x => x.IdCuenta).HasDatabaseName("ix_mov_cuenta");            e.HasIndex(x => x.IdCuenta).HasDatabaseName("ix_mov_cuenta");            e.HasIndex(x => x.IdCuenta).HasDatabaseName("ix_mov_cuenta");
            e.HasOne(x => x.Cuenta)            e.HasOne(x => x.Cuenta)            e.HasOne(x => x.Cuenta)
                .WithMany(x => x.Movimientos)                .WithMany(x => x.Movimientos)                .WithMany(x => x.Movimientos)                .WithMany(x => x.Movimientos)
                .HasForeignKey(x => x.IdCuenta)                .HasForeignKey(x => x.IdCuenta)                .HasForeignKey(x => x.IdCuenta)                .HasForeignKey(x => x.IdCuenta)
                .OnDelete(DeleteBehavior.Cascade)                .OnDelete(DeleteBehavior.Cascade)                .OnDelete(DeleteBehavior.Cascade)                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_mov_cuenta");                .HasConstraintName("fk_mov_cuenta");                .HasConstraintName("fk_mov_cuenta");                .HasConstraintName("fk_mov_cuenta");
            e.HasOne(x => x.Venta)            e.HasOne(x => x.Venta)            e.HasOne(x => x.Venta)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdVenta)                .HasForeignKey(x => x.IdVenta)                .HasForeignKey(x => x.IdVenta)                .HasForeignKey(x => x.IdVenta)
                .OnDelete(DeleteBehavior.SetNull)                .OnDelete(DeleteBehavior.SetNull)                .OnDelete(DeleteBehavior.SetNull)                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_mov_venta");                .HasConstraintName("fk_mov_venta");                .HasConstraintName("fk_mov_venta");                .HasConstraintName("fk_mov_venta");
            e.HasOne(x => x.Usuario)            e.HasOne(x => x.Usuario)            e.HasOne(x => x.Usuario)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_mov_usuario");                .HasConstraintName("fk_mov_usuario");                .HasConstraintName("fk_mov_usuario");                .HasConstraintName("fk_mov_usuario");
        });        });
    }

    private static void ConfigurarGastosIngresos(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gasto>(e =>        modelBuilder.Entity<Gasto>(e =>
        {        {
            e.ToTable("gastos");            e.ToTable("gastos");            e.ToTable("gastos");
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_gasto");            e.Property(x => x.Id).HasColumnName("id_gasto");            e.Property(x => x.Id).HasColumnName("id_gasto");
            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.Property(x => x.Concepto).HasColumnName("concepto").HasMaxLength(150).IsRequired();            e.Property(x => x.Concepto).HasColumnName("concepto").HasMaxLength(150).IsRequired();            e.Property(x => x.Concepto).HasColumnName("concepto").HasMaxLength(150).IsRequired();
            e.Property(x => x.Monto).HasColumnName("monto").HasPrecision(12, 2);            e.Property(x => x.Monto).HasColumnName("monto").HasPrecision(12, 2);            e.Property(x => x.Monto).HasColumnName("monto").HasPrecision(12, 2);
            e.Property(x => x.Categoria).HasColumnName("categoria").HasMaxLength(100);            e.Property(x => x.Categoria).HasColumnName("categoria").HasMaxLength(100);            e.Property(x => x.Categoria).HasColumnName("categoria").HasMaxLength(100);
            e.Property(x => x.Observacion).HasColumnName("observacion").HasMaxLength(255);            e.Property(x => x.Observacion).HasColumnName("observacion").HasMaxLength(255);            e.Property(x => x.Observacion).HasColumnName("observacion").HasMaxLength(255);
            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
            e.Property(x => x.FechaAlta).HasColumnName("fecha_alta").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.FechaAlta).HasColumnName("fecha_alta").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.FechaAlta).HasColumnName("fecha_alta").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.HasIndex(x => x.Fecha).HasDatabaseName("ix_gastos_fecha");            e.HasIndex(x => x.Fecha).HasDatabaseName("ix_gastos_fecha");            e.HasIndex(x => x.Fecha).HasDatabaseName("ix_gastos_fecha");
            e.HasOne(x => x.Usuario)            e.HasOne(x => x.Usuario)            e.HasOne(x => x.Usuario)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_gastos_usuario");                .HasConstraintName("fk_gastos_usuario");                .HasConstraintName("fk_gastos_usuario");                .HasConstraintName("fk_gastos_usuario");
        });        });

        modelBuilder.Entity<IngresoMercaderia>(e =>        modelBuilder.Entity<IngresoMercaderia>(e =>
        {        {
            e.ToTable("ingresos_mercaderia");            e.ToTable("ingresos_mercaderia");            e.ToTable("ingresos_mercaderia");
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_ingreso");            e.Property(x => x.Id).HasColumnName("id_ingreso");            e.Property(x => x.Id).HasColumnName("id_ingreso");
            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.Property(x => x.IdProveedor).HasColumnName("id_proveedor");            e.Property(x => x.IdProveedor).HasColumnName("id_proveedor");            e.Property(x => x.IdProveedor).HasColumnName("id_proveedor");
            e.Property(x => x.Proveedor).HasColumnName("proveedor").HasMaxLength(150);            e.Property(x => x.Proveedor).HasColumnName("proveedor").HasMaxLength(150);            e.Property(x => x.Proveedor).HasColumnName("proveedor").HasMaxLength(150);
            e.Property(x => x.Total).HasColumnName("total").HasPrecision(12, 2).HasDefaultValue(0m);            e.Property(x => x.Total).HasColumnName("total").HasPrecision(12, 2).HasDefaultValue(0m);            e.Property(x => x.Total).HasColumnName("total").HasPrecision(12, 2).HasDefaultValue(0m);
            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
            e.Property(x => x.Observacion).HasColumnName("observacion").HasMaxLength(255);            e.Property(x => x.Observacion).HasColumnName("observacion").HasMaxLength(255);            e.Property(x => x.Observacion).HasColumnName("observacion").HasMaxLength(255);
            e.HasIndex(x => x.Fecha).HasDatabaseName("ix_ingresos_fecha");            e.HasIndex(x => x.Fecha).HasDatabaseName("ix_ingresos_fecha");            e.HasIndex(x => x.Fecha).HasDatabaseName("ix_ingresos_fecha");
            e.HasIndex(x => x.IdProveedor).HasDatabaseName("ix_ingresos_proveedor");            e.HasIndex(x => x.IdProveedor).HasDatabaseName("ix_ingresos_proveedor");            e.HasIndex(x => x.IdProveedor).HasDatabaseName("ix_ingresos_proveedor");
            e.HasOne(x => x.ProveedorEntidad)            e.HasOne(x => x.ProveedorEntidad)            e.HasOne(x => x.ProveedorEntidad)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdProveedor)                .HasForeignKey(x => x.IdProveedor)                .HasForeignKey(x => x.IdProveedor)                .HasForeignKey(x => x.IdProveedor)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ingresos_proveedor");                .HasConstraintName("fk_ingresos_proveedor");                .HasConstraintName("fk_ingresos_proveedor");                .HasConstraintName("fk_ingresos_proveedor");
            e.HasOne(x => x.Usuario)            e.HasOne(x => x.Usuario)            e.HasOne(x => x.Usuario)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ingresos_usuario");                .HasConstraintName("fk_ingresos_usuario");                .HasConstraintName("fk_ingresos_usuario");                .HasConstraintName("fk_ingresos_usuario");
        });        });

        modelBuilder.Entity<IngresoMercaderiaDetalle>(e =>        modelBuilder.Entity<IngresoMercaderiaDetalle>(e =>
        {        {
            e.ToTable("ingresos_mercaderia_detalle");            e.ToTable("ingresos_mercaderia_detalle");            e.ToTable("ingresos_mercaderia_detalle");
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_detalle_ingreso");            e.Property(x => x.Id).HasColumnName("id_detalle_ingreso");            e.Property(x => x.Id).HasColumnName("id_detalle_ingreso");
            e.Property(x => x.IdIngreso).HasColumnName("id_ingreso");            e.Property(x => x.IdIngreso).HasColumnName("id_ingreso");            e.Property(x => x.IdIngreso).HasColumnName("id_ingreso");
            e.Property(x => x.IdItem).HasColumnName("id_item");            e.Property(x => x.IdItem).HasColumnName("id_item");            e.Property(x => x.IdItem).HasColumnName("id_item");
            e.Property(x => x.Cantidad).HasColumnName("cantidad");            e.Property(x => x.Cantidad).HasColumnName("cantidad");            e.Property(x => x.Cantidad).HasColumnName("cantidad");
            e.Property(x => x.CostoUnitario).HasColumnName("costo_unitario").HasPrecision(12, 2);            e.Property(x => x.CostoUnitario).HasColumnName("costo_unitario").HasPrecision(12, 2);            e.Property(x => x.CostoUnitario).HasColumnName("costo_unitario").HasPrecision(12, 2);
            e.Property(x => x.Subtotal).HasColumnName("subtotal").HasPrecision(12, 2);            e.Property(x => x.Subtotal).HasColumnName("subtotal").HasPrecision(12, 2);            e.Property(x => x.Subtotal).HasColumnName("subtotal").HasPrecision(12, 2);
            e.HasIndex(x => x.IdIngreso).HasDatabaseName("ix_ingdet_ingreso");            e.HasIndex(x => x.IdIngreso).HasDatabaseName("ix_ingdet_ingreso");            e.HasIndex(x => x.IdIngreso).HasDatabaseName("ix_ingdet_ingreso");
            e.HasIndex(x => x.IdItem).HasDatabaseName("ix_ingdet_item");            e.HasIndex(x => x.IdItem).HasDatabaseName("ix_ingdet_item");            e.HasIndex(x => x.IdItem).HasDatabaseName("ix_ingdet_item");
            e.HasOne(x => x.Ingreso)            e.HasOne(x => x.Ingreso)            e.HasOne(x => x.Ingreso)
                .WithMany(x => x.Detalles)                .WithMany(x => x.Detalles)                .WithMany(x => x.Detalles)                .WithMany(x => x.Detalles)
                .HasForeignKey(x => x.IdIngreso)                .HasForeignKey(x => x.IdIngreso)                .HasForeignKey(x => x.IdIngreso)                .HasForeignKey(x => x.IdIngreso)
                .OnDelete(DeleteBehavior.Cascade)                .OnDelete(DeleteBehavior.Cascade)                .OnDelete(DeleteBehavior.Cascade)                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_ingdet_ingreso");                .HasConstraintName("fk_ingdet_ingreso");                .HasConstraintName("fk_ingdet_ingreso");                .HasConstraintName("fk_ingdet_ingreso");
            e.HasOne(x => x.Item)            e.HasOne(x => x.Item)            e.HasOne(x => x.Item)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdItem)                .HasForeignKey(x => x.IdItem)                .HasForeignKey(x => x.IdItem)                .HasForeignKey(x => x.IdItem)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ingdet_item");                .HasConstraintName("fk_ingdet_item");                .HasConstraintName("fk_ingdet_item");                .HasConstraintName("fk_ingdet_item");
        });        });
    }

    private static void ConfigurarAtencionesAuditoria(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Atencion>(e =>        modelBuilder.Entity<Atencion>(e =>
        {        {
            e.ToTable("atenciones");            e.ToTable("atenciones");            e.ToTable("atenciones");
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_atencion");            e.Property(x => x.Id).HasColumnName("id_atencion");            e.Property(x => x.Id).HasColumnName("id_atencion");
            e.Property(x => x.IdCliente).HasColumnName("id_cliente");            e.Property(x => x.IdCliente).HasColumnName("id_cliente");            e.Property(x => x.IdCliente).HasColumnName("id_cliente");
            e.Property(x => x.IdEmpleado).HasColumnName("id_empleado");            e.Property(x => x.IdEmpleado).HasColumnName("id_empleado");            e.Property(x => x.IdEmpleado).HasColumnName("id_empleado");
            e.Property(x => x.IdItem).HasColumnName("id_item");            e.Property(x => x.IdItem).HasColumnName("id_item");            e.Property(x => x.IdItem).HasColumnName("id_item");
            e.Property(x => x.Tipo)            e.Property(x => x.Tipo)            e.Property(x => x.Tipo)
                .HasColumnName("tipo")                .HasColumnName("tipo")                .HasColumnName("tipo")                .HasColumnName("tipo")
                .HasColumnType("enum('turno', 'llegada')")                .HasColumnType("enum('turno', 'llegada')")                .HasColumnType("enum('turno', 'llegada')")                .HasColumnType("enum('turno', 'llegada')")
                .HasDefaultValue(TipoAtencion.Llegada)                .HasDefaultValue(TipoAtencion.Llegada)                .HasDefaultValue(TipoAtencion.Llegada)                .HasDefaultValue(TipoAtencion.Llegada)
                .HasConversion(                .HasConversion(                .HasConversion(                .HasConversion(
                    v => v == TipoAtencion.Turno ? "turno" : "llegada",                    v => v == TipoAtencion.Turno ? "turno" : "llegada",                    v => v == TipoAtencion.Turno ? "turno" : "llegada",                    v => v == TipoAtencion.Turno ? "turno" : "llegada",                    v => v == TipoAtencion.Turno ? "turno" : "llegada",
                    v => v == "turno" ? TipoAtencion.Turno : TipoAtencion.Llegada)                    v => v == "turno" ? TipoAtencion.Turno : TipoAtencion.Llegada)                    v => v == "turno" ? TipoAtencion.Turno : TipoAtencion.Llegada)                    v => v == "turno" ? TipoAtencion.Turno : TipoAtencion.Llegada)                    v => v == "turno" ? TipoAtencion.Turno : TipoAtencion.Llegada)
                .IsRequired();                .IsRequired();                .IsRequired();                .IsRequired();
            e.Property(x => x.FechaHora).HasColumnName("fecha_hora").HasColumnType("datetime");            e.Property(x => x.FechaHora).HasColumnName("fecha_hora").HasColumnType("datetime");            e.Property(x => x.FechaHora).HasColumnName("fecha_hora").HasColumnType("datetime");
            e.Property(x => x.Estado)            e.Property(x => x.Estado)            e.Property(x => x.Estado)
                .HasColumnName("estado")                .HasColumnName("estado")                .HasColumnName("estado")                .HasColumnName("estado")
                .HasColumnType("enum('pendiente', 'en_atencion', 'finalizada', 'cancelada')")                .HasColumnType("enum('pendiente', 'en_atencion', 'finalizada', 'cancelada')")                .HasColumnType("enum('pendiente', 'en_atencion', 'finalizada', 'cancelada')")                .HasColumnType("enum('pendiente', 'en_atencion', 'finalizada', 'cancelada')")
                .HasDefaultValue(EstadoAtencion.Pendiente)                .HasDefaultValue(EstadoAtencion.Pendiente)                .HasDefaultValue(EstadoAtencion.Pendiente)                .HasDefaultValue(EstadoAtencion.Pendiente)
                .HasConversion(                .HasConversion(                .HasConversion(                .HasConversion(
                    v => v == EstadoAtencion.Pendiente ? "pendiente"                    v => v == EstadoAtencion.Pendiente ? "pendiente"                    v => v == EstadoAtencion.Pendiente ? "pendiente"                    v => v == EstadoAtencion.Pendiente ? "pendiente"                    v => v == EstadoAtencion.Pendiente ? "pendiente"
                        : v == EstadoAtencion.EnAtencion ? "en_atencion"                        : v == EstadoAtencion.EnAtencion ? "en_atencion"                        : v == EstadoAtencion.EnAtencion ? "en_atencion"                        : v == EstadoAtencion.EnAtencion ? "en_atencion"                        : v == EstadoAtencion.EnAtencion ? "en_atencion"                        : v == EstadoAtencion.EnAtencion ? "en_atencion"
                        : v == EstadoAtencion.Finalizada ? "finalizada" : "cancelada",                        : v == EstadoAtencion.Finalizada ? "finalizada" : "cancelada",                        : v == EstadoAtencion.Finalizada ? "finalizada" : "cancelada",                        : v == EstadoAtencion.Finalizada ? "finalizada" : "cancelada",                        : v == EstadoAtencion.Finalizada ? "finalizada" : "cancelada",                        : v == EstadoAtencion.Finalizada ? "finalizada" : "cancelada",
                    v => v == "en_atencion" ? EstadoAtencion.EnAtencion                    v => v == "en_atencion" ? EstadoAtencion.EnAtencion                    v => v == "en_atencion" ? EstadoAtencion.EnAtencion                    v => v == "en_atencion" ? EstadoAtencion.EnAtencion                    v => v == "en_atencion" ? EstadoAtencion.EnAtencion
                        : v == "finalizada" ? EstadoAtencion.Finalizada                        : v == "finalizada" ? EstadoAtencion.Finalizada                        : v == "finalizada" ? EstadoAtencion.Finalizada                        : v == "finalizada" ? EstadoAtencion.Finalizada                        : v == "finalizada" ? EstadoAtencion.Finalizada                        : v == "finalizada" ? EstadoAtencion.Finalizada
                        : v == "cancelada" ? EstadoAtencion.Cancelada : EstadoAtencion.Pendiente)                        : v == "cancelada" ? EstadoAtencion.Cancelada : EstadoAtencion.Pendiente)                        : v == "cancelada" ? EstadoAtencion.Cancelada : EstadoAtencion.Pendiente)                        : v == "cancelada" ? EstadoAtencion.Cancelada : EstadoAtencion.Pendiente)                        : v == "cancelada" ? EstadoAtencion.Cancelada : EstadoAtencion.Pendiente)                        : v == "cancelada" ? EstadoAtencion.Cancelada : EstadoAtencion.Pendiente)
                .IsRequired();                .IsRequired();                .IsRequired();                .IsRequired();
            e.HasIndex(x => new { x.IdEmpleado, x.FechaHora }).HasDatabaseName("ix_atenciones_empleado");            e.HasIndex(x => new { x.IdEmpleado, x.FechaHora }).HasDatabaseName("ix_atenciones_empleado");            e.HasIndex(x => new { x.IdEmpleado, x.FechaHora }).HasDatabaseName("ix_atenciones_empleado");
            e.HasIndex(x => x.IdCliente).HasDatabaseName("ix_atenciones_cliente");            e.HasIndex(x => x.IdCliente).HasDatabaseName("ix_atenciones_cliente");            e.HasIndex(x => x.IdCliente).HasDatabaseName("ix_atenciones_cliente");
            e.HasOne(x => x.Cliente)            e.HasOne(x => x.Cliente)            e.HasOne(x => x.Cliente)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdCliente)                .HasForeignKey(x => x.IdCliente)                .HasForeignKey(x => x.IdCliente)                .HasForeignKey(x => x.IdCliente)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_atenciones_cliente");                .HasConstraintName("fk_atenciones_cliente");                .HasConstraintName("fk_atenciones_cliente");                .HasConstraintName("fk_atenciones_cliente");
            e.HasOne(x => x.Empleado)            e.HasOne(x => x.Empleado)            e.HasOne(x => x.Empleado)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdEmpleado)                .HasForeignKey(x => x.IdEmpleado)                .HasForeignKey(x => x.IdEmpleado)                .HasForeignKey(x => x.IdEmpleado)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_atenciones_empleado");                .HasConstraintName("fk_atenciones_empleado");                .HasConstraintName("fk_atenciones_empleado");                .HasConstraintName("fk_atenciones_empleado");
            e.HasOne(x => x.Servicio)            e.HasOne(x => x.Servicio)            e.HasOne(x => x.Servicio)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdItem)                .HasForeignKey(x => x.IdItem)                .HasForeignKey(x => x.IdItem)                .HasForeignKey(x => x.IdItem)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_atenciones_item");                .HasConstraintName("fk_atenciones_item");                .HasConstraintName("fk_atenciones_item");                .HasConstraintName("fk_atenciones_item");
        });        });

        modelBuilder.Entity<Auditoria>(e =>        modelBuilder.Entity<Auditoria>(e =>
        {        {
            e.ToTable("auditoria");            e.ToTable("auditoria");            e.ToTable("auditoria");
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_auditoria");            e.Property(x => x.Id).HasColumnName("id_auditoria");            e.Property(x => x.Id).HasColumnName("id_auditoria");
            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
            e.Property(x => x.Accion).HasColumnName("accion").HasMaxLength(40).IsRequired();            e.Property(x => x.Accion).HasColumnName("accion").HasMaxLength(40).IsRequired();            e.Property(x => x.Accion).HasColumnName("accion").HasMaxLength(40).IsRequired();
            e.Property(x => x.Entidad).HasColumnName("entidad").HasMaxLength(40).IsRequired();            e.Property(x => x.Entidad).HasColumnName("entidad").HasMaxLength(40).IsRequired();            e.Property(x => x.Entidad).HasColumnName("entidad").HasMaxLength(40).IsRequired();
            e.Property(x => x.IdEntidad).HasColumnName("id_entidad");            e.Property(x => x.IdEntidad).HasColumnName("id_entidad");            e.Property(x => x.IdEntidad).HasColumnName("id_entidad");
            e.Property(x => x.Detalle).HasColumnName("detalle").HasMaxLength(255);            e.Property(x => x.Detalle).HasColumnName("detalle").HasMaxLength(255);            e.Property(x => x.Detalle).HasColumnName("detalle").HasMaxLength(255);
            e.HasIndex(x => x.Fecha).HasDatabaseName("ix_auditoria_fecha");            e.HasIndex(x => x.Fecha).HasDatabaseName("ix_auditoria_fecha");            e.HasIndex(x => x.Fecha).HasDatabaseName("ix_auditoria_fecha");
            e.HasIndex(x => x.IdUsuario).HasDatabaseName("ix_auditoria_usuario");            e.HasIndex(x => x.IdUsuario).HasDatabaseName("ix_auditoria_usuario");            e.HasIndex(x => x.IdUsuario).HasDatabaseName("ix_auditoria_usuario");
            e.HasIndex(x => x.Accion).HasDatabaseName("ix_auditoria_accion");            e.HasIndex(x => x.Accion).HasDatabaseName("ix_auditoria_accion");            e.HasIndex(x => x.Accion).HasDatabaseName("ix_auditoria_accion");
            e.HasOne(x => x.Usuario)            e.HasOne(x => x.Usuario)            e.HasOne(x => x.Usuario)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_auditoria_usuario");                .HasConstraintName("fk_auditoria_usuario");                .HasConstraintName("fk_auditoria_usuario");                .HasConstraintName("fk_auditoria_usuario");
        });        });
    }

    private static void ConfigurarCajas(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Caja>(e =>        modelBuilder.Entity<Caja>(e =>
        {        {
            e.ToTable("cajas", t =>            e.ToTable("cajas", t =>            e.ToTable("cajas", t =>
            {            {            {
                t.HasCheckConstraint("chk_cajas_montos",                t.HasCheckConstraint("chk_cajas_montos",                t.HasCheckConstraint("chk_cajas_montos",                t.HasCheckConstraint("chk_cajas_montos",
                    "monto_inicial >= 0 AND (monto_esperado IS NULL OR monto_esperado >= 0) " +                    "monto_inicial >= 0 AND (monto_esperado IS NULL OR monto_esperado >= 0) " +                    "monto_inicial >= 0 AND (monto_esperado IS NULL OR monto_esperado >= 0) " +                    "monto_inicial >= 0 AND (monto_esperado IS NULL OR monto_esperado >= 0) " +                    "monto_inicial >= 0 AND (monto_esperado IS NULL OR monto_esperado >= 0) " +
                    "AND (monto_contado IS NULL OR monto_contado >= 0)");                    "AND (monto_contado IS NULL OR monto_contado >= 0)");                    "AND (monto_contado IS NULL OR monto_contado >= 0)");                    "AND (monto_contado IS NULL OR monto_contado >= 0)");                    "AND (monto_contado IS NULL OR monto_contado >= 0)");
            });            });            });
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_caja");            e.Property(x => x.Id).HasColumnName("id_caja");            e.Property(x => x.Id).HasColumnName("id_caja");
            e.Property(x => x.IdUsuarioApertura).HasColumnName("id_usuario_apertura");            e.Property(x => x.IdUsuarioApertura).HasColumnName("id_usuario_apertura");            e.Property(x => x.IdUsuarioApertura).HasColumnName("id_usuario_apertura");
            e.Property(x => x.FechaApertura).HasColumnName("fecha_apertura").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.FechaApertura).HasColumnName("fecha_apertura").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.FechaApertura).HasColumnName("fecha_apertura").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.Property(x => x.MontoInicial).HasColumnName("monto_inicial").HasPrecision(12, 2).HasDefaultValue(0m);            e.Property(x => x.MontoInicial).HasColumnName("monto_inicial").HasPrecision(12, 2).HasDefaultValue(0m);            e.Property(x => x.MontoInicial).HasColumnName("monto_inicial").HasPrecision(12, 2).HasDefaultValue(0m);
            e.Property(x => x.IdUsuarioCierre).HasColumnName("id_usuario_cierre");            e.Property(x => x.IdUsuarioCierre).HasColumnName("id_usuario_cierre");            e.Property(x => x.IdUsuarioCierre).HasColumnName("id_usuario_cierre");
            e.Property(x => x.FechaCierre).HasColumnName("fecha_cierre").HasColumnType("datetime");            e.Property(x => x.FechaCierre).HasColumnName("fecha_cierre").HasColumnType("datetime");            e.Property(x => x.FechaCierre).HasColumnName("fecha_cierre").HasColumnType("datetime");
            e.Property(x => x.MontoEsperado).HasColumnName("monto_esperado").HasPrecision(12, 2);            e.Property(x => x.MontoEsperado).HasColumnName("monto_esperado").HasPrecision(12, 2);            e.Property(x => x.MontoEsperado).HasColumnName("monto_esperado").HasPrecision(12, 2);
            e.Property(x => x.MontoContado).HasColumnName("monto_contado").HasPrecision(12, 2);            e.Property(x => x.MontoContado).HasColumnName("monto_contado").HasPrecision(12, 2);            e.Property(x => x.MontoContado).HasColumnName("monto_contado").HasPrecision(12, 2);
            e.Property(x => x.Diferencia).HasColumnName("diferencia").HasPrecision(12, 2);            e.Property(x => x.Diferencia).HasColumnName("diferencia").HasPrecision(12, 2);            e.Property(x => x.Diferencia).HasColumnName("diferencia").HasPrecision(12, 2);
            e.Property(x => x.Observacion).HasColumnName("observacion").HasMaxLength(255);            e.Property(x => x.Observacion).HasColumnName("observacion").HasMaxLength(255);            e.Property(x => x.Observacion).HasColumnName("observacion").HasMaxLength(255);
            e.Property(x => x.Estado)            e.Property(x => x.Estado)            e.Property(x => x.Estado)
                .HasColumnName("estado")                .HasColumnName("estado")                .HasColumnName("estado")                .HasColumnName("estado")
                .HasColumnType("enum('abierta', 'cerrada')")                .HasColumnType("enum('abierta', 'cerrada')")                .HasColumnType("enum('abierta', 'cerrada')")                .HasColumnType("enum('abierta', 'cerrada')")
                .HasDefaultValue("abierta")                .HasDefaultValue("abierta")                .HasDefaultValue("abierta")                .HasDefaultValue("abierta")
                .IsRequired();                .IsRequired();                .IsRequired();                .IsRequired();
            e.HasIndex(x => x.Estado).HasDatabaseName("ix_cajas_estado");            e.HasIndex(x => x.Estado).HasDatabaseName("ix_cajas_estado");            e.HasIndex(x => x.Estado).HasDatabaseName("ix_cajas_estado");
            e.HasIndex(x => x.FechaApertura).HasDatabaseName("ix_cajas_apertura");            e.HasIndex(x => x.FechaApertura).HasDatabaseName("ix_cajas_apertura");            e.HasIndex(x => x.FechaApertura).HasDatabaseName("ix_cajas_apertura");
            e.HasOne(x => x.UsuarioApertura)            e.HasOne(x => x.UsuarioApertura)            e.HasOne(x => x.UsuarioApertura)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdUsuarioApertura)                .HasForeignKey(x => x.IdUsuarioApertura)                .HasForeignKey(x => x.IdUsuarioApertura)                .HasForeignKey(x => x.IdUsuarioApertura)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cajas_usuario_apertura");                .HasConstraintName("fk_cajas_usuario_apertura");                .HasConstraintName("fk_cajas_usuario_apertura");                .HasConstraintName("fk_cajas_usuario_apertura");
            e.HasOne(x => x.UsuarioCierre)            e.HasOne(x => x.UsuarioCierre)            e.HasOne(x => x.UsuarioCierre)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdUsuarioCierre)                .HasForeignKey(x => x.IdUsuarioCierre)                .HasForeignKey(x => x.IdUsuarioCierre)                .HasForeignKey(x => x.IdUsuarioCierre)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cajas_usuario_cierre");                .HasConstraintName("fk_cajas_usuario_cierre");                .HasConstraintName("fk_cajas_usuario_cierre");                .HasConstraintName("fk_cajas_usuario_cierre");
        });        });

        modelBuilder.Entity<CajaMovimiento>(e =>        modelBuilder.Entity<CajaMovimiento>(e =>
        {        {
            e.ToTable("caja_movimientos", t =>            e.ToTable("caja_movimientos", t =>            e.ToTable("caja_movimientos", t =>
            {            {            {
                t.HasCheckConstraint("chk_caja_movimientos_monto", "monto > 0");                t.HasCheckConstraint("chk_caja_movimientos_monto", "monto > 0");                t.HasCheckConstraint("chk_caja_movimientos_monto", "monto > 0");                t.HasCheckConstraint("chk_caja_movimientos_monto", "monto > 0");
            });            });            });
            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id_movimiento_caja");            e.Property(x => x.Id).HasColumnName("id_movimiento_caja");            e.Property(x => x.Id).HasColumnName("id_movimiento_caja");
            e.Property(x => x.IdCaja).HasColumnName("id_caja");            e.Property(x => x.IdCaja).HasColumnName("id_caja");            e.Property(x => x.IdCaja).HasColumnName("id_caja");
            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");            e.Property(x => x.Fecha).HasColumnName("fecha").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.Property(x => x.Tipo)            e.Property(x => x.Tipo)            e.Property(x => x.Tipo)
                .HasColumnName("tipo")                .HasColumnName("tipo")                .HasColumnName("tipo")                .HasColumnName("tipo")
                .HasColumnType("enum('ingreso', 'egreso')")                .HasColumnType("enum('ingreso', 'egreso')")                .HasColumnType("enum('ingreso', 'egreso')")                .HasColumnType("enum('ingreso', 'egreso')")
                .IsRequired();                .IsRequired();                .IsRequired();                .IsRequired();
            e.Property(x => x.Monto).HasColumnName("monto").HasPrecision(12, 2);            e.Property(x => x.Monto).HasColumnName("monto").HasPrecision(12, 2);            e.Property(x => x.Monto).HasColumnName("monto").HasPrecision(12, 2);
            e.Property(x => x.Concepto).HasColumnName("concepto").HasMaxLength(150).IsRequired();            e.Property(x => x.Concepto).HasColumnName("concepto").HasMaxLength(150).IsRequired();            e.Property(x => x.Concepto).HasColumnName("concepto").HasMaxLength(150).IsRequired();
            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
            e.HasIndex(x => new { x.IdCaja, x.Fecha }).HasDatabaseName("ix_caja_movimientos_caja");            e.HasIndex(x => new { x.IdCaja, x.Fecha }).HasDatabaseName("ix_caja_movimientos_caja");            e.HasIndex(x => new { x.IdCaja, x.Fecha }).HasDatabaseName("ix_caja_movimientos_caja");
            e.HasOne(x => x.Caja)            e.HasOne(x => x.Caja)            e.HasOne(x => x.Caja)
                .WithMany(x => x.Movimientos)                .WithMany(x => x.Movimientos)                .WithMany(x => x.Movimientos)                .WithMany(x => x.Movimientos)
                .HasForeignKey(x => x.IdCaja)                .HasForeignKey(x => x.IdCaja)                .HasForeignKey(x => x.IdCaja)                .HasForeignKey(x => x.IdCaja)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_caja_movimientos_caja");                .HasConstraintName("fk_caja_movimientos_caja");                .HasConstraintName("fk_caja_movimientos_caja");                .HasConstraintName("fk_caja_movimientos_caja");
            e.HasOne(x => x.Usuario)            e.HasOne(x => x.Usuario)            e.HasOne(x => x.Usuario)
                .WithMany()                .WithMany()                .WithMany()                .WithMany()
                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)                .HasForeignKey(x => x.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_caja_movimientos_usuario");                .HasConstraintName("fk_caja_movimientos_usuario");                .HasConstraintName("fk_caja_movimientos_usuario");                .HasConstraintName("fk_caja_movimientos_usuario");
        });        });
    }
}
```

## `TFI_1.EFCore/Entidades/AtencionesAuditoria.cs`

```csharp
using System;

namespace TFI_1.EFCore.Entidades;

public class Atencion
{
    public int Id { get; set; }
    public int IdCliente { get; set; }
    public Cliente Cliente { get; set; } = null!;
    public int IdEmpleado { get; set; }
    public Empleado Empleado { get; set; } = null!;
    public int IdItem { get; set; }
    public Item Servicio { get; set; } = null!;
    public TipoAtencion Tipo { get; set; }
    public DateTime FechaHora { get; set; }
    public EstadoAtencion Estado { get; set; } = EstadoAtencion.Pendiente;

    public string Fecha => FechaHora.ToString("dd/MM/yyyy");
    public string Hora => FechaHora.ToString("HH:mm");
    public string ClienteNombre => Cliente?.NombreCompleto ?? string.Empty;
    public string EmpleadoNombre => Empleado?.NombreCompleto ?? string.Empty;
    public string ServicioNombre => Servicio?.Nombre ?? string.Empty;
    public string TipoNombre => Tipo == TipoAtencion.Turno ? "Turno" : "Orden de llegada";
    public string EstadoNombre => Estado switch
    {
        EstadoAtencion.Pendiente => "Pendiente",        EstadoAtencion.Pendiente => "Pendiente",
        EstadoAtencion.EnAtencion => "En atención",        EstadoAtencion.EnAtencion => "En atención",
        EstadoAtencion.Finalizada => "Finalizada",        EstadoAtencion.Finalizada => "Finalizada",
        _ => "Cancelada",        _ => "Cancelada",
    };
}

public class Auditoria
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public string Accion { get; set; } = string.Empty;
    public string Entidad { get; set; } = string.Empty;
    public int? IdEntidad { get; set; }
    public string? Detalle { get; set; }
}
```

## `TFI_1.EFCore/Entidades/Cajas.cs`

```csharp
using System;
using System.Collections.Generic;

namespace TFI_1.EFCore.Entidades;

public class Caja
{
    public int Id { get; set; }
    public int IdUsuarioApertura { get; set; }
    public Usuario UsuarioApertura { get; set; } = null!;
    public DateTime FechaApertura { get; set; }
    public decimal MontoInicial { get; set; }
    public int? IdUsuarioCierre { get; set; }
    public Usuario? UsuarioCierre { get; set; }
    public DateTime? FechaCierre { get; set; }
    public decimal? MontoEsperado { get; set; }
    public decimal? MontoContado { get; set; }
    public decimal? Diferencia { get; set; }
    public string? Observacion { get; set; }
    public string Estado { get; set; } = "abierta";

    public bool Abierta => Estado == "abierta";
    public List<CajaMovimiento> Movimientos { get; set; } = new();
}

public class CajaMovimiento
{
    public int Id { get; set; }
    public int IdCaja { get; set; }
    public Caja Caja { get; set; } = null!;
    public DateTime Fecha { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public string Concepto { get; set; } = string.Empty;
    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; } = null!;
}
```

## `TFI_1.EFCore/Entidades/Catalogo.cs`

```csharp
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
```

## `TFI_1.EFCore/Entidades/CuentaCorriente.cs`

```csharp
using System;
using System.Collections.Generic;

namespace TFI_1.EFCore.Entidades;

public class CuentaCorriente
{
    public int Id { get; set; }
    public int IdCliente { get; set; }
    public Cliente Cliente { get; set; } = null!;
    public decimal Saldo { get; set; }
    public DateTime FechaActualizacion { get; set; } = DateTime.Now;

    public List<MovimientoCuentaCorriente> Movimientos { get; set; } = new();
}

public class MovimientoCuentaCorriente
{
    public int Id { get; set; }
    public int IdCuenta { get; set; }
    public CuentaCorriente Cuenta { get; set; } = null!;
    public DateTime Fecha { get; set; } = DateTime.Now;
    public TipoMovimientoCuentaCorriente Tipo { get; set; }
    public decimal Monto { get; set; }
    public string? Concepto { get; set; }
    public int? IdVenta { get; set; }
    public Venta? Venta { get; set; }
    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public string TipoTexto => Tipo == TipoMovimientoCuentaCorriente.Cargo ? "Cargo" : "Abono";
}
```

## `TFI_1.EFCore/Entidades/Enumeraciones.cs`

```csharp
namespace TFI_1.EFCore.Entidades;

public enum TipoItem
{
    Servicio,
    Producto,
}

public enum Modalidad
{
    Turnos,
    OrdenLlegada,
}

public enum TipoAtencion
{
    Turno,
    Llegada,
}

public enum EstadoAtencion
{
    Pendiente,
    EnAtencion,
    Finalizada,
    Cancelada,
}

public enum TipoMovimientoCuentaCorriente
{
    Cargo,
    Abono,
}
```

## `TFI_1.EFCore/Entidades/GastosIngresos.cs`

```csharp
using System;
using System.Collections.Generic;

namespace TFI_1.EFCore.Entidades;

public class Gasto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Concepto { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public string? Categoria { get; set; }
    public string? Observacion { get; set; }
    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public DateTime FechaAlta { get; set; }

    public string? NombreUsuario => Usuario?.NombreUsuario;
}

public class IngresoMercaderia
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public int? IdProveedor { get; set; }
    public Proveedor? ProveedorEntidad { get; set; }
    public string? Proveedor { get; set; }
    public decimal Total { get; set; }
    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public string? Observacion { get; set; }

    public List<IngresoMercaderiaDetalle> Detalles { get; set; } = new();

    public string? NombreUsuario => Usuario?.NombreUsuario;
}

public class IngresoMercaderiaDetalle
{
    public int Id { get; set; }
    public int IdIngreso { get; set; }
    public IngresoMercaderia Ingreso { get; set; } = null!;
    public int IdItem { get; set; }
    public Item Item { get; set; } = null!;
    public int Cantidad { get; set; }
    public decimal CostoUnitario { get; set; }
    public decimal Subtotal { get; set; }
}
```

## `TFI_1.EFCore/Entidades/Personas.cs`

```csharp
using System.Collections.Generic;

namespace TFI_1.EFCore.Entidades;

public class Rol
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class Usuario
{
    public int Id { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public int IdRol { get; set; }
    public Rol Rol { get; set; } = null!;
    public bool Activo { get; set; } = true;

    public string RolNombre => Rol?.Nombre ?? string.Empty;
}

public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Dni { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public bool Activo { get; set; } = true;

    public string NombreCompleto => $"{Apellido}, {Nombre}";
    public bool EsConsumidorFinal => Dni == "00000000";
}

public class Empleado
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public Modalidad Modalidad { get; set; }
    public bool Activo { get; set; } = true;

    public string NombreCompleto => $"{Apellido}, {Nombre}";
    public string ModalidadTexto => Modalidad == Modalidad.Turnos ? "Turnos" : "Orden de llegada";
}
```

## `TFI_1.EFCore/Entidades/Ventas.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace TFI_1.EFCore.Entidades;

public class Venta
{
    public int Id { get; set; }
    public int IdCliente { get; set; }
    public Cliente Cliente { get; set; } = null!;
    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public int? IdEmpleado { get; set; }
    public Empleado? Empleado { get; set; }
    public DateTime Fecha { get; set; }
    public string MedioPago { get; set; } = string.Empty;
    public int? IdTarjetaCredito { get; set; }
    public TarjetaCredito? TarjetaCredito { get; set; }
    public int? CantidadCuotas { get; set; }
    public decimal Descuento { get; set; }
    public decimal Recargo { get; set; }
    public decimal Total { get; set; }
    public bool Anulada { get; set; }
    public DateTime? FechaAnulacion { get; set; }
    public int? IdUsuarioAnulacion { get; set; }
    public Usuario? UsuarioAnulacion { get; set; }
    public string? MotivoAnulacion { get; set; }

    public List<DetalleVenta> Detalles { get; set; } = new();

    public decimal Subtotal => Detalles.Sum(d => d.Subtotal);
}

public class DetalleVenta
{
    public int Id { get; set; }
    public int IdVenta { get; set; }
    public Venta Venta { get; set; } = null!;
    public int IdItem { get; set; }
    public Item Item { get; set; } = null!;
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal Subtotal { get; set; }
}
```

## `TFI_1.EFCore/Dtos/ReportesDtos.cs`

```csharp
using System;

namespace TFI_1.EFCore.Dtos;

public class ResumenDto
{
    public decimal Total { get; set; }
    public int Cantidad { get; set; }
    public decimal TicketPromedio { get; set; }
}

public class VentasPorMedioPagoDto
{
    public string Pago { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal Total { get; set; }
}

public class ItemVendidoDto
{
    public string Item { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal Total { get; set; }
}

public class VentasPorDiaDto
{
    public DateTime Fecha { get; set; }
    public int Cantidad { get; set; }
    public decimal Total { get; set; }
}

public class ClientesComprasDto
{
    public string Cliente { get; set; } = string.Empty;
    public string DNI { get; set; } = string.Empty;
    public int Compras { get; set; }
    public decimal Total { get; set; }
}

public class VentasPorUsuarioDto
{
    public string Usuario { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal Total { get; set; }
}

public class ServiciosSolicitadosDto
{
    public string Servicio { get; set; } = string.Empty;
    public int Veces { get; set; }
    public int Cantidad { get; set; }
    public decimal Total { get; set; }
}

public class VentasPorEmpleadoDto
{
    public string Empleado { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal Total { get; set; }
}
```

## `TFI_1.EFCore/Dtos/VariosDtos.cs`

```csharp
using System;

namespace TFI_1.EFCore.Dtos;

public class CajaListadoDto
{
    public int Id { get; set; }
    public DateTime Apertura { get; set; }
    public string UsuarioApertura { get; set; } = string.Empty;
    public decimal Inicial { get; set; }
    public DateTime? Cierre { get; set; }
    public string? UsuarioCierre { get; set; }
    public decimal? Esperado { get; set; }
    public decimal? Contado { get; set; }
    public decimal? Diferencia { get; set; }
    public string Estado { get; set; } = string.Empty;
    public string? Observacion { get; set; }
}

public class CajaMovimientoDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Concepto { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public string Usuario { get; set; } = string.Empty;
}

public class ResumenVentasDto
{
    public string MedioPago { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal Total { get; set; }
}

public class MovimientoCuentaCorrienteDto
{
    public DateTime Fecha { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public string? Concepto { get; set; }
    public int? IdVenta { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
}

public class IngresoResumenDto
{
    public DateTime Fecha { get; set; }
    public int Cantidad { get; set; }
    public decimal Total { get; set; }
}

public class GastoCategoriaDto
{
    public string Categoria { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal Total { get; set; }
}

public class ProductoStockDto
{
    public string Producto { get; set; } = string.Empty;
    public int? Stock { get; set; }
}

public class UltimaVentaDto
{
    public int Venta { get; set; }
    public DateTime Fecha { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string? Empleado { get; set; }
    public string Usuario { get; set; } = string.Empty;
    public string Pago { get; set; } = string.Empty;
    public decimal Total { get; set; }
}

public class AuditoriaListadoDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Usuario { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public string Accion { get; set; } = string.Empty;
    public string Entidad { get; set; } = string.Empty;
    public int? IdEntidad { get; set; }
    public string? Detalle { get; set; }
}

public class AccionAuditoriaDto
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
}
```

## `TFI_1.EFCore/Dtos/VentasDtos.cs`

```csharp
using System;

namespace TFI_1.EFCore.Dtos;

public class VentaListadoDto
{
    public int Venta { get; set; }
    public DateTime Fecha { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string? Empleado { get; set; }
    public string Pago { get; set; } = string.Empty;
    public string? Tarjeta { get; set; }
    public int? Cuotas { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Descuento { get; set; }
    public decimal Recargo { get; set; }
    public decimal Total { get; set; }
    public bool Anulada { get; set; }
}

public class VentaCabeceraDto
{
    public int Venta { get; set; }
    public DateTime Fecha { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string? Empleado { get; set; }
    public string Pago { get; set; } = string.Empty;
    public int? IdTarjeta { get; set; }
    public string? Banco { get; set; }
    public string? TipoTarjeta { get; set; }
    public int? Cuotas { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Descuento { get; set; }
    public bool Anulada { get; set; }
    public DateTime? FechaAnulacion { get; set; }
    public string? MotivoAnulacion { get; set; }
    public string? UsuarioAnulacion { get; set; }
    public decimal Recargo { get; set; }
    public decimal Total { get; set; }
}

public class VentaDetalleDto
{
    public string Item { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal Subtotal { get; set; }
}
```

## `TFI_1.EFCore/Repositorios/AtencionRepositorio.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class AtencionRepositorio
{
    public List<Atencion> Listar(string? filtro = null, string? estado = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        IQueryable<Atencion> consulta = db.Atenciones.AsNoTracking()        IQueryable<Atencion> consulta = db.Atenciones.AsNoTracking()
            .Include(a => a.Cliente)            .Include(a => a.Cliente)            .Include(a => a.Cliente)
            .Include(a => a.Empleado)            .Include(a => a.Empleado)            .Include(a => a.Empleado)
            .Include(a => a.Servicio);            .Include(a => a.Servicio);            .Include(a => a.Servicio);

        if (!string.IsNullOrEmpty(filtro))        if (!string.IsNullOrEmpty(filtro))
            consulta = consulta.Where(a =>            consulta = consulta.Where(a =>            consulta = consulta.Where(a =>
                a.Cliente.Nombre.Contains(filtro) ||                a.Cliente.Nombre.Contains(filtro) ||                a.Cliente.Nombre.Contains(filtro) ||                a.Cliente.Nombre.Contains(filtro) ||
                a.Cliente.Apellido.Contains(filtro) ||                a.Cliente.Apellido.Contains(filtro) ||                a.Cliente.Apellido.Contains(filtro) ||                a.Cliente.Apellido.Contains(filtro) ||
                a.Servicio.Nombre.Contains(filtro));                a.Servicio.Nombre.Contains(filtro));                a.Servicio.Nombre.Contains(filtro));                a.Servicio.Nombre.Contains(filtro));

        if (!string.IsNullOrWhiteSpace(estado))        if (!string.IsNullOrWhiteSpace(estado))
        {        {
            EstadoAtencion valor = estado switch            EstadoAtencion valor = estado switch            EstadoAtencion valor = estado switch
            {            {            {
                "en_atencion" => EstadoAtencion.EnAtencion,                "en_atencion" => EstadoAtencion.EnAtencion,                "en_atencion" => EstadoAtencion.EnAtencion,                "en_atencion" => EstadoAtencion.EnAtencion,
                "finalizada" => EstadoAtencion.Finalizada,                "finalizada" => EstadoAtencion.Finalizada,                "finalizada" => EstadoAtencion.Finalizada,                "finalizada" => EstadoAtencion.Finalizada,
                "cancelada" => EstadoAtencion.Cancelada,                "cancelada" => EstadoAtencion.Cancelada,                "cancelada" => EstadoAtencion.Cancelada,                "cancelada" => EstadoAtencion.Cancelada,
                _ => EstadoAtencion.Pendiente,                _ => EstadoAtencion.Pendiente,                _ => EstadoAtencion.Pendiente,                _ => EstadoAtencion.Pendiente,
            };            };            };
            consulta = consulta.Where(a => a.Estado == valor);            consulta = consulta.Where(a => a.Estado == valor);            consulta = consulta.Where(a => a.Estado == valor);
        }        }

        return consulta.OrderByDescending(a => a.FechaHora).ToList();        return consulta.OrderByDescending(a => a.FechaHora).ToList();
    }

    public int Insertar(Atencion atencion)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        var nueva = new Atencion        var nueva = new Atencion
        {        {
            IdCliente = atencion.IdCliente != 0 ? atencion.IdCliente : atencion.Cliente.Id,            IdCliente = atencion.IdCliente != 0 ? atencion.IdCliente : atencion.Cliente.Id,            IdCliente = atencion.IdCliente != 0 ? atencion.IdCliente : atencion.Cliente.Id,
            IdEmpleado = atencion.IdEmpleado != 0 ? atencion.IdEmpleado : atencion.Empleado.Id,            IdEmpleado = atencion.IdEmpleado != 0 ? atencion.IdEmpleado : atencion.Empleado.Id,            IdEmpleado = atencion.IdEmpleado != 0 ? atencion.IdEmpleado : atencion.Empleado.Id,
            IdItem = atencion.IdItem != 0 ? atencion.IdItem : atencion.Servicio.Id,            IdItem = atencion.IdItem != 0 ? atencion.IdItem : atencion.Servicio.Id,            IdItem = atencion.IdItem != 0 ? atencion.IdItem : atencion.Servicio.Id,
            Tipo = atencion.Tipo,            Tipo = atencion.Tipo,            Tipo = atencion.Tipo,
            FechaHora = atencion.FechaHora,            FechaHora = atencion.FechaHora,            FechaHora = atencion.FechaHora,
            Estado = atencion.Estado,            Estado = atencion.Estado,            Estado = atencion.Estado,
        };        };

        db.Atenciones.Add(nueva);        db.Atenciones.Add(nueva);
        db.SaveChanges();        db.SaveChanges();
        atencion.Id = nueva.Id;        atencion.Id = nueva.Id;
        return nueva.Id;        return nueva.Id;
    }

    public void Actualizar(Atencion atencion)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        Atencion actual = db.Atenciones.Find(atencion.Id)        Atencion actual = db.Atenciones.Find(atencion.Id)
            ?? throw new InvalidOperationException("La atención no existe.");            ?? throw new InvalidOperationException("La atención no existe.");            ?? throw new InvalidOperationException("La atención no existe.");

        actual.IdCliente = atencion.IdCliente != 0 ? atencion.IdCliente : atencion.Cliente.Id;        actual.IdCliente = atencion.IdCliente != 0 ? atencion.IdCliente : atencion.Cliente.Id;
        actual.IdEmpleado = atencion.IdEmpleado != 0 ? atencion.IdEmpleado : atencion.Empleado.Id;        actual.IdEmpleado = atencion.IdEmpleado != 0 ? atencion.IdEmpleado : atencion.Empleado.Id;
        actual.IdItem = atencion.IdItem != 0 ? atencion.IdItem : atencion.Servicio.Id;        actual.IdItem = atencion.IdItem != 0 ? atencion.IdItem : atencion.Servicio.Id;
        actual.Tipo = atencion.Tipo;        actual.Tipo = atencion.Tipo;
        actual.FechaHora = atencion.FechaHora;        actual.FechaHora = atencion.FechaHora;
        actual.Estado = atencion.Estado;        actual.Estado = atencion.Estado;
        db.SaveChanges();        db.SaveChanges();
    }

    public void CambiarEstado(int id, EstadoAtencion estado)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        db.Atenciones.Where(a => a.Id == id).ExecuteUpdate(s => s.SetProperty(a => a.Estado, estado));        db.Atenciones.Where(a => a.Id == id).ExecuteUpdate(s => s.SetProperty(a => a.Estado, estado));
    }

    public bool ExisteConflictoHorario(int idEmpleado, DateTime fechaHora, int duracionMin, int? exceptoId = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        DateTime fin = fechaHora.AddMinutes(Math.Max(duracionMin, 1));        DateTime fin = fechaHora.AddMinutes(Math.Max(duracionMin, 1));

        return db.Atenciones.Any(a =>        return db.Atenciones.Any(a =>
            a.IdEmpleado == idEmpleado &&            a.IdEmpleado == idEmpleado &&            a.IdEmpleado == idEmpleado &&
            a.Estado != EstadoAtencion.Cancelada &&            a.Estado != EstadoAtencion.Cancelada &&            a.Estado != EstadoAtencion.Cancelada &&
            (exceptoId == null || a.Id != exceptoId) &&            (exceptoId == null || a.Id != exceptoId) &&            (exceptoId == null || a.Id != exceptoId) &&
            fechaHora < a.FechaHora.AddMinutes(Math.Max(a.Servicio.DuracionMin ?? 0, 1)) &&            fechaHora < a.FechaHora.AddMinutes(Math.Max(a.Servicio.DuracionMin ?? 0, 1)) &&            fechaHora < a.FechaHora.AddMinutes(Math.Max(a.Servicio.DuracionMin ?? 0, 1)) &&
            a.FechaHora < fin);            a.FechaHora < fin);            a.FechaHora < fin);
    }

    public int CantidadHoy()
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        DateTime inicio = DateTime.Today;        DateTime inicio = DateTime.Today;
        DateTime fin = inicio.AddDays(1);        DateTime fin = inicio.AddDays(1);

        return db.Atenciones.Count(a =>        return db.Atenciones.Count(a =>
            a.FechaHora >= inicio &&            a.FechaHora >= inicio &&            a.FechaHora >= inicio &&
            a.FechaHora < fin &&            a.FechaHora < fin &&            a.FechaHora < fin &&
            a.Estado != EstadoAtencion.Cancelada);            a.Estado != EstadoAtencion.Cancelada);            a.Estado != EstadoAtencion.Cancelada);
    }
}
```

## `TFI_1.EFCore/Repositorios/AuditoriaRepositorio.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Dtos;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class AuditoriaRepositorio
{
    public void Registrar(int usuarioId, string accion, string entidad, int? idEntidad, string? detalle)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        db.Auditorias.Add(new Auditoria        db.Auditorias.Add(new Auditoria
        {        {
            IdUsuario = usuarioId,            IdUsuario = usuarioId,            IdUsuario = usuarioId,
            Accion = accion,            Accion = accion,            Accion = accion,
            Entidad = entidad,            Entidad = entidad,            Entidad = entidad,
            IdEntidad = idEntidad,            IdEntidad = idEntidad,            IdEntidad = idEntidad,
            Detalle = string.IsNullOrWhiteSpace(detalle) ? null : detalle,            Detalle = string.IsNullOrWhiteSpace(detalle) ? null : detalle,            Detalle = string.IsNullOrWhiteSpace(detalle) ? null : detalle,
        });        });
        db.SaveChanges();        db.SaveChanges();
    }

    public List<AuditoriaListadoDto> Listar(DateTime desde, DateTime hasta, int? usuarioId,
        string? accion, int? limite = null, int? desplazamiento = null)        string? accion, int? limite = null, int? desplazamiento = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

        IQueryable<Auditoria> consulta = db.Auditorias.AsNoTracking()        IQueryable<Auditoria> consulta = db.Auditorias.AsNoTracking()
            .Where(a => a.Fecha >= inicio && a.Fecha < fin);            .Where(a => a.Fecha >= inicio && a.Fecha < fin);            .Where(a => a.Fecha >= inicio && a.Fecha < fin);

        if (usuarioId.HasValue)        if (usuarioId.HasValue)
            consulta = consulta.Where(a => a.IdUsuario == usuarioId.Value);            consulta = consulta.Where(a => a.IdUsuario == usuarioId.Value);            consulta = consulta.Where(a => a.IdUsuario == usuarioId.Value);
        if (!string.IsNullOrWhiteSpace(accion))        if (!string.IsNullOrWhiteSpace(accion))
            consulta = consulta.Where(a => a.Accion == accion);            consulta = consulta.Where(a => a.Accion == accion);            consulta = consulta.Where(a => a.Accion == accion);

        IQueryable<AuditoriaListadoDto> proyeccion = consulta        IQueryable<AuditoriaListadoDto> proyeccion = consulta
            .OrderByDescending(a => a.Fecha)            .OrderByDescending(a => a.Fecha)            .OrderByDescending(a => a.Fecha)
            .ThenByDescending(a => a.Id)            .ThenByDescending(a => a.Id)            .ThenByDescending(a => a.Id)
            .Select(a => new AuditoriaListadoDto            .Select(a => new AuditoriaListadoDto            .Select(a => new AuditoriaListadoDto
            {            {            {
                Id = a.Id,                Id = a.Id,                Id = a.Id,                Id = a.Id,
                Fecha = a.Fecha,                Fecha = a.Fecha,                Fecha = a.Fecha,                Fecha = a.Fecha,
                Usuario = a.Usuario.NombreUsuario,                Usuario = a.Usuario.NombreUsuario,                Usuario = a.Usuario.NombreUsuario,                Usuario = a.Usuario.NombreUsuario,
                Codigo = a.Accion,                Codigo = a.Accion,                Codigo = a.Accion,                Codigo = a.Accion,
                Accion = string.Empty,                Accion = string.Empty,                Accion = string.Empty,                Accion = string.Empty,
                Entidad = a.Entidad,                Entidad = a.Entidad,                Entidad = a.Entidad,                Entidad = a.Entidad,
                IdEntidad = a.IdEntidad,                IdEntidad = a.IdEntidad,                IdEntidad = a.IdEntidad,                IdEntidad = a.IdEntidad,
                Detalle = a.Detalle,                Detalle = a.Detalle,                Detalle = a.Detalle,                Detalle = a.Detalle,
            });            });            });

        List<AuditoriaListadoDto> filas = Comun.Paginar(proyeccion, limite, desplazamiento).ToList();        List<AuditoriaListadoDto> filas = Comun.Paginar(proyeccion, limite, desplazamiento).ToList();
        foreach (AuditoriaListadoDto fila in filas)        foreach (AuditoriaListadoDto fila in filas)
            fila.Accion = TraducirAccion(fila.Codigo);            fila.Accion = TraducirAccion(fila.Codigo);            fila.Accion = TraducirAccion(fila.Codigo);

        return filas;        return filas;
    }

    public int Contar(DateTime desde, DateTime hasta, int? usuarioId, string? accion)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

        IQueryable<Auditoria> consulta = db.Auditorias.AsNoTracking()        IQueryable<Auditoria> consulta = db.Auditorias.AsNoTracking()
            .Where(a => a.Fecha >= inicio && a.Fecha < fin);            .Where(a => a.Fecha >= inicio && a.Fecha < fin);            .Where(a => a.Fecha >= inicio && a.Fecha < fin);

        if (usuarioId.HasValue)        if (usuarioId.HasValue)
            consulta = consulta.Where(a => a.IdUsuario == usuarioId.Value);            consulta = consulta.Where(a => a.IdUsuario == usuarioId.Value);            consulta = consulta.Where(a => a.IdUsuario == usuarioId.Value);
        if (!string.IsNullOrWhiteSpace(accion))        if (!string.IsNullOrWhiteSpace(accion))
            consulta = consulta.Where(a => a.Accion == accion);            consulta = consulta.Where(a => a.Accion == accion);            consulta = consulta.Where(a => a.Accion == accion);

        return consulta.Count();        return consulta.Count();
    }

    public List<AccionAuditoriaDto> Acciones()
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Auditorias.AsNoTracking()        return db.Auditorias.AsNoTracking()
            .Select(a => a.Accion)            .Select(a => a.Accion)            .Select(a => a.Accion)
            .Distinct()            .Distinct()            .Distinct()
            .ToList()            .ToList()            .ToList()
            .Select(codigo => new AccionAuditoriaDto            .Select(codigo => new AccionAuditoriaDto            .Select(codigo => new AccionAuditoriaDto
            {            {            {
                Codigo = codigo,                Codigo = codigo,                Codigo = codigo,                Codigo = codigo,
                Nombre = TraducirAccion(codigo),                Nombre = TraducirAccion(codigo),                Nombre = TraducirAccion(codigo),                Nombre = TraducirAccion(codigo),
            })            })            })
            .OrderBy(a => a.Nombre)            .OrderBy(a => a.Nombre)            .OrderBy(a => a.Nombre)
            .ToList();            .ToList();            .ToList();
    }

    private static string TraducirAccion(string accion) => accion switch
    {
        "venta" => "Venta",        "venta" => "Venta",
        "anulacion_venta" => "Anulación de venta",        "anulacion_venta" => "Anulación de venta",
        "abono" => "Pago de cuenta corriente",        "abono" => "Pago de cuenta corriente",
        "gasto_alta" => "Alta de gasto",        "gasto_alta" => "Alta de gasto",
        "gasto_edicion" => "Edición de gasto",        "gasto_edicion" => "Edición de gasto",
        "ingreso_mercaderia" => "Ingreso de mercadería",        "ingreso_mercaderia" => "Ingreso de mercadería",
        "usuario_alta" => "Alta de usuario",        "usuario_alta" => "Alta de usuario",
        "usuario_edicion" => "Edición de usuario",        "usuario_edicion" => "Edición de usuario",
        "usuario_desactivacion" => "Desactivación de usuario",        "usuario_desactivacion" => "Desactivación de usuario",
        "usuario_reactivacion" => "Reactivación de usuario",        "usuario_reactivacion" => "Reactivación de usuario",
        "caja_apertura" => "Apertura de caja",        "caja_apertura" => "Apertura de caja",
        "caja_cierre" => "Cierre de caja",        "caja_cierre" => "Cierre de caja",
        "caja_ingreso" => "Ingreso de caja",        "caja_ingreso" => "Ingreso de caja",
        "caja_egreso" => "Egreso de caja",        "caja_egreso" => "Egreso de caja",
        "backup_generado" => "Copia de seguridad generada",        "backup_generado" => "Copia de seguridad generada",
        _ => accion,        _ => accion,
    };
}
```

## `TFI_1.EFCore/Repositorios/BancoRepositorio.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class BancoRepositorio
{
    public List<Banco> Listar(bool soloActivos = true)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        IQueryable<Banco> consulta = db.Bancos.AsNoTracking();        IQueryable<Banco> consulta = db.Bancos.AsNoTracking();

        if (soloActivos)        if (soloActivos)
            consulta = consulta.Where(b => b.Activo);            consulta = consulta.Where(b => b.Activo);            consulta = consulta.Where(b => b.Activo);

        return consulta.OrderBy(b => b.Nombre).ToList();        return consulta.OrderBy(b => b.Nombre).ToList();
    }

    public bool ExisteNombre(string nombre, int? exceptoId = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Bancos.Any(b =>        return db.Bancos.Any(b =>
            b.Nombre == nombre &&            b.Nombre == nombre &&            b.Nombre == nombre &&
            (exceptoId == null || b.Id != exceptoId));            (exceptoId == null || b.Id != exceptoId));            (exceptoId == null || b.Id != exceptoId));
    }

    public int Insertar(Banco banco)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        var nuevo = new Banco { Nombre = banco.Nombre, Activo = banco.Activo };        var nuevo = new Banco { Nombre = banco.Nombre, Activo = banco.Activo };
        db.Bancos.Add(nuevo);        db.Bancos.Add(nuevo);
        db.SaveChanges();        db.SaveChanges();
        banco.Id = nuevo.Id;        banco.Id = nuevo.Id;
        return nuevo.Id;        return nuevo.Id;
    }

    public void Actualizar(Banco banco)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        Banco actual = db.Bancos.Find(banco.Id)        Banco actual = db.Bancos.Find(banco.Id)
            ?? throw new InvalidOperationException("El banco no existe.");            ?? throw new InvalidOperationException("El banco no existe.");            ?? throw new InvalidOperationException("El banco no existe.");

        actual.Nombre = banco.Nombre;        actual.Nombre = banco.Nombre;
        actual.Activo = banco.Activo;        actual.Activo = banco.Activo;
        db.SaveChanges();        db.SaveChanges();
    }

    public void Desactivar(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        using var tx = db.Database.BeginTransaction();        using var tx = db.Database.BeginTransaction();
        db.Bancos.Where(b => b.Id == id).ExecuteUpdate(s => s.SetProperty(b => b.Activo, false));        db.Bancos.Where(b => b.Id == id).ExecuteUpdate(s => s.SetProperty(b => b.Activo, false));
        db.TarjetasCredito.Where(t => t.IdBanco == id).ExecuteUpdate(s => s.SetProperty(t => t.Activo, false));        db.TarjetasCredito.Where(t => t.IdBanco == id).ExecuteUpdate(s => s.SetProperty(t => t.Activo, false));
        tx.Commit();        tx.Commit();
    }

    public void Reactivar(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        using var tx = db.Database.BeginTransaction();        using var tx = db.Database.BeginTransaction();
        db.Bancos.Where(b => b.Id == id).ExecuteUpdate(s => s.SetProperty(b => b.Activo, true));        db.Bancos.Where(b => b.Id == id).ExecuteUpdate(s => s.SetProperty(b => b.Activo, true));
        db.TarjetasCredito.Where(t => t.IdBanco == id).ExecuteUpdate(s => s.SetProperty(t => t.Activo, true));        db.TarjetasCredito.Where(t => t.IdBanco == id).ExecuteUpdate(s => s.SetProperty(t => t.Activo, true));
        tx.Commit();        tx.Commit();
    }
}
```

## `TFI_1.EFCore/Repositorios/CajaRepositorio.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Dtos;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class CajaRepositorio
{
    /// <summary>Abre una caja solo si no hay otra abierta; devuelve 0 si ya había una.</summary>
    public int Abrir(int usuarioId, decimal montoInicial)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        using var tx = db.Database.BeginTransaction(IsolationLevel.Serializable);        using var tx = db.Database.BeginTransaction(IsolationLevel.Serializable);

        if (db.Cajas.Any(c => c.Estado == "abierta"))        if (db.Cajas.Any(c => c.Estado == "abierta"))
            return 0;            return 0;            return 0;

        var caja = new Caja        var caja = new Caja
        {        {
            IdUsuarioApertura = usuarioId,            IdUsuarioApertura = usuarioId,            IdUsuarioApertura = usuarioId,
            FechaApertura = DateTime.Now,            FechaApertura = DateTime.Now,            FechaApertura = DateTime.Now,
            MontoInicial = montoInicial,            MontoInicial = montoInicial,            MontoInicial = montoInicial,
            Estado = "abierta",            Estado = "abierta",            Estado = "abierta",
        };        };

        db.Cajas.Add(caja);        db.Cajas.Add(caja);
        db.SaveChanges();        db.SaveChanges();
        tx.Commit();        tx.Commit();
        return caja.Id;        return caja.Id;
    }

    public Caja? ObtenerAbierta()
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Cajas.AsNoTracking()        return db.Cajas.AsNoTracking()
            .Include(c => c.UsuarioApertura)            .Include(c => c.UsuarioApertura)            .Include(c => c.UsuarioApertura)
            .Include(c => c.UsuarioCierre)            .Include(c => c.UsuarioCierre)            .Include(c => c.UsuarioCierre)
            .Where(c => c.Estado == "abierta")            .Where(c => c.Estado == "abierta")            .Where(c => c.Estado == "abierta")
            .OrderByDescending(c => c.Id)            .OrderByDescending(c => c.Id)            .OrderByDescending(c => c.Id)
            .FirstOrDefault();            .FirstOrDefault();            .FirstOrDefault();
    }

    public Caja? ObtenerPorId(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Cajas.AsNoTracking()        return db.Cajas.AsNoTracking()
            .Include(c => c.UsuarioApertura)            .Include(c => c.UsuarioApertura)            .Include(c => c.UsuarioApertura)
            .Include(c => c.UsuarioCierre)            .Include(c => c.UsuarioCierre)            .Include(c => c.UsuarioCierre)
            .SingleOrDefault(c => c.Id == id);            .SingleOrDefault(c => c.Id == id);            .SingleOrDefault(c => c.Id == id);
    }

    public bool Cerrar(int idCaja, int usuarioId, decimal montoEsperado, decimal montoContado, string? observacion)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        int filas = db.Cajas        int filas = db.Cajas
            .Where(c => c.Id == idCaja && c.Estado == "abierta")            .Where(c => c.Id == idCaja && c.Estado == "abierta")            .Where(c => c.Id == idCaja && c.Estado == "abierta")
            .ExecuteUpdate(s => s            .ExecuteUpdate(s => s            .ExecuteUpdate(s => s
                .SetProperty(c => c.IdUsuarioCierre, (int?)usuarioId)                .SetProperty(c => c.IdUsuarioCierre, (int?)usuarioId)                .SetProperty(c => c.IdUsuarioCierre, (int?)usuarioId)                .SetProperty(c => c.IdUsuarioCierre, (int?)usuarioId)
                .SetProperty(c => c.FechaCierre, (DateTime?)DateTime.Now)                .SetProperty(c => c.FechaCierre, (DateTime?)DateTime.Now)                .SetProperty(c => c.FechaCierre, (DateTime?)DateTime.Now)                .SetProperty(c => c.FechaCierre, (DateTime?)DateTime.Now)
                .SetProperty(c => c.MontoEsperado, (decimal?)montoEsperado)                .SetProperty(c => c.MontoEsperado, (decimal?)montoEsperado)                .SetProperty(c => c.MontoEsperado, (decimal?)montoEsperado)                .SetProperty(c => c.MontoEsperado, (decimal?)montoEsperado)
                .SetProperty(c => c.MontoContado, (decimal?)montoContado)                .SetProperty(c => c.MontoContado, (decimal?)montoContado)                .SetProperty(c => c.MontoContado, (decimal?)montoContado)                .SetProperty(c => c.MontoContado, (decimal?)montoContado)
                .SetProperty(c => c.Diferencia, (decimal?)(montoContado - montoEsperado))                .SetProperty(c => c.Diferencia, (decimal?)(montoContado - montoEsperado))                .SetProperty(c => c.Diferencia, (decimal?)(montoContado - montoEsperado))                .SetProperty(c => c.Diferencia, (decimal?)(montoContado - montoEsperado))
                .SetProperty(c => c.Observacion, Comun.Limpiar(observacion))                .SetProperty(c => c.Observacion, Comun.Limpiar(observacion))                .SetProperty(c => c.Observacion, Comun.Limpiar(observacion))                .SetProperty(c => c.Observacion, Comun.Limpiar(observacion))
                .SetProperty(c => c.Estado, "cerrada"));                .SetProperty(c => c.Estado, "cerrada"));                .SetProperty(c => c.Estado, "cerrada"));                .SetProperty(c => c.Estado, "cerrada"));

        return filas == 1;        return filas == 1;
    }

    public int RegistrarMovimiento(int idCaja, string tipo, decimal monto, string concepto, int usuarioId)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        var movimiento = new CajaMovimiento        var movimiento = new CajaMovimiento
        {        {
            IdCaja = idCaja,            IdCaja = idCaja,            IdCaja = idCaja,
            Fecha = DateTime.Now,            Fecha = DateTime.Now,            Fecha = DateTime.Now,
            Tipo = tipo,            Tipo = tipo,            Tipo = tipo,
            Monto = monto,            Monto = monto,            Monto = monto,
            Concepto = concepto,            Concepto = concepto,            Concepto = concepto,
            IdUsuario = usuarioId,            IdUsuario = usuarioId,            IdUsuario = usuarioId,
        };        };

        db.CajaMovimientos.Add(movimiento);        db.CajaMovimientos.Add(movimiento);
        db.SaveChanges();        db.SaveChanges();
        return movimiento.Id;        return movimiento.Id;
    }

    public List<CajaMovimientoDto> ListarMovimientos(int idCaja)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.CajaMovimientos.AsNoTracking()        return db.CajaMovimientos.AsNoTracking()
            .Where(m => m.IdCaja == idCaja)            .Where(m => m.IdCaja == idCaja)            .Where(m => m.IdCaja == idCaja)
            .OrderBy(m => m.Fecha)            .OrderBy(m => m.Fecha)            .OrderBy(m => m.Fecha)
            .ThenBy(m => m.Id)            .ThenBy(m => m.Id)            .ThenBy(m => m.Id)
            .Select(m => new CajaMovimientoDto            .Select(m => new CajaMovimientoDto            .Select(m => new CajaMovimientoDto
            {            {            {
                Id = m.Id,                Id = m.Id,                Id = m.Id,                Id = m.Id,
                Fecha = m.Fecha,                Fecha = m.Fecha,                Fecha = m.Fecha,                Fecha = m.Fecha,
                Tipo = m.Tipo,                Tipo = m.Tipo,                Tipo = m.Tipo,                Tipo = m.Tipo,
                Concepto = m.Concepto,                Concepto = m.Concepto,                Concepto = m.Concepto,                Concepto = m.Concepto,
                Monto = m.Monto,                Monto = m.Monto,                Monto = m.Monto,                Monto = m.Monto,
                Usuario = m.Usuario.NombreUsuario,                Usuario = m.Usuario.NombreUsuario,                Usuario = m.Usuario.NombreUsuario,                Usuario = m.Usuario.NombreUsuario,
            })            })            })
            .ToList();            .ToList();            .ToList();
    }

    public List<ResumenVentasDto> ResumenVentas(DateTime desde, DateTime? hasta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        IQueryable<Venta> consulta = db.Ventas.AsNoTracking()        IQueryable<Venta> consulta = db.Ventas.AsNoTracking()
            .Where(v => v.Fecha >= desde && !v.Anulada);            .Where(v => v.Fecha >= desde && !v.Anulada);            .Where(v => v.Fecha >= desde && !v.Anulada);

        if (hasta.HasValue)        if (hasta.HasValue)
            consulta = consulta.Where(v => v.Fecha <= hasta.Value);            consulta = consulta.Where(v => v.Fecha <= hasta.Value);            consulta = consulta.Where(v => v.Fecha <= hasta.Value);

        return consulta        return consulta
            .GroupBy(v => v.MedioPago)            .GroupBy(v => v.MedioPago)            .GroupBy(v => v.MedioPago)
            .Select(g => new ResumenVentasDto            .Select(g => new ResumenVentasDto            .Select(g => new ResumenVentasDto
            {            {            {
                MedioPago = g.Key,                MedioPago = g.Key,                MedioPago = g.Key,                MedioPago = g.Key,
                Cantidad = g.Count(),                Cantidad = g.Count(),                Cantidad = g.Count(),                Cantidad = g.Count(),
                Total = g.Sum(v => v.Total),                Total = g.Sum(v => v.Total),                Total = g.Sum(v => v.Total),                Total = g.Sum(v => v.Total),
            })            })            })
            .OrderBy(x => x.MedioPago)            .OrderBy(x => x.MedioPago)            .OrderBy(x => x.MedioPago)
            .ToList();            .ToList();            .ToList();
    }

    public decimal VentasEfectivo(DateTime desde, DateTime? hasta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        IQueryable<Venta> consulta = db.Ventas.AsNoTracking()        IQueryable<Venta> consulta = db.Ventas.AsNoTracking()
            .Where(v => v.Fecha >= desde && !v.Anulada && v.MedioPago == "efectivo");            .Where(v => v.Fecha >= desde && !v.Anulada && v.MedioPago == "efectivo");            .Where(v => v.Fecha >= desde && !v.Anulada && v.MedioPago == "efectivo");

        if (hasta.HasValue)        if (hasta.HasValue)
            consulta = consulta.Where(v => v.Fecha <= hasta.Value);            consulta = consulta.Where(v => v.Fecha <= hasta.Value);            consulta = consulta.Where(v => v.Fecha <= hasta.Value);

        return consulta.Sum(v => (decimal?)v.Total) ?? 0m;        return consulta.Sum(v => (decimal?)v.Total) ?? 0m;
    }

    public decimal TotalMovimientos(int idCaja, string tipo)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.CajaMovimientos.AsNoTracking()        return db.CajaMovimientos.AsNoTracking()
            .Where(m => m.IdCaja == idCaja && m.Tipo == tipo)            .Where(m => m.IdCaja == idCaja && m.Tipo == tipo)            .Where(m => m.IdCaja == idCaja && m.Tipo == tipo)
            .Sum(m => (decimal?)m.Monto) ?? 0m;            .Sum(m => (decimal?)m.Monto) ?? 0m;            .Sum(m => (decimal?)m.Monto) ?? 0m;
    }

    public List<CajaListadoDto> Listar(DateTime desde, DateTime hasta, int? limite = null, int? desplazamiento = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

        IQueryable<CajaListadoDto> consulta = db.Cajas.AsNoTracking()        IQueryable<CajaListadoDto> consulta = db.Cajas.AsNoTracking()
            .Where(c => c.FechaApertura >= inicio && c.FechaApertura < fin)            .Where(c => c.FechaApertura >= inicio && c.FechaApertura < fin)            .Where(c => c.FechaApertura >= inicio && c.FechaApertura < fin)
            .OrderByDescending(c => c.FechaApertura)            .OrderByDescending(c => c.FechaApertura)            .OrderByDescending(c => c.FechaApertura)
            .ThenByDescending(c => c.Id)            .ThenByDescending(c => c.Id)            .ThenByDescending(c => c.Id)
            .Select(c => new CajaListadoDto            .Select(c => new CajaListadoDto            .Select(c => new CajaListadoDto
            {            {            {
                Id = c.Id,                Id = c.Id,                Id = c.Id,                Id = c.Id,
                Apertura = c.FechaApertura,                Apertura = c.FechaApertura,                Apertura = c.FechaApertura,                Apertura = c.FechaApertura,
                UsuarioApertura = c.UsuarioApertura.NombreUsuario,                UsuarioApertura = c.UsuarioApertura.NombreUsuario,                UsuarioApertura = c.UsuarioApertura.NombreUsuario,                UsuarioApertura = c.UsuarioApertura.NombreUsuario,
                Inicial = c.MontoInicial,                Inicial = c.MontoInicial,                Inicial = c.MontoInicial,                Inicial = c.MontoInicial,
                Cierre = c.FechaCierre,                Cierre = c.FechaCierre,                Cierre = c.FechaCierre,                Cierre = c.FechaCierre,
                UsuarioCierre = c.UsuarioCierre == null ? null : c.UsuarioCierre.NombreUsuario,                UsuarioCierre = c.UsuarioCierre == null ? null : c.UsuarioCierre.NombreUsuario,                UsuarioCierre = c.UsuarioCierre == null ? null : c.UsuarioCierre.NombreUsuario,                UsuarioCierre = c.UsuarioCierre == null ? null : c.UsuarioCierre.NombreUsuario,
                Esperado = c.MontoEsperado,                Esperado = c.MontoEsperado,                Esperado = c.MontoEsperado,                Esperado = c.MontoEsperado,
                Contado = c.MontoContado,                Contado = c.MontoContado,                Contado = c.MontoContado,                Contado = c.MontoContado,
                Diferencia = c.Diferencia,                Diferencia = c.Diferencia,                Diferencia = c.Diferencia,                Diferencia = c.Diferencia,
                Estado = c.Estado,                Estado = c.Estado,                Estado = c.Estado,                Estado = c.Estado,
                Observacion = c.Observacion,                Observacion = c.Observacion,                Observacion = c.Observacion,                Observacion = c.Observacion,
            });            });            });

        return Comun.Paginar(consulta, limite, desplazamiento).ToList();        return Comun.Paginar(consulta, limite, desplazamiento).ToList();
    }

    public int Contar(DateTime desde, DateTime hasta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);
        return db.Cajas.Count(c => c.FechaApertura >= inicio && c.FechaApertura < fin);        return db.Cajas.Count(c => c.FechaApertura >= inicio && c.FechaApertura < fin);
    }
}
```

## `TFI_1.EFCore/Repositorios/CategoriaRepositorio.cs`

```csharp
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class CategoriaRepositorio
{
    public List<Categoria> Listar(string? filtro = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        IQueryable<Categoria> consulta = db.Categorias.AsNoTracking();        IQueryable<Categoria> consulta = db.Categorias.AsNoTracking();

        if (!string.IsNullOrEmpty(filtro))        if (!string.IsNullOrEmpty(filtro))
            consulta = consulta.Where(c => c.Nombre.Contains(filtro));            consulta = consulta.Where(c => c.Nombre.Contains(filtro));            consulta = consulta.Where(c => c.Nombre.Contains(filtro));

        return consulta.OrderBy(c => c.Nombre).ToList();        return consulta.OrderBy(c => c.Nombre).ToList();
    }

    public bool ExisteNombre(string nombre, int? exceptoId = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Categorias.Any(c =>        return db.Categorias.Any(c =>
            c.Nombre == nombre &&            c.Nombre == nombre &&            c.Nombre == nombre &&
            (exceptoId == null || c.Id != exceptoId));            (exceptoId == null || c.Id != exceptoId));            (exceptoId == null || c.Id != exceptoId));
    }

    public int Insertar(Categoria categoria)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        var nueva = new Categoria { Nombre = categoria.Nombre, Activo = categoria.Activo };        var nueva = new Categoria { Nombre = categoria.Nombre, Activo = categoria.Activo };
        db.Categorias.Add(nueva);        db.Categorias.Add(nueva);
        db.SaveChanges();        db.SaveChanges();
        categoria.Id = nueva.Id;        categoria.Id = nueva.Id;
        return nueva.Id;        return nueva.Id;
    }

    public void Actualizar(Categoria categoria)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        Categoria actual = db.Categorias.Find(categoria.Id)        Categoria actual = db.Categorias.Find(categoria.Id)
            ?? throw new InvalidOperationException("La categoría no existe.");            ?? throw new InvalidOperationException("La categoría no existe.");            ?? throw new InvalidOperationException("La categoría no existe.");

        actual.Nombre = categoria.Nombre;        actual.Nombre = categoria.Nombre;
        actual.Activo = categoria.Activo;        actual.Activo = categoria.Activo;
        db.SaveChanges();        db.SaveChanges();
    }

    public void Desactivar(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        db.Categorias.Where(c => c.Id == id).ExecuteUpdate(s => s.SetProperty(c => c.Activo, false));        db.Categorias.Where(c => c.Id == id).ExecuteUpdate(s => s.SetProperty(c => c.Activo, false));
    }

    public void Reactivar(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        db.Categorias.Where(c => c.Id == id).ExecuteUpdate(s => s.SetProperty(c => c.Activo, true));        db.Categorias.Where(c => c.Id == id).ExecuteUpdate(s => s.SetProperty(c => c.Activo, true));
    }
}
```

## `TFI_1.EFCore/Repositorios/ClienteRepositorio.cs`

```csharp
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class ClienteRepositorio
{
    public List<Cliente> Listar(string? filtro = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        IQueryable<Cliente> consulta = db.Clientes.AsNoTracking();        IQueryable<Cliente> consulta = db.Clientes.AsNoTracking();

        if (!string.IsNullOrEmpty(filtro))        if (!string.IsNullOrEmpty(filtro))
            consulta = consulta.Where(c =>            consulta = consulta.Where(c =>            consulta = consulta.Where(c =>
                c.Dni.Contains(filtro) ||                c.Dni.Contains(filtro) ||                c.Dni.Contains(filtro) ||                c.Dni.Contains(filtro) ||
                c.Apellido.Contains(filtro) ||                c.Apellido.Contains(filtro) ||                c.Apellido.Contains(filtro) ||                c.Apellido.Contains(filtro) ||
                c.Nombre.Contains(filtro));                c.Nombre.Contains(filtro));                c.Nombre.Contains(filtro));                c.Nombre.Contains(filtro));

        return consulta.OrderBy(c => c.Apellido).ThenBy(c => c.Nombre).ToList();        return consulta.OrderBy(c => c.Apellido).ThenBy(c => c.Nombre).ToList();
    }

    public bool ExisteDni(string dni, int? exceptoId = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Clientes.Any(c =>        return db.Clientes.Any(c =>
            c.Dni == dni &&            c.Dni == dni &&            c.Dni == dni &&
            (exceptoId == null || c.Id != exceptoId));            (exceptoId == null || c.Id != exceptoId));            (exceptoId == null || c.Id != exceptoId));
    }

    public Cliente? ObtenerPorDni(string dni)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Clientes.AsNoTracking().SingleOrDefault(c => c.Dni == dni);        return db.Clientes.AsNoTracking().SingleOrDefault(c => c.Dni == dni);
    }

    public int Insertar(Cliente cliente)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        var nuevo = new Cliente        var nuevo = new Cliente
        {        {
            Nombre = cliente.Nombre,            Nombre = cliente.Nombre,            Nombre = cliente.Nombre,
            Apellido = cliente.Apellido,            Apellido = cliente.Apellido,            Apellido = cliente.Apellido,
            Dni = cliente.Dni,            Dni = cliente.Dni,            Dni = cliente.Dni,
            Telefono = Comun.Limpiar(cliente.Telefono),            Telefono = Comun.Limpiar(cliente.Telefono),            Telefono = Comun.Limpiar(cliente.Telefono),
            Activo = cliente.Activo,            Activo = cliente.Activo,            Activo = cliente.Activo,
        };        };

        db.Clientes.Add(nuevo);        db.Clientes.Add(nuevo);
        db.SaveChanges();        db.SaveChanges();
        cliente.Id = nuevo.Id;        cliente.Id = nuevo.Id;
        return nuevo.Id;        return nuevo.Id;
    }

    public void Actualizar(Cliente cliente)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        Cliente actual = db.Clientes.Find(cliente.Id)        Cliente actual = db.Clientes.Find(cliente.Id)
            ?? throw new InvalidOperationException("El cliente no existe.");            ?? throw new InvalidOperationException("El cliente no existe.");            ?? throw new InvalidOperationException("El cliente no existe.");

        actual.Nombre = cliente.Nombre;        actual.Nombre = cliente.Nombre;
        actual.Apellido = cliente.Apellido;        actual.Apellido = cliente.Apellido;
        actual.Dni = cliente.Dni;        actual.Dni = cliente.Dni;
        actual.Telefono = Comun.Limpiar(cliente.Telefono);        actual.Telefono = Comun.Limpiar(cliente.Telefono);
        actual.Activo = cliente.Activo;        actual.Activo = cliente.Activo;
        db.SaveChanges();        db.SaveChanges();
    }

    public void Desactivar(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        db.Clientes.Where(c => c.Id == id).ExecuteUpdate(s => s.SetProperty(c => c.Activo, false));        db.Clientes.Where(c => c.Id == id).ExecuteUpdate(s => s.SetProperty(c => c.Activo, false));
    }

    public void Reactivar(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        db.Clientes.Where(c => c.Id == id).ExecuteUpdate(s => s.SetProperty(c => c.Activo, true));        db.Clientes.Where(c => c.Id == id).ExecuteUpdate(s => s.SetProperty(c => c.Activo, true));
    }
}
```

## `TFI_1.EFCore/Repositorios/Comun.cs`

```csharp
using System;
using System.Linq;

namespace TFI_1.EFCore.Repositorios;

internal static class Comun
{
    /// <summary>Rango equivalente a DATE(columna) BETWEEN desde AND hasta.</summary>
    public static (DateTime Inicio, DateTime Fin) Rango(DateTime desde, DateTime hasta) =>
        (desde.Date, hasta.Date.AddDays(1));        (desde.Date, hasta.Date.AddDays(1));

    public static IQueryable<T> Paginar<T>(IQueryable<T> consulta, int? limite, int? desplazamiento)
    {
        if (desplazamiento is > 0)        if (desplazamiento is > 0)
            consulta = consulta.Skip(desplazamiento.Value);            consulta = consulta.Skip(desplazamiento.Value);            consulta = consulta.Skip(desplazamiento.Value);

        if (limite is > 0 and < int.MaxValue)        if (limite is > 0 and < int.MaxValue)
            consulta = consulta.Take(limite.Value);            consulta = consulta.Take(limite.Value);            consulta = consulta.Take(limite.Value);

        return consulta;        return consulta;
    }

    public static string? Limpiar(string? valor) =>
        string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();        string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
}
```

## `TFI_1.EFCore/Repositorios/CuentaCorrienteRepositorio.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Dtos;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class CuentaCorrienteRepositorio
{
    public CuentaCorriente? ObtenerPorCliente(int idCliente)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.CuentasCorrientes.AsNoTracking().SingleOrDefault(c => c.IdCliente == idCliente);        return db.CuentasCorrientes.AsNoTracking().SingleOrDefault(c => c.IdCliente == idCliente);
    }

    public int CrearSiNoExiste(int idCliente)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        CuentaCorriente? existente = db.CuentasCorrientes.AsNoTracking()        CuentaCorriente? existente = db.CuentasCorrientes.AsNoTracking()
            .SingleOrDefault(c => c.IdCliente == idCliente);            .SingleOrDefault(c => c.IdCliente == idCliente);            .SingleOrDefault(c => c.IdCliente == idCliente);

        if (existente != null)        if (existente != null)
            return existente.Id;            return existente.Id;            return existente.Id;

        var nueva = new CuentaCorriente        var nueva = new CuentaCorriente
        {        {
            IdCliente = idCliente,            IdCliente = idCliente,            IdCliente = idCliente,
            Saldo = 0m,            Saldo = 0m,            Saldo = 0m,
            FechaActualizacion = DateTime.Now,            FechaActualizacion = DateTime.Now,            FechaActualizacion = DateTime.Now,
        };        };
        db.CuentasCorrientes.Add(nueva);        db.CuentasCorrientes.Add(nueva);
        db.SaveChanges();        db.SaveChanges();
        return nueva.Id;        return nueva.Id;
    }

    public decimal ObtenerSaldo(int idCliente)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.CuentasCorrientes        return db.CuentasCorrientes
            .Where(c => c.IdCliente == idCliente)            .Where(c => c.IdCliente == idCliente)            .Where(c => c.IdCliente == idCliente)
            .Select(c => (decimal?)c.Saldo)            .Select(c => (decimal?)c.Saldo)            .Select(c => (decimal?)c.Saldo)
            .SingleOrDefault() ?? 0m;            .SingleOrDefault() ?? 0m;            .SingleOrDefault() ?? 0m;
    }

    public void ActualizarSaldo(int idCuenta, decimal nuevoSaldo)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        db.CuentasCorrientes        db.CuentasCorrientes
            .Where(c => c.Id == idCuenta)            .Where(c => c.Id == idCuenta)            .Where(c => c.Id == idCuenta)
            .ExecuteUpdate(s => s.SetProperty(c => c.Saldo, nuevoSaldo));            .ExecuteUpdate(s => s.SetProperty(c => c.Saldo, nuevoSaldo));            .ExecuteUpdate(s => s.SetProperty(c => c.Saldo, nuevoSaldo));
    }

    public List<MovimientoCuentaCorriente> ListarMovimientos(int idCuenta, DateTime? desde = null, DateTime? hasta = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        IQueryable<MovimientoCuentaCorriente> consulta = db.MovimientosCuentaCorriente        IQueryable<MovimientoCuentaCorriente> consulta = db.MovimientosCuentaCorriente
            .AsNoTracking()            .AsNoTracking()            .AsNoTracking()
            .Where(m => m.IdCuenta == idCuenta);            .Where(m => m.IdCuenta == idCuenta);            .Where(m => m.IdCuenta == idCuenta);

        if (desde.HasValue)        if (desde.HasValue)
            consulta = consulta.Where(m => m.Fecha >= desde.Value.Date);            consulta = consulta.Where(m => m.Fecha >= desde.Value.Date);            consulta = consulta.Where(m => m.Fecha >= desde.Value.Date);
        if (hasta.HasValue)        if (hasta.HasValue)
            consulta = consulta.Where(m => m.Fecha < hasta.Value.Date.AddDays(1));            consulta = consulta.Where(m => m.Fecha < hasta.Value.Date.AddDays(1));            consulta = consulta.Where(m => m.Fecha < hasta.Value.Date.AddDays(1));

        return consulta.OrderByDescending(m => m.Fecha).ThenByDescending(m => m.Id).ToList();        return consulta.OrderByDescending(m => m.Fecha).ThenByDescending(m => m.Id).ToList();
    }

    public int InsertarMovimiento(MovimientoCuentaCorriente movimiento)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        var nuevo = new MovimientoCuentaCorriente        var nuevo = new MovimientoCuentaCorriente
        {        {
            IdCuenta = movimiento.IdCuenta,            IdCuenta = movimiento.IdCuenta,            IdCuenta = movimiento.IdCuenta,
            Fecha = movimiento.Fecha,            Fecha = movimiento.Fecha,            Fecha = movimiento.Fecha,
            Tipo = movimiento.Tipo,            Tipo = movimiento.Tipo,            Tipo = movimiento.Tipo,
            Monto = movimiento.Monto,            Monto = movimiento.Monto,            Monto = movimiento.Monto,
            Concepto = movimiento.Concepto,            Concepto = movimiento.Concepto,            Concepto = movimiento.Concepto,
            IdVenta = movimiento.IdVenta,            IdVenta = movimiento.IdVenta,            IdVenta = movimiento.IdVenta,
            IdUsuario = movimiento.IdUsuario,            IdUsuario = movimiento.IdUsuario,            IdUsuario = movimiento.IdUsuario,
        };        };

        db.MovimientosCuentaCorriente.Add(nuevo);        db.MovimientosCuentaCorriente.Add(nuevo);
        db.SaveChanges();        db.SaveChanges();
        movimiento.Id = nuevo.Id;        movimiento.Id = nuevo.Id;
        return nuevo.Id;        return nuevo.Id;
    }

    /// <summary>Registra el abono y descuenta el saldo dentro de una transacción.</summary>
    public int RegistrarAbono(int idCliente, decimal monto, string? concepto, int idUsuario, DateTime fecha)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        using var tx = db.Database.BeginTransaction();        using var tx = db.Database.BeginTransaction();

        try        try
        {        {
            CuentaCorriente? cuenta = db.CuentasCorrientes            CuentaCorriente? cuenta = db.CuentasCorrientes            CuentaCorriente? cuenta = db.CuentasCorrientes
                .FromSqlInterpolated($"SELECT * FROM cuentas_corrientes WHERE id_cliente = {idCliente} FOR UPDATE")                .FromSqlInterpolated($"SELECT * FROM cuentas_corrientes WHERE id_cliente = {idCliente} FOR UPDATE")                .FromSqlInterpolated($"SELECT * FROM cuentas_corrientes WHERE id_cliente = {idCliente} FOR UPDATE")                .FromSqlInterpolated($"SELECT * FROM cuentas_corrientes WHERE id_cliente = {idCliente} FOR UPDATE")
                .SingleOrDefault();                .SingleOrDefault();                .SingleOrDefault();                .SingleOrDefault();

            if (cuenta == null)            if (cuenta == null)            if (cuenta == null)
            {            {            {
                cuenta = new CuentaCorriente                cuenta = new CuentaCorriente                cuenta = new CuentaCorriente                cuenta = new CuentaCorriente
                {                {                {                {
                    IdCliente = idCliente,                    IdCliente = idCliente,                    IdCliente = idCliente,                    IdCliente = idCliente,                    IdCliente = idCliente,
                    Saldo = 0m,                    Saldo = 0m,                    Saldo = 0m,                    Saldo = 0m,                    Saldo = 0m,
                    FechaActualizacion = DateTime.Now,                    FechaActualizacion = DateTime.Now,                    FechaActualizacion = DateTime.Now,                    FechaActualizacion = DateTime.Now,                    FechaActualizacion = DateTime.Now,
                };                };                };                };
                db.CuentasCorrientes.Add(cuenta);                db.CuentasCorrientes.Add(cuenta);                db.CuentasCorrientes.Add(cuenta);                db.CuentasCorrientes.Add(cuenta);
                db.SaveChanges();                db.SaveChanges();                db.SaveChanges();                db.SaveChanges();
            }            }            }

            var movimiento = new MovimientoCuentaCorriente            var movimiento = new MovimientoCuentaCorriente            var movimiento = new MovimientoCuentaCorriente
            {            {            {
                IdCuenta = cuenta.Id,                IdCuenta = cuenta.Id,                IdCuenta = cuenta.Id,                IdCuenta = cuenta.Id,
                Fecha = fecha,                Fecha = fecha,                Fecha = fecha,                Fecha = fecha,
                Tipo = TipoMovimientoCuentaCorriente.Abono,                Tipo = TipoMovimientoCuentaCorriente.Abono,                Tipo = TipoMovimientoCuentaCorriente.Abono,                Tipo = TipoMovimientoCuentaCorriente.Abono,
                Monto = monto,                Monto = monto,                Monto = monto,                Monto = monto,
                Concepto = concepto,                Concepto = concepto,                Concepto = concepto,                Concepto = concepto,
                IdVenta = null,                IdVenta = null,                IdVenta = null,                IdVenta = null,
                IdUsuario = idUsuario,                IdUsuario = idUsuario,                IdUsuario = idUsuario,                IdUsuario = idUsuario,
            };            };            };

            db.MovimientosCuentaCorriente.Add(movimiento);            db.MovimientosCuentaCorriente.Add(movimiento);            db.MovimientosCuentaCorriente.Add(movimiento);
            cuenta.Saldo -= monto;            cuenta.Saldo -= monto;            cuenta.Saldo -= monto;
            cuenta.FechaActualizacion = DateTime.Now;            cuenta.FechaActualizacion = DateTime.Now;            cuenta.FechaActualizacion = DateTime.Now;
            db.SaveChanges();            db.SaveChanges();            db.SaveChanges();

            tx.Commit();            tx.Commit();            tx.Commit();
            return movimiento.Id;            return movimiento.Id;            return movimiento.Id;
        }        }
        catch        catch
        {        {
            tx.Rollback();            tx.Rollback();            tx.Rollback();
            throw;            throw;            throw;
        }        }
    }

    public List<MovimientoCuentaCorrienteDto> MovimientosResumen(int idCliente, DateTime desde, DateTime hasta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

        return db.MovimientosCuentaCorriente.AsNoTracking()        return db.MovimientosCuentaCorriente.AsNoTracking()
            .Where(m => m.Cuenta.IdCliente == idCliente && m.Fecha >= inicio && m.Fecha < fin)            .Where(m => m.Cuenta.IdCliente == idCliente && m.Fecha >= inicio && m.Fecha < fin)            .Where(m => m.Cuenta.IdCliente == idCliente && m.Fecha >= inicio && m.Fecha < fin)
            .OrderByDescending(m => m.Fecha)            .OrderByDescending(m => m.Fecha)            .OrderByDescending(m => m.Fecha)
            .Select(m => new MovimientoCuentaCorrienteDto            .Select(m => new MovimientoCuentaCorrienteDto            .Select(m => new MovimientoCuentaCorrienteDto
            {            {            {
                Fecha = m.Fecha,                Fecha = m.Fecha,                Fecha = m.Fecha,                Fecha = m.Fecha,
                Tipo = m.Tipo == TipoMovimientoCuentaCorriente.Cargo ? "cargo" : "abono",                Tipo = m.Tipo == TipoMovimientoCuentaCorriente.Cargo ? "cargo" : "abono",                Tipo = m.Tipo == TipoMovimientoCuentaCorriente.Cargo ? "cargo" : "abono",                Tipo = m.Tipo == TipoMovimientoCuentaCorriente.Cargo ? "cargo" : "abono",
                Monto = m.Monto,                Monto = m.Monto,                Monto = m.Monto,                Monto = m.Monto,
                Concepto = m.Concepto,                Concepto = m.Concepto,                Concepto = m.Concepto,                Concepto = m.Concepto,
                IdVenta = m.IdVenta,                IdVenta = m.IdVenta,                IdVenta = m.IdVenta,                IdVenta = m.IdVenta,
                NombreUsuario = m.Usuario.NombreUsuario,                NombreUsuario = m.Usuario.NombreUsuario,                NombreUsuario = m.Usuario.NombreUsuario,                NombreUsuario = m.Usuario.NombreUsuario,
            })            })            })
            .ToList();            .ToList();            .ToList();
    }
}
```

## `TFI_1.EFCore/Repositorios/DashboardRepositorio.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Dtos;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class DashboardRepositorio
{
    public decimal TotalVendidoHoy()
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = (DateTime.Today, DateTime.Today.AddDays(1));        (DateTime inicio, DateTime fin) = (DateTime.Today, DateTime.Today.AddDays(1));
        return db.Ventas.AsNoTracking()        return db.Ventas.AsNoTracking()
            .Where(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada)            .Where(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada)            .Where(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada)
            .Sum(v => (decimal?)v.Total) ?? 0m;            .Sum(v => (decimal?)v.Total) ?? 0m;            .Sum(v => (decimal?)v.Total) ?? 0m;
    }

    public int CantidadVentasHoy()
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = (DateTime.Today, DateTime.Today.AddDays(1));        (DateTime inicio, DateTime fin) = (DateTime.Today, DateTime.Today.AddDays(1));
        return db.Ventas.AsNoTracking().Count(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada);        return db.Ventas.AsNoTracking().Count(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada);
    }

    public int TotalClientes()
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Clientes.AsNoTracking().Count(c => c.Activo);        return db.Clientes.AsNoTracking().Count(c => c.Activo);
    }

    public int TotalItems()
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Items.AsNoTracking().Count(i => i.Activo);        return db.Items.AsNoTracking().Count(i => i.Activo);
    }

    public int ProductosStockBajo()
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Items.AsNoTracking().Count(i =>        return db.Items.AsNoTracking().Count(i =>
            i.Activo && i.Tipo == TipoItem.Producto && i.Stock <= i.StockMinimo);            i.Activo && i.Tipo == TipoItem.Producto && i.Stock <= i.StockMinimo);            i.Activo && i.Tipo == TipoItem.Producto && i.Stock <= i.StockMinimo);
    }

    public List<ProductoStockDto> ProductoMayorStock()
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Items.AsNoTracking()        return db.Items.AsNoTracking()
            .Where(i => i.Activo && i.Tipo == TipoItem.Producto)            .Where(i => i.Activo && i.Tipo == TipoItem.Producto)            .Where(i => i.Activo && i.Tipo == TipoItem.Producto)
            .OrderByDescending(i => i.Stock)            .OrderByDescending(i => i.Stock)            .OrderByDescending(i => i.Stock)
            .Select(i => new ProductoStockDto { Producto = i.Nombre, Stock = i.Stock })            .Select(i => new ProductoStockDto { Producto = i.Nombre, Stock = i.Stock })            .Select(i => new ProductoStockDto { Producto = i.Nombre, Stock = i.Stock })
            .Take(1)            .Take(1)            .Take(1)
            .ToList();            .ToList();            .ToList();
    }

    public List<ProductoStockDto> ProductoMenorStock()
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Items.AsNoTracking()        return db.Items.AsNoTracking()
            .Where(i => i.Activo && i.Tipo == TipoItem.Producto && i.Stock > 0)            .Where(i => i.Activo && i.Tipo == TipoItem.Producto && i.Stock > 0)            .Where(i => i.Activo && i.Tipo == TipoItem.Producto && i.Stock > 0)
            .OrderBy(i => i.Stock)            .OrderBy(i => i.Stock)            .OrderBy(i => i.Stock)
            .Select(i => new ProductoStockDto { Producto = i.Nombre, Stock = i.Stock })            .Select(i => new ProductoStockDto { Producto = i.Nombre, Stock = i.Stock })            .Select(i => new ProductoStockDto { Producto = i.Nombre, Stock = i.Stock })
            .Take(1)            .Take(1)            .Take(1)
            .ToList();            .ToList();            .ToList();
    }

    public List<UltimaVentaDto> UltimasVentas(int cantidad)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Ventas.AsNoTracking()        return db.Ventas.AsNoTracking()
            .Where(v => !v.Anulada)            .Where(v => !v.Anulada)            .Where(v => !v.Anulada)
            .OrderByDescending(v => v.Fecha)            .OrderByDescending(v => v.Fecha)            .OrderByDescending(v => v.Fecha)
            .ThenByDescending(v => v.Id)            .ThenByDescending(v => v.Id)            .ThenByDescending(v => v.Id)
            .Select(v => new UltimaVentaDto            .Select(v => new UltimaVentaDto            .Select(v => new UltimaVentaDto
            {            {            {
                Venta = v.Id,                Venta = v.Id,                Venta = v.Id,                Venta = v.Id,
                Fecha = v.Fecha,                Fecha = v.Fecha,                Fecha = v.Fecha,                Fecha = v.Fecha,
                Cliente = v.Cliente.Apellido + ", " + v.Cliente.Nombre,                Cliente = v.Cliente.Apellido + ", " + v.Cliente.Nombre,                Cliente = v.Cliente.Apellido + ", " + v.Cliente.Nombre,                Cliente = v.Cliente.Apellido + ", " + v.Cliente.Nombre,
                Empleado = v.Empleado == null ? null : v.Empleado.Apellido + ", " + v.Empleado.Nombre,                Empleado = v.Empleado == null ? null : v.Empleado.Apellido + ", " + v.Empleado.Nombre,                Empleado = v.Empleado == null ? null : v.Empleado.Apellido + ", " + v.Empleado.Nombre,                Empleado = v.Empleado == null ? null : v.Empleado.Apellido + ", " + v.Empleado.Nombre,
                Usuario = v.Usuario.NombreUsuario,                Usuario = v.Usuario.NombreUsuario,                Usuario = v.Usuario.NombreUsuario,                Usuario = v.Usuario.NombreUsuario,
                Pago = v.MedioPago,                Pago = v.MedioPago,                Pago = v.MedioPago,                Pago = v.MedioPago,
                Total = v.Total,                Total = v.Total,                Total = v.Total,                Total = v.Total,
            })            })            })
            .Take(cantidad)            .Take(cantidad)            .Take(cantidad)
            .ToList();            .ToList();            .ToList();
    }
}
```

## `TFI_1.EFCore/Repositorios/EmpleadoRepositorio.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class EmpleadoRepositorio
{
    public List<Empleado> Listar(string? filtro = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        IQueryable<Empleado> consulta = db.Empleados.AsNoTracking();        IQueryable<Empleado> consulta = db.Empleados.AsNoTracking();

        if (!string.IsNullOrEmpty(filtro))        if (!string.IsNullOrEmpty(filtro))
            consulta = consulta.Where(e => e.Apellido.Contains(filtro) || e.Nombre.Contains(filtro));            consulta = consulta.Where(e => e.Apellido.Contains(filtro) || e.Nombre.Contains(filtro));            consulta = consulta.Where(e => e.Apellido.Contains(filtro) || e.Nombre.Contains(filtro));

        return consulta.OrderBy(e => e.Apellido).ThenBy(e => e.Nombre).ToList();        return consulta.OrderBy(e => e.Apellido).ThenBy(e => e.Nombre).ToList();
    }

    public int Insertar(Empleado empleado)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        var nuevo = new Empleado        var nuevo = new Empleado
        {        {
            Nombre = empleado.Nombre,            Nombre = empleado.Nombre,            Nombre = empleado.Nombre,
            Apellido = empleado.Apellido,            Apellido = empleado.Apellido,            Apellido = empleado.Apellido,
            Modalidad = empleado.Modalidad,            Modalidad = empleado.Modalidad,            Modalidad = empleado.Modalidad,
            Activo = empleado.Activo,            Activo = empleado.Activo,            Activo = empleado.Activo,
        };        };

        db.Empleados.Add(nuevo);        db.Empleados.Add(nuevo);
        db.SaveChanges();        db.SaveChanges();
        empleado.Id = nuevo.Id;        empleado.Id = nuevo.Id;
        return nuevo.Id;        return nuevo.Id;
    }

    public void Actualizar(Empleado empleado)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        Empleado actual = db.Empleados.Find(empleado.Id)        Empleado actual = db.Empleados.Find(empleado.Id)
            ?? throw new InvalidOperationException("El empleado no existe.");            ?? throw new InvalidOperationException("El empleado no existe.");            ?? throw new InvalidOperationException("El empleado no existe.");

        actual.Nombre = empleado.Nombre;        actual.Nombre = empleado.Nombre;
        actual.Apellido = empleado.Apellido;        actual.Apellido = empleado.Apellido;
        actual.Modalidad = empleado.Modalidad;        actual.Modalidad = empleado.Modalidad;
        actual.Activo = empleado.Activo;        actual.Activo = empleado.Activo;
        db.SaveChanges();        db.SaveChanges();
    }

    public void Desactivar(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        db.Empleados.Where(e => e.Id == id).ExecuteUpdate(s => s.SetProperty(e => e.Activo, false));        db.Empleados.Where(e => e.Id == id).ExecuteUpdate(s => s.SetProperty(e => e.Activo, false));
    }

    public void Reactivar(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        db.Empleados.Where(e => e.Id == id).ExecuteUpdate(s => s.SetProperty(e => e.Activo, true));        db.Empleados.Where(e => e.Id == id).ExecuteUpdate(s => s.SetProperty(e => e.Activo, true));
    }
}
```

## `TFI_1.EFCore/Repositorios/GastoRepositorio.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Dtos;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class GastoRepositorio
{
    public List<Gasto> Listar(DateTime? desde = null, DateTime? hasta = null, string? filtro = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        IQueryable<Gasto> consulta = db.Gastos.AsNoTracking().Include(g => g.Usuario);        IQueryable<Gasto> consulta = db.Gastos.AsNoTracking().Include(g => g.Usuario);

        // Igual que el original: no usa DATE(), compara contra la medianoche.        // Igual que el original: no usa DATE(), compara contra la medianoche.
        if (desde.HasValue)        if (desde.HasValue)
            consulta = consulta.Where(g => g.Fecha >= desde.Value.Date);            consulta = consulta.Where(g => g.Fecha >= desde.Value.Date);            consulta = consulta.Where(g => g.Fecha >= desde.Value.Date);
        if (hasta.HasValue)        if (hasta.HasValue)
            consulta = consulta.Where(g => g.Fecha <= hasta.Value.Date);            consulta = consulta.Where(g => g.Fecha <= hasta.Value.Date);            consulta = consulta.Where(g => g.Fecha <= hasta.Value.Date);

        if (!string.IsNullOrEmpty(filtro))        if (!string.IsNullOrEmpty(filtro))
            consulta = consulta.Where(g =>            consulta = consulta.Where(g =>            consulta = consulta.Where(g =>
                g.Concepto.Contains(filtro) ||                g.Concepto.Contains(filtro) ||                g.Concepto.Contains(filtro) ||                g.Concepto.Contains(filtro) ||
                (g.Categoria ?? string.Empty).Contains(filtro));                (g.Categoria ?? string.Empty).Contains(filtro));                (g.Categoria ?? string.Empty).Contains(filtro));                (g.Categoria ?? string.Empty).Contains(filtro));

        return consulta.OrderByDescending(g => g.Fecha).ThenByDescending(g => g.Id).ToList();        return consulta.OrderByDescending(g => g.Fecha).ThenByDescending(g => g.Id).ToList();
    }

    public Gasto? ObtenerPorId(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Gastos.AsNoTracking()        return db.Gastos.AsNoTracking()
            .Include(g => g.Usuario)            .Include(g => g.Usuario)            .Include(g => g.Usuario)
            .SingleOrDefault(g => g.Id == id);            .SingleOrDefault(g => g.Id == id);            .SingleOrDefault(g => g.Id == id);
    }

    public int Insertar(Gasto gasto)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        var nuevo = new Gasto        var nuevo = new Gasto
        {        {
            Fecha = gasto.Fecha,            Fecha = gasto.Fecha,            Fecha = gasto.Fecha,
            Concepto = gasto.Concepto,            Concepto = gasto.Concepto,            Concepto = gasto.Concepto,
            Monto = gasto.Monto,            Monto = gasto.Monto,            Monto = gasto.Monto,
            Categoria = Comun.Limpiar(gasto.Categoria),            Categoria = Comun.Limpiar(gasto.Categoria),            Categoria = Comun.Limpiar(gasto.Categoria),
            Observacion = Comun.Limpiar(gasto.Observacion),            Observacion = Comun.Limpiar(gasto.Observacion),            Observacion = Comun.Limpiar(gasto.Observacion),
            IdUsuario = gasto.IdUsuario,            IdUsuario = gasto.IdUsuario,            IdUsuario = gasto.IdUsuario,
        };        };

        db.Gastos.Add(nuevo);        db.Gastos.Add(nuevo);
        db.SaveChanges();        db.SaveChanges();
        gasto.Id = nuevo.Id;        gasto.Id = nuevo.Id;
        return nuevo.Id;        return nuevo.Id;
    }

    public void Actualizar(Gasto gasto)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        Gasto actual = db.Gastos.Find(gasto.Id)        Gasto actual = db.Gastos.Find(gasto.Id)
            ?? throw new InvalidOperationException("El gasto no existe.");            ?? throw new InvalidOperationException("El gasto no existe.");            ?? throw new InvalidOperationException("El gasto no existe.");

        actual.Fecha = gasto.Fecha;        actual.Fecha = gasto.Fecha;
        actual.Concepto = gasto.Concepto;        actual.Concepto = gasto.Concepto;
        actual.Monto = gasto.Monto;        actual.Monto = gasto.Monto;
        actual.Categoria = Comun.Limpiar(gasto.Categoria);        actual.Categoria = Comun.Limpiar(gasto.Categoria);
        actual.Observacion = Comun.Limpiar(gasto.Observacion);        actual.Observacion = Comun.Limpiar(gasto.Observacion);
        db.SaveChanges();        db.SaveChanges();
    }

    public decimal TotalGastos(DateTime desde, DateTime hasta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Gastos.AsNoTracking()        return db.Gastos.AsNoTracking()
            .Where(g => g.Fecha >= desde.Date && g.Fecha <= hasta.Date)            .Where(g => g.Fecha >= desde.Date && g.Fecha <= hasta.Date)            .Where(g => g.Fecha >= desde.Date && g.Fecha <= hasta.Date)
            .Sum(g => (decimal?)g.Monto) ?? 0m;            .Sum(g => (decimal?)g.Monto) ?? 0m;            .Sum(g => (decimal?)g.Monto) ?? 0m;
    }

    public List<GastoCategoriaDto> GastosPorCategoria(DateTime desde, DateTime hasta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Gastos.AsNoTracking()        return db.Gastos.AsNoTracking()
            .Where(g => g.Fecha >= desde.Date && g.Fecha <= hasta.Date)            .Where(g => g.Fecha >= desde.Date && g.Fecha <= hasta.Date)            .Where(g => g.Fecha >= desde.Date && g.Fecha <= hasta.Date)
            .GroupBy(g => g.Categoria)            .GroupBy(g => g.Categoria)            .GroupBy(g => g.Categoria)
            .Select(g => new GastoCategoriaDto            .Select(g => new GastoCategoriaDto            .Select(g => new GastoCategoriaDto
            {            {            {
                Categoria = g.Key ?? "Sin categoría",                Categoria = g.Key ?? "Sin categoría",                Categoria = g.Key ?? "Sin categoría",                Categoria = g.Key ?? "Sin categoría",
                Cantidad = g.Count(),                Cantidad = g.Count(),                Cantidad = g.Count(),                Cantidad = g.Count(),
                Total = g.Sum(x => x.Monto),                Total = g.Sum(x => x.Monto),                Total = g.Sum(x => x.Monto),                Total = g.Sum(x => x.Monto),
            })            })            })
            .OrderByDescending(x => x.Total)            .OrderByDescending(x => x.Total)            .OrderByDescending(x => x.Total)
            .ToList();            .ToList();            .ToList();
    }
}
```

## `TFI_1.EFCore/Repositorios/IngresoMercaderiaRepositorio.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Dtos;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class IngresoMercaderiaRepositorio
{
    public List<IngresoMercaderia> Listar(DateTime? desde = null, DateTime? hasta = null,
        string? filtro = null, int? limite = null, int? desplazamiento = null)        string? filtro = null, int? limite = null, int? desplazamiento = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        IQueryable<IngresoMercaderia> consulta = db.IngresosMercaderia        IQueryable<IngresoMercaderia> consulta = db.IngresosMercaderia
            .AsNoTracking()            .AsNoTracking()            .AsNoTracking()
            .Include(i => i.Usuario);            .Include(i => i.Usuario);            .Include(i => i.Usuario);

        if (desde.HasValue)        if (desde.HasValue)
            consulta = consulta.Where(i => i.Fecha >= desde.Value.Date);            consulta = consulta.Where(i => i.Fecha >= desde.Value.Date);            consulta = consulta.Where(i => i.Fecha >= desde.Value.Date);
        if (hasta.HasValue)        if (hasta.HasValue)
            consulta = consulta.Where(i => i.Fecha < hasta.Value.Date.AddDays(1));            consulta = consulta.Where(i => i.Fecha < hasta.Value.Date.AddDays(1));            consulta = consulta.Where(i => i.Fecha < hasta.Value.Date.AddDays(1));

        if (!string.IsNullOrEmpty(filtro))        if (!string.IsNullOrEmpty(filtro))
            consulta = consulta.Where(i => (i.Proveedor ?? string.Empty).Contains(filtro));            consulta = consulta.Where(i => (i.Proveedor ?? string.Empty).Contains(filtro));            consulta = consulta.Where(i => (i.Proveedor ?? string.Empty).Contains(filtro));

        consulta = consulta.OrderByDescending(i => i.Fecha).ThenByDescending(i => i.Id);        consulta = consulta.OrderByDescending(i => i.Fecha).ThenByDescending(i => i.Id);
        return Comun.Paginar(consulta, limite, desplazamiento).ToList();        return Comun.Paginar(consulta, limite, desplazamiento).ToList();
    }

    public int Contar(DateTime? desde = null, DateTime? hasta = null, string? filtro = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return Filtrar(db.IngresosMercaderia.AsNoTracking(), desde, hasta, filtro).Count();        return Filtrar(db.IngresosMercaderia.AsNoTracking(), desde, hasta, filtro).Count();
    }

    public decimal Sumar(DateTime? desde = null, DateTime? hasta = null, string? filtro = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return Filtrar(db.IngresosMercaderia.AsNoTracking(), desde, hasta, filtro)        return Filtrar(db.IngresosMercaderia.AsNoTracking(), desde, hasta, filtro)
            .Sum(i => (decimal?)i.Total) ?? 0m;            .Sum(i => (decimal?)i.Total) ?? 0m;            .Sum(i => (decimal?)i.Total) ?? 0m;
    }

    public IngresoMercaderia? ObtenerPorId(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.IngresosMercaderia.AsNoTracking()        return db.IngresosMercaderia.AsNoTracking()
            .Include(i => i.Usuario)            .Include(i => i.Usuario)            .Include(i => i.Usuario)
            .Include(i => i.Detalles)            .Include(i => i.Detalles)            .Include(i => i.Detalles)
                .ThenInclude(d => d.Item)                .ThenInclude(d => d.Item)                .ThenInclude(d => d.Item)                .ThenInclude(d => d.Item)
            .SingleOrDefault(i => i.Id == id);            .SingleOrDefault(i => i.Id == id);            .SingleOrDefault(i => i.Id == id);
    }

    public int Insertar(IngresoMercaderia ingreso)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        using var tx = db.Database.BeginTransaction();        using var tx = db.Database.BeginTransaction();

        try        try
        {        {
            var nuevo = new IngresoMercaderia            var nuevo = new IngresoMercaderia            var nuevo = new IngresoMercaderia
            {            {            {
                Fecha = ingreso.Fecha,                Fecha = ingreso.Fecha,                Fecha = ingreso.Fecha,                Fecha = ingreso.Fecha,
                IdProveedor = ingreso.IdProveedor,                IdProveedor = ingreso.IdProveedor,                IdProveedor = ingreso.IdProveedor,                IdProveedor = ingreso.IdProveedor,
                Proveedor = ingreso.Proveedor,                Proveedor = ingreso.Proveedor,                Proveedor = ingreso.Proveedor,                Proveedor = ingreso.Proveedor,
                Total = ingreso.Total,                Total = ingreso.Total,                Total = ingreso.Total,                Total = ingreso.Total,
                IdUsuario = ingreso.IdUsuario,                IdUsuario = ingreso.IdUsuario,                IdUsuario = ingreso.IdUsuario,                IdUsuario = ingreso.IdUsuario,
                Observacion = ingreso.Observacion,                Observacion = ingreso.Observacion,                Observacion = ingreso.Observacion,                Observacion = ingreso.Observacion,
            };            };            };

            db.IngresosMercaderia.Add(nuevo);            db.IngresosMercaderia.Add(nuevo);            db.IngresosMercaderia.Add(nuevo);
            db.SaveChanges();            db.SaveChanges();            db.SaveChanges();

            foreach (IngresoMercaderiaDetalle detalle in ingreso.Detalles)            foreach (IngresoMercaderiaDetalle detalle in ingreso.Detalles)            foreach (IngresoMercaderiaDetalle detalle in ingreso.Detalles)
            {            {            {
                int idItem = detalle.IdItem != 0 ? detalle.IdItem : detalle.Item?.Id ?? 0;                int idItem = detalle.IdItem != 0 ? detalle.IdItem : detalle.Item?.Id ?? 0;                int idItem = detalle.IdItem != 0 ? detalle.IdItem : detalle.Item?.Id ?? 0;                int idItem = detalle.IdItem != 0 ? detalle.IdItem : detalle.Item?.Id ?? 0;
                decimal subtotal = detalle.CostoUnitario * detalle.Cantidad;                decimal subtotal = detalle.CostoUnitario * detalle.Cantidad;                decimal subtotal = detalle.CostoUnitario * detalle.Cantidad;                decimal subtotal = detalle.CostoUnitario * detalle.Cantidad;

                db.IngresosMercaderiaDetalle.Add(new IngresoMercaderiaDetalle                db.IngresosMercaderiaDetalle.Add(new IngresoMercaderiaDetalle                db.IngresosMercaderiaDetalle.Add(new IngresoMercaderiaDetalle                db.IngresosMercaderiaDetalle.Add(new IngresoMercaderiaDetalle
                {                {                {                {
                    IdIngreso = nuevo.Id,                    IdIngreso = nuevo.Id,                    IdIngreso = nuevo.Id,                    IdIngreso = nuevo.Id,                    IdIngreso = nuevo.Id,
                    IdItem = idItem,                    IdItem = idItem,                    IdItem = idItem,                    IdItem = idItem,                    IdItem = idItem,
                    Cantidad = detalle.Cantidad,                    Cantidad = detalle.Cantidad,                    Cantidad = detalle.Cantidad,                    Cantidad = detalle.Cantidad,                    Cantidad = detalle.Cantidad,
                    CostoUnitario = detalle.CostoUnitario,                    CostoUnitario = detalle.CostoUnitario,                    CostoUnitario = detalle.CostoUnitario,                    CostoUnitario = detalle.CostoUnitario,                    CostoUnitario = detalle.CostoUnitario,
                    Subtotal = subtotal,                    Subtotal = subtotal,                    Subtotal = subtotal,                    Subtotal = subtotal,                    Subtotal = subtotal,
                });                });                });                });

                Item item = db.Items.Find(idItem)                Item item = db.Items.Find(idItem)                Item item = db.Items.Find(idItem)                Item item = db.Items.Find(idItem)
                    ?? throw new InvalidOperationException("El producto no existe.");                    ?? throw new InvalidOperationException("El producto no existe.");                    ?? throw new InvalidOperationException("El producto no existe.");                    ?? throw new InvalidOperationException("El producto no existe.");                    ?? throw new InvalidOperationException("El producto no existe.");

                item.Stock = (item.Stock ?? 0) + detalle.Cantidad;                item.Stock = (item.Stock ?? 0) + detalle.Cantidad;                item.Stock = (item.Stock ?? 0) + detalle.Cantidad;                item.Stock = (item.Stock ?? 0) + detalle.Cantidad;
                if (detalle.Item != null)                if (detalle.Item != null)                if (detalle.Item != null)                if (detalle.Item != null)
                    item.Precio = detalle.Item.Precio;                    item.Precio = detalle.Item.Precio;                    item.Precio = detalle.Item.Precio;                    item.Precio = detalle.Item.Precio;                    item.Precio = detalle.Item.Precio;
                item.Costo = detalle.CostoUnitario;                item.Costo = detalle.CostoUnitario;                item.Costo = detalle.CostoUnitario;                item.Costo = detalle.CostoUnitario;
            }            }            }

            db.SaveChanges();            db.SaveChanges();            db.SaveChanges();
            tx.Commit();            tx.Commit();            tx.Commit();
            ingreso.Id = nuevo.Id;            ingreso.Id = nuevo.Id;            ingreso.Id = nuevo.Id;
            return nuevo.Id;            return nuevo.Id;            return nuevo.Id;
        }        }
        catch        catch
        {        {
            tx.Rollback();            tx.Rollback();            tx.Rollback();
            throw;            throw;            throw;
        }        }
    }

    public List<IngresoResumenDto> IngresosResumen(DateTime desde, DateTime hasta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

        return db.IngresosMercaderia.AsNoTracking()        return db.IngresosMercaderia.AsNoTracking()
            .Where(i => i.Fecha >= inicio && i.Fecha < fin)            .Where(i => i.Fecha >= inicio && i.Fecha < fin)            .Where(i => i.Fecha >= inicio && i.Fecha < fin)
            .GroupBy(i => i.Fecha.Date)            .GroupBy(i => i.Fecha.Date)            .GroupBy(i => i.Fecha.Date)
            .Select(g => new IngresoResumenDto            .Select(g => new IngresoResumenDto            .Select(g => new IngresoResumenDto
            {            {            {
                Fecha = g.Key,                Fecha = g.Key,                Fecha = g.Key,                Fecha = g.Key,
                Cantidad = g.Count(),                Cantidad = g.Count(),                Cantidad = g.Count(),                Cantidad = g.Count(),
                Total = g.Sum(x => x.Total),                Total = g.Sum(x => x.Total),                Total = g.Sum(x => x.Total),                Total = g.Sum(x => x.Total),
            })            })            })
            .OrderBy(x => x.Fecha)            .OrderBy(x => x.Fecha)            .OrderBy(x => x.Fecha)
            .ToList();            .ToList();            .ToList();
    }

    private static IQueryable<IngresoMercaderia> Filtrar(IQueryable<IngresoMercaderia> consulta,
        DateTime? desde, DateTime? hasta, string? filtro)        DateTime? desde, DateTime? hasta, string? filtro)
    {
        if (desde.HasValue)        if (desde.HasValue)
            consulta = consulta.Where(i => i.Fecha >= desde.Value.Date);            consulta = consulta.Where(i => i.Fecha >= desde.Value.Date);            consulta = consulta.Where(i => i.Fecha >= desde.Value.Date);
        if (hasta.HasValue)        if (hasta.HasValue)
            consulta = consulta.Where(i => i.Fecha < hasta.Value.Date.AddDays(1));            consulta = consulta.Where(i => i.Fecha < hasta.Value.Date.AddDays(1));            consulta = consulta.Where(i => i.Fecha < hasta.Value.Date.AddDays(1));
        if (!string.IsNullOrEmpty(filtro))        if (!string.IsNullOrEmpty(filtro))
            consulta = consulta.Where(i => (i.Proveedor ?? string.Empty).Contains(filtro));            consulta = consulta.Where(i => (i.Proveedor ?? string.Empty).Contains(filtro));            consulta = consulta.Where(i => (i.Proveedor ?? string.Empty).Contains(filtro));

        return consulta;        return consulta;
    }
}
```

## `TFI_1.EFCore/Repositorios/ItemRepositorio.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class ItemRepositorio
{
    public List<Item> Listar(string? filtro = null, int? categoriaId = null, TipoItem? tipo = null,
        bool soloStockBajo = false)        bool soloStockBajo = false)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        IQueryable<Item> consulta = db.Items.AsNoTracking().Include(i => i.Categoria);        IQueryable<Item> consulta = db.Items.AsNoTracking().Include(i => i.Categoria);

        if (!string.IsNullOrEmpty(filtro))        if (!string.IsNullOrEmpty(filtro))
            consulta = consulta.Where(i => i.Nombre.Contains(filtro));            consulta = consulta.Where(i => i.Nombre.Contains(filtro));            consulta = consulta.Where(i => i.Nombre.Contains(filtro));
        if (categoriaId.HasValue)        if (categoriaId.HasValue)
            consulta = consulta.Where(i => i.IdCategoria == categoriaId.Value);            consulta = consulta.Where(i => i.IdCategoria == categoriaId.Value);            consulta = consulta.Where(i => i.IdCategoria == categoriaId.Value);
        if (tipo.HasValue)        if (tipo.HasValue)
            consulta = consulta.Where(i => i.Tipo == tipo.Value);            consulta = consulta.Where(i => i.Tipo == tipo.Value);            consulta = consulta.Where(i => i.Tipo == tipo.Value);
        if (soloStockBajo)        if (soloStockBajo)
            consulta = consulta.Where(i => i.Tipo == TipoItem.Producto && i.Stock <= i.StockMinimo);            consulta = consulta.Where(i => i.Tipo == TipoItem.Producto && i.Stock <= i.StockMinimo);            consulta = consulta.Where(i => i.Tipo == TipoItem.Producto && i.Stock <= i.StockMinimo);

        return consulta.OrderBy(i => i.Nombre).ToList();        return consulta.OrderBy(i => i.Nombre).ToList();
    }

    public Item? ObtenerPorId(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Items        return db.Items
            .AsNoTracking()            .AsNoTracking()            .AsNoTracking()
            .Include(i => i.Categoria)            .Include(i => i.Categoria)            .Include(i => i.Categoria)
            .SingleOrDefault(i => i.Id == id);            .SingleOrDefault(i => i.Id == id);            .SingleOrDefault(i => i.Id == id);
    }

    public bool ExisteNombre(string nombre, int? exceptoId = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Items.Any(i =>        return db.Items.Any(i =>
            i.Nombre == nombre &&            i.Nombre == nombre &&            i.Nombre == nombre &&
            (exceptoId == null || i.Id != exceptoId));            (exceptoId == null || i.Id != exceptoId));            (exceptoId == null || i.Id != exceptoId));
    }

    public int Insertar(Item item)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        var nuevo = new Item        var nuevo = new Item
        {        {
            Nombre = item.Nombre,            Nombre = item.Nombre,            Nombre = item.Nombre,
            Tipo = item.Tipo,            Tipo = item.Tipo,            Tipo = item.Tipo,
            Precio = item.Precio,            Precio = item.Precio,            Precio = item.Precio,
            IdCategoria = item.IdCategoria != 0 ? item.IdCategoria : item.Categoria.Id,            IdCategoria = item.IdCategoria != 0 ? item.IdCategoria : item.Categoria.Id,            IdCategoria = item.IdCategoria != 0 ? item.IdCategoria : item.Categoria.Id,
            Activo = item.Activo,            Activo = item.Activo,            Activo = item.Activo,
            Costo = item.Costo,            Costo = item.Costo,            Costo = item.Costo,
            DuracionMin = item.Tipo == TipoItem.Servicio ? item.DuracionMin : null,            DuracionMin = item.Tipo == TipoItem.Servicio ? item.DuracionMin : null,            DuracionMin = item.Tipo == TipoItem.Servicio ? item.DuracionMin : null,
            Stock = item.Tipo == TipoItem.Producto ? item.Stock : null,            Stock = item.Tipo == TipoItem.Producto ? item.Stock : null,            Stock = item.Tipo == TipoItem.Producto ? item.Stock : null,
            StockMinimo = item.Tipo == TipoItem.Producto ? item.StockMinimo : null,            StockMinimo = item.Tipo == TipoItem.Producto ? item.StockMinimo : null,            StockMinimo = item.Tipo == TipoItem.Producto ? item.StockMinimo : null,
        };        };

        db.Items.Add(nuevo);        db.Items.Add(nuevo);
        db.SaveChanges();        db.SaveChanges();
        item.Id = nuevo.Id;        item.Id = nuevo.Id;
        return nuevo.Id;        return nuevo.Id;
    }

    public void Actualizar(Item item)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        Item actual = db.Items.Find(item.Id)        Item actual = db.Items.Find(item.Id)
            ?? throw new InvalidOperationException("El ítem no existe.");            ?? throw new InvalidOperationException("El ítem no existe.");            ?? throw new InvalidOperationException("El ítem no existe.");

        actual.Nombre = item.Nombre;        actual.Nombre = item.Nombre;
        actual.Tipo = item.Tipo;        actual.Tipo = item.Tipo;
        actual.Precio = item.Precio;        actual.Precio = item.Precio;
        actual.Costo = item.Costo;        actual.Costo = item.Costo;
        actual.IdCategoria = item.IdCategoria != 0 ? item.IdCategoria : item.Categoria.Id;        actual.IdCategoria = item.IdCategoria != 0 ? item.IdCategoria : item.Categoria.Id;
        actual.Activo = item.Activo;        actual.Activo = item.Activo;
        actual.DuracionMin = item.Tipo == TipoItem.Servicio ? item.DuracionMin : null;        actual.DuracionMin = item.Tipo == TipoItem.Servicio ? item.DuracionMin : null;
        actual.Stock = item.Tipo == TipoItem.Producto ? item.Stock : null;        actual.Stock = item.Tipo == TipoItem.Producto ? item.Stock : null;
        actual.StockMinimo = item.Tipo == TipoItem.Producto ? item.StockMinimo : null;        actual.StockMinimo = item.Tipo == TipoItem.Producto ? item.StockMinimo : null;
        db.SaveChanges();        db.SaveChanges();
    }

    public void Desactivar(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        db.Items.Where(i => i.Id == id).ExecuteUpdate(s => s.SetProperty(i => i.Activo, false));        db.Items.Where(i => i.Id == id).ExecuteUpdate(s => s.SetProperty(i => i.Activo, false));
    }

    public void Reactivar(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        db.Items.Where(i => i.Id == id).ExecuteUpdate(s => s.SetProperty(i => i.Activo, true));        db.Items.Where(i => i.Id == id).ExecuteUpdate(s => s.SetProperty(i => i.Activo, true));
    }
}
```

## `TFI_1.EFCore/Repositorios/ProveedorRepositorio.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class ProveedorRepositorio
{
    public List<Proveedor> Listar(string? filtro = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        IQueryable<Proveedor> consulta = db.Proveedores.AsNoTracking();        IQueryable<Proveedor> consulta = db.Proveedores.AsNoTracking();

        if (!string.IsNullOrEmpty(filtro))        if (!string.IsNullOrEmpty(filtro))
            consulta = consulta.Where(p =>            consulta = consulta.Where(p =>            consulta = consulta.Where(p =>
                p.Nombre.Contains(filtro) ||                p.Nombre.Contains(filtro) ||                p.Nombre.Contains(filtro) ||                p.Nombre.Contains(filtro) ||
                (p.Cuit ?? string.Empty).Contains(filtro));                (p.Cuit ?? string.Empty).Contains(filtro));                (p.Cuit ?? string.Empty).Contains(filtro));                (p.Cuit ?? string.Empty).Contains(filtro));

        return consulta.OrderBy(p => p.Nombre).ToList();        return consulta.OrderBy(p => p.Nombre).ToList();
    }

    public Proveedor? ObtenerPorId(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Proveedores.AsNoTracking().SingleOrDefault(p => p.Id == id);        return db.Proveedores.AsNoTracking().SingleOrDefault(p => p.Id == id);
    }

    public bool ExisteNombre(string nombre, int? exceptoId = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Proveedores.Any(p =>        return db.Proveedores.Any(p =>
            p.Nombre == nombre &&            p.Nombre == nombre &&            p.Nombre == nombre &&
            (exceptoId == null || p.Id != exceptoId));            (exceptoId == null || p.Id != exceptoId));            (exceptoId == null || p.Id != exceptoId));
    }

    public bool ExisteCuit(string cuit, int? exceptoId = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Proveedores.Any(p =>        return db.Proveedores.Any(p =>
            p.Cuit == cuit &&            p.Cuit == cuit &&            p.Cuit == cuit &&
            (exceptoId == null || p.Id != exceptoId));            (exceptoId == null || p.Id != exceptoId));            (exceptoId == null || p.Id != exceptoId));
    }

    public int Insertar(Proveedor proveedor)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        var nuevo = new Proveedor        var nuevo = new Proveedor
        {        {
            Nombre = proveedor.Nombre,            Nombre = proveedor.Nombre,            Nombre = proveedor.Nombre,
            Cuit = Comun.Limpiar(proveedor.Cuit),            Cuit = Comun.Limpiar(proveedor.Cuit),            Cuit = Comun.Limpiar(proveedor.Cuit),
            Telefono = Comun.Limpiar(proveedor.Telefono),            Telefono = Comun.Limpiar(proveedor.Telefono),            Telefono = Comun.Limpiar(proveedor.Telefono),
            Email = Comun.Limpiar(proveedor.Email),            Email = Comun.Limpiar(proveedor.Email),            Email = Comun.Limpiar(proveedor.Email),
            Direccion = Comun.Limpiar(proveedor.Direccion),            Direccion = Comun.Limpiar(proveedor.Direccion),            Direccion = Comun.Limpiar(proveedor.Direccion),
            Activo = proveedor.Activo,            Activo = proveedor.Activo,            Activo = proveedor.Activo,
        };        };

        db.Proveedores.Add(nuevo);        db.Proveedores.Add(nuevo);
        db.SaveChanges();        db.SaveChanges();
        proveedor.Id = nuevo.Id;        proveedor.Id = nuevo.Id;
        return nuevo.Id;        return nuevo.Id;
    }

    public void Actualizar(Proveedor proveedor)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        Proveedor actual = db.Proveedores.Find(proveedor.Id)        Proveedor actual = db.Proveedores.Find(proveedor.Id)
            ?? throw new InvalidOperationException("El proveedor no existe.");            ?? throw new InvalidOperationException("El proveedor no existe.");            ?? throw new InvalidOperationException("El proveedor no existe.");

        actual.Nombre = proveedor.Nombre;        actual.Nombre = proveedor.Nombre;
        actual.Cuit = Comun.Limpiar(proveedor.Cuit);        actual.Cuit = Comun.Limpiar(proveedor.Cuit);
        actual.Telefono = Comun.Limpiar(proveedor.Telefono);        actual.Telefono = Comun.Limpiar(proveedor.Telefono);
        actual.Email = Comun.Limpiar(proveedor.Email);        actual.Email = Comun.Limpiar(proveedor.Email);
        actual.Direccion = Comun.Limpiar(proveedor.Direccion);        actual.Direccion = Comun.Limpiar(proveedor.Direccion);
        actual.Activo = proveedor.Activo;        actual.Activo = proveedor.Activo;
        db.SaveChanges();        db.SaveChanges();
    }

    public void Desactivar(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        db.Proveedores.Where(p => p.Id == id).ExecuteUpdate(s => s.SetProperty(p => p.Activo, false));        db.Proveedores.Where(p => p.Id == id).ExecuteUpdate(s => s.SetProperty(p => p.Activo, false));
    }

    public void Reactivar(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        db.Proveedores.Where(p => p.Id == id).ExecuteUpdate(s => s.SetProperty(p => p.Activo, true));        db.Proveedores.Where(p => p.Id == id).ExecuteUpdate(s => s.SetProperty(p => p.Activo, true));
    }
}
```

## `TFI_1.EFCore/Repositorios/ReporteRepositorio.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Dtos;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class ReporteRepositorio
{
    public List<ResumenDto> Resumen(DateTime desde, DateTime hasta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

        ResumenDto? resumen = db.Ventas.AsNoTracking()        ResumenDto? resumen = db.Ventas.AsNoTracking()
            .Where(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada)            .Where(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada)            .Where(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada)
            .GroupBy(_ => 1)            .GroupBy(_ => 1)            .GroupBy(_ => 1)
            .Select(g => new ResumenDto            .Select(g => new ResumenDto            .Select(g => new ResumenDto
            {            {            {
                Total = g.Sum(v => v.Total),                Total = g.Sum(v => v.Total),                Total = g.Sum(v => v.Total),                Total = g.Sum(v => v.Total),
                Cantidad = g.Count(),                Cantidad = g.Count(),                Cantidad = g.Count(),                Cantidad = g.Count(),
                TicketPromedio = g.Average(v => (decimal?)v.Total) ?? 0m,                TicketPromedio = g.Average(v => (decimal?)v.Total) ?? 0m,                TicketPromedio = g.Average(v => (decimal?)v.Total) ?? 0m,                TicketPromedio = g.Average(v => (decimal?)v.Total) ?? 0m,
            })            })            })
            .SingleOrDefault();            .SingleOrDefault();            .SingleOrDefault();

        // El original devuelve una fila con ceros cuando no hay ventas en el período.        // El original devuelve una fila con ceros cuando no hay ventas en el período.
        return new List<ResumenDto> { resumen ?? new ResumenDto() };        return new List<ResumenDto> { resumen ?? new ResumenDto() };
    }

    public List<VentasPorMedioPagoDto> VentasPorMedioPago(DateTime desde, DateTime hasta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

        return db.Ventas.AsNoTracking()        return db.Ventas.AsNoTracking()
            .Where(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada)            .Where(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada)            .Where(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada)
            .GroupBy(v => v.MedioPago)            .GroupBy(v => v.MedioPago)            .GroupBy(v => v.MedioPago)
            .Select(g => new VentasPorMedioPagoDto            .Select(g => new VentasPorMedioPagoDto            .Select(g => new VentasPorMedioPagoDto
            {            {            {
                Pago = g.Key,                Pago = g.Key,                Pago = g.Key,                Pago = g.Key,
                Cantidad = g.Count(),                Cantidad = g.Count(),                Cantidad = g.Count(),                Cantidad = g.Count(),
                Total = g.Sum(v => v.Total),                Total = g.Sum(v => v.Total),                Total = g.Sum(v => v.Total),                Total = g.Sum(v => v.Total),
            })            })            })
            .OrderByDescending(x => x.Total)            .OrderByDescending(x => x.Total)            .OrderByDescending(x => x.Total)
            .ToList();            .ToList();            .ToList();
    }

    public List<ItemVendidoDto> ProductosMasVendidos(DateTime desde, DateTime hasta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

        return (from d in db.DetallesVenta.AsNoTracking()        return (from d in db.DetallesVenta.AsNoTracking()
                join v in db.Ventas on d.IdVenta equals v.Id                join v in db.Ventas on d.IdVenta equals v.Id                join v in db.Ventas on d.IdVenta equals v.Id                join v in db.Ventas on d.IdVenta equals v.Id
                join i in db.Items on d.IdItem equals i.Id                join i in db.Items on d.IdItem equals i.Id                join i in db.Items on d.IdItem equals i.Id                join i in db.Items on d.IdItem equals i.Id
                where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada                where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada                where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada                where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada
                group d by new { i.Id, i.Nombre, i.Tipo } into g                group d by new { i.Id, i.Nombre, i.Tipo } into g                group d by new { i.Id, i.Nombre, i.Tipo } into g                group d by new { i.Id, i.Nombre, i.Tipo } into g
                select new ItemVendidoDto                select new ItemVendidoDto                select new ItemVendidoDto                select new ItemVendidoDto
                {                {                {                {
                    Item = g.Key.Nombre,                    Item = g.Key.Nombre,                    Item = g.Key.Nombre,                    Item = g.Key.Nombre,                    Item = g.Key.Nombre,
                    Tipo = g.Key.Tipo == TipoItem.Producto ? "producto" : "servicio",                    Tipo = g.Key.Tipo == TipoItem.Producto ? "producto" : "servicio",                    Tipo = g.Key.Tipo == TipoItem.Producto ? "producto" : "servicio",                    Tipo = g.Key.Tipo == TipoItem.Producto ? "producto" : "servicio",                    Tipo = g.Key.Tipo == TipoItem.Producto ? "producto" : "servicio",
                    Cantidad = g.Sum(x => x.Cantidad),                    Cantidad = g.Sum(x => x.Cantidad),                    Cantidad = g.Sum(x => x.Cantidad),                    Cantidad = g.Sum(x => x.Cantidad),                    Cantidad = g.Sum(x => x.Cantidad),
                    Total = g.Sum(x => x.Subtotal),                    Total = g.Sum(x => x.Subtotal),                    Total = g.Sum(x => x.Subtotal),                    Total = g.Sum(x => x.Subtotal),                    Total = g.Sum(x => x.Subtotal),
                })                })                })                })
            .OrderByDescending(x => x.Cantidad)            .OrderByDescending(x => x.Cantidad)            .OrderByDescending(x => x.Cantidad)
            .ThenByDescending(x => x.Total)            .ThenByDescending(x => x.Total)            .ThenByDescending(x => x.Total)
            .Take(10)            .Take(10)            .Take(10)
            .ToList();            .ToList();            .ToList();
    }

    public List<VentasPorDiaDto> VentasPorDia(DateTime desde, DateTime hasta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

        return db.Ventas.AsNoTracking()        return db.Ventas.AsNoTracking()
            .Where(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada)            .Where(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada)            .Where(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada)
            .GroupBy(v => v.Fecha.Date)            .GroupBy(v => v.Fecha.Date)            .GroupBy(v => v.Fecha.Date)
            .Select(g => new VentasPorDiaDto            .Select(g => new VentasPorDiaDto            .Select(g => new VentasPorDiaDto
            {            {            {
                Fecha = g.Key,                Fecha = g.Key,                Fecha = g.Key,                Fecha = g.Key,
                Cantidad = g.Count(),                Cantidad = g.Count(),                Cantidad = g.Count(),                Cantidad = g.Count(),
                Total = g.Sum(v => v.Total),                Total = g.Sum(v => v.Total),                Total = g.Sum(v => v.Total),                Total = g.Sum(v => v.Total),
            })            })            })
            .OrderBy(x => x.Fecha)            .OrderBy(x => x.Fecha)            .OrderBy(x => x.Fecha)
            .ToList();            .ToList();            .ToList();
    }

    public List<ClientesComprasDto> ClientesConMasCompras(DateTime desde, DateTime hasta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

        return (from v in db.Ventas.AsNoTracking()        return (from v in db.Ventas.AsNoTracking()
                join c in db.Clientes on v.IdCliente equals c.Id                join c in db.Clientes on v.IdCliente equals c.Id                join c in db.Clientes on v.IdCliente equals c.Id                join c in db.Clientes on v.IdCliente equals c.Id
                where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada                where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada                where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada                where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada
                group v by new { c.Id, c.Apellido, c.Nombre, c.Dni } into g                group v by new { c.Id, c.Apellido, c.Nombre, c.Dni } into g                group v by new { c.Id, c.Apellido, c.Nombre, c.Dni } into g                group v by new { c.Id, c.Apellido, c.Nombre, c.Dni } into g
                select new ClientesComprasDto                select new ClientesComprasDto                select new ClientesComprasDto                select new ClientesComprasDto
                {                {                {                {
                    Cliente = g.Key.Apellido + ", " + g.Key.Nombre,                    Cliente = g.Key.Apellido + ", " + g.Key.Nombre,                    Cliente = g.Key.Apellido + ", " + g.Key.Nombre,                    Cliente = g.Key.Apellido + ", " + g.Key.Nombre,                    Cliente = g.Key.Apellido + ", " + g.Key.Nombre,
                    DNI = g.Key.Dni,                    DNI = g.Key.Dni,                    DNI = g.Key.Dni,                    DNI = g.Key.Dni,                    DNI = g.Key.Dni,
                    Compras = g.Count(),                    Compras = g.Count(),                    Compras = g.Count(),                    Compras = g.Count(),                    Compras = g.Count(),
                    Total = g.Sum(v => v.Total),                    Total = g.Sum(v => v.Total),                    Total = g.Sum(v => v.Total),                    Total = g.Sum(v => v.Total),                    Total = g.Sum(v => v.Total),
                })                })                })                })
            .OrderByDescending(x => x.Compras)            .OrderByDescending(x => x.Compras)            .OrderByDescending(x => x.Compras)
            .ThenByDescending(x => x.Total)            .ThenByDescending(x => x.Total)            .ThenByDescending(x => x.Total)
            .Take(20)            .Take(20)            .Take(20)
            .ToList();            .ToList();            .ToList();
    }

    public List<VentasPorUsuarioDto> VentasPorUsuario(DateTime desde, DateTime hasta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

        return (from v in db.Ventas.AsNoTracking()        return (from v in db.Ventas.AsNoTracking()
                join u in db.Usuarios on v.IdUsuario equals u.Id                join u in db.Usuarios on v.IdUsuario equals u.Id                join u in db.Usuarios on v.IdUsuario equals u.Id                join u in db.Usuarios on v.IdUsuario equals u.Id
                join r in db.Roles on u.IdRol equals r.Id                join r in db.Roles on u.IdRol equals r.Id                join r in db.Roles on u.IdRol equals r.Id                join r in db.Roles on u.IdRol equals r.Id
                where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada                where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada                where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada                where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada
                group v by new { u.Id, u.NombreUsuario, r.Nombre } into g                group v by new { u.Id, u.NombreUsuario, r.Nombre } into g                group v by new { u.Id, u.NombreUsuario, r.Nombre } into g                group v by new { u.Id, u.NombreUsuario, r.Nombre } into g
                select new VentasPorUsuarioDto                select new VentasPorUsuarioDto                select new VentasPorUsuarioDto                select new VentasPorUsuarioDto
                {                {                {                {
                    Usuario = g.Key.NombreUsuario,                    Usuario = g.Key.NombreUsuario,                    Usuario = g.Key.NombreUsuario,                    Usuario = g.Key.NombreUsuario,                    Usuario = g.Key.NombreUsuario,
                    Rol = g.Key.Nombre,                    Rol = g.Key.Nombre,                    Rol = g.Key.Nombre,                    Rol = g.Key.Nombre,                    Rol = g.Key.Nombre,
                    Cantidad = g.Count(),                    Cantidad = g.Count(),                    Cantidad = g.Count(),                    Cantidad = g.Count(),                    Cantidad = g.Count(),
                    Total = g.Sum(v => v.Total),                    Total = g.Sum(v => v.Total),                    Total = g.Sum(v => v.Total),                    Total = g.Sum(v => v.Total),                    Total = g.Sum(v => v.Total),
                })                })                })                })
            .OrderByDescending(x => x.Total)            .OrderByDescending(x => x.Total)            .OrderByDescending(x => x.Total)
            .ToList();            .ToList();            .ToList();
    }

    public List<ServiciosSolicitadosDto> ServiciosMasSolicitados(DateTime desde, DateTime hasta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

        return (from d in db.DetallesVenta.AsNoTracking()        return (from d in db.DetallesVenta.AsNoTracking()
                join v in db.Ventas on d.IdVenta equals v.Id                join v in db.Ventas on d.IdVenta equals v.Id                join v in db.Ventas on d.IdVenta equals v.Id                join v in db.Ventas on d.IdVenta equals v.Id
                join i in db.Items on d.IdItem equals i.Id                join i in db.Items on d.IdItem equals i.Id                join i in db.Items on d.IdItem equals i.Id                join i in db.Items on d.IdItem equals i.Id
                where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada && i.Tipo == TipoItem.Servicio                where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada && i.Tipo == TipoItem.Servicio                where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada && i.Tipo == TipoItem.Servicio                where v.Fecha >= inicio && v.Fecha < fin && !v.Anulada && i.Tipo == TipoItem.Servicio
                group d by new { i.Id, i.Nombre } into g                group d by new { i.Id, i.Nombre } into g                group d by new { i.Id, i.Nombre } into g                group d by new { i.Id, i.Nombre } into g
                select new ServiciosSolicitadosDto                select new ServiciosSolicitadosDto                select new ServiciosSolicitadosDto                select new ServiciosSolicitadosDto
                {                {                {                {
                    Servicio = g.Key.Nombre,                    Servicio = g.Key.Nombre,                    Servicio = g.Key.Nombre,                    Servicio = g.Key.Nombre,                    Servicio = g.Key.Nombre,
                    Veces = g.Count(),                    Veces = g.Count(),                    Veces = g.Count(),                    Veces = g.Count(),                    Veces = g.Count(),
                    Cantidad = g.Sum(x => x.Cantidad),                    Cantidad = g.Sum(x => x.Cantidad),                    Cantidad = g.Sum(x => x.Cantidad),                    Cantidad = g.Sum(x => x.Cantidad),                    Cantidad = g.Sum(x => x.Cantidad),
                    Total = g.Sum(x => x.Subtotal),                    Total = g.Sum(x => x.Subtotal),                    Total = g.Sum(x => x.Subtotal),                    Total = g.Sum(x => x.Subtotal),                    Total = g.Sum(x => x.Subtotal),
                })                })                })                })
            .OrderByDescending(x => x.Cantidad)            .OrderByDescending(x => x.Cantidad)            .OrderByDescending(x => x.Cantidad)
            .ThenByDescending(x => x.Total)            .ThenByDescending(x => x.Total)            .ThenByDescending(x => x.Total)
            .Take(20)            .Take(20)            .Take(20)
            .ToList();            .ToList();            .ToList();
    }

    public List<VentasPorEmpleadoDto> VentasPorEmpleado(DateTime desde, DateTime hasta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

        return (from v in db.Ventas.AsNoTracking()        return (from v in db.Ventas.AsNoTracking()
                join e in db.Empleados on v.IdEmpleado equals (int?)e.Id into empleados                join e in db.Empleados on v.IdEmpleado equals (int?)e.Id into empleados                join e in db.Empleados on v.IdEmpleado equals (int?)e.Id into empleados                join e in db.Empleados on v.IdEmpleado equals (int?)e.Id into empleados
                from e in empleados.DefaultIfEmpty()                from e in empleados.DefaultIfEmpty()                from e in empleados.DefaultIfEmpty()                from e in empleados.DefaultIfEmpty()
                where v.Fecha >= inicio && v.Fecha < fin                where v.Fecha >= inicio && v.Fecha < fin                where v.Fecha >= inicio && v.Fecha < fin                where v.Fecha >= inicio && v.Fecha < fin
                group v by new { v.IdEmpleado, e.Apellido, e.Nombre } into g                group v by new { v.IdEmpleado, e.Apellido, e.Nombre } into g                group v by new { v.IdEmpleado, e.Apellido, e.Nombre } into g                group v by new { v.IdEmpleado, e.Apellido, e.Nombre } into g
                select new VentasPorEmpleadoDto                select new VentasPorEmpleadoDto                select new VentasPorEmpleadoDto                select new VentasPorEmpleadoDto
                {                {                {                {
                    Empleado = g.Key.Apellido == null                    Empleado = g.Key.Apellido == null                    Empleado = g.Key.Apellido == null                    Empleado = g.Key.Apellido == null                    Empleado = g.Key.Apellido == null
                        ? "Sin empleado"                        ? "Sin empleado"                        ? "Sin empleado"                        ? "Sin empleado"                        ? "Sin empleado"                        ? "Sin empleado"
                        : g.Key.Apellido + ", " + g.Key.Nombre,                        : g.Key.Apellido + ", " + g.Key.Nombre,                        : g.Key.Apellido + ", " + g.Key.Nombre,                        : g.Key.Apellido + ", " + g.Key.Nombre,                        : g.Key.Apellido + ", " + g.Key.Nombre,                        : g.Key.Apellido + ", " + g.Key.Nombre,
                    Cantidad = g.Count(),                    Cantidad = g.Count(),                    Cantidad = g.Count(),                    Cantidad = g.Count(),                    Cantidad = g.Count(),
                    Total = g.Sum(v => v.Total),                    Total = g.Sum(v => v.Total),                    Total = g.Sum(v => v.Total),                    Total = g.Sum(v => v.Total),                    Total = g.Sum(v => v.Total),
                })                })                })                })
            .OrderByDescending(x => x.Total)            .OrderByDescending(x => x.Total)            .OrderByDescending(x => x.Total)
            .ToList();            .ToList();            .ToList();
    }
}
```

## `TFI_1.EFCore/Repositorios/TarjetaCreditoRepositorio.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class TarjetaCreditoRepositorio
{
    public List<TarjetaCredito> Listar(bool soloActivas = true)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        IQueryable<TarjetaCredito> consulta = db.TarjetasCredito        IQueryable<TarjetaCredito> consulta = db.TarjetasCredito
            .AsNoTracking()            .AsNoTracking()            .AsNoTracking()
            .Include(t => t.Banco);            .Include(t => t.Banco);            .Include(t => t.Banco);

        if (soloActivas)        if (soloActivas)
            consulta = consulta.Where(t => t.Activo && t.Banco.Activo);            consulta = consulta.Where(t => t.Activo && t.Banco.Activo);            consulta = consulta.Where(t => t.Activo && t.Banco.Activo);

        return consulta.OrderBy(t => t.Banco.Nombre).ThenBy(t => t.TipoTarjeta).ToList();        return consulta.OrderBy(t => t.Banco.Nombre).ThenBy(t => t.TipoTarjeta).ToList();
    }

    public List<TarjetaCredito> ListarPorBanco(int idBanco, bool soloActivas = true)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        IQueryable<TarjetaCredito> consulta = db.TarjetasCredito        IQueryable<TarjetaCredito> consulta = db.TarjetasCredito
            .AsNoTracking()            .AsNoTracking()            .AsNoTracking()
            .Include(t => t.Banco)            .Include(t => t.Banco)            .Include(t => t.Banco)
            .Where(t => t.IdBanco == idBanco);            .Where(t => t.IdBanco == idBanco);            .Where(t => t.IdBanco == idBanco);

        if (soloActivas)        if (soloActivas)
            consulta = consulta.Where(t => t.Activo);            consulta = consulta.Where(t => t.Activo);            consulta = consulta.Where(t => t.Activo);

        return consulta.OrderBy(t => t.TipoTarjeta).ToList();        return consulta.OrderBy(t => t.TipoTarjeta).ToList();
    }

    public TarjetaCredito? ObtenerPorId(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.TarjetasCredito        return db.TarjetasCredito
            .AsNoTracking()            .AsNoTracking()            .AsNoTracking()
            .Include(t => t.Banco)            .Include(t => t.Banco)            .Include(t => t.Banco)
            .SingleOrDefault(t => t.Id == id);            .SingleOrDefault(t => t.Id == id);            .SingleOrDefault(t => t.Id == id);
    }

    public bool ExisteTipo(int idBanco, string tipo, int? exceptoId = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.TarjetasCredito.Any(t =>        return db.TarjetasCredito.Any(t =>
            t.IdBanco == idBanco &&            t.IdBanco == idBanco &&            t.IdBanco == idBanco &&
            t.TipoTarjeta == tipo &&            t.TipoTarjeta == tipo &&            t.TipoTarjeta == tipo &&
            (exceptoId == null || t.Id != exceptoId));            (exceptoId == null || t.Id != exceptoId));            (exceptoId == null || t.Id != exceptoId));
    }

    public int Insertar(TarjetaCredito tarjeta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        var nueva = new TarjetaCredito        var nueva = new TarjetaCredito
        {        {
            IdBanco = tarjeta.IdBanco,            IdBanco = tarjeta.IdBanco,            IdBanco = tarjeta.IdBanco,
            TipoTarjeta = tarjeta.TipoTarjeta,            TipoTarjeta = tarjeta.TipoTarjeta,            TipoTarjeta = tarjeta.TipoTarjeta,
            PorcentajeRecargo = tarjeta.PorcentajeRecargo,            PorcentajeRecargo = tarjeta.PorcentajeRecargo,            PorcentajeRecargo = tarjeta.PorcentajeRecargo,
            CuotasMax = tarjeta.CuotasMax,            CuotasMax = tarjeta.CuotasMax,            CuotasMax = tarjeta.CuotasMax,
            Activo = tarjeta.Activo,            Activo = tarjeta.Activo,            Activo = tarjeta.Activo,
            FechaAlta = DateTime.Now,            FechaAlta = DateTime.Now,            FechaAlta = DateTime.Now,
        };        };

        db.TarjetasCredito.Add(nueva);        db.TarjetasCredito.Add(nueva);
        db.SaveChanges();        db.SaveChanges();
        tarjeta.Id = nueva.Id;        tarjeta.Id = nueva.Id;
        return nueva.Id;        return nueva.Id;
    }

    public void Actualizar(TarjetaCredito tarjeta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        TarjetaCredito actual = db.TarjetasCredito.Find(tarjeta.Id)        TarjetaCredito actual = db.TarjetasCredito.Find(tarjeta.Id)
            ?? throw new InvalidOperationException("La tarjeta no existe.");            ?? throw new InvalidOperationException("La tarjeta no existe.");            ?? throw new InvalidOperationException("La tarjeta no existe.");

        actual.IdBanco = tarjeta.IdBanco;        actual.IdBanco = tarjeta.IdBanco;
        actual.TipoTarjeta = tarjeta.TipoTarjeta;        actual.TipoTarjeta = tarjeta.TipoTarjeta;
        actual.PorcentajeRecargo = tarjeta.PorcentajeRecargo;        actual.PorcentajeRecargo = tarjeta.PorcentajeRecargo;
        actual.CuotasMax = tarjeta.CuotasMax;        actual.CuotasMax = tarjeta.CuotasMax;
        actual.Activo = tarjeta.Activo;        actual.Activo = tarjeta.Activo;
        db.SaveChanges();        db.SaveChanges();
    }

    public void Desactivar(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        db.TarjetasCredito.Where(t => t.Id == id).ExecuteUpdate(s => s.SetProperty(t => t.Activo, false));        db.TarjetasCredito.Where(t => t.Id == id).ExecuteUpdate(s => s.SetProperty(t => t.Activo, false));
    }

    public void Reactivar(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        db.TarjetasCredito.Where(t => t.Id == id).ExecuteUpdate(s => s.SetProperty(t => t.Activo, true));        db.TarjetasCredito.Where(t => t.Id == id).ExecuteUpdate(s => s.SetProperty(t => t.Activo, true));
    }
}
```

## `TFI_1.EFCore/Repositorios/UsuarioRepositorio.cs`

```csharp
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class UsuarioRepositorio
{
    public Usuario? ObtenerPorNombre(string nombreUsuario)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Usuarios        return db.Usuarios
            .AsNoTracking()            .AsNoTracking()            .AsNoTracking()
            .Include(u => u.Rol)            .Include(u => u.Rol)            .Include(u => u.Rol)
            .SingleOrDefault(u => u.NombreUsuario == nombreUsuario);            .SingleOrDefault(u => u.NombreUsuario == nombreUsuario);            .SingleOrDefault(u => u.NombreUsuario == nombreUsuario);
    }

    public List<Usuario> Listar(string? filtro = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        IQueryable<Usuario> consulta = db.Usuarios.AsNoTracking().Include(u => u.Rol);        IQueryable<Usuario> consulta = db.Usuarios.AsNoTracking().Include(u => u.Rol);

        if (!string.IsNullOrEmpty(filtro))        if (!string.IsNullOrEmpty(filtro))
            consulta = consulta.Where(u => u.NombreUsuario.Contains(filtro));            consulta = consulta.Where(u => u.NombreUsuario.Contains(filtro));            consulta = consulta.Where(u => u.NombreUsuario.Contains(filtro));

        return consulta.OrderBy(u => u.NombreUsuario).ToList();        return consulta.OrderBy(u => u.NombreUsuario).ToList();
    }

    public List<Rol> ObtenerRoles()
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Roles.AsNoTracking().OrderBy(r => r.Nombre).ToList();        return db.Roles.AsNoTracking().OrderBy(r => r.Nombre).ToList();
    }

    public bool ExisteNombre(string nombreUsuario, int? exceptoId = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Usuarios.Any(u =>        return db.Usuarios.Any(u =>
            u.NombreUsuario == nombreUsuario &&            u.NombreUsuario == nombreUsuario &&            u.NombreUsuario == nombreUsuario &&
            (exceptoId == null || u.Id != exceptoId));            (exceptoId == null || u.Id != exceptoId));            (exceptoId == null || u.Id != exceptoId));
    }

    public int Insertar(Usuario usuario)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        var nuevo = new Usuario        var nuevo = new Usuario
        {        {
            NombreUsuario = usuario.NombreUsuario,            NombreUsuario = usuario.NombreUsuario,            NombreUsuario = usuario.NombreUsuario,
            Contrasena = usuario.Contrasena,            Contrasena = usuario.Contrasena,            Contrasena = usuario.Contrasena,
            IdRol = usuario.IdRol != 0 ? usuario.IdRol : usuario.Rol.Id,            IdRol = usuario.IdRol != 0 ? usuario.IdRol : usuario.Rol.Id,            IdRol = usuario.IdRol != 0 ? usuario.IdRol : usuario.Rol.Id,
            Activo = usuario.Activo,            Activo = usuario.Activo,            Activo = usuario.Activo,
        };        };

        db.Usuarios.Add(nuevo);        db.Usuarios.Add(nuevo);
        db.SaveChanges();        db.SaveChanges();
        usuario.Id = nuevo.Id;        usuario.Id = nuevo.Id;
        return nuevo.Id;        return nuevo.Id;
    }

    public void Actualizar(Usuario usuario)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        Usuario actual = db.Usuarios.Find(usuario.Id)        Usuario actual = db.Usuarios.Find(usuario.Id)
            ?? throw new InvalidOperationException("El usuario no existe.");            ?? throw new InvalidOperationException("El usuario no existe.");            ?? throw new InvalidOperationException("El usuario no existe.");

        actual.NombreUsuario = usuario.NombreUsuario;        actual.NombreUsuario = usuario.NombreUsuario;
        actual.Contrasena = usuario.Contrasena;        actual.Contrasena = usuario.Contrasena;
        actual.IdRol = usuario.IdRol != 0 ? usuario.IdRol : usuario.Rol.Id;        actual.IdRol = usuario.IdRol != 0 ? usuario.IdRol : usuario.Rol.Id;
        actual.Activo = usuario.Activo;        actual.Activo = usuario.Activo;
        db.SaveChanges();        db.SaveChanges();
    }

    public void Desactivar(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        db.Usuarios.Where(u => u.Id == id).ExecuteUpdate(s => s.SetProperty(u => u.Activo, false));        db.Usuarios.Where(u => u.Id == id).ExecuteUpdate(s => s.SetProperty(u => u.Activo, false));
    }

    public void Reactivar(int id)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        db.Usuarios.Where(u => u.Id == id).ExecuteUpdate(s => s.SetProperty(u => u.Activo, true));        db.Usuarios.Where(u => u.Id == id).ExecuteUpdate(s => s.SetProperty(u => u.Activo, true));
    }
}
```

## `TFI_1.EFCore/Repositorios/VentaRepositorio.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Dtos;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class VentaRepositorio
{
    /// <summary>Registra cabecera, detalle, stock y cuenta corriente en una transacción.</summary>
    public int Registrar(Venta venta)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        using var tx = db.Database.BeginTransaction();        using var tx = db.Database.BeginTransaction();

        try        try
        {        {
            int idCliente = venta.IdCliente != 0 ? venta.IdCliente : venta.Cliente?.Id ?? 0;            int idCliente = venta.IdCliente != 0 ? venta.IdCliente : venta.Cliente?.Id ?? 0;            int idCliente = venta.IdCliente != 0 ? venta.IdCliente : venta.Cliente?.Id ?? 0;
            int idUsuario = venta.IdUsuario != 0 ? venta.IdUsuario : venta.Usuario?.Id ?? 0;            int idUsuario = venta.IdUsuario != 0 ? venta.IdUsuario : venta.Usuario?.Id ?? 0;            int idUsuario = venta.IdUsuario != 0 ? venta.IdUsuario : venta.Usuario?.Id ?? 0;

            decimal total = venta.Detalles.Sum(d => d.Precio * d.Cantidad)            decimal total = venta.Detalles.Sum(d => d.Precio * d.Cantidad)            decimal total = venta.Detalles.Sum(d => d.Precio * d.Cantidad)
                - venta.Descuento + venta.Recargo;                - venta.Descuento + venta.Recargo;                - venta.Descuento + venta.Recargo;                - venta.Descuento + venta.Recargo;

            var nueva = new Venta            var nueva = new Venta            var nueva = new Venta
            {            {            {
                IdCliente = idCliente,                IdCliente = idCliente,                IdCliente = idCliente,                IdCliente = idCliente,
                IdUsuario = idUsuario,                IdUsuario = idUsuario,                IdUsuario = idUsuario,                IdUsuario = idUsuario,
                IdEmpleado = venta.IdEmpleado,                IdEmpleado = venta.IdEmpleado,                IdEmpleado = venta.IdEmpleado,                IdEmpleado = venta.IdEmpleado,
                Fecha = venta.Fecha,                Fecha = venta.Fecha,                Fecha = venta.Fecha,                Fecha = venta.Fecha,
                MedioPago = venta.MedioPago,                MedioPago = venta.MedioPago,                MedioPago = venta.MedioPago,                MedioPago = venta.MedioPago,
                IdTarjetaCredito = venta.IdTarjetaCredito,                IdTarjetaCredito = venta.IdTarjetaCredito,                IdTarjetaCredito = venta.IdTarjetaCredito,                IdTarjetaCredito = venta.IdTarjetaCredito,
                CantidadCuotas = venta.CantidadCuotas,                CantidadCuotas = venta.CantidadCuotas,                CantidadCuotas = venta.CantidadCuotas,                CantidadCuotas = venta.CantidadCuotas,
                Descuento = venta.Descuento,                Descuento = venta.Descuento,                Descuento = venta.Descuento,                Descuento = venta.Descuento,
                Recargo = venta.Recargo,                Recargo = venta.Recargo,                Recargo = venta.Recargo,                Recargo = venta.Recargo,
                Total = total,                Total = total,                Total = total,                Total = total,
                Anulada = false,                Anulada = false,                Anulada = false,                Anulada = false,
            };            };            };

            db.Ventas.Add(nueva);            db.Ventas.Add(nueva);            db.Ventas.Add(nueva);
            db.SaveChanges();            db.SaveChanges();            db.SaveChanges();

            foreach (DetalleVenta detalle in venta.Detalles)            foreach (DetalleVenta detalle in venta.Detalles)            foreach (DetalleVenta detalle in venta.Detalles)
            {            {            {
                int idItem = detalle.IdItem != 0 ? detalle.IdItem : detalle.Item?.Id ?? 0;                int idItem = detalle.IdItem != 0 ? detalle.IdItem : detalle.Item?.Id ?? 0;                int idItem = detalle.IdItem != 0 ? detalle.IdItem : detalle.Item?.Id ?? 0;                int idItem = detalle.IdItem != 0 ? detalle.IdItem : detalle.Item?.Id ?? 0;
                db.DetallesVenta.Add(new DetalleVenta                db.DetallesVenta.Add(new DetalleVenta                db.DetallesVenta.Add(new DetalleVenta                db.DetallesVenta.Add(new DetalleVenta
                {                {                {                {
                    IdVenta = nueva.Id,                    IdVenta = nueva.Id,                    IdVenta = nueva.Id,                    IdVenta = nueva.Id,                    IdVenta = nueva.Id,
                    IdItem = idItem,                    IdItem = idItem,                    IdItem = idItem,                    IdItem = idItem,                    IdItem = idItem,
                    Cantidad = detalle.Cantidad,                    Cantidad = detalle.Cantidad,                    Cantidad = detalle.Cantidad,                    Cantidad = detalle.Cantidad,                    Cantidad = detalle.Cantidad,
                    Precio = detalle.Precio,                    Precio = detalle.Precio,                    Precio = detalle.Precio,                    Precio = detalle.Precio,                    Precio = detalle.Precio,
                    Subtotal = detalle.Precio * detalle.Cantidad,                    Subtotal = detalle.Precio * detalle.Cantidad,                    Subtotal = detalle.Precio * detalle.Cantidad,                    Subtotal = detalle.Precio * detalle.Cantidad,                    Subtotal = detalle.Precio * detalle.Cantidad,
                });                });                });                });
            }            }            }

            db.SaveChanges();            db.SaveChanges();            db.SaveChanges();

            var ids = venta.Detalles            var ids = venta.Detalles            var ids = venta.Detalles
                .Select(d => d.IdItem != 0 ? d.IdItem : d.Item?.Id ?? 0)                .Select(d => d.IdItem != 0 ? d.IdItem : d.Item?.Id ?? 0)                .Select(d => d.IdItem != 0 ? d.IdItem : d.Item?.Id ?? 0)                .Select(d => d.IdItem != 0 ? d.IdItem : d.Item?.Id ?? 0)
                .Distinct()                .Distinct()                .Distinct()                .Distinct()
                .ToList();                .ToList();                .ToList();                .ToList();

            var datosItems = db.Items.AsNoTracking()            var datosItems = db.Items.AsNoTracking()            var datosItems = db.Items.AsNoTracking()
                .Where(i => ids.Contains(i.Id))                .Where(i => ids.Contains(i.Id))                .Where(i => ids.Contains(i.Id))                .Where(i => ids.Contains(i.Id))
                .Select(i => new { i.Id, i.Tipo, i.Nombre })                .Select(i => new { i.Id, i.Tipo, i.Nombre })                .Select(i => new { i.Id, i.Tipo, i.Nombre })                .Select(i => new { i.Id, i.Tipo, i.Nombre })
                .ToDictionary(i => i.Id);                .ToDictionary(i => i.Id);                .ToDictionary(i => i.Id);                .ToDictionary(i => i.Id);

            foreach (DetalleVenta detalle in venta.Detalles)            foreach (DetalleVenta detalle in venta.Detalles)            foreach (DetalleVenta detalle in venta.Detalles)
            {            {            {
                int idItem = detalle.IdItem != 0 ? detalle.IdItem : detalle.Item?.Id ?? 0;                int idItem = detalle.IdItem != 0 ? detalle.IdItem : detalle.Item?.Id ?? 0;                int idItem = detalle.IdItem != 0 ? detalle.IdItem : detalle.Item?.Id ?? 0;                int idItem = detalle.IdItem != 0 ? detalle.IdItem : detalle.Item?.Id ?? 0;
                if (!datosItems.TryGetValue(idItem, out var datos) || datos.Tipo != TipoItem.Producto)                if (!datosItems.TryGetValue(idItem, out var datos) || datos.Tipo != TipoItem.Producto)                if (!datosItems.TryGetValue(idItem, out var datos) || datos.Tipo != TipoItem.Producto)                if (!datosItems.TryGetValue(idItem, out var datos) || datos.Tipo != TipoItem.Producto)
                    continue;                    continue;                    continue;                    continue;                    continue;

                int filas = db.Items                int filas = db.Items                int filas = db.Items                int filas = db.Items
                    .Where(i => i.Id == idItem && i.Stock >= detalle.Cantidad)                    .Where(i => i.Id == idItem && i.Stock >= detalle.Cantidad)                    .Where(i => i.Id == idItem && i.Stock >= detalle.Cantidad)                    .Where(i => i.Id == idItem && i.Stock >= detalle.Cantidad)                    .Where(i => i.Id == idItem && i.Stock >= detalle.Cantidad)
                    .ExecuteUpdate(s => s.SetProperty(                    .ExecuteUpdate(s => s.SetProperty(                    .ExecuteUpdate(s => s.SetProperty(                    .ExecuteUpdate(s => s.SetProperty(                    .ExecuteUpdate(s => s.SetProperty(
                        i => i.Stock,                        i => i.Stock,                        i => i.Stock,                        i => i.Stock,                        i => i.Stock,                        i => i.Stock,
                        i => (i.Stock ?? 0) - detalle.Cantidad));                        i => (i.Stock ?? 0) - detalle.Cantidad));                        i => (i.Stock ?? 0) - detalle.Cantidad));                        i => (i.Stock ?? 0) - detalle.Cantidad));                        i => (i.Stock ?? 0) - detalle.Cantidad));                        i => (i.Stock ?? 0) - detalle.Cantidad));

                if (filas == 0)                if (filas == 0)                if (filas == 0)                if (filas == 0)
                    throw new InvalidOperationException($"Stock insuficiente de {datos.Nombre}.");                    throw new InvalidOperationException($"Stock insuficiente de {datos.Nombre}.");                    throw new InvalidOperationException($"Stock insuficiente de {datos.Nombre}.");                    throw new InvalidOperationException($"Stock insuficiente de {datos.Nombre}.");                    throw new InvalidOperationException($"Stock insuficiente de {datos.Nombre}.");
            }            }            }

            if (venta.MedioPago == "cuenta_corriente")            if (venta.MedioPago == "cuenta_corriente")            if (venta.MedioPago == "cuenta_corriente")
            {            {            {
                CuentaCorriente? cuenta = db.CuentasCorrientes                CuentaCorriente? cuenta = db.CuentasCorrientes                CuentaCorriente? cuenta = db.CuentasCorrientes                CuentaCorriente? cuenta = db.CuentasCorrientes
                    .FromSqlInterpolated($"SELECT * FROM cuentas_corrientes WHERE id_cliente = {idCliente} FOR UPDATE")                    .FromSqlInterpolated($"SELECT * FROM cuentas_corrientes WHERE id_cliente = {idCliente} FOR UPDATE")                    .FromSqlInterpolated($"SELECT * FROM cuentas_corrientes WHERE id_cliente = {idCliente} FOR UPDATE")                    .FromSqlInterpolated($"SELECT * FROM cuentas_corrientes WHERE id_cliente = {idCliente} FOR UPDATE")                    .FromSqlInterpolated($"SELECT * FROM cuentas_corrientes WHERE id_cliente = {idCliente} FOR UPDATE")
                    .SingleOrDefault();                    .SingleOrDefault();                    .SingleOrDefault();                    .SingleOrDefault();                    .SingleOrDefault();

                if (cuenta == null)                if (cuenta == null)                if (cuenta == null)                if (cuenta == null)
                {                {                {                {
                    cuenta = new CuentaCorriente                    cuenta = new CuentaCorriente                    cuenta = new CuentaCorriente                    cuenta = new CuentaCorriente                    cuenta = new CuentaCorriente
                    {                    {                    {                    {                    {
                        IdCliente = idCliente,                        IdCliente = idCliente,                        IdCliente = idCliente,                        IdCliente = idCliente,                        IdCliente = idCliente,                        IdCliente = idCliente,
                        Saldo = 0m,                        Saldo = 0m,                        Saldo = 0m,                        Saldo = 0m,                        Saldo = 0m,                        Saldo = 0m,
                        FechaActualizacion = DateTime.Now,                        FechaActualizacion = DateTime.Now,                        FechaActualizacion = DateTime.Now,                        FechaActualizacion = DateTime.Now,                        FechaActualizacion = DateTime.Now,                        FechaActualizacion = DateTime.Now,
                    };                    };                    };                    };                    };
                    db.CuentasCorrientes.Add(cuenta);                    db.CuentasCorrientes.Add(cuenta);                    db.CuentasCorrientes.Add(cuenta);                    db.CuentasCorrientes.Add(cuenta);                    db.CuentasCorrientes.Add(cuenta);
                    db.SaveChanges();                    db.SaveChanges();                    db.SaveChanges();                    db.SaveChanges();                    db.SaveChanges();
                }                }                }                }

                db.MovimientosCuentaCorriente.Add(new MovimientoCuentaCorriente                db.MovimientosCuentaCorriente.Add(new MovimientoCuentaCorriente                db.MovimientosCuentaCorriente.Add(new MovimientoCuentaCorriente                db.MovimientosCuentaCorriente.Add(new MovimientoCuentaCorriente
                {                {                {                {
                    IdCuenta = cuenta.Id,                    IdCuenta = cuenta.Id,                    IdCuenta = cuenta.Id,                    IdCuenta = cuenta.Id,                    IdCuenta = cuenta.Id,
                    Fecha = venta.Fecha,                    Fecha = venta.Fecha,                    Fecha = venta.Fecha,                    Fecha = venta.Fecha,                    Fecha = venta.Fecha,
                    Tipo = TipoMovimientoCuentaCorriente.Cargo,                    Tipo = TipoMovimientoCuentaCorriente.Cargo,                    Tipo = TipoMovimientoCuentaCorriente.Cargo,                    Tipo = TipoMovimientoCuentaCorriente.Cargo,                    Tipo = TipoMovimientoCuentaCorriente.Cargo,
                    Monto = total,                    Monto = total,                    Monto = total,                    Monto = total,                    Monto = total,
                    Concepto = $"Venta #{nueva.Id}",                    Concepto = $"Venta #{nueva.Id}",                    Concepto = $"Venta #{nueva.Id}",                    Concepto = $"Venta #{nueva.Id}",                    Concepto = $"Venta #{nueva.Id}",
                    IdVenta = nueva.Id,                    IdVenta = nueva.Id,                    IdVenta = nueva.Id,                    IdVenta = nueva.Id,                    IdVenta = nueva.Id,
                    IdUsuario = idUsuario,                    IdUsuario = idUsuario,                    IdUsuario = idUsuario,                    IdUsuario = idUsuario,                    IdUsuario = idUsuario,
                });                });                });                });

                cuenta.Saldo += total;                cuenta.Saldo += total;                cuenta.Saldo += total;                cuenta.Saldo += total;
                cuenta.FechaActualizacion = DateTime.Now;                cuenta.FechaActualizacion = DateTime.Now;                cuenta.FechaActualizacion = DateTime.Now;                cuenta.FechaActualizacion = DateTime.Now;
                db.SaveChanges();                db.SaveChanges();                db.SaveChanges();                db.SaveChanges();
            }            }            }

            tx.Commit();            tx.Commit();            tx.Commit();
            venta.Id = nueva.Id;            venta.Id = nueva.Id;            venta.Id = nueva.Id;
            venta.Total = total;            venta.Total = total;            venta.Total = total;
            return nueva.Id;            return nueva.Id;            return nueva.Id;
        }        }
        catch        catch
        {        {
            tx.Rollback();            tx.Rollback();            tx.Rollback();
            throw;            throw;            throw;
        }        }
    }

    public List<VentaListadoDto> Listar(DateTime desde, DateTime hasta, int? usuarioId = null,
        int? empleadoId = null, int? ventaId = null, int? limite = null, int? desplazamiento = null)        int? empleadoId = null, int? ventaId = null, int? limite = null, int? desplazamiento = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

        IQueryable<Venta> consulta = db.Ventas.AsNoTracking()        IQueryable<Venta> consulta = db.Ventas.AsNoTracking()
            .Where(v => v.Fecha >= inicio && v.Fecha < fin);            .Where(v => v.Fecha >= inicio && v.Fecha < fin);            .Where(v => v.Fecha >= inicio && v.Fecha < fin);

        if (usuarioId.HasValue)        if (usuarioId.HasValue)
            consulta = consulta.Where(v => v.IdUsuario == usuarioId.Value);            consulta = consulta.Where(v => v.IdUsuario == usuarioId.Value);            consulta = consulta.Where(v => v.IdUsuario == usuarioId.Value);
        if (empleadoId.HasValue)        if (empleadoId.HasValue)
            consulta = consulta.Where(v => v.IdEmpleado == empleadoId.Value);            consulta = consulta.Where(v => v.IdEmpleado == empleadoId.Value);            consulta = consulta.Where(v => v.IdEmpleado == empleadoId.Value);
        if (ventaId.HasValue)        if (ventaId.HasValue)
            consulta = consulta.Where(v => v.Id == ventaId.Value);            consulta = consulta.Where(v => v.Id == ventaId.Value);            consulta = consulta.Where(v => v.Id == ventaId.Value);

        IQueryable<VentaListadoDto> proyeccion = consulta        IQueryable<VentaListadoDto> proyeccion = consulta
            .OrderByDescending(v => v.Fecha)            .OrderByDescending(v => v.Fecha)            .OrderByDescending(v => v.Fecha)
            .ThenByDescending(v => v.Id)            .ThenByDescending(v => v.Id)            .ThenByDescending(v => v.Id)
            .Select(v => new VentaListadoDto            .Select(v => new VentaListadoDto            .Select(v => new VentaListadoDto
            {            {            {
                Venta = v.Id,                Venta = v.Id,                Venta = v.Id,                Venta = v.Id,
                Fecha = v.Fecha,                Fecha = v.Fecha,                Fecha = v.Fecha,                Fecha = v.Fecha,
                Cliente = v.Cliente.Apellido + ", " + v.Cliente.Nombre,                Cliente = v.Cliente.Apellido + ", " + v.Cliente.Nombre,                Cliente = v.Cliente.Apellido + ", " + v.Cliente.Nombre,                Cliente = v.Cliente.Apellido + ", " + v.Cliente.Nombre,
                Usuario = v.Usuario.NombreUsuario,                Usuario = v.Usuario.NombreUsuario,                Usuario = v.Usuario.NombreUsuario,                Usuario = v.Usuario.NombreUsuario,
                Empleado = v.Empleado == null ? null : v.Empleado.Apellido + ", " + v.Empleado.Nombre,                Empleado = v.Empleado == null ? null : v.Empleado.Apellido + ", " + v.Empleado.Nombre,                Empleado = v.Empleado == null ? null : v.Empleado.Apellido + ", " + v.Empleado.Nombre,                Empleado = v.Empleado == null ? null : v.Empleado.Apellido + ", " + v.Empleado.Nombre,
                Pago = v.MedioPago,                Pago = v.MedioPago,                Pago = v.MedioPago,                Pago = v.MedioPago,
                Tarjeta = v.TarjetaCredito == null                Tarjeta = v.TarjetaCredito == null                Tarjeta = v.TarjetaCredito == null                Tarjeta = v.TarjetaCredito == null
                    ? null                    ? null                    ? null                    ? null                    ? null
                    : v.TarjetaCredito.Banco.Nombre + " - " + v.TarjetaCredito.TipoTarjeta,                    : v.TarjetaCredito.Banco.Nombre + " - " + v.TarjetaCredito.TipoTarjeta,                    : v.TarjetaCredito.Banco.Nombre + " - " + v.TarjetaCredito.TipoTarjeta,                    : v.TarjetaCredito.Banco.Nombre + " - " + v.TarjetaCredito.TipoTarjeta,                    : v.TarjetaCredito.Banco.Nombre + " - " + v.TarjetaCredito.TipoTarjeta,
                Cuotas = v.CantidadCuotas,                Cuotas = v.CantidadCuotas,                Cuotas = v.CantidadCuotas,                Cuotas = v.CantidadCuotas,
                Subtotal = v.Detalles.Sum(d => (decimal?)d.Subtotal) ?? 0m,                Subtotal = v.Detalles.Sum(d => (decimal?)d.Subtotal) ?? 0m,                Subtotal = v.Detalles.Sum(d => (decimal?)d.Subtotal) ?? 0m,                Subtotal = v.Detalles.Sum(d => (decimal?)d.Subtotal) ?? 0m,
                Descuento = v.Descuento,                Descuento = v.Descuento,                Descuento = v.Descuento,                Descuento = v.Descuento,
                Recargo = v.Recargo,                Recargo = v.Recargo,                Recargo = v.Recargo,                Recargo = v.Recargo,
                Total = v.Total,                Total = v.Total,                Total = v.Total,                Total = v.Total,
                Anulada = v.Anulada,                Anulada = v.Anulada,                Anulada = v.Anulada,                Anulada = v.Anulada,
            });            });            });

        return Comun.Paginar(proyeccion, limite, desplazamiento).ToList();        return Comun.Paginar(proyeccion, limite, desplazamiento).ToList();
    }

    public int Contar(DateTime desde, DateTime hasta, int? usuarioId = null,
        int? empleadoId = null, int? ventaId = null)        int? empleadoId = null, int? ventaId = null)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);        (DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

        IQueryable<Venta> consulta = db.Ventas.AsNoTracking()        IQueryable<Venta> consulta = db.Ventas.AsNoTracking()
            .Where(v => v.Fecha >= inicio && v.Fecha < fin);            .Where(v => v.Fecha >= inicio && v.Fecha < fin);            .Where(v => v.Fecha >= inicio && v.Fecha < fin);

        if (usuarioId.HasValue)        if (usuarioId.HasValue)
            consulta = consulta.Where(v => v.IdUsuario == usuarioId.Value);            consulta = consulta.Where(v => v.IdUsuario == usuarioId.Value);            consulta = consulta.Where(v => v.IdUsuario == usuarioId.Value);
        if (empleadoId.HasValue)        if (empleadoId.HasValue)
            consulta = consulta.Where(v => v.IdEmpleado == empleadoId.Value);            consulta = consulta.Where(v => v.IdEmpleado == empleadoId.Value);            consulta = consulta.Where(v => v.IdEmpleado == empleadoId.Value);
        if (ventaId.HasValue)        if (ventaId.HasValue)
            consulta = consulta.Where(v => v.Id == ventaId.Value);            consulta = consulta.Where(v => v.Id == ventaId.Value);            consulta = consulta.Where(v => v.Id == ventaId.Value);

        return consulta.Count();        return consulta.Count();
    }

    public VentaCabeceraDto? ObtenerCabecera(int ventaId)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.Ventas.AsNoTracking()        return db.Ventas.AsNoTracking()
            .Where(v => v.Id == ventaId)            .Where(v => v.Id == ventaId)            .Where(v => v.Id == ventaId)
            .Select(v => new VentaCabeceraDto            .Select(v => new VentaCabeceraDto            .Select(v => new VentaCabeceraDto
            {            {            {
                Venta = v.Id,                Venta = v.Id,                Venta = v.Id,                Venta = v.Id,
                Fecha = v.Fecha,                Fecha = v.Fecha,                Fecha = v.Fecha,                Fecha = v.Fecha,
                Cliente = v.Cliente.Apellido + ", " + v.Cliente.Nombre,                Cliente = v.Cliente.Apellido + ", " + v.Cliente.Nombre,                Cliente = v.Cliente.Apellido + ", " + v.Cliente.Nombre,                Cliente = v.Cliente.Apellido + ", " + v.Cliente.Nombre,
                Usuario = v.Usuario.NombreUsuario,                Usuario = v.Usuario.NombreUsuario,                Usuario = v.Usuario.NombreUsuario,                Usuario = v.Usuario.NombreUsuario,
                Empleado = v.Empleado == null ? null : v.Empleado.Apellido + ", " + v.Empleado.Nombre,                Empleado = v.Empleado == null ? null : v.Empleado.Apellido + ", " + v.Empleado.Nombre,                Empleado = v.Empleado == null ? null : v.Empleado.Apellido + ", " + v.Empleado.Nombre,                Empleado = v.Empleado == null ? null : v.Empleado.Apellido + ", " + v.Empleado.Nombre,
                Pago = v.MedioPago,                Pago = v.MedioPago,                Pago = v.MedioPago,                Pago = v.MedioPago,
                IdTarjeta = v.IdTarjetaCredito,                IdTarjeta = v.IdTarjetaCredito,                IdTarjeta = v.IdTarjetaCredito,                IdTarjeta = v.IdTarjetaCredito,
                Banco = v.TarjetaCredito == null ? null : v.TarjetaCredito.Banco.Nombre,                Banco = v.TarjetaCredito == null ? null : v.TarjetaCredito.Banco.Nombre,                Banco = v.TarjetaCredito == null ? null : v.TarjetaCredito.Banco.Nombre,                Banco = v.TarjetaCredito == null ? null : v.TarjetaCredito.Banco.Nombre,
                TipoTarjeta = v.TarjetaCredito == null ? null : v.TarjetaCredito.TipoTarjeta,                TipoTarjeta = v.TarjetaCredito == null ? null : v.TarjetaCredito.TipoTarjeta,                TipoTarjeta = v.TarjetaCredito == null ? null : v.TarjetaCredito.TipoTarjeta,                TipoTarjeta = v.TarjetaCredito == null ? null : v.TarjetaCredito.TipoTarjeta,
                Cuotas = v.CantidadCuotas,                Cuotas = v.CantidadCuotas,                Cuotas = v.CantidadCuotas,                Cuotas = v.CantidadCuotas,
                Subtotal = v.Detalles.Sum(d => (decimal?)d.Subtotal) ?? 0m,                Subtotal = v.Detalles.Sum(d => (decimal?)d.Subtotal) ?? 0m,                Subtotal = v.Detalles.Sum(d => (decimal?)d.Subtotal) ?? 0m,                Subtotal = v.Detalles.Sum(d => (decimal?)d.Subtotal) ?? 0m,
                Descuento = v.Descuento,                Descuento = v.Descuento,                Descuento = v.Descuento,                Descuento = v.Descuento,
                Anulada = v.Anulada,                Anulada = v.Anulada,                Anulada = v.Anulada,                Anulada = v.Anulada,
                FechaAnulacion = v.FechaAnulacion,                FechaAnulacion = v.FechaAnulacion,                FechaAnulacion = v.FechaAnulacion,                FechaAnulacion = v.FechaAnulacion,
                MotivoAnulacion = v.MotivoAnulacion,                MotivoAnulacion = v.MotivoAnulacion,                MotivoAnulacion = v.MotivoAnulacion,                MotivoAnulacion = v.MotivoAnulacion,
                UsuarioAnulacion = v.UsuarioAnulacion == null ? null : v.UsuarioAnulacion.NombreUsuario,                UsuarioAnulacion = v.UsuarioAnulacion == null ? null : v.UsuarioAnulacion.NombreUsuario,                UsuarioAnulacion = v.UsuarioAnulacion == null ? null : v.UsuarioAnulacion.NombreUsuario,                UsuarioAnulacion = v.UsuarioAnulacion == null ? null : v.UsuarioAnulacion.NombreUsuario,
                Recargo = v.Recargo,                Recargo = v.Recargo,                Recargo = v.Recargo,                Recargo = v.Recargo,
                Total = v.Total,                Total = v.Total,                Total = v.Total,                Total = v.Total,
            })            })            })
            .SingleOrDefault();            .SingleOrDefault();            .SingleOrDefault();
    }

    public List<VentaDetalleDto> ObtenerDetalle(int ventaId)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        return db.DetallesVenta.AsNoTracking()        return db.DetallesVenta.AsNoTracking()
            .Where(d => d.IdVenta == ventaId)            .Where(d => d.IdVenta == ventaId)            .Where(d => d.IdVenta == ventaId)
            .OrderBy(d => d.Id)            .OrderBy(d => d.Id)            .OrderBy(d => d.Id)
            .Select(d => new VentaDetalleDto            .Select(d => new VentaDetalleDto            .Select(d => new VentaDetalleDto
            {            {            {
                Item = d.Item.Nombre,                Item = d.Item.Nombre,                Item = d.Item.Nombre,                Item = d.Item.Nombre,
                Tipo = d.Item.Tipo == TipoItem.Producto ? "producto" : "servicio",                Tipo = d.Item.Tipo == TipoItem.Producto ? "producto" : "servicio",                Tipo = d.Item.Tipo == TipoItem.Producto ? "producto" : "servicio",                Tipo = d.Item.Tipo == TipoItem.Producto ? "producto" : "servicio",
                Cantidad = d.Cantidad,                Cantidad = d.Cantidad,                Cantidad = d.Cantidad,                Cantidad = d.Cantidad,
                Precio = d.Precio,                Precio = d.Precio,                Precio = d.Precio,                Precio = d.Precio,
                Subtotal = d.Subtotal,                Subtotal = d.Subtotal,                Subtotal = d.Subtotal,                Subtotal = d.Subtotal,
            })            })            })
            .ToList();            .ToList();            .ToList();
    }

    /// <summary>Anula sin borrar: devuelve stock, revierte la cuenta corriente y marca la venta.</summary>
    public void Anular(int ventaId, int usuarioId, string motivo)
    {
        using var db = new PeluqueriaDbContext();        using var db = new PeluqueriaDbContext();
        using var tx = db.Database.BeginTransaction();        using var tx = db.Database.BeginTransaction();

        try        try
        {        {
            Venta venta = db.Ventas            Venta venta = db.Ventas            Venta venta = db.Ventas
                .FromSqlInterpolated($"SELECT * FROM ventas WHERE id_venta = {ventaId} AND anulada = 0 FOR UPDATE")                .FromSqlInterpolated($"SELECT * FROM ventas WHERE id_venta = {ventaId} AND anulada = 0 FOR UPDATE")                .FromSqlInterpolated($"SELECT * FROM ventas WHERE id_venta = {ventaId} AND anulada = 0 FOR UPDATE")                .FromSqlInterpolated($"SELECT * FROM ventas WHERE id_venta = {ventaId} AND anulada = 0 FOR UPDATE")
                .AsNoTracking()                .AsNoTracking()                .AsNoTracking()                .AsNoTracking()
                .SingleOrDefault()                .SingleOrDefault()                .SingleOrDefault()                .SingleOrDefault()
                ?? throw new InvalidOperationException("La venta no existe o ya fue anulada.");                ?? throw new InvalidOperationException("La venta no existe o ya fue anulada.");                ?? throw new InvalidOperationException("La venta no existe o ya fue anulada.");                ?? throw new InvalidOperationException("La venta no existe o ya fue anulada.");

            var detalles = db.DetallesVenta.AsNoTracking()            var detalles = db.DetallesVenta.AsNoTracking()            var detalles = db.DetallesVenta.AsNoTracking()
                .Where(d => d.IdVenta == ventaId)                .Where(d => d.IdVenta == ventaId)                .Where(d => d.IdVenta == ventaId)                .Where(d => d.IdVenta == ventaId)
                .Select(d => new { d.IdItem, d.Cantidad })                .Select(d => new { d.IdItem, d.Cantidad })                .Select(d => new { d.IdItem, d.Cantidad })                .Select(d => new { d.IdItem, d.Cantidad })
                .ToList();                .ToList();                .ToList();                .ToList();

            var ids = detalles.Select(d => d.IdItem).Distinct().ToList();            var ids = detalles.Select(d => d.IdItem).Distinct().ToList();            var ids = detalles.Select(d => d.IdItem).Distinct().ToList();
            var idsProducto = db.Items.AsNoTracking()            var idsProducto = db.Items.AsNoTracking()            var idsProducto = db.Items.AsNoTracking()
                .Where(i => i.Tipo == TipoItem.Producto && ids.Contains(i.Id))                .Where(i => i.Tipo == TipoItem.Producto && ids.Contains(i.Id))                .Where(i => i.Tipo == TipoItem.Producto && ids.Contains(i.Id))                .Where(i => i.Tipo == TipoItem.Producto && ids.Contains(i.Id))
                .Select(i => i.Id)                .Select(i => i.Id)                .Select(i => i.Id)                .Select(i => i.Id)
                .ToList();                .ToList();                .ToList();                .ToList();

            foreach (var detalle in detalles.Where(d => idsProducto.Contains(d.IdItem)))            foreach (var detalle in detalles.Where(d => idsProducto.Contains(d.IdItem)))            foreach (var detalle in detalles.Where(d => idsProducto.Contains(d.IdItem)))
            {            {            {
                db.Items                db.Items                db.Items                db.Items
                    .Where(i => i.Id == detalle.IdItem)                    .Where(i => i.Id == detalle.IdItem)                    .Where(i => i.Id == detalle.IdItem)                    .Where(i => i.Id == detalle.IdItem)                    .Where(i => i.Id == detalle.IdItem)
                    .ExecuteUpdate(s => s.SetProperty(                    .ExecuteUpdate(s => s.SetProperty(                    .ExecuteUpdate(s => s.SetProperty(                    .ExecuteUpdate(s => s.SetProperty(                    .ExecuteUpdate(s => s.SetProperty(
                        i => i.Stock,                        i => i.Stock,                        i => i.Stock,                        i => i.Stock,                        i => i.Stock,                        i => i.Stock,
                        i => (i.Stock ?? 0) + detalle.Cantidad));                        i => (i.Stock ?? 0) + detalle.Cantidad));                        i => (i.Stock ?? 0) + detalle.Cantidad));                        i => (i.Stock ?? 0) + detalle.Cantidad));                        i => (i.Stock ?? 0) + detalle.Cantidad));                        i => (i.Stock ?? 0) + detalle.Cantidad));
            }            }            }

            if (venta.MedioPago == "cuenta_corriente")            if (venta.MedioPago == "cuenta_corriente")            if (venta.MedioPago == "cuenta_corriente")
            {            {            {
                CuentaCorriente cuenta = db.CuentasCorrientes                CuentaCorriente cuenta = db.CuentasCorrientes                CuentaCorriente cuenta = db.CuentasCorrientes                CuentaCorriente cuenta = db.CuentasCorrientes
                    .FromSqlInterpolated($"SELECT * FROM cuentas_corrientes WHERE id_cliente = {venta.IdCliente} FOR UPDATE")                    .FromSqlInterpolated($"SELECT * FROM cuentas_corrientes WHERE id_cliente = {venta.IdCliente} FOR UPDATE")                    .FromSqlInterpolated($"SELECT * FROM cuentas_corrientes WHERE id_cliente = {venta.IdCliente} FOR UPDATE")                    .FromSqlInterpolated($"SELECT * FROM cuentas_corrientes WHERE id_cliente = {venta.IdCliente} FOR UPDATE")                    .FromSqlInterpolated($"SELECT * FROM cuentas_corrientes WHERE id_cliente = {venta.IdCliente} FOR UPDATE")
                    .SingleOrDefault()                    .SingleOrDefault()                    .SingleOrDefault()                    .SingleOrDefault()                    .SingleOrDefault()
                    ?? throw new InvalidOperationException("No se encontró la cuenta corriente del cliente.");                    ?? throw new InvalidOperationException("No se encontró la cuenta corriente del cliente.");                    ?? throw new InvalidOperationException("No se encontró la cuenta corriente del cliente.");                    ?? throw new InvalidOperationException("No se encontró la cuenta corriente del cliente.");                    ?? throw new InvalidOperationException("No se encontró la cuenta corriente del cliente.");

                db.MovimientosCuentaCorriente.Add(new MovimientoCuentaCorriente                db.MovimientosCuentaCorriente.Add(new MovimientoCuentaCorriente                db.MovimientosCuentaCorriente.Add(new MovimientoCuentaCorriente                db.MovimientosCuentaCorriente.Add(new MovimientoCuentaCorriente
                {                {                {                {
                    IdCuenta = cuenta.Id,                    IdCuenta = cuenta.Id,                    IdCuenta = cuenta.Id,                    IdCuenta = cuenta.Id,                    IdCuenta = cuenta.Id,
                    Fecha = DateTime.Now,                    Fecha = DateTime.Now,                    Fecha = DateTime.Now,                    Fecha = DateTime.Now,                    Fecha = DateTime.Now,
                    Tipo = TipoMovimientoCuentaCorriente.Abono,                    Tipo = TipoMovimientoCuentaCorriente.Abono,                    Tipo = TipoMovimientoCuentaCorriente.Abono,                    Tipo = TipoMovimientoCuentaCorriente.Abono,                    Tipo = TipoMovimientoCuentaCorriente.Abono,
                    Monto = venta.Total,                    Monto = venta.Total,                    Monto = venta.Total,                    Monto = venta.Total,                    Monto = venta.Total,
                    Concepto = $"Anulación venta #{ventaId}: {motivo}",                    Concepto = $"Anulación venta #{ventaId}: {motivo}",                    Concepto = $"Anulación venta #{ventaId}: {motivo}",                    Concepto = $"Anulación venta #{ventaId}: {motivo}",                    Concepto = $"Anulación venta #{ventaId}: {motivo}",
                    IdVenta = ventaId,                    IdVenta = ventaId,                    IdVenta = ventaId,                    IdVenta = ventaId,                    IdVenta = ventaId,
                    IdUsuario = usuarioId,                    IdUsuario = usuarioId,                    IdUsuario = usuarioId,                    IdUsuario = usuarioId,                    IdUsuario = usuarioId,
                });                });                });                });

                cuenta.Saldo -= venta.Total;                cuenta.Saldo -= venta.Total;                cuenta.Saldo -= venta.Total;                cuenta.Saldo -= venta.Total;
                cuenta.FechaActualizacion = DateTime.Now;                cuenta.FechaActualizacion = DateTime.Now;                cuenta.FechaActualizacion = DateTime.Now;                cuenta.FechaActualizacion = DateTime.Now;
                db.SaveChanges();                db.SaveChanges();                db.SaveChanges();                db.SaveChanges();
            }            }            }

            int filas = db.Ventas            int filas = db.Ventas            int filas = db.Ventas
                .Where(v => v.Id == ventaId && !v.Anulada)                .Where(v => v.Id == ventaId && !v.Anulada)                .Where(v => v.Id == ventaId && !v.Anulada)                .Where(v => v.Id == ventaId && !v.Anulada)
                .ExecuteUpdate(s => s                .ExecuteUpdate(s => s                .ExecuteUpdate(s => s                .ExecuteUpdate(s => s
                    .SetProperty(v => v.Anulada, true)                    .SetProperty(v => v.Anulada, true)                    .SetProperty(v => v.Anulada, true)                    .SetProperty(v => v.Anulada, true)                    .SetProperty(v => v.Anulada, true)
                    .SetProperty(v => v.FechaAnulacion, (DateTime?)DateTime.Now)                    .SetProperty(v => v.FechaAnulacion, (DateTime?)DateTime.Now)                    .SetProperty(v => v.FechaAnulacion, (DateTime?)DateTime.Now)                    .SetProperty(v => v.FechaAnulacion, (DateTime?)DateTime.Now)                    .SetProperty(v => v.FechaAnulacion, (DateTime?)DateTime.Now)
                    .SetProperty(v => v.IdUsuarioAnulacion, (int?)usuarioId)                    .SetProperty(v => v.IdUsuarioAnulacion, (int?)usuarioId)                    .SetProperty(v => v.IdUsuarioAnulacion, (int?)usuarioId)                    .SetProperty(v => v.IdUsuarioAnulacion, (int?)usuarioId)                    .SetProperty(v => v.IdUsuarioAnulacion, (int?)usuarioId)
                    .SetProperty(v => v.MotivoAnulacion, motivo));                    .SetProperty(v => v.MotivoAnulacion, motivo));                    .SetProperty(v => v.MotivoAnulacion, motivo));                    .SetProperty(v => v.MotivoAnulacion, motivo));                    .SetProperty(v => v.MotivoAnulacion, motivo));

            if (filas == 0)            if (filas == 0)            if (filas == 0)
                throw new InvalidOperationException("La venta ya fue anulada.");                throw new InvalidOperationException("La venta ya fue anulada.");                throw new InvalidOperationException("La venta ya fue anulada.");                throw new InvalidOperationException("La venta ya fue anulada.");

            tx.Commit();            tx.Commit();            tx.Commit();
        }        }
        catch        catch
        {        {
            tx.Rollback();            tx.Rollback();            tx.Rollback();
            throw;            throw;            throw;
        }        }
    }
}
```

## `TFI_1.EFCore/Utilidades/TablaExtensiones.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace TFI_1.EFCore.Utilidades;

/// <summary>
/// Convierte resultados tipados de EF Core al DataTable que consumían las grillas
/// del proyecto original (mismos nombres de columna que los alias SQL).
/// </summary>
public static class TablaExtensiones
{
    public static DataTable ToDataTable<T>(this IEnumerable<T> filas)
    {
        PropertyInfo[] propiedades = typeof(T)        PropertyInfo[] propiedades = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)            .GetProperties(BindingFlags.Public | BindingFlags.Instance)            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.GetIndexParameters().Length == 0)            .Where(p => p.CanRead && p.GetIndexParameters().Length == 0)            .Where(p => p.CanRead && p.GetIndexParameters().Length == 0)
            .ToArray();            .ToArray();            .ToArray();

        var tabla = new DataTable();        var tabla = new DataTable();

        foreach (PropertyInfo propiedad in propiedades)        foreach (PropertyInfo propiedad in propiedades)
        {        {
            Type tipo = Nullable.GetUnderlyingType(propiedad.PropertyType) ?? propiedad.PropertyType;            Type tipo = Nullable.GetUnderlyingType(propiedad.PropertyType) ?? propiedad.PropertyType;            Type tipo = Nullable.GetUnderlyingType(propiedad.PropertyType) ?? propiedad.PropertyType;
            if (tipo.IsEnum)            if (tipo.IsEnum)            if (tipo.IsEnum)
                tipo = typeof(string);                tipo = typeof(string);                tipo = typeof(string);                tipo = typeof(string);
            tabla.Columns.Add(propiedad.Name, tipo);            tabla.Columns.Add(propiedad.Name, tipo);            tabla.Columns.Add(propiedad.Name, tipo);
        }        }

        foreach (T fila in filas)        foreach (T fila in filas)
        {        {
            DataRow nueva = tabla.NewRow();            DataRow nueva = tabla.NewRow();            DataRow nueva = tabla.NewRow();
            foreach (PropertyInfo propiedad in propiedades)            foreach (PropertyInfo propiedad in propiedades)            foreach (PropertyInfo propiedad in propiedades)
            {            {            {
                object? valor = propiedad.GetValue(fila);                object? valor = propiedad.GetValue(fila);                object? valor = propiedad.GetValue(fila);                object? valor = propiedad.GetValue(fila);
                if (valor is Enum enumeracion)                if (valor is Enum enumeracion)                if (valor is Enum enumeracion)                if (valor is Enum enumeracion)
                    valor = enumeracion.ToString();                    valor = enumeracion.ToString();                    valor = enumeracion.ToString();                    valor = enumeracion.ToString();                    valor = enumeracion.ToString();
                nueva[propiedad.Name] = valor ?? DBNull.Value;                nueva[propiedad.Name] = valor ?? DBNull.Value;                nueva[propiedad.Name] = valor ?? DBNull.Value;                nueva[propiedad.Name] = valor ?? DBNull.Value;
            }            }            }
            tabla.Rows.Add(nueva);            tabla.Rows.Add(nueva);            tabla.Rows.Add(nueva);
        }        }

        return tabla;        return tabla;
    }
}
```

## `TFI_1.EFCore.Verificacion/Program.cs`

```csharp
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
        "SELECT COUNT(*) AS `Value` FROM information_schema.tables WHERE table_schema = DATABASE()").Single());        "SELECT COUNT(*) AS `Value` FROM information_schema.tables WHERE table_schema = DATABASE()").Single());

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
var atencion = db.Atenciones.AsNoTracking()
    .Where(a => a.Estado != EstadoAtencion.Cancelada)
    .OrderBy(a => a.Id)
    .Select(a => new { a.Id, a.IdEmpleado, a.FechaHora })
    .FirstOrDefault();

Console.WriteLine();
Console.WriteLine("3) Repositorios de catálogo y personas");
Verificar("Usuario.Listar", () => new UsuarioRepositorio().Listar());
Verificar("Usuario.ObtenerRoles", () => new UsuarioRepositorio().ObtenerRoles());
Verificar("Cliente.Listar", () => new ClienteRepositorio().Listar());
Verificar("Cliente.Listar(filtro)", () => new ClienteRepositorio().Listar("a"));
Verificar("Categoria.Listar", () => new CategoriaRepositorio().Listar());
Verificar("Item.Listar", () => new ItemRepositorio().Listar());
Verificar("Item.Listar(servicios)", () => new ItemRepositorio().Listar(tipo: TipoItem.Servicio));
Verificar("Item.Listar(stock bajo)", () => new ItemRepositorio().Listar(soloStockBajo: true));
Verificar("Empleado.Listar", () => new EmpleadoRepositorio().Listar());
Verificar("Proveedor.Listar", () => new ProveedorRepositorio().Listar());
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
        throw new InvalidOperationException($"Contar={cantidad}, Listar={filas}");        throw new InvalidOperationException($"Contar={cantidad}, Listar={filas}");
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
        new CuentaCorrienteRepositorio().MovimientosResumen(idCliente, desde, hasta));        new CuentaCorrienteRepositorio().MovimientosResumen(idCliente, desde, hasta));
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
        new IngresoMercaderiaRepositorio().ObtenerPorId(ingresoId.Value));        new IngresoMercaderiaRepositorio().ObtenerPorId(ingresoId.Value));

Console.WriteLine();
Console.WriteLine("8) Atenciones y auditoría");
Verificar("Atencion.Listar", () => new AtencionRepositorio().Listar());
Verificar("Atencion.Listar(estado)", () => new AtencionRepositorio().Listar(estado: "pendiente"));
Verificar("Atencion.CantidadHoy", () => new AtencionRepositorio().CantidadHoy());

if (atencion != null)
{
    Verificar("Atencion.ExisteConflictoHorario (GREATEST)", () =>
    {
        bool conflicto = new AtencionRepositorio()        bool conflicto = new AtencionRepositorio()
            .ExisteConflictoHorario(atencion.IdEmpleado, atencion.FechaHora, 30);            .ExisteConflictoHorario(atencion.IdEmpleado, atencion.FechaHora, 30);            .ExisteConflictoHorario(atencion.IdEmpleado, atencion.FechaHora, 30);
        if (!conflicto)        if (!conflicto)
            throw new InvalidOperationException("se esperaba conflicto con la misma atención");            throw new InvalidOperationException("se esperaba conflicto con la misma atención");            throw new InvalidOperationException("se esperaba conflicto con la misma atención");
        return "true";        return "true";
    });
}
else if (empleadoId.HasValue)
{
    Verificar("Atencion.ExisteConflictoHorario (GREATEST)", () =>
        new AtencionRepositorio().ExisteConflictoHorario(empleadoId.Value, DateTime.Today.AddHours(9), 30));        new AtencionRepositorio().ExisteConflictoHorario(empleadoId.Value, DateTime.Today.AddHours(9), 30));
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
        .Where(v => v.Fecha >= desde && v.Fecha < hasta.AddDays(1) && !v.Anulada)        .Where(v => v.Fecha >= desde && v.Fecha < hasta.AddDays(1) && !v.Anulada)
        .Sum(v => (decimal?)v.Total) ?? 0m;        .Sum(v => (decimal?)v.Total) ?? 0m;

    ResumenDto fila = new ReporteRepositorio().Resumen(desde, hasta).Single();
    if (fila.Total != esperado)
        throw new InvalidOperationException($"total {fila.Total} != {esperado}");        throw new InvalidOperationException($"total {fila.Total} != {esperado}");
    return $"total {fila.Total:0.00}, ticket {fila.TicketPromedio:0.00}";
});
Verificar("Reporte.VentasPorMedioPago", () => new ReporteRepositorio().VentasPorMedioPago(desde, hasta));
Verificar("Reporte.ProductosMasVendidos", () => new ReporteRepositorio().ProductosMasVendidos(desde, hasta));
Verificar("Reporte.VentasPorDia", () => new ReporteRepositorio().VentasPorDia(desde, hasta));
Verificar("Reporte.ClientesConMasCompras", () => new ReporteRepositorio().ClientesConMasCompras(desde, hasta));
Verificar("Reporte.VentasPorUsuario", () => new ReporteRepositorio().VentasPorUsuario(desde, hasta));
Verificar("Reporte.ServiciosMasSolicitados", () => new ReporteRepositorio().ServiciosMasSolicitados(desde, hasta));
Verificar("Reporte.VentasPorEmpleado", () => new ReporteRepositorio().VentasPorEmpleado(desde, hasta));

Console.WriteLine();
Console.WriteLine("11) Compatibilidad con DataTable");
Verificar("TablaExtensiones.ToDataTable<VentaListadoDto>", () =>
{
    DataTable tabla = new VentaRepositorio().Listar(desde, hasta, limite: 5).ToDataTable();
    if (tabla.Columns.Count == 0)
        throw new InvalidOperationException("la tabla quedó sin columnas");        throw new InvalidOperationException("la tabla quedó sin columnas");
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
        object? valor = prueba();        object? valor = prueba();
        Console.WriteLine($"  [OK]    {nombre} -> {Describir(valor)}");        Console.WriteLine($"  [OK]    {nombre} -> {Describir(valor)}");
    }
    catch (Exception ex)
    {
        fallos.Add($"{nombre}: {ex.GetType().Name}: {ex.Message}");        fallos.Add($"{nombre}: {ex.GetType().Name}: {ex.Message}");
        Console.WriteLine($"  [FALLO] {nombre} -> {ex.GetType().Name}: {ex.Message}");        Console.WriteLine($"  [FALLO] {nombre} -> {ex.GetType().Name}: {ex.Message}");
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
```

## `TFI_1.EFCore.Verificacion/TFI_1.EFCore.Verificacion.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0-windows</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <RootNamespace>TFI_1.EFCore.Verificacion</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\TFI_1.EFCore\TFI_1.EFCore.csproj" />
  </ItemGroup>

  <ItemGroup>
    <None Include="..\TFI_1.EFCore\appsettings.json" Link="appsettings.json"
          Condition="Exists('..\TFI_1.EFCore\appsettings.json')">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
    <None Include="..\TFI_1.EFCore\appsettings.example.json" Link="appsettings.json"
          Condition="!Exists('..\TFI_1.EFCore\appsettings.json')">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
  </ItemGroup>

</Project>
```

