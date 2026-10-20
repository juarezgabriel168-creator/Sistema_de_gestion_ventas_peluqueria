using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
public partial class FormDashboard : Form
{
	private readonly DashboardNegocio _negocio = new DashboardNegocio();
	private readonly AtencionNegocio _atencionNegocio = new AtencionNegocio();
	private readonly ToolTip _tooltipGrafico = new ToolTip();
	private readonly List<Rectangle> _zonasBarras = new List<Rectangle>();
	private readonly List<string> _detallesBarras = new List<string>();

	private DataTable? _ultimasVentasParaGrafico;
	private int _indiceTooltip = -1;

	public FormDashboard()
	{
		InitializeComponent();
		FormClosed += (s, e) => _tooltipGrafico.Dispose();
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
			Mensajes.Error(this, "No se pudieron cargar los indicadores", ex);
		}
	}

	private void pnlGrafico_Paint(object? sender, PaintEventArgs e)
	{
		Graphics g = e.Graphics;
		g.SmoothingMode = SmoothingMode.AntiAlias;
		g.Clear(pnlGrafico.BackColor);

		using var fuenteTitulo = new Font("Segoe UI", 9F, FontStyle.Bold);
		using var fuenteEtiqueta = new Font("Segoe UI", 7.5F);
		using var fuenteValor = new Font("Segoe UI", 7.5F, FontStyle.Bold);
		using var brochaTexto = new SolidBrush(Color.FromArgb(70, 70, 70));
		using var brochaSuave = new SolidBrush(Color.FromArgb(130, 130, 130));
		using var plumaGrilla = new Pen(Color.FromArgb(226, 226, 226));
		using var plumaEje = new Pen(Color.FromArgb(160, 160, 160));

		g.DrawString("Últimas ventas (importe)", fuenteTitulo, brochaTexto, 10, 6);

		_zonasBarras.Clear();
		_detallesBarras.Clear();
		_indiceTooltip = -1;

		if (_ultimasVentasParaGrafico == null || _ultimasVentasParaGrafico.Rows.Count == 0)
		{
			g.DrawString("Todavía no hay ventas para mostrar.", fuenteEtiqueta, brochaSuave, 10, 30);
			return;
		}

		const int margenIzq = 62;
		const int margenDer = 10;
		const int margenSup = 26;
		const int margenInf = 20;

		int ancho = pnlGrafico.Width - margenIzq - margenDer;
		int alto = pnlGrafico.Height - margenSup - margenInf;
		if (ancho < 40 || alto < 30)
		{
			return;
		}

		decimal maximo = 0;
		foreach (DataRow fila in _ultimasVentasParaGrafico.Rows)
		{
			maximo = Math.Max(maximo, Convert.ToDecimal(fila["Total"]));
		}

		// Escala "redonda": el techo y las líneas guía son múltiplos de 1, 2 o 5.
		decimal paso = CalcularPaso(maximo);
		decimal techo = Math.Max(paso, Math.Ceiling(maximo / paso) * paso);
		int divisiones = Math.Max(1, (int)Math.Round(techo / paso));

		int yBase = margenSup + alto;

		for (int i = 0; i <= divisiones; i++)
		{
			decimal valor = paso * i;
			int y = yBase - (int)Math.Round((double)(valor / techo) * alto);
			g.DrawLine(i == 0 ? plumaEje : plumaGrilla, margenIzq, y, margenIzq + ancho, y);

			string etiqueta = FormatearImporte(valor);
			SizeF tamEtiqueta = g.MeasureString(etiqueta, fuenteEtiqueta);
			g.DrawString(etiqueta, fuenteEtiqueta, brochaSuave,
				margenIzq - tamEtiqueta.Width - 6, y - tamEtiqueta.Height / 2f);
		}

		int n = _ultimasVentasParaGrafico.Rows.Count;
		int separacion = Math.Max(6, ancho / (n * 6));
		int anchoBarra = Math.Max(10, (ancho - separacion * (n + 1)) / n);
		int pasoBarra = anchoBarra + separacion;

		using var brochaBarra = new LinearGradientBrush(
			new Rectangle(0, margenSup, Math.Max(1, anchoBarra), Math.Max(1, alto)),
			Color.FromArgb(0, 158, 84),
			Color.FromArgb(0, 104, 56),
			LinearGradientMode.Vertical);
		using var plumaBarra = new Pen(Color.FromArgb(0, 90, 48));

		bool mostrarImporte = pasoBarra >= 46;
		bool mostrarNumero = pasoBarra >= 22;

		// Se dibujan de la más vieja a la más nueva: la última venta queda a la derecha.
		for (int posicion = 0; posicion < n; posicion++)
		{
			DataRow fila = _ultimasVentasParaGrafico.Rows[n - 1 - posicion];
			int venta = Convert.ToInt32(fila["Venta"]);
			decimal total = Convert.ToDecimal(fila["Total"]);
			int altoBarra = total <= 0
				? 0
				: Math.Max(3, (int)Math.Round((double)(total / techo) * alto));
			int x = margenIzq + separacion + posicion * pasoBarra;
			int y = yBase - altoBarra;

			if (altoBarra > 0)
			{
				var barra = new Rectangle(x, y, anchoBarra, altoBarra);
				g.FillRectangle(brochaBarra, barra);
				g.DrawRectangle(plumaBarra, barra);
			}

			_zonasBarras.Add(new Rectangle(x - separacion / 2, margenSup,
				anchoBarra + separacion, alto + margenInf));
			_detallesBarras.Add(DetalleVentaTexto(fila));

			if (mostrarNumero)
			{
				string textoVenta = $"V{venta}";
				SizeF tamVenta = g.MeasureString(textoVenta, fuenteEtiqueta);
				g.DrawString(textoVenta, fuenteEtiqueta, brochaTexto,
					x + (anchoBarra - tamVenta.Width) / 2f, yBase + 2);
			}

			if (mostrarImporte)
			{
				string textoTotal = FormatearImporte(total);
				SizeF tamTotal = g.MeasureString(textoTotal, fuenteValor);
				float tx = x + (anchoBarra - tamTotal.Width) / 2f;
				float ty = y - tamTotal.Height - 2;
				if (ty >= margenSup)
				{
					g.DrawString(textoTotal, fuenteValor, brochaTexto, tx, ty);
				}
				else
				{
					// La barra llega arriba: el importe se escribe dentro, en blanco.
					g.DrawString(textoTotal, fuenteValor, Brushes.White, tx, y + 2);
				}
			}
		}
	}

	private void pnlGrafico_MouseMove(object? sender, MouseEventArgs e)
	{
		int indice = -1;
		for (int i = 0; i < _zonasBarras.Count; i++)
		{
			if (_zonasBarras[i].Contains(e.Location))
			{
				indice = i;
				break;
			}
		}

		if (indice == _indiceTooltip)
		{
			return;
		}

		_indiceTooltip = indice;
		_tooltipGrafico.SetToolTip(pnlGrafico,
			indice >= 0 ? _detallesBarras[indice] : string.Empty);
	}

	private void pnlGrafico_MouseLeave(object? sender, EventArgs e)
	{
		_indiceTooltip = -1;
		_tooltipGrafico.SetToolTip(pnlGrafico, string.Empty);
	}

	private void pnlGrafico_Resize(object? sender, EventArgs e) => pnlGrafico.Invalidate();

	private static string DetalleVentaTexto(DataRow fila)
	{
		DateTime fecha = Convert.ToDateTime(fila["Fecha"]);
		string cliente = Convert.ToString(fila["Cliente"]) ?? string.Empty;
		string pago = Convert.ToString(fila["Pago"]) ?? string.Empty;
		decimal total = Convert.ToDecimal(fila["Total"]);
		return $"Venta #{fila["Venta"]}{Environment.NewLine}{fecha:g}" +
			$"{Environment.NewLine}Cliente: {cliente}" +
			$"{Environment.NewLine}Pago: {pago}" +
			$"{Environment.NewLine}Total: {total:C2}";
	}

	/// <summary>Elige un paso "redondo" (1, 2 o 5 por potencia de 10) para el eje.</summary>
	private static decimal CalcularPaso(decimal maximo)
	{
		if (maximo <= 0)
		{
			return 1;
		}

		double objetivo = (double)maximo / 5.0;
		double potencia = Math.Pow(10, Math.Floor(Math.Log10(objetivo)));
		double normalizado = objetivo / potencia;
		double factor = normalizado <= 1 ? 1 : normalizado <= 2 ? 2 : normalizado <= 5 ? 5 : 10;
		return (decimal)(factor * potencia);
	}

	/// <summary>Formato corto para los ejes: $ 0, $ 1,5 mil, $ 2 M.</summary>
	private static string FormatearImporte(decimal valor)
	{
		if (valor >= 1_000_000m)
		{
			return $"$ {valor / 1_000_000m:0.#} M";
		}
		if (valor >= 1_000m)
		{
			return $"$ {valor / 1_000m:0.#} mil";
		}
		return $"$ {valor:0.##}";
	}
}
