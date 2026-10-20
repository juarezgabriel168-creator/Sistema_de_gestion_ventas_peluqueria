using System;
using MySql.Data.MySqlClient;

namespace TFI_1.Datos;

/// <summary>
/// Crea parámetros SQL con el tipo de dato explícito. Evita AddWithValue, que
/// deja que el proveedor adivine el tipo y puede fallar con decimales, fechas
/// o valores nulos.
/// </summary>
internal static class Parametros
{
	private const byte Verdadero = 1;
	private const byte Falso = 0;

	public static MySqlParameter Entero(string nombre, int valor) =>
		new MySqlParameter(nombre, MySqlDbType.Int32) { Value = valor };

	public static MySqlParameter EnteroNulo(string nombre, int? valor) =>
		new MySqlParameter(nombre, MySqlDbType.Int32) { Value = valor ?? (object)DBNull.Value };

	public static MySqlParameter Texto(string nombre, string? valor, int? tamano = null) =>
		new MySqlParameter(nombre, MySqlDbType.VarChar, tamano ?? Math.Max(valor?.Length ?? 1, 1))
		{
			Value = valor ?? (object)DBNull.Value
		};

	public static MySqlParameter Fecha(string nombre, DateTime valor) =>
		new MySqlParameter(nombre, MySqlDbType.DateTime) { Value = valor };

	public static MySqlParameter FechaNula(string nombre, DateTime? valor) =>
		new MySqlParameter(nombre, MySqlDbType.DateTime) { Value = valor ?? (object)DBNull.Value };

	public static MySqlParameter SoloFecha(string nombre, DateTime valor) =>
		new MySqlParameter(nombre, MySqlDbType.Date) { Value = valor.Date };

	public static MySqlParameter SoloFechaNula(string nombre, DateTime? valor) =>
		new MySqlParameter(nombre, MySqlDbType.Date) { Value = valor?.Date ?? (object)DBNull.Value };

	public static MySqlParameter Importe(string nombre, decimal valor) =>
		new MySqlParameter(nombre, MySqlDbType.Decimal)
		{
			Precision = 12,
			Scale = 2,
			Value = valor
		};

	public static MySqlParameter ImporteNulo(string nombre, decimal? valor) =>
		new MySqlParameter(nombre, MySqlDbType.Decimal)
		{
			Precision = 12,
			Scale = 2,
			Value = valor ?? (object)DBNull.Value
		};

	public static MySqlParameter Booleano(string nombre, bool valor) =>
		new MySqlParameter(nombre, MySqlDbType.Byte) { Value = valor ? Verdadero : Falso };
}
