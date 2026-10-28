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

	private ToolStripMenuItem mnuReportesAvanzado;

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
        menuPrincipal = new MenuStrip();
        menuInicio = new ToolStripMenuItem();
        mnuInicioDashboard = new ToolStripMenuItem();
        menuVentas = new ToolStripMenuItem();
        mnuVentasNueva = new ToolStripMenuItem();
        mnuVentasHistorial = new ToolStripMenuItem();
        mnuVentasCaja = new ToolStripMenuItem();
        menuProductos = new ToolStripMenuItem();
        mnuProductosItems = new ToolStripMenuItem();
        mnuProductosCategorias = new ToolStripMenuItem();
        menuClientes = new ToolStripMenuItem();
        mnuClientesListado = new ToolStripMenuItem();
        mnuClientesCuentaCorriente = new ToolStripMenuItem();
        menuTurnos = new ToolStripMenuItem();
        mnuTurnosAtenciones = new ToolStripMenuItem();
        menuAdministracion = new ToolStripMenuItem();
        mnuAdminUsuarios = new ToolStripMenuItem();
        mnuAdminEmpleados = new ToolStripMenuItem();
        mnuAdminTarjetas = new ToolStripMenuItem();
        mnuAdminGastos = new ToolStripMenuItem();
        mnuAdminProveedores = new ToolStripMenuItem();
        mnuAdminIngresoMercaderia = new ToolStripMenuItem();
        mnuAdminHistorialMercaderia = new ToolStripMenuItem();
        mnuAdminAuditoria = new ToolStripMenuItem();
        mnuAdminBackup = new ToolStripMenuItem();
        menuReportes = new ToolStripMenuItem();
        mnuReportesDiario = new ToolStripMenuItem();
        mnuReportesMensual = new ToolStripMenuItem();
        mnuReportesAvanzado = new ToolStripMenuItem();
        mnuCerrarSesion = new ToolStripMenuItem();
        mnuSalir = new ToolStripMenuItem();
        statusPrincipal = new StatusStrip();
        lblUsuario = new ToolStripStatusLabel();
        pibMenu = new PictureBox();
        menuPrincipal.SuspendLayout();
        statusPrincipal.SuspendLayout();
        ((ISupportInitialize)pibMenu).BeginInit();
        SuspendLayout();
        // 
        // menuPrincipal
        // 
        menuPrincipal.Items.AddRange(new ToolStripItem[] { menuInicio, menuVentas, menuClientes, menuTurnos, mnuAdminGastos, menuProductos, menuReportes, menuAdministracion, mnuCerrarSesion, mnuSalir });
        menuPrincipal.Location = new Point(0, 0);
        menuPrincipal.Name = "menuPrincipal";
        menuPrincipal.Size = new Size(1000, 24);
        menuPrincipal.TabIndex = 0;
        // 
        // menuInicio
        // 
        menuInicio.DropDownItems.AddRange(new ToolStripItem[] { mnuInicioDashboard });
        menuInicio.Name = "menuInicio";
        menuInicio.Size = new Size(48, 20);
        menuInicio.Text = "Inicio";
        // 
        // mnuInicioDashboard
        // 
        mnuInicioDashboard.Name = "mnuInicioDashboard";
        mnuInicioDashboard.Size = new Size(131, 22);
        mnuInicioDashboard.Text = "Dashboard";
        mnuInicioDashboard.Click += mnuInicioDashboard_Click;
        // 
        // menuVentas
        // 
        menuVentas.DropDownItems.AddRange(new ToolStripItem[] { mnuVentasNueva, mnuVentasCaja, mnuVentasHistorial });
        menuVentas.Name = "menuVentas";
        menuVentas.Size = new Size(53, 20);
        menuVentas.Text = "Ventas";
        // 
        // mnuVentasNueva
        // 
        mnuVentasNueva.Name = "mnuVentasNueva";
        mnuVentasNueva.Size = new Size(171, 22);
        mnuVentasNueva.Text = "Nueva venta";
        mnuVentasNueva.Click += mnuVentasNueva_Click;
        // 
        // mnuVentasHistorial
        // 
        mnuVentasHistorial.Name = "mnuVentasHistorial";
        mnuVentasHistorial.Size = new Size(171, 22);
        mnuVentasHistorial.Text = "Historial de ventas";
        mnuVentasHistorial.Click += mnuVentasHistorial_Click;
        // 
        // mnuVentasCaja
        // 
        mnuVentasCaja.Name = "mnuVentasCaja";
        mnuVentasCaja.Size = new Size(171, 22);
        mnuVentasCaja.Text = "Caja";
        mnuVentasCaja.Click += mnuVentasCaja_Click;
        // 
        // menuProductos
        // 
        menuProductos.DropDownItems.AddRange(new ToolStripItem[] { mnuProductosItems, mnuProductosCategorias, mnuAdminProveedores, mnuAdminIngresoMercaderia, mnuAdminHistorialMercaderia });
        menuProductos.Name = "menuProductos";
        menuProductos.Size = new Size(73, 20);
        menuProductos.Text = "Productos";
        // 
        // mnuProductosItems
        // 
        mnuProductosItems.Name = "mnuProductosItems";
        mnuProductosItems.Size = new Size(185, 22);
        mnuProductosItems.Text = "Productos y servicios";
        mnuProductosItems.Click += mnuProductosItems_Click;
        // 
        // mnuProductosCategorias
        // 
        mnuProductosCategorias.Name = "mnuProductosCategorias";
        mnuProductosCategorias.Size = new Size(185, 22);
        mnuProductosCategorias.Text = "Categorías";
        mnuProductosCategorias.Click += mnuProductosCategorias_Click;
        // 
        // menuClientes
        // 
        menuClientes.DropDownItems.AddRange(new ToolStripItem[] { mnuClientesListado, mnuClientesCuentaCorriente });
        menuClientes.Name = "menuClientes";
        menuClientes.Size = new Size(61, 20);
        menuClientes.Text = "Clientes";
        // 
        // mnuClientesListado
        // 
        mnuClientesListado.Name = "mnuClientesListado";
        mnuClientesListado.Size = new Size(162, 22);
        mnuClientesListado.Text = "Clientes";
        mnuClientesListado.Click += mnuClientesListado_Click;
        // 
        // mnuClientesCuentaCorriente
        // 
        mnuClientesCuentaCorriente.Name = "mnuClientesCuentaCorriente";
        mnuClientesCuentaCorriente.Size = new Size(162, 22);
        mnuClientesCuentaCorriente.Text = "Cuenta corriente";
        mnuClientesCuentaCorriente.Click += mnuClientesCuentaCorriente_Click;
        // 
        // menuTurnos
        // 
        menuTurnos.DropDownItems.AddRange(new ToolStripItem[] { mnuTurnosAtenciones });
        menuTurnos.Name = "menuTurnos";
        menuTurnos.Size = new Size(56, 20);
        menuTurnos.Text = "Turnos";
        // 
        // mnuTurnosAtenciones
        // 
        mnuTurnosAtenciones.Name = "mnuTurnosAtenciones";
        mnuTurnosAtenciones.Size = new Size(133, 22);
        mnuTurnosAtenciones.Text = "Atenciones";
        mnuTurnosAtenciones.Click += mnuTurnosAtenciones_Click;
        // 
        // menuAdministracion
        // 
        menuAdministracion.DropDownItems.AddRange(new ToolStripItem[] { mnuAdminEmpleados, mnuAdminTarjetas, mnuAdminUsuarios, mnuAdminAuditoria, mnuAdminBackup });
        menuAdministracion.Name = "menuAdministracion";
        menuAdministracion.Size = new Size(100, 20);
        menuAdministracion.Text = "Administración";
        // 
        // mnuAdminUsuarios
        // 
        mnuAdminUsuarios.Name = "mnuAdminUsuarios";
        mnuAdminUsuarios.Size = new Size(196, 22);
        mnuAdminUsuarios.Text = "Usuarios";
        mnuAdminUsuarios.Click += mnuAdminUsuarios_Click;
        // 
        // mnuAdminEmpleados
        // 
        mnuAdminEmpleados.Name = "mnuAdminEmpleados";
        mnuAdminEmpleados.Size = new Size(196, 22);
        mnuAdminEmpleados.Text = "Empleados";
        mnuAdminEmpleados.Click += mnuAdminEmpleados_Click;
        // 
        // mnuAdminTarjetas
        // 
        mnuAdminTarjetas.Name = "mnuAdminTarjetas";
        mnuAdminTarjetas.Size = new Size(196, 22);
        mnuAdminTarjetas.Text = "Tarjetas de Crédito";
        mnuAdminTarjetas.Click += mnuAdminTarjetas_Click;
        // 
        // mnuAdminGastos
        // 
        mnuAdminGastos.Name = "mnuAdminGastos";
        mnuAdminGastos.Size = new Size(196, 22);
        mnuAdminGastos.Text = "Gastos";
        mnuAdminGastos.Click += mnuAdminGastos_Click;
        // 
        // mnuAdminProveedores
        // 
        mnuAdminProveedores.Name = "mnuAdminProveedores";
        mnuAdminProveedores.Size = new Size(196, 22);
        mnuAdminProveedores.Text = "Proveedores";
        mnuAdminProveedores.Click += mnuAdminProveedores_Click;
        // 
        // mnuAdminIngresoMercaderia
        // 
        mnuAdminIngresoMercaderia.Name = "mnuAdminIngresoMercaderia";
        mnuAdminIngresoMercaderia.Size = new Size(196, 22);
        mnuAdminIngresoMercaderia.Text = "Ingreso de Mercadería";
        mnuAdminIngresoMercaderia.Click += mnuAdminIngresoMercaderia_Click;
        // 
        // mnuAdminHistorialMercaderia
        // 
        mnuAdminHistorialMercaderia.Name = "mnuAdminHistorialMercaderia";
        mnuAdminHistorialMercaderia.Size = new Size(196, 22);
        mnuAdminHistorialMercaderia.Text = "Historial de Mercadería";
        mnuAdminHistorialMercaderia.Click += mnuAdminHistorialMercaderia_Click;
        // 
        // mnuAdminAuditoria
        // 
        mnuAdminAuditoria.Name = "mnuAdminAuditoria";
        mnuAdminAuditoria.Size = new Size(196, 22);
        mnuAdminAuditoria.Text = "Auditoría";
        mnuAdminAuditoria.Click += mnuAdminAuditoria_Click;
        // 
        // mnuAdminBackup
        // 
        mnuAdminBackup.Name = "mnuAdminBackup";
        mnuAdminBackup.Size = new Size(196, 22);
        mnuAdminBackup.Text = "Copia de seguridad";
        mnuAdminBackup.Click += mnuAdminBackup_Click;
        // 
        // menuReportes
        // 
        menuReportes.DropDownItems.AddRange(new ToolStripItem[] { mnuReportesDiario, mnuReportesMensual, mnuReportesAvanzado });
        menuReportes.Name = "menuReportes";
        menuReportes.Size = new Size(65, 20);
        menuReportes.Text = "Reportes";
        // 
        // mnuReportesDiario
        // 
        mnuReportesDiario.Name = "mnuReportesDiario";
        mnuReportesDiario.Size = new Size(178, 22);
        mnuReportesDiario.Text = "Reporte diario";
        mnuReportesDiario.Click += mnuReportesDiario_Click;
        // 
        // mnuReportesMensual
        // 
        mnuReportesMensual.Name = "mnuReportesMensual";
        mnuReportesMensual.Size = new Size(178, 22);
        mnuReportesMensual.Text = "Reporte mensual";
        mnuReportesMensual.Click += mnuReportesMensual_Click;
        // 
        // mnuReportesAvanzado
        // 
        mnuReportesAvanzado.Name = "mnuReportesAvanzado";
        mnuReportesAvanzado.Size = new Size(178, 22);
        mnuReportesAvanzado.Text = "Reportes avanzados";
        mnuReportesAvanzado.Click += mnuReportesAvanzado_Click;
        // 
        // mnuCerrarSesion
        // 
        mnuCerrarSesion.Name = "mnuCerrarSesion";
        mnuCerrarSesion.Size = new Size(87, 20);
        mnuCerrarSesion.Text = "Cerrar sesión";
        mnuCerrarSesion.Click += mnuCerrarSesion_Click;
        // 
        // mnuSalir
        // 
        mnuSalir.Name = "mnuSalir";
        mnuSalir.Size = new Size(41, 20);
        mnuSalir.Text = "Salir";
        mnuSalir.Click += mnuSalir_Click;
        // 
        // statusPrincipal
        // 
        statusPrincipal.Items.AddRange(new ToolStripItem[] { lblUsuario });
        statusPrincipal.Location = new Point(0, 566);
        statusPrincipal.Name = "statusPrincipal";
        statusPrincipal.Size = new Size(1000, 22);
        statusPrincipal.TabIndex = 1;
        // 
        // lblUsuario
        // 
        lblUsuario.Name = "lblUsuario";
        lblUsuario.Size = new Size(53, 17);
        lblUsuario.Text = "Usuario: ";
        // 
        // pibMenu
        // 
        pibMenu.BackColor = SystemColors.ActiveCaption;
        pibMenu.Dock = DockStyle.Fill;
        pibMenu.Image = Resources.Code_Generated_Image;
        pibMenu.Location = new Point(0, 24);
        pibMenu.Name = "pibMenu";
        pibMenu.Size = new Size(1000, 542);
        pibMenu.SizeMode = PictureBoxSizeMode.StretchImage;
        pibMenu.TabIndex = 3;
        pibMenu.TabStop = false;
        // 
        // FormPrincipal
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1000, 588);
        Controls.Add(pibMenu);
        Controls.Add(statusPrincipal);
        Controls.Add(menuPrincipal);
        IsMdiContainer = true;
        MainMenuStrip = menuPrincipal;
        Name = "FormPrincipal";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Peluquería - Sistema de Gestión";
        WindowState = FormWindowState.Maximized;
        Load += FormPrincipal_Load;
        menuPrincipal.ResumeLayout(false);
        menuPrincipal.PerformLayout();
        statusPrincipal.ResumeLayout(false);
        statusPrincipal.PerformLayout();
        ((ISupportInitialize)pibMenu).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
