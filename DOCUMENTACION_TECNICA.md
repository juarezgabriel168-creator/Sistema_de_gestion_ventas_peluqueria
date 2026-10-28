# Documentación técnica, funcional y de defensa — TFI_1

**Sistema de Gestión de Peluquería**  
Trabajo Final Integrador — Programación II  
Tecnología: **C# · Windows Forms · .NET 10 · MySQL 8.x**  
Fecha límite de presentación: **5/11/2026**

---

## Índice

1. [Propósito y alcance del documento](#1-propósito-y-alcance-del-documento)
2. [Resumen funcional del sistema](#2-resumen-funcional-del-sistema)
3. [Arquitectura por capas](#3-arquitectura-por-capas)
4. [Punto de entrada y configuración](#4-punto-de-entrada-y-configuración)
5. [Componentes transversales](#5-componentes-transversales)
6. [Capa de modelos](#6-capa-de-modelos)
7. [Capa de acceso a datos](#7-capa-de-acceso-a-datos)
8. [Capa de negocio](#8-capa-de-negocio)
9. [Capa de presentación](#9-capa-de-presentación)
10. [Base de datos: scripts SQL](#10-base-de-datos-scripts-sql)
11. [Validaciones, errores y seguridad](#11-validaciones-errores-y-seguridad)
12. [Matriz de cumplimiento de la consigna](#12-matriz-de-cumplimiento-de-la-consigna)
13. [Pruebas y verificación](#13-pruebas-y-verificación)
14. [Guía de defensa y demostración](#14-guía-de-defensa-y-demostración)
15. [Observaciones y mejoras detectadas](#15-observaciones-y-mejoras-detectadas)
16. [Apéndice: inventario de archivos](#16-apéndice-inventario-de-archivos)

---

## 1. Propósito y alcance del documento

Este documento explica **qué hace cada bloque importante del sistema**, **para qué sirve cada variable relevante** y **cómo se cumple cada punto de la consigna** del segundo parcial.

El proyecto implementa una peluquería que:

- vende **servicios** (corte, coloración, brushing, tratamientos);
- vende **productos físicos** con control de stock;
- agenda **turnos** y registra **órdenes de llegada**;
- administra **clientes, empleados, usuarios, proveedores, categorías y tarjetas**;
- controla **caja**, **cuenta corriente**, **gastos** y **mercadería**;
- genera **reportes**, **ticket imprimible**, **auditoría** y **copia de seguridad**.

### Convenciones

- Se documenta el código de producción y los dos scripts SQL.
- Los archivos `*.Designer.cs` no se transcriben línea por línea: contienen la declaración y ubicación visual de los controles generados por el diseñador. La lógica que los usa está en los `*.cs`.
- En el árbol de trabajo actual hay **37 formularios funcionales**, todos con su archivo `.Designer.cs` y su `.resx`.
- El relevamiento contó **617 declaraciones de controles/componentes** dentro de los diseñadores. Los formularios principales (`FormPrincipal`, `FormNuevaVenta`, `FormGastos`, `FormDashboard`) superan los 25 controles cada uno.
- Los ajustes de tamaño y anclaje que se hacen desde el `.cs` **no crean controles**: solo acomodan controles que ya existen en el diseñador. Esto es importante para el requisito de que todo se vea en la vista **Diseño** de Visual Studio.

---

## 2. Resumen funcional del sistema

### 2.1 Módulos y pantallas

| Módulo | Pantalla principal | Función |
|---|---|---|
| Seguridad | `FormLog` | Inicio de sesión contra MySQL. |
| Seguridad | `FormPrincipal` | Menú MDI, visibilidad según rol, cierre de sesión. |
| Inicio | `FormDashboard` | Indicadores del día, stock bajo, últimas ventas y gráfico. |
| Ventas | `FormNuevaVenta` | Carrito, descuento, medio de pago, recargo de tarjeta y registro transaccional. Requiere una caja abierta. |
| Ventas | `FormHistorialVentas` | Búsqueda paginada, filtros, detalle, ticket y anulación. |
| Ventas | `FormDetalleVenta` | Cabecera, ítems, impresión y anulación. |
| Ventas | `FormTicket` | Comprobante no fiscal, formato de 37 caracteres e impresión. |
| Ventas | `FormAnularVenta` | Motivo obligatorio y aviso de efectos de la anulación. |
| Caja | `FormCaja` | Apertura, ingresos, egresos, resumen y cierre. |
| Caja | `FormCerrarCaja` | Efectivo contado y cálculo de diferencia en vivo. |
| Caja | `FormHistorialCaja` | Historial paginado de cierres. |
| Productos | `FormItems` | Listado, búsqueda, filtros, stock bajo, alta y edición. |
| Productos | `FormItemEditar` | Alta/edición de producto o servicio. |
| Productos | `FormCategorias` | Listado y administración de categorías. |
| Productos | `FormCategoriaEditar` | Alta/edición de categoría. |
| Clientes | `FormClientes` | Listado, búsqueda y activación/desactivación. |
| Clientes | `FormClienteEditar` | Alta/edición de cliente. |
| Clientes | `FormCuentaCorriente` | Saldo, movimientos y registro de abonos. |
| Turnos | `FormAtenciones` | Listado y filtro por estado. |
| Turnos | `FormAtencionEditar` | Alta/edición de turno u orden de llegada. |
| Administración | `FormUsuarios` | Listado y estado de usuarios. |
| Administración | `FormUsuarioEditar` | Alta/edición, rol y política de contraseña. |
| Administración | `FormEmpleados` | Listado de peluqueros. |
| Administración | `FormEmpleadoEditar` | Alta/edición y modalidad de atención. |
| Administración | `FormTarjetaDeCredito` | Bancos, tarjetas, recargos y cuotas. |
| Administración | `FormBancoEditar` | Alta/edición de banco. |
| Administración | `FormTarjetaEditar` | Alta/edición de tarjeta. |
| Gastos | `FormGastos` | Alta, edición y consulta de gastos. |
| Productos | `FormProveedores` | Listado y estado de proveedores. |
| Productos | `FormProveedorEditar` | Alta/edición de proveedor. |
| Productos | `FormIngresoMercaderia` | Compra de productos, stock y costo. |
| Productos | `FormHistorialMercaderia` | Historial paginado y detalle de ingresos. |
| Administración | `FormAuditoria` | Consulta paginada de acciones sensibles. |
| Administración | `FormBackup` | Generación de copia `.sql` con `mysqldump`. |
| Reportes | `FormReporteDiario` | Resumen del día, medios de pago, más vendidos, gastos, empleados y anulaciones. |
| Reportes | `FormReporteMensual` | Evolución por día, medios de pago, gastos, empleados y anulaciones. |
| Reportes | `FormReporteAvanzado` | Clientes con más compras, ventas por usuario y servicios más solicitados. |

### 2.2 Roles

| Rol | ID | Alcance |
|---|---:|---|
| Administrador | 1 | Todo el sistema. |
| Vendedor | 2 | Ventas, clientes, productos y turnos. |
| Supervisor | 3 | Ventas, clientes, productos, turnos y reportes. |

La restricción se aplica en dos niveles:

1. **Visibilidad del menú** en `FormPrincipal_Load`.
2. **Verificación defensiva** en cada evento administrativo con `ExigirAdministrador()`.

Esto evita que una opción oculta pueda abrirse por un camino indirecto.

---

## 3. Arquitectura por capas

### 3.1 Diagrama

```text
┌──────────────────────────────────────────────────────────────┐
│ Presentacion                                                │
│ Formularios WinForms, controles, eventos, validación de UI  │
└───────────────────────────┬──────────────────────────────────┘
                            │ llama a
┌───────────────────────────▼──────────────────────────────────┐
│ Negocio                                                     │
│ Reglas, permisos, sesión, auditoría, cálculos, transacciones│
└───────────────────────────┬──────────────────────────────────┘
                            │ llama a
┌───────────────────────────▼──────────────────────────────────┐
│ Datos                                                       │
│ MySqlConnection, comandos, parámetros, DataTable, mapeo     │
└───────────────────────────┬──────────────────────────────────┘
                            │ SQL
┌───────────────────────────▼──────────────────────────────────┐
│ MySQL 8.x — base "peluqueria"                               │
└──────────────────────────────────────────────────────────────┘

Models: clases compartidas por las tres capas.
Validaciones, Sesion, Configuracion y Mensajes: soporte transversal.
```

### 3.2 Responsabilidades

| Capa / carpeta | Responsabilidad | No debería hacer |
|---|---|---|
| `Models` | Representar entidades, propiedades calculadas y reglas de dominio simples. | Abrir conexiones o mostrar mensajes. |
| `Datos` | Ejecutar SQL, mapear filas a objetos, manejar transacciones. | Decidir permisos o mostrar formularios. |
| `Negocio` | Validar reglas, aplicar permisos, auditar y coordinar operaciones. | Tener controles WinForms. |
| `Presentacion` | Mostrar datos, capturar eventos y dar mensajes claros. | Concentrar la lógica central o el SQL. |
| `BaseDatos` | Crear el esquema, restricciones e índices. | Contener lógica de aplicación. |

### 3.3 Patrones usados

- **Separación en capas** con dependencia descendente.
- **DTO/Modelos** con propiedades calculadas y constructores.
- **DAO/Repositorio** en `Datos`.
- **Servicio de aplicación** en `Negocio`.
- **Sesión global controlada** (`Sesion`).
- **Eliminación lógica** con `activo`.
- **Transacciones** en venta, anulación, abono e ingreso de mercadería.
- **Parámetros tipados** en lugar de concatenar SQL.
- **Auditoría** de operaciones sensibles.
- **Paginación** en historiales.
- **Manejo centralizado de errores** con `Mensajes`.

### 3.4 Flujo de una operación

Ejemplo: registrar una venta.

```text
FormNuevaVenta
  → arma Venta + List<DetalleVenta>
  → VentaNegocio.Registrar
      → valida cliente, usuario, ítems, medio de pago y descuento
      → relee precios y stock desde ItemDatos
      → calcula recargo de tarjeta
      → VentaDatos.Registrar (transacción)
          → INSERT ventas
          → INSERT detalle_venta
          → UPDATE items.stock
          → si es cuenta corriente: cargo + saldo
          → COMMIT
      → AuditoriaNegocio.Registrar
  → ofrece FormTicket
```

---

## 4. Punto de entrada y configuración

### 4.1 `Program.cs`

```csharp
[STAThread]
static void Main()
{
    ApplicationConfiguration.Initialize();
    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
    Application.ThreadException += (s, e) => Mensajes.ErrorGlobal(e.Exception);
    AppDomain.CurrentDomain.UnhandledException += ...;
    try { Application.Run(new FormLog()); }
    catch (Exception ex) { Mensajes.ErrorGlobal(ex); }
}
```

| Elemento | Para qué sirve |
|---|---|
| `[STAThread]` | Requiere WinForms para usar COM y portapapeles de forma segura. |
| `ApplicationConfiguration.Initialize()` | Aplica configuración generada por .NET (DPI, fuente, estilo). |
| `SetUnhandledExceptionMode(CatchException)` | Evita que una excepción no controlada cierre el proceso sin mensaje. |
| `Application.ThreadException` | Captura errores del hilo de interfaz. |
| `AppDomain.CurrentDomain.UnhandledException` | Captura errores de otros hilos o del dominio. |
| `Application.Run(new FormLog())` | Inicia el bucle de mensajes con el login como pantalla inicial. |
| `try/catch` final | Si falla el arranque, muestra/loguea el error. |

### 4.2 `App.config`

| Clave | Uso |
|---|---|
| `connectionStrings/Peluqueria` | Cadena de conexión MySQL. |
| `NombreNegocio` | Título de la ventana y encabezado del ticket. |
| `LimiteDeudaCuentaCorriente` | Umbral de aviso de deuda; no bloquea la venta. |
| `RutaMysqldump` | Ruta al ejecutable de copia de seguridad. |

La contraseña real **no se documenta** en este archivo. En el repositorio se distribuye `App.config.example`; el `App.config` real queda fuera de Git. La aplicación usa un usuario MySQL con permisos mínimos, no `root`.

### 4.3 `ConexionBD.cs`

- `Cadena`: campo estático calculado una sola vez.
- `ObtenerCadena()`: lee `ConfigurationManager.ConnectionStrings["Peluqueria"]` y lanza `ConfigurationErrorsException` si no existe.
- `ObtenerConexion()`: devuelve una conexión nueva; cada operación la abre y la cierra con `using`.
- `CadenaConexion`: expone la cadena para que `BackupNegocio` construya el archivo de opciones de `mysqldump`.

**Por qué es importante:** centraliza la conexión y evita repetir la cadena en cada consulta.

### 4.4 `Parametros.cs`

Crea `MySqlParameter` con tipo explícito:

| Método | Tipo SQL | Uso |
|---|---|---|
| `Entero` | `Int32` | IDs y cantidades. |
| `EnteroNulo` | `Int32` nullable | Filtros opcionales. |
| `Texto` | `VarChar` | Nombres, estados, conceptos. |
| `Fecha` | `DateTime` | Fechas con hora. |
| `FechaNula` | `DateTime` nullable | Cierres o filtros abiertos. |
| `SoloFecha` | `Date` | Comparaciones por día. |
| `SoloFechaNula` | `Date` nullable | Rangos opcionales. |
| `Importe` | `Decimal(12,2)` | Precios, totales, saldos. |
| `ImporteNulo` | `Decimal(12,2)` nullable | Costos o montos opcionales. |
| `Booleano` | `Byte` | `activo` representado como 1/0. |

**Ventaja:** evita `AddWithValue`, que deja que el proveedor adivine el tipo y puede fallar con decimales, fechas o nulos.

### 4.5 `Configuracion.cs`

| Miembro | Función |
|---|---|
| `NombreNegocioPorDefecto` | Valor de respaldo. |
| `LimiteDeudaPorDefecto` | `50000m`. |
| `NombreNegocio` | Lee `appSettings`; si falta, usa el valor por defecto. |
| `LimiteDeudaCuentaCorriente` | Lee y convierte el umbral con cultura actual. |
| `RutaMysqldump` | Lee la ruta del ejecutable. |
| `LeerTexto` / `LeerDecimal` | Funciones privadas de lectura segura. |

**Beneficio:** la aplicación funciona aunque falte una clave o tenga un valor inválido.

---

## 5. Componentes transversales

### 5.1 `Validaciones.cs`

Es la **única fuente de verdad** para reglas compartidas.

| Miembro | Tipo | Regla |
|---|---|---|
| `LargoDni` | constante `8` | Longitud exacta del DNI. |
| `TextoVacio` | consulta | `null`, vacío o solo espacios. |
| `DniValido` | consulta | 8 caracteres, todos dígitos. |
| `EmailValido` | consulta | Opcional; si existe, un solo `@`, punto posterior y sin espacios. |
| `ImporteValido` | consulta + `out` | Decimal válido y `>= 0`. |
| `ImportePositivo` | consulta + `out` | Decimal válido y `> 0`. |
| `EsImportePositivo` | consulta | `> 0`. |
| `EnteroNoNegativo` | consulta + `out` | Entero `>= 0`. |
| `EnteroPositivo` | consulta + `out` | Entero `> 0`. |
| `RangoDeFechasValido` | consulta | `desde <= hasta`. |
| `FechaNoFutura` | consulta | Fecha `<= hoy`. |
| `TurnoNoPasado` | consulta | Tolerancia de 5 minutos. |
| `ExigirTexto` | lanza `ArgumentException` | Campo obligatorio. |
| `ExigirDni` | lanza | DNI válido. |
| `ExigirEmail` | lanza | Email válido. |
| `ExigirLongitudMaxima` | lanza | No supera el máximo. |
| `ExigirImporteNoNegativo` | lanza | Precio/costo no negativo. |
| `ExigirImportePositivo` | lanza | Monto mayor a cero. |
| `ExigirFechaNoFutura` | lanza | Gasto no futuro. |

**Patrón:** los modelos usan setters que llaman a `Validaciones`; la capa de negocio repite las reglas críticas antes de persistir. La UI también valida para dar respuesta inmediata. No hay reglas contradictorias porque todas llaman a la misma clase.

### 5.2 `Sesion.cs`

| Miembro | Para qué sirve |
|---|---|
| `RolAdministrador`, `RolVendedor`, `RolSupervisor` | IDs fijos que coinciden con el script SQL. |
| `UsuarioActual` | Usuario autenticado en memoria. |
| `HaySesion` | Comprueba si hay usuario. |
| `RolId` | ID del rol o 0. |
| `NombreUsuario` | Nombre para mostrar y auditar. |
| `RolNombre` | Nombre legible del rol. |
| `EsAdministrador` | Permiso total. |
| `EsSupervisor` | Permiso de reportes. |
| `PuedeVender` | Administrador o vendedor. |
| `PuedeVerReportes` | Administrador o supervisor. |
| `CerrarSesion()` | Limpia `UsuarioActual`. |

### 5.3 `Mensajes.cs`

Unifica la salida de errores:

- `Error(owner, mensaje, ex)`: muestra un texto contextual y el detalle.
- `ErrorGlobal(ex)`: usado por `Program.cs`; registra y muestra.
- `Describir(ex)`: traduce excepciones conocidas (MySQL, red, permisos) a texto comprensible.
- `RegistrarEnArchivo(ex)`: escribe en `%LocalAppData%\Peluqueria\errores.log`.

**Para qué sirve:** el usuario no ve un stack trace crudo y el equipo puede revisar el log.

---

## 6. Capa de modelos

Los modelos están en `TFI_1/Models`. Se usan realmente en las capas de datos, negocio y presentación.

### 6.1 Enumeraciones

| Enum | Valores | Significado |
|---|---|---|
| `TipoAtencion` | `Turno`, `Llegada` | Cómo se atiende al cliente. |
| `EstadoAtencion` | `Pendiente`, `EnAtencion`, `Finalizada`, `Cancelada` | Ciclo de vida. |
| `TipoItem` | `Servicio`, `Producto` | Distingue servicio de producto físico. |
| `TipoMovimientoCC` | `Cargo`, `Abono` | Suma o resta deuda. |
| `Modalidad` | `Turnos`, `OrdenLlegada` | Modalidad del empleado. |

### 6.2 `Atencion`

**Propósito:** representar un turno o una orden de llegada.

| Propiedad | Para qué sirve |
|---|---|
| `Id` | PK de `atenciones`. |
| `Cliente` | Relación con el cliente. |
| `Empleado` | Peluquero asignado. |
| `Servicio` | Ítem de tipo servicio. |
| `Tipo` | Turno o llegada. |
| `FechaHora` | Fecha y hora programada/registrada. |
| `Fecha` | Texto `dd/MM/yyyy` para grillas. |
| `Hora` | Texto `HH:mm` para grillas. |
| `ClienteNombre` | Nombre completo seguro ante null. |
| `EmpleadoNombre` | Nombre completo seguro ante null. |
| `ServicioNombre` | Nombre del servicio. |
| `Estado` | Estado actual. |
| `TipoNombre` | Texto legible del tipo. |
| `EstadoNombre` | Texto legible del estado. |
| `Cancelar()` | Solo permite cancelar si no está finalizada. |

**Regla de dominio:** una atención finalizada no se cancela desde el modelo.

### 6.3 `Banco`

- `Id`: PK.
- `Nombre`: nombre único.
- `Activo`: baja lógica.
- `ToString()`: devuelve el nombre para que los ComboBox lo muestren directamente.

### 6.4 `Caja`

**Propósito:** turno de caja.

| Propiedad | Para qué sirve |
|---|---|
| `Id` | PK. |
| `IdUsuarioApertura` | Usuario que abrió. |
| `UsuarioApertura` | Nombre traído por JOIN. |
| `FechaApertura` | Inicio del turno. |
| `MontoInicial` | Fondo inicial. |
| `IdUsuarioCierre` | Usuario que cerró. |
| `UsuarioCierre` | Nombre traído por JOIN. |
| `FechaCierre` | Fin del turno. |
| `MontoEsperado` | Calculado al cerrar. |
| `MontoContado` | Declarado por el cajero. |
| `Diferencia` | `contado - esperado`. |
| `Observacion` | Nota de cierre. |
| `Estado` | `abierta` o `cerrada`. |
| `Abierta` | Propiedad calculada. |

### 6.5 `Categoria`

- `_nombre`: campo privado que respalda la propiedad.
- `Id`: PK.
- `Activo`: baja lógica.
- `Nombre`: setter que exige texto y aplica `Trim()`.
- Constructor vacío y constructor con nombre.

**Encapsulamiento:** el nombre no puede quedar vacío ni con espacios externos.

### 6.6 `Cliente`

| Miembro | Para qué sirve |
|---|---|
| `_dni` | Respaldo validado. |
| `Id` | PK. |
| `Nombre`, `Apellido` | Datos personales. |
| `Telefono` | Contacto opcional. |
| `Activo` | Baja lógica. |
| `Dni` | Setter que exige 8 dígitos. |
| `NombreCompleto` | `Apellido, Nombre`. |
| `DniConsumidorFinal` | Constante `"00000000"`. |
| `EsConsumidorFinal` | Evita cuenta corriente para ese cliente. |
| Constructores | Facilitan la creación en código y pruebas. |

### 6.7 `CuentaCorriente`

- `Id`: PK.
- `IdCliente`: dueño de la cuenta.
- `Saldo`: deuda acumulada.
- `FechaActualizacion`: último movimiento.

### 6.8 `DetalleVenta`

**Propósito:** una línea de la venta.

| Miembro | Para qué sirve |
|---|---|
| `Id` | PK del detalle. |
| `Item` | Producto o servicio vendido. |
| `Cantidad` | Unidades, privada para forzar constructor. |
| `Precio` | Precio congelado al momento de vender. |
| `Subtotal` | `Precio * Cantidad`. |
| Constructor `(Item, int)` | Valida cantidad positiva y crea la línea. |

**Decisión de diseño:** el precio se guarda en el detalle para que un cambio posterior del precio del ítem no altere ventas históricas.

### 6.9 `Empleado`

| Miembro | Para qué sirve |
|---|---|
| `Id`, `Nombre`, `Apellido` | Identificación. |
| `Modalidad` | Turnos u orden de llegada. |
| `Activo` | Baja lógica. |
| `AtiendeConTurno()` | `Modalidad == Turnos`. |
| `ModalidadTexto` | Texto legible. |
| `NombreCompleto` | `Apellido, Nombre`. |

### 6.10 `Gasto`

- `Id`: PK.
- `Fecha`: fecha del gasto.
- `Concepto`: obligatorio.
- `Monto`: importe positivo.
- `Categoria`: agrupación libre.
- `Observacion`: detalle.
- `IdUsuario`: quién lo cargó.
- `NombreUsuario`: nombre traído por JOIN.
- `FechaAlta`: auditoría temporal.

### 6.11 `IngresoMercaderia`

**Propósito:** cabecera de una compra de mercadería.

| Miembro | Para qué sirve |
|---|---|
| `Id` | PK. |
| `IdProveedor` | FK. |
| `Fecha` | Fecha del ingreso. |
| `Proveedor` | Nombre congelado como snapshot. |
| `Total` | Suma de subtotales. |
| `IdUsuario` | Quién registró. |
| `NombreUsuario` | Nombre por JOIN. |
| `Observacion` | Nota. |
| `Detalles` | Composición de líneas. |
| `AgregarDetalle(...)` | Crea y agrega una línea. |

**Por qué se guarda el nombre del proveedor además de la FK:** si el proveedor se renombra, el historial conserva el nombre que tenía al momento del ingreso.

### 6.12 `IngresoMercaderiaDetalle`

- `Id`: PK.
- `Item`: producto.
- `Cantidad`: unidades ingresadas.
- `CostoUnitario`: costo de compra.
- `Subtotal`: `CostoUnitario * Cantidad`.
- Constructor con validación.

### 6.13 `Item`

**Propósito:** producto o servicio.

| Miembro | Para qué sirve |
|---|---|
| `_precio` | Respaldo del precio validado. |
| `Id` | PK. |
| `Nombre` | Nombre del ítem. |
| `Tipo` | Producto o servicio. |
| `DuracionMin` | Solo servicios; minutos. |
| `Stock` | Solo productos; unidades. |
| `StockMinimo` | Umbral de reposición. |
| `Categoria` | Relación obligatoria. |
| `CategoriaNombre` | Nombre para grillas. |
| `Activo` | Baja lógica. |
| `Precio` | Setter que prohíbe negativos. |
| `Costo` | Costo de compra del producto. |
| `EsProducto` | `Tipo == Producto`. |
| `StockBajo` | Producto con `Stock <= StockMinimo`. |
| `HayStockPara(cantidad)` | Servicio siempre disponible; producto según stock. |
| `DescontarStock(cantidad)` | Valida y descuenta en memoria. |

**Nota:** el descuento real de stock se hace en SQL dentro de la transacción de venta; `DescontarStock` se usa en el dominio y las pruebas.

### 6.14 `MovimientoCuentaCorriente`

- `Id`: PK.
- `IdCuenta`: cuenta afectada.
- `Fecha`: momento.
- `Tipo`: cargo o abono.
- `Monto`: importe.
- `Concepto`: descripción.
- `IdVenta`: venta relacionada, si corresponde.
- `IdUsuario`: responsable.
- `TipoTexto`: “Cargo” o “Abono”.

### 6.15 `Proveedor`

- `Id`: PK.
- `Nombre`: obligatorio y único.
- `Cuit`, `Telefono`, `Email`, `Direccion`: opcionales.
- `Activo`: baja lógica.
- `ToString()`: nombre para ComboBox.

### 6.16 `Rol`

- `Id`, `Nombre`.
- Constructores vacío y con parámetros.

### 6.17 `TarjetaCredito`

| Miembro | Para qué sirve |
|---|---|
| `Id` | PK. |
| `IdBanco` | FK. |
| `NombreBanco` | Nombre por JOIN, no se guarda en la tabla de tarjetas. |
| `TipoTarjeta` | Visa, Mastercard, etc. |
| `PorcentajeRecargo` | Interés que se aplica a la venta. |
| `CuotasMax` | Máximo permitido. |
| `Activo` | Baja lógica. |
| `FechaAlta` | Auditoría. |
| `DescripcionCorta` | `Banco - Tipo`. |

### 6.18 `Usuario`

| Miembro | Para qué sirve |
|---|---|
| `_nombreUsuario` | Respaldo validado. |
| `Id` | PK. |
| `NombreUsuario` | Setter que exige texto y aplica `Trim()`. |
| `Contrasena` | Guarda el hash BCrypt, nunca texto plano. |
| `Rol` | Relación obligatoria. |
| `RolNombre` | Texto para grillas y menú. |
| `Activo` | Baja lógica. |
| `EsAdministrador()` | Compara el nombre del rol. |

### 6.19 `Venta`

| Miembro | Para qué sirve |
|---|---|
| `Id` | PK. |
| `Recargo` | Interés de tarjeta. |
| `Cliente` | Cliente de la venta. |
| `Usuario` | Usuario que registra. |
| `IdEmpleado` | Peluquero que atendió, opcional. |
| `Fecha` | Fecha/hora. |
| `MedioPago` | `efectivo`, `tarjeta`, `transferencia`, `cuenta_corriente`. |
| `IdTarjetaCredito` | FK opcional. |
| `CantidadCuotas` | Cuotas de tarjeta. |
| `Descuento` | Descuento manual. |
| `Detalles` | Composición de líneas. |
| `Subtotal` | Suma de detalles. |
| `Total` | `Subtotal - Descuento + Recargo`. |
| `AgregarDetalle(item, cantidad)` | Agrega una línea. |

**Composición:** una `Venta` contiene `DetalleVenta`; no existe detalle sin venta.

---

## 7. Capa de acceso a datos

Todos los archivos están en `TFI_1/Datos`. El patrón general es:

1. `using MySqlConnection conexion = ConexionBD.ObtenerConexion();`
2. Crear `MySqlCommand` con SQL y parámetros tipados.
3. Abrir la conexión.
4. Usar `ExecuteScalar`, `ExecuteNonQuery`, `ExecuteReader` o `MySqlDataAdapter`.
5. Cerrar y liberar con `using`.

### 7.1 `Connection`, `Command` y transacciones

- Las conexiones son **por operación**, no globales.
- Las operaciones que afectan varias tablas usan `BeginTransaction`, `Commit` y `Rollback`.
- Las ventas y anulaciones bloquean filas con `FOR UPDATE` cuando hay riesgo de concurrencia.

### 7.2 `AtencionDatos.cs`

Métodos:

| Método | Qué hace |
|---|---|
| `Listar(filtro, estado)` | JOIN con clientes, empleados e items; filtra por nombre/apellido/servicio y estado. |
| `Insertar(atencion)` | Inserta y devuelve `LAST_INSERT_ID`. |
| `Actualizar(atencion)` | Actualiza cliente, empleado, servicio, tipo, fecha y estado. |
| `CambiarEstado(id, estado)` | Actualiza solo el estado. |
| `ExisteConflictoHorario(...)` | Detecta superposición con duración del servicio y mínimo de 1 minuto. |
| `CantidadHoy()` | Cuenta atenciones del día no canceladas. |
| `AgregarParametros` | Centraliza los parámetros del alta/edición. |
| `Leer` | Mapea una fila a `Atencion`. |
| `ATexto` / `ATipo` / `AEstado` / `AModalidad` | Traducen enums a los valores del ENUM de MySQL y viceversa. |

**Variable importante:** `@inicio` y `@fin` delimitan el rango del turno; `@exceptoId` permite editar sin compararse consigo mismo.

### 7.3 `AuditoriaDatos.cs`

| Método | Qué hace |
|---|---|
| `Registrar(...)` | Inserta usuario, acción, entidad, id y detalle. |
| `Listar(...)` | Consulta paginada por fechas, usuario y acción. |
| `Contar(...)` | Total de registros del filtro. |
| `Acciones()` | Lista de acciones distintas para el ComboBox. |

La tabla `auditoria` tiene índices por fecha, usuario y acción.

### 7.4 `BancoDatos.cs`

- `Listar(soloActivos)`.
- `ExisteNombre` para evitar duplicados.
- `Insertar`, `Actualizar`, `Desactivar`, `Reactivar`.
- `Desactivar`/`Reactivar` cambian el estado del banco y el de todas sus tarjetas.
- `Actualizar` hace lo mismo solo cuando el estado activo/inactivo realmente cambia (editar solo el nombre no toca las tarjetas).
- `TieneTarjetas` verifica si el banco tiene tarjetas antes de intentar borrarlo.
- `Eliminar` borra físicamente el banco; solo se permite si no tiene tarjetas.

### 7.5 `CajaDatos.cs`

| Método | Qué hace |
|---|---|
| `Abrir` | Inserta una caja solo si no existe otra `abierta`; devuelve 0 si ya había. |
| `ObtenerAbierta` | Busca la caja abierta más reciente. |
| `ObtenerPorId` | Lee una caja con nombres de usuarios. |
| `Cerrar` | Actualiza cierre, esperado, contado, diferencia y estado; solo si estaba abierta. |
| `RegistrarMovimiento` | Inserta ingreso/egreso de efectivo. |
| `ListarMovimientos` | Historial del turno. |
| `ListarAnulaciones` | Ventas anuladas en el rango por `fecha_anulacion`, con cliente, pago, total, usuario y motivo. |
| `ResumenVentas` | Agrupa ventas por medio de pago. |
| `VentasEfectivo` | Suma ventas no anuladas en efectivo del rango. |
| `TotalMovimientos` | Suma ingresos o egresos. |
| `Listar` | Historial paginado de cajas. |
| `Contar` | Total para paginación. |

**Detalle importante:** las ventas no tienen `id_caja`. El efectivo del turno se calcula por **rango de fechas**: desde `fecha_apertura` hasta `fecha_cierre` (o sin límite si sigue abierta).

### 7.6 `CategoriaDatos.cs`

- `Listar(filtro)`.
- `ExisteNombre`.
- `Insertar`, `Actualizar`, `Desactivar`, `Reactivar`.

### 7.7 `ClienteDatos.cs`

| Método | Qué hace |
|---|---|
| `Listar(filtro)` | Busca por DNI, apellido o nombre. |
| `ExisteDni` | Evita duplicados, excluyendo el propio ID al editar. |
| `ObtenerPorDni` | Usado para recuperar el Consumidor Final. |
| `Insertar`, `Actualizar`, `Desactivar`, `Reactivar`. |
| `EsConsumidorFinal(id)` | Detecta el cliente `00000000`, que no se puede borrar. |
| `TieneDependencias(id)` | Verifica ventas, cuenta corriente y turnos antes de borrar. |
| `Eliminar(id)` | Borrado físico solo si no tiene dependencias. |
| `AgregarParametros`, `Leer` | Centralizan parámetros y mapeo. |

### 7.8 `CuentaCorrienteDatos.cs`

| Método | Qué hace |
|---|---|
| `ObtenerPorCliente` | Busca la cuenta del cliente. |
| `CrearSiNoExiste` | Crea la cuenta con saldo 0 si falta. |
| `ObtenerSaldo` | Devuelve el saldo actual. |
| `ActualizarSaldo` | Fija un saldo y actualiza fecha. |
| `ListarMovimientos` | Historial por rango. |
| `InsertarMovimiento` | Inserta cargo/abono. |
| `RegistrarAbono` | Transacción: inserta abono y resta saldo. |
| `MovimientosResumen` | Consulta con datos para la grilla. |

### 7.9 `DashboardDatos.cs`

- `TotalVendidoHoy()`: `SUM(total)` de ventas no anuladas de hoy.
- `CantidadVentasHoy()`: `COUNT(*)` de hoy.
- `TotalClientes()`: clientes activos.
- `TotalItems()`: ítems activos.
- `ProductosStockBajo()`: productos activos con `stock <= stock_minimo`.
- `ProductoMayorStock()`: producto con más stock.
- `ProductoMenorStock()`: producto con menos stock.
- `UltimasVentas(cantidad)`: últimas ventas para el gráfico.
- `EjecutarEscalar(sql)`: auxiliar para conteos y sumas.

### 7.10 `EmpleadoDatos.cs`

- `Listar(filtro)`.
- `Insertar`, `Actualizar`, `Desactivar`, `Reactivar`.
- Traducción de `Modalidad` a `turnos`/`orden_llegada`.

### 7.11 `GastoDatos.cs`

- `Listar(desde, hasta, filtro)`.
- `ObtenerPorId`.
- `Insertar`, `Actualizar`.
- `TotalGastos(desde, hasta)`.
- `GastosPorCategoria(desde, hasta)`.

### 7.12 `IngresoMercaderiaDatos.cs`

| Método | Qué hace |
|---|---|
| `Listar(...)` | Cabeceras filtradas y paginadas. |
| `Contar`, `Sumar` | Totales del filtro, independientes de la página. |
| `ObtenerPorId` | Cabecera + detalle. |
| `Insertar` | Transacción: cabecera, detalle, stock y costo. |
| `ObtenerDetalles` | Líneas de un ingreso. |
| `LeerCabecera` | Mapeo. |
| `IngresosResumen` | Agrupado por día para reportes. |

**Transacción de ingreso:** por cada línea inserta el detalle y actualiza `items.stock = stock + cantidad`, `precio` y `costo`. Si algo falla, `Rollback`.

### 7.13 `ItemDatos.cs`

| Método | Qué hace |
|---|---|
| `Listar(filtro, categoriaId, tipo, soloStockBajo)` | Filtros combinables con SQL. |
| `ObtenerPorId` | Relee el ítem real antes de vender. |
| `ExisteNombre` | Evita duplicados. |
| `Insertar`, `Actualizar`, `Desactivar`, `Reactivar`. |
| `TieneDependencias(id)` | Verifica ventas, ingresos de mercadería y turnos antes de borrar. |
| `Eliminar(id)` | Borrado físico solo si no tiene dependencias. |
| `AgregarParametros`, `Leer` | Mapeo y parámetros. |
| `LeerIntNulo`, `LeerDecimalNulo` | Manejo de NULL. |
| `ATexto`, `ATipo` | Traducción del ENUM. |

**Filtro de stock bajo:**

```sql
AND (@soloStockBajo = 0
     OR (i.tipo = 'producto' AND i.stock <= i.stock_minimo))
```

### 7.14 `ProveedorDatos.cs`

- `Listar(filtro)`.
- `ObtenerPorId`.
- `ExisteNombre`.
- `ExisteCuit`.
- `Insertar`, `Actualizar`, `Desactivar`, `Reactivar`.
- `AgregarParametros`, `Leer`.

### 7.15 `ReporteDatos.cs`

| Método | Consulta |
|---|---|
| `Resumen` | Total, cantidad y ticket promedio. |
| `VentasPorMedioPago` | Agrupado por `medio_pago`. |
| `ProductosMasVendidos` | Top 10 por cantidad. |
| `VentasPorDia` | Serie diaria para el mensual. |
| `ClientesConMasCompras` | Top 20 por cantidad. |
| `VentasPorUsuario` | Agrupado por usuario y rol. |
| `ServiciosMasSolicitados` | Top 20 de ítems tipo servicio. |
| `VentasPorEmpleado` | Agrupado por empleado. |
| `Anulaciones` | Ventas anuladas del período por `fecha_anulacion`, con venta, cliente, pago, total, usuario y motivo. |

Todas filtran ventas no anuladas. `Anulaciones` es la excepción: lista las ventas anuladas por fecha de anulación.

### 7.16 `TarjetaCreditoDatos.cs`

- `Listar(soloActivas)`.
- `ListarPorBanco`.
- `ObtenerPorId`.
- `ExisteTipo`.
- `Insertar`, `Actualizar`, `Desactivar`, `Reactivar`.
- `Cargar(cmd, tarjeta)`.
- `Consultar(condicion, params)`.

### 7.17 `UsuarioDatos.cs`

| Método | Qué hace |
|---|---|
| `ObtenerPorNombre` | Login; trae usuario, hash y rol. |
| `Listar(filtro)` | Búsqueda por nombre. |
| `ObtenerRoles` | ComboBox de roles. |
| `ExisteNombre` | Evita duplicados. |
| `Insertar`, `Actualizar`, `Desactivar`, `Reactivar`. |
| `TieneDependencias(id)` | Verifica ventas, cajas, gastos, ingresos, movimientos y auditoría. |
| `EsUltimoAdministrador(id)` | Evita dejar el sistema sin administradores activos. |
| `Eliminar(id)` | Borrado físico solo si no tiene dependencias. |

### 7.18 `VentaDatos.cs`

**El archivo más importante de la capa Datos.**

#### `Registrar(Venta venta)`

Transacción completa:

1. `INSERT INTO ventas` con cliente, usuario, empleado, fecha, medio de pago, tarjeta, cuotas, descuento, recargo y total.
2. `SELECT LAST_INSERT_ID()` guarda el ID en `venta.Id`.
3. Por cada detalle: `INSERT INTO detalle_venta` con precio y subtotal congelados.
4. Si el ítem es producto: `UPDATE items SET stock = stock - @cantidad WHERE id_item = @item AND stock >= @cantidad`.
5. Si el `UPDATE` afecta 0 filas, lanza “Stock insuficiente” y hace `Rollback`.
6. Si el medio de pago es `cuenta_corriente`:
   - busca la cuenta con `FOR UPDATE`;
   - si no existe, la crea;
   - inserta un movimiento `cargo`;
   - suma el total al saldo.
7. `Commit`.

**Variables locales:**

| Variable | Función |
|---|---|
| `conexion` | Conexión abierta. |
| `transaccion` | Unidad atómica. |
| `cmd` | INSERT de cabecera. |
| `detalle` | Línea actual del `foreach`. |
| `cmdDetalle` | INSERT del detalle. |
| `cmdStock` | UPDATE condicional de stock. |
| `cmdCuenta` | SELECT de cuenta con bloqueo. |
| `resultado` | ID de cuenta o `DBNull`. |
| `idCuenta` | Cuenta a usar. |
| `cmdNueva` | Creación de cuenta si falta. |
| `cmdMov` | Cargo en cuenta corriente. |
| `cmdSaldo` | Actualización de saldo. |

**Por qué es robusto:** si falla el stock o la cuenta, no queda una venta a medias.

#### `Listar`, `Contar`, `ObtenerCabecera`, `ObtenerDetalle`

- `Listar` devuelve `DataTable` con columnas legibles (`Venta`, `Fecha`, `Cliente`, `Usuario`, `Empleado`, `Pago`, `Tarjeta`, `Cuotas`, `Subtotal`, `Descuento`, `Recargo`, `Total`, `Anulada`).
- `Contar` usa los mismos filtros para la paginación.
- `ObtenerCabecera` agrega datos de anulación y tarjeta.
- `ObtenerDetalle` devuelve ítem, tipo, cantidad, precio y subtotal.

#### `Anular(ventaId, usuarioId, motivo)`

Transacción:

1. Bloquea la venta no anulada con `FOR UPDATE`.
2. Devuelve stock solo de productos.
3. Si fue cuenta corriente, inserta un `abono` por el total y resta el saldo.
4. Marca `anulada = 1`, fecha, usuario y motivo.
5. `Commit`.

**No se borra la venta:** queda en el historial para auditoría. La anulación se muestra en la pestaña `Anulaciones` del reporte diario/mensual y en la caja del turno; no se suma dos veces al efectivo esperado.

---

## 8. Capa de negocio

### 8.1 `AtencionNegocio.cs`

- `Listar(filtro, estado)`.
- `Guardar(atencion)`:
  - exige cliente, empleado y servicio;
  - define `Tipo` según modalidad del empleado;
  - para orden de llegada fuerza `FechaHora = DateTime.Now`;
  - impide turnos nuevos en el pasado;
  - detecta superposición con `ExisteConflictoHorario`;
  - inserta o actualiza.
- `Cancelar(atencion)`: llama al modelo y persiste el estado.
- `CambiarEstado(atencion)`.
- `Eliminar(id)`: solo administrador; borra la atención y audita.
- `CantidadHoy()`.

### 8.2 `AuditoriaNegocio.cs`

- `LargoDetalle = 255`: recorta el detalle al tamaño de la columna.
- `Registrar(...)`: si no hay usuario o acción, **no hace nada**; una falla de auditoría no debe romper la operación real.
- `Listar`, `Contar`, `Acciones`.

### 8.3 `BackupNegocio.cs`

- `Generar(carpetaDestino)`:
  - valida carpeta;
  - lee la cadena con `MySqlConnectionStringBuilder`;
  - crea `backup_<base>_yyyyMMdd_HHmmss.sql`;
  - escribe un archivo temporal `.cnf` con credenciales;
  - ejecuta `mysqldump --defaults-extra-file=... --single-transaction --no-tablespaces --default-character-set=utf8mb4 --databases ...`;
  - espera hasta 120 segundos;
  - verifica que el archivo exista y no esté vacío;
  - audita;
  - borra el archivo temporal en `finally`.
- `ConstruirArchivoOpciones`: arma `[client]` con host, puerto, usuario y contraseña escapada.
- `ValorOpcion`: escapa barras y comillas.

**Seguridad:** la contraseña no aparece en la línea de comandos ni en el nombre del proceso.

### 8.4 `BancoNegocio.cs`

- `Listar`.
- `Insertar`, `Actualizar`: validan nombre obligatorio y longitud.
- `Desactivar`, `Reactivar`: bajan/suben el banco y todas sus tarjetas.
- `Eliminar`: borrado físico solo si el banco no tiene tarjetas; en caso contrario lanza error.
- `Validar(banco)`.

### 8.5 `CajaNegocio.cs`

| Método | Regla |
|---|---|
| `Abrir` | Monto inicial no negativo, sesión activa, una sola caja abierta. |
| `Cerrar` | Contado no negativo, caja existente y abierta, observación máximo 255. |
| `RegistrarMovimiento` | Tipo ingreso/egreso, monto > 0, concepto obligatorio, caja abierta. |
| `CalcularEsperado` | `inicial + ventas efectivo + ingresos - egresos`. |
| `VentasEfectivo` | Ventas no anuladas en efectivo del rango. |
| `TotalMovimientos` | Suma por tipo. |
| `ResumenVentas` | Agrupado por medio de pago. |
| `ListarAnulaciones` | Anulaciones del turno para mostrarlas en la caja; no se suman al efectivo. |
| `Listar` / `Contar` | Historial paginado. |

### 8.6 `CategoriaNegocio.cs`

- `Listar`.
- `Guardar`: nombre obligatorio, máximo 100, sin duplicados.
- `Desactivar`, `Reactivar`.

### 8.7 `ClienteNegocio.cs`

- `Listar`.
- `Guardar`:
  - nombre y apellido obligatorios;
  - DNI válido;
  - DNI único;
  - inserta o actualiza.
- `Desactivar`, `Reactivar`.
- `Eliminar(id)`: solo administrador; bloquea el Consumidor Final y clientes con ventas, cuenta corriente o turnos; audita.
- `ObtenerOCrearConsumidorFinal()`: crea el cliente `00000000` si no existe.

### 8.8 `Configuracion.cs`

Ya explicado en 4.5. Es negocio porque transforma configuración en valores de aplicación con defaults.

### 8.9 `CuentaCorrienteNegocio.cs`

- `LimiteDeudaAviso`: lee `Configuracion`.
- `ObtenerPorCliente`, `CrearSiNoExiste`, `ObtenerSaldo`, `ActualizarSaldo`.
- `ListarMovimientos`.
- `InsertarMovimiento`: cuenta válida, monto positivo, usuario.
- `RegistrarAbono`:
  - cliente válido;
  - monto positivo;
  - sesión activa;
  - debe existir deuda;
  - el abono no puede superar el saldo;
  - concepto por defecto “Pago a cuenta”;
  - audita.
- `MovimientosResumen`.

### 8.10 `DashboardNegocio.cs`

Delega los ocho indicadores a `DashboardDatos`. No tiene lógica extra: es una fachada de lectura.

### 8.11 `EmpleadoNegocio.cs`

- `Listar`.
- `Guardar`: nombre/apellido obligatorios; modalidad válida; duplicados.
- `Desactivar`, `Reactivar`.

### 8.12 `GastoNegocio.cs`

- `Listar`, `ObtenerPorId`.
- `Insertar`:
  - concepto obligatorio, máximo 150;
  - categoría máximo 100;
  - observación máximo 255;
  - monto > 0;
  - fecha no futura;
  - audita.
- `Actualizar`: mismas reglas.
- `TotalGastos`, `GastosPorCategoria`.

### 8.13 `IngresoMercaderiaNegocio.cs`

- `Listar`, `Contar`, `Sumar`, `ObtenerPorId`.
- `Insertar`:
  - proveedor obligatorio y existente;
  - nombre máximo 150;
  - observación máximo 255;
  - al menos una línea;
  - usuario activo;
  - cada línea: ítem válido, **solo producto**, cantidad > 0, costo >= 0.
- `IngresosResumen`.

### 8.14 `ItemNegocio.cs`

- `Listar`, `ListarCategorias`.
- `Guardar`:
  - nombre obligatorio;
  - precio >= 0;
  - costo >= 0;
  - categoría obligatoria;
  - si es producto: stock y stock mínimo obligatorios y no negativos; duración se limpia;
  - si es servicio: duración > 0; stock, stock mínimo y costo se limpian;
  - nombre único;
  - inserta o actualiza.
- `Desactivar`, `Reactivar`.
- `Eliminar(id)`: solo administrador; bloquea ítems con ventas, ingresos de mercadería o turnos; audita.

**Esta validación es la que sostiene la adaptación al negocio:** un servicio no maneja stock; un producto sí.

### 8.15 `ProveedorNegocio.cs`

- `Listar`, `ObtenerPorId`.
- `Guardar`:
  - limpia nulos/espacios;
  - nombre obligatorio;
  - email válido;
  - longitudes;
  - nombre único;
  - CUIT único si se cargó.
- `Desactivar`, `Reactivar`.
- `Limpiar(valor)`.

### 8.16 `ReporteNegocio.cs`

- `Resumen`, `VentasPorMedioPago`, `ProductosMasVendidos`, `VentasPorDia`, `ClientesConMasCompras`, `VentasPorUsuario`, `ServiciosMasSolicitados`, `VentasPorEmpleado`, `Anulaciones`.
- `ValidarRango`: `desde <= hasta`.
- Todas las consultas se delegan a `ReporteDatos`.

### 8.17 `Sesion.cs`

Ya explicado. Es la fuente de identidad y permisos.

### 8.18 `TarjetaCreditoNegocio.cs`

- `Listar`, `ListarPorBanco`, `ObtenerPorId`.
- `Insertar`, `Actualizar`.
- `Desactivar`, `Reactivar`.
- `Validar`:
  - banco obligatorio;
  - tipo obligatorio;
  - recargo >= 0;
  - cuotas >= 1.

### 8.19 `UsuarioNegocio.cs`

**Reglas destacadas:**

- `LargoMinimoContrasena = 8`.
- `LargoMaximoContrasena = 100`.
- `ContrasenasDebiles`: lista negra.
- `ValidarContrasena`:
  - no vacía;
  - largo;
  - mayúscula;
  - minúscula;
  - número;
  - no contiene el nombre de usuario;
  - no es una contraseña común.
- `Autenticar`:
  - usuario y contraseña obligatorios;
  - usuario existente;
  - hash válido;
  - contraseña correcta;
  - usuario activo.
- `Guardar`:
  - nombre obligatorio y máximo 50;
  - rol obligatorio;
  - nombre único;
  - política solo al crear o cambiar contraseña;
  - BCrypt al crear/cambiar;
  - conserva hash si no se cambia;
  - audita alta/edición.
- `Desactivar`: no permite desactivar el propio usuario; audita.
- `Reactivar`.
- `Eliminar`: solo administrador; no permite el propio usuario, el último administrador activo ni usuarios con historial asociado; audita.
- `VerificarContrasena`: maneja hash inválido como error de acceso, no como crash.

### 8.20 `VentaNegocio.cs`

**Es el corazón de las reglas de venta.**

`Registrar` valida:

1. venta no nula;
2. cliente con ID;
3. usuario en sesión;
4. al menos un detalle;
5. medio de pago;
6. descuento no negativo;
7. caja abierta;
8. tarjeta: tarjeta y cuotas válidas;
9. cuenta corriente: no Consumidor Final;
10. **relee cada ítem desde la base** para evitar precios manipulados;
11. descuento no mayor al subtotal;
12. recargo de tarjeta según porcentaje;
13. cuotas no mayores al máximo;
14. fecha actual;
15. transacción;
16. auditoría.

**Variables locales importantes:**

| Variable | Función |
|---|---|
| `recalculado` | Nueva lista con ítems leídos de la base. |
| `detalle` | Línea original. |
| `actual` | Ítem real de la base. |
| `tarjeta` | Tarjeta seleccionada. |
| `idVenta` | ID devuelto por la transacción. |

`Anular` valida ID, usuario y motivo; recorta el motivo a 200 para que quepa en el concepto; llama a `VentaDatos.Anular`; audita.

---

## 9. Capa de presentación

### 9.1 Estructura de cada formulario

Cada formulario tiene:

- `FormX.cs`: lógica y eventos.
- `FormX.Designer.cs`: controles, posiciones, tamaños y textos.
- `FormX.resx`: recursos.

Los controles se declaran en el diseñador con `new()`. Los `.cs` pueden ajustar `Dock`, `Anchor`, anchos o visibilidad en tiempo de ejecución, pero **no crean controles desde cero**. Por eso todos los campos y botones se pueden ver en la vista **Diseño**.

### 9.2 Patrones comunes de los listados

- `_negocio`: servicio de negocio.
- `Cargar()`: consulta y enlaza a `DataGridView`.
- `Seleccionado()` / `Seleccionada()`: obtiene la entidad de la fila actual.
- `ActualizarBotonEstado()`: cambia “Desactivar” por “Reactivar”.
- `chkSoloActivos`: filtra en memoria o en SQL según pantalla.
- `btnBuscar`: aplica filtro.
- `btnRefrescar`: vuelve a cargar.
- `btnNuevo`: abre el formulario de alta.
- `btnEditar`: abre el formulario de edición.
- `btnDesactivar`: baja lógica.
- `btnEliminar`: visible solo para administradores; borrado físico con validación de dependencias.
- `Mensajes.Error`: error contextual.

### 9.3 Catálogo de formularios

#### `FormLog`

- Variables: `_negocio` (`UsuarioNegocio`).
- Controles: `txtNomUsuario`, `txtConUsuario`, `btnIngresar`, `chkMostar`, `lblInicioSesion`, `pictureBox1`.
- `btnIngresar_Click`: valida campos, autentica, guarda `Sesion.UsuarioActual`, abre `FormPrincipal`.
- Si el principal se cierra por “Cerrar sesión”, vuelve a mostrar el login en vez de cerrar la aplicación.
- `chkMostar_CheckedChanged`: alterna `PasswordChar`.

#### `FormPrincipal`

- `CerrarSesionPendiente`: distingue cierre de sesión de salida.
- `_Load`: título, usuario y visibilidad de menús.
- `AplicarImagenDeFondo`: pinta la imagen en el `MdiClient`.
- `AbrirFormulario`: reutiliza una instancia abierta del mismo tipo; si no, la abre como MDI.
- Eventos de menú: Dashboard, Nueva venta, Historial, Caja, Ítems, Categorías, Clientes, Cuenta corriente, Atenciones, Usuarios, Empleados, Tarjetas, Gastos, Ingreso, Historial de mercadería, Proveedores, Auditoría, Backup, Reportes, Cerrar sesión, Salir.
- `ExigirAdministrador`: segunda barrera de permisos.

#### `FormDashboard`

Variables:

- `_negocio`: indicadores.
- `_atencionNegocio`: atenciones de hoy.
- `_tooltipGrafico`: tooltip del gráfico.
- `_zonasBarras`: rectángulos de las barras.
- `_detallesBarras`: texto de cada barra.
- `_ultimasVentasParaGrafico`: datos del gráfico.
- `_indiceTooltip`: barra bajo el mouse.

Métodos:

- `AjustarLayout`: responsive.
- `Refrescar`: consulta todos los indicadores.
- `pnlGrafico_Paint`: dibuja barras y escala.
- `MouseMove` / `MouseLeave`: tooltip.
- `CalcularPaso`: escala “redonda”.
- `FormatearImporte`.
- `DetalleVentaTexto`.

#### `FormNuevaVenta`

Variables:

- `_ventaNegocio`, `_clienteNegocio`, `_itemNegocio`, `_empleadoNegocio`, `_tarjetaNegocio`, `_ccNegocio`, `_cajaNegocio`, `_bancoNegocio`.
- `_carrito`: lista de `DetalleVenta`.
- `MediosPago`: array `efectivo`, `tarjeta`, `transferencia`, `cuenta_corriente`.
- `_recalculando`: evita recursión al ajustar el descuento.
- `_tarjetasActivas`: tarjetas disponibles.

Flujo:

1. `Load`: carga clientes, ítems, empleados, bancos y tarjetas.
2. `cboMedioPago`: muestra/oculta banco, tarjeta y cuotas.
3. `btnAgregar`: valida stock acumulado y agrega/actualiza línea.
4. `RecalcularTotales`: subtotal, descuento y total.
5. `CalcularRecargo`: aplica porcentaje de la tarjeta.
6. `btnRegistrar`: valida cliente y carrito; **verifica que la caja esté abierta** (si no, muestra un aviso y cancela); arma `Venta`; avisa si la deuda supera el límite; registra; ofrece ticket; limpia.

**Regla de caja:** la venta solo se registra si existe una caja abierta. La comprobación se hace en `FormNuevaVenta` para mostrar el `MessageBox` de advertencia y también en `VentaNegocio.Registrar` como segunda barrera.

**Variables locales de `btnRegistrar`:**

| Variable | Función |
|---|---|
| `cliente` | Cliente seleccionado. |
| `idEmpleado` | Empleado opcional. |
| `idTarjeta` / `cuotas` | Datos de tarjeta. |
| `tarjeta` | Tarjeta elegida. |
| `saldoActual` | Deuda actual. |
| `totalVenta` | Total calculado. |
| `saldoNuevo` | Deuda después de la venta. |
| `continuar` | Respuesta al aviso de límite. |
| `venta` | Entidad a persistir. |
| `id` | ID generado. |
| `respuestaTicket` | Si quiere ver el ticket. |

#### `FormHistorialVentas`

Variables:

- `_ventaNegocio`, `_usuarioNegocio`, `_empleadoNegocio`.
- `TamanoPagina = 50`.
- `_pagina`, `_totalRegistros`.

Métodos:

- `SetDateTimePickerSafe`: amplía rango antes de asignar fecha.
- `Buscar`: reinicia página.
- `CargarPagina`: consulta con límite/offset.
- `ActualizarPaginacion`.
- `FormatearGrid`: oculta IDs y da formato.
- `btnVerDetalle`: abre `FormDetalleVenta`.

#### `FormDetalleVenta`

- `_ventaId`: ID recibido.
- `_cabecera`, `_detalle`: `DataTable`.
- `Cargar`: cabecera y detalle.
- `btnAnular`: abre `FormAnularVenta`, confirma y llama a `VentaNegocio.Anular`.
- `btnImprimir`: imprime el detalle.
- `btnTicket`: abre `FormTicket`.
- Helpers: `Capitalizar`, `TieneTarjeta`, `TextoTarjeta`, `ObtenerRecargo`, `ObtenerSubtotal`.

#### `FormTicket`

- `Ancho = 37`: ancho fijo del ticket.
- `NombreNegocio`: encabezado.
- `_ventaId`.
- `_textoTicket`.
- `ConstruirTicket`: cabecera, cliente, ítems, subtotal, descuento, recargo, total y medio de pago.
- `Centrar`, `Truncar`, `LineaImporte`, `Columna`, `Capitalizar`.
- `btnImprimir`: `PrintDialog` y `PrintDocument`.

#### `FormCaja`

Variables:

- `_negocio`.
- `_caja`: caja abierta o `null`.

Métodos:

- `Cargar`: estado, resumen, grilla de ventas, movimientos y anulaciones.
- `Habilitar(abierta)`: habilita/deshabilita operaciones.
- `btnAbrir`: pide monto inicial.
- `btnIngreso` / `btnEgreso`: piden monto y concepto.
- `RegistrarMovimiento(tipo)`.
- `btnCerrarCaja`: abre `FormCerrarCaja`.
- `btnHistorial`: abre `FormHistorialCaja`.
- `FormatearResumen`, `FormatearMovimientos`, `FormatearAnulaciones`.
- Pestañas `Movimientos` y `Anulaciones`; las anulaciones se muestran con su total, pero no se suman dos veces al efectivo esperado.

#### `FormCerrarCaja`

- `_esperado`: efectivo esperado.
- `MontoContado`: resultado público.
- `Observacion`: resultado público.
- `ActualizarDiferencia`: `contado - esperado` en vivo.
- `btnConfirmar`: valida y cierra con `DialogResult.OK`.

#### `FormHistorialCaja`

- `TamanoPagina = 50`.
- `_pagina`, `_totalRegistros`.
- `CargarPagina`, `ActualizarPaginacion`.
- `ColorearDiferencias`: resalta faltantes/sobrantes.
- `FormatearGrid`.

#### `FormItems`

Variables:

- `_negocio`.
- `_cargandoFiltros`: evita recargas mientras se llenan los combos.

Métodos:

- `CargarFiltros`: tipos y categorías.
- `Cargar`: filtros + stock bajo.
- `Filtro_Changed`.
- `btnNuevo`, `btnEditar`, `btnDesactivar`, `btnEliminar` (solo administrador).
- `ActualizarBotonEstado`.

#### `FormItemEditar`

- `_item` / `_esNuevo`.
- `cboTipo_SelectedIndexChanged` y `AlternarCampos`: muestra campos de producto o servicio.
- `Validar`: nombre, precio, categoría, stock/duración.
- `btnGuardar`: llama a `ItemNegocio.Guardar`.

#### `FormCategorias` / `FormCategoriaEditar`

- Listado con búsqueda, activos y baja lógica.
- Edición con nombre obligatorio y único.

#### `FormClientes` / `FormClienteEditar`

- Listado con búsqueda por DNI/nombre/apellido y filtro de activos.
- `btnEliminar` visible solo para el administrador; borrado físico con validación de dependencias.
- Edición: nombre, apellido, DNI, teléfono, activo.
- DNI único y de 8 dígitos.

#### `FormCuentaCorriente`

Variables:

- `_negocio`, `_clienteNegocio`.
- `ClienteSeleccionado`: cliente del combo.

Métodos:

- `AjustarRangoFechas`.
- `Cargar`: clientes activos no Consumidor Final; saldo y movimientos.
- `Refrescar`.
- `FormatearGrilla`, `FormatearColumna`.
- `btnRegistrarAbono`: pide monto, valida y registra.
- Aviso de deuda alta.

#### `FormAtenciones`

- `EstadosFiltro`: `null`, `pendiente`, `en_atencion`, `finalizada`, `cancelada`.
- `_cargandoEstado`.
- `Cargar`.
- `btnNuevo`, `btnEditar`, `btnCancelarAtencion`, `btnEliminar` (solo administrador).
- Columnas explícitas: Fecha, Hora, Cliente, Peluquero, Servicio, Tipo, Estado.

#### `FormAtencionEditar`

- `Estados`: array de enums.
- `_atencion`, `_esNueva`.
- Carga clientes, empleados y servicios.
- `ActualizarTipo`: según modalidad del empleado.
- `Validar`: cliente, empleado, servicio, fecha.
- `btnGuardar`: llama a `AtencionNegocio.Guardar`.

#### `FormUsuarios` / `FormUsuarioEditar`

- `FormUsuarios`: listado, búsqueda, activos, estado y `btnEliminar` (solo administrador).
- `FormUsuarioEditar`:
  - `_usuario`, `_esNuevo`, `_ayuda` (ToolTip);
  - `txtContrasena`, `txtConfirmar` con longitud máxima;
  - `Validar`: nombre, rol, coincidencia de contraseñas y política;
  - `btnGuardar`: `UsuarioNegocio.Guardar`.

#### `FormEmpleados` / `FormEmpleadoEditar`

- Listado con activos y baja lógica.
- Edición: nombre, apellido, modalidad, activo.

#### `FormTarjetaDeCredito`

- `_bancos`, `_tarjetas`.
- `_cargando`.
- `CargarBancos`, `BancoCambiado`, `CargarTarjetas`, `TarjetaCambiada`.
- Alta/edición/baja lógica de bancos y tarjetas.
- `btnBancoEliminar`: borra físicamente un banco sin tarjetas asociadas.
- Muestra recargo y cuotas máximas.
- `Aviso` y `Error` para mensajes.

#### `FormBancoEditar` / `FormTarjetaEditar`

- Edición de banco: nombre y activo.
- Edición de tarjeta: banco, tipo, recargo, cuotas, activo.
- Validan y llaman a la capa de negocio.

#### `FormGastos`

Variables:

- `_negocio`.
- `_cargando`: evita recargas durante la carga.
- `_editando`: gasto en edición o `null`.

Métodos:

- `Cargar`: rango, filtro, total y limpieza.
- `FormatearGrid`: oculta IDs, formato de fecha/moneda.
- `Limpiar`: reinicia formulario.
- `LeerFormulario`: valida concepto, monto y fecha; arma `Gasto`.
- `btnGuardar`: inserta o actualiza.
- `CellDoubleClick`: carga el gasto para editar.

#### `FormProveedores` / `FormProveedorEditar`

- Listado con búsqueda, activos y baja lógica.
- Edición: nombre, CUIT, teléfono, email, dirección, activo.
- Valida longitudes, email y duplicados.

#### `FormIngresoMercaderia`

Variables:

- `_negocio`, `_itemNegocio`, `_proveedorNegocio`.
- `_detalles`: líneas del ingreso.

Métodos:

- `CargarProveedores`, `CargarItems`.
- `cboProducto_SelectedIndexChanged`: muestra stock/costo actual.
- `btnAgregar`: valida producto, cantidad y costo; agrega detalle.
- `btnQuitar`.
- `RefrescarDetalle`, `FormatearDetalle`, `ActualizarTotal`.
- `LeerCabecera`.
- `btnGuardar`: llama a `IngresoMercaderiaNegocio.Insertar`.

#### `FormHistorialMercaderia`

- `TamanoPagina = 50`.
- `_cargando`, `_pagina`, `_totalRegistros`.
- `Cargar`, `CargarPagina`, `ActualizarPaginacion`.
- `FormatearIngresos`, `CargarDetalle`.
- `btnNuevo`: abre `FormIngresoMercaderia`.
- Filtros por fecha y proveedor.

#### `FormAuditoria`

- `_negocio`, `_usuarioNegocio`.
- `TamanoPagina = 50`.
- `_pagina`, `_totalRegistros`.
- `CargarUsuarios`, `CargarAcciones`, `Cargar`, `CargarPagina`.
- `FormatearGrid`, `OcultarColumna`, `AjustarColumna`.

#### `FormBackup`

- `_negocio`.
- `_ultimaCarpeta`.
- `AjustarLayout`.
- `btnElegir`: `FolderBrowserDialog`.
- `btnGenerar`: llama a `BackupNegocio.Generar`.
- `btnAbrirCarpeta`: abre el Explorador en la carpeta.

#### `FormReporteDiario`

Variables:

- `_negocio`, `_gastos`.
- `printDocument1`, `printPreviewDialog1`.

Métodos:

- `AjustarLayout`.
- `Cargar`: resumen, medios de pago, más vendidos, gastos por categoría, ventas por empleado y anulaciones.
- `btnImprimir`.
- `printDocument1_PrintPage`: incluye el detalle y el resumen de anulaciones.

#### `FormReporteMensual`

- Misma estructura que el diario, con `VentasPorDia` y rango mensual.
- Pestaña `Anulaciones` con el detalle y el total anulado del mes.
- Imprime el resumen, incluidas las anulaciones.

#### `FormReporteAvanzado`

- `_negocio`.
- `Cargar`: clientes con más compras, ventas por usuario y servicios más solicitados.
- `FormatearTotal(DataGridView)`: aplica formato monetario a las columnas de importes.

#### `FormAnularVenta`

- `Motivo`: texto obligatorio.
- Avisa si la venta fue a cuenta corriente y qué se revertirá.
- `btnAceptar`: valida y devuelve `DialogResult.OK`.

#### `FormCerrarCaja`

Ya explicado.

### 9.4 Total de formularios

| Grupo | Cantidad |
|---|---:|
| Seguridad y navegación | 3 |
| Ventas y caja | 8 |
| Productos, mercadería y categorías | 8 |
| Clientes y cuenta corriente | 3 |
| Turnos | 2 |
| Administración | 9 |
| Gastos | 1 |
| Reportes | 3 |
| **Total** | **37** |

---

## 10. Base de datos: scripts SQL

### 10.1 `BaseDatos/peluqueria.sql`

**Objetivo:** crear la base `peluqueria`, las 20 tablas y los datos mínimos.

#### Cabecera

- Motor: MySQL 8.x / MariaDB 10.4+.
- Ejecutable completo desde consola, Workbench o phpMyAdmin.
- Re-ejecutable: elimina tablas y las vuelve a crear.
- **Advertencia:** borra todos los datos existentes.
- Usuarios iniciales con contraseña BCrypt: `admin/admin123`, `vendedor/vendedor123`, `supervisor/supervisor123`.

#### Creación y limpieza

```sql
CREATE DATABASE IF NOT EXISTS peluqueria
  CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE peluqueria;
SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE IF EXISTS ...;
SET FOREIGN_KEY_CHECKS = 1;
```

- `utf8mb4`: soporta acentos, ñ y emojis.
- `FOREIGN_KEY_CHECKS = 0`: permite borrar tablas en cualquier orden.
- Se desactivan y reactivan las claves foráneas alrededor de la limpieza.

#### Tabla `roles`

| Columna | Tipo | Restricción |
|---|---|---|
| `id_rol` | `INT` | PK, autoincremental. |
| `nombre` | `VARCHAR(50)` | NOT NULL, UNIQUE. |

#### Tabla `usuarios`

| Columna | Tipo | Restricción |
|---|---|---|
| `id_usuario` | `INT` | PK. |
| `nombre_usuario` | `VARCHAR(50)` | NOT NULL, UNIQUE. |
| `contrasena` | `VARCHAR(100)` | Hash BCrypt. |
| `id_rol` | `INT` | FK a `roles`. |
| `activo` | `TINYINT(1)` | Default 1. |

FK con `ON UPDATE CASCADE ON DELETE RESTRICT`: no se borra un rol con usuarios.

#### Tabla `clientes`

- `id_cliente` PK.
- `nombre`, `apellido` `VARCHAR(100)` NOT NULL.
- `dni` `CHAR(8)` NOT NULL UNIQUE.
- `telefono` `VARCHAR(30)` NULL.
- `activo` `TINYINT(1)`.

#### Tabla `empleados`

- `id_empleado` PK.
- `nombre`, `apellido`.
- `modalidad` `ENUM('turnos','orden_llegada')`.
- `activo`.

#### Tabla `categorias`

- `id_categoria` PK.
- `nombre` `VARCHAR(100)`.
- `activo`.

#### Tabla `items`

**Núcleo del catálogo.**

| Columna | Tipo | Uso |
|---|---|---|
| `id_item` | INT PK | ID. |
| `nombre` | VARCHAR(150) | Nombre. |
| `tipo` | ENUM('producto','servicio') | Discriminador. |
| `precio` | DECIMAL(12,2) | Precio de venta. |
| `duracion_min` | INT NULL | Solo servicio. |
| `stock` | INT NULL | Solo producto. |
| `stock_minimo` | INT NULL | Solo producto. |
| `costo` | DECIMAL(12,2) NULL | Solo producto. |
| `id_categoria` | INT | FK. |
| `activo` | TINYINT(1) | Baja lógica. |

Restricciones:

- `chk_precio`: `precio >= 0`.
- `chk_stock`: servicio sin stock; producto con stock y stock mínimo no negativos.
- `chk_duracion`: servicio con duración > 0; producto sin duración.
- `chk_costo`: costo nulo o no negativo.
- Índice `ix_items_categoria`.

#### Tablas `bancos` y `tarjetas_credito`

- `bancos`: `id_banco`, `nombre`, `activo`.
- `tarjetas_credito`: PK, FK a banco, tipo, porcentaje, cuotas máximas, activo, fecha de alta.
- `uq_tarjetas_banco_tipo`: no se repite el mismo tipo en el mismo banco.
- FK restrictiva.

#### Tabla `ventas`

| Columna | Uso |
|---|---|
| `id_venta` | PK. |
| `id_cliente` | FK. |
| `id_usuario` | FK. |
| `id_empleado` | FK opcional. |
| `fecha` | DATETIME. |
| `medio_pago` | ENUM de cuatro valores. |
| `id_tarjeta_credito` | FK opcional. |
| `cantidad_cuotas` | NULL si no es tarjeta. |
| `descuento`, `recargo`, `total` | DECIMAL(12,2). |
| `anulada` | 1/0. |
| `fecha_anulacion`, `id_usuario_anulacion`, `motivo_anulacion` | Trazabilidad de anulación. |

Índices por cliente, fecha y anulada.

#### Tabla `detalle_venta`

- `id_detalle` PK.
- `id_venta` FK con `ON DELETE CASCADE`.
- `id_item` FK restrictiva.
- `cantidad`, `precio`, `subtotal`.

**Por qué se guarda `precio`:** es el precio histórico de la operación.

#### Tablas de cuenta corriente

`cuentas_corrientes`:

- PK.
- `id_cliente` único.
- `saldo` DECIMAL(12,2).
- `fecha_actualizacion`.

`movimientos_cuenta_corriente`:

- PK.
- FK a cuenta con `ON DELETE CASCADE`.
- `tipo` ENUM('cargo','abono').
- `monto`, `concepto`, `id_venta`, `id_usuario`.

**Regla:** un cargo suma deuda; un abono la resta. El historial conserva ambos.

#### Tablas de gastos y mercadería

`gastos`:

- PK.
- `fecha`, `concepto`, `monto`, `categoria`, `observacion`.
- `id_usuario` FK.
- `fecha_alta`.

`proveedores`:

- PK.
- `nombre` único.
- `cuit`, `telefono`, `email`, `direccion`.
- `activo`.

`ingresos_mercaderia`:

- PK.
- `fecha`.
- `id_proveedor` FK opcional.
- `proveedor` snapshot de texto.
- `total`, `id_usuario`, `observacion`.

`ingresos_mercaderia_detalle`:

- PK.
- FK a ingreso con `ON DELETE CASCADE`.
- FK a ítem.
- `cantidad`, `costo_unitario`, `subtotal`.

#### Tabla `atenciones`

| Columna | Uso |
|---|---|
| `id_atencion` | PK. |
| `id_cliente` | FK. |
| `id_empleado` | FK. |
| `id_item` | Servicio. |
| `tipo` | ENUM('turno','llegada'). |
| `fecha_hora` | DATETIME. |
| `estado` | ENUM de cuatro estados. |

Índices por empleado+fecha y por cliente.

#### Tabla `auditoria`

- `id_auditoria` PK.
- `fecha`, `id_usuario`, `accion`, `entidad`, `id_entidad`, `detalle`.
- Índices por fecha, usuario y acción.
- FK a usuario restrictiva: no se borra un usuario con auditoría.

#### Tablas `cajas` y `caja_movimientos`

`cajas`:

- PK.
- `id_usuario_apertura`, `fecha_apertura`, `monto_inicial`.
- `id_usuario_cierre`, `fecha_cierre`, `monto_esperado`, `monto_contado`, `diferencia`, `observacion`.
- `estado` ENUM('abierta','cerrada').
- `chk_cajas_montos`: montos no negativos.

`caja_movimientos`:

- PK.
- FK a caja.
- FK a usuario.
- `tipo` ENUM('ingreso','egreso').
- `monto > 0`.
- Índice caja+fecha.

#### Datos iniciales

- Roles 1, 2 y 3.
- Usuarios `admin`, `vendedor`, `supervisor` con hash BCrypt.
- Consumidor Final y Juan Rodríguez.
- Ana Gómez (turnos) y Luis Pérez (orden de llegada).
- Categorías: Cortes, Coloración, Tratamientos, Productos.
- Seis ítems: tres servicios y tres productos.
- Dos proveedores.
- Dos bancos y tres tarjetas.

**Por qué el Consumidor Final es importante:** permite vender sin registrar cliente, pero se prohíbe usarlo para cuenta corriente.

### 10.2 `BaseDatos/crear_usuario_app.sql`

```sql
CREATE USER IF NOT EXISTS 'peluqueria_app'@'localhost'
  IDENTIFIED BY 'CAMBIAR_ESTA_CONTRASENA';

GRANT SELECT, INSERT, UPDATE, DELETE
  ON peluqueria.* TO 'peluqueria_app'@'localhost';

FLUSH PRIVILEGES;
```

| Elemento | Para qué sirve |
|---|---|
| `CREATE USER IF NOT EXISTS` | Evita error si ya existe. |
| `IDENTIFIED BY` | Contraseña inicial; debe cambiarse. |
| `GRANT SELECT, INSERT, UPDATE, DELETE` | Permisos mínimos de aplicación. |
| `ON peluqueria.*` | Solo esa base. |
| `FLUSH PRIVILEGES` | Recarga permisos. |

**Principio de menor privilegio:** la aplicación no puede `CREATE`, `DROP` ni `ALTER`. Las copias de seguridad se restauran con un usuario administrador, no con `peluqueria_app`.

### 10.3 Relaciones principales

```text
roles 1──N usuarios
clientes 1──N ventas
usuarios 1──N ventas
empleados 1──N ventas
ventas 1──N detalle_venta
items 1──N detalle_venta
clientes 1──1 cuentas_corrientes
cuentas_corrientes 1──N movimientos_cuenta_corriente
ventas 1──N movimientos_cuenta_corriente (opcional)
categorias 1──N items
bancos 1──N tarjetas_credito
tarjetas_credito 1──N ventas (opcional)
proveedores 1──N ingresos_mercaderia
ingresos_mercaderia 1──N ingresos_mercaderia_detalle
items 1──N ingresos_mercaderia_detalle
clientes 1──N atenciones
empleados 1──N atenciones
items 1──N atenciones
usuarios 1──N gastos
usuarios 1──N auditoria
usuarios 1──N cajas (apertura/cierre)
cajas 1──N caja_movimientos
```

### 10.4 Normalización y duplicación

- Las entidades tienen su propia tabla.
- Los nombres se obtienen por JOIN, salvo dos **snapshots deliberados**:
  - `ingresos_mercaderia.proveedor`;
  - `detalle_venta.precio`.
- No se duplican listas de ítems ni totales calculables.
- Los totales de venta se guardan porque son el importe histórico cobrado.
- El saldo de cuenta corriente se guarda para consulta rápida, y se respalda con el historial de movimientos.

### 10.5 Restricciones e integridad

- PK en todas las tablas.
- FK con `RESTRICT`, `CASCADE` o `SET NULL` según el caso.
- `UNIQUE` en DNI, nombre de usuario, rol, nombre de proveedor, banco+tipo de tarjeta y cliente de cuenta corriente.
- `CHECK` en precios, stock, duración, costo, montos de caja y movimientos.
- `ENUM` para estados y tipos controlados.
- Índices en fechas, clientes, usuarios, estados y relaciones.

---

## 11. Validaciones, errores y seguridad

### 11.1 Matriz de validaciones

| Requisito de la consigna | Implementación | Dónde |
|---|---|---|
| Campos obligatorios | `ExigirTexto`, `TextoVacio` | Modelos, negocio, UI. |
| Tipos de datos | `TryParse`, controles numéricos, parámetros tipados | `Validaciones`, `Parametros`, formularios. |
| Valores numéricos | `ImporteValido`, `EnteroNoNegativo` | Formularios y negocio. |
| Valores negativos | `ExigirImporteNoNegativo`, setters | `Item`, `Gasto`, `Caja`, etc. |
| Longitud de campos | `ExigirLongitudMaxima`, `MaxLength` | Negocio y UI. |
| Datos duplicados | `ExisteDni`, `ExisteNombre`, `ExisteCuit`, `ExisteTipo` | Datos/Negocio. |
| Información inexistente | `ObtenerPorId`/`ObtenerPorNombre` + excepción | Negocio. |
| Cantidades de productos | `DetalleVenta` y `IngresoMercaderiaNegocio` | Modelo/Negocio. |
| Stock disponible | `HayStockPara`, UPDATE condicional | Modelo/SQL. |
| Fechas | `RangoDeFechasValido`, `FechaNoFutura`, `TurnoNoPasado` | Validaciones. |
| Contraseñas | `ValidarContrasena` | UsuarioNegocio. |
| Permisos | `Sesion`, `ExigirAdministrador` | Negocio/UI. |

### 11.2 Mensajes claros

- Los formularios usan `MessageBox` con título y detalle.
- Los errores inesperados usan `Mensajes.Error`.
- Los errores de validación se muestran con `ArgumentException` capturada.
- Los errores de stock se muestran con `InvalidOperationException`.
- No se muestra el stack trace al usuario.

### 11.3 Seguridad

- Contraseñas con **BCrypt**.
- Política de contraseñas.
- Usuario MySQL con permisos mínimos.
- `App.config` fuera de Git.
- Parámetros SQL tipados.
- No se concatena entrada del usuario en SQL.
- Backup con archivo temporal de credenciales.
- Auditoría de operaciones sensibles.
- Baja lógica para preservar historial.

---

## 12. Matriz de cumplimiento de la consigna

Archivo de referencia: `TFI_1/Archivos.md/segundo-parcial-programacion-2.md`.

### 12.1 Punto 1 — Introducción y elección del negocio

| Exigencia | Cumplimiento |
|---|---|
| Aplicación de gestión y ventas | Sistema completo de peluquería. |
| Rubro libre | Peluquería, uno de los ejemplos propuestos. |
| Resolver una necesidad real | Ventas, turnos, stock, caja, clientes y reportes. |

### 12.2 Punto 2 — Objetivo general

| Requisito | Implementación |
|---|---|
| Autenticar usuarios | `FormLog` + `UsuarioNegocio.Autenticar`. |
| Administrar usuarios | `FormUsuarios` + `FormUsuarioEditar`. |
| Administrar productos o servicios | `FormItems` + `FormItemEditar`. |
| Registrar ventas | `FormNuevaVenta` + `VentaNegocio` + `VentaDatos`. |
| Consultar información | Dashboard, historiales, reportes y auditoría. |
| Controlar stock | Productos con stock, stock mínimo y descuento transaccional. |
| Gestionar clientes | `FormClientes`, `FormClienteEditar` y cuenta corriente. |
| Dashboard | `FormDashboard` con datos de MySQL. |
| Manejar errores y validar | `Mensajes`, `Validaciones`, try/catch y reglas de negocio. |
| POO | 18 modelos, servicios, DAO, composición y encapsulamiento. |
| Múltiples formularios | 37 formularios funcionales. |
| Persistir en MySQL | 20 tablas y operaciones CRUD. |

### 12.3 Punto 3 — Requisitos tecnológicos

| Requisito | Cumplimiento |
|---|---|
| C# | Todo el proyecto. |
| Windows Forms | 37 formularios WinForms. |
| MySQL | Base `peluqueria`, scripts y `MySql.Data`. |
| Persistencia real | No se usan listas como almacenamiento principal. |
| Alta, baja, modificación y consulta | Implementadas en las entidades principales. |

### 12.4 Punto 4.1 — Login

| Exigencia | Implementación |
|---|---|
| Formulario inicial | `Application.Run(new FormLog())`. |
| Usuario y contraseña | `txtNomUsuario`, `txtConUsuario`. |
| Verificar contra la base | `UsuarioDatos.ObtenerPorNombre` + BCrypt. |
| Usuario inexistente | `UnauthorizedAccessException("El usuario no existe.")`. |
| Contraseña incorrecta | `UnauthorizedAccessException("Contraseña incorrecta.")`. |
| Campos vacíos | Validación previa en `FormLog` y en `UsuarioNegocio`. |
| Usuario inactivo | `if (!usuario.Activo) throw ...`. |
| Acceso al sistema | Abre `FormPrincipal` y guarda `Sesion.UsuarioActual`. |

### 12.5 Punto 4.2 — Administración de usuarios

| Exigencia | Implementación |
|---|---|
| Crear | `FormUsuarioEditar` + `UsuarioNegocio.Guardar`. |
| Modificar | Mismo formulario con `_esNuevo = false`. |
| Desactivar | `UsuarioNegocio.Desactivar`; no permite autodesactivarse. |
| Eliminar | `UsuarioNegocio.Eliminar`; solo administrador, sin historial y sin dejar el sistema sin administradores. |
| Consultar | `FormUsuarios` con búsqueda y filtro de activos. |
| Establecer contraseña | Campo + BCrypt + política. |
| Asignar rol | ComboBox de roles. |
| Roles | Administrador, Vendedor, Supervisor. |
| Restringir funcionalidades | Menú visible por rol + `ExigirAdministrador`. |

### 12.6 Punto 4.3 — Dashboard

| Exigencia | Implementación |
|---|---|
| Pantalla posterior al login | `FormPrincipal` es la pantalla principal; el módulo **Inicio** abre el Dashboard. |
| Vista general | Tarjetas de indicadores y gráfico. |
| Total vendido hoy | `DashboardDatos.TotalVendidoHoy`. |
| Cantidad de ventas | `CantidadVentasHoy`. |
| Productos registrados | `TotalItems`. |
| Stock bajo | `ProductosStockBajo`. |
| Clientes | `TotalClientes`. |
| Últimas ventas | `UltimasVentas(10)`. |
| Usuario conectado | `lblUsuario` en `FormPrincipal` y en `FormDashboard`. |
| Datos desde MySQL | Todas las consultas usan `ConexionBD`. |

### 12.7 Punto 4.4 — Gestión de productos o servicios

| Exigencia | Implementación |
|---|---|
| Registrar | `FormItemEditar` + `ItemNegocio.Guardar`. |
| Modificar | Mismo formulario en modo edición. |
| Desactivar | `ItemNegocio.Desactivar`. |
| Eliminar | `ItemNegocio.Eliminar`; solo administrador y sin ventas, ingresos ni turnos asociados. |
| Consultar | `FormItems`. |
| Buscar | Filtro por nombre. |
| Filtrar | Tipo, categoría y stock bajo. |
| Datos del negocio | Nombre, tipo, precio, categoría, duración, stock, stock mínimo, costo, activo. |
| Adaptación sin stock | Servicios usan duración y no stock; productos usan stock. |
| Peluquería | Servicios y turnos como mecanismo equivalente. |

### 12.8 Punto 4.5 — Categorías

| Exigencia | Implementación |
|---|---|
| Organizar productos/servicios | `items.id_categoria`. |
| Administrar categorías | `FormCategorias` + `FormCategoriaEditar`. |
| Categorías de peluquería | Cortes, Coloración, Tratamientos, Productos. |
| Baja lógica | `activo`. |

### 12.9 Punto 4.6 — Gestión de clientes

| Exigencia | Implementación |
|---|---|
| Registrar | `FormClienteEditar`. |
| Modificar | Mismo formulario. |
| Consultar | `FormClientes`. |
| Buscar | DNI, nombre o apellido. |
| Desactivar | `ClienteNegocio.Desactivar`. |
| Eliminar | `ClienteNegocio.Eliminar`; solo administrador, sin ventas/cuenta corriente/turnos y nunca el Consumidor Final. |
| Identificación suficiente | Nombre, apellido, DNI único y teléfono. |

### 12.10 Punto 4.7 — Registro de ventas

| Exigencia | Implementación |
|---|---|
| Cliente | ComboBox obligatorio. |
| Productos/servicios | ComboBox de ítems activos. |
| Cantidades | `numCantidad`. |
| Precio | Se relee de la base. |
| Subtotal | `DetalleVenta.Subtotal`. |
| Descuento | `numDescuento`, validado contra subtotal. |
| Total | `Venta.Total` y cálculo en pantalla. |
| Medio de pago | Efectivo, tarjeta, transferencia, cuenta corriente. |
| Cálculo automático | No se pide total manual. |
| Cantidad negativa | `DetalleVenta` exige cantidad > 0. |
| Producto inexistente | `VentaNegocio` relee y rechaza ítems inactivos/inexistentes. |
| Sin stock | Validación en UI + UPDATE condicional en SQL. |
| Venta vacía | `VentaNegocio` exige al menos un detalle. |
| Datos obligatorios | Cliente, usuario, medio de pago y tarjeta cuando corresponde. |
| Caja abierta | `FormNuevaVenta` muestra un aviso y `VentaNegocio.Registrar` rechaza la venta si no hay caja abierta. |

### 12.11 Punto 4.8 — Detalle de venta

| Exigencia | Implementación |
|---|---|
| Múltiples ítems | `List<DetalleVenta>` y grilla del carrito. |
| Guardar cabecera y detalle | `ventas` + `detalle_venta`. |
| Precio histórico | `detalle_venta.precio`. |
| Subtotal | `cantidad * precio`. |
| Total | Cabecera `total`. |
| Consulta | `FormDetalleVenta` y `FormTicket`. |

### 12.12 Punto 4.9 — Control de stock y equivalente

| Exigencia | Implementación |
|---|---|
| Actualización automática | `UPDATE items.stock` dentro de la transacción. |
| Impedir vender más que el stock | Validación en UI + condición `stock >= cantidad`. |
| Stock bajo | `Item.StockBajo`, filtro “Solo stock bajo” y contador del Dashboard. |
| Peluquería con servicios | Se implementa además agenda/turnos, duración, disponibilidad y estados. |
| Justificación | El stock se aplica solo a productos; los servicios se modelan con turnos y duración. |

**Mecanismo equivalente implementado:** `atenciones` con:
- turno u orden de llegada;
- empleado y modalidad;
- servicio y duración;
- fecha/hora;
- estados;
- detección de superposición;
- historial y cantidad del día.

### 12.13 Punto 4.10 — Historial de ventas

| Exigencia | Implementación |
|---|---|
| Visualizar ventas | `FormHistorialVentas`. |
| Detalle de una venta | `FormDetalleVenta`. |
| Buscar venta | Por número, cliente, usuario y empleado. |
| Filtrar por fecha | `dtpDesde` y `dtpHasta`. |
| Identificar usuario | Columna `Usuario`. |
| Filtros adicionales | Empleado, número de venta y paginación. |
| Impresión | Ticket y detalle. |
| Anulación | Motivo, devolución de stock y reversión de cuenta. |

### 12.14 Punto 4.11 — Submenús y navegación

```text
Inicio
Ventas
  ├── Nueva venta
  ├── Caja
  └── Historial de ventas
Clientes
  ├── Clientes
  └── Cuenta corriente
Turnos
  └── Atenciones
Gastos
Productos
  ├── Productos y servicios
  ├── Categorías
  ├── Proveedores
  ├── Ingreso de mercadería
  └── Historial de mercadería
Reportes
  ├── Diario
  ├── Mensual
  └── Avanzado
Administración
  ├── Empleados
  ├── Tarjetas de crédito
  ├── Usuarios
  ├── Auditoría
  └── Copia de seguridad
Cerrar sesión
Salir
```

La organización prioriza las tareas cotidianas: Gastos quedó como acceso de primer nivel; Proveedores, Ingreso e Historial de mercadería quedaron dentro de Productos; las opciones administrativas siguen restringidas por rol.

### 12.15 Punto 5 — Formularios

| Exigencia | Cumplimiento |
|---|---|
| Mínimo 10 formularios funcionales | **37** formularios con finalidad concreta. |
| No formularios de relleno | Cada uno cubre un flujo real. |
| Login, Dashboard, usuarios, productos, categorías, clientes, venta, historial, detalle, reportes | Todos presentes y varios adicionales. |

### 12.16 Punto 6 — Programación Orientada a Objetos

| Criterio | Implementación |
|---|---|
| Entidades principales | `Usuario`, `Item`, `Cliente`, `Venta`, `DetalleVenta`, `Categoria`, etc. |
| Uso real | Se usan en datos, negocio y presentación. |
| Encapsulamiento | Setters validados en `Cliente`, `Item`, `Categoria`, `Usuario`; campos privados. |
| Propiedades | Calculadas: `Total`, `Subtotal`, `StockBajo`, `NombreCompleto`, `Abierta`. |
| Métodos | `DescontarStock`, `Cancelar`, `AgregarDetalle`, `EsAdministrador`. |
| Constructores | Con parámetros y vacíos para mapeo. |
| Relaciones | `Venta` contiene `DetalleVenta`; `Item` tiene `Categoria`; `Atencion` relaciona cliente/empleado/servicio. |
| Composición | Venta-detalle e ingreso-detalle. |
| Separación de responsabilidades | Capas y servicios. |
| Evitar lógica en botones | La lógica central está en `Negocio` y `Datos`; los formularios coordinan. |

### 12.17 Punto 7 — Validaciones

| Exigencia | Implementación |
|---|---|
| Campos obligatorios | `ExigirTexto` y validaciones de UI. |
| Tipos de datos | `TryParse`, `NumericUpDown`, parámetros tipados. |
| Valores numéricos | Importes y enteros validados. |
| Negativos | Precio, costo, montos y cantidades. |
| Longitud | `ExigirLongitudMaxima` y `MaxLength`. |
| Duplicados | DNI, usuario, categoría, ítem, proveedor y CUIT. |
| Inexistencia | Búsquedas y excepciones. |
| Cantidades | Detalle de venta e ingreso. |
| Stock | UI + condición SQL. |
| Mensajes claros | `MessageBox` contextual y `Mensajes`. |

### 12.18 Punto 8 — Base de datos

| Exigencia | Implementación |
|---|---|
| MySQL | Base `peluqueria`. |
| Usuarios | `usuarios`, `roles`. |
| Productos/servicios | `items`. |
| Categorías | `categorias`. |
| Clientes | `clientes`. |
| Ventas | `ventas`. |
| Detalle de ventas | `detalle_venta`. |
| Tablas adicionales | Caja, cuenta corriente, gastos, proveedores, mercadería, atenciones, auditoría, bancos y tarjetas. |
| Claves primarias | Todas las tablas. |
| Claves foráneas | Relaciones explícitas. |
| Tipos adecuados | DECIMAL(12,2), DATETIME, CHAR(8), ENUM, TINYINT(1). |
| Restricciones | UNIQUE, CHECK, FK, NOT NULL. |
| Sin duplicación innecesaria | Entidades separadas y snapshots justificados. |

### 12.19 Punto 9 — Operaciones CRUD

| Entidad | Create | Read | Update | Delete |
|---|---|---|---|---|
| Usuarios | `UsuarioDatos.Insertar` | `Listar` / `ObtenerPorNombre` | `Actualizar` | `Desactivar` o `Eliminar` (solo sin historial) |
| Clientes | `ClienteDatos.Insertar` | `Listar` / `ObtenerPorDni` | `Actualizar` | `Desactivar` o `Eliminar` (solo sin historial) |
| Ítems | `ItemDatos.Insertar` | `Listar` / `ObtenerPorId` | `Actualizar` | `Desactivar` o `Eliminar` (solo sin historial) |
| Categorías | `CategoriaDatos.Insertar` | `Listar` | `Actualizar` | `Desactivar` |
| Empleados | `EmpleadoDatos.Insertar` | `Listar` | `Actualizar` | `Desactivar` |
| Proveedores | `ProveedorDatos.Insertar` | `Listar` / `ObtenerPorId` | `Actualizar` | `Desactivar` |
| Bancos | `BancoDatos.Insertar` | `Listar` | `Actualizar` | `Desactivar` (o `Eliminar` si no tiene tarjetas) |
| Tarjetas | `TarjetaCreditoDatos.Insertar` | `Listar` | `Actualizar` | `Desactivar` |
| Ventas | `VentaDatos.Registrar` | `Listar` / `ObtenerCabecera` | No se edita; se anula | `anulada = 1` |
| Gastos | `GastoDatos.Insertar` | `Listar` | `Actualizar` | No se borra; historial |
| Ingresos | `IngresoMercaderiaDatos.Insertar` | `Listar` / `ObtenerPorId` | No se edita | Historial |
| Atenciones | `AtencionDatos.Insertar` | `Listar` | `Actualizar` | `Cancelar` o `Eliminar` (solo administrador) |
| Cajas | `CajaDatos.Abrir` | `ObtenerAbierta` / `Listar` | `Cerrar` | No se borra |
| Movimientos CC | `InsertarMovimiento` | `ListarMovimientos` | No se edita | Historial |

**Criterio:** la información histórica no se elimina físicamente. Se usa `activo = 0`, `anulada = 1` o estados. Excepciones controladas, siempre para el administrador y solo si el registro no tiene dependencias: bancos sin tarjetas, ítems sin ventas/ingresos/turnos, clientes sin ventas/cuenta corriente/turnos (nunca el Consumidor Final), atenciones y usuarios sin historial (ni el propio usuario ni el último administrador activo).

### 12.20 Punto 10 — Búsquedas y filtros

| Pantalla | Búsqueda/filtro |
|---|---|
| Ventas | Fechas, usuario, empleado, número, paginación. |
| Clientes | DNI, nombre, apellido, activos. |
| Ítems | Nombre, tipo, categoría, stock bajo, activos. |
| Categorías | Nombre, activos. |
| Usuarios | Nombre, activos. |
| Empleados | Nombre, activos. |
| Proveedores | Nombre, CUIT, email, activos. |
| Gastos | Rango de fechas, concepto/categoría. |
| Mercadería | Rango de fechas, proveedor, paginación. |
| Auditoría | Rango de fechas, usuario, acción, paginación. |
| Caja | Rango de fechas, paginación. |
| Atenciones | Texto y estado. |

Todos los filtros se aplican en SQL o sobre datos recién traídos de MySQL, no sobre valores escritos a mano.

### 12.21 Punto 11 — Reportes

Se supera el mínimo de dos:

1. **Reporte diario**: resumen, medios de pago, productos más vendidos, gastos por categoría y ventas por empleado.
2. **Reporte mensual**: ventas por día, medios de pago, gastos y empleados.
3. **Reporte avanzado**: clientes con más compras, ventas por usuario y servicios más solicitados.
4. **Dashboard**: indicadores del día.
5. **Historiales**: ventas, caja, mercadería y auditoría.

### 12.22 Punto 12 — Arquitectura y organización

| Exigencia | Implementación |
|---|---|
| Código organizado | Carpetas por capa y responsabilidad. |
| Presentación → Negocio → Datos → BD | Dependencia respetada. |
| Evitar SQL en eventos `Click` | El SQL está en `Datos`; los formularios llaman a `Negocio`. |
| Separación de responsabilidades | Modelos, DAO, servicios, UI y utilidades. |

---

## 13. Pruebas y verificación

### 13.1 Proyecto de pruebas

`TFI_1.Tests` contiene **101 pruebas** que no necesitan MySQL:

| Archivo | Qué cubre |
|---|---|
| `CajaTests.cs` | Cálculo de efectivo esperado. |
| `ClienteTests.cs` | DNI, consumidor final, nombre completo. |
| `ContrasenaTests.cs` | Política de contraseñas. |
| `DetalleVentaTests.cs` | Subtotal, cantidad y precio. |
| `ItemTests.cs` | Stock, stock bajo, descuento, precio. |
| `MensajesTests.cs` | Descripción de errores. |
| `ValidacionesTests.cs` | DNI, email, importes, fechas, turnos, longitudes. |
| `VentaTests.cs` | Subtotal, descuento, recargo y total. |

### 13.2 Comandos de verificación

```powershell
dotnet build TFI_1/TFI_1.csproj --no-incremental
dotnet test TFI_1.Tests/TFI_1.Tests.csproj
dotnet format whitespace TFI_1.slnx --verify-no-changes --exclude "**/*.Designer.cs"
```

### 13.3 Resultado esperado

- Compilación: **0 advertencias, 0 errores**.
- Pruebas: **101 aprobadas**.
- Formato: sin cambios pendientes en archivos que no sean `.Designer.cs`.

> Las pruebas son de lógica y no reemplazan una prueba manual con MySQL real. Para la defensa conviene ejecutar el sistema con la base creada.

---

## 14. Guía de defensa y demostración

### 14.1 Recorrido sugerido (10–15 minutos)

1. Mostrar `peluqueria.sql` y explicar las 20 tablas y las FK.
2. Iniciar sesión como `admin` y mostrar el Dashboard.
3. Crear un cliente y un ítem producto/servicio.
4. Intentar registrar una venta con la caja cerrada para mostrar el aviso.
5. Abrir la caja desde **Ventas → Caja** con un fondo inicial.
6. Registrar una venta con dos ítems, descuento y tarjeta.
7. Mostrar el ticket y el descuento de stock.
8. Consultar el historial y el detalle.
9. Anular la venta y mostrar la devolución de stock.
10. Registrar un ingreso/egreso de caja y cerrar con diferencia.
11. Registrar un turno y mostrar la detección de superposición.
12. Registrar un gasto y verlo en el reporte diario.
13. Mostrar un reporte avanzado.
14. Abrir auditoría y backup.
15. Cerrar sesión y entrar como `vendedor` para mostrar las restricciones.

### 14.2 Preguntas probables

**¿Dónde está la lógica de negocio?**  
En `TFI_1/Negocio`; los formularios solo coordinan y muestran.

**¿Dónde está el SQL?**  
En `TFI_1/Datos`; no está en los eventos `Click`.

**¿Cómo se evita el stock negativo?**  
Doble barrera: validación en la venta y `UPDATE ... WHERE stock >= cantidad` dentro de la transacción.

**¿Por qué no se borran ventas?**  
Por trazabilidad; se marca `anulada = 1` y se revierte el stock/cuenta.

**¿Cómo se protegen las contraseñas?**  
BCrypt; nunca se guarda texto plano. La política se exige al crear o cambiar.

**¿Cómo se aplican los roles?**  
Menú visible por rol y `ExigirAdministrador` en cada acción administrativa.

**¿Por qué hay tablas que parecen duplicar datos?**  
`detalle_venta.precio` y `ingresos_mercaderia.proveedor` son snapshots históricos deliberados.

**¿Qué pasa si falla la venta?**  
`VentaDatos.Registrar` usa transacción y `Rollback`; no quedan cabeceras huérfanas.

**¿Cómo se calcula el efectivo esperado?**  
`inicial + ventas en efectivo + ingresos - egresos`; las anuladas no cuentan. La caja muestra las anulaciones del turno en una pestaña separada para que queden registradas, pero no las suma dos veces.

**¿Se puede vender sin caja?**  
No. La venta exige una caja abierta; si no la hay, se muestra un aviso y no se registra.

**¿La cuenta corriente bloquea la venta?**  
No. Avisa si supera `LimiteDeCuentaCorriente`; el usuario decide.

### 14.3 Puntos a remarcar

- 37 formularios, muy por encima del mínimo de 10.
- 20 tablas con PK, FK, UNIQUE, CHECK e índices.
- 101 pruebas automatizadas.
- Separación real en capas.
- Transacciones en operaciones críticas.
- Baja lógica y auditoría.
- Adaptación del negocio: stock para productos y turnos para servicios.

---

## 15. Observaciones y mejoras detectadas

Estas observaciones no impiden la entrega, pero conviene revisarlas antes de la defensa:

1. **La caja no está asociada por FK a la venta**: el efectivo se calcula por rango de fechas. Es una decisión simple y funciona, pero si se abren/cierran cajas varias veces el mismo día, el rango debe interpretarse correctamente.
2. **La caja es obligatoria para vender**: si está cerrada, `FormNuevaVenta` muestra un `MessageBox` de advertencia y `VentaNegocio.Registrar` rechaza la operación. El efectivo se calcula por rango de fechas, no por una FK entre venta y caja.
3. **Las pruebas no cubren MySQL real**: son de dominio y validaciones. Para la defensa, hacer una prueba manual completa.
4. **Los diseñadores tienen cambios locales de Visual Studio** que no deben commitearse junto con la documentación. Mantener fuera del commit los `.Designer.cs`/`.resx` modificados y `TFI_1/Archivos.md/` salvo pedido explícito.

---

## 16. Apéndice: inventario de archivos

### 16.1 Raíz

| Archivo/carpeta | Función |
|---|---|
| `README.md` | Guía funcional, instalación y estado. |
| `DOCUMENTACION_TECNICA.md` | Este documento. |
| `TFI_1.slnx` | Solución con app y pruebas. |
| `BaseDatos/peluqueria.sql` | Esquema y datos iniciales. |
| `BaseDatos/crear_usuario_app.sql` | Usuario MySQL con permisos mínimos. |

### 16.2 `TFI_1`

| Archivo/carpeta | Función |
|---|---|
| `Program.cs` | Arranque, errores globales y login. |
| `Validaciones.cs` | Reglas compartidas. |
| `App.config` | Conexión y configuración local. |
| `App.config.example` | Plantilla sin credenciales reales. |
| `Models/` | 18 clases + enums. |
| `Datos/` | 19 clases de acceso y utilidades. |
| `Negocio/` | 20 clases de reglas y soporte. |
| `Presentacion/` | 37 formularios + `Mensajes`. |

### 16.3 Modelos

`Atencion`, `Banco`, `Caja`, `Categoria`, `Cliente`, `CuentaCorriente`, `DetalleVenta`, `Empleado`, `Gasto`, `IngresoMercaderia`, `IngresoMercaderiaDetalle`, `Item`, `MovimientoCuentaCorriente`, `Proveedor`, `Rol`, `TarjetaCredito`, `Usuario`, `Venta`.

### 16.4 Datos

`AtencionDatos`, `AuditoriaDatos`, `BancoDatos`, `CajaDatos`, `CategoriaDatos`, `ClienteDatos`, `ConexionBD`, `CuentaCorrienteDatos`, `DashboardDatos`, `EmpleadoDatos`, `GastoDatos`, `IngresoMercaderiaDatos`, `ItemDatos`, `Parametros`, `ProveedorDatos`, `ReporteDatos`, `TarjetaCreditoDatos`, `UsuarioDatos`, `VentaDatos`.

### 16.5 Negocio

`AtencionNegocio`, `AuditoriaNegocio`, `BackupNegocio`, `BancoNegocio`, `CajaNegocio`, `CategoriaNegocio`, `ClienteNegocio`, `Configuracion`, `CuentaCorrienteNegocio`, `DashboardNegocio`, `EmpleadoNegocio`, `GastoNegocio`, `IngresoMercaderiaNegocio`, `ItemNegocio`, `ProveedorNegocio`, `ReporteNegocio`, `Sesion`, `TarjetaCreditoNegocio`, `UsuarioNegocio`, `VentaNegocio`.

### 16.6 Formularios

`FormAnularVenta`, `FormAtencionEditar`, `FormAtenciones`, `FormAuditoria`, `FormBackup`, `FormBancoEditar`, `FormCaja`, `FormCategoriaEditar`, `FormCategorias`, `FormCerrarCaja`, `FormClienteEditar`, `FormClientes`, `FormCuentaCorriente`, `FormDashboard`, `FormDetalleVenta`, `FormEmpleadoEditar`, `FormEmpleados`, `FormGastos`, `FormHistorialCaja`, `FormHistorialMercaderia`, `FormHistorialVentas`, `FormIngresoMercaderia`, `FormItemEditar`, `FormItems`, `FormLog`, `FormNuevaVenta`, `FormPrincipal`, `FormProveedorEditar`, `FormProveedores`, `FormReporteAvanzado`, `FormReporteDiario`, `FormReporteMensual`, `FormTarjetaDeCredito`, `FormTarjetaEditar`, `FormTicket`, `FormUsuarioEditar`, `FormUsuarios`.

---

**Fin del documento.**
