using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
public partial class FormItemEditar : Form
{
	private readonly ItemNegocio _negocio = new ItemNegocio();
	private readonly Item? _item;
	private readonly bool _esNuevo;

	public FormItemEditar() : this(null)
	{
	}

	public FormItemEditar(Item? item)
	{
		InitializeComponent();
		_item = item;
		_esNuevo = item == null;
	}

	private void FormItemEditar_Load(object sender, EventArgs e)
	{
		try
		{
			cboTipo.Items.Clear();
			cboTipo.Items.AddRange("Servicio", "Producto");
			cboCategoria.DataSource = _negocio.ListarCategorias();
			cboCategoria.DisplayMember = "Nombre";
			cboCategoria.ValueMember = "Id";
			if (_esNuevo)
			{
				Text = "Nuevo ítem";
				cboTipo.SelectedIndex = 0;
				chkActivo.Checked = true;
				if (cboCategoria.Items.Count > 0)
				{
					cboCategoria.SelectedIndex = 0;
				}
			}
			else
			{
				Text = "Editar ítem";
				txtNombre.Text = _item.Nombre;
				txtPrecio.Text = _item.Precio.ToString(CultureInfo.CurrentCulture);
				chkActivo.Checked = _item.Activo;
				cboTipo.SelectedIndex = ((_item.Tipo == TipoItem.Producto) ? 1 : 0);
				if (_item.Categoria != null)
				{
					cboCategoria.SelectedValue = _item.Categoria.Id;
				}
				if (_item.Tipo == TipoItem.Producto)
				{
					txtStock.Text = _item.Stock?.ToString() ?? string.Empty;
					txtStockMinimo.Text = _item.StockMinimo?.ToString() ?? string.Empty;
				}
				else
				{
					txtDuracion.Text = _item.DuracionMin?.ToString() ?? string.Empty;
				}
			}
			AlternarCampos();
		}
		catch (Exception ex)
		{
			MessageBox.Show("No se pudieron cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}

	private void cboTipo_SelectedIndexChanged(object sender, EventArgs e)
	{
		AlternarCampos();
	}

	private void AlternarCampos()
	{
		bool flag = cboTipo.SelectedIndex == 1;
		lblDuracion.Visible = !flag;
		txtDuracion.Visible = !flag;
		lblStock.Visible = flag;
		txtStock.Visible = flag;
		lblStockMinimo.Visible = flag;
		txtStockMinimo.Visible = flag;
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!Validar())
		{
			return;
		}
		try
		{
			Item item = (_esNuevo ? new Item() : _item);
			item.Nombre = txtNombre.Text.Trim();
			item.Tipo = ((cboTipo.SelectedIndex == 1) ? TipoItem.Producto : TipoItem.Servicio);
			item.Precio = decimal.Parse(txtPrecio.Text, CultureInfo.CurrentCulture);
			item.Categoria = (Categoria)cboCategoria.SelectedItem;
			item.Activo = chkActivo.Checked;
			if (item.Tipo == TipoItem.Producto)
			{
				item.Stock = int.Parse(txtStock.Text);
				item.StockMinimo = int.Parse(txtStockMinimo.Text);
				item.DuracionMin = null;
			}
			else
			{
				item.DuracionMin = (string.IsNullOrWhiteSpace(txtDuracion.Text) ? ((int?)null) : new int?(int.Parse(txtDuracion.Text)));
				item.Stock = null;
				item.StockMinimo = null;
			}
			_negocio.Guardar(item);
			DialogResult = DialogResult.OK;
			Close();
		}
		catch (ArgumentException ex)
		{
			MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		catch (FormatException)
		{
			MessageBox.Show("Verifique el formato del precio y los números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		catch (Exception ex3)
		{
			MessageBox.Show("No se pudo guardar el ítem: " + ex3.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		if (string.IsNullOrWhiteSpace(txtNombre.Text))
		{
			errProvider.SetError(txtNombre, "Ingrese el nombre del ítem.");
			result = false;
		}
		if (!decimal.TryParse(txtPrecio.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var result2) || result2 < 0m)
		{
			errProvider.SetError(txtPrecio, "Ingrese un precio válido (mayor o igual a 0).");
			result = false;
		}
		if (cboCategoria.SelectedItem == null)
		{
			errProvider.SetError(cboCategoria, "Seleccione una categoría.");
			result = false;
		}
		int result5;
		if (cboTipo.SelectedIndex == 1)
		{
			if (!int.TryParse(txtStock.Text, out var result3) || result3 < 0)
			{
				errProvider.SetError(txtStock, "Ingrese un stock válido (mayor o igual a 0).");
				result = false;
			}
			if (!int.TryParse(txtStockMinimo.Text, out var result4) || result4 < 0)
			{
				errProvider.SetError(txtStockMinimo, "Ingrese un stock mínimo válido (mayor o igual a 0).");
				result = false;
			}
		}
		else if (!string.IsNullOrWhiteSpace(txtDuracion.Text) && (!int.TryParse(txtDuracion.Text, out result5) || result5 <= 0))
		{
			errProvider.SetError(txtDuracion, "La duración debe ser mayor a cero.");
			result = false;
		}
		return result;
	}
}
