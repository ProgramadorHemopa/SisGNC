<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecurityUsers.aspx.cs" 
Inherits="HMP.WebInterface.SisRNCWeb.Www.Pages.SecurityUsers" 
MasterPageFile="~/Aut/AutAdmin.master" Title="SisGNCWeb - Permissão de Acesso" culture="auto" uiculture="auto"  %>    

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register src="~/UserControl/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanelPrincipal" runat="server">
<ContentTemplate>

    <div class="row-fluid"><%--Início FieldSet--%>
        <div class="span12">
        <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Processo</h5>
            </div>
            <div class="widget-content nopadding">
                <div class="form-horizontal">

                    <div class="control-group">
                        <label class="control-label">Núcleo :</label>
                        <div class="controls">
		                <asp:DropDownList ID="ddlNUC_ID" runat="server" AutoPostBack="true" 
                            onselectedindexchanged="ddlNUC_ID_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>

                   <div class="control-group">
                        <label class="control-label">Usuário :</label>
                        <div class="controls">
                        <asp:DropDownList ID="ddlSU_ID"  runat="server" AutoPostBack="true" onselectedindexchanged="ddlSU_ID_SelectedIndexChanged" meta:resourcekey="ddlSU_ID"></asp:DropDownList>
                        </div>
                    </div>


                    <div class="widget-box"><%--Início tag Grid--%>
                        <div class="widget-content nopadding">
                                            <!--- GRID DE INFORMAÇÕES --->
                            
                           
                            <asp:GridView ID="grdObjects" Width="85%" runat="server"  
                                    EmptyDataText="Nenhum registro encontrado" AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped table-hover data-table" OnRowDataBound="grdObjects_RowDataBound"  AllowPaging="true" 
                                    meta:resourcekey="grdObjectsResource1" 
                                    onselectedindexchanged="grdObjects_SelectedIndexChanged" >         
                                <Columns>

                                    <asp:BoundField DataField="SO_OBJECTID" HeaderText="ID"
                                        meta:resourcekey="BF_SO_OBJECTID" />

                                    <asp:BoundField DataField="SO_DESC" HeaderText="Descrição"
                                        meta:resourcekey="BF_SO_DESC" />
                    
                                    <asp:BoundField DataField="SUD_PERMISSION" HeaderText="Permissão" Visible="false"
                                        meta:resourcekey="BF_SUD_PERMISSION" />                    
                
                                    <asp:TemplateField HeaderText="Permissão" ItemStyle-HorizontalAlign="Center" 
                                        meta:resourcekey="TF_SUD_PERMISSION">
                                        <ItemTemplate>
                                             <asp:CheckBox ID="chkPermissao" runat="server" Checked="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>        
                                                                                                                                                                                         
                                </Columns>                                                                                                                                                   
                                                                                                             
                            </asp:GridView> 		                                                                                                                                       
      		                                                                                                                                       
        
                        </div>
                    </div><%--Fim tag Grid--%> 

                                    <div class="form-actions">
			                            <asp:Button runat="server" ID="btnVoltar" CssClass="btn btn-primary" Text="Voltar" OnClick="btnVoltar_Click"/>  
                                                                                                                    
			                            <asp:Button runat="server" ID="btnOk" CssClass="btn btn-primary" Text="Salvar" OnClick="btnOk_Click"/> 
                                    </div>
                </div>
            </div>
        </div>
        </div>
    </div><%--FimFieldSet --%>
                                                                                                                                                        
		                                                                                                                                                               
		<uc1:MessageBox ID="MessageBox1" runat="server" />                                                                                                             
                                                                                                                                                                         
        <asp:HiddenField ID="hidSU_ID" runat="server" />                                                                                                                
	                                                                                                                                                                       
                                                                                                                                                                         
  </ContentTemplate>                                                                                                                                                     
 </asp:UpdatePanel>                                                                                                                                                      
</asp:Content>                                                                                                                                                           
