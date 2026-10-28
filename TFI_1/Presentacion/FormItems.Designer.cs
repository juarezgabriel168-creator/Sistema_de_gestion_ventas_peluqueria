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

	private Button btnEliminar;

	private Button btnRefrescar;

	private DataGridView dgvItems;

	private DataGridViewTextBoxColumn colNombre;

	private DataGridViewTextBoxColumn colTipo;

	private DataGridViewTextBoxColumn colCategoria;

	private DataGridViewTextBoxColumn colPrecio;

	private DataGridViewTextBoxColumn colCosto;

	private DataGridViewTextBoxColumn colStock;

	private DataGridViewTextBoxColumn colStockMinimo;

	private DataGridViewTextBoxColumn colDuracion;

	private DataGridViewCheckBoxColumn colActivo;

	private CheckBox chkSoloActivos;

	private CheckBox chkStockBajo;

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
        btnEliminar = new Button();
        btnRefrescar = new Button();
        chkSoloActivos = new CheckBox();
        chkStockBajo = new CheckBox();
        dgvItems = new DataGridView();
        colNombre = new DataGridViewTextBoxColumn();
        colTipo = new DataGridViewTextBoxColumn();
        colCategoria = new DataGridViewTextBoxColumn();
        colPrecio = new DataGridViewTextBoxColumn();
        colCosto = new DataGridViewTextBoxColumn();
        colStock = new DataGridViewTextBoxColumn();
        colStockMinimo = new DataGridViewTextBoxColumn();
        colDuracion = new DataGridViewTextBoxColumn();
        colActivo = new DataGridViewCheckBoxColumn();
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
        // btnEliminar
        // 
        btnEliminar.ForeColor = Color.Firebrick;
        btnEliminar.Location = new Point(320, 82);
        btnEliminar.Name = "btnEliminar";
        btnEliminar.Size = new Size(90, 28);
        btnEliminar.TabIndex = 10;
        btnEliminar.Text = "Eliminar";
        btnEliminar.UseVisualStyleBackColor = true;
        btnEliminar.Click += btnEliminar_Click;
        // 
        // btnRefrescar
        // 
        btnRefrescar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefrescar.Location = new Point(748, 82);
        btnRefrescar.Name = "btnRefrescar";
        btnRefrescar.Size = new Size(100, 28);
        btnRefrescar.TabIndex = 11;
        btnRefrescar.Text = "Refrescar";
        btnRefrescar.UseVisualStyleBackColor = true;
        btnRefrescar.Click += btnRefrescar_Click;
        // 
        // chkSoloActivos
        // 
        chkSoloActivos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        chkSoloActivos.AutoSize = true;
        chkSoloActivos.Checked = true;
        chkSoloActivos.CheckState = CheckState.Checked;
        chkSoloActivos.Location = new Point(757, 16);
        chkSoloActivos.Name = "chkSoloActivos";
        chkSoloActivos.Size = new Size(91, 19);
        chkSoloActivos.TabIndex = 8;
        chkSoloActivos.Text = "Solo Activos";
        chkSoloActivos.UseVisualStyleBackColor = true;
        chkSoloActivos.CheckedChanged += chkSoloActivos_CheckedChanged;
        // 
        // chkStockBajo
        //
        chkStockBajo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        chkStockBajo.AutoSize = true;
        chkStockBajo.Location = new Point(540, 16);
        chkStockBajo.Name = "chkStockBajo";
        chkStockBajo.Size = new Size(105, 19);
        chkStockBajo.TabIndex = 12;
        chkStockBajo.Text = "Solo stock bajo";
        chkStockBajo.UseVisualStyleBackColor = true;
        chkStockBajo.CheckedChanged += Filtro_Changed;
        //
        // dgvItems
        // 
        dgvItems.AllowUserToAddRows = false;
        dgvItems.AllowUserToDeleteRows = false;
        dgvItems.AllowUserToResizeRows = false;
        dgvItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvItems.AutoGenerateColumns = false;
        dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvItems.BackgroundColor = SystemColors.ButtonHighlight;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = SystemColors.Control;
        dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = SystemColors.Window;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        dgvItems.DefaultCellStyle = dataGridViewCellStyle2;
        dgvItems.Location = new Point(12, 120);
        dgvItems.MultiSelect = false;
        dgvItems.Name = "dgvItems";
        dgvItems.ReadOnly = true;
        dgvItems.RowHeadersVisible = false;
        dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvItems.Size = new Size(836, 368);
        dgvItems.TabIndex = 12;
        dgvItems.Columns.AddRange(new DataGridViewColumn[] { colNombre, colTipo, colCategoria, colPrecio, colCosto, colStock, colStockMinimo, colDuracion, colActivo });
        dgvItems.SelectionChanged += dgvItems_SelectionChanged;
        // 
        // colNombre
        // 
        colNombre.DataPropertyName = "Nombre";
        colNombre.FillWeight = 22F;
        colNombre.HeaderText = "Nombre";
        colNombre.Name = "colNombre";
        colNombre.ReadOnly = true;
        // 
        // colTipo
        // 
        colTipo.DataPropertyName = "Tipo";
        colTipo.FillWeight = 9F;
        colTipo.HeaderText = "Tipo";
        colTipo.Name = "colTipo";
        colTipo.ReadOnly = true;
        // 
        // colCategoria
        // 
        colCategoria.DataPropertyName = "CategoriaNombre";
        colCategoria.FillWeight = 18F;
        colCategoria.HeaderText = "Categoría";
        colCategoria.Name = "colCategoria";
        colCategoria.ReadOnly = true;
        // 
        // colPrecio
        // 
        colPrecio.DataPropertyName = "Precio";
        colPrecio.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        colPrecio.DefaultCellStyle.Format = "C2";
        colPrecio.FillWeight = 11F;
        colPrecio.HeaderText = "Precio";
        colPrecio.Name = "colPrecio";
        colPrecio.ReadOnly = true;
        // 
        // colCosto
        // 
        colCosto.DataPropertyName = "Costo";
        colCosto.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        colCosto.DefaultCellStyle.Format = "C2";
        colCosto.DefaultCellStyle.NullValue = "-";
        colCosto.FillWeight = 11F;
        colCosto.HeaderText = "Costo";
        colCosto.Name = "colCosto";
        colCosto.ReadOnly = true;
        // 
        // colStock
        // 
        colStock.DataPropertyName = "Stock";
        colStock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        colStock.DefaultCellStyle.NullValue = "-";
        colStock.FillWeight = 8F;
        colStock.HeaderText = "Stock";
        colStock.Name = "colStock";
        colStock.ReadOnly = true;
        // 
        // colStockMinimo
        // 
        colStockMinimo.DataPropertyName = "StockMinimo";
        colStockMinimo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        colStockMinimo.DefaultCellStyle.NullValue = "-";
        colStockMinimo.FillWeight = 10F;
        colStockMinimo.HeaderText = "Stock mínimo";
        colStockMinimo.Name = "colStockMinimo";
        colStockMinimo.ReadOnly = true;
        // 
        // colDuracion
        // 
        colDuracion.DataPropertyName = "DuracionMin";
        colDuracion.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        colDuracion.DefaultCellStyle.NullValue = "-";
        colDuracion.FillWeight = 11F;
        colDuracion.HeaderText = "Duración (min)";
        colDuracion.Name = "colDuracion";
        colDuracion.ReadOnly = true;
        // 
        // colActivo
        // 
        colActivo.DataPropertyName = "Activo";
        colActivo.FillWeight = 8F;
        colActivo.HeaderText = "Activo";
        colActivo.Name = "colActivo";
        colActivo.ReadOnly = true;
        // 
        // FormItems
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        ClientSize = new Size(860, 500);
        Controls.Add(dgvItems);
        Controls.Add(chkStockBajo);
        Controls.Add(chkSoloActivos);
        Controls.Add(btnRefrescar);
        Controls.Add(btnEliminar);
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
