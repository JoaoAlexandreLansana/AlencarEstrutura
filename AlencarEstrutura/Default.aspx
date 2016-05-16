<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AlencarEstrutura._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Alencar Estruturas</h1>
        <p class="lead"><asp:Label ID="lblboasVindas" Text="" runat="server" /></p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Detalhes &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Orçamentos</h2>
            <p>
                Consulte seus Orçamentos por status, por data ou todos.</p>
            <p>
                Cadastre novos orçamentos.</p>
            <p>
                Altere orçamentos criados</p>
            <p>
                <asp:Button ID="btnOrcamento" runat="server" Text="Acessar >>" CssClass="btn btn-default" OnClick="btnOrcamento_Click"/>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Notas Fiscais</h2>
            <p>
                Visualize as Notas Fiscais por status, por data, por numero ou todas elas de uma vez.</p>
            <p>
                Cadastre Notas Fiscais novas ou exclua as antigas.</p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Acessar &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Controle de Estoque</h2>
            <p>
                Controle o seu estoque por Produto, Quantidade, Valor.</p>
            <p>
                Cadastre novos produtos.</p>
            <p>
                Efetue pedidos compras.</p>
            <p>
                <asp:Button ID="btnControleEstoque" runat="server" Text="Acessar >>" CssClass="btn btn-default" OnClick="btnControleEstoque_Click"/>
            </p>
        </div>
    </div>

</asp:Content>
