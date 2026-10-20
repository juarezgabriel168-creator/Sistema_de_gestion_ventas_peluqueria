using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;
partial class FormCuentaCorriente : Form
{
	private IContainer components = null;

	private Label lblCliente;

	private ComboBox cboCliente;

	private Label lblSaldoTitulo;

	private Label lblSaldo;

	private Label lblDesde;

	private DateTimePicker dtpDesde;

	private Label lblHasta;

	private DateTimePicker dtpHasta;

	private Button btnBuscar;

	private Button btnRefrescar;

	private DataGridView dgvMovimientos;

	private GroupBox grpAbono;

	private Label lblMonto;

	private NumericUpDown numMonto;

	private Label lblConcepto;

	private TextBox txtConcepto;

	private Button btnRegistrarAbono;

	private Label lblSaldoSinDeuda;

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
        lblCliente = new Label();
        cboCliente = new ComboBox();
        lblSaldoTitulo = new Label();
        lblSaldo = new Label();
        lblDesde = new Label();
        dtpDesde = new DateTimePicker();
        lblHasta = new Label();
        dtpHasta = new DateTimePicker();
        btnBuscar = new Button();
        btnRefrescar = new Button();
        dgvMovimientos = new DataGridView();
        grpAbono = new GroupBox();
        lblMonto = new Label();
        numMonto = new NumericUpDown();
        lblConcepto = new Label();
        txtConcepto = new TextBox();
        btnRegistrarAbono = new Button();
        lblSaldoSinDeuda = new Label();
        ((ISupportInitialize)dgvMovimientos).BeginInit();
        grpAbono.SuspendLayout();
        ((ISupportInitialize)numMonto).BeginInit();
        SuspendLayout();
        // 
        // lblCliente
        // 
        lblCliente.AutoSize = true;
        lblCliente.Location = new Point(12, 18);
        lblCliente.Name = "lblCliente";
        lblCliente.Size = new Size(47, 15);
        lblCliente.TabIndex = 0;
        lblCliente.Text = "Cliente:";
        // 
        // cboCliente
        // 
        cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCliente.FormattingEnabled = true;
        cboCliente.Location = new Point(75, 14);
        cboCliente.Name = "cboCliente";
        cboCliente.Size = new Size(340, 23);
        cboCliente.TabIndex = 1;
        cboCliente.SelectedIndexChanged += cboCliente_SelectedIndexChanged;
        // 
        // lblSaldoTitulo
        // 
        lblSaldoTitulo.AutoSize = true;
        lblSaldoTitulo.Location = new Point(440, 18);
        lblSaldoTitulo.Name = "lblSaldoTitulo";
        lblSaldoTitulo.Size = new Size(95, 15);
        lblSaldoTitulo.TabIndex = 2;
        lblSaldoTitulo.Text = "Saldo adeudado:";
        // 
        // lblSaldo
        // 
        lblSaldo.AutoSize = true;
        lblSaldo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblSaldo.ForeColor = Color.Firebrick;
        lblSaldo.Location = new Point(545, 14);
        lblSaldo.Name = "lblSaldo";
        lblSaldo.Size = new Size(26, 21);
        lblSaldo.TabIndex = 3;
        lblSaldo.Text = "—";
        // 
        // lblDesde
        // 
        lblDesde.AutoSize = true;
        lblDesde.Location = new Point(12, 56);
        lblDesde.Name = "lblDesde";
        lblDesde.Size = new Size(42, 15);
        lblDesde.TabIndex = 4;
        lblDesde.Text = "Desde:";
        // 
        // dtpDesde
        // 
        dtpDesde.Format = DateTimePickerFormat.Short;
        dtpDesde.Location = new Point(75, 52);
        dtpDesde.Name = "dtpDesde";
        dtpDesde.Size = new Size(120, 23);
        dtpDesde.TabIndex = 5;
        // 
        // lblHasta
        // 
        lblHasta.AutoSize = true;
        lblHasta.Location = new Point(210, 56);
        lblHasta.Name = "lblHasta";
        lblHasta.Size = new Size(40, 15);
        lblHasta.TabIndex = 6;
        lblHasta.Text = "Hasta:";
        // 
        // dtpHasta
        // 
        dtpHasta.Format = DateTimePickerFormat.Short;
        dtpHasta.Location = new Point(255, 52);
        dtpHasta.Name = "dtpHasta";
        dtpHasta.Size = new Size(120, 23);
        dtpHasta.TabIndex = 7;
        // 
        // btnBuscar
        // 
        btnBuscar.Location = new Point(390, 51);
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Size = new Size(80, 25);
        btnBuscar.TabIndex = 8;
        btnBuscar.Text = "Buscar";
        btnBuscar.UseVisualStyleBackColor = true;
        btnBuscar.Click += btnBuscar_Click;
        // 
        // btnRefrescar
        // 
        btnRefrescar.Location = new Point(476, 51);
        btnRefrescar.Name = "btnRefrescar";
        btnRefrescar.Size = new Size(90, 25);
        btnRefrescar.TabIndex = 9;
        btnRefrescar.Text = "Refrescar";
        btnRefrescar.UseVisualStyleBackColor = true;
        btnRefrescar.Click += btnRefrescar_Click;
        // 
        // dgvMovimientos
        // 
        dgvMovimientos.AllowUserToAddRows = false;
        dgvMovimientos.AllowUserToDeleteRows = false;
        dgvMovimientos.AllowUserToResizeRows = false;
        dgvMovimientos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvMovimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvMovimientos.BackgroundColor = SystemColors.Window;
        dgvMovimientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvMovimientos.Location = new Point(12, 86);
        dgvMovimientos.MultiSelect = false;
        dgvMovimientos.Name = "dgvMovimientos";
        dgvMovimientos.ReadOnly = true;
        dgvMovimientos.RowHeadersVisible = false;
        dgvMovimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvMovimientos.Size = new Size(876, 370);
        dgvMovimientos.TabIndex = 10;
        // 
        // grpAbono
        // 
        grpAbono.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grpAbono.Controls.Add(lblMonto);
        grpAbono.Controls.Add(numMonto);
        grpAbono.Controls.Add(lblConcepto);
        grpAbono.Controls.Add(txtConcepto);
        grpAbono.Controls.Add(btnRegistrarAbono);
        grpAbono.Controls.Add(lblSaldoSinDeuda);
        grpAbono.Location = new Point(12, 466);
        grpAbono.Name = "grpAbono";
        grpAbono.Size = new Size(876, 100);
        grpAbono.TabIndex = 11;
        grpAbono.TabStop = false;
        grpAbono.Text = "Registrar pago (abono)";
        // 
        // lblMonto
        // 
        lblMonto.AutoSize = true;
        lblMonto.Location = new Point(15, 34);
        lblMonto.Name = "lblMonto";
        lblMonto.Size = new Size(46, 15);
        lblMonto.TabIndex = 0;
        lblMonto.Text = "Monto:";
        // 
        // numMonto
        // 
        numMonto.DecimalPlaces = 2;
        numMonto.Location = new Point(70, 31);
        numMonto.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
        numMonto.Name = "numMonto";
        numMonto.Size = new Size(140, 23);
        numMonto.TabIndex = 1;
        numMonto.ThousandsSeparator = true;
        // 
        // lblConcepto
        // 
        lblConcepto.AutoSize = true;
        lblConcepto.Location = new Point(230, 34);
        lblConcepto.Name = "lblConcepto";
        lblConcepto.Size = new Size(62, 15);
        lblConcepto.TabIndex = 2;
        lblConcepto.Text = "Concepto:";
        // 
        // txtConcepto
        // 
        txtConcepto.Location = new Point(300, 31);
        txtConcepto.MaxLength = 120;
        txtConcepto.Name = "txtConcepto";
        txtConcepto.Size = new Size(360, 23);
        txtConcepto.TabIndex = 3;
        // 
        // btnRegistrarAbono
        // 
        btnRegistrarAbono.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRegistrarAbono.Location = new Point(680, 29);
        btnRegistrarAbono.Name = "btnRegistrarAbono";
        btnRegistrarAbono.Size = new Size(180, 28);
        btnRegistrarAbono.TabIndex = 4;
        btnRegistrarAbono.Text = "Registrar abono";
        btnRegistrarAbono.UseVisualStyleBackColor = true;
        btnRegistrarAbono.Click += btnRegistrarAbono_Click;
        // 
        // lblSaldoSinDeuda
        // 
        lblSaldoSinDeuda.AutoSize = true;
        lblSaldoSinDeuda.ForeColor = Color.DimGray;
        lblSaldoSinDeuda.Location = new Point(15, 66);
        lblSaldoSinDeuda.Name = "lblSaldoSinDeuda";
        lblSaldoSinDeuda.Size = new Size(0, 15);
        lblSaldoSinDeuda.TabIndex = 5;
        // 
        // FormCuentaCorriente
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        ClientSize = new Size(900, 580);
        Controls.Add(lblCliente);
        Controls.Add(cboCliente);
        Controls.Add(lblSaldoTitulo);
        Controls.Add(lblSaldo);
        Controls.Add(lblDesde);
        Controls.Add(dtpDesde);
        Controls.Add(lblHasta);
        Controls.Add(dtpHasta);
        Controls.Add(btnBuscar);
        Controls.Add(btnRefrescar);
        Controls.Add(dgvMovimientos);
        Controls.Add(grpAbono);
        MinimumSize = new Size(760, 480);
        Name = "FormCuentaCorriente";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Cuenta corriente";
        Load += FormCuentaCorriente_Load;
        ((ISupportInitialize)dgvMovimientos).EndInit();
        grpAbono.ResumeLayout(false);
        grpAbono.PerformLayout();
        ((ISupportInitialize)numMonto).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
