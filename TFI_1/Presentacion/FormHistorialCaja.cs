using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

/// <summary>
/// Historial de turnos de caja: apertura, cierre, montos y diferencia.
/// </summary>
public partial class FormHistorialCaja : Form
{
	private readonly CajaNegocio _negocio = new();

	public FormHistorialCaja()
	{
		InitializeComponent();
	}

	private void FormHistorialCaja_Load(object sender, EventArgs e)
	{
		dtpDesde.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
		dtpHasta.Value = DateTime.Today;
		Cargar();
	}

	private void Cargar()
	{
		try
		{
			DataTable tabla = _negocio.Listar(dtpDesde.Value.Date, dtpHasta.Value.Date);
			dgvCajas.DataSource = tabla;
			FormatearGrid();
			ColorearDiferencias();
			lblTotal.Text = $"{tabla.Rows.Count} turno(s) de caja";
		}
		catch (Exception ex)
		{
			MessageBox.Show("No se pudo cargar el historial: " + ex.Message, "Caja",
				MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void FormatearGrid()
	{
		OcultarColumna("Id");
		AjustarColumna("Apertura", formato: "dd/MM/yyyy HH:mm");
		AjustarColumna("Cierre", formato: "dd/MM/yyyy HH:mm");
		AjustarColumna("UsuarioApertura", titulo: "Abrió");
		AjustarColumna("UsuarioCierre", titulo: "Cerró");
		AjustarColumna("Inicial", formato: "C2");
		AjustarColumna("Esperado", formato: "C2");
		AjustarColumna("Contado", formato: "C2");
		AjustarColumna("Diferencia", formato: "C2");
		AjustarColumna("Observacion", titulo: "Observación");
	}

	private void ColorearDiferencias()
	{
		foreach (DataGridViewRow fila in dgvCajas.Rows)
		{
			if (fila.DataBoundItem is not DataRowView vista || vista["Diferencia"] == DBNull.Value)
			{
				continue;
			}

			if (Convert.ToDecimal(vista["Diferencia"]) != 0m)
			{
				fila.DefaultCellStyle.ForeColor = Color.Firebrick;
			}
		}
	}

	private void OcultarColumna(string nombre)
	{
		DataGridViewColumn? columna = dgvCajas.Columns[nombre];
		if (columna != null)
		{
			columna.Visible = false;
		}
	}

	private void AjustarColumna(string nombre, string? titulo = null, string? formato = null)
	{
		DataGridViewColumn? columna = dgvCajas.Columns[nombre];
		if (columna == null)
		{
			return;
		}
		if (titulo != null)
		{
			columna.HeaderText = titulo;
		}
		if (formato != null)
		{
			columna.DefaultCellStyle.Format = formato;
		}
	}

	private void btnBuscar_Click(object sender, EventArgs e) => Cargar();

	private void btnCerrar_Click(object sender, EventArgs e) => Close();
}
