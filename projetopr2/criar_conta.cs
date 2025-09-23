using System;
using System.Data.SqlClient;
using System.Net.Mail;
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
            string confirmarSenha = textBox4.Text.Trim();

            // Validação de campos
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

            // Gera código de 6 dígitos
            Random rnd = new Random();
            string tokenConfirmacao = rnd.Next(100000, 999999).ToString();

            try
        {
                // Salva no banco de dados
                using (var conn = new SqlConnection(@"Data Source=SQLEXPRESS;Initial Catalog=cj3027724pr2;User ID=aluno;Password=aluno"))
                {
                    conn.Open();
                    var cmd = new SqlCommand(
                        "INSERT INTO cadastro (Nome, Email, Senha, EmailConfirmado, TokenConfirmacao) " +
                        "VALUES (@nome, @email, @senha, 0, @token)", conn);

                    cmd.Parameters.AddWithValue("@nome", username);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", password); // ⚠️ futuramente use hash
                    cmd.Parameters.AddWithValue("@token", tokenConfirmacao);

                    cmd.ExecuteNonQuery();
        }

                // Envia email estilizado com HTML
                EnviarEmailConfirmacao(email, username, tokenConfirmacao);

                MessageBox.Show($"Conta criada! Um código de confirmação foi enviado para {email}.");

                // Abre a tela de confirmação de token
                confirmacaosenha confirmar = new confirmacaosenha(email);
                confirmar.Show();
                this.Hide();
            }
            catch (Exception ex)
        {
                MessageBox.Show("Erro ao criar conta: " + ex.Message);
            }
        }

        private void EnviarEmailConfirmacao(string email, string nome, string token)
        {
            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
        {
                smtp.Credentials = new System.Net.NetworkCredential(
                    "cienfleuroux@gmail.com", // seu e-mail
                    "nekc osbg gkcy ajqo"        // senha de app do Gmail
                );
                smtp.EnableSsl = true;

                var mail = new MailMessage("cienfleuroux@gmail.com", email);
                mail.Subject = "Confirme sua conta - Café do Dia";

                // Corpo em HTML
                mail.Body = $@"
<html>
<body style='font-family: Arial, sans-serif; background-color:#f9f9f9; padding:20px;'>
  <div style='max-width:600px; margin:auto; background-color:#ffffff; border-radius:10px; padding:30px; text-align:center; box-shadow:0 0 10px rgba(0,0,0,0.1);'>
    <h2 style='color:#6b4226;'>Bem-vindo ao CienFleur!!</h2>
    <p>Olá <strong>{nome}</strong>,</p>
    <p>Para concluir seu cadastro, use o código de verificação abaixo:</p>
    <h1 style='background-color:#ffdf7e; color:#6b4226; padding:15px; border-radius:10px; display:inline-block;'>{token}</h1>
    <p>Digite esse código na tela de confirmação para ativar sua conta.</p>
    <hr style='margin:20px 0;'/>
    <p style='font-size:12px; color:#888888;'>Se você não solicitou essa conta, ignore este e-mail.</p>
    <p style='font-size:12px; color:#888888;'>Equipe Cien Fleur ☕</p>
  </div>
</body>
</html>
";

                mail.IsBodyHtml = true;
                smtp.Send(mail);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            tela_login form1 = new tela_login();
            form1.Show();
            this.Hide();
        }

        // Eventos extras do Designer tratados como vazios
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void pictureBox2_Click_1(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
    }
}
