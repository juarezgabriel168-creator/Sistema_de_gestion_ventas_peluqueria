using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;
partial class FormNuevaVenta : Form
{
	private IContainer components = null;

	private Label lblTitulo;

	private Label lblCliente;

	private ComboBox cboCliente;

	private Label lblMedioPago;

	private ComboBox cboMedioPago;

	private Label lblItem;

	private ComboBox cboItem;

	private Label lblCantidad;

	private NumericUpDown numCantidad;

	private Button btnAgregar;

	private Label lblInfo;

	private DataGridView dgvCarrito;

	private Button btnQuitar;

	private Label lblSubtotalTitulo;

	private Label lblSubtotalValor;

	private Label lblDescuentoTitulo;

	private Label lblDescuentoValor;

	private NumericUpDown numDescuento;

	private Label lblTotalTitulo;

	private Label lblTotalValor;

	private Button btnRegistrar;

	private Button btnCancelar;

	private Label lblEmpleado;

	private ComboBox cboEmpleado;

	private Label lblBanco;

	private ComboBox cboBanco;

	private Label lblTarjeta;

	private ComboBox cboTarjeta;

	private Label lblCuotas;

	private NumericUpDown numCuotas;

	private Label lblTarjetaInfo;

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
        lblTitulo = new Label();
        lblCliente = new Label();
        cboCliente = new ComboBox();
        lblMedioPago = new Label();
        cboMedioPago = new ComboBox();
        lblItem = new Label();
        cboItem = new ComboBox();
        lblCantidad = new Label();
        numCantidad = new NumericUpDown();
        btnAgregar = new Button();
        lblInfo = new Label();
        dgvCarrito = new DataGridView();
        btnQuitar = new Button();
        lblSubtotalTitulo = new Label();
        lblSubtotalValor = new Label();
        lblDescuentoTitulo = new Label();
        lblDescuentoValor = new Label();
        numDescuento = new NumericUpDown();
        lblTotalTitulo = new Label();
        lblTotalValor = new Label();
        btnRegistrar = new Button();
        btnCancelar = new Button();
        lblEmpleado = new Label();
        cboEmpleado = new ComboBox();
        lblBanco = new Label();
        cboBanco = new ComboBox();
        lblTarjeta = new Label();
        cboTarjeta = new ComboBox();
        lblCuotas = new Label();
        numCuotas = new NumericUpDown();
        lblTarjetaInfo = new Label();
        lblDescuento2 = new Label();
        ((ISupportInitialize)numCantidad).BeginInit();
        ((ISupportInitialize)dgvCarrito).BeginInit();
        ((ISupportInitialize)numDescuento).BeginInit();
        ((ISupportInitialize)numCuotas).BeginInit();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.Location = new Point(12, 10);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(123, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Nueva venta";
        // 
        // lblCliente
        // 
        lblCliente.AutoSize = true;
        lblCliente.Location = new Point(12, 50);
        lblCliente.Name = "lblCliente";
        lblCliente.Size = new Size(47, 15);
        lblCliente.TabIndex = 1;
        lblCliente.Text = "Cliente:";
        // 
        // cboCliente
        // 
        cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCliente.FormattingEnabled = true;
        cboCliente.Location = new Point(12, 68);
        cboCliente.Name = "cboCliente";
        cboCliente.Size = new Size(280, 23);
        cboCliente.TabIndex = 2;
        // 
        // lblMedioPago
        // 
        lblMedioPago.AutoSize = true;
        lblMedioPago.Location = new Point(310, 50);
        lblMedioPago.Name = "lblMedioPago";
        lblMedioPago.Size = new Size(90, 15);
        lblMedioPago.TabIndex = 3;
        lblMedioPago.Text = "Medio de pago:";
        // 
        // cboMedioPago
        // 
        cboMedioPago.DropDownStyle = ComboBoxStyle.DropDownList;
        cboMedioPago.FormattingEnabled = true;
        cboMedioPago.Location = new Point(310, 68);
        cboMedioPago.Name = "cboMedioPago";
        cboMedioPago.Size = new Size(170, 23);
        cboMedioPago.TabIndex = 4;
        cboMedioPago.SelectedIndexChanged += cboMedioPago_SelectedIndexChanged;
        // 
        // lblItem
        // 
        lblItem.AutoSize = true;
        lblItem.Location = new Point(12, 104);
        lblItem.Name = "lblItem";
        lblItem.Size = new Size(112, 15);
        lblItem.TabIndex = 5;
        lblItem.Text = "Producto o servicio:";
        // 
        // cboItem
        // 
        cboItem.DropDownStyle = ComboBoxStyle.DropDownList;
        cboItem.FormattingEnabled = true;
        cboItem.Location = new Point(12, 122);
        cboItem.Name = "cboItem";
        cboItem.Size = new Size(320, 23);
        cboItem.TabIndex = 6;
        cboItem.SelectedIndexChanged += cboItem_SelectedIndexChanged;
        // 
        // lblCantidad
        // 
        lblCantidad.AutoSize = true;
        lblCantidad.Location = new Point(344, 104);
        lblCantidad.Name = "lblCantidad";
        lblCantidad.Size = new Size(58, 15);
        lblCantidad.TabIndex = 7;
        lblCantidad.Text = "Cantidad:";
        // 
        // numCantidad
        // 
        numCantidad.Location = new Point(344, 122);
        numCantidad.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        numCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numCantidad.Name = "numCantidad";
        numCantidad.Size = new Size(60, 23);
        numCantidad.TabIndex = 8;
        numCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // btnAgregar
        // 
        btnAgregar.Location = new Point(547, 120);
        btnAgregar.Name = "btnAgregar";
        btnAgregar.Size = new Size(100, 26);
        btnAgregar.TabIndex = 9;
        btnAgregar.Text = "Agregar";
        btnAgregar.UseVisualStyleBackColor = true;
        btnAgregar.Click += btnAgregar_Click;
        // 
        // lblInfo
        // 
        lblInfo.AutoSize = true;
        lblInfo.ForeColor = Color.DimGray;
        lblInfo.Location = new Point(528, 126);
        lblInfo.Name = "lblInfo";
        lblInfo.Size = new Size(0, 15);
        lblInfo.TabIndex = 10;
        // 
        // dgvCarrito
        // 
        dgvCarrito.AllowUserToAddRows = false;
        dgvCarrito.AllowUserToDeleteRows = false;
        dgvCarrito.AllowUserToResizeRows = false;
        dgvCarrito.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        dgvCarrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvCarrito.BackgroundColor = SystemColors.ButtonHighlight;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = SystemColors.Control;
        dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        dgvCarrito.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        dgvCarrito.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = SystemColors.Window;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        dgvCarrito.DefaultCellStyle = dataGridViewCellStyle2;
        dgvCarrito.Location = new Point(12, 198);
        dgvCarrito.MultiSelect = false;
        dgvCarrito.Name = "dgvCarrito";
        dgvCarrito.ReadOnly = true;
        dgvCarrito.RowHeadersVisible = false;
        dgvCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCarrito.Size = new Size(560, 296);
        dgvCarrito.TabIndex = 11;
        // 
        // btnQuitar
        // 
        btnQuitar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnQuitar.Location = new Point(12, 502);
        btnQuitar.Name = "btnQuitar";
        btnQuitar.Size = new Size(139, 40);
        btnQuitar.TabIndex = 12;
        btnQuitar.Text = "Quitar ítem";
        btnQuitar.UseVisualStyleBackColor = true;
        btnQuitar.Click += btnQuitar_Click;
        // 
        // lblSubtotalTitulo
        // 
        lblSubtotalTitulo.AutoSize = true;
        lblSubtotalTitulo.Font = new Font("Segoe UI", 10F);
        lblSubtotalTitulo.Location = new Point(704, 176);
        lblSubtotalTitulo.Name = "lblSubtotalTitulo";
        lblSubtotalTitulo.Size = new Size(63, 19);
        lblSubtotalTitulo.TabIndex = 13;
        lblSubtotalTitulo.Text = "Subtotal:";
        // 
        // lblSubtotalValor
        // 
        lblSubtotalValor.AutoSize = true;
        lblSubtotalValor.Font = new Font("Segoe UI", 10F);
        lblSubtotalValor.Location = new Point(844, 179);
        lblSubtotalValor.Name = "lblSubtotalValor";
        lblSubtotalValor.Size = new Size(44, 19);
        lblSubtotalValor.TabIndex = 14;
        lblSubtotalValor.Text = "$0,00";
        // 
        // lblDescuentoTitulo
        // 
        lblDescuentoTitulo.AutoSize = true;
        lblDescuentoTitulo.Font = new Font("Segoe UI", 10F);
        lblDescuentoTitulo.Location = new Point(690, 208);
        lblDescuentoTitulo.Name = "lblDescuentoTitulo";
        lblDescuentoTitulo.Size = new Size(77, 19);
        lblDescuentoTitulo.TabIndex = 15;
        lblDescuentoTitulo.Text = "Descuento:";
        // 
        // lblDescuentoValor
        // 
        lblDescuentoValor.AutoSize = true;
        lblDescuentoValor.Font = new Font("Segoe UI", 10F);
        lblDescuentoValor.Location = new Point(844, 208);
        lblDescuentoValor.Name = "lblDescuentoValor";
        lblDescuentoValor.Size = new Size(44, 19);
        lblDescuentoValor.TabIndex = 16;
        lblDescuentoValor.Text = "$0,00";
        // 
        // numDescuento
        // 
        numDescuento.DecimalPlaces = 2;
        numDescuento.Location = new Point(420, 122);
        numDescuento.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
        numDescuento.Name = "numDescuento";
        numDescuento.Size = new Size(102, 23);
        numDescuento.TabIndex = 16;
        numDescuento.ValueChanged += numDescuento_ValueChanged;
        // 
        // lblTotalTitulo
        // 
        lblTotalTitulo.AutoSize = true;
        lblTotalTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTotalTitulo.Location = new Point(694, 253);
        lblTotalTitulo.Name = "lblTotalTitulo";
        lblTotalTitulo.Size = new Size(73, 25);
        lblTotalTitulo.TabIndex = 17;
        lblTotalTitulo.Text = "TOTAL:";
        // 
        // lblTotalValor
        // 
        lblTotalValor.AutoSize = true;
        lblTotalValor.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTotalValor.ForeColor = Color.FromArgb(0, 102, 51);
        lblTotalValor.Location = new Point(817, 253);
        lblTotalValor.Name = "lblTotalValor";
        lblTotalValor.Size = new Size(71, 30);
        lblTotalValor.TabIndex = 18;
        lblTotalValor.Text = "$0,00";
        // 
        // btnRegistrar
        // 
        btnRegistrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnRegistrar.Location = new Point(588, 500);
        btnRegistrar.Name = "btnRegistrar";
        btnRegistrar.Size = new Size(140, 42);
        btnRegistrar.TabIndex = 19;
        btnRegistrar.Text = "Registrar venta";
        btnRegistrar.UseVisualStyleBackColor = true;
        btnRegistrar.Click += btnRegistrar_Click;
        // 
        // btnCancelar
        // 
        btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancelar.DialogResult = DialogResult.Cancel;
        btnCancelar.Location = new Point(740, 500);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(140, 42);
        btnCancelar.TabIndex = 20;
        btnCancelar.Text = "Cancelar";
        btnCancelar.UseVisualStyleBackColor = true;
        btnCancelar.Click += btnCancelar_Click;
        // 
        // lblEmpleado
        // 
        lblEmpleado.AutoSize = true;
        lblEmpleado.Location = new Point(12, 148);
        lblEmpleado.Name = "lblEmpleado";
        lblEmpleado.Size = new Size(63, 15);
        lblEmpleado.TabIndex = 21;
        lblEmpleado.Text = "Empleado:";
        // 
        // cboEmpleado
        // 
        cboEmpleado.DropDownStyle = ComboBoxStyle.DropDownList;
        cboEmpleado.FormattingEnabled = true;
        cboEmpleado.Location = new Point(12, 166);
        cboEmpleado.Name = "cboEmpleado";
        cboEmpleado.Size = new Size(200, 23);
        cboEmpleado.TabIndex = 22;
        // 
        // lblBanco
        // 
        lblBanco.AutoSize = true;
        lblBanco.Location = new Point(224, 148);
        lblBanco.Name = "lblBanco";
        lblBanco.Size = new Size(43, 15);
        lblBanco.TabIndex = 23;
        lblBanco.Text = "Banco:";
        // 
        // cboBanco
        // 
        cboBanco.DropDownStyle = ComboBoxStyle.DropDownList;
        cboBanco.FormattingEnabled = true;
        cboBanco.Location = new Point(224, 166);
        cboBanco.Name = "cboBanco";
        cboBanco.Size = new Size(170, 23);
        cboBanco.TabIndex = 24;
        // 
        // lblTarjeta
        // 
        lblTarjeta.AutoSize = true;
        lblTarjeta.Location = new Point(406, 148);
        lblTarjeta.Name = "lblTarjeta";
        lblTarjeta.Size = new Size(86, 15);
        lblTarjeta.TabIndex = 25;
        lblTarjeta.Text = "Tipo de tarjeta:";
        // 
        // cboTarjeta
        // 
        cboTarjeta.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTarjeta.FormattingEnabled = true;
        cboTarjeta.Location = new Point(406, 166);
        cboTarjeta.Name = "cboTarjeta";
        cboTarjeta.Size = new Size(150, 23);
        cboTarjeta.TabIndex = 26;
        // 
        // lblCuotas
        // 
        lblCuotas.AutoSize = true;
        lblCuotas.Location = new Point(587, 148);
        lblCuotas.Name = "lblCuotas";
        lblCuotas.Size = new Size(47, 15);
        lblCuotas.TabIndex = 27;
        lblCuotas.Text = "Cuotas:";
        // 
        // numCuotas
        // 
        numCuotas.Location = new Point(587, 166);
        numCuotas.Maximum = new decimal(new int[] { 36, 0, 0, 0 });
        numCuotas.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numCuotas.Name = "numCuotas";
        numCuotas.Size = new Size(60, 23);
        numCuotas.TabIndex = 28;
        numCuotas.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // lblTarjetaInfo
        // 
        lblTarjetaInfo.AutoSize = true;
        lblTarjetaInfo.ForeColor = Color.DimGray;
        lblTarjetaInfo.Location = new Point(224, 176);
        lblTarjetaInfo.Name = "lblTarjetaInfo";
        lblTarjetaInfo.Size = new Size(0, 15);
        lblTarjetaInfo.TabIndex = 29;
        // 
        // lblDescuento2
        // 
        lblDescuento2.AutoSize = true;
        lblDescuento2.Location = new Point(420, 104);
        lblDescuento2.Name = "lblDescuento2";
        lblDescuento2.Size = new Size(66, 15);
        lblDescuento2.TabIndex = 30;
        lblDescuento2.Text = "Descuento:";
        // 
        // FormNuevaVenta
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        ClientSize = new Size(900, 560);
        Controls.Add(lblDescuento2);
        Controls.Add(lblEmpleado);
        Controls.Add(cboEmpleado);
        Controls.Add(lblBanco);
        Controls.Add(cboBanco);
        Controls.Add(lblTarjeta);
        Controls.Add(cboTarjeta);
        Controls.Add(lblCuotas);
        Controls.Add(numCuotas);
        Controls.Add(lblTarjetaInfo);
        Controls.Add(btnCancelar);
        Controls.Add(btnRegistrar);
        Controls.Add(lblTotalValor);
        Controls.Add(lblTotalTitulo);
        Controls.Add(numDescuento);
        Controls.Add(lblDescuentoTitulo);
        Controls.Add(lblDescuentoValor);
        Controls.Add(lblSubtotalValor);
        Controls.Add(lblSubtotalTitulo);
        Controls.Add(btnQuitar);
        Controls.Add(dgvCarrito);
        Controls.Add(lblInfo);
        Controls.Add(btnAgregar);
        Controls.Add(numCantidad);
        Controls.Add(lblCantidad);
        Controls.Add(cboItem);
        Controls.Add(lblItem);
        Controls.Add(cboMedioPago);
        Controls.Add(lblMedioPago);
        Controls.Add(cboCliente);
        Controls.Add(lblCliente);
        Controls.Add(lblTitulo);
        Font = new Font("Segoe UI", 9F);
        Name = "FormNuevaVenta";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Nueva Venta";
        Load += FormNuevaVenta_Load;
        ((ISupportInitialize)numCantidad).EndInit();
        ((ISupportInitialize)dgvCarrito).EndInit();
        ((ISupportInitialize)numDescuento).EndInit();
        ((ISupportInitialize)numCuotas).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblDescuento2;
}
