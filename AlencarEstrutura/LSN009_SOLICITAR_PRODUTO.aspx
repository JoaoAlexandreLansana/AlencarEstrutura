<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LSN009_SOLICITAR_PRODUTO.aspx.cs" Inherits="AlencarEstrutura.LSN009_SOLICITAR_PRODUTO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Panel ID="pnlTitulo" runat="server" HorizontalAlign="Center">
            <table style="width: 100%">
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lbltitulo" runat="server" Text="Solicitar Produto"></asp:Label></h2>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div>
        <asp:Panel ID="pnlManutencao" runat="server" GroupingText="Dados">
            <table style="width: 100%">
                <tr>
                    <td style="width: 137px">
                        <asp:Label ID="Label1" runat="server" Text="Código"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodigo" runat="server" Columns="8" MaxLength="10" Enabled="false"></asp:TextBox>
                        <asp:Button ID="btnBuscaEstoque" runat="server" Text="..." CssClass="btn btn-secundary" data-target="#myModal" data-toggle="modal" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px">
                        <asp:Label ID="lblDescricao" runat="server" Text="Descrição"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescricao" runat="server" Columns="60" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px">
                        <asp:Label ID="lblQuantidade" runat="server" Text="Quantidade"></asp:Label>
                    </td>
                    <td style="margin-left: 40px">
                        <asp:TextBox ID="txtQuantidade" runat="server" MaxLength="8" Columns="8"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 137px; height: 39px;"></td>
                    <td style="height: 39px">
                        <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-primary" Text="Salvar" Width="80px" OnClick="btnSalvar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-info" Text="Cancelar" Width="80px" OnClick="btnCancelar_Click" />
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
                        <h4 class="modal-title">Buscar Estoque</h4>
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
                                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Auto" Height="200">
                                        <asp:GridView ID="gvEstoque" runat="server" Width="100%" DataKeyNames="IdEstoque" OnSelectedIndexChanged="gvEstoque_SelectedIndexChanged" AutoGenerateColumns="false">
                                            <HeaderStyle BackColor="#8181F7"
                                                ForeColor="black" />
                                            <Columns>
                                                <asp:BoundField DataField="IdEstoque" HeaderText="Codigo" />
                                                <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
