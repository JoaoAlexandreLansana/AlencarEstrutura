<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LSN004_ESTOQUE.aspx.cs" Inherits="AlencarEstrutura.LSN004_ESTOQUE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Panel ID="pnlTitulo" runat="server" HorizontalAlign="Center">
            <table style="width: 100%">
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lbltitulo" runat="server" Text="Cadastrar Estoque"></asp:Label></h2>
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
                        <asp:Button ID="btnBuscaEstoque" runat="server" Text="..." CssClass="btn btn-secundary" data-target="#myModal" data-toggle="modal" OnClick="btnBuscaEstoque_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px">
                        <asp:Label ID="lblDescricao" runat="server" Text="Descrição"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescricao" runat="server" Columns="60"></asp:TextBox>
                        <asp:Label ID="lblProduto" runat="server" Text="Produto"></asp:Label>
                        <asp:DropDownList ID="ddlProduto" runat="server">
                            <asp:ListItem>Selecione</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px">
                        <asp:Label ID="lblQuantidade" runat="server" Text="Quantidade"></asp:Label>
                    </td>
                    <td style="margin-left: 40px">
                        <asp:TextBox ID="txtQuantidade" runat="server" MaxLength="8" Columns="8"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="ftbQUantidade" runat="server" FilterType="Numbers, Custom"
                            ValidChars=".," TargetControlID="txtQuantidade" />
                        <asp:Label ID="lblValidade" runat="server" Text="Validade"></asp:Label>
                        <asp:TextBox ID="txtValidade" runat="server" Columns="10" onblur="check_date(this)"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtDataInicial_CalendarExtender" runat="server" TargetControlID="txtValidade" DaysModeTitleFormat="dd/MM/yyyy" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px">
                        <asp:Label ID="lblObservacao" Text="Observação" runat="server" required="required"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtObservacao" runat="server" Columns="60"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 137px; height: 39px;"></td>
                    <td style="height: 39px">
                        <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-primary" Text="Salvar" Width="80px" OnClick="btnSalvar_Click" />
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
    <script type="text/javascript">
        function check_date(field) {
            var checkstr = "0123456789";
            var DateField = field;
            var Datevalue = "";
            var DateTemp = "";
            var seperator = "/";
            var day;
            var month;
            var year;
            var leap = 0;
            var err = 0;
            var i;
            err = 0;
            DateValue = DateField.value;
            /* Delete all chars except 0..9 */
            for (i = 0; i < DateValue.length; i++) {
                if (checkstr.indexOf(DateValue.substr(i, 1)) >= 0) {
                    DateTemp = DateTemp + DateValue.substr(i, 1);
                }
            }
            DateValue = DateTemp;
            /* Always change date to 8 digits - string*/
            /* if year is entered as 2-digit / always assume 20xx */
            if (DateValue.length == 6) {
                DateValue = DateValue.substr(0, 4) + '20' + DateValue.substr(4, 2);
            }
            if (DateValue.length != 8) {
                err = 19;
            }
            /* year is wrong if year = 0000 */
            year = DateValue.substr(4, 4);
            if (year == 0) {
                err = 20;
            }
            /* Validation of month*/
            month = DateValue.substr(2, 2);
            if ((month < 1) || (month > 12)) {
                err = 21;
            }
            /* Validation of day*/
            day = DateValue.substr(0, 2);
            if (day < 1) {
                err = 22;
            }
            /* Validation leap-year / february / day */
            if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0)) {
                leap = 1;
            }
            if ((month == 2) && (leap == 1) && (day > 29)) {
                err = 23;
            }
            if ((month == 2) && (leap != 1) && (day > 28)) {
                err = 24;
            }
            /* Validation of other months */
            if ((day > 31) && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))) {
                err = 25;
            }
            if ((day > 30) && ((month == "04") || (month == "06") || (month == "09") || (month == "11"))) {
                err = 26;
            }
            /* if 00 ist entered, no error, deleting the entry */
            if ((day == 0) && (month == 0) && (year == 00)) {
                err = 0; day = ""; month = ""; year = ""; seperator = "";
            }
            /* if no error, write the completed date to Input-Field (e.g. 13.12.2001) */
            if (err == 0) {
                DateField.value = day + seperator + month + seperator + year;
            } else {/* Error-message if err != 0 */
                alert("Data no formato incorreto!");
                DateField.select();
                DateField.focus();
            }
        }
    </script>
</asp:Content>
