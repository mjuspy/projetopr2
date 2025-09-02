using System;
using System.Drawing;
using System.Windows.Forms;

namespace projetopr2
{
    public partial class Form4 : Form
    {
        private Panel catalogPanel;

        public Form4()
        {
            InitializeComponent();

            catalogPanel = new Panel();
            catalogPanel.AutoScroll = true;
            catalogPanel.Dock = DockStyle.Fill;

            this.Controls.Add(catalogPanel);
        }

       
            private void Form4_Load(object sender, EventArgs e)
        {
            int y = 10;
            for (int i = 1; i <= 30; i++)
            {
                Label lbl = new Label();
                lbl.Text = $"Produto {i}";
                lbl.Location = new Point(10, y);
                lbl.AutoSize = true;
                catalogPanel.Controls.Add(lbl);
                y += 30; // espaço vertical entre os labels
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

