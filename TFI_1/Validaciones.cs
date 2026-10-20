using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace TFI_1;

/// <summary>
/// Reglas de validación compartidas por los modelos, la capa de negocio y los
/// formularios. La idea es que cada regla (DNI, email, importes, fechas) se
/// escriba una sola vez y todos usen la misma.
/// </summary>
public static class Validaciones
{
	public const int LargoDni = 8;

	// ---------- Consultas (devuelven true/false) ----------

	public static bool TextoVacio([NotNullWhen(false)] string? valor) => string.IsNullOrWhiteSpace(valor);

	public static bool DniValido(string? dni) =>
		!TextoVacio(dni) && dni!.Length == LargoDni && dni.All(char.IsDigit);

	/// <summary>El email es opcional: vacío es válido.</summary>
	public static bool EmailValido(string? email)
	{
		if (TextoVacio(email))
		{
			return true;
		}

		string valor = email!.Trim();
		int arroba = valor.IndexOf('@');
		if (arroba <= 0 || arroba != valor.LastIndexOf('@'))
		{
			return false;
		}

		int punto = valor.LastIndexOf('.');
		return punto > arroba + 1 && punto < valor.Length - 1 && !valor.Contains(' ');
	}

	/// <summary>Importe escrito por el usuario: número válido y mayor o igual a cero.</summary>
	public static bool ImporteValido(string? texto, out decimal valor) =>
		decimal.TryParse(texto, NumberStyles.Number, CultureInfo.CurrentCulture, out valor) && valor >= 0;

	/// <summary>Importe escrito por el usuario: número válido y mayor a cero.</summary>
	public static bool ImportePositivo(string? texto, out decimal valor) =>
		decimal.TryParse(texto, NumberStyles.Number, CultureInfo.CurrentCulture, out valor) && valor > 0;

	public static bool EsImportePositivo(decimal valor) => valor > 0;

	/// <summary>Entero escrito por el usuario: número válido y mayor o igual a cero.</summary>
	public static bool EnteroNoNegativo(string? texto, out int valor) =>
		int.TryParse(texto, out valor) && valor >= 0;

	/// <summary>Entero escrito por el usuario: número válido y mayor a cero.</summary>
	public static bool EnteroPositivo(string? texto, out int valor) =>
		int.TryParse(texto, out valor) && valor > 0;

	public static bool RangoDeFechasValido(DateTime desde, DateTime hasta) => desde.Date <= hasta.Date;

	public static bool FechaNoFutura(DateTime fecha) => fecha.Date <= DateTime.Today;

	/// <summary>Un turno no puede ser anterior a "ahora" (con una pequeña tolerancia).</summary>
	public static bool TurnoNoPasado(DateTime fechaHora, int toleranciaMinutos = 5) =>
		fechaHora >= DateTime.Now.AddMinutes(-toleranciaMinutos);

	// ---------- Reglas que lanzan ArgumentException (modelos y negocio) ----------

	public static void ExigirTexto(string? valor, string mensaje)
	{
		if (TextoVacio(valor))
		{
			throw new ArgumentException(mensaje);
		}
	}

	public static void ExigirDni(string? dni)
	{
		if (!DniValido(dni))
		{
			throw new ArgumentException("El DNI debe tener 8 dígitos numéricos.");
		}
	}

	public static void ExigirEmail(string? email)
	{
		if (!EmailValido(email))
		{
			throw new ArgumentException("El email no parece válido.");
		}
	}

	public static void ExigirImporteNoNegativo(decimal valor, string mensaje)
	{
		if (valor < 0)
		{
			throw new ArgumentException(mensaje);
		}
	}

	public static void ExigirImportePositivo(decimal valor, string mensaje)
	{
		if (!EsImportePositivo(valor))
		{
			throw new ArgumentException(mensaje);
		}
	}

	public static void ExigirFechaNoFutura(DateTime fecha, string mensaje)
	{
		if (!FechaNoFutura(fecha))
		{
			throw new ArgumentException(mensaje);
		}
	}
}
