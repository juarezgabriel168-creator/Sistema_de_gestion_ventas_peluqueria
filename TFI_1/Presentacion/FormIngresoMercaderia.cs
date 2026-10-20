using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;
public partial class FormIngresoMercaderia : Form
{
	private readonly IngresoMercaderiaNegocio _negocio = new();
	private readonly ItemNegocio _itemNegocio = new();
	private readonly ProveedorNegocio _proveedorNegocio = new();
	private readonly List<IngresoMercaderiaDetalle> _detalles = new();
	private bool _cargando;

	public FormIngresoMercaderia()
	{
		InitializeComponent();
	}

	private void FormIngresoMercaderia_Load(object sender, EventArgs e)
	{
		dtpFecha.Value = DateTime.Now;
		CargarProveedores();
		CargarItems();
		RefrescarDetalle();
	}

	private void CargarProveedores()
	{
		try
		{
			cboProveedor.DataSource = _proveedorNegocio.Listar()
				.Where(p => p.Activo)
				.ToList();
			cboProveedor.DisplayMember = "Nombre";
			cboProveedor.ValueMember = "Id";
			cboProveedor.SelectedIndex = -1;
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudieron cargar los proveedores", ex);
		}
	}

	private void CargarItems()
	{
		try
		{
			cboProducto.DataSource = _itemNegocio.Listar()
				.Where(i => i.Activo && i.EsProducto)
				.ToList();
			cboProducto.DisplayMember = "Nombre";
			cboProducto.ValueMember = "Id";
			cboProducto.SelectedIndex = -1;
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudieron cargar los productos", ex);
		}
	}

	private void cboProducto_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (cboProducto.SelectedItem is Item item)
		{
			lblPrecioCosto.Text = item.Costo.HasValue ? item.Costo.Value.ToString("C2") : "(sin costo)";
			lblStockActual.Text = item.Stock.HasValue ? item.Stock.Value.ToString() : "0";
		}
		else
		{
			lblPrecioCosto.Text = string.Empty;
			lblStockActual.Text = string.Empty;
		}
	}

	private void btnAgregar_Click(object sender, EventArgs e)
	{
		if (cboProducto.SelectedItem is not Item item)
		{
			MessageBox.Show("Seleccione un producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		int cantidad = (int)numCantidad.Value;
		decimal costo = numCosto.Value;
		if (cantidad <= 0)
		{
			MessageBox.Show("La cantidad debe ser mayor a cero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		if (costo < 0)
		{
			MessageBox.Show("El costo no puede ser negativo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}

		var existente = _detalles.FirstOrDefault(d => d.Item.Id == item.Id);
		if (existente != null)
		{
			_detalles.Remove(existente);
			existente.Cantidad += cantidad;
			if (costo > 0) existente.CostoUnitario = costo;
			_detalles.Add(existente);
		}
		else
		{
			_detalles.Add(new IngresoMercaderiaDetalle(item, cantidad, costo));
		}
		RefrescarDetalle();
		numCantidad.Value = 1;
		numCosto.Value = Math.Max(numCosto.Minimum, item.Costo ?? 0m);
	}

	private void btnQuitar_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.CurrentRow?.DataBoundItem is IngresoMercaderiaDetalle det)
		{
			_detalles.Remove(det);
			RefrescarDetalle();
		}
	}

	private void RefrescarDetalle()
	{
		// Se enlaza una copia de la lista: si se reutiliza la misma instancia,
		// el CurrencyManager conserva la posición -1 que tomó al bindear la lista
		// vacía al cargar (List<T> no emite eventos de cambio) y el DataGridView
		// revienta con IndexOutOfRangeException al hacer clic en una fila.
		dgvDetalle.DataSource = null;
		dgvDetalle.DataSource = _detalles.ToList();
		FormatearDetalle();
		ActualizarTotal();
	}

	private void FormatearDetalle()
	{
		if (dgvDetalle.Columns.Contains("Id"))
			dgvDetalle.Columns["Id"].Visible = false;
		if (dgvDetalle.Columns.Contains("Subtotal"))
			dgvDetalle.Columns["Subtotal"].DefaultCellStyle.Format = "C2";
		if (dgvDetalle.Columns.Contains("CostoUnitario"))
			dgvDetalle.Columns["CostoUnitario"].DefaultCellStyle.Format = "C2";
	}

	private void ActualizarTotal()
	{
		decimal total = _detalles.Sum(d => d.Subtotal);
		lblTotalValor.Text = total.ToString("C2");
	}

	private void numCosto_ValueChanged(object sender, EventArgs e) { }

	private IngresoMercaderia? LeerCabecera()
	{
		if (cboProveedor.SelectedItem is not Proveedor proveedor)
		{
			MessageBox.Show("Seleccione un proveedor (Administración → Proveedores).", "Aviso",
				MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return null;
		}
		if (_detalles.Count == 0)
		{
			MessageBox.Show("Agregue al menos un producto al ingreso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return null;
		}
		if (Sesion.UsuarioActual == null)
		{
			MessageBox.Show("No hay un usuario en sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return null;
		}

		var ingreso = new IngresoMercaderia
		{
			IdProveedor = proveedor.Id,
			Proveedor = proveedor.Nombre,
			Fecha = dtpFecha.Value,
			IdUsuario = Sesion.UsuarioActual.Id,
			Observacion = string.IsNullOrWhiteSpace(txtObservacion.Text) ? null : txtObservacion.Text.Trim()
		};
		foreach (var d in _detalles)
			ingreso.AgregarDetalle(d.Item, d.Cantidad, d.CostoUnitario);
		return ingreso;
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		var ingreso = LeerCabecera();
		if (ingreso == null)
			return;
		try
		{
			int id = _negocio.Insertar(ingreso);
			MessageBox.Show($"Ingreso #{id} registrado. Total: {ingreso.Total:C2}", "Ingreso registrado",
				MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			_detalles.Clear();
			cboProveedor.SelectedIndex = -1;
			txtObservacion.Clear();
			RefrescarDetalle();
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo registrar el ingreso", ex);
		}
	}

	private void btnCerrar_Click(object sender, EventArgs e) => Close();
}
