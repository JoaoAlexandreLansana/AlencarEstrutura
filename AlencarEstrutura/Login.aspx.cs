using System;
using System.Web;
using System.Web.Security;
using WebApplication2.DAL;
using WebApplication2.DTO;

namespace AlencarEstrutura
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            UsuarioDAL dbUsuario = new UsuarioDAL();
            Usuario objUsuario = dbUsuario.ValidaUsuario(txtLogin.Text, txtSenha.Text);
            
            if (objUsuario.idUsuario > 0)
            {
                HttpCookie cookie = new HttpCookie("Login");
                cookie.Value = txtLogin.Text;
                Response.Cookies.Add(cookie);

                HttpCookie cookieId = new HttpCookie("IdUsuario");
                cookieId.Value = objUsuario.idUsuario.ToString();
                Response.Cookies.Add(cookieId);
                FormsAuthentication.RedirectFromLoginPage(txtLogin.Text, true);
            }
            else
            {
                this.Session.Add("danger", "Usuário ou senha inválidos");
            }
        }
    }
}