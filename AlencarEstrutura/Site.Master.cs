using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlencarEstrutura
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["login"];
            if (string.IsNullOrEmpty(cookie.Value))
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void linkButtonSair_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["login"];
            cookie.Value = string.Empty;
            Response.Cookies.Add(cookie);
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void lbCadUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN001_USUARIO.aspx");
        }

        protected void lbCadEmpresa_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN002_EMPRESA.aspx");
        }

        protected void lbCadProduto_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN003_PRODUTO.aspx");
        }

        protected void lbCategoria_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN011_CATEGORIA.aspx");
        }

        protected void lbEstoque_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN004_ESTOQUE.aspx");
        }

        protected void lbPedidoCompra_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN006_PEDIDOCOMPRA.aspx");
        }

        protected void lbFornecedor_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN007_FORNECEDOR.aspx");
        }

        protected void lbControleEstoque_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN019_CONTROLE_ESTOQUE.aspx");
        }

        protected void lbCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN014_CLIENTE.aspx");
        }

        protected void lbOrcamento_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN020_ORCAMENTO.aspx");
        }

        protected void lbImprimirPedidoCompra_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN021_IMPRIMIRPEDIDOCOMPRA.aspx");
        }

        protected void lbImprimirOrcamento_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN022_IMPRIMIRORCAMENTO.aspx");
        }

        protected void lbGeraNF_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN023_GERARNF.aspx");
        }

       
        protected void lbSolicitarProduto_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN009_SOLICITAR_PRODUTO.aspx");
        }

        protected void lbArmazenarProduto_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN010_ARMAZENAR_PRODUTO.aspx");
        }

        protected void lbConsultaEstoque_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN012_CONSULTAR_ESTOQUE.aspx");
        }

        protected void lbAlteraNF_Click(object sender, EventArgs e)
        {
            Response.Redirect("LSN024_ALTERAR_NF.aspx");
        }
    }
}