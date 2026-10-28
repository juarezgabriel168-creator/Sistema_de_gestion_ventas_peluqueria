using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace TFI_1.Datos;

/// <summary>
/// Acceso a la tabla de auditoría: registra operaciones sensibles y permite
/// consultarlas por rango de fechas, usuario y tipo de acción.
/// </summary>
public class AuditoriaDatos
{
	// La base puede tener tablas creadas con distintas colaciones (por ejemplo,
	// utf8mb4_unicode_ci y utf8mb4_0900_ai_ci). Forzamos una sola al comparar
	// textos para evitar el error "Illegal mix of collations".
	private const string Collation = "utf8mb4_unicode_ci";

	// Traduce el código interno de la acción a un texto legible para el usuario.
	private const string CamposAccion =
		"CASE {0} " +
		"  WHEN 'venta' THEN 'Venta' " +
		"  WHEN 'anulacion_venta' THEN 'Anulación de venta' " +
		"  WHEN 'abono' THEN 'Pago de cuenta corriente' " +
		"  WHEN 'gasto_alta' THEN 'Alta de gasto' " +
		"  WHEN 'gasto_edicion' THEN 'Edición de gasto' " +
		"  WHEN 'ingreso_mercaderia' THEN 'Ingreso de mercadería' " +
		"  WHEN 'anulacion_ingreso_mercaderia' THEN 'Anulación de ingreso de mercadería' " +
		"  WHEN 'usuario_alta' THEN 'Alta de usuario' " +
		"  WHEN 'usuario_edicion' THEN 'Edición de usuario' " +
		"  WHEN 'usuario_desactivacion' THEN 'Desactivación de usuario' " +
		"  WHEN 'usuario_reactivacion' THEN 'Reactivación de usuario' " +
		"  WHEN 'caja_apertura' THEN 'Apertura de caja' " +
		"  WHEN 'caja_cierre' THEN 'Cierre de caja' " +
		"  WHEN 'caja_ingreso' THEN 'Ingreso de caja' " +
		"  WHEN 'caja_egreso' THEN 'Egreso de caja' " +
		"  WHEN 'backup_generado' THEN 'Copia de seguridad generada' " +
		"  ELSE {0} END";

	public void Registrar(int usuarioId, string accion, string entidad, int? idEntidad, string? detalle)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"INSERT INTO auditoria (id_usuario, accion, entidad, id_entidad, detalle) " +
			"VALUES (@usuario, @accion, @entidad, @id, @detalle);", conexion);
		cmd.Parameters.Add(Parametros.Entero("@usuario", usuarioId));
		cmd.Parameters.Add(Parametros.Texto("@accion", accion));
		cmd.Parameters.Add(Parametros.Texto("@entidad", entidad));
		cmd.Parameters.Add(Parametros.EnteroNulo("@id", idEntidad));
		cmd.Parameters.Add(Parametros.Texto("@detalle", string.IsNullOrWhiteSpace(detalle) ? null : detalle));
		conexion.Open();
		cmd.ExecuteNonQuery();
	}

	public DataTable Listar(DateTime desde, DateTime hasta, int? usuarioId, string? accion,
		int? limite = null, int? desplazamiento = null)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT a.id_auditoria AS Id, a.fecha AS Fecha, u.nombre_usuario AS Usuario, " +
			"       a.accion AS Codigo, " +
			string.Format(CamposAccion, $"a.accion COLLATE {Collation}") + " AS Accion, " +
			"       a.entidad AS Entidad, a.id_entidad AS IdEntidad, a.detalle AS Detalle " +
			"FROM auditoria AS a " +
			"INNER JOIN usuarios AS u ON u.id_usuario = a.id_usuario " +
			"WHERE DATE(a.fecha) BETWEEN @desde AND @hasta " +
			"  AND (@usuario IS NULL OR a.id_usuario = @usuario) " +
			$"  AND (@accion IS NULL OR a.accion = @accion COLLATE {Collation}) " +
			"ORDER BY a.fecha DESC, a.id_auditoria DESC " +
			"LIMIT @limite OFFSET @desplazamiento;", conexion);
		cmd.Parameters.Add(Parametros.SoloFecha("@desde", desde.Date));
		cmd.Parameters.Add(Parametros.SoloFecha("@hasta", hasta.Date));
		cmd.Parameters.Add(Parametros.EnteroNulo("@usuario", usuarioId));
		cmd.Parameters.Add(Parametros.Texto("@accion", string.IsNullOrWhiteSpace(accion) ? null : accion));
		cmd.Parameters.Add(Parametros.Entero("@limite", limite ?? int.MaxValue));
		cmd.Parameters.Add(Parametros.Entero("@desplazamiento", desplazamiento ?? 0));
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new DataTable();
		adaptador.Fill(tabla);
		return tabla;
	}

	/// <summary>Cuenta los movimientos que cumplen los mismos filtros que Listar.</summary>
	public int Contar(DateTime desde, DateTime hasta, int? usuarioId, string? accion)
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT COUNT(*) FROM auditoria AS a " +
			"WHERE DATE(a.fecha) BETWEEN @desde AND @hasta " +
			"  AND (@usuario IS NULL OR a.id_usuario = @usuario) " +
			$"  AND (@accion IS NULL OR a.accion = @accion COLLATE {Collation});", conexion);
		cmd.Parameters.Add(Parametros.SoloFecha("@desde", desde.Date));
		cmd.Parameters.Add(Parametros.SoloFecha("@hasta", hasta.Date));
		cmd.Parameters.Add(Parametros.EnteroNulo("@usuario", usuarioId));
		cmd.Parameters.Add(Parametros.Texto("@accion", string.IsNullOrWhiteSpace(accion) ? null : accion));
		conexion.Open();
		return Convert.ToInt32(cmd.ExecuteScalar());
	}

	// Acciones que ya tienen movimientos, para armar el filtro de la pantalla.
	public DataTable Acciones()
	{
		using MySqlConnection conexion = ConexionBD.ObtenerConexion();
		using MySqlCommand cmd = new MySqlCommand(
			"SELECT DISTINCT accion AS Codigo, " +
			string.Format(CamposAccion, $"accion COLLATE {Collation}") + " AS Nombre " +
			"FROM auditoria ORDER BY Nombre;", conexion);
		using MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
		DataTable tabla = new DataTable();
		adaptador.Fill(tabla);
		return tabla;
	}
}
