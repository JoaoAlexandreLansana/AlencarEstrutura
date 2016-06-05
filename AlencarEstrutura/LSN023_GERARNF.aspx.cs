using AlencarEstrutura.DAL;
using AlencarEstrutura.DataSets;
using AlencarEstrutura.DTO;
using System;
using System.Data;
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
            btnCancelar_Click(this, null);
            Orcamento objOrcamento = new Orcamento();
            OrcamentoDAL dbOrcamento = new OrcamentoDAL();
            DataTable dtOrcamento = dbOrcamento.ObterListaOrcamentoPorStatus(ref erro);

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
            CrystalReportViewer1.ReportSource = null;
            CrystalReportViewer1.DataBind();
        }
        private void bindOrcamento(Orcamento orcamento)
        {
            NotaFiscal objNota = new NotaFiscal();
            NotaFiscalDAL dbNota = new NotaFiscalDAL();
            objNota = dbNota.ObterNotaFiscalPorIDOrcamento(orcamento.IdOrcamento, ref erro);
            txtVencimento.Text = (objNota == null) ? "" : objNota.Vencimento.ToString("dd/MM/yyyy");
            hdIdNF.Value = (objNota == null) ? "" : objNota.IdNotaFiscal.ToString();
            txtCodigo.Text = orcamento.IdOrcamento.ToString();
            txtDescricao.Text = orcamento.Descricao;
            txtValor.Text = orcamento.Valor.ToString();
            hfValorReal.Value = orcamento.Valor.ToString();
            txtEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtCodCliente.Text = orcamento.IdPessoa.ToString();
            txtNomeCliente.Text = orcamento.NomeCliente;
            ddlDesconto.SelectedIndex = 0;
        }

        protected void ddlDesconto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CrystalReportViewer1.ReportSource = null;
                CrystalReportViewer1.DataBind();
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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            CrystalReportViewer1.ReportSource = null;
            CrystalReportViewer1.DataBind();
            txtCodigo.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtEmissao.Text = string.Empty;
            txtNomeCliente.Text = string.Empty;
            txtValor.Text = string.Empty;
            txtVencimento.Text = string.Empty;
            txtCodCliente.Text = string.Empty;
            txtBusca.Text = string.Empty;
            hfValorReal.Value = string.Empty;
            hdIdNF.Value = string.Empty;
            ddlDesconto.SelectedIndex = 0;
        }

        protected void gvOrcamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrcamento.PageIndex = e.NewPageIndex;
            carregaGvOrcamento();
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            CrystalReportViewer1.ReportSource = null;
            CrystalReportViewer1.DataBind();

            if (!validacoes())
            {
                Session.Add("danger", "Preencha os campos corretamente! " + erro);
                return;
            }

            NotaFiscal objNotafical = new NotaFiscal();
            NotaFiscalDAL dbNotaFiscal = new NotaFiscalDAL();

            objNotafical.IdEmpresa = 1;
            objNotafical.idOrcamento = Convert.ToInt32(txtCodigo.Text);
            objNotafical.Status = 0;
            objNotafical.Valor = Convert.ToDecimal(txtValor.Text);
            objNotafical.Vencimento = Convert.ToDateTime(txtVencimento.Text);
            objNotafical.DataEmissao = Convert.ToDateTime(txtEmissao.Text);
            objNotafical.IdPessoa = Convert.ToInt32(txtCodCliente.Text);
            objNotafical.Desconto = Convert.ToInt16(ddlDesconto.SelectedValue);

            if (!ConsultaNF())
            {
                if (!dbNotaFiscal.InserirNotaFiscal(objNotafical, ref erro))
                {
                    Session.Add("danger", "Não foi possível inserir a Nota Fiscal" + erro);
                    return;
                }
            }
            else
            {
                if (!dbNotaFiscal.AtualizaNotaFiscal(objNotafical, ref erro))
                {
                    Session.Add("danger", "Não foi possível Atualizar a Nota Fiscal" + erro);
                    return;
                }
            }
            imprimir();
        }

        private void imprimir()
        {
            if (!validacoes())
            {
                Session.Add("danger", "Preencha os campos corretamente! ");
            }
            else
            {
                NotaFiscalDAL dbNotaFiscal = new NotaFiscalDAL();

                LSN023_GERARNF_ crystalReport = new LSN023_GERARNF_();
                LSN023_NOTAFISCAL dsNF = dbNotaFiscal.GetData(Convert.ToInt32(txtCodigo.Text), ref erro);
                if (dsNF != null)
                {
                    crystalReport.SetDataSource(dsNF);
                    CrystalReportViewer1.ReportSource = crystalReport;
                    crystalReport.PrintToPrinter(1, false, 0, 0);
                }

                if (erro != "") Session.Add("danger", "Não foi possível imprimir " + erro);
            }
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

            if (objNotafical.idOrcamento != 0)
            {
                return true;
            }
            return false;
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {
            CrystalReportViewer1.ReportSource = null;
            CrystalReportViewer1.DataBind();

            if (!validacoes())
            {
                Session.Add("danger", "Selecione uma Nota Fiscal ");
            }
            else
            {
                NotaFiscal objNotafical = new NotaFiscal();
                NotaFiscalDAL dbNotaFiscal = new NotaFiscalDAL();

                objNotafical.IdEmpresa = 1;
                objNotafical.idOrcamento = Convert.ToInt32(txtCodigo.Text);
                objNotafical.IdNotaFiscal = Convert.ToInt32(hdIdNF.Value);
                objNotafical.Status = 0;
                objNotafical.Valor = Convert.ToDecimal(txtValor.Text);
                objNotafical.Vencimento = Convert.ToDateTime(txtVencimento.Text);
                objNotafical.DataEmissao = Convert.ToDateTime(txtEmissao.Text);
                objNotafical.IdPessoa = Convert.ToInt32(txtCodCliente.Text);
                objNotafical.Desconto = Convert.ToInt16(ddlDesconto.SelectedValue);

                if (!ConsultaNF())
                {
                    if (!dbNotaFiscal.InserirNotaFiscal(objNotafical, ref erro))
                    {
                        Session.Add("danger", "Não foi possível inserir a Nota Fiscal" + erro);
                        return;
                    }
                }
                else
                {
                    if (!dbNotaFiscal.AtualizaNotaFiscal(objNotafical, ref erro) && erro != "")
                    {
                        Session.Add("danger", "Não foi possível Atualizar a Nota Fiscal" + erro);
                        return;
                    }
                }
                LSN023_GERARNF_ crystalReport = new LSN023_GERARNF_();
                LSN023_NOTAFISCAL dsNF = dbNotaFiscal.GetData(Convert.ToInt32(txtCodigo.Text), ref erro);
                if (dsNF != null)
                {
                    crystalReport.SetDataSource(dsNF);
                    CrystalReportViewer1.ReportSource = crystalReport;
                }
                if (erro != "") Session.Add("danger", "Não foi possível imprimir " + erro);
            }
        }

        protected void txtBusca_TextChanged(object sender, EventArgs e)
        {
            OrcamentoDAL dbOrcamento = new OrcamentoDAL();
            DataTable lstProduto = dbOrcamento.PesquisaListaOrcamentoPendente(txtBusca.Text, ref erro);

            gvOrcamento.DataSource = lstProduto;
            gvOrcamento.AutoGenerateSelectButton = true;
            gvOrcamento.DataBind();
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "openModal();", true);
        }
    }
}