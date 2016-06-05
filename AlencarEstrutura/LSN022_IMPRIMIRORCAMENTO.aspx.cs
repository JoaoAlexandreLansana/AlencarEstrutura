using AlencarEstrutura.DAL;
using AlencarEstrutura.DataSets;
using AlencarEstrutura.DTO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System;
using System.Data;

namespace AlencarEstrutura
{
    public partial class LSN022_IMPRIMIRORCAMENTO : System.Web.UI.Page
    {
        private string erro = string.Empty;
        LSN022_IMPRIMIRORCAMENTO_ _crystalReport = new LSN022_IMPRIMIRORCAMENTO_();
        LSN020_ORCAMENTO_ds _dsPedido = new LSN020_ORCAMENTO_ds();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregaGvOrcamento();
            }
        }

        protected void gvOrcamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCancelar_Click(this, null);
            Orcamento objOrcamento = new Orcamento();
            OrcamentoDAL dbOrcamento = new OrcamentoDAL();

            objOrcamento = dbOrcamento.ObterOrcamentoPorId(Convert.ToInt32(gvOrcamento.SelectedDataKey.Value), ref erro);

            if (objOrcamento != null) bindOrcamento(objOrcamento);
        }

        private void bindOrcamento(Orcamento orcamento)
        {
            txtCodigo.Text = orcamento.IdOrcamento.ToString();
            txtDescricao.Text = orcamento.Descricao;
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

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            if (!validacoes())
            {
                Session.Add("danger", "Selecione um Orçamento ");
            }
            else
            {
                OrcamentoDAL dbPedidoCompra = new OrcamentoDAL();

                _crystalReport = new LSN022_IMPRIMIRORCAMENTO_();
                _dsPedido = dbPedidoCompra.GetData(Convert.ToInt32(txtCodigo.Text), ref erro);
                if (_dsPedido != null)
                {
                    _crystalReport.SetDataSource(_dsPedido);
                    CrystalReportViewer1.ReportSource = _crystalReport;
                    _crystalReport.PrintToPrinter(1, false, 0, 0);
                }

                if (erro != "") Session.Add("danger", "Não foi possível imprimir " + erro);
            }
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {
            if (!validacoes())
            {
                Session.Add("danger", "Selecione um Orçamento ");
            }
            else
            {
                OrcamentoDAL dbPedidoCompra = new OrcamentoDAL();

                _crystalReport = new LSN022_IMPRIMIRORCAMENTO_();
                _dsPedido = dbPedidoCompra.GetData(Convert.ToInt32(txtCodigo.Text), ref erro);
                if (_dsPedido != null)
                {
                    _crystalReport.SetDataSource(_dsPedido);
                    CrystalReportViewer1.ReportSource = _crystalReport;
                }
                if (erro != "") Session.Add("danger", "Não foi possível imprimir " + erro);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            CrystalReportViewer1.ReportSource = null;
            CrystalReportViewer1.DataBind();
            txtCodigo.Text = string.Empty;
            txtDescricao.Text = string.Empty;
        }

        private bool validacoes()
        {
            if (txtCodigo.Text == "")
            {
                return false;
            }
            return true;
        }
    }
}