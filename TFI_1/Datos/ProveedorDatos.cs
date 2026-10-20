using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

public class ProveedorDatos
{
	public List<Proveedor> Listar(string? filtro = null)
	{
		List<Proveedor> lista = new();
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT id_proveedor, nombre, cuit, telefono, email, direccion, activo " +
			"FROM proveedores " +
			"WHERE (@filtro = '' OR nombre LIKE CONCAT('%', @filtro, '%') " +
			"                  OR COALESCE(cuit, '') LIKE CONCAT('%', @filtro, '%')) " +
			"ORDER BY nombre;", conexion);
		cmd.Parameters.AddWithValue("@filtro", filtro ?? string.Empty);
		conexion.Open();
		using MySqlDataReader rd = cmd.ExecuteReader();
		while (rd.Read())
			lista.Add(Leer(rd));
		return lista;
	}

	public Proveedor? ObtenerPorId(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT id_proveedor, nombre, cuit, telefono, email, direccion, activo " +
			"FROM proveedores WHERE id_proveedor = @id;", conexion);
		cmd.Parameters.AddWithValue("@id", id);
		conexion.Open();
		using MySqlDataReader rd = cmd.ExecuteReader();
		return rd.Read() ? Leer(rd) : null;
	}

	public bool ExisteNombre(string nombre, int? exceptoId = null)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT COUNT(*) FROM proveedores " +
			"WHERE nombre = @nombre " +
			"  AND (@exceptoId IS NULL OR id_proveedor <> @exceptoId);", conexion);
		cmd.Parameters.AddWithValue("@nombre", nombre);
		cmd.Parameters.AddWithValue("@exceptoId", ((object?)exceptoId) ?? DBNull.Value);
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
	}

	public int Insertar(Proveedor proveedor)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"INSERT INTO proveedores (nombre, cuit, telefono, email, direccion, activo) " +
			"VALUES (@nombre, @cuit, @telefono, @email, @direccion, @activo); " +
			"SELECT LAST_INSERT_ID();", conexion);
		AgregarParametros(cmd, proveedor);
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar());
	}

	public void Actualizar(Proveedor proveedor)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"UPDATE proveedores " +
			"SET nombre = @nombre, cuit = @cuit, telefono = @telefono, " +
			"    email = @email, direccion = @direccion, activo = @activo " +
			"WHERE id_proveedor = @id;", conexion);
		AgregarParametros(cmd, proveedor);
		cmd.Parameters.AddWithValue("@id", proveedor.Id);
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public void Desactivar(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"UPDATE proveedores SET activo = 0 WHERE id_proveedor = @id;", conexion);
		cmd.Parameters.AddWithValue("@id", id);
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public void Reactivar(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"UPDATE proveedores SET activo = 1 WHERE id_proveedor = @id;", conexion);
		cmd.Parameters.AddWithValue("@id", id);
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	private static void AgregarParametros(MySqlCommand cmd, Proveedor proveedor)
	{
		cmd.Parameters.AddWithValue("@nombre", proveedor.Nombre);
		cmd.Parameters.AddWithValue("@cuit", (object?)proveedor.Cuit ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@telefono", (object?)proveedor.Telefono ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@email", (object?)proveedor.Email ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@direccion", (object?)proveedor.Direccion ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@activo", proveedor.Activo);
	}

	private static Proveedor Leer(MySqlDataReader rd)
	{
		return new Proveedor
		{
			Id = rd.GetInt32("id_proveedor"),
			Nombre = rd.GetString("nombre"),
			Cuit = rd.IsDBNull(rd.GetOrdinal("cuit")) ? null : rd.GetString("cuit"),
			Telefono = rd.IsDBNull(rd.GetOrdinal("telefono")) ? null : rd.GetString("telefono"),
			Email = rd.IsDBNull(rd.GetOrdinal("email")) ? null : rd.GetString("email"),
			Direccion = rd.IsDBNull(rd.GetOrdinal("direccion")) ? null : rd.GetString("direccion"),
			Activo = rd.GetBoolean("activo")
		};
	}
}
