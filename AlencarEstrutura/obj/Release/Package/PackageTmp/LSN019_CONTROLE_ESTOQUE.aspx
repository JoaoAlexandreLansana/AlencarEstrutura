<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LSN019_CONTROLE_ESTOQUE.aspx.cs" Inherits="AlencarEstrutura.LSN019_CONTROLE_ESTOQUE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Panel ID="pnlTitulo" runat="server" HorizontalAlign="Center">
            <table style="width: 100%">
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lbltitulo" runat="server" Text="Controle de Estoque"></asp:Label></h2>
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
                        <asp:Button ID="btnBuscaProduto" runat="server" Text="..." CssClass="btn btn-secundary" data-target="#myModal" data-toggle="modal" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px">
                        <asp:Label ID="lblProduto" runat="server" Text="Produto"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescricao" runat="server" Columns="60" MaxLength="60"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblQuantidade" runat="server" Text="Quantidade"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtQuantidade" runat="server" Columns="6"></asp:TextBox>
                        <asp:Label ID="lblDisponivel" runat="server" Text="Disponível: "></asp:Label>
                        <asp:Label ID="lblQuantidadeDisponivel" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="width: 137px; height: 39px;"></td>
                    <td style="height: 39px">
                        <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-primary" Text="Concluir" Width="80px" OnClick="btnSalvar_Click" />
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
                        <h4 class="modal-title">Buscar Produtos</h4>
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
                                    <asp:GridView ID="gvProduto" runat="server" Width="100%" DataKeyNames="IdEstoque" AutoGenerateColumns="false" OnSelectedIndexChanged="gvProduto_SelectedIndexChanged">
                                        <Columns>
                                            <asp:BoundField HeaderText="Codigo" DataField="IdProduto" />
                                            <asp:BoundField HeaderText="Descrição" DataField="Descricao" />
                                            <asp:BoundField HeaderText="Quantidade" DataField="Quantidade" />
                                        </Columns>
                                    </asp:GridView>
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
