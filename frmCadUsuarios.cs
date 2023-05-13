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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpertPay
{
    public partial class frmCadUsuarios : Form
    {
        public frmCadUsuarios()
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
            txtLogin.Enabled = habilita;
            txtSenha.Enabled = habilita;
            cmbEmpresas.Enabled = habilita;
            cmbCargos.Enabled = habilita;

            btnSalvar.Enabled = habilita;
            btnAdicionar.Enabled = !habilita;
            btnAlterar.Enabled = !habilita;
            btnExcluir.Enabled = !habilita;

            txtLogin.Focus();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            habilitaCampos(true);
            bInsert = true;

            txtId.Text = "";
            txtLogin.Text = "";
            txtSenha.Text = "";
            cmbEmpresas.Text = "";
            cmbCargos.Text = "";

            cmbEmpresas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCargos.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            habilitaCampos(true);
            bInsert = false;

            cmbEmpresas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCargos.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Verificando Login Existente
        private bool LoginJaExiste(string login)
        {
            bool existe = false;

            // Criar a conexão com o banco de dados
            using (SqlConnection conexao = new SqlConnection(strCon))
            {
                // Definir o comando SQL que será executado
                string sql = "SELECT COUNT(*) FROM Login WHERE Usuario = @login";
                SqlCommand comando = new SqlCommand(sql, conexao);

                // Adicionar o parâmetro para o login
                SqlParameter parametro = new SqlParameter("@login", SqlDbType.VarChar);
                parametro.Value = login;
                comando.Parameters.Add(parametro);

                // Abrir a conexão com o banco de dados
                conexao.Open();

                // Executar o comando SQL e obter o resultado
                int count = (int)comando.ExecuteScalar();

                // Verificar se o login já existe
                if (count > 0)
                {
                    existe = true;
                }
            }

            return existe;
        }

        private class ItemComboBox
        {
            public int Id { get; set; }
            public string Descricao { get; set; }

            public override string ToString()
            {
                return Descricao;
            }
        }

        private void frmCadUsuarios_Load(object sender, EventArgs e)
        {
            habilitaCampos(false);

            // Pegando as Empresas !!
            strSql = string.Empty;
            strSql = "SELECT Id, RazaoSocial FROM Empresas Where Ativo = 0";

            sqlCon = new SqlConnection(strCon);
            SqlCommand comandoEmp = new SqlCommand(strSql, sqlCon);

            sqlCon.Open();

            SqlDataReader reader = comandoEmp.ExecuteReader();

            if (reader.HasRows == false)
                throw new Exception("Dados não encontrados!");

            while (reader.Read())
            {
                ItemComboBox item = new ItemComboBox();
                item.Id = reader.GetInt32(0);
                item.Descricao = reader.GetString(1);
                cmbEmpresas.Items.Add(item);
            }

            reader.Close();
            reader.Dispose();

            // Pegando os Cargos!
            strSql = string.Empty;
            strSql = "SELECT Id, Descricao FROM Cargos";

            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            sqlCon.Open();

            SqlDataReader rd = comando.ExecuteReader();

            if (rd.HasRows == false)
                throw new Exception("Dados não encontrados!");

            while (rd.Read())
            {
                ItemComboBox item = new ItemComboBox();
                item.Id = rd.GetInt32(0);
                item.Descricao = rd.GetString(1);
                cmbCargos.Items.Add(item);
            }

            rd.Close();
            rd.Dispose();

            currentId = 0;
            LoadRecord(currentId);

            btnProximo.PerformClick(); // Trazendo o primeiro registro
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text == "")
            {
                MessageBox.Show("Preencha o Login!");
                txtLogin.Focus();
                return;
            }
            else if (txtSenha.Text == "")
            {
                MessageBox.Show("Preencha a Senha!");
                txtSenha.Focus();
                return;
            }
            else if (cmbEmpresas.Text == "")
            {
                MessageBox.Show("Preencha a Empresa!");
                cmbEmpresas.Focus();
                return;
            }
            else if (cmbCargos.Text == "")
            {
                MessageBox.Show("Preencha o Cargo!");
                cmbCargos.Focus();
                return;
            };

            if (bInsert)
                if (LoginJaExiste(txtLogin.Text))
                {
                    MessageBox.Show("O Login já existe!");
                    txtLogin.Focus();
                    return;
                }

            cmbEmpresas.DropDownStyle = ComboBoxStyle.DropDown;
            cmbCargos.DropDownStyle = ComboBoxStyle.DropDown;

            // Se eu estiver inserindo, faço um Insert no banco, se não, faço um Update!
            if (bInsert)
                strSql = "INSERT INTO Login (Usuario, Senha, IdEmpresas, IdCargos) " +
                         "VALUES (@Login, @Senha, @IdEmpresas, @IdCargos)";
            else
                strSql = "UPDATE Login SET Usuario = @Login, IdEmpresas = @IdEmpresas, IdCargos = @IdCargos " +
                         "WHERE Id = @Id";

            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            // Pego apenas o ID do meu combo, para salvar no banco!
            ItemComboBox comboEmpresa = (ItemComboBox)cmbEmpresas.SelectedItem;
            ItemComboBox comboCargos = (ItemComboBox)cmbCargos.SelectedItem;

            // Passo o Id como parametro, apenas se for Update
            if (!bInsert)
                comando.Parameters.Add("@Id", SqlDbType.Int).Value = txtId.Text;

            comando.Parameters.AddWithValue("@Login", SqlDbType.VarChar).Value = txtLogin.Text;
            comando.Parameters.AddWithValue("@Senha", SqlDbType.VarChar).Value = txtSenha.Text;
            comando.Parameters.AddWithValue("@IdEmpresas", SqlDbType.Int).Value = comboEmpresa.Id;
            comando.Parameters.AddWithValue("@IdCargos", SqlDbType.Int).Value = comboCargos.Id;

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

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                // decrementa o ID atual para obter o registro anterior
                currentId--;

                // consulta o banco de dados para encontrar o registro anterior com um ID menor
                string query = $"SELECT * FROM Login WHERE ID = (SELECT MAX(ID) FROM Login WHERE ID = {currentId})";
                // execute a consulta

                sqlCon.Open();

                int primeiroID;

                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                // se houver um registro encontrado
                if (reader.Read())
                {
                    // preenche os campos do formulário com os dados do registro anterior

                    txtId.Text = reader["ID"].ToString();
                    txtLogin.Text = reader["Usuario"].ToString();
                    txtSenha.Text = reader["Senha"].ToString();
                    cmbEmpresas.Text = reader["IdEmpresas"].ToString();
                    cmbCargos.Text = reader["IdCargos"].ToString();

                    // atualiza o ID atual com o ID do registro encontrado
                    currentId = Convert.ToInt32(reader["ID"]);
                }
                else
                {
                    if (!reader.HasRows)
                    {
                        // fecha o reader
                        reader.Close();
                        sqlCon.Close();

                        using (SqlConnection sqlCon = new SqlConnection(strCon))
                        {
                            SqlCommand command = new SqlCommand("SELECT TOP 1 ID FROM Login ORDER BY ID ASC", sqlCon);
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
            }
            catch (Exception)
            {
                throw;
            }

             LoadRecord(currentId);

             PreencherDescricao(cmbEmpresas, "RazaoSocial", "Empresas");
             PreencherDescricao(cmbCargos, "Descricao", "Cargos"); 
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            try
            {
                // incrementa o ID atual para obter o próximo registro
                currentId++;

                // consulta o banco de dados para encontrar o próximo registro com um ID maior
                string query = $"SELECT * FROM Login WHERE ID = (SELECT MIN(ID) FROM Login WHERE ID = {currentId})";
                // execute a consulta

                sqlCon.Open();

                int ultimoID;

                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                // se houver um registro encontrado
                if (reader.Read())
                {
                    // preenche os campos do formulário com os dados do próximo registro

                    txtId.Text = reader["ID"].ToString();
                    txtLogin.Text = reader["Usuario"].ToString();
                    txtSenha.Text = reader["Senha"].ToString();
                    cmbEmpresas.Text = reader["IdEmpresas"].ToString();
                    cmbCargos.Text = reader["IdCargos"].ToString();

                    // atualiza o ID atual com o ID do registro encontrado
                    currentId = Convert.ToInt32(reader["ID"]);
                }
                else
                {
                    if (!reader.HasRows)
                    {
                        // fecha o reader
                        reader.Close();
                        sqlCon.Close();

                        using (SqlConnection sqlCon = new SqlConnection(strCon))
                        {
                            SqlCommand command = new SqlCommand("SELECT TOP 1 ID FROM Login ORDER BY ID DESC", sqlCon);
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

                PreencherDescricao(cmbEmpresas, "RazaoSocial", "Empresas");
                PreencherDescricao(cmbCargos, "Descricao", "Cargos");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void LoadRecord(int id)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Login WHERE ID = @id", sqlCon);
            cmd.Parameters.AddWithValue("@id", id);

            sqlCon.Close();

            sqlCon.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                txtId.Text = reader["ID"].ToString();
                txtLogin.Text = reader["Usuario"].ToString();
                txtSenha.Text = reader["Senha"].ToString();
                cmbEmpresas.Text = reader["IdEmpresas"].ToString();
                cmbCargos.Text = reader["IdCargos"].ToString();

            }
            reader.Close();
            sqlCon.Close();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (rdbCodigo.Checked)
                    strSql = "Select * from Login where Id=@Id";
                else if (rdbNome.Checked)
                    strSql = "Select * from Login where Usuario=@Usuario";

                sqlCon = new SqlConnection(strCon);
                SqlCommand comando = new SqlCommand(strSql, sqlCon);

                if (rdbCodigo.Checked)
                    comando.Parameters.Add("@Id", SqlDbType.Int).Value = txtBuscar.Text;
                else if (rdbNome.Checked)
                    comando.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = txtBuscar.Text;

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
                    txtLogin.Text = Convert.ToString(dr["Usuario"]);
                    txtSenha.Text = Convert.ToString(dr["Senha"]);
                    cmbEmpresas.Text = Convert.ToString(dr["IdEmpresas"]);
                    cmbCargos.Text = Convert.ToString(dr["IdCargos"]);

                    PreencherDescricao(cmbEmpresas, "RazaoSocial", "Empresas");
                    PreencherDescricao(cmbCargos, "Descricao", "Cargos");
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

        private void PreencherDescricao(System.Windows.Forms.ComboBox cmb, string campo, string tabela)
        {
            if (cmb.Text != "")
            {
                // De Acordo com o ID preenchido, faço o Select e pego a Descrição
                string strSql = $"SELECT {campo} FROM {tabela} WHERE ID = {cmb.Text}";

                using (SqlConnection sqlCon = new SqlConnection(strCon))
                {
                    sqlCon.Open();

                    SqlCommand cmd = new SqlCommand(strSql, sqlCon);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string descricao = reader.GetString(0);

                            // exibir a descrição na caixa de texto
                            cmb.Text = descricao;
                        }
                    }
                }
            }
        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
                txtBuscar.Text = "Buscar Usuário...";
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // Se confirmado, excluo o registro selecionado
            if (MessageBox.Show("Confirma a exclusão?", "Cuidado", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {

                strSql = "Delete From Login WHERE Id = @Id";

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

        private void frmCadUsuarios_KeyDown(object sender, KeyEventArgs e)
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
    }
}
