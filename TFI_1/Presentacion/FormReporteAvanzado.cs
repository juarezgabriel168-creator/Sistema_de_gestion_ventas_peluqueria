using System;
using System.Windows.Forms;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

public partial class FormReporteAvanzado : Form
{
	private readonly ReporteNegocio _negocio = new();

	public FormReporteAvanzado()
	{
		InitializeComponent();
	}

	private void FormReporteAvanzado_Load(object sender, EventArgs e)
	{
		DateTime hoy = DateTime.Today;
		dtpDesde.Value = new DateTime(hoy.Year, hoy.Month, 1);
		dtpHasta.Value = hoy;
		Cargar();
	}

	private void btnActualizar_Click(object sender, EventArgs e) => Cargar();

	private void Cargar()
	{
		try
		{
			DateTime desde = dtpDesde.Value.Date;
			DateTime hasta = dtpHasta.Value.Date;
			if (!Validaciones.RangoDeFechasValido(desde, hasta))
			{
				MessageBox.Show("La fecha 'desde' no puede ser posterior a la fecha 'hasta'.",
					"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}

			dgvClientes.DataSource = _negocio.ClientesConMasCompras(desde, hasta);
			dgvUsuarios.DataSource = _negocio.VentasPorUsuario(desde, hasta);
			dgvServicios.DataSource = _negocio.ServiciosMasSolicitados(desde, hasta);

			FormatearTotal(dgvClientes);
			FormatearTotal(dgvUsuarios);
			FormatearTotal(dgvServicios);
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudieron generar los reportes", ex);
		}
	}

	private static void FormatearTotal(DataGridView grid)
	{
		if (grid.Columns.Contains("Total"))
		{
			grid.Columns["Total"]!.DefaultCellStyle.Format = "C2";
		}
	}
}
