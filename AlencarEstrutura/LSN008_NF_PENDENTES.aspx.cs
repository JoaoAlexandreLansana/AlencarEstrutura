using AlencarEstrutura.DAL;
using AlencarEstrutura.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlencarEstrutura
{
    public partial class LSN008_NF_PENDENTES : System.Web.UI.Page
    {
        private string erro = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Orcamento objOrcamento = new Orcamento();
            OrcamentoDAL dbOrcamento = new OrcamentoDAL();

            DataTable dtOrcamento = dbOrcamento.ObterListaOrcamentoPendentes(ref erro);

            if (dtOrcamento == null && erro != "")
            {
                Session.Add("danger", "Não foi possível Carregar a lista de Orçamentos " + erro);
            }
            else
            {
                gvOrcamentos.DataSource = dtOrcamento;
                gvOrcamentos.DataBind();
            }

            NotaFiscal objNF = new NotaFiscal();
            NotaFiscalDAL dbNF = new NotaFiscalDAL();

            DataTable dtNF = dbNF.ObterListaDeNotaFiscaisAVencidas(ref erro);

            if (dtNF == null && erro != "")
            {
                Session.Add("danger", "Não foi possível Carregar a lista de Notas Fiscais! " + erro);
            }
            else
            {
                gvNotas.DataSource = dtNF;
                gvNotas.DataBind();
            }
        }
    }
}