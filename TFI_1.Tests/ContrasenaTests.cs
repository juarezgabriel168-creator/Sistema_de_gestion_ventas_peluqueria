using System;
using System.Collections.Generic;
using TFI_1.Negocio;
using Xunit;

namespace TFI_1.Tests;

public class ContrasenaTests
{
	[Theory]
	[InlineData("admin", "ClaveSegura9")]
	[InlineData("vendedor", "OtraClave2026")]
	[InlineData("ana", "Clave2026x")]
	[InlineData("", "Segura2026x")]
	public void Contrasena_Valida_NoTieneProblemas(string usuario, string clave)
	{
		List<string> problemas = UsuarioNegocio.ValidarContrasena(usuario, clave);

		Assert.Empty(problemas);
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData("   ")]
	public void Contrasena_Vacia_EsObligatoria(string? clave)
	{
		List<string> problemas = UsuarioNegocio.ValidarContrasena("admin", clave);

		Assert.Contains(problemas, p => p.Contains("obligatoria", StringComparison.OrdinalIgnoreCase));
	}

	[Theory]
	[InlineData("corta1A", "8 caracteres")]
	[InlineData("sinmayuscula1", "mayúscula")]
	[InlineData("SINMINUSCULA1", "minúscula")]
	[InlineData("SinNumeroXX", "número")]
	[InlineData("Admin1234", "nombre de usuario")]
	[InlineData("Password", "común")]
	public void Contrasena_Invalida_InformaElProblema(string clave, string fragmento)
	{
		List<string> problemas = UsuarioNegocio.ValidarContrasena("admin", clave);

		Assert.Contains(problemas, p => p.Contains(fragmento, StringComparison.OrdinalIgnoreCase));
	}

	[Fact]
	public void Contrasena_DemasiadoLarga_InformaElLimite()
	{
		string clave = "Aa1" + new string('x', UsuarioNegocio.LargoMaximoContrasena);
		List<string> problemas = UsuarioNegocio.ValidarContrasena("admin", clave);

		Assert.Contains(problemas, p => p.Contains("100", StringComparison.OrdinalIgnoreCase));
	}

	[Fact]
	public void Contrasena_ConUsuarioYComun_InformaAmbos()
	{
		List<string> problemas = UsuarioNegocio.ValidarContrasena("vendedor", "Vendedor123");

		Assert.Contains(problemas, p => p.Contains("nombre de usuario", StringComparison.OrdinalIgnoreCase));
		Assert.Contains(problemas, p => p.Contains("común", StringComparison.OrdinalIgnoreCase));
	}
}
