using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpertPay
{
    public partial class frmCadCargos : Form
    {
        public frmCadCargos()
        {
            InitializeComponent();
        }

        SqlConnection sqlCon = null;
        private string strCon = "Data Source=DESKTOP-58A1GD0;Initial Catalog=ExpertPay;Integrated Security=SSPI;";
        //private string strCon = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ExpertPay;Data Source=ExpertPay";
        
        private string strSql = string.Empty;
        private bool bInsert = false;

        private int currentId; // o ID Atual

        private void habilitaCampos(bool habilita)
        {
            txtDescricao.Enabled = habilita;
            txtNomeReduzido.Enabled = habilita;
            cmbNivel.Enabled = habilita;
            txtDataCadastro.Enabled = habilita;
            txtAtribuicoes.Enabled = habilita;

            btnSalvar.Enabled = habilita;

            btnAdicionar.Enabled = !habilita;
            btnAlterar.Enabled = !habilita;
            btnExcluir.Enabled = !habilita;
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            habilitaCampos(true);
            bInsert = true;

            txtId.Text = "";
            txtDescricao.Text = "";
            txtNomeReduzido.Text = "";
            cmbNivel.Text = "";
            txtDataCadastro.Text = "";
            txtAtribuicoes.Text = "";

            txtDescricao.Focus();
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtDescricao.Text == "")
            {
                MessageBox.Show("Preencha a Descrição!");
                txtDescricao.Focus();
                return;
            }
            else if (cmbNivel.Text == "")
            {
                MessageBox.Show("Preencha o Nível!");
                cmbNivel.Focus();
                return;
            }
            else if (txtAtribuicoes.Text == "")
            {
                MessageBox.Show("Preencha as Atribuições!");
                txtAtribuicoes.Focus();
                return;
            };

            // Se eu estiver inserindo, faço um Insert no banco, se não, faço um Update!
            if (bInsert)
                strSql = "INSERT INTO Cargos (Descricao, NomeReduzido, Nivel, DataCadastro, Atribuicoes) " +
                         "VALUES (@Descricao, @NomeReduzido, @Nivel, @DataCadastro, @Atribuicoes)";
            else
                strSql = "UPDATE Cargos SET Descricao = @Descricao, NomeReduzido = @NomeReduzido, Nivel = @Nivel, " +
                         "DataCadastro = @DataCadastro, Atribuicoes = @Atribuicoes " +
                         "WHERE Id = @Id";


            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            // Passo o Id como parametro, apenas se for Update
            if (!bInsert)
                comando.Parameters.Add("@Id", SqlDbType.Int).Value = txtId.Text;

            comando.Parameters.AddWithValue("@Descricao", SqlDbType.VarChar).Value = txtDescricao.Text;
            comando.Parameters.AddWithValue("@NomeReduzido", SqlDbType.VarChar).Value = txtNomeReduzido.Text;
            comando.Parameters.AddWithValue("@Nivel", SqlDbType.VarChar).Value = cmbNivel.Text;
            comando.Parameters.AddWithValue("@DataCadastro", SqlDbType.Date).Value = DateTime.Now;
            comando.Parameters.AddWithValue("@Atribuicoes", SqlDbType.VarChar).Value = txtAtribuicoes.Text;

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
                sqlCon.Close();
            }
        }

        private void frmCadCargos_Load(object sender, EventArgs e)
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
                    strSql = "Select * from Cargos where Id=@Id";
                else if (rdbNome.Checked)
                    strSql = "Select * from Cargos where Nome=@Nome";

                sqlCon = new SqlConnection(strCon);
                SqlCommand comando = new SqlCommand(strSql, sqlCon);

                if (rdbCodigo.Checked)
                    comando.Parameters.Add("@Id", SqlDbType.Int).Value = txtBuscar.Text;
                else if (rdbNome.Checked)
                    comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txtBuscar.Text;

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
                    txtDescricao.Text = Convert.ToString(dr["Descricao"]);
                    txtNomeReduzido.Text = Convert.ToString(dr["NomeReduzido"]);
                    cmbNivel.Text = Convert.ToString(dr["Nivel"]);
                    txtDataCadastro.Text = dr["DataCadastro"].ToString();
                    txtAtribuicoes.Text = Convert.ToString(dr["Atribuicoes"]);

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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // Se confirmado, excluo o registro selecionado
            if (MessageBox.Show("Confirma a exclusão?", "Cuidado", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {

                strSql = "Delete From Cargos WHERE Id = @Id";

                sqlCon = new SqlConnection(strCon);
                SqlCommand comando = new SqlCommand(strSql, sqlCon);

                // Passo o meu Id como parametro
                comando.Parameters.Add("@Id", SqlDbType.Int).Value = txtId.Text;

                try
                {
                    sqlCon.Open();
                    comando.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw;
                }
                finally
                {
                    sqlCon.Close();
                    btnAnterior.PerformClick();
                }
            }
        }

        private void frmCadCargos_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    if (btnAdicionar.Enabled)
                        btnAdicionar.PerformClick();
                    break;
                case Keys.F6:
                    if (btnAlterar.Enabled)
                        btnAlterar.PerformClick();
                    break;
                case Keys.F7:
                    if (btnExcluir.Enabled)
                        btnExcluir.PerformClick();
                    break;
                case Keys.F8:
                    if (btnSalvar.Enabled)
                        btnSalvar.PerformClick();
                    break;
                case Keys.F9:
                    this.Close();
                    break;
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            try
            {
                // incrementa o ID atual para obter o próximo registro
                currentId++;

                // consulta o banco de dados para encontrar o próximo registro com um ID maior
                string query = $"SELECT * FROM Cargos WHERE ID = (SELECT MIN(ID) FROM Cargos WHERE ID = {currentId})";
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
                            SqlCommand command = new SqlCommand("SELECT TOP 1 ID FROM Cargos ORDER BY ID DESC", sqlCon);
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
                string query = $"SELECT * FROM Cargos WHERE ID = (SELECT MAX(ID) FROM Cargos WHERE ID = {currentId})";
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
                            SqlCommand command = new SqlCommand("SELECT TOP 1 ID FROM Cargos ORDER BY ID ASC", sqlCon);
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

            SqlCommand cmd = new SqlCommand("SELECT * FROM Cargos WHERE ID = @id", sqlCon);
            cmd.Parameters.AddWithValue("@id", id);

            sqlCon.Close();

            sqlCon.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                txtId.Text = reader["Id"].ToString();
                txtDescricao.Text = reader["Descricao"].ToString();
                txtNomeReduzido.Text = reader["NomeReduzido"].ToString();
                cmbNivel.Text = reader["Nivel"].ToString();
                txtDataCadastro.Text = reader["DataCadastro"].ToString();
                txtAtribuicoes.Text = reader["Atribuicoes"].ToString();
            }

            reader.Close();
            sqlCon.Close();
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
                txtBuscar.Text = "Buscar Cargo...";
        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
        }
    }
}
