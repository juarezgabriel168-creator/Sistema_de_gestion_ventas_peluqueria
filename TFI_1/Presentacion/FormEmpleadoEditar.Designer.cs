using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
partial class FormEmpleadoEditar : Form
{
	private IContainer components = null;

	private Label lblNombre;

	private TextBox txtNombre;

	private Label lblApellido;

	private TextBox txtApellido;

	private Label lblModalidad;

	private ComboBox cboModalidad;

	private CheckBox chkActivo;

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
        lblNombre = new Label();
        txtNombre = new TextBox();
        lblApellido = new Label();
        txtApellido = new TextBox();
        lblModalidad = new Label();
        cboModalidad = new ComboBox();
        chkActivo = new CheckBox();
        btnGuardar = new Button();
        btnCancelar = new Button();
        errProvider = new ErrorProvider(components);
        ((ISupportInitialize)errProvider).BeginInit();
        SuspendLayout();
        // 
        // lblNombre
        // 
        lblNombre.AutoSize = true;
        lblNombre.Location = new Point(20, 18);
        lblNombre.Name = "lblNombre";
        lblNombre.Size = new Size(54, 15);
        lblNombre.TabIndex = 0;
        lblNombre.Text = "Nombre:";
        // 
        // txtNombre
        // 
        txtNombre.Location = new Point(20, 36);
        txtNombre.MaxLength = 50;
        txtNombre.Name = "txtNombre";
        txtNombre.Size = new Size(300, 23);
        txtNombre.TabIndex = 1;
        // 
        // lblApellido
        // 
        lblApellido.AutoSize = true;
        lblApellido.Location = new Point(20, 70);
        lblApellido.Name = "lblApellido";
        lblApellido.Size = new Size(54, 15);
        lblApellido.TabIndex = 2;
        lblApellido.Text = "Apellido:";
        // 
        // txtApellido
        // 
        txtApellido.Location = new Point(20, 88);
        txtApellido.MaxLength = 50;
        txtApellido.Name = "txtApellido";
        txtApellido.Size = new Size(300, 23);
        txtApellido.TabIndex = 3;
        // 
        // lblModalidad
        // 
        lblModalidad.AutoSize = true;
        lblModalidad.Location = new Point(20, 122);
        lblModalidad.Name = "lblModalidad";
        lblModalidad.Size = new Size(67, 15);
        lblModalidad.TabIndex = 4;
        lblModalidad.Text = "Modalidad:";
        // 
        // cboModalidad
        // 
        cboModalidad.DropDownStyle = ComboBoxStyle.DropDownList;
        cboModalidad.FormattingEnabled = true;
        cboModalidad.Location = new Point(20, 140);
        cboModalidad.Name = "cboModalidad";
        cboModalidad.Size = new Size(300, 23);
        cboModalidad.TabIndex = 5;
        // 
        // chkActivo
        // 
        chkActivo.AutoSize = true;
        chkActivo.Location = new Point(20, 178);
        chkActivo.Name = "chkActivo";
        chkActivo.Size = new Size(60, 19);
        chkActivo.TabIndex = 6;
        chkActivo.Text = "Activo";
        chkActivo.UseVisualStyleBackColor = true;
        // 
        // btnGuardar
        // 
        btnGuardar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnGuardar.Location = new Point(180, 212);
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Size = new Size(100, 32);
        btnGuardar.TabIndex = 7;
        btnGuardar.Text = "Guardar";
        btnGuardar.UseVisualStyleBackColor = true;
        btnGuardar.Click += btnGuardar_Click;
        // 
        // btnCancelar
        // 
        btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancelar.DialogResult = DialogResult.Cancel;
        btnCancelar.Location = new Point(290, 212);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(110, 32);
        btnCancelar.TabIndex = 8;
        btnCancelar.Text = "Cancelar";
        btnCancelar.UseVisualStyleBackColor = true;
        btnCancelar.Click += btnCancelar_Click;
        // 
        // errProvider
        // 
        errProvider.ContainerControl = this;
        // 
        // FormEmpleadoEditar
        // 
        AcceptButton = btnGuardar;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancelar;
        ClientSize = new Size(420, 264);
        Controls.Add(btnCancelar);
        Controls.Add(btnGuardar);
        Controls.Add(chkActivo);
        Controls.Add(cboModalidad);
        Controls.Add(lblModalidad);
        Controls.Add(txtApellido);
        Controls.Add(lblApellido);
        Controls.Add(txtNombre);
        Controls.Add(lblNombre);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormEmpleadoEditar";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Empleado";
        Load += FormEmpleadoEditar_Load;
        ((ISupportInitialize)errProvider).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
