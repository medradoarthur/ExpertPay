using System.Data.SqlClient;

namespace ExpertPay
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label29 = new System.Windows.Forms.Label();
            this.btnCadUsuarios = new System.Windows.Forms.Button();
            this.btnCadEmpresas = new System.Windows.Forms.Button();
            this.btnCargos = new System.Windows.Forms.Button();
            this.btnCadFuncionario = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(1, 106);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1071, 20);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "---------------------------------------------------------------------------------" +
    "--------------------------------------------------------------";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "btnCadCargos.png");
            this.imageList1.Images.SetKeyName(1, "btnCadEmpresas.png");
            this.imageList1.Images.SetKeyName(2, "btnCadUsuarios.png");
            this.imageList1.Images.SetKeyName(3, "btnSair.png");
            this.imageList1.Images.SetKeyName(4, "big-search-len.png");
            this.imageList1.Images.SetKeyName(5, "btnAdicionar.png");
            this.imageList1.Images.SetKeyName(6, "btnAdiconarFuncionarios.png");
            this.imageList1.Images.SetKeyName(7, "btnAlterar.png");
            this.imageList1.Images.SetKeyName(8, "btnAnterior.png");
            this.imageList1.Images.SetKeyName(9, "btnCadCargos.png");
            this.imageList1.Images.SetKeyName(10, "btnCadEmpresas.png");
            this.imageList1.Images.SetKeyName(11, "btnCadUsuarios.png");
            this.imageList1.Images.SetKeyName(12, "btnEntrar.png");
            this.imageList1.Images.SetKeyName(13, "btnGerarFolha.png");
            this.imageList1.Images.SetKeyName(14, "btnProximo.png");
            this.imageList1.Images.SetKeyName(15, "btnSair.png");
            this.imageList1.Images.SetKeyName(16, "btnTrocarUsuario.png");
            this.imageList1.Images.SetKeyName(17, "expert pay png.png");
            this.imageList1.Images.SetKeyName(18, "homeexpert.jpg");
            this.imageList1.Images.SetKeyName(19, "iconeSistema.ico");
            this.imageList1.Images.SetKeyName(20, "iconeSistema.png");
            this.imageList1.Images.SetKeyName(21, "Relatorio.jpeg");
            this.imageList1.Images.SetKeyName(22, "search-engine-optimization.png");
            this.imageList1.Images.SetKeyName(23, "senha.png");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1116, 597);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(967, 553);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(115, 20);
            this.label29.TabIndex = 88;
            this.label29.Text = "Versão 0.0.0.1";
            // 
            // btnCadUsuarios
            // 
            this.btnCadUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadUsuarios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCadUsuarios.ImageIndex = 16;
            this.btnCadUsuarios.ImageList = this.imageList1;
            this.btnCadUsuarios.Location = new System.Drawing.Point(378, 13);
            this.btnCadUsuarios.Margin = new System.Windows.Forms.Padding(4);
            this.btnCadUsuarios.Name = "btnCadUsuarios";
            this.btnCadUsuarios.Size = new System.Drawing.Size(96, 96);
            this.btnCadUsuarios.TabIndex = 92;
            this.btnCadUsuarios.UseVisualStyleBackColor = true;
            this.btnCadUsuarios.Click += new System.EventHandler(this.btnCadUsuarios_Click_1);
            // 
            // btnCadEmpresas
            // 
            this.btnCadEmpresas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadEmpresas.ImageKey = "btnCadEmpresas.png";
            this.btnCadEmpresas.ImageList = this.imageList1;
            this.btnCadEmpresas.Location = new System.Drawing.Point(251, 13);
            this.btnCadEmpresas.Margin = new System.Windows.Forms.Padding(4);
            this.btnCadEmpresas.Name = "btnCadEmpresas";
            this.btnCadEmpresas.Size = new System.Drawing.Size(96, 96);
            this.btnCadEmpresas.TabIndex = 91;
            this.btnCadEmpresas.UseVisualStyleBackColor = true;
            this.btnCadEmpresas.Click += new System.EventHandler(this.btnCadEmpresas_Click_1);
            // 
            // btnCargos
            // 
            this.btnCargos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargos.ImageKey = "btnCadCargos.png";
            this.btnCargos.ImageList = this.imageList1;
            this.btnCargos.Location = new System.Drawing.Point(128, 14);
            this.btnCargos.Margin = new System.Windows.Forms.Padding(4);
            this.btnCargos.Name = "btnCargos";
            this.btnCargos.Size = new System.Drawing.Size(96, 96);
            this.btnCargos.TabIndex = 90;
            this.btnCargos.UseVisualStyleBackColor = true;
            this.btnCargos.Click += new System.EventHandler(this.btnCargos_Click_1);
            // 
            // btnCadFuncionario
            // 
            this.btnCadFuncionario.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadFuncionario.ImageKey = "btnCadUsuarios.png";
            this.btnCadFuncionario.ImageList = this.imageList1;
            this.btnCadFuncionario.Location = new System.Drawing.Point(9, 14);
            this.btnCadFuncionario.Margin = new System.Windows.Forms.Padding(4);
            this.btnCadFuncionario.Name = "btnCadFuncionario";
            this.btnCadFuncionario.Size = new System.Drawing.Size(96, 96);
            this.btnCadFuncionario.TabIndex = 89;
            this.btnCadFuncionario.UseVisualStyleBackColor = true;
            this.btnCadFuncionario.Click += new System.EventHandler(this.btnCadFuncionario_Click_1);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(1121, 597);
            this.Controls.Add(this.btnCadUsuarios);
            this.Controls.Add(this.btnCadEmpresas);
            this.Controls.Add(this.btnCargos);
            this.Controls.Add(this.btnCadFuncionario);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExpertPay";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPrincipal_FormClosed);
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button btnCadUsuarios;
        private System.Windows.Forms.Button btnCadEmpresas;
        private System.Windows.Forms.Button btnCargos;
        private System.Windows.Forms.Button btnCadFuncionario;
    }
}

