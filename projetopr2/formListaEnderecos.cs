using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace projetopr2
{
    public partial class formListaEnderecos : Form
    {
        string connectionString = @"Data Source=PCZAO;Initial Catalog=cj3027724pr2;Integrated Security=True;";
        
        public string EnderecoSelecionado { get; private set; } // devolve o endereço escolhido

        public formListaEnderecos()
        {
            InitializeComponent();
        }

        private void formListaEnderecos_Load(object sender, EventArgs e)
        {
            CarregarEnderecos();
        }

        private void CarregarEnderecos()
        {
            try
            {
                // 1. VERIFICAR SE O USUÁRIO ESTÁ LOGADO
                if (SessaoUsuario1.UsuarioLogado == null)
                {
                    MessageBox.Show("Erro: Não há usuário logado.");
                    return;
                }

                // 2. PEGAR O ID DA SESSÃO CORRETA
                int idUsuarioLogado = SessaoUsuario1.UsuarioLogado.Id;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "SELECT EnderecoID, rua, numero, bairro, cidade, estado, cep FROM Enderecos WHERE cod_cliente = @id";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    // 3. USAR O ID DA SESSÃO NA QUERY
                    cmd.Parameters.AddWithValue("@id", idUsuarioLogado); // <-- CORRIGIDO!

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvEnderecos.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar endereços: " + ex.Message);
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (dgvEnderecos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um endereço para continuar.");
                return;
            }

            DataGridViewRow linha = dgvEnderecos.SelectedRows[0];
            string rua = linha.Cells["rua"].Value.ToString();
            string numero = linha.Cells["numero"].Value.ToString();
            string bairro = linha.Cells["bairro"].Value.ToString();
            string cidade = linha.Cells["cidade"].Value.ToString();
            string estado = linha.Cells["estado"].Value.ToString();
            string cep = linha.Cells["cep"].Value.ToString();

            EnderecoSelecionado = $"{rua}, {numero}, {bairro}, {cidade} - {estado}, CEP: {cep}";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            // abre o form de cadastro de endereço que você já tem
            Form1 novoEndereco = new Form1();
            novoEndereco.ShowDialog();

            // recarrega a lista após o cadastro
            CarregarEnderecos();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvEnderecos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
