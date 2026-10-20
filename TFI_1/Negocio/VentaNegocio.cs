using System;
using System.Collections.Generic;
using System.Data;
using TFI_1.Datos;
using TFI_1.Models;

namespace TFI_1.Negocio;

public class VentaNegocio
{
    private readonly TarjetaCreditoDatos _tarjetaDatos = new();

    private readonly VentaDatos _datos = new VentaDatos();

	private readonly ItemDatos _itemDatos = new ItemDatos();

    public int Registrar(Venta venta)
    {
        if (venta == null)
            throw new ArgumentException("La venta es obligatoria.");
        if (venta.Cliente == null || venta.Cliente.Id <= 0)
            throw new ArgumentException("Debe seleccionar un cliente.");
        if (venta.Usuario == null || venta.Usuario.Id <= 0)
            throw new ArgumentException("No hay un usuario en sesión para registrar la venta.");
        if (venta.Detalles.Count == 0)
            throw new ArgumentException("La venta debe tener al menos un ítem.");
        if (string.IsNullOrWhiteSpace(venta.MedioPago))
            throw new ArgumentException("Debe seleccionar un medio de pago.");
        if (venta.Descuento < 0m)
            throw new ArgumentException("El descuento no puede ser negativo.");

        // Validaciones según medio de pago.
        if (venta.MedioPago == "tarjeta")
        {
            if (!venta.IdTarjetaCredito.HasValue || venta.IdTarjetaCredito.Value <= 0)
                throw new ArgumentException("Debe seleccionar el banco y el tipo de tarjeta.");
            if (!venta.CantidadCuotas.HasValue || venta.CantidadCuotas.Value < 1)
                throw new ArgumentException("Debe indicar la cantidad de cuotas.");
        }
        if (venta.MedioPago == "cuenta_corriente" && venta.Cliente.EsConsumidorFinal)
            throw new ArgumentException("No se puede vender a cuenta corriente al Consumidor Final. Seleccione un cliente.");

        // Recalcular precios y stock con los datos reales de la base de datos.
        // Así el total no depende de valores manipulables desde la pantalla.
        List<DetalleVenta> recalculado = new List<DetalleVenta>();
        foreach (DetalleVenta detalle in venta.Detalles)
        {
            Item actual = _itemDatos.ObtenerPorId(detalle.Item.Id);
            if (actual == null || !actual.Activo)
                throw new ArgumentException($"El ítem '{detalle.Item.Nombre}' ya no está disponible.");
            recalculado.Add(new DetalleVenta(actual, detalle.Cantidad));
        }
        venta.Detalles.Clear();
        venta.Detalles.AddRange(recalculado);

        if (venta.Descuento > venta.Subtotal)
            throw new ArgumentException("El descuento no puede superar el subtotal.");

        // El recargo se calcula al final, sobre el subtotal ya recalculado.
        venta.Recargo = 0m;
        if (venta.MedioPago == "tarjeta")
        {
            TarjetaCredito? tarjeta = _tarjetaDatos.ObtenerPorId(venta.IdTarjetaCredito!.Value);
            if (tarjeta == null || !tarjeta.Activo)
                throw new ArgumentException("La tarjeta seleccionada ya no está disponible.");
            if (venta.CantidadCuotas > tarjeta.CuotasMax)
                throw new ArgumentException($"Esta tarjeta permite hasta {tarjeta.CuotasMax} cuotas.");
            venta.Recargo = Math.Round((venta.Subtotal - venta.Descuento) * tarjeta.PorcentajeRecargo / 100m, 2);
        }
        else
        {
            venta.IdTarjetaCredito = null;
            venta.CantidadCuotas = null;
        }

        venta.Fecha = DateTime.Now;
        return _datos.Registrar(venta);
    }

    public DataTable Listar(DateTime desde, DateTime hasta, int? usuarioId = null, int? empleadoId = null, int? ventaId = null)
	{
		return _datos.Listar(desde, hasta, usuarioId, empleadoId, ventaId);
	}

	public DataTable ObtenerCabecera(int ventaId)
	{
		return _datos.ObtenerCabecera(ventaId);
	}

	public DataTable ObtenerDetalle(int ventaId)
	{
		return _datos.ObtenerDetalle(ventaId);
	}
}
