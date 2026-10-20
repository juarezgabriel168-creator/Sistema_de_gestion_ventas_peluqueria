using System;
using System.Data;
using System.Windows.Forms;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

/// <summary>
/// Consulta de la auditoría: quién hizo cada operación sensible, cuándo y
/// sobre qué registro. Solo accesible para administradores.
/// </summary>
public partial class FormAuditoria : Form
{
	private readonly AuditoriaNegocio _negocio = new();
	private readonly UsuarioNegocio _usuarioNegocio = new();

	public FormAuditoria()
	{
		InitializeComponent();
	}

	private void FormAuditoria_Load(object sender, EventArgs e)
	{
		try
		{
			dtpDesde.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
			dtpHasta.Value = DateTime.Today;
			CargarUsuarios();
			CargarAcciones();
			Cargar();
		}
		catch (Exception ex)
		{
			MessageBox.Show("No se pudo cargar la auditoría: " + ex.Message, "Error",
				MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void CargarUsuarios()
	{
		DataTable tabla = new DataTable();
		tabla.Columns.Add("Id", typeof(int));
		tabla.Columns.Add("Nombre", typeof(string));
		tabla.Rows.Add(0, "(Todos)");

		foreach (var usuario in _usuarioNegocio.Listar())
		{
			tabla.Rows.Add(usuario.Id, usuario.NombreUsuario);
		}

		cboUsuario.DataSource = tabla;
		cboUsuario.DisplayMember = "Nombre";
		cboUsuario.ValueMember = "Id";
	}

	private void CargarAcciones()
	{
		DataTable tabla = _negocio.Acciones();
		DataRow fila = tabla.NewRow();
		fila["Codigo"] = string.Empty;
		fila["Nombre"] = "(Todas)";
		tabla.Rows.InsertAt(fila, 0);

		cboAccion.DataSource = tabla;
		cboAccion.DisplayMember = "Nombre";
		cboAccion.ValueMember = "Codigo";
	}

	private void Cargar()
	{
		try
		{
			int usuarioId = cboUsuario.SelectedValue == null ? 0 : Convert.ToInt32(cboUsuario.SelectedValue);
			string? accion = Convert.ToString(cboAccion.SelectedValue);
			if (string.IsNullOrWhiteSpace(accion))
			{
				accion = null;
			}

			DataTable tabla = _negocio.Listar(dtpDesde.Value.Date, dtpHasta.Value.Date,
				usuarioId > 0 ? usuarioId : ((int?)null), accion);
			dgvAuditoria.DataSource = tabla;
			FormatearGrid();
			lblTotal.Text = $"{tabla.Rows.Count} registro(s)";
		}
		catch (Exception ex)
		{
			MessageBox.Show("No se pudo cargar la auditoría: " + ex.Message, "Error",
				MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void FormatearGrid()
	{
		OcultarColumna("Id");
		OcultarColumna("Codigo");
		AjustarColumna("Fecha", formato: "dd/MM/yyyy HH:mm");
		AjustarColumna("Entidad", titulo: "Tabla");
		AjustarColumna("IdEntidad", titulo: "Nº");
		// El detalle es el dato más largo: le damos más espacio que al resto.
		AjustarColumna("Detalle", peso: 220);
		AjustarColumna("Usuario", peso: 60);
		AjustarColumna("Accion", peso: 95);
	}

	private void OcultarColumna(string nombre)
	{
		DataGridViewColumn? columna = dgvAuditoria.Columns[nombre];
		if (columna != null)
		{
			columna.Visible = false;
		}
	}

	private void AjustarColumna(string nombre, string? titulo = null, string? formato = null, int? peso = null)
	{
		DataGridViewColumn? columna = dgvAuditoria.Columns[nombre];
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
		if (peso.HasValue)
		{
			columna.FillWeight = peso.Value;
		}
	}

	private void btnBuscar_Click(object sender, EventArgs e) => Cargar();

	private void btnCerrar_Click(object sender, EventArgs e) => Close();
}
