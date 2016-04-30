<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LSN002_EMPRESA.aspx.cs" Inherits="AlencarEstrutura.LSN002_EMPRESA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Panel ID="pnlTitulo" runat="server" HorizontalAlign="Center">
            <table style="width: 100%">
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lbltitulo" runat="server" Text="Cadastrar Empresa"></asp:Label></h2>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div>
        <asp:Panel ID="pnlManutencao" runat="server" GroupingText="Dados">
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Label ID="lblNome" Text="Nome Fantasia" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNomeFantasia" runat="server" required="required"></asp:TextBox>
                        <asp:Button ID="btnBuscaEmpresa" runat="server" Text="..." CssClass="btn btn-secundary"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblRazao" Text="Razão Social" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRazao" runat="server" required="required"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCNPJ" Text="CNPJ" runat="server" required="required"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCNPJ" runat="server" required="required"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEmail" Text="E-mail" runat="server"></asp:Label>
                        
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Columns="50" MaxLength="30"></asp:TextBox>
                        <asp:Label ID="lblDDD" runat="server" Text="DDD"></asp:Label>
                        <asp:TextBox ID="txtDd" runat="server" Columns="2" MaxLength="2"></asp:TextBox>
                        <asp:Label ID="lblTelefone" runat="server" Text="Telefone"></asp:Label>
                        <asp:TextBox ID="txtTelefone" runat="server" CssClass="" Columns="10" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEndereco" Text="Endereço" runat="server" required="required"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndereco" runat="server" required="required" Columns="50" MaxLength="30"></asp:TextBox>
                        <asp:Label ID="Label1" runat="server" Text="No."></asp:Label>
                        <asp:TextBox ID="txtNumero" runat="server" Columns="4"></asp:TextBox>
                        &nbsp;<asp:Label ID="lblComplemento" runat="server" Text="Complemento"></asp:Label>
                        <asp:TextBox ID="txtComplemento" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="CEP"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCEP" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEstado" runat="server" Text="UF" /></td>
                    <td>
                        <asp:DropDownList ID="ddlEstado" runat="server" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:Label ID="lblMunicipio" runat="server" Text="Município"></asp:Label>

                        <asp:DropDownList ID="ddlMunicipio" runat="server">
                        </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
