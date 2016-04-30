using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DTO
{
    public class PessoaJuridica
    {
        public int IdPessoaJuridica { get; set; }
        public long CNPJ { get; set; }
        public string RazaoSocial { get; set; }

        public string NomeFantasia { get; set; }
        public string InscricaoEstadual { get; set; }
    }
}