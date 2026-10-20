using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

/// <summary>
/// Genera una copia de seguridad de la base en un archivo .sql usando
/// mysqldump, en la carpeta que elija el usuario.
/// </summary>
public partial class FormBackup : Form
{
	private readonly BackupNegocio _negocio = new BackupNegocio();

	private string? _ultimaCarpeta;

	public FormBackup()
	{
		InitializeComponent();
	}

	private void FormBackup_Load(object sender, EventArgs e)
	{
		string carpeta = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Backups Peluquería");
		try
		{
			Directory.CreateDirectory(carpeta);
		}
		catch
		{
			// Si no se puede crear, se usa Documentos, que siempre existe.
			carpeta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		}
		txtCarpeta.Text = carpeta;
	}

	private void btnElegir_Click(object sender, EventArgs e)
	{
		using FolderBrowserDialog dialogo = new FolderBrowserDialog
		{
			Description = "Elija la carpeta donde guardar la copia de seguridad.",
			UseDescriptionForTitle = true,
			ShowNewFolderButton = true,
		};
		if (Directory.Exists(txtCarpeta.Text))
		{
			dialogo.SelectedPath = txtCarpeta.Text;
		}
		if (dialogo.ShowDialog(this) == DialogResult.OK)
		{
			txtCarpeta.Text = dialogo.SelectedPath;
		}
	}

	private void btnGenerar_Click(object sender, EventArgs e)
	{
		try
		{
			UseWaitCursor = true;
			btnGenerar.Enabled = false;
			lblResultado.ForeColor = Color.DimGray;
			lblResultado.Text = "Generando la copia...";

			string ruta = _negocio.Generar(txtCarpeta.Text);

			_ultimaCarpeta = Path.GetDirectoryName(ruta);
			btnAbrirCarpeta.Enabled = _ultimaCarpeta != null;
			lblResultado.ForeColor = Color.ForestGreen;
			lblResultado.Text = "Copia generada: " + ruta;

			MessageBox.Show(
				"La copia de seguridad se generó correctamente." +
				Environment.NewLine + Environment.NewLine + ruta,
				"Copia de seguridad", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		catch (Exception ex)
		{
			lblResultado.ForeColor = Color.Firebrick;
			lblResultado.Text = "No se pudo generar la copia.";
			MessageBox.Show(ex.Message, "Copia de seguridad",
				MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
		finally
		{
			UseWaitCursor = false;
			btnGenerar.Enabled = true;
		}
	}

	private void btnAbrirCarpeta_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(_ultimaCarpeta))
		{
			return;
		}

		try
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = _ultimaCarpeta,
				UseShellExecute = true,
			});
		}
		catch (Exception ex)
		{
			MessageBox.Show("No se pudo abrir la carpeta: " + ex.Message, "Copia de seguridad",
				MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
	}

	private void btnCerrar_Click(object sender, EventArgs e) => Close();
}
