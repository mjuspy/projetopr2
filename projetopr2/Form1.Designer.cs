namespace projetopr2
{
    partial class Form1
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
            this.labelNome = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.listBoxEnderecos = new System.Windows.Forms.ListBox();
            this.buttonAdicionarEndereco = new System.Windows.Forms.Button();
            this.buttonRemoverEndereco = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelNome
            // 
            this.labelNome.AutoSize = true;
            this.labelNome.Location = new System.Drawing.Point(285, 106);
            this.labelNome.Name = "labelNome";
            this.labelNome.Size = new System.Drawing.Size(35, 13);
            this.labelNome.TabIndex = 0;
            this.labelNome.Text = "label1";
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(288, 137);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(35, 13);
            this.labelEmail.TabIndex = 1;
            this.labelEmail.Text = "label2";
            // 
            // listBoxEnderecos
            // 
            this.listBoxEnderecos.FormattingEnabled = true;
            this.listBoxEnderecos.Location = new System.Drawing.Point(328, 253);
            this.listBoxEnderecos.Name = "listBoxEnderecos";
            this.listBoxEnderecos.Size = new System.Drawing.Size(120, 95);
            this.listBoxEnderecos.TabIndex = 2;
            // 
            // buttonAdicionarEndereco
            // 
            this.buttonAdicionarEndereco.Location = new System.Drawing.Point(98, 95);
            this.buttonAdicionarEndereco.Name = "buttonAdicionarEndereco";
            this.buttonAdicionarEndereco.Size = new System.Drawing.Size(75, 23);
            this.buttonAdicionarEndereco.TabIndex = 3;
            this.buttonAdicionarEndereco.Text = "button1";
            this.buttonAdicionarEndereco.UseVisualStyleBackColor = true;
            // 
            // buttonRemoverEndereco
            // 
            this.buttonRemoverEndereco.Location = new System.Drawing.Point(98, 137);
            this.buttonRemoverEndereco.Name = "buttonRemoverEndereco";
            this.buttonRemoverEndereco.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoverEndereco.TabIndex = 4;
            this.buttonRemoverEndereco.Text = "button2";
            this.buttonRemoverEndereco.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonRemoverEndereco);
            this.Controls.Add(this.buttonAdicionarEndereco);
            this.Controls.Add(this.listBoxEnderecos);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.labelNome);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNome;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.ListBox listBoxEnderecos;
        private System.Windows.Forms.Button buttonAdicionarEndereco;
        private System.Windows.Forms.Button buttonRemoverEndereco;
    }
}