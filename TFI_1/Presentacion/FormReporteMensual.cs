using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

public partial class FormReporteMensual : Form
{
	private readonly ReporteNegocio _negocio = new ReporteNegocio();
	private readonly GastoNegocio _gastos = new GastoNegocio();

	private PrintDocument printDocument1 = null!;
	private PrintPreviewDialog printPreviewDialog1 = null!;

	public FormReporteMensual()
	{
		InitializeComponent();
		printDocument1 = new PrintDocument { DocumentName = "Reporte mensual" };
		printDocument1.PrintPage += printDocument1_PrintPage;
		printPreviewDialog1 = new PrintPreviewDialog { Document = printDocument1, Size = new Size(900, 700) };
		dgvPorDia.Anchor = AnchorStyles.Top | AnchorStyles.Left;
		dgvPorEmpleado.Anchor = AnchorStyles.Top | AnchorStyles.Left;
		dgvGastosCategoria.Anchor = AnchorStyles.Top | AnchorStyles.Left;
		Resize += (_, _) => AjustarLayout();
		AjustarLayout();
	}

	private void AjustarLayout()
	{
		if (IsDisposed)
		{
			return;
		}

		const int margen = 12;
		const int separacion = 12;
		int anchoDisponible = ClientSize.Width - (margen * 2);
		int anchoColumna = Math.Max(220, (anchoDisponible - separacion) / 2);
		int xDerecha = margen + anchoColumna + separacion;

		dgvPorDia.SetBounds(margen, 168, anchoDisponible, 150);

		lblMasVendidos.Left = margen;
		lblGastosCategoria.Left = xDerecha;
		dgvMasVendidos.SetBounds(margen, 352, anchoColumna, 150);
		tabInferior.SetBounds(xDerecha, 352, anchoColumna, 150);

		const int yEmpleado = 536;
		int altoEmpleado = Math.Max(100, ClientSize.Height - yEmpleado - margen);
		lblProEmpleado.Left = margen;
		dgvPorEmpleado.SetBounds(margen, yEmpleado, anchoDisponible, altoEmpleado);
	}

	private void FormReporteMensual_Load(object sender, EventArgs e)
	{
		dtpMes.Value = DateTime.Today;
		Cargar();
	}

	private void btnActualizar_Click(object sender, EventArgs e)
	{
		Cargar();
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		printPreviewDialog1.ShowDialog(this);
	}

	private void Cargar()
	{
		try
		{
			DateTime primeroDelMes = new DateTime(dtpMes.Value.Year, dtpMes.Value.Month, 1);
			DateTime ultimoDelMes = primeroDelMes.AddMonths(1).AddDays(-1);

			DataTable resumen = _negocio.Resumen(primeroDelMes, ultimoDelMes);
			if (resumen.Rows.Count > 0)
			{
				DataRow fila = resumen.Rows[0];
				lblTotal.Text = $"Vendido: {Convert.ToDecimal(fila["Total"]):C2}";
				lblCantidad.Text = $"Ventas: {Convert.ToInt32(fila["Cantidad"])}";
				lblTicket.Text = $"Ticket promedio: {Convert.ToDecimal(fila["TicketPromedio"]):C2}";
				lblGananciaBruta.Text = $"Ganancia bruta del mes: {Convert.ToDecimal(fila["Total"]):C2}";
			}

			DataTable porDia = _negocio.VentasPorDia(primeroDelMes, ultimoDelMes);
			dgvPorDia.DataSource = porDia;
			lblDias.Text = $"Días con ventas: {porDia.Rows.Count}";

			dgvMasVendidos.DataSource = _negocio.ProductosMasVendidos(primeroDelMes, ultimoDelMes);
			dgvPorEmpleado.DataSource = _negocio.VentasPorEmpleado(primeroDelMes, ultimoDelMes);

			if (dgvPorEmpleado.Columns.Contains("Total"))
				dgvPorEmpleado.Columns["Total"]!.DefaultCellStyle.Format = "C2";

			decimal vendido = resumen.Rows.Count > 0 ? Convert.ToDecimal(resumen.Rows[0]["Total"]) : 0m;
			decimal gastos = _gastos.TotalGastos(primeroDelMes, ultimoDelMes);
			lblGastos.Text = $"Gastos del mes: {gastos:C2}";
			lblGananciaNeta.Text = $"Ganancia neta del mes: {vendido - gastos:C2}";

			dgvGastosCategoria.DataSource = _gastos.GastosPorCategoria(primeroDelMes, ultimoDelMes);
			if (dgvGastosCategoria.Columns.Contains("Total"))
				dgvGastosCategoria.Columns["Total"]!.DefaultCellStyle.Format = "C2";

			DataTable anulaciones = _negocio.Anulaciones(primeroDelMes, ultimoDelMes);
			dgvAnulaciones.DataSource = anulaciones;
			FormatearAnulaciones();
			decimal totalAnulado = 0m;
			foreach (DataRow fila in anulaciones.Rows)
			{
				totalAnulado += Convert.ToDecimal(fila["Total"]);
			}
			lblResumenAnulaciones.Text =
				$"Ventas anuladas: {anulaciones.Rows.Count} — Total: {totalAnulado:C2}";
			tabAnulaciones.Text = $"Anulaciones ({anulaciones.Rows.Count})";
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo generar el reporte", ex);
		}
	}

	private void FormatearAnulaciones()
	{
		AjustarColumna(dgvAnulaciones, "Venta", "Venta");
		AjustarColumna(dgvAnulaciones, "FechaAnulacion", "Fecha anulación", "dd/MM/yyyy HH:mm");
		AjustarColumna(dgvAnulaciones, "FechaVenta", "Fecha venta", "dd/MM/yyyy HH:mm");
		AjustarColumna(dgvAnulaciones, "Cliente", "Cliente");
		AjustarColumna(dgvAnulaciones, "Pago", "Pago");
		AjustarColumna(dgvAnulaciones, "Total", "Total", "C2");
		AjustarColumna(dgvAnulaciones, "Usuario", "Usuario");
		AjustarColumna(dgvAnulaciones, "Motivo", "Motivo");
	}

	private static void AjustarColumna(DataGridView grid, string nombre, string titulo, string? formato = null)
	{
		DataGridViewColumn? columna = grid.Columns[nombre];
		if (columna == null)
		{
			return;
		}
		columna.HeaderText = titulo;
		if (formato != null)
		{
			columna.DefaultCellStyle.Format = formato;
		}
	}

	private void printDocument1_PrintPage(object? sender, PrintPageEventArgs e)
	{
		DateTime primeroDelMes = new DateTime(dtpMes.Value.Year, dtpMes.Value.Month, 1);
		DateTime ultimoDelMes = primeroDelMes.AddMonths(1).AddDays(-1);
		var g = e.Graphics!;
		var fuenteTitulo = new Font("Segoe UI", 14, FontStyle.Bold);
		var fuente = new Font("Segoe UI", 10);
		var fuenteNegrita = new Font("Segoe UI", 10, FontStyle.Bold);
		int x = 50, y = 50;
		int ancho = e.PageBounds.Width - 100;

		g.DrawString("REPORTE MENSUAL", fuenteTitulo, Brushes.Black, x, y);
		y += 30;
		g.DrawString($"Mes: {primeroDelMes:MMMM yyyy}", fuenteNegrita, Brushes.Black, x, y);
		y += 40;

		DataTable resumen = _negocio.Resumen(primeroDelMes, ultimoDelMes);
		if (resumen.Rows.Count > 0)
		{
			DataRow fila = resumen.Rows[0];
			g.DrawString($"Vendido: {Convert.ToDecimal(fila["Total"]):C2}", fuente, Brushes.Black, x, y);
			g.DrawString($"Ventas: {Convert.ToInt32(fila["Cantidad"])}", fuente, Brushes.Black, x + 200, y);
			g.DrawString($"Ticket promedio: {Convert.ToDecimal(fila["TicketPromedio"]):C2}", fuente, Brushes.Black, x + 400, y);
		}
		y += 40;

		g.DrawString("VENTAS POR DÍA", fuenteNegrita, Brushes.Black, x, y);
		y += 20;
		g.DrawString("Fecha", fuenteNegrita, Brushes.Black, x, y);
		g.DrawString("Cantidad", fuenteNegrita, Brushes.Black, x + 150, y);
		g.DrawString("Total", fuenteNegrita, Brushes.Black, x + 250, y);
		y += 20;
		g.DrawLine(Pens.Gray, x, y, x + 400, y);
		y += 6;
		DataTable porDia = _negocio.VentasPorDia(primeroDelMes, ultimoDelMes);
		foreach (DataRow fila in porDia.Rows)
		{
			g.DrawString(Convert.ToDateTime(fila["Fecha"]).ToString("dd/MM/yyyy"), fuente, Brushes.Black, x, y);
			g.DrawString(Convert.ToString(fila["Cantidad"]), fuente, Brushes.Black, x + 150, y);
			g.DrawString(Convert.ToDecimal(fila["Total"]).ToString("C2"), fuente, Brushes.Black, x + 250, y);
			y += 20;
			if (y > e.PageBounds.Height - 100)
			{
				e.HasMorePages = true;
				return;
			}
		}
		y += 20;
		g.DrawString("VENTAS POR EMPLEADO", fuenteNegrita, Brushes.Black, x, y);
		y += 20;
		g.DrawString("Empleado", fuenteNegrita, Brushes.Black, x, y);
		g.DrawString("Cantidad", fuenteNegrita, Brushes.Black, x + 250, y);
		g.DrawString("Total", fuenteNegrita, Brushes.Black, x + 350, y);
		y += 20;
		g.DrawLine(Pens.Gray, x, y, x + 500, y);
		y += 6;
		foreach (DataRow fila in _negocio.VentasPorEmpleado(primeroDelMes, ultimoDelMes).Rows)
		{
			g.DrawString(Convert.ToString(fila["Empleado"]), fuente, Brushes.Black, x, y);
			g.DrawString(Convert.ToString(fila["Cantidad"]), fuente, Brushes.Black, x + 250, y);
			g.DrawString(Convert.ToDecimal(fila["Total"]).ToString("C2"), fuente, Brushes.Black, x + 350, y);
			y += 20;
		}
		y += 20;
		if (y > e.PageBounds.Height - 100)
		{
			e.HasMorePages = true;
			return;
		}
		DataTable anuladas = _negocio.Anulaciones(primeroDelMes, ultimoDelMes);
		decimal totalAnulado = 0m;
		foreach (DataRow fila in anuladas.Rows)
		{
			totalAnulado += Convert.ToDecimal(fila["Total"]);
		}
		g.DrawString("ANULACIONES", fuenteNegrita, Brushes.Black, x, y);
		y += 20;
		g.DrawString($"Ventas anuladas: {anuladas.Rows.Count} — Total: {totalAnulado:C2}",
			fuente, Brushes.Black, x, y);
		y += 20;
		g.DrawString("Venta", fuenteNegrita, Brushes.Black, x, y);
		g.DrawString("Fecha anulación", fuenteNegrita, Brushes.Black, x + 70, y);
		g.DrawString("Cliente", fuenteNegrita, Brushes.Black, x + 220, y);
		g.DrawString("Total", fuenteNegrita, Brushes.Black, x + 390, y);
		g.DrawString("Usuario", fuenteNegrita, Brushes.Black, x + 480, y);
		y += 20;
		g.DrawLine(Pens.Gray, x, y, x + 580, y);
		y += 6;
		foreach (DataRow fila in anuladas.Rows)
		{
			g.DrawString(Convert.ToString(fila["Venta"]), fuente, Brushes.Black, x, y);
			g.DrawString(Convert.ToDateTime(fila["FechaAnulacion"]).ToString("dd/MM/yyyy HH:mm"),
				fuente, Brushes.Black, x + 70, y);
			g.DrawString(Convert.ToString(fila["Cliente"]), fuente, Brushes.Black, x + 220, y);
			g.DrawString(Convert.ToDecimal(fila["Total"]).ToString("C2"), fuente, Brushes.Black, x + 390, y);
			g.DrawString(Convert.ToString(fila["Usuario"]), fuente, Brushes.Black, x + 480, y);
			y += 20;
			if (y > e.PageBounds.Height - 100)
			{
				e.HasMorePages = true;
				return;
			}
		}
		y += 20;
		g.DrawString("GASTOS POR CATEGORÍA", fuenteNegrita, Brushes.Black, x, y);
		y += 20;
		g.DrawString("Categoría", fuenteNegrita, Brushes.Black, x, y);
		g.DrawString("Cantidad", fuenteNegrita, Brushes.Black, x + 250, y);
		g.DrawString("Total", fuenteNegrita, Brushes.Black, x + 350, y);
		y += 20;
		g.DrawLine(Pens.Gray, x, y, x + 500, y);
		y += 6;
		foreach (DataRow fila in _gastos.GastosPorCategoria(primeroDelMes, ultimoDelMes).Rows)
		{
			g.DrawString(Convert.ToString(fila["Categoria"]), fuente, Brushes.Black, x, y);
			g.DrawString(Convert.ToString(fila["Cantidad"]), fuente, Brushes.Black, x + 250, y);
			g.DrawString(Convert.ToDecimal(fila["Total"]).ToString("C2"), fuente, Brushes.Black, x + 350, y);
			y += 20;
		}
		decimal vendidoMes = resumen.Rows.Count > 0 ? Convert.ToDecimal(resumen.Rows[0]["Total"]) : 0m;
		decimal gastosMes = _gastos.TotalGastos(primeroDelMes, ultimoDelMes);
		y += 10;
		g.DrawString($"TOTAL GASTOS: {gastosMes:C2}", fuenteNegrita, Brushes.Black, x, y);
		y += 20;
		g.DrawString($"GANANCIA NETA: {vendidoMes - gastosMes:C2}", fuenteNegrita, Brushes.Black, x, y);

		fuente.Dispose();
		fuenteNegrita.Dispose();
		fuenteTitulo.Dispose();
	}
}
