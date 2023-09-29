<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecurityObjects.aspx.cs" 
Inherits="HMP.WebInterface.SisRNCWeb.Www.Pages.SecurityObjects" 
MasterPageFile="~/Aut/AutAdmin.master" Title="SCPJ Web - Cadastro de Páginas" culture="auto" uiculture="auto"  %>    

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register src="~/UserControl/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanelPrincipal" runat="server">
<ContentTemplate>

	<fieldset class="MainForm">




		<p>
		   <label for="<%=ddlSO_PARENT.ClientID %>"><asp:Literal ID="litSO_PARENT" runat="server" Text="Pai:" meta:resourcekey="litSO_PARENT" ></asp:Literal></label>
           <asp:DropDownList ID="ddlSO_PARENT"  runat="server" meta:resourcekey="ddlSO_PARENT"></asp:DropDownList>
		</p>
		<br />
		<br />

		<p>
		   <label for="<%=txtSO_TYPE.ClientID %>"><asp:Literal ID="litSO_TYPE" runat="server" Text="Tipo, 1, 2, ou 3:" meta:resourcekey="litSO_TYPE" ></asp:Literal></label>
           <asp:TextBox ID="txtSO_TYPE"  MaxLength="10"  runat="server" meta:resourcekey="txtSO_TYPE"></asp:TextBox>
		</p>
		<br />
		<br />

		<p>
		   <label for="<%=txtSO_DESC.ClientID %>"><asp:Literal ID="litSO_DESC" runat="server" Text="Descrição:" meta:resourcekey="litSO_DESC" ></asp:Literal></label>
           <asp:TextBox ID="txtSO_DESC"  MaxLength="60"  runat="server" meta:resourcekey="txtSO_DESC"></asp:TextBox>
		</p>
		<br />
		<br />
		<p class="btnOk">                                                                                        
			<asp:Button runat="server" ID="btnVoltar" CssClass="" Text="Voltar" OnClick="btnVoltar_Click"/>  
                                                                                                                    
			<asp:Button runat="server" ID="btnOk" CssClass="" Text="Salvar" OnClick="btnOk_Click"/>          
		</p>                                                                                                     
		                                                                                                         
		<br />                                                                                                   
		<br />                                                                                                   
                                                                                      
                                                                                                                    
    	<br />                                                                                                   
		<br />                                                                                                   
	    <p>
        <!--- GRID DE INFORMAÇÕES --->                                                                              
        <asp:GridView ID="grdMain" Width="85%" runat="server"  EmptyDataText="Nenhum registro encontrado" AutoGenerateColumns="False"  PageSize="10"                     
        CssClass="dg" OnRowCommand="grdMain_RowCommand" onpageindexchanging="grdMain_PageIndexChanging" AllowPaging="true" meta:resourcekey="grdMainResource1" >         
            <Columns>

                <asp:BoundField DataField="SO_OBJECTID" HeaderText="ID"
                    meta:resourcekey="BF_SO_OBJECTID" />
                    
                <asp:BoundField DataField="SO_PARENT" HeaderText="Pai"
                    meta:resourcekey="BF_SO_PARENT" />

                <asp:BoundField DataField="SO_TYPE" HeaderText="Tipo"
                    meta:resourcekey="BF_SO_TYPE" />

                <asp:BoundField DataField="SO_DESC" HeaderText="Descrição"
                    meta:resourcekey="BF_SO_DESC" />
                <asp:ButtonField CommandName="Alterar" ButtonType="Image" ImageUrl="~/Skin/Default/Img/Icons/relat_l.gif"      
                   ItemStyle-HorizontalAlign="Center" HeaderText="Alterar" meta:resourcekey="TF_UPDATE"/>                      
                                                                                                                               
                <asp:ButtonField CommandName="Excluir" ButtonType="Image" ImageUrl="~/Skin/Default/Img/Icons/icon-delete.gif"  
                    ItemStyle-HorizontalAlign="Center" HeaderText="Excluir" meta:resourcekey="TF_DELETE"/>                     
                                                                                                                                                                         
            </Columns>                                                                                                                                                   
            <RowStyle CssClass="dgItem" />                                                                                                                               
            <PagerStyle HorizontalAlign="Center" />                                                                                                                      
            <HeaderStyle CssClass="dgHeader" />                                                                                                                          
            <AlternatingRowStyle CssClass="dgAlternate" />                                                                                                               
        </asp:GridView> 		                                                                                                                                       
        </p>
       </fieldset>                                                                                                                                                               
		<p>                                                                                                                                                            
		<asp:Literal ID="MSG001" runat="server" meta:resourcekey="MSG001"></asp:Literal>                                                                               
		<asp:Literal ID="MSG002" runat="server" meta:resourcekey="MSG002"></asp:Literal>                                                                               
		</p>                                                                                                                                                           
		                                                                                                                                                               
		<uc1:MessageBox ID="MessageBox1" runat="server" />                                                                                                             
                                                                                                                                                                         
        <asp:HiddenField ID="hidSO_OBJECTID" runat="server" />                                                                                                                
	                                                                                                                                                                       
                                                                                                                                                                         
  </ContentTemplate>                                                                                                                                                     
 </asp:UpdatePanel>                                                                                                                                                      
</asp:Content>                                                                                                                                                           
