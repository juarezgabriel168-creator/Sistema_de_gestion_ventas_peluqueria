using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;
partial class FormReporteDiario : Form
{
	private IContainer components = null;

	private Label lblTitulo;

	private Label lblFecha;

	private DateTimePicker dtpFecha;

	private Button btnActualizar;

	private Button btnImprimir;

	private Label lblTotal;

	private Label lblCantidad;

	private Label lblTicket;

	private Label lblGananciaBruta;

	private Label lblPorPago;

	private DataGridView dgvPorPago;

	private Label lblMasVendidos;

	private DataGridView dgvMasVendidos;

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
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
        lblTitulo = new Label();
        lblFecha = new Label();
        dtpFecha = new DateTimePicker();
        btnActualizar = new Button();
        btnImprimir = new Button();
        lblTotal = new Label();
        lblCantidad = new Label();
        lblTicket = new Label();
        lblGananciaBruta = new Label();
        lblPorPago = new Label();
        dgvPorPago = new DataGridView();
        lblMasVendidos = new Label();
        dgvMasVendidos = new DataGridView();
        lblGastos = new Label();
        lblGananciaNeta = new Label();
        lblGastosCategoria = new Label();
        dgvGastosCategoria = new DataGridView();
        lblPorEmpleado = new Label();
        dgvPorEmpleado = new DataGridView();
        tabInferior = new TabControl();
        tabGastos = new TabPage();
        tabAnulaciones = new TabPage();
        lblResumenAnulaciones = new Label();
        dgvAnulaciones = new DataGridView();
        ((ISupportInitialize)dgvPorPago).BeginInit();
        ((ISupportInitialize)dgvMasVendidos).BeginInit();
        ((ISupportInitialize)dgvGastosCategoria).BeginInit();
        ((ISupportInitialize)dgvPorEmpleado).BeginInit();
        tabInferior.SuspendLayout();
        tabGastos.SuspendLayout();
        tabAnulaciones.SuspendLayout();
        ((ISupportInitialize)dgvAnulaciones).BeginInit();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.Location = new Point(12, 10);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(141, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Reporte diario";
        // 
        // lblFecha
        // 
        lblFecha.AutoSize = true;
        lblFecha.Location = new Point(12, 52);
        lblFecha.Name = "lblFecha";
        lblFecha.Size = new Size(41, 15);
        lblFecha.TabIndex = 1;
        lblFecha.Text = "Fecha:";
        // 
        // dtpFecha
        // 
        dtpFecha.Format = DateTimePickerFormat.Short;
        dtpFecha.Location = new Point(66, 50);
        dtpFecha.Name = "dtpFecha";
        dtpFecha.Size = new Size(160, 23);
        dtpFecha.TabIndex = 2;
        // 
        // btnActualizar
        // 
        btnActualizar.Location = new Point(238, 48);
        btnActualizar.Name = "btnActualizar";
        btnActualizar.Size = new Size(110, 26);
        btnActualizar.TabIndex = 3;
        btnActualizar.Text = "Actualizar";
        btnActualizar.UseVisualStyleBackColor = true;
        btnActualizar.Click += btnActualizar_Click;
        // 
        // btnImprimir
        // 
        btnImprimir.Location = new Point(360, 48);
        btnImprimir.Name = "btnImprimir";
        btnImprimir.Size = new Size(90, 26);
        btnImprimir.TabIndex = 4;
        btnImprimir.Text = "Imprimir";
        btnImprimir.UseVisualStyleBackColor = true;
        btnImprimir.Click += btnImprimir_Click;
        // 
        // lblTotal
        // 
        lblTotal.AutoSize = true;
        lblTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblTotal.Location = new Point(12, 86);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(118, 20);
        lblTotal.TabIndex = 5;
        lblTotal.Text = "Vendido: $ 0,00";
        // 
        // lblCantidad
        // 
        lblCantidad.AutoSize = true;
        lblCantidad.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblCantidad.Location = new Point(250, 86);
        lblCantidad.Name = "lblCantidad";
        lblCantidad.Size = new Size(73, 20);
        lblCantidad.TabIndex = 6;
        lblCantidad.Text = "Ventas: 0";
        // 
        // lblTicket
        // 
        lblTicket.AutoSize = true;
        lblTicket.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblTicket.Location = new Point(450, 86);
        lblTicket.Name = "lblTicket";
        lblTicket.Size = new Size(175, 20);
        lblTicket.TabIndex = 7;
        lblTicket.Text = "Ticket promedio: $ 0,00";
        // 
        // lblGananciaBruta
        // 
        lblGananciaBruta.AutoSize = true;
        lblGananciaBruta.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblGananciaBruta.Location = new Point(12, 112);
        lblGananciaBruta.Name = "lblGananciaBruta";
        lblGananciaBruta.Size = new Size(208, 19);
        lblGananciaBruta.TabIndex = 8;
        lblGananciaBruta.Text = "Ganancia bruta del día: $ 0,00";
        // 
        // lblPorPago
        // 
        lblPorPago.AutoSize = true;
        lblPorPago.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPorPago.Location = new Point(12, 182);
        lblPorPago.Name = "lblPorPago";
        lblPorPago.Size = new Size(151, 15);
        lblPorPago.TabIndex = 9;
        lblPorPago.Text = "Ventas por medio de pago";
        // 
        // dgvPorPago
        // 
        dgvPorPago.AllowUserToAddRows = false;
        dgvPorPago.AllowUserToDeleteRows = false;
        dgvPorPago.AllowUserToResizeRows = false;
        dgvPorPago.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvPorPago.BackgroundColor = SystemColors.ButtonHighlight;
        dgvPorPago.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvPorPago.Location = new Point(12, 200);
        dgvPorPago.Name = "dgvPorPago";
        dgvPorPago.ReadOnly = true;
        dgvPorPago.RowHeadersVisible = false;
        dgvPorPago.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPorPago.Size = new Size(436, 120);
        dgvPorPago.TabIndex = 10;
        // 
        // lblMasVendidos
        // 
        lblMasVendidos.AutoSize = true;
        lblMasVendidos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblMasVendidos.Location = new Point(12, 323);
        lblMasVendidos.Name = "lblMasVendidos";
        lblMasVendidos.Size = new Size(201, 15);
        lblMasVendidos.TabIndex = 11;
        lblMasVendidos.Text = "Productos y servicios más vendidos";
        // 
        // dgvMasVendidos
        // 
        dgvMasVendidos.AllowUserToAddRows = false;
        dgvMasVendidos.AllowUserToDeleteRows = false;
        dgvMasVendidos.AllowUserToResizeRows = false;
        dgvMasVendidos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        dgvMasVendidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvMasVendidos.BackgroundColor = SystemColors.ButtonHighlight;
        dgvMasVendidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvMasVendidos.Location = new Point(12, 341);
        dgvMasVendidos.Name = "dgvMasVendidos";
        dgvMasVendidos.ReadOnly = true;
        dgvMasVendidos.RowHeadersVisible = false;
        dgvMasVendidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvMasVendidos.Size = new Size(436, 285);
        dgvMasVendidos.TabIndex = 12;
        // 
        // lblGastos
        // 
        lblGastos.AutoSize = true;
        lblGastos.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblGastos.Location = new Point(250, 112);
        lblGastos.Name = "lblGastos";
        lblGastos.Size = new Size(139, 17);
        lblGastos.TabIndex = 13;
        lblGastos.Text = "Gastos del dia: $ 0,00";
        // 
        // lblGananciaNeta
        // 
        lblGananciaNeta.AutoSize = true;
        lblGananciaNeta.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblGananciaNeta.Location = new Point(450, 112);
        lblGananciaNeta.Name = "lblGananciaNeta";
        lblGananciaNeta.Size = new Size(191, 17);
        lblGananciaNeta.TabIndex = 14;
        lblGananciaNeta.Text = "Ganancias neta del dia: $ 0,00";
        // 
        // lblGastosCategoria
        // 
        lblGastosCategoria.AutoSize = true;
        lblGastosCategoria.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblGastosCategoria.Location = new Point(460, 323);
        lblGastosCategoria.Name = "lblGastosCategoria";
        lblGastosCategoria.Size = new Size(121, 15);
        lblGastosCategoria.TabIndex = 15;
        lblGastosCategoria.Text = "Gastos y anulaciones";
        // 
        // dgvGastosCategoria
        // 
        dgvGastosCategoria.AllowUserToAddRows = false;
        dgvGastosCategoria.AllowUserToDeleteRows = false;
        dgvGastosCategoria.AllowUserToResizeRows = false;
        dgvGastosCategoria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvGastosCategoria.BackgroundColor = SystemColors.ButtonHighlight;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = SystemColors.Control;
        dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        dgvGastosCategoria.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        dgvGastosCategoria.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = SystemColors.Window;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        dgvGastosCategoria.DefaultCellStyle = dataGridViewCellStyle2;
        dgvGastosCategoria.Dock = DockStyle.Fill;
        dgvGastosCategoria.Location = new Point(3, 3);
        dgvGastosCategoria.Name = "dgvGastosCategoria";
        dgvGastosCategoria.ReadOnly = true;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = SystemColors.Control;
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
        dgvGastosCategoria.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
        dgvGastosCategoria.RowHeadersVisible = false;
        dgvGastosCategoria.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvGastosCategoria.Size = new Size(422, 257);
        dgvGastosCategoria.TabIndex = 0;
        // 
        // tabGastos
        // 
        tabGastos.Controls.Add(dgvGastosCategoria);
        tabGastos.Location = new Point(4, 24);
        tabGastos.Name = "tabGastos";
        tabGastos.Padding = new Padding(3);
        tabGastos.Size = new Size(428, 257);
        tabGastos.TabIndex = 0;
        tabGastos.Text = "Gastos por categoría";
        tabGastos.UseVisualStyleBackColor = true;
        // 
        // lblResumenAnulaciones
        // 
        lblResumenAnulaciones.Dock = DockStyle.Top;
        lblResumenAnulaciones.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblResumenAnulaciones.Location = new Point(3, 3);
        lblResumenAnulaciones.Name = "lblResumenAnulaciones";
        lblResumenAnulaciones.Size = new Size(422, 22);
        lblResumenAnulaciones.TabIndex = 0;
        lblResumenAnulaciones.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dgvAnulaciones
        // 
        dgvAnulaciones.AllowUserToAddRows = false;
        dgvAnulaciones.AllowUserToDeleteRows = false;
        dgvAnulaciones.AllowUserToResizeRows = false;
        dgvAnulaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        dgvAnulaciones.BackgroundColor = SystemColors.ButtonHighlight;
        dgvAnulaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvAnulaciones.Dock = DockStyle.Fill;
        dgvAnulaciones.Location = new Point(3, 25);
        dgvAnulaciones.MultiSelect = false;
        dgvAnulaciones.Name = "dgvAnulaciones";
        dgvAnulaciones.ReadOnly = true;
        dgvAnulaciones.RowHeadersVisible = false;
        dgvAnulaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvAnulaciones.Size = new Size(422, 229);
        dgvAnulaciones.TabIndex = 1;
        // 
        // tabAnulaciones
        // 
        tabAnulaciones.Controls.Add(dgvAnulaciones);
        tabAnulaciones.Controls.Add(lblResumenAnulaciones);
        tabAnulaciones.Location = new Point(4, 24);
        tabAnulaciones.Name = "tabAnulaciones";
        tabAnulaciones.Padding = new Padding(3);
        tabAnulaciones.Size = new Size(428, 257);
        tabAnulaciones.TabIndex = 1;
        tabAnulaciones.Text = "Anulaciones";
        tabAnulaciones.UseVisualStyleBackColor = true;
        // 
        // tabInferior
        // 
        tabInferior.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tabInferior.Controls.Add(tabGastos);
        tabInferior.Controls.Add(tabAnulaciones);
        tabInferior.Location = new Point(460, 341);
        tabInferior.Name = "tabInferior";
        tabInferior.SelectedIndex = 0;
        tabInferior.Size = new Size(436, 285);
        tabInferior.TabIndex = 16;
        // 
        // lblPorEmpleado
        // 
        lblPorEmpleado.AutoSize = true;
        lblPorEmpleado.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblPorEmpleado.Location = new Point(460, 182);
        lblPorEmpleado.Name = "lblPorEmpleado";
        lblPorEmpleado.Size = new Size(129, 15);
        lblPorEmpleado.TabIndex = 17;
        lblPorEmpleado.Text = "Ventas por empleados";
        // 
        // dgvPorEmpleado
        // 
        dgvPorEmpleado.AllowUserToAddRows = false;
        dgvPorEmpleado.AllowUserToDeleteRows = false;
        dgvPorEmpleado.AllowUserToResizeRows = false;
        dgvPorEmpleado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        dgvPorEmpleado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvPorEmpleado.BackgroundColor = SystemColors.ButtonHighlight;
        dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = SystemColors.Control;
        dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
        dgvPorEmpleado.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
        dgvPorEmpleado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle5.BackColor = SystemColors.Window;
        dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
        dgvPorEmpleado.DefaultCellStyle = dataGridViewCellStyle5;
        dgvPorEmpleado.Location = new Point(460, 200);
        dgvPorEmpleado.Name = "dgvPorEmpleado";
        dgvPorEmpleado.ReadOnly = true;
        dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle6.BackColor = SystemColors.Control;
        dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
        dgvPorEmpleado.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
        dgvPorEmpleado.RowHeadersVisible = false;
        dgvPorEmpleado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPorEmpleado.Size = new Size(436, 120);
        dgvPorEmpleado.TabIndex = 18;
        // 
        // FormReporteDiario
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        ClientSize = new Size(908, 638);
        Controls.Add(dgvPorEmpleado);
        Controls.Add(lblPorEmpleado);
        Controls.Add(tabInferior);
        Controls.Add(lblGastosCategoria);
        Controls.Add(lblGananciaNeta);
        Controls.Add(lblGastos);
        Controls.Add(dgvMasVendidos);
        Controls.Add(lblMasVendidos);
        Controls.Add(dgvPorPago);
        Controls.Add(lblPorPago);
        Controls.Add(lblGananciaBruta);
        Controls.Add(btnImprimir);
        Controls.Add(lblTicket);
        Controls.Add(lblCantidad);
        Controls.Add(lblTotal);
        Controls.Add(btnActualizar);
        Controls.Add(dtpFecha);
        Controls.Add(lblFecha);
        Controls.Add(lblTitulo);
        Font = new Font("Segoe UI", 9F);
        Name = "FormReporteDiario";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Reporte Diario";
        Load += FormReporteDiario_Load;
        ((ISupportInitialize)dgvPorPago).EndInit();
        ((ISupportInitialize)dgvMasVendidos).EndInit();
        ((ISupportInitialize)dgvGastosCategoria).EndInit();
        ((ISupportInitialize)dgvPorEmpleado).EndInit();
        tabGastos.ResumeLayout(false);
        tabAnulaciones.ResumeLayout(false);
        ((ISupportInitialize)dgvAnulaciones).EndInit();
        tabInferior.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblGastos;
    private Label lblGananciaNeta;
    private Label lblGastosCategoria;
    private DataGridView dgvGastosCategoria;
    private Label lblPorEmpleado;
    private DataGridView dgvPorEmpleado;
    private TabControl tabInferior;
    private TabPage tabGastos;
    private TabPage tabAnulaciones;
    private Label lblResumenAnulaciones;
    private DataGridView dgvAnulaciones;
}
