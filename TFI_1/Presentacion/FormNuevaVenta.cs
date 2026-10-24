using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

public partial class FormNuevaVenta : Form
{
	private readonly VentaNegocio _ventaNegocio = new VentaNegocio();
	private readonly ClienteNegocio _clienteNegocio = new ClienteNegocio();
	private readonly ItemNegocio _itemNegocio = new ItemNegocio();
	private readonly EmpleadoNegocio _empleadoNegocio = new EmpleadoNegocio();
	private readonly TarjetaCreditoNegocio _tarjetaNegocio = new TarjetaCreditoNegocio();
	private readonly CuentaCorrienteNegocio _ccNegocio = new CuentaCorrienteNegocio();
	private readonly CajaNegocio _cajaNegocio = new();
	private readonly List<DetalleVenta> _carrito = new List<DetalleVenta>();

	private static readonly string[] MediosPago = { "efectivo", "tarjeta", "transferencia", "cuenta_corriente" };

	private bool _recalculando;

	private readonly BancoNegocio _bancoNegocio = new BancoNegocio();
	private List<TarjetaCredito> _tarjetasActivas = new List<TarjetaCredito>();

	public FormNuevaVenta()
	{
		InitializeComponent();
		Resize += (_, _) => AjustarLayout();
		AjustarLayout();
	}

	private void AjustarLayout()
	{
		if (IsDisposed)
		{
			return;
		}

		const int margenDerecho = 12;

		lblSubtotalValor.Left = ClientSize.Width - lblSubtotalValor.Width - margenDerecho;
		lblDescuentoValor.Left = ClientSize.Width - lblDescuentoValor.Width - margenDerecho;
		lblTotalValor.Left = ClientSize.Width - lblTotalValor.Width - margenDerecho;

		lblSubtotalTitulo.Left = lblSubtotalValor.Left - lblSubtotalTitulo.Width - 14;
		lblDescuentoTitulo.Left = lblDescuentoValor.Left - lblDescuentoTitulo.Width - 14;
		lblTotalTitulo.Left = lblTotalValor.Left - lblTotalTitulo.Width - 14;

		int limiteResumen = Math.Min(lblSubtotalTitulo.Left, Math.Min(lblDescuentoTitulo.Left, lblTotalTitulo.Left));
		dgvCarrito.Width = Math.Max(320, limiteResumen - dgvCarrito.Left - 12);
	}

	private void FormNuevaVenta_Load(object sender, EventArgs e)
	{
		try
		{
			cboMedioPago.Items.Clear();
			cboMedioPago.Items.AddRange(new object[] { "Efectivo", "Tarjeta", "Transferencia", "Cuenta Corriente" });
			cboMedioPago.SelectedIndex = 0;

			cboCliente.DataSource = _clienteNegocio.Listar().Where((Cliente c) => c.Activo).ToList();
			cboCliente.DisplayMember = "NombreCompleto";
			cboCliente.ValueMember = "Id";

			// Cliente "Consumidor Final" por defecto.
			var consumidorFinal = _clienteNegocio.ObtenerOCrearConsumidorFinal();
			var cf = cboCliente.Items.Cast<Cliente>().FirstOrDefault(c => c.Dni == "00000000");
			if (cf != null)
				cboCliente.SelectedItem = cf;

			cboItem.DataSource = _itemNegocio.Listar().Where((Item i) => i.Activo).ToList();
			cboItem.DisplayMember = "Nombre";
			cboItem.ValueMember = "Id";

			numCantidad.Value = 1m;
			ActualizarInfoItem();
			RefrescarCarrito();
			CargarEmpleadosYTarjetas();
			ActualizarVisibilidadPago();
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo iniciar la venta", ex);
		}
	}

	private void CargarEmpleadosYTarjetas()
	{
		List<Empleado> empleados = new List<Empleado> { new Empleado { Id = 0, Nombre = "—", Apellido = "Seleccione" } };
		empleados.AddRange(_empleadoNegocio.Listar().Where((Empleado em) => em.Activo).ToList());
		cboEmpleado.DataSource = empleados;
		cboEmpleado.DisplayMember = "NombreCompleto";
		cboEmpleado.ValueMember = "Id";
		cboEmpleado.SelectedIndex = 0;

		// Solo bancos activos que tengan al menos una tarjeta activa.
		_tarjetasActivas = _tarjetaNegocio.Listar(soloActivas: true);
		List<Banco> bancos = new List<Banco> { new Banco { Id = 0, Nombre = "Seleccione" } };
		bancos.AddRange(_bancoNegocio.Listar(soloActivos: true)
			.Where((Banco b) => _tarjetasActivas.Any((TarjetaCredito t) => t.IdBanco == b.Id)));
		cboBanco.DisplayMember = "Nombre";
		cboBanco.ValueMember = "Id";
		cboBanco.DataSource = bancos;
		cboBanco.SelectedIndex = 0;

		CargarTiposDeBanco();

		// Se conectan al final para que la carga inicial no dispare recálculos a medias.
		cboBanco.SelectedIndexChanged += (s, e) => CargarTiposDeBanco();
		cboTarjeta.SelectedIndexChanged += cboTarjeta_SelectedIndexChanged;
		numCuotas.ValueChanged += (s, e) => RecalcularTotales();
	}

	private void CargarTiposDeBanco()
	{
		int idBanco = cboBanco.SelectedItem is Banco banco ? banco.Id : 0;
		List<TarjetaCredito> tipos = new List<TarjetaCredito> { new TarjetaCredito { Id = 0, TipoTarjeta = "Seleccione" } };
		tipos.AddRange(_tarjetasActivas.Where((TarjetaCredito t) => t.IdBanco == idBanco));
		cboTarjeta.DisplayMember = "TipoTarjeta";
		cboTarjeta.ValueMember = "Id";
		cboTarjeta.DataSource = tipos;
		cboTarjeta.SelectedIndex = 0;
		cboTarjeta_SelectedIndexChanged(null, EventArgs.Empty);
	}

	private void cboTarjeta_SelectedIndexChanged(object? sender, EventArgs e)
	{
		// Las cuotas no pueden superar el máximo de la tarjeta elegida.
		numCuotas.Maximum = TarjetaElegida?.CuotasMax ?? 1;
		numCuotas.Value = 1;
		RecalcularTotales();
	}

	private TarjetaCredito? TarjetaElegida =>
		cboTarjeta != null && cboTarjeta.SelectedItem is TarjetaCredito t && t.Id > 0 ? t : null;

	private decimal CalcularRecargo(decimal baseCobro)
	{
		if (cboMedioPago.SelectedIndex != 1)
			return 0m;
		TarjetaCredito? t = TarjetaElegida;
		return t == null ? 0m : Math.Round(baseCobro * t.PorcentajeRecargo / 100m, 2);
	}

	private void ActualizarInfoTarjeta(decimal recargo, decimal total)
	{
		if (lblTarjetaInfo == null)
			return;
		TarjetaCredito? t = TarjetaElegida;
		if (cboMedioPago.SelectedIndex != 1 || t == null)
		{
			lblTarjetaInfo.Text = string.Empty;
			return;
		}
		int cuotas = (int)numCuotas.Value;
		lblTarjetaInfo.Text = $"Interés {t.PorcentajeRecargo:N2} %: +{recargo:C2}   |   {cuotas} cuota(s) de {total / cuotas:C2}   (máx. {t.CuotasMax})";
	}

	private void cboMedioPago_SelectedIndexChanged(object sender, EventArgs e)
	{
		ActualizarVisibilidadPago();
	}

	private void ActualizarVisibilidadPago()
	{
		bool esTarjeta = cboMedioPago.SelectedIndex == 1; // "Tarjeta"
		lblBanco.Visible = esTarjeta;
		cboBanco.Visible = esTarjeta;
		lblTarjeta.Visible = esTarjeta;
		cboTarjeta.Visible = esTarjeta;
		lblCuotas.Visible = esTarjeta;
		numCuotas.Visible = esTarjeta;
		RecalcularTotales();
	}

	private void cboItem_SelectedIndexChanged(object sender, EventArgs e)
	{
		ActualizarInfoItem();
	}

	private void ActualizarInfoItem()
	{
		if (cboItem.SelectedItem is not Item item)
		{
			lblInfo.Text = string.Empty;
			return;
		}
		string detalle = item.EsProducto ? $"Stock: {item.Stock}" : $"Duración: {item.DuracionMin} min";
		lblInfo.Text = $"Precio: {item.Precio:C2}   {detalle}";
	}

	private void btnAgregar_Click(object sender, EventArgs e)
	{
		if (cboItem.SelectedItem is not Item item)
		{
			MessageBox.Show("Seleccione un producto o servicio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		int cantidad = (int)numCantidad.Value;
		try
		{
			DetalleVenta? existente = _carrito.FirstOrDefault((DetalleVenta d) => d.Item.Id == item.Id);
			int total = (existente?.Cantidad ?? 0) + cantidad;
			if (item.EsProducto && total > (item.Stock ?? 0))
			{
				MessageBox.Show($"Stock insuficiente de {item.Nombre}. Disponible: {item.Stock}.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			if (existente != null)
			{
				_carrito.Remove(existente);
			}
			_carrito.Add(new DetalleVenta(item, total));
			RefrescarCarrito();
			numCantidad.Value = 1m;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnQuitar_Click(object sender, EventArgs e)
	{
		if (dgvCarrito.CurrentRow?.DataBoundItem is DetalleVenta detalle)
		{
			_carrito.Remove(detalle);
			RefrescarCarrito();
		}
	}

	private void numDescuento_ValueChanged(object sender, EventArgs e)
	{
		if (!_recalculando)
			RecalcularTotales();
	}

	private void RefrescarCarrito()
	{
		// Se enlaza una copia de la lista: si se reutiliza la misma instancia,
		// el CurrencyManager conserva la posición -1 que tomó al bindear la lista
		// vacía al cargar (List<T> no emite eventos de cambio) y el DataGridView
		// revienta con IndexOutOfRangeException al hacer clic en una fila.
		dgvCarrito.DataSource = null;
		dgvCarrito.DataSource = _carrito.ToList();
		RecalcularTotales();
	}

	private void RecalcularTotales()
	{
		decimal subtotal = _carrito.Sum((DetalleVenta d) => d.Subtotal);
		decimal descuento = numDescuento.Value;
		if (descuento > subtotal)
		{
			_recalculando = true;
			descuento = subtotal;
			numDescuento.Value = descuento;
			_recalculando = false;
		}
		decimal total = CalcularTotal();

		lblSubtotalValor.Text = subtotal.ToString("C2");
		lblDescuentoValor.Text = descuento.ToString("C2");
		lblTotalValor.Text = total.ToString("C2");
		ActualizarInfoTarjeta(CalcularRecargo(subtotal - descuento), total);
	}

	// Total de la venta con el descuento y el recargo de tarjeta ya aplicados.
	private decimal CalcularTotal()
	{
		decimal subtotal = _carrito.Sum((DetalleVenta d) => d.Subtotal);
		decimal descuento = Math.Min(numDescuento.Value, subtotal);
		decimal recargo = CalcularRecargo(subtotal - descuento);
		return subtotal - descuento + recargo;
	}

	private void btnRegistrar_Click(object sender, EventArgs e)
	{
		if (cboCliente.SelectedItem is not Cliente cliente)
		{
			MessageBox.Show("Seleccione un cliente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		if (_carrito.Count == 0)
		{
			MessageBox.Show("Agregue al menos un ítem a la venta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}

		int? idEmpleado = null;
		if (cboEmpleado.SelectedItem is Empleado emp && emp.Id > 0)
			idEmpleado = emp.Id;

		int? idTarjeta = null;
		int? cuotas = null;
		if (cboMedioPago.SelectedIndex == 1) // tarjeta
		{
			TarjetaCredito? tarjeta = TarjetaElegida;
			if (tarjeta == null)
			{
				MessageBox.Show("Seleccione el banco y el tipo de tarjeta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			idTarjeta = tarjeta.Id;
			cuotas = (int)numCuotas.Value;
		}

		try
		{
			if (_cajaNegocio.ObtenerAbierta() == null)
			{
				MessageBox.Show(
					"Para registrar una venta primero debe abrir la caja.\n\n" +
					"Vaya a Ventas → Caja, abra el turno con el fondo inicial y vuelva a intentarlo.",
					"Caja cerrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// Aviso (no bloqueo) si la venta a cuenta corriente deja al cliente
			// por encima del límite de deuda.
			if (MediosPago[cboMedioPago.SelectedIndex] == "cuenta_corriente")
			{
				decimal saldoActual = _ccNegocio.ObtenerSaldo(cliente.Id);
				decimal totalVenta = CalcularTotal();
				decimal saldoNuevo = saldoActual + totalVenta;
				if (saldoNuevo > CuentaCorrienteNegocio.LimiteDeudaAviso)
				{
					DialogResult continuar = MessageBox.Show(
						$"Atención: {cliente.NombreCompleto} quedaría con una deuda de {saldoNuevo:C2}.\n" +
						$"Saldo actual: {saldoActual:C2} · Esta venta: {totalVenta:C2}\n" +
						$"El límite de aviso es {CuentaCorrienteNegocio.LimiteDeudaAviso:C2}.\n\n" +
						"¿Desea registrar la venta igual?",
						"Deuda alta en cuenta corriente",
						MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
						MessageBoxDefaultButton.Button2);
					if (continuar != DialogResult.Yes)
					{
						return;
					}
				}
			}

			Venta venta = new Venta
			{
				Cliente = cliente,
				Usuario = Sesion.UsuarioActual!,
				IdEmpleado = idEmpleado,
				MedioPago = MediosPago[cboMedioPago.SelectedIndex],
				IdTarjetaCredito = idTarjeta,
				CantidadCuotas = cuotas,
				Descuento = numDescuento.Value
			};
			foreach (DetalleVenta detalle in _carrito)
				venta.Detalles.Add(detalle);

			int id = _ventaNegocio.Registrar(venta);

			DialogResult respuestaTicket = MessageBox.Show(
				$"Venta #{id} registrada.\nTotal: {venta.Total:C2}\n\n¿Desea ver el ticket?",
				"Venta registrada", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

			if (respuestaTicket == DialogResult.Yes)
			{
				using FormTicket ticket = new FormTicket(id);
				ticket.ShowDialog(this);
			}

			_carrito.Clear();
			_recalculando = true;
			numDescuento.Value = 0m;
			_recalculando = false;
			RefrescarCarrito();
			if (cboBanco.Items.Count > 0) cboBanco.SelectedIndex = 0;
		}
		catch (ArgumentException ex)
		{
			MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		catch (InvalidOperationException ex)
		{
			MessageBox.Show(ex.Message, "Venta no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo registrar la venta", ex);
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		if (_carrito.Count > 0)
		{
			var respuesta = MessageBox.Show("¿Desea cancelar la venta? Se perderán los ítems del carrito.", "Confirmar cancelación",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (respuesta != DialogResult.Yes)
				return;
		}
		Close();
	}
}
