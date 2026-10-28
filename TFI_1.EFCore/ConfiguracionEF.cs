using System;
using System.IO;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace TFI_1.EFCore;

/// <summary>
/// Resuelve la cadena de conexión: variable de entorno, appsettings.json o
/// una cadena de ejemplo sin credenciales reales.
/// </summary>
public static class ConfiguracionEF
{
	public const string VariableConexion = "TFI_PELUQUERIA_CONNECTION";
	public const string VariableVersion = "TFI_MYSQL_VERSION";

	// Valor de ejemplo. La cadena real se configura en appsettings.json
	// (archivo ignorado por Git) o en la variable de entorno.
	private const string CadenaOriginal =
		"Server=localhost;Port=3306;Database=peluqueria;Uid=peluqueria_app;" +
		"Pwd=CAMBIAR_ESTA_CONTRASENA;CharSet=utf8mb4;";

	private static readonly Lazy<string> _cadena = new(LeerCadena);
	private static readonly Lazy<MySqlServerVersion> _version = new(LeerVersion);

	public static string CadenaConexion => _cadena.Value;

	public static MySqlServerVersion VersionServidor => _version.Value;

	public static DbContextOptions<PeluqueriaDbContext> CrearOpciones() =>
		new DbContextOptionsBuilder<PeluqueriaDbContext>()
			.UseMySql(CadenaConexion, VersionServidor)
			.Options;

	private static string LeerCadena()
	{
		string? desdeEntorno = Environment.GetEnvironmentVariable(VariableConexion);
		if (!string.IsNullOrWhiteSpace(desdeEntorno))
			return desdeEntorno.Trim();

		foreach (string archivo in ArchivosCandidatos())
		{
			if (!File.Exists(archivo))
				continue;

			try
			{
				using JsonDocument documento = JsonDocument.Parse(File.ReadAllText(archivo));
				if (documento.RootElement.TryGetProperty("ConnectionStrings", out JsonElement cadenas)
					&& cadenas.TryGetProperty("Peluqueria", out JsonElement valor)
					&& !string.IsNullOrWhiteSpace(valor.GetString()))
				{
					return valor.GetString()!.Trim();
				}
			}
			catch (JsonException)
			{
				// Si el archivo está mal formado se prueba el siguiente candidato.
			}
		}

		return CadenaOriginal;
	}

	private static MySqlServerVersion LeerVersion()
	{
		string? texto = Environment.GetEnvironmentVariable(VariableVersion);
		if (!string.IsNullOrWhiteSpace(texto) && Version.TryParse(texto, out Version? version))
			return new MySqlServerVersion(version);

		return new MySqlServerVersion(new Version(8, 0, 46));
	}

	private static string[] ArchivosCandidatos() =>
		new[]
		{
			Path.Combine(AppContext.BaseDirectory, "appsettings.json"),
			Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"),
		};
}
