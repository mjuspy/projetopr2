using System;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Windows.Forms;

namespace projetopr2
{
    public partial class criar_conta : Form
    {
        // ✅ String de conexão com autenticação do Windows
        string conexaoString = @"Data Source=PCZAO;Initial Catalog=cj3027724pr2;Integrated Security=True;";

        public criar_conta()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string email = textBox2.Text.Trim();
            string password = textBox3.Text.Trim();
            string confirmarSenha = textBox4.Text.Trim();

            // 🔸 Validação básica
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmarSenha))
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return;
            }

            if (password != confirmarSenha)
            {
                MessageBox.Show("As senhas não coincidem.");
                return;
            }

            // 🔹 Gera código de confirmação (token)
            Random rnd = new Random();
            string tokenConfirmacao = rnd.Next(100000, 999999).ToString();

            try
            {
                // ✅ Salva no banco de dados com autenticação do Windows
                using (SqlConnection conn = new SqlConnection(conexaoString))
                {
                    conn.Open();
                    string query = @"INSERT INTO cadastro 
                                    (Nome, Email, Senha, EmailConfirmado, TokenConfirmacao) 
                                    VALUES (@nome, @email, @senha, 0, @token)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", username);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@senha", password); // ⚠️ depois usar hash
                        cmd.Parameters.AddWithValue("@token", tokenConfirmacao);
                        cmd.ExecuteNonQuery();
                    }
                }

                // ✅ Envia email com o código de confirmação
                EnviarEmailConfirmacao(email, username, tokenConfirmacao);

                MessageBox.Show($"Conta criada! Um código de confirmação foi enviado para {email}.");

                // ✅ Abre a tela de confirmação (Form)
                confimacaosenha frm = new confimacaosenha(email);
                frm.ShowDialog();

                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao criar conta: " + ex.Message);
            }
        }

        // 🔹 Envio do email
        private void EnviarEmailConfirmacao(string email, string nome, string token)
        {
            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new System.Net.NetworkCredential(
                    "cienfleuroux@gmail.com",      // seu e-mail
                    "nekc osbg gkcy ajqo"          // senha de app (não a senha normal)
                );
                smtp.EnableSsl = true;

                var mail = new MailMessage("cienfleuroux@gmail.com", email);
                mail.Subject = "Confirme sua conta - Cien Fleur";

                mail.Body = $@"
<html>
<body style='font-family: Arial, sans-serif; background-color:#f9f9f9; padding:20px;'>
  <div style='max-width:600px; margin:auto; background-color:#ffffff; border-radius:10px; padding:30px; text-align:center; box-shadow:0 0 10px rgba(0,0,0,0.1);'>
    <h2 style='color:#6b4226;'>Bem-vindo ao Cien Fleur ☕</h2>
    <p>Olá <strong>{nome}</strong>,</p>
    <p>Para concluir seu cadastro, use o código de verificação abaixo:</p>
    <h1 style='background-color:#ffdf7e; color:#6b4226; padding:15px; border-radius:10px; display:inline-block;'>{token}</h1>
    <p>Digite esse código na tela de confirmação para ativar sua conta.</p>
    <hr style='margin:20px 0;'/>
    <p style='font-size:12px; color:#888888;'>Se você não solicitou essa conta, ignore este e-mail.</p>
    <p style='font-size:12px; color:#888888;'>Equipe Cien Fleur ☕</p>
  </div>
</body>
</html>";

                mail.IsBodyHtml = true;
                smtp.Send(mail);
            }
        }

        // 🔹 Botão para voltar ao login
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            tela_login form1 = new tela_login();
            form1.Show();
            this.Hide();
        }

        // 🔸 Eventos vazios do Designer
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void pictureBox2_Click_1(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
    }
}
