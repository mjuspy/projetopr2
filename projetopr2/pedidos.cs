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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

// Usings para Email
using System.Net;
using System.Net.Mail;

namespace projetopr2
{
    public partial class pedidos : Form
    {
        // Sua string de conexão
        string connectionString = @"Data Source=PCZAO;Initial Catalog=cj3027724pr2;Integrated Security=True;";

        private DataTable carrinhoTable = new DataTable();

        public pedidos()
        {
            InitializeComponent();
        }

        private void pedidos_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CarregarCarrinho();
        }

        // --- MÉTODO COM AS CORES BEGES ---
        private void ConfigurarGrid()
        {
            // Define as cores
            Color corFundoGrid = Color.FromArgb(255, 239, 208);      // #ffefd0
            Color corFundoCabecalho = Color.FromArgb(250, 223, 181); // #FADFB5
            Color corTextoCabecalho = Color.FromArgb(92, 75, 72);       // Marrom escuro
            Color corTextoCorpo = Color.FromArgb(92, 75, 72);       // Marrom escuro
            Color corLinha = Color.FromArgb(252, 231, 204);
            Color corSelecao = Color.FromArgb(252, 231, 204);

            // --- 1. CONFIGURAÇÃO DAS COLUNAS ---
            dgvCarrinho.AutoGenerateColumns = false;
            dgvCarrinho.Columns.Clear();
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ID_item", HeaderText = "ID", Name = "ID_item", ReadOnly = true, Visible = false });
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nome_produto", HeaderText = "Produto", Name = "Nome_produto", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Preco_produto", HeaderText = "Preço Unit.", Name = "Preco_produto", ReadOnly = true, DefaultCellStyle = { Format = "C2" } });
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantidade", HeaderText = "Quantidade", Name = "Quantidade", ReadOnly = false });
            var subtotalColumn = new DataGridViewTextBoxColumn { HeaderText = "Subtotal", Name = "Subtotal", ReadOnly = true, DefaultCellStyle = { Format = "C2" } };
            dgvCarrinho.Columns.Add(subtotalColumn);

            // --- 2. CÓDIGO DE ESTILO ---
            dgvCarrinho.BackColor = corFundoGrid;
            dgvCarrinho.ForeColor = corTextoCorpo;
            dgvCarrinho.BorderStyle = BorderStyle.None;
            dgvCarrinho.RowHeadersVisible = false;
            dgvCarrinho.AllowUserToResizeRows = false;
            dgvCarrinho.EnableHeadersVisualStyles = false;
            dgvCarrinho.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCarrinho.ColumnHeadersDefaultCellStyle.BackColor = corFundoCabecalho;
            dgvCarrinho.ColumnHeadersDefaultCellStyle.ForeColor = corTextoCabecalho;
            dgvCarrinho.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvCarrinho.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCarrinho.ColumnHeadersHeight = 40;
            dgvCarrinho.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvCarrinho.DefaultCellStyle.SelectionBackColor = corSelecao;
            dgvCarrinho.DefaultCellStyle.SelectionForeColor = corTextoCorpo;
            dgvCarrinho.RowTemplate.Height = 35;
            dgvCarrinho.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCarrinho.GridColor = corLinha;
            dgvCarrinho.RowsDefaultCellStyle.BackColor = corFundoGrid;
            dgvCarrinho.AlternatingRowsDefaultCellStyle.BackColor = corFundoGrid;
        }

        // FUNÇÃO 1 (Carregar Carrinho)
        private void CarregarCarrinho()
        {
            try
            {
                carrinhoTable.Clear();
                if (SessaoUsuario1.UsuarioLogado != null)
                {
                    int idUsuarioLogado = SessaoUsuario1.UsuarioLogado.Id;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string sql = "SELECT ID_item, Nome_produto, Preco_produto, Quantidade FROM Itens_carrinho WHERE cod_cliente = @IdCliente";
                        SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                        adapter.SelectCommand.Parameters.AddWithValue("@IdCliente", idUsuarioLogado);
                        adapter.Fill(carrinhoTable);
                    }
                }
                dgvCarrinho.DataSource = carrinhoTable;
                AtualizarTotais();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar o carrinho: " + ex.Message, "Erro de Conexão");
            }
        }

        // FUNÇÃO 2 (Atualizar Totais)
        private void AtualizarTotais()
        {
            decimal subtotalGeral = 0;
            foreach (DataGridViewRow row in dgvCarrinho.Rows)
            {
                if (row.IsNewRow) continue;
                decimal preco = Convert.ToDecimal(row.Cells["Preco_produto"].Value);
                int quantidade = Convert.ToInt32(row.Cells["Quantidade"].Value);
                decimal subtotalItem = preco * quantidade;
                row.Cells["Subtotal"].Value = subtotalItem;
                subtotalGeral += subtotalItem;
            }
            decimal taxaEntrega = 0.00m;
            if (chkEntrega.Checked)
            {
                taxaEntrega = 5.00m;
            }
            decimal total = subtotalGeral + taxaEntrega;
            lblSubtotal.Text = $"Subtotal: {subtotalGeral:C2}";
            lblEntrega.Text = $"Entrega: {taxaEntrega:C2}";
            lblTotal.Text = $"Total: {total:C2}";
        }


        // FUNÇÃO 3 (Salvar Pedido no Banco)
        private int CriarPedidoNoBanco(decimal subtotal, decimal taxaEntrega, decimal total, string tipoEntrega, string endereco, string formaPagamento)
        {
            string nomeCliente = SessaoUsuario1.UsuarioLogado.Nome;
            string emailCliente = SessaoUsuario1.UsuarioLogado.Email;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = @"INSERT INTO pedidos 
                                     (Data_pedido, Nome_cliente, Email_cliente, Endereco_entrega, Taxa_entrega, Total, Status_pedido, TipoEntrega, Subtotal, FormaPagamento) 
                                   VALUES 
                                     (@Data, @Nome, @Email, @Endereco, @Taxa, @Total, @Status, @Tipo, @Subtotal, @Pagamento);
                                   SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Data", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Nome", nomeCliente);
                        cmd.Parameters.AddWithValue("@Email", emailCliente);
                        cmd.Parameters.AddWithValue("@Endereco", (object)endereco ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Taxa", taxaEntrega);
                        cmd.Parameters.AddWithValue("@Total", total);
                        cmd.Parameters.AddWithValue("@Status", "Pendente");
                        cmd.Parameters.AddWithValue("@Tipo", tipoEntrega);
                        cmd.Parameters.AddWithValue("@Subtotal", subtotal);
                        cmd.Parameters.AddWithValue("@Pagamento", formaPagamento);
                        con.Open();
                        int novoPedidoID = Convert.ToInt32(cmd.ExecuteScalar());
                        return novoPedidoID;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro crítico ao salvar o pedido: " + ex.Message);
                return 0;
            }
        }


        // --- FUNÇÃO 4 (ATUALIZADA) - Enviar E-mail Estilizado ---
        private async Task EnviarEmailConfirmacao(string emailCliente, string nomeCliente, int pedidoID, decimal totalPedido, string formaPagamento, string tipoEntrega, string endereco)
        {
            try
            {
                // ----- CONFIGURE SEU E-MAIL DE ENVIO AQUI -----
                string emailRemetente = "cienfleuroux@gmail.com";
                string senhaApp = "nekc osbg gkcy ajqo"; // <-- A senha que o Google gerou!
                // ---------------------------------------------

                MailMessage mensagem = new MailMessage();
                mensagem.From = new MailAddress(emailRemetente, "Cien Fleur");
                mensagem.To.Add(emailCliente); 
         
                mensagem.Subject = $"Cien Fleur - Confirmação do Pedido Nº {pedidoID}";

                mensagem.IsBodyHtml = true;

                // --- NOVO CORPO DE E-MAIL ESTILIZADO ---
                string corpoEmail = $@"
<body style='margin: 0; padding: 0; font-family: Arial, sans-serif; background-color: #fcfbf9;'>
    <table align='center' border='0' cellpadding='0' cellspacing='0' width='600' style='width: 600px; border-collapse: collapse; border: 1px solid #FADFB5; margin-top: 20px; margin-bottom: 20px;'>
        
        <tr>
            <td align='center' style='background-color: #FADFB5; padding: 30px 0;'>
                <h1 style='color: #5C4B48; font-family: ""Times New Roman"", Times, serif; margin: 0; font-size: 36px;'>Cien Fleur</h1>
            </td>
        </tr>

        <tr>
            <td style='background-color: #ffffff; padding: 40px 30px;'>
                <h2 style='color: #5C4B48; font-size: 24px; margin-top: 0;'>Olá, {nomeCliente}!</h2>
                <p style='color: #5C4B48; font-size: 16px; line-height: 1.6;'>
                    Seu pedido foi recebido e já está sendo preparado com muito carinho.
                </p>
                
                <table border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse: collapse; margin-top: 30px;'>
                    <tr><td colspan='2' style='padding-bottom: 20px;'><h3 style='color: #5C4B48; margin: 0;'>Resumo do Pedido</h3></td></tr>
                    
                    <tr style='border-bottom: 1px solid #EAE0DE;'>
                        <td style='padding: 12px 0; color: #5C4B48; font-size: 16px;'>Nº do Pedido:</td>
                        <td align='right' style='padding: 12px 0; color: #5C4B48; font-size: 16px; font-weight: bold;'>{pedidoID}</td>
                    </tr>
                    <tr style='border-bottom: 1px solid #EAE0DE;'>
                        <td style='padding: 12px 0; color: #5C4B48; font-size: 16px;'>Tipo de Entrega:</td>
                        <td align='right' style='padding: 12px 0; color: #5C4B48; font-size: 16px;'>{tipoEntrega}</td>
                    </tr>
                    {(tipoEntrega == "Entrega" ? $@"
                    <tr style='border-bottom: 1px solid #EAE0DE;'>
                        <td style='padding: 12px 0; color: #5C4B48; font-size: 16px;'>Endereço:</td>
                        <td align='right' style='padding: 12px 0; color: #5C4B48; font-size: 16px;'>{endereco}</td>
                    </tr>" : "")}
                    <tr style='border-bottom: 1px solid #EAE0DE;'>
                        <td style='padding: 12px 0; color: #5C4B48; font-size: 16px;'>Forma de Pagamento:</td>
                        <td align='right' style='padding: 12px 0; color: #5C4B48; font-size: 16px;'>{formaPagamento}</td>
                    </tr>
                    <tr style='border-bottom: 1px solid #EAE0DE;'>
                        <td style='padding: 12px 0; color: #5C4B48; font-size: 16px;'>Status:</td>
                        <td align='right' style='padding: 12px 0; color: #5C4B48; font-size: 16px;'>Pendente</td>
                    </tr>
                    <tr style='background-color: #ffefd0;'>
                        <td style='padding: 15px 0 15px 10px; color: #5C4B48; font-size: 18px; font-weight: bold;'>Valor Total:</td>
                        <td align='right' style='padding: 15px 10px 15px 0; color: #5C4B48; font-size: 18px; font-weight: bold;'>{totalPedido:C2}</td>
                    </tr>
                </table>

                <p style='color: #5C4B48; font-size: 16px; line-height: 1.6; margin-top: 30px;'>
                    Obrigado por escolher a <strong>Cien Fleur</strong>!
                </p>
            </td>
        </tr>

        <tr>
            <td style='background-color: #FADFB5; padding: 30px; text-align: center;'>
                <p style='color: #5C4B48; font-size: 12px; margin: 0;'>
                    © 2025 Cien Fleur. Todos os direitos reservados.<br/>
                    Este é um e-mail automático, por favor não responda.
                </p>
            </td>
        </tr>
    </table>
</body>";

                mensagem.Body = corpoEmail;
                // --- FIM DO NOVO CORPO DE E-MAIL ---


                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailRemetente, senhaApp);

                await smtpClient.SendMailAsync(mensagem);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"O pedido foi salvo, mas houve um erro ao enviar o e-mail de confirmação: {ex.Message}",
                                "Aviso de E-mail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // --- FUNÇÃO 5 (ATUALIZADA) - BOTÃO FINALIZAR ---
        private async void btnFinalizar_Click(object sender, EventArgs e)
        {
            // 1. Validações
            if (dgvCarrinho.Rows.Count == 0)
            {
                MessageBox.Show("Seu carrinho está vazio!", "Atenção");
                return;
            }
            if (!chkEntrega.Checked && !chkRetira.Checked)
            {
                MessageBox.Show("Por favor, selecione 'Entrega' ou 'Retira'.", "Opção Necessária");
                return;
            }

            // Validação de Pagamento
            string formaPagamento = "";
            if (rbPix.Checked) { formaPagamento = "Pix"; }
            else if (rbCredito.Checked) { formaPagamento = "Cartão de Crédito"; }
            else if (rbDebito.Checked) { formaPagamento = "Cartão de Débito"; }

            if (string.IsNullOrEmpty(formaPagamento))
            {
                MessageBox.Show("Por favor, selecione uma forma de pagamento.", "Pagamento Necessário");
                return;
            }

            // 2. Pegar Dados do Cliente
            if (SessaoUsuario1.UsuarioLogado == null)
            {
                MessageBox.Show("Você precisa fazer o login para finalizar um pedido.", "Login Necessário");
                return;
            }
            int idDoClienteAtual = SessaoUsuario1.UsuarioLogado.Id;
            string emailCliente = SessaoUsuario1.UsuarioLogado.Email;
            string nomeCliente = SessaoUsuario1.UsuarioLogado.Nome;

            // 3. Calcular Totais Reais
            decimal subtotalGeral = 0;
            foreach (DataGridViewRow row in dgvCarrinho.Rows)
            {
                if (row.IsNewRow) continue;
                subtotalGeral += Convert.ToDecimal(row.Cells["Subtotal"].Value);
            }
            decimal taxaEntrega = chkEntrega.Checked ? 5.00m : 0.00m;
            decimal totalPedidoDecimal = subtotalGeral + taxaEntrega;


            // 4. Lógica de 'Retira'
            if (chkRetira.Checked)
            {
                var resposta = MessageBox.Show($"Confirmar pedido para RETIRADA no valor de {totalPedidoDecimal:C2}?",
                                                "Finalizar Pedido", MessageBoxButtons.YesNo);

                if (resposta == DialogResult.Yes)
                {
                    string tipoEntrega = "Retira";
                    int pedidoID = CriarPedidoNoBanco(subtotalGeral, 0.00m, totalPedidoDecimal, tipoEntrega, null, formaPagamento);

                    if (pedidoID > 0)
                    {
                        // Envia o e-mail (passando 'null' para o endereço)
                        await EnviarEmailConfirmacao(emailCliente, nomeCliente, pedidoID, totalPedidoDecimal, formaPagamento, tipoEntrega, null);

                        LimparCarrinhoAposPedido(idDoClienteAtual);
                        MessageBox.Show("Pedido finalizado para retirada! Nº do Pedido: " + pedidoID, "Cien Fleur");
                        CarregarCarrinho();
                    }
                }
            }
            // 5. Lógica de 'Entrega'
            else if (chkEntrega.Checked)
            {
                formListaEnderecos telaEnderecos = new formListaEnderecos();
                telaEnderecos.ShowDialog();

                if (telaEnderecos.EnderecoSelecionado != null)
                {
                    string enderecoEscolhido = telaEnderecos.EnderecoSelecionado;
                    var resposta = MessageBox.Show($"Confirmar pedido para ENTREGA em:\n{enderecoEscolhido}\n\nValor total: {totalPedidoDecimal:C2}?",
                                                    "Finalizar Pedido", MessageBoxButtons.YesNo);

                    if (resposta == DialogResult.Yes)
                    {
                        string tipoEntrega = "Entrega";
                        int pedidoID = CriarPedidoNoBanco(subtotalGeral, taxaEntrega, totalPedidoDecimal, tipoEntrega, enderecoEscolhido, formaPagamento);

                        if (pedidoID > 0)
                        {
                            // Envia o e-mail (passando o endereço)
                            await EnviarEmailConfirmacao(emailCliente, nomeCliente, pedidoID, totalPedidoDecimal, formaPagamento, tipoEntrega, enderecoEscolhido);

                            LimparCarrinhoAposPedido(idDoClienteAtual);
                            MessageBox.Show("Pedido finalizado com sucesso! Nº do Pedido: " + pedidoID, "Cien Fleur");
                            CarregarCarrinho();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("A seleção de endereço foi cancelada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // FUNÇÃO 6 (Limpar Carrinho)
        private void LimparCarrinhoAposPedido(int idCliente)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "DELETE FROM Itens_carrinho WHERE cod_cliente = @idCliente";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao limpar o carrinho: " + ex.Message, "Erro de Banco de Dados");
            }
        }


        // --- CÓDIGOS RESTANTES (EVENTOS DE CHECKBOX E OUTROS) ---

        private void chkEntrega_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEntrega.Checked)
            {
                chkRetira.Checked = false;
            }
            AtualizarTotais();
        }

        private void chkRetira_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRetira.Checked)
            {
                chkEntrega.Checked = false;
            }
            AtualizarTotais();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void AtualizarItemNoBanco(int idItem, int novaQuantidade) { }
        private void RemoverItemDoBanco(int idItem) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void lblSubtotal_Click(object sender, EventArgs e) { }
        private void lblEntrega_Click(object sender, EventArgs e) { }
        private void lblTotal_Click(object sender, EventArgs e) { }
    }
}