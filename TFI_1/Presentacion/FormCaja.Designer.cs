using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;

partial class FormCaja : Form
{
	private IContainer components = null;

	private Label lblTitulo;
	private Label lblEstado;
	private Label lblApertura;
	private Label lblResumen;
	private Label lblMontoInicial;
	private NumericUpDown numMontoInicial;
	private Button btnAbrir;
	private Label lblMontoMov;
	private NumericUpDown numMontoMov;
	private Label lblConceptoMov;
	private TextBox txtConceptoMov;
	private Button btnIngreso;
	private Button btnEgreso;
	private Label lblVentasTitulo;
	private DataGridView dgvResumen;
	private Label lblMovsTitulo;
	private DataGridView dgvMovimientos;
	private TabControl tabDetalle;
	private TabPage tabMovimientos;
	private TabPage tabAnulaciones;
	private Label lblResumenAnulaciones;
	private DataGridView dgvAnulaciones;
	private Label lblEsperado;
	private Button btnActualizar;
	private Button btnHistorial;
	private Button btnCerrarCaja;

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
		lblEstado = new Label();
		lblApertura = new Label();
		lblResumen = new Label();
		lblMontoInicial = new Label();
		numMontoInicial = new NumericUpDown();
		btnAbrir = new Button();
		lblMontoMov = new Label();
		numMontoMov = new NumericUpDown();
		lblConceptoMov = new Label();
		txtConceptoMov = new TextBox();
		btnIngreso = new Button();
		btnEgreso = new Button();
		lblVentasTitulo = new Label();
		dgvResumen = new DataGridView();
		lblMovsTitulo = new Label();
		dgvMovimientos = new DataGridView();
		tabDetalle = new TabControl();
		tabMovimientos = new TabPage();
		tabAnulaciones = new TabPage();
		lblResumenAnulaciones = new Label();
		dgvAnulaciones = new DataGridView();
		lblEsperado = new Label();
		btnActualizar = new Button();
		btnHistorial = new Button();
		btnCerrarCaja = new Button();
		((ISupportInitialize)numMontoInicial).BeginInit();
		((ISupportInitialize)numMontoMov).BeginInit();
		((ISupportInitialize)dgvResumen).BeginInit();
		((ISupportInitialize)dgvMovimientos).BeginInit();
		tabDetalle.SuspendLayout();
		tabMovimientos.SuspendLayout();
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
		lblTitulo.Size = new Size(60, 25);
		lblTitulo.TabIndex = 0;
		lblTitulo.Text = "Caja";
		//
		// lblEstado
		//
		lblEstado.AutoSize = true;
		lblEstado.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
		lblEstado.Location = new Point(12, 45);
		lblEstado.Name = "lblEstado";
		lblEstado.Size = new Size(150, 30);
		lblEstado.TabIndex = 1;
		lblEstado.Text = "Caja cerrada";
		//
		// lblApertura
		//
		lblApertura.AutoSize = true;
		lblApertura.Location = new Point(12, 82);
		lblApertura.Name = "lblApertura";
		lblApertura.Size = new Size(420, 15);
		lblApertura.TabIndex = 2;
		lblApertura.Text = "No hay una caja abierta.";
		//
		// lblResumen
		//
		lblResumen.Location = new Point(12, 108);
		lblResumen.Name = "lblResumen";
		lblResumen.Size = new Size(420, 100);
		lblResumen.TabIndex = 3;
		//
		// lblMontoInicial
		//
		lblMontoInicial.AutoSize = true;
		lblMontoInicial.Location = new Point(16, 225);
		lblMontoInicial.Name = "lblMontoInicial";
		lblMontoInicial.Size = new Size(81, 15);
		lblMontoInicial.TabIndex = 4;
		lblMontoInicial.Text = "Monto inicial:";
		//
		// numMontoInicial
		//
		numMontoInicial.DecimalPlaces = 2;
		numMontoInicial.Location = new Point(110, 222);
		numMontoInicial.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
		numMontoInicial.Name = "numMontoInicial";
		numMontoInicial.Size = new Size(120, 23);
		numMontoInicial.TabIndex = 5;
		numMontoInicial.ThousandsSeparator = true;
		//
		// btnAbrir
		//
		btnAbrir.Location = new Point(240, 218);
		btnAbrir.Name = "btnAbrir";
		btnAbrir.Size = new Size(100, 30);
		btnAbrir.TabIndex = 6;
		btnAbrir.Text = "Abrir caja";
		btnAbrir.UseVisualStyleBackColor = true;
		btnAbrir.Click += btnAbrir_Click;
		//
		// lblMontoMov
		//
		lblMontoMov.AutoSize = true;
		lblMontoMov.Location = new Point(16, 265);
		lblMontoMov.Name = "lblMontoMov";
		lblMontoMov.Size = new Size(44, 15);
		lblMontoMov.TabIndex = 7;
		lblMontoMov.Text = "Monto:";
		//
		// numMontoMov
		//
		numMontoMov.DecimalPlaces = 2;
		numMontoMov.Location = new Point(70, 262);
		numMontoMov.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
		numMontoMov.Name = "numMontoMov";
		numMontoMov.Size = new Size(110, 23);
		numMontoMov.TabIndex = 8;
		numMontoMov.ThousandsSeparator = true;
		//
		// lblConceptoMov
		//
		lblConceptoMov.AutoSize = true;
		lblConceptoMov.Location = new Point(192, 265);
		lblConceptoMov.Name = "lblConceptoMov";
		lblConceptoMov.Size = new Size(57, 15);
		lblConceptoMov.TabIndex = 9;
		lblConceptoMov.Text = "Concepto:";
		//
		// txtConceptoMov
		//
		txtConceptoMov.Location = new Point(258, 262);
		txtConceptoMov.MaxLength = 150;
		txtConceptoMov.Name = "txtConceptoMov";
		txtConceptoMov.Size = new Size(220, 23);
		txtConceptoMov.TabIndex = 10;
		//
		// btnIngreso
		//
		btnIngreso.Location = new Point(488, 260);
		btnIngreso.Name = "btnIngreso";
		btnIngreso.Size = new Size(90, 30);
		btnIngreso.TabIndex = 11;
		btnIngreso.Text = "Ingreso";
		btnIngreso.UseVisualStyleBackColor = true;
		btnIngreso.Click += btnIngreso_Click;
		//
		// btnEgreso
		//
		btnEgreso.Location = new Point(584, 260);
		btnEgreso.Name = "btnEgreso";
		btnEgreso.Size = new Size(90, 30);
		btnEgreso.TabIndex = 12;
		btnEgreso.Text = "Egreso";
		btnEgreso.UseVisualStyleBackColor = true;
		btnEgreso.Click += btnEgreso_Click;
		//
		// lblVentasTitulo
		//
		lblVentasTitulo.AutoSize = true;
		lblVentasTitulo.Location = new Point(16, 305);
		lblVentasTitulo.Name = "lblVentasTitulo";
		lblVentasTitulo.Size = new Size(216, 15);
		lblVentasTitulo.TabIndex = 13;
		lblVentasTitulo.Text = "Ventas del turno por medio de pago";
		//
		// dgvResumen
		//
		dgvResumen.AllowUserToAddRows = false;
		dgvResumen.AllowUserToDeleteRows = false;
		dgvResumen.AllowUserToResizeRows = false;
		dgvResumen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
		dgvResumen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		dgvResumen.BackgroundColor = SystemColors.ButtonHighlight;
		dgvResumen.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dgvResumen.Location = new Point(16, 327);
		dgvResumen.MultiSelect = false;
		dgvResumen.Name = "dgvResumen";
		dgvResumen.ReadOnly = true;
		dgvResumen.RowHeadersVisible = false;
		dgvResumen.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		dgvResumen.Size = new Size(410, 150);
		dgvResumen.TabIndex = 14;
		//
		// lblMovsTitulo
		//
		lblMovsTitulo.AutoSize = true;
		lblMovsTitulo.Location = new Point(446, 305);
		lblMovsTitulo.Name = "lblMovsTitulo";
		lblMovsTitulo.Size = new Size(118, 15);
		lblMovsTitulo.TabIndex = 15;
		lblMovsTitulo.Text = "Movimientos y anulaciones";
		//
		// dgvMovimientos
		//
		dgvMovimientos.AllowUserToAddRows = false;
		dgvMovimientos.AllowUserToDeleteRows = false;
		dgvMovimientos.AllowUserToResizeRows = false;
		dgvMovimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		dgvMovimientos.BackgroundColor = SystemColors.ButtonHighlight;
		dgvMovimientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dgvMovimientos.Dock = DockStyle.Fill;
		dgvMovimientos.Location = new Point(3, 3);
		dgvMovimientos.MultiSelect = false;
		dgvMovimientos.Name = "dgvMovimientos";
		dgvMovimientos.ReadOnly = true;
		dgvMovimientos.RowHeadersVisible = false;
		dgvMovimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		dgvMovimientos.Size = new Size(422, 116);
		dgvMovimientos.TabIndex = 0;
		//
		// tabMovimientos
		//
		tabMovimientos.Controls.Add(dgvMovimientos);
		tabMovimientos.Location = new Point(4, 24);
		tabMovimientos.Name = "tabMovimientos";
		tabMovimientos.Padding = new Padding(3);
		tabMovimientos.Size = new Size(428, 122);
		tabMovimientos.TabIndex = 0;
		tabMovimientos.Text = "Movimientos";
		tabMovimientos.UseVisualStyleBackColor = true;
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
		dgvAnulaciones.Size = new Size(422, 94);
		dgvAnulaciones.TabIndex = 1;
		//
		// tabAnulaciones
		//
		tabAnulaciones.Controls.Add(dgvAnulaciones);
		tabAnulaciones.Controls.Add(lblResumenAnulaciones);
		tabAnulaciones.Location = new Point(4, 24);
		tabAnulaciones.Name = "tabAnulaciones";
		tabAnulaciones.Padding = new Padding(3);
		tabAnulaciones.Size = new Size(428, 122);
		tabAnulaciones.TabIndex = 1;
		tabAnulaciones.Text = "Anulaciones";
		tabAnulaciones.UseVisualStyleBackColor = true;
		//
		// tabDetalle
		//
		tabDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		tabDetalle.Controls.Add(tabMovimientos);
		tabDetalle.Controls.Add(tabAnulaciones);
		tabDetalle.Location = new Point(446, 327);
		tabDetalle.Name = "tabDetalle";
		tabDetalle.SelectedIndex = 0;
		tabDetalle.Size = new Size(436, 150);
		tabDetalle.TabIndex = 16;
		//
		// lblEsperado
		//
		lblEsperado.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		lblEsperado.AutoSize = true;
		lblEsperado.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
		lblEsperado.Location = new Point(16, 492);
		lblEsperado.Name = "lblEsperado";
		lblEsperado.Size = new Size(200, 21);
		lblEsperado.TabIndex = 17;
		//
		// btnActualizar
		//
		btnActualizar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		btnActualizar.Location = new Point(16, 520);
		btnActualizar.Name = "btnActualizar";
		btnActualizar.Size = new Size(120, 30);
		btnActualizar.TabIndex = 18;
		btnActualizar.Text = "Actualizar";
		btnActualizar.UseVisualStyleBackColor = true;
		btnActualizar.Click += btnActualizar_Click;
		//
		// btnHistorial
		//
		btnHistorial.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		btnHistorial.Location = new Point(592, 520);
		btnHistorial.Name = "btnHistorial";
		btnHistorial.Size = new Size(120, 30);
		btnHistorial.TabIndex = 19;
		btnHistorial.Text = "Historial";
		btnHistorial.UseVisualStyleBackColor = true;
		btnHistorial.Click += btnHistorial_Click;
		//
		// btnCerrarCaja
		//
		btnCerrarCaja.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		btnCerrarCaja.Location = new Point(718, 520);
		btnCerrarCaja.Name = "btnCerrarCaja";
		btnCerrarCaja.Size = new Size(164, 30);
		btnCerrarCaja.TabIndex = 20;
		btnCerrarCaja.Text = "Cerrar caja";
		btnCerrarCaja.UseVisualStyleBackColor = true;
		btnCerrarCaja.Click += btnCerrarCaja_Click;
		//
		// FormCaja
		//
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = SystemColors.ActiveCaption;
		ClientSize = new Size(900, 566);
		Controls.Add(btnCerrarCaja);
		Controls.Add(btnHistorial);
		Controls.Add(btnActualizar);
		Controls.Add(lblEsperado);
		Controls.Add(tabDetalle);
		Controls.Add(lblMovsTitulo);
		Controls.Add(dgvResumen);
		Controls.Add(lblVentasTitulo);
		Controls.Add(btnEgreso);
		Controls.Add(btnIngreso);
		Controls.Add(txtConceptoMov);
		Controls.Add(lblConceptoMov);
		Controls.Add(numMontoMov);
		Controls.Add(lblMontoMov);
		Controls.Add(btnAbrir);
		Controls.Add(numMontoInicial);
		Controls.Add(lblMontoInicial);
		Controls.Add(lblResumen);
		Controls.Add(lblApertura);
		Controls.Add(lblEstado);
		Controls.Add(lblTitulo);
		Font = new Font("Segoe UI", 9F);
		Name = "FormCaja";
		StartPosition = FormStartPosition.CenterParent;
		Text = "Caja";
		((ISupportInitialize)numMontoInicial).EndInit();
		((ISupportInitialize)numMontoMov).EndInit();
		((ISupportInitialize)dgvResumen).EndInit();
		((ISupportInitialize)dgvMovimientos).EndInit();
		tabMovimientos.ResumeLayout(false);
		tabAnulaciones.ResumeLayout(false);
		((ISupportInitialize)dgvAnulaciones).EndInit();
		tabDetalle.ResumeLayout(false);
		ResumeLayout(false);
		PerformLayout();
	}
}
