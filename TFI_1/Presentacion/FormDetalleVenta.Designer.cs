using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;
partial class FormDetalleVenta : Form
{
	private IContainer components = null;

	private Label lblTitulo;

	private Label lblCabecera;

	private DataGridView dgvDetalle;

	private Label lblTotales;

	private Button btnImprimir;

	private Button btnTicket;

	private Button btnCerrar;

	private System.Drawing.Printing.PrintDocument printDocument1;

	private PrintPreviewDialog printPreviewDialog1;

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
        ComponentResourceManager resources = new ComponentResourceManager(typeof(FormDetalleVenta));
        lblTitulo = new Label();
        lblCabecera = new Label();
        dgvDetalle = new DataGridView();
        lblTotales = new Label();
        btnImprimir = new Button();
        btnTicket = new Button();
        btnCerrar = new Button();
        printDocument1 = new System.Drawing.Printing.PrintDocument();
        printPreviewDialog1 = new PrintPreviewDialog();
        ((ISupportInitialize)dgvDetalle).BeginInit();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.Location = new Point(12, 10);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(154, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Detalle de venta";
        // 
        // lblCabecera
        // 
        lblCabecera.AutoSize = true;
        lblCabecera.Location = new Point(14, 45);
        lblCabecera.Name = "lblCabecera";
        lblCabecera.Size = new Size(0, 15);
        lblCabecera.TabIndex = 1;
        // 
        // dgvDetalle
        // 
        dgvDetalle.AllowUserToAddRows = false;
        dgvDetalle.AllowUserToDeleteRows = false;
        dgvDetalle.AllowUserToResizeRows = false;
        dgvDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvDetalle.Location = new Point(12, 191);
        dgvDetalle.MultiSelect = false;
        dgvDetalle.Name = "dgvDetalle";
        dgvDetalle.ReadOnly = true;
        dgvDetalle.RowHeadersVisible = false;
        dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDetalle.Size = new Size(776, 250);
        dgvDetalle.TabIndex = 2;
        // 
        // lblTotales
        // 
        lblTotales.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblTotales.AutoSize = true;
        lblTotales.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblTotales.Location = new Point(12, 452);
        lblTotales.Name = "lblTotales";
        lblTotales.Size = new Size(0, 20);
        lblTotales.TabIndex = 3;
        // 
        // btnImprimir
        // 
        btnImprimir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnImprimir.Location = new Point(12, 486);
        btnImprimir.Name = "btnImprimir";
        btnImprimir.Size = new Size(112, 32);
        btnImprimir.TabIndex = 4;
        btnImprimir.Text = "Imprimir";
        btnImprimir.UseVisualStyleBackColor = true;
        btnImprimir.Click += btnImprimir_Click;
        // 
        // btnTicket
        // 
        btnTicket.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnTicket.Location = new Point(132, 486);
        btnTicket.Name = "btnTicket";
        btnTicket.Size = new Size(112, 32);
        btnTicket.TabIndex = 5;
        btnTicket.Text = "Ticket";
        btnTicket.UseVisualStyleBackColor = true;
        btnTicket.Click += btnTicket_Click;
        // 
        // btnCerrar
        // 
        btnCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCerrar.DialogResult = DialogResult.Cancel;
        btnCerrar.Location = new Point(676, 486);
        btnCerrar.Name = "btnCerrar";
        btnCerrar.Size = new Size(112, 32);
        btnCerrar.TabIndex = 6;
        btnCerrar.Text = "Cerrar";
        btnCerrar.UseVisualStyleBackColor = true;
        // 
        // printDocument1
        // 
        printDocument1.DocumentName = "Detalle de venta";
        printDocument1.PrintPage += printDocument1_PrintPage;
        // 
        // printPreviewDialog1
        // 
        printPreviewDialog1.AutoScrollMargin = new Size(0, 0);
        printPreviewDialog1.AutoScrollMinSize = new Size(0, 0);
        printPreviewDialog1.ClientSize = new Size(784, 661);
        printPreviewDialog1.Document = printDocument1;
        printPreviewDialog1.Enabled = true;
        printPreviewDialog1.Icon = (Icon)resources.GetObject("printPreviewDialog1.Icon");
        printPreviewDialog1.Name = "printPreviewDialog1";
        printPreviewDialog1.Visible = false;
        // 
        // FormDetalleVenta
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        CancelButton = btnCerrar;
        ClientSize = new Size(800, 529);
        Controls.Add(btnCerrar);
        Controls.Add(btnTicket);
        Controls.Add(btnImprimir);
        Controls.Add(lblTotales);
        Controls.Add(dgvDetalle);
        Controls.Add(lblCabecera);
        Controls.Add(lblTitulo);
        Font = new Font("Segoe UI", 9F);
        Name = "FormDetalleVenta";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Detalle de Venta";
        Load += FormDetalleVenta_Load;
        ((ISupportInitialize)dgvDetalle).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
