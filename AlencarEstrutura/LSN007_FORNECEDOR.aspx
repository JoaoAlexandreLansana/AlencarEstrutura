<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LSN007_FORNECEDOR.aspx.cs" Inherits="AlencarEstrutura.LSN007_FORNECEDOR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div>
        <asp:Panel ID="pnlTitulo" runat="server" HorizontalAlign="Center">
            <table style="width: 100%">
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lbltitulo" runat="server" Text="Fornecedor"></asp:Label></h2>
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
                        <asp:Label ID="Label4" runat="server" Text="Código"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodigo" runat="server" Columns="8" MaxLength="8" Enabled="false"></asp:TextBox>
                        <asp:Button ID="btnBuscaEmpresa" runat="server" CssClass="btn btn-secundary" Text="..."  data-target="#myModal" data-toggle="modal"/>
                        </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNome" runat="server" Text="Nome Fantasia"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNomeFantasia" runat="server" Columns="50" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblRazao" Text="Razão Social" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRazao" runat="server" Columns="50" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCNPJ" Text="CNPJ" runat="server" required="required"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCNPJ" runat="server" Columns="15" MaxLength="15"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEmail" Text="E-mail" runat="server"></asp:Label>
                        
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Columns="50" MaxLength="30"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTelefone" runat="server" Text="Telefone"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDd" runat="server" Columns="2" MaxLength="2"></asp:TextBox> - 
                        <asp:TextBox ID="txtTelefone" runat="server" Columns="10" CssClass="" MaxLength="10"></asp:TextBox>
                        <asp:RadioButtonList ID="rbTipoTelefone" runat="server" RepeatColumns="3">
                            <asp:ListItem Selected="True" Value="1">Residencial</asp:ListItem>
                            <asp:ListItem Value="2">Comercial</asp:ListItem>
                            <asp:ListItem Value="3">Celular</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEndereco" Text="Endereço" runat="server" required="required"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndereco" runat="server" Columns="50" MaxLength="30"></asp:TextBox>
                        <asp:Label ID="Label1" runat="server" Text="No. "></asp:Label>
                        <asp:TextBox ID="txtNumero" runat="server" Columns="4"></asp:TextBox>
                        &nbsp;<asp:Label ID="lblComplemento" runat="server" Text="Complemento"></asp:Label>
                        <asp:TextBox ID="txtComplemento" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="CEP"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCEP" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEstado" runat="server" Text="UF" /></td>
                    <td>
                        <asp:DropDownList ID="ddlEstado" runat="server" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:Label ID="lblMunicipio" runat="server" Text="Município"></asp:Label>

                        <asp:DropDownList ID="ddlMunicipio" runat="server">
                        </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-info" OnClick="btnCancelar_Click"/>
                        <asp:Button ID="btnExcluir" runat="server" CssClass="btn btn-warning" Text="Excluir" />
                    </td>
                </tr>
            </table>
        </asp:Panel><!-- Modal -->
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
                                    <asp:GridView ID="gvFornecedor" runat="server" DataKeyNames="CODIGO" OnSelectedIndexChanged="gvFornecedor_SelectedIndexChanged">
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
