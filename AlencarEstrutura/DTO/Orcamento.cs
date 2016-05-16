using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DTO
{
    public class Orcamento
    {
        public int IdOrcamento { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public DateTime Vencimento { get; set; }
        public int IdPessoa { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public double Valor { get; set; }
        public int Status { get; set; }
        public int IdProdutoOrcamento { get; set; }
        public int IdProduto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorMetro { get; set; }
        public decimal Qdte_metro_quadrado { get; set; }
        public int ValorUnitario { get; set; }
    }
}