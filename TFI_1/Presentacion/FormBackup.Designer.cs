using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;

partial class FormBackup : Form
{
	private IContainer components = null;

	private Label lblTitulo;
	private Label lblExplicacion;
	private Label lblCarpeta;
	private TextBox txtCarpeta;
	private Button btnElegir;
	private Label lblArchivo;
	private Button btnGenerar;
	private Button btnAbrirCarpeta;
	private Label lblResultado;
	private Label lblAyuda;
	private Button btnCerrar;

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
		lblExplicacion = new Label();
		lblCarpeta = new Label();
		txtCarpeta = new TextBox();
		btnElegir = new Button();
		lblArchivo = new Label();
		btnGenerar = new Button();
		btnAbrirCarpeta = new Button();
		lblResultado = new Label();
		lblAyuda = new Label();
		btnCerrar = new Button();
		SuspendLayout();
		//
		// lblTitulo
		//
		lblTitulo.AutoSize = true;
		lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
		lblTitulo.Location = new Point(12, 12);
		lblTitulo.Name = "lblTitulo";
		lblTitulo.Size = new Size(200, 25);
		lblTitulo.TabIndex = 0;
		lblTitulo.Text = "Copia de seguridad";
		//
		// lblExplicacion
		//
		lblExplicacion.Location = new Point(14, 48);
		lblExplicacion.Name = "lblExplicacion";
		lblExplicacion.Size = new Size(610, 44);
		lblExplicacion.TabIndex = 1;
		lblExplicacion.Text = "Se genera un archivo .sql con toda la base de datos (tablas y datos). " +
			"Guardelo en un lugar seguro: sirve para restaurar el sistema si algo falla.";
		//
		// lblCarpeta
		//
		lblCarpeta.AutoSize = true;
		lblCarpeta.Location = new Point(14, 104);
		lblCarpeta.Name = "lblCarpeta";
		lblCarpeta.Size = new Size(115, 15);
		lblCarpeta.TabIndex = 2;
		lblCarpeta.Text = "Carpeta de destino:";
		//
		// txtCarpeta
		//
		txtCarpeta.Location = new Point(14, 126);
		txtCarpeta.Name = "txtCarpeta";
		txtCarpeta.ReadOnly = true;
		txtCarpeta.Size = new Size(520, 23);
		txtCarpeta.TabIndex = 3;
		//
		// btnElegir
		//
		btnElegir.Location = new Point(544, 124);
		btnElegir.Name = "btnElegir";
		btnElegir.Size = new Size(86, 28);
		btnElegir.TabIndex = 4;
		btnElegir.Text = "Elegir...";
		btnElegir.UseVisualStyleBackColor = true;
		btnElegir.Click += btnElegir_Click;
		//
		// lblArchivo
		//
		lblArchivo.AutoSize = true;
		lblArchivo.ForeColor = Color.DimGray;
		lblArchivo.Location = new Point(14, 160);
		lblArchivo.Name = "lblArchivo";
		lblArchivo.Size = new Size(430, 15);
		lblArchivo.TabIndex = 5;
		lblArchivo.Text = "El archivo se nombra solo: backup_peluqueria_AAAAMMDD_HHMMSS.sql";
		//
		// btnGenerar
		//
		btnGenerar.Location = new Point(14, 194);
		btnGenerar.Name = "btnGenerar";
		btnGenerar.Size = new Size(150, 34);
		btnGenerar.TabIndex = 6;
		btnGenerar.Text = "Generar copia";
		btnGenerar.UseVisualStyleBackColor = true;
		btnGenerar.Click += btnGenerar_Click;
		//
		// btnAbrirCarpeta
		//
		btnAbrirCarpeta.Enabled = false;
		btnAbrirCarpeta.Location = new Point(176, 194);
		btnAbrirCarpeta.Name = "btnAbrirCarpeta";
		btnAbrirCarpeta.Size = new Size(130, 34);
		btnAbrirCarpeta.TabIndex = 7;
		btnAbrirCarpeta.Text = "Abrir carpeta";
		btnAbrirCarpeta.UseVisualStyleBackColor = true;
		btnAbrirCarpeta.Click += btnAbrirCarpeta_Click;
		//
		// lblResultado
		//
		lblResultado.AutoEllipsis = true;
		lblResultado.Location = new Point(14, 244);
		lblResultado.Name = "lblResultado";
		lblResultado.Size = new Size(612, 40);
		lblResultado.TabIndex = 8;
		//
		// lblAyuda
		//
		lblAyuda.ForeColor = Color.DimGray;
		lblAyuda.Location = new Point(14, 294);
		lblAyuda.Name = "lblAyuda";
		lblAyuda.Size = new Size(612, 32);
		lblAyuda.TabIndex = 9;
		lblAyuda.Text = "Para restaurar: mysql -u usuario -p peluqueria < archivo.sql " +
			"(los detalles estan en el README).";
		//
		// btnCerrar
		//
		btnCerrar.Location = new Point(538, 326);
		btnCerrar.Name = "btnCerrar";
		btnCerrar.Size = new Size(88, 30);
		btnCerrar.TabIndex = 10;
		btnCerrar.Text = "Cerrar";
		btnCerrar.UseVisualStyleBackColor = true;
		btnCerrar.Click += btnCerrar_Click;
		//
		// FormBackup
		//
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = SystemColors.ActiveCaption;
		ClientSize = new Size(640, 370);
		Controls.Add(btnCerrar);
		Controls.Add(lblAyuda);
		Controls.Add(lblResultado);
		Controls.Add(btnAbrirCarpeta);
		Controls.Add(btnGenerar);
		Controls.Add(lblArchivo);
		Controls.Add(btnElegir);
		Controls.Add(txtCarpeta);
		Controls.Add(lblCarpeta);
		Controls.Add(lblExplicacion);
		Controls.Add(lblTitulo);
		Font = new Font("Segoe UI", 9F);
		Load += FormBackup_Load;
		Name = "FormBackup";
		StartPosition = FormStartPosition.CenterParent;
		Text = "Copia de seguridad";
		ResumeLayout(false);
		PerformLayout();
	}
}
