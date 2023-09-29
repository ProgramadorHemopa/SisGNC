<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Normas.aspx.cs" 
    Inherits="HMP.WebInterface.SisRNCWeb.Www.Pages.Normas" 
    MasterPageFile="~/Aut/AutAdmin.master" Title="SisGNCWeb - Cadastro de Normas" culture="auto" uiculture="auto"  %>    

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Src="~/UserControl/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanelPrincipal" runat="server">
    <ContentTemplate>


    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-align-justify"></i></span>
                        <h5>Cadastro - Normas
                        </h5>
                    </div>
                    <div class="widget-content nopadding">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label">Descrição:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtNRM_DESCRICAO" runat="server"></asp:TextBox>
                                </div>	
                            </div>

                            <div class="form-actions">
                                <asp:Button ID="btnOk" runat="server" Text="Salvar" CssClass="btn btn-success" OnClick="btnOk_Click" />
                            </div>

                            <div class="widget-content nopadding">
                                <asp:GridView ID="grdMain" Width="100%" runat="server" EmptyDataText="Nenhum registro encontrado" AutoGenerateColumns="False" PageSize="10"
                                    CssClass="table table-bordered table-striped table-hover" OnRowCommand="grdMain_RowCommand" OnPageIndexChanging="grdMain_PageIndexChanging" AllowPaging="true">
                                    <Columns>

                                        <asp:BoundField DataField="NRM_DESCRICAO" HeaderText="Tipo" />
                                        <asp:ButtonField CommandName="Alterar" ButtonType="Image" ImageUrl="~/Skin/Default/Img/Icons/relat_l.gif"      
                                           ItemStyle-HorizontalAlign="Center" HeaderText="Alterar" />                   
                                                                                                                               
                                        <asp:ButtonField CommandName="Excluir" ButtonType="Image" ImageUrl="~/Skin/Default/Img/Icons/icon-delete.gif"  
                                            ItemStyle-HorizontalAlign="Center" HeaderText="Excluir" />
                                    </Columns>

                                </asp:GridView>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
                                                                                                                                                   
		<uc1:MessageBox ID="MessageBox1" runat="server" />                                                                                                             
                                                                                                                                                                         
        <asp:HiddenField ID="hidNRM_ID" runat="server" />                                                                                                                
	                                                                                                                                                                       
                                                                                                                                                                         
  </ContentTemplate>                                                                                                                                                     
 </asp:UpdatePanel>                                                                                                                                                      
</asp:Content>                                                                                                                                                           
