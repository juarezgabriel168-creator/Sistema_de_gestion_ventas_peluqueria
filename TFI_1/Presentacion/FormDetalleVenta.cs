using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
public partial class FormDetalleVenta : Form
{
	private readonly VentaNegocio _negocio = new VentaNegocio();
	private readonly int _ventaId;

	private DataTable? _cabecera;
	private DataTable? _detalle;

	public FormDetalleVenta()
	{
		InitializeComponent();
	}

	public FormDetalleVenta(int ventaId) : this()
	{
		_ventaId = ventaId;
	}

	private void FormDetalleVenta_Load(object sender, EventArgs e)
	{
		Cargar();
	}

	private void Cargar()
	{
		if (_ventaId <= 0)
		{
			return;
		}
		try
		{
			_cabecera = _negocio.ObtenerCabecera(_ventaId);
			if (_cabecera.Rows.Count == 0)
			{
				MessageBox.Show("La venta seleccionada no existe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
				return;
			}
			_detalle = _negocio.ObtenerDetalle(_ventaId);
			DataRow fila = _cabecera.Rows[0];
            string cabecera =
				$"Venta #{fila["Venta"]}\n" +
				$"Fecha: {Convert.ToDateTime(fila["Fecha"]):g}\n" +
				$"Cliente: {fila["Cliente"]}\n" +
				$"Empleado: {fila["Empleado"]}\n" +
				$"Usuario: {fila["Usuario"]}\n" +
				$"Medio de pago: {Capitalizar(Convert.ToString(fila["Pago"]))}";
            if (TieneTarjeta(fila))
            {
                cabecera += $"\nTarjeta: {TextoTarjeta(fila)}";
                if (fila["Cuotas"] != DBNull.Value)
                    cabecera += $"\nCuotas: {fila["Cuotas"]}";
            }
            lblCabecera.Text = cabecera;

            dgvDetalle.DataSource = _detalle;

            decimal recargo = ObtenerRecargo(fila);
            lblTotales.Text =
                   $"Subtotal: {ObtenerSubtotal(fila):C2}      " +
				   $"Descuento: {Convert.ToDecimal(fila["Descuento"]):C2}      " +
					(recargo > 0 ? $"Recargo: {recargo:C2}      " : string.Empty) +
				   $"TOTAL: {Convert.ToDecimal(fila["Total"]):C2}";

			// Estado de la venta: si está anulada se muestra el aviso y se
			// deshabilita el botón para que no se pueda anular dos veces.
			bool anulada = fila.Table.Columns.Contains("Anulada") && fila["Anulada"] != DBNull.Value
				&& Convert.ToBoolean(fila["Anulada"]);
			lblAnulada.Visible = anulada;
			btnAnular.Enabled = !anulada;
			btnAnular.Text = anulada ? "Anulada" : "Anular venta";

			if (anulada)
			{
				string aviso = "\n\nVENTA ANULADA";
				if (fila.Table.Columns.Contains("UsuarioAnulacion") && fila["UsuarioAnulacion"] != DBNull.Value)
					aviso += $" por {fila["UsuarioAnulacion"]}";
				if (fila.Table.Columns.Contains("FechaAnulacion") && fila["FechaAnulacion"] != DBNull.Value)
					aviso += $" el {Convert.ToDateTime(fila["FechaAnulacion"]):g}";
				if (fila.Table.Columns.Contains("MotivoAnulacion") && fila["MotivoAnulacion"] != DBNull.Value)
					aviso += $"\nMotivo: {fila["MotivoAnulacion"]}";
				lblCabecera.Text += aviso;
			}
        }
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo cargar el detalle", ex);
		}
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		if (_cabecera == null || _cabecera.Rows.Count == 0)
			return;

		DataRow fila = _cabecera.Rows[0];
		string cliente = Convert.ToString(fila["Cliente"]) ?? string.Empty;
		decimal total = Convert.ToDecimal(fila["Total"]);
		bool cuentaCorriente = Convert.ToString(fila["Pago"]) == "cuenta_corriente";

		using FormAnularVenta dialogo = new FormAnularVenta(_ventaId, cliente, total, cuentaCorriente);
		if (dialogo.ShowDialog(this) != DialogResult.OK)
			return;

		if (Sesion.UsuarioActual == null)
		{
			MessageBox.Show("No hay un usuario en sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}

		try
		{
			_negocio.Anular(_ventaId, Sesion.UsuarioActual.Id, dialogo.Motivo);
			string mensaje = cuentaCorriente
				? "La venta fue anulada. Se devolvió el stock y se revirtió el cargo en la cuenta corriente."
				: "La venta fue anulada y se devolvió el stock de los productos.";
			MessageBox.Show(mensaje, "Venta anulada", MessageBoxButtons.OK, MessageBoxIcon.Information);
			Cargar();
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo anular la venta", ex);
		}
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		if (_cabecera == null || _detalle == null)
			return;
		try
		{
			printPreviewDialog1.ShowDialog(this);
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo imprimir el detalle", ex);
		}
	}

	private void btnTicket_Click(object sender, EventArgs e)
	{
		if (_ventaId <= 0)
			return;
		try
		{
			using FormTicket ticket = new FormTicket(_ventaId);
			ticket.ShowDialog(this);
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo abrir el ticket", ex);
		}
	}

	private void printDocument1_PrintPage(object? sender, PrintPageEventArgs e)
	{
		if (_cabecera == null || _detalle == null)
			return;
		var g = e.Graphics;
		var fuenteTitulo = new Font("Segoe UI", 14, FontStyle.Bold);
		var fuente = new Font("Segoe UI", 10);
		var fuenteNegrita = new Font("Segoe UI", 10, FontStyle.Bold);
		var fila = _cabecera.Rows[0];
		int x = 50;
		int y = 50;
		int ancho = e.PageBounds.Width - 100;

		g.DrawString("DETALLE DE VENTA", fuenteTitulo, Brushes.Black, x, y);
		y += 30;
		g.DrawString($"Venta #{fila["Venta"]}", fuenteNegrita, Brushes.Black, x, y);
		g.DrawString($"Fecha: {Convert.ToDateTime(fila["Fecha"]):g}", fuente, Brushes.Black, x + 200, y);
		y += 20;
		g.DrawString($"Cliente: {fila["Cliente"]}", fuente, Brushes.Black, x, y);
		g.DrawString($"Empleado: {fila["Empleado"]}", fuente, Brushes.Black, x + 250, y);
		y += 20;
		g.DrawString($"Usuario: {fila["Usuario"]}", fuente, Brushes.Black, x, y);
		g.DrawString($"Medio de pago: {Capitalizar(Convert.ToString(fila["Pago"]))}", fuente, Brushes.Black, x + 200, y);
		y += 20;
        if (TieneTarjeta(fila))
        {
            g.DrawString($"Tarjeta: {TextoTarjeta(fila)}", fuente, Brushes.Black, x, y);
            if (fila["Cuotas"] != DBNull.Value)
                g.DrawString($"Cuotas: {fila["Cuotas"]}", fuente, Brushes.Black, x + 300, y);
            y += 20;
        }
        y += 10;

		// Encabezado de columnas del detalle.
		g.DrawString("Ítem", fuenteNegrita, Brushes.Black, x, y);
		g.DrawString("Cant.", fuenteNegrita, Brushes.Black, x + 200, y);
		g.DrawString("Precio", fuenteNegrita, Brushes.Black, x + 260, y);
		g.DrawString("Subtotal", fuenteNegrita, Brushes.Black, x + 340, y);
		y += 20;
		g.DrawLine(Pens.Gray, x, y, x + ancho, y);
		y += 6;

		foreach (DataRow d in _detalle.Rows)
		{
			g.DrawString(Convert.ToString(d["Item"]), fuente, Brushes.Black, x, y);
			g.DrawString(Convert.ToString(d["Cantidad"]), fuente, Brushes.Black, x + 200, y);
			g.DrawString(Convert.ToDecimal(d["Precio"]).ToString("C2"), fuente, Brushes.Black, x + 260, y);
			g.DrawString(Convert.ToDecimal(d["Subtotal"]).ToString("C2"), fuente, Brushes.Black, x + 340, y);
			y += 20;
			if (y > e.PageBounds.Height - 80)
			{
				e.HasMorePages = true;
				return;
			}
		}

		y += 10;
		g.DrawLine(Pens.Gray, x, y, x + ancho, y);
		y += 20;
        decimal recargo = ObtenerRecargo(fila);
        g.DrawString($"Subtotal: {ObtenerSubtotal(fila):C2}", fuenteNegrita, Brushes.Black, x, y);
        g.DrawString($"Descuento: {Convert.ToDecimal(fila["Descuento"]):C2}", fuenteNegrita, Brushes.Black, x + 200, y);
        y += 20;
        if (recargo > 0)
            g.DrawString($"Recargo: {recargo:C2}", fuenteNegrita, Brushes.Black, x, y);
        g.DrawString($"TOTAL: {Convert.ToDecimal(fila["Total"]):C2}", fuenteNegrita, Brushes.Black, x + 200, y);

        fuente.Dispose();
		fuenteNegrita.Dispose();
		fuenteTitulo.Dispose();
	}

	private static string Capitalizar(string? texto)
	{
		if (string.IsNullOrWhiteSpace(texto))
		{
			return string.Empty;
		}
		return char.ToUpper(texto[0]) + texto.Substring(1);
	}
	private static bool TieneTarjeta(DataRow fila) =>
	fila.Table.Columns.Contains("IdTarjeta") && fila["IdTarjeta"] != DBNull.Value;

	private static string TextoTarjeta(DataRow fila) =>
		$"{fila["Banco"]} - {fila["TipoTarjeta"]}";

	private static decimal ObtenerRecargo(DataRow fila) =>
		fila.Table.Columns.Contains("Recargo") && fila["Recargo"] != DBNull.Value
			? Convert.ToDecimal(fila["Recargo"])
            : 0m;
    private static decimal ObtenerSubtotal(DataRow fila) =>
    fila.Table.Columns.Contains("Subtotal") && fila["Subtotal"] != DBNull.Value
        ? Convert.ToDecimal(fila["Subtotal"])
        : 0m;
}
