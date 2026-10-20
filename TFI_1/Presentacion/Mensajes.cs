using System;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TFI_1.Presentacion;

/// <summary>
/// Mensajes de error unificados. Todos los formularios muestran los errores
/// con el mismo formato: una frase que explica qué se estaba haciendo y,
/// debajo, el detalle. Los errores inesperados se registran en un archivo.
/// </summary>
public static class Mensajes
{
	/// <summary>
	/// Muestra un error de una operación concreta. Ejemplo:
	/// <c>Mensajes.Error(this, "No se pudo cargar el historial", ex);</c>
	/// </summary>
	public static void Error(IWin32Window? owner, string mensaje, Exception ex)
	{
		string texto = string.IsNullOrWhiteSpace(mensaje) ? "Ocurrió un error" : mensaje.Trim();
		if (!texto.EndsWith("."))
		{
			texto += ".";
		}

		string detalle = Describir(ex);
		if (!string.IsNullOrWhiteSpace(detalle))
		{
			texto += Environment.NewLine + Environment.NewLine + detalle;
		}

		MessageBox.Show(owner, texto, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
	}

	/// <summary>
	/// Muestra un error inesperado (no controlado), lo registra en un archivo
	/// y avisa dónde quedó el detalle técnico.
	/// </summary>
	public static void ErrorGlobal(Exception ex)
	{
		string ruta = RegistrarEnArchivo(ex);
		MessageBox.Show(
			"Ocurrió un error inesperado y la operación no se completó." +
			Environment.NewLine + Environment.NewLine +
			Describir(ex) +
			Environment.NewLine + Environment.NewLine +
			"El detalle se guardó en:" + Environment.NewLine + ruta,
			"Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
	}

	/// <summary>
	/// Traduce las excepciones más comunes a un texto que el usuario pueda
	/// entender. Para el resto, muestra el mensaje original.
	/// </summary>
	public static string Describir(Exception ex)
	{
		if (ex is MySqlException mysql)
		{
			switch (mysql.Number)
			{
				case 1042:
				case 2002:
				case 2003:
				case 2005:
				case 2006:
					return "No se pudo conectar con la base de datos. Verifique que MySQL esté funcionando y que la conexión de App.config sea correcta.";
				case 1045:
					return "La base de datos rechazó el usuario o la contraseña. Revise la conexión de App.config.";
				case 1062:
					return "Ya existe un registro con ese dato (por ejemplo, un DNI, usuario o nombre repetido).";
				case 1451:
					return "No se puede borrar o modificar porque el registro está siendo usado por otros datos.";
				case 1452:
					return "Falta un dato relacionado (por ejemplo, un cliente o producto que ya no existe).";
			}

			return string.IsNullOrWhiteSpace(mysql.Message)
				? "Error de la base de datos."
				: mysql.Message;
		}

		if (ex is TimeoutException)
		{
			return "La operación tardó demasiado. Intente nuevamente.";
		}

		return string.IsNullOrWhiteSpace(ex.Message) ? "Ocurrió un error inesperado." : ex.Message;
	}

	/// <summary>Guarda el detalle del error y devuelve la ruta del archivo.</summary>
	private static string RegistrarEnArchivo(Exception ex)
	{
		try
		{
			string carpeta = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
				"Peluqueria");
			Directory.CreateDirectory(carpeta);

			string ruta = Path.Combine(carpeta, "errores.log");
			File.AppendAllText(ruta,
				$"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {ex}{Environment.NewLine}{Environment.NewLine}");
			return ruta;
		}
		catch
		{
			return "(no se pudo guardar el registro del error)";
		}
	}
}
