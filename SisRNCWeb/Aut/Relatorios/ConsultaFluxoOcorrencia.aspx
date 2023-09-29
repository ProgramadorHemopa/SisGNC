<%@ Page Title="SisGNCWeb - Consultar SAC ou SAP" Language="C#" MasterPageFile="~/Aut/AutAdmin.master" AutoEventWireup="true" CodeFile="ConsultaFluxoOcorrencia.aspx.cs"
    Inherits="HMP.WebInterface.SisRNCWeb.Www.Pages.ConsultaFluxoOcorrencia" %>

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
                        <h5>Consultar Fluxo Ocorrências
                        </h5>
                    </div>
                    <div class="widget-content nopadding">
                        <div class="form-horizontal">

                            <table>
                                <tr>

                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Responsável pela Abertura:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlFuncionario" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Tipo:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlTipoOcorrencia" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Local de Ocorrência:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlUnidadeOcorrencia" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>

                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Motivo:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlMotivoOcorrencia" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Unid. Gerencial Responsável pela Resolução:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlUnidadeRespResolucao" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Situação:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlSTCOCR_ID" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Data Inicial Abertura:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtDataInicio" AutoComplete="off" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtDataInicio_CalendarExtender" runat="server" Enabled="True"
                                                    TargetControlID="txtDataInicio" Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                            </div>
                                        </div>
                                    </td>

                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Data Final Abertura:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtDataFim" AutoComplete="off" runat="server"></asp:TextBox>
                                            </div>
                                            <cc1:CalendarExtender ID="txtDataFim_CalendarExtender" runat="server" Enabled="True"
                                                TargetControlID="txtDataFim" Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Número Ocorrência:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtOCR_NUMERO" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                </tr>






                            </table>

                            <div class="form-actions">
                                <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="btn btn-success" OnClick="btnPesquisar_Click" />
                            </div>

                            <!-- Modificado por Angelo Matos em 21012020 -->
                            <div class="widget-content nopadding" style="overflow:auto; height: 500px">
                                <asp:GridView ID="grdMain" Width="70%" runat="server" EmptyDataText="Nenhum registro encontrado" AutoGenerateColumns="False" PageSize="15"
                                    CssClass="table table-bordered table-striped table-hover" OnRowCommand="grdMain_RowCommand" OnPageIndexChanging="grdMain_PageIndexChanging" AllowPaging="true">
                                    <Columns>

                                        <asp:BoundField DataField="OCR_NUMEROS" HeaderText="Numero Ocorr." />
                                        <asp:BoundField DataField="OCR_DATAOCORRENCIA" HeaderText="Data Ocorrência" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="LOCAL_OCORRENCIA" HeaderText="Local Ocorrência" />
                                        <asp:BoundField DataField="MTV_DESCRICAO" HeaderText="Motivo" />
                                        <asp:BoundField DataField="RESPABERTURA" HeaderText="Respons. Abertura" />
                                        <asp:BoundField DataField="OCR_DATAABERTURA" HeaderText="Data Abertura" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="UNIDADE_RESPONSAVEL" HeaderText="Unidade Responsavel Resolução" />
                                        <asp:BoundField DataField="ACAO_IMEDIATA" HeaderText="Data Execução Ação Imediata" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="ATE_IMEDIATA" HeaderText="Dias Corridos p/ Ação Imediata" />
                                        <asp:BoundField DataField="ANALISE_CRITICA" HeaderText="Data Analise Crítica" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="ANALISECAUSA_PA" HeaderText="Data Análise de Causa" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="ATE_ANALISECAUSA" HeaderText="Dias Corridos p/ Analise de Causa" />
                                        <asp:BoundField DataField="ELABORACAO_PA" HeaderText="Data p/ Elaboração PA" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="ATE_ELABORACAO_PA" HeaderText="Dias Corridos p/ Elaboração PA" />
                                        <asp:BoundField DataField="DATA_FINAL_PA_GRID" HeaderText="Execução do PA" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="VERIFICACAO" HeaderText="Data Verificação de Eficácia" DataFormatString="{0:d}" />
                                        <%--<asp:BoundField DataField="STCOCR_DESCRICAO" HeaderText="Situação Oc." />--%>
                                        

                                        
                                        
                                        
                                        <%--<asp:BoundField DataField="ATE_CRITICA" HeaderText="Dias Analise Crítica" />--%>
                                        
                                        <%--<asp:BoundField DataField="PA" HeaderText="Dias Ana. Causa/Elab PA" DataFormatString="{0:d}" />--%>                                       
                                        
                                        
                                        <%--<asp:BoundField DataField="ATE_VERIFICACAO" HeaderText="Dias Ver. Eficácia" />
                                        <asp:BoundField DataField="VERIFICACAO" HeaderText="Data Ver. Eficácia" DataFormatString="{0:d}" />--%>

                                        <asp:ButtonField CommandName="Visualizar" ButtonType="Image" ImageUrl="~/Skin/Default/Img/Icons/ico_lista.gif"
                                            ItemStyle-HorizontalAlign="Center" HeaderText="Consultar Ocorrência" />

                                        <%--<asp:BoundField DataField="OCR_NUMEROS" HeaderText="Número / (Nº Antigo)" />
                                        <asp:BoundField DataField="RESPABERTURA" HeaderText="Resp. Abertura" />
                                        <asp:BoundField DataField="UNIDADE_RESPONSAVEL" HeaderText="Unidade Resp." />
                                        <asp:BoundField DataField="STCOCR_DESCRICAO" HeaderText="Situação Oc." />
                                        <asp:BoundField DataField="OCR_DATAOCORRENCIA" HeaderText="Data Oc." DataFormatString="{0:d}" />

                                        <asp:BoundField DataField="OCR_DATAABERTURA" HeaderText="Data Aber." DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="ATE_IMEDIATA" HeaderText="Dias Ação Imediata" />
                                        <asp:BoundField DataField="ACAO_IMEDIATA" HeaderText="Data Ação Imediata" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="ATE_CRITICA" HeaderText="Dias Analise Crítica" />
                                        <asp:BoundField DataField="ANALISE_CRITICA" HeaderText="Data Analise Crítica" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="PA" HeaderText="Dias Ana. Causa/Elab PA" DataFormatString="{0:d}" />                                       
                                        <asp:BoundField DataField="ELABORACAO_PA" HeaderText="Data Ana. Causa/Elab. PA" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="DATA_FINAL_PA" HeaderText="Data Conclusão PA" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="ATE_VERIFICACAO" HeaderText="Dias Ver. Eficácia" />
                                        <asp:BoundField DataField="VERIFICACAO" HeaderText="Data Ver. Eficácia" DataFormatString="{0:d}" />

                                        <asp:ButtonField CommandName="Visualizar" ButtonType="Image" ImageUrl="~/Skin/Default/Img/Icons/ico_lista.gif"
                                            ItemStyle-HorizontalAlign="Center" HeaderText="Consultar Ocorrência" />--%>


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
                                    <asp:HyperLink ID="lnkPrint" runat="server" Visible="false" CssClass="btnPrint" Target="_blank" meta:resourcekey="lnkPrint" NavigateUrl="~/Aut/Reports/LoadReportRV.aspx?ReportName=RelatorioFluxo.rdlc">Imprimir Relatório</asp:HyperLink>
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

