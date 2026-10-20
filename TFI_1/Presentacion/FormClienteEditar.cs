using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
public partial class FormClienteEditar : Form
{
	private readonly ClienteNegocio _negocio = new ClienteNegocio();
	private readonly Cliente? _cliente;
	private readonly bool _esNuevo;

	public FormClienteEditar() : this(null)
	{
	}

	public FormClienteEditar(Cliente? cliente)
	{
		InitializeComponent();
		_cliente = cliente;
		_esNuevo = cliente == null;
	}

	private void FormClienteEditar_Load(object sender, EventArgs e)
	{
		if (_esNuevo)
		{
			Text = "Nuevo cliente";
			chkActivo.Checked = true;
			return;
		}
		Text = "Editar cliente";
		txtNombre.Text = _cliente!.Nombre;
		txtApellido.Text = _cliente!.Apellido;
		txtDni.Text = _cliente!.Dni;
		txtTelefono.Text = _cliente!.Telefono;
		chkActivo.Checked = _cliente!.Activo;
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!Validar())
		{
			return;
		}
		try
		{
			Cliente cliente = (_esNuevo ? new Cliente() : _cliente!);
			cliente.Nombre = txtNombre.Text.Trim();
			cliente.Apellido = txtApellido.Text.Trim();
			cliente.Dni = txtDni.Text.Trim();
			cliente.Telefono = txtTelefono.Text.Trim();
			cliente.Activo = chkActivo.Checked;
			_negocio.Guardar(cliente);
			DialogResult = DialogResult.OK;
			Close();
		}
		catch (ArgumentException ex)
		{
			MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		catch (Exception ex2)
		{
			MessageBox.Show("No se pudo guardar el cliente: " + ex2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		bool result = true;
		if (string.IsNullOrWhiteSpace(txtNombre.Text))
		{
			errProvider.SetError(txtNombre, "Ingrese el nombre.");
			result = false;
		}
		if (string.IsNullOrWhiteSpace(txtApellido.Text))
		{
			errProvider.SetError(txtApellido, "Ingrese el apellido.");
			result = false;
		}
		string text = txtDni.Text.Trim();
		if (text.Length != 8 || !text.All(char.IsDigit))
		{
			errProvider.SetError(txtDni, "El DNI debe tener 8 dígitos numéricos.");
			result = false;
		}
		return result;
	}
}
