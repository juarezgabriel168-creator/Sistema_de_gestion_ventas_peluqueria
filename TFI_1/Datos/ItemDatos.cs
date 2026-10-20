using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

public class ItemDatos
{
	public List<Item> Listar(string? filtro = null, int? categoriaId = null, TipoItem? tipo = null)
	{
		List<Item> list = new List<Item>();
		using (MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion())
		{
			using MySqlCommand mySqlCommand = new MySqlCommand("\n                SELECT i.id_item, i.nombre, i.tipo, i.precio, i.duracion_min,\n                       i.stock, i.stock_minimo, i.costo, i.activo,\n                       c.id_categoria, c.nombre AS categoria\n                FROM items AS i\n                INNER JOIN categorias AS c ON c.id_categoria = i.id_categoria\n                WHERE (@filtro = '' OR i.nombre LIKE CONCAT('%', @filtro, '%'))\n                  AND (@categoriaId IS NULL OR i.id_categoria = @categoriaId)\n                  AND (@tipo IS NULL OR i.tipo = @tipo)\n                ORDER BY i.nombre;", mySqlConnection);
			mySqlCommand.Parameters.AddWithValue("@filtro", filtro ?? string.Empty);
			mySqlCommand.Parameters.AddWithValue("@categoriaId", ((object)categoriaId) ?? DBNull.Value);
			mySqlCommand.Parameters.AddWithValue("@tipo", (!tipo.HasValue) ? ((IConvertible)DBNull.Value) : ((IConvertible)ATexto(tipo.Value)));
			mySqlConnection.Open();
			using MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
			while (mySqlDataReader.Read())
			{
				list.Add(Leer(mySqlDataReader));
			}
		}
		return list;
	}

	public Item? ObtenerPorId(int id)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("\n                SELECT i.id_item, i.nombre, i.tipo, i.precio, i.duracion_min,\n                       i.stock, i.stock_minimo, i.costo, i.activo,\n                       c.id_categoria, c.nombre AS categoria\n                FROM items AS i\n                INNER JOIN categorias AS c ON c.id_categoria = i.id_categoria\n                WHERE i.id_item = @id;", mySqlConnection);
		mySqlCommand.Parameters.AddWithValue("@id", id);
		mySqlConnection.Open();
		using MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
		if (mySqlDataReader.Read())
		{
			return Leer(mySqlDataReader);
		}
		return null;
	}

	public bool ExisteNombre(string nombre, int? exceptoId = null)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("\n                SELECT COUNT(*)\n                FROM items\n                WHERE nombre = @nombre\n                  AND (@exceptoId IS NULL OR id_item <> @exceptoId);", mySqlConnection);
		mySqlCommand.Parameters.AddWithValue("@nombre", nombre);
		mySqlCommand.Parameters.AddWithValue("@exceptoId", ((object)exceptoId) ?? DBNull.Value);
		mySqlConnection.Open();
		return Convert.ToInt32(mySqlCommand.ExecuteScalar()) > 0;
	}

	public int Insertar(Item item)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("\n                INSERT INTO items (nombre, tipo, precio, duracion_min, stock, stock_minimo, costo, id_categoria, activo)\n                VALUES (@nombre, @tipo, @precio, @duracion, @stock, @stockMinimo, @costo, @categoria, @activo);\n                SELECT LAST_INSERT_ID();", mySqlConnection);
		AgregarParametros(mySqlCommand, item);
		mySqlConnection.Open();
		return Convert.ToInt32(mySqlCommand.ExecuteScalar());
	}

	public void Actualizar(Item item)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("\n                UPDATE items\n                SET nombre = @nombre,\n                    tipo = @tipo,\n                    precio = @precio,\n                    costo = @costo,\n                    duracion_min = @duracion,\n                    stock = @stock,\n                    stock_minimo = @stockMinimo,\n                    id_categoria = @categoria,\n                    activo = @activo\n                WHERE id_item = @id;", mySqlConnection);
		AgregarParametros(mySqlCommand, item);
		mySqlCommand.Parameters.AddWithValue("@id", item.Id);
		mySqlConnection.Open();
		mySqlCommand.ExecuteNonQuery();
	}

	public void Desactivar(int id)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("UPDATE items SET activo = 0 WHERE id_item = @id;", mySqlConnection);
		mySqlCommand.Parameters.AddWithValue("@id", id);
		mySqlConnection.Open();
		mySqlCommand.ExecuteNonQuery();
	}

	public void Reactivar(int id)
	{
		using MySqlConnection mySqlConnection = ConexionBD.ObtenerConexion();
		using MySqlCommand mySqlCommand = new MySqlCommand("UPDATE items SET activo = 1 WHERE id_item = @id;", mySqlConnection);
		mySqlCommand.Parameters.AddWithValue("@id", id);
		mySqlConnection.Open();
		mySqlCommand.ExecuteNonQuery();
	}

	private static void AgregarParametros(MySqlCommand cmd, Item item)
	{
		cmd.Parameters.AddWithValue("@nombre", item.Nombre);
		cmd.Parameters.AddWithValue("@tipo", ATexto(item.Tipo));
		cmd.Parameters.AddWithValue("@precio", item.Precio);
		cmd.Parameters.AddWithValue("@costo", ((object?)item.Costo) ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@categoria", item.Categoria.Id);
		cmd.Parameters.AddWithValue("@activo", item.Activo);
		if (item.Tipo == TipoItem.Producto)
		{
			cmd.Parameters.AddWithValue("@duracion", DBNull.Value);
			cmd.Parameters.AddWithValue("@stock", ((object)item.Stock) ?? DBNull.Value);
			cmd.Parameters.AddWithValue("@stockMinimo", ((object)item.StockMinimo) ?? DBNull.Value);
		}
		else
		{
			cmd.Parameters.AddWithValue("@duracion", ((object)item.DuracionMin) ?? DBNull.Value);
			cmd.Parameters.AddWithValue("@stock", DBNull.Value);
			cmd.Parameters.AddWithValue("@stockMinimo", DBNull.Value);
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
