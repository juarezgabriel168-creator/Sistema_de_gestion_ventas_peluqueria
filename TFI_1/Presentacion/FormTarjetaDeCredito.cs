using System;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;
using System.Drawing;

namespace TFI_1.Presentacion;

public partial class FormTarjetaDeCredito : Form
{

    private readonly BancoNegocio _bancos = new();
    private readonly TarjetaCreditoNegocio _tarjetas = new();
    private Banco? _banco;                // banco seleccionado
    private TarjetaCredito? _tarjeta;     // tarjeta en edición
    private bool _cargando;

    public FormTarjetaDeCredito()
    {
        InitializeComponent();
    }

    private void FormTarjetaDeCredito_Load(object? sender, EventArgs e) => CargarBancos();

    // ===================== BANCOS =====================

    private void CargarBancos(int? seleccionarId = null)
    {
        _cargando = true;
        try
        {
            dgvBancos.DataSource = _bancos.Listar(chkSoloActivos.Checked);
            if (dgvBancos.Columns.Contains("Id")) dgvBancos.Columns["Id"].Visible = false;

            if (seleccionarId.HasValue)
                foreach (DataGridViewRow fila in dgvBancos.Rows)
                    if (fila.DataBoundItem is Banco b && b.Id == seleccionarId)
                    {
                        dgvBancos.CurrentCell = fila.Cells["Nombre"];
                        break;
                    }
        }
        catch (Exception ex) { Error("No se pudieron cargar los bancos: ", ex); }
        finally { _cargando = false; }

        BancoCambiado();
    }

    private void BancoCambiado()
    {
        _banco = dgvBancos.CurrentRow?.DataBoundItem as Banco;
        txtBanco.Text = _banco?.Nombre ?? string.Empty;
        chkBancoActivo.Checked = _banco?.Activo ?? true;
        btnBancoGuardar.Text = _banco == null ? "Guardar" : "Actualizar";
        btnBancoDesactivar.Text = (_banco != null && !_banco.Activo) ? "Reactivar" : "Desactivar";
        grpTarjetas.Text = _banco == null ? "Tipos de tarjeta" : $"Tipos de tarjeta de {_banco.Nombre}";
        grpTarjetas.Enabled = _banco != null;
        CargarTarjetas();
    }

    private void dgvBancos_SelectionChanged(object? sender, EventArgs e)
    {
        if (!_cargando) BancoCambiado();
    }

    private void btnBancoNuevo_Click(object? sender, EventArgs e)
    {
        _cargando = true;
        dgvBancos.CurrentCell = null;
        dgvBancos.ClearSelection();
        _cargando = false;
        BancoCambiado();
        txtBanco.Focus();
    }

    private void btnBancoGuardar_Click(object? sender, EventArgs e)
    {
        var banco = new Banco
        {
            Id = _banco?.Id ?? 0,
            Nombre = txtBanco.Text,
            Activo = chkBancoActivo.Checked
        };
        try
        {
            int id = banco.Id == 0 ? _bancos.Insertar(banco) : banco.Id;
            if (banco.Id != 0) _bancos.Actualizar(banco);
            CargarBancos(id);
        }
        catch (ArgumentException ex) { Aviso(ex.Message); }
        catch (Exception ex) { Error("No se pudo guardar el banco: ", ex); }
    }

    private void btnBancoDesactivar_Click(object? sender, EventArgs e)
    {
        if (_banco == null) { Aviso("Seleccione un banco de la lista."); return; }
        bool reactivar = !_banco.Activo;
        string pregunta = reactivar
            ? $"¿Reactivar '{_banco.Nombre}' y todas sus tarjetas?"
            : $"¿Desactivar '{_banco.Nombre}' y todas sus tarjetas?";
        if (MessageBox.Show(pregunta, "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        try
        {
            if (reactivar) _bancos.Reactivar(_banco.Id);
            else _bancos.Desactivar(_banco.Id);
            CargarBancos();
        }
        catch (Exception ex) { Error("No se pudo actualizar el banco: ", ex); }
    }

    // ===================== TARJETAS =====================

    private void CargarTarjetas()
    {
        _cargando = true;
        try
        {
            dgvTarjetas.DataSource = _banco == null
                ? null
                : _tarjetas.ListarPorBanco(_banco.Id, chkSoloActivos.Checked);

            foreach (var col in new[] { "Id", "IdBanco", "NombreBanco", "DescripcionCorta", "FechaAlta" })
                if (dgvTarjetas.Columns.Contains(col)) dgvTarjetas.Columns[col].Visible = false;
            if (dgvTarjetas.Columns.Contains("TipoTarjeta")) dgvTarjetas.Columns["TipoTarjeta"].HeaderText = "Tipo";
            if (dgvTarjetas.Columns.Contains("PorcentajeRecargo"))
            {
                dgvTarjetas.Columns["PorcentajeRecargo"].HeaderText = "% Interés";
                dgvTarjetas.Columns["PorcentajeRecargo"].DefaultCellStyle.Format = "N2";
            }
            if (dgvTarjetas.Columns.Contains("CuotasMax")) dgvTarjetas.Columns["CuotasMax"].HeaderText = "Cuotas máx.";
        }
        catch (Exception ex) { Error("No se pudieron cargar las tarjetas: ", ex); }
        finally { _cargando = false; }

        NuevaTarjeta();
    }

    private void TarjetaCambiada()
    {
        _tarjeta = dgvTarjetas.CurrentRow?.DataBoundItem as TarjetaCredito;
        cboTipo.Text = _tarjeta?.TipoTarjeta ?? string.Empty;
        numRecargo.Value = _tarjeta?.PorcentajeRecargo ?? 0;
        numCuotasMax.Value = _tarjeta?.CuotasMax ?? 1;
        chkTarjetaActivo.Checked = _tarjeta?.Activo ?? true;
        btnTarjetaGuardar.Text = _tarjeta == null ? "Guardar" : "Actualizar";
        btnTarjetaDesactivar.Text = (_tarjeta != null && !_tarjeta.Activo) ? "Reactivar" : "Desactivar";
    }

    private void NuevaTarjeta()
    {
        _cargando = true;
        dgvTarjetas.CurrentCell = null;
        dgvTarjetas.ClearSelection();
        _cargando = false;
        TarjetaCambiada();
    }

    private void dgvTarjetas_SelectionChanged(object? sender, EventArgs e)
    {
        if (!_cargando) TarjetaCambiada();
    }

    private void btnTarjetaNuevo_Click(object? sender, EventArgs e)
    {
        NuevaTarjeta();
        cboTipo.Focus();
    }

    private void btnTarjetaGuardar_Click(object? sender, EventArgs e)
    {
        if (_banco == null) { Aviso("Seleccione un banco."); return; }
        var tarjeta = new TarjetaCredito
        {
            Id = _tarjeta?.Id ?? 0,
            IdBanco = _banco.Id,
            TipoTarjeta = cboTipo.Text,
            PorcentajeRecargo = numRecargo.Value,
            CuotasMax = (int)numCuotasMax.Value,
            Activo = chkTarjetaActivo.Checked
        };
        try
        {
            if (tarjeta.Id == 0) _tarjetas.Insertar(tarjeta);
            else _tarjetas.Actualizar(tarjeta);
            CargarTarjetas();
        }
        catch (ArgumentException ex) { Aviso(ex.Message); }
        catch (Exception ex) { Error("No se pudo guardar la tarjeta: ", ex); }
    }

    private void btnTarjetaDesactivar_Click(object? sender, EventArgs e)
    {
        if (_tarjeta == null) { Aviso("Seleccione una tarjeta de la lista."); return; }
        bool reactivar = !_tarjeta.Activo;
        string pregunta = reactivar
            ? $"¿Reactivar la tarjeta '{_tarjeta.TipoTarjeta}'?"
            : $"¿Desactivar la tarjeta '{_tarjeta.TipoTarjeta}'?";
        if (MessageBox.Show(pregunta, "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        try
        {
            if (reactivar) _tarjetas.Reactivar(_tarjeta.Id);
            else _tarjetas.Desactivar(_tarjeta.Id);
            CargarTarjetas();
        }
        catch (Exception ex) { Error("No se pudo actualizar la tarjeta: ", ex); }
    }

    // ===================== COMÚN =====================

    private void chkSoloActivos_CheckedChanged(object? sender, EventArgs e)
    {
        if (!_cargando) CargarBancos(_banco?.Id);
    }
    private void btnCerrar_Click(object? sender, EventArgs e)
    {
        var respuesta = MessageBox.Show("¿Desea salir?", "Salir",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (respuesta == DialogResult.Yes)
            Close();
    }

    private static void Aviso(string mensaje) =>
        MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

    private static void Error(string prefijo, Exception ex) =>
        Mensajes.Error(null, prefijo, ex);

    private void btnBancoEditar_Click(object sender, EventArgs e)
    {
        if (_banco == null) { Aviso("Seleccione un banco de la lista."); return; }
        int id = _banco.Id;
        using var form = new FormBancoEditar(_banco);
        if (form.ShowDialog(this) == DialogResult.OK)
            CargarBancos(id);
    }

    private void btnTarjetaEditar_Click(object sender, EventArgs e)
    {
        var tarjeta = _tarjeta;
        if (tarjeta == null) { Aviso("Seleccione una tarjeta de la lista."); return; }
        using var form = new FormTarjetaEditar(tarjeta);
        if (form.ShowDialog(this) == DialogResult.OK)
            CargarTarjetas();
    }
}