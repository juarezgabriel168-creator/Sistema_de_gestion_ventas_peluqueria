using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
public partial class FormDashboard : Form
{
	private readonly DashboardNegocio _negocio = new DashboardNegocio();
	private readonly AtencionNegocio _atencionNegocio = new AtencionNegocio();

	private DataTable? _ultimasVentasParaGrafico;

	public FormDashboard()
	{
		InitializeComponent();
	}

	private void FormDashboard_Load(object sender, EventArgs e)
	{
		Refrescar();
	}

	private void btnActualizar_Click(object sender, EventArgs e)
	{
		Refrescar();
	}

	private void Refrescar()
	{
		try
		{
			lblValorVendido.Text = _negocio.TotalVendidoHoy().ToString("C");
			lblValorVentas.Text = _negocio.CantidadVentasHoy().ToString();
			lblValorClientes.Text = _negocio.TotalClientes().ToString();
			lblValorItems.Text = _negocio.TotalItems().ToString();
			lblValorStock.Text = _negocio.ProductosStockBajo().ToString();
			lblValorTurnos.Text = _atencionNegocio.CantidadHoy().ToString();

			// Producto con mayor y menor stock.
			var mayor = _negocio.ProductoMayorStock();
			var menor = _negocio.ProductoMenorStock();
			if (mayor.Rows.Count > 0)
			{
				lblValorMayorStock.Text = $"{mayor.Rows[0]["Producto"]} ({mayor.Rows[0]["Stock"]})";
			}
			else
			{
				lblValorMayorStock.Text = "—";
			}
			if (menor.Rows.Count > 0)
			{
				lblValorMenorStock.Text = $"{menor.Rows[0]["Producto"]} ({menor.Rows[0]["Stock"]})";
			}
			else
			{
				lblValorMenorStock.Text = "—";
			}

			// Datos para el gráfico de barras (últimas 10 ventas).
			_ultimasVentasParaGrafico = _negocio.UltimasVentas(10);
			dgvUltimasVentas.DataSource = _ultimasVentasParaGrafico;
			pnlGrafico.Invalidate();
		}
		catch (Exception ex)
		{
			MessageBox.Show("No se pudieron cargar los indicadores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void pnlGrafico_Paint(object? sender, PaintEventArgs e)
	{
		var g = e.Graphics;
		g.Clear(pnlGrafico.BackColor);
		if (_ultimasVentasParaGrafico == null || _ultimasVentasParaGrafico.Rows.Count == 0)
			return;

		int margenIzq = 40, margenDer = 20, margenSup = 30, margenInf = 30;
		int anchoDisponible = pnlGrafico.Width - margenIzq - margenDer;
		int altoDisponible = pnlGrafico.Height - margenSup - margenInf;

		decimal maxTotal = 0;
		foreach (DataRow fila in _ultimasVentasParaGrafico.Rows)
		{
			decimal total = Convert.ToDecimal(fila["Total"]);
			if (total > maxTotal) maxTotal = total;
		}
		if (maxTotal == 0) maxTotal = 1;

		using var plumaEje = new Pen(Color.Gray, 1);
		g.DrawLine(plumaEje, margenIzq, margenSup, margenIzq, margenSup + altoDisponible);
		g.DrawLine(plumaEje, margenIzq, margenSup + altoDisponible, margenIzq + anchoDisponible, margenSup + altoDisponible);

		using var font = new Font("Segoe UI", 8);
		using var brocha = new SolidBrush(Color.FromArgb(0, 102, 51));

		int n = _ultimasVentasParaGrafico.Rows.Count;
		int anchoBarra = Math.Max(15, (anchoDisponible / n) - 8);

		for (int i = 0; i < n; i++)
		{
			DataRow fila = _ultimasVentasParaGrafico.Rows[i];
			int venta = Convert.ToInt32(fila["Venta"]);
			decimal total = Convert.ToDecimal(fila["Total"]);
			int altoBarra = (int)((total / maxTotal) * altoDisponible);
			int x = margenIzq + i * (anchoBarra + 8) + 4;
			int yBase = margenSup + altoDisponible;
			g.FillRectangle(brocha, x, yBase - altoBarra, anchoBarra, altoBarra);
			g.DrawString($"V{venta}", font, Brushes.Black, x, yBase + 2);
			g.DrawString(total.ToString("C0"), font, Brushes.Black, x, Math.Max(margenSup, yBase - altoBarra - 13));
		}
	}
}
