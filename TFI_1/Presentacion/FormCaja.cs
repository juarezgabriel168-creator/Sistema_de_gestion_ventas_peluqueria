using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

/// <summary>
/// Operación de la caja del turno: apertura con fondo inicial, ingresos y
/// egresos de efectivo, cierre con el efectivo contado e historial.
/// </summary>
public partial class FormCaja : Form
{
	private readonly CajaNegocio _negocio = new();

	private Caja? _caja;

	public FormCaja()
	{
		InitializeComponent();
	}

	private void FormCaja_Load(object sender, EventArgs e)
	{
		Cargar();
	}

	private void Cargar()
	{
		try
		{
			Caja? caja = _negocio.ObtenerAbierta();
			_caja = caja;

			if (caja == null)
			{
				lblEstado.Text = "Caja cerrada";
				lblEstado.ForeColor = Color.Firebrick;
				lblApertura.Text = "No hay una caja abierta. Ingrese el fondo inicial y ábrala.";
				lblResumen.Text = string.Empty;
				lblEsperado.Text = string.Empty;
				dgvResumen.DataSource = null;
				dgvMovimientos.DataSource = null;
				Habilitar(false);
				return;
			}

			lblEstado.Text = "Caja abierta";
			lblEstado.ForeColor = Color.ForestGreen;
			lblApertura.Text = $"Abierta el {caja.FechaApertura:dd/MM/yyyy HH:mm} por {caja.UsuarioApertura} " +
				$"· Fondo inicial: {caja.MontoInicial:C2}";

			decimal ventas = _negocio.VentasEfectivo(caja);
			decimal ingresos = _negocio.TotalMovimientos(caja.Id, "ingreso");
			decimal egresos = _negocio.TotalMovimientos(caja.Id, "egreso");
			decimal esperado = _negocio.CalcularEsperado(caja);

			lblResumen.Text =
				$"Ventas en efectivo: {ventas:C2}{Environment.NewLine}" +
				$"Ingresos de caja: {ingresos:C2}{Environment.NewLine}" +
				$"Egresos de caja: {egresos:C2}{Environment.NewLine}" +
				$"Fondo inicial: {caja.MontoInicial:C2}";
			lblEsperado.Text = $"Efectivo esperado en caja: {esperado:C2}";

			dgvResumen.DataSource = _negocio.ResumenVentas(caja);
			FormatearResumen();
			dgvMovimientos.DataSource = _negocio.ListarMovimientos(caja.Id);
			FormatearMovimientos();
			Habilitar(true);
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo cargar la caja", ex);
		}
	}

	private void Habilitar(bool abierta)
	{
		btnAbrir.Enabled = !abierta;
		numMontoInicial.Enabled = !abierta;
		btnIngreso.Enabled = abierta;
		btnEgreso.Enabled = abierta;
		numMontoMov.Enabled = abierta;
		txtConceptoMov.Enabled = abierta;
		btnCerrarCaja.Enabled = abierta;
	}

	private void btnAbrir_Click(object sender, EventArgs e)
	{
		try
		{
			_negocio.Abrir(numMontoInicial.Value);
			numMontoInicial.Value = 0m;
			Cargar();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
	}

	private void btnIngreso_Click(object sender, EventArgs e) => RegistrarMovimiento("ingreso");

	private void btnEgreso_Click(object sender, EventArgs e) => RegistrarMovimiento("egreso");

	private void RegistrarMovimiento(string tipo)
	{
		Caja? caja = _caja;
		if (caja == null)
		{
			return;
		}

		try
		{
			_negocio.RegistrarMovimiento(caja.Id, tipo, numMontoMov.Value, txtConceptoMov.Text);
			numMontoMov.Value = 0m;
			txtConceptoMov.Clear();
			Cargar();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
	}

	private void btnCerrarCaja_Click(object sender, EventArgs e)
	{
		Caja? caja = _caja;
		if (caja == null)
		{
			return;
		}

		try
		{
			decimal esperado = _negocio.CalcularEsperado(caja);
			using FormCerrarCaja dialogo = new FormCerrarCaja(esperado);
			if (dialogo.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}

			_negocio.Cerrar(caja.Id, dialogo.MontoContado, dialogo.Observacion);
			decimal diferencia = dialogo.MontoContado - esperado;
			MessageBox.Show(
				$"Caja cerrada.{Environment.NewLine}Efectivo esperado: {esperado:C2}" +
				$"{Environment.NewLine}Efectivo contado: {dialogo.MontoContado:C2}" +
				$"{Environment.NewLine}Diferencia: {diferencia:C2}",
				"Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
			Cargar();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
	}

	private void btnActualizar_Click(object sender, EventArgs e) => Cargar();

	private void btnHistorial_Click(object sender, EventArgs e)
	{
		using FormHistorialCaja historial = new FormHistorialCaja();
		historial.ShowDialog(this);
	}

	private void FormatearResumen()
	{
		AjustarColumna("MedioPago", titulo: "Medio de pago");
		AjustarColumna("Cantidad", titulo: "Ventas");
		AjustarColumna("Total", titulo: "Total", formato: "C2");
	}

	private void FormatearMovimientos()
	{
		OcultarColumna("Id");
		AjustarColumna("Fecha", formato: "dd/MM/yyyy HH:mm");
		AjustarColumna("Monto", formato: "C2");
	}

	private void OcultarColumna(string nombre)
	{
		DataGridViewColumn? columna = dgvMovimientos.Columns[nombre];
		if (columna != null)
		{
			columna.Visible = false;
		}
	}

	private void AjustarColumna(string nombre, string? titulo = null, string? formato = null)
	{
		DataGridViewColumn? columna = dgvMovimientos.Columns[nombre] ?? dgvResumen.Columns[nombre];
		if (columna == null)
		{
			return;
		}
		if (titulo != null)
		{
			columna.HeaderText = titulo;
		}
		if (formato != null)
		{
			columna.DefaultCellStyle.Format = formato;
		}
	}
}
