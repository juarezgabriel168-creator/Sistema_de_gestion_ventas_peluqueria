using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

public class IngresoMercaderiaDatos
{
	public List<IngresoMercaderia> Listar(DateTime? desde = null, DateTime? hasta = null, string? filtro = null,
		int? limite = null, int? desplazamiento = null)
	{
		List<IngresoMercaderia> lista = new();
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT i.id_ingreso, i.fecha, i.id_proveedor, i.proveedor, i.total, i.id_usuario, i.observacion, " +
			"       i.anulada, i.fecha_anulacion, i.id_usuario_anulacion, i.motivo_anulacion, " +
			"       u.nombre_usuario, ua.nombre_usuario AS usuario_anulacion " +
			"FROM ingresos_mercaderia AS i " +
			"INNER JOIN usuarios AS u ON u.id_usuario = i.id_usuario " +
			"LEFT JOIN usuarios AS ua ON ua.id_usuario = i.id_usuario_anulacion " +
			"WHERE (@desde IS NULL OR DATE(i.fecha) >= @desde) " +
			"  AND (@hasta IS NULL OR DATE(i.fecha) <= @hasta) " +
			"  AND (@filtro = '' OR i.proveedor LIKE CONCAT('%', @filtro, '%')) " +
			"ORDER BY i.fecha DESC, i.id_ingreso DESC " +
			"LIMIT @limite OFFSET @desplazamiento;", conexion);
		cmd.Parameters.Add(Parametros.SoloFechaNula("@desde", desde?.Date));
		cmd.Parameters.Add(Parametros.SoloFechaNula("@hasta", hasta?.Date));
		cmd.Parameters.Add(Parametros.Texto("@filtro", filtro ?? string.Empty));
		cmd.Parameters.Add(Parametros.Entero("@limite", limite ?? int.MaxValue));
		cmd.Parameters.Add(Parametros.Entero("@desplazamiento", desplazamiento ?? 0));
		conexion.Open();
		using MySqlDataReader rd = cmd.ExecuteReader();
		while (rd.Read())
			lista.Add(LeerCabecera(rd));
		return lista;
	}

	/// <summary>Cuenta los ingresos que cumplen los mismos filtros que Listar.</summary>
	public int Contar(DateTime? desde = null, DateTime? hasta = null, string? filtro = null)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT COUNT(*) FROM ingresos_mercaderia AS i " +
			"WHERE (@desde IS NULL OR DATE(i.fecha) >= @desde) " +
			"  AND (@hasta IS NULL OR DATE(i.fecha) <= @hasta) " +
			"  AND (@filtro = '' OR i.proveedor LIKE CONCAT('%', @filtro, '%'));", conexion);
		cmd.Parameters.Add(Parametros.SoloFechaNula("@desde", desde?.Date));
		cmd.Parameters.Add(Parametros.SoloFechaNula("@hasta", hasta?.Date));
		cmd.Parameters.Add(Parametros.Texto("@filtro", filtro ?? string.Empty));
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar());
	}

	/// <summary>Suma el total de los ingresos que cumplen los mismos filtros que Listar.</summary>
	public decimal Sumar(DateTime? desde = null, DateTime? hasta = null, string? filtro = null)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT COALESCE(SUM(i.total), 0) FROM ingresos_mercaderia AS i " +
			"WHERE (@desde IS NULL OR DATE(i.fecha) >= @desde) " +
			"  AND (@hasta IS NULL OR DATE(i.fecha) <= @hasta) " +
			"  AND (@filtro = '' OR i.proveedor LIKE CONCAT('%', @filtro, '%')) " +
			"  AND i.anulada = 0;", conexion);
		cmd.Parameters.Add(Parametros.SoloFechaNula("@desde", desde?.Date));
		cmd.Parameters.Add(Parametros.SoloFechaNula("@hasta", hasta?.Date));
		cmd.Parameters.Add(Parametros.Texto("@filtro", filtro ?? string.Empty));
		conexion.Open();
		return Convert.ToDecimal(cmd.ExecuteScalar());
	}

	public IngresoMercaderia? ObtenerPorId(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT i.id_ingreso, i.fecha, i.id_proveedor, i.proveedor, i.total, i.id_usuario, i.observacion, " +
			"       i.anulada, i.fecha_anulacion, i.id_usuario_anulacion, i.motivo_anulacion, " +
			"       u.nombre_usuario, ua.nombre_usuario AS usuario_anulacion " +
			"FROM ingresos_mercaderia AS i " +
			"INNER JOIN usuarios AS u ON u.id_usuario = i.id_usuario " +
			"LEFT JOIN usuarios AS ua ON ua.id_usuario = i.id_usuario_anulacion " +
			"WHERE i.id_ingreso = @id;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		using MySqlDataReader rd = cmd.ExecuteReader();
		IngresoMercaderia? ingreso = null;
		if (rd.Read())
			ingreso = LeerCabecera(rd);
		rd.Close();

		if (ingreso != null)
		{
			ingreso.Detalles.Clear();
			foreach (var det in ObtenerDetalles(id, conexion))
				ingreso.Detalles.Add(det);
		}
		return ingreso;
	}

	public int Insertar(IngresoMercaderia ingreso)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		conexion.Open();
		using MySqlTransaction trans = conexion.BeginTransaction();
		try
		{
			using (MySqlCommand cmd = new MySqlCommand(
				"INSERT INTO ingresos_mercaderia (fecha, id_proveedor, proveedor, total, id_usuario, observacion) " +
				"VALUES (@fecha, @idProveedor, @proveedor, @total, @idUsuario, @observacion); " +
				"SELECT LAST_INSERT_ID();", conexion, trans))
			{
				cmd.Parameters.Add(Parametros.Fecha("@fecha", ingreso.Fecha));
				cmd.Parameters.Add(Parametros.EnteroNulo("@idProveedor", ingreso.IdProveedor));
				cmd.Parameters.Add(Parametros.Texto("@proveedor", ingreso.Proveedor));
				cmd.Parameters.Add(Parametros.Importe("@total", ingreso.Total));
				cmd.Parameters.Add(Parametros.Entero("@idUsuario", ingreso.IdUsuario));
				cmd.Parameters.Add(Parametros.Texto("@observacion", ingreso.Observacion));
				ingreso.Id = Convert.ToInt32(cmd.ExecuteScalar());
			}

			foreach (var detalle in ingreso.Detalles)
			{
				using (MySqlCommand cmdDet = new MySqlCommand(
					"INSERT INTO ingresos_mercaderia_detalle (id_ingreso, id_item, cantidad, costo_unitario, subtotal) " +
					"VALUES (@ingreso, @item, @cantidad, @costo, @subtotal);", conexion, trans))
				{
					cmdDet.Parameters.Add(Parametros.Entero("@ingreso", ingreso.Id));
					cmdDet.Parameters.Add(Parametros.Entero("@item", detalle.Item.Id));
					cmdDet.Parameters.Add(Parametros.Entero("@cantidad", detalle.Cantidad));
					cmdDet.Parameters.Add(Parametros.Importe("@costo", detalle.CostoUnitario));
					cmdDet.Parameters.Add(Parametros.Importe("@subtotal", detalle.Subtotal));
					cmdDet.ExecuteNonQuery();
				}

				// Actualizar stock y costo del item
				using (MySqlCommand cmdItem = new MySqlCommand(
					"UPDATE items SET stock = COALESCE(stock, 0) + @cantidad, " +
					"    precio = @precio, costo = @costo " +
					"WHERE id_item = @item;", conexion, trans))
				{
					cmdItem.Parameters.Add(Parametros.Entero("@cantidad", detalle.Cantidad));
					cmdItem.Parameters.Add(Parametros.Importe("@precio", detalle.Item.Precio));
					cmdItem.Parameters.Add(Parametros.Importe("@costo", detalle.CostoUnitario));
					cmdItem.Parameters.Add(Parametros.Entero("@item", detalle.Item.Id));
					cmdItem.ExecuteNonQuery();
				}
			}

			trans.Commit();
			return ingreso.Id;
		}
		catch
		{
			trans.Rollback();
			throw;
		}
	}

	/// <summary>
	/// Anula un ingreso sin borrarlo: descuenta del stock las cantidades
	/// ingresadas y deja registrado el motivo, la fecha y el usuario.
	/// </summary>
	public void Anular(int idIngreso, int usuarioId, string motivo)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		conexion.Open();
		using MySqlTransaction transaccion = conexion.BeginTransaction();
		try
		{
			using (MySqlCommand cmd = new MySqlCommand(
				"SELECT id_ingreso FROM ingresos_mercaderia " +
				"WHERE id_ingreso = @id AND anulada = 0 FOR UPDATE;", conexion, transaccion))
			{
				cmd.Parameters.Add(Parametros.Entero("@id", idIngreso));
				using MySqlDataReader rd = cmd.ExecuteReader();
				if (!rd.Read())
					throw new InvalidOperationException("El ingreso no existe o ya fue anulado.");
			}

			// Revertir el stock de todos los productos del ingreso.
			using (MySqlCommand cmd = new MySqlCommand(
				"UPDATE items AS i " +
				"INNER JOIN ingresos_mercaderia_detalle AS d ON d.id_item = i.id_item " +
				"SET i.stock = COALESCE(i.stock, 0) - d.cantidad " +
				"WHERE d.id_ingreso = @id;", conexion, transaccion))
			{
				cmd.Parameters.Add(Parametros.Entero("@id", idIngreso));
				cmd.ExecuteNonQuery();
			}

			using (MySqlCommand cmd = new MySqlCommand(
				"UPDATE ingresos_mercaderia " +
				"SET anulada = 1, fecha_anulacion = NOW(), " +
				"    id_usuario_anulacion = @usuario, motivo_anulacion = @motivo " +
				"WHERE id_ingreso = @id AND anulada = 0;", conexion, transaccion))
			{
				cmd.Parameters.Add(Parametros.Entero("@usuario", usuarioId));
				cmd.Parameters.Add(Parametros.Texto("@motivo", motivo));
				cmd.Parameters.Add(Parametros.Entero("@id", idIngreso));
				if (cmd.ExecuteNonQuery() == 0)
					throw new InvalidOperationException("El ingreso ya fue anulado.");
			}

			transaccion.Commit();
		}
		catch
		{
			transaccion.Rollback();
			throw;
		}
	}

	private List<IngresoMercaderiaDetalle> ObtenerDetalles(int idIngreso, MySqlConnection conexion)
	{
		List<IngresoMercaderiaDetalle> detalles = new();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT d.id_detalle_ingreso, d.id_item, d.cantidad, d.costo_unitario, d.subtotal, " +
			"       i.nombre AS item_nombre " +
			"FROM ingresos_mercaderia_detalle AS d " +
			"INNER JOIN items AS i ON i.id_item = d.id_item " +
			"WHERE d.id_ingreso = @ingreso ORDER BY d.id_detalle_ingreso;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@ingreso", idIngreso));
		using MySqlDataReader rd = cmd.ExecuteReader();
		while (rd.Read())
		{
			detalles.Add(new IngresoMercaderiaDetalle
			{
				Id = rd.GetInt32("id_detalle_ingreso"),
				Item = new Item
				{
					Id = rd.GetInt32("id_item"),
					Nombre = rd.GetString("item_nombre")
				},
				Cantidad = rd.GetInt32("cantidad"),
				CostoUnitario = rd.GetDecimal("costo_unitario")
			});
		}
		return detalles;
	}

	private static IngresoMercaderia LeerCabecera(MySqlDataReader rd)
	{
		return new IngresoMercaderia
		{
			Id = rd.GetInt32("id_ingreso"),
			Fecha = rd.GetDateTime("fecha"),
			IdProveedor = rd.IsDBNull(rd.GetOrdinal("id_proveedor")) ? null : rd.GetInt32("id_proveedor"),
			Proveedor = rd.IsDBNull(rd.GetOrdinal("proveedor")) ? null : rd.GetString("proveedor"),
			Total = rd.GetDecimal("total"),
			IdUsuario = rd.GetInt32("id_usuario"),
			NombreUsuario = rd.IsDBNull(rd.GetOrdinal("nombre_usuario")) ? null : rd.GetString("nombre_usuario"),
			Observacion = rd.IsDBNull(rd.GetOrdinal("observacion")) ? null : rd.GetString("observacion"),
			Anulada = rd.GetBoolean("anulada"),
			FechaAnulacion = rd.IsDBNull(rd.GetOrdinal("fecha_anulacion")) ? null : rd.GetDateTime("fecha_anulacion"),
			IdUsuarioAnulacion = rd.IsDBNull(rd.GetOrdinal("id_usuario_anulacion"))
				? null
				: rd.GetInt32("id_usuario_anulacion"),
			NombreUsuarioAnulacion = rd.IsDBNull(rd.GetOrdinal("usuario_anulacion"))
				? null
				: rd.GetString("usuario_anulacion"),
			MotivoAnulacion = rd.IsDBNull(rd.GetOrdinal("motivo_anulacion"))
				? null
				: rd.GetString("motivo_anulacion")
		};
	}

	public DataTable IngresosResumen(DateTime desde, DateTime hasta)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT DATE(fecha) AS Fecha, COUNT(*) AS Cantidad, COALESCE(SUM(total), 0) AS Total " +
			"FROM ingresos_mercaderia WHERE DATE(fecha) BETWEEN @desde AND @hasta " +
			"  AND anulada = 0 " +
			"GROUP BY DATE(fecha) ORDER BY Fecha;", conexion);
		cmd.Parameters.Add(Parametros.SoloFecha("@desde", desde));
		cmd.Parameters.Add(Parametros.SoloFecha("@hasta", hasta));
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new();
		adaptador.Fill(tabla);
		return tabla;
	}
}
