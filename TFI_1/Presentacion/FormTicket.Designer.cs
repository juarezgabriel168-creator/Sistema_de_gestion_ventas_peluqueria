using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;
partial class FormTicket : Form
{
	private IContainer components = null;

	private Label lblTitulo;

	private TextBox txtTicket;

	private Button btnImprimir;

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
		lblTitulo = new Label();
		txtTicket = new TextBox();
		btnImprimir = new Button();
		btnCerrar = new Button();
		printDocument1 = new System.Drawing.Printing.PrintDocument();
		printPreviewDialog1 = new PrintPreviewDialog();
		SuspendLayout();
		// 
		// lblTitulo
		// 
		lblTitulo.AutoSize = true;
		lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
		lblTitulo.Location = new Point(12, 10);
		lblTitulo.Name = "lblTitulo";
		lblTitulo.Size = new Size(124, 21);
		lblTitulo.TabIndex = 0;
		lblTitulo.Text = "Ticket de venta";
		// 
		// txtTicket
		// 
		txtTicket.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		txtTicket.BackColor = Color.White;
		txtTicket.Font = new Font("Consolas", 9F);
		txtTicket.Location = new Point(12, 40);
		txtTicket.Multiline = true;
		txtTicket.Name = "txtTicket";
		txtTicket.ReadOnly = true;
		txtTicket.ScrollBars = ScrollBars.Vertical;
		txtTicket.Size = new Size(396, 500);
		txtTicket.TabIndex = 1;
		txtTicket.WordWrap = false;
		// 
		// btnImprimir
		// 
		btnImprimir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		btnImprimir.Location = new Point(12, 552);
		btnImprimir.Name = "btnImprimir";
		btnImprimir.Size = new Size(112, 32);
		btnImprimir.TabIndex = 2;
		btnImprimir.Text = "Imprimir";
		btnImprimir.UseVisualStyleBackColor = true;
		btnImprimir.Click += btnImprimir_Click;
		// 
		// btnCerrar
		// 
		btnCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		btnCerrar.DialogResult = DialogResult.Cancel;
		btnCerrar.Location = new Point(296, 552);
		btnCerrar.Name = "btnCerrar";
		btnCerrar.Size = new Size(112, 32);
		btnCerrar.TabIndex = 3;
		btnCerrar.Text = "Cerrar";
		btnCerrar.UseVisualStyleBackColor = true;
		// 
		// printDocument1
		// 
		printDocument1.DocumentName = "Ticket de venta";
		printDocument1.PrintPage += printDocument1_PrintPage;
		// 
		// printPreviewDialog1
		// 
		printPreviewDialog1.AutoScrollMargin = new Size(0, 0);
		printPreviewDialog1.AutoScrollMinSize = new Size(0, 0);
		printPreviewDialog1.ClientSize = new Size(600, 700);
		printPreviewDialog1.Document = printDocument1;
		printPreviewDialog1.Enabled = true;
		printPreviewDialog1.Name = "printPreviewDialog1";
		printPreviewDialog1.Visible = false;
		// 
		// FormTicket
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = SystemColors.ActiveCaption;
		CancelButton = btnCerrar;
		ClientSize = new Size(420, 596);
		Controls.Add(btnCerrar);
		Controls.Add(btnImprimir);
		Controls.Add(txtTicket);
		Controls.Add(lblTitulo);
		Font = new Font("Segoe UI", 9F);
		Name = "FormTicket";
		StartPosition = FormStartPosition.CenterParent;
		Text = "Ticket de venta";
		Load += FormTicket_Load;
		ResumeLayout(false);
		PerformLayout();
	}
}
