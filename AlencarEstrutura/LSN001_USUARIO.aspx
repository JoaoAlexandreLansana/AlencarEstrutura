<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LSN001_USUARIO.aspx.cs" Inherits="AlencarEstrutura.LSN001_USUARIO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div>
        <asp:Panel ID="pnlTitulo" runat="server" HorizontalAlign="Center">
            <table style="width: 100%">
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lbltitulo" runat="server" Text="Cadastrar Usuários"></asp:Label></h2>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div>
        <asp:Panel ID="pnlManutencao" runat="server" GroupingText="Dados">
            <table style="width: 100%">
                <tr>
                    <td style="width: 159px">
                        <asp:Label ID="lblNome" Text="Nome" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNome" runat="server" required="required"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 159px">
                        <asp:Label ID="lblLogin" Text="Login" runat="server" ></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLogin" runat="server" required="required"></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 159px">
                        <asp:Label ID="lblEmail" Text="E-mail" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 159px">
                        <asp:Label ID="lblSenha" Text="Senha" runat="server" required="required"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSenha" runat="server" required="required" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 159px">
                        <asp:Label ID="lblConfirmaSenha" Text="Confirmar Senha" runat="server" required="required"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtConfirmaSenha" runat="server" required="required" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 159px">&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
