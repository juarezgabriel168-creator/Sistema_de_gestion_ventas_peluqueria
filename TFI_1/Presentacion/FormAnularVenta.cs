using System;
using System.Windows.Forms;

namespace TFI_1.Presentacion;

/// <summary>
/// Pide el motivo para anular una venta y avisa qué efectos tiene la operación.
/// </summary>
public partial class FormAnularVenta : Form
{
	public FormAnularVenta(int ventaId, string cliente, decimal total, bool cuentaCorriente)
	{
		InitializeComponent();
		Text = $"Anular venta #{ventaId}";
		lblResumen.Text = $"Venta #{ventaId} — {cliente} — Total: {total:C2}";
		if (!cuentaCorriente)
		{
			lblAviso.Text = "Se devolverá el stock de los productos. La venta no se borra: " +
				"queda registrada como anulada.";
		}
	}

	public string Motivo => txtMotivo.Text.Trim();

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtMotivo.Text))
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
