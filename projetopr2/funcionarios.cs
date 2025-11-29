using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing; // Necessário para o relatório gráfico
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
            CarregarPedidos(); // Já carrega com os filtros (ou sem nada no inicio)
            EstilizarDataGridView();

            dgvPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPedidos.MultiSelect = false;
            dgvPedidos.ReadOnly = true;

            // Preenche o combo de status
            cmbStatus.Items.AddRange(new string[] {
                "Pagamento pendente",
                "Pagamento aprovado",
                "Em preparo",
                "Saiu para entrega",
                "Entregue",
                "Cancelado"
            });
        }

        // 🔍 MÉTODO DE PESQUISA INTELIGENTE (FILTRA NOME E DATA)
        private void CarregarPedidos()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    // Começa pegando tudo
                    string query = @"SELECT 
                                        ID_pedido, 
                                        Nome_cliente, 
                                        FormaPagamento, 
                                        Status_pedido, 
                                        Total, 
                                        Data_pedido, 
                                        Email_cliente 
                                     FROM Pedidos 
                                     WHERE 1=1"; // Truque para adicionar ANDs depois

                    // Filtro de Nome
                    if (!string.IsNullOrEmpty(txtBuscaNome.Text))
                    {
                        query += " AND Nome_cliente LIKE @nome";
                    }

                    // Filtro de Data (Só se o Checkbox estiver marcado)
                    if (chkFiltrarData.Checked)
                    {
                        query += " AND CAST(Data_pedido AS DATE) = @data";
                    }

                    query += " ORDER BY Data_pedido DESC";

                    SqlCommand cmd = new SqlCommand(query, conexao);

                    // Adiciona os parâmetros
                    if (!string.IsNullOrEmpty(txtBuscaNome.Text))
                    {
                        cmd.Parameters.AddWithValue("@nome", "%" + txtBuscaNome.Text + "%");
                    }
                    if (chkFiltrarData.Checked)
                    {
                        cmd.Parameters.AddWithValue("@data", dtpBuscaData.Value.Date);
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvPedidos.DataSource = dt;

                    // --- CALCULA O TOTAL VENDIDO NA TELA ---
                    decimal totalGeral = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Total"] != DBNull.Value)
                        {
                            totalGeral += Convert.ToDecimal(row["Total"]);
                        }
                    }
                    // Atualiza a Label de Total
                    lblTotalVendido.Text = "Total Filtrado: " + totalGeral.ToString("C");
                    lblTotalVendido.ForeColor = Color.Green;
                }

                if (dgvPedidos.Columns.Contains("ID_pedido"))
                    dgvPedidos.Columns["ID_pedido"].HeaderText = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar pedidos: " + ex.Message);
            }
        }

        // Botão de Pesquisar (Chama o CarregarPedidos)
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            CarregarPedidos();
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

        // 📊 RELATÓRIO PROFISSIONAL (GRÁFICO)
        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.Rows.Count == 0)
            {
                MessageBox.Show("Não há dados na tabela para gerar relatório.");
                return;
            }

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(ImprimirRelatorio);
            pd.DefaultPageSettings.Landscape = true; // Paisagem cabe mais dados

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = pd;
            preview.WindowState = FormWindowState.Maximized;
            preview.ShowDialog();
        }

        // --- O DESENHISTA DO RELATÓRIO ---
        private void ImprimirRelatorio(object sender, PrintPageEventArgs e)
        {
            Font fonteTitulo = new Font("Arial", 18, FontStyle.Bold);
            Font fonteCabecalho = new Font("Arial", 10, FontStyle.Bold);
            Font fonteDados = new Font("Courier New", 9);

            SolidBrush pincel = new SolidBrush(Color.Black);
            Pen caneta = new Pen(Color.Black, 1);

            float y = 30;
            float margemEsq = 30;

            // Título
            e.Graphics.DrawString("RELATÓRIO DE VENDAS - CIEN FLEUR", fonteTitulo, pincel, margemEsq, y);
            y += 30;
            e.Graphics.DrawString("Gerado em: " + DateTime.Now.ToString(), fonteDados, pincel, margemEsq, y);
            y += 40;

            // Cabeçalho das colunas
            float xId = margemEsq;
            float xData = margemEsq + 60;
            float xCliente = margemEsq + 200;
            float xPagamento = margemEsq + 500;
            float xValor = margemEsq + 700;
            float xStatus = margemEsq + 850;

            e.Graphics.DrawString("ID", fonteCabecalho, pincel, xId, y);
            e.Graphics.DrawString("DATA", fonteCabecalho, pincel, xData, y);
            e.Graphics.DrawString("CLIENTE", fonteCabecalho, pincel, xCliente, y);
            e.Graphics.DrawString("PAGAMENTO", fonteCabecalho, pincel, xPagamento, y);
            e.Graphics.DrawString("VALOR", fonteCabecalho, pincel, xValor, y);
            e.Graphics.DrawString("STATUS", fonteCabecalho, pincel, xStatus, y);

            y += 20;
            e.Graphics.DrawLine(caneta, margemEsq, y, e.PageBounds.Width - 30, y);
            y += 10;

            decimal totalRelatorio = 0;

            // Loop nos dados da Grid
            foreach (DataGridViewRow row in dgvPedidos.Rows)
            {
                if (row.IsNewRow) continue;

                string id = row.Cells["ID_pedido"].Value.ToString();
                string data = Convert.ToDateTime(row.Cells["Data_pedido"].Value).ToString("dd/MM/yyyy HH:mm");

                string cliente = row.Cells["Nome_cliente"].Value.ToString();
                if (cliente.Length > 25) cliente = cliente.Substring(0, 25) + "..."; // Corta nome longo

                string pag = row.Cells["FormaPagamento"].Value?.ToString() ?? "N/A";

                decimal valor = Convert.ToDecimal(row.Cells["Total"].Value);
                totalRelatorio += valor;

                string status = row.Cells["Status_pedido"].Value?.ToString() ?? "";

                e.Graphics.DrawString(id, fonteDados, pincel, xId, y);
                e.Graphics.DrawString(data, fonteDados, pincel, xData, y);
                e.Graphics.DrawString(cliente, fonteDados, pincel, xCliente, y);
                e.Graphics.DrawString(pag, fonteDados, pincel, xPagamento, y);
                e.Graphics.DrawString(valor.ToString("C"), fonteDados, pincel, xValor, y);
                e.Graphics.DrawString(status, fonteDados, pincel, xStatus, y);

                y += 20;
            }

            y += 20;
            e.Graphics.DrawLine(caneta, margemEsq, y, e.PageBounds.Width - 30, y);
            y += 10;

            // Total Final no Relatório
            Font fonteTotal = new Font("Arial", 14, FontStyle.Bold);
            string textoTotal = "TOTAL GERAL: " + totalRelatorio.ToString("C");
            StringFormat alinhamentoDir = new StringFormat() { Alignment = StringAlignment.Far };
            e.Graphics.DrawString(textoTotal, fonteTotal, Brushes.DarkBlue, e.PageBounds.Width - 50, y, alinhamentoDir);
        }

        // 🖱️ Corrige a seleção de linha no clique da célula
        private void dgvPedidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                dgvPedidos.CurrentCell = dgvPedidos.Rows[e.RowIndex].Cells[e.ColumnIndex];
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form1 = new Form3();
            form1.Show();
            this.Hide();
        }

        private void btnFiltrar_Click_1(object sender, EventArgs e)
        {
            CarregarPedidos();
        }
    }
}