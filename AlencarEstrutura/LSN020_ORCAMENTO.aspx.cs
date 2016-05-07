using AlencarEstrutura.DAL;
using AlencarEstrutura.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlencarEstrutura
{
    public partial class LSN020_ORCAMENTO : Page
    {
        private string erro = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregaGvCliente();
                carregaGvOrcamento();
                CarregaddlProduto();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpa();
        }

        private void limpa()
        {

        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            Orcamento objOrcamento = new Orcamento();
            OrcamentoDAL dbOrcamento = new OrcamentoDAL();

            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                if (dbOrcamento.ExcluirProdutoPorIdOrcamento(Convert.ToInt32(txtCodigo.Text), ref erro))
                {
                    if (dbOrcamento.ExcluirOrcamento(Convert.ToInt32(txtCodigo.Text), ref erro))
                    {
                        Session.Add("success", "Orçamento Excluído com Sucesso! ");
                        limpa();
                    }
                    else
                    {
                        Session.Add("danger", "Não foi possível excluído o Orçamento " + erro);
                    }
                }
                else
                {
                    Session.Add("danger", "Não foi possível excluído o Orçamento " + erro);
                }
            }
        }

        private bool validacoes()
        {
            if (string.IsNullOrEmpty(txtCodCliente.Text) || string.IsNullOrEmpty(txtNomeCliente.Text) || string.IsNullOrEmpty(ddlProduto.SelectedValue))
            {
                return false;
            }
            return true;
        }
        protected void gvCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            PessoaDAL dbPessoa = new PessoaDAL();

            Pessoa objPessoa = dbPessoa.ObterPessoaID(Convert.ToInt32(gvCliente.SelectedDataKey.Value), ref erro);

            Pessoa objPessoaPorTipo = new Pessoa();

            if (objPessoa != null && objPessoa.TipoPessoa == "F")
            {
                objPessoaPorTipo = dbPessoa.ObterPessoaFisicaPorID(Convert.ToInt32(gvCliente.SelectedDataKey.Value), ref erro);
                if (erro != "")
                {
                    Session.Add("danger", "Não foi possível Carregar o Cliente selecionado!" + erro);
                    return;
                }
            }
            else if (objPessoa != null && objPessoa.TipoPessoa == "J")
            {
                objPessoaPorTipo = dbPessoa.ObterPessoaJuridicaPorID(Convert.ToInt32(gvCliente.SelectedDataKey.Values), ref erro);
                if (erro != "")
                {
                    Session.Add("danger", "Não foi possível Carregar o Cliente selecionado!" + erro);
                    return;
                }
            }

            bindCliente(objPessoaPorTipo);
        }

        private void bindCliente(Pessoa cliente)
        {
            txtCodCliente.Text = cliente.IdPessoa.ToString();
            txtNomeCliente.Text = (cliente.TipoPessoa == "F") ? cliente.NomePessoa : cliente.NomeFantasia;
        }

        public void carregaGvCliente()
        {
            Pessoa objPessoa = new Pessoa();
            PessoaDAL dbCliente = new PessoaDAL();
            DataTable dtCliente = dbCliente.ObterListaPessoa(ref erro);


            gvCliente.DataSource = dtCliente;
            gvCliente.AutoGenerateSelectButton = true;
            gvCliente.DataBind();
        }

        public void carregaGvOrcamento()
        {
            Orcamento objOrcamento = new Orcamento();
            OrcamentoDAL dbOrcamento = new OrcamentoDAL();
            DataTable dtOrcamento = dbOrcamento.ObterListaOrcamento(ref erro);

            gvOrcamento.DataSource = dtOrcamento;
            gvOrcamento.AutoGenerateSelectButton = true;
            gvOrcamento.DataBind();
        }

        public void carregaGvProduto()
        {
            Orcamento objOrcamento = new Orcamento();
            OrcamentoDAL dbOrcamento = new OrcamentoDAL();
            DataTable dtOrcamento = dbOrcamento.ObterListaProdutoOrcamentoPorID(Convert.ToInt32(txtCodigo.Text), ref erro);

            gvProdutos.DataSource = dtOrcamento;
            gvProdutos.AutoGenerateSelectButton = true;
            gvProdutos.DataBind();
            if (dtOrcamento != null)
            {
                lblTotal.Text = "TOTAL: " + dtOrcamento.Rows[0].ItemArray[6].ToString();
            }
        }

        protected void gvOrcamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            Orcamento objOrcamento = new Orcamento();
            OrcamentoDAL dbOrcamento = new OrcamentoDAL();

            objOrcamento = dbOrcamento.ObterOrcamentoPorId(Convert.ToInt32(gvOrcamento.SelectedDataKey.Value), ref erro);

            if (objOrcamento != null) bindOrcamento(objOrcamento);
        }

        private void bindOrcamento(Orcamento orcamento)
        {
            txtCodigo.Text = orcamento.IdOrcamento.ToString();
            txtDescricao.Text = orcamento.Descricao;
            carregaGvProduto();
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            Orcamento objOrcamento = new Orcamento();
            OrcamentoDAL dbOrcamento = new OrcamentoDAL();

            objOrcamento.IdOrcamento = (string.IsNullOrEmpty(txtCodigo.Text)) ? 0 : Convert.ToInt32(txtCodigo.Text);

            objOrcamento.Descricao = txtDescricao.Text;
            objOrcamento.IdPessoa = Convert.ToInt32(txtCodCliente.Text);
            objOrcamento.IdProduto = Convert.ToInt32(ddlProduto.SelectedValue);
            objOrcamento.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
            objOrcamento.Qdte_metro_quadrado = Convert.ToDecimal(txtQtdeMetroQuadrado.Text);

            if (cbValorUnitario.Checked)
            {
                objOrcamento.Valor = Convert.ToDecimal(txtValorPrevisto.Text) * Convert.ToDecimal(txtQuantidade.Text);
            }
            else
            {
                objOrcamento.Valor = (Convert.ToDecimal(txtValorPorMetro.Text) * (Convert.ToDecimal(txtQtdeMetroQuadrado.Text))) * Convert.ToDecimal(txtQuantidade.Text);
            }

            int idOrcamento = 0;

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                idOrcamento = dbOrcamento.InserirOrcamento(objOrcamento, ref erro);
                if (erro != "")
                {
                    Session.Add("danger", "Não foi possível criar o Orçamento " + erro);
                    return;
                }

                txtCodigo.Text = idOrcamento.ToString();
            }

            //objOrcamento.ValorUnitario =  (cbValorUnitario.Checked) ? 1 : 0 ;
            objOrcamento.ValorUnitario = Convert.ToInt16(cbValorUnitario.Checked);

            if (string.IsNullOrEmpty(hfCodigoProdutoOrcamento.Value))
            {
                if (!dbOrcamento.AdicionarProduto(objOrcamento, ref erro, Convert.ToInt32(txtCodigo.Text)))
                {
                    Session.Add("danger", "Não foi possível Adicionar o produto ao Orcamento numero " + idOrcamento + "! " + erro);
                    return;
                }
            }
            else
            {
                objOrcamento.IdProdutoOrcamento = Convert.ToInt32(hfCodigoProdutoOrcamento.Value);
                if (!dbOrcamento.AtualizarProduto(objOrcamento, ref erro, Convert.ToInt32(txtCodigo.Text)))
                {
                    Session.Add("danger", "Não foi possível atualizar o produto! " + erro);
                    return;
                }
            }
            carregaGvProduto();
            carregaGvOrcamento();
        }

        protected void cbValorUnitario_CheckedChanged(object sender, EventArgs e)
        {
            cbValorPorMetro.Checked = false;
            txtValorPrevisto.Enabled = true;
            txtValorPorMetro.Enabled = false;
        }

        protected void cbValorPorMetro_CheckedChanged(object sender, EventArgs e)
        {
            cbValorUnitario.Checked = false;
            txtValorPrevisto.Enabled = false;
            txtValorPorMetro.Enabled = true;
        }

        protected void ddlProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Produto objProduto = new Produto();
            ProdutoDAL dbProduto = new ProdutoDAL();

            objProduto = dbProduto.ObterProdutoPorID(Convert.ToInt32(ddlProduto.SelectedValue), ref erro);

            txtValorPrevisto.Text = objProduto.Valor.ToString();
            txtValorPorMetro.Text = objProduto.ValorPorMetro.ToString();

        }

        private void CarregaddlProduto()
        {
            Produto objProduto = new Produto();
            ProdutoDAL dbProduto = new ProdutoDAL();
            List<Produto> lstProduto = dbProduto.ObterListadeProduto(ref erro);

            if (lstProduto != null)
            {
                foreach (Produto Produto in lstProduto)
                {
                    ListItem lst = new ListItem();

                    lst.Value = Produto.IdProduto.ToString();
                    lst.Text = Produto.Descricao;

                    ddlProduto.Items.Add(lst);
                }
                ddlProduto.DataBind();
            }
            else
            {
                Session.Add("danger", "Não foi possível carregar a lista de produto " + erro);
            }
        }

        protected void gvProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Orcamento objOrcamento = new Orcamento();
            OrcamentoDAL dbOrcamento = new OrcamentoDAL();

            Produto objProduto = new Produto();
            ProdutoDAL dbProduto = new ProdutoDAL();

            Pessoa objPessoa = new Pessoa();
            PessoaDAL dbPessia = new PessoaDAL();

            objOrcamento = dbOrcamento.ObertProdutoPorID(Convert.ToInt32(gvProdutos.SelectedDataKey.Value), ref erro);

            objPessoa = dbPessia.ObterPessoaID(objOrcamento.IdPessoa, ref erro);

            objProduto = dbProduto.ObterProdutoPorID(objOrcamento.IdProduto, ref erro);

            if (objOrcamento != null && objProduto != null && objPessoa != null)
            {
                bindProduto(objOrcamento, objProduto, objPessoa);
            }
            else
            {
                Session.Add("danger", "Erro " + erro);
            }
        }

        public void bindProduto(Orcamento orcamento, Produto produto, Pessoa pessoa)
        {
            txtCodCliente.Text = orcamento.IdPessoa.ToString();
            txtDescricao.Text = orcamento.Descricao;
            txtQuantidade.Text = orcamento.Quantidade.ToString();
            txtQtdeMetroQuadrado.Text = orcamento.Qdte_metro_quadrado.ToString();
            ddlProduto.SelectedValue = orcamento.IdProduto.ToString();
            txtNomeCliente.Text = pessoa.NomePessoa;
            txtValorPrevisto.Text = produto.Valor.ToString();
            txtValorPorMetro.Text = produto.ValorPorMetro.ToString();
            hfCodigoProdutoOrcamento.Value = orcamento.IdProdutoOrcamento.ToString();

            if (orcamento.ValorUnitario == 1)
            {
                cbValorUnitario.Checked = true;
                cbValorPorMetro.Checked = false;
            }
            else
            {
                cbValorPorMetro.Checked = true;
                cbValorUnitario.Checked = false;
            }
        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {
            Orcamento objOrcamento = new Orcamento();
            OrcamentoDAL dbOrcamento = new OrcamentoDAL();

            if (!string.IsNullOrEmpty(hfCodigoProdutoOrcamento.Value))
            {
                if (dbOrcamento.ExcluirProdutoPorIdOrcamento(Convert.ToInt32(txtCodigo.Text), ref erro))
                {
                    Session.Add("success", "Produto Excluído com Sucesso! ");
                    carregaGvProduto();
                }
                else
                {
                    Session.Add("danger", "Não foi possível excluído o Produto " + erro);
                }
            }
        }
    }
}