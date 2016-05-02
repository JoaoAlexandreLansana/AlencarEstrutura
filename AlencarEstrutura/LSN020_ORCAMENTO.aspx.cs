using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlencarEstrutura
{
    public partial class LSN020_ORCAMENTO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
            if(string.IsNullOrEmpty(txtCodCliente.Text)|| string.IsNullOrEmpty(txtNomeCliente.Text)|| string.IsNullOrEmpty(ddlProduto.SelectedValue))
            {
                return false;
            }
            return true;
        }
    }
}