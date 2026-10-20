using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TFI_1.Presentacion;

partial class FormProveedores : Form
{
	private IContainer components = null;

	private Label lblTitulo;
	private Label lblBuscar;
	private TextBox txtBuscar;
	private Button btnBuscar;
	private Button btnRefrescar;
	private CheckBox chkSoloActivos;
	private DataGridView dgvProveedores;
	private Button btnNuevo;
	private Button btnEditar;
	private Button btnDesactivar;
	private Button btnCerrar;
	private DataGridViewTextBoxColumn colNombre;
	private DataGridViewTextBoxColumn colCuit;
	private DataGridViewTextBoxColumn colTelefono;
	private DataGridViewTextBoxColumn colEmail;
	private DataGridViewTextBoxColumn colDireccion;
	private DataGridViewCheckBoxColumn colActivo;

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
		lblTitulo = new Label();
		lblBuscar = new Label();
		txtBuscar = new TextBox();
		btnBuscar = new Button();
		btnRefrescar = new Button();
		chkSoloActivos = new CheckBox();
		dgvProveedores = new DataGridView();
		colNombre = new DataGridViewTextBoxColumn();
		colCuit = new DataGridViewTextBoxColumn();
		colTelefono = new DataGridViewTextBoxColumn();
		colEmail = new DataGridViewTextBoxColumn();
		colDireccion = new DataGridViewTextBoxColumn();
		colActivo = new DataGridViewCheckBoxColumn();
		btnNuevo = new Button();
		btnEditar = new Button();
		btnDesactivar = new Button();
		btnCerrar = new Button();
		((ISupportInitialize)dgvProveedores).BeginInit();
		SuspendLayout();
		//
		// lblTitulo
		//
		lblTitulo.AutoSize = true;
		lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
		lblTitulo.Location = new Point(12, 10);
		lblTitulo.Name = "lblTitulo";
		lblTitulo.Size = new Size(180, 25);
		lblTitulo.TabIndex = 0;
		lblTitulo.Text = "Proveedores";
		//
		// lblBuscar
		//
		lblBuscar.AutoSize = true;
		lblBuscar.Location = new Point(12, 50);
		lblBuscar.Name = "lblBuscar";
		lblBuscar.Size = new Size(45, 15);
		lblBuscar.TabIndex = 1;
		lblBuscar.Text = "Buscar:";
		//
		// txtBuscar
		//
		txtBuscar.Location = new Point(12, 68);
		txtBuscar.Name = "txtBuscar";
		txtBuscar.PlaceholderText = "nombre o CUIT...";
		txtBuscar.Size = new Size(260, 23);
		txtBuscar.TabIndex = 2;
		txtBuscar.KeyDown += txtBuscar_KeyDown;
		//
		// btnBuscar
		//
		btnBuscar.Location = new Point(284, 65);
		btnBuscar.Name = "btnBuscar";
		btnBuscar.Size = new Size(80, 26);
		btnBuscar.TabIndex = 3;
		btnBuscar.Text = "Buscar";
		btnBuscar.UseVisualStyleBackColor = true;
		btnBuscar.Click += btnBuscar_Click;
		//
		// btnRefrescar
		//
		btnRefrescar.Location = new Point(370, 65);
		btnRefrescar.Name = "btnRefrescar";
		btnRefrescar.Size = new Size(90, 26);
		btnRefrescar.TabIndex = 4;
		btnRefrescar.Text = "Refrescar";
		btnRefrescar.UseVisualStyleBackColor = true;
		btnRefrescar.Click += btnRefrescar_Click;
		//
		// chkSoloActivos
		//
		chkSoloActivos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		chkSoloActivos.AutoSize = true;
		chkSoloActivos.Checked = true;
		chkSoloActivos.CheckState = CheckState.Checked;
		chkSoloActivos.Location = new Point(748, 71);
		chkSoloActivos.Name = "chkSoloActivos";
		chkSoloActivos.Size = new Size(92, 19);
		chkSoloActivos.TabIndex = 5;
		chkSoloActivos.Text = "Solo Activos";
		chkSoloActivos.UseVisualStyleBackColor = true;
		chkSoloActivos.CheckedChanged += chkSoloActivos_CheckedChanged;
		//
		// dgvProveedores
		//
		dgvProveedores.AllowUserToAddRows = false;
		dgvProveedores.AllowUserToDeleteRows = false;
		dgvProveedores.AllowUserToResizeRows = false;
		dgvProveedores.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		dgvProveedores.AutoGenerateColumns = false;
		dgvProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		dgvProveedores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dgvProveedores.Columns.AddRange(new DataGridViewColumn[] { colNombre, colCuit, colTelefono, colEmail, colDireccion, colActivo });
		dgvProveedores.Location = new Point(12, 104);
		dgvProveedores.MultiSelect = false;
		dgvProveedores.Name = "dgvProveedores";
		dgvProveedores.ReadOnly = true;
		dgvProveedores.RowHeadersVisible = false;
		dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		dgvProveedores.Size = new Size(836, 300);
		dgvProveedores.TabIndex = 6;
		dgvProveedores.CellDoubleClick += dgvProveedores_CellDoubleClick;
		dgvProveedores.SelectionChanged += dgvProveedores_SelectionChanged;
		//
		// colNombre
		//
		colNombre.DataPropertyName = "Nombre";
		colNombre.FillWeight = 30F;
		colNombre.HeaderText = "Nombre";
		colNombre.Name = "colNombre";
		colNombre.ReadOnly = true;
		//
		// colCuit
		//
		colCuit.DataPropertyName = "Cuit";
		colCuit.FillWeight = 15F;
		colCuit.HeaderText = "CUIT";
		colCuit.Name = "colCuit";
		colCuit.ReadOnly = true;
		//
		// colTelefono
		//
		colTelefono.DataPropertyName = "Telefono";
		colTelefono.FillWeight = 15F;
		colTelefono.HeaderText = "Teléfono";
		colTelefono.Name = "colTelefono";
		colTelefono.ReadOnly = true;
		//
		// colEmail
		//
		colEmail.DataPropertyName = "Email";
		colEmail.FillWeight = 22F;
		colEmail.HeaderText = "Email";
		colEmail.Name = "colEmail";
		colEmail.ReadOnly = true;
		//
		// colDireccion
		//
		colDireccion.DataPropertyName = "Direccion";
		colDireccion.FillWeight = 23F;
		colDireccion.HeaderText = "Dirección";
		colDireccion.Name = "colDireccion";
		colDireccion.ReadOnly = true;
		//
		// colActivo
		//
		colActivo.DataPropertyName = "Activo";
		colActivo.FillWeight = 10F;
		colActivo.HeaderText = "Activo";
		colActivo.Name = "colActivo";
		colActivo.ReadOnly = true;
		//
		// btnNuevo
		//
		btnNuevo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		btnNuevo.Location = new Point(12, 420);
		btnNuevo.Name = "btnNuevo";
		btnNuevo.Size = new Size(90, 30);
		btnNuevo.TabIndex = 7;
		btnNuevo.Text = "Nuevo";
		btnNuevo.UseVisualStyleBackColor = true;
		btnNuevo.Click += btnNuevo_Click;
		//
		// btnEditar
		//
		btnEditar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		btnEditar.Location = new Point(108, 420);
		btnEditar.Name = "btnEditar";
		btnEditar.Size = new Size(90, 30);
		btnEditar.TabIndex = 8;
		btnEditar.Text = "Editar";
		btnEditar.UseVisualStyleBackColor = true;
		btnEditar.Click += btnEditar_Click;
		//
		// btnDesactivar
		//
		btnDesactivar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		btnDesactivar.Location = new Point(204, 420);
		btnDesactivar.Name = "btnDesactivar";
		btnDesactivar.Size = new Size(100, 30);
		btnDesactivar.TabIndex = 9;
		btnDesactivar.Text = "Desactivar";
		btnDesactivar.UseVisualStyleBackColor = true;
		btnDesactivar.Click += btnDesactivar_Click;
		//
		// btnCerrar
		//
		btnCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		btnCerrar.DialogResult = DialogResult.Cancel;
		btnCerrar.Location = new Point(768, 420);
		btnCerrar.Name = "btnCerrar";
		btnCerrar.Size = new Size(80, 30);
		btnCerrar.TabIndex = 10;
		btnCerrar.Text = "Cerrar";
		btnCerrar.UseVisualStyleBackColor = true;
		btnCerrar.Click += btnCerrar_Click;
		//
		// FormProveedores
		//
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = SystemColors.ActiveCaption;
		CancelButton = btnCerrar;
		ClientSize = new Size(860, 464);
		Controls.Add(btnCerrar);
		Controls.Add(btnDesactivar);
		Controls.Add(btnEditar);
		Controls.Add(btnNuevo);
		Controls.Add(dgvProveedores);
		Controls.Add(chkSoloActivos);
		Controls.Add(btnRefrescar);
		Controls.Add(btnBuscar);
		Controls.Add(txtBuscar);
		Controls.Add(lblBuscar);
		Controls.Add(lblTitulo);
		Font = new Font("Segoe UI", 9F);
		MinimumSize = new Size(760, 400);
		Name = "FormProveedores";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Proveedores";
		Load += FormProveedores_Load;
		((ISupportInitialize)dgvProveedores).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}
}
