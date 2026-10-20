using System;
using System.Linq;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
public partial class FormAtencionEditar : Form
{
	private static readonly EstadoAtencion[] Estados = { EstadoAtencion.Pendiente, EstadoAtencion.EnAtencion, EstadoAtencion.Finalizada, EstadoAtencion.Cancelada };

	private readonly AtencionNegocio _atencionNegocio = new AtencionNegocio();
	private readonly ClienteNegocio _clienteNegocio = new ClienteNegocio();
	private readonly EmpleadoNegocio _empleadoNegocio = new EmpleadoNegocio();
	private readonly ItemNegocio _itemNegocio = new ItemNegocio();

	private readonly Atencion? _atencion;
	private readonly bool _esNueva;

	public FormAtencionEditar() : this(null)
	{
	}

	public FormAtencionEditar(Atencion? atencion)
	{
		InitializeComponent();
		_atencion = atencion;
		_esNueva = atencion == null;
	}

	private void FormAtencionEditar_Load(object sender, EventArgs e)
	{
		try
		{
			cboCliente.DataSource = _clienteNegocio.Listar().Where((Cliente c) => c.Activo).ToList();
			cboCliente.DisplayMember = "NombreCompleto";
			cboCliente.ValueMember = "Id";

			cboEmpleado.DataSource = _empleadoNegocio.Listar().Where((Empleado em) => em.Activo).ToList();
			cboEmpleado.DisplayMember = "NombreCompleto";
			cboEmpleado.ValueMember = "Id";

			cboServicio.DataSource = _itemNegocio.Listar(tipo: TipoItem.Servicio).Where((Item i) => i.Activo).ToList();
			cboServicio.DisplayMember = "Nombre";
			cboServicio.ValueMember = "Id";

			cboEstado.Items.Clear();
			cboEstado.Items.AddRange(new object[] { "Pendiente", "En atención", "Finalizada", "Cancelada" });

			if (_esNueva)
			{
				Text = "Nueva atención";
				dtpFechaHora.Value = DateTime.Now;
				cboEstado.SelectedIndex = 0;
			}
			else
			{
				Text = "Editar atención";
				if (_atencion.Cliente != null)
				{
					cboCliente.SelectedValue = _atencion.Cliente.Id;
				}
				if (_atencion.Empleado != null)
				{
					cboEmpleado.SelectedValue = _atencion.Empleado.Id;
				}
				if (_atencion.Servicio != null)
				{
					cboServicio.SelectedValue = _atencion.Servicio.Id;
				}
				dtpFechaHora.Value = _atencion.FechaHora;
				cboEstado.SelectedIndex = Array.IndexOf(Estados, _atencion.Estado);
				if (cboEstado.SelectedIndex < 0)
				{
					cboEstado.SelectedIndex = 0;
				}
			}

			ActualizarTipo();
		}
		catch (Exception ex)
		{
			MessageBox.Show("No se pudieron cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}

	private void cboEmpleado_SelectedIndexChanged(object sender, EventArgs e)
	{
		ActualizarTipo();
	}

	private void ActualizarTipo()
	{
		if (cboEmpleado.SelectedItem is not Empleado empleado)
		{
			lblTipo.Text = "Tipo de atención: -";
			dtpFechaHora.Enabled = false;
			return;
		}
		if (empleado.AtiendeConTurno())
		{
			lblTipo.Text = "Tipo de atención: Turno (se usa la fecha y hora indicadas)";
			dtpFechaHora.Enabled = true;
		}
		else
		{
			lblTipo.Text = "Tipo de atención: Orden de llegada (se registra con la hora actual)";
			dtpFechaHora.Enabled = false;
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
			Atencion atencion = _esNueva ? new Atencion() : _atencion;
			atencion.Cliente = (Cliente)cboCliente.SelectedItem;
			atencion.Empleado = (Empleado)cboEmpleado.SelectedItem;
			atencion.Servicio = (Item)cboServicio.SelectedItem;
			atencion.FechaHora = dtpFechaHora.Value;
			atencion.Estado = Estados[cboEstado.SelectedIndex];

			_atencionNegocio.Guardar(atencion);
			DialogResult = DialogResult.OK;
			Close();
		}
		catch (ArgumentException ex)
		{
			MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		catch (Exception ex)
		{
			MessageBox.Show("No se pudo guardar la atención: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		bool valido = true;
		if (cboCliente.SelectedItem == null)
		{
			errProvider.SetError(cboCliente, "Seleccione un cliente.");
			valido = false;
		}
		if (cboEmpleado.SelectedItem == null)
		{
			errProvider.SetError(cboEmpleado, "Seleccione un peluquero.");
			valido = false;
		}
		if (cboServicio.SelectedItem == null)
		{
			errProvider.SetError(cboServicio, "Seleccione un servicio.");
			valido = false;
		}
		return valido;
	}
}
