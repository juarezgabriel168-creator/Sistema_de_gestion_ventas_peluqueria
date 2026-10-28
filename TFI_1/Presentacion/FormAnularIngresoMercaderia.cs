using System;
using System.Windows.Forms;

namespace TFI_1.Presentacion;

/// <summary>
/// Pide el motivo para anular un ingreso de mercadería y avisa qué efectos
/// tiene la operación sobre el stock.
/// </summary>
public partial class FormAnularIngresoMercaderia : Form
{
	public FormAnularIngresoMercaderia(int ingresoId, string proveedor, decimal total)
	{
		InitializeComponent();
		Text = $"Anular ingreso #{ingresoId}";
		lblResumen.Text = $"Ingreso #{ingresoId} — {proveedor} — Total: {total:C2}";
	}

	public string Motivo => txtMotivo.Text.Trim();

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		if (Validaciones.TextoVacio(txtMotivo.Text))
		{
			errProvider.SetError(txtMotivo, "Indique el motivo de la anulación.");
			txtMotivo.Focus();
			return;
		}

		DialogResult = DialogResult.OK;
		Close();
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.Cancel;
		Close();
	}
}
