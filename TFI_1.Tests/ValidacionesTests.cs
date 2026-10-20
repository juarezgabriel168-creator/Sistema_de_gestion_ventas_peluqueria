using System;
using System.Globalization;
using TFI_1.Models;
using Xunit;

namespace TFI_1.Tests;

public class ValidacionesTests
{
	[Theory]
	[InlineData("12345678", true)]
	[InlineData("00000000", true)]
	[InlineData("1234567", false)]
	[InlineData("123456789", false)]
	[InlineData("1234567a", false)]
	[InlineData("", false)]
	[InlineData(null, false)]
	public void Dni_SeValidaConOchoDigitos(string? dni, bool esperado)
	{
		Assert.Equal(esperado, Validaciones.DniValido(dni));
	}

	[Theory]
	[InlineData(null, true)]
	[InlineData("", true)]
	[InlineData("   ", true)]
	[InlineData("ana@ejemplo.com", true)]
	[InlineData("ana.perez@ejemplo.com.ar", true)]
	[InlineData("ana@ejemplo", false)]
	[InlineData("ana.ejemplo.com", false)]
	[InlineData("ana@@ejemplo.com", false)]
	[InlineData("@ejemplo.com", false)]
	[InlineData("ana@ejemplo.", false)]
	[InlineData("ana perez@ejemplo.com", false)]
	public void Email_SeValidaCuandoEstaPresente(string? email, bool esperado)
	{
		Assert.Equal(esperado, Validaciones.EmailValido(email));
	}

	[Fact]
	public void Importe_AceptaNumerosYRechazaNegativos()
	{
		string positivo = 1500.5m.ToString(CultureInfo.CurrentCulture);

		Assert.True(Validaciones.ImporteValido(positivo, out decimal valor));
		Assert.Equal(1500.5m, valor);
		Assert.True(Validaciones.ImporteValido("0", out _));
		Assert.False(Validaciones.ImporteValido("-5", out _));
		Assert.False(Validaciones.ImporteValido("abc", out _));
		Assert.False(Validaciones.ImporteValido("", out _));
	}

	[Theory]
	[InlineData("10", true)]
	[InlineData("0", false)]
	[InlineData("-1", false)]
	[InlineData("abc", false)]
	public void ImportePositivo_ExigeMayorACero(string texto, bool esperado)
	{
		Assert.Equal(esperado, Validaciones.ImportePositivo(texto, out _));
	}

	[Theory]
	[InlineData("0", true)]
	[InlineData("25", true)]
	[InlineData("-1", false)]
	[InlineData("1,5", false)]
	[InlineData("", false)]
	public void EnteroNoNegativo_SeValida(string texto, bool esperado)
	{
		Assert.Equal(esperado, Validaciones.EnteroNoNegativo(texto, out _));
	}

	[Theory]
	[InlineData("1", true)]
	[InlineData("90", true)]
	[InlineData("0", false)]
	[InlineData("-3", false)]
	public void EnteroPositivo_ExigeMayorACero(string texto, bool esperado)
	{
		Assert.Equal(esperado, Validaciones.EnteroPositivo(texto, out _));
	}

	[Fact]
	public void RangoDeFechas_DesdeNoPuedeSuperarHasta()
	{
		DateTime hoy = DateTime.Today;

		Assert.True(Validaciones.RangoDeFechasValido(hoy, hoy));
		Assert.True(Validaciones.RangoDeFechasValido(hoy.AddDays(-1), hoy));
		Assert.False(Validaciones.RangoDeFechasValido(hoy.AddDays(1), hoy));
	}

	[Fact]
	public void FechaNoFutura_SoloAceptaHastaHoy()
	{
		Assert.True(Validaciones.FechaNoFutura(DateTime.Today));
		Assert.True(Validaciones.FechaNoFutura(DateTime.Today.AddDays(-1)));
		Assert.False(Validaciones.FechaNoFutura(DateTime.Today.AddDays(1)));
	}

	[Fact]
	public void TurnoNoPasado_ToleraCincoMinutos()
	{
		DateTime ahora = DateTime.Now;

		Assert.True(Validaciones.TurnoNoPasado(ahora));
		Assert.True(Validaciones.TurnoNoPasado(ahora.AddMinutes(-1)));
		Assert.False(Validaciones.TurnoNoPasado(ahora.AddMinutes(-10)));
	}

	[Fact]
	public void ExigirTexto_LanzaCuandoEstaVacio()
	{
		Assert.Throws<ArgumentException>(() => Validaciones.ExigirTexto("   ", "Falta el nombre."));
		Validaciones.ExigirTexto("Ana", "Falta el nombre.");
	}

	[Fact]
	public void ExigirDni_LanzaCuandoEsInvalido()
	{
		Assert.Throws<ArgumentException>(() => Validaciones.ExigirDni("123"));
		Validaciones.ExigirDni("12345678");
	}

	[Fact]
	public void ExigirEmail_LanzaCuandoEsInvalido()
	{
		Assert.Throws<ArgumentException>(() => Validaciones.ExigirEmail("ana@ejemplo"));
		Validaciones.ExigirEmail("ana@ejemplo.com");
	}

	[Fact]
	public void ExigirLongitudMaxima_AceptaElLimiteYRechazaElExceso()
	{
		Validaciones.ExigirLongitudMaxima(null, 3, "Demasiado largo.");
		Validaciones.ExigirLongitudMaxima("abc", 3, "Demasiado largo.");
		Assert.Throws<ArgumentException>(() =>
			Validaciones.ExigirLongitudMaxima("abcd", 3, "Demasiado largo."));
	}

	[Fact]
	public void ExigirImportePositivo_LanzaConCeroONegativo()
	{
		Assert.Throws<ArgumentException>(() => Validaciones.ExigirImportePositivo(0m, "Debe ser mayor a cero."));
		Assert.Throws<ArgumentException>(() => Validaciones.ExigirImportePositivo(-1m, "Debe ser mayor a cero."));
		Validaciones.ExigirImportePositivo(10m, "Debe ser mayor a cero.");
	}

	[Fact]
	public void ExigirFechaNoFutura_LanzaConManana()
	{
		Assert.Throws<ArgumentException>(() =>
			Validaciones.ExigirFechaNoFutura(DateTime.Today.AddDays(1), "No puede ser futura."));
		Validaciones.ExigirFechaNoFutura(DateTime.Today, "No puede ser futura.");
	}

	// Los modelos usan las mismas reglas: si se saltean un formulario, igual fallan.
	[Fact]
	public void Cliente_RechazaDniInvalido()
	{
		Assert.Throws<ArgumentException>(() => new Cliente { Dni = "123" });
	}

	[Fact]
	public void Categoria_RechazaNombreVacio()
	{
		Assert.Throws<ArgumentException>(() => new Categoria { Nombre = "  " });
	}

	[Fact]
	public void Item_RechazaPrecioNegativo()
	{
		Assert.Throws<ArgumentException>(() => new Item { Precio = -1m });
	}

	[Fact]
	public void Usuario_RechazaNombreVacio()
	{
		Assert.Throws<ArgumentException>(() => new Usuario { NombreUsuario = "" });
	}
}
