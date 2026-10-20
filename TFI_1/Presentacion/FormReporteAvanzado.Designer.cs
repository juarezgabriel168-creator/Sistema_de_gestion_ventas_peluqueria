using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;

partial class FormReporteAvanzado : Form
{
    private IContainer components = null;

    private Label lblTitulo;

    private Label lblDesde;

    private DateTimePicker dtpDesde;

    private Label lblHasta;

    private DateTimePicker dtpHasta;

    private Button btnActualizar;

    private TabControl tabReportes;

    private TabPage tabClientes;

    private TabPage tabUsuarios;

    private TabPage tabServicios;

    private DataGridView dgvClientes;

    private DataGridView dgvUsuarios;

    private DataGridView dgvServicios;

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
        lblDesde = new Label();
        dtpDesde = new DateTimePicker();
        lblHasta = new Label();
        dtpHasta = new DateTimePicker();
        btnActualizar = new Button();
        tabReportes = new TabControl();
        tabClientes = new TabPage();
        tabUsuarios = new TabPage();
        tabServicios = new TabPage();
        dgvClientes = new DataGridView();
        dgvUsuarios = new DataGridView();
        dgvServicios = new DataGridView();
        tabReportes.SuspendLayout();
        tabClientes.SuspendLayout();
        tabUsuarios.SuspendLayout();
        tabServicios.SuspendLayout();
        ((ISupportInitialize)dgvClientes).BeginInit();
        ((ISupportInitialize)dgvUsuarios).BeginInit();
        ((ISupportInitialize)dgvServicios).BeginInit();
        SuspendLayout();
        //
        // lblTitulo
        //
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.Location = new Point(12, 10);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(190, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Reportes avanzados";
        //
        // lblDesde
        //
        lblDesde.AutoSize = true;
        lblDesde.Location = new Point(12, 52);
        lblDesde.Name = "lblDesde";
        lblDesde.Size = new Size(42, 15);
        lblDesde.TabIndex = 1;
        lblDesde.Text = "Desde:";
        //
        // dtpDesde
        //
        dtpDesde.Format = DateTimePickerFormat.Short;
        dtpDesde.Location = new Point(60, 49);
        dtpDesde.Name = "dtpDesde";
        dtpDesde.Size = new Size(120, 23);
        dtpDesde.TabIndex = 2;
        //
        // lblHasta
        //
        lblHasta.AutoSize = true;
        lblHasta.Location = new Point(195, 52);
        lblHasta.Name = "lblHasta";
        lblHasta.Size = new Size(40, 15);
        lblHasta.TabIndex = 3;
        lblHasta.Text = "Hasta:";
        //
        // dtpHasta
        //
        dtpHasta.Format = DateTimePickerFormat.Short;
        dtpHasta.Location = new Point(243, 49);
        dtpHasta.Name = "dtpHasta";
        dtpHasta.Size = new Size(120, 23);
        dtpHasta.TabIndex = 4;
        //
        // btnActualizar
        //
        btnActualizar.Location = new Point(380, 48);
        btnActualizar.Name = "btnActualizar";
        btnActualizar.Size = new Size(110, 27);
        btnActualizar.TabIndex = 5;
        btnActualizar.Text = "Actualizar";
        btnActualizar.UseVisualStyleBackColor = true;
        btnActualizar.Click += btnActualizar_Click;
        //
        // tabReportes
        //
        tabReportes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tabReportes.Controls.Add(tabClientes);
        tabReportes.Controls.Add(tabUsuarios);
        tabReportes.Controls.Add(tabServicios);
        tabReportes.Location = new Point(12, 85);
        tabReportes.Name = "tabReportes";
        tabReportes.SelectedIndex = 0;
        tabReportes.Size = new Size(896, 523);
        tabReportes.TabIndex = 6;
        //
        // tabClientes
        //
        tabClientes.Controls.Add(dgvClientes);
        tabClientes.Location = new Point(4, 24);
        tabClientes.Name = "tabClientes";
        tabClientes.Padding = new Padding(3);
        tabClientes.Size = new Size(888, 495);
        tabClientes.TabIndex = 0;
        tabClientes.Text = "Clientes con más compras";
        tabClientes.UseVisualStyleBackColor = true;
        //
        // tabUsuarios
        //
        tabUsuarios.Controls.Add(dgvUsuarios);
        tabUsuarios.Location = new Point(4, 24);
        tabUsuarios.Name = "tabUsuarios";
        tabUsuarios.Padding = new Padding(3);
        tabUsuarios.Size = new Size(888, 495);
        tabUsuarios.TabIndex = 1;
        tabUsuarios.Text = "Ventas por usuario";
        tabUsuarios.UseVisualStyleBackColor = true;
        //
        // tabServicios
        //
        tabServicios.Controls.Add(dgvServicios);
        tabServicios.Location = new Point(4, 24);
        tabServicios.Name = "tabServicios";
        tabServicios.Padding = new Padding(3);
        tabServicios.Size = new Size(888, 495);
        tabServicios.TabIndex = 2;
        tabServicios.Text = "Servicios más solicitados";
        tabServicios.UseVisualStyleBackColor = true;
        //
        // dgvClientes
        //
        dgvClientes.AllowUserToAddRows = false;
        dgvClientes.AllowUserToDeleteRows = false;
        dgvClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvClientes.BackgroundColor = SystemColors.Window;
        dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvClientes.Dock = DockStyle.Fill;
        dgvClientes.Location = new Point(3, 3);
        dgvClientes.MultiSelect = false;
        dgvClientes.Name = "dgvClientes";
        dgvClientes.ReadOnly = true;
        dgvClientes.RowHeadersVisible = false;
        dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvClientes.Size = new Size(882, 489);
        dgvClientes.TabIndex = 0;
        //
        // dgvUsuarios
        //
        dgvUsuarios.AllowUserToAddRows = false;
        dgvUsuarios.AllowUserToDeleteRows = false;
        dgvUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvUsuarios.BackgroundColor = SystemColors.Window;
        dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvUsuarios.Dock = DockStyle.Fill;
        dgvUsuarios.Location = new Point(3, 3);
        dgvUsuarios.MultiSelect = false;
        dgvUsuarios.Name = "dgvUsuarios";
        dgvUsuarios.ReadOnly = true;
        dgvUsuarios.RowHeadersVisible = false;
        dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvUsuarios.Size = new Size(882, 489);
        dgvUsuarios.TabIndex = 0;
        //
        // dgvServicios
        //
        dgvServicios.AllowUserToAddRows = false;
        dgvServicios.AllowUserToDeleteRows = false;
        dgvServicios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvServicios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvServicios.BackgroundColor = SystemColors.Window;
        dgvServicios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvServicios.Dock = DockStyle.Fill;
        dgvServicios.Location = new Point(3, 3);
        dgvServicios.MultiSelect = false;
        dgvServicios.Name = "dgvServicios";
        dgvServicios.ReadOnly = true;
        dgvServicios.RowHeadersVisible = false;
        dgvServicios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvServicios.Size = new Size(882, 489);
        dgvServicios.TabIndex = 0;
        //
        // FormReporteAvanzado
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ActiveCaption;
        ClientSize = new Size(920, 620);
        Controls.Add(tabReportes);
        Controls.Add(btnActualizar);
        Controls.Add(dtpHasta);
        Controls.Add(lblHasta);
        Controls.Add(dtpDesde);
        Controls.Add(lblDesde);
        Controls.Add(lblTitulo);
        Font = new Font("Segoe UI", 9F);
        Name = "FormReporteAvanzado";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Reportes avanzados";
        Load += FormReporteAvanzado_Load;
        tabReportes.ResumeLayout(false);
        tabClientes.ResumeLayout(false);
        tabUsuarios.ResumeLayout(false);
        tabServicios.ResumeLayout(false);
        ((ISupportInitialize)dgvClientes).EndInit();
        ((ISupportInitialize)dgvUsuarios).EndInit();
        ((ISupportInitialize)dgvServicios).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
