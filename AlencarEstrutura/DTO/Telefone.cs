using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DTO
{
    public class Telefone
    {
        public int IdTelefone { get; set; }
        public int DDD { get; set; }
        public string Numero { get; set; }
        public string TipoTelefone { get; set; }
        public int IdTipoTelefone { get; set; }
        public int IdPessoa { get; set; }
    }
}