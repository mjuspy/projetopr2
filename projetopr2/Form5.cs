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

namespace projetopr2
{
    public partial class Form5 : Form
    {
        // Coloque a string de conexão aqui
        private readonly string conexao = "Data Source=sqlexpress;Initial Catalog=cj3027724pr2;User ID=aluno;Password=aluno";

        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();
            string novaSenha = textBox4.Text;
            string confirmarSenha = textBox5.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(novaSenha) || string.IsNullOrEmpty(confirmarSenha))
            {
                MessageBox.Show("Preencha todos os campos.");
                return;
            }

            if (novaSenha != confirmarSenha)
            {
                MessageBox.Show("As senhas não coincidem.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(conexao))
                {
                    con.Open();

                    // Verifica se o email existe e busca a senha atual
                    string verificarEmail = "SELECT senha FROM cadastro WHERE email = @Email";
                    SqlCommand cmdVerificar = new SqlCommand(verificarEmail, con);
                    cmdVerificar.Parameters.AddWithValue("@Email", email);
                    object result = cmdVerificar.ExecuteScalar();

                    if (result == null)
                    {
                        MessageBox.Show("E-mail não encontrado.");
                        return;
                    }

                    string senhaAtual = result.ToString();

                    if (senhaAtual == novaSenha)
                    {
                        MessageBox.Show("A nova senha não pode ser igual à senha atual. Tente novamente!");
                        return;
                    }

                    // Atualiza a senha
                    string atualizarSenha = "UPDATE cadastro SET senha = @Senha WHERE email = @Email";
                    SqlCommand cmdAtualizar = new SqlCommand(atualizarSenha, con);
                    cmdAtualizar.Parameters.AddWithValue("@Senha", novaSenha);
                    cmdAtualizar.Parameters.AddWithValue("@Email", email);

                    cmdAtualizar.ExecuteNonQuery();

                    MessageBox.Show("Senha alterada com sucesso!");

                    Form1 form1 = new Form1();
                    form1.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
    }
}