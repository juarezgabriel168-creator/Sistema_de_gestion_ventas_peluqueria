using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;

partial class FormAnularIngresoMercaderia
{
	private IContainer components = null;

	private Label lblTitulo;
	private Label lblResumen;
	private Label lblAviso;
	private Label lblMotivo;
	private TextBox txtMotivo;
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
		lblTitulo = new Label();
		lblResumen = new Label();
		lblAviso = new Label();
		lblMotivo = new Label();
		txtMotivo = new TextBox();
		btnAceptar = new Button();
		btnCancelar = new Button();
		errProvider = new ErrorProvider(components);
		((ISupportInitialize)errProvider).BeginInit();
		SuspendLayout();
		//
		// lblTitulo
		//
		lblTitulo.AutoSize = true;
		lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
		lblTitulo.Location = new Point(20, 16);
		lblTitulo.Name = "lblTitulo";
		lblTitulo.Size = new Size(260, 21);
		lblTitulo.TabIndex = 0;
		lblTitulo.Text = "Anular ingreso de mercadería";
		//
		// lblResumen
		//
		lblResumen.AutoSize = true;
		lblResumen.Location = new Point(20, 48);
		lblResumen.MaximumSize = new Size(440, 0);
		lblResumen.Name = "lblResumen";
		lblResumen.Size = new Size(0, 15);
		lblResumen.TabIndex = 1;
		//
		// lblAviso
		//
		lblAviso.AutoSize = true;
		lblAviso.ForeColor = Color.DarkRed;
		lblAviso.Location = new Point(20, 78);
		lblAviso.MaximumSize = new Size(440, 0);
		lblAviso.Name = "lblAviso";
		lblAviso.Size = new Size(0, 15);
		lblAviso.TabIndex = 2;
		lblAviso.Text = "Se descontará del stock la cantidad ingresada. El ingreso no se borra: queda registrado como anulado.";
		//
		// lblMotivo
		//
		lblMotivo.AutoSize = true;
		lblMotivo.Location = new Point(20, 140);
		lblMotivo.Name = "lblMotivo";
		lblMotivo.Size = new Size(56, 15);
		lblMotivo.TabIndex = 3;
		lblMotivo.Text = "Motivo:";
		//
		// txtMotivo
		//
		txtMotivo.Location = new Point(20, 160);
		txtMotivo.MaxLength = 255;
		txtMotivo.Name = "txtMotivo";
		txtMotivo.PlaceholderText = "Por ejemplo: carga duplicada, proveedor equivocado...";
		txtMotivo.Size = new Size(440, 23);
		txtMotivo.TabIndex = 4;
		//
		// btnAceptar
		//
		btnAceptar.ForeColor = Color.DarkRed;
		btnAceptar.Location = new Point(288, 200);
		btnAceptar.Name = "btnAceptar";
		btnAceptar.Size = new Size(80, 30);
		btnAceptar.TabIndex = 5;
		btnAceptar.Text = "Anular";
		btnAceptar.UseVisualStyleBackColor = true;
		btnAceptar.Click += btnAceptar_Click;
		//
		// btnCancelar
		//
		btnCancelar.DialogResult = DialogResult.Cancel;
		btnCancelar.Location = new Point(380, 200);
		btnCancelar.Name = "btnCancelar";
		btnCancelar.Size = new Size(80, 30);
		btnCancelar.TabIndex = 6;
		btnCancelar.Text = "Cancelar";
		btnCancelar.UseVisualStyleBackColor = true;
		btnCancelar.Click += btnCancelar_Click;
		//
		// errProvider
		//
		errProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
		errProvider.ContainerControl = this;
		//
		// FormAnularIngresoMercaderia
		//
		AcceptButton = btnAceptar;
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = SystemColors.ActiveCaption;
		CancelButton = btnCancelar;
		ClientSize = new Size(480, 250);
		Controls.Add(btnCancelar);
		Controls.Add(btnAceptar);
		Controls.Add(txtMotivo);
		Controls.Add(lblMotivo);
		Controls.Add(lblAviso);
		Controls.Add(lblResumen);
		Controls.Add(lblTitulo);
		Font = new Font("Segoe UI", 9F);
		FormBorderStyle = FormBorderStyle.FixedDialog;
		MaximizeBox = false;
		MinimizeBox = false;
		Name = "FormAnularIngresoMercaderia";
		ShowIcon = false;
		StartPosition = FormStartPosition.CenterParent;
		Text = "Anular ingreso de mercadería";
		((ISupportInitialize)errProvider).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}
}
