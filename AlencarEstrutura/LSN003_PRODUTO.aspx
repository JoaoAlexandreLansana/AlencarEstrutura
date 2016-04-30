<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LSN003_PRODUTO.aspx.cs" Inherits="AlencarEstrutura.LSN003_PRODUTO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Panel ID="pnlTitulo" runat="server" HorizontalAlign="Center">
            <table style="width: 100%">
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lbltitulo" runat="server" Text="Cadastrar Produto"></asp:Label></h2>
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
                        <asp:TextBox ID="txtCodigo" runat="server" required="required" Columns="8" OnTextChanged="txtCodigo_TextChanged" AutoPostBack="true" Enabled="False"></asp:TextBox>
                        <asp:Button ID="btnBuscaProduto" runat="server" data-target="#myModal" data-toggle="modal" Text="..." CssClass="btn btn-secundary"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDescricao" runat="server" Text="Descrição"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescricao" runat="server" required="required" Columns="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCategoria" Text="Categoria" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCategoria" runat="server"></asp:DropDownList>
                        &nbsp;<asp:Label ID="Label5" runat="server" Text="Valor Unit."></asp:Label>
                        <asp:TextBox ID="txtValor" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Peso"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPeso" runat="server"></asp:TextBox>
                        <asp:Label ID="Label7" runat="server" Text="Litros"></asp:Label>
                        <asp:TextBox ID="txtLitros" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblObservacao" Text="Observação" runat="server" required="required"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtObservacao" runat="server" Columns="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" Width="80"/>
                        <asp:Button ID="btnExcluir" runat="server" CssClass="btn btn-warning" Text="Excluir" Width="80" OnClick="btnExcluir_Click"/>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Modal Header</h4>
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
                                    <asp:GridView ID="gvProduto" runat="server" DataKeyNames="IdProduto" OnSelectedIndexChanged="gvProduto_SelectedIndexChanged">
                                        <Columns>
                                            
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
    </div>
</asp:Content>
