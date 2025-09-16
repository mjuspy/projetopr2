using System;
using System;
using System.Windows.Forms;

namespace projetopr2
{
    public partial class criar_conta : Form
    {
        public criar_conta()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string email = textBox2.Text.Trim();
            string password = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return;
            }

            // Passa os dados para o Form6 e abre ele
            entrega form6 = new entrega(username, email, password);
            form6.Show();

            // Esconde o Form2
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            tela_login form1 = new tela_login();
            form1.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Se precisar colocar algo aqui, fica à vontade
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Se precisar colocar algo aqui, fica à vontade
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Se precisar colocar algo aqui, fica à vontade
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
