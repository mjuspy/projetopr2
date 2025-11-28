using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // <--- IMPORTANTE: Adicionar isso para o SQL funcionar
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetopr2
{
    public partial class Form3 : Form
    {
        // Coloque aqui a sua string de conexão (a mesma que você usa nos outros forms)
        string connectionString = @"Data Source=PCZAO;Initial Catalog=cj3027724pr2;Integrated Security=True;";

        // Variável global para guardar qual produto estamos editando no momento
        int idProdutoSelecionado = 0;

        public Form3()
        {
            InitializeComponent();
        }

        // --- Evento que roda quando a tela abre ---
        private void Form3_Load(object sender, EventArgs e)
        {
            CarregarProdutos(); // Carrega a tabela assim que abre a janela
        }

        // --- Método para buscar os dados no banco e colocar no Grid ---
        private void CarregarProdutos()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    // Pega tudo da tabela Produtos para mostrar na lista
                    SqlDataAdapter da = new SqlDataAdapter("SELECT id, nome, descricao, preco FROM Produtos", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Joga os dados dentro do DataGridView (sua tabela visual)
                    dgvProdutos.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produtos: " + ex.Message);
            }
        }

        // --- Evento de clique no BOTÃO SALVAR ---
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // 1. Validações básicas
            if (idProdutoSelecionado == 0)
            {
                MessageBox.Show("Por favor, clique em um produto na tabela abaixo para editar.");
                return;
            }

            if (txtNome.Text == "" || txtPreco.Text == "")
            {
                MessageBox.Show("Nome e Preço são obrigatórios!");
                return;
            }

            // 2. O comando de Atualizar (UPDATE)
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string sql = @"UPDATE Produtos 
                                   SET nome = @nome, 
                                       descricao = @descricao, 
                                       preco = @preco 
                                   WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Passando os valores das caixinhas para o comando SQL
                        cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                        cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                        cmd.Parameters.AddWithValue("@id", idProdutoSelecionado);

                        // Convertendo o preço corretamente (para evitar erro de virgula/ponto)
                        decimal preco;
                        if (decimal.TryParse(txtPreco.Text, out preco))
                        {
                            cmd.Parameters.AddWithValue("@preco", preco);
                        }
                        else
                        {
                            MessageBox.Show("O valor do preço está inválido. Use números e vírgula.");
                            return;
                        }

                        cmd.ExecuteNonQuery(); // Executa a atualização
                    }
                }

                MessageBox.Show("Produto atualizado com sucesso!");

                // Limpa os campos e recarrega a tabela
                LimparCampos();
                CarregarProdutos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message);
            }
        }

        // --- Método auxiliar para limpar as caixas de texto ---
        private void LimparCampos()
        {
            txtNome.Text = "";
            txtDescricao.Text = "";
            txtPreco.Text = "";
            idProdutoSelecionado = 0; // Reseta o ID
        }

        // --- IMPORTANTE: Você precisa criar esse evento no seu DataGridView ---
        // Vá no Design, clique no raiozinho (eventos) do Grid e dê clique duplo em "CellClick"
        private void dgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se clicou em uma linha válida (e não no cabeçalho)
            if (e.RowIndex >= 0)
            {
                // Pega a linha que foi clicada
                DataGridViewRow row = dgvProdutos.Rows[e.RowIndex];

                // Preenche as caixinhas com os dados dessa linha
                txtNome.Text = row.Cells["nome"].Value.ToString();
                txtDescricao.Text = row.Cells["descricao"].Value.ToString();
                txtPreco.Text = row.Cells["preco"].Value.ToString();

                // Guarda o ID na variável para usarmos no botão Salvar depois
                idProdutoSelecionado = Convert.ToInt32(row.Cells["id"].Value);
            }
        }

        // Seus outros eventos vazios (pode deixar assim ou apagar se não usar)
        private void txtNome_TextChanged(object sender, EventArgs e) { }
        private void txtDescricao_TextChanged(object sender, EventArgs e) { }
        private void txtPreco_TextChanged(object sender, EventArgs e) { }
        private void Readonly_Click(object sender, EventArgs e) { }

        private void dgvProdutos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}