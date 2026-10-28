using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

public class EmpleadoDatos
{
	public List<Empleado> Listar(string? filtro = null)
	{
		List<Empleado> lista = new List<Empleado>();
		using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
		{
			using MySqlCommand cmd = new MySqlCommand("\n                SELECT id_empleado, nombre, apellido, modalidad, activo\n                FROM empleados\n                WHERE (@filtro = ''\n                       OR apellido LIKE CONCAT('%', @filtro, '%')\n                       OR nombre LIKE CONCAT('%', @filtro, '%'))\n                ORDER BY apellido, nombre;", conexion);
			cmd.Parameters.Add(Parametros.Texto("@filtro", filtro ?? string.Empty));
			conexion.Open();
			using MySqlDataReader rd = cmd.ExecuteReader();
			while (rd.Read())
			{
				lista.Add(new Empleado
				{
					Id = rd.GetInt32("id_empleado"),
					Nombre = rd.GetString("nombre"),
					Apellido = rd.GetString("apellido"),
					Modalidad = AModalidad(rd.GetString("modalidad")),
					Activo = rd.GetBoolean("activo")
				});
			}
		}
		return lista;
	}

	public int Insertar(Empleado empleado)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("\n                INSERT INTO empleados (nombre, apellido, modalidad, activo)\n                VALUES (@nombre, @apellido, @modalidad, @activo);\n                SELECT LAST_INSERT_ID();", conexion);
		AgregarParametros(cmd, empleado);
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar());
	}

	public void Actualizar(Empleado empleado)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("\n                UPDATE empleados\n                SET nombre = @nombre, apellido = @apellido, modalidad = @modalidad, activo = @activo\n                WHERE id_empleado = @id;", conexion);
		AgregarParametros(cmd, empleado);
		cmd.Parameters.Add(Parametros.Entero("@id", empleado.Id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public void Desactivar(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("UPDATE empleados SET activo = 0 WHERE id_empleado = @id;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public void Reactivar(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("UPDATE empleados SET activo = 1 WHERE id_empleado = @id;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public bool TieneDependencias(int id)
	{
		const string sql = @"
                SELECT (SELECT COUNT(*) FROM ventas WHERE id_empleado = @id)
                     + (SELECT COUNT(*) FROM atenciones WHERE id_empleado = @id);";

		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(sql, conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
	}

	public void Eliminar(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("DELETE FROM empleados WHERE id_empleado = @id;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	private static void AgregarParametros(MySqlCommand cmd, Empleado empleado)
	{
		cmd.Parameters.Add(Parametros.Texto("@nombre", empleado.Nombre));
		cmd.Parameters.Add(Parametros.Texto("@apellido", empleado.Apellido));
		cmd.Parameters.Add(Parametros.Texto("@modalidad", ATexto(empleado.Modalidad)));
		cmd.Parameters.Add(Parametros.Booleano("@activo", empleado.Activo));
	}

	private static string ATexto(Modalidad modalidad)
	{
		return (modalidad == Modalidad.Turnos) ? "turnos" : "orden_llegada";
	}

	private static Modalidad AModalidad(string texto)
	{
		return (!(texto == "turnos")) ? Modalidad.OrdenLlegada : Modalidad.Turnos;
	}
}
