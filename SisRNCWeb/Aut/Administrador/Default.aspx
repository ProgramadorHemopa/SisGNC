<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" 
Inherits="HMP.WebInterface.SisRNCWeb.Www.Pages.Default" 
MasterPageFile="~/Aut/AutAdmin.master" Title="SCPJ Web - Atualizacao" culture="auto" uiculture="auto"  %>    

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register src="~/UserControl/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanelPrincipal" runat="server">
<ContentTemplate>

	                                                                                                                                                                       
          <asp:Literal ID="Literal1" runat="server" Text="Usuários Logados:"></asp:Literal>
          <asp:Literal ID="litUser" runat="server" Text=""></asp:Literal>                                                                                                                                                               
  </ContentTemplate>                                                                                                                                                     
 </asp:UpdatePanel>                                                                                                                                                      
</asp:Content>                                                                                                                                                           
