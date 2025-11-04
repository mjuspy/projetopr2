namespace projetopr2
{
    partial class pedidos
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pedidos));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dgvCarrinho = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblEntrega = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.chkEntrega = new System.Windows.Forms.CheckBox();
            this.chkRetira = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPix = new System.Windows.Forms.RadioButton();
            this.rbCredito = new System.Windows.Forms.RadioButton();
            this.rbDebito = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrinho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // dgvCarrinho
            // 
            this.dgvCarrinho.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.dgvCarrinho.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvCarrinho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCarrinho.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvCarrinho.Location = new System.Drawing.Point(289, 336);
            this.dgvCarrinho.Name = "dgvCarrinho";
            this.dgvCarrinho.Size = new System.Drawing.Size(850, 559);
            this.dgvCarrinho.TabIndex = 1;
            this.dgvCarrinho.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-20, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1920, 1080);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.lblSubtotal.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtotal.Location = new System.Drawing.Point(1274, 441);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(113, 31);
            this.lblSubtotal.TabIndex = 2;
            this.lblSubtotal.Text = "Subtotal";
            this.lblSubtotal.Click += new System.EventHandler(this.lblSubtotal_Click);
            // 
            // lblEntrega
            // 
            this.lblEntrega.AutoSize = true;
            this.lblEntrega.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.lblEntrega.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntrega.Location = new System.Drawing.Point(1274, 490);
            this.lblEntrega.Name = "lblEntrega";
            this.lblEntrega.Size = new System.Drawing.Size(199, 31);
            this.lblEntrega.TabIndex = 9;
            this.lblEntrega.Text = "Taxa de entrega";
            this.lblEntrega.Click += new System.EventHandler(this.lblEntrega_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.lblTotal.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(1274, 550);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(75, 31);
            this.lblTotal.TabIndex = 10;
            this.lblTotal.Text = "Total";
            this.lblTotal.Click += new System.EventHandler(this.lblTotal_Click);
            // 
            // btnRemover
            // 
            this.btnRemover.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.btnRemover.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemover.ForeColor = System.Drawing.Color.Black;
            this.btnRemover.Location = new System.Drawing.Point(1357, 782);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(116, 23);
            this.btnRemover.TabIndex = 11;
            this.btnRemover.Text = "Remover item";
            this.btnRemover.UseVisualStyleBackColor = false;
         //   this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.btnFinalizar.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.Location = new System.Drawing.Point(1357, 811);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(116, 23);
            this.btnFinalizar.TabIndex = 12;
            this.btnFinalizar.Text = "Finalizar pedido";
            this.btnFinalizar.UseVisualStyleBackColor = false;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // chkEntrega
            // 
            this.chkEntrega.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.chkEntrega.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEntrega.ForeColor = System.Drawing.Color.Black;
            this.chkEntrega.Location = new System.Drawing.Point(1289, 710);
            this.chkEntrega.Name = "chkEntrega";
            this.chkEntrega.Size = new System.Drawing.Size(104, 27);
            this.chkEntrega.TabIndex = 15;
            this.chkEntrega.Text = "Entrega";
            this.chkEntrega.UseVisualStyleBackColor = false;
            // 
            // chkRetira
            // 
            this.chkRetira.AutoSize = true;
            this.chkRetira.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.chkRetira.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRetira.Location = new System.Drawing.Point(1449, 711);
            this.chkRetira.Name = "chkRetira";
            this.chkRetira.Size = new System.Drawing.Size(81, 26);
            this.chkRetira.TabIndex = 14;
            this.chkRetira.Text = "Retira";
            this.chkRetira.UseVisualStyleBackColor = false;
            this.chkRetira.CheckedChanged += new System.EventHandler(this.chkRetira_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbDebito);
            this.groupBox1.Controls.Add(this.rbCredito);
            this.groupBox1.Controls.Add(this.rbPix);
            this.groupBox1.Location = new System.Drawing.Point(1280, 602);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 78);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Forma de Pagamento";
            // 
            // rbPix
            // 
            this.rbPix.AutoSize = true;
            this.rbPix.Location = new System.Drawing.Point(6, 20);
            this.rbPix.Name = "rbPix";
            this.rbPix.Size = new System.Drawing.Size(39, 17);
            this.rbPix.TabIndex = 0;
            this.rbPix.TabStop = true;
            this.rbPix.Text = "Pix";
            this.rbPix.UseVisualStyleBackColor = true;
            // 
            // rbCredito
            // 
            this.rbCredito.AutoSize = true;
            this.rbCredito.Location = new System.Drawing.Point(6, 43);
            this.rbCredito.Name = "rbCredito";
            this.rbCredito.Size = new System.Drawing.Size(105, 17);
            this.rbCredito.TabIndex = 1;
            this.rbCredito.TabStop = true;
            this.rbCredito.Text = "Cartão de Débito";
            this.rbCredito.UseVisualStyleBackColor = true;
            // 
            // rbDebito
            // 
            this.rbDebito.AutoSize = true;
            this.rbDebito.Location = new System.Drawing.Point(144, 20);
            this.rbDebito.Name = "rbDebito";
            this.rbDebito.Size = new System.Drawing.Size(107, 17);
            this.rbDebito.TabIndex = 3;
            this.rbDebito.TabStop = true;
            this.rbDebito.Text = "Cartão de Crédito";
            this.rbDebito.UseVisualStyleBackColor = true;
            // 
            // pedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1061);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkRetira);
            this.Controls.Add(this.chkEntrega);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblEntrega);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.dgvCarrinho);
            this.Controls.Add(this.pictureBox1);
            this.Name = "pedidos";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.pedidos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrinho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dgvCarrinho;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblEntrega;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.CheckBox chkEntrega;
        private System.Windows.Forms.CheckBox chkRetira;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbDebito;
        private System.Windows.Forms.RadioButton rbCredito;
        private System.Windows.Forms.RadioButton rbPix;
    }
}