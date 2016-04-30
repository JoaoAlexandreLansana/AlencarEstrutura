using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DTO
{
    public class Estoque
    {
        public int IdEstoque { get; set; }
        public string Descricao { get; set; }
        public int IdProduto { get; set; }
        public DateTime Validade { get; set; }
        public decimal Quantidade { get; set; }
        public string Observacao { get; set; }
    }
}