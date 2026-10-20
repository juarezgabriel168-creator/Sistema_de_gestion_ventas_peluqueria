using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
partial class FormItemEditar : Form
{
	private IContainer components = null;

	private Label lblTipo;

	private ComboBox cboTipo;

	private Label lblNombre;

	private TextBox txtNombre;

	private Label lblPrecio;

	private TextBox txtPrecio;

	private Label lblCategoria;

	private ComboBox cboCategoria;

	private Label lblDuracion;

	private TextBox txtDuracion;

	private Label lblStock;

	private TextBox txtStock;

	private Label lblStockMinimo;

	private TextBox txtStockMinimo;

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
        lblTipo = new Label();
        cboTipo = new ComboBox();
        lblNombre = new Label();
        txtNombre = new TextBox();
        lblPrecio = new Label();
        txtPrecio = new TextBox();
        lblCategoria = new Label();
        cboCategoria = new ComboBox();
        lblDuracion = new Label();
        txtDuracion = new TextBox();
        lblStock = new Label();
        txtStock = new TextBox();
        lblStockMinimo = new Label();
        txtStockMinimo = new TextBox();
        chkActivo = new CheckBox();
        btnGuardar = new Button();
        btnCancelar = new Button();
        errProvider = new ErrorProvider(components);
        ((ISupportInitialize)errProvider).BeginInit();
        SuspendLayout();
        // 
        // lblTipo
        // 
        lblTipo.AutoSize = true;
        lblTipo.Location = new Point(20, 18);
        lblTipo.Name = "lblTipo";
        lblTipo.Size = new Size(34, 15);
        lblTipo.TabIndex = 0;
        lblTipo.Text = "Tipo:";
        // 
        // cboTipo
        // 
        cboTipo.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTipo.FormattingEnabled = true;
        cboTipo.Location = new Point(20, 36);
        cboTipo.Name = "cboTipo";
        cboTipo.Size = new Size(300, 23);
        cboTipo.TabIndex = 1;
        cboTipo.SelectedIndexChanged += cboTipo_SelectedIndexChanged;
        // 
        // lblNombre
        // 
        lblNombre.AutoSize = true;
        lblNombre.Location = new Point(20, 70);
        lblNombre.Name = "lblNombre";
        lblNombre.Size = new Size(54, 15);
        lblNombre.TabIndex = 2;
        lblNombre.Text = "Nombre:";
        // 
        // txtNombre
        // 
        txtNombre.Location = new Point(20, 88);
        txtNombre.MaxLength = 80;
        txtNombre.Name = "txtNombre";
        txtNombre.Size = new Size(300, 23);
        txtNombre.TabIndex = 3;
        // 
        // lblPrecio
        // 
        lblPrecio.AutoSize = true;
        lblPrecio.Location = new Point(20, 122);
        lblPrecio.Name = "lblPrecio";
        lblPrecio.Size = new Size(43, 15);
        lblPrecio.TabIndex = 4;
        lblPrecio.Text = "Precio:";
        // 
        // txtPrecio
        // 
        txtPrecio.Location = new Point(20, 140);
        txtPrecio.Name = "txtPrecio";
        txtPrecio.Size = new Size(140, 23);
        txtPrecio.TabIndex = 5;
        // 
        // lblCategoria
        // 
        lblCategoria.AutoSize = true;
        lblCategoria.Location = new Point(180, 122);
        lblCategoria.Name = "lblCategoria";
        lblCategoria.Size = new Size(61, 15);
        lblCategoria.TabIndex = 6;
        lblCategoria.Text = "Categoría:";
        // 
        // cboCategoria
        // 
        cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCategoria.FormattingEnabled = true;
        cboCategoria.Location = new Point(180, 140);
        cboCategoria.Name = "cboCategoria";
        cboCategoria.Size = new Size(140, 23);
        cboCategoria.TabIndex = 7;
        // 
        // lblDuracion
        // 
        lblDuracion.AutoSize = true;
        lblDuracion.Location = new Point(20, 174);
        lblDuracion.Name = "lblDuracion";
        lblDuracion.Size = new Size(90, 15);
        lblDuracion.TabIndex = 8;
        lblDuracion.Text = "Duración (min):";
        // 
        // txtDuracion
        // 
        txtDuracion.Location = new Point(20, 192);
        txtDuracion.Name = "txtDuracion";
        txtDuracion.Size = new Size(140, 23);
        txtDuracion.TabIndex = 9;
        // 
        // lblStock
        // 
        lblStock.AutoSize = true;
        lblStock.Location = new Point(20, 174);
        lblStock.Name = "lblStock";
        lblStock.Size = new Size(39, 15);
        lblStock.TabIndex = 10;
        lblStock.Text = "Stock:";
        // 
        // txtStock
        // 
        txtStock.Location = new Point(20, 192);
        txtStock.Name = "txtStock";
        txtStock.Size = new Size(140, 23);
        txtStock.TabIndex = 11;
        // 
        // lblStockMinimo
        // 
        lblStockMinimo.AutoSize = true;
        lblStockMinimo.Location = new Point(180, 174);
        lblStockMinimo.Name = "lblStockMinimo";
        lblStockMinimo.Size = new Size(84, 15);
        lblStockMinimo.TabIndex = 12;
        lblStockMinimo.Text = "Stock mínimo:";
        // 
        // txtStockMinimo
        // 
        txtStockMinimo.Location = new Point(180, 192);
        txtStockMinimo.Name = "txtStockMinimo";
        txtStockMinimo.Size = new Size(140, 23);
        txtStockMinimo.TabIndex = 13;
        // 
        // chkActivo
        // 
        chkActivo.AutoSize = true;
        chkActivo.Location = new Point(20, 240);
        chkActivo.Name = "chkActivo";
        chkActivo.Size = new Size(60, 19);
        chkActivo.TabIndex = 14;
        chkActivo.Text = "Activo";
        chkActivo.UseVisualStyleBackColor = true;
        // 
        // btnGuardar
        // 
        btnGuardar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnGuardar.Location = new Point(140, 280);
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Size = new Size(100, 32);
        btnGuardar.TabIndex = 15;
        btnGuardar.Text = "Guardar";
        btnGuardar.UseVisualStyleBackColor = true;
        btnGuardar.Click += btnGuardar_Click;
        // 
        // btnCancelar
        // 
        btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancelar.DialogResult = DialogResult.Cancel;
        btnCancelar.Location = new Point(250, 280);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(110, 32);
        btnCancelar.TabIndex = 16;
        btnCancelar.Text = "Cancelar";
        btnCancelar.UseVisualStyleBackColor = true;
        btnCancelar.Click += btnCancelar_Click;
        // 
        // errProvider
        // 
        errProvider.ContainerControl = this;
        // 
        // FormItemEditar
        // 
        AcceptButton = btnGuardar;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancelar;
        ClientSize = new Size(380, 330);
        Controls.Add(btnCancelar);
        Controls.Add(btnGuardar);
        Controls.Add(chkActivo);
        Controls.Add(txtStockMinimo);
        Controls.Add(lblStockMinimo);
        Controls.Add(txtStock);
        Controls.Add(lblStock);
        Controls.Add(txtDuracion);
        Controls.Add(lblDuracion);
        Controls.Add(cboCategoria);
        Controls.Add(lblCategoria);
        Controls.Add(txtPrecio);
        Controls.Add(lblPrecio);
        Controls.Add(txtNombre);
        Controls.Add(lblNombre);
        Controls.Add(cboTipo);
        Controls.Add(lblTipo);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormItemEditar";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Ítem";
        Load += FormItemEditar_Load;
        ((ISupportInitialize)errProvider).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
