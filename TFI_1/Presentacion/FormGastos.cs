using System;
using System.Linq;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

public partial class FormGastos : Form
{
	private readonly GastoNegocio _negocio = new();
	private bool _cargando;
	private Gasto? _editando;

	public FormGastos()
	{
		InitializeComponent();
		txtConcepto.MaxLength = 150;
		txtCategoria.MaxLength = 100;
		txtObservacion.MaxLength = 255;
	}

	private void FormGastos_Load(object sender, EventArgs e)
	{
		dtpFecha.Value = DateTime.Today;
		Cargar();
	}

	private void Cargar()
	{
		try
		{
			_cargando = true;
			var gastos = _negocio.Listar(dtpDesde.Value.Date, dtpHasta.Value.Date, txtBuscar.Text.Trim());
			dgvGastos.DataSource = gastos;
			FormatearGrid();
			lblTotal.Text = $"Total del período: {gastos.Sum(g => g.Monto):C2}  ·  {gastos.Count} gasto(s)";
			Limpiar();
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudieron cargar los gastos", ex);
		}
		finally
		{
			_cargando = false;
		}
	}

	private void FormatearGrid()
	{
		if (dgvGastos.Columns.Contains("Id"))
			dgvGastos.Columns["Id"]!.Visible = false;
		if (dgvGastos.Columns.Contains("IdUsuario"))
			dgvGastos.Columns["IdUsuario"]!.Visible = false;
		if (dgvGastos.Columns.Contains("FechaAlta"))
			dgvGastos.Columns["FechaAlta"]!.Visible = false;
		if (dgvGastos.Columns.Contains("Fecha"))
			dgvGastos.Columns["Fecha"]!.DefaultCellStyle.Format = "dd/MM/yyyy";
		if (dgvGastos.Columns.Contains("Monto"))
			dgvGastos.Columns["Monto"]!.DefaultCellStyle.Format = "C2";

		DataGridViewColumn? columnaUsuario = dgvGastos.Columns.Contains("NombreUsuario")
			? dgvGastos.Columns["NombreUsuario"]
			: null;
		if (columnaUsuario != null)
			columnaUsuario.HeaderText = "Usuario";
	}

	private void Limpiar()
	{
		_editando = null;
		dtpFecha.Value = DateTime.Today;
		txtConcepto.Clear();
		// Inicializar numMonto de forma segura respetando el Minimum del control
		numMonto.Value = numMonto.Minimum;
		txtCategoria.Clear();
		txtObservacion.Clear();
		btnGuardar.Text = "Guardar";
		txtConcepto.Focus();
	}

	private Gasto? LeerFormulario()
	{
		if (Validaciones.TextoVacio(txtConcepto.Text))
		{
			MessageBox.Show("Ingrese un concepto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return null;
		}
		if (!Validaciones.EsImportePositivo(numMonto.Value))
		{
			MessageBox.Show("El monto debe ser mayor a cero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return null;
		}
		if (!Validaciones.FechaNoFutura(dtpFecha.Value))
		{
			MessageBox.Show("La fecha no puede ser futura.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return null;
		}

		return new Gasto
		{
			Id = _editando?.Id ?? 0,
			Fecha = dtpFecha.Value.Date,
			Concepto = txtConcepto.Text.Trim(),
			Monto = numMonto.Value,
			Categoria = string.IsNullOrWhiteSpace(txtCategoria.Text) ? null : txtCategoria.Text.Trim(),
			Observacion = string.IsNullOrWhiteSpace(txtObservacion.Text) ? null : txtObservacion.Text.Trim(),
			IdUsuario = Sesion.UsuarioActual?.Id ?? 0
		};
	}

	private void btnNuevo_Click(object sender, EventArgs e) => Limpiar();

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		var gasto = LeerFormulario();
		if (gasto == null)
			return;
		try
		{
			if (_editando == null)
				_negocio.Insertar(gasto);
			else
				_negocio.Actualizar(gasto);
			Cargar();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void dgvGastos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex < 0)
			return;
		var fila = dgvGastos.Rows[e.RowIndex];
		var id = Convert.ToInt32(fila.Cells["Id"].Value);
		var gasto = _negocio.ObtenerPorId(id);
		if (gasto == null)
			return;
		_editando = gasto;
		dtpFecha.Value = gasto.Fecha;
		txtConcepto.Text = gasto.Concepto;
		numMonto.Value = gasto.Monto;
		txtCategoria.Text = gasto.Categoria ?? string.Empty;
		txtObservacion.Text = gasto.Observacion ?? string.Empty;
		btnGuardar.Text = "Actualizar";
	}

	private void btnRefrescar_Click(object sender, EventArgs e) => Cargar();

	private void btnBuscar_Click(object sender, EventArgs e) => Cargar();

	private void dtpDesde_ValueChanged(object sender, EventArgs e)
	{
		if (!_cargando)
			Cargar();
	}

	private void dtpHasta_ValueChanged(object sender, EventArgs e)
	{
		if (!_cargando)
			Cargar();
	}

	private void txtBuscar_TextChanged(object sender, EventArgs e)
	{
		if (!_cargando)
			Cargar();
	}
}
