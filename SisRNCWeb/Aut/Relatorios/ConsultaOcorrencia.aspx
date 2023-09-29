<%@ Page Title="Relatório Ocorrencias" Language="C#" MasterPageFile="~/Aut/AutAdmin.master" AutoEventWireup="true" CodeFile="ConsultaOcorrencia.aspx.cs"
    Inherits="HMP.WebInterface.SisRNCWeb.Www.Pages.ConsultaOcorrencia" %>

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
                        <h5>Relatório - Ocorrências
                        </h5>
                    </div>
                    <div class="widget-content nopadding">
                        <div class="form-horizontal">

                            <table>
                                 <tr>
                                        <div class="control-group">
                                            <label class="control-label">Unid. Gerencial:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlUnidadeRespResolucao" width="900" runat="server"></asp:DropDownList>
                                            </div>
                                     </div>
                                    </tr>
                               
                                <tr>
                                   
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Situação:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlSTCOCR_ID" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Motivo:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlMTV_ID" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                
                                    <tr>
                                    <div class="control-group">
                                        <label class="control-label">Data Inicial:</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtDataInicio" runat="server"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDataInicio_CalendarExtender"  runat="server" Enabled="True"
                                                TargetControlID="txtDataInicio" Format="dd/MM/yyyy" >
                                            </cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </tr>
                                    <tr>
                                
                                    <div class="control-group">
                                        <label class="control-label">Data Final:</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtDataFim" runat="server"></asp:TextBox>
                                        </div>
                                        <cc1:CalendarExtender ID="txtDataFim_CalendarExtender" runat="server" Enabled="True"
                                            TargetControlID="txtDataFim" Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </div>
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
                                        <asp:BoundField DataField="OCR_DATAOCORRENCIA" HeaderText="Data Ocorrência" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="OCR_DATAABERTURA" HeaderText="Data Abertura" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="UNIDADE_OCORRENCIA" HeaderText="Local Ocorrência" />
                                        <asp:BoundField DataField="FUNCIONARIO_RESPONSAVEL" HeaderText="Resp. Resolução" />
                                        <asp:BoundField DataField="UNIDADE_RESPONSAVEL" HeaderText="Unidade Responsável" />
                                        <asp:BoundField DataField="STCOCR_DESCRICAO" HeaderText="Situação Ocorrência" />
                                        <asp:BoundField DataField="OCR_DESCRICAO" HeaderText="Descrição" />

                                        <asp:ButtonField CommandName="Visualizar" ButtonType="Image" ImageUrl="~/Skin/Default/Img/Icons/ico_lista.gif"
                                            ItemStyle-HorizontalAlign="Center" HeaderText="Visualizar" />

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

