using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
public partial class FormCategoriaEditar : Form
{
	private readonly CategoriaNegocio _negocio = new CategoriaNegocio();
	private readonly Categoria? _categoria;
	private readonly bool _esNueva;

	public FormCategoriaEditar() : this(null)
	{
	}

	public FormCategoriaEditar(Categoria? categoria)
	{
		InitializeComponent();
		_categoria = categoria;
		_esNueva = categoria == null;
	}

	private void FormCategoriaEditar_Load(object sender, EventArgs e)
	{
		if (_esNueva)
		{
			Text = "Nueva categoría";
			chkActivo.Checked = true;
		}
		else
		{
			Text = "Editar categoría";
			txtNombre.Text = _categoria!.Nombre;
			chkActivo.Checked = _categoria!.Activo;
		}
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!Validar())
		{
			return;
		}
		try
		{
			Categoria categoria = (_esNueva ? new Categoria() : _categoria!);
			categoria.Nombre = txtNombre.Text.Trim();
			categoria.Activo = chkActivo.Checked;
			_negocio.Guardar(categoria);
			DialogResult = DialogResult.OK;
			Close();
		}
		catch (ArgumentException ex)
		{
			MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		catch (Exception ex2)
		{
			MessageBox.Show("No se pudo guardar la categoría: " + ex2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.Cancel;
		Close();
	}

	private bool Validar()
	{
		errProvider.Clear();
		if (string.IsNullOrWhiteSpace(txtNombre.Text))
		{
			errProvider.SetError(txtNombre, "Ingrese el nombre de la categoría.");
			return false;
		}
		return true;
	}
}
