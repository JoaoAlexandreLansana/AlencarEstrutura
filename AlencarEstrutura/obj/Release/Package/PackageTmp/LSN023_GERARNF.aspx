<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LSN023_GERARNF.aspx.cs" Inherits="AlencarEstrutura.LSN023_GERARNF" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Panel ID="pnlTitulo" runat="server" HorizontalAlign="Center">
            <table style="width: 100%">
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lbltitulo" runat="server" Text="Gerar Nota Fiscal"></asp:Label></h2>
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
                        <asp:Label ID="Label1" runat="server" Text="Número"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodigo" runat="server" Columns="8" MaxLength="10" Enabled="false"></asp:TextBox>
                        <asp:Button ID="btnNotaFiscal" runat="server" Text="..." CssClass="btn btn-secundary" data-target="#myModal" data-toggle="modal" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px">
                        <asp:Label ID="Label6" runat="server" Text="Cliente"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodCliente" runat="server" Columns="4" Enabled="False"></asp:TextBox>
                        <asp:TextBox ID="txtNomeCliente" runat="server" Columns="50" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px">
                        <asp:Label ID="lblDescricao" runat="server" Text="Descrição"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescricao" runat="server" Enabled="False"></asp:TextBox>
                        &nbsp;<asp:Label ID="Label3" runat="server" Text="Emissão"></asp:Label>
                        &nbsp;<asp:TextBox ID="txtEmissao" runat="server" Columns="10" Enabled="False" MaxLength="10"></asp:TextBox>
                        &nbsp;<asp:Label ID="Label4" runat="server" Text="Vencimento"></asp:Label>
                        <asp:TextBox ID="txtVencimento" runat="server" Columns="10" MaxLength="10"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtDataInicial_CalendarExtender" runat="server" TargetControlID="txtVencimento" DaysModeTitleFormat="dd/MM/yyyy" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px">
                        <asp:Label ID="Label2" runat="server" Text="Valor"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtValor" runat="server" Columns="10" MaxLength="10" Enabled="False"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label5" runat="server" Text="Aplicar Desconto de "></asp:Label>
                        <asp:DropDownList ID="ddlDesconto" runat="server" OnSelectedIndexChanged="ddlDesconto_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0">0%</asp:ListItem>
                            <asp:ListItem Value="5">5%</asp:ListItem>
                            <asp:ListItem Value="10">10%</asp:ListItem>
                            <asp:ListItem Value="15">15%</asp:ListItem>
                            <asp:ListItem Value="20">20%</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px">&nbsp;</td>
                    <td>
                        <asp:HiddenField ID="hfValorReal" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px; height: 39px;"></td>
                    <td style="height: 39px">
                        <asp:Button ID="btnImprimir" runat="server" CssClass="btn btn-primary" Text="Imprimir" Width="80px" OnClick="btnImprimir_Click" />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="btn tbn-info" Text="Cancelar" OnClick="btnCancelar_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlImprimir" runat="server">
            <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true" />
        </asp:Panel>

        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Buscar Orçamentos</h4>
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
                                    <asp:Panel ID="pnlBusca" runat="server" Height="200">
                                        <asp:GridView ID="gvOrcamento" runat="server" Width="100%" DataKeyNames="PKNI020_IDORCAMENTO" AutoGenerateColumns="false" OnSelectedIndexChanged="gvOrcamento_SelectedIndexChanged" OnPageIndexChanging="gvOrcamento_PageIndexChanging">
                                            <Columns>
                                                <asp:BoundField HeaderText="Codigo" DataField="PKNI020_IDORCAMENTO" />
                                                <asp:BoundField HeaderText="Descrição" DataField="ATSF020_DESCRICAO" />
                                                <asp:BoundField HeaderText="Valor" DataField="ATDC020_VALOR" />
                                            </Columns>
                                            <PagerSettings Position="Bottom" Mode="NextPreviousFirstLast"
                                                PreviousPageText="<"
                                                NextPageText=">"
                                                FirstPageText="<<"
                                                LastPageText=">>" PageButtonCount="15" />
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
