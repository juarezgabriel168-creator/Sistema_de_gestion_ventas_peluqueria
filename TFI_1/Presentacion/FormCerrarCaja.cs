using System;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;

/// <summary>
/// Diálogo de cierre de caja: muestra el efectivo esperado, pide el efectivo
/// contado y la observación, y calcula la diferencia en vivo.
/// </summary>
public partial class FormCerrarCaja : Form
{
	private readonly decimal _esperado;

	public decimal MontoContado { get; private set; }

	public string? Observacion { get; private set; }

	public FormCerrarCaja(decimal esperado)
	{
		InitializeComponent();
		_esperado = esperado;
		lblEsperado.Text = $"Efectivo esperado en caja: {esperado:C2}";
		numContado.Value = Math.Max(numContado.Minimum, Math.Min(numContado.Maximum, esperado));
		ActualizarDiferencia();
	}

	private void numContado_ValueChanged(object sender, EventArgs e) => ActualizarDiferencia();

	private void ActualizarDiferencia()
	{
		decimal diferencia = numContado.Value - _esperado;
		lblDiferencia.Text = $"Diferencia: {diferencia:C2}";
		if (diferencia == 0m)
		{
			lblDiferencia.ForeColor = Color.ForestGreen;
		}
		else if (diferencia > 0m)
		{
			lblDiferencia.ForeColor = Color.DarkOrange;
		}
		else
		{
			lblDiferencia.ForeColor = Color.Firebrick;
		}
	}

	private void btnConfirmar_Click(object sender, EventArgs e)
	{
		MontoContado = numContado.Value;
		Observacion = string.IsNullOrWhiteSpace(txtObservacion.Text) ? null : txtObservacion.Text.Trim();
		DialogResult = DialogResult.OK;
		Close();
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.Cancel;
		Close();
	}
}
