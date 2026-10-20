using TFI_1.Models;
using Xunit;

namespace TFI_1.Tests;

public class CajaTests
{
	[Fact]
	public void CajaNueva_EstaAbierta()
	{
		Caja caja = new Caja();

		Assert.Equal("abierta", caja.Estado);
		Assert.True(caja.Abierta);
	}

	[Fact]
	public void CajaCerrada_NoEstaAbierta()
	{
		Caja caja = new Caja { Estado = "cerrada" };

		Assert.False(caja.Abierta);
	}

	[Fact]
	public void CajaAbierta_ConEstadoExplicito()
	{
		Caja caja = new Caja { Estado = "abierta" };

		Assert.True(caja.Abierta);
	}
}
