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
    public partial class LSN006_PEDIDOCOMPRA : System.Web.UI.Page
    {
        private string erro = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CarregaddlProduto();
                CarregaDdlFornecedor();
                carregaGvPedido();
            }
        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {
            PedidoCompraDAL dbPedido = new PedidoCompraDAL();

            if (!dbPedido.ExcluirProdutoPorIdPedido(Convert.ToInt32(hfIdPedido.Value), ref erro))
            {
                Session.Add("danger", "Não foi possível Excluir o produto " + erro);
            }
            else
            {
                Session.Add("success", "Produto Removido ");
            }
        }

        protected void gvPedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGVProdutos(Convert.ToInt32(gvPedidos.SelectedDataKey.Value));
            txtCodigo.Text = gvPedidos.SelectedDataKey.Value.ToString();
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            PedidoCompra objPedidoCompra = new PedidoCompra();
            PedidoCompraDAL dbPedidoCompra = new PedidoCompraDAL();

            HttpCookie cookie = Request.Cookies["IdUsuario"];


            objPedidoCompra.DataPedido = DateTime.Now;
            objPedidoCompra.IdProduto = Convert.ToInt32(ddlProduto.SelectedValue);
            objPedidoCompra.Observacao = txtObservacao.Text;
            objPedidoCompra.Status = 1;
            objPedidoCompra.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
            objPedidoCompra.ValorPrevisto = Convert.ToDouble(txtValorPrevisto.Text);
            objPedidoCompra.IdFornecedor = (ddlFornecedor.SelectedValue == "Selecione") ? 0 : Convert.ToInt32(ddlFornecedor.SelectedValue);
            objPedidoCompra.IdUsuario = Convert.ToInt32(cookie.Value);

            int IdPedido = 0;

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                IdPedido = dbPedidoCompra.CriarPedidoCompra(objPedidoCompra, ref erro);
                if (erro != "")
                {
                    Session.Add("danger", "Não foi possível criar o Pedido de Compras " + erro);
                    return;
                }
                HttpCookie cookiePedido = new HttpCookie("idPedidoCompra");
                cookiePedido.Value = IdPedido.ToString();
                Response.Cookies.Add(cookiePedido);
                txtCodigo.Text = IdPedido.ToString();
            }

            if (string.IsNullOrEmpty(hfIdPedido.Value))
            {
                if (!dbPedidoCompra.AdicionarProdutos(objPedidoCompra, ref erro, Convert.ToInt32(txtCodigo.Text)))
                {
                    Session.Add("danger", "Não foi possível Adicionar o produto ao pedido de compra numero " + IdPedido + "! " + erro);
                    return;
                }
            }
            else
            {
                objPedidoCompra.IdProdutoPedido = Convert.ToInt32(hfIdPedido.Value);
                if (!dbPedidoCompra.AtualizaProduto(objPedidoCompra, ref erro, Convert.ToInt32(txtCodigo.Text)))
                {
                    Session.Add("danger", "Não foi possível atualizar o produto! " + erro);
                    return;
                }
            }
            CarregaGVProdutos(Convert.ToInt32(txtCodigo.Text));
            carregaGvPedido();
        }

        private void Validacoes()
        {
            if (ddlProduto.SelectedValue == string.Empty || string.IsNullOrEmpty(txtQuantidade.Text))
            {
                Session.Add("danger", "Preencha o formulário corretamente!");
                return;
            }
        }

        private void CarregaGVProdutos(int IdPedido)
        {
            PedidoCompra objPedidoCompra = new PedidoCompra();
            PedidoCompraDAL dbPedidoCompra = new PedidoCompraDAL();

            DataTable dt = dbPedidoCompra.CarregaListaProdutos(IdPedido, ref erro);

            gvProdutos.DataSource = dt;
            gvProdutos.AutoGenerateSelectButton = true;
            gvProdutos.DataBind();
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

        private void CarregaDdlFornecedor()
        {
            Pessoa objProduto = new Pessoa();
            FornecedorDAL dbProduto = new FornecedorDAL();
            List<Pessoa> lstProduto = dbProduto.ObterListaFornecedor(ref erro);

            foreach (Pessoa Pessoa in lstProduto)
            {
                ListItem lst = new ListItem();

                lst.Value = Pessoa.IdPessoa.ToString();
                lst.Text = Pessoa.NomeFantasia;

                ddlFornecedor.Items.Add(lst);
            }
            ddlFornecedor.DataBind();
        }

        protected void btnConcluir_Click(object sender, EventArgs e)
        {
            HttpCookie cookieId = Request.Cookies["idPedidoCompra"];
            cookieId.Value = "";
            Response.Cookies.Add(cookieId);
            Session.Add("success", "Pedido de Compra concluído com Sucesso! ");
            limpa();
            carregaGvPedido();
        }

        private void limpa()
        {
            txtCodigo.Text = string.Empty;
            txtObservacao.Text = string.Empty;
            txtQuantidade.Text = string.Empty;
            txtValorPrevisto.Text = string.Empty;
            ddlProduto.SelectedValue = "Selecione";
            ddlFornecedor.SelectedValue = "Selecione";
            gvProdutos.DataSource = null;
            gvProdutos.DataBind();
            hfIdPedido.Value = string.Empty;
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

        protected void gvProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {

            PedidoCompraDAL dbPedidoCompra = new PedidoCompraDAL();
            Produto_Pedido objPedidoCompra = dbPedidoCompra.CarregaProdutoporId(Convert.ToInt32(gvProdutos.SelectedDataKey.Value), ref erro);

            if (objPedidoCompra != null)
            {
                ddlProduto.SelectedValue = objPedidoCompra.IdProduto.ToString();
                ddlFornecedor.SelectedValue = (objPedidoCompra.IdFornecedor == 0) ? "Selecione" : objPedidoCompra.IdFornecedor.ToString();
                txtQuantidade.Text = objPedidoCompra.Quantidade.ToString();
                txtValorPrevisto.Text = objPedidoCompra.ValorPrevisto.ToString();
                txtObservacao.Text = objPedidoCompra.Observacao;
                hfIdPedido.Value = objPedidoCompra.IdProdutoPedido.ToString();
            }

            if (erro != "")
            {
                Session.Add("danger", "Erro. " + erro);
            }
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            PedidoCompraDAL dbPedidoCompra = new PedidoCompraDAL();
            PedidoCompra objPedidoCompra = new PedidoCompra();

            if (dbPedidoCompra.ExcluirProdutoPorIdPedido(Convert.ToInt32(txtCodigo.Text), ref erro) || erro == "")
            {
                if(dbPedidoCompra.ExcluirPedidoCompra(Convert.ToInt32(txtCodigo.Text), ref erro) || erro == "")
                {
                    Session.Add("success", "Pedido de Compra excluído com sucesso! ");
                    limpa();
                }
                else
                {
                    Session.Add("danger", "Não foi possível deletar o produto! " + erro);
                }
            }
            else
            {
                Session.Add("danger", "Não foi possível excluir o Pedido de Compra! " + erro);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpa();
        }

        protected void ddlProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Produto objProduto = new Produto();
            ProdutoDAL dbProduto = new ProdutoDAL();

            objProduto = dbProduto.ObterProdutoPorID(Convert.ToInt32(ddlProduto.SelectedValue), ref erro);

            txtValorPrevisto.Text = objProduto.Valor.ToString();

        }
    }
}