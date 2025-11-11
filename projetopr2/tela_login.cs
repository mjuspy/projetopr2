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

namespace projetopr2
{
    public partial class tela_login : Form
    {
        // 🔹 Conexão antiga (autenticação SQL) — mantida comentada
        //string connectionStringWindows = @"Data Source=SQLEXPRESS;Initial Catalog=cj3027724pr2;User ID=aluno;Password=aluno";

        // 🔹 Nova conexão (autenticação do Windows)
        string connectionStringWindows = @"Data Source=PCZAO;Initial Catalog=cj3027724pr2;Integrated Security=True;";

        public tela_login()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged_1(object sender, EventArgs e) { }
        private void pictureBox1_Click_1(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Mostrar informações do usuário
            labelNome.Text = SessaoUsuario.Nome;
            labelEmail.Text = SessaoUsuario.Email;

            // Carregar endereços
            //CarregarEnderecos();
        }

        //private void CarregarEnderecos()
        //{
        //    listBoxEnderecos.Items.Clear();

        //    try
        //    {
        //        // 🔹 Conexão com autenticação do Windows
        //        using (var conn = new SqlConnection(connectionStringWindows))
        //        {
        //            conn.Open();
        //            var cmd = new SqlCommand("SELECT * FROM Enderecos WHERE cod_cliente=@id", conn);
        //            cmd.Parameters.AddWithValue("@id", SessaoUsuario.Id);

        //            var reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                string endereco = $"{reader["Rua"]}, {reader["Numero"]} - {reader["Cidade"]}";
        //                listBoxEnderecos.Items.Add(endereco);
        //            }
        //        }

        //        // 🔸 Código antigo de conexão (mantido comentado)
        //        /*
        //        using (var conn = new SqlConnection(@"Data Source=SQLEXPRESS;Initial Catalog=cj3027724pr2;User ID=aluno;Password=aluno"))
        //        {
        //            conn.Open();
        //            var cmd = new SqlCommand("SELECT * FROM Enderecos WHERE cod_cliente=@id", conn);
        //            cmd.Parameters.AddWithValue("@id", SessaoUsuario.Id);
        //            var reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                string endereco = $"{reader["Rua"]}, {reader["Numero"]} - {reader["Cidade"]}";
        //                listBoxEnderecos.Items.Add(endereco);
        //            }
        //        }
        //        */
        //    }
        //    catch (SqlException erro)
        //    {
        //        MessageBox.Show("Erro ao se conectar no banco de dados.\nVerifique os dados informados.\n\n" + erro.Message);
        //    }
        //}

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string novoEndereco = Interaction.InputBox("Digite o endereço no formato: Rua, Número - Cidade", "Adicionar Endereço");

            if (!string.IsNullOrWhiteSpace(novoEndereco))
            {
                MessageBox.Show($"Endereço informado: {novoEndereco}");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            criar_conta form2 = new criar_conta();
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

            // 🔹 Verificação de login do FUNCIONÁRIO
            if (email.Equals("funcionario", StringComparison.OrdinalIgnoreCase) && senha == "cienfleur123")
            {
                MessageBox.Show("Login de funcionário realizado com sucesso!");

                // Abre o painel administrativo
                funcionarios painelFuncionario = new funcionarios();
                painelFuncionario.Show();
                this.Hide();
                return;
            }

            // 🔹 Login normal de CLIENTE
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionStringWindows))
                {
                    conn.Open();

                    string sql = "SELECT [cod_cliente], [nome], [email] FROM [cadastro] WHERE [email] = @Email AND [senha] = @Senha";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Senha", senha);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            Usuario usuarioLogado = new Usuario
                            {
                                Id = Convert.ToInt32(reader["cod_cliente"]),
                                Nome = reader["nome"].ToString(),
                                Email = reader["email"].ToString()
                            };

                            SessaoUsuario1.Login(usuarioLogado);

                            MessageBox.Show($"Bem-vindo(a), {usuarioLogado.Nome}! 💐");

                            tela_inicial telaInicial = new tela_inicial();
                            telaInicial.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Email ou senha inválidos.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar ao banco de dados: " + ex.Message);
            }
        }


        // 🔸 Código antigo de conexão (mantido comentado)
        /*
        using (SqlConnection conn = new SqlConnection(@"Data Source=SQLEXPRESS;Initial Catalog=cj3027724pr2;User ID=aluno;Password=aluno"))
        {
            conn.Open();
            string sql = "SELECT COUNT(*) FROM cadastro WHERE Email = @Email AND Senha = @Senha";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Senha", senha);
                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Login realizado com sucesso!");
                    tela_inicial form3 = new tela_inicial();
                    form3.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Email ou senha inválidos.");
                }
            }
        }
        */
        //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Erro ao conectar ao banco de dados: " + ex.Message);
        //    }
        //}

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
                        // 🔹 Nova conexão com autenticação do Windows
                        using (var conn = new SqlConnection(connectionStringWindows))
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

                                //CarregarEnderecos();
                            }
                        }

                        // 🔸 Código antigo de conexão (mantido comentado)
                        /*
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
                        */
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao remover endereço: " + ex.Message);
                    }
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
