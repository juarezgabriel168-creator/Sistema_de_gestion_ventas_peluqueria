using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

public class AtencionDatos
{
	public List<Atencion> Listar(string? filtro = null, string? estado = null)
	{
		List<Atencion> lista = new List<Atencion>();
		using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
		{
			using MySqlCommand cmd = new MySqlCommand(
				"SELECT a.id_atencion, a.tipo AS atencion_tipo, a.fecha_hora, a.estado, " +
				"       c.id_cliente, c.nombre AS cliente_nombre, c.apellido AS cliente_apellido, " +
				"       e.id_empleado, e.nombre AS empleado_nombre, e.apellido AS empleado_apellido, e.modalidad, " +
				"       i.id_item, i.nombre AS item_nombre " +
				"FROM atenciones AS a " +
				"INNER JOIN clientes AS c ON c.id_cliente = a.id_cliente " +
				"INNER JOIN empleados AS e ON e.id_empleado = a.id_empleado " +
				"INNER JOIN items AS i ON i.id_item = a.id_item " +
				"WHERE (@filtro = '' " +
				"       OR c.nombre LIKE CONCAT('%', @filtro, '%') " +
				"       OR c.apellido LIKE CONCAT('%', @filtro, '%') " +
				"       OR i.nombre LIKE CONCAT('%', @filtro, '%')) " +
				"  AND (@estado IS NULL OR a.estado = @estado) " +
				"ORDER BY a.fecha_hora DESC;", conexion);
			cmd.Parameters.AddWithValue("@filtro", filtro ?? string.Empty);
			cmd.Parameters.AddWithValue("@estado", ((object)estado) ?? DBNull.Value);
			conexion.Open();
			using MySqlDataReader rd = cmd.ExecuteReader();
			while (rd.Read())
			{
				lista.Add(Leer(rd));
			}
		}
		return lista;
	}

	public int Insertar(Atencion atencion)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"INSERT INTO atenciones (id_cliente, id_empleado, id_item, tipo, fecha_hora, estado) " +
			"VALUES (@cliente, @empleado, @item, @tipo, @fecha, @estado); " +
			"SELECT LAST_INSERT_ID();", conexion);
		AgregarParametros(cmd, atencion);
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar());
	}

	public void Actualizar(Atencion atencion)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"UPDATE atenciones " +
			"SET id_cliente = @cliente, id_empleado = @empleado, id_item = @item, " +
			"    tipo = @tipo, fecha_hora = @fecha, estado = @estado " +
			"WHERE id_atencion = @id;", conexion);
		AgregarParametros(cmd, atencion);
		cmd.Parameters.AddWithValue("@id", atencion.Id);
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public void CambiarEstado(int id, EstadoAtencion estado)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"UPDATE atenciones SET estado = @estado WHERE id_atencion = @id;", conexion);
		cmd.Parameters.AddWithValue("@estado", ATexto(estado));
		cmd.Parameters.AddWithValue("@id", id);
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public bool ExisteConflictoHorario(int idEmpleado, DateTime fechaHora, int duracionMin, int? exceptoId = null)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT COUNT(*) FROM atenciones AS a " +
			"INNER JOIN items AS i ON i.id_item = a.id_item " +
			"WHERE a.id_empleado = @empleado AND a.estado <> 'cancelada' " +
			"  AND (@exceptoId IS NULL OR a.id_atencion <> @exceptoId) " +
			"  AND @inicio < DATE_ADD(a.fecha_hora, INTERVAL GREATEST(COALESCE(i.duracion_min, 0), 1) MINUTE) " +
			"  AND a.fecha_hora < @fin;", conexion);
		cmd.Parameters.AddWithValue("@empleado", idEmpleado);
		cmd.Parameters.AddWithValue("@inicio", fechaHora);
		// Se usa un mínimo de 1 minuto para que dos turnos a la misma hora
		// también se detecten aunque el servicio no tenga duración cargada.
		cmd.Parameters.AddWithValue("@fin", fechaHora.AddMinutes(Math.Max(duracionMin, 1)));
		cmd.Parameters.AddWithValue("@exceptoId", ((object?)exceptoId) ?? DBNull.Value);
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
	}

	public int CantidadHoy()
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT COUNT(*) FROM atenciones " +
			"WHERE DATE(fecha_hora) = CURDATE() AND estado <> 'cancelada';", conexion);
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar());
	}

	private static void AgregarParametros(MySqlCommand cmd, Atencion atencion)
	{
		cmd.Parameters.AddWithValue("@cliente", atencion.Cliente.Id);
		cmd.Parameters.AddWithValue("@empleado", atencion.Empleado.Id);
		cmd.Parameters.AddWithValue("@item", atencion.Servicio.Id);
		cmd.Parameters.AddWithValue("@tipo", ATexto(atencion.Tipo));
		cmd.Parameters.AddWithValue("@fecha", atencion.FechaHora);
		cmd.Parameters.AddWithValue("@estado", ATexto(atencion.Estado));
	}

	private static Atencion Leer(MySqlDataReader rd)
	{
		return new Atencion
		{
			Id = rd.GetInt32("id_atencion"),
			Tipo = ATipo(rd.GetString("atencion_tipo")),
			FechaHora = rd.GetDateTime("fecha_hora"),
			Estado = AEstado(rd.GetString("estado")),
			Cliente = new Cliente
			{
				Id = rd.GetInt32("id_cliente"),
				Nombre = rd.GetString("cliente_nombre"),
				Apellido = rd.GetString("cliente_apellido")
			},
			Empleado = new Empleado
			{
				Id = rd.GetInt32("id_empleado"),
				Nombre = rd.GetString("empleado_nombre"),
				Apellido = rd.GetString("empleado_apellido"),
				Modalidad = AModalidad(rd.GetString("modalidad"))
			},
			Servicio = new Item
			{
				Id = rd.GetInt32("id_item"),
				Nombre = rd.GetString("item_nombre")
			}
		};
	}

	private static string ATexto(TipoAtencion tipo)
	{
		return (tipo == TipoAtencion.Turno) ? "turno" : "llegada";
	}

	private static TipoAtencion ATipo(string texto)
	{
		return (texto == "turno") ? TipoAtencion.Turno : TipoAtencion.Llegada;
	}

	private static string ATexto(EstadoAtencion estado)
	{
		return estado switch
		{
			EstadoAtencion.Pendiente => "pendiente",
			EstadoAtencion.EnAtencion => "en_atencion",
			EstadoAtencion.Finalizada => "finalizada",
			_ => "cancelada",
		};
	}

	private static EstadoAtencion AEstado(string texto)
	{
		return texto switch
		{
			"en_atencion" => EstadoAtencion.EnAtencion,
			"finalizada" => EstadoAtencion.Finalizada,
			"cancelada" => EstadoAtencion.Cancelada,
			_ => EstadoAtencion.Pendiente,
		};
	}

	private static Modalidad AModalidad(string texto)
	{
		return (texto == "turnos") ? Modalidad.Turnos : Modalidad.OrdenLlegada;
	}
}
