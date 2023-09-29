<%@ Page Language="C#" MasterPageFile="~/Aut/Aut.master" AutoEventWireup="true" CodeFile="AutDefault.aspx.cs"
    Inherits="HMP.WebInterface.SisRNCWeb.Www.Pages.Aut_Default" Title="SisGNCWeb - Resumo" %>


<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.18773, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <div class="alert alert-success">
        Bem-Vindo(a)
        <%=lLogin%>
        -
        <%= lData %>,
        <%=lDiaSemana %>
        <br />
        <br />
        <asp:Literal ID="litInitialText" runat="server"></asp:Literal>  
        <p>
            <asp:Literal ID="TITLE" runat="server" Text="Resumo do Usuário"></asp:Literal>
        </p>
    </div>
   
    <br />
    <br />

       <div runat="server" visible="false" id="divAtuacao">
        <div class="control-group">
            <label class="control-label">Selecione sua unidade de atuação:</label>
            <div class="controls">
                <asp:DropDownList ID="ddlUnidades" runat="server" Width="600px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-actions">
            <asp:Button ID="btnOk" runat="server" CssClass="btn btn-info" OnClick="btnOk_Click" Text="Salvar" />
        </div>
    </div>
   <br />
    
    <asp:Button ID="ghostButton" runat="server" Style="display: none" />
    <asp:HiddenField ID="hfValue" runat="server" />

    <asp:Button ID="btnAtualizarSenha" runat="server" Visible="false" OnClick="btnAtualizar_Click" Text="Atualizar Senha" />

    <asp:HiddenField runat="server" ID="hidESD_ID" />
</asp:Content>
