using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
public partial class FormReporteDiario : Form
{
	private readonly ReporteNegocio _negocio = new ReporteNegocio();
    private readonly GastoNegocio _gastos = new GastoNegocio();

    private PrintDocument printDocument1 = null!;
	private PrintPreviewDialog printPreviewDialog1 = null!;

	public FormReporteDiario()
	{
		InitializeComponent();
		printDocument1 = new PrintDocument { DocumentName = "Reporte diario" };
		printDocument1.PrintPage += printDocument1_PrintPage;
		printPreviewDialog1 = new PrintPreviewDialog { Document = printDocument1, Size = new Size(900, 700) };
	}

	private void FormReporteDiario_Load(object sender, EventArgs e)
	{
		dtpFecha.Value = DateTime.Today;
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
			DateTime fecha = dtpFecha.Value.Date;
			DataTable resumen = _negocio.Resumen(fecha, fecha);
			if (resumen.Rows.Count > 0)
			{
				DataRow fila = resumen.Rows[0];
				lblTotal.Text = $"Vendido: {Convert.ToDecimal(fila["Total"]):C2}";
				lblCantidad.Text = $"Ventas: {Convert.ToInt32(fila["Cantidad"])}";
				lblTicket.Text = $"Ticket promedio: {Convert.ToDecimal(fila["TicketPromedio"]):C2}";
			}

			DataTable porPago = _negocio.VentasPorMedioPago(fecha, fecha);
			dgvPorPago.DataSource = porPago;

			// Ganancia bruta = suma de los totales por medio de pago.
			decimal gananciaBruta = 0m;
			foreach (DataRow fila in porPago.Rows)
				gananciaBruta += Convert.ToDecimal(fila["Total"]);
			lblGananciaBruta.Text = $"Ganancia bruta del día: {gananciaBruta:C2}";

            dgvPorEmpleado.DataSource = _negocio.VentasPorEmpleado(fecha, fecha);
            if (dgvPorEmpleado.Columns.Contains("Total"))
                dgvPorEmpleado.Columns["Total"]!.DefaultCellStyle.Format = "C2";

            decimal gastos = _gastos.TotalGastos(fecha, fecha);
            lblGastos.Text = $"Gastos del día: {gastos:C2}";
            lblGananciaNeta.Text = $"Ganancia neta del día: {gananciaBruta - gastos:C2}";

            dgvGastosCategoria.DataSource = _gastos.GastosPorCategoria(fecha, fecha);
            if (dgvGastosCategoria.Columns.Contains("Total"))
            dgvGastosCategoria.Columns["Total"]!.DefaultCellStyle.Format = "C2";

            dgvMasVendidos.DataSource = _negocio.ProductosMasVendidos(fecha, fecha);
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo generar el reporte", ex);
		}
	}

	private void printDocument1_PrintPage(object? sender, PrintPageEventArgs e)
	{
		DateTime fecha = dtpFecha.Value.Date;
		var g = e.Graphics!;
		var fuenteTitulo = new Font("Segoe UI", 14, FontStyle.Bold);
		var fuente = new Font("Segoe UI", 10);
		var fuenteNegrita = new Font("Segoe UI", 10, FontStyle.Bold);
		int x = 50, y = 50;
		int ancho = e.PageBounds.Width - 100;

		g.DrawString("REPORTE DIARIO", fuenteTitulo, Brushes.Black, x, y);
		y += 30;
		g.DrawString($"Fecha: {fecha:dd/MM/yyyy}", fuenteNegrita, Brushes.Black, x, y);
		y += 40;

		DataTable resumen = _negocio.Resumen(fecha, fecha);
		if (resumen.Rows.Count > 0)
		{
			DataRow fila = resumen.Rows[0];
			g.DrawString($"Vendido: {Convert.ToDecimal(fila["Total"]):C2}", fuente, Brushes.Black, x, y);
			g.DrawString($"Ventas: {Convert.ToInt32(fila["Cantidad"])}", fuente, Brushes.Black, x + 200, y);
			g.DrawString($"Ticket promedio: {Convert.ToDecimal(fila["TicketPromedio"]):C2}", fuente, Brushes.Black, x + 400, y);
		}
		y += 40;

		decimal gananciaBruta = 0m;
		DataTable porPago = _negocio.VentasPorMedioPago(fecha, fecha);
		g.DrawString("DESGLOSE POR MEDIO DE PAGO", fuenteNegrita, Brushes.Black, x, y);
		y += 20;
		g.DrawString("Medio", fuenteNegrita, Brushes.Black, x, y);
		g.DrawString("Cantidad", fuenteNegrita, Brushes.Black, x + 150, y);
		g.DrawString("Total", fuenteNegrita, Brushes.Black, x + 250, y);
		y += 20;
		g.DrawLine(Pens.Gray, x, y, x + 400, y);
		y += 6;
		foreach (DataRow fila in porPago.Rows)
		{
			g.DrawString(Convert.ToString(fila["Pago"]), fuente, Brushes.Black, x, y);
			g.DrawString(Convert.ToString(fila["Cantidad"]), fuente, Brushes.Black, x + 150, y);
			g.DrawString(Convert.ToDecimal(fila["Total"]).ToString("C2"), fuente, Brushes.Black, x + 250, y);
			gananciaBruta += Convert.ToDecimal(fila["Total"]);
			y += 20;
			if (y > e.PageBounds.Height - 100)
			{
				e.HasMorePages = true;
				return;
			}
		}
        g.DrawString($"GANANCIA BRUTA: {gananciaBruta:C2}", fuenteNegrita, Brushes.Black, x, y + 10);
        y += 50;

        g.DrawString("VENTAS POR EMPLEADO", fuenteNegrita, Brushes.Black, x, y);
        y += 20;
        g.DrawString("Empleado", fuenteNegrita, Brushes.Black, x, y);
        g.DrawString("Cantidad", fuenteNegrita, Brushes.Black, x + 250, y);
        g.DrawString("Total", fuenteNegrita, Brushes.Black, x + 350, y);
        y += 20;
        g.DrawLine(Pens.Gray, x, y, x + 500, y);
        y += 6;
        foreach (DataRow fila in _negocio.VentasPorEmpleado(fecha, fecha).Rows)
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
        foreach (DataRow fila in _gastos.GastosPorCategoria(fecha, fecha).Rows)
        {
            g.DrawString(Convert.ToString(fila["Categoria"]), fuente, Brushes.Black, x, y);
            g.DrawString(Convert.ToString(fila["Cantidad"]), fuente, Brushes.Black, x + 250, y);
            g.DrawString(Convert.ToDecimal(fila["Total"]).ToString("C2"), fuente, Brushes.Black, x + 350, y);
            y += 20;
        }
        decimal gastosDia = _gastos.TotalGastos(fecha, fecha);
        y += 10;
        g.DrawString($"TOTAL GASTOS: {gastosDia:C2}", fuenteNegrita, Brushes.Black, x, y);
        y += 20;
        g.DrawString($"GANANCIA NETA: {gananciaBruta - gastosDia:C2}", fuenteNegrita, Brushes.Black, x, y);

        fuente.Dispose();
		fuenteNegrita.Dispose();
		fuenteTitulo.Dispose();
	}
}
