using AlencarEstrutura.DAL;
using AlencarEstrutura.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlencarEstrutura
{
    public partial class LSN003_PRODUTO : System.Web.UI.Page
    {
        public string erro = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaDDLCategoria();
            CarregaGvProduto();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Validacoes();

            Produto objProduto = new Produto();
            ProdutoDAL dbProduto = new ProdutoDAL();

            objProduto.IdProduto = (string.IsNullOrEmpty(txtCodigo.Text)) ? 0 : Convert.ToInt32(txtCodigo.Text);

            objProduto.Descricao = txtDescricao.Text;
            objProduto.IdCategoria = Convert.ToInt32(ddlCategoria.SelectedValue);
            objProduto.Observacao = txtObservacao.Text;
            objProduto.Valor = (string.IsNullOrEmpty(txtValor.Text)) ? 0 : Convert.ToDecimal(txtValor.Text);
            objProduto.Peso = (string.IsNullOrEmpty(txtPeso.Text)) ? 0 : Convert.ToDecimal(txtPeso.Text);
            objProduto.Litros = (string.IsNullOrEmpty(txtLitros.Text)) ? 0 : Convert.ToDecimal(txtLitros.Text);

            if (objProduto.IdProduto == 0)
            {
                if (!dbProduto.InserirProduto(objProduto, ref erro))
                {
                    Session.Add("danger", "Não foi possível efetuar o cadastro! " + erro);
                }
                else
                {
                    Session.Add("success", "Cadastro efetuado com Sucesso! ");
                }
            }
            else
            {
                if (!dbProduto.AtualizarProdutoPorId(objProduto, ref erro))
                {
                    Session.Add("danger", "Não foi possível atualizar o Produto! " + erro);
                }
                else
                {
                    Session.Add("success", "Cadastro efetuado com Sucesso! ");
                }
            }
        }

        protected void btnBuscaProduto_Click(object sender, EventArgs e)
        {

        }

        private void CarregaDDLCategoria()
        {
            Produto objCategoria = new Produto();
            CategoriaDAL dbCategoria = new CategoriaDAL();
            List<Categoria> lstCategoria = dbCategoria.ObterListaDeCategoria(ref erro);

            foreach (Categoria categoria in lstCategoria)
            {
                ListItem lst = new ListItem();

                lst.Value = categoria.IdCategoria.ToString();
                lst.Text = categoria.Descricao;

                ddlCategoria.Items.Add(lst);
            }
            ddlCategoria.DataBind();
        }

        private void CarregaGvProduto()
        {
            Categoria objProduto = new Categoria();
            ProdutoDAL dbProduto = new ProdutoDAL();
            List<Produto> lstProduto = dbProduto.ObterListadeProduto(ref erro);


            gvProduto.DataSource = lstProduto;
            gvProduto.AutoGenerateSelectButton = true;
            gvProduto.DataBind();

        }

        protected void gvProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProdutoDAL dbProduto = new ProdutoDAL();
            Produto objProduto = new Produto();

            objProduto = dbProduto.ObterProdutoPorID(Convert.ToInt32(gvProduto.SelectedDataKey.Value), ref erro);

            if (objProduto != null) BindProduto(objProduto);
        }

        private void BindProduto(Produto objProduto)
        {
            txtCodigo.Text = objProduto.IdProduto.ToString();
            txtDescricao.Text = objProduto.Descricao;
            txtObservacao.Text = objProduto.Observacao;
            txtValor.Text = objProduto.Valor.ToString();
            txtPeso.Text = objProduto.Peso.ToString();
            txtLitros.Text = objProduto.Litros.ToString();
            ddlCategoria.SelectedIndex = objProduto.IdCategoria;
        }

        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                ProdutoDAL dbProduto = new ProdutoDAL();
                Produto objProduto = new Produto();

                objProduto = dbProduto.ObterProdutoPorID(Convert.ToInt32(txtCodigo.Text), ref erro);

                if (objProduto != null) BindProduto(objProduto);
            }
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            Validacoes();

            ProdutoDAL dbProduto = new ProdutoDAL();
            Produto objProduto = new Produto();

            if (!dbProduto.ExcluirProduto(Convert.ToInt32(txtCodigo.Text),ref erro))
            {
                Session.Add("danger", "Não foi possível deletar o Produto! " + erro);
            }
            else
            {
                Session.Add("success", "Produto deletado com Sucesso! ");
            }
        }

        private void Validacoes()
        {
            if(string.IsNullOrEmpty(txtCodigo.Text) || string.IsNullOrEmpty(txtDescricao.Text))
            {
                Session.Add("danger", "Preencha os campos obrigatórios! ");
                return;
            }
        }
    }
}