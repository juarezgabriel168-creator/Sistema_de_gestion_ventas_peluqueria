-- =====================================================================
--  Usuario de base de datos para la aplicación
-- ---------------------------------------------------------------------
--  Ejecutar UNA sola vez, como root, después de crear la base con
--  peluqueria.sql.
--
--  Crea el usuario "peluqueria_app" con permisos mínimos: solo puede
--  leer y escribir datos (SELECT, INSERT, UPDATE, DELETE) en la base
--  "peluqueria". No puede crear, borrar ni modificar tablas.
--
--  IMPORTANTE: cambiar la contraseña de ejemplo por una propia y usar
--  la misma en TFI_1/App.config.
-- =====================================================================

CREATE USER IF NOT EXISTS 'peluqueria_app'@'localhost'
    IDENTIFIED BY 'CAMBIAR_ESTA_CONTRASENA';

GRANT SELECT, INSERT, UPDATE, DELETE ON peluqueria.* TO 'peluqueria_app'@'localhost';

FLUSH PRIVILEGES;

-- Si el usuario ya existía con otra contraseña, ejecutar también:
-- ALTER USER 'peluqueria_app'@'localhost' IDENTIFIED BY 'CAMBIAR_ESTA_CONTRASENA';

-- Verificación: debe mostrar los dos GRANT (USAGE y los permisos sobre peluqueria).
SHOW GRANTS FOR 'peluqueria_app'@'localhost';
