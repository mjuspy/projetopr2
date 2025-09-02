using System;
using System;
using System.Windows.Forms;

namespace projetopr2
{
    public partial class Form2 : Form
    {
        public Form2()
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
            Form6 form6 = new Form6(username, email, password);
            form6.Show();

            // Esconde o Form2
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
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
    }
}
