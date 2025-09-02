using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace projetopr2
{
    public partial class Form6 : Form
    {
        // Variáveis para armazenar dados recebidos do Form2
        private string _username;
        private string _email;
        private string _password;

        // Construtor que recebe os dados do Form2
        public Form6(string username, string email, string password)
        {
            InitializeComponent();

            _username = username;
            _email = email;
            _password = password;
        }

        // Evento do botão para salvar endereço, bairro e telefone e inserir no banco
        private void buttoncc_Click(object sender, EventArgs e)
        {
            string enderecoDigitado = endereco.Text.Trim();
            string bairroDigitado = bairro.Text.Trim();
            string telefoneDigitado = telefone.Text.Trim();

            if (string.IsNullOrEmpty(enderecoDigitado) || string.IsNullOrEmpty(bairroDigitado) || string.IsNullOrEmpty(telefoneDigitado))
            {
                MessageBox.Show("Por favor, preencha endereço, bairro e telefone.");
                return;
            }

            string connectionString = @"Data Source=sqlexpress;Initial Catalog=cj3027724pr2;User ID=aluno;Password=aluno";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"INSERT INTO cadastro (nome, email, senha, endereco, bairro, telefone) 
                                   VALUES (@Nome, @Email, @Senha, @Endereco, @Bairro, @Telefone)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nome", _username);
                        cmd.Parameters.AddWithValue("@Email", _email);
                        cmd.Parameters.AddWithValue("@Senha", _password);
                        cmd.Parameters.AddWithValue("@Endereco", enderecoDigitado);
                        cmd.Parameters.AddWithValue("@Bairro", bairroDigitado);
                        cmd.Parameters.AddWithValue("@Telefone", telefoneDigitado);

                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            MessageBox.Show("Cadastro concluído com sucesso!");
                            this.Close(); // Fecha o Form6
                        }
                        else
                        {
                            MessageBox.Show("Erro ao concluir cadastro.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar/inserir no banco: " + ex.Message);
            }
        }
    }
}

