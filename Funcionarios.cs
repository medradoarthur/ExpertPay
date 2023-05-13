using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertPay
{
    public class Funcionarios
    {
        public string Nome { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public string Empresa { get; set; }
        public string CpfCnpj { get; set; }
        public string PISPASEP { get; set; }
        public DateTime? DataNasc { get; set; }
        public string RG { get; set; }
        public string Endereco { get; set; }
        public int NumeroEndereco { get; set; }
        public string EstadoEndereco { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public int Telefone { get; set; }
        public string EstadoCivil { get; set; }
        public string Nacionalidade { get; set; }
        public string NomePai { get; set; }
        public string NomeMae { get; set; }
        public decimal SalarioBruto { get; set; }
        public string Observacoes { get; set; }
        public DateTime? DataAdmissao { get; set; }
        public DateTime? DataDemissao { get; set; }

        public Funcionarios(string nome, bool ativo, string empresa, string cpfCnpj, string endereco, int numeroEndereco, string cidade, string cep, int telefone, decimal salarioBruto)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("O nome do funcionário deve ser informado.");
            }

            if (string.IsNullOrEmpty(empresa))
            {
                throw new ArgumentException("A empresa deve ser informado.");
            }

            if (string.IsNullOrEmpty(cpfCnpj))
            {
                throw new ArgumentException("O CPF ou CNPJ do funcionário deve ser informado.");
            }

            if (string.IsNullOrEmpty(endereco))
            {
                throw new ArgumentException("O endereço do funcionário deve ser informado.");
            }

            if (string.IsNullOrEmpty(cidade))
            {
                throw new ArgumentException("A cidade do funcionário deve ser informada.");
            }

            if (string.IsNullOrEmpty(cep))
            {
                throw new ArgumentException("O CEP do funcionário deve ser informado.");
            }

            if (telefone == 0)
            {
                throw new ArgumentException("O telefone do funcionário deve ser informado.");
            }

            if (salarioBruto == 0)
            {
                throw new ArgumentException("O salário bruto do funcionário deve ser informado.");
            }

            Nome = nome;
            Ativo = ativo;
            CpfCnpj = cpfCnpj;
            Endereco = endereco;
            NumeroEndereco = numeroEndereco;
            Cidade = cidade;
            CEP = cep;
            Telefone = telefone;
            SalarioBruto = salarioBruto;
        }
    }


}
