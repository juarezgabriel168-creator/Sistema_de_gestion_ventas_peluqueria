using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;

partial class FormProveedorEditar : Form
{
	private IContainer components = null;

	private Label lblNombre;
	private TextBox txtNombre;
	private Label lblCuit;
	private TextBox txtCuit;
	private Label lblTelefono;
	private TextBox txtTelefono;
	private Label lblEmail;
	private TextBox txtEmail;
	private Label lblDireccion;
	private TextBox txtDireccion;
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
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		components = new Container();
		lblNombre = new Label();
		txtNombre = new TextBox();
		lblCuit = new Label();
		txtCuit = new TextBox();
		lblTelefono = new Label();
		txtTelefono = new TextBox();
		lblEmail = new Label();
		txtEmail = new TextBox();
		lblDireccion = new Label();
		txtDireccion = new TextBox();
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
		txtNombre.Location = new Point(20, 38);
		txtNombre.Name = "txtNombre";
		txtNombre.Size = new Size(420, 23);
		txtNombre.TabIndex = 1;
		//
		// lblCuit
		//
		lblCuit.AutoSize = true;
		lblCuit.Location = new Point(20, 72);
		lblCuit.Name = "lblCuit";
		lblCuit.Size = new Size(35, 15);
		lblCuit.TabIndex = 2;
		lblCuit.Text = "CUIT:";
		//
		// txtCuit
		//
		txtCuit.Location = new Point(20, 92);
		txtCuit.Name = "txtCuit";
		txtCuit.Size = new Size(200, 23);
		txtCuit.TabIndex = 3;
		//
		// lblTelefono
		//
		lblTelefono.AutoSize = true;
		lblTelefono.Location = new Point(240, 72);
		lblTelefono.Name = "lblTelefono";
		lblTelefono.Size = new Size(56, 15);
		lblTelefono.TabIndex = 4;
		lblTelefono.Text = "Teléfono:";
		//
		// txtTelefono
		//
		txtTelefono.Location = new Point(240, 92);
		txtTelefono.Name = "txtTelefono";
		txtTelefono.Size = new Size(200, 23);
		txtTelefono.TabIndex = 5;
		//
		// lblEmail
		//
		lblEmail.AutoSize = true;
		lblEmail.Location = new Point(20, 126);
		lblEmail.Name = "lblEmail";
		lblEmail.Size = new Size(39, 15);
		lblEmail.TabIndex = 6;
		lblEmail.Text = "Email:";
		//
		// txtEmail
		//
		txtEmail.Location = new Point(20, 146);
		txtEmail.Name = "txtEmail";
		txtEmail.Size = new Size(420, 23);
		txtEmail.TabIndex = 7;
		//
		// lblDireccion
		//
		lblDireccion.AutoSize = true;
		lblDireccion.Location = new Point(20, 180);
		lblDireccion.Name = "lblDireccion";
		lblDireccion.Size = new Size(60, 15);
		lblDireccion.TabIndex = 8;
		lblDireccion.Text = "Dirección:";
		//
		// txtDireccion
		//
		txtDireccion.Location = new Point(20, 200);
		txtDireccion.Name = "txtDireccion";
		txtDireccion.Size = new Size(420, 23);
		txtDireccion.TabIndex = 9;
		//
		// chkActivo
		//
		chkActivo.AutoSize = true;
		chkActivo.Location = new Point(20, 238);
		chkActivo.Name = "chkActivo";
		chkActivo.Size = new Size(60, 19);
		chkActivo.TabIndex = 10;
		chkActivo.Text = "Activo";
		chkActivo.UseVisualStyleBackColor = true;
		//
		// btnGuardar
		//
		btnGuardar.Location = new Point(268, 272);
		btnGuardar.Name = "btnGuardar";
		btnGuardar.Size = new Size(80, 30);
		btnGuardar.TabIndex = 11;
		btnGuardar.Text = "Guardar";
		btnGuardar.UseVisualStyleBackColor = true;
		btnGuardar.Click += btnGuardar_Click;
		//
		// btnCancelar
		//
		btnCancelar.DialogResult = DialogResult.Cancel;
		btnCancelar.Location = new Point(360, 272);
		btnCancelar.Name = "btnCancelar";
		btnCancelar.Size = new Size(80, 30);
		btnCancelar.TabIndex = 12;
		btnCancelar.Text = "Cancelar";
		btnCancelar.UseVisualStyleBackColor = true;
		btnCancelar.Click += btnCancelar_Click;
		//
		// errProvider
		//
		errProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
		errProvider.ContainerControl = this;
		//
		// FormProveedorEditar
		//
		AcceptButton = btnGuardar;
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = SystemColors.ActiveCaption;
		CancelButton = btnCancelar;
		ClientSize = new Size(460, 320);
		Controls.Add(btnCancelar);
		Controls.Add(btnGuardar);
		Controls.Add(chkActivo);
		Controls.Add(txtDireccion);
		Controls.Add(lblDireccion);
		Controls.Add(txtEmail);
		Controls.Add(lblEmail);
		Controls.Add(txtTelefono);
		Controls.Add(lblTelefono);
		Controls.Add(txtCuit);
		Controls.Add(lblCuit);
		Controls.Add(txtNombre);
		Controls.Add(lblNombre);
		Font = new Font("Segoe UI", 9F);
		FormBorderStyle = FormBorderStyle.FixedDialog;
		MaximizeBox = false;
		MinimizeBox = false;
		Name = "FormProveedorEditar";
		ShowIcon = false;
		StartPosition = FormStartPosition.CenterParent;
		Text = "Proveedor";
		Load += FormProveedorEditar_Load;
		((ISupportInitialize)errProvider).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}
}
