using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;
partial class FormAtencionEditar : Form
{
	private IContainer components = null;

	private Label lblCliente;

	private ComboBox cboCliente;

	private Label lblEmpleado;

	private ComboBox cboEmpleado;

	private Label lblServicio;

	private ComboBox cboServicio;

	private Label lblTipo;

	private Label lblFechaHora;

	private DateTimePicker dtpFechaHora;

	private Label lblEstado;

	private ComboBox cboEstado;

	private Button btnGuardar;

	private Button btnCancelar;

	private ErrorProvider errProvider;

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		if (disposing)
		{
			errProvider.Dispose();
		}
		base.Dispose(disposing);
	}

    private void InitializeComponent()
    {
        components = new Container();
        lblCliente = new Label();
        cboCliente = new ComboBox();
        lblEmpleado = new Label();
        cboEmpleado = new ComboBox();
        lblServicio = new Label();
        cboServicio = new ComboBox();
        lblTipo = new Label();
        lblFechaHora = new Label();
        dtpFechaHora = new DateTimePicker();
        lblEstado = new Label();
        cboEstado = new ComboBox();
        btnGuardar = new Button();
        btnCancelar = new Button();
        errProvider = new ErrorProvider(components);
        ((ISupportInitialize)errProvider).BeginInit();
        SuspendLayout();
        // 
        // lblCliente
        // 
        lblCliente.AutoSize = true;
        lblCliente.Location = new Point(20, 18);
        lblCliente.Name = "lblCliente";
        lblCliente.Size = new Size(47, 15);
        lblCliente.TabIndex = 0;
        lblCliente.Text = "Cliente:";
        // 
        // cboCliente
        // 
        cboCliente.BackColor = SystemColors.GradientActiveCaption;
        cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCliente.FormattingEnabled = true;
        cboCliente.Location = new Point(20, 36);
        cboCliente.Name = "cboCliente";
        cboCliente.Size = new Size(360, 23);
        cboCliente.TabIndex = 1;
        // 
        // lblEmpleado
        // 
        lblEmpleado.AutoSize = true;
        lblEmpleado.Location = new Point(20, 72);
        lblEmpleado.Name = "lblEmpleado";
        lblEmpleado.Size = new Size(64, 15);
        lblEmpleado.TabIndex = 2;
        lblEmpleado.Text = "Peluquero:";
        // 
        // cboEmpleado
        // 
        cboEmpleado.DropDownStyle = ComboBoxStyle.DropDownList;
        cboEmpleado.FormattingEnabled = true;
        cboEmpleado.Location = new Point(20, 90);
        cboEmpleado.Name = "cboEmpleado";
        cboEmpleado.Size = new Size(360, 23);
        cboEmpleado.TabIndex = 3;
        cboEmpleado.SelectedIndexChanged += cboEmpleado_SelectedIndexChanged;
        // 
        // lblServicio
        // 
        lblServicio.AutoSize = true;
        lblServicio.Location = new Point(20, 126);
        lblServicio.Name = "lblServicio";
        lblServicio.Size = new Size(51, 15);
        lblServicio.TabIndex = 4;
        lblServicio.Text = "Servicio:";
        // 
        // cboServicio
        // 
        cboServicio.DropDownStyle = ComboBoxStyle.DropDownList;
        cboServicio.FormattingEnabled = true;
        cboServicio.Location = new Point(20, 144);
        cboServicio.Name = "cboServicio";
        cboServicio.Size = new Size(360, 23);
        cboServicio.TabIndex = 5;
        // 
        // lblTipo
        // 
        lblTipo.AutoSize = true;
        lblTipo.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
        lblTipo.ForeColor = Color.DimGray;
        lblTipo.Location = new Point(20, 180);
        lblTipo.Name = "lblTipo";
        lblTipo.Size = new Size(0, 15);
        lblTipo.TabIndex = 6;
        // 
        // lblFechaHora
        // 
        lblFechaHora.AutoSize = true;
        lblFechaHora.Location = new Point(20, 210);
        lblFechaHora.Name = "lblFechaHora";
        lblFechaHora.Size = new Size(77, 15);
        lblFechaHora.TabIndex = 7;
        lblFechaHora.Text = "Fecha y hora:";
        // 
        // dtpFechaHora
        // 
        dtpFechaHora.CustomFormat = "dd/MM/yyyy HH:mm";
        dtpFechaHora.Format = DateTimePickerFormat.Custom;
        dtpFechaHora.Location = new Point(20, 228);
        dtpFechaHora.Name = "dtpFechaHora";
        dtpFechaHora.ShowUpDown = true;
        dtpFechaHora.Size = new Size(200, 23);
        dtpFechaHora.TabIndex = 8;
        // 
        // lblEstado
        // 
        lblEstado.AutoSize = true;
        lblEstado.Location = new Point(240, 210);
        lblEstado.Name = "lblEstado";
        lblEstado.Size = new Size(45, 15);
        lblEstado.TabIndex = 9;
        lblEstado.Text = "Estado:";
        // 
        // cboEstado
        // 
        cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        cboEstado.FormattingEnabled = true;
        cboEstado.Location = new Point(240, 228);
        cboEstado.Name = "cboEstado";
        cboEstado.Size = new Size(140, 23);
        cboEstado.TabIndex = 10;
        // 
        // btnGuardar
        // 
        btnGuardar.Location = new Point(180, 280);
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Size = new Size(100, 34);
        btnGuardar.TabIndex = 11;
        btnGuardar.Text = "Guardar";
        btnGuardar.UseVisualStyleBackColor = true;
        btnGuardar.Click += btnGuardar_Click;
        // 
        // btnCancelar
        // 
        btnCancelar.DialogResult = DialogResult.Cancel;
        btnCancelar.Location = new Point(290, 280);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(90, 34);
        btnCancelar.TabIndex = 12;
        btnCancelar.Text = "Cancelar";
        btnCancelar.UseVisualStyleBackColor = true;
        btnCancelar.Click += btnCancelar_Click;
        // 
        // errProvider
        // 
        errProvider.ContainerControl = this;
        // 
        // FormAtencionEditar
        // 
        AcceptButton = btnGuardar;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        CancelButton = btnCancelar;
        ClientSize = new Size(400, 330);
        Controls.Add(btnCancelar);
        Controls.Add(btnGuardar);
        Controls.Add(cboEstado);
        Controls.Add(lblEstado);
        Controls.Add(dtpFechaHora);
        Controls.Add(lblFechaHora);
        Controls.Add(lblTipo);
        Controls.Add(cboServicio);
        Controls.Add(lblServicio);
        Controls.Add(cboEmpleado);
        Controls.Add(lblEmpleado);
        Controls.Add(cboCliente);
        Controls.Add(lblCliente);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormAtencionEditar";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Atención";
        Load += FormAtencionEditar_Load;
        ((ISupportInitialize)errProvider).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
