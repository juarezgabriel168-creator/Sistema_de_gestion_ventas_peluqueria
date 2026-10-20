using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
public partial class FormCategorias : Form
{
	private readonly CategoriaNegocio _negocio = new CategoriaNegocio();

	public FormCategorias()
	{
		InitializeComponent();
	}

	private void FormCategorias_Load(object sender, EventArgs e)
	{
		Cargar();
	}

	private void Cargar(string? filtro = null)
	{
		try
		{
			var lista = _negocio.Listar(filtro);
			dgvCategorias.DataSource = chkSoloActivos.Checked ? lista.Where(x => x.Activo).ToList() : lista;
			ActualizarBotonEstado();
		}
		catch (Exception ex)
		{
			MessageBox.Show("No se pudo cargar la lista de categorías: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private Categoria? Seleccionada()
	{
		return dgvCategorias.CurrentRow?.DataBoundItem as Categoria;
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
		using FormCategoriaEditar formCategoriaEditar = new FormCategoriaEditar(null);
		if (formCategoriaEditar.ShowDialog(this) == DialogResult.OK)
		{
			Cargar(txtBuscar.Text.Trim());
		}
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		Categoria categoria = Seleccionada();
		if (categoria == null)
		{
			MessageBox.Show("Seleccione una categoría de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		using FormCategoriaEditar formCategoriaEditar = new FormCategoriaEditar(categoria);
		if (formCategoriaEditar.ShowDialog(this) == DialogResult.OK)
		{
			Cargar(txtBuscar.Text.Trim());
		}
	}

	// Al cambiar el tilde "Solo Activos" se recarga el listado.
	private void chkSoloActivos_CheckedChanged(object sender, EventArgs e)
	{
		Cargar(txtBuscar.Text.Trim());
	}

	private void dgvCategorias_SelectionChanged(object sender, EventArgs e)
	{
		ActualizarBotonEstado();
	}

	// El mismo botón desactiva o reactiva, según el estado de la categoría seleccionada.
	private void ActualizarBotonEstado()
	{
		Categoria? categoria = Seleccionada();
		btnDesactivar.Text = (categoria != null && !categoria.Activo) ? "Reactivar" : "Desactivar";
	}

	private void btnDesactivar_Click(object sender, EventArgs e)
	{
		Categoria categoria = Seleccionada();
		if (categoria == null)
		{
			MessageBox.Show("Seleccione una categoría de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}

		bool reactivar = !categoria.Activo;
		string accion = reactivar ? "reactivar" : "desactivar";
		DialogResult dialogResult = MessageBox.Show(
			"¿Desea " + accion + " la categoría '" + categoria.Nombre + "'?",
			"Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dialogResult != DialogResult.Yes)
		{
			return;
		}

		try
		{
			if (reactivar)
			{
				_negocio.Reactivar(categoria.Id);
			}
			else
			{
				_negocio.Desactivar(categoria.Id);
			}
			Cargar(txtBuscar.Text.Trim());
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}
}
