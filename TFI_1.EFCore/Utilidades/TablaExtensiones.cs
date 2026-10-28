using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace TFI_1.EFCore.Utilidades;

/// <summary>
/// Convierte resultados tipados de EF Core al DataTable que consumían las grillas
/// del proyecto original (mismos nombres de columna que los alias SQL).
/// </summary>
public static class TablaExtensiones
{
	public static DataTable ToDataTable<T>(this IEnumerable<T> filas)
	{
		PropertyInfo[] propiedades = typeof(T)
			.GetProperties(BindingFlags.Public | BindingFlags.Instance)
			.Where(p => p.CanRead && p.GetIndexParameters().Length == 0)
			.ToArray();

		var tabla = new DataTable();

		foreach (PropertyInfo propiedad in propiedades)
		{
			Type tipo = Nullable.GetUnderlyingType(propiedad.PropertyType) ?? propiedad.PropertyType;
			if (tipo.IsEnum)
				tipo = typeof(string);
			tabla.Columns.Add(propiedad.Name, tipo);
		}

		foreach (T fila in filas)
		{
			DataRow nueva = tabla.NewRow();
			foreach (PropertyInfo propiedad in propiedades)
			{
				object? valor = propiedad.GetValue(fila);
				if (valor is Enum enumeracion)
					valor = enumeracion.ToString();
				nueva[propiedad.Name] = valor ?? DBNull.Value;
			}
			tabla.Rows.Add(nueva);
		}

		return tabla;
	}
}
