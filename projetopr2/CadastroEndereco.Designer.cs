namespace projetopr2
{
    partial class CadastroEndereco
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnSalvarEndereco = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(151, 110);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(524, 108);
            this.listBox1.TabIndex = 0;
            // 
            // btnSalvarEndereco
            // 
            this.btnSalvarEndereco.Location = new System.Drawing.Point(151, 234);
            this.btnSalvarEndereco.Name = "btnSalvarEndereco";
            this.btnSalvarEndereco.Size = new System.Drawing.Size(123, 23);
            this.btnSalvarEndereco.TabIndex = 1;
            this.btnSalvarEndereco.Text = "Usar esse endereço";
            this.btnSalvarEndereco.UseVisualStyleBackColor = true;
            this.btnSalvarEndereco.Click += new System.EventHandler(this.btnSalvarEndereco_Click);
            // 
            // CadastroEndereco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSalvarEndereco);
            this.Controls.Add(this.listBox1);
            this.Name = "CadastroEndereco";
            this.Text = "Form3";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnSalvarEndereco;
    }
}