using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;
partial class FormIngresoMercaderia : Form
{
	private IContainer components = null;

	private TableLayoutPanel tlpPrincipal;
	private Label lblTitulo;
	private GroupBox grpCabecera;
	private TableLayoutPanel tlpCabecera;
	private Label lblProveedor;
	private ComboBox cboProveedor;
	private Label lblFecha;
	private DateTimePicker dtpFecha;
	private Label lblObservacion;
	private TextBox txtObservacion;
	private GroupBox grpDetalle;
	private TableLayoutPanel tlpDetalle;
	private Label lblProducto;
	private ComboBox cboProducto;
	private Label lblCantidad;
	private NumericUpDown numCantidad;
	private Label lblCosto;
	private NumericUpDown numCosto;
	private Button btnAgregar;
	private Button btnQuitar;
	private DataGridView dgvDetalle;
	private Panel pnlTotal;
	private Label lblTotalTitulo;
	private Label lblTotalValor;
	private Panel pnlBotones;
	private Button btnGuardar;
	private Button btnCerrar;
	private Label lblPrecioCostoTitulo;
	private Label lblPrecioCosto;
	private Label lblStockActualTitulo;
	private Label lblStockActual;

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
		grpCabecera = new GroupBox();
		tlpCabecera = new TableLayoutPanel();
		lblProveedor = new Label();
		cboProveedor = new ComboBox();
		lblFecha = new Label();
		dtpFecha = new DateTimePicker();
		lblObservacion = new Label();
		txtObservacion = new TextBox();
		grpDetalle = new GroupBox();
		tlpDetalle = new TableLayoutPanel();
		lblProducto = new Label();
		cboProducto = new ComboBox();
		lblCantidad = new Label();
		numCantidad = new NumericUpDown();
		lblCosto = new Label();
		numCosto = new NumericUpDown();
		btnAgregar = new Button();
		btnQuitar = new Button();
		lblPrecioCostoTitulo = new Label();
		lblPrecioCosto = new Label();
		lblStockActualTitulo = new Label();
		lblStockActual = new Label();
		dgvDetalle = new DataGridView();
		pnlTotal = new Panel();
		lblTotalTitulo = new Label();
		lblTotalValor = new Label();
		pnlBotones = new Panel();
		btnGuardar = new Button();
		btnCerrar = new Button();
		tlpPrincipal.SuspendLayout();
		grpCabecera.SuspendLayout();
		tlpCabecera.SuspendLayout();
		grpDetalle.SuspendLayout();
		tlpDetalle.SuspendLayout();
		((ISupportInitialize)numCantidad).BeginInit();
		((ISupportInitialize)numCosto).BeginInit();
		((ISupportInitialize)dgvDetalle).BeginInit();
		pnlTotal.SuspendLayout();
		pnlBotones.SuspendLayout();
		SuspendLayout();
		//
		// tlpPrincipal
		//
		tlpPrincipal.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		tlpPrincipal.ColumnCount = 1;
		tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tlpPrincipal.Controls.Add(lblTitulo, 0, 0);
		tlpPrincipal.Controls.Add(grpCabecera, 0, 1);
		tlpPrincipal.Controls.Add(grpDetalle, 0, 2);
		tlpPrincipal.Controls.Add(pnlTotal, 0, 3);
		tlpPrincipal.Controls.Add(pnlBotones, 0, 4);
		tlpPrincipal.Location = new Point(12, 12);
		tlpPrincipal.Name = "tlpPrincipal";
		tlpPrincipal.RowCount = 5;
		tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.AutoSize));
		tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.AutoSize));
		tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.AutoSize));
		tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.AutoSize));
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
		lblTitulo.Size = new Size(161, 31);
		lblTitulo.TabIndex = 0;
		lblTitulo.Text = "Ingreso Mercadería";
		//
		// grpCabecera
		//
		grpCabecera.Controls.Add(tlpCabecera);
		grpCabecera.Dock = DockStyle.Fill;
		grpCabecera.Location = new Point(3, 62);
		grpCabecera.Name = "grpCabecera";
		grpCabecera.Padding = new Padding(10);
		grpCabecera.Size = new Size(854, 88);
		grpCabecera.TabIndex = 1;
		grpCabecera.TabStop = false;
		grpCabecera.Text = "Cabecera del ingreso";
		//
		// tlpCabecera
		//
		tlpCabecera.ColumnCount = 8;
		tlpCabecera.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
		tlpCabecera.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
		tlpCabecera.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
		tlpCabecera.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
		tlpCabecera.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
		tlpCabecera.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
		tlpCabecera.Controls.Add(lblProveedor, 0, 0);
		tlpCabecera.Controls.Add(cboProveedor, 1, 0);
		tlpCabecera.Controls.Add(lblFecha, 2, 0);
		tlpCabecera.Controls.Add(dtpFecha, 3, 0);
		tlpCabecera.Controls.Add(lblObservacion, 4, 0);
		tlpCabecera.Controls.Add(txtObservacion, 5, 0);
		tlpCabecera.Dock = DockStyle.Fill;
		tlpCabecera.Location = new Point(10, 25);
		tlpCabecera.Name = "tlpCabecera";
		tlpCabecera.RowCount = 1;
		tlpCabecera.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
		tlpCabecera.Size = new Size(834, 32);
		tlpCabecera.TabIndex = 0;
		//
		// lblProveedor
		//
		lblProveedor.AutoSize = true;
		lblProveedor.Location = new Point(3, 0);
		lblProveedor.Name = "lblProveedor";
		lblProveedor.Size = new Size(66, 15);
		lblProveedor.TabIndex = 0;
		lblProveedor.Text = "Proveedor:";
		//
		// cboProveedor
		//
		cboProveedor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		cboProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
		cboProveedor.Location = new Point(75, 3);
		cboProveedor.Name = "cboProveedor";
		cboProveedor.Size = new Size(222, 23);
		cboProveedor.TabIndex = 1;
		//
		// lblFecha
		//
		lblFecha.AutoSize = true;
		lblFecha.Location = new Point(303, 0);
		lblFecha.Name = "lblFecha";
		lblFecha.Size = new Size(40, 15);
		lblFecha.TabIndex = 2;
		lblFecha.Text = "Fecha:";
		//
		// dtpFecha
		//
		dtpFecha.Format = DateTimePickerFormat.Short;
		dtpFecha.Location = new Point(349, 3);
		dtpFecha.Name = "dtpFecha";
		dtpFecha.Size = new Size(222, 23);
		dtpFecha.TabIndex = 3;
		//
		// lblObservacion
		//
		lblObservacion.AutoSize = true;
		lblObservacion.Location = new Point(577, 0);
		lblObservacion.Name = "lblObservacion";
		lblObservacion.Size = new Size(74, 15);
		lblObservacion.TabIndex = 4;
		lblObservacion.Text = "Observación:";
		//
		// txtObservacion
		//
		txtObservacion.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		txtObservacion.Location = new Point(657, 3);
		txtObservacion.Name = "txtObservacion";
		txtObservacion.Size = new Size(174, 23);
		txtObservacion.TabIndex = 5;
		//
		// grpDetalle
		//
		grpDetalle.Controls.Add(tlpDetalle);
		grpDetalle.Dock = DockStyle.Fill;
		grpDetalle.Location = new Point(3, 156);
		grpDetalle.Name = "grpDetalle";
		grpDetalle.Padding = new Padding(10);
		grpDetalle.Size = new Size(854, 225);
		grpDetalle.TabIndex = 2;
		grpDetalle.TabStop = false;
		grpDetalle.Text = "Detalle de productos";
		//
		// tlpDetalle
		//
		tlpDetalle.ColumnCount = 8;
		tlpDetalle.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
		tlpDetalle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
		tlpDetalle.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
		tlpDetalle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
		tlpDetalle.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
		tlpDetalle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
		tlpDetalle.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
		tlpDetalle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
		tlpDetalle.Controls.Add(lblProducto, 0, 0);
		tlpDetalle.Controls.Add(cboProducto, 1, 0);
		tlpDetalle.Controls.Add(lblCantidad, 2, 0);
		tlpDetalle.Controls.Add(numCantidad, 3, 0);
		tlpDetalle.Controls.Add(lblCosto, 4, 0);
		tlpDetalle.Controls.Add(numCosto, 5, 0);
		tlpDetalle.Controls.Add(btnAgregar, 6, 0);
		tlpDetalle.Controls.Add(btnQuitar, 7, 0);
		tlpDetalle.Controls.Add(lblPrecioCostoTitulo, 0, 1);
		tlpDetalle.Controls.Add(lblPrecioCosto, 1, 1);
		tlpDetalle.Controls.Add(lblStockActualTitulo, 2, 1);
		tlpDetalle.Controls.Add(lblStockActual, 3, 1);
		tlpDetalle.Controls.Add(dgvDetalle, 0, 2);
		tlpDetalle.SetColumnSpan(dgvDetalle, 8);
		tlpDetalle.Dock = DockStyle.Fill;
		tlpDetalle.Location = new Point(10, 25);
		tlpDetalle.Name = "tlpDetalle";
		tlpDetalle.RowCount = 3;
		tlpDetalle.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
		tlpDetalle.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
		tlpDetalle.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tlpDetalle.Size = new Size(834, 180);
		tlpDetalle.TabIndex = 0;
		//
		// lblProducto
		//
		lblProducto.AutoSize = true;
		lblProducto.Location = new Point(3, 0);
		lblProducto.Name = "lblProducto";
		lblProducto.Size = new Size(57, 15);
		lblProducto.TabIndex = 0;
		lblProducto.Text = "Producto:";
		//
		// cboProducto
		//
		cboProducto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		cboProducto.DropDownStyle = ComboBoxStyle.DropDownList;
		cboProducto.Location = new Point(123, 3);
		cboProducto.Name = "cboProducto";
		cboProducto.Size = new Size(180, 23);
		cboProducto.TabIndex = 1;
		cboProducto.SelectedIndexChanged += cboProducto_SelectedIndexChanged;
		//
		// lblCantidad
		//
		lblCantidad.AutoSize = true;
		lblCantidad.Location = new Point(309, 0);
		lblCantidad.Name = "lblCantidad";
		lblCantidad.Size = new Size(58, 15);
		lblCantidad.TabIndex = 2;
		lblCantidad.Text = "Cantidad:";
		//
		// numCantidad
		//
		numCantidad.Location = new Point(373, 3);
		numCantidad.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
		numCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
		numCantidad.Name = "numCantidad";
		numCantidad.Size = new Size(60, 23);
		numCantidad.TabIndex = 3;
		numCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
		//
		// lblCosto
		//
		lblCosto.AutoSize = true;
		lblCosto.Location = new Point(439, 0);
		lblCosto.Name = "lblCosto";
		lblCosto.Size = new Size(70, 15);
		lblCosto.TabIndex = 4;
		lblCosto.Text = "Costo unit.:";
		//
		// numCosto
		//
		numCosto.DecimalPlaces = 2;
		numCosto.Location = new Point(515, 3);
		numCosto.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
		numCosto.Name = "numCosto";
		numCosto.Size = new Size(180, 23);
		numCosto.TabIndex = 4;
		//
		// btnAgregar
		//
		btnAgregar.Location = new Point(701, 3);
		btnAgregar.Name = "btnAgregar";
		btnAgregar.Size = new Size(80, 26);
		btnAgregar.TabIndex = 5;
		btnAgregar.Text = "Agregar";
		btnAgregar.UseVisualStyleBackColor = true;
		btnAgregar.Click += btnAgregar_Click;
		//
		// btnQuitar
		//
		btnQuitar.Location = new Point(787, 3);
		btnQuitar.Name = "btnQuitar";
		btnQuitar.Size = new Size(80, 26);
		btnQuitar.TabIndex = 6;
		btnQuitar.Text = "Quitar";
		btnQuitar.UseVisualStyleBackColor = true;
		btnQuitar.Click += btnQuitar_Click;
		//
		// lblPrecioCostoTitulo
		//
		lblPrecioCostoTitulo.AutoSize = true;
		lblPrecioCostoTitulo.Location = new Point(3, 32);
		lblPrecioCostoTitulo.Name = "lblPrecioCostoTitulo";
		lblPrecioCostoTitulo.Size = new Size(74, 15);
		lblPrecioCostoTitulo.TabIndex = 7;
		lblPrecioCostoTitulo.Text = "Costo previo:";
		//
		// lblPrecioCosto
		//
		lblPrecioCosto.AutoSize = true;
		lblPrecioCosto.Location = new Point(123, 32);
		lblPrecioCosto.Name = "lblPrecioCosto";
		lblPrecioCosto.Size = new Size(0, 15);
		lblPrecioCosto.TabIndex = 8;
		//
		// lblStockActualTitulo
		//
		lblStockActualTitulo.AutoSize = true;
		lblStockActualTitulo.Location = new Point(309, 32);
		lblStockActualTitulo.Name = "lblStockActualTitulo";
		lblStockActualTitulo.Size = new Size(72, 15);
		lblStockActualTitulo.TabIndex = 9;
		lblStockActualTitulo.Text = "Stock actual:";
		//
		// lblStockActual
		//
		lblStockActual.AutoSize = true;
		lblStockActual.Location = new Point(373, 32);
		lblStockActual.Name = "lblStockActual";
		lblStockActual.Size = new Size(0, 15);
		lblStockActual.TabIndex = 10;
		//
		// dgvDetalle
		//
		dgvDetalle.AllowUserToAddRows = false;
		dgvDetalle.AllowUserToDeleteRows = false;
		dgvDetalle.AllowUserToResizeRows = false;
		dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dgvDetalle.Location = new Point(3, 59);
		dgvDetalle.Name = "dgvDetalle";
		dgvDetalle.ReadOnly = true;
		dgvDetalle.RowHeadersVisible = false;
		dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		dgvDetalle.Size = new Size(828, 118);
		dgvDetalle.TabIndex = 11;
		//
		// pnlTotal
		//
		pnlTotal.Controls.Add(lblTotalTitulo);
		pnlTotal.Controls.Add(lblTotalValor);
		pnlTotal.Dock = DockStyle.Fill;
		pnlTotal.Location = new Point(3, 387);
		pnlTotal.Name = "pnlTotal";
		pnlTotal.Size = new Size(854, 34);
		pnlTotal.TabIndex = 3;
		//
		// lblTotalTitulo
		//
		lblTotalTitulo.AutoSize = true;
		lblTotalTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
		lblTotalTitulo.Location = new Point(3, 6);
		lblTotalTitulo.Name = "lblTotalTitulo";
		lblTotalTitulo.Size = new Size(48, 20);
		lblTotalTitulo.TabIndex = 0;
		lblTotalTitulo.Text = "Total:";
		//
		// lblTotalValor
		//
		lblTotalValor.AutoSize = true;
		lblTotalValor.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
		lblTotalValor.ForeColor = Color.FromArgb(0, 102, 51);
		lblTotalValor.Location = new Point(57, 6);
		lblTotalValor.Name = "lblTotalValor";
		lblTotalValor.Size = new Size(48, 20);
		lblTotalValor.TabIndex = 1;
		lblTotalValor.Text = "$0,00";
		//
		// pnlBotones
		//
		pnlBotones.Controls.Add(btnGuardar);
		pnlBotones.Controls.Add(btnCerrar);
		pnlBotones.Dock = DockStyle.Fill;
		pnlBotones.Location = new Point(3, 427);
		pnlBotones.Name = "pnlBotones";
		pnlBotones.Size = new Size(854, 36);
		pnlBotones.TabIndex = 4;
		//
		// btnGuardar
		//
		btnGuardar.Location = new Point(3, 3);
		btnGuardar.Name = "btnGuardar";
		btnGuardar.Size = new Size(100, 30);
		btnGuardar.TabIndex = 0;
		btnGuardar.Text = "Registrar ingreso";
		btnGuardar.UseVisualStyleBackColor = true;
		btnGuardar.Click += btnGuardar_Click;
		//
		// btnCerrar
		//
		btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		btnCerrar.DialogResult = DialogResult.Cancel;
		btnCerrar.Location = new Point(771, 3);
		btnCerrar.Name = "btnCerrar";
		btnCerrar.Size = new Size(80, 30);
		btnCerrar.TabIndex = 1;
		btnCerrar.Text = "Cerrar";
		btnCerrar.UseVisualStyleBackColor = true;
		btnCerrar.Click += btnCerrar_Click;
		//
		// FormIngresoMercaderia
		//
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = SystemColors.ActiveCaption;
		CancelButton = btnCerrar;
		ClientSize = new Size(884, 560);
		Controls.Add(tlpPrincipal);
		Font = new Font("Segoe UI", 9F);
		Name = "FormIngresoMercaderia";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Ingreso de Mercadería";
		Load += FormIngresoMercaderia_Load;
		tlpPrincipal.ResumeLayout(false);
		tlpPrincipal.PerformLayout();
		grpCabecera.ResumeLayout(false);
		tlpCabecera.ResumeLayout(false);
		tlpCabecera.PerformLayout();
		grpDetalle.ResumeLayout(false);
		tlpDetalle.ResumeLayout(false);
		tlpDetalle.PerformLayout();
		((ISupportInitialize)numCantidad).EndInit();
		((ISupportInitialize)numCosto).EndInit();
		((ISupportInitialize)dgvDetalle).EndInit();
		pnlTotal.ResumeLayout(false);
		pnlTotal.PerformLayout();
		pnlBotones.ResumeLayout(false);
		ResumeLayout(false);
		PerformLayout();
	}
}
