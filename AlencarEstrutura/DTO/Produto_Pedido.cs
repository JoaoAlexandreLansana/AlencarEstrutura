using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DTO
{
    public class Produto_Pedido
    {
        public int IdProdutoPedido { get; set; }
        public int IdProduto { get; set; }
        public string Observacao { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorPrevisto { get; set; }
        public int IdFornecedor { get; set; }
    }
}