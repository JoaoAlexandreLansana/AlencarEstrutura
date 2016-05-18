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
    public partial class LSN002_EMPRESA : System.Web.UI.Page
    {
        private string erro = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEmpresa();
                CarregaDDLEstado();
            }
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
            validaFormulario();

            Pessoa objPessoa = new Pessoa();

            objPessoa.CNPJ = Convert.ToInt64(txtCNPJ.Text);
            objPessoa.RazaoSocial = txtRazao.Text;
            objPessoa.NomeFantasia = txtNomeFantasia.Text;
            objPessoa.Email = txtEmail.Text;
            objPessoa.IdPessoa = Convert.ToInt32(hfCodigoEmpresa.Value);

            objPessoa.telefone = new Telefone();

            objPessoa.telefone.Numero = txtNumero.Text;
            objPessoa.telefone.DDD = Convert.ToInt32(txtDd.Text);

            objPessoa.endereco = new Endereco();

            objPessoa.endereco.Logradouro = txtEndereco.Text;
            objPessoa.endereco.Numero = txtNumero.Text;
            objPessoa.endereco.CEP = txtCEP.Text;
            objPessoa.endereco.Complemento = txtComplemento.Text;
            objPessoa.endereco.IdMunicipio = ddlMunicipio.SelectedIndex;
            objPessoa.endereco.IdEstado = ddlEstado.SelectedIndex;

            EmpresaDAL dbEmpresa = new EmpresaDAL();
            PessoaDAL dbPessoa = new PessoaDAL();
            EnderecoDAL dbEndereco = new EnderecoDAL();
            TelefoneDAL dbTelefone = new TelefoneDAL();

            if (string.IsNullOrEmpty(hfCodigoEmpresa.Value))
            {
                if (!dbPessoa.InserirPessoa(objPessoa, objPessoa.endereco, objPessoa.telefone, ref erro))
                {
                    Session.Add("danger", "Não possível efetuar o cadastro! " + erro);
                }
                Session.Add("success", "Cadastro efetuado com sucesso!");
                BindEmpresa();
            }
            else
            {
                if (!dbEmpresa.AtualizarEmpresa(objPessoa, ref erro) && erro != "")
                {
                    Session.Add("danger", "Não possível efetuar o cadastro! " + erro);
                    return;
                }
                if (!dbEndereco.AtualizaEndereco(objPessoa.endereco, ref erro) && erro != "")
                {
                    Session.Add("danger", "Não possível atualizar o endereço! " + erro);
                    return;
                }
                if (!dbTelefone.AtualizarTelefone(objPessoa.telefone, ref erro) && erro != "")
                {
                    Session.Add("danger", "Não possível atualizar o telefone! " + erro);
                    return;
                }
                Session.Add("success", "Cadastro atualizado com Sucesso! ");
                BindEmpresa();
            }

        }

        private void validaFormulario()
        {
            if (string.IsNullOrEmpty(txtNomeFantasia.Text) || string.IsNullOrEmpty(txtCNPJ.Text) || string.IsNullOrEmpty(txtEndereco.Text) || string.IsNullOrEmpty(txtNumero.Text))
            {
                Session.Add("danger", "Preencha os Campos Obrigatórios!");
            }

        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDDLMunicipioPorID(Convert.ToInt32(ddlEstado.SelectedValue));
        }

        private void BindEmpresa()
        {
            Pessoa objEmpresa = new Pessoa();
            EmpresaDAL dbEmpresa = new EmpresaDAL();

            objEmpresa = dbEmpresa.CarregaEmpresa();

            if (objEmpresa != null)
            {
                hfCodigoEmpresa.Value = objEmpresa.IdPessoa.ToString();
                txtNomeFantasia.Text = objEmpresa.NomeFantasia;
                txtRazao.Text = objEmpresa.RazaoSocial;
                txtCNPJ.Text = objEmpresa.CNPJ.ToString();
                txtEmail.Text = objEmpresa.Email;
                txtDd.Text = objEmpresa.telefone.DDD.ToString();
                txtTelefone.Text = objEmpresa.telefone.Numero;
                txtEndereco.Text = objEmpresa.endereco.Logradouro;
                txtNumero.Text = objEmpresa.endereco.Numero;
                txtComplemento.Text = objEmpresa.endereco.Complemento;
                txtCEP.Text = objEmpresa.endereco.CEP;
                ddlEstado.SelectedValue = objEmpresa.endereco.IdEstado.ToString();
                CarregaDDLMunicipioPorID(objEmpresa.endereco.IdEstado);
                ddlMunicipio.SelectedValue = objEmpresa.endereco.IdMunicipio.ToString();
            }
        }
    }
}