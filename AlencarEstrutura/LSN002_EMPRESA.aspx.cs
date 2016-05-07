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

        private void CarregaDDLMunicipioPorID()
        {
            MunicipioDAL objEstado = new MunicipioDAL();
            List<Municipio> lstMunicipio = objEstado.ObterListaDeMunicipioPorID(Convert.ToInt32(ddlEstado.SelectedValue));

            foreach (Municipio lstSigla in lstMunicipio)
            {
                ListItem lst = new ListItem();

                lst.Value = lstSigla.IdEstado.ToString();
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
            objPessoa.NomePessoa = txtNomeFantasia.Text;
            objPessoa.RazaoSocial = txtRazao.Text;
            objPessoa.Email = txtEmail.Text;

            Telefone objTelefone = new Telefone();

            objTelefone.Numero = txtNumero.Text;
            objTelefone.DDD = Convert.ToInt32(txtDd.Text);

            Endereco objEndereco = new Endereco();

            objEndereco.Logradouro = txtEndereco.Text;
            objEndereco.Numero = txtNumero.Text;
            objEndereco.CEP = txtCEP.Text;
            objEndereco.Complemento = txtComplemento.Text;
            objEndereco.IdMunicipio = ddlMunicipio.SelectedIndex;
            objEndereco.IdEstado = ddlEstado.SelectedIndex;

        }

        private void validaFormulario()
        {
            if (string.IsNullOrEmpty(txtNomeFantasia.Text)|| string.IsNullOrEmpty(txtCNPJ.Text) || string.IsNullOrEmpty(txtEndereco.Text) || string.IsNullOrEmpty(txtNumero.Text))
            {
                Session.Add("danger", "Preencha os Campos Obrigatórios!");
            }

        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDDLMunicipioPorID();
        }

        private void BindEmpresa()
        {
            
        }
    }
}