<%@ Page Title="SisGNCWeb - Registro de Não Conformidades" Language="C#" MasterPageFile="~/Aut/AutAdmin.master" AutoEventWireup="true" CodeFile="PlanoDeAcao.aspx.cs"
    Inherits="HMP.WebInterface.SisRNCWeb.Www.Pages.PlanoDeAcao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-align-justify"></i></span>
                        <h5>Plano de Ação</h5>
                    </div>
                    <div class="widget-content nopadding">
                        <div class="form-horizontal">



                            <table>
                                <tr runat="server" id="trEdicao" visible="false">
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Tipo:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlTipoOcorrencia" Enabled="false" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Número Ocorrência:</label>

                                            <div class="controls">
                                                <h5>
                                                    <asp:Label ID="lblOCR_NUMERO" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Responsável pela Abertura:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlRespAbertura" Enabled="false" runat="server" Style="width: auto"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>

                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Situação Plano de Ação:</label>
                                            <div class="controls">
                                                <h5>
                                                    <asp:Label ID="lblSTPLNAC_ID" runat="server"></asp:Label>
                                                </h5>
                                            </div>

                                        </div>
                                    </td>
                                </tr>
                                <tr>

                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Responsável pela Elaboração:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlFuncionario" Enabled="false" runat="server" Style="width: auto"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Plano de Ação:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtPLAC_NOME" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label">Data Final p/ Conclusão do PA:</label>
                                            <div class="controls">
                                                <asp:Label ID="lblDataConclusao" Enabled="false" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="control-group" id="divJustCancelamentoPA" runat="server" visible="false">
                                            <label class="control-label">Justificativa de Cancelamento:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtJUST_DESCRICAO" Height="100px" Enabled="false" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                    </td>
                                </tr>
                            </table>
                            <div class="form-actions">
                                <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="btn btn-success" OnClick="btnVoltar_Click" />
                                <asp:Button ID="btnExcluir" runat="server" Text="Excluir" Visible="false" CssClass="btn btn-success" OnClick="btnExcluir_Click" />
                                <asp:Button ID="btnNovaAcao" runat="server" Text="Adicionar Nova Etapa" Visible="false" CssClass="btn btn-success" OnClick="btnNovaAcao_Click" />
                                <asp:Button ID="btnConcluirPlano" runat="server" Text="Finalizar Elaboração do Plano de Ação" Visible="false" CssClass="btn btn-success" OnClick="btnConcluirPlano_Click" />
                                <asp:Button ID="btnVerificaEficacia" runat="server" CssClass="btn btn-success" Visible="false" OnClick="btnVerificaEficacia_Click" Text="Verificação de Eficácia" />
                                <asp:Button ID="btnCancelarPA" runat="server" CssClass="btn btn-success" Visible="false" OnClick="btnCancelarPA_Click" Text="Cancelar Plano de Ação" />

                            </div>


                            <div class="widget-content nopadding">
                                <asp:GridView ID="grdMain" Width="100%" runat="server" EmptyDataText="Nenhum registro encontrado" AutoGenerateColumns="False" PageSize="10"
                                    CssClass="table table-bordered table-striped table-hover"
                                    OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound"
                                    OnPageIndexChanging="grdMain_PageIndexChanging" AllowPaging="true">
                                    <Columns>

                                        <asp:BoundField DataField="ACS_OQUE" HeaderText="O que" />
                                        <asp:BoundField DataField="UNIDADENOME_QUEM" HeaderText="Quem (Resp. Execução)" />
                                        <asp:BoundField DataField="SITUACAO" HeaderText="Situação" Visible="false" />
                                        <asp:BoundField DataField="ACS_DATAINICIOPREVISAO" HeaderText="Início Previsão" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="ACS_DATATERMINOPREVISAO" HeaderText="Término Previsão" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="ACS_DATAEXECUCAO" HeaderText="Data Execução" DataFormatString="{0:d}" />

                                        <asp:ButtonField CommandName="Visualizar" ButtonType="Image" ImageUrl="~/Skin/Default/Img/Icons/ico_lista.gif"
                                            ItemStyle-HorizontalAlign="Center" HeaderText="Visualizar" />
                                        <asp:ButtonField CommandName="Alterar" ButtonType="Image" ImageUrl="~/Skin/Default/Img/Icons/relat_l.gif"
                                            ItemStyle-HorizontalAlign="Center" HeaderText="Alterar" Visible="false" />
                                        <asp:ButtonField CommandName="Excluir" ButtonType="Image" ImageUrl="~/Skin/Default/Img/Icons/icon-delete.gif"
                                            ItemStyle-HorizontalAlign="Center" HeaderText="Excluir" Visible="false" />
                                        <asp:ButtonField CommandName="Executar" ButtonType="Image" ImageUrl="~/Skin/Default/Img/Icons/newpost.gif"
                                            ItemStyle-HorizontalAlign="Center" HeaderText="Executar Ação" />
                                        <asp:ButtonField CommandName="Reprogramar" ButtonType="Image" ImageUrl="~/Skin/Default/Img/Icons/btn_calendar.gif"
                                            ItemStyle-HorizontalAlign="Center" HeaderText="Reprogramar Ação" />


                                    </Columns>

                                </asp:GridView>
                                <img alt="Legenda" src="../../Skin/Default/Img/legenda_acoes.png" />
                            </div>

                            <div class="accordion-group widget-box" id="divVerificacaoEficacia" runat="server" visible="false">

                                <div class="collapse in accordion-body" id="collapseGVerificacaoEficacia">
                                    <div class="widget-title">
                                        <ul class="nav nav-tabs">
                                            <h3>
                                                <div class="alert alert-success">
                                                    <li class="active"><a data-toggle="tab" href="#tab2d">Verificação de Eficácia</a></li>
                                                </div>
                                            </h3>
                                        </ul>
                                    </div>
                                    <div class="widget-content tab-content">
                                        <div id="tab2d" class="tab-pane active">
                                            <br />
                                            <div class="widget-content nopadding">
                                                <div class="form-horizontal">
                                                    <div id="divNovaVerificaoEficacia" runat="server" visible="false">
                                                        <div class="control-group">
                                                            <label class="control-label">Nome Plano:</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtPLNAC_NOME" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <label class="control-label">Data Verificação:</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtVRFEFC_DATA" runat="server"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="txtVRFEFC_DATA_CalendarExtender1" runat="server" Enabled="True"
                                                                    TargetControlID="txtVRFEFC_DATA" Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <label class="control-label">Situação:</label>
                                                            <div class="controls">
                                                                <asp:RadioButtonList ID="rblVRFEFC_SITUACAO" runat="server">
                                                                    <asp:ListItem Text="Eficaz" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Não Eficaz (Elaborar novo Plano de Ação)" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Não Eficaz" Value="3"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <label class="control-label">Observação:</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtVRFEFC_OBSERVACAO" runat="server" Height="100px" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="form-actions">
                                                            <asp:Button ID="btnOkVerificacao" runat="server" CssClass="btn btn-success" OnClick="btnOkVerificacao_Click"
                                                                Text="Salvar Verificação da Eficácia" />

                                                            <asp:Button ID="btnCancelarVerificacao" runat="server" CssClass="btn btn-success" OnClick="btnCancelarVerificacao_Click"
                                                                Text="Fechar" />
                                                        </div>
                                                    </div>

                                                    <div class="widget-content nopadding">
                                                        <asp:GridView ID="grdEficacia" Width="100%" runat="server" EmptyDataText="Nenhum registro encontrado" AutoGenerateColumns="False" PageSize="10"
                                                            CssClass="table table-bordered table-striped table-hover"
                                                            OnPageIndexChanging="grdEficacia_PageIndexChanging" AllowPaging="true">
                                                            <Columns>
                                                                <asp:BoundField DataField="PLNAC_NOME" HeaderText="Plano de Ação" />
                                                                <asp:BoundField DataField="VRFEFC_DATA" HeaderText="Data Verificação" DataFormatString="{0:d}" />
                                                                <asp:BoundField DataField="SITUACAO_PLANO" HeaderText="Situação" />
                                                                <asp:BoundField DataField="VRFEFC_OBSERVACAO" HeaderText="Observação" />
                                                            </Columns>

                                                        </asp:GridView>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>


                            <div id="divCancelarPA" runat="server" visible="false">
                                <div class="widget-title">
                                    <ul class="nav nav-tabs">
                                        <h3>
                                            <div class="alert alert-success">
                                                <li class="active"><a data-toggle="tab" href="#tab2d">Cancelar Plano de Ação</a></li>
                                            </div>
                                        </h3>
                                    </ul>
                                </div>
                                <br />
                                <div class="control-group">
                                    <div class="controls">
                                        <asp:RadioButtonList ID="rblCancelamento_Situacao"  TextAlign="Right" Height="10" runat="server">
                                            <asp:ListItem Text="Finalizar Ocorrência" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Elaborar novo Plano de Ação" Value="2"></asp:ListItem>

                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label">Data Execução:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtDataJustificativaNQ" Enabled="false" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDataJustificativaNQ_CalendarExtender1" runat="server" Enabled="True"
                                            TargetControlID="txtDataJustificativaNQ" Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label">Justificativa de Cancelamento:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtJustificativaNQ" Height="100px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    </div>
                                </div>




                                <div class="form-actions">

                                    <asp:Button ID="btnFechaCancelaPA" runat="server" CssClass="btn btn-success" OnClick="btnFechaCancelaPA_Click" Text="Fechar" />
                                    <asp:Button ID="btnOkCancelaPA" runat="server" CssClass="btn btn-success" OnClick="btnOkCancelaPA_Click" Text="Salvar" />

                                </div>

                            </div>

                            <div id="divAcoes" runat="server" visible="false">
                                <div class="widget-title">
                                    <ul class="nav nav-tabs">
                                        <h3>
                                            <div class="alert alert-success">
                                                <li class="active"><a data-toggle="tab" href="#tab2d">Etapas do Plano</a></li>
                                            </div>
                                        </h3>
                                    </ul>
                                </div>
                                <div class="widget-content tab-content">
                                    <div id="tab2d" class="tab-pane active">
                                        <br />
                                        <div class="widget-content nopadding">
                                            <div class="form-horizontal">
                                                <div class="control-group">
                                                    <label class="control-label">O quê:</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtACS_OQUE" Height="100px" TextMode="MultiLine" MaxLength="500" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="control-group">
                                                    <label class="control-label">Como:</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtACS_COMO" Height="100px" TextMode="MultiLine" MaxLength="500" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="control-group">
                                                    <label class="control-label">Onde:</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtACS_ONDE" Height="100px" TextMode="MultiLine" MaxLength="200" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>


                                                <table>
                                                    <tr>
                                                        <td>
                                                            <div class="control-group">
                                                                <label class="control-label">Quando? Início Previsão:</label>
                                                                <div class="controls">
                                                                    <asp:TextBox ID="txtACS_DATAINICIOPREVISAO" runat="server"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtACS_DATAINICIOPREVISAO_CalendarExtender" runat="server" Enabled="True"
                                                                        TargetControlID="txtACS_DATAINICIOPREVISAO" Format="dd/MM/yyyy">
                                                                    </cc1:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="control-group">
                                                                <label class="control-label">Quando? Término Previsão:</label>
                                                                <div class="controls">
                                                                    <asp:TextBox ID="txtACS_DATATERMINOPREVISAO" runat="server"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtACS_DATATERMINOPREVISAO_CalendarExtender1" runat="server" Enabled="True"
                                                                        TargetControlID="txtACS_DATATERMINOPREVISAO" Format="dd/MM/yyyy">
                                                                    </cc1:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div class="control-group">
                                                    <label class="control-label">Por quê:</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtACS_PORQUE" Height="100px" TextMode="MultiLine" MaxLength="500" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <table>
                                                    <tr>
                                                        <td>
                                                            <div class="control-group">
                                                                <label class="control-label">Quem (Resp. Execução):</label>
                                                                <div class="controls">
                                                                    <asp:DropDownList ID="ddlUNIDADES_QUEM" runat="server" Style="width: auto"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="control-group">
                                                                <label class="control-label">Quanto Custa:</label>
                                                                <div class="controls">
                                                                    <asp:TextBox ID="txtACS_QUANTO" MaxLength="45" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <div id="divReprogramacao" runat="server" visible="false">
                                                    <div class="widget-title">
                                                        <ul class="nav nav-tabs">
                                                            <h3>
                                                                <div class="alert alert-success">
                                                                    <li class="active"><a data-toggle="tab" href="#tab2d">Reprogramação Ações</a></li>
                                                                </div>
                                                            </h3>
                                                        </ul>
                                                    </div>
                                                    <br />
                                                    <div id="divNovaReprogramacao" runat="server" visible="false">
                                                        <div class="control-group">
                                                            <label class="control-label">Nova Data Início:</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtNovaDataInicio" runat="server"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="txtNovaDataInicio_CalendarExtender1" runat="server" Enabled="True"
                                                                    TargetControlID="txtNovaDataInicio" Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <label class="control-label">Nova Data Fim:</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtNovaDataFim" runat="server"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="txtNovaDataFim_CalendarExtender1" runat="server" Enabled="True"
                                                                    TargetControlID="txtNovaDataFim" Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <label class="control-label">Justificativa Reprogramação:</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtRPGAC_JUSTIFICATIVA" Height="100px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="widget-content nopadding">
                                                        <asp:GridView ID="grdReprogramacao" Width="100%" runat="server" EmptyDataText="Nenhum registro encontrado" AutoGenerateColumns="False" PageSize="10"
                                                            CssClass="table table-bordered table-striped table-hover"
                                                            OnPageIndexChanging="grdReprogramacao_PageIndexChanging" AllowPaging="true">
                                                            <Columns>
                                                                <asp:BoundField DataField="RPGAC_DATAINICIOANTERIOR" HeaderText="Data Início Anterior" DataFormatString="{0:d}" />
                                                                <asp:BoundField DataField="RPGAC_DATAFIMANTERIOR" HeaderText="Data Fim Anterior" DataFormatString="{0:d}" />
                                                                <asp:BoundField DataField="RPGAC_JUSTIFICATIVA" HeaderText="Justificativa Solicitação" />
                                                                <asp:BoundField DataField="RPGAC_OBSERVACAONQ" HeaderText="Observação NQ" />
                                                                <%--  <asp:BoundField DataField="RPGAC_JUSTIFICATIVACANCELAMENTO" HeaderText="Justificativa Cancelamento" />--%>
                                                            </Columns>

                                                        </asp:GridView>
                                                    </div>

                                                    <div id="divValidarReprogramacao" runat="server" visible="false">

                                                        <div class="control-group">
                                                            <label class="control-label">Observação NQ:</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtRPGAC_OBSERVACAONQ" Height="100px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="control-group" runat="server" id="divJustificativaCancelamentoAcao" visible="false">
                                                            <label class="control-label">Justificativa Cancelado:</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtRPGAC_JUSTIFICATIVACANCELAMENTO" Height="100px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="form-actions">
                                                            <asp:Button ID="btnOkValidacaoReprogramacao" runat="server" CssClass="btn btn-success" OnClick="btnOkValidacaoReprogramacao_Click" Text="Aprovar Reprogramação" />

                                                            <asp:Button ID="btnCancelarReprogramacao" runat="server" Visible="false" CssClass="btn btn-success" OnClick="btnCancelarReprogramacao_Click" Text="Cancelar Ação" />

                                                            <asp:Button ID="btnDevolverReprogramacao" runat="server" Visible="false" CssClass="btn btn-success" OnClick="btnDevolverReprogramacao_Click" Text="Devolver Reprogramação" />
                                                        </div>
                                                    </div>

                                                </div>

                                                <div id="divExecutarAcao" runat="server" visible="false">
                                                    <div class="widget-title">
                                                        <ul class="nav nav-tabs">
                                                            <h3>
                                                                <div class="alert alert-success">
                                                                    <li class="active"><a data-toggle="tab" href="#tab2d">Execução do Plano</a></li>
                                                                </div>
                                                            </h3>
                                                        </ul>
                                                    </div>
                                                    <br />
                                                    <div class="control-group">
                                                        <label class="control-label">Data Execução:</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtACS_DATAEXECUCAO" Enabled="false" runat="server"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txtACS_DATAEXECUCAO_CalendarExtender1" runat="server" Enabled="True"
                                                                TargetControlID="txtACS_DATAEXECUCAO" Format="dd/MM/yyyy">
                                                            </cc1:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <div class="control-group">
                                                        <label class="control-label">Descrição da Execução:</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtACS_DESCRICAOEXECUCAO" Height="100px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>


                                                    <div id="divJustificaAtraso" class="control-group" runat="server" visible="false">
                                                        <label class="control-label">Justificativa Atraso:</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtACS_JUSTIFICATIVAATRASO" Height="100px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                        </div>

                                                    </div>


                                                    <div class="control-group">
                                                        <label class="control-label">Anexo:</label>
                                                        <div class="controls">
                                                            <asp:FileUpload ID="fuANXACS_ARQUIVO" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div id="divAnexoAcao" runat="server" visible="false" class="control-group">
                                                        <div class="controls">
                                                            <asp:LinkButton ID="lnkAnexoAcao" runat="server" OnClick="lnkAnexoAcao_Click"></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="alert alert-danger" id="divAlertaAcaoAtrasada" runat="server" align="center" visible="false">
                                                        <b>Está ação encontra-se atrasada. Informe no Campo "Justificativa Atraso" o motivo.
                                                        </b>
                                                        <br />

                                                    </div>
                                                </div>







                                                <div class="form-actions">
                                                    <asp:Button ID="btnOkAcoes" runat="server" CssClass="btn btn-success" OnClick="btnOkAcoes_Click" Text="Salvar Etapa" />

                                                    <asp:Button ID="btnOkReprogramar" runat="server" CssClass="btn btn-success" Visible="false" OnClick="btnOkReprogramar_Click" Text="Solicitar Reprogramação" />

                                                    <asp:Button ID="btnOkExecucaoAcao" runat="server" CssClass="btn btn-success" Visible="false" OnClick="btnOkExecucaoAcao_Click" Text="Salvar Execução da Ação" />

                                                    <asp:Button ID="btnNovaReprogramacao" runat="server" CssClass="btn btn-success" Visible="false" OnClick="btnNovaReprogramacao_Click" Text="Nova Reprogramação" />

                                                    <asp:Button ID="btnCancelarAcoes" runat="server" CssClass="btn btn-success" OnClick="btnCancelarAcoes_Click" Text="Fechar" />
                                                </div>


                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <uc1:MessageBox ID="MessageBox1" runat="server" />
                <asp:HiddenField ID="hidOCR_ID" runat="server" />
                <asp:HiddenField ID="hidOCR_NUMEROANTIGO" runat="server" />
                <asp:HiddenField ID="hidSTCOCR_ID" runat="server" />
                <asp:HiddenField ID="hidSTPLNAC_ID" runat="server" />
                <asp:HiddenField ID="hidPLNAC_ID" runat="server" />
                <asp:HiddenField ID="hidACS_ID" runat="server" />
                <asp:HiddenField ID="hidACS_ID_MAIORATRASADO" runat="server" />
                <asp:HiddenField ID="hidACS_COUNT" runat="server" />
                <asp:HiddenField ID="hidRPGAC_ID" runat="server" />
                <asp:HiddenField ID="hidANXACS_ID" runat="server" />
                <asp:HiddenField ID="hidMATRICULA_RESPRESOLUCAO" runat="server" />
</asp:Content>

