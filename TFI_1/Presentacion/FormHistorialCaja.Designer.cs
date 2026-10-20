using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;

partial class FormHistorialCaja : Form
{
	private IContainer components = null;

	private Label lblTitulo;
	private Label lblDesde;
	private DateTimePicker dtpDesde;
	private Label lblHasta;
	private DateTimePicker dtpHasta;
	private Button btnBuscar;
	private Label lblTotal;
	private DataGridView dgvCajas;
	private Button btnCerrar;

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
		btnBuscar = new Button();
		lblTotal = new Label();
		dgvCajas = new DataGridView();
		btnCerrar = new Button();
		((ISupportInitialize)dgvCajas).BeginInit();
		SuspendLayout();
		//
		// lblTitulo
		//
		lblTitulo.AutoSize = true;
		lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
		lblTitulo.Location = new Point(12, 10);
		lblTitulo.Name = "lblTitulo";
		lblTitulo.Size = new Size(180, 25);
		lblTitulo.TabIndex = 0;
		lblTitulo.Text = "Historial de caja";
		//
		// lblDesde
		//
		lblDesde.AutoSize = true;
		lblDesde.Location = new Point(14, 52);
		lblDesde.Name = "lblDesde";
		lblDesde.Size = new Size(42, 15);
		lblDesde.TabIndex = 1;
		lblDesde.Text = "Desde:";
		//
		// dtpDesde
		//
		dtpDesde.Format = DateTimePickerFormat.Short;
		dtpDesde.Location = new Point(64, 49);
		dtpDesde.Name = "dtpDesde";
		dtpDesde.Size = new Size(115, 23);
		dtpDesde.TabIndex = 2;
		//
		// lblHasta
		//
		lblHasta.AutoSize = true;
		lblHasta.Location = new Point(190, 52);
		lblHasta.Name = "lblHasta";
		lblHasta.Size = new Size(40, 15);
		lblHasta.TabIndex = 3;
		lblHasta.Text = "Hasta:";
		//
		// dtpHasta
		//
		dtpHasta.Format = DateTimePickerFormat.Short;
		dtpHasta.Location = new Point(240, 49);
		dtpHasta.Name = "dtpHasta";
		dtpHasta.Size = new Size(115, 23);
		dtpHasta.TabIndex = 4;
		//
		// btnBuscar
		//
		btnBuscar.Location = new Point(370, 47);
		btnBuscar.Name = "btnBuscar";
		btnBuscar.Size = new Size(86, 28);
		btnBuscar.TabIndex = 5;
		btnBuscar.Text = "Buscar";
		btnBuscar.UseVisualStyleBackColor = true;
		btnBuscar.Click += btnBuscar_Click;
		//
		// lblTotal
		//
		lblTotal.AutoSize = true;
		lblTotal.Location = new Point(14, 82);
		lblTotal.Name = "lblTotal";
		lblTotal.Size = new Size(0, 15);
		lblTotal.TabIndex = 6;
		//
		// dgvCajas
		//
		dgvCajas.AllowUserToAddRows = false;
		dgvCajas.AllowUserToDeleteRows = false;
		dgvCajas.AllowUserToResizeRows = false;
		dgvCajas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		dgvCajas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		dgvCajas.BackgroundColor = SystemColors.ButtonHighlight;
		dgvCajas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dgvCajas.Location = new Point(12, 104);
		dgvCajas.MultiSelect = false;
		dgvCajas.Name = "dgvCajas";
		dgvCajas.ReadOnly = true;
		dgvCajas.RowHeadersVisible = false;
		dgvCajas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		dgvCajas.Size = new Size(876, 400);
		dgvCajas.TabIndex = 7;
		//
		// btnCerrar
		//
		btnCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		btnCerrar.Location = new Point(800, 518);
		btnCerrar.Name = "btnCerrar";
		btnCerrar.Size = new Size(88, 30);
		btnCerrar.TabIndex = 8;
		btnCerrar.Text = "Cerrar";
		btnCerrar.UseVisualStyleBackColor = true;
		btnCerrar.Click += btnCerrar_Click;
		//
		// FormHistorialCaja
		//
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = SystemColors.ActiveCaption;
		ClientSize = new Size(900, 560);
		Controls.Add(btnCerrar);
		Controls.Add(dgvCajas);
		Controls.Add(lblTotal);
		Controls.Add(btnBuscar);
		Controls.Add(dtpHasta);
		Controls.Add(lblHasta);
		Controls.Add(dtpDesde);
		Controls.Add(lblDesde);
		Controls.Add(lblTitulo);
		Font = new Font("Segoe UI", 9F);
		Name = "FormHistorialCaja";
		StartPosition = FormStartPosition.CenterParent;
		Text = "Historial de caja";
		((ISupportInitialize)dgvCajas).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}
}
