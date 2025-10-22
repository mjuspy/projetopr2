using Newtonsoft.Json;
using System;
using System.Data.SqlClient; // ADICIONADO: Necessário para interagir com o SQL Server
using System.Net.Http;
using System.Windows.Forms;

// CORREÇÃO: Usings duplicados foram removidos para deixar o código mais limpo.

namespace projetopr2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnBuscarCEP_Click(object sender, EventArgs e)
        {
            // CORREÇÃO: Havia uma chave { extra aqui, que foi removida.

            // Pega o CEP do TextBox e remove traços ou pontos
            string cep = txtCEP.Text.Replace("-", "").Replace(".", "");

            // Verifica se o CEP tem 8 dígitos
            if (cep.Length != 8)
            {
                MessageBox.Show("Por favor, digite um CEP válido com 8 dígitos.");
                return;
            }

            // Monta a URL da API
            string url = $"https://viacep.com.br/ws/{cep}/json/";

            try
            {
                // Cria um cliente HTTP para fazer a requisição
                using (HttpClient client = new HttpClient())
                {
                    // Faz a requisição e espera pela resposta
                    string json = await client.GetStringAsync(url);

                    // Se a resposta contiver a palavra "erro", o CEP é inválido
                    if (json.Contains("\"erro\": true"))
                    {
                        MessageBox.Show("CEP não encontrado. Verifique o número digitado.");
                        return;
                    }

                    // "Traduz" a resposta JSON para um objeto dinâmico
                    dynamic resultado = JsonConvert.DeserializeObject(json);

                    // Preenche os TextBoxes com os dados recebidos
                    txtRua.Text = resultado.logradouro;
                    txtBairro.Text = resultado.bairro;
                    txtCidade.Text = resultado.localidade;
                    txtEstado.Text = resultado.uf;

                    // Coloca o foco no campo de número, para o usuário preencher
                    txtNumero.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao buscar o CEP: " + ex.Message);
            }
        }

        private void btnSalvarEndereco_Click(object sender, EventArgs e)
        {
            // --- 1. Validação ---
            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show("Por favor, preencha o número do endereço.", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumero.Focus();
                return; // Para a execução aqui se o campo estiver vazio
            }

            // --- 2. Obter o ID do Usuário Logado ---
            // Usando a sua classe de sessão
            int usuarioLogadoID = SessaoUsuari.ID_usuario;

            // --- 3. Preparar e Executar o Comando SQL ---
            // CORREÇÃO: A string de conexão e a query SQL foram movidas para DENTRO do método.

            // !! IMPORTANTE !! Substitua pela sua string de conexão real
            string stringDeConexao = "Server=SEU_SERVIDOR;Database=SEU_BANCO;User Id=SEU_USUARIO;Password=SUA_SENHA;";

            // Usei a sua query SQL com os nomes das suas colunas (cod_cliente, etc)
            string sqlQuery = @"INSERT INTO Enderecos 
                                  (cod_cliente, CEP, Rua, Numero, Complemento, Bairro, Cidade, Estado, Apelido) 
                                VALUES 
                                  (@cod_cliente, @CEP, @Rua, @Numero, @Complemento, @Bairro, @Cidade, @Estado, @Apelido)";

            // CORREÇÃO: O bloco try-catch foi colocado aqui, dentro do método, para executar a lógica.
            try
            {
                using (SqlConnection conexao = new SqlConnection(stringDeConexao))
                {
                    using (SqlCommand comando = new SqlCommand(sqlQuery, conexao))
                    {
                        // Adiciona os parâmetros para evitar SQL Injection
                        // Note que usei @cod_cliente, como na sua query
                        comando.Parameters.AddWithValue("@cod_cliente", usuarioLogadoID);
                        comando.Parameters.AddWithValue("@CEP", txtCEP.Text);
                        comando.Parameters.AddWithValue("@Rua", txtRua.Text);
                        comando.Parameters.AddWithValue("@Numero", txtNumero.Text);
                        comando.Parameters.AddWithValue("@Complemento", string.IsNullOrEmpty(txtComplemento.Text) ? (object)DBNull.Value : txtComplemento.Text);
                        comando.Parameters.AddWithValue("@Bairro", txtBairro.Text);
                        comando.Parameters.AddWithValue("@Cidade", txtCidade.Text);
                        comando.Parameters.AddWithValue("@Estado", txtEstado.Text);
                        //comando.Parameters.AddWithValue("@Apelido", string.IsNullOrEmpty(txtApelido.Text) ? (object)DBNull.Value : txtApelido.Text);

                        conexao.Open(); // Abre a conexão com o banco
                        comando.ExecuteNonQuery(); // Executa o comando de inserção

                        MessageBox.Show("Endereço salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Fecha o formulário de cadastro de endereço
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao salvar o endereço: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}