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
| Inicio | Dashboard | Ver totales del día (vendido, ventas, clientes, stock bajo) y un gráfico de las últimas 10 ventas, con escala, importes y detalle al pasar el mouse. |
| Ventas | Nueva venta | Armar el carrito con productos y servicios, elegir medio de pago (efectivo, tarjeta, transferencia o cuenta corriente) y registrar la venta. Al registrar, ofrece ver el ticket. |
| Ventas | Historial de ventas | Buscar y ver las ventas realizadas por rango de fechas, empleado o número de venta, ver el detalle de cada una y ver/imprimir el ticket. Permite **anular** una venta indicando el motivo: se devuelve el stock, se revierte el cargo de la cuenta corriente y la venta queda marcada como anulada (no se borra). |
| Ventas | Caja | Abrir el turno con un fondo inicial, registrar ingresos y egresos de efectivo, ver las ventas del turno por medio de pago y cerrar la caja con el efectivo contado y la diferencia contra lo esperado. Incluye el historial de cierres. |
| Productos | Productos y servicios | Alta, edición, activación/desactivación y filtros de productos y servicios. Los **servicios** exigen una duración y no manejan stock ni costo; los **productos** manejan stock, stock mínimo y costo de compra, y no duración. |
| Productos | Categorías | Administrar las categorías de productos y servicios. |
| Clientes | Clientes | Alta, edición, activación/desactivación y búsqueda de clientes. |
| Clientes | Cuenta corriente | Ver el saldo y los movimientos de cada cliente y registrar pagos (abonos). Avisa cuando la deuda supera los **$50.000** (al vender a cuenta corriente y al consultar la cuenta). El aviso no bloquea la venta. |
| Turnos | Atenciones | Registrar turnos y órdenes de llegada, y cambiar su estado (pendiente, en atención, finalizada, cancelada). No permite turnos en el pasado ni superpuestos para el mismo peluquero. |
| Administración | Usuarios | Crear y editar usuarios y asignarles un rol. Las contraseñas nuevas deben tener al menos 8 caracteres, con una mayúscula, una minúscula y un número, y no pueden contener el nombre de usuario. |
| Administración | Empleados | Alta y edición de empleados (con turnos u orden de llegada). |
| Administración | Tarjetas de crédito | Administrar bancos y tarjetas, con su porcentaje de recargo y cantidad de cuotas. |
| Administración | Gastos | Registrar y consultar los gastos del negocio por rango de fechas, con el total del período y el usuario que los cargó. |
| Administración | Proveedores | Alta, edición, activación/desactivación y búsqueda de proveedores (nombre, CUIT, teléfono, email y dirección). |
| Administración | Ingreso de mercadería | Registrar la compra de productos a un proveedor de la lista, actualizando stock y costo. Solo admite productos (no servicios). |
| Administración | Historial de mercadería | Consultar los ingresos por rango de fechas y proveedor, ver el detalle de cada uno (productos, cantidades y costos) y el total del período. |
| Administración | Auditoría | Consultar quién hizo cada operación sensible (ventas, anulaciones, pagos de cuenta corriente, gastos, mercadería y usuarios), filtrando por rango de fechas, usuario y tipo de acción. |
| Administración | Copia de seguridad | Generar un archivo `.sql` con toda la base (tablas y datos) en la carpeta elegida, con fecha y hora en el nombre. Solo para administradores; incluye cómo restaurarla. |
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

### 2. Crear el usuario de la base de datos

La aplicación no se conecta como `root`, sino con un usuario propio que
solo puede leer y escribir datos. Ejecutar como root el script
`BaseDatos/crear_usuario_app.sql` (cambiando antes la contraseña de
ejemplo por una propia):

```bash
mysql -u root -p < BaseDatos/crear_usuario_app.sql
```

El script crea el usuario `peluqueria_app` con permisos `SELECT`,
`INSERT`, `UPDATE` y `DELETE` únicamente sobre la base `peluqueria`. No
puede crear ni borrar tablas.

### 3. Configurar la conexión

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
       connectionString="Server=localhost;Port=3306;Database=peluqueria;Uid=peluqueria_app;Pwd=TU_CONTRASEÑA;CharSet=utf8mb4;"
       providerName="MySql.Data.MySqlClient" />
</connectionStrings>
```

- `Server` → dónde está MySQL (normalmente `localhost`).
- `Uid` / `Pwd` → el usuario y la contraseña creados en el paso anterior.
- `Database` → dejar `peluqueria`.

### 3.1. Configuración opcional (`appSettings`)

El mismo `App.config` permite ajustar tres valores sin recompilar. Si se omiten,
la aplicación usa los valores por defecto:

| Clave | Por defecto | Para qué sirve |
| --- | --- | --- |
| `NombreNegocio` | `Peluquería` | Título de la ventana principal y encabezado del ticket. |
| `LimiteDeudaCuentaCorriente` | `50000` | Deuda a partir de la cual se avisa al vender a cuenta corriente (solo avisa, no bloquea). |
| `RutaMysqldump` | `mysqldump` | Programa que genera la copia de seguridad. Si no está en el PATH, poner la ruta completa. |

```xml
<appSettings>
  <add key="NombreNegocio" value="Peluquería" />
  <add key="LimiteDeudaCuentaCorriente" value="50000" />
  <add key="RutaMysqldump" value="mysqldump" />
</appSettings>
```

### 4. Compilar y ejecutar

Desde la carpeta raíz del proyecto:

```bash
dotnet restore
dotnet build TFI_1/TFI_1.csproj
dotnet run --project TFI_1/TFI_1.csproj
```

O, desde **Visual Studio**: abrir la solución `TFI_1.slnx`, y presionar
**F5** (o el botón ▶ Iniciar).

### 5. Iniciar sesión

Al arrancar aparece la pantalla de login. Usuarios de prueba:

| Usuario | Contraseña | Rol |
|---|---|---|
| `admin` | `admin123` | Administrador |
| `vendedor` | `vendedor123` | Vendedor |
| `supervisor` | `supervisor123` | Supervisor |

### 6. Copias de seguridad

En **Administración → Copia de seguridad** se elige la carpeta y se genera un
archivo `backup_peluqueria_AAAAMMDD_HHMMSS.sql` con toda la base (tablas y
datos). Conviene hacer una copia antes de actualizar el sistema o de hacer
cambios grandes.

Para **restaurar** una copia (con la aplicación cerrada), desde el símbolo del
sistema (cmd):

```bat
mysql --user=root --password peluqueria < "C:\ruta\backup_peluqueria_20260101_120000.sql"
```

En PowerShell, anteponer `cmd /c` al comando. El archivo incluye
`CREATE DATABASE` y `USE peluqueria`, así que la restauración reemplaza las
tablas de esa base. Hay que restaurar con un usuario que pueda crear y borrar
tablas (por ejemplo `root`), no con `peluqueria_app`.

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
| Administración (usuarios, empleados, tarjetas, gastos, proveedores, mercadería, auditoría, copia de seguridad) | ✔ | | |
| Reportes | ✔ | ✔ | |
| Categorías (dentro de Productos) | ✔ | | |

---

## Estructura del proyecto

```
TFI_1/
├── .editorconfig             # Reglas de estilo del código
├── BaseDatos/
│   ├── peluqueria.sql        # Script de creación de la base de datos
│   └── crear_usuario_app.sql # Usuario de MySQL con permisos acotados
├── TFI_1/
│   ├── App.config            # Cadena de conexión y configuración
│   ├── Program.cs            # Punto de entrada (abre el login)
│   ├── Validaciones.cs       # Reglas compartidas (DNI, email, importes, fechas)
│   ├── Models/               # Clases del dominio (Cliente, Venta, Item, ...)
│   ├── Datos/                # Acceso a MySQL (consultas y parámetros tipados)
│   ├── Negocio/              # Reglas de negocio y validaciones
│   └── Presentacion/         # Formularios (pantallas de la aplicación)
├── TFI_1.Tests/              # Pruebas automatizadas de la lógica
├── TFI_1.slnx                # Solución de Visual Studio
└── README.md
```

## Estilo del código

El proyecto sigue un estilo único definido en `.editorconfig`: indentación con
tabulaciones, archivos UTF-8, saltos de línea CRLF y `namespace` en una sola
línea. Los formularios generados por Visual Studio (`.Designer.cs`) conservan
la indentación con espacios.

Para comprobar que todo el código respeta el formato:

```bash
dotnet format whitespace TFI_1.slnx --verify-no-changes --exclude "**/*.Designer.cs"
```

## Pruebas automatizadas

El proyecto `TFI_1.Tests` prueba la lógica que **no depende de la base de
datos**: modelos, stock, ventas, caja, la política de contraseñas y las
validaciones compartidas (DNI, email, importes, fechas y turnos). No necesita
MySQL ni modifica datos.

```bash
dotnet test TFI_1.Tests/TFI_1.Tests.csproj
```

Son 99 pruebas de unidad; si alguna falla, el comando termina con error y
muestra el detalle.

La solución completa compila **sin advertencias** (`0 advertencias`), lo que
ayuda a detectar temprano campos sin usar o posibles valores nulos.

Cada formulario se compone de tres archivos:

- `FormX.cs` → la lógica.
- `FormX.Designer.cs` → la definición de los controles (lo que se ve en [Diseño]).
- `FormX.resx` → los recursos.

---

## Notas y limitaciones conocidas

- La contraseña de la base se guarda en texto plano en `TFI_1/App.config`
  (archivo local, excluido de Git). La aplicación se conecta con el usuario
  `peluqueria_app`, que solo tiene permisos de lectura y escritura sobre la
  base `peluqueria`.
- Las ventas **no se pueden editar** una vez registradas. Si hubo un error,
  se pueden **anular** (con motivo): se devuelve el stock y, si se pagó a cuenta
  corriente, se revierte el cargo. La venta anulada queda en el historial y no
  suma en los totales ni reportes.
- La **caja** no bloquea la venta cuando está cerrada: se puede vender igual,
  pero esa venta no queda asociada a ningún turno de caja. Conviene abrir la
  caja antes de empezar a vender para poder controlar el efectivo del turno.
- El **límite de aviso de deuda** en cuenta corriente es de **$50.000** por
  defecto y se cambia con la clave `LimiteDeudaCuentaCorriente` de
  `App.config` (ver la sección de configuración). Es solo un aviso: si el
  cliente supera el límite, la venta se puede confirmar igual.
- Las **copias de seguridad** se generan con `mysqldump` y no incluyen
  procedimientos ni triggers (la base no los usa). Las credenciales se pasan
  en un archivo temporal que se borra al terminar, para no exponerlas en la
  línea de comandos.
- Los **listados largos** (historial de ventas, auditoría, mercadería y caja)
  muestran **50 filas por página** con botones **Anterior/Siguiente**. El total
  y los importes siempre se calculan sobre todos los registros del período,
  no solo sobre la página visible.
- Las consultas usan **parámetros tipados** (`MySqlDbType`) en lugar de
  `AddWithValue`. Cada dato viaja con su tipo real (entero, decimal, fecha,
  texto, booleano o nulo), lo que evita conversiones implícitas y problemas
  con importes, fechas y campos opcionales.
- Los **errores** se muestran siempre con el mismo formato: una frase que
  explica qué se estaba haciendo y el detalle del problema. Los errores
  inesperados se registran en
  `%LocalAppData%\Peluqueria\errores.log` para poder revisarlos después.
- Cambiar las contraseñas de los usuarios de prueba después de la primera
  instalación (menú **Administración → Usuarios**). Los usuarios del script
  (`admin`/`admin123`, `vendedor`/`vendedor123`, `supervisor`/`supervisor123`)
  no cumplen la política de contraseñas: la política se exige al crear un
  usuario o al cambiar la contraseña, no al iniciar sesión.

---

## Trabajo Final Integrador

Proyecto desarrollado como Trabajo Final Integrador de la Tecnicatura en
Programación.
