using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

public partial class FormHistorialVentas : Form
{
    private readonly VentaNegocio _ventaNegocio = new VentaNegocio();
    private readonly UsuarioNegocio _usuarioNegocio = new UsuarioNegocio();
    private readonly EmpleadoNegocio _empleadoNegocio = new EmpleadoNegocio();

    private const int TamanoPagina = 50;
    private int _pagina;
    private int _totalRegistros;

    public FormHistorialVentas()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Asigna un valor a un DateTimePicker de forma segura, ampliando primero
    /// su rango MinDate/MaxDate para evitar ArgumentOutOfRangeException.
    /// </summary>
    private static void SetDateTimePickerSafe(DateTimePicker dtp, DateTime value)
    {
        try
        {
            dtp.MinDate = new DateTime(1900, 1, 1);
            dtp.MaxDate = new DateTime(2100, 12, 31);
            dtp.Value = value;
        }
        catch
        {
            // Si aun así falla, dejamos el valor por defecto del control
        }
    }

    private void FormHistorialVentas_Load(object sender, EventArgs e)
    {
        try
        {
            DateTime hoy = DateTime.Today;

            // Fechas seguras: rango ampliado para evitar ArgumentOutOfRangeException
            SetDateTimePickerSafe(dtpDesde, hoy.AddDays(-30));
            SetDateTimePickerSafe(dtpHasta, hoy);

            List<Usuario> usuarios = new List<Usuario>
            {
                new Usuario { Id = 0, NombreUsuario = "Todos" }
            };
            usuarios.AddRange(_usuarioNegocio.Listar());
            cboUsuario.DataSource = usuarios;
            cboUsuario.DisplayMember = "NombreUsuario";
            cboUsuario.ValueMember = "Id";
            if (cboUsuario.Items.Count > 0) cboUsuario.SelectedIndex = 0;

            List<Empleado> empleados = new List<Empleado> { new Empleado { Id = 0, Nombre = "Todos", Apellido = "" } };
            empleados.AddRange(_empleadoNegocio.Listar().Where((Empleado em) => em.Activo).ToList());
            cboEmpleado.DataSource = empleados;
            cboEmpleado.DisplayMember = "NombreCompleto";
            cboEmpleado.ValueMember = "Id";
            if (cboEmpleado.Items.Count > 0) cboEmpleado.SelectedIndex = 0;

            cboEmpleado.SelectedIndexChanged += (s, ev) => Buscar();
            txtNumeroVenta.TextChanged += (s, ev) => Buscar();

            Buscar();
        }
        catch (Exception ex)
        {
            Mensajes.Error(this, "No se pudo iniciar el historial de ventas", ex);
        }
    }

    private void Buscar()
    {
        _pagina = 0;
        CargarPagina();
    }

    // Trae solo la página actual y cuenta el total con los mismos filtros.
    private void CargarPagina()
    {
        // Si los controles dinámicos aún no se inicializaron (formulario no cargado completamente), salir.
        if (txtNumeroVenta == null || cboEmpleado == null || cboUsuario == null || dgvVentas == null)
            return;

        if (!Validaciones.RangoDeFechasValido(dtpDesde.Value, dtpHasta.Value))
        {
            MessageBox.Show("La fecha 'Desde' no puede ser mayor que 'Hasta'.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return;
        }
        try
        {
            int? usuarioId = (cboUsuario.SelectedItem is Usuario usuario && usuario.Id > 0) ? usuario.Id : (int?)null;
            int? empleadoId = (cboEmpleado.SelectedItem is Empleado empleado && empleado.Id > 0) ? empleado.Id : (int?)null;
            int? ventaId = null;
            if (!string.IsNullOrWhiteSpace(txtNumeroVenta.Text) && int.TryParse(txtNumeroVenta.Text.Trim(), out int nro))
                ventaId = nro;

            _totalRegistros = _ventaNegocio.Contar(dtpDesde.Value, dtpHasta.Value, usuarioId, empleadoId, ventaId);
            dgvVentas.DataSource = _ventaNegocio.Listar(dtpDesde.Value, dtpHasta.Value, usuarioId, empleadoId, ventaId,
                TamanoPagina, _pagina * TamanoPagina);
            FormatearGrid();
            ActualizarPaginacion();
        }
        catch (Exception ex)
        {
            Mensajes.Error(this, "No se pudo cargar el historial", ex);
        }
    }

    private void ActualizarPaginacion()
    {
        int paginas = Math.Max(1, (int)Math.Ceiling(_totalRegistros / (double)TamanoPagina));
        if (_pagina >= paginas)
        {
            _pagina = paginas - 1;
        }

        lblPagina.Text = $"Página {_pagina + 1} de {paginas} — {_totalRegistros} venta(s)";
        btnAnterior.Enabled = _pagina > 0;
        btnSiguiente.Enabled = _pagina + 1 < paginas;
    }

    private void btnAnterior_Click(object sender, EventArgs e)
    {
        if (_pagina == 0)
        {
            return;
        }
        _pagina--;
        CargarPagina();
    }

    private void btnSiguiente_Click(object sender, EventArgs e)
    {
        if (!btnSiguiente.Enabled)
        {
            return;
        }
        _pagina++;
        CargarPagina();
    }
    private void FormatearGrid()
    {
        // Por si la grilla todavía recibe esa columna desde otra consulta.
        if (dgvVentas.Columns.Contains("IdTarjeta"))
            dgvVentas.Columns["IdTarjeta"]!.Visible = false;

        if (dgvVentas.Columns.Contains("Venta"))
            dgvVentas.Columns["Venta"]!.HeaderText = "Nº venta";
        if (dgvVentas.Columns.Contains("Fecha"))
            dgvVentas.Columns["Fecha"]!.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        if (dgvVentas.Columns.Contains("Pago"))
            dgvVentas.Columns["Pago"]!.HeaderText = "Medio de pago";

        foreach (string columna in new[] { "Subtotal", "Descuento", "Recargo", "Total" })
            if (dgvVentas.Columns.Contains(columna))
                dgvVentas.Columns[columna]!.DefaultCellStyle.Format = "C2";

        DataGridViewColumn? columnaAnulada = dgvVentas.Columns["Anulada"];
        if (columnaAnulada != null)
        {
            columnaAnulada.HeaderText = "Anulada";
            // Las ventas anuladas se muestran en gris para distinguirlas.
            foreach (DataGridViewRow fila in dgvVentas.Rows)
            {
                object? valor = fila.Cells["Anulada"].Value;
                if (valor != null && valor != DBNull.Value && Convert.ToBoolean(valor))
                {
                    fila.DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
        }
    }

    private void btnBuscar_Click(object sender, EventArgs e)
    {
        Buscar();
    }

    private void btnRefrescar_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime hoy = DateTime.Today;
            SetDateTimePickerSafe(dtpDesde, hoy.AddDays(-30));
            SetDateTimePickerSafe(dtpHasta, hoy);

            if (cboUsuario.Items.Count > 0) cboUsuario.SelectedIndex = 0;
            if (cboEmpleado.Items.Count > 0) cboEmpleado.SelectedIndex = 0;
            txtNumeroVenta.Clear();
            Buscar();
        }
        catch (Exception ex)
        {
            Mensajes.Error(this, "No se pudo refrescar el historial", ex);
        }
    }

    private void btnVerDetalle_Click(object sender, EventArgs e)
    {
        if (dgvVentas.CurrentRow == null)
        {
            MessageBox.Show("Seleccione una venta de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return;
        }
        try
        {
            int idVenta = Convert.ToInt32(dgvVentas.CurrentRow.Cells["Venta"].Value);
            using FormDetalleVenta formulario = new FormDetalleVenta(idVenta);
            formulario.ShowDialog(this);
            // Puede haberse anulado la venta desde el detalle: se refresca la lista.
            Buscar();
        }
        catch (Exception ex)
        {
            Mensajes.Error(this, "No se pudo abrir el detalle", ex);
        }
    }
}
