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
    public partial class LSN009_SOLICITAR_PRODUTO : System.Web.UI.Page
    {
        private string erro = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaGvEstoque();
            }
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

        protected void gvEstoque_SelectedIndexChanged(object sender, EventArgs e)
        {
            EstoqueDAL dbEstoque = new EstoqueDAL();
            Estoque objEstoque = new Estoque();

            objEstoque = dbEstoque.ObterEstoquePorID(Convert.ToInt32(gvEstoque.SelectedDataKey.Value), ref erro);

            if (objEstoque != null) BindProduto(objEstoque);
        }

        private void BindProduto(Estoque objEstoque)
        {
            txtCodigo.Text = objEstoque.IdEstoque.ToString();
            txtDescricao.Text = objEstoque.Descricao;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCodigo.Text))
                {
                    Estoque objEstoque = new Estoque();
                    EstoqueDAL dbEstoque = new EstoqueDAL();

                    objEstoque = dbEstoque.ObterEstoquePorID(Convert.ToInt32(txtCodigo.Text), ref erro);

                    objEstoque.Quantidade = (objEstoque.Quantidade - Convert.ToDecimal(txtQuantidade.Text));

                    if (!dbEstoque.AtualizaEstoque(objEstoque, ref erro))
                    {
                        Session.Add("danger", "Não foi possível dar baixa neste produto! " + erro);
                        return;
                    }
                    Session.Add("success", "Produto retirado com Sucesso! ");

                    CarregaGvEstoque();
                }
                else
                {
                    Session.Add("danger", "Selecione um produto");
                }
            }
            catch (Exception ex)
            {
                Session.Add("danger", ex);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtQuantidade.Text = string.Empty;
        }
    }
}