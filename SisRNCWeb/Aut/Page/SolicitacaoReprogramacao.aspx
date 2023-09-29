<%@ Page Title="SisGNCWeb - Registro de Não Conformidades" Language="C#" MasterPageFile="~/Aut/AutAdmin.master" AutoEventWireup="true" CodeFile="SolicitacaoReprogramacao.aspx.cs" 
    Inherits="HMP.WebInterface.SisRNCWeb.Www.Pages.SolicitacaoReprogramacao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-align-justify"></i></span>
                        <h5>Solicitações de Reprogramação
                        </h5>
                    </div>
                    <div class="widget-content nopadding">
                        <div class="form-horizontal">

                            <table>
                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Número Ocorrência:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtOCR_NUMERO" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Unid. Gerencial Responsável pela Resolução:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlUnidadeRespResolucao" runat="server"></asp:DropDownList>
                                            </div>	
                                        </div>
                                    </td>
                                </tr>

                            </table>

                            <div class="form-actions">
                                <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="btn btn-success" OnClick="btnPesquisar_Click" />
                            </div>

                            <div class="widget-content nopadding">
                                <asp:GridView ID="grdMain" Width="100%" runat="server" EmptyDataText="Nenhum registro encontrado" AutoGenerateColumns="False" PageSize="10"
                                    CssClass="table table-bordered table-striped table-hover" OnRowCommand="grdMain_RowCommand" OnPageIndexChanging="grdMain_PageIndexChanging" AllowPaging="true">
                                    <Columns>

                                        <asp:BoundField DataField="OCR_NUMERO" HeaderText="Número" />
                                        <asp:BoundField DataField="OCR_DATAABERTURA" HeaderText="Data Abertura" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="OCR_DATAOCORRENCIA" HeaderText="Data Ocorrência" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="UNIDADE_OCORRENCIA" HeaderText="Local Ocorrência" />
                                        <asp:BoundField DataField="UNIDADE_RESPONSAVEL" HeaderText="Unidade Responsável" />
                                        <asp:BoundField DataField="STCOCR_DESCRICAO" HeaderText="Situação Ocorrência" />

                                        <asp:ButtonField CommandName="Visualizar" ButtonType="Image" ImageUrl="~/Skin/Default/Img/Icons/ico_lista.gif"      
                                           ItemStyle-HorizontalAlign="Center" HeaderText="Visualizar" />                   
                                                                                                                               

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
</asp:Content>

