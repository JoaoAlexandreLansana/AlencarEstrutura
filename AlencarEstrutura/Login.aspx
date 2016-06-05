<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AlencarEstrutura.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="respond" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
            </Scripts>
        </asp:ScriptManager>
        <div>
            <%
                // mensagem

                if (this.Session["danger"] != null)
                {
                    Response.Write("<div class=\"alert alert-danger\">");
                    Response.Write("<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>");
                    Response.Write(this.Session["danger"].ToString());
                    Response.Write("</div>");
                    this.Session.Remove("danger");
                }

                if (this.Session["success"] != null)
                {
                    Response.Write("<div class=\"alert alert-success\">");
                    Response.Write("<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>");
                    Response.Write(this.Session["success"].ToString());
                    Response.Write("</div>");
                    this.Session.Remove("success");
                }

            %>
        </div>
        <div id="img-logo">
            <table style="width: 100%">
                <tr><td> &nbsp; &nbsp;</td></tr>
                <tr>
                    <td align="center">
                        <img src="image/login_button.png" style="height: 186px; width: 190px" /></td>
                </tr>
            </table>
        </div>
        <div align="center">
            <asp:Panel ID="pblLogin" GroupingText="Bem Vindo!" runat="server" Width="50%">
                <table style="width: 50%">
                    <tr>
                        <td>
                            <%--<asp:Label ID="lblLogin" runat="server" Text="Login"></asp:Label></td>--%>
                        <td>
                            <asp:TextBox ID="txtLogin" CssClass="form-control" placeholder="Usuário" required="required" autofocus="autofocus" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <%--<asp:Label ID="lblSenha" runat="server" Text="Senha"></asp:Label>--%>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSenha" CssClass="form-control" placeholder="Senha" required="required" autofocus="autofocus" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnEntrar" CssClass="btn btn-primary" runat="server" Width="80px" Text="Entrar" OnClick="btnEntrar_Click" /></td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
    <div id="footer" class="footer" align="left">Developed by - João Alexandre</div>
</body>
</html>
