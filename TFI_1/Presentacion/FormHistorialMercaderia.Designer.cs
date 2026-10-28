using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;

partial class FormHistorialMercaderia : Form
{
	private IContainer components = null;

	private Label lblTitulo;
	private Label lblDesde;
	private DateTimePicker dtpDesde;
	private Label lblHasta;
	private DateTimePicker dtpHasta;
	private Label lblBuscar;
	private TextBox txtBuscar;
	private Button btnBuscar;
	private Button btnRefrescar;
	private Button btnNuevo;
	private DataGridView dgvIngresos;
	private Label lblDetalle;
	private DataGridView dgvDetalle;
	private Label lblTotal;
	private Label lblCantidad;
	private Button btnCerrar;
private Button btnAnterior;
private Label lblPagina;
private Button btnSiguiente;
	private Button btnAnular;

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
			components.Dispose();
		base.Dispose(disposing);
	}

    private void InitializeComponent()
    {
        lblTitulo = new Label();
        lblDesde = new Label();
        dtpDesde = new DateTimePicker();
        lblHasta = new Label();
        dtpHasta = new DateTimePicker();
        lblBuscar = new Label();
        txtBuscar = new TextBox();
        btnBuscar = new Button();
        btnRefrescar = new Button();
        btnNuevo = new Button();
        dgvIngresos = new DataGridView();
        lblDetalle = new Label();
        dgvDetalle = new DataGridView();
        lblTotal = new Label();
        lblCantidad = new Label();
        btnCerrar = new Button();
btnAnterior = new Button();
lblPagina = new Label();
btnSiguiente = new Button();
        btnAnular = new Button();
        ((ISupportInitialize)dgvIngresos).BeginInit();
        ((ISupportInitialize)dgvDetalle).BeginInit();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.Location = new Point(12, 10);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(218, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Historial de mercadería";
        // 
        // lblDesde
        // 
        lblDesde.AutoSize = true;
        lblDesde.Location = new Point(12, 50);
        lblDesde.Name = "lblDesde";
        lblDesde.Size = new Size(42, 15);
        lblDesde.TabIndex = 1;
        lblDesde.Text = "Desde:";
        // 
        // dtpDesde
        // 
        dtpDesde.Format = DateTimePickerFormat.Short;
        dtpDesde.Location = new Point(12, 68);
        dtpDesde.Name = "dtpDesde";
        dtpDesde.Size = new Size(130, 23);
        dtpDesde.TabIndex = 2;
        dtpDesde.ValueChanged += dtpDesde_ValueChanged;
        // 
        // lblHasta
        // 
        lblHasta.AutoSize = true;
        lblHasta.Location = new Point(152, 50);
        lblHasta.Name = "lblHasta";
        lblHasta.Size = new Size(40, 15);
        lblHasta.TabIndex = 3;
        lblHasta.Text = "Hasta:";
        // 
        // dtpHasta
        // 
        dtpHasta.Format = DateTimePickerFormat.Short;
        dtpHasta.Location = new Point(152, 68);
        dtpHasta.Name = "dtpHasta";
        dtpHasta.Size = new Size(130, 23);
        dtpHasta.TabIndex = 4;
        dtpHasta.ValueChanged += dtpHasta_ValueChanged;
        // 
        // lblBuscar
        // 
        lblBuscar.AutoSize = true;
        lblBuscar.Location = new Point(292, 50);
        lblBuscar.Name = "lblBuscar";
        lblBuscar.Size = new Size(64, 15);
        lblBuscar.TabIndex = 5;
        lblBuscar.Text = "Proveedor:";
        // 
        // txtBuscar
        // 
        txtBuscar.Location = new Point(292, 68);
        txtBuscar.Name = "txtBuscar";
        txtBuscar.PlaceholderText = "proveedor...";
        txtBuscar.Size = new Size(200, 23);
        txtBuscar.TabIndex = 6;
        txtBuscar.TextChanged += txtBuscar_TextChanged;
        // 
        // btnBuscar
        // 
        btnBuscar.Location = new Point(500, 65);
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Size = new Size(80, 26);
        btnBuscar.TabIndex = 7;
        btnBuscar.Text = "Buscar";
        btnBuscar.UseVisualStyleBackColor = true;
        btnBuscar.Click += btnBuscar_Click;
        // 
        // btnRefrescar
        // 
        btnRefrescar.Location = new Point(586, 65);
        btnRefrescar.Name = "btnRefrescar";
        btnRefrescar.Size = new Size(90, 26);
        btnRefrescar.TabIndex = 8;
        btnRefrescar.Text = "Refrescar";
        btnRefrescar.UseVisualStyleBackColor = true;
        btnRefrescar.Click += btnRefrescar_Click;
        // 
        // btnNuevo
        // 
        btnNuevo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnNuevo.Location = new Point(788, 65);
        btnNuevo.Name = "btnNuevo";
        btnNuevo.Size = new Size(120, 26);
        btnNuevo.TabIndex = 9;
        btnNuevo.Text = "Nuevo ingreso";
        btnNuevo.UseVisualStyleBackColor = true;
        btnNuevo.Click += btnNuevo_Click;
        // 
        // dgvIngresos
        // 
        dgvIngresos.AllowUserToAddRows = false;
        dgvIngresos.AllowUserToDeleteRows = false;
        dgvIngresos.AllowUserToResizeRows = false;
        dgvIngresos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        dgvIngresos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvIngresos.BackgroundColor = SystemColors.ButtonHighlight;
        dgvIngresos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvIngresos.Location = new Point(12, 100);
        dgvIngresos.MultiSelect = false;
        dgvIngresos.Name = "dgvIngresos";
        dgvIngresos.ReadOnly = true;
        dgvIngresos.RowHeadersVisible = false;
        dgvIngresos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvIngresos.Size = new Size(896, 228);
        dgvIngresos.TabIndex = 10;
        dgvIngresos.SelectionChanged += dgvIngresos_SelectionChanged;
        // 
        // lblDetalle
        // 
        lblDetalle.AutoSize = true;
        lblDetalle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblDetalle.Location = new Point(12, 340);
        lblDetalle.Name = "lblDetalle";
        lblDetalle.Size = new Size(188, 15);
        lblDetalle.TabIndex = 11;
        lblDetalle.Text = "Detalle del ingreso seleccionado:";
        // 
        // dgvDetalle
        // 
        dgvDetalle.AllowUserToAddRows = false;
        dgvDetalle.AllowUserToDeleteRows = false;
        dgvDetalle.AllowUserToResizeRows = false;
        dgvDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDetalle.BackgroundColor = SystemColors.ButtonHighlight;
        dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvDetalle.Location = new Point(12, 360);
        dgvDetalle.MultiSelect = false;
        dgvDetalle.Name = "dgvDetalle";
        dgvDetalle.ReadOnly = true;
        dgvDetalle.RowHeadersVisible = false;
        dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDetalle.Size = new Size(896, 196);
        dgvDetalle.TabIndex = 12;
        // 
        // lblTotal
        // 
        lblTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblTotal.AutoSize = true;
        lblTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTotal.Location = new Point(12, 568);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(140, 15);
        lblTotal.TabIndex = 13;
        lblTotal.Text = "Total del período: $ 0,00";
        // 
        // lblCantidad
        // 
        lblCantidad.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblCantidad.AutoSize = true;
        lblCantidad.Location = new Point(270, 568);
        lblCantidad.Name = "lblCantidad";
        lblCantidad.Size = new Size(63, 15);
        lblCantidad.TabIndex = 14;
        lblCantidad.Text = "Ingresos: 0";
        // 
        // btnCerrar
        // 
        btnCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCerrar.DialogResult = DialogResult.Cancel;
        btnCerrar.Location = new Point(828, 564);
        btnCerrar.Name = "btnCerrar";
        btnCerrar.Size = new Size(80, 30);
        btnCerrar.TabIndex = 15;
        btnCerrar.Text = "Cerrar";
        btnCerrar.UseVisualStyleBackColor = true;
        btnCerrar.Click += btnCerrar_Click;
        // 
        // btnAnterior
        // 
        btnAnterior.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnAnterior.Location = new Point(360, 564);
        btnAnterior.Name = "btnAnterior";
        btnAnterior.Size = new Size(90, 30);
        btnAnterior.TabIndex = 16;
        btnAnterior.Text = "◀ Anterior";
        btnAnterior.UseVisualStyleBackColor = true;
        btnAnterior.Click += btnAnterior_Click;
        // 
        // lblPagina
        // 
        lblPagina.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblPagina.Location = new Point(458, 569);
        lblPagina.Name = "lblPagina";
        lblPagina.Size = new Size(200, 20);
        lblPagina.TabIndex = 17;
        lblPagina.Text = "Página 1 de 1";
        lblPagina.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // btnSiguiente
        // 
        btnSiguiente.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnSiguiente.Location = new Point(666, 564);
        btnSiguiente.Name = "btnSiguiente";
        btnSiguiente.Size = new Size(90, 30);
        btnSiguiente.TabIndex = 18;
        btnSiguiente.Text = "Siguiente ▶";
        btnSiguiente.UseVisualStyleBackColor = true;
        btnSiguiente.Click += btnSiguiente_Click;
        //
        // btnAnular
        //
        btnAnular.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAnular.Enabled = false;
        btnAnular.ForeColor = Color.DarkRed;
        btnAnular.Location = new Point(788, 334);
        btnAnular.Name = "btnAnular";
        btnAnular.Size = new Size(120, 25);
        btnAnular.TabIndex = 19;
        btnAnular.Text = "Anular ingreso";
        btnAnular.UseVisualStyleBackColor = true;
        btnAnular.Click += btnAnular_Click;
        //
        // FormHistorialMercaderia
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        CancelButton = btnCerrar;
        ClientSize = new Size(920, 620);
        Controls.Add(btnCerrar);
        Controls.Add(btnAnterior);
        Controls.Add(lblPagina);
        Controls.Add(btnSiguiente);
        Controls.Add(lblCantidad);
        Controls.Add(lblTotal);
        Controls.Add(btnAnular);
        Controls.Add(dgvDetalle);
        Controls.Add(lblDetalle);
        Controls.Add(dgvIngresos);
        Controls.Add(btnNuevo);
        Controls.Add(btnRefrescar);
        Controls.Add(btnBuscar);
        Controls.Add(txtBuscar);
        Controls.Add(lblBuscar);
        Controls.Add(dtpHasta);
        Controls.Add(lblHasta);
        Controls.Add(dtpDesde);
        Controls.Add(lblDesde);
        Controls.Add(lblTitulo);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(820, 520);
        Name = "FormHistorialMercaderia";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Historial de mercadería";
        Load += FormHistorialMercaderia_Load;
        ((ISupportInitialize)dgvIngresos).EndInit();
        ((ISupportInitialize)dgvDetalle).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
