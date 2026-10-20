using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

/// <summary>
/// Consulta y cobro de la cuenta corriente de los clientes: muestra el saldo
/// adeudado, el historial de movimientos (cargos por venta y abonos) y permite
/// registrar pagos.
/// </summary>
public partial class FormCuentaCorriente : Form
{
	private readonly CuentaCorrienteNegocio _negocio = new();
	private readonly ClienteNegocio _clienteNegocio = new();

	public FormCuentaCorriente()
	{
		InitializeComponent();
	}

	private void FormCuentaCorriente_Load(object? sender, EventArgs e)
	{
		try
		{
			AjustarRangoFechas(dtpDesde, DateTime.Today.AddYears(-1));
			AjustarRangoFechas(dtpHasta, DateTime.Today);

			var clientes = _clienteNegocio.Listar()
				.Where(c => c.Activo && !c.EsConsumidorFinal)
				.OrderBy(c => c.NombreCompleto)
				.ToList();

			cboCliente.DataSource = clientes;
			cboCliente.DisplayMember = "NombreCompleto";
			cboCliente.ValueMember = "Id";
			if (cboCliente.Items.Count > 0)
				cboCliente.SelectedIndex = 0;

			Cargar();
		}
		catch (Exception ex)
		{
			MessageBox.Show("No se pudo abrir la cuenta corriente: " + ex.Message, "Error",
				MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void cboCliente_SelectedIndexChanged(object? sender, EventArgs e)
	{
		Cargar();
	}

	private void btnBuscar_Click(object? sender, EventArgs e)
	{
		Cargar();
	}

	private void btnRefrescar_Click(object? sender, EventArgs e)
	{
		Refrescar();
	}

	/// <summary>
	/// Asigna una fecha a un DateTimePicker ampliando antes su rango MinDate/MaxDate,
	/// para evitar ArgumentOutOfRangeException si el valor por defecto queda fuera.
	/// </summary>
	private static void AjustarRangoFechas(DateTimePicker dtp, DateTime valor)
	{
		try
		{
			dtp.MinDate = new DateTime(1900, 1, 1);
			dtp.MaxDate = new DateTime(2100, 12, 31);
			dtp.Value = valor;
		}
		catch
		{
			// Si aun así falla, se deja el valor por defecto del control.
		}
	}

	private Cliente? ClienteSeleccionado => cboCliente.SelectedItem as Cliente;

	private void Cargar()
	{
		if (ClienteSeleccionado is not Cliente cliente)
		{
			lblSaldo.Text = "—";
			dgvMovimientos.DataSource = null;
			btnRegistrarAbono.Enabled = false;
			return;
		}

		try
		{
			decimal saldo = _negocio.ObtenerSaldo(cliente.Id);
			lblSaldo.Text = saldo.ToString("C2");
			lblSaldo.ForeColor = saldo > 0 ? Color.Firebrick : Color.SeaGreen;

			if (saldo > 0)
			{
				lblSaldoSinDeuda.Text = string.Empty;
				numMonto.Maximum = saldo;
				btnRegistrarAbono.Enabled = true;
			}
			else
			{
				lblSaldoSinDeuda.Text = "El cliente no tiene saldo adeudado.";
				numMonto.Maximum = 0m;
				numMonto.Value = 0m;
				btnRegistrarAbono.Enabled = false;
			}

			DataTable movimientos = _negocio.MovimientosResumen(cliente.Id, dtpDesde.Value, dtpHasta.Value);
			dgvMovimientos.DataSource = movimientos;
			FormatearGrilla();
		}
		catch (Exception ex)
		{
			MessageBox.Show("No se pudieron cargar los movimientos: " + ex.Message, "Error",
				MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void Refrescar()
	{
		AjustarRangoFechas(dtpDesde, DateTime.Today.AddYears(-1));
		AjustarRangoFechas(dtpHasta, DateTime.Today);
		numMonto.Value = 0m;
		txtConcepto.Clear();
		Cargar();
	}

	private void FormatearGrilla()
	{
		FormatearColumna("fecha", "Fecha", "dd/MM/yyyy HH:mm");
		FormatearColumna("tipo", "Tipo");
		FormatearColumna("concepto", "Concepto");
		FormatearColumna("monto", "Monto", "C2");
		FormatearColumna("id_venta", "Nº venta");
		FormatearColumna("nombre_usuario", "Usuario");
	}

	private void FormatearColumna(string nombre, string encabezado, string? formato = null)
	{
		if (!dgvMovimientos.Columns.Contains(nombre))
			return;
		DataGridViewColumn? columna = dgvMovimientos.Columns[nombre];
		if (columna == null)
			return;
		columna.HeaderText = encabezado;
		if (formato != null)
			columna.DefaultCellStyle.Format = formato;
	}

	private void btnRegistrarAbono_Click(object? sender, EventArgs e)
	{
		if (ClienteSeleccionado is not Cliente cliente)
		{
			MessageBox.Show("Seleccione un cliente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}

		decimal monto = numMonto.Value;
		if (monto <= 0)
		{
			MessageBox.Show("Ingrese el monto del abono.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}

		decimal saldo = _negocio.ObtenerSaldo(cliente.Id);
		DialogResult confirmacion = MessageBox.Show(
			$"¿Registrar un pago de {monto:C2} para {cliente.NombreCompleto}?\nSaldo actual: {saldo:C2}",
			"Confirmar pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (confirmacion != DialogResult.Yes)
			return;

		try
		{
			_negocio.RegistrarAbono(cliente.Id, monto, txtConcepto.Text);
			MessageBox.Show("Pago registrado correctamente.", "Abono registrado",
				MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			numMonto.Value = 0m;
			txtConcepto.Clear();
			Cargar();
		}
		catch (ArgumentException ex)
		{
			MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		catch (InvalidOperationException ex)
		{
			MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		catch (Exception ex)
		{
			MessageBox.Show("No se pudo registrar el abono: " + ex.Message, "Error",
				MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}
}
