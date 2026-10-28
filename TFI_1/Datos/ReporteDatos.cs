using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace TFI_1.Datos;

public class ReporteDatos
{
	// Resumen del período: total vendido, cantidad de ventas y ticket promedio.
	public DataTable Resumen(DateTime desde, DateTime hasta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT COALESCE(SUM(v.total), 0) AS Total, " +
			"       COUNT(*) AS Cantidad, " +
			"       COALESCE(AVG(v.total), 0) AS TicketPromedio " +
			"FROM ventas AS v " +
			"WHERE DATE(v.fecha) BETWEEN @desde AND @hasta " +
			"  AND v.anulada = 0;", conexion);
		cmd.Parameters.Add(Parametros.SoloFecha("@desde", desde.Date));
		cmd.Parameters.Add(Parametros.SoloFecha("@hasta", hasta.Date));
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new DataTable();
		adaptador.Fill(tabla);
		return tabla;
	}

	// Ventas agrupadas por medio de pago.
	public DataTable VentasPorMedioPago(DateTime desde, DateTime hasta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT v.medio_pago AS Pago, COUNT(*) AS Cantidad, " +
			"       COALESCE(SUM(v.total), 0) AS Total " +
			"FROM ventas AS v " +
			"WHERE DATE(v.fecha) BETWEEN @desde AND @hasta " +
			"  AND v.anulada = 0 " +
			"GROUP BY v.medio_pago " +
			"ORDER BY Total DESC;", conexion);
		cmd.Parameters.Add(Parametros.SoloFecha("@desde", desde.Date));
		cmd.Parameters.Add(Parametros.SoloFecha("@hasta", hasta.Date));
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new DataTable();
		adaptador.Fill(tabla);
		return tabla;
	}

	// Productos y servicios más vendidos en el período (top 10 por cantidad).
	public DataTable ProductosMasVendidos(DateTime desde, DateTime hasta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT i.nombre AS Item, i.tipo AS Tipo, " +
			"       SUM(d.cantidad) AS Cantidad, SUM(d.subtotal) AS Total " +
			"FROM detalle_venta AS d " +
			"INNER JOIN ventas AS v ON v.id_venta = d.id_venta " +
			"INNER JOIN items AS i ON i.id_item = d.id_item " +
			"WHERE DATE(v.fecha) BETWEEN @desde AND @hasta " +
			"  AND v.anulada = 0 " +
			"GROUP BY i.id_item " +
			"ORDER BY Cantidad DESC, Total DESC " +
			"LIMIT 10;", conexion);
		cmd.Parameters.Add(Parametros.SoloFecha("@desde", desde.Date));
		cmd.Parameters.Add(Parametros.SoloFecha("@hasta", hasta.Date));
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new DataTable();
		adaptador.Fill(tabla);
		return tabla;
	}

	// Ventas agrupadas por día (para el reporte mensual).
	public DataTable VentasPorDia(DateTime desde, DateTime hasta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT DATE(v.fecha) AS Fecha, COUNT(*) AS Cantidad, " +
			"       COALESCE(SUM(v.total), 0) AS Total " +
			"FROM ventas AS v " +
			"WHERE DATE(v.fecha) BETWEEN @desde AND @hasta " +
			"  AND v.anulada = 0 " +
			"GROUP BY DATE(v.fecha) " +
			"ORDER BY Fecha;", conexion);
		cmd.Parameters.Add(Parametros.SoloFecha("@desde", desde.Date));
		cmd.Parameters.Add(Parametros.SoloFecha("@hasta", hasta.Date));
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new DataTable();
		adaptador.Fill(tabla);
		return tabla;
	}
	// Clientes que más compras hicieron en el período (top 20 por cantidad).
	public DataTable ClientesConMasCompras(DateTime desde, DateTime hasta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT CONCAT(c.apellido, ', ', c.nombre) AS Cliente, c.dni AS DNI, " +
			"       COUNT(*) AS Compras, COALESCE(SUM(v.total), 0) AS Total " +
			"FROM ventas AS v " +
			"INNER JOIN clientes AS c ON c.id_cliente = v.id_cliente " +
			"WHERE DATE(v.fecha) BETWEEN @desde AND @hasta " +
			"  AND v.anulada = 0 " +
			"GROUP BY c.id_cliente, c.apellido, c.nombre, c.dni " +
			"ORDER BY Compras DESC, Total DESC " +
			"LIMIT 20;", conexion);
		cmd.Parameters.Add(Parametros.SoloFecha("@desde", desde.Date));
		cmd.Parameters.Add(Parametros.SoloFecha("@hasta", hasta.Date));
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new DataTable();
		adaptador.Fill(tabla);
		return tabla;
	}

	// Ventas agrupadas por usuario del sistema (quién registró la venta).
	public DataTable VentasPorUsuario(DateTime desde, DateTime hasta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT u.nombre_usuario AS Usuario, r.nombre AS Rol, " +
			"       COUNT(*) AS Cantidad, COALESCE(SUM(v.total), 0) AS Total " +
			"FROM ventas AS v " +
			"INNER JOIN usuarios AS u ON u.id_usuario = v.id_usuario " +
			"INNER JOIN roles AS r ON r.id_rol = u.id_rol " +
			"WHERE DATE(v.fecha) BETWEEN @desde AND @hasta " +
			"  AND v.anulada = 0 " +
			"GROUP BY u.id_usuario, u.nombre_usuario, r.nombre " +
			"ORDER BY Total DESC;", conexion);
		cmd.Parameters.Add(Parametros.SoloFecha("@desde", desde.Date));
		cmd.Parameters.Add(Parametros.SoloFecha("@hasta", hasta.Date));
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new DataTable();
		adaptador.Fill(tabla);
		return tabla;
	}

	// Servicios más solicitados en el período (top 20 por cantidad).
	public DataTable ServiciosMasSolicitados(DateTime desde, DateTime hasta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT i.nombre AS Servicio, COUNT(*) AS Veces, " +
			"       SUM(d.cantidad) AS Cantidad, SUM(d.subtotal) AS Total " +
			"FROM detalle_venta AS d " +
			"INNER JOIN ventas AS v ON v.id_venta = d.id_venta " +
			"INNER JOIN items AS i ON i.id_item = d.id_item " +
			"WHERE DATE(v.fecha) BETWEEN @desde AND @hasta " +
			"  AND v.anulada = 0 " +
			"  AND i.tipo = 'servicio' " +
			"GROUP BY i.id_item, i.nombre " +
			"ORDER BY Cantidad DESC, Total DESC " +
			"LIMIT 20;", conexion);
		cmd.Parameters.Add(Parametros.SoloFecha("@desde", desde.Date));
		cmd.Parameters.Add(Parametros.SoloFecha("@hasta", hasta.Date));
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new DataTable();
		adaptador.Fill(tabla);
		return tabla;
	}

	// Ventas agrupadas por empleado (quien realizó la venta).
	public DataTable VentasPorEmpleado(DateTime desde, DateTime hasta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT COALESCE(CONCAT(e.apellido, ', ', e.nombre), 'Sin empleado') AS Empleado, " +
			"       COUNT(*) AS Cantidad, " +
			"       COALESCE(SUM(v.total), 0) AS Total " +
			"FROM ventas AS v " +
			"LEFT JOIN empleados AS e ON e.id_empleado = v.id_empleado " +
			"WHERE DATE(v.fecha) BETWEEN @desde AND @hasta " +
			"  AND v.anulada = 0 " +
			"GROUP BY v.id_empleado, e.apellido, e.nombre " +
			"ORDER BY Total DESC;", conexion);
		cmd.Parameters.Add(Parametros.SoloFecha("@desde", desde.Date));
		cmd.Parameters.Add(Parametros.SoloFecha("@hasta", hasta.Date));
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new DataTable();
		adaptador.Fill(tabla);
		return tabla;
	}

	// Ventas anuladas en el período, ordenadas por fecha de anulación.
	// Se filtra por fecha_anulacion: el registro diario/mensual muestra la
	// anulación el día en que ocurrió, aunque la venta sea de otro día.
	public DataTable Anulaciones(DateTime desde, DateTime hasta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT v.id_venta AS Venta, v.fecha AS FechaVenta, " +
			"       v.fecha_anulacion AS FechaAnulacion, " +
			"       COALESCE(CONCAT(c.apellido, ', ', c.nombre), 'Consumidor final') AS Cliente, " +
			"       v.medio_pago AS Pago, v.total AS Total, " +
			"       COALESCE(ua.nombre_usuario, 'Sin usuario') AS Usuario, " +
			"       v.motivo_anulacion AS Motivo " +
			"FROM ventas AS v " +
			"LEFT JOIN clientes AS c ON c.id_cliente = v.id_cliente " +
			"LEFT JOIN usuarios AS ua ON ua.id_usuario = v.id_usuario_anulacion " +
			"WHERE v.anulada = 1 " +
			"  AND v.fecha_anulacion IS NOT NULL " +
			"  AND DATE(v.fecha_anulacion) BETWEEN @desde AND @hasta " +
			"ORDER BY v.fecha_anulacion DESC, v.id_venta DESC;", conexion);
		cmd.Parameters.Add(Parametros.SoloFecha("@desde", desde.Date));
		cmd.Parameters.Add(Parametros.SoloFecha("@hasta", hasta.Date));
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new DataTable();
		adaptador.Fill(tabla);
		return tabla;
	}
}
