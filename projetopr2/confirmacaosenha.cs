using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace projetopr2
{
    public partial class confirmacaosenha : Form
    {
        private string nomeUsuario;
        private string emailUsuario;
        private string senhaUsuario;
        private string tokenGerado;

        // >>> Construtor novo que aceita 4 argumentos (nome, email, senha, token)
        public confirmacaosenha(string nome, string email, string senha, string token)
        {
            InitializeComponent();
            nomeUsuario = nome;
            emailUsuario = email;
            senhaUsuario = senha;
            tokenGerado = token;
        }

        // Se você também precisar manter o construtor padrão (designer), pode deixar:
        public confirmacaosenha()
        {
            InitializeComponent();
        }
        // <<< fim dos construtores

        private void confimacaosenha_Load(object sender, EventArgs e)
        {
            // Você pode, se quiser, mostrar parte do email na tela:
            // labelInfo.Text = $"Código enviado para: {emailUsuario}";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // evento do designer (vazio por enquanto)
        }

        private void buttonConfirmar_Click(object sender, EventArgs e)
        {
            string tokenDigitado = textBoxToken.Text.Trim();

            if (string.IsNullOrEmpty(tokenDigitado))
            {
                MessageBox.Show("Digite o código enviado por e-mail.");
                return;
            }

            if (tokenDigitado == tokenGerado)
            {
                try
                {
                    using (var conn = new SqlConnection(@"Data Source=PCZAO;Initial Catalog=cj3027724pr2;Integrated Security=True;"))
                    {
                        conn.Open();

                        // Antes de inserir, opcionalmente verifique se o email já não existe
                        var checkCmd = new SqlCommand("SELECT COUNT(*) FROM cadastro WHERE Email = @Email", conn);
                        checkCmd.Parameters.AddWithValue("@Email", emailUsuario);
                        int exists = (int)checkCmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            MessageBox.Show("Este e-mail já está cadastrado.");
                            return;
                        }

                        // Insere usuário confirmado
                        var cmd = new SqlCommand(
                            "INSERT INTO cadastro (Nome, Email, Senha, EmailConfirmado, TokenConfirmacao) VALUES (@Nome, @Email, @Senha, 1, @Token)",
                            conn);

                        cmd.Parameters.AddWithValue("@Nome", nomeUsuario);
                        cmd.Parameters.AddWithValue("@Email", emailUsuario);
                        cmd.Parameters.AddWithValue("@Senha", senhaUsuario);
                        cmd.Parameters.AddWithValue("@Token", tokenGerado);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Conta confirmada e criada com sucesso!");

                    tela_login telaLogin = new tela_login();
                    telaLogin.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao criar conta: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Código incorreto. Verifique o e-mail.");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
                     // Se quiser voltar ao login sem confirmar:
            tela_login login = new tela_login();
            login.Show();
            this.Hide();
        }
    }
}
