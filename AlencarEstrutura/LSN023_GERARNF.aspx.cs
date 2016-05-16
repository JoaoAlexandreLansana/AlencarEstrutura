using AlencarEstrutura.DAL;
using AlencarEstrutura.DataSets;
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
    public partial class LSN023_GERARNF : System.Web.UI.Page
    {
        private string erro = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregaGvOrcamento();
            }
        }
        public void carregaGvOrcamento()
        {
            Orcamento objOrcamento = new Orcamento();
            OrcamentoDAL dbOrcamento = new OrcamentoDAL();
            DataTable dtOrcamento = dbOrcamento.ObterListaOrcamento(ref erro);

            gvOrcamento.DataSource = dtOrcamento;
            gvOrcamento.AutoGenerateSelectButton = true;
            gvOrcamento.DataBind();
        }

        protected void gvOrcamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            Orcamento objOrcamento = new Orcamento();
            OrcamentoDAL dbOrcamento = new OrcamentoDAL();

            objOrcamento = dbOrcamento.ObterOrcamentoPorId(Convert.ToInt32(gvOrcamento.SelectedDataKey.Value), ref erro);

            if (objOrcamento != null) bindOrcamento(objOrcamento);
        }
        private void bindOrcamento(Orcamento orcamento)
        {
            txtCodigo.Text = orcamento.IdOrcamento.ToString();
            txtDescricao.Text = orcamento.Descricao;
            txtValor.Text = orcamento.Valor.ToString();
            hfValorReal.Value = orcamento.Valor.ToString();
            txtEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtCodCliente.Text = orcamento.IdPessoa.ToString();
            txtNomeCliente.Text = orcamento.NomeCliente;
        }

        protected void ddlDesconto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(txtCodigo.Text == "")
                {
                    Session.Add("danger", "Selecione um orçamento");
                    return;
                }

                double valorAtual = (string.IsNullOrEmpty(hfValorReal.Value)) ? 0 : Convert.ToDouble(hfValorReal.Value);

                double valorDesconto = (valorAtual * Convert.ToInt16(ddlDesconto.SelectedValue)) / 100;

                double novoValor = valorAtual - valorDesconto;

                txtValor.Text = Math.Round(novoValor, 2).ToString();
            }
            catch (Exception ex)
            {
                Session.Add("danger", ex.Message);
            }
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtEmissao.Text = string.Empty;
            txtVencimento.Text = string.Empty;
            txtValor.Text = string.Empty;
            hfValorReal.Value = string.Empty;
            ddlDesconto.SelectedIndex = 0;
        }

        protected void gvOrcamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrcamento.PageIndex = e.NewPageIndex;
            carregaGvOrcamento();
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            if (!validacoes())
            {
                Session.Add("danger", "Preencha os campos corretamente! " + erro);
                return;
            }            

            NotaFiscal objNotafical = new NotaFiscal();
            NotaFiscalDAL dbNotaFiscal = new NotaFiscalDAL();

            objNotafical.IdEmpresa = 1;
            objNotafical.idOrcamento = Convert.ToInt32(txtCodigo.Text);
            objNotafical.Status = 1;
            objNotafical.Valor = Convert.ToDecimal(txtValor.Text);
            objNotafical.Vencimento = Convert.ToDateTime(txtVencimento.Text);
            objNotafical.DataEmissao = Convert.ToDateTime(txtEmissao.Text);
            objNotafical.IdPessoa = Convert.ToInt32(txtCodCliente.Text);

            if (!ConsultaNF())
            {
                if (!dbNotaFiscal.InserirNotaFiscal(objNotafical, ref erro))
                {
                    Session.Add("danger", "Não foi possível insirir a Nota Fiscal" + erro);
                    return;
                }
            }
            imprimir();
        }

        private void imprimir()
        {
            NotaFiscalDAL dbNotaFiscal = new NotaFiscalDAL();

            LSN023_GERARNF_ crystalReport = new LSN023_GERARNF_();
            LSN023_NOTAFISCAL dsNF = dbNotaFiscal.GetData(Convert.ToInt32(txtCodigo.Text), ref erro);
            if (dsNF != null)
            {
                crystalReport.SetDataSource(dsNF);
                CrystalReportViewer1.ReportSource = crystalReport;
            }

            if (erro != "") Session.Add("danger", "Não foi possível imprimir " + erro);
        }

        private bool validacoes()
        {
            if(txtCodigo.Text == "" || txtCodCliente.Text == "" || txtVencimento.Text == "")
            {
                return false;
            }
            return true;
        }

        private bool ConsultaNF()
        {
            NotaFiscal objNotafical = new NotaFiscal();
            NotaFiscalDAL dbNotaFiscal = new NotaFiscalDAL();

            objNotafical = dbNotaFiscal.ObterNotaFiscalPorIDOrcamento(Convert.ToInt32(txtCodigo.Text), ref erro);

            if(objNotafical.idOrcamento != 0)
            {
                return true;
            }
            return false;
        }
    }
}