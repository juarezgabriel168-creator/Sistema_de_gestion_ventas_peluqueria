# Sistema de Gestión de Peluquería

Aplicación de escritorio para administrar una peluquería: ventas, caja,
clientes, cuentas corrientes, turnos/atenciones, stock de productos,
gastos, reportes y usuarios.

Está hecha en **C# con Windows Forms (.NET)** y guarda los datos en una
base de datos **MySQL**.

---

## Funcionalidades

| Módulo | Pantalla | Qué permite hacer |
|---|---|---|
| Inicio | Dashboard | Ver totales del día (vendido, ventas, clientes, stock bajo) y un gráfico de los últimos días. |
| Ventas | Nueva venta | Armar el carrito con productos y servicios, elegir medio de pago (efectivo, tarjeta, transferencia o cuenta corriente) y registrar la venta. Al registrar, ofrece ver el ticket. |
| Ventas | Historial de ventas | Buscar y ver las ventas realizadas por rango de fechas, empleado o número de venta, ver el detalle de cada una y ver/imprimir el ticket. |
| Productos | Productos y servicios | Alta, edición, activación/desactivación y filtros de productos y servicios. |
| Productos | Categorías | Administrar las categorías de productos y servicios. |
| Clientes | Clientes | Alta, edición, activación/desactivación y búsqueda de clientes. |
| Clientes | Cuenta corriente | Ver el saldo y los movimientos de cada cliente y registrar pagos (abonos). |
| Turnos | Atenciones | Registrar turnos y órdenes de llegada, y cambiar su estado (pendiente, en atención, finalizada, cancelada). |
| Administración | Usuarios | Crear y editar usuarios y asignarles un rol. |
| Administración | Empleados | Alta y edición de empleados (con turnos u orden de llegada). |
| Administración | Tarjetas de crédito | Administrar bancos y tarjetas, con su porcentaje de recargo y cantidad de cuotas. |
| Administración | Gastos | Registrar y consultar los gastos del negocio por rango de fechas, con el total del período y el usuario que los cargó. |
| Administración | Proveedores | Alta, edición, activación/desactivación y búsqueda de proveedores (nombre, CUIT, teléfono, email y dirección). |
| Administración | Ingreso de mercadería | Registrar la compra de productos a un proveedor de la lista, actualizando stock y costo. |
| Administración | Historial de mercadería | Consultar los ingresos por rango de fechas y proveedor, ver el detalle de cada uno (productos, cantidades y costos) y el total del período. |
| Reportes | Reporte diario / mensual | Ver ventas, gastos e ingresos por período. |

En la barra de menú también están **Cerrar sesión** (vuelve a la pantalla de
login para cambiar de usuario, sin cerrar el programa) y **Salir** (cierra
la aplicación).

En los listados (clientes, productos/servicios, categorías, empleados,
usuarios, bancos y tarjetas), el botón **Desactivar** se convierte en
**Reactivar** cuando el registro seleccionado está inactivo, así se puede
volver a activar sin tener que entrar a editar.

Cada listado tiene además un tilde **Solo Activos** (viene marcado por
defecto): si lo dejás marcado, solo se ven los registros activos; si lo
desmarcás, aparecen también los inactivos (para poder reactivarlos).

---

## Tecnologías

- **Lenguaje:** C# (.NET 10, Windows Forms)
- **Base de datos:** MySQL 8.x
- **Librerías (NuGet):**
  - `MySql.Data` 26.7.0 — conexión a MySQL
  - `BCrypt.Net-Next` 4.2.0 — encriptado de contraseñas

El código está organizado en capas:

```
Presentacion  ->  Negocio  ->  Datos  ->  (MySQL)
                   Models  (compartido)
```

---

## Requisitos

Antes de empezar hay que tener instalado:

1. **Windows** 10 u 11.
2. **.NET SDK 10** — se descarga de https://dotnet.microsoft.com/download
   (para comprobar: `dotnet --version`).
3. **MySQL Server 8.x** en ejecución — https://dev.mysql.com/downloads/mysql/
   (opcional pero recomendado: **MySQL Workbench** para administrar la base).
4. **Visual Studio 2022** (17.13 o superior) con la carga de trabajo
   *Desarrollo de escritorio de .NET* — opcional, para trabajar con el
   diseñador visual. Los comandos de abajo alcanzan para compilar y ejecutar.

---

## Puesta en marcha paso a paso

### 1. Crear la base de datos

Ejecutar el script `BaseDatos/peluqueria.sql`. Crea la base `peluqueria`
con todas sus tablas y datos de ejemplo.

- **Opción A (línea de comandos):**
  ```bash
  mysql -u root -p < BaseDatos/peluqueria.sql
  ```
- **Opción B (MySQL Workbench):** abrir el archivo y ejecutarlo completo
  (botón del rayo).

> ⚠️ El script **borra y vuelve a crear las tablas**. Si ya tenías datos
> cargados, se pierden. Usalo solo para la primera instalación o para
> reiniciar la base desde cero.

### 2. Configurar la conexión

Copiar el archivo de ejemplo y ajustar la cadena de conexión con los datos
de tu MySQL. El archivo real (`App.config`) está excluido de Git para no
subir contraseñas al repositorio:

```powershell
copy TFI_1\App.config.example TFI_1\App.config
```

Luego editar `TFI_1/App.config`:

```xml
<connectionStrings>
  <add name="Peluqueria"
       connectionString="Server=localhost;Port=3306;Database=peluqueria;Uid=root;Pwd=TU_CONTRASEÑA;CharSet=utf8mb4;"
       providerName="MySql.Data.MySqlClient" />
</connectionStrings>
```

- `Server` → dónde está MySQL (normalmente `localhost`).
- `Uid` / `Pwd` → usuario y contraseña de MySQL.
- `Database` → dejar `peluqueria`.

### 3. Compilar y ejecutar

Desde la carpeta raíz del proyecto:

```bash
dotnet restore
dotnet build TFI_1/TFI_1.csproj
dotnet run --project TFI_1/TFI_1.csproj
```

O, desde **Visual Studio**: abrir la solución `TFI_1.slnx`, y presionar
**F5** (o el botón ▶ Iniciar).

### 4. Iniciar sesión

Al arrancar aparece la pantalla de login. Usuarios de prueba:

| Usuario | Contraseña | Rol |
|---|---|---|
| `admin` | `admin123` | Administrador |
| `vendedor` | `vendedor123` | Vendedor |
| `supervisor` | `supervisor123` | Supervisor |

---

## Roles y permisos

El sistema tiene tres roles. Al iniciar sesión, el menú se arma según el
rol del usuario:

| Menú | Administrador | Supervisor | Vendedor |
|---|:---:|:---:|:---:|
| Inicio (Dashboard) | ✔ | ✔ | ✔ |
| Ventas | ✔ | ✔ | ✔ |
| Productos y servicios | ✔ | ✔ | ✔ |
| Clientes (incluye Cuenta corriente) | ✔ | ✔ | ✔ |
| Turnos | ✔ | ✔ | ✔ |
| Administración (usuarios, empleados, tarjetas, gastos, proveedores, mercadería) | ✔ | | |
| Reportes | ✔ | ✔ | |
| Categorías (dentro de Productos) | ✔ | | |

---

## Estructura del proyecto

```
TFI_1/
├── BaseDatos/
│   └── peluqueria.sql        # Script de creación de la base de datos
├── TFI_1/
│   ├── App.config            # Cadena de conexión a MySQL
│   ├── Program.cs            # Punto de entrada (abre el login)
│   ├── Models/               # Clases del dominio (Cliente, Venta, Item, ...)
│   ├── Datos/                # Acceso a MySQL (consultas SQL)
│   ├── Negocio/              # Reglas de negocio y validaciones
│   └── Presentacion/         # Formularios (pantallas de la aplicación)
├── TFI_1.slnx                # Solución de Visual Studio
└── README.md
```

Cada formulario se compone de tres archivos:

- `FormX.cs` → la lógica.
- `FormX.Designer.cs` → la definición de los controles (lo que se ve en [Diseño]).
- `FormX.resx` → los recursos.

---

## Notas y limitaciones conocidas

- La contraseña de MySQL está escrita en texto plano en `App.config`
  (práctica habitual en proyectos de práctica, pero **no** recomendable en
  producción).
- El usuario `root` de MySQL se usa directamente; lo ideal sería crear un
  usuario con permisos acotados solo sobre la base `peluqueria`.
- Las ventas **no se pueden anular ni editar** una vez registradas.
- Cambiar las contraseñas de los usuarios de prueba después de la primera
  instalación (menú **Administración → Usuarios**).

---

## Trabajo Final Integrador

Proyecto desarrollado como Trabajo Final Integrador de la Tecnicatura en
Programación.
