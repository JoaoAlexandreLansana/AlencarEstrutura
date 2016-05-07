using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication2.DAL;
using WebApplication2.DTO;

namespace AlencarEstrutura
{
    public partial class LSN001_USUARIO : System.Web.UI.Page
    {

        private string erro = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaGvUsario();
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            string Erro = string.Empty;

            if (txtSenha.Text != txtConfirmaSenha.Text) this.Session.Add("danger", "As senhas devem ser iguais!");

            if (!ValidarFormulario())
            {
                Session.Add("danger", "Preencha o formulário corretamente!");
                
            }

            Usuario dbUsuario = new Usuario();
            UsuarioDAL objusuario = new UsuarioDAL();

            dbUsuario.Login = txtLogin.Text;
            dbUsuario.Nome = txtNome.Text;
            dbUsuario.Senha = txtSenha.Text;
            dbUsuario.Email = txtEmail.Text;
            dbUsuario.Status = 1;
            dbUsuario.IdPerfil = 1;
            dbUsuario.IdEmpresa = 1;
            dbUsuario.idUsuario = (string.IsNullOrEmpty(txtCodigo.Text)) ? 0 : Convert.ToInt32(txtCodigo.Text);

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                if (!objusuario.InserirUsuario(dbUsuario, ref Erro))
                {
                    Session.Add("danger", "Erro ao Inserir resgistro " + Erro);
                    Limpar();
                }
                else
                {
                    Session.Add("success", "Cadastro efetuado com sucesso");
                    Limpar();
                    CarregaGvUsario();
                }
            }
            else
            {
                if (!objusuario.AtualizarUsuario(dbUsuario, ref Erro))
                {
                    Session.Add("danger", "Erro ao Atualizar resgistro " + Erro);
                    Limpar();
                }
                else
                {
                    Session.Add("success", "Cadastro Atualizar com sucesso");
                    Limpar();
                    CarregaGvUsario();
                }
            }
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrEmpty(txtNome.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtLogin.Text) || string.IsNullOrEmpty(txtSenha.Text))
            {
                return false;
            }
            return true;
        }

        protected void gvUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            UsuarioDAL dbUsuario = new UsuarioDAL();
            Usuario objUsuario = new Usuario();

            objUsuario = dbUsuario.ObterUsuarioPorID(Convert.ToInt32(gvUsuario.SelectedDataKey.Value), ref erro);

            if (objUsuario != null) BindUsuario(objUsuario);
        }

        private void CarregaGvUsario()
        {

            UsuarioDAL dbUsuario = new UsuarioDAL();
            Usuario objUsuario = new Usuario();
            DataTable dtUsuario = dbUsuario.ObterListadeUsuario(ref erro);


            gvUsuario.DataSource = dtUsuario;
            gvUsuario.AutoGenerateSelectButton = true;
            gvUsuario.DataBind();

        }

        public void BindUsuario(Usuario objUsuario)
        {
            txtCodigo.Text = objUsuario.idUsuario.ToString();
            txtNome.Text = objUsuario.Nome.ToString();
            txtLogin.Text = objUsuario.Login.ToString();
            txtEmail.Text = objUsuario.Email.ToString();
            txtSenha.Text = objUsuario.Senha.ToString();

        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            UsuarioDAL dbUsuario = new UsuarioDAL();

            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                dbUsuario.DeleteUsuario(Convert.ToInt32(txtCodigo.Text), ref erro);
                Session.Add("success", "Cadastro Deletado com sucesso");
                Limpar();
            }
            else
            {
                Session.Add("danger", "Selecione um Usuario ");
                Limpar();
            }
            CarregaGvUsario();
        }

        private void Limpar()
        {
            txtCodigo.Text = string.Empty;
            txtConfirmaSenha.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtLogin.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtSenha.Text = string.Empty;

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpar();
        }


    }
}