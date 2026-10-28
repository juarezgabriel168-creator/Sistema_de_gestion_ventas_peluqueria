namespace TFI_1.Presentacion
{
    partial class FormTarjetaEditar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblBanco = new Label();
            txtBanco = new TextBox();
            lblTipo = new Label();
            cboTipo = new ComboBox();
            lblRecargo = new Label();
            numRecargo = new NumericUpDown();
            lblCuotas = new Label();
            numCuotasMax = new NumericUpDown();
            chkActivo = new CheckBox();
            btnGardar = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)numRecargo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCuotasMax).BeginInit();
            SuspendLayout();
            // 
            // lblBanco
            // 
            lblBanco.AutoSize = true;
            lblBanco.Location = new Point(14, 16);
            lblBanco.Name = "lblBanco";
            lblBanco.Size = new Size(40, 15);
            lblBanco.TabIndex = 0;
            lblBanco.Text = "Banco";
            // 
            // txtBanco
            // 
            txtBanco.Location = new Point(16, 34);
            txtBanco.Name = "txtBanco";
            txtBanco.ReadOnly = true;
            txtBanco.Size = new Size(316, 23);
            txtBanco.TabIndex = 1;
            txtBanco.TabStop = false;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(16, 68);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(85, 15);
            lblTipo.TabIndex = 1;
            lblTipo.Text = "Tipo de Tarjeta";
            // 
            // cboTipo
            // 
            cboTipo.FormattingEnabled = true;
            cboTipo.Items.AddRange(new object[] { "Credito", "Debito" });
            cboTipo.Location = new Point(16, 86);
            cboTipo.MaxLength = 40;
            cboTipo.Name = "cboTipo";
            cboTipo.Size = new Size(316, 23);
            cboTipo.TabIndex = 2;
            // 
            // lblRecargo
            // 
            lblRecargo.AutoSize = true;
            lblRecargo.Location = new Point(16, 120);
            lblRecargo.Name = "lblRecargo";
            lblRecargo.Size = new Size(58, 15);
            lblRecargo.TabIndex = 3;
            lblRecargo.Text = "% Interes:";
            // 
            // numRecargo
            // 
            numRecargo.DecimalPlaces = 2;
            numRecargo.Location = new Point(16, 138);
            numRecargo.Name = "numRecargo";
            numRecargo.Size = new Size(150, 23);
            numRecargo.TabIndex = 4;
            // 
            // lblCuotas
            // 
            lblCuotas.AutoSize = true;
            lblCuotas.Location = new Point(182, 120);
            lblCuotas.Name = "lblCuotas";
            lblCuotas.Size = new Size(75, 15);
            lblCuotas.TabIndex = 5;
            lblCuotas.Text = "Cuotas max.:";
            // 
            // numCuotasMax
            // 
            numCuotasMax.Location = new Point(182, 138);
            numCuotasMax.Maximum = new decimal(new int[] { 36, 0, 0, 0 });
            numCuotasMax.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCuotasMax.Name = "numCuotasMax";
            numCuotasMax.Size = new Size(150, 23);
            numCuotasMax.TabIndex = 6;
            numCuotasMax.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.Location = new Point(16, 174);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(60, 19);
            chkActivo.TabIndex = 7;
            chkActivo.Text = "Activo";
            chkActivo.UseVisualStyleBackColor = true;
            // 
            // btnGardar
            // 
            btnGardar.Location = new Point(167, 173);
            btnGardar.Name = "btnGardar";
            btnGardar.Size = new Size(80, 30);
            btnGardar.TabIndex = 8;
            btnGardar.Text = "Guardar";
            btnGardar.UseVisualStyleBackColor = true;
            btnGardar.Click += btnGardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(253, 173);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(80, 30);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FormTarjetaEditar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(344, 215);
            Controls.Add(btnCancelar);
            Controls.Add(btnGardar);
            Controls.Add(chkActivo);
            Controls.Add(numCuotasMax);
            Controls.Add(lblCuotas);
            Controls.Add(numRecargo);
            Controls.Add(lblRecargo);
            Controls.Add(cboTipo);
            Controls.Add(lblTipo);
            Controls.Add(txtBanco);
            Controls.Add(lblBanco);
            Name = "FormTarjetaEditar";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Editar Tarjeta";
            ((System.ComponentModel.ISupportInitialize)numRecargo).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCuotasMax).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblBanco;
        private TextBox txtBanco;
        private Label lblTipo;
        private ComboBox cboTipo;
        private Label lblRecargo;
        private NumericUpDown numRecargo;
        private Label lblCuotas;
        private NumericUpDown numCuotasMax;
        private CheckBox chkActivo;
        private Button btnGardar;
        private Button btnCancelar;
    }
}