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

using System.Diagnostics;
using System.IO;
using Microsoft.Reporting.WinForms;

namespace ExpertPay
{
    public partial class frmCadFuncionarios : Form
    {
        public frmCadFuncionarios()
        {
            InitializeComponent();
        }

        SqlConnection sqlCon = null;
        private string strCon = "Data Source=DESKTOP-58A1GD0;Initial Catalog=ExpertPay;Integrated Security=SSPI;";
        //private string strCon = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ExpertPay;Data Source=ExpertPay";
        private string strSql = string.Empty;

        private bool bInsert = false;
        private int currentId; // o ID Atual

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            habilitaCampos(true);
            bInsert = true;

            txtId.Text = "";
            txtNome.Text = "";
            chkInativo.Checked = false;
            txtDataCadastro.Text = "";
            txtCpfCnpj.Text = "";
            txtPisPasep.Text = "";
            txtDataNasc.Text = "";
            txtRG.Text = "";
            txtEndereco.Text = "";
            txtNumero.Text = "";
            txtEstado.Text = "";
            txtCidade.Text = "";
            txtCEP.Text = "";
            txtEmail1.Text = "";
            txtEmail2.Text = "";
            txtTelefone.Text = "";
            txtEstadoCivil.Text = "";
            txtNacionalidade.Text = "";
            txtNomePai.Text = "";
            txtNomeMae.Text = "";
            txtSalBruto.Text = "";
            txtObs.Text = "";
            txtDataAdmissao.Text = "";
            txtDataDemissao.Text = "";
            cmbEmpresas.Text = "";
            cmbCargos.Text = "";

            cmbEmpresas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCargos.DropDownStyle = ComboBoxStyle.DropDownList;

            txtNome.Focus();
        }

        private void frmCadFuncionarios_Activated(object sender, EventArgs e)
        {
            // Pegando a data atual para o campo de Cadastro
            DateTime dataAtual = DateTime.Now;
            txtDataCadastro.Text = dataAtual.ToString("dd/MM/yyyy");

        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (rdbCodigo.Checked)
                    strSql = "Select * from Funcionarios where Id=@Id";
                else if (rdbNome.Checked)
                    strSql = "Select * from Funcionarios where Nome=@Nome";
                else if (rdbCpf.Checked)
                    strSql = "Select * from Funcionarios where CpfCnpj=@CpfCnpj";

                sqlCon = new SqlConnection(strCon);
                SqlCommand comando = new SqlCommand(strSql, sqlCon);

                if (rdbCodigo.Checked)
                    comando.Parameters.Add("@Id", SqlDbType.Int).Value = txtBuscar.Text;
                else if (rdbNome.Checked)
                    comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txtBuscar.Text;
                else if (rdbCpf.Checked)
                    comando.Parameters.Add("@CpfCnpj", SqlDbType.Char).Value = txtBuscar.Text;

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
                    // comando.Parameters.AddWithValue("@Nome", SqlDbType.VarChar).Value = txtBuscar.Text;

                    txtId.Text = Convert.ToString(dr["Id"]);
                    txtNome.Text = Convert.ToString(dr["Nome"]);
                    chkInativo.Checked = Convert.ToBoolean(dr["Ativo"]);
                    txtDataCadastro.Text = dr["DataCadastro"].ToString();
                    txtCpfCnpj.Text = Convert.ToString(dr["CpfCnpj"]);
                    txtPisPasep.Text = Convert.ToString(dr["PisPasep"]);
                    txtDataNasc.Text = dr["DataNasc"].ToString();
                    txtRG.Text = Convert.ToString(dr["RG"]);
                    txtEndereco.Text = Convert.ToString(dr["Endereco"]);
                    txtNumero.Text = Convert.ToString(dr["NumeroEndereco"]);
                    txtEstado.Text = Convert.ToString(dr["EstadoEndereco"]);
                    txtCidade.Text = Convert.ToString(dr["Cidade"]);
                    txtCEP.Text = Convert.ToString(dr["CEP"]);
                    txtEmail1.Text = Convert.ToString(dr["Email1"]);
                    txtEmail2.Text = Convert.ToString(dr["Email2"]);
                    txtTelefone.Text = Convert.ToString(dr["Telefone"]);
                    txtEstadoCivil.Text = Convert.ToString(dr["EstadoCivil"]);
                    txtNacionalidade.Text = Convert.ToString(dr["Nacionalidade"]);
                    txtNomePai.Text = Convert.ToString(dr["NomePai"]);
                    txtNomeMae.Text = Convert.ToString(dr["NomeMae"]);
                    txtSalBruto.Text = Convert.ToString(dr["SalarioBruto"]);
                    txtObs.Text = Convert.ToString(dr["Observacoes"]);
                    txtDataAdmissao.Text = dr["DataAdmissao"].ToString();
                    txtDataDemissao.Text = dr["DataDemissao"].ToString();

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

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            habilitaCampos(true);
            bInsert = false;

            cmbEmpresas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCargos.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar Funcionário...";
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            try
            {
                // incrementa o ID atual para obter o próximo registro
                currentId++;

                // consulta o banco de dados para encontrar o próximo registro com um ID maior
                string query = $"SELECT * FROM Funcionarios WHERE ID = (SELECT MIN(ID) FROM Funcionarios WHERE ID = {currentId})";
                // execute a consulta

                sqlCon.Open();

                int ultimoID;

                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                // se houver um registro encontrado
                if (reader.Read())
                {
                    // atualiza o ID atual com o ID do registro encontrado
                    currentId = Convert.ToInt32(reader["ID"]);

                    cmbEmpresas.Text = reader["IdEmpresas"].ToString();
                    cmbCargos.Text = reader["IdCargos"].ToString();
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
                            SqlCommand command = new SqlCommand("SELECT TOP 1 ID FROM Funcionarios ORDER BY ID DESC", sqlCon);
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

        private void LoadRecord(int id)
        {
            sqlCon = new SqlConnection(strCon);

            SqlCommand cmd = new SqlCommand("SELECT * FROM Funcionarios WHERE ID = @id", sqlCon);
            cmd.Parameters.AddWithValue("@id", id);

            sqlCon.Close();

            sqlCon.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                txtId.Text = reader["Id"].ToString();

                txtNome.Text = Convert.ToString(reader["Nome"]);
                chkInativo.Checked = Convert.ToBoolean(reader["Ativo"]);
                txtDataCadastro.Text = reader["DataCadastro"].ToString();
                txtCpfCnpj.Text = Convert.ToString(reader["CpfCnpj"]);
                txtPisPasep.Text = Convert.ToString(reader["PisPasep"]);
                txtDataNasc.Text = reader["DataNasc"].ToString();
                txtRG.Text = Convert.ToString(reader["RG"]);
                txtEndereco.Text = Convert.ToString(reader["Endereco"]);
                txtNumero.Text = Convert.ToString(reader["NumeroEndereco"]);
                txtEstado.Text = Convert.ToString(reader["EstadoEndereco"]);
                txtCidade.Text = Convert.ToString(reader["Cidade"]);
                txtCEP.Text = Convert.ToString(reader["CEP"]);
                txtEmail1.Text = Convert.ToString(reader["Email1"]);
                txtEmail2.Text = Convert.ToString(reader["Email2"]);
                txtTelefone.Text = Convert.ToString(reader["Telefone"]);
                txtEstadoCivil.Text = Convert.ToString(reader["EstadoCivil"]);
                txtNacionalidade.Text = Convert.ToString(reader["Nacionalidade"]);
                txtNomePai.Text = Convert.ToString(reader["NomePai"]);
                txtNomeMae.Text = Convert.ToString(reader["NomeMae"]);
                txtSalBruto.Text = Convert.ToString(reader["SalarioBruto"]);
                txtObs.Text = Convert.ToString(reader["Observacoes"]);
                txtDataAdmissao.Text = reader["DataAdmissao"].ToString();
                txtDataDemissao.Text = reader["DataDemissao"].ToString();

                cmbEmpresas.Text = reader["IdEmpresas"].ToString();
                cmbCargos.Text = reader["IdCargos"].ToString();

            }

            reader.Close();
            sqlCon.Close();
        }

        private void frmCadFuncionarios_Load(object sender, EventArgs e)
        {
            habilitaCampos(false);
            DateTime dataAtual = DateTime.Now;
            txtDataCadastro.Text = dataAtual.ToString("dd/MM/yyyy");

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

        private class ItemComboBox
        {
            public int Id { get; set; }
            public string Descricao { get; set; }

            public override string ToString()
            {
                return Descricao;
            }
        }

        private void habilitaCampos(bool habilita)
        {
            txtNome.Enabled = habilita;
            chkInativo.Enabled = habilita;
            txtDataCadastro.Enabled = habilita;
            txtCpfCnpj.Enabled = habilita;
            txtPisPasep.Enabled = habilita;
            txtDataNasc.Enabled = habilita;
            txtRG.Enabled = habilita;
            txtEndereco.Enabled = habilita;
            txtNumero.Enabled = habilita;
            txtEstado.Enabled = habilita;
            txtCidade.Enabled = habilita;
            txtCEP.Enabled = habilita;
            txtEmail1.Enabled = habilita;
            txtEmail2.Enabled = habilita;
            txtTelefone.Enabled = habilita;
            txtEstadoCivil.Enabled = habilita;
            txtNacionalidade.Enabled = habilita;
            txtNomePai.Enabled = habilita;
            txtNomeMae.Enabled = habilita;
            txtSalBruto.Enabled = habilita;
            txtObs.Enabled = habilita;
            txtDataAdmissao.Enabled = habilita;
            txtDataDemissao.Enabled = habilita;
            cmbEmpresas.Enabled = habilita;
            cmbCargos.Enabled = habilita;

            btnSalvar.Enabled = habilita;

            btnAdicionar.Enabled = !habilita;
            btnAlterar.Enabled = !habilita;
            btnGerarFolha.Enabled = !habilita;
            btnAnterior.Enabled = !habilita;
            btnProximo.Enabled = !habilita;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Preencha o Nome!");
                txtNome.Focus();
                return;
            }
            else if (cmbEmpresas.Text == "")
            {
                MessageBox.Show("Preencha a Empresa!");
                cmbEmpresas.Focus();
                return;
            }
            else if (txtCpfCnpj.Text == "")
            {
                MessageBox.Show("Preencha o Cpf/CNPJ!");
                txtCpfCnpj.Focus();
                return;
            }
            else if (cmbCargos.Text == "")
            {
                MessageBox.Show("Preencha o Cargo!");
                cmbCargos.Focus();
                return;
            }
            else if (txtEndereco.Text == "")
            {
                MessageBox.Show("Preencha o endereço!");
                txtEndereco.Focus();
                return;
            }
            else if (txtSalBruto.Text == "")
            {
                MessageBox.Show("Preencha o Salário Bruto!");
                txtSalBruto.Focus();
                return;
            }
            else if (txtDataAdmissao.Text == "")
            {
                MessageBox.Show("Preencha a Data de Admissão!");
                txtDataAdmissao.Focus();
                return;
            }

            cmbEmpresas.DropDownStyle = ComboBoxStyle.DropDown;
            cmbCargos.DropDownStyle = ComboBoxStyle.DropDown;

            // Se eu estiver inserindo, faço um Insert no banco, se não, faço um Update!
            if (bInsert)
                strSql = "INSERT INTO Funcionarios (Nome, Ativo, CpfCnpj, PISPASEP, DataNasc, RG, Endereco, NumeroEndereco, EstadoEndereco, Cidade, CEP, Email1, Email2, Telefone, EstadoCivil, Nacionalidade, NomePai, NomeMae, SalarioBruto, Observacoes, DataAdmissao, DataDemissao, IdEmpresas, IdCargos) " +
                          "VALUES (@Nome, @Ativo, @CpfCnpj, @PISPASEP, @DataNasc, @RG, @Endereco, @NumeroEndereco, @EstadoEndereco, @Cidade, @CEP, @Email1, @Email2, @Telefone, @EstadoCivil, @Nacionalidade, @NomePai, @NomeMae, @SalarioBruto, @Observacoes, @DataAdmissao, @DataDemissao, @IdEmpresas, @IdCargos)";
            else
                strSql = "UPDATE Funcionarios SET Nome = @Nome, Ativo = @Ativo, CpfCnpj = @CpfCnpj, " +
                         "PISPASEP = @PISPASEP, DataNasc = @DataNasc, RG = @RG, Endereco = @Endereco, NumeroEndereco = @NumeroEndereco, " +
                         "EstadoEndereco = @EstadoEndereco, Cidade = @Cidade, CEP = @CEP, Email1 = @Email1, Email2 = @Email2, " +
                         "Telefone = @Telefone, EstadoCivil = @EstadoCivil, Nacionalidade = @Nacionalidade, NomePai = @NomePai, NomeMae = @NomeMae, " +
                         "SalarioBruto = @SalarioBruto, Observacoes = @Observacoes, DataAdmissao = @DataAdmissao, DataDemissao = @DataDemissao, " +
                         "IdEmpresas = @IdEmpresas, IdCargos = @IdCargos " +
                         "WHERE Id = @Id";


            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            // Pego apenas o ID do meu combo, para salvar no banco!
            ItemComboBox comboEmpresa = (ItemComboBox)cmbEmpresas.SelectedItem;
            ItemComboBox comboCargos = (ItemComboBox)cmbCargos.SelectedItem;


            // Passo o Id como parametro, apenas se for Update
            if (!bInsert)
                comando.Parameters.Add("@Id", SqlDbType.Int).Value = txtId.Text;

            comando.Parameters.AddWithValue("@Nome", SqlDbType.VarChar).Value = txtNome.Text;
            comando.Parameters.AddWithValue("@Ativo", SqlDbType.Bit).Value = chkInativo.Checked;
            comando.Parameters.AddWithValue("@DataCadastro", SqlDbType.Date).Value = DateTime.Now.Date;
            comando.Parameters.AddWithValue("@CpfCnpj", SqlDbType.Char).Value = txtCpfCnpj.Text;
            comando.Parameters.AddWithValue("@PISPASEP", SqlDbType.VarChar).Value = txtPisPasep.Text;

            DateTime dataNasc;
            if (DateTime.TryParse(txtDataNasc.Text, out dataNasc))
            {
                comando.Parameters.AddWithValue("@DataNasc", dataNasc);
            }
            else
            {
                // Se a conversão falhar, você pode definir um valor padrão, como null ou uma data mínima.
                comando.Parameters.AddWithValue("@DataNasc", DBNull.Value);
            }
            comando.Parameters.AddWithValue("@RG", SqlDbType.VarChar).Value = txtRG.Text;
            comando.Parameters.AddWithValue("@Endereco", SqlDbType.VarChar).Value = txtEndereco.Text;
            comando.Parameters.AddWithValue("@NumeroEndereco", SqlDbType.Int).Value = txtNumero.Text;
            comando.Parameters.AddWithValue("@EstadoEndereco", SqlDbType.Char).Value = txtEstado.Text;
            comando.Parameters.AddWithValue("@Cidade", SqlDbType.VarChar).Value = txtCidade.Text;
            comando.Parameters.AddWithValue("@CEP", SqlDbType.VarChar).Value = txtCEP.Text;
            comando.Parameters.AddWithValue("@Email1", SqlDbType.VarChar).Value = txtEmail1.Text;
            comando.Parameters.AddWithValue("@Email2", SqlDbType.VarChar).Value = txtEmail2.Text;
            comando.Parameters.AddWithValue("@Telefone", SqlDbType.VarChar).Value = txtTelefone.Text;
            comando.Parameters.AddWithValue("@EstadoCivil", SqlDbType.VarChar).Value = txtEstadoCivil.Text;
            comando.Parameters.Add("@Nacionalidade", SqlDbType.VarChar).Value = txtNacionalidade.Text;
            comando.Parameters.Add("@NomePai", SqlDbType.VarChar).Value = txtNomePai.Text;
            comando.Parameters.Add("@NomeMae", SqlDbType.VarChar).Value = txtNomeMae.Text;
            comando.Parameters.Add("@SalarioBruto", SqlDbType.Decimal).Value = txtSalBruto.Text;
            comando.Parameters.Add("@Observacoes", SqlDbType.Text).Value = txtObs.Text;

            DateTime dataAd;
            if (DateTime.TryParse(txtDataAdmissao.Text, out dataAd))
            {
                comando.Parameters.AddWithValue("@DataAdmissao", dataAd);
            }
            else
            {
                // Se a conversão falhar, você pode definir um valor padrão, como null ou uma data mínima.
                comando.Parameters.AddWithValue("@DataAdmissao", DBNull.Value);
            }

            DateTime dataDem;
            if (DateTime.TryParse(txtDataDemissao.Text, out dataDem))
            {
                comando.Parameters.AddWithValue("@DataDemissao", dataDem);
            }
            else
            {
                // Se a conversão falhar, você pode definir um valor padrão, como null ou uma data mínima.
                comando.Parameters.AddWithValue("@DataDemissao", DBNull.Value);
            }

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
                string query = $"SELECT * FROM Funcionarios WHERE ID = (SELECT MAX(ID) FROM Funcionarios WHERE ID = {currentId})";
                // execute a consulta

                sqlCon.Open();

                int primeiroID;

                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                // se houver um registro encontrado
                if (reader.Read())
                {
                    // atualiza o ID atual com o ID do registro encontrado
                    currentId = Convert.ToInt32(reader["ID"]);

                    cmbEmpresas.Text = reader["IdEmpresas"].ToString();
                    cmbCargos.Text = reader["IdCargos"].ToString();
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
                            SqlCommand command = new SqlCommand("SELECT TOP 1 ID FROM Funcionarios ORDER BY ID ASC", sqlCon);
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

                PreencherDescricao(cmbEmpresas, "RazaoSocial", "Empresas");
                PreencherDescricao(cmbCargos, "Descricao", "Cargos");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnGerarFolha_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkInativo.Checked)
                {
                    MessageBox.Show("Usuário Inativo!");
                    return;
                }
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;

                //Caminho para o relatorio
                reportViewer.LocalReport.ReportEmbeddedResource = "ExpertPay.FolhaPagamento.rdlc";

                decimal salarioBruto = Convert.ToDecimal(txtSalBruto.Text);
                decimal inss;

                // Parametros do relatorio: confia no pai 
                List<ReportParameter> listReportParameter = new List<ReportParameter>();
                listReportParameter.Add(new ReportParameter("Nome", txtNome.Text));
                listReportParameter.Add(new ReportParameter("CnpjCpf", txtCpfCnpj.Text));
                listReportParameter.Add(new ReportParameter("Cargo", cmbCargos.Text));

                listReportParameter.Add(new ReportParameter("Codigo", txtId.Text));
                listReportParameter.Add(new ReportParameter("NomeEmpresa", cmbEmpresas.Text));
                listReportParameter.Add(new ReportParameter("EnderecoEmpresa", "  "));
                listReportParameter.Add(new ReportParameter("SalarioBase", txtSalBruto.Text));

                if (salarioBruto <= 1302.00m)
                {
                    inss = salarioBruto * 0.075m;
                    listReportParameter.Add(new ReportParameter("PorcentagemINSS", "7,5%"));
                    listReportParameter.Add(new ReportParameter("DescontosINSS", inss.ToString("N2")));
                }
                else if (salarioBruto <= 2571.29m)
                {
                    inss = salarioBruto * 0.09m;
                    listReportParameter.Add(new ReportParameter("PorcentagemINSS", "9,0%"));
                    listReportParameter.Add(new ReportParameter("DescontosINSS", inss.ToString("N2")));
                }
                else if (salarioBruto <= 3856.94m)
                {
                    inss = salarioBruto * 0.12m;
                    listReportParameter.Add(new ReportParameter("PorcentagemINSS", "12,0%"));
                    listReportParameter.Add(new ReportParameter("DescontosINSS", inss.ToString("N2")));
                }
                else if (salarioBruto <= 7507.49m)
                {
                    inss = salarioBruto * 0.14m;
                    listReportParameter.Add(new ReportParameter("PorcentagemINSS", "14,0%"));
                    listReportParameter.Add(new ReportParameter("DescontosINSS", inss.ToString("N2")));
                }
                else
                {
                    inss = 7507.49m * 0.14m;
                    listReportParameter.Add(new ReportParameter("PorcentagemINSS", "%"));
                    listReportParameter.Add(new ReportParameter("DescontosINSS", inss.ToString("N2")));
                }

                decimal baseCalculo = salarioBruto - inss;
                decimal descontoIRRF = 0;

                if (baseCalculo > 4664.68m)
                {
                    descontoIRRF = baseCalculo * 0.275m;

                    listReportParameter.Add(new ReportParameter("PorcetagemIRRF", "27,5%"));
                    listReportParameter.Add(new ReportParameter("DescontosIRRF", descontoIRRF.ToString("N2")));
                }
                else if (baseCalculo > 3751.05m)
                {
                    descontoIRRF = baseCalculo * 0.225m;

                    listReportParameter.Add(new ReportParameter("PorcetagemIRRF", "22,5%"));
                    listReportParameter.Add(new ReportParameter("DescontosIRRF", descontoIRRF.ToString("N2")));
                }
                else if (baseCalculo > 2826.65m)
                {
                    descontoIRRF = baseCalculo * 0.075m;

                    listReportParameter.Add(new ReportParameter("PorcetagemIRRF", "7,5%"));
                    listReportParameter.Add(new ReportParameter("DescontosIRRF", descontoIRRF.ToString("N2")));
                }
                // não há desconto para baseCalculo abaixo de 1903.98

                decimal descontoTotal = inss + descontoIRRF;
                decimal salLiquid = salarioBruto - inss - descontoIRRF;

                listReportParameter.Add(new ReportParameter("TotalDescontos", descontoTotal.ToString("N2")));
                listReportParameter.Add(new ReportParameter("SalarioLiquido", salLiquid.ToString("N2")));

                reportViewer.LocalReport.SetParameters(listReportParameter);

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extesion;

                byte[] bytePDF = reportViewer.LocalReport.Render("Pdf", null, out mimeType, out encoding,
                    out extesion, out streamids, out warnings);

                FileStream fileStreamPDF = null;

                //Nome do arquivo sim, para não sobrescrever
                string nomeArquivoPDF = Path.GetTempPath() +
                    "FolhaPagamento" + DateTime.Now.ToString("dd_MM_yyyy-HH_mm_ss") + ".pdf";

                // Passo cada byte do relatório, salvo e executo!! respeita
                fileStreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
                fileStreamPDF.Write(bytePDF, 0, bytePDF.Length);
                fileStreamPDF.Close();

                Process.Start(nomeArquivoPDF);

                MessageBox.Show("Folha gerada com sucesso!!");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmCadFuncionarios_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F6:
                    if (btnAdicionar.Enabled)
                        btnAdicionar.PerformClick();
                    break;
                case Keys.F7:
                    if (btnAlterar.Enabled)
                        btnAlterar.PerformClick();
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

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
