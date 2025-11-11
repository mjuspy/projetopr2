using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace projetopr2
{
    public partial class formMeusPedidos : Form
    {
        string connectionString = @"Data Source=PCZAO;Initial Catalog=cj3027724pr2;Integrated Security=True;";

        public formMeusPedidos()
        {
            InitializeComponent();
        }

        private void formPedidosCliente_Load(object sender, EventArgs e)
        {
            CarregarPedidosCliente();
            EstilizarDataGridView();
        }

        // 🔹 Carrega os pedidos do cliente logado usando o e-mail
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
                    string sql = @"
                        SELECT 
                            Pedidos.ID_pedido AS [ID do Pedido],
                            Pedidos.Data_pedido AS [Data do Pedido],
                            cadastro.nome AS [Cliente],
                            cadastro.email AS [E-mail],
                            Pedidos.TipoEntrega AS [Tipo de Entrega],
                            Endereco_entrega AS [Endereço de Entrega],
                            FormaPagamento AS [Forma de Pagamento],
                            Subtotal AS [Subtotal],
                            Taxa_entrega AS [Taxa de Entrega],
                            Total AS [Total],
                            Status_pedido AS [Status]
                        FROM Pedidos
                        WHERE cadastro.email = @Email
                        ORDER BY Pedidos.Data_pedido DESC";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Email", emailCliente);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvMeusPedidos.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Você ainda não possui pedidos realizados.", "Cien Fleur ☕");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar pedidos: " + ex.Message);
            }
        }

        // 🎨 Estilização do DataGridView (Cien Fleur Theme)
        private void EstilizarDataGridView()
        {
            dgvMeusPedidos.BorderStyle = BorderStyle.None;
            dgvMeusPedidos.BackgroundColor = ColorTranslator.FromHtml("#fadfb5");
            dgvMeusPedidos.EnableHeadersVisualStyles = false;

            dgvMeusPedidos.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#f2cfa0");
            dgvMeusPedidos.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#3a2e2a");
            dgvMeusPedidos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvMeusPedidos.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#fff7e6");
            dgvMeusPedidos.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#3a2e2a");
            dgvMeusPedidos.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#f7d9a5");
            dgvMeusPedidos.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#3a2e2a");
            dgvMeusPedidos.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dgvMeusPedidos.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#faebd7");
            dgvMeusPedidos.GridColor = ColorTranslator.FromHtml("#e5c99b");

            dgvMeusPedidos.RowHeadersVisible = false;
            dgvMeusPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvPedidosCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // futuro: exibir detalhes do pedido
        }

        private void formMeusPedidos_Load(object sender, EventArgs e)
        {

        }
    }
}
