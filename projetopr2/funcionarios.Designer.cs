namespace projetopr2
{
    partial class funcionarios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(funcionarios));
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnAtualizarStatus = new System.Windows.Forms.Button();
            this.btnEnviarEmail = new System.Windows.Forms.Button();
            this.btnRelatorio = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtBuscaNome = new System.Windows.Forms.TextBox();
            this.chkFiltrarData = new System.Windows.Forms.CheckBox();
            this.dtpBuscaData = new System.Windows.Forms.DateTimePicker();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.lblTotalVendido = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPedidos
            // 
            this.dgvPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedidos.Location = new System.Drawing.Point(120, 74);
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.Size = new System.Drawing.Size(1554, 574);
            this.dgvPedidos.TabIndex = 0;
            this.dgvPedidos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPedidos_CellClick);
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(120, 654);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(371, 21);
            this.cmbStatus.TabIndex = 1;
            // 
            // btnAtualizarStatus
            // 
            this.btnAtualizarStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(208)))));
            this.btnAtualizarStatus.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizarStatus.ForeColor = System.Drawing.Color.Black;
            this.btnAtualizarStatus.Location = new System.Drawing.Point(507, 654);
            this.btnAtualizarStatus.Name = "btnAtualizarStatus";
            this.btnAtualizarStatus.Size = new System.Drawing.Size(128, 23);
            this.btnAtualizarStatus.TabIndex = 2;
            this.btnAtualizarStatus.Text = "Atualizar Status";
            this.btnAtualizarStatus.UseVisualStyleBackColor = false;
            this.btnAtualizarStatus.Click += new System.EventHandler(this.btnAtualizarStatus_Click);
            // 
            // btnEnviarEmail
            // 
            this.btnEnviarEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(208)))));
            this.btnEnviarEmail.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarEmail.Location = new System.Drawing.Point(775, 654);
            this.btnEnviarEmail.Name = "btnEnviarEmail";
            this.btnEnviarEmail.Size = new System.Drawing.Size(128, 23);
            this.btnEnviarEmail.TabIndex = 3;
            this.btnEnviarEmail.Text = "Notificar cliente";
            this.btnEnviarEmail.UseVisualStyleBackColor = false;
            this.btnEnviarEmail.Click += new System.EventHandler(this.btnEnviarEmail_Click);
            // 
            // btnRelatorio
            // 
            this.btnRelatorio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(208)))));
            this.btnRelatorio.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelatorio.Location = new System.Drawing.Point(641, 654);
            this.btnRelatorio.Name = "btnRelatorio";
            this.btnRelatorio.Size = new System.Drawing.Size(128, 23);
            this.btnRelatorio.TabIndex = 4;
            this.btnRelatorio.Text = "Gerar relatório";
            this.btnRelatorio.UseVisualStyleBackColor = false;
            this.btnRelatorio.Click += new System.EventHandler(this.btnRelatorio_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(208)))));
            this.button1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(120, 695);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 34);
            this.button1.TabIndex = 5;
            this.button1.Text = "Produtos";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtBuscaNome
            // 
            this.txtBuscaNome.Location = new System.Drawing.Point(1547, 743);
            this.txtBuscaNome.Name = "txtBuscaNome";
            this.txtBuscaNome.Size = new System.Drawing.Size(200, 20);
            this.txtBuscaNome.TabIndex = 6;
            // 
            // chkFiltrarData
            // 
            this.chkFiltrarData.AutoSize = true;
            this.chkFiltrarData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(208)))));
            this.chkFiltrarData.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFiltrarData.Location = new System.Drawing.Point(1547, 779);
            this.chkFiltrarData.Name = "chkFiltrarData";
            this.chkFiltrarData.Size = new System.Drawing.Size(114, 23);
            this.chkFiltrarData.TabIndex = 7;
            this.chkFiltrarData.Text = "Filtar por data";
            this.chkFiltrarData.UseVisualStyleBackColor = false;
            // 
            // dtpBuscaData
            // 
            this.dtpBuscaData.Location = new System.Drawing.Point(1547, 808);
            this.dtpBuscaData.Name = "dtpBuscaData";
            this.dtpBuscaData.Size = new System.Drawing.Size(200, 20);
            this.dtpBuscaData.TabIndex = 8;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(208)))));
            this.btnFiltrar.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.Location = new System.Drawing.Point(1444, 652);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(111, 37);
            this.btnFiltrar.TabIndex = 9;
            this.btnFiltrar.Text = "Pesquisar";
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click_1);
            // 
            // lblTotalVendido
            // 
            this.lblTotalVendido.AutoSize = true;
            this.lblTotalVendido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(208)))));
            this.lblTotalVendido.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalVendido.Location = new System.Drawing.Point(1616, 652);
            this.lblTotalVendido.Name = "lblTotalVendido";
            this.lblTotalVendido.Size = new System.Drawing.Size(45, 19);
            this.lblTotalVendido.TabIndex = 10;
            this.lblTotalVendido.Text = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-3, -3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1920, 1080);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(208)))));
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1588, 695);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 19);
            this.label1.TabIndex = 12;
            this.label1.Text = "Filtar vendas";
            // 
            // label2
            // 
            this.label2.AllowDrop = true;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(208)))));
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1543, 721);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 19);
            this.label2.TabIndex = 13;
            this.label2.Text = "Nome do cliente";
            // 
            // funcionarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1855, 934);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTotalVendido);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.dtpBuscaData);
            this.Controls.Add(this.chkFiltrarData);
            this.Controls.Add(this.txtBuscaNome);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRelatorio);
            this.Controls.Add(this.btnEnviarEmail);
            this.Controls.Add(this.btnAtualizarStatus);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.dgvPedidos);
            this.Controls.Add(this.pictureBox1);
            this.Name = "funcionarios";
            this.Text = "funcionarios";
            this.Load += new System.EventHandler(this.funcionarios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnAtualizarStatus;
        private System.Windows.Forms.Button btnEnviarEmail;
        private System.Windows.Forms.Button btnRelatorio;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtBuscaNome;
        private System.Windows.Forms.CheckBox chkFiltrarData;
        private System.Windows.Forms.DateTimePicker dtpBuscaData;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Label lblTotalVendido;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}