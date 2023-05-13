using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ExpertPay
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        // Para que serve tantos códigos ...
        // Se a vida 
        // não é programada 
        // e as melhores coisas 
        // Não tem lógica 

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin(this);

            frmLogin.Owner = this;
            this.Enabled = false;

            //frmLogin.Show();
            frmLogin.ShowDialog();
            //frmLogin.Controls[0].Select();
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is frmLogin)
            {
                frmLogin frmLogin = (frmLogin)sender;

                // Verifica se o login foi bem-sucedido
                if (frmLogin.bAcessoSistema)
                {
                    // Habilito o formulário principal
                    this.Enabled = true;
                }
                else
                {
                    // Fecho o formulário principal, se o login não foi bem-sucedido
                    Application.Exit();
                }
            }
        }

        private void btnCadFuncionario_Click_1(object sender, EventArgs e)
        {
            frmCadFuncionarios frmCadFuncionarios = new frmCadFuncionarios();
            frmCadFuncionarios.ShowDialog();
        }

        private void btnCargos_Click_1(object sender, EventArgs e)
        {
            frmCadCargos frmCadCargos = new frmCadCargos();
            frmCadCargos.ShowDialog();
        }

        private void btnCadEmpresas_Click_1(object sender, EventArgs e)
        {
            frmCadEmpresas frmCadEmpresas = new frmCadEmpresas();
            frmCadEmpresas.ShowDialog();
        }

        private void btnCadUsuarios_Click_1(object sender, EventArgs e)
        {
            frmCadUsuarios frmCadUsuarios = new frmCadUsuarios();
            frmCadUsuarios.ShowDialog();
        }
    }
}
