﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LSN020_ORCAMENTO.aspx.cs" Inherits="AlencarEstrutura.LSN020_ORCAMENTO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Panel ID="pnlTitulo" runat="server" HorizontalAlign="Center">
            <table style="width: 100%">
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lbltitulo" runat="server" Text="Orçamento"></asp:Label></h2>
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
                        <asp:TextBox ID="txtCodigo" runat="server" Columns="8" MaxLength="10" Enabled="false" required="required"></asp:TextBox>
                        <button type="button" class="btn btn-secundary" data-target="#myModal" data-toggle="modal">...</button>
                        <asp:HiddenField ID="hfCodigoProdutoOrcamento" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px">
                        <asp:Label ID="Label5" runat="server" Text="Descrição"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescricao" runat="server" Columns="50" MaxLength="50" required="required"></asp:TextBox>
                        <asp:TextBox ID="txtVencimento" runat="server" Columns="10" MaxLength="10" required="required" data-mask="00/00/0000"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtDataInicial_CalendarExtender" runat="server" TargetControlID="txtVencimento" DaysModeTitleFormat="dd/MM/yyyy" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" />
                        <cc1:FilteredTextBoxExtender ID="ftbVencimento" runat="server" FilterType="Numbers,Custom"
                            TargetControlID="txtVencimento" ValidChars="/" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px">
                        <asp:Label ID="Label2" runat="server" Text="Cliente"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodCliente" runat="server" Columns="8" Enabled="False" MaxLength="8"></asp:TextBox>
                        <button runat="server" class="btn btn-secundary" data-target="#myModalCliente" data-toggle="modal">...</button>
                        <asp:TextBox ID="txtNomeCliente" runat="server" Columns="50" Enabled="False" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px; height: 28px;">
                        <asp:Label ID="lblProduto" runat="server" Text="Produto"></asp:Label>
                    </td>
                    <td style="height: 28px">
                        <asp:DropDownList ID="ddlProduto" runat="server" OnSelectedIndexChanged="ddlProduto_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0">Selecione</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lblQuantidade" runat="server" Text="Quantidade"></asp:Label>
                        <asp:TextBox ID="txtQuantidade" runat="server" Columns="8" MaxLength="8"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="ftbTelefone" runat="server" FilterType="Numbers,Custom"
                            TargetControlID="txtQuantidade" ValidChars=".," />
                        <asp:Label ID="Label6" runat="server" Text="Metros²"></asp:Label>
                        <asp:TextBox ID="txtQtdeMetroQuadrado" runat="server" Columns="8" MaxLength="8"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="ftbMetro" runat="server" FilterType="Numbers,Custom"
                            TargetControlID="txtQtdeMetroQuadrado" ValidChars=".," />
                        <asp:Label ID="lblValor" runat="server" Text="Valor Unit."></asp:Label>
                        <asp:CheckBox ID="cbValorUnitario" runat="server" OnCheckedChanged="cbValorUnitario_CheckedChanged" AutoPostBack="true" Checked="true" />
                        <asp:TextBox ID="txtValorPrevisto" runat="server" Columns="6" Enabled="False"></asp:TextBox>
                        <asp:Label ID="Label3" runat="server" Text="Valor por Metro²"></asp:Label>
                        <asp:CheckBox ID="cbValorPorMetro" runat="server" OnCheckedChanged="cbValorPorMetro_CheckedChanged" AutoPostBack="true" />
                        <asp:TextBox ID="txtValorPorMetro" runat="server" Columns="6" MaxLength="6" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">
                        <asp:Button ID="btnAdicionar" runat="server" CssClass="btn btn-primary" Text="Adicionar" OnClick="btnAdicionar_Click" Width="80px" />
                        <asp:Button ID="btnLimpar" runat="server" CssClass="btn btn-info" Text="Limpar" Width="80px" OnClick="btnLimpar_Click" />
                        <asp:Button ID="btnRemover0" runat="server" CssClass="btn btn-warning" OnClick="btnRemover_Click" Text="Remover" Width="80px" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px">&nbsp;</td>
                    <td>
                        <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Auto" Height="200">
                            <asp:GridView ID="gvProdutos" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="PKNI022_IDPRODUTO_ORCAMENTO" OnSelectedIndexChanged="gvProdutos_SelectedIndexChanged">
                                <HeaderStyle BackColor="#8181F7"
                                    ForeColor="black" />
                                <Columns>
                                    <asp:BoundField HeaderText="Cod. Produto" DataField="FKNI022_IDPRODUTO" />
                                    <asp:BoundField HeaderText="Descrição" DataField="ATSF003_DESCRICAO" />
                                    <asp:BoundField HeaderText="Quantidade" DataField="ATNI022_QUANTIDADE" />
                                    <asp:BoundField HeaderText="Valor" DataField="ATDC022_VALOR" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px; height: 39px;">&nbsp;</td>
                    <td style="height: 39px" align="right">
                        <asp:Label ID="lblDescTotal" Text="TOTAL: " runat="server"></asp:Label>
                        <asp:Label ID="lblTotal" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px; height: 39px;"></td>
                    <td align="right" style="height: 39px">
                        <asp:CheckBox ID="cbAprovado" runat="server" Text="Aprovado" />
                        <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-primary" OnClick="btnSalvar_Click" Text="Salvar" Width="80px" />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-info" OnClick="btnCancelar_Click" Text="Cancelar" Width="80px" />
                        <asp:Button ID="btnExcluir" runat="server" CssClass="btn btn-warning" OnClick="btnExcluir_Click" Text="Excluir" Width="80px" />
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
                        <h4 class="modal-title">Buscar Orçamentos</h4>
                    </div>
                    <div class="modal-body">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblBusca" runat="server" Text="Buscar"></asp:Label>
                                    <asp:TextBox ID="txtBusca" runat="server" onBlur="openModal()" OnTextChanged="txtBusca_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <asp:Button ID="btnBusca" runat="server" Text="Buscar" CssClass="btn btn-info" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlModal" runat="server" ScrollBars="Auto" Height="200">
                                        <asp:GridView ID="gvOrcamento" runat="server" Width="100%" DataKeyNames="PKNI020_IDORCAMENTO" AutoGenerateColumns="false" OnSelectedIndexChanged="gvOrcamento_SelectedIndexChanged">
                                            <HeaderStyle BackColor="#8181F7"
                                                ForeColor="black" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Codigo" DataField="PKNI020_IDORCAMENTO" />
                                                <asp:BoundField HeaderText="Descrição" DataField="ATSF020_DESCRICAO" />
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
        <!-- Modal cliente -->
        <div class="modal fade" id="myModalCliente" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Buscar Clientes</h4>
                    </div>
                    <div class="modal-body">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Buscar"></asp:Label>
                                    <asp:TextBox ID="txtBuscaCliente" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnBuscaCliente" runat="server" Text="Buscar" CssClass="btn btn-info" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlModal2" runat="server" ScrollBars="Auto" Height="200">
                                        <asp:GridView ID="gvCliente" runat="server" Width="100%" DataKeyNames="CODIGO" AutoGenerateColumns="false" OnSelectedIndexChanged="gvCliente_SelectedIndexChanged">
                                            <HeaderStyle BackColor="#8181F7"
                                                ForeColor="black" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Código" DataField="CODIGO" />
                                                <asp:BoundField HeaderText="Nome" DataField="NOME" />
                                                <asp:BoundField HeaderText="Tipo Pessoa" DataField="TIPOPESSOA" />
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
