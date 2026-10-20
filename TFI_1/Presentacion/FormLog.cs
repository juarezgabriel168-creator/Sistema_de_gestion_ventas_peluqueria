using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using TFI_1.Datos;
using TFI_1.Models;
using TFI_1.Negocio;
using TFI_1.Presentacion;
using System.Runtime.InteropServices;



namespace TFI_1
{
    public partial class FormLog : Form
    {
        private readonly UsuarioNegocio _negocio = new UsuarioNegocio();

        public FormLog()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNomUsuario.Text) || string.IsNullOrWhiteSpace(txtConUsuario.Text))
                {
                    MessageBox.Show("Ingrese usuario y contraseña.", "Datos incompletos",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Sesion.UsuarioActual = _negocio.Autenticar(txtNomUsuario.Text.Trim(), txtConUsuario.Text);

                var principal = new FormPrincipal();
                principal.FormClosed += (s, args) =>
                {
                    if (principal.CerrarSesionPendiente)
                    {
                        // El usuario cerró sesión: se vuelve a esta pantalla de login
                        // en lugar de cerrar la aplicación.
                        Sesion.CerrarSesion();
                        txtNomUsuario.Clear();
                        txtConUsuario.Clear();
                        txtNomUsuario.Focus();
                        Show();
                    }
                    else
                    {
                        Application.Exit();
                    }
                };
                principal.Show();
                Hide();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"Valor nulo inesperado: {ex.Message}\nParámetro: {ex.ParamName}\n\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (MySqlException)
            {
                MessageBox.Show("No se pudo conectar con la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.GetType().Name}\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkMostar_CheckedChanged(object sender, EventArgs e)
        {
            txtConUsuario.UseSystemPasswordChar = !chkMostar.Checked;
        }
    }
}


