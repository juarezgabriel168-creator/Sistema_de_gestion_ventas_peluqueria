using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

/// <summary>
/// Muestra el ticket (comprobante) de una venta y permite imprimirlo.
/// </summary>
public partial class FormTicket : Form
{
	// Ancho de cada línea del ticket, en caracteres (se usa una letra de ancho fijo).
	private const int Ancho = 37;

	private const string NombreNegocio = "PELUQUERÍA";

	private readonly VentaNegocio _negocio = new VentaNegocio();
	private readonly int _ventaId;

	private string _textoTicket = string.Empty;

	public FormTicket()
	{
		InitializeComponent();
	}

	public FormTicket(int ventaId) : this()
	{
		_ventaId = ventaId;
	}

	private void FormTicket_Load(object sender, EventArgs e)
	{
		if (_ventaId <= 0)
		{
			return;
		}

		try
		{
			DataTable cabecera = _negocio.ObtenerCabecera(_ventaId);
			if (cabecera.Rows.Count == 0)
			{
				MessageBox.Show("La venta seleccionada no existe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
				return;
			}

			DataTable detalle = _negocio.ObtenerDetalle(_ventaId);
			_textoTicket = ConstruirTicket(cabecera.Rows[0], detalle);
			txtTicket.Text = _textoTicket;
			txtTicket.SelectionStart = 0;
			txtTicket.SelectionLength = 0;
		}
		catch (Exception ex)
		{
			MessageBox.Show("No se pudo generar el ticket: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(_textoTicket))
		{
			return;
		}

		try
		{
			// Se intenta usar un ancho de ticket (80 mm aprox.). Si la impresora
			// no admite ese tamaño, se imprime con el papel que tenga configurado.
			try
			{
				printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Ticket 80mm", 315, 1200);
				printDocument1.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);
			}
			catch
			{
				// Tamaño de papel no soportado: se continúa con la configuración por defecto.
			}

			printPreviewDialog1.ShowDialog(this);
		}
		catch (Exception ex)
		{
			MessageBox.Show("No se pudo imprimir el ticket: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void printDocument1_PrintPage(object? sender, PrintPageEventArgs e)
	{
		using Font fuente = new Font("Consolas", 9F);
		e.Graphics!.DrawString(_textoTicket, fuente, Brushes.Black, e.MarginBounds);
	}

	// Arma el texto del ticket con líneas de ancho fijo.
	private static string ConstruirTicket(DataRow fila, DataTable detalle)
	{
		string separador = new string('-', Ancho);
		StringBuilder sb = new StringBuilder();

		bool anulada = fila.Table.Columns.Contains("Anulada") && fila["Anulada"] != DBNull.Value
			&& Convert.ToBoolean(fila["Anulada"]);

		sb.AppendLine(Centrar(NombreNegocio));
		sb.AppendLine(Centrar("Ticket de venta"));
		if (anulada)
		{
			sb.AppendLine(Centrar("*** ANULADA ***"));
		}
		sb.AppendLine(separador);
		sb.AppendLine("Venta Nº: " + fila["Venta"]);
		sb.AppendLine("Fecha: " + Convert.ToDateTime(fila["Fecha"]).ToString("dd/MM/yyyy HH:mm"));
		sb.AppendLine("Cliente: " + Convert.ToString(fila["Cliente"]));

		string? empleado = Convert.ToString(fila["Empleado"]);
		if (!string.IsNullOrWhiteSpace(empleado))
			sb.AppendLine("Empleado: " + empleado);

		sb.AppendLine("Atendió: " + Convert.ToString(fila["Usuario"]));
		sb.AppendLine(separador);
		sb.AppendLine("CANT  " + "DESCRIPCIÓN".PadRight(18) + " " + "IMPORTE".PadLeft(12));
		sb.AppendLine(separador);

		foreach (DataRow d in detalle.Rows)
		{
			string nombre = Truncar(Convert.ToString(d["Item"]) ?? string.Empty, 18);
			sb.AppendLine(
				Convert.ToDecimal(d["Cantidad"]).ToString("0").PadLeft(4) + "  " +
				nombre.PadRight(18) + " " +
				Convert.ToDecimal(d["Subtotal"]).ToString("C2").PadLeft(12));
		}

		sb.AppendLine(separador);

		decimal subtotal = Columna(fila, "Subtotal");
		decimal descuento = Columna(fila, "Descuento");
		decimal recargo = Columna(fila, "Recargo");
		decimal total = Columna(fila, "Total");

		sb.AppendLine(LineaImporte("Subtotal:", subtotal));
		if (descuento > 0m)
			sb.AppendLine(LineaImporte("Descuento:", -descuento));
		if (recargo > 0m)
			sb.AppendLine(LineaImporte("Recargo:", recargo));
		sb.AppendLine(LineaImporte("TOTAL:", total));
		sb.AppendLine(separador);

		sb.AppendLine("Medio de pago: " + Capitalizar(Convert.ToString(fila["Pago"])));
		if (fila.Table.Columns.Contains("IdTarjeta") && fila["IdTarjeta"] != DBNull.Value)
		{
			sb.AppendLine("Tarjeta: " + fila["Banco"] + " - " + fila["TipoTarjeta"]);
			if (fila["Cuotas"] != DBNull.Value)
				sb.AppendLine("Cuotas: " + fila["Cuotas"]);
		}

		sb.AppendLine(separador);
		if (anulada)
		{
			sb.AppendLine(Centrar("VENTA ANULADA"));
			if (fila.Table.Columns.Contains("FechaAnulacion") && fila["FechaAnulacion"] != DBNull.Value)
			{
				sb.AppendLine("Anulada: " + Convert.ToDateTime(fila["FechaAnulacion"]).ToString("dd/MM/yyyy HH:mm"));
			}
			if (fila.Table.Columns.Contains("MotivoAnulacion") && fila["MotivoAnulacion"] != DBNull.Value)
			{
				sb.AppendLine("Motivo: " + fila["MotivoAnulacion"]);
			}
		}
		else
		{
			sb.AppendLine(Centrar("¡Gracias por su compra!"));
		}
		sb.AppendLine(separador);
		sb.AppendLine("Emitido: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

		return sb.ToString();
	}

	private static string Centrar(string texto)
	{
		if (texto.Length >= Ancho)
		{
			return texto;
		}
		int espacios = (Ancho - texto.Length) / 2;
		return new string(' ', espacios) + texto;
	}

	private static string Truncar(string texto, int largo)
	{
		return texto.Length <= largo ? texto : texto.Substring(0, largo);
	}

	private static string LineaImporte(string etiqueta, decimal importe)
	{
		return etiqueta.PadRight(Ancho - 12) + importe.ToString("C2").PadLeft(12);
	}

	private static decimal Columna(DataRow fila, string columna)
	{
		return fila.Table.Columns.Contains(columna) && fila[columna] != DBNull.Value
			? Convert.ToDecimal(fila[columna])
			: 0m;
	}

	private static string Capitalizar(string? texto)
	{
		if (string.IsNullOrWhiteSpace(texto))
		{
			return string.Empty;
		}
		return char.ToUpper(texto[0]) + texto.Substring(1);
	}
}
