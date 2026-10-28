using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;
partial class FormGastos : Form
{
	private IContainer components = null;

	private TableLayoutPanel tlpPrincipal;
	private Label lblTitulo;
	private TableLayoutPanel tlpFiltros;
	private Label lblDesde;
	private DateTimePicker dtpDesde;
	private Label lblHasta;
	private DateTimePicker dtpHasta;
	private Label lblBuscar;
	private TextBox txtBuscar;
	private Button btnBuscar;
	private Button btnRefrescar;
	private GroupBox grpDatos;
	private TableLayoutPanel tlpDatos;
	private Label lblFecha;
	private DateTimePicker dtpFecha;
	private Label lblConcepto;
	private TextBox txtConcepto;
	private Label lblMonto;
	private NumericUpDown numMonto;
	private Label lblCategoria;
	private TextBox txtCategoria;
	private Label lblObservacion;
	private TextBox txtObservacion;
	private Panel pnlBotones;
	private Button btnNuevo;
	private Button btnGuardar;
	private Button btnEliminar;
	private Button btnCerrar;
	private Label lblTotal;
	private DataGridView dgvGastos;

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
			components.Dispose();
		base.Dispose(disposing);
	}

    private void InitializeComponent()
    {
        tlpPrincipal = new TableLayoutPanel();
        lblTitulo = new Label();
        tlpFiltros = new TableLayoutPanel();
        lblDesde = new Label();
        dtpDesde = new DateTimePicker();
        lblHasta = new Label();
        dtpHasta = new DateTimePicker();
        lblBuscar = new Label();
        txtBuscar = new TextBox();
        btnBuscar = new Button();
        btnRefrescar = new Button();
        grpDatos = new GroupBox();
        tlpDatos = new TableLayoutPanel();
        lblFecha = new Label();
        dtpFecha = new DateTimePicker();
        lblConcepto = new Label();
        txtConcepto = new TextBox();
        lblMonto = new Label();
        numMonto = new NumericUpDown();
        lblCategoria = new Label();
        txtCategoria = new TextBox();
        lblObservacion = new Label();
        txtObservacion = new TextBox();
        pnlBotones = new Panel();
        btnNuevo = new Button();
        btnGuardar = new Button();
        btnEliminar = new Button();
        btnCerrar = new Button();
        lblTotal = new Label();
        dgvGastos = new DataGridView();
        tlpPrincipal.SuspendLayout();
        tlpFiltros.SuspendLayout();
        grpDatos.SuspendLayout();
        tlpDatos.SuspendLayout();
        ((ISupportInitialize)numMonto).BeginInit();
        pnlBotones.SuspendLayout();
        ((ISupportInitialize)dgvGastos).BeginInit();
        SuspendLayout();
        // 
        // tlpPrincipal
        // 
        tlpPrincipal.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tlpPrincipal.ColumnCount = 1;
        tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tlpPrincipal.Controls.Add(lblTitulo, 0, 0);
        tlpPrincipal.Controls.Add(tlpFiltros, 0, 1);
        tlpPrincipal.Controls.Add(grpDatos, 0, 2);
        tlpPrincipal.Controls.Add(pnlBotones, 0, 3);
        tlpPrincipal.Controls.Add(dgvGastos, 0, 4);
        tlpPrincipal.Location = new Point(12, 12);
        tlpPrincipal.Name = "tlpPrincipal";
        tlpPrincipal.RowCount = 5;
        tlpPrincipal.RowStyles.Add(new RowStyle());
        tlpPrincipal.RowStyles.Add(new RowStyle());
        tlpPrincipal.RowStyles.Add(new RowStyle());
        tlpPrincipal.RowStyles.Add(new RowStyle());
        tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tlpPrincipal.Size = new Size(860, 536);
        tlpPrincipal.TabIndex = 0;
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.Location = new Point(3, 0);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Padding = new Padding(0, 0, 0, 8);
        lblTitulo.Size = new Size(71, 33);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Gastos";
        // 
        // tlpFiltros
        // 
        tlpFiltros.ColumnCount = 8;
        tlpFiltros.ColumnStyles.Add(new ColumnStyle());
        tlpFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tlpFiltros.ColumnStyles.Add(new ColumnStyle());
        tlpFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tlpFiltros.ColumnStyles.Add(new ColumnStyle());
        tlpFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tlpFiltros.ColumnStyles.Add(new ColumnStyle());
        tlpFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        tlpFiltros.Controls.Add(lblDesde, 0, 0);
        tlpFiltros.Controls.Add(dtpDesde, 1, 0);
        tlpFiltros.Controls.Add(lblHasta, 2, 0);
        tlpFiltros.Controls.Add(dtpHasta, 3, 0);
        tlpFiltros.Controls.Add(lblBuscar, 4, 0);
        tlpFiltros.Controls.Add(txtBuscar, 5, 0);
        tlpFiltros.Controls.Add(btnBuscar, 6, 0);
        tlpFiltros.Controls.Add(btnRefrescar, 7, 0);
        tlpFiltros.Dock = DockStyle.Fill;
        tlpFiltros.Location = new Point(3, 36);
        tlpFiltros.Name = "tlpFiltros";
        tlpFiltros.RowCount = 1;
        tlpFiltros.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        tlpFiltros.Size = new Size(854, 32);
        tlpFiltros.TabIndex = 1;
        // 
        // lblDesde
        // 
        lblDesde.Anchor = AnchorStyles.None;
        lblDesde.AutoSize = true;
        lblDesde.Location = new Point(3, 8);
        lblDesde.Name = "lblDesde";
        lblDesde.Size = new Size(42, 15);
        lblDesde.TabIndex = 0;
        lblDesde.Text = "Desde:";
        // 
        // dtpDesde
        // 
        dtpDesde.Format = DateTimePickerFormat.Short;
        dtpDesde.Location = new Point(51, 3);
        dtpDesde.Name = "dtpDesde";
        dtpDesde.Size = new Size(114, 23);
        dtpDesde.TabIndex = 1;
        dtpDesde.ValueChanged += dtpDesde_ValueChanged;
        // 
        // lblHasta
        // 
        lblHasta.Anchor = AnchorStyles.None;
        lblHasta.AutoSize = true;
        lblHasta.Location = new Point(171, 8);
        lblHasta.Name = "lblHasta";
        lblHasta.Size = new Size(40, 15);
        lblHasta.TabIndex = 2;
        lblHasta.Text = "Hasta:";
        // 
        // dtpHasta
        // 
        dtpHasta.Format = DateTimePickerFormat.Short;
        dtpHasta.Location = new Point(217, 3);
        dtpHasta.Name = "dtpHasta";
        dtpHasta.Size = new Size(114, 23);
        dtpHasta.TabIndex = 3;
        dtpHasta.ValueChanged += dtpHasta_ValueChanged;
        // 
        // lblBuscar
        // 
        lblBuscar.Anchor = AnchorStyles.None;
        lblBuscar.AutoSize = true;
        lblBuscar.Location = new Point(337, 8);
        lblBuscar.Name = "lblBuscar";
        lblBuscar.Size = new Size(45, 15);
        lblBuscar.TabIndex = 4;
        lblBuscar.Text = "Buscar:";
        // 
        // txtBuscar
        // 
        txtBuscar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtBuscar.Location = new Point(388, 4);
        txtBuscar.Name = "txtBuscar";
        txtBuscar.PlaceholderText = "concepto o categoría...";
        txtBuscar.Size = new Size(357, 23);
        txtBuscar.TabIndex = 5;
        txtBuscar.TextChanged += txtBuscar_TextChanged;
        // 
        // btnBuscar
        // 
        btnBuscar.Location = new Point(751, 3);
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Size = new Size(80, 26);
        btnBuscar.TabIndex = 6;
        btnBuscar.Text = "Buscar";
        btnBuscar.UseVisualStyleBackColor = true;
        btnBuscar.Click += btnBuscar_Click;
        // 
        // btnRefrescar
        // 
        btnRefrescar.Location = new Point(837, 3);
        btnRefrescar.Name = "btnRefrescar";
        btnRefrescar.Size = new Size(14, 26);
        btnRefrescar.TabIndex = 7;
        btnRefrescar.Text = "Refrescar";
        btnRefrescar.UseVisualStyleBackColor = true;
        btnRefrescar.Click += btnRefrescar_Click;
        // 
        // grpDatos
        // 
        grpDatos.Controls.Add(tlpDatos);
        grpDatos.Dock = DockStyle.Fill;
        grpDatos.Location = new Point(3, 74);
        grpDatos.Name = "grpDatos";
        grpDatos.Padding = new Padding(10);
        grpDatos.Size = new Size(854, 134);
        grpDatos.TabIndex = 2;
        grpDatos.TabStop = false;
        grpDatos.Text = "Nuevo gasto";
        // 
        // tlpDatos
        // 
        tlpDatos.ColumnCount = 8;
        tlpDatos.ColumnStyles.Add(new ColumnStyle());
        tlpDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tlpDatos.ColumnStyles.Add(new ColumnStyle());
        tlpDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tlpDatos.ColumnStyles.Add(new ColumnStyle());
        tlpDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tlpDatos.ColumnStyles.Add(new ColumnStyle());
        tlpDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tlpDatos.Controls.Add(lblFecha, 0, 0);
        tlpDatos.Controls.Add(dtpFecha, 1, 0);
        tlpDatos.Controls.Add(lblConcepto, 2, 0);
        tlpDatos.Controls.Add(txtConcepto, 3, 0);
        tlpDatos.Controls.Add(lblMonto, 0, 1);
        tlpDatos.Controls.Add(numMonto, 1, 1);
        tlpDatos.Controls.Add(txtCategoria, 3, 1);
        tlpDatos.Controls.Add(lblObservacion, 4, 0);
        tlpDatos.Controls.Add(txtObservacion, 5, 0);
        tlpDatos.Controls.Add(lblCategoria, 2, 1);
        tlpDatos.Dock = DockStyle.Fill;
        tlpDatos.Location = new Point(10, 26);
        tlpDatos.Name = "tlpDatos";
        tlpDatos.RowCount = 2;
        tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tlpDatos.Size = new Size(834, 98);
        tlpDatos.TabIndex = 0;
        // 
        // lblFecha
        // 
        lblFecha.Anchor = AnchorStyles.None;
        lblFecha.AutoSize = true;
        lblFecha.Location = new Point(5, 7);
        lblFecha.Name = "lblFecha";
        lblFecha.Size = new Size(41, 15);
        lblFecha.TabIndex = 0;
        lblFecha.Text = "Fecha:";
        // 
        // dtpFecha
        // 
        dtpFecha.Format = DateTimePickerFormat.Short;
        dtpFecha.Location = new Point(55, 3);
        dtpFecha.Name = "dtpFecha";
        dtpFecha.Size = new Size(114, 23);
        dtpFecha.TabIndex = 1;
        // 
        // lblConcepto
        // 
        lblConcepto.Anchor = AnchorStyles.None;
        lblConcepto.AutoSize = true;
        lblConcepto.Location = new Point(213, 7);
        lblConcepto.Name = "lblConcepto";
        lblConcepto.Size = new Size(62, 15);
        lblConcepto.TabIndex = 2;
        lblConcepto.Text = "Concepto:";
        // 
        // txtConcepto
        // 
        txtConcepto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtConcepto.Location = new Point(281, 3);
        txtConcepto.Name = "txtConcepto";
        txtConcepto.Size = new Size(152, 23);
        txtConcepto.TabIndex = 3;
        // 
        // lblMonto
        // 
        lblMonto.Anchor = AnchorStyles.None;
        lblMonto.AutoSize = true;
        lblMonto.Location = new Point(3, 56);
        lblMonto.Name = "lblMonto";
        lblMonto.Size = new Size(46, 15);
        lblMonto.TabIndex = 4;
        lblMonto.Text = "Monto:";
        // 
        // numMonto
        // 
        numMonto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        numMonto.DecimalPlaces = 2;
        numMonto.Location = new Point(55, 52);
        numMonto.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        numMonto.Name = "numMonto";
        numMonto.Size = new Size(152, 23);
        numMonto.TabIndex = 5;
        // 
        // lblCategoria
        // 
        lblCategoria.Anchor = AnchorStyles.None;
        lblCategoria.AutoSize = true;
        lblCategoria.Location = new Point(213, 56);
        lblCategoria.Name = "lblCategoria";
        lblCategoria.Size = new Size(61, 15);
        lblCategoria.TabIndex = 6;
        lblCategoria.Text = "Categoría:";
        // 
        // txtCategoria
        // 
        txtCategoria.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtCategoria.Location = new Point(281, 52);
        txtCategoria.Name = "txtCategoria";
        txtCategoria.Size = new Size(152, 23);
        txtCategoria.TabIndex = 7;
        // 
        // lblObservacion
        // 
        lblObservacion.Anchor = AnchorStyles.None;
        lblObservacion.AutoSize = true;
        lblObservacion.Location = new Point(439, 7);
        lblObservacion.Name = "lblObservacion";
        lblObservacion.Size = new Size(76, 15);
        lblObservacion.TabIndex = 8;
        lblObservacion.Text = "Observación:";
        // 
        // txtObservacion
        // 
        txtObservacion.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtObservacion.Location = new Point(521, 3);
        txtObservacion.Name = "txtObservacion";
        txtObservacion.Size = new Size(152, 23);
        txtObservacion.TabIndex = 9;
        // 
        // pnlBotones
        // 
        pnlBotones.Controls.Add(btnNuevo);
        pnlBotones.Controls.Add(btnGuardar);
        pnlBotones.Controls.Add(btnEliminar);
        pnlBotones.Controls.Add(btnCerrar);
        pnlBotones.Controls.Add(lblTotal);
        pnlBotones.Dock = DockStyle.Fill;
        pnlBotones.Location = new Point(3, 214);
        pnlBotones.Name = "pnlBotones";
        pnlBotones.Size = new Size(854, 36);
        pnlBotones.TabIndex = 3;
        // 
        // btnNuevo
        // 
        btnNuevo.Location = new Point(3, 3);
        btnNuevo.Name = "btnNuevo";
        btnNuevo.Size = new Size(80, 30);
        btnNuevo.TabIndex = 0;
        btnNuevo.Text = "Nuevo";
        btnNuevo.UseVisualStyleBackColor = true;
        btnNuevo.Click += btnNuevo_Click;
        // 
        // btnGuardar
        // 
        btnGuardar.Location = new Point(89, 3);
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Size = new Size(90, 30);
        btnGuardar.TabIndex = 1;
        btnGuardar.Text = "Guardar";
        btnGuardar.UseVisualStyleBackColor = true;
        btnGuardar.Click += btnGuardar_Click;
        //
        // btnEliminar
        //
        btnEliminar.ForeColor = Color.Firebrick;
        btnEliminar.Location = new Point(185, 3);
        btnEliminar.Name = "btnEliminar";
        btnEliminar.Size = new Size(90, 30);
        btnEliminar.TabIndex = 2;
        btnEliminar.Text = "Eliminar";
        btnEliminar.UseVisualStyleBackColor = true;
        btnEliminar.Click += btnEliminar_Click;
        //
        // btnCerrar
        //
        btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnCerrar.DialogResult = DialogResult.Cancel;
        btnCerrar.Location = new Point(771, 3);
        btnCerrar.Name = "btnCerrar";
        btnCerrar.Size = new Size(80, 30);
        btnCerrar.TabIndex = 3;
        btnCerrar.Text = "Cerrar";
        btnCerrar.UseVisualStyleBackColor = true;
        btnCerrar.Click += btnCerrar_Click;
        // 
        // lblTotal
        // 
        lblTotal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTotal.Location = new Point(460, 9);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(300, 20);
        lblTotal.TabIndex = 4;
        lblTotal.Text = "Total del período: $ 0,00";
        lblTotal.TextAlign = ContentAlignment.MiddleRight;
        // 
        // dgvGastos
        // 
        dgvGastos.AllowUserToAddRows = false;
        dgvGastos.AllowUserToDeleteRows = false;
        dgvGastos.AllowUserToResizeRows = false;
        dgvGastos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvGastos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvGastos.BackgroundColor = SystemColors.ButtonHighlight;
        dgvGastos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvGastos.Location = new Point(3, 256);
        dgvGastos.Name = "dgvGastos";
        dgvGastos.ReadOnly = true;
        dgvGastos.RowHeadersVisible = false;
        dgvGastos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvGastos.Size = new Size(854, 277);
        dgvGastos.TabIndex = 4;
        dgvGastos.CellDoubleClick += dgvGastos_CellDoubleClick;
        // 
        // FormGastos
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        CancelButton = btnCerrar;
        ClientSize = new Size(884, 560);
        Controls.Add(tlpPrincipal);
        Font = new Font("Segoe UI", 9F);
        Name = "FormGastos";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Gastos";
        Load += FormGastos_Load;
        tlpPrincipal.ResumeLayout(false);
        tlpPrincipal.PerformLayout();
        tlpFiltros.ResumeLayout(false);
        tlpFiltros.PerformLayout();
        grpDatos.ResumeLayout(false);
        tlpDatos.ResumeLayout(false);
        tlpDatos.PerformLayout();
        ((ISupportInitialize)numMonto).EndInit();
        pnlBotones.ResumeLayout(false);
        ((ISupportInitialize)dgvGastos).EndInit();
        ResumeLayout(false);
    }
}
