using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing; // Necessário para imprimir
using System.Windows.Forms;

namespace projetopr2
{
    public partial class formMeusPedidos : Form
    {
        string connectionString = @"Data Source=PCZAO;Initial Catalog=cj3027724pr2;Integrated Security=True;";

        // --- VARIÁVEIS GLOBAIS PARA O DESIGN DA NOTINHA ---
        // (Guardam os dados para usar no desenho)
        int _idPedido = 0;
        string _dataPedido = "";
        string _nomeCliente = "";
        string _tipoEntrega = "";
        string _endereco = "";
        string _formaPagamento = "";
        decimal _subtotal = 0;
        decimal _taxa = 0;
        decimal _total = 0;

        public formMeusPedidos()
        {
            InitializeComponent();
        }

        private void formMeusPedidos_Load(object sender, EventArgs e)
        {
            CarregarPedidosCliente();
            EstilizarDataGridView();
        }

        // --- CARREGA OS PEDIDOS NA TABELA ---
        private void CarregarPedidosCliente()
        {
            try
            {
                if (SessaoUsuario1.UsuarioLogado == null)
                {
                    MessageBox.Show("Erro: Nenhum usuário logado.");
                    return;
                }

                string emailCliente = SessaoUsuario1.UsuarioLogado.Email;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // SQL ajustado para suas colunas
                    string sql = @"
                        SELECT 
                            ID_pedido AS [Nº Pedido],
                            Data_pedido AS [Data],
                            Nome_cliente AS [Cliente],
                            TipoEntrega AS [Entrega],
                            Endereco_entrega AS [Endereço],
                            FormaPagamento AS [Pagamento],
                            Total AS [Valor Total],
                            Subtotal, 
                            Taxa_entrega,
                            Status_pedido AS [Status]
                        FROM Pedidos
                        WHERE Email_cliente = @Email
                        ORDER BY Data_pedido DESC";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Email", emailCliente);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvMeusPedidos.DataSource = dt;

                    // Escondemos as colunas que usamos no cálculo mas não precisam aparecer na grid
                    if (dgvMeusPedidos.Columns.Contains("Subtotal")) dgvMeusPedidos.Columns["Subtotal"].Visible = false;
                    if (dgvMeusPedidos.Columns.Contains("Taxa_entrega")) dgvMeusPedidos.Columns["Taxa_entrega"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar pedidos: " + ex.Message);
            }
        }

        // --- ESTILO E BOTÃO DE IMPRIMIR ---
        private void EstilizarDataGridView()
        {
            dgvMeusPedidos.BorderStyle = BorderStyle.None;
            dgvMeusPedidos.BackgroundColor = ColorTranslator.FromHtml("#fadfb5");
            dgvMeusPedidos.EnableHeadersVisualStyles = false;

            // Cabeçalho
            dgvMeusPedidos.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#f2cfa0");
            dgvMeusPedidos.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#3a2e2a");
            dgvMeusPedidos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Linhas
            dgvMeusPedidos.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#fff7e6");
            dgvMeusPedidos.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#3a2e2a");
            dgvMeusPedidos.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#f7d9a5");
            dgvMeusPedidos.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#3a2e2a");
            dgvMeusPedidos.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvMeusPedidos.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#faebd7");

            dgvMeusPedidos.GridColor = ColorTranslator.FromHtml("#e5c99b");
            dgvMeusPedidos.RowHeadersVisible = false;
            dgvMeusPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Adiciona o botão se não existir
            if (dgvMeusPedidos.Columns["btnImprimir"] == null)
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "btnImprimir";
                btn.HeaderText = "Comprovante";
                btn.Text = "🖨️ Ver Nota";
                btn.UseColumnTextForButtonValue = true;
                dgvMeusPedidos.Columns.Add(btn);
            }
        }

        // --- EVENTO DE CLIQUE (DISPARA A IMPRESSÃO) ---
        private void dgvMeusPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvMeusPedidos.Columns[e.ColumnIndex].Name == "btnImprimir")
            {
                try
                {
                    // Pega o ID
                    int idPedido = Convert.ToInt32(dgvMeusPedidos.Rows[e.RowIndex].Cells["Nº Pedido"].Value);

                    // Busca os dados no banco
                    MontarDadosDaNota(idPedido);

                    // Configura a impressão
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler(ImprimirNotinha);

                    PrintPreviewDialog preview = new PrintPreviewDialog();
                    preview.Document = pd;
                    preview.WindowState = FormWindowState.Maximized; // Abre grandão pra ver bonito
                    preview.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao preparar impressão: " + ex.Message);
                }
            }
        }

        // --- BUSCA DADOS NO BANCO PARA A NOTINHA ---
        private void MontarDadosDaNota(int idPedido)
        {
            try
            {
                _idPedido = idPedido;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SELECT * FROM Pedidos WHERE ID_pedido = @id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id", idPedido);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Preenche as variáveis globais
                        _dataPedido = reader["Data_pedido"].ToString();
                        _nomeCliente = reader["Nome_cliente"].ToString();
                        _tipoEntrega = reader["TipoEntrega"].ToString();
                        _endereco = reader["Endereco_entrega"].ToString();
                        _formaPagamento = reader["FormaPagamento"].ToString();

                        // Converte valores monetários com segurança
                        _subtotal = reader["Subtotal"] != DBNull.Value ? Convert.ToDecimal(reader["Subtotal"]) : 0;
                        _taxa = reader["Taxa_entrega"] != DBNull.Value ? Convert.ToDecimal(reader["Taxa_entrega"]) : 0;
                        _total = reader["Total"] != DBNull.Value ? Convert.ToDecimal(reader["Total"]) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar dados da nota: " + ex.Message);
            }
        }

        // --- O ARTISTA: DESENHA A NOTINHA BONITA ---
        // --- O ARTISTA: DESENHA A NOTINHA AGORA GRANDE E CENTRALIZADA ---
        private void ImprimirNotinha(object sender, PrintPageEventArgs e)
        {
            // 1. CONFIGURAÇÃO DE TAMANHOS (AUMENTAMOS TUDO)
            // Fontes maiores para ler melhor na folha A4
            Font fonteTitulo = new Font("Arial", 24, FontStyle.Bold);     // Era 16
            Font fonteSubtitulo = new Font("Arial", 14, FontStyle.Bold);  // Era 11
            Font fonteRegular = new Font("Courier New", 12);              // Era 10
            Font fonteNegrito = new Font("Courier New", 12, FontStyle.Bold);
            Font fonteTotal = new Font("Arial", 18, FontStyle.Bold);      // Era 14

            SolidBrush pincel = new SolidBrush(Color.Black);
            Pen caneta = new Pen(Color.Black, 2); // Linha mais grossa (2px)

            // 2. CÁLCULO PARA CENTRALIZAR NA FOLHA A4
            float larguraPagina = 500; // Aumentamos a largura da nota (era 280)

            // Essa conta mágica descobre a margem para ficar exatamente no meio da folha
            float margemEsq = (e.PageBounds.Width - larguraPagina) / 2;
            float y = 50; // Começa um pouco mais para baixo (margem superior)

            float centroNota = margemEsq + (larguraPagina / 2); // Centro relativo à nota
            float margemDir = margemEsq + larguraPagina;        // Onde termina a nota à direita

            // Formatação de alinhamento
            StringFormat alinCentro = new StringFormat() { Alignment = StringAlignment.Center };
            StringFormat alinDireita = new StringFormat() { Alignment = StringAlignment.Far };

            // --- DESENHO ---

            // 1. CABEÇALHO
            e.Graphics.DrawString("CIEN FLEUR", fonteTitulo, pincel, centroNota, y, alinCentro);
            y += 40;
            e.Graphics.DrawString("Cafeteria & Confeitaria", fonteRegular, pincel, centroNota, y, alinCentro);
            y += 30;
            e.Graphics.DrawLine(caneta, margemEsq, y, margemDir, y);
            y += 20;

            // 2. DADOS DO PEDIDO
            e.Graphics.DrawString($"PEDIDO: #{_idPedido}", fonteSubtitulo, pincel, margemEsq, y);
            y += 30;
            e.Graphics.DrawString($"Data: {_dataPedido}", fonteRegular, pincel, margemEsq, y);
            y += 25;
            e.Graphics.DrawString($"Cliente: {_nomeCliente}", fonteRegular, pincel, margemEsq, y);
            y += 40;

            // 3. ENTREGA
            e.Graphics.DrawString("ENTREGA:", fonteNegrito, pincel, margemEsq, y);
            y += 25;
            e.Graphics.DrawString($"{_tipoEntrega}", fonteRegular, pincel, margemEsq, y);
            y += 25;

            if (_tipoEntrega == "Delivery" && !string.IsNullOrEmpty(_endereco))
            {
                // Caixa de texto maior para o endereço
                RectangleF rectEnd = new RectangleF(margemEsq, y, larguraPagina, 50);
                e.Graphics.DrawString($"End: {_endereco}", fonteRegular, pincel, rectEnd);
                y += 50;
            }

            y += 10;
            e.Graphics.DrawLine(caneta, margemEsq, y, margemDir, y);
            y += 20;

            // 4. TOTAIS (Alinhados à direita)
            e.Graphics.DrawString("Subtotal:", fonteRegular, pincel, margemEsq, y);
            e.Graphics.DrawString(string.Format("{0:C}", _subtotal), fonteRegular, pincel, margemDir, y, alinDireita);
            y += 25;

            e.Graphics.DrawString("Taxa:", fonteRegular, pincel, margemEsq, y);
            e.Graphics.DrawString(string.Format("{0:C}", _taxa), fonteRegular, pincel, margemDir, y, alinDireita);
            y += 40;

            // Fundo Cinza Grande para o Total
            e.Graphics.FillRectangle(Brushes.LightGray, margemEsq, y, larguraPagina, 40);

            // Ajuste fino para o texto ficar no meio da tarja cinza
            e.Graphics.DrawString("TOTAL", fonteTotal, pincel, margemEsq + 10, y + 8);
            e.Graphics.DrawString(string.Format("{0:C}", _total), fonteTotal, pincel, margemDir - 10, y + 8, alinDireita);
            y += 60;

            // 5. RODAPÉ
            e.Graphics.DrawString($"Pagamento: {_formaPagamento}", fonteRegular, pincel, margemEsq, y);
            y += 50;

            e.Graphics.DrawString("Obrigado pela preferência!", fonteRegular, pincel, centroNota, y, alinCentro);
            y += 25;
            e.Graphics.DrawString("Volte Sempre! ☕", fonteRegular, pincel, centroNota, y, alinCentro);

            // Borda em volta da nota inteira (Para parecer um papel)
            float alturaTotal = y + 20; // Altura onde o desenho parou
            e.Graphics.DrawRectangle(caneta, margemEsq - 10, 40, larguraPagina + 20, alturaTotal - 40);
        }
        // Métodos que não são mais usados
        private void formPedidosCliente_Load(object sender, EventArgs e) { }
    }
}