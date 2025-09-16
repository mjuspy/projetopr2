using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace projetopr2
{
    public partial class confirmacaosenha : Form
    {
        private string emailUsuario;

        public confirmacaosenha(string email)
        {
            InitializeComponent();
            emailUsuario = email;
        }

        private void buttonConfirmar_Click(object sender, EventArgs e)
        {
            string tokenDigitado = textBoxToken.Text.Trim();

            if (string.IsNullOrEmpty(tokenDigitado))
            {
                MessageBox.Show("Digite o código enviado por e-mail.");
                return;
            }

            try
            {
                using (var conn = new SqlConnection(@"Data Source=SQLEXPRESS;Initial Catalog=cj3027724pr2;User ID=aluno;Password=aluno"))
                {
                    conn.Open();

                    // Verifica se o token é válido
                    var cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM cadastro WHERE Email=@Email AND TokenConfirmacao=@Token", conn);

                    cmd.Parameters.AddWithValue("@Email", emailUsuario);
                    cmd.Parameters.AddWithValue("@Token", tokenDigitado);

                    int resultado = (int)cmd.ExecuteScalar();

                    if (resultado > 0)
                    {
                        // Atualiza EmailConfirmado
                        var cmdUpdate = new SqlCommand(
                            "UPDATE cadastro SET EmailConfirmado=1 WHERE Email=@Email", conn);
                        cmdUpdate.Parameters.AddWithValue("@Email", emailUsuario);
                        cmdUpdate.ExecuteNonQuery();

                        MessageBox.Show("E-mail confirmado com sucesso!");

                        // Abre a tela inicial
                        tela_inicial telaInicial = new tela_inicial();
                        telaInicial.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Código incorreto. Verifique o e-mail.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao confirmar: " + ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // voltar para a tela anterior se desejar
        }
    }
}
