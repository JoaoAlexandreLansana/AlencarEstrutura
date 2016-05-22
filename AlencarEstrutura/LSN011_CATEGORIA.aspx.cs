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
    public partial class LSN011_CATEGORIA : System.Web.UI.Page
    {
        public string erro = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaGvCategoria();                
            }
            
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {

                string erro = string.Empty;
                if (string.IsNullOrEmpty(txtDescricao.Text))
                {
                    Session.Add("danger", "Preencha o formulário corretamente!");
                }

                Categoria objCategoria = new Categoria();
                CategoriaDAL dbCategoria = new CategoriaDAL();

                objCategoria.IdCategoria = (string.IsNullOrEmpty(txtCodigo.Text)) ? 0 : Convert.ToInt32(txtCodigo.Text);
                objCategoria.Descricao = txtDescricao.Text;
                objCategoria.Observacao = txtObservacao.Text;

                //se existe a categoria que está salvando, então atualiza
                if (dbCategoria.ObterCategoriaPorID(objCategoria.IdCategoria, ref erro).IdCategoria != 0)
                {
                    if (!dbCategoria.AtulizaCategoria(objCategoria, ref erro))
                    {
                        Session.Add("danger", "Não foi possível atualizar o registro " + erro);
                    }
                    else
                    {
                        Session.Add("success", "Cadastro Efetuado com Sucesso!");
                        CarregaGvCategoria();
                    }
                }
                else
                {
                    if (!dbCategoria.InserirCategoria(objCategoria, ref erro))
                    {
                        Session.Add("danger", "Não foi possível atualizar o registro " + erro);
                    }
                    else
                    {
                        Session.Add("success", "Cadastro Efetuado com Sucesso!");
                        CarregaGvCategoria();
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("danger", "Erro " + ex);
            }
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            string erro = string.Empty;
            if (string.IsNullOrEmpty(txtDescricao.Text))
            {
                Session.Add("danger", "Preencha o formulário corretamente!");
            }

            Categoria objCategoria = new Categoria();
            CategoriaDAL dbCategoria = new CategoriaDAL();

            objCategoria.IdCategoria = Convert.ToInt32(txtCodigo.Text);
            objCategoria.Descricao = txtDescricao.Text;
            objCategoria.Observacao = txtObservacao.Text;

            if (!dbCategoria.ExcluirCategoria(objCategoria.IdCategoria, ref erro))
            {
                Session.Add("danger", "Não foi possível excluir o Registro! " + erro);
            }
            else
            {
                Session.Add("danger", "Registro Excluído com Sucesso!");
            }
        }

        protected void gvCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CategoriaDAL dbCategoria = new CategoriaDAL();
            Categoria objCategoria = new Categoria();

            objCategoria = dbCategoria.ObterCategoriaPorID(Convert.ToInt32(gvCategoria.SelectedDataKey.Value), ref erro);

            if (objCategoria != null) BindProduto(objCategoria);
        }

        private void BindProduto(Categoria objCategoria)
        {
            txtCodigo.Text = objCategoria.IdCategoria.ToString();
            txtDescricao.Text = objCategoria.Descricao;
            txtObservacao.Text = objCategoria.Observacao;
        }

        private void CarregaGvCategoria()
        {
            Categoria objCategoria = new Categoria();
            CategoriaDAL dbCategoria = new CategoriaDAL();
            List<Categoria> lstProduto = dbCategoria.ObterListaDeCategoria(ref erro);


            gvCategoria.DataSource = lstProduto;
            gvCategoria.AutoGenerateSelectButton = true;
            gvCategoria.DataBind();

        }

        private void limpa()
        {
            txtCodigo.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtObservacao.Text = string.Empty;
        }
        protected void txtBusca_TextChanged(object sender, EventArgs e)
        {
            Categoria objCategoria = new Categoria();
            CategoriaDAL dbCategoria = new CategoriaDAL();
            List<Categoria> lstProduto = dbCategoria.PesquisaListaDeCategoriaPorId(txtBusca.Text, ref erro);
            
            gvCategoria.DataSource = lstProduto;
            gvCategoria.AutoGenerateSelectButton = true;
            gvCategoria.DataBind();
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "openModal();", true);
        }
    }
}