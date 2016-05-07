using AlencarEstrutura.DAL;
using AlencarEstrutura.DataSets;
using AlencarEstrutura.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;

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
            if(dsPedido != null)
            {
                crystalReport.SetDataSource(dsPedido);
                CrystalReportViewer1.ReportSource = crystalReport;
            }

            if(erro != "") Session.Add("danger", "Não foi possível imprimir "+ erro);
        }

        protected void gvPedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodigo.Text = gvPedidos.SelectedDataKey.Value.ToString();

            PedidoCompra objPedidoCompra = new PedidoCompra();
            PedidoCompraDAL dbPedidoCompra = new PedidoCompraDAL();

            objPedidoCompra = dbPedidoCompra.ObterPedidoPorID(Convert.ToInt32(txtCodigo.Text),ref erro);

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
    }
}