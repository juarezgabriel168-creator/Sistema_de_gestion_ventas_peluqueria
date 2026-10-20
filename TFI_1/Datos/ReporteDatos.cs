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
			"  AND v.anulada = 0 " +
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
            "GROUP BY v.id_empleado, e.apellido, e.nombre " +
            "ORDER BY Total DESC;", conexion);
        cmd.Parameters.Add(Parametros.SoloFecha("@desde", desde.Date));
        cmd.Parameters.Add(Parametros.SoloFecha("@hasta", hasta.Date));
        using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
        DataTable tabla = new DataTable();
        adaptador.Fill(tabla);
        return tabla;
    }
}
