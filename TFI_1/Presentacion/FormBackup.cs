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
		Resize += (_, _) => AjustarLayout();
		AjustarLayout();
	}

	private void AjustarLayout()
	{
		if (IsDisposed)
		{
			return;
		}

		const int margen = 12;
		btnElegir.Left = ClientSize.Width - margen - btnElegir.Width;
		txtCarpeta.Width = Math.Max(100, btnElegir.Left - txtCarpeta.Left - 10);

		int ancho = Math.Max(200, ClientSize.Width - (margen * 2));
		lblExplicacion.Width = ancho;
		lblResultado.Width = ancho;
		lblAyuda.Width = ancho;

		btnCerrar.Left = ClientSize.Width - margen - btnCerrar.Width;
		btnCerrar.Top = ClientSize.Height - margen - btnCerrar.Height;
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
			Mensajes.Error(this, "No se pudo generar la copia de seguridad", ex);
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
			Mensajes.Error(this, "No se pudo abrir la carpeta", ex);
		}
	}

	private void btnCerrar_Click(object sender, EventArgs e) => Close();
}
