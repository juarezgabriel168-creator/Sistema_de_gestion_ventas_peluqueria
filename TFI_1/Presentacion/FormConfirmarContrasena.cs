using System;
using System.Windows.Forms;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

/// <summary>
/// Pide la contraseña del usuario actual antes de ejecutar una operación
/// irreversible, como eliminar un registro.
/// </summary>
public partial class FormConfirmarContrasena : Form
{
	private readonly UsuarioNegocio _negocio = new();

	public FormConfirmarContrasena(string detalle)
	{
		InitializeComponent();
		lblMensaje.Text =
			$"Va a eliminar {detalle}." + Environment.NewLine +
			"Ingrese la contraseña del usuario actual para confirmar:";
	}

	private void FormConfirmarContrasena_Shown(object? sender, EventArgs e)
	{
		txtContrasena.Focus();
	}

	private void chkMostrar_CheckedChanged(object? sender, EventArgs e)
	{
		txtContrasena.UseSystemPasswordChar = !chkMostrar.Checked;
	}

	private void btnAceptar_Click(object? sender, EventArgs e)
	{
		errProvider.SetError(txtContrasena, string.Empty);

		if (Validaciones.TextoVacio(txtContrasena.Text))
		{
			errProvider.SetError(txtContrasena, "Ingrese la contraseña.");
			txtContrasena.Focus();
			return;
		}

		try
		{
			if (!_negocio.ConfirmarContrasenaActual(txtContrasena.Text))
			{
				errProvider.SetError(txtContrasena, "Contraseña incorrecta.");
				txtContrasena.SelectAll();
				txtContrasena.Focus();
				return;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, ex.Message, "No se pudo confirmar",
				MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		DialogResult = DialogResult.OK;
		Close();
	}

	private void btnCancelar_Click(object? sender, EventArgs e)
	{
		DialogResult = DialogResult.Cancel;
		Close();
	}
}
