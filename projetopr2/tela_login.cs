using Microsoft.VisualBasic;
using projetopr2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace projetopr2
{
    public partial class tela_login : Form
    {
        public tela_login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Mostrar informações do usuário
            labelNome.Text = SessaoUsuario.Nome;
            labelEmail.Text = SessaoUsuario.Email;

            // Carregar endereços
            CarregarEnderecos();
        }

        private void CarregarEnderecos()
        {
            listBoxEnderecos.Items.Clear();
            try
            {

                using (var conn = new SqlConnection(@"Data Source=SQLEXPRESS;Initial Catalog=cj3027724pr2;User ID=aluno;Password=aluno"))
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT * FROM Enderecos WHERE IdCliente=@id", conn);
                    cmd.Parameters.AddWithValue("@id", SessaoUsuario.Id);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string endereco = $"{reader["Rua"]}, {reader["Numero"]} - {reader["Cidade"]}";
                        listBoxEnderecos.Items.Add(endereco);
                    }
                }
                string connetionString = @"Data Source=sqlexpress;Initial Catalog=cj3027724pr2;User ID=aluno;Password=aluno";
                SqlConnection cnn;
                
                cnn = new SqlConnection(connetionString);
                cnn.Open();

                cnn.Close();
            }
            catch (SqlException erro)
            {
                MessageBox.Show("Erro ao se conectar no banco de dados \n" +
                "Verifique os dados informados" + erro);
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string novoEndereco = Interaction.InputBox("Digite o endereço no formato: Rua, Número - Cidade", "Adicionar Endereço");


            if (!string.IsNullOrWhiteSpace(novoEndereco)) ;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            criar_conta form2 = new criar_conta();

            // Abre o Form3
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string email = textBox4.Text.Trim();
            string senha = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Por favor, preencha email e senha.");
                return;
            }

            // String de conexão - ajuste para seu servidor e banco
            string connectionString = @"Data Source=sqlexpress;Initial Catalog=cj3027724pr2; User ID=aluno; Password=aluno";

            using (SqlConnection conn = new SqlConnection(connectionString))

            {
                try
                {
                    conn.Open();

                    // Query parametrizada para evitar SQL Injection
                    string sql = "SELECT COUNT(*) FROM cadastro WHERE email = @Email AND  senha = @Senha";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Senha", senha);

                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {

                            cmd.Parameters.AddWithValue("@id", SessaoUsuario.Id);
                            //cmd.Parameters.AddWithValue("@rua", partes[0].Trim());
                            //cmd.Parameters.AddWithValue("@numero", partes[1].Trim());
                            //cmd.Parameters.AddWithValue("@cidade", partes[2].Trim());
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Login realizado com sucesso!");
                            // Aqui você pode abrir a próxima tela ou continuar o fluxo
                            tela_inicial form3 = new tela_inicial();
                            form3.Show();

                            // Oculta a tela de login
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Email ou senha inválidos.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados: " + ex.Message);

                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            redefinir_senha form5 = new redefinir_senha();
            form5.Show();
            this.Hide();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tela_inicial form7 = new tela_inicial();
            form7.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)

        {
            if (listBoxEnderecos.SelectedItem != null)
            {
                string enderecoSelecionado = listBoxEnderecos.SelectedItem.ToString();
                var confirm = MessageBox.Show($"Deseja remover o endereço:\n{enderecoSelecionado}?", "Remover Endereço", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        using (var conn = new SqlConnection(@"Data Source=SQLEXPRESS;Initial Catalog=cj3027724pr2;User ID=aluno;Password=aluno"))
                        {
                            conn.Open();
                            var partes = enderecoSelecionado.Split(new char[] { ',', '-' }, StringSplitOptions.RemoveEmptyEntries);
                            if (partes.Length == 3)
                            {
                                var cmd = new SqlCommand(
                                    "DELETE FROM Enderecos WHERE IdCliente=@id AND Rua=@rua AND Numero=@numero AND Cidade=@cidade", conn);
                                cmd.Parameters.AddWithValue("@id", SessaoUsuario.Id);
                                cmd.Parameters.AddWithValue("@rua", partes[0].Trim());
                                cmd.Parameters.AddWithValue("@numero", partes[1].Trim());
                                cmd.Parameters.AddWithValue("@cidade", partes[2].Trim());
                                cmd.ExecuteNonQuery();

                                CarregarEnderecos();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao remover endereço: " + ex.Message);
                    }
                }
            }
        }

        //private void Form1_Load(object sender, EventArgs e)
        //{

        //}

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}

