using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TFI_1.Negocio;
using TFI_1.Properties;

namespace TFI_1.Presentacion;

public partial class FormPrincipal : Form
{
	/// <summary>
	/// Indica que el formulario se cerró porque el usuario pidió "Cerrar sesión",
	/// y no porque quiso salir de la aplicación.
	/// </summary>
	public bool CerrarSesionPendiente { get; private set; }

	public FormPrincipal()
	{
		InitializeComponent();
		AplicarImagenDeFondo();
	}

	private void FormPrincipal_Load(object sender, EventArgs e)
	{
		Text = Configuracion.NombreNegocio + " - Sistema de Gestión";

		if (Sesion.UsuarioActual == null)
		{
			return;
		}

		if (lblUsuario != null)
		{
			lblUsuario.Text = "Usuario: " + Sesion.NombreUsuario + " (" + Sesion.RolNombre + ")";
		}
		if (menuAdministracion != null)
		{
			menuAdministracion.Visible = Sesion.EsAdministrador;
		}
		if (menuReportes != null)
		{
			menuReportes.Visible = Sesion.PuedeVerReportes;
		}
		if (mnuProductosCategorias != null)
		{
			mnuProductosCategorias.Visible = Sesion.EsAdministrador;
		}
		if (mnuAdminGastos != null)
		{
			mnuAdminGastos.Visible = Sesion.EsAdministrador;
		}
		if (mnuAdminProveedores != null)
		{
			mnuAdminProveedores.Visible = Sesion.EsAdministrador;
		}
		if (mnuAdminIngresoMercaderia != null)
		{
			mnuAdminIngresoMercaderia.Visible = Sesion.EsAdministrador;
		}
		if (mnuAdminHistorialMercaderia != null)
		{
			mnuAdminHistorialMercaderia.Visible = Sesion.EsAdministrador;
		}
	}

	private void AplicarImagenDeFondo()
	{
		if (pibMenu == null)
		{
			return;
		}

		foreach (Control control in Controls)
		{
			if (control is MdiClient cliente)
			{
				cliente.BackColor = pibMenu.BackColor;
				cliente.Paint += (object? s, PaintEventArgs e) =>
				{
					if (pibMenu.Image != null)
					{
						e.Graphics.DrawImage(pibMenu.Image, cliente.ClientRectangle);
					}
				};
			}
		}
		pibMenu.Visible = false;
	}

	private void AbrirFormulario(Form formulario)
	{
		Form? form = MdiChildren.FirstOrDefault((Form f) => f.GetType() == formulario.GetType());
		if (form != null)
		{
			if (form.WindowState == FormWindowState.Minimized)
			{
				form.WindowState = FormWindowState.Normal;
			}
			form.Activate();
			formulario.Dispose();
		}
		else
		{
			formulario.MdiParent = this;
			formulario.Show();
		}
	}

	private void mnuInicioDashboard_Click(object sender, EventArgs e)
	{
		AbrirFormulario(new FormDashboard());
	}

	private void mnuVentasNueva_Click(object sender, EventArgs e)
	{
		AbrirFormulario(new FormNuevaVenta());
	}

	private void mnuVentasHistorial_Click(object sender, EventArgs e)
	{
		AbrirFormulario(new FormHistorialVentas());
	}

	private void mnuVentasCaja_Click(object sender, EventArgs e)
	{
		AbrirFormulario(new FormCaja());
	}

	private void mnuProductosItems_Click(object sender, EventArgs e)
	{
		AbrirFormulario(new FormItems());
	}

	private void mnuProductosCategorias_Click(object sender, EventArgs e)
	{
		if (ExigirAdministrador())
		{
			AbrirFormulario(new FormCategorias());
		}
	}

	private void mnuClientesListado_Click(object sender, EventArgs e)
	{
		AbrirFormulario(new FormClientes());
	}

	private void mnuClientesCuentaCorriente_Click(object sender, EventArgs e)
	{
		AbrirFormulario(new FormCuentaCorriente());
	}

	private void mnuTurnosAtenciones_Click(object sender, EventArgs e)
	{
		AbrirFormulario(new FormAtenciones());
	}

	private void mnuAdminUsuarios_Click(object sender, EventArgs e)
	{
		if (ExigirAdministrador())
		{
			AbrirFormulario(new FormUsuarios());
		}
	}

	private void mnuAdminEmpleados_Click(object sender, EventArgs e)
	{
		if (ExigirAdministrador())
		{
			AbrirFormulario(new FormEmpleados());
		}
	}

	private void mnuAdminTarjetas_Click(object sender, EventArgs e)
	{
		if (ExigirAdministrador())
		{
			AbrirFormulario(new FormTarjetaDeCredito());
		}
	}

	private void mnuAdminGastos_Click(object sender, EventArgs e)
	{
		if (ExigirAdministrador())
		{
			AbrirFormulario(new FormGastos());
		}
	}

	private void mnuAdminIngresoMercaderia_Click(object sender, EventArgs e)
	{
		if (ExigirAdministrador())
		{
			AbrirFormulario(new FormIngresoMercaderia());
		}
	}

	private void mnuAdminHistorialMercaderia_Click(object sender, EventArgs e)
	{
		if (ExigirAdministrador())
		{
			AbrirFormulario(new FormHistorialMercaderia());
		}
	}

	private void mnuAdminProveedores_Click(object sender, EventArgs e)
	{
		if (ExigirAdministrador())
		{
			AbrirFormulario(new FormProveedores());
		}
	}

	private void mnuAdminAuditoria_Click(object sender, EventArgs e)
	{
		if (ExigirAdministrador())
		{
			AbrirFormulario(new FormAuditoria());
		}
	}

	private void mnuAdminBackup_Click(object sender, EventArgs e)
	{
		if (ExigirAdministrador())
		{
			AbrirFormulario(new FormBackup());
		}
	}

	private void mnuReportesDiario_Click(object sender, EventArgs e)
	{
		AbrirFormulario(new FormReporteDiario());
	}

	private void mnuReportesMensual_Click(object sender, EventArgs e)
	{
		AbrirFormulario(new FormReporteMensual());
	}

	private void mnuReportesAvanzado_Click(object sender, EventArgs e)
	{
		AbrirFormulario(new FormReporteAvanzado());
	}

	private void mnuCerrarSesion_Click(object sender, EventArgs e)
	{
		DialogResult respuesta = MessageBox.Show(
			"¿Desea cerrar la sesión actual?",
			"Cerrar sesión",
			MessageBoxButtons.YesNo,
			MessageBoxIcon.Question);

		if (respuesta != DialogResult.Yes)
		{
			return;
		}

		// Se marca para que el login sepa que debe volver a mostrarse
		// en lugar de cerrar la aplicación.
		CerrarSesionPendiente = true;
		Close();
	}

	private void mnuSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private static bool ExigirAdministrador()
	{
		if (Sesion.EsAdministrador)
		{
			return true;
		}
		MessageBox.Show("No tiene permisos para acceder a esta sección.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		return false;
	}
}
