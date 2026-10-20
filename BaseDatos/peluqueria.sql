-- =====================================================================
--  Sistema de Gestión de Peluquería  ·  Base de datos "peluqueria"
-- ---------------------------------------------------------------------
--  Motor: MySQL 8.x  /  MariaDB 10.4+
--
--  Este script crea la base completa (todas las tablas) y carga los
--  datos mínimos para poder iniciar sesión y usar el sistema.
--
--  FORMAS DE EJECUTARLO
--    A) Línea de comandos:
--         mysql -u root -p < peluqueria.sql
--    B) Abrir el archivo en MySQL Workbench o phpMyAdmin y ejecutarlo
--       completo (todo el contenido de una sola vez).
--
--  Es re-ejecutable: elimina las tablas si ya existen y las vuelve a
--  crear. ¡ATENCIÓN! Eso BORRA todos los datos existentes de la base.
--
--  USUARIOS CREADOS (la contraseña se guarda como hash BCrypt):
--       admin       / admin123       -> Administrador
--       vendedor    / vendedor123    -> Vendedor
--       supervisor  / supervisor123  -> Supervisor
--  >>> Cambiar estas contraseñas después de la primera instalación
--      (menú Usuarios).
-- =====================================================================

CREATE DATABASE IF NOT EXISTS peluqueria
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE peluqueria;

SET NAMES utf8mb4;

-- ---------------------------------------------------------------------
--  Limpieza (permite volver a ejecutar el script sin errores)
-- ---------------------------------------------------------------------
SET FOREIGN_KEY_CHECKS = 0;

DROP TABLE IF EXISTS ingresos_mercaderia_detalle;
DROP TABLE IF EXISTS ingresos_mercaderia;
DROP TABLE IF EXISTS movimientos_cuenta_corriente;
DROP TABLE IF EXISTS cuentas_corrientes;
DROP TABLE IF EXISTS gastos;
DROP TABLE IF EXISTS detalle_venta;
DROP TABLE IF EXISTS ventas;
DROP TABLE IF EXISTS atenciones;
DROP TABLE IF EXISTS tarjetas_credito;
DROP TABLE IF EXISTS bancos;
DROP TABLE IF EXISTS items;
DROP TABLE IF EXISTS categorias;
DROP TABLE IF EXISTS empleados;
DROP TABLE IF EXISTS clientes;
DROP TABLE IF EXISTS usuarios;
DROP TABLE IF EXISTS roles;

SET FOREIGN_KEY_CHECKS = 1;

-- =====================================================================
--  1. SEGURIDAD Y USUARIOS
-- =====================================================================

CREATE TABLE roles (
    id_rol  INT          NOT NULL AUTO_INCREMENT,
    nombre  VARCHAR(50)  NOT NULL,
    PRIMARY KEY (id_rol),
    UNIQUE KEY uq_roles_nombre (nombre)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

CREATE TABLE usuarios (
    id_usuario     INT          NOT NULL AUTO_INCREMENT,
    nombre_usuario VARCHAR(50)  NOT NULL,
    contrasena     VARCHAR(100) NOT NULL,   -- hash BCrypt, nunca texto plano
    id_rol         INT          NOT NULL,
    activo         TINYINT(1)   NOT NULL DEFAULT 1,
    PRIMARY KEY (id_usuario),
    UNIQUE KEY uq_usuarios_nombre (nombre_usuario),
    CONSTRAINT fk_usuarios_rol FOREIGN KEY (id_rol) REFERENCES roles (id_rol)
        ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

-- =====================================================================
--  2. PERSONAS
-- =====================================================================

CREATE TABLE clientes (
    id_cliente INT          NOT NULL AUTO_INCREMENT,
    nombre     VARCHAR(100) NOT NULL,
    apellido   VARCHAR(100) NOT NULL,
    dni        CHAR(8)      NOT NULL,
    telefono   VARCHAR(30)  NULL,
    activo     TINYINT(1)   NOT NULL DEFAULT 1,
    PRIMARY KEY (id_cliente),
    UNIQUE KEY uq_clientes_dni (dni)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

CREATE TABLE empleados (
    id_empleado INT          NOT NULL AUTO_INCREMENT,
    nombre      VARCHAR(100) NOT NULL,
    apellido    VARCHAR(100) NOT NULL,
    modalidad   ENUM('turnos', 'orden_llegada') NOT NULL DEFAULT 'orden_llegada',
    activo      TINYINT(1)   NOT NULL DEFAULT 1,
    PRIMARY KEY (id_empleado)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

-- =====================================================================
--  3. CATÁLOGO
-- =====================================================================

CREATE TABLE categorias (
    id_categoria INT          NOT NULL AUTO_INCREMENT,
    nombre       VARCHAR(100) NOT NULL,
    activo       TINYINT(1)   NOT NULL DEFAULT 1,
    PRIMARY KEY (id_categoria)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

-- items = servicios y productos. Los servicios usan duracion_min (y no
-- tienen stock); los productos usan stock / stock_minimo / costo.
CREATE TABLE items (
    id_item      INT           NOT NULL AUTO_INCREMENT,
    nombre       VARCHAR(150)  NOT NULL,
    tipo         ENUM('producto', 'servicio') NOT NULL,
    precio       DECIMAL(12,2) NOT NULL DEFAULT 0,
    duracion_min INT           NULL,
    stock        INT           NULL,
    stock_minimo INT           NULL,
    costo        DECIMAL(12,2) NULL,
    id_categoria INT           NOT NULL,
    activo       TINYINT(1)    NOT NULL DEFAULT 1,
    PRIMARY KEY (id_item),
    KEY ix_items_categoria (id_categoria),
    CONSTRAINT fk_items_categoria FOREIGN KEY (id_categoria) REFERENCES categorias (id_categoria)
        ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

-- =====================================================================
--  4. TARJETAS DE CRÉDITO
-- =====================================================================

CREATE TABLE bancos (
    id_banco INT          NOT NULL AUTO_INCREMENT,
    nombre   VARCHAR(100) NOT NULL,
    activo   TINYINT(1)   NOT NULL DEFAULT 1,
    PRIMARY KEY (id_banco)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

CREATE TABLE tarjetas_credito (
    id_tarjeta_credito INT           NOT NULL AUTO_INCREMENT,
    id_banco           INT           NOT NULL,
    tipo_tarjeta       VARCHAR(50)   NOT NULL,
    porcentaje_recargo DECIMAL(5,2)  NOT NULL DEFAULT 0,
    cuotas_max         INT           NOT NULL DEFAULT 1,
    activo             TINYINT(1)    NOT NULL DEFAULT 1,
    fecha_alta         DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id_tarjeta_credito),
    UNIQUE KEY uq_tarjetas_banco_tipo (id_banco, tipo_tarjeta),
    CONSTRAINT fk_tarjetas_banco FOREIGN KEY (id_banco) REFERENCES bancos (id_banco)
        ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

-- =====================================================================
--  5. VENTAS
-- =====================================================================

CREATE TABLE ventas (
    id_venta           INT           NOT NULL AUTO_INCREMENT,
    id_cliente         INT           NOT NULL,
    id_usuario         INT           NOT NULL,
    id_empleado        INT           NULL,
    fecha              DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    medio_pago         ENUM('efectivo', 'tarjeta', 'transferencia', 'cuenta_corriente')
                                     NOT NULL DEFAULT 'efectivo',
    id_tarjeta_credito INT           NULL,
    cantidad_cuotas    INT           NULL,
    descuento          DECIMAL(12,2) NOT NULL DEFAULT 0,
    recargo            DECIMAL(12,2) NOT NULL DEFAULT 0,
    total              DECIMAL(12,2) NOT NULL DEFAULT 0,
    -- La venta anulada no se borra: se marca para conservar el historial.
    anulada            TINYINT(1)    NOT NULL DEFAULT 0,
    fecha_anulacion    DATETIME      NULL,
    id_usuario_anulacion INT         NULL,
    motivo_anulacion   VARCHAR(255)  NULL,
    PRIMARY KEY (id_venta),
    KEY ix_ventas_cliente (id_cliente),
    KEY ix_ventas_fecha (fecha),
    KEY ix_ventas_anulada (anulada),
    CONSTRAINT fk_ventas_cliente FOREIGN KEY (id_cliente) REFERENCES clientes (id_cliente)
        ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT fk_ventas_usuario FOREIGN KEY (id_usuario) REFERENCES usuarios (id_usuario)
        ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT fk_ventas_empleado FOREIGN KEY (id_empleado) REFERENCES empleados (id_empleado)
        ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT fk_ventas_tarjeta FOREIGN KEY (id_tarjeta_credito) REFERENCES tarjetas_credito (id_tarjeta_credito)
        ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT fk_ventas_usuario_anulacion FOREIGN KEY (id_usuario_anulacion) REFERENCES usuarios (id_usuario)
        ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

CREATE TABLE detalle_venta (
    id_detalle INT           NOT NULL AUTO_INCREMENT,
    id_venta   INT           NOT NULL,
    id_item    INT           NOT NULL,
    cantidad   INT           NOT NULL,
    precio     DECIMAL(12,2) NOT NULL,
    subtotal   DECIMAL(12,2) NOT NULL,
    PRIMARY KEY (id_detalle),
    KEY ix_detalle_venta (id_venta),
    KEY ix_detalle_item (id_item),
    CONSTRAINT fk_detalle_venta FOREIGN KEY (id_venta) REFERENCES ventas (id_venta)
        ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT fk_detalle_item FOREIGN KEY (id_item) REFERENCES items (id_item)
        ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

-- =====================================================================
--  6. CUENTA CORRIENTE
-- =====================================================================

CREATE TABLE cuentas_corrientes (
    id_cuenta_corriente INT           NOT NULL AUTO_INCREMENT,
    id_cliente          INT           NOT NULL,
    saldo               DECIMAL(12,2) NOT NULL DEFAULT 0,   -- deuda acumulada
    fecha_actualizacion DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id_cuenta_corriente),
    UNIQUE KEY uq_cuentas_cliente (id_cliente),
    CONSTRAINT fk_cuentas_cliente FOREIGN KEY (id_cliente) REFERENCES clientes (id_cliente)
        ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

-- Movimientos de la cuenta: 'cargo' suma deuda, 'abono' la descuenta.
CREATE TABLE movimientos_cuenta_corriente (
    id_movimiento INT           NOT NULL AUTO_INCREMENT,
    id_cuenta     INT           NOT NULL,
    fecha         DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    tipo          ENUM('cargo', 'abono') NOT NULL,
    monto         DECIMAL(12,2) NOT NULL,
    concepto      VARCHAR(255)  NULL,
    id_venta      INT           NULL,
    id_usuario    INT           NOT NULL,
    PRIMARY KEY (id_movimiento),
    KEY ix_mov_cuenta (id_cuenta),
    CONSTRAINT fk_mov_cuenta FOREIGN KEY (id_cuenta) REFERENCES cuentas_corrientes (id_cuenta_corriente)
        ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT fk_mov_venta FOREIGN KEY (id_venta) REFERENCES ventas (id_venta)
        ON UPDATE CASCADE ON DELETE SET NULL,
    CONSTRAINT fk_mov_usuario FOREIGN KEY (id_usuario) REFERENCES usuarios (id_usuario)
        ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

-- =====================================================================
--  7. GASTOS E INGRESOS DE MERCADERÍA
-- =====================================================================

CREATE TABLE gastos (
    id_gasto    INT           NOT NULL AUTO_INCREMENT,
    fecha       DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    concepto    VARCHAR(150)  NOT NULL,
    monto       DECIMAL(12,2) NOT NULL,
    categoria   VARCHAR(100)  NULL,
    observacion VARCHAR(255)  NULL,
    id_usuario  INT           NOT NULL,
    fecha_alta  DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id_gasto),
    KEY ix_gastos_fecha (fecha),
    CONSTRAINT fk_gastos_usuario FOREIGN KEY (id_usuario) REFERENCES usuarios (id_usuario)
        ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

CREATE TABLE proveedores (
    id_proveedor INT          NOT NULL AUTO_INCREMENT,
    nombre       VARCHAR(150) NOT NULL,
    cuit         VARCHAR(20)  NULL,
    telefono     VARCHAR(40)  NULL,
    email        VARCHAR(120) NULL,
    direccion    VARCHAR(200) NULL,
    activo       TINYINT(1)   NOT NULL DEFAULT 1,
    PRIMARY KEY (id_proveedor),
    UNIQUE KEY ux_proveedores_nombre (nombre)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

CREATE TABLE ingresos_mercaderia (
    id_ingreso  INT           NOT NULL AUTO_INCREMENT,
    fecha       DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    id_proveedor INT          NULL,
    -- Se guarda además el nombre del proveedor tal como estaba al momento
    -- del ingreso, para que el historial no cambie si luego se lo renombra.
    proveedor   VARCHAR(150)  NULL,
    total       DECIMAL(12,2) NOT NULL DEFAULT 0,
    id_usuario  INT           NOT NULL,
    observacion VARCHAR(255)  NULL,
    PRIMARY KEY (id_ingreso),
    KEY ix_ingresos_fecha (fecha),
    KEY ix_ingresos_proveedor (id_proveedor),
    CONSTRAINT fk_ingresos_usuario FOREIGN KEY (id_usuario) REFERENCES usuarios (id_usuario)
        ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT fk_ingresos_proveedor FOREIGN KEY (id_proveedor) REFERENCES proveedores (id_proveedor)
        ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

CREATE TABLE ingresos_mercaderia_detalle (
    id_detalle_ingreso INT           NOT NULL AUTO_INCREMENT,
    id_ingreso         INT           NOT NULL,
    id_item            INT           NOT NULL,
    cantidad           INT           NOT NULL,
    costo_unitario     DECIMAL(12,2) NOT NULL,
    subtotal           DECIMAL(12,2) NOT NULL,
    PRIMARY KEY (id_detalle_ingreso),
    KEY ix_ingdet_ingreso (id_ingreso),
    KEY ix_ingdet_item (id_item),
    CONSTRAINT fk_ingdet_ingreso FOREIGN KEY (id_ingreso) REFERENCES ingresos_mercaderia (id_ingreso)
        ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT fk_ingdet_item FOREIGN KEY (id_item) REFERENCES items (id_item)
        ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

-- =====================================================================
--  8. ATENCIONES (turnos y órdenes de llegada)
-- =====================================================================

CREATE TABLE atenciones (
    id_atencion INT       NOT NULL AUTO_INCREMENT,
    id_cliente  INT       NOT NULL,
    id_empleado INT       NOT NULL,
    id_item     INT       NOT NULL,   -- servicio a realizar
    tipo        ENUM('turno', 'llegada') NOT NULL DEFAULT 'llegada',
    fecha_hora  DATETIME  NOT NULL,
    estado      ENUM('pendiente', 'en_atencion', 'finalizada', 'cancelada')
                          NOT NULL DEFAULT 'pendiente',
    PRIMARY KEY (id_atencion),
    KEY ix_atenciones_empleado (id_empleado, fecha_hora),
    KEY ix_atenciones_cliente (id_cliente),
    CONSTRAINT fk_atenciones_cliente FOREIGN KEY (id_cliente) REFERENCES clientes (id_cliente)
        ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT fk_atenciones_empleado FOREIGN KEY (id_empleado) REFERENCES empleados (id_empleado)
        ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT fk_atenciones_item FOREIGN KEY (id_item) REFERENCES items (id_item)
        ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;

-- =====================================================================
--  9. DATOS INICIALES
-- =====================================================================

-- Roles (los identificadores 1, 2 y 3 son los que usa el sistema).
INSERT INTO roles (id_rol, nombre) VALUES
    (1, 'Administrador'),
    (2, 'Vendedor'),
    (3, 'Supervisor');

-- Usuarios (contraseñas como hash BCrypt).
INSERT INTO usuarios (id_usuario, nombre_usuario, contrasena, id_rol, activo) VALUES
    (1, 'admin',      '$2a$11$/HTJJ2I8lty4D5KPVbtND.ihKQO86lknNJpSLaOvAxE2i.ajW7JrC', 1, 1),
    (2, 'vendedor',   '$2a$11$14MIlJMFfIIRQYLkXPy/0OxWOx38a0IJK9yKkNO4Ho9DME2qOv9fC', 2, 1),
    (3, 'supervisor', '$2a$11$z83MEbfKlgIl8PqoCd2yHuD0w9ZkCHD0cpBtA7oK1f4eyn1ptbE4C', 3, 1);

-- Clientes (incluye el "Consumidor Final" que usa el sistema para ventas sin datos).
INSERT INTO clientes (id_cliente, nombre, apellido, dni, telefono, activo) VALUES
    (1, 'Consumidor', 'Final',     '00000000', NULL,         1),
    (2, 'Juan',       'Rodríguez', '30123456', '1145551234', 1);

-- Empleados.
INSERT INTO empleados (id_empleado, nombre, apellido, modalidad, activo) VALUES
    (1, 'Ana',  'Gómez', 'turnos',        1),
    (2, 'Luis', 'Pérez', 'orden_llegada', 1);

-- Categorías.
INSERT INTO categorias (id_categoria, nombre, activo) VALUES
    (1, 'Cortes',       1),
    (2, 'Coloración',   1),
    (3, 'Tratamientos', 1),
    (4, 'Productos',    1);

-- Items: servicios (duracion_min, sin stock) y productos (con stock y costo).
INSERT INTO items (id_item, nombre, tipo, precio, duracion_min, stock, stock_minimo, costo, id_categoria, activo) VALUES
    (1, 'Corte de cabello',    'servicio',  5000.00, 30, NULL, NULL,   NULL,    1, 1),
    (2, 'Coloración completa', 'servicio', 12000.00, 90, NULL, NULL,   NULL,    2, 1),
    (3, 'Brushing',            'servicio',  4500.00, 30, NULL, NULL,   NULL,    1, 1),
    (4, 'Shampoo anticaspa',   'producto',  3500.00, NULL, 20,  5, 2000.00, 4, 1),
    (5, 'Acondicionador',      'producto',  3800.00, NULL, 15,  5, 2200.00, 4, 1),
    (6, 'Máscara capilar',     'producto',  5200.00, NULL,  8,  3, 3000.00, 3, 1);

-- Proveedores de mercadería.
INSERT INTO proveedores (id_proveedor, nombre, cuit, telefono, email, direccion, activo) VALUES
    (1, 'Distribuidora Bella',  '30-71234567-8', '1145559876', 'ventas@bella.com',            'Av. Rivadavia 1234', 1),
    (2, 'Cosmética del Sur',    '30-70987654-1', '1144441122', 'pedidos@cosmeticadelsur.com', 'San Juan 567',       1);

-- Bancos y tarjetas de crédito.
INSERT INTO bancos (id_banco, nombre, activo) VALUES
    (1, 'Banco Nación',   1),
    (2, 'Banco Provincia', 1);

INSERT INTO tarjetas_credito (id_tarjeta_credito, id_banco, tipo_tarjeta, porcentaje_recargo, cuotas_max, activo) VALUES
    (1, 1, 'Visa',        3.50, 12, 1),
    (2, 1, 'Mastercard',  5.00, 18, 1),
    (3, 2, 'Visa',        2.00,  6, 1);

-- =====================================================================
--  Fin del script.
-- =====================================================================
