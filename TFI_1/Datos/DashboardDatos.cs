using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace TFI_1.Datos;

public class DashboardDatos
{
	public decimal TotalVendidoHoy()
	{
		return Convert.ToDecimal(EjecutarEscalar("SELECT COALESCE(SUM(total), 0) FROM ventas WHERE DATE(fecha) = CURDATE() AND anulada = 0;"));
	}

	public int CantidadVentasHoy()
	{
		return Convert.ToInt32(EjecutarEscalar("SELECT COUNT(*) FROM ventas WHERE DATE(fecha) = CURDATE() AND anulada = 0;"));
	}

	public int TotalClientes()
	{
		return Convert.ToInt32(EjecutarEscalar("SELECT COUNT(*) FROM clientes WHERE activo = 1;"));
	}

	public int TotalItems()
	{
		return Convert.ToInt32(EjecutarEscalar("SELECT COUNT(*) FROM items WHERE activo = 1;"));
	}

	public int ProductosStockBajo()
	{
		return Convert.ToInt32(EjecutarEscalar("SELECT COUNT(*) FROM items WHERE activo = 1 AND tipo = 'producto' AND stock <= stock_minimo;"));
	}

	public DataTable ProductoMayorStock()
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand(
			"SELECT nombre AS Producto, stock AS Stock FROM items " +
			"WHERE activo = 1 AND tipo = 'producto' " +
			"ORDER BY stock DESC LIMIT 1;", mySqlConnection);
		using MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
		DataTable dataTable = new DataTable();
		mySqlDataAdapter.Fill(dataTable);
		return dataTable;
	}

	public DataTable ProductoMenorStock()
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand(
			"SELECT nombre AS Producto, stock AS Stock FROM items " +
			"WHERE activo = 1 AND tipo = 'producto' AND stock > 0 " +
			"ORDER BY stock ASC LIMIT 1;", mySqlConnection);
		using MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
		DataTable dataTable = new DataTable();
		mySqlDataAdapter.Fill(dataTable);
		return dataTable;
	}

	public DataTable UltimasVentas(int cantidad)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("\n                SELECT v.id_venta            AS Venta,\n                       v.fecha               AS Fecha,\n                       CONCAT(c.apellido, ', ', c.nombre) AS Cliente,\n                       CONCAT(e.apellido, ', ', e.nombre) AS Empleado,\n                       u.nombre_usuario      AS Usuario,\n                       v.medio_pago          AS Pago,\n                       v.total               AS Total\n                FROM ventas AS v\n                INNER JOIN clientes AS c ON c.id_cliente = v.id_cliente\n                INNER JOIN usuarios AS u ON u.id_usuario = v.id_usuario\n                LEFT JOIN empleados AS e ON e.id_empleado = v.id_empleado\n                WHERE v.anulada = 0\n                ORDER BY v.fecha DESC, v.id_venta DESC\n                LIMIT @cantidad;", mySqlConnection);
		using MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
		mySqlCommand.Parameters.AddWithValue("@cantidad", cantidad);
		mySqlConnection.Open();
		DataTable dataTable = new DataTable();
		mySqlDataAdapter.Fill(dataTable);
		return dataTable;
	}

	private static object EjecutarEscalar(string sql)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
		mySqlConnection.Open();
		return mySqlCommand.ExecuteScalar();
	}
}
