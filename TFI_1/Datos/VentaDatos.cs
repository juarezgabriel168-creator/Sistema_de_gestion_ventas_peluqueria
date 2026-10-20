using System;
using System.Data;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

public class VentaDatos
{
	// Registra una venta completa dentro de una transacción: cabecera, detalle y descuento de stock.
	public int Registrar(Venta venta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		conexion.Open();
		using MySqlTransaction transaccion = conexion.BeginTransaction();
		try
		{
			using (MySqlCommand cmd = new MySqlCommand(
				"INSERT INTO ventas (id_cliente, id_usuario, id_empleado, fecha, medio_pago, " +
				"                        id_tarjeta_credito, cantidad_cuotas, descuento, recargo, total) " +
				"VALUES (@cliente, @usuario, @empleado, @fecha, @medioPago, " +
				"        @idTarjeta, @cantidadCuotas, @descuento, @recargo, @total); " +
				"SELECT LAST_INSERT_ID();", conexion, transaccion))
			{
				cmd.Parameters.AddWithValue("@cliente", venta.Cliente.Id);
				cmd.Parameters.AddWithValue("@usuario", venta.Usuario.Id);
				cmd.Parameters.AddWithValue("@empleado", (object)venta.IdEmpleado ?? DBNull.Value);
				cmd.Parameters.AddWithValue("@fecha", venta.Fecha);
				cmd.Parameters.AddWithValue("@medioPago", venta.MedioPago);
				cmd.Parameters.AddWithValue("@idTarjeta", (object)venta.IdTarjetaCredito ?? DBNull.Value);
				cmd.Parameters.AddWithValue("@cantidadCuotas", (object)venta.CantidadCuotas ?? DBNull.Value);
				cmd.Parameters.AddWithValue("@descuento", venta.Descuento);
				cmd.Parameters.AddWithValue("@recargo", venta.Recargo);
				cmd.Parameters.AddWithValue("@total", venta.Total);
				venta.Id = Convert.ToInt32(cmd.ExecuteScalar());
			}

			foreach (DetalleVenta detalle in venta.Detalles)
			{
				using (MySqlCommand cmdDetalle = new MySqlCommand(
					"INSERT INTO detalle_venta (id_venta, id_item, cantidad, precio, subtotal) " +
					"VALUES (@venta, @item, @cantidad, @precio, @subtotal);", conexion, transaccion))
				{
					cmdDetalle.Parameters.AddWithValue("@venta", venta.Id);
					cmdDetalle.Parameters.AddWithValue("@item", detalle.Item.Id);
					cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
					cmdDetalle.Parameters.AddWithValue("@precio", detalle.Precio);
					cmdDetalle.Parameters.AddWithValue("@subtotal", detalle.Subtotal);
					cmdDetalle.ExecuteNonQuery();
				}

				// El stock solo se descuenta en productos. La condición "stock >= cantidad" evita
				// dejar stock negativo si otro proceso vendió el mismo producto mientras tanto.
				if (detalle.Item.EsProducto)
				{
					using MySqlCommand cmdStock = new MySqlCommand(
						"UPDATE items SET stock = stock - @cantidad " +
						"WHERE id_item = @item AND stock >= @cantidad;", conexion, transaccion);
					cmdStock.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
					cmdStock.Parameters.AddWithValue("@item", detalle.Item.Id);
					if (cmdStock.ExecuteNonQuery() == 0)
					{
						throw new InvalidOperationException($"Stock insuficiente de {detalle.Item.Nombre}.");
					}
				}
			}
            if (venta.MedioPago == "cuenta_corriente")
            {
                // Busca la cuenta del cliente y la bloquea hasta el final de la transacción.
                int idCuenta;
                using (MySqlCommand cmdCuenta = new MySqlCommand(
                    "SELECT id_cuenta_corriente FROM cuentas_corrientes WHERE id_cliente = @cliente FOR UPDATE;",
                    conexion, transaccion))
                {
                    cmdCuenta.Parameters.AddWithValue("@cliente", venta.Cliente.Id);
                    object? resultado = cmdCuenta.ExecuteScalar();
                    if (resultado == null || resultado == DBNull.Value)
                    {
                        using MySqlCommand cmdNueva = new MySqlCommand(
                            "INSERT INTO cuentas_corrientes (id_cliente, saldo) VALUES (@cliente, 0); " +
                            "SELECT LAST_INSERT_ID();", conexion, transaccion);
                        cmdNueva.Parameters.AddWithValue("@cliente", venta.Cliente.Id);
                        idCuenta = Convert.ToInt32(cmdNueva.ExecuteScalar());
                    }
                    else
                    {
                        idCuenta = Convert.ToInt32(resultado);
                    }
                }

                using (MySqlCommand cmdMov = new MySqlCommand(
                    "INSERT INTO movimientos_cuenta_corriente (id_cuenta, fecha, tipo, monto, concepto, id_venta, id_usuario) " +
                    "VALUES (@cuenta, @fecha, 'cargo', @monto, @concepto, @venta, @usuario);", conexion, transaccion))
                {
                    cmdMov.Parameters.AddWithValue("@cuenta", idCuenta);
                    cmdMov.Parameters.AddWithValue("@fecha", venta.Fecha);
                    cmdMov.Parameters.AddWithValue("@monto", venta.Total);
                    cmdMov.Parameters.AddWithValue("@concepto", $"Venta #{venta.Id}");
                    cmdMov.Parameters.AddWithValue("@venta", venta.Id);
                    cmdMov.Parameters.AddWithValue("@usuario", venta.Usuario.Id);
                    cmdMov.ExecuteNonQuery();
                }

                using (MySqlCommand cmdSaldo = new MySqlCommand(
                    "UPDATE cuentas_corrientes SET saldo = saldo + @monto, fecha_actualizacion = NOW() " +
                    "WHERE id_cuenta_corriente = @cuenta;", conexion, transaccion))
                {
                    cmdSaldo.Parameters.AddWithValue("@monto", venta.Total);
                    cmdSaldo.Parameters.AddWithValue("@cuenta", idCuenta);
                    cmdSaldo.ExecuteNonQuery();
                }
            }
            transaccion.Commit();
			return venta.Id;
		}
		catch
		{
			transaccion.Rollback();
			throw;
		}
	}

    public DataTable Listar(DateTime desde, DateTime hasta, int? usuarioId = null, int? empleadoId = null, int? ventaId = null)
    {
        using MySqlConnection conexion = ConexionBD.ObtenerConexion();
        using MySqlCommand cmd = new MySqlCommand(
            "SELECT v.id_venta AS Venta, v.fecha AS Fecha, " +
            "       CONCAT(c.apellido, ', ', c.nombre) AS Cliente, " +
            "       u.nombre_usuario AS Usuario, " +
            "       CONCAT(e.apellido, ', ', e.nombre) AS Empleado, " +
            "       v.medio_pago AS Pago, " +
            "       CONCAT(b.nombre, ' - ', t.tipo_tarjeta) AS Tarjeta, " +
			"       v.cantidad_cuotas AS Cuotas, " +
			"       (SELECT COALESCE(SUM(d.subtotal), 0) FROM detalle_venta AS d WHERE d.id_venta = v.id_venta) AS Subtotal, " +
			"       v.descuento AS Descuento, " +
            "       v.recargo AS Recargo, v.total AS Total, v.anulada AS Anulada " +
            "FROM ventas AS v " +
            "INNER JOIN clientes AS c ON c.id_cliente = v.id_cliente " +
            "INNER JOIN usuarios AS u ON u.id_usuario = v.id_usuario " +
            "LEFT JOIN empleados AS e ON e.id_empleado = v.id_empleado " +
            "LEFT JOIN tarjetas_credito AS t ON t.id_tarjeta_credito = v.id_tarjeta_credito " +
            "LEFT JOIN bancos AS b ON b.id_banco = t.id_banco " +
            "WHERE DATE(v.fecha) BETWEEN @desde AND @hasta " +
            "  AND (@usuarioId IS NULL OR v.id_usuario = @usuarioId) " +
            "  AND (@empleadoId IS NULL OR v.id_empleado = @empleadoId) " +
            "  AND (@ventaId IS NULL OR v.id_venta = @ventaId) " +
            "ORDER BY v.fecha DESC, v.id_venta DESC;", conexion);
        cmd.Parameters.AddWithValue("@desde", desde.Date);
        cmd.Parameters.AddWithValue("@hasta", hasta.Date);
        cmd.Parameters.AddWithValue("@usuarioId", ((object?)usuarioId) ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@empleadoId", ((object?)empleadoId) ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@ventaId", ((object?)ventaId) ?? DBNull.Value);
        using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
        DataTable tabla = new DataTable();
        adaptador.Fill(tabla);
        return tabla;
    }

    public DataTable ObtenerCabecera(int ventaId)
	{
        using MySqlConnection conexion = ConexionBD.ObtenerConexion();
        using MySqlCommand cmd = new MySqlCommand(
            "SELECT v.id_venta AS Venta, v.fecha AS Fecha, " +
            "       CONCAT(c.apellido, ', ', c.nombre) AS Cliente, " +
            "       u.nombre_usuario AS Usuario, " +
            "       CONCAT(e.apellido, ', ', e.nombre) AS Empleado, " +
            "       v.medio_pago AS Pago, v.id_tarjeta_credito AS IdTarjeta, " +
            "       b.nombre AS Banco, t.tipo_tarjeta AS TipoTarjeta, " +
			"       v.cantidad_cuotas AS Cuotas, " +
			"       (SELECT COALESCE(SUM(d.subtotal), 0) FROM detalle_venta AS d WHERE d.id_venta = v.id_venta) AS Subtotal, " +
			"       v.descuento AS Descuento, " +
            "       v.anulada AS Anulada, v.fecha_anulacion AS FechaAnulacion, " +
            "       v.motivo_anulacion AS MotivoAnulacion, " +
            "       ua.nombre_usuario AS UsuarioAnulacion, " +
            "       v.recargo AS Recargo, v.total AS Total " +
            "FROM ventas AS v " +
            "INNER JOIN clientes AS c ON c.id_cliente = v.id_cliente " +
            "INNER JOIN usuarios AS u ON u.id_usuario = v.id_usuario " +
            "LEFT JOIN empleados AS e ON e.id_empleado = v.id_empleado " +
            "LEFT JOIN tarjetas_credito AS t ON t.id_tarjeta_credito = v.id_tarjeta_credito " +
            "LEFT JOIN bancos AS b ON b.id_banco = t.id_banco " +
            "LEFT JOIN usuarios AS ua ON ua.id_usuario = v.id_usuario_anulacion " +
            "WHERE v.id_venta = @id;", conexion);
        cmd.Parameters.AddWithValue("@id", ventaId);
        using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
        DataTable tabla = new DataTable();
        adaptador.Fill(tabla);
        return tabla;
    }

	public DataTable ObtenerDetalle(int ventaId)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT i.nombre AS Item, i.tipo AS Tipo, d.cantidad AS Cantidad, " +
			"       d.precio AS Precio, d.subtotal AS Subtotal " +
			"FROM detalle_venta AS d " +
			"INNER JOIN items AS i ON i.id_item = d.id_item " +
			"WHERE d.id_venta = @id " +
			"ORDER BY d.id_detalle;", conexion);
		cmd.Parameters.AddWithValue("@id", ventaId);
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new DataTable();
		adaptador.Fill(tabla);
		return tabla;
	}

	// Anula una venta dentro de una transacción: devuelve el stock de los
	// productos, revierte el cargo de la cuenta corriente (si corresponde) y
	// deja la venta marcada como anulada. No se borra ningún registro.
	public void Anular(int ventaId, int usuarioId, string motivo)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		conexion.Open();
		using MySqlTransaction transaccion = conexion.BeginTransaction();
		try
		{
			string medioPago;
			decimal total;
			using (MySqlCommand cmd = new MySqlCommand(
				"SELECT medio_pago, total FROM ventas " +
				"WHERE id_venta = @id AND anulada = 0 FOR UPDATE;", conexion, transaccion))
			{
				cmd.Parameters.AddWithValue("@id", ventaId);
				using MySqlDataReader rd = cmd.ExecuteReader();
				if (!rd.Read())
				{
					throw new InvalidOperationException("La venta no existe o ya fue anulada.");
				}
				medioPago = rd.GetString("medio_pago");
				total = rd.GetDecimal("total");
			}

			// Devuelve al stock solamente los productos (los servicios no mueven stock).
			using (MySqlCommand cmd = new MySqlCommand(
				"UPDATE items AS i " +
				"INNER JOIN detalle_venta AS d ON d.id_item = i.id_item " +
				"SET i.stock = i.stock + d.cantidad " +
				"WHERE d.id_venta = @id AND i.tipo = 'producto';", conexion, transaccion))
			{
				cmd.Parameters.AddWithValue("@id", ventaId);
				cmd.ExecuteNonQuery();
			}

			// Si se pagó a cuenta corriente, se registra un abono por el total
			// y se descuenta de la deuda (queda el movimiento para el historial).
			if (medioPago == "cuenta_corriente")
			{
				int idCuenta;
				using (MySqlCommand cmd = new MySqlCommand(
					"SELECT c.id_cuenta_corriente FROM cuentas_corrientes AS c " +
					"INNER JOIN ventas AS v ON v.id_cliente = c.id_cliente " +
					"WHERE v.id_venta = @id FOR UPDATE;", conexion, transaccion))
				{
					cmd.Parameters.AddWithValue("@id", ventaId);
					object? resultado = cmd.ExecuteScalar();
					if (resultado == null || resultado == DBNull.Value)
					{
						throw new InvalidOperationException("No se encontró la cuenta corriente del cliente.");
					}
					idCuenta = Convert.ToInt32(resultado);
				}

				using (MySqlCommand cmd = new MySqlCommand(
					"INSERT INTO movimientos_cuenta_corriente " +
					"    (id_cuenta, fecha, tipo, monto, concepto, id_venta, id_usuario) " +
					"VALUES (@cuenta, NOW(), 'abono', @monto, @concepto, @venta, @usuario);",
					conexion, transaccion))
				{
					cmd.Parameters.AddWithValue("@cuenta", idCuenta);
					cmd.Parameters.AddWithValue("@monto", total);
					cmd.Parameters.AddWithValue("@concepto", $"Anulación venta #{ventaId}: {motivo}");
					cmd.Parameters.AddWithValue("@venta", ventaId);
					cmd.Parameters.AddWithValue("@usuario", usuarioId);
					cmd.ExecuteNonQuery();
				}

				using (MySqlCommand cmd = new MySqlCommand(
					"UPDATE cuentas_corrientes SET saldo = saldo - @monto, fecha_actualizacion = NOW() " +
					"WHERE id_cuenta_corriente = @cuenta;", conexion, transaccion))
				{
					cmd.Parameters.AddWithValue("@monto", total);
					cmd.Parameters.AddWithValue("@cuenta", idCuenta);
					cmd.ExecuteNonQuery();
				}
			}

			using (MySqlCommand cmd = new MySqlCommand(
				"UPDATE ventas SET anulada = 1, fecha_anulacion = NOW(), " +
				"       id_usuario_anulacion = @usuario, motivo_anulacion = @motivo " +
				"WHERE id_venta = @id AND anulada = 0;", conexion, transaccion))
			{
				cmd.Parameters.AddWithValue("@usuario", usuarioId);
				cmd.Parameters.AddWithValue("@motivo", motivo);
				cmd.Parameters.AddWithValue("@id", ventaId);
				if (cmd.ExecuteNonQuery() == 0)
				{
					throw new InvalidOperationException("La venta ya fue anulada.");
				}
			}

			transaccion.Commit();
		}
		catch
		{
			transaccion.Rollback();
			throw;
		}
	}
}
