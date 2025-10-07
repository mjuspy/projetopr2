using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace projetopr2
{
    public partial class Form2 : Form
    {
        // Lista para armazenar produtos do carrinho
        List<Produto> carrinho = new List<Produto>();

        public Form2()
        {
            InitializeComponent();
            InicializarProdutos();
        }

        // Produtos disponíveis para compra
        private List<Produto> produtos = new List<Produto>();
        private void InicializarProdutos()
        {
            produtos.Add(new Produto("Expresso", 4.00));
            produtos.Add(new Produto("Cappuccino", 5.50));
            produtos.Add(new Produto("Mocca", 7.00));

            // Adicionar produtos no ComboBox
            foreach (var p in produtos)
            {
                comboBoxProdutos.Items.Add(p.Nome);
            }
            comboBoxProdutos.SelectedIndex = 0;
        }

        // Botão Adicionar ao carrinho
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            var produtoSelecionado = produtos[comboBoxProdutos.SelectedIndex];
            int quantidade = (int)numericUpDownQuantidade.Value;

            // Adicionar no carrinho
            carrinho.Add(new Produto(produtoSelecionado.Nome, produtoSelecionado.Preco, quantidade));

            AtualizarCarrinho();
        }

        // Atualiza a ListBox do carrinho e total
        private void AtualizarCarrinho()
        {
            listBoxCarrinho.Items.Clear();
            double total = 0;

            foreach (var item in carrinho)
            {
                listBoxCarrinho.Items.Add($"{item.Nome} - {item.Quantidade}x R${item.Preco} = R${item.Preco * item.Quantidade}");
                total += item.Preco * item.Quantidade;
            }

            lblTotal.Text = $"Total: R${total}";
        }

        // Botão Limpar carrinho
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            carrinho.Clear();
            AtualizarCarrinho();
        }
    }

    // Classe Produto
    public class Produto
    {
        public string Nome { get; set; }
        public double Preco { get; set; }
        public int Quantidade { get; set; }

        public Produto(string nome, double preco, int quantidade = 1)
        {
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }
    }
}
