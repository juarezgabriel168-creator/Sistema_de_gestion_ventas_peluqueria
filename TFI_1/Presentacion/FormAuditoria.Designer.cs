using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;

partial class FormAuditoria : Form
{
	private IContainer components = null;

	private Label lblTitulo;
	private Label lblDesde;
	private DateTimePicker dtpDesde;
	private Label lblHasta;
	private DateTimePicker dtpHasta;
	private Label lblUsuario;
	private ComboBox cboUsuario;
	private Label lblAccion;
	private ComboBox cboAccion;
	private Button btnBuscar;
	private Label lblTotal;
	private DataGridView dgvAuditoria;
	private Button btnCerrar;
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
		lblAccion = new Label();
		cboAccion = new ComboBox();
		btnBuscar = new Button();
		lblTotal = new Label();
		dgvAuditoria = new DataGridView();
		btnCerrar = new Button();
		lblPagina = new Label();
		btnAnterior = new Button();
		btnSiguiente = new Button();
		((ISupportInitialize)dgvAuditoria).BeginInit();
		SuspendLayout();
		//
		// lblTitulo
		//
		lblTitulo.AutoSize = true;
		lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
		lblTitulo.Location = new Point(12, 10);
		lblTitulo.Name = "lblTitulo";
		lblTitulo.Size = new Size(100, 25);
		lblTitulo.TabIndex = 0;
		lblTitulo.Text = "Auditoría";
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
		lblHasta.Location = new Point(188, 52);
		lblHasta.Name = "lblHasta";
		lblHasta.Size = new Size(40, 15);
		lblHasta.TabIndex = 3;
		lblHasta.Text = "Hasta:";
		//
		// dtpHasta
		//
		dtpHasta.Format = DateTimePickerFormat.Short;
		dtpHasta.Location = new Point(238, 49);
		dtpHasta.Name = "dtpHasta";
		dtpHasta.Size = new Size(115, 23);
		dtpHasta.TabIndex = 4;
		//
		// lblUsuario
		//
		lblUsuario.AutoSize = true;
		lblUsuario.Location = new Point(362, 52);
		lblUsuario.Name = "lblUsuario";
		lblUsuario.Size = new Size(50, 15);
		lblUsuario.TabIndex = 5;
		lblUsuario.Text = "Usuario:";
		//
		// cboUsuario
		//
		cboUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
		cboUsuario.FormattingEnabled = true;
		cboUsuario.Location = new Point(420, 49);
		cboUsuario.Name = "cboUsuario";
		cboUsuario.Size = new Size(150, 23);
		cboUsuario.TabIndex = 6;
		//
		// lblAccion
		//
		lblAccion.AutoSize = true;
		lblAccion.Location = new Point(582, 52);
		lblAccion.Name = "lblAccion";
		lblAccion.Size = new Size(44, 15);
		lblAccion.TabIndex = 7;
		lblAccion.Text = "Acción:";
		//
		// cboAccion
		//
		cboAccion.DropDownStyle = ComboBoxStyle.DropDownList;
		cboAccion.FormattingEnabled = true;
		cboAccion.Location = new Point(634, 49);
		cboAccion.Name = "cboAccion";
		cboAccion.Size = new Size(160, 23);
		cboAccion.TabIndex = 8;
		//
		// btnBuscar
		//
		btnBuscar.Location = new Point(802, 47);
		btnBuscar.Name = "btnBuscar";
		btnBuscar.Size = new Size(86, 28);
		btnBuscar.TabIndex = 9;
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
		lblTotal.TabIndex = 10;
		//
		// dgvAuditoria
		//
		dgvAuditoria.AllowUserToAddRows = false;
		dgvAuditoria.AllowUserToDeleteRows = false;
		dgvAuditoria.AllowUserToResizeRows = false;
		dgvAuditoria.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		dgvAuditoria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		dgvAuditoria.BackgroundColor = SystemColors.ButtonHighlight;
		dgvAuditoria.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dgvAuditoria.Location = new Point(12, 104);
		dgvAuditoria.MultiSelect = false;
		dgvAuditoria.Name = "dgvAuditoria";
		dgvAuditoria.ReadOnly = true;
		dgvAuditoria.RowHeadersVisible = false;
		dgvAuditoria.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		dgvAuditoria.Size = new Size(876, 400);
		dgvAuditoria.TabIndex = 11;
		//
		// btnCerrar
		//
		btnCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		btnCerrar.Location = new Point(806, 518);
		btnCerrar.Name = "btnCerrar";
		btnCerrar.Size = new Size(82, 30);
		btnCerrar.TabIndex = 12;
		btnCerrar.Text = "Cerrar";
		btnCerrar.UseVisualStyleBackColor = true;
		btnCerrar.Click += btnCerrar_Click;
		//
		// btnAnterior
		//
		btnAnterior.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		btnAnterior.Location = new Point(12, 518);
		btnAnterior.Name = "btnAnterior";
		btnAnterior.Size = new Size(90, 30);
		btnAnterior.TabIndex = 13;
		btnAnterior.Text = "◀ Anterior";
		btnAnterior.UseVisualStyleBackColor = true;
		btnAnterior.Click += btnAnterior_Click;
		//
		// lblPagina
		//
		lblPagina.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		lblPagina.Location = new Point(110, 523);
		lblPagina.Name = "lblPagina";
		lblPagina.Size = new Size(200, 20);
		lblPagina.TabIndex = 14;
		lblPagina.Text = "Página 1 de 1";
		lblPagina.TextAlign = ContentAlignment.MiddleCenter;
		//
		// btnSiguiente
		//
		btnSiguiente.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		btnSiguiente.Location = new Point(318, 518);
		btnSiguiente.Name = "btnSiguiente";
		btnSiguiente.Size = new Size(90, 30);
		btnSiguiente.TabIndex = 15;
		btnSiguiente.Text = "Siguiente ▶";
		btnSiguiente.UseVisualStyleBackColor = true;
		btnSiguiente.Click += btnSiguiente_Click;
		//
		// FormAuditoria
		//
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = SystemColors.ActiveCaption;
		ClientSize = new Size(900, 560);
		Controls.Add(btnCerrar);
		Controls.Add(btnAnterior);
		Controls.Add(lblPagina);
		Controls.Add(btnSiguiente);
		Controls.Add(dgvAuditoria);
		Controls.Add(lblTotal);
		Controls.Add(btnBuscar);
		Controls.Add(cboAccion);
		Controls.Add(lblAccion);
		Controls.Add(cboUsuario);
		Controls.Add(lblUsuario);
		Controls.Add(dtpHasta);
		Controls.Add(lblHasta);
		Controls.Add(dtpDesde);
		Controls.Add(lblDesde);
		Controls.Add(lblTitulo);
		Font = new Font("Segoe UI", 9F);
		Name = "FormAuditoria";
		StartPosition = FormStartPosition.CenterParent;
		Text = "Auditoría";
		((ISupportInitialize)dgvAuditoria).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}
}
