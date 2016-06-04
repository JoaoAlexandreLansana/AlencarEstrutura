using AlencarEstrutura.DAL;
using AlencarEstrutura.DataSets;
using AlencarEstrutura.DTO;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace AlencarEstrutura
{
    public partial class LSN021_IMPRIMIRPEDIDOCOMPRA : System.Web.UI.Page
    {
        private string erro = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregaGvPedido();
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            PedidoCompraDAL dbPedidoCompra = new PedidoCompraDAL();

            LSN006_PEDIDOCOMPRA_ crystalReport = new LSN006_PEDIDOCOMPRA_();
            DataSet1 dsPedido = dbPedidoCompra.GetData(Convert.ToInt32(txtCodigo.Text), ref erro);
            if (dsPedido != null)
            {
                crystalReport.SetDataSource(dsPedido);
                CrystalReportViewer1.ReportSource = crystalReport;
                crystalReport.PrintToPrinter(1, false, 0, 0);
            }

            if (erro != "") Session.Add("danger", "Não foi possível imprimir " + erro);
        }

        protected void gvPedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodigo.Text = gvPedidos.SelectedDataKey.Value.ToString();

            PedidoCompra objPedidoCompra = new PedidoCompra();
            PedidoCompraDAL dbPedidoCompra = new PedidoCompraDAL();

            objPedidoCompra = dbPedidoCompra.ObterPedidoPorID(Convert.ToInt32(txtCodigo.Text), ref erro);

            if (objPedidoCompra != null) binPedidoCompra(objPedidoCompra);
        }

        private void binPedidoCompra(PedidoCompra pedidoCompra)
        {
            txtData.Text = pedidoCompra.DataPedido.ToString();
            txtData.Text = pedidoCompra.DataPedido.ToString();
        }

        private void carregaGvPedido()
        {
            PedidoCompra objPedidoCompra = new PedidoCompra();
            PedidoCompraDAL dbPedidoCompra = new PedidoCompraDAL();

            DataTable dt = dbPedidoCompra.CarregaListaPedidos(ref erro);

            gvPedidos.DataSource = dt;
            gvPedidos.AutoGenerateSelectButton = true;
            gvPedidos.DataBind();
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {
            if (!validacoes())
            {
                Session.Add("danger", "Selecione uma Pedido de Compra");
            }
            else
            {
                PedidoCompraDAL dbPedidoCompra = new PedidoCompraDAL();

                LSN006_PEDIDOCOMPRA_ crystalReport = new LSN006_PEDIDOCOMPRA_();
                DataSet1 dsPedido = dbPedidoCompra.GetData(Convert.ToInt32(txtCodigo.Text), ref erro);
                if (dsPedido != null)
                {
                    crystalReport.SetDataSource(dsPedido);
                    CrystalReportViewer1.ReportSource = crystalReport;
                }
                if (erro != "") Session.Add("danger", "Não foi possível imprimir " + erro);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            CrystalReportViewer1.ReportSource = null;
            CrystalReportViewer1.DataBind();
            txtCodigo.Text = string.Empty;
            txtData.Text = string.Empty;
        }

        private bool validacoes()
        {
            if (txtCodigo.Text == "")
            {
                return false;
            }
            return true;
        }

        protected void txtBusca_TextChanged(object sender, EventArgs e)
        {
            PedidoCompra objPedidoCompra = new PedidoCompra();
            PedidoCompraDAL dbPedidoCompra = new PedidoCompraDAL();

            DataTable dt = dbPedidoCompra.PesquisaListaPedidos(txtBusca.Text, ref erro);

            gvPedidos.DataSource = dt;
            gvPedidos.AutoGenerateSelectButton = true;
            gvPedidos.DataBind();
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "openModal();", true);
        }

        protected void gvPedidos_SelectedIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPedidos.PageIndex = e.NewPageIndex;
            carregaGvPedido();
        }
    }
}