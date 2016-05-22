<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LSN014_CLIENTE.aspx.cs" Inherits="AlencarEstrutura.LSN014_CLIENTE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
    </script>
    <div>
        <asp:Panel ID="pnlTitulo" runat="server" HorizontalAlign="Center">
            <table style="width: 100%">
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lbltitulo" runat="server" Text="Cliente"></asp:Label></h2>
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
                        <asp:Label ID="Label13" runat="server" Text="Tipo de Pessoa"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rbTipoPessoa" runat="server" RepeatColumns="3">
                            <asp:ListItem Selected="True" Value="F">Física</asp:ListItem>
                            <asp:ListItem Value="J">Jurídica</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Nome"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNome" runat="server" MaxLength="60" Columns="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 51px">
                        <asp:Label ID="Label4" runat="server" Text="CPF/CNPJ"></asp:Label>
                    </td>
                    <td style="height: 51px">
                        <asp:TextBox ID="txtCPF" runat="server" MaxLength="14" Columns="14  "></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                            TargetControlID="txtCPF" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="E-mail"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Columns="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Telefone"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDDD" runat="server" MaxLength="3" Columns="3"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="ftbDDD" runat="server" FilterType="Numbers"
                            TargetControlID="txtDDD" />
                        <asp:TextBox ID="txtTelefone" runat="server" MaxLength="10" Columns="10"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="ftbTelefone" runat="server" FilterType="Numbers"
                            TargetControlID="txtTelefone" />
                        <asp:RadioButtonList ID="rbTipoTelefone" runat="server" RepeatColumns="3">
                            <asp:ListItem Selected="True" Value="1">Residencial</asp:ListItem>
                            <asp:ListItem Value="2">Comercial</asp:ListItem>
                            <asp:ListItem Value="3">Celular</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Endereço"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRua" runat="server" MaxLength="50" Columns="50"></asp:TextBox>
                        <asp:Label ID="Label8" runat="server" Text="Nº"></asp:Label>
                        <asp:TextBox ID="txtNumero" runat="server" MaxLength="6" Columns="6"></asp:TextBox>
                        <asp:Label ID="Label12" runat="server" Text="Complemento"></asp:Label>
                        <asp:TextBox ID="txtComplemento" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="Bairro"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBairro" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="UF"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEstado" runat="server" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:Label ID="Label10" runat="server" Text="Município"></asp:Label>
                        <asp:DropDownList ID="ddlMunicipio" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="CEP"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCep" runat="server" MaxLength="9" Columns="9"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px; height: 39px;">&nbsp;</td>
                    <td style="height: 39px" align="right">
                        <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-primary" Text="Salvar" Width="80px" OnClick="btnSalvar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-info" Text="Cancelar" Width="80px" OnClick="btnCancelar_Click" />
                        <asp:Button ID="btnExcluir" runat="server" CssClass="btn btn-warning" Text="Excluir" Width="80px" OnClick="btnExcluir_Click" />
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
                        <h4 class="modal-title">Buscar Clientes</h4>
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
                                    <asp:Panel ID="pblGrid" runat="server" Height="200" ScrollBars="Auto">
                                        <asp:GridView ID="gvCliente" runat="server" Width="100%" DataKeyNames="CODIGO" AutoGenerateColumns="false" OnSelectedIndexChanged="gvCliente_SelectedIndexChanged">
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
