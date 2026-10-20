using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;
partial class FormAtenciones : Form
{
	private IContainer components = null;

	private Label lblTitulo;

	private Label lblEstado;

	private ComboBox cboEstado;

	private TextBox txtBuscar;

	private Button btnBuscar;

	private Button btnRefrescar;

	private DataGridView dgvAtenciones;

	private DataGridViewTextBoxColumn colId;

	private DataGridViewTextBoxColumn colCliente;

	private DataGridViewTextBoxColumn colEmpleado;

	private DataGridViewTextBoxColumn colServicio;

	private DataGridViewTextBoxColumn colTipo;

	private DataGridViewTextBoxColumn colFecha;

	private DataGridViewTextBoxColumn colHora;

	private DataGridViewTextBoxColumn colEstado;

	private Button btnNuevo;

	private Button btnEditar;

	private Button btnCancelarAtencion;

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
        lblEstado = new Label();
        cboEstado = new ComboBox();
        txtBuscar = new TextBox();
        btnBuscar = new Button();
        btnRefrescar = new Button();
        dgvAtenciones = new DataGridView();
        btnNuevo = new Button();
        btnEditar = new Button();
        btnCancelarAtencion = new Button();
        ((ISupportInitialize)dgvAtenciones).BeginInit();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.Location = new Point(12, 10);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(189, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Turnos y atenciones";
        // 
        // lblEstado
        // 
        lblEstado.AutoSize = true;
        lblEstado.Location = new Point(12, 50);
        lblEstado.Name = "lblEstado";
        lblEstado.Size = new Size(45, 15);
        lblEstado.TabIndex = 1;
        lblEstado.Text = "Estado:";
        // 
        // cboEstado
        // 
        cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        cboEstado.FormattingEnabled = true;
        cboEstado.Location = new Point(12, 68);
        cboEstado.Name = "cboEstado";
        cboEstado.Size = new Size(150, 23);
        cboEstado.TabIndex = 2;
        cboEstado.SelectedIndexChanged += cboEstado_SelectedIndexChanged;
        // 
        // txtBuscar
        // 
        txtBuscar.Location = new Point(175, 68);
        txtBuscar.Name = "txtBuscar";
        txtBuscar.PlaceholderText = "Buscar cliente o servicio...";
        txtBuscar.Size = new Size(240, 23);
        txtBuscar.TabIndex = 3;
        // 
        // btnBuscar
        // 
        btnBuscar.Location = new Point(425, 66);
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Size = new Size(90, 26);
        btnBuscar.TabIndex = 4;
        btnBuscar.Text = "Buscar";
        btnBuscar.UseVisualStyleBackColor = true;
        btnBuscar.Click += btnBuscar_Click;
        // 
        // btnRefrescar
        // 
        btnRefrescar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefrescar.Location = new Point(758, 66);
        btnRefrescar.Name = "btnRefrescar";
        btnRefrescar.Size = new Size(130, 26);
        btnRefrescar.TabIndex = 5;
        btnRefrescar.Text = "Refrescar";
        btnRefrescar.UseVisualStyleBackColor = true;
        btnRefrescar.Click += btnRefrescar_Click;
        // 
        // colId
        // 
        colId = new DataGridViewTextBoxColumn();
        colId.DataPropertyName = "Id";
        colId.HeaderText = "Id";
        colId.Name = "colId";
        colId.ReadOnly = true;
        colId.Visible = false;
        // 
        // colCliente
        // 
        colCliente = new DataGridViewTextBoxColumn();
        colCliente.DataPropertyName = "ClienteNombre";
        colCliente.HeaderText = "Cliente";
        colCliente.Name = "colCliente";
        colCliente.ReadOnly = true;
        // 
        // colEmpleado
        // 
        colEmpleado = new DataGridViewTextBoxColumn();
        colEmpleado.DataPropertyName = "EmpleadoNombre";
        colEmpleado.HeaderText = "Empleado";
        colEmpleado.Name = "colEmpleado";
        colEmpleado.ReadOnly = true;
        // 
        // colServicio
        // 
        colServicio = new DataGridViewTextBoxColumn();
        colServicio.DataPropertyName = "ServicioNombre";
        colServicio.HeaderText = "Servicio";
        colServicio.Name = "colServicio";
        colServicio.ReadOnly = true;
        // 
        // colTipo
        // 
        colTipo = new DataGridViewTextBoxColumn();
        colTipo.DataPropertyName = "TipoNombre";
        colTipo.HeaderText = "Tipo";
        colTipo.Name = "colTipo";
        colTipo.ReadOnly = true;
        // 
        // colFecha
        // 
        colFecha = new DataGridViewTextBoxColumn();
        colFecha.DataPropertyName = "Fecha";
        colFecha.HeaderText = "Fecha";
        colFecha.Name = "colFecha";
        colFecha.ReadOnly = true;
        // 
        // colHora
        // 
        colHora = new DataGridViewTextBoxColumn();
        colHora.DataPropertyName = "Hora";
        colHora.HeaderText = "Hora";
        colHora.Name = "colHora";
        colHora.ReadOnly = true;
        // 
        // colEstado
        // 
        colEstado = new DataGridViewTextBoxColumn();
        colEstado.DataPropertyName = "EstadoNombre";
        colEstado.HeaderText = "Estado";
        colEstado.Name = "colEstado";
        colEstado.ReadOnly = true;
        // 
        // dgvAtenciones
        // 
        dgvAtenciones.AllowUserToAddRows = false;
        dgvAtenciones.AllowUserToDeleteRows = false;
        dgvAtenciones.AllowUserToResizeRows = false;
        dgvAtenciones.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvAtenciones.AutoGenerateColumns = false;
        dgvAtenciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvAtenciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvAtenciones.Location = new Point(12, 108);
        dgvAtenciones.MultiSelect = false;
        dgvAtenciones.Name = "dgvAtenciones";
        dgvAtenciones.ReadOnly = true;
        dgvAtenciones.RowHeadersVisible = false;
        dgvAtenciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvAtenciones.Size = new Size(876, 392);
        dgvAtenciones.TabIndex = 6;
        dgvAtenciones.Columns.AddRange(new DataGridViewColumn[] {
        colId,
        colCliente,
        colEmpleado,
        colServicio,
        colTipo,
        colFecha,
        colHora,
        colEstado});
        // 
        // btnNuevo
        // 
        btnNuevo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnNuevo.Location = new Point(12, 512);
        btnNuevo.Name = "btnNuevo";
        btnNuevo.Size = new Size(90, 32);
        btnNuevo.TabIndex = 7;
        btnNuevo.Text = "Nuevo";
        btnNuevo.UseVisualStyleBackColor = true;
        btnNuevo.Click += btnNuevo_Click;
        // 
        // btnEditar
        // 
        btnEditar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnEditar.Location = new Point(108, 512);
        btnEditar.Name = "btnEditar";
        btnEditar.Size = new Size(90, 32);
        btnEditar.TabIndex = 8;
        btnEditar.Text = "Editar";
        btnEditar.UseVisualStyleBackColor = true;
        btnEditar.Click += btnEditar_Click;
        // 
        // btnCancelarAtencion
        // 
        btnCancelarAtencion.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnCancelarAtencion.Location = new Point(204, 512);
        btnCancelarAtencion.Name = "btnCancelarAtencion";
        btnCancelarAtencion.Size = new Size(130, 32);
        btnCancelarAtencion.TabIndex = 9;
        btnCancelarAtencion.Text = "Cancelar turno";
        btnCancelarAtencion.UseVisualStyleBackColor = true;
        btnCancelarAtencion.Click += btnCancelarAtencion_Click;
        // 
        // FormAtenciones
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        ClientSize = new Size(900, 560);
        Controls.Add(btnCancelarAtencion);
        Controls.Add(btnEditar);
        Controls.Add(btnNuevo);
        Controls.Add(dgvAtenciones);
        Controls.Add(btnRefrescar);
        Controls.Add(btnBuscar);
        Controls.Add(txtBuscar);
        Controls.Add(cboEstado);
        Controls.Add(lblEstado);
        Controls.Add(lblTitulo);
        Font = new Font("Segoe UI", 9F);
        Name = "FormAtenciones";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Turnos y Atenciones";
        Load += FormAtenciones_Load;
        ((ISupportInitialize)dgvAtenciones).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
