# Documentación técnica — Migración de la capa de acceso a datos a EF Core

**Proyecto:** `TFI_1` — Sistema de gestión para peluquería  
**Alcance:** únicamente la capa de acceso a datos (`TFI_1/Datos`)  
**Proyecto destino:** `TFI_1.EFCore`  
**Proveedor:** Pomelo.EntityFrameworkCore.MySql  
**Fecha de verificación:** octubre de 2026

---

## 1. Objetivo

Reescribir todas las operaciones de acceso a datos del proyecto original
(`TFI_1/Datos`) utilizando Entity Framework Core, sin modificar el proyecto
original y sin alterar el esquema MySQL existente.

La migración es **paralela y comprobable**: ambas capas pueden convivir en el
mismo repositorio y compararse método por método.

### Fuera de alcance

- Capa de presentación (`TFI_1/Presentacion`).
- Capa de negocio (`TFI_1/Negocio`).
- Modelos de dominio originales (`TFI_1/Models`).
- Esquema de base de datos y migraciones.
- `ConexionBD`, `Parametros` y `BackupNegocio` (infraestructura ADO.NET y
  `mysqldump`).

---

## 2. Restricciones y decisiones

| Restricción / decisión | Resultado |
|---|---|
| No modificar ningún archivo del proyecto original | Solo se agregaron dos proyectos nuevos y dos líneas en `TFI_1.slnx`. |
| Usar el mismo framework que el original | `net10.0-windows`. |
| Usar un proveedor MySQL para EF Core | Pomelo 9.0.0. |
| No mezclar versiones de EF Core | Entity Framework Core 9.0.21 en todos los paquetes. |
| No generar migraciones | El `DbContext` mapea el esquema existente de `BaseDatos/peluqueria.sql`. |
| Conservar el comportamiento del original | Mismos filtros, órdenes, agregados, transacciones y bloqueos. |
| Mantener la cadena de conexión original | Se lee de `appsettings.json`, con una plantilla local; el archivo real se ignora. |

### Paquetes

| Paquete | Versión |
|---|---|
| `Pomelo.EntityFrameworkCore.MySql` | 9.0.0 |
| `Microsoft.EntityFrameworkCore` | 9.0.21 |
| `Microsoft.EntityFrameworkCore.Relational` | 9.0.21 |

> Pomelo 9.0.0 es la última versión publicada y no existe una compilación para
> EF Core 10. Por ese motivo se fijó toda la línea en EF Core 9.

---

## 3. Arquitectura de la capa migrada

```text
TFI_1.EFCore
├── ConfiguracionEF.cs          Resolución de conexión y versión del servidor
├── PeluqueriaDbContext.cs      DbContext + Fluent API
├── appsettings.example.json    Plantilla de conexión
├── Entidades/                  POCOs y enumeraciones
├── Dtos/                       Resultados tipados
├── Repositorios/               Operaciones de acceso a datos
└── Utilidades/
    └── TablaExtensiones.cs     Conversión DTO -> DataTable

TFI_1.EFCore.Verificacion
├── Program.cs                  Prueba de humo de solo lectura
└── TFI_1.EFCore.Verificacion.csproj
```

### Flujo de una consulta

```text
Repositorio
  -> new PeluqueriaDbContext()
  -> DbSet<T> / LINQ
  -> Traducción a SQL por Pomelo
  -> MySQL
  -> Entidades o DTOs
  -> Dispose del contexto
```

Cada método de repositorio crea y libera su propio `DbContext`, igual que el
original abría y cerraba una `MySqlConnection` por operación. Esto evita
estados compartidos y mantiene el comportamiento de las transacciones.

---

## 4. Configuración de la conexión

`ConfiguracionEF` resuelve la cadena en este orden:

1. Variable de entorno `TFI_PELUQUERIA_CONNECTION`.
2. `ConnectionStrings:Peluqueria` de `appsettings.json`.
3. Una cadena de ejemplo sin credenciales reales.

```csharp
public static string CadenaConexion => _cadena.Value;

public static MySqlServerVersion VersionServidor => _version.Value;

public static DbContextOptions<PeluqueriaDbContext> CrearOpciones() =>
    new DbContextOptionsBuilder<PeluqueriaDbContext>()
        .UseMySql(CadenaConexion, VersionServidor)
        .Options;
```

`PeluqueriaDbContext.OnConfiguring` usa esa configuración cuando el contexto se
crea sin opciones explícitas:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    if (!optionsBuilder.IsConfigured)
        optionsBuilder.UseMySql(ConfiguracionEF.CadenaConexion,
                                ConfiguracionEF.VersionServidor);
}
```

La versión del servidor se declara como `8.0.46`, que es la instalada. Puede
sobrescribirse con `TFI_MYSQL_VERSION`.

### Cadena utilizada

```text
Server=localhost;Port=3306;Database=peluqueria;Uid=peluqueria_app;
Pwd=********;CharSet=utf8mb4;
```

---

## 5. Modelo de datos y `DbContext`

El `DbContext` expone los 20 `DbSet` correspondientes a las 20 tablas del
esquema:

| Tabla MySQL | Entidad | `DbSet` |
|---|---|---|
| `roles` | `Rol` | `Roles` |
| `usuarios` | `Usuario` | `Usuarios` |
| `clientes` | `Cliente` | `Clientes` |
| `empleados` | `Empleado` | `Empleados` |
| `categorias` | `Categoria` | `Categorias` |
| `items` | `Item` | `Items` |
| `bancos` | `Banco` | `Bancos` |
| `tarjetas_credito` | `TarjetaCredito` | `TarjetasCredito` |
| `ventas` | `Venta` | `Ventas` |
| `detalle_venta` | `DetalleVenta` | `DetallesVenta` |
| `cuentas_corrientes` | `CuentaCorriente` | `CuentasCorrientes` |
| `movimientos_cuenta_corriente` | `MovimientoCuentaCorriente` | `MovimientosCuentaCorriente` |
| `gastos` | `Gasto` | `Gastos` |
| `proveedores` | `Proveedor` | `Proveedores` |
| `ingresos_mercaderia` | `IngresoMercaderia` | `IngresosMercaderia` |
| `ingresos_mercaderia_detalle` | `IngresoMercaderiaDetalle` | `IngresosMercaderiaDetalle` |
| `atenciones` | `Atencion` | `Atenciones` |
| `auditoria` | `Auditoria` | `Auditorias` |
| `cajas` | `Caja` | `Cajas` |
| `caja_movimientos` | `CajaMovimiento` | `CajaMovimientos` |

### 5.1 Configuración Fluent

Se configuró explícitamente:

- Nombres de tabla en minúsculas.
- Nombres de columna en `snake_case`.
- Claves primarias (`id_*`).
- Longitudes máximas de cadenas.
- Precisión decimal: `decimal(12,2)` para importes y `decimal(5,2)` para
  porcentajes.
- Valores por defecto (`activo`, `fecha`, `saldo`, `estado`, etc.).
- Índices únicos y no únicos con sus nombres originales.
- Claves foráneas y comportamiento de borrado.
- Restricciones `CHECK` de `items`, `cajas` y `caja_movimientos`.

Ejemplo:

```csharp
modelBuilder.Entity<Item>(e =>
{
    e.ToTable("items", t =>
    {
        t.HasCheckConstraint("chk_precio", "precio >= 0");
        t.HasCheckConstraint("chk_stock", "...");
        t.HasCheckConstraint("chk_duracion", "...");
        t.HasCheckConstraint("chk_costo", "costo IS NULL OR costo >= 0");
    });

    e.HasKey(x => x.Id);
    e.Property(x => x.Id).HasColumnName("id_item");
    e.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();
    e.Property(x => x.Precio).HasColumnName("precio").HasPrecision(12, 2);
    e.Property(x => x.IdCategoria).HasColumnName("id_categoria");

    e.HasOne(x => x.Categoria)
        .WithMany()
        .HasForeignKey(x => x.IdCategoria)
        .OnDelete(DeleteBehavior.Restrict)
        .HasConstraintName("fk_items_categoria");
});
```

### 5.2 Conversión de enumeraciones

Las columnas `ENUM` de MySQL se mapean a enumeraciones de C# mediante
`HasConversion`.

| Propiedad | Valores MySQL | Enum C# |
|---|---|---|
| `Empleado.Modalidad` | `turnos`, `orden_llegada` | `Modalidad.Turnos`, `Modalidad.OrdenLlegada` |
| `Item.Tipo` | `producto`, `servicio` | `TipoItem.Producto`, `TipoItem.Servicio` |
| `Atencion.Tipo` | `turno`, `llegada` | `TipoAtencion.Turno`, `TipoAtencion.Llegada` |
| `Atencion.Estado` | `pendiente`, `en_atencion`, `finalizada`, `cancelada` | `EstadoAtencion` |
| `MovimientoCuentaCorriente.Tipo` | `cargo`, `abono` | `TipoMovimientoCuentaCorriente` |

Los valores por defecto de columnas convertidas se configuran con el valor del
enum, no con el texto:

```csharp
e.Property(x => x.Tipo)
    .HasColumnName("tipo")
    .HasColumnType("enum('turno', 'llegada')")
    .HasDefaultValue(TipoAtencion.Llegada)
    .HasConversion(
        v => v == TipoAtencion.Turno ? "turno" : "llegada",
        v => v == "turno" ? TipoAtencion.Turno : TipoAtencion.Llegada)
    .IsRequired();
```

> Durante la verificación se detectó que `HasDefaultValue("llegada")` falla
> porque EF espera el tipo CLR de la propiedad (`TipoAtencion`). Se corrigió
> usando `HasDefaultValue(TipoAtencion.Llegada)` y
> `HasDefaultValue(EstadoAtencion.Pendiente)`.

### 5.3 Claves foráneas y borrado

| Relación | Comportamiento |
|---|---|
| `usuarios -> roles` | `Restrict` |
| `items -> categorias` | `Restrict` |
| `tarjetas_credito -> bancos` | `Restrict` |
| `ventas -> clientes/usuarios/empleados/tarjetas` | `Restrict` |
| `detalle_venta -> ventas` | `Cascade` |
| `detalle_venta -> items` | `Restrict` |
| `cuentas_corrientes -> clientes` | `Restrict` |
| `movimientos_cuenta_corriente -> cuentas_corrientes` | `Cascade` |
| `movimientos_cuenta_corriente -> ventas` | `SetNull` |
| `gastos -> usuarios` | `Restrict` |
| `ingresos_mercaderia -> proveedores/usuarios` | `Restrict` |
| `ingresos_mercaderia_detalle -> ingresos_mercaderia` | `Cascade` |
| `atenciones -> clientes/empleados/items` | `Restrict` |
| `auditoria -> usuarios` | `Restrict` |
| `cajas -> usuarios` | `Restrict` |
| `caja_movimientos -> cajas/usuarios` | `Restrict` |

### 5.4 Propiedades calculadas

Se conservaron las propiedades de solo lectura de los modelos originales
(`NombreCompleto`, `Subtotal`, `StockBajo`, `Abierta`, etc.). EF Core las ignora
automáticamente porque no tienen setter, por lo que no generan columnas.

### 5.5 Diferencias de nombres deliberadas

| Entidad | Propiedad | Motivo |
|---|---|---|
| `Atencion` | `Servicio` | Evita chocar con el nombre de la tabla y refleja que `IdItem` apunta a un servicio. |
| `IngresoMercaderia` | `ProveedorEntidad` | La propiedad `Proveedor` ya existe como texto denormalizado en la tabla. |

---

## 6. DTOs y compatibilidad con `DataTable`

El original devolvía `DataTable` para las grillas. La migración devuelve listas
tipadas y ofrece una conversión explícita para no romper la presentación.

| DTO | Método original que reemplaza |
|---|---|
| `VentaListadoDto` | `VentaDatos.Listar` |
| `VentaCabeceraDto` | `VentaDatos.ObtenerCabecera` |
| `VentaDetalleDto` | `VentaDatos.ObtenerDetalle` |
| `CajaListadoDto` | `CajaDatos.Listar` |
| `CajaMovimientoDto` | `CajaDatos.ListarMovimientos` |
| `ResumenVentasDto` | `CajaDatos.ResumenVentas` |
| `MovimientoCuentaCorrienteDto` | `CuentaCorrienteDatos.MovimientosResumen` |
| `IngresoResumenDto` | `IngresoMercaderiaDatos.IngresosResumen` |
| `GastoCategoriaDto` | `GastoDatos.GastosPorCategoria` |
| `ProductoStockDto` | `DashboardDatos.ProductoMayorStock/MenorStock` |
| `UltimaVentaDto` | `DashboardDatos.UltimasVentas` |
| `AuditoriaListadoDto` | `AuditoriaDatos.Listar` |
| `AccionAuditoriaDto` | `AuditoriaDatos.Acciones` |
| `ResumenDto` | `ReporteDatos.Resumen` |
| `VentasPorMedioPagoDto` | `ReporteDatos.VentasPorMedioPago` |
| `ItemVendidoDto` | `ReporteDatos.ProductosMasVendidos` |
| `VentasPorDiaDto` | `ReporteDatos.VentasPorDia` |
| `ClientesComprasDto` | `ReporteDatos.ClientesConMasCompras` |
| `VentasPorUsuarioDto` | `ReporteDatos.VentasPorUsuario` |
| `ServiciosSolicitadosDto` | `ReporteDatos.ServiciosMasSolicitados` |
| `VentasPorEmpleadoDto` | `ReporteDatos.VentasPorEmpleado` |

`TablaExtensiones.ToDataTable<T>()` usa reflexión para crear las columnas con
los mismos nombres que tenían los alias SQL originales:

```csharp
DataTable tabla = new VentaRepositorio()
    .Listar(desde, hasta)
    .ToDataTable();
```

Maneja `null` como `DBNull.Value` y convierte enumeraciones a texto.

---

## 7. Repositorios

Cada clase `*Datos` del original tiene un repositorio equivalente. Todos los
métodos públicos fueron migrados.

### 7.1 Lecturas simples

Patrón general:

```csharp
public List<Cliente> Listar(string? filtro = null)
{
    using var db = new PeluqueriaDbContext();
    IQueryable<Cliente> consulta = db.Clientes.AsNoTracking();

    if (!string.IsNullOrEmpty(filtro))
        consulta = consulta.Where(c =>
            c.Dni.Contains(filtro) ||
            c.Apellido.Contains(filtro) ||
            c.Nombre.Contains(filtro));

    return consulta.OrderBy(c => c.Apellido)
                   .ThenBy(c => c.Nombre)
                   .ToList();
}
```

Se usa `AsNoTracking()` en todas las consultas de solo lectura para evitar el
costo del change tracker.

### 7.2 Altas

```csharp
db.Clientes.Add(nuevo);
db.SaveChanges();
cliente.Id = nuevo.Id;
return nuevo.Id;
```

EF Core obtiene el `AUTO_INCREMENT` y lo asigna a la entidad. Equivale a
`INSERT ...; SELECT LAST_INSERT_ID();`.

### 7.3 Modificaciones

Dos variantes:

- **Entidad cargada:** `Find(id)`, asignación de propiedades y `SaveChanges()`.
  Se usa cuando el original actualizaba varias columnas de una fila.
- **Actualización directa:** `ExecuteUpdate(...)`. Se usa para bajas lógicas y
  cambios de estado de una sola columna, evitando cargar la entidad.

```csharp
db.Clientes
    .Where(c => c.Id == id)
    .ExecuteUpdate(s => s.SetProperty(c => c.Activo, false));
```

### 7.4 Bajas lógicas

No se elimina ningún registro. Se replica el `UPDATE ... SET activo = 0`.

---

## 8. Operaciones transaccionales y concurrencia

### 8.1 `VentaRepositorio.Registrar`

Replica `VentaDatos.Registrar` dentro de una transacción:

1. Calcula el total:
   `sum(precio * cantidad) - descuento + recargo`.
2. Inserta la cabecera en `ventas`.
3. Inserta el detalle en `detalle_venta`.
4. Para cada producto, descuenta stock con una actualización condicional:
   `WHERE id_item = @id AND stock >= @cantidad`.
5. Si no se actualizó ninguna fila, lanza `Stock insuficiente` y revierte todo.
6. Si el medio de pago es `cuenta_corriente`:
   - bloquea la cuenta con `SELECT ... FOR UPDATE`;
   - la crea si no existe;
   - inserta el movimiento de tipo `cargo`;
   - incrementa el saldo.
7. Confirma la transacción.

La actualización condicional de stock evita dejar valores negativos si otro
proceso vendió el mismo producto durante la operación.

### 8.2 `VentaRepositorio.Anular`

1. Bloquea la venta con `SELECT ... FOR UPDATE` y `anulada = 0`.
2. Devuelve el stock solo de los productos.
3. Si fue a cuenta corriente:
   - bloquea la cuenta;
   - inserta un movimiento de tipo `abono`;
   - descuenta el saldo.
4. Marca la venta como anulada, con fecha, usuario y motivo.
5. Verifica que la actualización haya afectado exactamente una fila.
6. Confirma o revierte.

No se borra ningún registro: la venta queda marcada como anulada.

### 8.3 `CuentaCorrienteRepositorio.RegistrarAbono`

Bloquea la cuenta del cliente con `FOR UPDATE`, la crea si no existe, inserta el
movimiento `abono` y descuenta el saldo. Todo dentro de una transacción.

### 8.4 `CajaRepositorio.Abrir`

El original usaba un `INSERT ... SELECT ... WHERE NOT EXISTS` atómico. En EF
Core se reproduce con:

```csharp
using var tx = db.Database.BeginTransaction(IsolationLevel.Serializable);

if (db.Cajas.Any(c => c.Estado == "abierta"))
    return 0;

db.Cajas.Add(caja);
db.SaveChanges();
tx.Commit();
```

El nivel `Serializable` evita que dos aperturas concurrentes inserten dos cajas
abiertas. Devuelve `0` si ya había una.

### 8.5 `CajaRepositorio.Cerrar`

Una sola sentencia `ExecuteUpdate` actualiza:

- usuario de cierre,
- fecha de cierre,
- monto esperado,
- monto contado,
- diferencia,
- observación,
- estado.

Devuelve `true` solo si se actualizó una fila.

### 8.6 `BancoRepositorio.Desactivar` / `Reactivar`

Dentro de una transacción:

1. Actualiza el banco.
2. Actualiza en cascada todas sus tarjetas.

Así se conserva la consistencia del original.

### 8.7 `IngresoMercaderiaRepositorio.Insertar`

Dentro de una transacción:

1. Inserta la cabecera.
2. Inserta cada detalle.
3. Actualiza el stock, el costo y, si corresponde, el precio del ítem.

---

## 9. Traducción de consultas

### 9.1 Rangos de fecha

El original usaba `DATE(columna) BETWEEN @desde AND @hasta`. Eso impide usar
índices sobre la columna. La migración usa rangos semiabiertos:

```csharp
DateTime inicio = desde.Date;
DateTime fin = hasta.Date.AddDays(1);

consulta.Where(x => x.Fecha >= inicio && x.Fecha < fin);
```

El resultado es equivalente y la consulta puede aprovechar el índice.

**Excepción deliberada:** `GastoRepositorio` conserva la comparación contra la
medianoche (`fecha <= hasta.Date`) porque así estaba en el SQL original.

### 9.2 Búsquedas de texto

| Original | EF Core |
|---|---|
| `LIKE CONCAT('%', @filtro, '%')` | `columna.Contains(filtro)` |
| `@filtro = '' OR ...` | `if (!string.IsNullOrEmpty(filtro))` |

### 9.3 Agregados

```csharp
return db.Ventas.AsNoTracking()
    .Where(v => v.Fecha >= inicio && v.Fecha < fin && !v.Anulada)
    .GroupBy(v => v.MedioPago)
    .Select(g => new VentasPorMedioPagoDto
    {
        Pago = g.Key,
        Cantidad = g.Count(),
        Total = g.Sum(v => v.Total),
    })
    .OrderByDescending(x => x.Total)
    .ToList();
```

Los `SUM` sobre conjuntos vacíos se manejan con `(decimal?)` y `?? 0m` para
devolver `0` en lugar de `NULL`.

### 9.4 `Resumen` sin ventas

El original devuelve una fila con ceros porque usa agregados sin `GROUP BY`.
Para conservarlo:

```csharp
ResumenDto? resumen = ...GroupBy(_ => 1)...SingleOrDefault();

return new List<ResumenDto> { resumen ?? new ResumenDto() };
```

### 9.5 Paginación

```csharp
if (desplazamiento is > 0)
    consulta = consulta.Skip(desplazamiento.Value);

if (limite is > 0 and < int.MaxValue)
    consulta = consulta.Take(limite.Value);
```

Equivale a `LIMIT @limite OFFSET @desplazamiento`.

### 9.6 Orden determinista

Cuando el original ordenaba por fecha, se agregó `ThenByDescending(x => x.Id)`
para que la paginación sea estable ante fechas repetidas.

### 9.7 `Math.Max` en `ExisteConflictoHorario`

El original usaba `GREATEST(COALESCE(i.duracion_min, 0), 1)`. EF Core traduce
`Math.Max(a, b)` a `GREATEST`. La consulta se verificó contra MySQL y se ejecuta
correctamente.

### 9.8 Auditoría

El original traducía el código de acción en SQL con `CASE`. EF Core no traduce
esa expresión de forma confiable, así que:

1. Se pagina y materializa el código (`venta`, `abono`, etc.).
2. Se traduce en memoria con `TraducirAccion`.

El resultado es el mismo y el `CASE` no afecta el rendimiento del paginado.

### 9.9 SQL explícito

Solo se usa SQL explícito para bloqueos de fila:

```csharp
db.CuentasCorrientes
    .FromSqlInterpolated($"SELECT * FROM cuentas_corrientes WHERE id_cliente = {idCliente} FOR UPDATE")
    .SingleOrDefault();
```

`FromSqlInterpolated` parametriza los valores y evita inyección SQL. Se usa
únicamente en:

- `VentaRepositorio.Registrar`
- `VentaRepositorio.Anular`
- `CuentaCorrienteRepositorio.RegistrarAbono`

---

## 10. Verificación

### 10.1 Compilación

```powershell
dotnet build TFI_1.slnx
```

Resultado:

```text
Compilación correcta.
    0 Advertencia(s)
    0 Errores
```

### 10.2 Prueba de humo

```powershell
dotnet run --project TFI_1.EFCore.Verificacion
```

La consola `TFI_1.EFCore.Verificacion` es de **solo lectura**. Comprueba:

- `CanConnect`.
- Existencia de las 20 tablas.
- Conteo de cada `DbSet`.
- Todos los métodos de lectura de los 17 repositorios.
- La traducción de `Math.Max` a `GREATEST`.
- La coherencia entre `Reporte.Resumen` y la suma LINQ.
- La conversión de DTOs a `DataTable`.

Resultado:

```text
[OK] CanConnect -> true
[OK] Tablas visibles en el esquema actual -> 20
[OK] ventas -> 5
[OK] atenciones -> 2
[OK] Reporte.Resumen (total coincide con la suma LINQ) -> total 33940,00, ticket 8485,00
[OK] TablaExtensiones.ToDataTable<VentaListadoDto> -> 5 filas, 13 columnas

RESULTADO: todas las comprobaciones pasaron.
```

La consola devuelve código de salida `1` si alguna comprobación falla.

### 10.3 Qué no cubre la prueba

No se ejecutan altas, bajas ni modificaciones para no alterar la base de
desarrollo. Las operaciones de escritura se validaron por compilación y por
revisión de la traducción, pero no se ejecutaron contra datos reales.

---

## 11. Limitaciones conocidas

1. **`BackupNegocio`** usa `mysqldump`; no es una operación de tablas y queda
   fuera de EF Core.
2. **`ConexionBD` y `Parametros`** son infraestructura ADO.NET del proyecto
   original; no se migraron.
3. **Sin migraciones**: el modelo mapea el esquema existente. Si se modifica
   `BaseDatos/peluqueria.sql`, hay que actualizar el `DbContext` manualmente.
4. **Pomelo 9 / EF Core 10**: la combinación actual es estable, pero no existe
   un proveedor Pomelo para EF Core 10. Si aparece, conviene actualizar todos
   los paquetes a la misma línea.
5. **`FOR UPDATE`**: el SQL explícito es específico de MySQL. Una migración a
   otro motor requeriría revisar esos tres métodos.
6. **Integración con WinForms**: la migración es una capa paralela; no se
   reemplazaron las llamadas de `TFI_1/Negocio` por los nuevos repositorios.

---

## 12. Inventario de archivos

| Archivo | Responsabilidad |
|---|---|
| `TFI_1.EFCore.csproj` | Target y paquetes. |
| `appsettings.example.json` | Plantilla de conexión. |
| `ConfiguracionEF.cs` | Resolución de conexión y versión. |
| `PeluqueriaDbContext.cs` | Mapeo completo del esquema. |
| `Entidades/Personas.cs` | `Rol`, `Usuario`, `Cliente`, `Empleado`. |
| `Entidades/Catalogo.cs` | `Categoria`, `Item`, `Banco`, `TarjetaCredito`, `Proveedor`. |
| `Entidades/Ventas.cs` | `Venta`, `DetalleVenta`. |
| `Entidades/CuentaCorriente.cs` | `CuentaCorriente`, `MovimientoCuentaCorriente`. |
| `Entidades/GastosIngresos.cs` | `Gasto`, `IngresoMercaderia`, `IngresoMercaderiaDetalle`. |
| `Entidades/AtencionesAuditoria.cs` | `Atencion`, `Auditoria`. |
| `Entidades/Cajas.cs` | `Caja`, `CajaMovimiento`. |
| `Entidades/Enumeraciones.cs` | Enumeraciones del dominio. |
| `Dtos/VentasDtos.cs` | DTOs de ventas. |
| `Dtos/ReportesDtos.cs` | DTOs de reportes. |
| `Dtos/VariosDtos.cs` | DTOs de caja, cuenta corriente, auditoría y dashboard. |
| `Repositorios/Comun.cs` | Rangos, paginación y limpieza de texto. |
| `Repositorios/UsuarioRepositorio.cs` | `UsuarioDatos`. |
| `Repositorios/ClienteRepositorio.cs` | `ClienteDatos`. |
| `Repositorios/CategoriaRepositorio.cs` | `CategoriaDatos`. |
| `Repositorios/ItemRepositorio.cs` | `ItemDatos`. |
| `Repositorios/EmpleadoRepositorio.cs` | `EmpleadoDatos`. |
| `Repositorios/ProveedorRepositorio.cs` | `ProveedorDatos`. |
| `Repositorios/BancoRepositorio.cs` | `BancoDatos`. |
| `Repositorios/TarjetaCreditoRepositorio.cs` | `TarjetaCreditoDatos`. |
| `Repositorios/VentaRepositorio.cs` | `VentaDatos`. |
| `Repositorios/CuentaCorrienteRepositorio.cs` | `CuentaCorrienteDatos`. |
| `Repositorios/CajaRepositorio.cs` | `CajaDatos`. |
| `Repositorios/GastoRepositorio.cs` | `GastoDatos`. |
| `Repositorios/IngresoMercaderiaRepositorio.cs` | `IngresoMercaderiaDatos`. |
| `Repositorios/AtencionRepositorio.cs` | `AtencionDatos`. |
| `Repositorios/AuditoriaRepositorio.cs` | `AuditoriaDatos`. |
| `Repositorios/DashboardRepositorio.cs` | `DashboardDatos`. |
| `Repositorios/ReporteRepositorio.cs` | `ReporteDatos`. |
| `Utilidades/TablaExtensiones.cs` | Conversión DTO -> `DataTable`. |
| `TFI_1.EFCore.Verificacion/Program.cs` | Prueba de humo de solo lectura. |

El código completo de todos los archivos está en
[`CODIGO_COMPLETO.md`](CODIGO_COMPLETO.md).

---

## 13. Uso

### Consulta directa

```csharp
using TFI_1.EFCore;

using var db = new PeluqueriaDbContext();

var ventas = db.Ventas
    .AsNoTracking()
    .Where(v => v.Fecha >= DateTime.Today && !v.Anulada)
    .OrderByDescending(v => v.Fecha)
    .ToList();
```

### Uso de un repositorio

```csharp
using TFI_1.EFCore.Repositorios;

var repo = new VentaRepositorio();

int cantidad = repo.Contar(desde, hasta);

var filas = repo.Listar(desde, hasta, limite: 50);
var cabecera = repo.ObtenerCabecera(ventaId);
var detalle = repo.ObtenerDetalle(ventaId);
```

### Conversión para una grilla existente

```csharp
using TFI_1.EFCore.Utilidades;

dgvVentas.DataSource = new VentaRepositorio()
    .Listar(desde, hasta)
    .ToDataTable();
```

---

## 14. Referencias

- Esquema: `BaseDatos/peluqueria.sql`.
- Capa original: `TFI_1/Datos/`.
- Modelos originales: `TFI_1/Models/`.
- Cadena de conexión original: `TFI_1/App.config`.
- Código completo de la migración: [`CODIGO_COMPLETO.md`](CODIGO_COMPLETO.md).
- Resumen rápido: [`README.md`](README.md).
