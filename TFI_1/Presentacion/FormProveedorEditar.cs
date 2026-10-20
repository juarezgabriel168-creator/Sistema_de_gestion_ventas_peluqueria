using System;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

public partial class FormProveedorEditar : Form
{
	private readonly ProveedorNegocio _negocio = new();
	private readonly Proveedor? _proveedor;
	private readonly bool _esNuevo;

	public FormProveedorEditar() : this(null)
	{
	}

	public FormProveedorEditar(Proveedor? proveedor)
	{
		InitializeComponent();
		txtNombre.MaxLength = 150;
		txtCuit.MaxLength = 20;
		txtTelefono.MaxLength = 40;
		txtEmail.MaxLength = 120;
		txtDireccion.MaxLength = 200;
		_proveedor = proveedor;
		_esNuevo = proveedor == null;
	}

	private void FormProveedorEditar_Load(object sender, EventArgs e)
	{
		if (_esNuevo)
		{
			Text = "Nuevo proveedor";
			chkActivo.Checked = true;
			txtNombre.Focus();
		}
		else
		{
			Text = "Editar proveedor";
			txtNombre.Text = _proveedor!.Nombre;
			txtCuit.Text = _proveedor.Cuit ?? string.Empty;
			txtTelefono.Text = _proveedor.Telefono ?? string.Empty;
			txtEmail.Text = _proveedor.Email ?? string.Empty;
			txtDireccion.Text = _proveedor.Direccion ?? string.Empty;
			chkActivo.Checked = _proveedor.Activo;
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
			Proveedor proveedor = _esNuevo ? new Proveedor() : _proveedor!;
			proveedor.Nombre = txtNombre.Text.Trim();
			proveedor.Cuit = txtCuit.Text.Trim();
			proveedor.Telefono = txtTelefono.Text.Trim();
			proveedor.Email = txtEmail.Text.Trim();
			proveedor.Direccion = txtDireccion.Text.Trim();
			proveedor.Activo = chkActivo.Checked;

			_negocio.Guardar(proveedor);
			DialogResult = DialogResult.OK;
			Close();
		}
		catch (ArgumentException ex)
		{
			MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		catch (Exception ex2)
		{
			MessageBox.Show("No se pudo guardar el proveedor: " + ex2.Message, "Error",
				MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		if (Validaciones.TextoVacio(txtNombre.Text))
		{
			errProvider.SetError(txtNombre, "Ingrese el nombre del proveedor.");
			txtNombre.Focus();
			return false;
		}
		return true;
	}
}
