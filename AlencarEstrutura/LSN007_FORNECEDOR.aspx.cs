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
    public partial class LSN007_FORNECEDOR : System.Web.UI.Page
    {
        private string erro = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaDDLEstado();
            carregaGvFornecedor();
        }

        private void CarregaDDLEstado()
        {
            EstadoDAL objEstado = new EstadoDAL();
            List<Estado> lstEstado = objEstado.ObterListaDeEstados();

            foreach (Estado lstSigla in lstEstado)
            {
                ListItem lst = new ListItem();

                lst.Value = lstSigla.IdEstado.ToString();
                lst.Text = lstSigla.Sigla;

                ddlEstado.Items.Add(lst);
            }
            ddlEstado.DataBind();
        }

        private void CarregaDDLMunicipioPorID(int idEstado)
        {
            MunicipioDAL objEstado = new MunicipioDAL();
            List<Municipio> lstMunicipio = objEstado.ObterListaDeMunicipioPorID(idEstado);

            foreach (Municipio lstSigla in lstMunicipio)
            {
                ListItem lst = new ListItem();

                lst.Value = lstSigla.IdMunicipio.ToString();
                lst.Text = lstSigla.Nome;

                ddlMunicipio.Items.Add(lst);
            }
            ddlMunicipio.DataBind();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!validaFormulario())
            {
                Session.Add("danger", "Preencha os campos corretamente!" + erro);
                return;
            }

            Pessoa objPessoa = new Pessoa();

            objPessoa.IdPessoa = (string.IsNullOrEmpty(txtCodigo.Text)) ? 0 : Convert.ToInt32(txtCodigo.Text);

            objPessoa.CNPJ = Convert.ToInt64(txtCNPJ.Text);
            objPessoa.NomePessoa = txtNomeFantasia.Text;
            objPessoa.NomeFantasia = txtNomeFantasia.Text;
            objPessoa.RazaoSocial = txtRazao.Text;
            objPessoa.Email = txtEmail.Text;
            objPessoa.TipoPessoa = "J";

            Telefone objTelefone = new Telefone();

            objTelefone.Numero = txtTelefone.Text;
            objTelefone.DDD = Convert.ToInt32(txtDd.Text);
            objTelefone.IdPessoa = objPessoa.IdPessoa;
            objTelefone.IdTipoTelefone = Convert.ToInt32(rbTipoTelefone.SelectedValue);

            Endereco objEndereco = new Endereco();

            objEndereco.Logradouro = txtEndereco.Text;
            objEndereco.Numero = txtNumero.Text;
            objEndereco.CEP = txtCEP.Text;
            objEndereco.Complemento = txtComplemento.Text;
            objEndereco.IdMunicipio = ddlMunicipio.SelectedIndex;
            objEndereco.IdEstado = ddlEstado.SelectedIndex;
            objEndereco.IdPessoa = objPessoa.IdPessoa;

            PessoaDAL dbPessoa = new PessoaDAL();

            if (objPessoa.IdPessoa == 0)
            {
                if (dbPessoa.InserirPessoa(objPessoa, objEndereco, objTelefone, ref erro))
                {
                    Session.Add("success", "Cadastro Efetuado com Sucesso!");
                }
                else
                {
                    Session.Add("danger", "Não foi possível efetuar o cadastro!" + erro);
                }
            }
            else
            {
                if (!dbPessoa.AtualizarPessoa(objPessoa, ref erro))
                {
                    Session.Add("danger", "Não foi possível Atualizar o cadastro!" + erro);
                    return;
                }
                FornecedorDAL dbFornecedor = new FornecedorDAL();

                if (!dbFornecedor.AtualizaFornecedor(objPessoa, ref erro) && erro != "")
                {
                    Session.Add("danger", "Não foi possível Atualizar o cadastro!" + erro);
                    return;
                }
                TelefoneDAL dbTelefone = new TelefoneDAL();

                if (!dbTelefone.AtualizarTelefone(objTelefone, ref erro) && erro != "")
                {
                    Session.Add("danger", "Não foi possível Atualizar o cadastro!" + erro);
                    return;
                }
                EnderecoDAL dbEndereco = new EnderecoDAL();

                if (!dbEndereco.AtualizaEndereco(objEndereco, ref erro) && erro != "")
                {
                    Session.Add("danger", "Não foi possível Atualizar o cadastro!" + erro);
                    return;
                }
                Session.Add("success", "Cadastro Atualizado com Sucesso!");
                carregaGvFornecedor();
                limpar();
            }
        }

        private bool validaFormulario()
        {
            if (string.IsNullOrEmpty(txtNomeFantasia.Text) || string.IsNullOrEmpty(txtCNPJ.Text) || string.IsNullOrEmpty(txtEndereco.Text) || string.IsNullOrEmpty(txtNumero.Text))
            {
                Session.Add("danger", "Preencha os Campos Obrigatórios!");
                return false;
            }
            return true;

        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDDLMunicipioPorID(Convert.ToInt32(ddlEstado.SelectedValue));
        }

        protected void gvFornecedor_SelectedIndexChanged(object sender, EventArgs e)
        {

            FornecedorDAL dbFornecedor = new FornecedorDAL();

            Pessoa objFornecedor = dbFornecedor.ObterFornecedorPorIDPessoa(Convert.ToInt32(gvFornecedor.SelectedDataKey.Value), ref erro);

            bindFornecedor(objFornecedor);
        }

        private void bindFornecedor(Pessoa fornecedor)
        {
            txtCodigo.Text = fornecedor.IdPessoa.ToString();
            txtNomeFantasia.Text = fornecedor.NomeFantasia;
            txtRazao.Text = fornecedor.RazaoSocial;
            txtCNPJ.Text = fornecedor.CNPJ.ToString();
            txtEmail.Text = fornecedor.Email;
            txtDd.Text = fornecedor.telefone.DDD.ToString();
            txtTelefone.Text = fornecedor.telefone.Numero;
            rbTipoTelefone.SelectedValue = fornecedor.telefone.TipoTelefone;
            txtEndereco.Text = fornecedor.endereco.Logradouro;
            txtNumero.Text = fornecedor.endereco.Numero;
            txtComplemento.Text = fornecedor.endereco.Complemento;
            txtCEP.Text = fornecedor.endereco.CEP;
            ddlEstado.SelectedValue = fornecedor.endereco.IdEstado.ToString();
            CarregaDDLMunicipioPorID(fornecedor.endereco.IdEstado);
            ddlMunicipio.SelectedValue = fornecedor.endereco.IdMunicipio.ToString();

        }

        public void carregaGvFornecedor()
        {
            Pessoa objProduto = new Pessoa();
            FornecedorDAL dbFornecedor = new FornecedorDAL();
            DataTable dtProduto = dbFornecedor.ObterListaNomeFornecedor(ref erro);


            gvFornecedor.DataSource = dtProduto;
            gvFornecedor.AutoGenerateSelectButton = true;
            gvFornecedor.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpar();
        }

        private void limpar()
        {
            txtCodigo.Text = string.Empty;
            txtNomeFantasia.Text = string.Empty;
            txtRazao.Text = string.Empty;
            txtCNPJ.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtDd.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            rbTipoTelefone.SelectedValue = string.Empty;
            txtEndereco.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtComplemento.Text = string.Empty;
            txtCEP.Text = string.Empty;
            ddlMunicipio.Items.Clear();
            CarregaDDLEstado();
        }
    }
}