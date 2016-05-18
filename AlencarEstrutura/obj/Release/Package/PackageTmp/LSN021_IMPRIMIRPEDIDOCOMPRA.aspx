<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LSN021_IMPRIMIRPEDIDOCOMPRA.aspx.cs" Inherits="AlencarEstrutura.LSN021_IMPRIMIRPEDIDOCOMPRA" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Panel ID="pnlTitulo" runat="server" HorizontalAlign="Center">
            <table style="width: 100%">
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lbltitulo" runat="server" Text="Imprimir Pedido de Compra"></asp:Label></h2>
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
                        <asp:TextBox ID="txtCodigo" runat="server" Columns="8" MaxLength="10" Enabled="true"></asp:TextBox>
                        <asp:Button ID="btnBuscaCategoria" runat="server" Text="..." CssClass="btn btn-secundary" data-target="#myModal" data-toggle="modal" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px">
                        <asp:Label ID="lblDescricao" runat="server" Text="Data Pedido"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtData" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px; height: 39px;"></td>
                    <td style="height: 39px">
                        <asp:Button ID="btnImprimir" runat="server" CssClass="btn btn-primary" Text="Imprimir" Width="80px" OnClick="btnImprimir_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlImprimir" runat="server">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
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
