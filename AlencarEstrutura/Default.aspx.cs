using AlencarEstrutura.DAL;
using AlencarEstrutura.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlencarEstrutura
{
    public partial class _Default : Page
    {
        private string erro = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Orcamento objOrcamento = new Orcamento();
            OrcamentoDAL dbOrcamento = new OrcamentoDAL();

            string comunicado = string.Empty;

            int qtdeOrcamento = dbOrcamento.ObertOrcamentosPendentes(ref erro);

            if (qtdeOrcamento > 0)
            {
                comunicado = ", temos " + qtdeOrcamento + " Orçamento(s) pendente(s)";
            }

            NotaFiscal objNotaFiscal = new NotaFiscal();
            NotaFiscalDAL dbNotaFiscal = new NotaFiscalDAL();

            int qtdeNotasFiscais = dbNotaFiscal.ObterNotasFiscaisPendentes(ref erro);

            if (qtdeNotasFiscais > 0)
            {
                comunicado += " temos " + qtdeNotasFiscais + " NF Vencida(s)";
            }

            HttpCookie cookie = Request.Cookies["login"];

            lblboasVindas.Text = "Bem vindo " + cookie.Value + comunicado;
        }

        protected void btnControleEstoque_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN019_CONTROLE_ESTOQUE.aspx");
        }

        protected void btnOrcamento_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN020_ORCAMENTO.aspx");
        }

        protected void btnNotaFiscal_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN023_GERARNF.aspx");
        }
    }
}