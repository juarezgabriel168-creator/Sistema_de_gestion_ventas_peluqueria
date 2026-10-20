using System;
using System.Linq;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

/// <summary>
/// Listado de proveedores: permite buscar, filtrar los activos, y dar de
/// alta, editar, desactivar o reactivar un proveedor.
/// </summary>
public partial class FormProveedores : Form
{
	private readonly ProveedorNegocio _negocio = new();

	public FormProveedores()
	{
		InitializeComponent();
	}

	private void FormProveedores_Load(object sender, EventArgs e)
	{
		Cargar();
	}

	private void Cargar(string? filtro = null)
	{
		try
		{
			var lista = _negocio.Listar(filtro);
			dgvProveedores.DataSource = chkSoloActivos.Checked
				? lista.Where(p => p.Activo).ToList()
				: lista;
			ActualizarBotonEstado();
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo cargar la lista de proveedores", ex);
		}
	}

	private Proveedor? Seleccionado()
	{
		return dgvProveedores.CurrentRow?.DataBoundItem as Proveedor;
	}

	private void btnBuscar_Click(object sender, EventArgs e)
	{
		Cargar(txtBuscar.Text.Trim());
	}

	private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			e.SuppressKeyPress = true;
			Cargar(txtBuscar.Text.Trim());
		}
	}

	private void btnRefrescar_Click(object sender, EventArgs e)
	{
		txtBuscar.Clear();
		Cargar();
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		using FormProveedorEditar formulario = new FormProveedorEditar(null);
		if (formulario.ShowDialog(this) == DialogResult.OK)
		{
			Cargar(txtBuscar.Text.Trim());
		}
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		Proveedor? proveedor = Seleccionado();
		if (proveedor == null)
		{
			MessageBox.Show("Seleccione un proveedor de la lista.", "Aviso",
				MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}

		using FormProveedorEditar formulario = new FormProveedorEditar(proveedor);
		if (formulario.ShowDialog(this) == DialogResult.OK)
		{
			Cargar(txtBuscar.Text.Trim());
		}
	}

	private void dgvProveedores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			btnEditar_Click(sender, EventArgs.Empty);
		}
	}

	private void chkSoloActivos_CheckedChanged(object sender, EventArgs e)
	{
		Cargar(txtBuscar.Text.Trim());
	}

	private void dgvProveedores_SelectionChanged(object sender, EventArgs e)
	{
		ActualizarBotonEstado();
	}

	// El mismo botón desactiva o reactiva, según el estado del proveedor seleccionado.
	private void ActualizarBotonEstado()
	{
		Proveedor? proveedor = Seleccionado();
		btnDesactivar.Text = (proveedor != null && !proveedor.Activo) ? "Reactivar" : "Desactivar";
	}

	private void btnDesactivar_Click(object sender, EventArgs e)
	{
		Proveedor? proveedor = Seleccionado();
		if (proveedor == null)
		{
			MessageBox.Show("Seleccione un proveedor de la lista.", "Aviso",
				MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}

		bool reactivar = !proveedor.Activo;
		string accion = reactivar ? "reactivar" : "desactivar";
		DialogResult respuesta = MessageBox.Show(
			"¿Desea " + accion + " el proveedor '" + proveedor.Nombre + "'?",
			"Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (respuesta != DialogResult.Yes)
		{
			return;
		}

		try
		{
			if (reactivar)
			{
				_negocio.Reactivar(proveedor.Id);
			}
			else
			{
				_negocio.Desactivar(proveedor.Id);
			}
			Cargar(txtBuscar.Text.Trim());
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo cambiar el estado", ex);
		}
	}

	private void btnCerrar_Click(object sender, EventArgs e)
	{
		Close();
	}
}
