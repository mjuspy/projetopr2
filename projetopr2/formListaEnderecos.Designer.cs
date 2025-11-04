namespace projetopr2
{
    partial class formListaEnderecos
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
            this.dgvEnderecos = new System.Windows.Forms.DataGridView();
            this.btnFechar = new System.Windows.Forms.Button();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnderecos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEnderecos
            // 
            this.dgvEnderecos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEnderecos.Location = new System.Drawing.Point(96, 55);
            this.dgvEnderecos.MultiSelect = false;
            this.dgvEnderecos.Name = "dgvEnderecos";
            this.dgvEnderecos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEnderecos.Size = new System.Drawing.Size(390, 273);
            this.dgvEnderecos.TabIndex = 0;
            this.dgvEnderecos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEnderecos_CellContentClick);
            // 
            // btnFechar
            // 
            this.btnFechar.Location = new System.Drawing.Point(96, 26);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(75, 23);
            this.btnFechar.TabIndex = 1;
            this.btnFechar.Text = "Fechar tela";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Location = new System.Drawing.Point(209, 334);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(75, 23);
            this.btnSelecionar.TabIndex = 2;
            this.btnSelecionar.Text = "Selecionar";
            this.btnSelecionar.UseVisualStyleBackColor = true;
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(96, 334);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(107, 23);
            this.btnAdicionar.TabIndex = 3;
            this.btnAdicionar.Text = "Adicionar novo";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // formListaEnderecos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.btnSelecionar);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.dgvEnderecos);
            this.Name = "formListaEnderecos";
            this.Text = "formListaEnderecos";
            this.Load += new System.EventHandler(this.formListaEnderecos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnderecos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEnderecos;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnSelecionar;
        private System.Windows.Forms.Button btnAdicionar;
    }
}