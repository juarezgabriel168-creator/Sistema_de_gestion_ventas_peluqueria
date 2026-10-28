using System;
using System.Windows.Forms;

namespace TFI_1.Presentacion;

/// <summary>
/// Confirmación común para las eliminaciones físicas: primero pregunta si se
/// desea continuar y después exige la contraseña del usuario actual.
/// </summary>
public static class ConfirmacionEliminacion
{
	public static bool Confirmar(IWin32Window owner, string detalle, string advertencia)
	{
		string texto =
			$"¿Eliminar definitivamente {detalle}?" + Environment.NewLine + Environment.NewLine +
			advertencia;

		DialogResult respuesta = MessageBox.Show(
			owner, texto, "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
		if (respuesta != DialogResult.Yes)
		{
			return false;
		}

		using FormConfirmarContrasena confirmar = new(detalle);
		return confirmar.ShowDialog(owner) == DialogResult.OK;
	}
}
