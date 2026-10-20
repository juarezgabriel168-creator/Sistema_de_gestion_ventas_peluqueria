using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
partial class FormDashboard : Form
{
	private IContainer components = null;

	private Label lblTitulo;

	private Label lblSubtitulo;

	private Button btnActualizar;

	private Label lblMayorStock;

	private Label lblValorMayorStock;

	private Label lblMenorStock;

	private Label lblValorMenorStock;

	private Panel pnlGrafico;

	private Panel panelCard1;

	private Label lblValorVendido;

	private Label lblTituloVendido;

	private Panel panelCard2;

	private Label lblValorVentas;

	private Label lblTituloVentas;

	private Panel panelCard3;

	private Label lblValorClientes;

	private Label lblTituloClientes;

	private Panel panelCard4;

	private Label lblValorItems;

	private Label lblTituloItems;

	private Panel panelCard5;

	private Label lblValorStock;

	private Label lblTituloStock;

	private Panel panelCard6;

	private Label lblValorTurnos;

	private Label lblTituloTurnos;

	private Label lblUltimas;

	private DataGridView dgvUltimasVentas;

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
        lblSubtitulo = new Label();
        btnActualizar = new Button();
        panelCard1 = new Panel();
        lblValorVendido = new Label();
        lblTituloVendido = new Label();
        panelCard2 = new Panel();
        lblValorVentas = new Label();
        lblTituloVentas = new Label();
        panelCard3 = new Panel();
        lblValorClientes = new Label();
        lblTituloClientes = new Label();
        panelCard4 = new Panel();
        lblValorItems = new Label();
        lblTituloItems = new Label();
        panelCard5 = new Panel();
        lblValorStock = new Label();
        lblTituloStock = new Label();
        panelCard6 = new Panel();
        lblValorTurnos = new Label();
        lblTituloTurnos = new Label();
        lblUltimas = new Label();
        dgvUltimasVentas = new DataGridView();
        pnlGrafico = new Panel();
        lblMayorStock = new Label();
        lblValorMayorStock = new Label();
        lblMenorStock = new Label();
        lblValorMenorStock = new Label();
        panelCard1.SuspendLayout();
        panelCard2.SuspendLayout();
        panelCard3.SuspendLayout();
        panelCard4.SuspendLayout();
        panelCard5.SuspendLayout();
        panelCard6.SuspendLayout();
        ((ISupportInitialize)dgvUltimasVentas).BeginInit();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitulo.Location = new Point(12, 12);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(229, 30);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Resumen del sistema";
        // 
        // lblSubtitulo
        // 
        lblSubtitulo.AutoSize = true;
        lblSubtitulo.ForeColor = Color.Gray;
        lblSubtitulo.Location = new Point(15, 44);
        lblSubtitulo.Name = "lblSubtitulo";
        lblSubtitulo.Size = new Size(241, 15);
        lblSubtitulo.TabIndex = 1;
        lblSubtitulo.Text = "Indicadores en vivo desde la base Peluquería";
        // 
        // btnActualizar
        // 
        btnActualizar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnActualizar.Location = new Point(892, 20);
        btnActualizar.Name = "btnActualizar";
        btnActualizar.Size = new Size(176, 32);
        btnActualizar.TabIndex = 2;
        btnActualizar.Text = "Actualizar";
        btnActualizar.UseVisualStyleBackColor = true;
        btnActualizar.Click += btnActualizar_Click;
        // 
        // panelCard1
        // 
        panelCard1.BackColor = Color.FromArgb(240, 244, 248);
        panelCard1.BorderStyle = BorderStyle.FixedSingle;
        panelCard1.Controls.Add(lblValorVendido);
        panelCard1.Controls.Add(lblTituloVendido);
        panelCard1.Location = new Point(12, 75);
        panelCard1.Name = "panelCard1";
        panelCard1.Size = new Size(168, 90);
        panelCard1.TabIndex = 3;
        // 
        // lblValorVendido
        // 
        lblValorVendido.AutoSize = true;
        lblValorVendido.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblValorVendido.Location = new Point(8, 34);
        lblValorVendido.Name = "lblValorVendido";
        lblValorVendido.Size = new Size(28, 32);
        lblValorVendido.TabIndex = 1;
        lblValorVendido.Text = "0";
        // 
        // lblTituloVendido
        // 
        lblTituloVendido.AutoSize = true;
        lblTituloVendido.ForeColor = Color.Gray;
        lblTituloVendido.Location = new Point(10, 12);
        lblTituloVendido.Name = "lblTituloVendido";
        lblTituloVendido.Size = new Size(73, 15);
        lblTituloVendido.TabIndex = 0;
        lblTituloVendido.Text = "Vendido hoy";
        // 
        // panelCard2
        // 
        panelCard2.BackColor = Color.FromArgb(240, 244, 248);
        panelCard2.BorderStyle = BorderStyle.FixedSingle;
        panelCard2.Controls.Add(lblValorVentas);
        panelCard2.Controls.Add(lblTituloVentas);
        panelCard2.Location = new Point(192, 75);
        panelCard2.Name = "panelCard2";
        panelCard2.Size = new Size(168, 90);
        panelCard2.TabIndex = 4;
        // 
        // lblValorVentas
        // 
        lblValorVentas.AutoSize = true;
        lblValorVentas.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblValorVentas.Location = new Point(8, 34);
        lblValorVentas.Name = "lblValorVentas";
        lblValorVentas.Size = new Size(28, 32);
        lblValorVentas.TabIndex = 1;
        lblValorVentas.Text = "0";
        // 
        // lblTituloVentas
        // 
        lblTituloVentas.AutoSize = true;
        lblTituloVentas.ForeColor = Color.Gray;
        lblTituloVentas.Location = new Point(10, 12);
        lblTituloVentas.Name = "lblTituloVentas";
        lblTituloVentas.Size = new Size(64, 15);
        lblTituloVentas.TabIndex = 0;
        lblTituloVentas.Text = "Ventas hoy";
        // 
        // panelCard3
        // 
        panelCard3.BackColor = Color.FromArgb(240, 244, 248);
        panelCard3.BorderStyle = BorderStyle.FixedSingle;
        panelCard3.Controls.Add(lblValorClientes);
        panelCard3.Controls.Add(lblTituloClientes);
        panelCard3.Location = new Point(372, 75);
        panelCard3.Name = "panelCard3";
        panelCard3.Size = new Size(168, 90);
        panelCard3.TabIndex = 5;
        // 
        // lblValorClientes
        // 
        lblValorClientes.AutoSize = true;
        lblValorClientes.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblValorClientes.Location = new Point(8, 34);
        lblValorClientes.Name = "lblValorClientes";
        lblValorClientes.Size = new Size(28, 32);
        lblValorClientes.TabIndex = 1;
        lblValorClientes.Text = "0";
        // 
        // lblTituloClientes
        // 
        lblTituloClientes.AutoSize = true;
        lblTituloClientes.ForeColor = Color.Gray;
        lblTituloClientes.Location = new Point(10, 12);
        lblTituloClientes.Name = "lblTituloClientes";
        lblTituloClientes.Size = new Size(49, 15);
        lblTituloClientes.TabIndex = 0;
        lblTituloClientes.Text = "Clientes";
        // 
        // panelCard4
        // 
        panelCard4.BackColor = Color.FromArgb(240, 244, 248);
        panelCard4.BorderStyle = BorderStyle.FixedSingle;
        panelCard4.Controls.Add(lblValorItems);
        panelCard4.Controls.Add(lblTituloItems);
        panelCard4.Location = new Point(552, 75);
        panelCard4.Name = "panelCard4";
        panelCard4.Size = new Size(168, 90);
        panelCard4.TabIndex = 6;
        // 
        // lblValorItems
        // 
        lblValorItems.AutoSize = true;
        lblValorItems.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblValorItems.Location = new Point(8, 34);
        lblValorItems.Name = "lblValorItems";
        lblValorItems.Size = new Size(28, 32);
        lblValorItems.TabIndex = 1;
        lblValorItems.Text = "0";
        // 
        // lblTituloItems
        // 
        lblTituloItems.AutoSize = true;
        lblTituloItems.ForeColor = Color.Gray;
        lblTituloItems.Location = new Point(10, 12);
        lblTituloItems.Name = "lblTituloItems";
        lblTituloItems.Size = new Size(118, 15);
        lblTituloItems.TabIndex = 0;
        lblTituloItems.Text = "Productos y servicios";
        // 
        // panelCard5
        // 
        panelCard5.BackColor = Color.FromArgb(253, 240, 240);
        panelCard5.BorderStyle = BorderStyle.FixedSingle;
        panelCard5.Controls.Add(lblValorStock);
        panelCard5.Controls.Add(lblTituloStock);
        panelCard5.Location = new Point(732, 75);
        panelCard5.Name = "panelCard5";
        panelCard5.Size = new Size(168, 90);
        panelCard5.TabIndex = 7;
        // 
        // lblValorStock
        // 
        lblValorStock.AutoSize = true;
        lblValorStock.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblValorStock.ForeColor = Color.FromArgb(178, 34, 34);
        lblValorStock.Location = new Point(8, 34);
        lblValorStock.Name = "lblValorStock";
        lblValorStock.Size = new Size(28, 32);
        lblValorStock.TabIndex = 1;
        lblValorStock.Text = "0";
        // 
        // lblTituloStock
        // 
        lblTituloStock.AutoSize = true;
        lblTituloStock.ForeColor = Color.FromArgb(178, 34, 34);
        lblTituloStock.Location = new Point(10, 12);
        lblTituloStock.Name = "lblTituloStock";
        lblTituloStock.Size = new Size(62, 15);
        lblTituloStock.TabIndex = 0;
        lblTituloStock.Text = "Stock bajo";
        // 
        // panelCard6
        // 
        panelCard6.BackColor = Color.FromArgb(240, 248, 244);
        panelCard6.BorderStyle = BorderStyle.FixedSingle;
        panelCard6.Controls.Add(lblValorTurnos);
        panelCard6.Controls.Add(lblTituloTurnos);
        panelCard6.Location = new Point(912, 75);
        panelCard6.Name = "panelCard6";
        panelCard6.Size = new Size(168, 90);
        panelCard6.TabIndex = 8;
        // 
        // lblValorTurnos
        // 
        lblValorTurnos.AutoSize = true;
        lblValorTurnos.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblValorTurnos.ForeColor = Color.FromArgb(0, 102, 51);
        lblValorTurnos.Location = new Point(8, 34);
        lblValorTurnos.Name = "lblValorTurnos";
        lblValorTurnos.Size = new Size(28, 32);
        lblValorTurnos.TabIndex = 1;
        lblValorTurnos.Text = "0";
        // 
        // lblTituloTurnos
        // 
        lblTituloTurnos.AutoSize = true;
        lblTituloTurnos.ForeColor = Color.FromArgb(0, 102, 51);
        lblTituloTurnos.Location = new Point(10, 12);
        lblTituloTurnos.Name = "lblTituloTurnos";
        lblTituloTurnos.Size = new Size(67, 15);
        lblTituloTurnos.TabIndex = 0;
        lblTituloTurnos.Text = "Turnos hoy";
        // 
        // lblUltimas
        // 
        lblUltimas.AutoSize = true;
        lblUltimas.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblUltimas.Location = new Point(12, 180);
        lblUltimas.Name = "lblUltimas";
        lblUltimas.Size = new Size(113, 20);
        lblUltimas.TabIndex = 8;
        lblUltimas.Text = "Últimas ventas";
        // 
        // dgvUltimasVentas
        // 
        dgvUltimasVentas.AllowUserToAddRows = false;
        dgvUltimasVentas.AllowUserToDeleteRows = false;
        dgvUltimasVentas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        dgvUltimasVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvUltimasVentas.BackgroundColor = SystemColors.ButtonHighlight;
        dgvUltimasVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvUltimasVentas.Location = new Point(15, 238);
        dgvUltimasVentas.Name = "dgvUltimasVentas";
        dgvUltimasVentas.ReadOnly = true;
        dgvUltimasVentas.RowHeadersVisible = false;
        dgvUltimasVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvUltimasVentas.Size = new Size(1056, 200);
        dgvUltimasVentas.TabIndex = 9;
        // 
        // pnlGrafico
        // 
        pnlGrafico.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        pnlGrafico.BorderStyle = BorderStyle.FixedSingle;
        pnlGrafico.Location = new Point(12, 448);
        pnlGrafico.Name = "pnlGrafico";
        pnlGrafico.Size = new Size(1056, 140);
        pnlGrafico.TabIndex = 14;
        pnlGrafico.Paint += pnlGrafico_Paint;
        pnlGrafico.MouseMove += pnlGrafico_MouseMove;
        pnlGrafico.MouseLeave += pnlGrafico_MouseLeave;
        pnlGrafico.Resize += pnlGrafico_Resize;
        // 
        // lblMayorStock
        // 
        lblMayorStock.AutoSize = true;
        lblMayorStock.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblMayorStock.Location = new Point(921, 168);
        lblMayorStock.Name = "lblMayorStock";
        lblMayorStock.Size = new Size(132, 15);
        lblMayorStock.TabIndex = 10;
        lblMayorStock.Text = "Producto mayor stock:";
        // 
        // lblValorMayorStock
        // 
        lblValorMayorStock.AutoSize = true;
        lblValorMayorStock.Location = new Point(732, 186);
        lblValorMayorStock.Name = "lblValorMayorStock";
        lblValorMayorStock.Size = new Size(0, 15);
        lblValorMayorStock.TabIndex = 11;
        // 
        // lblMenorStock
        // 
        lblMenorStock.AutoSize = true;
        lblMenorStock.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblMenorStock.Location = new Point(741, 168);
        lblMenorStock.Name = "lblMenorStock";
        lblMenorStock.Size = new Size(134, 15);
        lblMenorStock.TabIndex = 12;
        lblMenorStock.Text = "Producto menor stock:";
        // 
        // lblValorMenorStock
        // 
        lblValorMenorStock.AutoSize = true;
        lblValorMenorStock.Location = new Point(732, 220);
        lblValorMenorStock.Name = "lblValorMenorStock";
        lblValorMenorStock.Size = new Size(0, 15);
        lblValorMenorStock.TabIndex = 13;
        // 
        // FormDashboard
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        ClientSize = new Size(1080, 600);
        Controls.Add(dgvUltimasVentas);
        Controls.Add(lblUltimas);
        Controls.Add(pnlGrafico);
        Controls.Add(lblValorMenorStock);
        Controls.Add(lblMenorStock);
        Controls.Add(lblValorMayorStock);
        Controls.Add(lblMayorStock);
        Controls.Add(panelCard6);
        Controls.Add(panelCard5);
        Controls.Add(panelCard4);
        Controls.Add(panelCard3);
        Controls.Add(panelCard2);
        Controls.Add(panelCard1);
        Controls.Add(btnActualizar);
        Controls.Add(lblSubtitulo);
        Controls.Add(lblTitulo);
        Font = new Font("Segoe UI", 9F);
        Name = "FormDashboard";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Panel de Control";
        Load += FormDashboard_Load;
        panelCard1.ResumeLayout(false);
        panelCard1.PerformLayout();
        panelCard2.ResumeLayout(false);
        panelCard2.PerformLayout();
        panelCard3.ResumeLayout(false);
        panelCard3.PerformLayout();
        panelCard4.ResumeLayout(false);
        panelCard4.PerformLayout();
        panelCard5.ResumeLayout(false);
        panelCard5.PerformLayout();
        panelCard6.ResumeLayout(false);
        panelCard6.PerformLayout();
        ((ISupportInitialize)dgvUltimasVentas).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
