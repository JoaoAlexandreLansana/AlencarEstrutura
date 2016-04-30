<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LSN006_PEDIDOCOMPRA.aspx.cs" Inherits="AlencarEstrutura.LSN006_PEDIDOCOMPRA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Panel ID="pnlTitulo" runat="server" HorizontalAlign="Center">
            <table style="width: 100%">
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lbltitulo" runat="server" Text="Pedido de Compra"></asp:Label></h2>
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
                        <asp:Button ID="btnBuscaPedidoCompra" runat="server" Text="..." CssClass="btn btn-secundary" data-target="#myModal" data-toggle="modal" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px">
                        <asp:Label ID="lblProduto" runat="server" Text="Produto"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProduto" runat="server">
                            <asp:ListItem>Selecione</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lblQuantidade" runat="server" Text="Quantidade"></asp:Label>
                        <asp:TextBox ID="txtQuantidade" runat="server" Columns="8" MaxLength="8"></asp:TextBox>
                        <asp:Label ID="lblFornecedor" runat="server" Text="Fornecedor"></asp:Label>
                        <asp:DropDownList ID="ddlFornecedor" runat="server">
                            <asp:ListItem>Selecione</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lblValor" runat="server" Text="Valor Previsto"></asp:Label>
                        <asp:TextBox ID="txtValorPrevisto" runat="server" Columns="6"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px; height: 28px;">
                        <asp:Label ID="lblObservacao" runat="server" required="required" Text="Observação"></asp:Label>
                    </td>
                    <td style="margin-left: 40px; height: 28px;">
                        <asp:TextBox ID="txtObservacao" runat="server" Columns="60"></asp:TextBox>
                        <asp:Button ID="btnAdicionar" runat="server" CssClass="btn btn-info" OnClick="btnAdicionar_Click" Text="Adicionar" />
                        <asp:Button ID="btnExcluir" runat="server" CssClass="btn btn-warning" OnClick="btnExcluir_Click" Text="Remover" Width="80px" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px">&nbsp;</td>
                    <td>
                        <asp:GridView ID="gvProdutos" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="PKNI018_IDPRODUTO_PEDIDO" OnSelectedIndexChanged="gvProdutos_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField HeaderText="Cod. Produto" DataField="FKNI018_IDPRODUTO" />
                                <asp:BoundField HeaderText="Descrição" DataField="ATSF003_DESCRICAO" />
                                <asp:BoundField HeaderText="Quantidade" DataField="ATDC018_QUANTIDADE" />
                                <asp:BoundField HeaderText="Valor Previsto" DataField="ATDC018_VALORPREVISTO" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px; height: 39px;"></td>
                    <td style="height: 39px">
                        <asp:Button ID="btnConcluir" runat="server" CssClass="btn btn-primary" Text="Concluir" Width="80px" OnClick="btnConcluir_Click" />
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
                                    <asp:GridView ID="gvPedidos" runat="server" Width="100%" DataKeyNames="PKNI006_IDPEDIDOCOMPRA" AutoGenerateColumns="false" OnSelectedIndexChanged="gvPedidos_SelectedIndexChanged">
                                        <Columns>
                                            <asp:BoundField HeaderText="Codigo Pedido Compra" DataField="PKNI006_IDPEDIDOCOMPRA" />
                                            <asp:BoundField HeaderText="Data" DataField="ATDT006_DATAPEDIDO" />
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
