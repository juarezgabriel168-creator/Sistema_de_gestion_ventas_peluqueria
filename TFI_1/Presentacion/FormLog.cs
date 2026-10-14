using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using TFI_1.Datos;
using TFI_1.Models;
using TFI_1.Negocio;
using TFI_1.Presentacion;



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
                Sesion.UsuarioActual = _negocio.Autenticar(txtNomUsuario.Text, txtConUsuario.Text);

                var dashboard = new FormDashboard();
                dashboard.FormClosed += (s, args) => Application.Exit();
                dashboard.Show();
                Hide();
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
        }
    }
}


