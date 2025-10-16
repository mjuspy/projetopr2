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
    public partial class cardapio1 : Form
    {
        //private string connectionString = @"Data Source=SQLEXPRESS;Initial Catalog=cj3027724pr2;User ID=aluno;Password=aluno;";
        string connectionString = @"Data Source=PCZAO;Initial Catalog=cj3027724pr2;Integrated Security=True;";

        public cardapio1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// O "Cérebro" do Cardápio. Este método centraliza toda a lógica de adicionar itens ao carrinho.
        /// </summary>
        /// <param name="nomeProduto">O nome do produto, ex: "LATTE".</param>
        /// <param name="precoProduto">O preço unitário, ex: 12.00m.</param>
        /// <param name="quantidade">A quantidade que o usuário escolheu, vinda do NumericUpDown.</param>
        /// 


        private void AdicionarAoCarrinho(string nomeProduto, decimal precoProduto, int quantidade)
        {
            // =================================================================================
            // CORREÇÃO: Verificando a classe de sessão CORRETA -> SessaoUsuari (sem o 'o')
            // =================================================================================
            if (!SessaoUsuario1.IsLoggedIn)
            {
                MessageBox.Show("Você precisa fazer o login para adicionar itens ao carrinho.",
                                "Login Necessário",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                tela_login telaDeLogin = new tela_login(); // Usando o nome da sua tela de login
                telaDeLogin.ShowDialog();

                if (SessaoUsuari.IsLoggedIn)
                {
                    AdicionarAoCarrinho(nomeProduto, precoProduto, quantidade);
                }

                return;
            }
            // =================================================================================
            // FIM DA CORREÇÃO
            // =================================================================================

            if (quantidade <= 0)
            {
                MessageBox.Show("Por favor, selecione uma quantidade válida.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string sqlVerifica = "SELECT Quantidade FROM Itens_carrinho WHERE Nome_produto = @nome";
                    SqlCommand cmdVerifica = new SqlCommand(sqlVerifica, con);
                    cmdVerifica.Parameters.AddWithValue("@nome", nomeProduto);
                    object resultado = cmdVerifica.ExecuteScalar();

                    if (resultado != null)
                    {
                        string sqlAtualiza = "UPDATE Itens_carrinho SET Quantidade = Quantidade + @qtd WHERE Nome_produto = @nome";
                        SqlCommand cmdAtualiza = new SqlCommand(sqlAtualiza, con);
                        cmdAtualiza.Parameters.AddWithValue("@qtd", quantidade);
                        cmdAtualiza.Parameters.AddWithValue("@nome", nomeProduto);
                        cmdAtualiza.ExecuteNonQuery();
                    }
                    else
                    {
                        string sqlInsere = "INSERT INTO Itens_carrinho (Nome_produto, Preco_produto, Quantidade) VALUES (@nome, @preco, @qtd)";
                        SqlCommand cmdInsere = new SqlCommand(sqlInsere, con);
                        cmdInsere.Parameters.AddWithValue("@nome", nomeProduto);
                        cmdInsere.Parameters.AddWithValue("@preco", precoProduto);
                        cmdInsere.Parameters.AddWithValue("@qtd", quantidade);
                        cmdInsere.ExecuteNonQuery();
                    }
                }
                MessageBox.Show($"{quantidade}x {nomeProduto} foi(foram) adicionado(s) ao carrinho!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao adicionar o item: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdicionarExpressoDuplo_Click_Click(object sender, EventArgs e)
        {
            int quantidade = Convert.ToInt32(numQuantidadeExpresso.Value);
            AdicionarAoCarrinho("EXPRESSO DUPLO", 8.10m, quantidade);
        }

        

        private void btnAdicionarExpressoDuplo_Click_Click_1(object sender, EventArgs e)
        {

            int quantidade = Convert.ToInt32(numQuantidadeExpresso.Value);
            AdicionarAoCarrinho("EXPRESSO DUPLO", 8.10m, quantidade);
        }

        private void btnAdicionarExpressoMachiato_Click_Click_1(object sender, EventArgs e)
        {
            int quantidade = Convert.ToInt32(numQuantidadeExpressoMachiato.Value);
            AdicionarAoCarrinho("EXPRESSO MACHIATO", 8.20m, quantidade);
        }

        private void btnAdicionarCappuccinoItaliano_Click_Click_1(object sender, EventArgs e)
        {
            int quantidade = Convert.ToInt32(numQuantidadeCappuccinoItaliano.Value);
            AdicionarAoCarrinho("CAPPUCCINO ITALIANO", 12.00m, quantidade);
        }

        private void btnAdicionarLatte_Click_Click_1(object sender, EventArgs e)
        {
            int quantidade = Convert.ToInt32(numQuantidadeLatte.Value);
            AdicionarAoCarrinho("LATTE", 12.00m, quantidade);
        }

        private void btnAdicionarCappuccinoBrasileiro_Click_Click_1(object sender, EventArgs e)
        {
            int quantidade = Convert.ToInt32(numQuantidadeCappuccinoBrasileiro.Value);
            //if (quantidade > 0)
            //{
            //    //MessageBox.Show("");
            //}
            AdicionarAoCarrinho("CAPPUCCINO BRASILEIRO", 14.00m, quantidade);
        }

        private void btnAdicionarMocha_Click_Click_1(object sender, EventArgs e)
        {
            int quantidade = Convert.ToInt32(numQuantidadeMocha.Value);
            AdicionarAoCarrinho("MOCHA", 16.00m, quantidade);
            //MessageBox.Show("");
        }
        











        private void btnAdicionarExpressoMachiato_Click_Click(object sender, EventArgs e)
        {

        }

        private void btnAdicionarLatte_Click_Click(object sender, EventArgs e)
        {

        }

        private void btnAdicionarCappuccinoItaliano_Click_Click(object sender, EventArgs e)
        {

        }

        private void btnAdicionarCappuccinoBrasileiro_Click_Click(object sender, EventArgs e)
        {


        }

        private void btnAdicionarMocha_Click_Click(object sender, EventArgs e)
        {

        }

        private void numQuantidadeCappuccinoItaliano_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numQuantidadeExpresso_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numQuantidadeExpressoMachiato_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pedidos frm = new pedidos();
            frm.ShowDialog();
            
        }

        private void cardapio1_Load(object sender, EventArgs e)
        {

        }
    }
}
