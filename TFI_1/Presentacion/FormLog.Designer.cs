namespace TFI_1
{
    partial class FormLog
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLog));
            tableLayoutPanel1 = new TableLayoutPanel();
            txtConUsuario = new TextBox();
            lblConUsuario = new Label();
            txtNomUsuario = new TextBox();
            lblNomUsuario = new Label();
            lblInicioSesion = new Label();
            btnIngresar = new Button();
            pictureBox1 = new PictureBox();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40.47306F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10.906702F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.48883F));
            tableLayoutPanel1.Controls.Add(txtConUsuario, 2, 2);
            tableLayoutPanel1.Controls.Add(lblConUsuario, 1, 2);
            tableLayoutPanel1.Controls.Add(txtNomUsuario, 2, 1);
            tableLayoutPanel1.Controls.Add(lblNomUsuario, 1, 1);
            tableLayoutPanel1.Controls.Add(lblInicioSesion, 1, 0);
            tableLayoutPanel1.Controls.Add(btnIngresar, 1, 3);
            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 22.2222214F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 22.2222233F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 22.2222233F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(761, 360);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // txtConUsuario
            // 
            txtConUsuario.Anchor = AnchorStyles.Left;
            txtConUsuario.Location = new Point(394, 187);
            txtConUsuario.Name = "txtConUsuario";
            txtConUsuario.Size = new Size(279, 23);
            txtConUsuario.TabIndex = 12;
            // 
            // lblConUsuario
            // 
            lblConUsuario.Anchor = AnchorStyles.Right;
            lblConUsuario.AutoSize = true;
            lblConUsuario.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblConUsuario.Location = new Point(319, 191);
            lblConUsuario.Name = "lblConUsuario";
            lblConUsuario.Size = new Size(69, 15);
            lblConUsuario.TabIndex = 7;
            lblConUsuario.Text = "Contraseña:";
            lblConUsuario.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtNomUsuario
            // 
            txtNomUsuario.Anchor = AnchorStyles.Left;
            txtNomUsuario.Location = new Point(394, 107);
            txtNomUsuario.Name = "txtNomUsuario";
            txtNomUsuario.Size = new Size(279, 23);
            txtNomUsuario.TabIndex = 1;
            // 
            // lblNomUsuario
            // 
            lblNomUsuario.Anchor = AnchorStyles.Right;
            lblNomUsuario.AutoSize = true;
            lblNomUsuario.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNomUsuario.Location = new Point(334, 111);
            lblNomUsuario.Name = "lblNomUsuario";
            lblNomUsuario.Size = new Size(54, 15);
            lblNomUsuario.TabIndex = 0;
            lblNomUsuario.Text = "Nombre:";
            lblNomUsuario.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblInicioSesion
            // 
            lblInicioSesion.Anchor = AnchorStyles.Bottom;
            lblInicioSesion.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(lblInicioSesion, 2);
            lblInicioSesion.Font = new Font("Arial Rounded MT Bold", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInicioSesion.Location = new Point(406, 47);
            lblInicioSesion.Name = "lblInicioSesion";
            lblInicioSesion.Size = new Size(256, 32);
            lblInicioSesion.TabIndex = 13;
            lblInicioSesion.Text = "INICIO DE SESION";
            // 
            // btnIngresar
            // 
            btnIngresar.Anchor = AnchorStyles.None;
            btnIngresar.BackgroundImageLayout = ImageLayout.Center;
            tableLayoutPanel1.SetColumnSpan(btnIngresar, 2);
            btnIngresar.Location = new Point(479, 282);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(111, 34);
            btnIngresar.TabIndex = 2;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.InitialImage = Properties.Resources.Code_Generated_Image;
            pictureBox1.Location = new Point(3, 82);
            pictureBox1.Name = "pictureBox1";
            tableLayoutPanel1.SetRowSpan(pictureBox1, 2);
            pictureBox1.Size = new Size(302, 154);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 14;
            pictureBox1.TabStop = false;
            // 
            // FormLog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(761, 360);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormLog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TextBox txtConUsuario;
        private Label lblConUsuario;
        private Label lblNomUsuario;
        private TextBox txtNomUsuario;
        private Button btnIngresar;
        private Label lblInicioSesion;
        private PictureBox pictureBox1;
    }
}
