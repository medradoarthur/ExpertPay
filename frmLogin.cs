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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpertPay
{
    public partial class frmLogin : Form
    {

        private frmPrincipal _frmPrincipal;

        public frmLogin(frmPrincipal frmPrincipal)
        {
            InitializeComponent();
            _frmPrincipal = frmPrincipal;
        }

        public bool bAcessoSistema = false;

        SqlConnection sqlCon = null;
        private string strCon = "Data Source=DESKTOP-58A1GD0;Initial Catalog=ExpertPay;Integrated Security=SSPI;";
        private string strSql = string.Empty;

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string strSenhaBanco = string.Empty;

            if (txtSenha.Text == "" )
            {
                MessageBox.Show("Preencha a senha!");
                txtSenha.Focus();
                return;
            }

            strSql = "Select * from Login where Usuario=@Usuario AND Senha=@Senha";

            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            comando.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = cmbLogin.Text;
            comando.Parameters.Add("@Senha", SqlDbType.VarChar).Value = txtSenha.Text;

            try
            {
                sqlCon.Open();
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.HasRows == false)
                    throw new Exception("Senha inválida!");

                dr.Read();

                bAcessoSistema = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bAcessoSistema = false;
            }
            finally
            {
                sqlCon.Close();
            }
        
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            strSql = string.Empty;
            strSql = "SELECT Usuario FROM Login";

            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            sqlCon.Open();
            SqlDataReader reader = comando.ExecuteReader();

            if (reader.HasRows == false)
                throw new Exception("Dados não encontrados!");

            while (reader.Read())
            {
                cmbLogin.Items.Add(reader["Usuario"].ToString());
            }

            reader.Close();
            reader.Dispose();

            // Verifica se há pelo menos um item no combo
            if (cmbLogin.Items.Count > 0)
            {
                // Seleciona o primeiro item da lista
                cmbLogin.SelectedIndex = 0;
            }

            cmbLogin.Focus();
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Verifica se o login foi bem-sucedido
            if (bAcessoSistema)
            {
                // Habilita o formulário principal
                _frmPrincipal.Enabled = true;
            }
            else
            {
                Application.Exit();
                // Fecha o formulário principal, se o login não foi bem-sucedido
                //_frmPrincipal.Close();
            }
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            // Ao teclar o "Enter", pula para o próximo campo!
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void txtSenha_Enter(object sender, EventArgs e)
        {
            txtSenha.Text = "";
        }
    }
}
