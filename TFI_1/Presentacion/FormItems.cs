using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TFI_1.Models;
using TFI_1.Negocio;

namespace TFI_1.Presentacion;

public partial class FormItems : Form
{
	private readonly ItemNegocio _negocio = new ItemNegocio();
	private bool _cargandoFiltros;

	public FormItems()
	{
		InitializeComponent();
		btnEliminar.Visible = Sesion.EsAdministrador;
	}

	private void FormItems_Load(object sender, EventArgs e)
	{
		CargarFiltros();
		Cargar();
	}

	private void CargarFiltros()
	{
		_cargandoFiltros = true;
		cboTipo.Items.Clear();
		cboTipo.Items.AddRange("Todos", "Servicio", "Producto");
		cboTipo.SelectedIndex = 0;
		List<Categoria> categorias = new()
		{
			new Categoria("Todas")
			{
				Id = 0
			}
		};
		categorias.AddRange(_negocio.ListarCategorias());
		cboCategoria.DataSource = categorias;
		cboCategoria.DisplayMember = "Nombre";
		cboCategoria.ValueMember = "Id";
		cboCategoria.SelectedIndex = 0;
		_cargandoFiltros = false;
	}

	private void Cargar()
	{
		try
		{
			string filtro = txtBuscar.Text.Trim();
			int? categoriaId = ((cboCategoria.SelectedItem is Categoria { Id: > 0 } categoria) ? new int?(categoria.Id) : ((int?)null));
			int selectedIndex = cboTipo.SelectedIndex;
			if (1 == 0)
			{
			}
			TipoItem? tipoItem = selectedIndex switch
			{
				1 => (TipoItem?)TipoItem.Servicio,
				2 => TipoItem.Producto,
				_ => null,
			};
			if (1 == 0)
			{
			}
			TipoItem? tipo = tipoItem;
			var lista = _negocio.Listar(filtro, categoriaId, tipo, chkStockBajo.Checked);
			dgvItems.DataSource = chkSoloActivos.Checked ? lista.Where(x => x.Activo).ToList() : lista;
			ActualizarBotonEstado();
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo cargar el catálogo", ex);
		}
	}

	private Item? Seleccionado()
	{
		return dgvItems.CurrentRow?.DataBoundItem as Item;
	}

	private void Filtro_Changed(object sender, EventArgs e)
	{
		if (!_cargandoFiltros)
		{
			Cargar();
		}
	}

	private void btnBuscar_Click(object sender, EventArgs e)
	{
		Cargar();
	}

	private void btnRefrescar_Click(object sender, EventArgs e)
	{
		txtBuscar.Clear();
		CargarFiltros();
		Cargar();
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		using FormItemEditar formItemEditar = new FormItemEditar(null);
		if (formItemEditar.ShowDialog(this) == DialogResult.OK)
		{
			Cargar();
		}
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		Item? item = Seleccionado();
		if (item == null)
		{
			MessageBox.Show("Seleccione un ítem de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		using FormItemEditar formItemEditar = new FormItemEditar(item);
		if (formItemEditar.ShowDialog(this) == DialogResult.OK)
		{
			Cargar();
		}
	}

	// Al cambiar el tilde "Solo Activos" se recarga el listado.
	private void chkSoloActivos_CheckedChanged(object sender, EventArgs e)
	{
		Cargar();
	}

	private void dgvItems_SelectionChanged(object sender, EventArgs e)
	{
		ActualizarBotonEstado();
	}

	// El mismo botón desactiva o reactiva, según el estado del ítem seleccionado.
	private void ActualizarBotonEstado()
	{
		Item? item = Seleccionado();
		btnDesactivar.Text = (item != null && !item.Activo) ? "Reactivar" : "Desactivar";
	}

	private void btnDesactivar_Click(object sender, EventArgs e)
	{
		Item? item = Seleccionado();
		if (item == null)
		{
			MessageBox.Show("Seleccione un ítem de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}

		bool reactivar = !item.Activo;
		string accion = reactivar ? "reactivar" : "desactivar";
		DialogResult dialogResult = MessageBox.Show(
			"¿Desea " + accion + " '" + item.Nombre + "'?",
			"Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dialogResult != DialogResult.Yes)
		{
			return;
		}

		try
		{
			if (reactivar)
			{
				_negocio.Reactivar(item.Id);
			}
			else
			{
				_negocio.Desactivar(item.Id);
			}
			Cargar();
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo cambiar el estado", ex);
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		Item? item = Seleccionado();
		if (item == null)
		{
			MessageBox.Show("Seleccione un ítem de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}

		if (!ConfirmacionEliminacion.Confirmar(
			this,
			$"el ítem '{item.Nombre}'",
			"Esta acción no se puede deshacer. Si el ítem tiene ventas, ingresos o turnos asociados, " +
			"no se podrá eliminar y deberá desactivarlo."))
		{
			return;
		}

		try
		{
			_negocio.Eliminar(item.Id);
			Cargar();
		}
		catch (InvalidOperationException ex)
		{
			MessageBox.Show(ex.Message, "No se puede eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		catch (Exception ex)
		{
			Mensajes.Error(this, "No se pudo eliminar el ítem", ex);
		}
	}
}
