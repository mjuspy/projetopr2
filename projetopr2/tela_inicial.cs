using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
        pedidos form6 = new pedidos();
            form6.Show();
            this.Hide();
                }
private void btnVerPedidos_Click(object sender, EventArgs e)
        {
            if (SessaoUsuari.IsLoggedIn)
            {
                // Se o usuário está logado, abre a tela de pedidos.
                pedidos formPedidos = new pedidos();
                formPedidos.Show();
            }
            else
            {
                // Se não está logado, exibe a mensagem e abre a tela de login.
                MessageBox.Show("Para visualizar seus pedidos, efetue o login.", "Acesso Restrito", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                tela_login formLogin = new tela_login(); // Supondo que sua tela se chame tela_login
                formLogin.ShowDialog(); // ShowDialog impede o usuário de clicar em outras janelas.
            }
        }
    
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            tela_login form1 = new tela_login();
            form1.Show();
            this.Hide();
        }
            private void buttonPerfil_Click(object sender, EventArgs e)
        {
            if (!SessaoUsuario.Logado)
            {
                criar_conta criarConta = new criar_conta();
                criarConta.Show();
            }
            else
            {
               
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
    }
}
