using AlencarEstrutura.DAL;
using AlencarEstrutura.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace AlencarEstrutura
{
    public partial class LSN014_CLIENTE : System.Web.UI.Page
    {
        private string erro = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaDDLEstado();
                carregaGvCliente();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!validacoes())
            {
                Session.Add("danger", "Preencha os campos corretamente!");
                return;
            }

            Pessoa objPessoa = new Pessoa();

            objPessoa.IdPessoa = (string.IsNullOrEmpty(txtCodigo.Text)) ? 0 : Convert.ToInt32(txtCodigo.Text);
            objPessoa.CPF = Convert.ToInt64(txtCPF.Text);
            objPessoa.Email = txtEmail.Text;
            objPessoa.TipoPessoa = rbTipoPessoa.SelectedValue;
            objPessoa.NomePessoa = txtNome.Text;
            objPessoa.CPF = Convert.ToInt64(txtCPF.Text);
            objPessoa.CNPJ = Convert.ToInt64(txtCPF.Text);

            Telefone objTelefone = new Telefone();

            objTelefone.Numero = txtTelefone.Text;
            objTelefone.DDD = Convert.ToInt32(txtDDD.Text);
            objTelefone.IdPessoa = objPessoa.IdPessoa;
            objTelefone.IdTipoTelefone = Convert.ToInt32(rbTipoTelefone.SelectedValue);

            Endereco objEndereco = new Endereco();

            objEndereco.Logradouro = txtRua.Text;
            objEndereco.Numero = txtNumero.Text;
            objEndereco.Bairro = txtBairro.Text;
            objEndereco.CEP = txtCep.Text;
            objEndereco.Complemento = txtComplemento.Text;
            objEndereco.IdMunicipio = Convert.ToInt32(ddlMunicipio.SelectedValue);
            objEndereco.IdEstado = Convert.ToInt32(ddlEstado.SelectedValue);
            objEndereco.IdPessoa = objPessoa.IdPessoa;

            PessoaDAL dbPessoa = new PessoaDAL();

            if (objPessoa.IdPessoa == 0)
            {
                if (rbTipoPessoa.SelectedValue == "F")
                {
                    if (dbPessoa.InserirPessoaFisica(objPessoa, objEndereco, objTelefone, ref erro))
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
                    if (dbPessoa.InserirPessoaJuridica(objPessoa, objEndereco, objTelefone, ref erro))
                    {
                        Session.Add("success", "Cadastro Efetuado com Sucesso!");
                    }
                    else
                    {
                        Session.Add("danger", "Não foi possível efetuar o cadastro!" + erro);
                    }
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

                limpa();
            }
            carregaGvCliente();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpa();
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            if (!validacoes())
            {
                Session.Add("danger", "Preencha os campos obrigatórios! " + erro);
                return;
            }

            PessoaDAL dbPessoa = new PessoaDAL();

            if (!dbPessoa.ExcluirPessoa(Convert.ToInt32(txtCodigo.Text),ref erro))
            {
                Session.Add("danger", "Não foi possível Excluir o cadastro!" + erro);
                return;
            }
            else
            {
                Session.Add("success", "Cliente excluído com sucesso! ");
                limpa();
            }
            carregaGvCliente();
        }

        private bool validacoes()
        {
            if (string.IsNullOrEmpty(txtNome.Text) || string.IsNullOrEmpty(txtRua.Text) || string.IsNullOrEmpty(txtDDD.Text) || string.IsNullOrEmpty(txtTelefone.Text) || string.IsNullOrEmpty(txtNumero.Text))
            {
                return false;
            }
            return true;
        }

        private void limpa()
        {
            txtCodigo.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtCPF.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            txtDDD.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            txtRua.Text = string.Empty;
            txtCep.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtComplemento.Text = string.Empty;
            ddlEstado.SelectedIndex = 0;
            ddlMunicipio.SelectedIndex = 0;
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDDLMunicipioPorID(Convert.ToInt32(ddlEstado.SelectedValue));
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

        public void carregaGvCliente()
        {
            Pessoa objPessoa = new Pessoa();
            PessoaDAL dbCliente = new PessoaDAL();
            DataTable dtCliente = dbCliente.ObterListaPessoa(ref erro);

            gvCliente.DataSource = dtCliente;
            gvCliente.AutoGenerateSelectButton = true;
            gvCliente.DataBind();
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

            if(objPessoaPorTipo != null)bindCliente(objPessoaPorTipo);
        }

        private void bindCliente(Pessoa cliente)
        {
            txtCodigo.Text = cliente.IdPessoa.ToString();
            txtNome.Text = cliente.NomePessoa;
            rbTipoPessoa.SelectedValue = cliente.TipoPessoa;
            txtCPF.Text = cliente.CPF.ToString();
            txtEmail.Text = cliente.Email;
            txtDDD.Text = cliente.telefone.DDD.ToString();
            txtTelefone.Text = cliente.telefone.Numero;
            rbTipoTelefone.SelectedValue = cliente.telefone.TipoTelefone;
            txtRua.Text = cliente.endereco.Logradouro;
            txtNumero.Text = cliente.endereco.Numero;
            txtBairro.Text = cliente.endereco.Bairro;
            txtComplemento.Text = cliente.endereco.Complemento;
            txtCep.Text = cliente.endereco.CEP;
            ddlEstado.SelectedValue = cliente.endereco.IdEstado.ToString();
            CarregaDDLMunicipioPorID(cliente.endereco.IdEstado);
            ddlMunicipio.SelectedValue = cliente.endereco.IdMunicipio.ToString();

        }
    }
}