using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DTO
{
    public class Empresa
    {
        public int IdEmpresa { get; set; }
        public string NomeFantasia { get; set; }
        public string Razao { get; set; }
        public string Email { get; set; }
        public long CNPJ { get; set; }
        public int IdPessoa { get; set; }
    }
}