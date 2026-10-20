using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

/// <summary>
/// Historial de ingresos de mercadería: consulta los ingresos por rango de
/// fechas y proveedor, muestra el detalle de cada uno y el total del período.
/// </summary>
public partial class FormHistorialMercaderia : Form
{
	private readonly IngresoMercaderiaNegocio _negocio = new();
	private bool _cargando;

	private const int TamanoPagina = 50;
	private int _pagina;
	private int _totalRegistros;

	public FormHistorialMercaderia()
	{
		InitializeComponent();
	}

	private void FormHistorialMercaderia_Load(object? sender, EventArgs e)
	{
		_cargando = true;
		try
		{
			AjustarRangoFechas(dtpDesde, DateTime.Today.AddDays(-30));
			AjustarRangoFechas(dtpHasta, DateTime.Today);
		}
		finally
		{
			_cargando = false;
		}

		Cargar();
	}

	/// <summary>
	/// Asigna una fecha a un DateTimePicker ampliando antes su rango MinDate/MaxDate,
	/// para evitar ArgumentOutOfRangeException si el valor queda fuera de rango.
	/// </summary>
	private static void AjustarRangoFechas(DateTimePicker dtp, DateTime valor)
	{
		try
		{
			dtp.MinDate = new DateTime(1900, 1, 1);
			dtp.MaxDate = new DateTime(2100, 12, 31);
			dtp.Value = valor;
		}
		catch
		{
			// Si aun así falla, se deja el valor por defecto del control.
		}
	}

	private void Cargar()
	{
		_pagina = 0;
		CargarPagina();
	}

	// Trae solo la página actual; el total y la suma se calculan sobre todo el período.
	private void CargarPagina()
	{
		if (!Validaciones.RangoDeFechasValido(dtpDesde.Value, dtpHasta.Value))
		{
			MessageBox.Show("La fecha 'Desde' no puede ser mayor que 'Hasta'.", "Aviso",
				MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}

		try
		{
			_cargando = true;
			string filtro = txtBuscar.Text.Trim();
			_totalRegistros = _negocio.Contar(dtpDesde.Value.Date, dtpHasta.Value.Date, filtro);
			List<IngresoMercaderia> ingresos =
				_negocio.Listar(dtpDesde.Value.Date, dtpHasta.Value.Date, filtro,
					TamanoPagina, _pagina * TamanoPagina);
			dgvIngresos.DataSource = ingresos;
			FormatearIngresos();
			lblCantidad.Text = $"Ingresos: {_totalRegistros}";
			lblTotal.Text = $"Total del período: {_negocio.Sumar(dtpDesde.Value.Date, dtpHasta.Value.Date, filtro):C2}";
			ActualizarPaginacion();
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo cargar el historial", ex);
		}
		finally
		{
			_cargando = false;
		}

		CargarDetalle();
	}

	private void ActualizarPaginacion()
	{
		int paginas = Math.Max(1, (int)Math.Ceiling(_totalRegistros / (double)TamanoPagina));
		if (_pagina >= paginas)
		{
			_pagina = paginas - 1;
		}

		lblPagina.Text = $"Página {_pagina + 1} de {paginas}";
		btnAnterior.Enabled = _pagina > 0;
		btnSiguiente.Enabled = _pagina + 1 < paginas;
	}

	private void btnAnterior_Click(object? sender, EventArgs e)
	{
		if (_pagina == 0)
		{
			return;
		}
		_pagina--;
		CargarPagina();
	}

	private void btnSiguiente_Click(object? sender, EventArgs e)
	{
		if (!btnSiguiente.Enabled)
		{
			return;
		}
		_pagina++;
		CargarPagina();
	}

	private void FormatearIngresos()
	{
		OcultarColumna(dgvIngresos, "Id");
		OcultarColumna(dgvIngresos, "IdUsuario");
		OcultarColumna(dgvIngresos, "Detalles");
		FormatearColumna(dgvIngresos, "Fecha", "Fecha", "dd/MM/yyyy");
		FormatearColumna(dgvIngresos, "Proveedor", "Proveedor");
		FormatearColumna(dgvIngresos, "Total", "Total", "C2");
		FormatearColumna(dgvIngresos, "NombreUsuario", "Usuario");
		FormatearColumna(dgvIngresos, "Observacion", "Observación");
	}

	private void CargarDetalle()
	{
		dgvDetalle.DataSource = null;
		lblDetalle.Text = "Detalle del ingreso seleccionado:";

		if (dgvIngresos.CurrentRow?.DataBoundItem is not IngresoMercaderia ingreso)
			return;

		try
		{
			IngresoMercaderia? completo = _negocio.ObtenerPorId(ingreso.Id);
			if (completo == null)
				return;

			dgvDetalle.DataSource = completo.Detalles
				.Select(d => new
				{
					Producto = d.Item.Nombre,
					d.Cantidad,
					d.CostoUnitario,
					d.Subtotal
				})
				.ToList();
			FormatearColumna(dgvDetalle, "Producto", "Producto");
			FormatearColumna(dgvDetalle, "Cantidad", "Cantidad");
			FormatearColumna(dgvDetalle, "CostoUnitario", "Costo unitario", "C2");
			FormatearColumna(dgvDetalle, "Subtotal", "Subtotal", "C2");

			lblDetalle.Text =
				$"Detalle del ingreso #{completo.Id} — {completo.Proveedor} — {completo.Fecha:dd/MM/yyyy}";
		}
		catch (Exception ex)
		{
			// No se muestra un diálogo para no interrumpir cada cambio de fila.
			lblDetalle.Text = "No se pudo cargar el detalle: " + ex.Message;
		}
	}

	private void dgvIngresos_SelectionChanged(object? sender, EventArgs e)
	{
		if (!_cargando)
			CargarDetalle();
	}

	private void btnBuscar_Click(object? sender, EventArgs e)
	{
		Cargar();
	}

	private void btnRefrescar_Click(object? sender, EventArgs e)
	{
		_cargando = true;
		try
		{
			AjustarRangoFechas(dtpDesde, DateTime.Today.AddDays(-30));
			AjustarRangoFechas(dtpHasta, DateTime.Today);
		}
		finally
		{
			_cargando = false;
		}
		txtBuscar.Clear();
		Cargar();
	}

	private void btnNuevo_Click(object? sender, EventArgs e)
	{
		using (FormIngresoMercaderia formulario = new FormIngresoMercaderia())
		{
			formulario.ShowDialog(this);
		}
		Cargar();
	}

	private void btnCerrar_Click(object? sender, EventArgs e)
	{
		Close();
	}

	private void dtpDesde_ValueChanged(object? sender, EventArgs e)
	{
		if (!_cargando)
			Cargar();
	}

	private void dtpHasta_ValueChanged(object? sender, EventArgs e)
	{
		if (!_cargando)
			Cargar();
	}

	private void txtBuscar_TextChanged(object? sender, EventArgs e)
	{
		if (!_cargando)
			Cargar();
	}

	private static void OcultarColumna(DataGridView grid, string nombre)
	{
		if (!grid.Columns.Contains(nombre))
			return;

		DataGridViewColumn? columna = grid.Columns[nombre];
		if (columna != null)
			columna.Visible = false;
	}

	private static void FormatearColumna(DataGridView grid, string nombre, string encabezado, string? formato = null)
	{
		if (!grid.Columns.Contains(nombre))
			return;

		DataGridViewColumn? columna = grid.Columns[nombre];
		if (columna == null)
			return;

		columna.HeaderText = encabezado;
		if (formato != null)
			columna.DefaultCellStyle.Format = formato;
	}
}
