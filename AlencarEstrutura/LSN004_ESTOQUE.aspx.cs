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
    public partial class LSN004_ESTOQUE : System.Web.UI.Page
    {
        private string erro = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaddlProduto();
            CarregaGvEstoque();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Validacoes();

            Estoque objEstoque = new Estoque();
            EstoqueDAL dbEstoque = new EstoqueDAL();

            objEstoque.IdEstoque = (string.IsNullOrEmpty(txtCodigo.Text)) ? 0 : Convert.ToInt32(txtCodigo.Text);
            objEstoque.Descricao = txtDescricao.Text;
            objEstoque.IdProduto = Convert.ToInt32(ddlProduto.SelectedValue);
            if (!string.IsNullOrEmpty(txtValidade.Text)) objEstoque.Validade = Convert.ToDateTime(txtValidade.Text);
            objEstoque.Quantidade = Convert.ToInt32(txtQuantidade.Text);
            objEstoque.Observacao = txtObservacao.Text;


            if (objEstoque.IdEstoque == 0)
            {
                if (dbEstoque.InserirEstoque(objEstoque, ref erro))
                {
                    Session.Add("success", "Cadastro Realizado com Sucesso! ");
                }
                else
                {
                    Session.Add("danger", "Não foi possível efetuar o cadastro! " + erro);
                }
            }
            else
            {
                if (dbEstoque.AtualizaEstoque(objEstoque, ref erro))
                {
                    Session.Add("success", "Cadastro Realizado com Sucesso! ");
                }
                else
                {
                    Session.Add("danger", "Não foi possível efetuar o cadastro! " + erro);
                }
            }

        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            Validacoes();

            Estoque objEstoque = new Estoque();
            EstoqueDAL dbEstoque = new EstoqueDAL();

            if(!dbEstoque.ExcluirEstoque(Convert.ToInt32(txtCodigo.Text),ref erro))
            {
                Session.Add("danger", "Não foi possível excluir o registro! " + erro);
            }
            else
            {
                Session.Add("success", "Cadastro Excluído com Sucesso! ");
            }
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
            txtObservacao.Text = objEstoque.Observacao;
            txtValidade.Text = objEstoque.Validade.ToString("dd/MM/yyyy");
            txtQuantidade.Text = objEstoque.Quantidade.ToString();
            ddlProduto.SelectedIndex = objEstoque.IdProduto;
        }

        private void CarregaddlProduto()
        {
            Produto objProduto = new Produto();
            ProdutoDAL dbProduto = new ProdutoDAL();
            List<Produto> lstProduto = dbProduto.ObterListadeProduto(ref erro);

            foreach (Produto Produto in lstProduto)
            {
                ListItem lst = new ListItem();

                lst.Value = Produto.IdProduto.ToString();
                lst.Text = Produto.Descricao;

                ddlProduto.Items.Add(lst);
            }
            ddlProduto.DataBind();
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

        private void Validacoes()
        {
            if (string.IsNullOrEmpty(txtDescricao.Text))
            {
                Session.Add("danger", "Preencha os campos obrigatórios! ");
                txtDescricao.Focus();
                return;
            }

            if (txtValidade.Text != "")
            {
                if (Convert.ToDateTime(txtValidade.Text) < DateTime.Now)
                {
                    Session.Add("danger", "Data Inválida");
                    return;
                }
            }
        }

        protected void btnBuscaEstoque_Click(object sender, EventArgs e)
        {
            CarregaGvEstoque();
        }
    }
}