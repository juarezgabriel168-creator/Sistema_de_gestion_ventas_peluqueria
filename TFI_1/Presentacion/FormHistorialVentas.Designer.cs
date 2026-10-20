using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;
partial class FormHistorialVentas : Form
{
	private IContainer components = new System.ComponentModel.Container();

	private Label lblTitulo;

	private Label lblDesde;

	private DateTimePicker dtpDesde;

	private Label lblHasta;

	private DateTimePicker dtpHasta;

	private Label lblUsuario;

	private ComboBox cboUsuario;

	private Button btnBuscar;

	private Button btnRefrescar;

	private DataGridView dgvVentas;

	private Button btnVerDetalle;

	private Label lblEmpleado;

	private ComboBox cboEmpleado;

	private Label lblNumeroVenta;

	private TextBox txtNumeroVenta;

	private Label lblPagina;

	private Button btnAnterior;

	private Button btnSiguiente;

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

    private void InitializeComponent()
    {
        lblTitulo = new Label();
        lblDesde = new Label();
        dtpDesde = new DateTimePicker();
        lblHasta = new Label();
        dtpHasta = new DateTimePicker();
        lblUsuario = new Label();
        cboUsuario = new ComboBox();
        btnBuscar = new Button();
        btnRefrescar = new Button();
        dgvVentas = new DataGridView();
        btnVerDetalle = new Button();
        lblEmpleado = new Label();
        cboEmpleado = new ComboBox();
        lblNumeroVenta = new Label();
        txtNumeroVenta = new TextBox();
        lblPagina = new Label();
        btnAnterior = new Button();
        btnSiguiente = new Button();
        ((ISupportInitialize)dgvVentas).BeginInit();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.Location = new Point(12, 10);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(176, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Historial de ventas";
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
        dtpDesde.Size = new Size(140, 23);
        dtpDesde.TabIndex = 2;
        // 
        // lblHasta
        // 
        lblHasta.AutoSize = true;
        lblHasta.Location = new Point(170, 50);
        lblHasta.Name = "lblHasta";
        lblHasta.Size = new Size(40, 15);
        lblHasta.TabIndex = 3;
        lblHasta.Text = "Hasta:";
        // 
        // dtpHasta
        // 
        dtpHasta.Format = DateTimePickerFormat.Short;
        dtpHasta.Location = new Point(170, 68);
        dtpHasta.Name = "dtpHasta";
        dtpHasta.Size = new Size(140, 23);
        dtpHasta.TabIndex = 4;
        // 
        // lblUsuario
        // 
        lblUsuario.AutoSize = true;
        lblUsuario.Location = new Point(330, 50);
        lblUsuario.Name = "lblUsuario";
        lblUsuario.Size = new Size(50, 15);
        lblUsuario.TabIndex = 5;
        lblUsuario.Text = "Usuario:";
        // 
        // cboUsuario
        // 
        cboUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
        cboUsuario.FormattingEnabled = true;
        cboUsuario.Location = new Point(330, 68);
        cboUsuario.Name = "cboUsuario";
        cboUsuario.Size = new Size(180, 23);
        cboUsuario.TabIndex = 6;
        // 
        // btnBuscar
        // 
        btnBuscar.Location = new Point(772, 65);
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Size = new Size(90, 26);
        btnBuscar.TabIndex = 7;
        btnBuscar.Text = "Buscar";
        btnBuscar.UseVisualStyleBackColor = true;
        btnBuscar.Click += btnBuscar_Click;
        // 
        // btnRefrescar
        // 
        btnRefrescar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefrescar.Location = new Point(877, 66);
        btnRefrescar.Name = "btnRefrescar";
        btnRefrescar.Size = new Size(100, 26);
        btnRefrescar.TabIndex = 8;
        btnRefrescar.Text = "Refrescar";
        btnRefrescar.UseVisualStyleBackColor = true;
        btnRefrescar.Click += btnRefrescar_Click;
        // 
        // dgvVentas
        // 
        dgvVentas.AllowUserToAddRows = false;
        dgvVentas.AllowUserToDeleteRows = false;
        dgvVentas.AllowUserToResizeRows = false;
        dgvVentas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvVentas.BackgroundColor = SystemColors.ButtonHighlight;
        dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvVentas.Location = new Point(12, 108);
        dgvVentas.MultiSelect = false;
        dgvVentas.Name = "dgvVentas";
        dgvVentas.ReadOnly = true;
        dgvVentas.RowHeadersVisible = false;
        dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvVentas.Size = new Size(975, 409);
        dgvVentas.TabIndex = 9;
        // 
        // btnVerDetalle
        // 
        btnVerDetalle.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnVerDetalle.Location = new Point(12, 529);
        btnVerDetalle.Name = "btnVerDetalle";
        btnVerDetalle.Size = new Size(150, 32);
        btnVerDetalle.TabIndex = 10;
        btnVerDetalle.Text = "Ver detalle";
        btnVerDetalle.UseVisualStyleBackColor = true;
        btnVerDetalle.Click += btnVerDetalle_Click;
        // 
        // lblEmpleado
        // 
        lblEmpleado.AutoSize = true;
        lblEmpleado.Location = new Point(520, 50);
        lblEmpleado.Name = "lblEmpleado";
        lblEmpleado.Size = new Size(63, 15);
        lblEmpleado.TabIndex = 11;
        lblEmpleado.Text = "Empleado:";
        // 
        // cboEmpleado
        // 
        cboEmpleado.DropDownStyle = ComboBoxStyle.DropDownList;
        cboEmpleado.FormattingEnabled = true;
        cboEmpleado.Location = new Point(520, 68);
        cboEmpleado.Name = "cboEmpleado";
        cboEmpleado.Size = new Size(150, 23);
        cboEmpleado.TabIndex = 12;
        // 
        // lblNumeroVenta
        // 
        lblNumeroVenta.AutoSize = true;
        lblNumeroVenta.Location = new Point(680, 50);
        lblNumeroVenta.Name = "lblNumeroVenta";
        lblNumeroVenta.Size = new Size(56, 15);
        lblNumeroVenta.TabIndex = 13;
        lblNumeroVenta.Text = "Nº Venta:";
        // 
        // txtNumeroVenta
        // 
        txtNumeroVenta.Location = new Point(680, 68);
        txtNumeroVenta.Name = "txtNumeroVenta";
        txtNumeroVenta.PlaceholderText = "Nº venta";
        txtNumeroVenta.Size = new Size(80, 23);
        txtNumeroVenta.TabIndex = 14;
        // 
        // btnAnterior
        // 
        btnAnterior.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnAnterior.Location = new Point(180, 529);
        btnAnterior.Name = "btnAnterior";
        btnAnterior.Size = new Size(90, 32);
        btnAnterior.TabIndex = 15;
        btnAnterior.Text = "◀ Anterior";
        btnAnterior.UseVisualStyleBackColor = true;
        btnAnterior.Click += btnAnterior_Click;
        // 
        // lblPagina
        // 
        lblPagina.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblPagina.Location = new Point(278, 536);
        lblPagina.Name = "lblPagina";
        lblPagina.Size = new Size(200, 20);
        lblPagina.TabIndex = 16;
        lblPagina.Text = "Página 1 de 1";
        lblPagina.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // btnSiguiente
        // 
        btnSiguiente.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnSiguiente.Location = new Point(486, 529);
        btnSiguiente.Name = "btnSiguiente";
        btnSiguiente.Size = new Size(90, 32);
        btnSiguiente.TabIndex = 17;
        btnSiguiente.Text = "Siguiente ▶";
        btnSiguiente.UseVisualStyleBackColor = true;
        btnSiguiente.Click += btnSiguiente_Click;
        // 
        // FormHistorialVentas
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        ClientSize = new Size(999, 577);
        Controls.Add(lblEmpleado);
        Controls.Add(cboEmpleado);
        Controls.Add(lblNumeroVenta);
        Controls.Add(txtNumeroVenta);
        Controls.Add(btnAnterior);
        Controls.Add(lblPagina);
        Controls.Add(btnSiguiente);
        Controls.Add(btnVerDetalle);
        Controls.Add(dgvVentas);
        Controls.Add(btnRefrescar);
        Controls.Add(btnBuscar);
        Controls.Add(cboUsuario);
        Controls.Add(lblUsuario);
        Controls.Add(dtpHasta);
        Controls.Add(lblHasta);
        Controls.Add(dtpDesde);
        Controls.Add(lblDesde);
        Controls.Add(lblTitulo);
        Font = new Font("Segoe UI", 9F);
        Name = "FormHistorialVentas";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Historial de Ventas";
        Load += FormHistorialVentas_Load;
        ((ISupportInitialize)dgvVentas).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
