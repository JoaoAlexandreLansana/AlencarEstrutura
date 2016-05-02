using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DTO
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string Descricao { get; set; }
        public int IdCategoria { get; set; }
        public string Observacao { get; set; }
        public decimal Valor { get; set; }
        public Decimal Peso { get; set; }
        public decimal Litros { get; set; }

        public Decimal ValorPorMetro { get; set; }
    }
}