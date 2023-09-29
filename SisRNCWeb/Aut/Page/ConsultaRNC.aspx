<%@ Page Title="SisGNCWeb - Consultar SAC ou SAP" Language="C#" MasterPageFile="~/Aut/AutAdmin.master" AutoEventWireup="true" CodeFile="ConsultaRNC.aspx.cs" 
    Inherits="HMP.WebInterface.SisRNCWeb.Www.Pages.ConsultaRNC" %>

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
                        <h5>Consultar SAC ou SAP
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
                                            <label class="control-label">Data Abertura:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtOCR_DATAABERTURA" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtOCR_DATAABERTURA_CalendarExtender"  runat="server" Enabled="True"  
                                                    TargetControlID="txtOCR_DATAABERTURA" Format="dd/MM/yyyy" >                                 
                                                </cc1:CalendarExtender> 	
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Tipo:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlTipoOcorrencia" runat="server"></asp:DropDownList>
                                            </div>	
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Data Ocorrência:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtOCR_DATAOCORRENCIA" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtOCR_DATAOCORRENCIA_CalendarExtender1"  runat="server" Enabled="True"  
                                                    TargetControlID="txtOCR_DATAOCORRENCIA" Format="dd/MM/yyyy" >                                 
                                                </cc1:CalendarExtender> 	
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
                                            <label class="control-label">Descrição:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtOCR_DESCRICAO" runat="server"></asp:TextBox>
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

                            <div class="widget-content nopadding">
                                <asp:GridView ID="grdMain" Width="100%" runat="server" EmptyDataText="Nenhum registro encontrado" AutoGenerateColumns="False" PageSize="10"
                                    CssClass="table table-bordered table-striped table-hover" OnRowCommand="grdMain_RowCommand" OnPageIndexChanging="grdMain_PageIndexChanging" AllowPaging="true">
                                    <Columns>

                                        <asp:BoundField DataField="OCR_NUMEROS" HeaderText="Número / (Nº Antigo)" />
                                        <asp:BoundField DataField="OCR_DATAABERTURA" HeaderText="Data Abertura" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="OCR_DATAOCORRENCIA" HeaderText="Data Ocorrência" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="RESPABERTURA" HeaderText="Resp. Abertura" />
                                        <asp:BoundField DataField="UNIDADE_RESPONSAVEL" HeaderText="Unidade Responsável" />
                                        <asp:BoundField DataField="STCOCR_DESCRICAO" HeaderText="Situação Ocorrência" />

                                        <asp:ButtonField CommandName="Visualizar" ButtonType="Image" ImageUrl="~/Skin/Default/Img/Icons/ico_lista.gif"      
                                           ItemStyle-HorizontalAlign="Center" HeaderText="Abrir Ocorrência" />                   
                                                                                                                               

                                    </Columns>

                                </asp:GridView>

                            </div>
                            <div class="control-group">
                                <label class="control-label">Total de Registros:</label>
                                <div class="controls">
                                   <asp:Literal ID="litTotal" runat="server" Text="0"></asp:Literal>
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

