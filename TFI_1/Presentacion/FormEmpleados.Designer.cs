using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
partial class FormEmpleados : Form
{
	private IContainer components = null;

	private Label lblTitulo;

	private TextBox txtBuscar;

	private Button btnBuscar;

	private Button btnNuevo;

	private Button btnEditar;

	private Button btnDesactivar;

	private Button btnRefrescar;

	private DataGridView dgvEmpleados;

	private CheckBox chkSoloActivos;

	private DataGridViewTextBoxColumn colApellido;

	private DataGridViewTextBoxColumn colNombre;

	private DataGridViewTextBoxColumn colModalidad;

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
        txtBuscar = new TextBox();
        btnBuscar = new Button();
        btnNuevo = new Button();
        btnEditar = new Button();
        btnDesactivar = new Button();
        btnRefrescar = new Button();
        chkSoloActivos = new CheckBox();
        dgvEmpleados = new DataGridView();
        ((ISupportInitialize)dgvEmpleados).BeginInit();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.Location = new Point(12, 10);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(108, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Empleados";
        // 
        // txtBuscar
        // 
        txtBuscar.Location = new Point(12, 45);
        txtBuscar.Name = "txtBuscar";
        txtBuscar.PlaceholderText = "Buscar por nombre o apellido...";
        txtBuscar.Size = new Size(260, 23);
        txtBuscar.TabIndex = 1;
        // 
        // btnBuscar
        // 
        btnBuscar.Location = new Point(280, 44);
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Size = new Size(80, 25);
        btnBuscar.TabIndex = 2;
        btnBuscar.Text = "Buscar";
        btnBuscar.UseVisualStyleBackColor = true;
        btnBuscar.Click += btnBuscar_Click;
        // 
        // btnNuevo
        // 
        btnNuevo.Location = new Point(370, 44);
        btnNuevo.Name = "btnNuevo";
        btnNuevo.Size = new Size(90, 25);
        btnNuevo.TabIndex = 3;
        btnNuevo.Text = "Nuevo";
        btnNuevo.UseVisualStyleBackColor = true;
        btnNuevo.Click += btnNuevo_Click;
        // 
        // btnEditar
        // 
        btnEditar.Location = new Point(466, 44);
        btnEditar.Name = "btnEditar";
        btnEditar.Size = new Size(90, 25);
        btnEditar.TabIndex = 4;
        btnEditar.Text = "Editar";
        btnEditar.UseVisualStyleBackColor = true;
        btnEditar.Click += btnEditar_Click;
        // 
        // btnDesactivar
        // 
        btnDesactivar.Location = new Point(562, 44);
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
        btnRefrescar.Location = new Point(690, 44);
        btnRefrescar.Name = "btnRefrescar";
        btnRefrescar.Size = new Size(100, 25);
        btnRefrescar.TabIndex = 6;
        btnRefrescar.Text = "Refrescar";
        btnRefrescar.UseVisualStyleBackColor = true;
        btnRefrescar.Click += btnRefrescar_Click;
        // 
        // colApellido
        // 
        colApellido = new DataGridViewTextBoxColumn();
        colApellido.DataPropertyName = "Apellido";
        colApellido.HeaderText = "Apellido";
        colApellido.Name = "colApellido";
        colApellido.ReadOnly = true;
        // 
        // colNombre
        // 
        colNombre = new DataGridViewTextBoxColumn();
        colNombre.DataPropertyName = "Nombre";
        colNombre.HeaderText = "Nombre";
        colNombre.Name = "colNombre";
        colNombre.ReadOnly = true;
        // 
        // colModalidad
        // 
        colModalidad = new DataGridViewTextBoxColumn();
        colModalidad.DataPropertyName = "ModalidadTexto";
        colModalidad.HeaderText = "Modalidad";
        colModalidad.Name = "colModalidad";
        colModalidad.ReadOnly = true;
        // 
        // colActivo
        // 
        colActivo = new DataGridViewCheckBoxColumn();
        colActivo.DataPropertyName = "Activo";
        colActivo.HeaderText = "Activo";
        colActivo.Name = "colActivo";
        colActivo.ReadOnly = true;
        // 
        // dgvEmpleados
        // 
        dgvEmpleados.AllowUserToAddRows = false;
        dgvEmpleados.AllowUserToDeleteRows = false;
        dgvEmpleados.AllowUserToResizeRows = false;
        dgvEmpleados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvEmpleados.AutoGenerateColumns = false;
        dgvEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = SystemColors.Control;
        dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        dgvEmpleados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        dgvEmpleados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = SystemColors.Window;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        dgvEmpleados.DefaultCellStyle = dataGridViewCellStyle2;
        dgvEmpleados.Location = new Point(12, 90);
        dgvEmpleados.MultiSelect = false;
        dgvEmpleados.Name = "dgvEmpleados";
        dgvEmpleados.SelectionChanged += dgvEmpleados_SelectionChanged;
        dgvEmpleados.ReadOnly = true;
        dgvEmpleados.RowHeadersVisible = false;
        dgvEmpleados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvEmpleados.Size = new Size(778, 361);
        dgvEmpleados.TabIndex = 7;
        dgvEmpleados.Columns.AddRange(new DataGridViewColumn[] {
        colApellido,
        colNombre,
        colModalidad,
        colActivo});
        // 
        // chkSoloActivos
        // 
        chkSoloActivos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        chkSoloActivos.AutoSize = true;
        chkSoloActivos.Checked = true;
        chkSoloActivos.CheckState = CheckState.Checked;
        chkSoloActivos.Location = new Point(690, 16);
        chkSoloActivos.Name = "chkSoloActivos";
        chkSoloActivos.Size = new Size(100, 19);
        chkSoloActivos.TabIndex = 8;
        chkSoloActivos.Text = "Solo Activos";
        chkSoloActivos.UseVisualStyleBackColor = true;
        chkSoloActivos.CheckedChanged += chkSoloActivos_CheckedChanged;
        // 
        // FormEmpleados
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        ClientSize = new Size(802, 463);
        Controls.Add(dgvEmpleados);
        Controls.Add(chkSoloActivos);
        Controls.Add(btnRefrescar);
        Controls.Add(btnDesactivar);
        Controls.Add(btnEditar);
        Controls.Add(btnNuevo);
        Controls.Add(btnBuscar);
        Controls.Add(txtBuscar);
        Controls.Add(lblTitulo);
        Font = new Font("Segoe UI", 9F);
        Name = "FormEmpleados";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Empleados";
        Load += FormEmpleados_Load;
        ((ISupportInitialize)dgvEmpleados).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
