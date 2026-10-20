using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
public partial class FormEmpleadoEditar : Form
{
	private readonly EmpleadoNegocio _negocio = new EmpleadoNegocio();
	private readonly Empleado? _empleado;
	private readonly bool _esNuevo;

	public FormEmpleadoEditar() : this(null)
	{
	}

	public FormEmpleadoEditar(Empleado? empleado)
	{
		InitializeComponent();
		_empleado = empleado;
		_esNuevo = empleado == null;
	}

	private void FormEmpleadoEditar_Load(object sender, EventArgs e)
	{
		cboModalidad.Items.Clear();
		cboModalidad.Items.AddRange("Turnos", "Orden de llegada");
		if (_esNuevo)
		{
			Text = "Nuevo empleado";
			cboModalidad.SelectedIndex = 0;
			chkActivo.Checked = true;
		}
		else
		{
			Text = "Editar empleado";
			txtNombre.Text = _empleado!.Nombre;
			txtApellido.Text = _empleado!.Apellido;
			cboModalidad.SelectedIndex = ((_empleado!.Modalidad == Modalidad.OrdenLlegada) ? 1 : 0);
			chkActivo.Checked = _empleado!.Activo;
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
			Empleado empleado = (_esNuevo ? new Empleado() : _empleado!);
			empleado.Nombre = txtNombre.Text.Trim();
			empleado.Apellido = txtApellido.Text.Trim();
			empleado.Modalidad = ((cboModalidad.SelectedIndex == 1) ? Modalidad.OrdenLlegada : Modalidad.Turnos);
			empleado.Activo = chkActivo.Checked;
			_negocio.Guardar(empleado);
			DialogResult = DialogResult.OK;
			Close();
		}
		catch (ArgumentException ex)
		{
			MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		catch (Exception ex2)
		{
			MessageBox.Show("No se pudo guardar el empleado: " + ex2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		if (Validaciones.TextoVacio(txtNombre.Text))
		{
			errProvider.SetError(txtNombre, "Ingrese el nombre.");
			result = false;
		}
		if (Validaciones.TextoVacio(txtApellido.Text))
		{
			errProvider.SetError(txtApellido, "Ingrese el apellido.");
			result = false;
		}
		if (cboModalidad.SelectedIndex < 0)
		{
			errProvider.SetError(cboModalidad, "Seleccione una modalidad.");
			result = false;
		}
		return result;
	}
}
