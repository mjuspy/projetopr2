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
            // 1. VERIFICAR O LOGIN E PEGAR O ID
            // Usamos 'SessaoUsuario1.UsuarioLogado' que é o objeto salvo no seu login
            if (SessaoUsuario1.UsuarioLogado == null)
            {
                MessageBox.Show("Você precisa fazer o login para adicionar itens ao carrinho.",
                                "Login Necessário",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                tela_login telaDeLogin = new tela_login();
                telaDeLogin.ShowDialog();

                // Se o usuário fez o login, tentamos adicionar de novo.
                if (SessaoUsuario1.UsuarioLogado != null)
                {
                    // O usuário agora está logado, chama a função novamente
                    AdicionarAoCarrinho(nomeProduto, precoProduto, quantidade);
                }

                // Para a execução desta primeira chamada
                return;
            }

            // Se chegou aqui, o usuário ESTÁ LOGADO
            int idDoClienteAtual = SessaoUsuario1.UsuarioLogado.Id;

            // 2. VERIFICAR QUANTIDADE
            if (quantidade <= 0)
            {
                MessageBox.Show("Por favor, selecione uma quantidade válida.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. LÓGICA DE BANCO DE DADOS (AGORA COM 'cod_cliente')
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // SQL VERIFICA (CORRIGIDO: Filtra pelo produto E pelo cliente)
                    string sqlVerifica = "SELECT Quantidade FROM Itens_carrinho WHERE Nome_produto = @nome AND cod_cliente = @IdCliente";
                    SqlCommand cmdVerifica = new SqlCommand(sqlVerifica, con);
                    cmdVerifica.Parameters.AddWithValue("@nome", nomeProduto);
                    cmdVerifica.Parameters.AddWithValue("@IdCliente", idDoClienteAtual); // <--- CORREÇÃO
                    object resultado = cmdVerifica.ExecuteScalar();

                    if (resultado != null)
                    {
                        // SQL ATUALIZA (CORRIGIDO: Filtra pelo produto E pelo cliente)
                        string sqlAtualiza = "UPDATE Itens_carrinho SET Quantidade = Quantidade + @qtd " +
                                             "WHERE Nome_produto = @nome AND cod_cliente = @IdCliente";
                        SqlCommand cmdAtualiza = new SqlCommand(sqlAtualiza, con);
                        cmdAtualiza.Parameters.AddWithValue("@qtd", quantidade);
                        cmdAtualiza.Parameters.AddWithValue("@nome", nomeProduto);
                        cmdAtualiza.Parameters.AddWithValue("@IdCliente", idDoClienteAtual); // <--- CORREÇÃO
                        cmdAtualiza.ExecuteNonQuery();
                    }
                    else
                    {
                        // SQL INSERE (CORRIGIDO: Salva o cod_cliente junto)
                        string sqlInsere = "INSERT INTO Itens_carrinho (Nome_produto, Preco_produto, Quantidade, cod_cliente) " +
                                           "VALUES (@nome, @preco, @qtd, @IdCliente)";
                        SqlCommand cmdInsere = new SqlCommand(sqlInsere, con);
                        cmdInsere.Parameters.AddWithValue("@nome", nomeProduto);
                        cmdInsere.Parameters.AddWithValue("@preco", precoProduto);
                        cmdInsere.Parameters.AddWithValue("@qtd", quantidade);
                        cmdInsere.Parameters.AddWithValue("@IdCliente", idDoClienteAtual); // <--- CORREÇÃO
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

        private void button2_Click(object sender, EventArgs e)
        {
            pedidos frm = new pedidos();
            frm.ShowDialog();
        }
    }
}
