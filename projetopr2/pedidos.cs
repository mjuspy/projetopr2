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

namespace projetopr2
{
    public partial class pedidos : Form
    {
        //private string connectionString = @"Data Source=SQLEXPRESS;Initial Catalog=cj3027724pr2;User ID=aluno;Password=aluno;";
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
        private void ConfigurarGrid()
        {
            dgvCarrinho.AutoGenerateColumns = false;
            dgvCarrinho.Columns.Clear();

            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ID_item", HeaderText = "ID", Name = "ID_item", ReadOnly = true, Visible = false });
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nome_produto", HeaderText = "Produto", Name = "Nome_produto", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Preco_produto", HeaderText = "Preço Unit.", Name = "Preco_produto", ReadOnly = true, DefaultCellStyle = { Format = "C2" } });
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantidade", HeaderText = "Quantidade", Name = "Quantidade", ReadOnly = false });

            var subtotalColumn = new DataGridViewTextBoxColumn { HeaderText = "Subtotal", Name = "Subtotal", ReadOnly = true, DefaultCellStyle = { Format = "C2" } };
            dgvCarrinho.Columns.Add(subtotalColumn);
        }
        private void CarregarCarrinho()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "SELECT ID_item, Nome_produto, Preco_produto, Quantidade FROM Itens_carrinho";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                    carrinhoTable.Clear();
                    adapter.Fill(carrinhoTable);
                    dgvCarrinho.DataSource = carrinhoTable;
                }
                AtualizarTotais();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar o carrinho: " + ex.Message, "Erro de Conexão");
            }
        }
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

            decimal taxaEntrega = 5.00m;
            decimal total = subtotalGeral + taxaEntrega;

            lblSubtotal.Text = $"Subtotal: {subtotalGeral:C2}";
            lblEntrega.Text = $"Entrega: {taxaEntrega:C2}";
            lblTotal.Text = $"Total: {total:C2}";
        }
    

private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvCarrinho.Columns[e.ColumnIndex].Name != "Quantidade")
                return;

            try
            {
                int idItem = Convert.ToInt32(dgvCarrinho.Rows[e.RowIndex].Cells["ID_item"].Value);
                int novaQuantidade = Convert.ToInt32(dgvCarrinho.Rows[e.RowIndex].Cells["Quantidade"].Value);

                if (novaQuantidade > 0)
                {
                    AtualizarItemNoBanco(idItem, novaQuantidade);
                }
                else
                {
                    var resposta = MessageBox.Show("Tem certeza que quer remover esse item da sacola?", "Remover Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resposta == DialogResult.Yes)
                    {
                        RemoverItemDoBanco(idItem);
                        this.BeginInvoke(new MethodInvoker(CarregarCarrinho));
                    }
                    else
                    {
                        CarregarCarrinho();
                    }
                }

                AtualizarTotais();
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, insira um número válido para a quantidade.", "Entrada Inválida");
                CarregarCarrinho();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao atualizar o item: " + ex.Message, "Erro");
            }
        }
        private void AtualizarItemNoBanco(int idItem, int novaQuantidade)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "UPDATE Itens_carrinho SET Quantidade = @qtd WHERE ID_item = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@qtd", novaQuantidade);
                cmd.Parameters.AddWithValue("@id", idItem);
                cmd.ExecuteNonQuery();
            }
        }
        private void RemoverItemDoBanco(int idItem)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "DELETE FROM Itens_carrinho WHERE ID_item = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", idItem);
                cmd.ExecuteNonQuery();
            }
        }




        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblSubtotal_Click(object sender, EventArgs e)
        {

        }

        private void lblEntrega_Click(object sender, EventArgs e)
        {

        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (dgvCarrinho.CurrentRow == null)
            {
                MessageBox.Show("Por favor, selecione um item para remover.", "Nenhum item selecionado");
                return;
            }

            var resposta = MessageBox.Show("Tem certeza que quer remover o item selecionado?", "Remover Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resposta == DialogResult.Yes)
            {
                int idItem = Convert.ToInt32(dgvCarrinho.CurrentRow.Cells["ID_item"].Value);
                RemoverItemDoBanco(idItem);
                CarregarCarrinho();
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (dgvCarrinho.Rows.Count == 0)
            {
                MessageBox.Show("Seu carrinho está vazio!", "Atenção");
                return;
            }

            string totalPedido = lblTotal.Text;
            var resposta = MessageBox.Show($"Confirmar pedido no valor de {totalPedido}?", "Finalizar Pedido", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (resposta == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "DELETE FROM Itens_carrinho";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Pedido finalizado com sucesso! Agradecemos a preferência.", "Cien Fleur");
                CarregarCarrinho();
            }
        }

       
    }
}











