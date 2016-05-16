using AlencarEstrutura.DAL;
using AlencarEstrutura.DataSets;
using AlencarEstrutura.DTO;
using System;
using System.Data;

namespace AlencarEstrutura
{
    public partial class LSN022_IMPRIMIRORCAMENTO : System.Web.UI.Page
    {
        private string erro = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregaGvOrcamento();
            }
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
            OrcamentoDAL dbPedidoCompra = new OrcamentoDAL();

            LSN022_IMPRIMIRORCAMENTO_ crystalReport = new LSN022_IMPRIMIRORCAMENTO_();
            LSN020_ORCAMENTO_ds dsPedido = dbPedidoCompra.GetData(Convert.ToInt32(txtCodigo.Text), ref erro);
            if (dsPedido != null)
            {
                crystalReport.SetDataSource(dsPedido);
                CrystalReportViewer1.ReportSource = crystalReport;
            }

            if (erro != "") Session.Add("danger", "Não foi possível imprimir " + erro);
        }
    }
}