using Microsoft.VisualBasic;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic; // Para usar InputBox

namespace projetopr2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Mostrar informações do usuário
            labelNome.Text = SessaoUsuario.Nome;
            labelEmail.Text = SessaoUsuario.Email;

            // Carregar endereços
            CarregarEnderecos();
        }

        private void CarregarEnderecos()
        {
            listBoxEnderecos.Items.Clear();
            try
            {
                using (var conn = new SqlConnection(@"Data Source=SQLEXPRESS;Initial Catalog=cj3027724pr2;User ID=aluno;Password=aluno"))
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT * FROM Enderecos WHERE IdCliente=@id", conn);
                    cmd.Parameters.AddWithValue("@id", SessaoUsuario.Id);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string endereco = $"{reader["Rua"]}, {reader["Numero"]} - {reader["Cidade"]}";
                        listBoxEnderecos.Items.Add(endereco);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar endereços: " + ex.Message);
            }
        }

        private void buttonAdicionarEndereco_Click(object sender, EventArgs e)
        {
            string novoEndereco = Interaction.InputBox("Digite o endereço no formato: Rua, Número - Cidade", "Adicionar Endereço");

            if (!string.IsNullOrWhiteSpace(novoEndereco))
            {
                try
                {
                    using (var conn = new SqlConnection(@"Data Source=SQLEXPRESS;Initial Catalog=cj3027724pr2;User ID=aluno;Password=aluno"))
                    {
                        conn.Open();
                        var cmd = new SqlCommand(
                            "INSERT INTO Enderecos (IdCliente, Rua, Numero, Cidade) VALUES (@id, @rua, @numero, @cidade)", conn);

                        var partes = novoEndereco.Split(new char[] { ',', '-' }, StringSplitOptions.RemoveEmptyEntries);
                        if (partes.Length == 3)
                        {
                            cmd.Parameters.AddWithValue("@id", SessaoUsuario.Id);
                            cmd.Parameters.AddWithValue("@rua", partes[0].Trim());
                            cmd.Parameters.AddWithValue("@numero", partes[1].Trim());
                            cmd.Parameters.AddWithValue("@cidade", partes[2].Trim());
                            cmd.ExecuteNonQuery();

                            CarregarEnderecos();
                        }
                        else
                        {
                            MessageBox.Show("Formato inválido. Use: Rua, Número - Cidade");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar endereço: " + ex.Message);
                }
            }
        }

        private void buttonRemoverEndereco_Click(object sender, EventArgs e)
        {
            if (listBoxEnderecos.SelectedItem != null)
            {
                string enderecoSelecionado = listBoxEnderecos.SelectedItem.ToString();
                var confirm = MessageBox.Show($"Deseja remover o endereço:\n{enderecoSelecionado}?", "Remover Endereço", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        using (var conn = new SqlConnection(@"Data Source=SQLEXPRESS;Initial Catalog=cj3027724pr2;User ID=aluno;Password=aluno"))
                        {
                            conn.Open();
                            var partes = enderecoSelecionado.Split(new char[] { ',', '-' }, StringSplitOptions.RemoveEmptyEntries);
                            if (partes.Length == 3)
                            {
                                var cmd = new SqlCommand(
                                    "DELETE FROM Enderecos WHERE IdCliente=@id AND Rua=@rua AND Numero=@numero AND Cidade=@cidade", conn);
                                cmd.Parameters.AddWithValue("@id", SessaoUsuario.Id);
                                cmd.Parameters.AddWithValue("@rua", partes[0].Trim());
                                cmd.Parameters.AddWithValue("@numero", partes[1].Trim());
                                cmd.Parameters.AddWithValue("@cidade", partes[2].Trim());
                                cmd.ExecuteNonQuery();

                                CarregarEnderecos();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao remover endereço: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um endereço para remover.");
            }
        }
    }
}
