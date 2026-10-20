using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
public partial class FormClientes : Form
{
	private readonly ClienteNegocio _negocio = new ClienteNegocio();

	public FormClientes()
	{
		InitializeComponent();
	}

	private void FormClientes_Load(object sender, EventArgs e)
	{
		Cargar();
	}

	private void Cargar()
	{
		try
		{
			var lista = _negocio.Listar(txtBuscar.Text.Trim());
			dgvClientes.DataSource = chkSoloActivos.Checked ? lista.Where(x => x.Activo).ToList() : lista;
			ActualizarBotonEstado();
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo cargar la lista de clientes", ex);
		}
	}

	private Cliente? Seleccionado()
	{
		return dgvClientes.CurrentRow?.DataBoundItem as Cliente;
	}

	private void btnBuscar_Click(object sender, EventArgs e)
	{
		Cargar();
	}

	private void btnRefrescar_Click(object sender, EventArgs e)
	{
		txtBuscar.Clear();
		Cargar();
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		using FormClienteEditar formClienteEditar = new FormClienteEditar(null);
		if (formClienteEditar.ShowDialog(this) == DialogResult.OK)
		{
			Cargar();
		}
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		Cliente? cliente = Seleccionado();
		if (cliente == null)
		{
			MessageBox.Show("Seleccione un cliente de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		using FormClienteEditar formClienteEditar = new FormClienteEditar(cliente);
		if (formClienteEditar.ShowDialog(this) == DialogResult.OK)
		{
			Cargar();
		}
	}

	// Al cambiar el tilde "Solo Activos" se recarga el listado.
	private void chkSoloActivos_CheckedChanged(object sender, EventArgs e)
	{
		Cargar();
	}

	private void dgvClientes_SelectionChanged(object sender, EventArgs e)
	{
		ActualizarBotonEstado();
	}

	// El mismo botón desactiva o reactiva, según el estado del cliente seleccionado.
	private void ActualizarBotonEstado()
	{
		Cliente? cliente = Seleccionado();
		btnDesactivar.Text = (cliente != null && !cliente.Activo) ? "Reactivar" : "Desactivar";
	}

	private void btnDesactivar_Click(object sender, EventArgs e)
	{
		Cliente? cliente = Seleccionado();
		if (cliente == null)
		{
			MessageBox.Show("Seleccione un cliente de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}

		bool reactivar = !cliente.Activo;
		string accion = reactivar ? "reactivar" : "desactivar";
		DialogResult dialogResult = MessageBox.Show(
			"¿Desea " + accion + " a " + cliente.NombreCompleto + "?",
			"Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dialogResult != DialogResult.Yes)
		{
			return;
		}

		try
		{
			if (reactivar)
			{
				_negocio.Reactivar(cliente.Id);
			}
			else
			{
				_negocio.Desactivar(cliente.Id);
			}
			Cargar();
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo cambiar el estado", ex);
		}
	}
}
