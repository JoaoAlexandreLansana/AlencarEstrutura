using System;
using System.Collections.Generic;
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
        protected void Page_Load(object sender, EventArgs e)
        {

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

            if (!objusuario.InserirUsuario(dbUsuario, ref Erro))
            {
                Session.Add("danger", "Erro ao Inserir resgistro " + Erro);
            }
            else
            {
                Session.Add("success", "Cadastro efetuado com sucesso");
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
    }
}