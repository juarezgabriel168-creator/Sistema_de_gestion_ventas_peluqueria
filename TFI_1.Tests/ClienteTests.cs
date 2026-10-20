using System;
using TFI_1.Models;
using Xunit;

namespace TFI_1.Tests;

public class ClienteTests
{
	[Fact]
	public void Dni_Valido_SeGuarda()
	{
		Cliente cliente = new Cliente("Ana", "Pérez", "12345678");

		Assert.Equal("12345678", cliente.Dni);
	}

	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	[InlineData("1234567")]
	[InlineData("123456789")]
	[InlineData("1234567A")]
	[InlineData("12.345.678")]
	public void Dni_Invalido_Lanza(string dni)
	{
		Assert.Throws<ArgumentException>(() => new Cliente("Ana", "Pérez", dni));
	}

	[Fact]
	public void NombreCompleto_UsaApellidoYNombre()
	{
		Cliente cliente = new Cliente("Ana", "Pérez", "12345678");

		Assert.Equal("Pérez, Ana", cliente.NombreCompleto);
	}

	[Fact]
	public void EsConsumidorFinal_ConDniCero()
	{
		Cliente cliente = new Cliente("Consumidor", "Final", Cliente.DniConsumidorFinal);

		Assert.True(cliente.EsConsumidorFinal);
	}

	[Fact]
	public void EsConsumidorFinal_ConOtroDni_EsFalso()
	{
		Cliente cliente = new Cliente("Ana", "Pérez", "12345678");

		Assert.False(cliente.EsConsumidorFinal);
	}

	[Fact]
	public void Activo_PorDefecto_EsVerdadero()
	{
		Assert.True(new Cliente().Activo);
	}
}
