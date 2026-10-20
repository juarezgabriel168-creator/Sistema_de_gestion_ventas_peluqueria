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
                dgvPorEmpleado.Columns["Total"].DefaultCellStyle.Format = "C2";

            decimal vendido = resumen.Rows.Count > 0 ? Convert.ToDecimal(resumen.Rows[0]["Total"]) : 0m;
            decimal gastos = _gastos.TotalGastos(primeroDelMes, ultimoDelMes);
            lblGastos.Text = $"Gastos del mes: {gastos:C2}";
            lblGananciaNeta.Text = $"Ganancia neta del mes: {vendido - gastos:C2}";

            dgvGastosCategoria.DataSource = _gastos.GastosPorCategoria(primeroDelMes, ultimoDelMes);
            if (dgvGastosCategoria.Columns.Contains("Total"))
                dgvGastosCategoria.Columns["Total"].DefaultCellStyle.Format = "C2";
        }
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo generar el reporte", ex);
		}
	}

	private void printDocument1_PrintPage(object? sender, PrintPageEventArgs e)
	{
		DateTime primeroDelMes = new DateTime(dtpMes.Value.Year, dtpMes.Value.Month, 1);
		DateTime ultimoDelMes = primeroDelMes.AddMonths(1).AddDays(-1);
		var g = e.Graphics;
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
