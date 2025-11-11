using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace projetopr2
{
    public partial class formListaEnderecos : Form
    {
        string connectionString = @"Data Source=PCZAO;Initial Catalog=cj3027724pr2;Integrated Security=True;";

        public string EnderecoSelecionado { get; private set; }

        public formListaEnderecos()
        {
            InitializeComponent();
        }

        private void formListaEnderecos_Load(object sender, EventArgs e)
        {
            CarregarEnderecos();
            EstilizarDataGridView();

            dgvEnderecos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEnderecos.MultiSelect = false;
            dgvEnderecos.ReadOnly = true;
        }

        // 🎨 Deixa o DataGridView no mesmo estilo do outro formulário
        private void EstilizarDataGridView()
        {
            dgvEnderecos.BorderStyle = BorderStyle.None;
            dgvEnderecos.BackgroundColor = ColorTranslator.FromHtml("#fadfb5");
            dgvEnderecos.EnableHeadersVisualStyles = false;

            dgvEnderecos.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#f2cfa0");
            dgvEnderecos.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#3a2e2a");
            dgvEnderecos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvEnderecos.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#fff7e6");
            dgvEnderecos.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#3a2e2a");
            dgvEnderecos.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#f7d9a5");
            dgvEnderecos.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#3a2e2a");
            dgvEnderecos.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dgvEnderecos.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#faebd7");
            dgvEnderecos.GridColor = ColorTranslator.FromHtml("#e5c99b");

            dgvEnderecos.RowHeadersVisible = false;
            dgvEnderecos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // 📦 Carrega os endereços do usuário logado
        private void CarregarEnderecos()
        {
            try
            {
                if (SessaoUsuario1.UsuarioLogado == null)
                {
                    MessageBox.Show("Erro: Nenhum usuário logado.");
                    return;
                }

                int idUsuarioLogado = SessaoUsuario1.UsuarioLogado.Id;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = @"SELECT 
                                    EnderecoID AS [ID], 
                                    rua AS [Rua], 
                                    numero AS [Número], 
                                    bairro AS [Bairro], 
                                    cidade AS [Cidade], 
                                    estado AS [Estado], 
                                    cep AS [CEP]
                                   FROM Enderecos 
                                   WHERE cod_cliente = @id";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id", idUsuarioLogado);

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

        // ✅ Seleciona o endereço ao clicar em qualquer célula
        private void dgvEnderecos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                dgvEnderecos.CurrentCell = dgvEnderecos.Rows[e.RowIndex].Cells[e.ColumnIndex];
        }

        // 📍 Selecionar endereço e retornar ao formulário anterior
        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (dgvEnderecos.CurrentRow == null)
            {
                MessageBox.Show("Selecione um endereço para continuar.");
                return;
            }

            DataGridViewRow linha = dgvEnderecos.CurrentRow;

            string rua = linha.Cells["Rua"].Value.ToString();
            string numero = linha.Cells["Número"].Value.ToString();
            string bairro = linha.Cells["Bairro"].Value.ToString();
            string cidade = linha.Cells["Cidade"].Value.ToString();
            string estado = linha.Cells["Estado"].Value.ToString();
            string cep = linha.Cells["CEP"].Value.ToString();

            EnderecoSelecionado = $"{rua}, {numero}, {bairro}, {cidade} - {estado}, CEP: {cep}";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // ➕ Adicionar novo endereço e recarregar lista
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            Form1 novoEndereco = new Form1();
            novoEndereco.ShowDialog();
            CarregarEnderecos();
        }

        // ❌ Fechar formulário
        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
