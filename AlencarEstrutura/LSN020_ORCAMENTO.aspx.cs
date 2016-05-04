using AlencarEstrutura.DAL;
using AlencarEstrutura.DTO;
using System;
using System.Data;
using System.Web.UI;

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
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {

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
            objOrcamento.Valor = Convert.ToDecimal(txtValorPrevisto.Text) * Convert.ToDecimal(txtQuantidade.Text);

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

        }
    }
}