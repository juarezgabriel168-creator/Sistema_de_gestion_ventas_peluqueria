using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
partial class FormUsuarioEditar : Form
{
	private IContainer components = null;

	private Label lblUsuario;

	private TextBox txtUsuario;

	private Label lblContrasena;

	private TextBox txtContrasena;

	private Label lblConfirmar;

	private TextBox txtConfirmar;

	private Label lblAyuda;

	private Label lblRol;

	private ComboBox cboRol;

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
        lblUsuario = new Label();
        txtUsuario = new TextBox();
        lblContrasena = new Label();
        txtContrasena = new TextBox();
        lblConfirmar = new Label();
        txtConfirmar = new TextBox();
        lblAyuda = new Label();
        lblRol = new Label();
        cboRol = new ComboBox();
        chkActivo = new CheckBox();
        btnGuardar = new Button();
        btnCancelar = new Button();
        errProvider = new ErrorProvider(components);
        ((ISupportInitialize)errProvider).BeginInit();
        SuspendLayout();
        // 
        // lblUsuario
        // 
        lblUsuario.AutoSize = true;
        lblUsuario.Location = new Point(20, 18);
        lblUsuario.Name = "lblUsuario";
        lblUsuario.Size = new Size(50, 15);
        lblUsuario.TabIndex = 0;
        lblUsuario.Text = "Usuario:";
        // 
        // txtUsuario
        // 
        txtUsuario.Location = new Point(20, 36);
        txtUsuario.MaxLength = 40;
        txtUsuario.Name = "txtUsuario";
        txtUsuario.Size = new Size(300, 23);
        txtUsuario.TabIndex = 1;
        // 
        // lblContrasena
        // 
        lblContrasena.AutoSize = true;
        lblContrasena.Location = new Point(20, 70);
        lblContrasena.Name = "lblContrasena";
        lblContrasena.Size = new Size(70, 15);
        lblContrasena.TabIndex = 2;
        lblContrasena.Text = "Contraseña:";
        // 
        // txtContrasena
        // 
        txtContrasena.Location = new Point(20, 88);
        txtContrasena.Name = "txtContrasena";
        txtContrasena.Size = new Size(300, 23);
        txtContrasena.TabIndex = 3;
        txtContrasena.UseSystemPasswordChar = true;
        // 
        // lblConfirmar
        // 
        lblConfirmar.AutoSize = true;
        lblConfirmar.Location = new Point(20, 122);
        lblConfirmar.Name = "lblConfirmar";
        lblConfirmar.Size = new Size(125, 15);
        lblConfirmar.TabIndex = 4;
        lblConfirmar.Text = "Confirmar contraseña:";
        // 
        // txtConfirmar
        // 
        txtConfirmar.Location = new Point(20, 140);
        txtConfirmar.Name = "txtConfirmar";
        txtConfirmar.Size = new Size(300, 23);
        txtConfirmar.TabIndex = 5;
        txtConfirmar.UseSystemPasswordChar = true;
        // 
        // lblAyuda
        // 
        lblAyuda.AutoSize = true;
        lblAyuda.ForeColor = Color.Gray;
        lblAyuda.Location = new Point(20, 166);
        lblAyuda.Name = "lblAyuda";
        lblAyuda.Size = new Size(294, 15);
        lblAyuda.TabIndex = 6;
        lblAyuda.Text = "En edición: deje la contraseña vacía para no cambiarla.";
        // 
        // lblRol
        // 
        lblRol.AutoSize = true;
        lblRol.Location = new Point(20, 192);
        lblRol.Name = "lblRol";
        lblRol.Size = new Size(27, 15);
        lblRol.TabIndex = 7;
        lblRol.Text = "Rol:";
        // 
        // cboRol
        // 
        cboRol.DropDownStyle = ComboBoxStyle.DropDownList;
        cboRol.FormattingEnabled = true;
        cboRol.Location = new Point(20, 210);
        cboRol.Name = "cboRol";
        cboRol.Size = new Size(300, 23);
        cboRol.TabIndex = 8;
        // 
        // chkActivo
        // 
        chkActivo.AutoSize = true;
        chkActivo.Location = new Point(20, 245);
        chkActivo.Name = "chkActivo";
        chkActivo.Size = new Size(60, 19);
        chkActivo.TabIndex = 9;
        chkActivo.Text = "Activo";
        chkActivo.UseVisualStyleBackColor = true;
        // 
        // btnGuardar
        // 
        btnGuardar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnGuardar.Location = new Point(180, 280);
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Size = new Size(100, 32);
        btnGuardar.TabIndex = 10;
        btnGuardar.Text = "Guardar";
        btnGuardar.UseVisualStyleBackColor = true;
        btnGuardar.Click += btnGuardar_Click;
        // 
        // btnCancelar
        // 
        btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancelar.DialogResult = DialogResult.Cancel;
        btnCancelar.Location = new Point(290, 280);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(110, 32);
        btnCancelar.TabIndex = 11;
        btnCancelar.Text = "Cancelar";
        btnCancelar.UseVisualStyleBackColor = true;
        btnCancelar.Click += btnCancelar_Click;
        // 
        // errProvider
        // 
        errProvider.ContainerControl = this;
        // 
        // FormUsuarioEditar
        // 
        AcceptButton = btnGuardar;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        CancelButton = btnCancelar;
        ClientSize = new Size(420, 330);
        Controls.Add(btnCancelar);
        Controls.Add(btnGuardar);
        Controls.Add(chkActivo);
        Controls.Add(cboRol);
        Controls.Add(lblRol);
        Controls.Add(lblAyuda);
        Controls.Add(txtConfirmar);
        Controls.Add(lblConfirmar);
        Controls.Add(txtContrasena);
        Controls.Add(lblContrasena);
        Controls.Add(txtUsuario);
        Controls.Add(lblUsuario);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormUsuarioEditar";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Usuario";
        Load += FormUsuarioEditar_Load;
        ((ISupportInitialize)errProvider).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
