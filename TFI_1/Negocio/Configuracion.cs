using System;
using System.Configuration;
using System.Globalization;

namespace TFI_1.Negocio;

/// <summary>
/// Valores configurables desde la sección appSettings de App.config.
/// Si una clave no está definida o no es válida se usa el valor por defecto,
/// así la aplicación funciona igual sin tocar el archivo.
/// </summary>
public static class Configuracion
{
	public const string NombreNegocioPorDefecto = "Peluquería";

	public const decimal LimiteDeudaPorDefecto = 50000m;

	/// <summary>Nombre del negocio que aparece en el título y en el ticket.</summary>
	public static string NombreNegocio =>
		LeerTexto("NombreNegocio", NombreNegocioPorDefecto);

	/// <summary>
	/// Saldo a partir del cual se avisa (no se bloquea) antes de seguir
	/// cargando deuda en la cuenta corriente de un cliente.
	/// </summary>
	public static decimal LimiteDeudaCuentaCorriente =>
		LeerDecimal("LimiteDeudaCuentaCorriente", LimiteDeudaPorDefecto);

	private static string LeerTexto(string clave, string porDefecto)
	{
		string? valor = ConfigurationManager.AppSettings[clave];
		return string.IsNullOrWhiteSpace(valor) ? porDefecto : valor.Trim();
	}

	private static decimal LeerDecimal(string clave, decimal porDefecto)
	{
		string? valor = ConfigurationManager.AppSettings[clave];
		if (string.IsNullOrWhiteSpace(valor))
		{
			return porDefecto;
		}

		// Se acepta la coma decimal local ("75000,50") o el punto ("75000.50").
		// No se aceptan separadores de miles para no confundir "50.000" con 50.
		if (decimal.TryParse(valor, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out decimal numero)
			|| decimal.TryParse(valor, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out numero))
		{
			return numero >= 0m ? numero : porDefecto;
		}

		return porDefecto;
	}
}
