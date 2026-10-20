using System;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

public partial class FormAtenciones : Form
{
	private static readonly string?[] EstadosFiltro = { null, "pendiente", "en_atencion", "finalizada", "cancelada" };

	private readonly AtencionNegocio _negocio = new AtencionNegocio();
	private bool _cargandoEstado;

	public FormAtenciones()
	{
		InitializeComponent();
	}

	private void FormAtenciones_Load(object sender, EventArgs e)
	{
		_cargandoEstado = true;
		cboEstado.Items.Clear();
		cboEstado.Items.AddRange(new object[] { "Todos", "Pendiente", "En atención", "Finalizada", "Cancelada" });
		cboEstado.SelectedIndex = 0;
		_cargandoEstado = false;
		Cargar();
	}

	private void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (!_cargandoEstado)
		{
			Cargar();
		}
	}

	private void Cargar()
	{
		try
		{
			string filtro = txtBuscar.Text.Trim();
			string? estado = (cboEstado.SelectedIndex >= 0) ? EstadosFiltro[cboEstado.SelectedIndex] : null;
			dgvAtenciones.DataSource = _negocio.Listar(filtro, estado);
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudieron cargar las atenciones", ex);
		}
	}

	private Atencion? Seleccionada()
	{
		return dgvAtenciones.CurrentRow?.DataBoundItem as Atencion;
	}

	private void btnBuscar_Click(object sender, EventArgs e)
	{
		Cargar();
	}

	private void btnRefrescar_Click(object sender, EventArgs e)
	{
		txtBuscar.Clear();
		_cargandoEstado = true;
		cboEstado.SelectedIndex = 0;
		_cargandoEstado = false;
		Cargar();
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		using FormAtencionEditar formulario = new FormAtencionEditar(null);
		if (formulario.ShowDialog(this) == DialogResult.OK)
		{
			Cargar();
		}
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		Atencion? atencion = Seleccionada();
		if (atencion == null)
		{
			MessageBox.Show("Seleccione una atención de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		using FormAtencionEditar formulario = new FormAtencionEditar(atencion);
		if (formulario.ShowDialog(this) == DialogResult.OK)
		{
			Cargar();
		}
	}

	private void btnCancelarAtencion_Click(object sender, EventArgs e)
	{
		Atencion? atencion = Seleccionada();
		if (atencion == null)
		{
			MessageBox.Show("Seleccione una atención de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		if (atencion.Estado == EstadoAtencion.Cancelada)
		{
			MessageBox.Show("La atención seleccionada ya está cancelada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		DialogResult respuesta = MessageBox.Show("¿Desea cancelar la atención seleccionada?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (respuesta != DialogResult.Yes)
		{
			return;
		}
		try
		{
			_negocio.Cancelar(atencion);
			Cargar();
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo cancelar la atención", ex);
		}
	}
}
