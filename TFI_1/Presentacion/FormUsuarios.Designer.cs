using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
partial class FormUsuarios : Form
{
	private IContainer components = null;

	private Label lblTitulo;

	private TextBox txtBuscar;

	private Button btnBuscar;

	private Button btnNuevo;

	private Button btnEditar;

	private Button btnDesactivar;

	private Button btnEliminar;

	private Button btnRefrescar;

	private DataGridView dgvUsuarios;

	private DataGridViewTextBoxColumn colId;

	private DataGridViewTextBoxColumn colNombreUsuario;

	private DataGridViewTextBoxColumn colRol;

	private DataGridViewCheckBoxColumn colActivo;

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
        btnEliminar = new Button();
        btnRefrescar = new Button();
        chkSoloActivos = new CheckBox();
        dgvUsuarios = new DataGridView();
        colId = new DataGridViewTextBoxColumn();
        colNombreUsuario = new DataGridViewTextBoxColumn();
        colRol = new DataGridViewTextBoxColumn();
        colActivo = new DataGridViewCheckBoxColumn();
        ((ISupportInitialize)dgvUsuarios).BeginInit();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.Location = new Point(12, 10);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(89, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Usuarios";
        // 
        // txtBuscar
        // 
        txtBuscar.Location = new Point(12, 45);
        txtBuscar.Name = "txtBuscar";
        txtBuscar.PlaceholderText = "Buscar por nombre de usuario...";
        txtBuscar.Size = new Size(190, 23);
        txtBuscar.TabIndex = 1;
        // 
        // btnBuscar
        // 
        btnBuscar.Location = new Point(208, 44);
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Size = new Size(80, 25);
        btnBuscar.TabIndex = 2;
        btnBuscar.Text = "Buscar";
        btnBuscar.UseVisualStyleBackColor = true;
        btnBuscar.Click += btnBuscar_Click;
        // 
        // btnNuevo
        // 
        btnNuevo.Location = new Point(294, 44);
        btnNuevo.Name = "btnNuevo";
        btnNuevo.Size = new Size(80, 25);
        btnNuevo.TabIndex = 3;
        btnNuevo.Text = "Nuevo";
        btnNuevo.UseVisualStyleBackColor = true;
        btnNuevo.Click += btnNuevo_Click;
        // 
        // btnEditar
        // 
        btnEditar.Location = new Point(380, 44);
        btnEditar.Name = "btnEditar";
        btnEditar.Size = new Size(80, 25);
        btnEditar.TabIndex = 4;
        btnEditar.Text = "Editar";
        btnEditar.UseVisualStyleBackColor = true;
        btnEditar.Click += btnEditar_Click;
        // 
        // btnDesactivar
        // 
        btnDesactivar.Location = new Point(466, 44);
        btnDesactivar.Name = "btnDesactivar";
        btnDesactivar.Size = new Size(100, 25);
        btnDesactivar.TabIndex = 5;
        btnDesactivar.Text = "Desactivar";
        btnDesactivar.UseVisualStyleBackColor = true;
        btnDesactivar.Click += btnDesactivar_Click;
        // 
        // btnEliminar
        // 
        btnEliminar.ForeColor = Color.Firebrick;
        btnEliminar.Location = new Point(572, 44);
        btnEliminar.Name = "btnEliminar";
        btnEliminar.Size = new Size(80, 25);
        btnEliminar.TabIndex = 6;
        btnEliminar.Text = "Eliminar";
        btnEliminar.UseVisualStyleBackColor = true;
        btnEliminar.Click += btnEliminar_Click;
        // 
        // btnRefrescar
        // 
        btnRefrescar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefrescar.Location = new Point(658, 44);
        btnRefrescar.Name = "btnRefrescar";
        btnRefrescar.Size = new Size(90, 25);
        btnRefrescar.TabIndex = 7;
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
        chkSoloActivos.TabIndex = 9;
        chkSoloActivos.Text = "Solo Activos";
        chkSoloActivos.UseVisualStyleBackColor = true;
        chkSoloActivos.CheckedChanged += chkSoloActivos_CheckedChanged;
        // 
        // dgvUsuarios
        // 
        dgvUsuarios.AllowUserToAddRows = false;
        dgvUsuarios.AllowUserToDeleteRows = false;
        dgvUsuarios.AllowUserToResizeRows = false;
        dgvUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvUsuarios.AutoGenerateColumns = false;
        dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvUsuarios.BackgroundColor = SystemColors.ButtonHighlight;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = SystemColors.Control;
        dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        dgvUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = SystemColors.Window;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        dgvUsuarios.DefaultCellStyle = dataGridViewCellStyle2;
        dgvUsuarios.Location = new Point(12, 90);
        dgvUsuarios.MultiSelect = false;
        dgvUsuarios.Name = "dgvUsuarios";
        dgvUsuarios.ReadOnly = true;
        dgvUsuarios.RowHeadersVisible = false;
        dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvUsuarios.Size = new Size(736, 360);
        dgvUsuarios.TabIndex = 8;
        dgvUsuarios.Columns.AddRange(new DataGridViewColumn[] { colId, colNombreUsuario, colRol, colActivo });
        dgvUsuarios.SelectionChanged += dgvUsuarios_SelectionChanged;
        // 
        // colId
        // 
        colId.DataPropertyName = "Id";
        colId.FillWeight = 8F;
        colId.HeaderText = "ID";
        colId.Name = "colId";
        colId.ReadOnly = true;
        // 
        // colNombreUsuario
        // 
        colNombreUsuario.DataPropertyName = "NombreUsuario";
        colNombreUsuario.FillWeight = 32F;
        colNombreUsuario.HeaderText = "Usuario";
        colNombreUsuario.Name = "colNombreUsuario";
        colNombreUsuario.ReadOnly = true;
        // 
        // colRol
        // 
        colRol.DataPropertyName = "RolNombre";
        colRol.FillWeight = 25F;
        colRol.HeaderText = "Rol";
        colRol.Name = "colRol";
        colRol.ReadOnly = true;
        // 
        // colActivo
        // 
        colActivo.DataPropertyName = "Activo";
        colActivo.FillWeight = 10F;
        colActivo.HeaderText = "Activo";
        colActivo.Name = "colActivo";
        colActivo.ReadOnly = true;
        // 
        // FormUsuarios
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        ClientSize = new Size(760, 462);
        Controls.Add(dgvUsuarios);
        Controls.Add(chkSoloActivos);
        Controls.Add(btnRefrescar);
        Controls.Add(btnEliminar);
        Controls.Add(btnDesactivar);
        Controls.Add(btnEditar);
        Controls.Add(btnNuevo);
        Controls.Add(btnBuscar);
        Controls.Add(txtBuscar);
        Controls.Add(lblTitulo);
        Font = new Font("Segoe UI", 9F);
        Name = "FormUsuarios";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Usuarios";
        Load += FormUsuarios_Load;
        ((ISupportInitialize)dgvUsuarios).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
