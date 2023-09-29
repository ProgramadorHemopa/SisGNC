<%@ Page Title="Relatório Cidadão" Language="C#" MasterPageFile="~/Aut/AutAdmin.master" AutoEventWireup="true" CodeFile="ConsultaPlanoDeAcao.aspx.cs" 
    Inherits="HMP.WebInterface.SisRNCWeb.Www.Pages.ConsultaPlanoDeAcao" %>

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
                        <h5>Relatório - Plano de Ação
                        </h5>
                    </div>
                    <div class="widget-content nopadding">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label">Unid. Gerencial:</label>
                                <div class="controls">
                                    <asp:DropDownList ID="ddlUnidadeRespResolucao" runat="server"></asp:DropDownList>
                                </div>	
                            </div>

                            <div class="control-group">
                                <label class="control-label">N° Ocorrência:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtNumeroOcorrencia" runat="server"></asp:TextBox> Ex. 2015/001
                                </div>	
                            </div>
                       

                            <div class="form-actions">
                                <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="btn btn-success" OnClick="btnPesquisar_Click" />
                            </div>

                            <div class="widget-content nopadding">
                                <asp:GridView ID="grdMain" Width="100%" runat="server" EmptyDataText="Nenhum registro encontrado" AutoGenerateColumns="False" PageSize="10"
                                    CssClass="table table-bordered table-striped table-hover" OnPageIndexChanging="grdMain_PageIndexChanging" AllowPaging="true">
                                    <Columns>
                                        <asp:BoundField DataField="OCR_NUMERO" HeaderText="Número" />
                                        <asp:BoundField DataField="SITUACAO" HeaderText="Status Etapa" />
                                        <asp:BoundField DataField="acs_oque" HeaderText="O quê?" />
                                        <asp:BoundField DataField="acs_como" HeaderText="Como?" />
                                        <asp:BoundField DataField="acs_porque" HeaderText="Por quê?" />
                                        <asp:BoundField DataField="acs_quanto" HeaderText="Quanto?" />
                                        <asp:BoundField DataField="UNIDADENOME_QUEM" HeaderText="Quem?" />
                                        <asp:BoundField DataField="acs_onde" HeaderText="Onde?" />
                                        <asp:BoundField DataField="acs_dataexecucao" HeaderText="Data Execução" DataFormatString="{0:d}" />

                                    </Columns>

                                </asp:GridView>

                            </div>
                            <div class="control-group">
                                <label class="control-label">Total de Registros:</label>
                                <div class="controls">
                                   <asp:Literal ID="litTotal" runat="server" Text="0"></asp:Literal>
                                </div>
                            </div>

                            <div class="control-group">
                                <div class="controls">
                                   <asp:HyperLink ID="lnkPrint" runat="server" Visible="false" CssClass="btnPrint" Target="_blank" meta:resourcekey="lnkPrint" NavigateUrl="~/Aut/Reports/LoadReportRV.aspx?ReportName=Relatorio.rdlc">Imprimir Relatório</asp:HyperLink>   
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc1:MessageBox ID="MessageBox1" runat="server" />
</asp:Content>

