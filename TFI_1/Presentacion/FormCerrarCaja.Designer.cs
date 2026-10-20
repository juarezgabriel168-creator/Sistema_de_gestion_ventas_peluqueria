using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;

partial class FormCerrarCaja : Form
{
	private IContainer components = null;

	private Label lblTitulo;
	private Label lblEsperado;
	private Label lblContado;
	private NumericUpDown numContado;
	private Label lblDiferencia;
	private Label lblObs;
	private TextBox txtObservacion;
	private Button btnConfirmar;
	private Button btnCancelar;

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
		lblTitulo = new Label();
		lblEsperado = new Label();
		lblContado = new Label();
		numContado = new NumericUpDown();
		lblDiferencia = new Label();
		lblObs = new Label();
		txtObservacion = new TextBox();
		btnConfirmar = new Button();
		btnCancelar = new Button();
		((ISupportInitialize)numContado).BeginInit();
		SuspendLayout();
		//
		// lblTitulo
		//
		lblTitulo.AutoSize = true;
		lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
		lblTitulo.Location = new Point(16, 14);
		lblTitulo.Name = "lblTitulo";
		lblTitulo.Size = new Size(100, 21);
		lblTitulo.TabIndex = 0;
		lblTitulo.Text = "Cerrar caja";
		//
		// lblEsperado
		//
		lblEsperado.AutoSize = true;
		lblEsperado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
		lblEsperado.Location = new Point(16, 50);
		lblEsperado.Name = "lblEsperado";
		lblEsperado.Size = new Size(220, 19);
		lblEsperado.TabIndex = 1;
		lblEsperado.Text = "Efectivo esperado en caja:";
		//
		// lblContado
		//
		lblContado.AutoSize = true;
		lblContado.Location = new Point(16, 87);
		lblContado.Name = "lblContado";
		lblContado.Size = new Size(95, 15);
		lblContado.TabIndex = 2;
		lblContado.Text = "Efectivo contado:";
		//
		// numContado
		//
		numContado.DecimalPlaces = 2;
		numContado.Location = new Point(150, 84);
		numContado.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
		numContado.Name = "numContado";
		numContado.Size = new Size(150, 23);
		numContado.TabIndex = 3;
		numContado.ThousandsSeparator = true;
		numContado.ValueChanged += numContado_ValueChanged;
		//
		// lblDiferencia
		//
		lblDiferencia.AutoSize = true;
		lblDiferencia.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
		lblDiferencia.Location = new Point(16, 120);
		lblDiferencia.Name = "lblDiferencia";
		lblDiferencia.Size = new Size(80, 19);
		lblDiferencia.TabIndex = 4;
		lblDiferencia.Text = "Diferencia:";
		//
		// lblObs
		//
		lblObs.AutoSize = true;
		lblObs.Location = new Point(16, 155);
		lblObs.Name = "lblObs";
		lblObs.Size = new Size(76, 15);
		lblObs.TabIndex = 5;
		lblObs.Text = "Observación:";
		//
		// txtObservacion
		//
		txtObservacion.Location = new Point(16, 177);
		txtObservacion.MaxLength = 255;
		txtObservacion.Name = "txtObservacion";
		txtObservacion.Size = new Size(448, 23);
		txtObservacion.TabIndex = 6;
		//
		// btnConfirmar
		//
		btnConfirmar.Location = new Point(272, 210);
		btnConfirmar.Name = "btnConfirmar";
		btnConfirmar.Size = new Size(90, 30);
		btnConfirmar.TabIndex = 7;
		btnConfirmar.Text = "Confirmar";
		btnConfirmar.UseVisualStyleBackColor = true;
		btnConfirmar.Click += btnConfirmar_Click;
		//
		// btnCancelar
		//
		btnCancelar.Location = new Point(372, 210);
		btnCancelar.Name = "btnCancelar";
		btnCancelar.Size = new Size(90, 30);
		btnCancelar.TabIndex = 8;
		btnCancelar.Text = "Cancelar";
		btnCancelar.UseVisualStyleBackColor = true;
		btnCancelar.Click += btnCancelar_Click;
		//
		// FormCerrarCaja
		//
		AcceptButton = btnConfirmar;
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = SystemColors.ActiveCaption;
		CancelButton = btnCancelar;
		ClientSize = new Size(480, 256);
		Controls.Add(btnCancelar);
		Controls.Add(btnConfirmar);
		Controls.Add(txtObservacion);
		Controls.Add(lblObs);
		Controls.Add(lblDiferencia);
		Controls.Add(numContado);
		Controls.Add(lblContado);
		Controls.Add(lblEsperado);
		Controls.Add(lblTitulo);
		Font = new Font("Segoe UI", 9F);
		FormBorderStyle = FormBorderStyle.FixedDialog;
		MaximizeBox = false;
		MinimizeBox = false;
		Name = "FormCerrarCaja";
		StartPosition = FormStartPosition.CenterParent;
		Text = "Cerrar caja";
		((ISupportInitialize)numContado).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}
}
