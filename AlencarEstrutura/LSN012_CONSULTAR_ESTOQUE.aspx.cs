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
    public partial class LSN012_CONSULTAR_ESTOQUE : System.Web.UI.Page
    {
        private string erro = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaGvEstoque();
        }

        private void CarregaGvEstoque()
        {
            Estoque objEstoque = new Estoque();
            EstoqueDAL dbEstoque = new EstoqueDAL();
            List<Estoque> lstEstoque = dbEstoque.ObterListaEstoque(ref erro);


            gvEstoque.DataSource = lstEstoque;
            gvEstoque.AutoGenerateSelectButton = true;
            gvEstoque.DataBind();

        }
    }
}