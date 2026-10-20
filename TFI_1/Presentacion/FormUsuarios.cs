using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
public partial class FormUsuarios : Form
{
	private readonly UsuarioNegocio _negocio = new UsuarioNegocio();

	public FormUsuarios()
	{
		InitializeComponent();
	}

	private void FormUsuarios_Load(object sender, EventArgs e)
	{
		Cargar();
	}

	private void Cargar(string? filtro = null)
	{
		try
		{
			var lista = _negocio.Listar(filtro);
			dgvUsuarios.DataSource = chkSoloActivos.Checked ? lista.Where(x => x.Activo).ToList() : lista;
			ActualizarBotonEstado();
		}
		catch (Exception ex)
		{
			MessageBox.Show("No se pudo cargar la lista de usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private Usuario? Seleccionado()
	{
		return dgvUsuarios.CurrentRow?.DataBoundItem as Usuario;
	}

	private void btnBuscar_Click(object sender, EventArgs e)
	{
		Cargar(txtBuscar.Text.Trim());
	}

	private void btnRefrescar_Click(object sender, EventArgs e)
	{
		txtBuscar.Clear();
		Cargar();
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		using FormUsuarioEditar formUsuarioEditar = new FormUsuarioEditar(null);
		if (formUsuarioEditar.ShowDialog(this) == DialogResult.OK)
		{
			Cargar(txtBuscar.Text.Trim());
		}
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		Usuario usuario = Seleccionado();
		if (usuario == null)
		{
			MessageBox.Show("Seleccione un usuario de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		using FormUsuarioEditar formUsuarioEditar = new FormUsuarioEditar(usuario);
		if (formUsuarioEditar.ShowDialog(this) == DialogResult.OK)
		{
			Cargar(txtBuscar.Text.Trim());
		}
	}

	// Al cambiar el tilde "Solo Activos" se recarga el listado.
	private void chkSoloActivos_CheckedChanged(object sender, EventArgs e)
	{
		Cargar(txtBuscar.Text.Trim());
	}

	private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
	{
		ActualizarBotonEstado();
	}

	// El mismo botón desactiva o reactiva, según el estado del usuario seleccionado.
	private void ActualizarBotonEstado()
	{
		Usuario? usuario = Seleccionado();
		btnDesactivar.Text = (usuario != null && !usuario.Activo) ? "Reactivar" : "Desactivar";
	}

	private void btnDesactivar_Click(object sender, EventArgs e)
	{
		Usuario usuario = Seleccionado();
		if (usuario == null)
		{
			MessageBox.Show("Seleccione un usuario de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}

		bool reactivar = !usuario.Activo;
		string accion = reactivar ? "reactivar" : "desactivar";
		DialogResult dialogResult = MessageBox.Show(
			"¿Desea " + accion + " al usuario '" + usuario.NombreUsuario + "'?",
			"Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dialogResult != DialogResult.Yes)
		{
			return;
		}

		try
		{
			if (reactivar)
			{
				_negocio.Reactivar(usuario.Id);
			}
			else
			{
				_negocio.Desactivar(usuario.Id);
			}
			Cargar(txtBuscar.Text.Trim());
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}
}
