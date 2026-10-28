using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
partial class FormClienteEditar : Form
{
	private IContainer components = null;

	private Label lblNombre;

	private TextBox txtNombre;

	private Label lblApellido;

	private TextBox txtApellido;

	private Label lblDni;

	private TextBox txtDni;

	private Label lblTelefono;

	private TextBox txtTelefono;

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
        lblDni = new Label();
        txtDni = new TextBox();
        lblTelefono = new Label();
        txtTelefono = new TextBox();
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
        // lblDni
        // 
        lblDni.AutoSize = true;
        lblDni.Location = new Point(20, 122);
        lblDni.Name = "lblDni";
        lblDni.Size = new Size(30, 15);
        lblDni.TabIndex = 4;
        lblDni.Text = "DNI:";
        // 
        // txtDni
        // 
        txtDni.Location = new Point(20, 140);
        txtDni.MaxLength = 8;
        txtDni.Name = "txtDni";
        txtDni.Size = new Size(140, 23);
        txtDni.TabIndex = 5;
        // 
        // lblTelefono
        // 
        lblTelefono.AutoSize = true;
        lblTelefono.Location = new Point(180, 122);
        lblTelefono.Name = "lblTelefono";
        lblTelefono.Size = new Size(56, 15);
        lblTelefono.TabIndex = 6;
        lblTelefono.Text = "Teléfono:";
        // 
        // txtTelefono
        // 
        txtTelefono.Location = new Point(180, 140);
        txtTelefono.MaxLength = 20;
        txtTelefono.Name = "txtTelefono";
        txtTelefono.Size = new Size(140, 23);
        txtTelefono.TabIndex = 7;
        // 
        // chkActivo
        // 
        chkActivo.AutoSize = true;
        chkActivo.Location = new Point(20, 185);
        chkActivo.Name = "chkActivo";
        chkActivo.Size = new Size(60, 19);
        chkActivo.TabIndex = 8;
        chkActivo.Text = "Activo";
        chkActivo.UseVisualStyleBackColor = true;
        // 
        // btnGuardar
        // 
        btnGuardar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnGuardar.Location = new Point(180, 220);
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Size = new Size(100, 32);
        btnGuardar.TabIndex = 9;
        btnGuardar.Text = "Guardar";
        btnGuardar.UseVisualStyleBackColor = true;
        btnGuardar.Click += btnGuardar_Click;
        // 
        // btnCancelar
        // 
        btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancelar.DialogResult = DialogResult.Cancel;
        btnCancelar.Location = new Point(290, 220);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(110, 32);
        btnCancelar.TabIndex = 10;
        btnCancelar.Text = "Cancelar";
        btnCancelar.UseVisualStyleBackColor = true;
        btnCancelar.Click += btnCancelar_Click;
        // 
        // errProvider
        // 
        errProvider.ContainerControl = this;
        // 
        // FormClienteEditar
        // 
        AcceptButton = btnGuardar;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        CancelButton = btnCancelar;
        ClientSize = new Size(420, 272);
        Controls.Add(btnCancelar);
        Controls.Add(btnGuardar);
        Controls.Add(chkActivo);
        Controls.Add(txtTelefono);
        Controls.Add(lblTelefono);
        Controls.Add(txtDni);
        Controls.Add(lblDni);
        Controls.Add(txtApellido);
        Controls.Add(lblApellido);
        Controls.Add(txtNombre);
        Controls.Add(lblNombre);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormClienteEditar";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Cliente";
        Load += FormClienteEditar_Load;
        ((ISupportInitialize)errProvider).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
