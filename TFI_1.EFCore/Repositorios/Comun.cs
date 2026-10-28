using System;
using System.Linq;

namespace TFI_1.EFCore.Repositorios;

internal static class Comun
{
	/// <summary>Rango equivalente a DATE(columna) BETWEEN desde AND hasta.</summary>
	public static (DateTime Inicio, DateTime Fin) Rango(DateTime desde, DateTime hasta) =>
		(desde.Date, hasta.Date.AddDays(1));

	public static IQueryable<T> Paginar<T>(IQueryable<T> consulta, int? limite, int? desplazamiento)
	{
		if (desplazamiento is > 0)
			consulta = consulta.Skip(desplazamiento.Value);

		if (limite is > 0 and < int.MaxValue)
			consulta = consulta.Take(limite.Value);

		return consulta;
	}

	public static string? Limpiar(string? valor) =>
		string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
}
