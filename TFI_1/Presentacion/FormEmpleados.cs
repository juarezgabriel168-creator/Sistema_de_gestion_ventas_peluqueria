using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

public partial class FormEmpleados : Form
{
	private readonly EmpleadoNegocio _negocio = new EmpleadoNegocio();

	public FormEmpleados()
	{
		InitializeComponent();
	}

	private void FormEmpleados_Load(object sender, EventArgs e)
	{
		Cargar();
	}

	private void Cargar()
	{
		try
		{
			var lista = _negocio.Listar(txtBuscar.Text.Trim());
			dgvEmpleados.DataSource = chkSoloActivos.Checked ? lista.Where(x => x.Activo).ToList() : lista;
			ActualizarBotonEstado();
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo cargar la lista de empleados", ex);
		}
	}

	private Empleado? Seleccionado()
	{
		return dgvEmpleados.CurrentRow?.DataBoundItem as Empleado;
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
		using FormEmpleadoEditar formEmpleadoEditar = new FormEmpleadoEditar(null);
		if (formEmpleadoEditar.ShowDialog(this) == DialogResult.OK)
		{
			Cargar();
		}
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		Empleado? empleado = Seleccionado();
		if (empleado == null)
		{
			MessageBox.Show("Seleccione un empleado de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		using FormEmpleadoEditar formEmpleadoEditar = new FormEmpleadoEditar(empleado);
		if (formEmpleadoEditar.ShowDialog(this) == DialogResult.OK)
		{
			Cargar();
		}
	}

	// Al cambiar el tilde "Solo Activos" se recarga el listado.
	private void chkSoloActivos_CheckedChanged(object sender, EventArgs e)
	{
		Cargar();
	}

	private void dgvEmpleados_SelectionChanged(object sender, EventArgs e)
	{
		ActualizarBotonEstado();
	}

	// El mismo botón desactiva o reactiva, según el estado del empleado seleccionado.
	private void ActualizarBotonEstado()
	{
		Empleado? empleado = Seleccionado();
		btnDesactivar.Text = (empleado != null && !empleado.Activo) ? "Reactivar" : "Desactivar";
	}

	private void btnDesactivar_Click(object sender, EventArgs e)
	{
		Empleado? empleado = Seleccionado();
		if (empleado == null)
		{
			MessageBox.Show("Seleccione un empleado de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}

		bool reactivar = !empleado.Activo;
		string accion = reactivar ? "reactivar" : "desactivar";
		DialogResult dialogResult = MessageBox.Show(
			$"¿Desea {accion} a {empleado.Nombre} {empleado.Apellido}?",
			"Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dialogResult != DialogResult.Yes)
		{
			return;
		}

		try
		{
			if (reactivar)
			{
				_negocio.Reactivar(empleado.Id);
			}
			else
			{
				_negocio.Desactivar(empleado.Id);
			}
			Cargar();
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo cambiar el estado", ex);
		}
	}
}
