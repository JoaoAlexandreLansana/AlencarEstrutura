<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LSN008_NF_PENDENTES.aspx.cs" Inherits="AlencarEstrutura.LSN008_NF_PENDENTES" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <asp:Panel ID="pnlTitulo" runat="server" HorizontalAlign="Center">
            <table style="width: 100%">
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lbltitulo" runat="server" Text="NF e Orçamentos"></asp:Label></h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlGridOrcamento" runat="server" ScrollBars="Auto" Height="200" GroupingText="Orçamentos">
                            <asp:GridView ID="gvOrcamentos" runat="server" AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="Codigo" DataField="PKNI020_IDORCAMENTO"/>
                                    <asp:BoundField HeaderText="Descrição" DataField="ATSF020_DESCRICAO"/>
                                    <asp:BoundField HeaderText="Data Criação" DataField="ATDT020_DATA"/>
                                    <asp:BoundField HeaderText="Vencimento" DataField="ATDT020_VENCIMENTO"/>
                                    <asp:BoundField HeaderText="Valor" DataField="ATDC020_VALOR"/>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlGridNotas" runat="server" ScrollBars="Auto" Height="200" GroupingText="Notas Fiscais">
                            <asp:GridView ID="gvNotas" runat="server" AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="Codigo" DataField="FKNI021_IDORCAMENTO" />
                                    <asp:BoundField HeaderText="Vencimento" DataField="ATDT021_VENCIMENTO" />
                                    <asp:BoundField HeaderText="Emissão" DataField="ATDT021_DATA" />
                                    <asp:BoundField HeaderText="Valor" DataField="ATDC021_VALOR" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
