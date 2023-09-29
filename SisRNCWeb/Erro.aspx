<%@ Page Language="C#" MasterPageFile="~/Aut/AutAdmin.master" AutoEventWireup="true"
    CodeFile="Erro.aspx.cs" Inherits="HMP.WebInterface.SisRNCWeb.Www.Pages.Erro"
    Title="SisGNCWeb - Erro no Sistema" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    &nbsp;<p style="margin-top: 2em">
        <br />
        <span>
            <h3>
                Ocorreu um erro:</h3>
        </span>
        <br />
        <div runat="server" id="divDetalhesErro" style="width: 95%">
       </div>
    </p>
</asp:Content>
