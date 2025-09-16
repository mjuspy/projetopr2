namespace projetopr2
{
    partial class confirmacaosenha
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(confirmacaosenha));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.confirmarcod = new System.Windows.Forms.Button();
            this.codigoconfirm = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-8, -9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1920, 1080);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // confirmarcod
            // 
            this.confirmarcod.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmarcod.Location = new System.Drawing.Point(880, 683);
            this.confirmarcod.Name = "confirmarcod";
            this.confirmarcod.Size = new System.Drawing.Size(153, 38);
            this.confirmarcod.TabIndex = 1;
            this.confirmarcod.Text = "Confirmar";
            this.confirmarcod.UseVisualStyleBackColor = true;
            // 
            // codigoconfirm
            // 
            this.codigoconfirm.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codigoconfirm.Location = new System.Drawing.Point(728, 572);
            this.codigoconfirm.Multiline = true;
            this.codigoconfirm.Name = "codigoconfirm";
            this.codigoconfirm.Size = new System.Drawing.Size(454, 54);
            this.codigoconfirm.TabIndex = 2;
            // 
            // confirmacaosenha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1061);
            this.Controls.Add(this.codigoconfirm);
            this.Controls.Add(this.confirmarcod);
            this.Controls.Add(this.pictureBox1);
            this.Name = "confirmacaosenha";
            this.Text = "Form7";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button confirmarcod;
        private System.Windows.Forms.TextBox codigoconfirm;
    }
}