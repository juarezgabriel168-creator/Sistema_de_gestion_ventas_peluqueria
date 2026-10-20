using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;
using TFI_1.Datos;

namespace TFI_1.Negocio;

/// <summary>
/// Genera una copia de seguridad de la base con mysqldump. Las credenciales
/// se pasan en un archivo temporal (se borra al terminar) para no exponerlas
/// en la línea de comandos ni en el nombre del proceso.
/// </summary>
public class BackupNegocio
{
	private readonly AuditoriaNegocio _auditoria = new();

	private const int EsperaMaximaMs = 120000;

	/// <summary>Genera la copia y devuelve la ruta completa del archivo creado.</summary>
	public string Generar(string carpetaDestino)
	{
		if (string.IsNullOrWhiteSpace(carpetaDestino))
		{
			throw new ArgumentException("Seleccione la carpeta donde guardar la copia.");
		}
		if (!Directory.Exists(carpetaDestino))
		{
			throw new ArgumentException("La carpeta seleccionada no existe.");
		}

		MySqlConnectionStringBuilder cadena;
		try
		{
			cadena = new MySqlConnectionStringBuilder(ConexionBD.CadenaConexion);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("La cadena de conexión de App.config no es válida.", ex);
		}

		string baseDatos = cadena.Database;
		if (string.IsNullOrWhiteSpace(baseDatos))
		{
			throw new InvalidOperationException("La cadena de conexión no indica la base de datos.");
		}

		string archivo = Path.Combine(carpetaDestino,
			$"backup_{baseDatos}_{DateTime.Now:yyyyMMdd_HHmmss}.sql");
		string opciones = Path.Combine(Path.GetTempPath(), $"mysql_backup_{Guid.NewGuid():N}.cnf");

		try
		{
			File.WriteAllText(opciones, ConstruirArchivoOpciones(cadena), new UTF8Encoding(false));

			ProcessStartInfo inicio = new ProcessStartInfo
			{
				FileName = Configuracion.RutaMysqldump,
				UseShellExecute = false,
				RedirectStandardError = true,
				CreateNoWindow = true,
			};
			inicio.ArgumentList.Add("--defaults-extra-file=" + opciones);
			inicio.ArgumentList.Add("--single-transaction");
			inicio.ArgumentList.Add("--no-tablespaces");
			inicio.ArgumentList.Add("--default-character-set=utf8mb4");
			inicio.ArgumentList.Add("--databases");
			inicio.ArgumentList.Add(baseDatos);
			inicio.ArgumentList.Add("--result-file=" + archivo);

			Process? proceso;
			try
			{
				proceso = Process.Start(inicio);
			}
			catch (Win32Exception)
			{
				throw new InvalidOperationException(
					"No se encontró mysqldump. Configure la ruta completa en App.config " +
					"(clave RutaMysqldump), por ejemplo " +
					"\"C:\\Program Files\\MySQL\\MySQL Server 8.0\\bin\\mysqldump.exe\".");
			}

			if (proceso == null)
			{
				throw new InvalidOperationException("No se pudo iniciar mysqldump.");
			}

			using (proceso)
			{
				string error = proceso.StandardError.ReadToEnd();
				if (!proceso.WaitForExit(EsperaMaximaMs))
				{
					try
					{
						proceso.Kill(true);
					}
					catch
					{
						// El proceso ya puede haber terminado.
					}
					throw new TimeoutException("La copia tardó demasiado y se canceló.");
				}
				if (proceso.ExitCode != 0)
				{
					throw new InvalidOperationException(
						"mysqldump devolvió un error: " +
						(string.IsNullOrWhiteSpace(error) ? "sin detalle." : error.Trim()));
				}
			}

			FileInfo info = new FileInfo(archivo);
			if (!info.Exists || info.Length == 0)
			{
				throw new InvalidOperationException(
					"La copia se generó vacía. Verifique que la carpeta permita escribir.");
			}

			_auditoria.Registrar("backup_generado", "sistema", null,
				$"Copia de seguridad generada: {info.Name} ({info.Length / 1024.0:N1} KB).");

			return archivo;
		}
		finally
		{
			try
			{
				if (File.Exists(opciones))
				{
					File.Delete(opciones);
				}
			}
			catch
			{
				// Si no se puede borrar, no se interrumpe la operación.
			}
		}
	}

	// Archivo de opciones de MySQL: evita pasar la contraseña por parámetros.
	private static string ConstruirArchivoOpciones(MySqlConnectionStringBuilder cadena)
	{
		StringBuilder texto = new StringBuilder();
		texto.AppendLine("[client]");
		texto.AppendLine("host=" + ValorOpcion(cadena.Server));
		if (cadena.Port > 0)
		{
			texto.AppendLine("port=" + cadena.Port);
		}
		texto.AppendLine("user=" + ValorOpcion(cadena.UserID));
		texto.AppendLine("password=" + ValorOpcion(cadena.Password));
		return texto.ToString();
	}

	private static string ValorOpcion(string? valor)
	{
		valor ??= string.Empty;
		return "\"" + valor.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
	}
}
