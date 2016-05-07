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
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Código"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodigo" runat="server" required="required" Columns="8" AutoPostBack="true" Enabled="False"></asp:TextBox>
                        <asp:Button ID="btnBuscaUsuario" runat="server" CssClass="btn btn-secundary" data-target="#myModal" data-toggle="modal" Text="..." />
                    </td>
                </tr>
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
                        <asp:Label ID="lblLogin" Text="Login" runat="server"></asp:Label>
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
                        <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 159px">
                        <asp:Label ID="lblConfirmaSenha" Text="Confirmar Senha" runat="server" required="required"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtConfirmaSenha" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 159px">&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="btn tbn-info" Text="Cancelar" OnClick="btnCancelar_Click" />                        
                        <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CssClass="btn btn-warning" OnClick="btnExcluir_Click"  />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <!-- Modal -->
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Busca Usuario</h4>
                </div>
                <div class="modal-body">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblBusca" runat="server" Text="Buscar"></asp:Label>
                                <asp:TextBox ID="txtBusca" runat="server"></asp:TextBox>
                                <asp:Button ID="btnBusca" runat="server" Text="Buscar" CssClass="btn btn-info" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvUsuario" runat="server" DataKeyNames="PKNI001_IDUSUARIO" OnSelectedIndexChanged="gvUsuario_SelectedIndexChanged" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField HeaderText =" Cod. Usuario" DataField ="PKNI001_IDUSUARIO" />
                                        <asp:BoundField HeaderText =" Nome "        DataField ="ATSF001_NOME" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
