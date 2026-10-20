using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion
{
    public partial class FormTarjetaEditar : Form
    {
        private readonly TarjetaCreditoNegocio _negocio = new();
        private readonly TarjetaCredito _tarjeta;

        public FormTarjetaEditar(TarjetaCredito tarjeta)
        {
            InitializeComponent();
            _tarjeta = tarjeta;
            txtBanco.Text = tarjeta.NombreBanco;
            cboTipo.Text = tarjeta.TipoTarjeta;
            numRecargo.Value = Math.Clamp(tarjeta.PorcentajeRecargo, numRecargo.Minimum, numRecargo.Maximum);
            numCuotasMax.Value = Math.Clamp(tarjeta.CuotasMax, numCuotasMax.Minimum, numCuotasMax.Maximum);
            chkActivo.Checked = tarjeta.Activo;
        }

        private void btnGardar_Click(object sender, EventArgs e)
        {
            var editada = new TarjetaCredito
            {
                Id = _tarjeta.Id,
                IdBanco = _tarjeta.IdBanco,
                TipoTarjeta = cboTipo.Text,
                PorcentajeRecargo = numRecargo.Value,
                CuotasMax = (int)numCuotasMax.Value,
                Activo = chkActivo.Checked
            };
            try
            {
                _negocio.Actualizar(editada);
                DialogResult = DialogResult.OK;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar la tarjeta: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var respuesta = MessageBox.Show("¿Descartar los cambios?", "Cancelar",
     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
                DialogResult = DialogResult.Cancel;
        }
    }
}
