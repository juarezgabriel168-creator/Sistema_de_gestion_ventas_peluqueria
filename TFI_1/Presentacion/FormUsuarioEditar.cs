using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
public partial class FormUsuarioEditar : Form
{
	private readonly UsuarioNegocio _negocio = new UsuarioNegocio();
	private readonly Usuario? _usuario;
	private readonly bool _esNuevo;

	public FormUsuarioEditar() : this(null)
	{
	}

	public FormUsuarioEditar(Usuario? usuario)
	{
		InitializeComponent();
		_usuario = usuario;
		_esNuevo = usuario == null;
	}

	private void FormUsuarioEditar_Load(object sender, EventArgs e)
	{
		try
		{
			cboRol.DataSource = _negocio.ObtenerRoles();
			cboRol.DisplayMember = "Nombre";
			cboRol.ValueMember = "Id";
			if (_esNuevo)
			{
				Text = "Nuevo usuario";
				chkActivo.Checked = true;
				if (cboRol.Items.Count > 0)
				{
					cboRol.SelectedIndex = 0;
				}
				return;
			}
			Text = "Editar usuario";
			txtUsuario.Text = _usuario.NombreUsuario;
			chkActivo.Checked = _usuario.Activo;
			if (_usuario.Rol != null)
			{
				cboRol.SelectedValue = _usuario.Rol.Id;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("No se pudieron cargar los roles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!Validar())
		{
			return;
		}
		try
		{
			Usuario usuario = (_esNuevo ? new Usuario() : _usuario);
			usuario.NombreUsuario = txtUsuario.Text.Trim();
			usuario.Rol = (Rol)cboRol.SelectedItem;
			usuario.Activo = chkActivo.Checked;
			_negocio.Guardar(usuario, txtContrasena.Text, _esNuevo);
			DialogResult = DialogResult.OK;
			Close();
		}
		catch (ArgumentException ex)
		{
			MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		catch (Exception ex2)
		{
			MessageBox.Show("No se pudo guardar el usuario: " + ex2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.Cancel;
		Close();
	}

	private bool Validar()
	{
		errProvider.Clear();
		bool result = true;
		if (string.IsNullOrWhiteSpace(txtUsuario.Text))
		{
			errProvider.SetError(txtUsuario, "Ingrese el nombre de usuario.");
			result = false;
		}
		if (cboRol.SelectedItem == null)
		{
			errProvider.SetError(cboRol, "Seleccione un rol.");
			result = false;
		}
		if (_esNuevo && string.IsNullOrWhiteSpace(txtContrasena.Text))
		{
			errProvider.SetError(txtContrasena, "Ingrese una contraseña.");
			result = false;
		}
		if (!string.IsNullOrEmpty(txtContrasena.Text) && txtContrasena.Text != txtConfirmar.Text)
		{
			errProvider.SetError(txtConfirmar, "Las contraseñas no coinciden.");
			result = false;
		}
		return result;
	}
}
