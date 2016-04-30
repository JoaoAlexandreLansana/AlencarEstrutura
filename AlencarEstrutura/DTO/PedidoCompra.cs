using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DTO
{
    public class PedidoCompra : Produto_Pedido
    {
        public int IdPedidoCompra { get; set; }
        public int  Status { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataPedido { get; set; }
    }
}