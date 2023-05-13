using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpertPay
{
    public partial class frmCadEmpresas : Form
    {
        public frmCadEmpresas()
        {
            InitializeComponent();
        }

        SqlConnection sqlCon = null;
        private string strCon = "Data Source=DESKTOP-58A1GD0;Initial Catalog=ExpertPay;Integrated Security=SSPI;";

        private string strSql = string.Empty;
        private bool bInsert = false;

        private int currentId; // o ID Atual

        private void habilitaCampos(bool habilita)
        {
            txtRazaoSocial.Enabled = habilita;
            txtNomeFan.Enabled = habilita;
            txtCnpj.Enabled = habilita;
            txtDataCadastro.Enabled = habilita;
            txtIM.Enabled = habilita;
            txtEndereco.Enabled = habilita;
            txtTelefone2.Enabled = habilita;
            txtOcupacao.Enabled = habilita;
            txtIE.Enabled = habilita;
            txtTipo.Enabled = habilita;
            txtTelefone1.Enabled = habilita;
            txtEmail1.Enabled = habilita;
            txtCidade.Enabled = habilita;
            txtEmail2.Enabled = habilita;
            txtCep.Enabled = habilita;
            txtDataCadastro.Enabled = habilita;
            txtNumero.Enabled = habilita;
            txtEstado.Enabled = habilita;
            txtObs.Enabled = habilita;

            chkInativo.Enabled = habilita;


            btnSalvar.Enabled = habilita;
            btnAdicionar.Enabled = !habilita;
            btnAlterar.Enabled = !habilita;

            txtRazaoSocial.Focus();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            habilitaCampos(true);
            bInsert = true;

            txtId.Text = "";
            txtRazaoSocial.Text = "";
            txtNomeFan.Text = "";
            chkInativo.Checked = false;
            txtTipo.Text = "";
            txtCnpj.Text = "";
            txtIE.Text = "";
            txtIM.Text = "";
            txtOcupacao.Text = "";
            txtEndereco.Text = "";
            txtNumero.Text = "";
            txtEstado.Text = "";
            txtCidade.Text = "";
            txtCep.Text = "";
            txtEmail1.Text = "";
            txtEmail2.Text = "";
            txtTelefone1.Text = "";
            txtTelefone2.Text = "";
            txtObs.Text = "";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtRazaoSocial.Text == "")
            {
                MessageBox.Show("Preencha a Razão Social!");
                txtRazaoSocial.Focus();
                return;
            }
            else if (txtCnpj.Text == "")
            {
                MessageBox.Show("Preencha o CNPJ!");
                txtCnpj.Focus();
                return;
            }
            else if (txtEndereco.Text == "")
            {
                MessageBox.Show("Preencha o Endereço!");
                txtEndereco.Focus();
                return;
            }
            else if (txtCidade.Text == "")
            {
                MessageBox.Show("Preencha a Cidade!");
                txtCidade.Focus();
                return;
            };

            // Se eu estiver inserindo, faço um Insert no banco, se não, faço um Update!
            if (bInsert)
                strSql = "INSERT INTO Empresas (RazaoSocial, NomeFan, Ativo, DataCadastro, Tipo, Cnpj, IE, IM, Ocupacao, Endereco, NumeroEndereco, EstadoEndereco, Cidade, CEP, Email1, Email2, Telefone1, Telefone2, Observacoes) " +
                         "VALUES (@RazaoSocial, @NomeFan, @Ativo, @DataCadastro, @Tipo, @Cnpj, @IE, @IM, @Ocupacao, @Endereco, @NumeroEndereco, @EstadoEndereco, @Cidade, @CEP, @Email1, @Email2, @Telefone1, @Telefone2, @Observacoes)";
            else
                strSql = "UPDATE Empresas SET RazaoSocial = @RazaoSocial, NomeFan = @NomeFan, Ativo = @Ativo, " + 
                         "DataCadastro = @DataCadastro, Tipo = @Tipo, IE = @IE, IM = @IM, Ocupacao = @Ocupacao, " +
                         "Endereco = @Endereco, NumeroEndereco = @NumeroEndereco, EstadoEndereco = @EstadoEndereco, " +
                         "Cidade = @Cidade, CEP = @CEP, Email1 = @Email1, Email2 = @Email2, " +
                         "Telefone1 = @Telefone1, Telefone2 = @Telefone2, Observacoes = @Observacoes " +
                         "WHERE Id = @Id;";


            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            // Passo o Id como parametro, apenas se for Update
            if (!bInsert)
                comando.Parameters.Add("@Id", SqlDbType.Int).Value = txtId.Text;

            txtCnpj.Mask = ""; // Limpo a Mascara, para salvar somente os números

            comando.Parameters.AddWithValue("@RazaoSocial", SqlDbType.VarChar).Value = txtRazaoSocial.Text;
            comando.Parameters.AddWithValue("@NomeFan", SqlDbType.VarChar).Value = txtNomeFan.Text;
            comando.Parameters.AddWithValue("@Ativo", SqlDbType.Bit).Value = chkInativo.Checked;
            comando.Parameters.AddWithValue("@DataCadastro", SqlDbType.Date).Value = DateTime.Now;
            comando.Parameters.AddWithValue("@Tipo", SqlDbType.VarChar).Value = txtTipo.Text;
            comando.Parameters.AddWithValue("@Cnpj", SqlDbType.Char).Value = txtCnpj.Text;
            comando.Parameters.AddWithValue("@IE", SqlDbType.VarChar).Value = txtIE.Text;
            comando.Parameters.AddWithValue("@IM", SqlDbType.VarChar).Value = txtIM.Text;
            comando.Parameters.AddWithValue("@Ocupacao", SqlDbType.VarChar).Value = txtOcupacao.Text;
            comando.Parameters.AddWithValue("@Endereco", SqlDbType.VarChar).Value = txtEndereco.Text;

            if (string.IsNullOrEmpty(txtNumero.Text))
                comando.Parameters.AddWithValue("@NumeroEndereco", DBNull.Value);
            else
                comando.Parameters.AddWithValue("@NumeroEndereco", SqlDbType.Int).Value = txtNumero.Text;

            comando.Parameters.AddWithValue("@EstadoEndereco", SqlDbType.Char).Value = txtEstado.Text;
            comando.Parameters.AddWithValue("@Cidade", SqlDbType.VarChar).Value = txtCidade.Text;
            comando.Parameters.AddWithValue("@CEP", SqlDbType.VarChar).Value = txtCep.Text;
            comando.Parameters.AddWithValue("@Email1", SqlDbType.VarChar).Value = txtEmail1.Text;
            comando.Parameters.AddWithValue("@Email2", SqlDbType.VarChar).Value = txtEmail2.Text;

            if (string.IsNullOrEmpty(txtNumero.Text))
                comando.Parameters.AddWithValue("@Telefone1", DBNull.Value);
            else
                comando.Parameters.AddWithValue("@Telefone1", SqlDbType.Int).Value = txtTelefone1.Text;

            if (string.IsNullOrEmpty(txtNumero.Text))
                comando.Parameters.AddWithValue("@Telefone2", DBNull.Value);
            else
                comando.Parameters.AddWithValue("@Telefone2", SqlDbType.Int).Value = txtTelefone2.Text;
            
            comando.Parameters.AddWithValue("@Observacoes", SqlDbType.Text).Value = txtObs.Text;

            try
            {
                sqlCon.Open();
                comando.ExecuteNonQuery();
                if (bInsert)
                    MessageBox.Show("Cadastro realizado com sucesso!!");
                else
                    MessageBox.Show("Cadastro atualizado com sucesso!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                habilitaCampos(false);
                txtCnpj.Mask = "00.000.000/0000-00";
                sqlCon.Close();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            habilitaCampos(true);
            bInsert = false;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCadEmpresas_Load(object sender, EventArgs e)
        {
            habilitaCampos(false);
            DateTime dataAtual = DateTime.Now;
            txtDataCadastro.Text = dataAtual.ToString("dd/MM/yyyy");

            currentId = 0;
            LoadRecord(currentId);

            btnProximo.PerformClick(); // Trazendo o primeiro registro
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (rdbCodigo.Checked)
                    strSql = "Select * from Empresas where Id=@Id";
                else if (rdbNome.Checked)
                    strSql = "Select * from Empresas where Nome=@Nome";
                else if (rdbCnpj.Checked)
                    strSql = "Select * from Empresas where Cnpj=@Cnpj";

                sqlCon = new SqlConnection(strCon);
                SqlCommand comando = new SqlCommand(strSql, sqlCon);

                if (rdbCodigo.Checked)
                    comando.Parameters.Add("@Id", SqlDbType.Int).Value = txtBuscar.Text;
                else if (rdbNome.Checked)
                    comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txtBuscar.Text;
                else if (rdbCnpj.Checked)
                    comando.Parameters.Add("@Cnpj", SqlDbType.Int).Value = txtBuscar.Text;

                try
                {
                    if (txtBuscar.Text == string.Empty)
                    {
                        MessageBox.Show("É necessário digitar um valor!");
                        txtBuscar.Focus();
                    }

                    sqlCon.Open();
                    SqlDataReader dr = comando.ExecuteReader();

                    if (dr.HasRows == false)
                        throw new Exception("Dados não encontrados!");

                    dr.Read();

                    txtId.Text = Convert.ToString(dr["Id"]);
                    txtRazaoSocial.Text = Convert.ToString(dr["RazaoSocial"]);
                    txtNomeFan.Text = Convert.ToString(dr["NomeFan"]);
                    chkInativo.Checked = Convert.ToBoolean(dr["Ativo"]);
                    txtTipo.Text = Convert.ToString(dr["Tipo"]);
                    txtCnpj.Text = Convert.ToString(dr["Cnpj"]);
                    txtIE.Text = Convert.ToString(dr["IE"]);
                    txtIM.Text = Convert.ToString(dr["IM"]);
                    txtOcupacao.Text = Convert.ToString(dr["Ocupacao"]);
                    txtEndereco.Text = Convert.ToString(dr["Endereco"]);
                    txtNumero.Text = Convert.ToString(dr["NumeroEndereco"]);
                    txtEstado.Text = Convert.ToString(dr["EstadoEndereco"]);
                    txtCidade.Text = Convert.ToString(dr["Cidade"]);
                    txtCep.Text = Convert.ToString(dr["CEP"]);
                    txtEmail1.Text = Convert.ToString(dr["Email1"]);
                    txtEmail2.Text = Convert.ToString(dr["Email2"]);
                    txtTelefone1.Text = Convert.ToString(dr["Telefone1"]);
                    txtTelefone2.Text = Convert.ToString(dr["Telefone2"]);
                    txtObs.Text = Convert.ToString(dr["Observacoes"]);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                }
            }
        }

        private void txtCnpj_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Para aceitar somente números!!
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            try
            {
                // incrementa o ID atual para obter o próximo registro
                currentId++;

                // consulta o banco de dados para encontrar o próximo registro com um ID maior
                string query = $"SELECT * FROM Empresas WHERE ID = (SELECT MIN(ID) FROM Empresas WHERE ID = {currentId})";
                // execute a consulta

                sqlCon.Open();

                int ultimoID;

                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                // se houver um registro encontrado
                if (reader.Read())
                    // atualiza o ID atual com o ID do registro encontrado
                    currentId = Convert.ToInt32(reader["ID"]);
                else
                {
                    if (!reader.HasRows)
                    {
                        // fecha o reader
                        reader.Close();
                        sqlCon.Close();

                        using (SqlConnection sqlCon = new SqlConnection(strCon))
                        {
                            SqlCommand command = new SqlCommand("SELECT TOP 1 ID FROM Empresas ORDER BY ID DESC", sqlCon);
                            sqlCon.Open();
                            object result = command.ExecuteScalar();
                            ultimoID = (int)result;
                        }

                        if (ultimoID >= currentId)
                            btnProximo.PerformClick();
                        else
                            // caso não haja um registro encontrado, volta ao registro anterior
                            currentId--;
                    }
                }

                // fecha o reader
                reader.Close();
                sqlCon.Close();

                LoadRecord(currentId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                // decrementa o ID atual para obter o registro anterior
                currentId--;

                // consulta o banco de dados para encontrar o registro anterior com um ID menor
                string query = $"SELECT * FROM Empresas WHERE ID = (SELECT MAX(ID) FROM Empresas WHERE ID = {currentId})";
                // execute a consulta

                sqlCon.Open();

                int primeiroID;

                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                // se houver um registro encontrado
                if (reader.Read())
                    // atualiza o ID atual com o ID do registro encontrado
                    currentId = Convert.ToInt32(reader["ID"]);
                else
                {
                    if (!reader.HasRows)
                    {
                        // fecha o reader
                        reader.Close();
                        sqlCon.Close();

                        using (SqlConnection sqlCon = new SqlConnection(strCon))
                        {
                            SqlCommand command = new SqlCommand("SELECT TOP 1 ID FROM Empresas ORDER BY ID ASC", sqlCon);
                            sqlCon.Open();
                            object result = command.ExecuteScalar();
                            primeiroID = (int)result;
                        }

                        if (primeiroID <= currentId)
                            btnAnterior.PerformClick();
                        else
                            // caso não haja um registro encontrado, volta ao registro seguinte
                            currentId++;
                    }
                }

                // fecha o reader
                reader.Close();
                sqlCon.Close();

                LoadRecord(currentId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LoadRecord(int id)
        {
            sqlCon = new SqlConnection(strCon);

            SqlCommand cmd = new SqlCommand("SELECT * FROM Empresas WHERE ID = @id", sqlCon);
            cmd.Parameters.AddWithValue("@id", id);

            sqlCon.Close();

            sqlCon.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                txtId.Text = reader["Id"].ToString();
                txtRazaoSocial.Text = reader["RazaoSocial"].ToString();

                chkInativo.Checked = Convert.ToBoolean(reader["Ativo"]);
                txtTipo.Text = Convert.ToString(reader["Tipo"]);
                txtCnpj.Text = Convert.ToString(reader["Cnpj"]);
                txtIE.Text = Convert.ToString(reader["IE"]);
                txtIM.Text = Convert.ToString(reader["IM"]);
                txtOcupacao.Text = Convert.ToString(reader["Ocupacao"]);
                txtEndereco.Text = Convert.ToString(reader["Endereco"]);
                txtNumero.Text = Convert.ToString(reader["NumeroEndereco"]);
                txtEstado.Text = Convert.ToString(reader["EstadoEndereco"]);
                txtCidade.Text = Convert.ToString(reader["Cidade"]);
                txtCep.Text = Convert.ToString(reader["CEP"]);
                txtEmail1.Text = Convert.ToString(reader["Email1"]);
                txtEmail2.Text = Convert.ToString(reader["Email2"]);
                txtTelefone1.Text = Convert.ToString(reader["Telefone1"]);
                txtTelefone2.Text = Convert.ToString(reader["Telefone2"]);
                txtObs.Text = Convert.ToString(reader["Observacoes"]);
            }

            reader.Close();
            sqlCon.Close();
        }
    }
}
