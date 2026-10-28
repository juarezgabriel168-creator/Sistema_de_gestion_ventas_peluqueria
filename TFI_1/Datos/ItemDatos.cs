using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

public class ItemDatos
{
	public List<Item> Listar(string? filtro = null, int? categoriaId = null, TipoItem? tipo = null,
		bool soloStockBajo = false)
	{
		List<Item> lista = new List<Item>();
		using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
		{
			using MySqlCommand cmd = new MySqlCommand("\n                SELECT i.id_item, i.nombre, i.tipo, i.precio, i.duracion_min,\n                       i.stock, i.stock_minimo, i.costo, i.activo,\n                       c.id_categoria, c.nombre AS categoria\n                FROM items AS i\n                INNER JOIN categorias AS c ON c.id_categoria = i.id_categoria\n                WHERE (@filtro = '' OR i.nombre LIKE CONCAT('%', @filtro, '%'))\n                  AND (@categoriaId IS NULL OR i.id_categoria = @categoriaId)\n                  AND (@tipo IS NULL OR i.tipo = @tipo)\n                  AND (@soloStockBajo = 0 OR (i.tipo = 'producto' AND i.stock <= i.stock_minimo))\n                ORDER BY i.nombre;", conexion);
			cmd.Parameters.Add(Parametros.Texto("@filtro", filtro ?? string.Empty));
			cmd.Parameters.Add(Parametros.EnteroNulo("@categoriaId", categoriaId));
			cmd.Parameters.Add(Parametros.Texto("@tipo", tipo.HasValue ? ATexto(tipo.Value) : null));
			cmd.Parameters.Add(Parametros.Entero("@soloStockBajo", soloStockBajo ? 1 : 0));
			conexion.Open();
			using MySqlDataReader rd = cmd.ExecuteReader();
			while (rd.Read())
			{
				lista.Add(Leer(rd));
			}
		}
		return lista;
	}

	public Item? ObtenerPorId(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("\n                SELECT i.id_item, i.nombre, i.tipo, i.precio, i.duracion_min,\n                       i.stock, i.stock_minimo, i.costo, i.activo,\n                       c.id_categoria, c.nombre AS categoria\n                FROM items AS i\n                INNER JOIN categorias AS c ON c.id_categoria = i.id_categoria\n                WHERE i.id_item = @id;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		using MySqlDataReader rd = cmd.ExecuteReader();
		if (rd.Read())
		{
			return Leer(rd);
		}
		return null;
	}

	public bool ExisteNombre(string nombre, int? exceptoId = null)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("\n                SELECT COUNT(*)\n                FROM items\n                WHERE nombre = @nombre\n                  AND (@exceptoId IS NULL OR id_item <> @exceptoId);", conexion);
		cmd.Parameters.Add(Parametros.Texto("@nombre", nombre));
		cmd.Parameters.Add(Parametros.EnteroNulo("@exceptoId", exceptoId));
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
	}

	public int Insertar(Item item)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("\n                INSERT INTO items (nombre, tipo, precio, duracion_min, stock, stock_minimo, costo, id_categoria, activo)\n                VALUES (@nombre, @tipo, @precio, @duracion, @stock, @stockMinimo, @costo, @categoria, @activo);\n                SELECT LAST_INSERT_ID();", conexion);
		AgregarParametros(cmd, item);
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar());
	}

	public void Actualizar(Item item)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("\n                UPDATE items\n                SET nombre = @nombre,\n                    tipo = @tipo,\n                    precio = @precio,\n                    costo = @costo,\n                    duracion_min = @duracion,\n                    stock = @stock,\n                    stock_minimo = @stockMinimo,\n                    id_categoria = @categoria,\n                    activo = @activo\n                WHERE id_item = @id;", conexion);
		AgregarParametros(cmd, item);
		cmd.Parameters.Add(Parametros.Entero("@id", item.Id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public void Desactivar(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("UPDATE items SET activo = 0 WHERE id_item = @id;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public void Reactivar(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("UPDATE items SET activo = 1 WHERE id_item = @id;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	/// <summary>
	/// Indica si el ítem está referenciado por ventas, ingresos de mercadería
	/// o turnos. En ese caso no se puede borrar sin romper el historial.
	/// </summary>
	public bool TieneDependencias(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT (SELECT COUNT(*) FROM detalle_venta WHERE id_item = @id) " +
			"     + (SELECT COUNT(*) FROM ingresos_mercaderia_detalle WHERE id_item = @id) " +
			"     + (SELECT COUNT(*) FROM atenciones WHERE id_item = @id);", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
	}

	public void Eliminar(int id)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand("DELETE FROM items WHERE id_item = @id;", conexion);
		cmd.Parameters.Add(Parametros.Entero("@id", id));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	private static void AgregarParametros(MySqlCommand cmd, Item item)
	{
		cmd.Parameters.Add(Parametros.Texto("@nombre", item.Nombre));
		cmd.Parameters.Add(Parametros.Texto("@tipo", ATexto(item.Tipo)));
		cmd.Parameters.Add(Parametros.Importe("@precio", item.Precio));
		cmd.Parameters.Add(Parametros.ImporteNulo("@costo", item.Costo));
		cmd.Parameters.Add(Parametros.Entero("@categoria", item.Categoria.Id));
		cmd.Parameters.Add(Parametros.Booleano("@activo", item.Activo));
		if (item.Tipo == TipoItem.Producto)
		{
			cmd.Parameters.Add(Parametros.EnteroNulo("@duracion", null));
			cmd.Parameters.Add(Parametros.EnteroNulo("@stock", item.Stock));
			cmd.Parameters.Add(Parametros.EnteroNulo("@stockMinimo", item.StockMinimo));
		}
		else
		{
			cmd.Parameters.Add(Parametros.EnteroNulo("@duracion", item.DuracionMin));
			cmd.Parameters.Add(Parametros.EnteroNulo("@stock", null));
			cmd.Parameters.Add(Parametros.EnteroNulo("@stockMinimo", null));
		}
	}

	private static Item Leer(MySqlDataReader rd)
	{
		return new Item
		{
			Id = rd.GetInt32("id_item"),
			Nombre = rd.GetString("nombre"),
			Tipo = ATipo(rd.GetString("tipo")),
			Precio = rd.GetDecimal("precio"),
			DuracionMin = LeerIntNulo(rd, "duracion_min"),
			Stock = LeerIntNulo(rd, "stock"),
			StockMinimo = LeerIntNulo(rd, "stock_minimo"),
			Costo = LeerDecimalNulo(rd, "costo"),
			Categoria = new Categoria(rd.GetString("categoria"))
			{
				Id = rd.GetInt32("id_categoria")
			},
			Activo = rd.GetBoolean("activo")
		};
	}

	private static int? LeerIntNulo(MySqlDataReader rd, string columna)
	{
		int ordinal = rd.GetOrdinal(columna);
		return rd.IsDBNull(ordinal) ? ((int?)null) : new int?(Convert.ToInt32(rd.GetValue(ordinal)));
	}

	private static decimal? LeerDecimalNulo(MySqlDataReader rd, string columna)
	{
		int ordinal = rd.GetOrdinal(columna);
		return rd.IsDBNull(ordinal) ? ((decimal?)null) : new decimal?(Convert.ToDecimal(rd.GetValue(ordinal)));
	}

	private static string ATexto(TipoItem tipo)
	{
		return (tipo == TipoItem.Producto) ? "producto" : "servicio";
	}

	private static TipoItem ATipo(string texto)
	{
		return (texto == "producto") ? TipoItem.Producto : TipoItem.Servicio;
	}
}
