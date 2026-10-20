using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TFI_1.Negocio;
using TFI_1.Properties;

namespace TFI_1.Presentacion;
partial class FormPrincipal : Form
{
	private IContainer components = null;

	private MenuStrip menuPrincipal;

	private ToolStripMenuItem menuInicio;

	private ToolStripMenuItem mnuInicioDashboard;

	private ToolStripMenuItem menuVentas;

	private ToolStripMenuItem mnuVentasNueva;

	private ToolStripMenuItem mnuVentasHistorial;

	private ToolStripMenuItem mnuVentasCaja;

	private ToolStripMenuItem menuProductos;

	private ToolStripMenuItem mnuProductosItems;

	private ToolStripMenuItem mnuProductosCategorias;

	private ToolStripMenuItem menuClientes;

	private ToolStripMenuItem mnuClientesListado;

	private ToolStripMenuItem mnuClientesCuentaCorriente;

	private ToolStripMenuItem menuTurnos;

	private ToolStripMenuItem mnuTurnosAtenciones;

	private ToolStripMenuItem menuAdministracion;

	private ToolStripMenuItem mnuAdminUsuarios;

	private ToolStripMenuItem mnuAdminEmpleados;

	private ToolStripMenuItem mnuAdminTarjetas;

	private ToolStripMenuItem mnuAdminGastos;

	private ToolStripMenuItem mnuAdminProveedores;

	private ToolStripMenuItem mnuAdminIngresoMercaderia;

	private ToolStripMenuItem mnuAdminHistorialMercaderia;

	private ToolStripMenuItem mnuAdminAuditoria;

	private ToolStripMenuItem mnuAdminBackup;

	private ToolStripMenuItem menuReportes;

	private ToolStripMenuItem mnuReportesDiario;

	private ToolStripMenuItem mnuReportesMensual;

	private ToolStripMenuItem mnuCerrarSesion;

	private ToolStripMenuItem mnuSalir;

	private StatusStrip statusPrincipal;

	private ToolStripStatusLabel lblUsuario;

	private PictureBox pibMenu;

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
		this.menuPrincipal = new System.Windows.Forms.MenuStrip();
		this.menuInicio = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuInicioDashboard = new System.Windows.Forms.ToolStripMenuItem();
		this.menuVentas = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuVentasNueva = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuVentasHistorial = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuVentasCaja = new System.Windows.Forms.ToolStripMenuItem();
		this.menuProductos = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuProductosItems = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuProductosCategorias = new System.Windows.Forms.ToolStripMenuItem();
		this.menuClientes = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuClientesListado = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuClientesCuentaCorriente = new System.Windows.Forms.ToolStripMenuItem();
		this.menuTurnos = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuTurnosAtenciones = new System.Windows.Forms.ToolStripMenuItem();
		this.menuAdministracion = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuAdminUsuarios = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuAdminEmpleados = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuAdminTarjetas = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuAdminGastos = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuAdminProveedores = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuAdminIngresoMercaderia = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuAdminHistorialMercaderia = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuAdminAuditoria = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuAdminBackup = new System.Windows.Forms.ToolStripMenuItem();
		this.menuReportes = new System.Windows.Forms.ToolStripMenuItem();





























































































































































































































































































































































































































































































		this.mnuReportesDiario = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuReportesMensual = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuCerrarSesion = new System.Windows.Forms.ToolStripMenuItem();
		this.mnuSalir = new System.Windows.Forms.ToolStripMenuItem();
		this.statusPrincipal = new System.Windows.Forms.StatusStrip();
		this.lblUsuario = new System.Windows.Forms.ToolStripStatusLabel();
		this.pibMenu = new System.Windows.Forms.PictureBox();
		this.menuPrincipal.SuspendLayout();
		this.statusPrincipal.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pibMenu).BeginInit();
		this.SuspendLayout();
		this.menuPrincipal.Items.AddRangeSafe(this.menuInicio, this.menuVentas, this.menuProductos, this.menuClientes, this.menuTurnos, this.menuAdministracion, this.menuReportes, this.mnuCerrarSesion, this.mnuSalir);
		this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
		this.menuPrincipal.Name = "menuPrincipal";
		this.menuPrincipal.Size = new System.Drawing.Size(1000, 24);
		this.menuPrincipal.TabIndex = 0;
		this.menuInicio.DropDownItems.AddRangeSafe(this.mnuInicioDashboard);
		this.menuInicio.Name = "menuInicio";
		this.menuInicio.Size = new System.Drawing.Size(48, 20);
		this.menuInicio.Text = "Inicio";
		this.mnuInicioDashboard.Name = "mnuInicioDashboard";
		this.mnuInicioDashboard.Size = new System.Drawing.Size(131, 22);
		this.mnuInicioDashboard.Text = "Dashboard";
		this.mnuInicioDashboard.Click += new System.EventHandler(this.mnuInicioDashboard_Click);
		this.menuVentas.DropDownItems.AddRangeSafe(this.mnuVentasNueva, this.mnuVentasHistorial, this.mnuVentasCaja);
		this.menuVentas.Name = "menuVentas";
		this.menuVentas.Size = new System.Drawing.Size(53, 20);
		this.menuVentas.Text = "Ventas";
		this.mnuVentasNueva.Name = "mnuVentasNueva";
		this.mnuVentasNueva.Size = new System.Drawing.Size(171, 22);
		this.mnuVentasNueva.Text = "Nueva venta";
		this.mnuVentasNueva.Click += new System.EventHandler(this.mnuVentasNueva_Click);
		this.mnuVentasHistorial.Name = "mnuVentasHistorial";
		this.mnuVentasHistorial.Size = new System.Drawing.Size(171, 22);
		this.mnuVentasHistorial.Text = "Historial de ventas";
		this.mnuVentasHistorial.Click += new System.EventHandler(this.mnuVentasHistorial_Click);
		//
		// mnuVentasCaja
		//
		this.mnuVentasCaja.Name = "mnuVentasCaja";
		this.mnuVentasCaja.Size = new System.Drawing.Size(171, 22);
		this.mnuVentasCaja.Text = "Caja";
		this.mnuVentasCaja.Click += new System.EventHandler(this.mnuVentasCaja_Click);
		this.menuProductos.DropDownItems.AddRangeSafe(this.mnuProductosItems, this.mnuProductosCategorias);
		this.menuProductos.Name = "menuProductos";
		this.menuProductos.Size = new System.Drawing.Size(73, 20);
		this.menuProductos.Text = "Productos";
		this.mnuProductosItems.Name = "mnuProductosItems";
		this.mnuProductosItems.Size = new System.Drawing.Size(185, 22);
		this.mnuProductosItems.Text = "Productos y servicios";
		this.mnuProductosItems.Click += new System.EventHandler(this.mnuProductosItems_Click);
		this.mnuProductosCategorias.Name = "mnuProductosCategorias";
		this.mnuProductosCategorias.Size = new System.Drawing.Size(185, 22);
		this.mnuProductosCategorias.Text = "Categorías";
		this.mnuProductosCategorias.Click += new System.EventHandler(this.mnuProductosCategorias_Click);
		this.menuClientes.DropDownItems.AddRangeSafe(this.mnuClientesListado, this.mnuClientesCuentaCorriente);
		this.menuClientes.Name = "menuClientes";
		this.menuClientes.Size = new System.Drawing.Size(61, 20);
		this.menuClientes.Text = "Clientes";
		this.mnuClientesListado.Name = "mnuClientesListado";
		this.mnuClientesListado.Size = new System.Drawing.Size(116, 22);
		this.mnuClientesListado.Text = "Clientes";
		this.mnuClientesListado.Click += new System.EventHandler(this.mnuClientesListado_Click);
		this.mnuClientesCuentaCorriente.Name = "mnuClientesCuentaCorriente";
		this.mnuClientesCuentaCorriente.Size = new System.Drawing.Size(171, 22);
		this.mnuClientesCuentaCorriente.Text = "Cuenta corriente";
		this.mnuClientesCuentaCorriente.Click += new System.EventHandler(this.mnuClientesCuentaCorriente_Click);
		this.menuTurnos.DropDownItems.AddRangeSafe(this.mnuTurnosAtenciones);
		this.menuTurnos.Name = "menuTurnos";
		this.menuTurnos.Size = new System.Drawing.Size(56, 20);
		this.menuTurnos.Text = "Turnos";
		this.mnuTurnosAtenciones.Name = "mnuTurnosAtenciones";
		this.mnuTurnosAtenciones.Size = new System.Drawing.Size(133, 22);
		this.mnuTurnosAtenciones.Text = "Atenciones";
		this.mnuTurnosAtenciones.Click += new System.EventHandler(this.mnuTurnosAtenciones_Click);
		this.menuAdministracion.DropDownItems.AddRangeSafe(this.mnuAdminUsuarios, this.mnuAdminEmpleados, this.mnuAdminTarjetas, this.mnuAdminGastos, this.mnuAdminProveedores, this.mnuAdminIngresoMercaderia, this.mnuAdminHistorialMercaderia, this.mnuAdminAuditoria, this.mnuAdminBackup);
		this.menuAdministracion.Name = "menuAdministracion";
		this.menuAdministracion.Size = new System.Drawing.Size(100, 20);
		this.menuAdministracion.Text = "Administración";
		this.mnuAdminUsuarios.Name = "mnuAdminUsuarios";
		this.mnuAdminUsuarios.Size = new System.Drawing.Size(132, 22);
		this.mnuAdminUsuarios.Text = "Usuarios";
		this.mnuAdminUsuarios.Click += new System.EventHandler(this.mnuAdminUsuarios_Click);
		this.mnuAdminEmpleados.Name = "mnuAdminEmpleados";
		this.mnuAdminEmpleados.Size = new System.Drawing.Size(132, 22);
		this.mnuAdminEmpleados.Text = "Empleados";
		this.mnuAdminEmpleados.Click += new System.EventHandler(this.mnuAdminEmpleados_Click);
		//
		// mnuAdminTarjetas
		//
		this.mnuAdminTarjetas.Name = "mnuAdminTarjetas";
		this.mnuAdminTarjetas.Size = new System.Drawing.Size(132, 22);
		this.mnuAdminTarjetas.Text = "Tarjetas de Crédito";
		this.mnuAdminTarjetas.Click += new System.EventHandler(this.mnuAdminTarjetas_Click);
		//
		// mnuAdminGastos
		//
		this.mnuAdminGastos.Name = "mnuAdminGastos";
		this.mnuAdminGastos.Size = new System.Drawing.Size(132, 22);
		this.mnuAdminGastos.Text = "Gastos";
		this.mnuAdminGastos.Click += new System.EventHandler(this.mnuAdminGastos_Click);
		//
		// mnuAdminProveedores
		//
		this.mnuAdminProveedores.Name = "mnuAdminProveedores";
		this.mnuAdminProveedores.Size = new System.Drawing.Size(132, 22);
		this.mnuAdminProveedores.Text = "Proveedores";
		this.mnuAdminProveedores.Click += new System.EventHandler(this.mnuAdminProveedores_Click);
		//
		// mnuAdminIngresoMercaderia
		//
		this.mnuAdminIngresoMercaderia.Name = "mnuAdminIngresoMercaderia";
		this.mnuAdminIngresoMercaderia.Size = new System.Drawing.Size(132, 22);
		this.mnuAdminIngresoMercaderia.Text = "Ingreso de Mercadería";
		this.mnuAdminIngresoMercaderia.Click += new System.EventHandler(this.mnuAdminIngresoMercaderia_Click);
		//
		// mnuAdminHistorialMercaderia
		//
		this.mnuAdminHistorialMercaderia.Name = "mnuAdminHistorialMercaderia";
		this.mnuAdminHistorialMercaderia.Size = new System.Drawing.Size(132, 22);
		this.mnuAdminHistorialMercaderia.Text = "Historial de Mercadería";
		this.mnuAdminHistorialMercaderia.Click += new System.EventHandler(this.mnuAdminHistorialMercaderia_Click);
		//
		// mnuAdminAuditoria
		//
		this.mnuAdminAuditoria.Name = "mnuAdminAuditoria";
		this.mnuAdminAuditoria.Size = new System.Drawing.Size(132, 22);
		this.mnuAdminAuditoria.Text = "Auditoría";
		this.mnuAdminAuditoria.Click += new System.EventHandler(this.mnuAdminAuditoria_Click);
		//
		// mnuAdminBackup
		//
		this.mnuAdminBackup.Name = "mnuAdminBackup";
		this.mnuAdminBackup.Size = new System.Drawing.Size(132, 22);
		this.mnuAdminBackup.Text = "Copia de seguridad";
		this.mnuAdminBackup.Click += new System.EventHandler(this.mnuAdminBackup_Click);
		this.menuReportes.DropDownItems.AddRangeSafe(this.mnuReportesDiario, this.mnuReportesMensual);
		this.menuReportes.Name = "menuReportes";
		this.menuReportes.Size = new System.Drawing.Size(65, 20);
		this.menuReportes.Text = "Reportes";
		this.mnuReportesDiario.Name = "mnuReportesDiario";
		this.mnuReportesDiario.Size = new System.Drawing.Size(163, 22);
		this.mnuReportesDiario.Text = "Reporte diario";
		this.mnuReportesDiario.Click += new System.EventHandler(this.mnuReportesDiario_Click);
		this.mnuReportesMensual.Name = "mnuReportesMensual";
		this.mnuReportesMensual.Size = new System.Drawing.Size(163, 22);
		this.mnuReportesMensual.Text = "Reporte mensual";
		this.mnuReportesMensual.Click += new System.EventHandler(this.mnuReportesMensual_Click);
		this.mnuCerrarSesion.Name = "mnuCerrarSesion";
		this.mnuCerrarSesion.Size = new System.Drawing.Size(95, 20);
		this.mnuCerrarSesion.Text = "Cerrar sesión";
		this.mnuCerrarSesion.Click += new System.EventHandler(this.mnuCerrarSesion_Click);
		this.mnuSalir.Name = "mnuSalir";
		this.mnuSalir.Size = new System.Drawing.Size(41, 20);
		this.mnuSalir.Text = "Salir";
		this.mnuSalir.Click += new System.EventHandler(this.mnuSalir_Click);
		this.statusPrincipal.Items.AddRangeSafe(this.lblUsuario);
		this.statusPrincipal.Location = new System.Drawing.Point(0, 566);
		this.statusPrincipal.Name = "statusPrincipal";
		this.statusPrincipal.Size = new System.Drawing.Size(1000, 22);
		this.statusPrincipal.TabIndex = 1;
		this.lblUsuario.Name = "lblUsuario";
		this.lblUsuario.Size = new System.Drawing.Size(53, 17);
		this.lblUsuario.Text = "Usuario: ";
		this.pibMenu.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.pibMenu.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pibMenu.Image = TFI_1.Properties.Resources.Code_Generated_Image;
		this.pibMenu.Location = new System.Drawing.Point(0, 24);
		this.pibMenu.Name = "pibMenu";
		this.pibMenu.Size = new System.Drawing.Size(1000, 542);
		this.pibMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pibMenu.TabIndex = 3;
		this.pibMenu.TabStop = false;
		this.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(1000, 588);
		this.Controls.Add(this.pibMenu);
		this.Controls.Add(this.statusPrincipal);
		this.Controls.Add(this.menuPrincipal);
		this.IsMdiContainer = true;
		this.MainMenuStrip = this.menuPrincipal;
		this.Name = "FormPrincipal";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Peluquería - Sistema de Gestión";
		this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		this.Load += new System.EventHandler(this.FormPrincipal_Load);
		this.menuPrincipal.ResumeLayout(false);
		this.menuPrincipal.PerformLayout();
		this.statusPrincipal.ResumeLayout(false);
		this.statusPrincipal.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pibMenu).EndInit();
		this.ResumeLayout(false);
		this.PerformLayout();
	}
}
