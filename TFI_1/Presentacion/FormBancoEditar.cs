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
    public partial class FormBancoEditar : Form
    {
        private readonly BancoNegocio _negocio = new();
        private readonly Banco _banco;
        public FormBancoEditar(Banco banco)
        {
            InitializeComponent();
            _banco = banco;
            txtNombre.Text = banco.Nombre;
            chkActivo.Checked = banco.Activo;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var editado = new Banco
            {
                Id = _banco.Id,
                Nombre = txtNombre.Text,
                Activo = chkActivo.Checked
            };
            try
            {
                _negocio.Actualizar(editado);
                DialogResult = DialogResult.OK;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar el banco: " + ex.Message, "Error",
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
