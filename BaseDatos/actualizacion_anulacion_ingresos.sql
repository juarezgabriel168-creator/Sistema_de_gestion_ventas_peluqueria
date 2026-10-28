-- Actualización para bases existentes: agrega el registro de anulación a
-- ingresos_mercaderia. Es seguro ejecutarlo más de una vez.
--
-- Uso:
--   mysql -u peluqueria_app -p peluqueria < actualizacion_anulacion_ingresos.sql
-- o abrir el archivo desde MySQL Workbench y ejecutarlo completo.

USE peluqueria;

SET @sql = IF((
        SELECT COUNT(*) FROM information_schema.COLUMNS
        WHERE TABLE_SCHEMA = DATABASE()
          AND TABLE_NAME = 'ingresos_mercaderia'
          AND COLUMN_NAME = 'anulada'
    ) = 0,
    'ALTER TABLE ingresos_mercaderia ADD COLUMN anulada TINYINT(1) NOT NULL DEFAULT 0',
    'DO 0');
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

SET @sql = IF((
        SELECT COUNT(*) FROM information_schema.COLUMNS
        WHERE TABLE_SCHEMA = DATABASE()
          AND TABLE_NAME = 'ingresos_mercaderia'
          AND COLUMN_NAME = 'fecha_anulacion'
    ) = 0,
    'ALTER TABLE ingresos_mercaderia ADD COLUMN fecha_anulacion DATETIME NULL',
    'DO 0');
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

SET @sql = IF((
        SELECT COUNT(*) FROM information_schema.COLUMNS
        WHERE TABLE_SCHEMA = DATABASE()
          AND TABLE_NAME = 'ingresos_mercaderia'
          AND COLUMN_NAME = 'id_usuario_anulacion'
    ) = 0,
    'ALTER TABLE ingresos_mercaderia ADD COLUMN id_usuario_anulacion INT NULL',
    'DO 0');
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

SET @sql = IF((
        SELECT COUNT(*) FROM information_schema.COLUMNS
        WHERE TABLE_SCHEMA = DATABASE()
          AND TABLE_NAME = 'ingresos_mercaderia'
          AND COLUMN_NAME = 'motivo_anulacion'
    ) = 0,
    'ALTER TABLE ingresos_mercaderia ADD COLUMN motivo_anulacion VARCHAR(255) NULL',
    'DO 0');
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

SET @sql = IF((
        SELECT COUNT(*) FROM information_schema.STATISTICS
        WHERE TABLE_SCHEMA = DATABASE()
          AND TABLE_NAME = 'ingresos_mercaderia'
          AND INDEX_NAME = 'ix_ingresos_anulada'
    ) = 0,
    'ALTER TABLE ingresos_mercaderia ADD KEY ix_ingresos_anulada (anulada)',
    'DO 0');
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

SET @sql = IF((
        SELECT COUNT(*) FROM information_schema.TABLE_CONSTRAINTS
        WHERE CONSTRAINT_SCHEMA = DATABASE()
          AND TABLE_NAME = 'ingresos_mercaderia'
          AND CONSTRAINT_NAME = 'fk_ingresos_usuario_anulacion'
    ) = 0,
    'ALTER TABLE ingresos_mercaderia ADD CONSTRAINT fk_ingresos_usuario_anulacion FOREIGN KEY (id_usuario_anulacion) REFERENCES usuarios (id_usuario) ON UPDATE CASCADE ON DELETE RESTRICT',
    'DO 0');
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;
