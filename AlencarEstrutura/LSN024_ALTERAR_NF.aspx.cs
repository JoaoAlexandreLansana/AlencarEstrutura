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
    public partial class LSN024_ALTERAR_NF : System.Web.UI.Page
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
            NotaFiscal objNotaFiscal = new NotaFiscal();
            NotaFiscalDAL dbNotaFiscal = new NotaFiscalDAL();
            List<NotaFiscal> lstOrcamento = dbNotaFiscal.ObterListaDeNotaFiscais(ref erro);

            gvNF.DataSource = lstOrcamento;
            gvNF.AutoGenerateSelectButton = true;
            gvNF.DataBind();
        }

        protected void gvNF_SelectedIndexChanged(object sender, EventArgs e)
        {
            NotaFiscal objNotaFiscal = new NotaFiscal();
            NotaFiscalDAL dbNotaFiscal = new NotaFiscalDAL();

            objNotaFiscal = dbNotaFiscal.ObterNotaFiscalPorID(Convert.ToInt32(gvNF.SelectedDataKey.Value), ref erro);
            
            if (objNotaFiscal != null) bindNF(objNotaFiscal);
        }
        private void bindNF(NotaFiscal notaFiscal)
        {
            txtCodigo.Text = notaFiscal.IdNotaFiscal.ToString();
            txtOrcamento.Text = notaFiscal.idOrcamento.ToString();
            txtValor.Text = notaFiscal.Valor.ToString();
            hfValorReal.Value = notaFiscal.Valor.ToString();
            txtEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtCodCliente.Text = notaFiscal.IdPessoa.ToString();
            txtNomeCliente.Text = notaFiscal.NomeCliente;
            txtVencimento.Text = (notaFiscal.Vencimento.ToString() == "01/01/0001 00:00:00") ? "" : notaFiscal.Vencimento.ToString("dd/MM/yyyy");
            ddlDesconto.SelectedValue = notaFiscal.Desconto.ToString();
            ddlStatus.SelectedValue = notaFiscal.Status.ToString();
        }

        protected void ddlDesconto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCodigo.Text == "")
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

        protected void gvOrcamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvNF.PageIndex = e.NewPageIndex;
            carregaGvOrcamento();
        }

        private bool validacoes()
        {
            if (txtCodigo.Text == "" || txtCodCliente.Text == "" || txtVencimento.Text == "")
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

            if (objNotafical != null)
            {
                return true;
            }
            return false;
        }

        protected void txtBusca_TextChanged(object sender, EventArgs e)
        {
            OrcamentoDAL dbOrcamento = new OrcamentoDAL();
            DataTable lstProduto = dbOrcamento.PesquisaListaOrcamentoPendente(txtBusca.Text, ref erro);

            gvNF.DataSource = lstProduto;
            gvNF.AutoGenerateSelectButton = true;
            gvNF.DataBind();
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "openModal();", true);
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!validacoes())
            {
                Session.Add("danger", "Preencha os campos corretamente! " + erro);
                return;
            }

            NotaFiscal objNotafical = new NotaFiscal();
            NotaFiscalDAL dbNotaFiscal = new NotaFiscalDAL();

            objNotafical.IdEmpresa = 1;
            objNotafical.IdNotaFiscal = Convert.ToInt32(txtCodigo.Text);
            objNotafical.idOrcamento = Convert.ToInt32(txtOrcamento.Text);
            objNotafical.Status = Convert.ToInt32(ddlStatus.SelectedValue);
            objNotafical.Valor = Convert.ToDecimal(txtValor.Text);
            objNotafical.Vencimento = Convert.ToDateTime(txtVencimento.Text);
            objNotafical.DataEmissao = Convert.ToDateTime(txtEmissao.Text);
            objNotafical.IdPessoa = Convert.ToInt32(txtCodCliente.Text);
            objNotafical.Desconto = Convert.ToInt16(ddlDesconto.SelectedValue);

            if (ConsultaNF())
            {
                if (!dbNotaFiscal.AtualizaNotaFiscal(objNotafical, ref erro) && erro != "")
                {
                    Session.Add("danger", "Não foi possível Atualizar a Nota Fiscal" + erro);
                    return;
                }
                else
                {
                    Session.Add("success", "Nota Fiscal Atualizada com Sucesso!");
                }
            }
            else
            {
                Session.Add("danger", "Não foi possível Atualizar a Nota Fiscal" + erro);
            }
        }

        protected void btnCancela_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = string.Empty;
            txtEmissao.Text = string.Empty;
            txtNomeCliente.Text = string.Empty;
            txtValor.Text = string.Empty;
            txtVencimento.Text = string.Empty;
            txtCodCliente.Text = string.Empty;
            txtBusca.Text = string.Empty;
            hfValorReal.Value = string.Empty;
            ddlDesconto.SelectedIndex = 0;
        }
    }
}