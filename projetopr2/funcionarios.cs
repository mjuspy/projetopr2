using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace projetopr2
{
    public partial class funcionarios : Form
    {
        string connectionString = @"Data Source=PCZAO;Initial Catalog=cj3027724pr2;Integrated Security=True;";

        public funcionarios()
        {
            InitializeComponent();
        }

        private void funcionarios_Load(object sender, EventArgs e)
        {
            CarregarPedidos();
            EstilizarDataGridView();

            dgvPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPedidos.MultiSelect = false;
            dgvPedidos.ReadOnly = true;

            cmbStatus.Items.AddRange(new string[] {
                "Pagamento pendente",
                "Pagamento aprovado",
                "Em preparo",
                "Saiu para entrega",
                "Entregue",
                "Cancelado"
            });
        }

        // 🎨 Estilo do DataGridView
        private void EstilizarDataGridView()
        {
            dgvPedidos.BorderStyle = BorderStyle.None;
            dgvPedidos.BackgroundColor = ColorTranslator.FromHtml("#fadfb5");
            dgvPedidos.EnableHeadersVisualStyles = false;

            dgvPedidos.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#f2cfa0");
            dgvPedidos.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#3a2e2a");
            dgvPedidos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvPedidos.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#fff7e6");
            dgvPedidos.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#3a2e2a");
            dgvPedidos.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#f7d9a5");
            dgvPedidos.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#3a2e2a");
            dgvPedidos.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dgvPedidos.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#faebd7");
            dgvPedidos.GridColor = ColorTranslator.FromHtml("#e5c99b");

            dgvPedidos.RowHeadersVisible = false;
            dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // 📦 Carrega pedidos
        private void CarregarPedidos()
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                string query = @"SELECT 
                                ID_pedido, 
                                Nome_cliente, 
                                FormaPagamento, 
                                Status_pedido, 
                                Total, 
                                Data_pedido, 
                                Email_cliente 
                             FROM Pedidos";

                SqlDataAdapter da = new SqlDataAdapter(query, conexao);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPedidos.DataSource = dt;
            }

            if (dgvPedidos.Columns.Contains("ID_pedido"))
                dgvPedidos.Columns["ID_pedido"].HeaderText = "ID";
        }

        // 🔄 Atualiza status
        private void btnAtualizarStatus_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.CurrentRow == null)
            {
                MessageBox.Show("Selecione um pedido clicando sobre a linha desejada.");
                return;
            }

            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Selecione um novo status.");
                return;
            }

            int pedidoID = Convert.ToInt32(dgvPedidos.CurrentRow.Cells["ID_pedido"].Value);
            string novoStatus = cmbStatus.SelectedItem.ToString();

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                string query = "UPDATE Pedidos SET Status_pedido = @Status WHERE ID_pedido = @ID";
                SqlCommand cmd = new SqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@Status", novoStatus);
                cmd.Parameters.AddWithValue("@ID", pedidoID);

                conexao.Open();
                int linhasAfetadas = cmd.ExecuteNonQuery();
                conexao.Close();

                if (linhasAfetadas > 0)
                {
                    MessageBox.Show("Status atualizado com sucesso!");
                    CarregarPedidos();
                }
                else
                {
                    MessageBox.Show("Erro: Pedido não encontrado.");
                }
            }
        }

        // ☕ Envia e-mail estilizado
        private void btnEnviarEmail_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.CurrentRow == null)
            {
                MessageBox.Show("Selecione um pedido para enviar o e-mail.");
                return;
            }

            string emailCliente = dgvPedidos.CurrentRow.Cells["Email_cliente"].Value.ToString();
            string nomeCliente = dgvPedidos.CurrentRow.Cells["Nome_cliente"].Value.ToString();
            string status = dgvPedidos.CurrentRow.Cells["Status_pedido"].Value.ToString();

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("tuaempresa@gmail.com", "Cien Fleur Cafeteria");
                mail.To.Add(emailCliente);
                mail.Subject = "☕ Atualização do seu pedido - Cien Fleur";

                mail.Body = $@"
                <html>
                <body style='font-family: Segoe UI, sans-serif; background-color: #fffaf0; color: #4b3a2f; padding: 20px;'>
                    <div style='max-width: 600px; margin: auto; background: #fef6e4; border-radius: 12px; padding: 30px; box-shadow: 0 0 8px #e0c9a6;'>
                        <h2 style='color: #6b4e3d; text-align:center;'>Cien Fleur Cafeteria</h2>
                        <p style='font-size: 16px;'>Olá!, <b>{nomeCliente}</b> ☕💐</p>
                        <p>Seu pedido foi atualizado com o novo status:</p>
                        <div style='background-color:#faebd7; padding:10px; border-radius:8px; text-align:center; font-size:16px;'>
                            <b>{status}</b>
                        </div>
                        <p style='margin-top:20px;'>Agradecemos pela sua preferência e esperamos vê-lo novamente em breve!</p>
                        <p style='font-size:14px; color:#7a6653;'>Avec amour,<br><b>Equipe Cien Fleur</b> 🌷</p>
                    </div>
                </body>
                </html>";

                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("cienfleuroux@gmail.com", "nekc osbg gkcy ajqo");
                smtp.EnableSsl = true;
                smtp.Send(mail);

                MessageBox.Show("E-mail enviado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao enviar e-mail: " + ex.Message);
            }
        }

        // 📊 Relatório de vendas
        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                string query = @"SELECT Nome_cliente, FormaPagamento, Total, Data_pedido, Status_pedido FROM Pedidos";
                SqlCommand cmd = new SqlCommand(query, conexao);
                conexao.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                StringBuilder relatorio = new StringBuilder();
                relatorio.AppendLine("=== RELATÓRIO DE VENDAS - CIEN FLEUR ===\n");

                while (reader.Read())
                {
                    relatorio.AppendLine($"Cliente: {reader["Nome_cliente"]}");
                    relatorio.AppendLine($"Método: {reader["FormaPagamento"]}");
                    relatorio.AppendLine($"Valor: R$ {reader["Total"]}");
                    relatorio.AppendLine($"Data: {reader["Data_pedido"]}");
                    relatorio.AppendLine($"Status: {reader["Status_pedido"]}");
                    relatorio.AppendLine("----------------------------------------");
                }

                reader.Close();
                conexao.Close();

                string caminho = Path.Combine(Application.StartupPath, "relatorio_vendas.txt");
                File.WriteAllText(caminho, relatorio.ToString());

                MessageBox.Show($"Relatório gerado com sucesso!\n\nArquivo salvo em:\n{caminho}");
            }
        }

        // 🖱️ Corrige a seleção de linha no clique da célula
        private void dgvPedidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                dgvPedidos.CurrentCell = dgvPedidos.Rows[e.RowIndex].Cells[e.ColumnIndex];
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}
