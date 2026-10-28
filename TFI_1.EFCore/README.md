# TFI_1.EFCore

Capa de acceso a datos paralela al proyecto original `TFI_1`, reescrita con
**Entity Framework Core 9 + Pomelo MySQL**. El proyecto original no se modifica:
esta carpeta se agrega a la solución como un proyecto independiente.

## Documentación

- [`DOCUMENTACION_MIGRACION.md`](DOCUMENTACION_MIGRACION.md): documentación
  técnica completa de la migración de la capa de acceso a datos.
- [`CODIGO_COMPLETO.md`](CODIGO_COMPLETO.md): todo el código fuente de la
  migración en un único archivo Markdown.

## Paquetes

| Paquete | Versión | Motivo |
|---|---|---|
| `Pomelo.EntityFrameworkCore.MySql` | 9.0.0 | Proveedor MySQL; es la última versión publicada y no existe build para EF Core 10. |
| `Microsoft.EntityFrameworkCore` | 9.0.21 | Se fija a la misma línea 9 para no mezclar versiones con Pomelo. |
| `Microsoft.EntityFrameworkCore.Relational` | 9.0.21 | API relacional (`ExecuteUpdate`, `FromSqlInterpolated`, etc.). |

Marco de destino: `net10.0-windows`, igual que `TFI_1.csproj`.

## Estructura

```
TFI_1.EFCore/
  PeluqueriaDbContext.cs      DbContext + Fluent API (tablas, columnas, FKs, índices, checks)
  ConfiguracionEF.cs          Resolución de cadena de conexión y versión del servidor
  appsettings.example.json    Plantilla de conexión (copiar como appsettings.json)
  Entidades/                  POCOs y enumeraciones
  Dtos/                       Resultados tipados que reemplazan los DataTable
  Repositorios/               Un repositorio por clase *Datos del proyecto original
  Utilidades/TablaExtensiones.cs  DTO -> DataTable para las grillas existentes
```

## Conexión

`ConfiguracionEF` resuelve la cadena en este orden:

1. Variable de entorno `TFI_PELUQUERIA_CONNECTION`.
2. `ConnectionStrings:Peluqueria` de `appsettings.json` (copiado a la salida).
3. Una cadena de ejemplo sin credenciales reales.

Para desarrollo local, copiar `appsettings.example.json` como
`appsettings.json` y completar la clave de MySQL. El archivo real esta
ignorado por Git.

La versión del servidor se declara como `8.0.46` (la instalada); se puede
sobrescribir con `TFI_MYSQL_VERSION` (por ejemplo `8.0.46`).

```csharp
using var db = new PeluqueriaDbContext();
bool conecta = db.Database.CanConnect();

var ventas = new VentaRepositorio().Listar(DateTime.Today, DateTime.Today);
```

## Compilar y verificar

```powershell
dotnet build TFI_1.slnx
dotnet run --project TFI_1.EFCore.Verificacion
```

`TFI_1.EFCore.Verificacion` es una consola de solo lectura: comprueba la
conexión, los 20 `DbSet`, las traducciones LINQ de todos los repositorios y la
conversión a `DataTable`. Devuelve código de salida 1 si alguna comprobación falla.

## Equivalencias principales

| Operación original (`TFI_1/Datos`) | Equivalente EF Core |
|---|---|
| `MySqlConnection` + `MySqlCommand` + `MySqlDataReader` | `PeluqueriaDbContext` + LINQ + `ToList()` |
| `SELECT ... WHERE ...` | `.Where(...)` / `.AsNoTracking()` |
| `INNER JOIN` / `LEFT JOIN` | `.Include(...)`, navegaciones y `join` LINQ |
| `GROUP BY` + `SUM` / `COUNT` / `AVG` | `.GroupBy(...).Select(g => new Dto { ... })` |
| `INSERT` + `LAST_INSERT_ID()` | `db.Add(...)` + `SaveChanges()` (ID generado) |
| `UPDATE ... WHERE` | `ExecuteUpdate(...)` o entidad cargada + `SaveChanges()` |
| `DELETE` lógico (`activo = 0`) | `ExecuteUpdate(s => s.SetProperty(...))` |
| Transacciones manuales | `db.Database.BeginTransaction()` / `Commit()` / `Rollback()` |
| `SELECT ... FOR UPDATE` | `FromSqlInterpolated(...)` (único SQL explícito, para bloqueo de fila) |
| `DataTable` devuelto a las grillas | `List<Dto>` + `TablaExtensiones.ToDataTable<T>()` |

Ejemplo lado a lado:

```csharp
// Original: ClienteDatos.Listar
// SELECT ... FROM clientes
// WHERE @filtro = '' OR dni LIKE ... OR apellido LIKE ... OR nombre LIKE ...
// ORDER BY apellido, nombre;

// EF Core: ClienteRepositorio.Listar
using var db = new PeluqueriaDbContext();
return db.Clientes.AsNoTracking()
    .Where(c => c.Dni.Contains(filtro) ||
                c.Apellido.Contains(filtro) ||
                c.Nombre.Contains(filtro))
    .OrderBy(c => c.Apellido).ThenBy(c => c.Nombre)
    .ToList();
```

## Decisiones de comportamiento

- Se conservan los filtros, órdenes, paginación y columnas calculadas del original.
- `GastoRepositorio` mantiene la comparación contra la medianoche
  (`fecha <= hasta.Date`) tal como el SQL original, aunque otros rangos usan
  `[desde, hasta + 1 día)`.
- `ReporteRepositorio.Resumen` devuelve una fila con ceros cuando no hay ventas,
  igual que el agregado sin `GROUP BY` original.
- `VentasPorEmpleado` no filtra ventas anuladas, igual que el original.
- La traducción del código de auditoría se hace en memoria después de paginar.
- `CajaRepositorio.Abrir` usa una transacción `Serializable` para reproducir el
  `INSERT ... WHERE NOT EXISTS` atómico del original.

## Fuera del alcance de EF Core

- `ConexionBD` y `Parametros`: son infraestructura ADO.NET del proyecto original.
- `BackupNegocio`: ejecuta `mysqldump`, no es una operación de tablas.
- No se generan migraciones: el `DbContext` mapea el esquema existente de
  `BaseDatos/peluqueria.sql`.
