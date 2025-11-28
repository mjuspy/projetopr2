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
        string connectionString = @"Data Source=PCZAO;Initial Catalog=cj3027724pr2;Integrated Security=True;";

        // --- 1. VARIÁVEIS GLOBAIS PARA GUARDAR OS DADOS DO BANCO ---
        // (Valores padrão caso o banco falhe, mas eles serão substituídos ao carregar)

        // ID 1: Expresso Machiato
        string nomeProd1 = "EXPRESSO MACHIATO";
        decimal precoProd1 = 8.20m;

        // ID 2: Latte
        string nomeProd2 = "LATTE";
        decimal precoProd2 = 12.00m;

        // ID 3: Cappuccino Italiano
        string nomeProd3 = "CAPPUCCINO ITALIANO";
        decimal precoProd3 = 12.00m;

        // ID 4: Cappuccino Brasileiro
        string nomeProd4 = "CAPPUCCINO BRASILEIRO";
        decimal precoProd4 = 14.00m;

        // ID 5: Mocha
        string nomeProd5 = "MOCHA";
        decimal precoProd5 = 16.00m;

        // ID 6: Expresso Duplo (Caso tenha adicionado depois)
        string nomeProd6 = "EXPRESSO DUPLO";
        decimal precoProd6 = 8.10m;


        public cardapio1()
        {
            InitializeComponent();
        }

        // --- 2. O EVENTO LOAD (AQUI A MÁGICA ACONTECE) ---
        private void cardapio1_Load(object sender, EventArgs e)
        {
            // Deixe os fundos das labels transparentes para ficar bonito em cima do design do Canva
            ConfigurarLabelsTransparentes();

            // Busca no banco e atualiza as variáveis e a tela
            CarregarPrecosDoBanco();
        }

        private void ConfigurarLabelsTransparentes()
        {
            // Exemplo: lblNomeMachiato.Parent = pictureBoxFundo;
            // lblNomeMachiato.BackColor = Color.Transparent;
            // Você deve fazer isso para todas as labels se quiser transparência real em cima da PictureBox
        }

        private void CarregarPrecosDoBanco()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    // Garanta que você tem as colunas 'id', 'nome' e 'preco' na tabela Produtos
                    string sql = "SELECT id, nome, preco FROM Produtos";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["id"]);
                        string nome = reader["nome"].ToString();
                        decimal preco = Convert.ToDecimal(reader["preco"]);

                        // ATUALIZA AS VARIÁVEIS E AS LABELS NA TELA
                        // IMPORTANTE: Você precisa ter criado Labels na tela (ex: lblPrecoMachiato)
                        switch (id)
                        {
                            case 1: // Machiato
                                nomeProd1 = nome; // Atualiza a variável usada no botão
                                precoProd1 = preco;
                                // lblNomeMachiato.Text = nome;  <-- Descomente quando criar a Label
                                // lblPrecoMachiato.Text = "R$ " + preco.ToString("F2"); 
                                break;

                            case 2: // Latte
                                nomeProd2 = nome;
                                precoProd2 = preco;
                                // lblNomeLatte.Text = nome;
                                // lblPrecoLatte.Text = "R$ " + preco.ToString("F2");
                                break;

                            case 3: // Cappuccino Italiano
                                nomeProd3 = nome;
                                precoProd3 = preco;
                                // lblNomeCapItaliano.Text = nome;
                                // lblPrecoCapItaliano.Text = "R$ " + preco.ToString("F2");
                                break;

                            case 4: // Cappuccino Brasileiro
                                nomeProd4 = nome;
                                precoProd4 = preco;
                                // lblNomeCapBrasil.Text = nome;
                                // lblPrecoCapBrasil.Text = "R$ " + preco.ToString("F2");
                                break;

                            case 5: // Mocha
                                nomeProd5 = nome;
                                precoProd5 = preco;
                                // lblNomeMocha.Text = nome;
                                // lblPrecoMocha.Text = "R$ " + preco.ToString("F2");
                                break;

                            // Caso tenha o Expresso Duplo no banco como ID 6
                            case 6:
                                nomeProd6 = nome;
                                precoProd6 = preco;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar cardápio: " + ex.Message);
            }
        }

        private void AdicionarAoCarrinho(string nomeProduto, decimal precoProduto, int quantidade)
        {
            // ... (SEU CÓDIGO DE ADICIONAR AO CARRINHO CONTINUA IGUAL AQUI) ...
            // Vou resumir para não ocupar espaço, mas mantenha sua lógica de Login/SQL aqui.

            if (SessaoUsuario1.UsuarioLogado == null)
            {
                MessageBox.Show("Faça login!");
                new tela_login().ShowDialog();
                if (SessaoUsuario1.UsuarioLogado != null) AdicionarAoCarrinho(nomeProduto, precoProduto, quantidade);
                return;
            }

            int idDoClienteAtual = SessaoUsuario1.UsuarioLogado.Id;

            if (quantidade <= 0)
            {
                MessageBox.Show("Selecione uma quantidade válida.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    // Seu código de SELECT / UPDATE / INSERT ...
                    // ... Mantenha exatamente como você já fez ...

                    // Apenas para exemplo do Insert:
                    string sqlInsere = "INSERT INTO Itens_carrinho (Nome_produto, Preco_produto, Quantidade, cod_cliente) VALUES (@nome, @preco, @qtd, @IdCliente)";
                    // ... execução ...
                }
                MessageBox.Show($"{quantidade}x {nomeProduto} adicionado(s)!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        // --- 3. BOTÕES ATUALIZADOS PARA USAR AS VARIÁVEIS ---

        private void btnAdicionarExpressoMachiato_Click_Click_1(object sender, EventArgs e)
        {
            int quantidade = Convert.ToInt32(numQuantidadeExpressoMachiato.Value);
            // NÃO USA MAIS "Texto Fixo", USA A VARIÁVEL QUE VEIO DO BANCO
            AdicionarAoCarrinho(nomeProd1, precoProd1, quantidade);
        }

        private void btnAdicionarLatte_Click_Click_1(object sender, EventArgs e)
        {
            int quantidade = Convert.ToInt32(numQuantidadeLatte.Value);
            AdicionarAoCarrinho(nomeProd2, precoProd2, quantidade);
        }

        private void btnAdicionarCappuccinoItaliano_Click_Click_1(object sender, EventArgs e)
        {
            int quantidade = Convert.ToInt32(numQuantidadeCappuccinoItaliano.Value);
            AdicionarAoCarrinho(nomeProd3, precoProd3, quantidade);
        }

        private void btnAdicionarCappuccinoBrasileiro_Click_Click_1(object sender, EventArgs e)
        {
            int quantidade = Convert.ToInt32(numQuantidadeCappuccinoBrasileiro.Value);
            AdicionarAoCarrinho(nomeProd4, precoProd4, quantidade);
        }

        private void btnAdicionarMocha_Click_Click_1(object sender, EventArgs e)
        {
            int quantidade = Convert.ToInt32(numQuantidadeMocha.Value);
            AdicionarAoCarrinho(nomeProd5, precoProd5, quantidade);
        }

        private void btnAdicionarExpressoDuplo_Click_Click_1(object sender, EventArgs e)
        {
            int quantidade = Convert.ToInt32(numQuantidadeExpresso.Value);
            // Se o Expresso Duplo não estiver no banco, ele vai usar o valor padrão lá de cima (8.10)
            // Se estiver no banco (ex: ID 6), ele usará o valor atualizado.
            AdicionarAoCarrinho(nomeProd6, precoProd6, quantidade);
        }

        // ... Seus outros botões de navegação ...
        private void button1_Click(object sender, EventArgs e) { new pedidos().ShowDialog(); }
        private void button2_Click(object sender, EventArgs e) { new pedidos().ShowDialog(); }
        private void pictureBox2_Click(object sender, EventArgs e) { this.Close(); }

        // Botões antigos ou vazios
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void btnAdicionarExpressoDuplo_Click_Click(object sender, EventArgs e) { } // Cuidado com cliques duplicados
        private void btnAdicionarExpressoMachiato_Click_Click(object sender, EventArgs e) { }
        private void btnAdicionarLatte_Click_Click(object sender, EventArgs e) { }
        private void btnAdicionarCappuccinoItaliano_Click_Click(object sender, EventArgs e) { }
        private void btnAdicionarCappuccinoBrasileiro_Click_Click(object sender, EventArgs e) { }
        private void btnAdicionarMocha_Click_Click(object sender, EventArgs e) { }
        private void numQuantidadeCappuccinoItaliano_ValueChanged(object sender, EventArgs e) { }
        private void numQuantidadeExpresso_ValueChanged(object sender, EventArgs e) { }
        private void numQuantidadeExpressoMachiato_ValueChanged(object sender, EventArgs e) { }
    }
}