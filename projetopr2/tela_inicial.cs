using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetopr2
{
    public partial class tela_inicial : Form
    {
        public tela_inicial()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            cardapio form4 = new cardapio();
            form4.Show();

            // Oculta a tela de login
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            confirmacaosenha form7 = new confirmacaosenha();
            form7.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            tela_login form1 = new tela_login();
            form1.Show();
            this.Hide();
        }
    }
}
