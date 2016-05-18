<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LSN012_CONSULTAR_ESTOQUE.aspx.cs" Inherits="AlencarEstrutura.LSN012_CONSULTAR_ESTOQUE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Panel ID="pnlTitulo" runat="server" HorizontalAlign="Center">
            <table style="width: 100%">
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lbltitulo" runat="server" Text="Consultar Produto"></asp:Label></h2>
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
                        <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Auto" Height="400">
                            <asp:GridView ID="gvEstoque" runat="server" Width="100%" DataKeyNames="IdEstoque" AutoGenerateColumns="false" >
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
        </asp:Panel>
    </div>
</asp:Content>
