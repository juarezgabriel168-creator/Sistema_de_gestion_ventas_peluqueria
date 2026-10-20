using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
partial class FormItems : Form
{
	private IContainer components = null;

	private Label lblTitulo;

	private Label lblTipo;

	private ComboBox cboTipo;

	private Label lblCategoria;

	private ComboBox cboCategoria;

	private TextBox txtBuscar;

	private Button btnBuscar;

	private Button btnNuevo;

	private Button btnEditar;

	private Button btnDesactivar;

	private Button btnRefrescar;

	private DataGridView dgvItems;

	private CheckBox chkSoloActivos;

	private DataGridViewTextBoxColumn colNombre;

	private DataGridViewTextBoxColumn colTipo;

	private DataGridViewTextBoxColumn colCategoria;

	private DataGridViewTextBoxColumn colCategoriaNombre;

	private DataGridViewTextBoxColumn colPrecio;

	private DataGridViewTextBoxColumn colStock;

	private DataGridViewCheckBoxColumn colStockBajo;

	private DataGridViewCheckBoxColumn colActivo;

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
        lblTipo = new Label();
        cboTipo = new ComboBox();
        lblCategoria = new Label();
        cboCategoria = new ComboBox();
        txtBuscar = new TextBox();
        btnBuscar = new Button();
        btnNuevo = new Button();
        btnEditar = new Button();
        btnDesactivar = new Button();
        btnRefrescar = new Button();
        chkSoloActivos = new CheckBox();
        dgvItems = new DataGridView();
        ((ISupportInitialize)dgvItems).BeginInit();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.Location = new Point(12, 10);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(203, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Productos y Servicios";
        // 
        // lblTipo
        // 
        lblTipo.AutoSize = true;
        lblTipo.Location = new Point(12, 50);
        lblTipo.Name = "lblTipo";
        lblTipo.Size = new Size(34, 15);
        lblTipo.TabIndex = 1;
        lblTipo.Text = "Tipo:";
        // 
        // cboTipo
        // 
        cboTipo.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTipo.FormattingEnabled = true;
        cboTipo.Location = new Point(50, 47);
        cboTipo.Name = "cboTipo";
        cboTipo.Size = new Size(110, 23);
        cboTipo.TabIndex = 2;
        cboTipo.SelectedIndexChanged += Filtro_Changed;
        // 
        // lblCategoria
        // 
        lblCategoria.AutoSize = true;
        lblCategoria.Location = new Point(175, 50);
        lblCategoria.Name = "lblCategoria";
        lblCategoria.Size = new Size(61, 15);
        lblCategoria.TabIndex = 3;
        lblCategoria.Text = "Categoría:";
        // 
        // cboCategoria
        // 
        cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCategoria.FormattingEnabled = true;
        cboCategoria.Location = new Point(242, 47);
        cboCategoria.Name = "cboCategoria";
        cboCategoria.Size = new Size(160, 23);
        cboCategoria.TabIndex = 4;
        cboCategoria.SelectedIndexChanged += Filtro_Changed;
        // 
        // txtBuscar
        // 
        txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        txtBuscar.Location = new Point(540, 47);
        txtBuscar.Name = "txtBuscar";
        txtBuscar.PlaceholderText = "Buscar por nombre...";
        txtBuscar.Size = new Size(220, 23);
        txtBuscar.TabIndex = 5;
        // 
        // btnBuscar
        // 
        btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnBuscar.Location = new Point(768, 46);
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Size = new Size(80, 25);
        btnBuscar.TabIndex = 6;
        btnBuscar.Text = "Buscar";
        btnBuscar.UseVisualStyleBackColor = true;
        btnBuscar.Click += btnBuscar_Click;
        // 
        // btnNuevo
        // 
        btnNuevo.Location = new Point(12, 82);
        btnNuevo.Name = "btnNuevo";
        btnNuevo.Size = new Size(90, 28);
        btnNuevo.TabIndex = 7;
        btnNuevo.Text = "Nuevo";
        btnNuevo.UseVisualStyleBackColor = true;
        btnNuevo.Click += btnNuevo_Click;
        // 
        // btnEditar
        // 
        btnEditar.Location = new Point(108, 82);
        btnEditar.Name = "btnEditar";
        btnEditar.Size = new Size(90, 28);
        btnEditar.TabIndex = 8;
        btnEditar.Text = "Editar";
        btnEditar.UseVisualStyleBackColor = true;
        btnEditar.Click += btnEditar_Click;
        // 
        // btnDesactivar
        // 
        btnDesactivar.Location = new Point(204, 82);
        btnDesactivar.Name = "btnDesactivar";
        btnDesactivar.Size = new Size(110, 28);
        btnDesactivar.TabIndex = 9;
        btnDesactivar.Text = "Desactivar";
        btnDesactivar.UseVisualStyleBackColor = true;
        btnDesactivar.Click += btnDesactivar_Click;
        // 
        // btnRefrescar
        // 
        btnRefrescar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefrescar.Location = new Point(748, 82);
        btnRefrescar.Name = "btnRefrescar";
        btnRefrescar.Size = new Size(100, 28);
        btnRefrescar.TabIndex = 10;
        btnRefrescar.Text = "Refrescar";
        btnRefrescar.UseVisualStyleBackColor = true;
        btnRefrescar.Click += btnRefrescar_Click;
        // 
        // colNombre
        // 
        colNombre = new DataGridViewTextBoxColumn();
        colNombre.DataPropertyName = "Nombre";
        colNombre.HeaderText = "Nombre";
        colNombre.Name = "colNombre";
        colNombre.ReadOnly = true;
        // 
        // colTipo
        // 
        colTipo = new DataGridViewTextBoxColumn();
        colTipo.DataPropertyName = "Tipo";
        colTipo.HeaderText = "Tipo";
        colTipo.Name = "colTipo";
        colTipo.ReadOnly = true;
        // 
        // colCategoria
        // 
        colCategoria = new DataGridViewTextBoxColumn();
        colCategoria.DataPropertyName = "CategoriaNombre";
        colCategoria.HeaderText = "Categoría";
        colCategoria.Name = "colCategoria";
        colCategoria.ReadOnly = true;
        // 
        // colPrecio
        // 
        colPrecio = new DataGridViewTextBoxColumn();
        colPrecio.DataPropertyName = "Precio";
        colPrecio.HeaderText = "Precio";
        colPrecio.Name = "colPrecio";
        colPrecio.ReadOnly = true;
        colPrecio.DefaultCellStyle.Format = "C2";
        // 
        // colStock
        // 
        colStock = new DataGridViewTextBoxColumn();
        colStock.DataPropertyName = "Stock";
        colStock.HeaderText = "Stock";
        colStock.Name = "colStock";
        colStock.ReadOnly = true;
        // 
        // colStockBajo
        // 
        colStockBajo = new DataGridViewCheckBoxColumn();
        colStockBajo.DataPropertyName = "StockBajo";
        colStockBajo.HeaderText = "Stock bajo";
        colStockBajo.Name = "colStockBajo";
        colStockBajo.ReadOnly = true;
        // 
        // colActivo
        // 
        colActivo = new DataGridViewCheckBoxColumn();
        colActivo.DataPropertyName = "Activo";
        colActivo.HeaderText = "Activo";
        colActivo.Name = "colActivo";
        colActivo.ReadOnly = true;
        // 
        // dgvItems
        // 
        dgvItems.AllowUserToAddRows = false;
        dgvItems.AllowUserToDeleteRows = false;
        dgvItems.AllowUserToResizeRows = false;
        dgvItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvItems.AutoGenerateColumns = false;
        dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvItems.Location = new Point(12, 120);
        dgvItems.MultiSelect = false;
        dgvItems.Name = "dgvItems";
        dgvItems.SelectionChanged += dgvItems_SelectionChanged;
        dgvItems.ReadOnly = true;
        dgvItems.RowHeadersVisible = false;
        dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvItems.Size = new Size(836, 368);
        dgvItems.TabIndex = 11;
        dgvItems.Columns.AddRange(new DataGridViewColumn[] {
        colNombre,
        colTipo,
        colCategoria,
        colPrecio,
        colStock,
        colStockBajo,
        colActivo});
        // 
        // chkSoloActivos
        // 
        chkSoloActivos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        chkSoloActivos.AutoSize = true;
        chkSoloActivos.Checked = true;
        chkSoloActivos.CheckState = CheckState.Checked;
        chkSoloActivos.Location = new Point(748, 16);
        chkSoloActivos.Name = "chkSoloActivos";
        chkSoloActivos.Size = new Size(100, 19);
        chkSoloActivos.TabIndex = 8;
        chkSoloActivos.Text = "Solo Activos";
        chkSoloActivos.UseVisualStyleBackColor = true;
        chkSoloActivos.CheckedChanged += chkSoloActivos_CheckedChanged;
        // 
        // FormItems
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        ClientSize = new Size(860, 500);
        Controls.Add(dgvItems);
        Controls.Add(chkSoloActivos);
        Controls.Add(btnRefrescar);
        Controls.Add(btnDesactivar);
        Controls.Add(btnEditar);
        Controls.Add(btnNuevo);
        Controls.Add(btnBuscar);
        Controls.Add(txtBuscar);
        Controls.Add(cboCategoria);
        Controls.Add(lblCategoria);
        Controls.Add(cboTipo);
        Controls.Add(lblTipo);
        Controls.Add(lblTitulo);
        Font = new Font("Segoe UI", 9F);
        Name = "FormItems";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Productos y Servicios";
        Load += FormItems_Load;
        ((ISupportInitialize)dgvItems).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
