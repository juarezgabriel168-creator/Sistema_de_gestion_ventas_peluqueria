using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

public class EmpleadoDatos
{
	public List<Empleado> Listar(string? filtro = null)
	{
		List<Empleado> list = new List<Empleado>();
		using (MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion())
		{
			using MySqlCommand mySqlCommand = new MySqlCommand("\n                SELECT id_empleado, nombre, apellido, modalidad, activo\n                FROM empleados\n                WHERE (@filtro = ''\n                       OR apellido LIKE CONCAT('%', @filtro, '%')\n                       OR nombre LIKE CONCAT('%', @filtro, '%'))\n                ORDER BY apellido, nombre;", mySqlConnection);
			mySqlCommand.Parameters.AddWithValue("@filtro", filtro ?? string.Empty);
			mySqlConnection.Open();
			using MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
			while (mySqlDataReader.Read())
			{
				list.Add(new Empleado
				{
					Id = mySqlDataReader.GetInt32("id_empleado"),
					Nombre = mySqlDataReader.GetString("nombre"),
					Apellido = mySqlDataReader.GetString("apellido"),
					Modalidad = AModalidad(mySqlDataReader.GetString("modalidad")),
					Activo = mySqlDataReader.GetBoolean("activo")
				});
			}
		}
		return list;
	}

	public int Insertar(Empleado empleado)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("\n                INSERT INTO empleados (nombre, apellido, modalidad, activo)\n                VALUES (@nombre, @apellido, @modalidad, @activo);\n                SELECT LAST_INSERT_ID();", mySqlConnection);
		AgregarParametros(mySqlCommand, empleado);
		mySqlConnection.Open();
		return Convert.ToInt32(mySqlCommand.ExecuteScalar());
	}

	public void Actualizar(Empleado empleado)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("\n                UPDATE empleados\n                SET nombre = @nombre, apellido = @apellido, modalidad = @modalidad, activo = @activo\n                WHERE id_empleado = @id;", mySqlConnection);
		AgregarParametros(mySqlCommand, empleado);
		mySqlCommand.Parameters.AddWithValue("@id", empleado.Id);
		mySqlConnection.Open();
		mySqlCommand.ExecuteNonQuery();
	}

	public void Desactivar(int id)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("UPDATE empleados SET activo = 0 WHERE id_empleado = @id;", mySqlConnection);
		mySqlCommand.Parameters.AddWithValue("@id", id);
		mySqlConnection.Open();
		mySqlCommand.ExecuteNonQuery();
	}

	public void Reactivar(int id)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("UPDATE empleados SET activo = 1 WHERE id_empleado = @id;", mySqlConnection);
		mySqlCommand.Parameters.AddWithValue("@id", id);
		mySqlConnection.Open();
		mySqlCommand.ExecuteNonQuery();
	}

	private static void AgregarParametros(MySqlCommand cmd, Empleado empleado)
	{
		cmd.Parameters.AddWithValue("@nombre", empleado.Nombre);
		cmd.Parameters.AddWithValue("@apellido", empleado.Apellido);
		cmd.Parameters.AddWithValue("@modalidad", ATexto(empleado.Modalidad));
		cmd.Parameters.AddWithValue("@activo", empleado.Activo);
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
