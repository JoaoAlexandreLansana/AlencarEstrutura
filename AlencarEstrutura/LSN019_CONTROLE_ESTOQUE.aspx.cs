using AlencarEstrutura.DAL;
using AlencarEstrutura.DTO;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace AlencarEstrutura
{
    public partial class LSN019_CONTROLE_ESTOQUE : Page
    {
        private string erro = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaGvProduto();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Estoque objEstoque = new Estoque();
            EstoqueDAL dbEstoque = new EstoqueDAL();

            if (!validacoes())
            {
                Session.Add("danger", "Preencha todos os campos corretamente! ");
            }

            objEstoque = dbEstoque.ObterEstoquePorID(Convert.ToInt32(txtCodigo.Text), ref erro);

            objEstoque.Quantidade -= Convert.ToInt32(txtQuantidade.Text);

            if (!dbEstoque.AtualizaEstoque(objEstoque, ref erro))
            {
                Session.Add("danger", "Não foi possível dar baixa neste estoque ");
            }
            else
            {
                Session.Add("success", "Estoque atualizado! ");
                limpa();
            }
            CarregaGvProduto();
        }

        private bool validacoes()
        {
            if (string.IsNullOrEmpty(txtCodigo.Text) || string.IsNullOrEmpty(txtDescricao.Text) || string.IsNullOrEmpty(txtQuantidade.Text))
            {
                return false;
            }
            return true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpa();
        }

        private void CarregaGvProduto()
        {
            Estoque objEstoque = new Estoque();
            EstoqueDAL dbEstoque = new EstoqueDAL();
            List<Estoque> lstProduto = dbEstoque.ObterListaEstoque(ref erro);


            gvProduto.DataSource = lstProduto;
            gvProduto.AutoGenerateSelectButton = true;
            gvProduto.DataBind();

        }

        protected void gvProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            EstoqueDAL dbEstoque = new EstoqueDAL();
            Estoque objEstoque = new Estoque();

            objEstoque = dbEstoque.ObterEstoquePorID(Convert.ToInt32(gvProduto.SelectedDataKey.Value), ref erro);

            if (objEstoque != null) bindProduto(objEstoque);
        }

        private void bindProduto(Estoque dbEstoque)
        {
            txtCodigo.Text = dbEstoque.IdEstoque.ToString();
            txtDescricao.Text = dbEstoque.Descricao;
            if(dbEstoque.Quantidade < 1)
            {
                btnSalvar.Enabled = false;
                lblQuantidade.BackColor = System.Drawing.Color.Red;
            }
            lblQuantidadeDisponivel.Text = dbEstoque.Quantidade.ToString();
        }

        private void limpa()
        {
            txtCodigo.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtQuantidade.Text = string.Empty;
        }

        protected void txtBusca_TextChanged(object sender, EventArgs e)
        {
            Estoque objEstoque = new Estoque();
            EstoqueDAL dbEstoque = new EstoqueDAL();
            List<Estoque> lstProduto = dbEstoque.PesquisarListaEstoque(txtBusca.Text, ref erro);

            gvProduto.DataSource = lstProduto;
            gvProduto.AutoGenerateSelectButton = true;
            gvProduto.DataBind();
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "openModal();", true);
        }
    }
}