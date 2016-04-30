using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DTO
{
    public class Pessoa : PessoaFisica
    {
        public int IdPessoa { get; set; }
        public string Email { get; set; }
        public string NomePessoa { get; set; }
        public string TipoPessoa { get; set; }
        public string Senha { get; set; }
        public int Status { get; set; }

        public Telefone telefone { get; set; }
        public Endereco endereco { get; set; }
    }
}