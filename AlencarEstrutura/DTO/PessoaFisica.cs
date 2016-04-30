using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DTO
{
    public class PessoaFisica : PessoaJuridica
    {
        public int IdPessoaFisica { get; set; }
        public long CPF { get; set; }
        public string RG { get; set; }
        public int IdEstadoCivil { get; set; }
    }
}