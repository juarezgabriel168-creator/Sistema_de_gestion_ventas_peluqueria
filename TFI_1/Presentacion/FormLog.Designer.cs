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
            tableLayoutPanel1 = new TableLayoutPanel();
            txtConUsuario = new TextBox();
            lblConUsuario = new Label();
            lblNomUsuario = new Label();
            txtNomUsuario = new TextBox();
            btnIngresar = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(txtConUsuario, 1, 2);
            tableLayoutPanel1.Controls.Add(lblConUsuario, 0, 2);
            tableLayoutPanel1.Controls.Add(lblNomUsuario, 0, 1);
            tableLayoutPanel1.Controls.Add(txtNomUsuario, 1, 1);
            tableLayoutPanel1.Controls.Add(btnIngresar, 1, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 22.2222214F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 22.2222233F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 22.2222233F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.Size = new Size(558, 386);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // txtConUsuario
            // 
            txtConUsuario.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtConUsuario.Location = new Point(189, 201);
            txtConUsuario.Name = "txtConUsuario";
            txtConUsuario.Size = new Size(180, 23);
            txtConUsuario.TabIndex = 12;
            // 
            // lblConUsuario
            // 
            lblConUsuario.Anchor = AnchorStyles.Right;
            lblConUsuario.AutoSize = true;
            lblConUsuario.Location = new Point(113, 205);
            lblConUsuario.Name = "lblConUsuario";
            lblConUsuario.Size = new Size(70, 15);
            lblConUsuario.TabIndex = 7;
            lblConUsuario.Text = "Contraseña:";
            lblConUsuario.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNomUsuario
            // 
            lblNomUsuario.Anchor = AnchorStyles.Right;
            lblNomUsuario.AutoSize = true;
            lblNomUsuario.Location = new Point(129, 120);
            lblNomUsuario.Name = "lblNomUsuario";
            lblNomUsuario.Size = new Size(54, 15);
            lblNomUsuario.TabIndex = 0;
            lblNomUsuario.Text = "Nombre:";
            lblNomUsuario.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtNomUsuario
            // 
            txtNomUsuario.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtNomUsuario.Location = new Point(189, 116);
            txtNomUsuario.Name = "txtNomUsuario";
            txtNomUsuario.Size = new Size(180, 23);
            txtNomUsuario.TabIndex = 1;
            // 
            // btnIngresar
            // 
            btnIngresar.Anchor = AnchorStyles.None;
            btnIngresar.BackgroundImageLayout = ImageLayout.Center;
            btnIngresar.Location = new Point(223, 303);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(111, 34);
            btnIngresar.TabIndex = 2;
            btnIngresar.Text = "button1";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // FormLog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(558, 386);
            Controls.Add(tableLayoutPanel1);
            Name = "FormLog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TextBox txtConUsuario;
        private Label lblConUsuario;
        private Label lblNomUsuario;
        private TextBox txtNomUsuario;
        private Button btnIngresar;
    }
}
