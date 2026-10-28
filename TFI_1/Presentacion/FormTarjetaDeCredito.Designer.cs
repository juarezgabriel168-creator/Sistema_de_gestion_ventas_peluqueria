namespace TFI_1.Presentacion
{
    partial class FormTarjetaDeCredito
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
            tlpTarjetaCredito = new TableLayoutPanel();
            grpBancos = new GroupBox();
            btnBancoEditar = new Button();
            btnBancoEliminar = new Button();
            dgvBancos = new DataGridView();
            btnBancoGuardar = new Button();
            btnBancoDesactivar = new Button();
            btnBancoNuevo = new Button();
            chkBancoActivo = new CheckBox();
            txtBanco = new TextBox();
            lblNombreBanco = new Label();
            grpTarjetas = new GroupBox();
            btnTarjetaEditar = new Button();
            dgvTarjetas = new DataGridView();
            chkSoloActivos = new CheckBox();
            btnTarjetaDesactivar = new Button();
            btnTarjetaGuardar = new Button();
            btnTarjetaNuevo = new Button();
            chkTarjetaActivo = new CheckBox();
            numCuotasMax = new NumericUpDown();
            lblCuotas = new Label();
            numRecargo = new NumericUpDown();
            lblInteres = new Label();
            cboTipo = new ComboBox();
            lblTarjetas = new Label();
            btnCerrar = new Button();
            tlpTarjetaCredito.SuspendLayout();
            grpBancos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBancos).BeginInit();
            grpTarjetas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTarjetas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCuotasMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numRecargo).BeginInit();
            SuspendLayout();
            // 
            // tlpTarjetaCredito
            // 
            tlpTarjetaCredito.ColumnCount = 2;
            tlpTarjetaCredito.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43.18182F));
            tlpTarjetaCredito.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56.81818F));
            tlpTarjetaCredito.Controls.Add(grpBancos, 0, 0);
            tlpTarjetaCredito.Controls.Add(grpTarjetas, 1, 0);
            tlpTarjetaCredito.Controls.Add(chkSoloActivos, 0, 1);
            tlpTarjetaCredito.Controls.Add(btnCerrar, 1, 1);
            tlpTarjetaCredito.Dock = DockStyle.Fill;
            tlpTarjetaCredito.Location = new Point(0, 0);
            tlpTarjetaCredito.Name = "tlpTarjetaCredito";
            tlpTarjetaCredito.Padding = new Padding(12);
            tlpTarjetaCredito.RowCount = 2;
            tlpTarjetaCredito.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpTarjetaCredito.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpTarjetaCredito.Size = new Size(884, 521);
            tlpTarjetaCredito.TabIndex = 0;
            // 
            // grpBancos
            // 
            grpBancos.Controls.Add(btnBancoEditar);
            grpBancos.Controls.Add(btnBancoEliminar);
            grpBancos.Controls.Add(dgvBancos);
            grpBancos.Controls.Add(btnBancoGuardar);
            grpBancos.Controls.Add(btnBancoDesactivar);
            grpBancos.Controls.Add(btnBancoNuevo);
            grpBancos.Controls.Add(chkBancoActivo);
            grpBancos.Controls.Add(txtBanco);
            grpBancos.Controls.Add(lblNombreBanco);
            grpBancos.Dock = DockStyle.Fill;
            grpBancos.Location = new Point(12, 12);
            grpBancos.Margin = new Padding(0);
            grpBancos.Name = "grpBancos";
            grpBancos.Size = new Size(371, 457);
            grpBancos.TabIndex = 0;
            grpBancos.TabStop = false;
            grpBancos.Text = "Bancos";
            // 
            // btnBancoEditar
            // 
            btnBancoEditar.Location = new Point(280, 100);
            btnBancoEditar.Name = "btnBancoEditar";
            btnBancoEditar.Size = new Size(80, 30);
            btnBancoEditar.TabIndex = 8;
            btnBancoEditar.Text = "Editar";
            btnBancoEditar.UseVisualStyleBackColor = true;
            btnBancoEditar.Click += btnBancoEditar_Click;
            // 
            // btnBancoEliminar
            // 
            btnBancoEliminar.Location = new Point(12, 136);
            btnBancoEliminar.Name = "btnBancoEliminar";
            btnBancoEliminar.Size = new Size(80, 30);
            btnBancoEliminar.TabIndex = 9;
            btnBancoEliminar.Text = "Eliminar";
            btnBancoEliminar.UseVisualStyleBackColor = true;
            btnBancoEliminar.Click += btnBancoEliminar_Click;
            // 
            // dgvBancos
            // 
            dgvBancos.AllowUserToAddRows = false;
            dgvBancos.AllowUserToDeleteRows = false;
            dgvBancos.AllowUserToResizeRows = false;
            dgvBancos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvBancos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBancos.BackgroundColor = SystemColors.ButtonHighlight;
            dgvBancos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBancos.Location = new Point(6, 172);
            dgvBancos.MultiSelect = false;
            dgvBancos.Name = "dgvBancos";
            dgvBancos.ReadOnly = true;
            dgvBancos.RowHeadersVisible = false;
            dgvBancos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBancos.Size = new Size(359, 279);
            dgvBancos.TabIndex = 7;
            dgvBancos.SelectionChanged += dgvBancos_SelectionChanged;
            // 
            // btnBancoGuardar
            // 
            btnBancoGuardar.Location = new Point(98, 100);
            btnBancoGuardar.Name = "btnBancoGuardar";
            btnBancoGuardar.Size = new Size(80, 30);
            btnBancoGuardar.TabIndex = 6;
            btnBancoGuardar.Text = "Guardar";
            btnBancoGuardar.UseVisualStyleBackColor = true;
            btnBancoGuardar.Click += btnBancoGuardar_Click;
            // 
            // btnBancoDesactivar
            // 
            btnBancoDesactivar.Location = new Point(184, 100);
            btnBancoDesactivar.Name = "btnBancoDesactivar";
            btnBancoDesactivar.Size = new Size(90, 30);
            btnBancoDesactivar.TabIndex = 5;
            btnBancoDesactivar.Text = "Desactivar";
            btnBancoDesactivar.UseVisualStyleBackColor = true;
            btnBancoDesactivar.Click += btnBancoDesactivar_Click;
            // 
            // btnBancoNuevo
            // 
            btnBancoNuevo.Location = new Point(12, 100);
            btnBancoNuevo.Name = "btnBancoNuevo";
            btnBancoNuevo.Size = new Size(80, 30);
            btnBancoNuevo.TabIndex = 3;
            btnBancoNuevo.Text = "Nuevo";
            btnBancoNuevo.UseVisualStyleBackColor = true;
            btnBancoNuevo.Click += btnBancoNuevo_Click;
            // 
            // chkBancoActivo
            // 
            chkBancoActivo.AutoSize = true;
            chkBancoActivo.Checked = true;
            chkBancoActivo.CheckState = CheckState.Checked;
            chkBancoActivo.Location = new Point(12, 74);
            chkBancoActivo.Name = "chkBancoActivo";
            chkBancoActivo.Size = new Size(60, 19);
            chkBancoActivo.TabIndex = 2;
            chkBancoActivo.Text = "Activo";
            chkBancoActivo.UseVisualStyleBackColor = true;
            // 
            // txtBanco
            // 
            txtBanco.Location = new Point(12, 45);
            txtBanco.MaxLength = 80;
            txtBanco.Name = "txtBanco";
            txtBanco.Size = new Size(316, 23);
            txtBanco.TabIndex = 1;
            // 
            // lblNombreBanco
            // 
            lblNombreBanco.AutoSize = true;
            lblNombreBanco.Location = new Point(12, 25);
            lblNombreBanco.Name = "lblNombreBanco";
            lblNombreBanco.Size = new Size(106, 15);
            lblNombreBanco.TabIndex = 0;
            lblNombreBanco.Text = "Nombre del Banco";
            // 
            // grpTarjetas
            // 
            grpTarjetas.Controls.Add(btnTarjetaEditar);
            grpTarjetas.Controls.Add(dgvTarjetas);
            grpTarjetas.Controls.Add(btnTarjetaDesactivar);
            grpTarjetas.Controls.Add(btnTarjetaGuardar);
            grpTarjetas.Controls.Add(btnTarjetaNuevo);
            grpTarjetas.Controls.Add(chkTarjetaActivo);
            grpTarjetas.Controls.Add(numCuotasMax);
            grpTarjetas.Controls.Add(lblCuotas);
            grpTarjetas.Controls.Add(numRecargo);
            grpTarjetas.Controls.Add(lblInteres);
            grpTarjetas.Controls.Add(cboTipo);
            grpTarjetas.Controls.Add(lblTarjetas);
            grpTarjetas.Dock = DockStyle.Fill;
            grpTarjetas.Location = new Point(389, 12);
            grpTarjetas.Margin = new Padding(6, 0, 0, 0);
            grpTarjetas.Name = "grpTarjetas";
            grpTarjetas.Size = new Size(483, 457);
            grpTarjetas.TabIndex = 1;
            grpTarjetas.TabStop = false;
            grpTarjetas.Text = "Tarjetas";
            // 
            // btnTarjetaEditar
            // 
            btnTarjetaEditar.Cursor = Cursors.SizeAll;
            btnTarjetaEditar.Location = new Point(387, 100);
            btnTarjetaEditar.Name = "btnTarjetaEditar";
            btnTarjetaEditar.Size = new Size(80, 30);
            btnTarjetaEditar.TabIndex = 9;
            btnTarjetaEditar.Text = "Editar";
            btnTarjetaEditar.UseVisualStyleBackColor = true;
            btnTarjetaEditar.Click += btnTarjetaEditar_Click;
            // 
            // dgvTarjetas
            // 
            dgvTarjetas.AllowUserToAddRows = false;
            dgvTarjetas.AllowUserToDeleteRows = false;
            dgvTarjetas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTarjetas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTarjetas.BackgroundColor = SystemColors.ButtonHighlight;
            dgvTarjetas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTarjetas.Location = new Point(6, 136);
            dgvTarjetas.MultiSelect = false;
            dgvTarjetas.Name = "dgvTarjetas";
            dgvTarjetas.ReadOnly = true;
            dgvTarjetas.RowHeadersVisible = false;
            dgvTarjetas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTarjetas.Size = new Size(471, 315);
            dgvTarjetas.TabIndex = 9;
            dgvTarjetas.SelectionChanged += dgvTarjetas_SelectionChanged;
            // 
            // chkSoloActivos
            // 
            chkSoloActivos.Anchor = AnchorStyles.Left;
            chkSoloActivos.AutoSize = true;
            chkSoloActivos.Checked = true;
            chkSoloActivos.CheckState = CheckState.Checked;
            chkSoloActivos.Location = new Point(18, 18);
            chkSoloActivos.Margin = new Padding(6, 0, 0, 0);
            chkSoloActivos.Name = "chkSoloActivos";
            chkSoloActivos.Size = new Size(91, 19);
            chkSoloActivos.TabIndex = 8;
            chkSoloActivos.Text = "Solo Activos";
            chkSoloActivos.UseVisualStyleBackColor = true;
            chkSoloActivos.CheckedChanged += chkSoloActivos_CheckedChanged;
            // 
            // btnTarjetaDesactivar
            // 
            btnTarjetaDesactivar.Location = new Point(184, 100);
            btnTarjetaDesactivar.Name = "btnTarjetaDesactivar";
            btnTarjetaDesactivar.Size = new Size(90, 30);
            btnTarjetaDesactivar.TabIndex = 7;
            btnTarjetaDesactivar.Text = "Desactivar";
            btnTarjetaDesactivar.UseVisualStyleBackColor = true;
            btnTarjetaDesactivar.Click += btnTarjetaDesactivar_Click;
            // 
            // btnTarjetaGuardar
            // 
            btnTarjetaGuardar.Location = new Point(98, 100);
            btnTarjetaGuardar.Name = "btnTarjetaGuardar";
            btnTarjetaGuardar.Size = new Size(80, 30);
            btnTarjetaGuardar.TabIndex = 7;
            btnTarjetaGuardar.Text = "Guardar";
            btnTarjetaGuardar.UseVisualStyleBackColor = true;
            btnTarjetaGuardar.Click += btnTarjetaGuardar_Click;
            // 
            // btnTarjetaNuevo
            // 
            btnTarjetaNuevo.Location = new Point(12, 100);
            btnTarjetaNuevo.Name = "btnTarjetaNuevo";
            btnTarjetaNuevo.Size = new Size(80, 30);
            btnTarjetaNuevo.TabIndex = 7;
            btnTarjetaNuevo.Text = "Nuevo";
            btnTarjetaNuevo.UseVisualStyleBackColor = true;
            btnTarjetaNuevo.Click += btnTarjetaNuevo_Click;
            // 
            // chkTarjetaActivo
            // 
            chkTarjetaActivo.AutoSize = true;
            chkTarjetaActivo.Checked = true;
            chkTarjetaActivo.CheckState = CheckState.Checked;
            chkTarjetaActivo.Location = new Point(410, 47);
            chkTarjetaActivo.Name = "chkTarjetaActivo";
            chkTarjetaActivo.Size = new Size(60, 19);
            chkTarjetaActivo.TabIndex = 6;
            chkTarjetaActivo.Text = "Activo";
            chkTarjetaActivo.UseVisualStyleBackColor = true;
            // 
            // numCuotasMax
            // 
            numCuotasMax.DecimalPlaces = 2;
            numCuotasMax.Location = new Point(290, 45);
            numCuotasMax.Maximum = new decimal(new int[] { 36, 0, 0, 0 });
            numCuotasMax.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCuotasMax.Name = "numCuotasMax";
            numCuotasMax.Size = new Size(100, 23);
            numCuotasMax.TabIndex = 5;
            numCuotasMax.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblCuotas
            // 
            lblCuotas.AutoSize = true;
            lblCuotas.Location = new Point(290, 25);
            lblCuotas.Name = "lblCuotas";
            lblCuotas.Size = new Size(75, 15);
            lblCuotas.TabIndex = 4;
            lblCuotas.Text = "Cuotas max.:";
            // 
            // numRecargo
            // 
            numRecargo.DecimalPlaces = 2;
            numRecargo.Location = new Point(175, 45);
            numRecargo.Name = "numRecargo";
            numRecargo.Size = new Size(100, 23);
            numRecargo.TabIndex = 3;
            // 
            // lblInteres
            // 
            lblInteres.AutoSize = true;
            lblInteres.Location = new Point(175, 25);
            lblInteres.Name = "lblInteres";
            lblInteres.Size = new Size(55, 15);
            lblInteres.TabIndex = 2;
            lblInteres.Text = "% interes";
            // 
            // cboTipo
            // 
            cboTipo.FormattingEnabled = true;
            cboTipo.Items.AddRange(new object[] { "Credito", "Debito" });
            cboTipo.Location = new Point(12, 45);
            cboTipo.MaxLength = 40;
            cboTipo.Name = "cboTipo";
            cboTipo.Size = new Size(150, 23);
            cboTipo.TabIndex = 1;
            // 
            // lblTarjetas
            // 
            lblTarjetas.AutoSize = true;
            lblTarjetas.Location = new Point(12, 25);
            lblTarjetas.Name = "lblTarjetas";
            lblTarjetas.Size = new Size(85, 15);
            lblTarjetas.TabIndex = 0;
            lblTarjetas.Text = "Tipo de Tarjeta";
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Right;
            btnCerrar.Location = new Point(797, 480);
            btnCerrar.Margin = new Padding(0, 6, 0, 0);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(75, 23);
            btnCerrar.TabIndex = 2;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // FormTarjetaDeCredito
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(884, 521);
            Controls.Add(tlpTarjetaCredito);
            Name = "FormTarjetaDeCredito";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bancos y Tarjetas";
            Load += FormTarjetaDeCredito_Load;
            tlpTarjetaCredito.ResumeLayout(false);
            grpBancos.ResumeLayout(false);
            grpBancos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBancos).EndInit();
            grpTarjetas.ResumeLayout(false);
            grpTarjetas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTarjetas).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCuotasMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)numRecargo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpTarjetaCredito;
        private GroupBox grpBancos;
        private GroupBox grpTarjetas;
        private Button btnCerrar;
        private Button btnBancoGuardar;
        private Button btnBancoDesactivar;
        private Button btnBancoNuevo;
        private CheckBox chkBancoActivo;
        private TextBox txtBanco;
        private Label lblNombreBanco;
        private ComboBox cboTipo;
        private Label lblTarjetas;
        private NumericUpDown numCuotasMax;
        private Label lblCuotas;
        private NumericUpDown numRecargo;
        private Label lblInteres;
        private Button btnTarjetaDesactivar;
        private Button btnTarjetaGuardar;
        private Button btnTarjetaNuevo;
        private CheckBox chkTarjetaActivo;
        private DataGridView dgvBancos;
        private DataGridView dgvTarjetas;
        private CheckBox chkSoloActivos;
        private Button btnBancoEditar;
        private Button btnBancoEliminar;
        private Button btnTarjetaEditar;
    }
}