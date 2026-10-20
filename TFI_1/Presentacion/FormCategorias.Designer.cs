using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
partial class FormCategorias : Form
{
	private IContainer components = null;

	private Label lblTitulo;

	private TextBox txtBuscar;

	private Button btnBuscar;

	private Button btnNuevo;

	private Button btnEditar;

	private Button btnDesactivar;

	private Button btnRefrescar;

	private DataGridView dgvCategorias;

	private CheckBox chkSoloActivos;

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
        txtBuscar = new TextBox();
        btnBuscar = new Button();
        btnNuevo = new Button();
        btnEditar = new Button();
        btnDesactivar = new Button();
        btnRefrescar = new Button();
        chkSoloActivos = new CheckBox();
        dgvCategorias = new DataGridView();
        ((ISupportInitialize)dgvCategorias).BeginInit();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.Location = new Point(12, 10);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(106, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Categorías";
        // 
        // txtBuscar
        // 
        txtBuscar.Location = new Point(12, 45);
        txtBuscar.Name = "txtBuscar";
        txtBuscar.PlaceholderText = "Buscar por nombre...";
        txtBuscar.Size = new Size(220, 23);
        txtBuscar.TabIndex = 1;
        // 
        // btnBuscar
        // 
        btnBuscar.Location = new Point(240, 44);
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Size = new Size(80, 25);
        btnBuscar.TabIndex = 2;
        btnBuscar.Text = "Buscar";
        btnBuscar.UseVisualStyleBackColor = true;
        btnBuscar.Click += btnBuscar_Click;
        // 
        // btnNuevo
        // 
        btnNuevo.Location = new Point(340, 44);
        btnNuevo.Name = "btnNuevo";
        btnNuevo.Size = new Size(90, 25);
        btnNuevo.TabIndex = 3;
        btnNuevo.Text = "Nuevo";
        btnNuevo.UseVisualStyleBackColor = true;
        btnNuevo.Click += btnNuevo_Click;
        // 
        // btnEditar
        // 
        btnEditar.Location = new Point(436, 44);
        btnEditar.Name = "btnEditar";
        btnEditar.Size = new Size(90, 25);
        btnEditar.TabIndex = 4;
        btnEditar.Text = "Editar";
        btnEditar.UseVisualStyleBackColor = true;
        btnEditar.Click += btnEditar_Click;
        // 
        // btnDesactivar
        // 
        btnDesactivar.Location = new Point(532, 44);
        btnDesactivar.Name = "btnDesactivar";
        btnDesactivar.Size = new Size(110, 25);
        btnDesactivar.TabIndex = 5;
        btnDesactivar.Text = "Desactivar";
        btnDesactivar.UseVisualStyleBackColor = true;
        btnDesactivar.Click += btnDesactivar_Click;
        // 
        // btnRefrescar
        // 
        btnRefrescar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefrescar.Location = new Point(648, 44);
        btnRefrescar.Name = "btnRefrescar";
        btnRefrescar.Size = new Size(100, 25);
        btnRefrescar.TabIndex = 6;
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
        chkSoloActivos.Location = new Point(657, 16);
        chkSoloActivos.Name = "chkSoloActivos";
        chkSoloActivos.Size = new Size(91, 19);
        chkSoloActivos.TabIndex = 8;
        chkSoloActivos.Text = "Solo Activos";
        chkSoloActivos.UseVisualStyleBackColor = true;
        chkSoloActivos.CheckedChanged += chkSoloActivos_CheckedChanged;
        // 
        // dgvCategorias
        // 
        dgvCategorias.AllowUserToAddRows = false;
        dgvCategorias.AllowUserToDeleteRows = false;
        dgvCategorias.AllowUserToResizeRows = false;
        dgvCategorias.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvCategorias.BackgroundColor = SystemColors.ButtonHighlight;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = SystemColors.Control;
        dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        dgvCategorias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        dgvCategorias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = SystemColors.Window;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        dgvCategorias.DefaultCellStyle = dataGridViewCellStyle2;
        dgvCategorias.Location = new Point(12, 90);
        dgvCategorias.MultiSelect = false;
        dgvCategorias.Name = "dgvCategorias";
        dgvCategorias.ReadOnly = true;
        dgvCategorias.RowHeadersVisible = false;
        dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCategorias.Size = new Size(736, 360);
        dgvCategorias.TabIndex = 7;
        dgvCategorias.SelectionChanged += dgvCategorias_SelectionChanged;
        // 
        // FormCategorias
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        ClientSize = new Size(760, 462);
        Controls.Add(dgvCategorias);
        Controls.Add(chkSoloActivos);
        Controls.Add(btnRefrescar);
        Controls.Add(btnDesactivar);
        Controls.Add(btnEditar);
        Controls.Add(btnNuevo);
        Controls.Add(btnBuscar);
        Controls.Add(txtBuscar);
        Controls.Add(lblTitulo);
        Font = new Font("Segoe UI", 9F);
        Name = "FormCategorias";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Categorías";
        Load += FormCategorias_Load;
        ((ISupportInitialize)dgvCategorias).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
