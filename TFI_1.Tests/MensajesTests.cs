using System;
using TFI_1.Presentacion;
using Xunit;

namespace TFI_1.Tests;

public class MensajesTests
{
	[Fact]
	public void Describir_MuestraElMensajeOriginal()
	{
		string texto = Mensajes.Describir(new InvalidOperationException("sin stock"));

		Assert.Equal("sin stock", texto);
	}

	[Fact]
	public void Describir_SinMensaje_UsaTextoGenerico()
	{
		string texto = Mensajes.Describir(new Exception(string.Empty));

		Assert.Equal("Ocurrió un error inesperado.", texto);
	}

	[Fact]
	public void Describir_Timeout_EsAmigable()
	{
		string texto = Mensajes.Describir(new TimeoutException("x"));

		Assert.Contains("tardó demasiado", texto);
	}
}
