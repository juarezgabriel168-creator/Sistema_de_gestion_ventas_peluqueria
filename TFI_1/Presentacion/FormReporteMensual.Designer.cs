using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;
partial class FormReporteMensual : Form
{
	private IContainer components = null;

	private Label lblTitulo;

	private Label lblMes;

	private DateTimePicker dtpMes;

	private Button btnActualizar;

	private Button btnImprimir;

	private Label lblTotal;

	private Label lblCantidad;

	private Label lblTicket;

	private Label lblDias;

	private Label lblGananciaBruta;

	private Label lblPorDia;

	private DataGridView dgvPorDia;

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
        lblMes = new Label();
        dtpMes = new DateTimePicker();
        btnActualizar = new Button();
        btnImprimir = new Button();
        lblTotal = new Label();
        lblCantidad = new Label();
        lblTicket = new Label();
        lblDias = new Label();
        lblGananciaBruta = new Label();
        lblPorDia = new Label();
        dgvPorDia = new DataGridView();
        lblMasVendidos = new Label();
        dgvMasVendidos = new DataGridView();
        lblProEmpleado = new Label();
        dgvPorEmpleado = new DataGridView();
        lblGastos = new Label();
        lblGananciaNeta = new Label();
        lblGastosCategoria = new Label();
        dgvGastosCategoria = new DataGridView();
        tabInferior = new TabControl();
        tabGastos = new TabPage();
        tabAnulaciones = new TabPage();
        lblResumenAnulaciones = new Label();
        dgvAnulaciones = new DataGridView();
        ((ISupportInitialize)dgvPorDia).BeginInit();
        ((ISupportInitialize)dgvMasVendidos).BeginInit();
        ((ISupportInitialize)dgvPorEmpleado).BeginInit();
        ((ISupportInitialize)dgvGastosCategoria).BeginInit();
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
        lblTitulo.Size = new Size(163, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Reporte mensual";
        // 
        // lblMes
        // 
        lblMes.AutoSize = true;
        lblMes.Location = new Point(12, 52);
        lblMes.Name = "lblMes";
        lblMes.Size = new Size(32, 15);
        lblMes.TabIndex = 1;
        lblMes.Text = "Mes:";
        // 
        // dtpMes
        // 
        dtpMes.CustomFormat = "MMMM yyyy";
        dtpMes.Format = DateTimePickerFormat.Custom;
        dtpMes.Location = new Point(50, 50);
        dtpMes.Name = "dtpMes";
        dtpMes.ShowUpDown = true;
        dtpMes.Size = new Size(150, 23);
        dtpMes.TabIndex = 2;
        // 
        // btnActualizar
        // 
        btnActualizar.Location = new Point(212, 48);
        btnActualizar.Name = "btnActualizar";
        btnActualizar.Size = new Size(110, 26);
        btnActualizar.TabIndex = 3;
        btnActualizar.Text = "Actualizar";
        btnActualizar.UseVisualStyleBackColor = true;
        btnActualizar.Click += btnActualizar_Click;
        // 
        // btnImprimir
        // 
        btnImprimir.Location = new Point(332, 48);
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
        lblTotal.Location = new Point(12, 82);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(118, 20);
        lblTotal.TabIndex = 5;
        lblTotal.Text = "Vendido: $ 0,00";
        // 
        // lblCantidad
        // 
        lblCantidad.AutoSize = true;
        lblCantidad.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblCantidad.Location = new Point(230, 82);
        lblCantidad.Name = "lblCantidad";
        lblCantidad.Size = new Size(73, 20);
        lblCantidad.TabIndex = 6;
        lblCantidad.Text = "Ventas: 0";
        // 
        // lblTicket
        // 
        lblTicket.AutoSize = true;
        lblTicket.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblTicket.Location = new Point(410, 82);
        lblTicket.Name = "lblTicket";
        lblTicket.Size = new Size(175, 20);
        lblTicket.TabIndex = 7;
        lblTicket.Text = "Ticket promedio: $ 0,00";
        // 
        // lblDias
        // 
        lblDias.AutoSize = true;
        lblDias.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblDias.Location = new Point(600, 82);
        lblDias.Name = "lblDias";
        lblDias.Size = new Size(135, 20);
        lblDias.TabIndex = 8;
        lblDias.Text = "Días con ventas: 0";
        // 
        // lblGananciaBruta
        // 
        lblGananciaBruta.AutoSize = true;
        lblGananciaBruta.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblGananciaBruta.Location = new Point(12, 114);
        lblGananciaBruta.Name = "lblGananciaBruta";
        lblGananciaBruta.Size = new Size(214, 19);
        lblGananciaBruta.TabIndex = 9;
        lblGananciaBruta.Text = "Ganancia bruta del mes: $ 0,00";
        // 
        // lblPorDia
        // 
        lblPorDia.AutoSize = true;
        lblPorDia.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPorDia.Location = new Point(12, 146);
        lblPorDia.Name = "lblPorDia";
        lblPorDia.Size = new Size(85, 15);
        lblPorDia.TabIndex = 10;
        lblPorDia.Text = "Ventas por día";
        // 
        // dgvPorDia
        // 
        dgvPorDia.AllowUserToAddRows = false;
        dgvPorDia.AllowUserToDeleteRows = false;
        dgvPorDia.AllowUserToResizeRows = false;
        dgvPorDia.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        dgvPorDia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvPorDia.BackgroundColor = SystemColors.ButtonHighlight;
        dgvPorDia.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvPorDia.Location = new Point(12, 168);
        dgvPorDia.Name = "dgvPorDia";
        dgvPorDia.ReadOnly = true;
        dgvPorDia.RowHeadersVisible = false;
        dgvPorDia.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPorDia.Size = new Size(796, 150);
        dgvPorDia.TabIndex = 11;
        // 
        // lblMasVendidos
        // 
        lblMasVendidos.AutoSize = true;
        lblMasVendidos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblMasVendidos.Location = new Point(12, 330);
        lblMasVendidos.Name = "lblMasVendidos";
        lblMasVendidos.Size = new Size(201, 15);
        lblMasVendidos.TabIndex = 12;
        lblMasVendidos.Text = "Productos y servicios más vendidos";
        // 
        // dgvMasVendidos
        // 
        dgvMasVendidos.AllowUserToAddRows = false;
        dgvMasVendidos.AllowUserToDeleteRows = false;
        dgvMasVendidos.AllowUserToResizeRows = false;
        dgvMasVendidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvMasVendidos.BackgroundColor = SystemColors.ButtonHighlight;
        dgvMasVendidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvMasVendidos.Location = new Point(12, 352);
        dgvMasVendidos.Name = "dgvMasVendidos";
        dgvMasVendidos.ReadOnly = true;
        dgvMasVendidos.RowHeadersVisible = false;
        dgvMasVendidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvMasVendidos.Size = new Size(382, 150);
        dgvMasVendidos.TabIndex = 13;
        // 
        // lblProEmpleado
        // 
        lblProEmpleado.AutoSize = true;
        lblProEmpleado.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblProEmpleado.Location = new Point(12, 514);
        lblProEmpleado.Name = "lblProEmpleado";
        lblProEmpleado.Size = new Size(119, 15);
        lblProEmpleado.TabIndex = 14;
        lblProEmpleado.Text = "Ventas por Empleado";
        // 
        // dgvPorEmpleado
        // 
        dgvPorEmpleado.AllowUserToAddRows = false;
        dgvPorEmpleado.AllowUserToDeleteRows = false;
        dgvPorEmpleado.AllowUserToResizeRows = false;
        dgvPorEmpleado.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvPorEmpleado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvPorEmpleado.BackgroundColor = SystemColors.ButtonHighlight;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = SystemColors.Control;
        dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        dgvPorEmpleado.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        dgvPorEmpleado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = SystemColors.Window;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        dgvPorEmpleado.DefaultCellStyle = dataGridViewCellStyle2;
        dgvPorEmpleado.Location = new Point(12, 536);
        dgvPorEmpleado.Name = "dgvPorEmpleado";
        dgvPorEmpleado.ReadOnly = true;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = SystemColors.Control;
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
        dgvPorEmpleado.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
        dgvPorEmpleado.RowHeadersVisible = false;
        dgvPorEmpleado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPorEmpleado.Size = new Size(796, 153);
        dgvPorEmpleado.TabIndex = 15;
        // 
        // lblGastos
        // 
        lblGastos.AutoSize = true;
        lblGastos.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblGastos.Location = new Point(260, 114);
        lblGastos.Name = "lblGastos";
        lblGastos.Size = new Size(145, 17);
        lblGastos.TabIndex = 16;
        lblGastos.Text = "Gastos del mes: $ 0,00";
        // 
        // lblGananciaNeta
        // 
        lblGananciaNeta.AutoSize = true;
        lblGananciaNeta.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblGananciaNeta.Location = new Point(450, 114);
        lblGananciaNeta.Name = "lblGananciaNeta";
        lblGananciaNeta.Size = new Size(201, 17);
        lblGananciaNeta.TabIndex = 17;
        lblGananciaNeta.Text = "Ganancias neta del mes : $ 0,00";
        // 
        // lblGastosCategoria
        // 
        lblGastosCategoria.AutoSize = true;
        lblGastosCategoria.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblGastosCategoria.Location = new Point(416, 330);
        lblGastosCategoria.Name = "lblGastosCategoria";
        lblGastosCategoria.Size = new Size(121, 15);
        lblGastosCategoria.TabIndex = 18;
        lblGastosCategoria.Text = "Gastos y anulaciones";
        // 
        // dgvGastosCategoria
        // 
        dgvGastosCategoria.AllowUserToAddRows = false;
        dgvGastosCategoria.AllowUserToDeleteRows = false;
        dgvGastosCategoria.AllowUserToResizeRows = false;
        dgvGastosCategoria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvGastosCategoria.BackgroundColor = SystemColors.ButtonHighlight;
        dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = SystemColors.Control;
        dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
        dgvGastosCategoria.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
        dgvGastosCategoria.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle5.BackColor = SystemColors.Window;
        dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
        dgvGastosCategoria.DefaultCellStyle = dataGridViewCellStyle5;
        dgvGastosCategoria.Dock = DockStyle.Fill;
        dgvGastosCategoria.Location = new Point(3, 3);
        dgvGastosCategoria.Name = "dgvGastosCategoria";
        dgvGastosCategoria.ReadOnly = true;
        dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle6.BackColor = SystemColors.Control;
        dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
        dgvGastosCategoria.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
        dgvGastosCategoria.RowHeadersVisible = false;
        dgvGastosCategoria.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvGastosCategoria.Size = new Size(378, 116);
        dgvGastosCategoria.TabIndex = 0;
        // 
        // tabGastos
        // 
        tabGastos.Controls.Add(dgvGastosCategoria);
        tabGastos.Location = new Point(4, 24);
        tabGastos.Name = "tabGastos";
        tabGastos.Padding = new Padding(3);
        tabGastos.Size = new Size(384, 122);
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
        lblResumenAnulaciones.Size = new Size(378, 22);
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
        dgvAnulaciones.Size = new Size(378, 94);
        dgvAnulaciones.TabIndex = 1;
        // 
        // tabAnulaciones
        // 
        tabAnulaciones.Controls.Add(dgvAnulaciones);
        tabAnulaciones.Controls.Add(lblResumenAnulaciones);
        tabAnulaciones.Location = new Point(4, 24);
        tabAnulaciones.Name = "tabAnulaciones";
        tabAnulaciones.Padding = new Padding(3);
        tabAnulaciones.Size = new Size(384, 122);
        tabAnulaciones.TabIndex = 1;
        tabAnulaciones.Text = "Anulaciones";
        tabAnulaciones.UseVisualStyleBackColor = true;
        // 
        // tabInferior
        // 
        tabInferior.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        tabInferior.Controls.Add(tabGastos);
        tabInferior.Controls.Add(tabAnulaciones);
        tabInferior.Location = new Point(416, 352);
        tabInferior.Name = "tabInferior";
        tabInferior.SelectedIndex = 0;
        tabInferior.Size = new Size(392, 150);
        tabInferior.TabIndex = 19;
        // 
        // FormReporteMensual
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        ClientSize = new Size(820, 671);
        Controls.Add(tabInferior);
        Controls.Add(lblGastosCategoria);
        Controls.Add(lblGananciaNeta);
        Controls.Add(lblGastos);
        Controls.Add(dgvPorEmpleado);
        Controls.Add(lblProEmpleado);
        Controls.Add(dgvMasVendidos);
        Controls.Add(lblMasVendidos);
        Controls.Add(dgvPorDia);
        Controls.Add(lblPorDia);
        Controls.Add(lblGananciaBruta);
        Controls.Add(btnImprimir);
        Controls.Add(lblDias);
        Controls.Add(lblTicket);
        Controls.Add(lblCantidad);
        Controls.Add(lblTotal);
        Controls.Add(btnActualizar);
        Controls.Add(dtpMes);
        Controls.Add(lblMes);
        Controls.Add(lblTitulo);
        Font = new Font("Segoe UI", 9F);
        Name = "FormReporteMensual";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Reporte Mensual";
        Load += FormReporteMensual_Load;
        ((ISupportInitialize)dgvPorDia).EndInit();
        ((ISupportInitialize)dgvMasVendidos).EndInit();
        ((ISupportInitialize)dgvPorEmpleado).EndInit();
        ((ISupportInitialize)dgvGastosCategoria).EndInit();
        tabGastos.ResumeLayout(false);
        tabAnulaciones.ResumeLayout(false);
        ((ISupportInitialize)dgvAnulaciones).EndInit();
        tabInferior.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblProEmpleado;
    private DataGridView dgvPorEmpleado;
    private Label lblGastos;
    private Label lblGananciaNeta;
    private Label lblGastosCategoria;
    private DataGridView dgvGastosCategoria;
    private TabControl tabInferior;
    private TabPage tabGastos;
    private TabPage tabAnulaciones;
    private Label lblResumenAnulaciones;
    private DataGridView dgvAnulaciones;
}
