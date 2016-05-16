using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlencarEstrutura.DTO
{
    public class NotaFiscal
    {
        public int IdNotaFiscal { get; set; }
        public int idOrcamento { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime DataEmissao { get; set; }
        public int Status { get; set; }
        public int IdEmpresa { get; set; }
        public Decimal Valor { get; set; }
        public int Parcelas { get; set; }
        public int IdPessoa { get; set; }

    }
}