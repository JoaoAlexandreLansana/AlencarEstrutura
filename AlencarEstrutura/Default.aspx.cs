using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlencarEstrutura
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["login"];

            lblboasVindas.Text = "Bem vindo " + cookie.Value + ", hoje temos 10 Orçamentos pendentes e 8 Notas Fiscais à vencer.";

        }
    }
}