using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;

partial class FormConfirmarContrasena
{
	private IContainer components = null;

	private Label lblMensaje;
	private Label lblContrasena;
	private TextBox txtContrasena;
	private CheckBox chkMostrar;
	private Button btnAceptar;
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
		lblMensaje = new Label();
		lblContrasena = new Label();
		txtContrasena = new TextBox();
		chkMostrar = new CheckBox();
		btnAceptar = new Button();
		btnCancelar = new Button();
		errProvider = new ErrorProvider(components);
		((ISupportInitialize)errProvider).BeginInit();
		SuspendLayout();
		// 
		// lblMensaje
		// 
		lblMensaje.Location = new Point(20, 20);
		lblMensaje.Name = "lblMensaje";
		lblMensaje.Size = new Size(420, 45);
		lblMensaje.TabIndex = 0;
		lblMensaje.Text = "Va a eliminar el registro seleccionado.";
		// 
		// lblContrasena
		// 
		lblContrasena.AutoSize = true;
		lblContrasena.Location = new Point(20, 78);
		lblContrasena.Name = "lblContrasena";
		lblContrasena.Size = new Size(164, 15);
		lblContrasena.TabIndex = 1;
		lblContrasena.Text = "Contraseña del usuario actual:";
		// 
		// txtContrasena
		// 
		txtContrasena.Location = new Point(20, 100);
		txtContrasena.Name = "txtContrasena";
		txtContrasena.Size = new Size(420, 23);
		txtContrasena.TabIndex = 2;
		txtContrasena.UseSystemPasswordChar = true;
		// 
		// chkMostrar
		// 
		chkMostrar.AutoSize = true;
		chkMostrar.Location = new Point(20, 132);
		chkMostrar.Name = "chkMostrar";
		chkMostrar.Size = new Size(123, 19);
		chkMostrar.TabIndex = 3;
		chkMostrar.Text = "Mostrar contraseña";
		chkMostrar.UseVisualStyleBackColor = true;
		chkMostrar.CheckedChanged += chkMostrar_CheckedChanged;
		// 
		// btnAceptar
		// 
		btnAceptar.ForeColor = Color.Firebrick;
		btnAceptar.Location = new Point(250, 175);
		btnAceptar.Name = "btnAceptar";
		btnAceptar.Size = new Size(90, 32);
		btnAceptar.TabIndex = 4;
		btnAceptar.Text = "Eliminar";
		btnAceptar.UseVisualStyleBackColor = true;
		btnAceptar.Click += btnAceptar_Click;
		// 
		// btnCancelar
		// 
		btnCancelar.DialogResult = DialogResult.Cancel;
		btnCancelar.Location = new Point(350, 175);
		btnCancelar.Name = "btnCancelar";
		btnCancelar.Size = new Size(90, 32);
		btnCancelar.TabIndex = 5;
		btnCancelar.Text = "Cancelar";
		btnCancelar.UseVisualStyleBackColor = true;
		btnCancelar.Click += btnCancelar_Click;
		// 
		// errProvider
		// 
		errProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
		errProvider.ContainerControl = this;
		// 
		// FormConfirmarContrasena
		// 
		AcceptButton = btnAceptar;
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		CancelButton = btnCancelar;
		ClientSize = new Size(460, 230);
		Controls.Add(btnCancelar);
		Controls.Add(btnAceptar);
		Controls.Add(chkMostrar);
		Controls.Add(txtContrasena);
		Controls.Add(lblContrasena);
		Controls.Add(lblMensaje);
		Font = new Font("Segoe UI", 9F);
		FormBorderStyle = FormBorderStyle.FixedDialog;
		MaximizeBox = false;
		MinimizeBox = false;
		Name = "FormConfirmarContrasena";
		ShowInTaskbar = false;
		StartPosition = FormStartPosition.CenterParent;
		Text = "Confirmar eliminación";
		Shown += FormConfirmarContrasena_Shown;
		((ISupportInitialize)errProvider).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}
}
