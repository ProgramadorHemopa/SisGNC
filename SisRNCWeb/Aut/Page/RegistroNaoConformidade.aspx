<%@ Page Title="SisGNCWeb - Registro de Não Conformidades" Language="C#" MasterPageFile="~/Aut/AutAdmin.master" AutoEventWireup="true" CodeFile="RegistroNaoConformidade.aspx.cs"
    Inherits="HMP.WebInterface.SisRNCWeb.Www.Pages.RegistroNaoConformidade" EnableEventValidation="true" %>

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
                        <h5>Registrar SAC ou SAP
                        </h5>
                    </div>
                    <div class="widget-content nopadding" runat="server" id="divDadosOcorrencia" visible="true">
                        <div class="form-horizontal">

                            <div runat="server" id="trEdicao" visible="false">
                                <div class="control-group">
                                    <label class="control-label">Número Ocorrência:</label>

                                    <div class="controls">
                                        <h5>
                                            <asp:Label ID="lblOCR_NUMERO" runat="server"></asp:Label>
                                        </h5>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label">Situação Ocorrência:</label>
                                    <div class="controls">
                                        <h5>
                                            <asp:Label ID="lblSTCOCR_ID" runat="server"></asp:Label>
                                        </h5>
                                    </div>

                                </div>
                            </div>
                            <div id="trNumeroAntigo" runat="server" visible="false">
                                <div class="control-group">
                                    <label class="control-label">Número Antigo:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtOCR_NUMEROANTIGO" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="control-group" id="divRespAbertura" runat="server" visible="true">
                                <label class="control-label">Unidade Resp Abertura:</label>
                                <div class="controls">
                                    <asp:DropDownList ID="ddlUnidadeAbertura" runat="server" Enabled="false" Style="width: auto"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label">Responsável pela Abertura:</label>
                                <div class="controls">
                                    <asp:DropDownList ID="ddlFuncionario" runat="server" Style="width: auto"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label">Data Abertura:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtOCR_DATAABERTURA" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtOCR_DATAABERTURA_CalendarExtender" runat="server" Enabled="True"
                                        TargetControlID="txtOCR_DATAABERTURA" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label">Tipo:</label>
                                <div class="controls">
                                    <asp:DropDownList ID="ddlTipoOcorrencia" runat="server"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label">Data Ocorrência:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtOCR_DATAOCORRENCIA" AutoCompleteType="Disabled" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtOCR_DATAOCORRENCIA_CalendarExtender1" runat="server" Enabled="True"
                                        TargetControlID="txtOCR_DATAOCORRENCIA" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                </div>
                            </div>


                            <div class="control-group">
                                <label class="control-label">Local de Ocorrência:</label>
                                <div class="controls">
                                    <asp:DropDownList ID="ddlUnidadeOcorrencia" runat="server" Style="width: auto"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label">Motivo:</label>
                                <div class="controls">
                                    <asp:DropDownList ID="ddlMotivoOcorrencia" runat="server" Style="width: auto"></asp:DropDownList>
                                </div>
                            </div>


                            <div class="control-group">
                                <label class="control-label">Requisitos não atendidos (normas):</label>
                                <div class="controls">
                                    <div style="overflow-y: scroll; width: 90%; height: 150px">
                                        <asp:CheckBoxList ID="chkNormas" runat="server" TextAlign="Right" RepeatDirection="Vertical"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>



                            <div class="control-group" id="divDocumentos" style="width:auto" runat="server" visible="false">
                                <label class="control-label">Documentos:</label>
                                <div class="controls">
                                    <div style="overflow-y: scroll; width:90%; height:auto;">

                                        <div class="widget-content nopadding">


                                            <asp:GridView ID="grdDoc" Width="100%" runat="server" EmptyDataText="Nenhum registro encontrado" AutoGenerateColumns="False" PageSize="10"
                                                CssClass="table table-bordered table-striped table-hover" OnPageIndexChanging="grdDoc_PageIndexChanging" AllowPaging="true">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkDoc" Visible="false" runat="server"/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DOC_ID" HeaderText="ID"/>
                                                    <asp:BoundField DataField="DOC_CODIGO" HeaderText="Código" />
                                                    <asp:BoundField DataField="DOC_NOME" HeaderText="Nome Documento" />
                                                    <asp:BoundField DataField="DOC_VERSAO" HeaderText="Revisão" />
                                                    <asp:BoundField DataField="DOC_DATAELABORACAO" HeaderText="Data Elaboração/Revisão" DataFormatString="{0:d}" />
                                                    <asp:BoundField DataField="DOC_DATAATIVACAO" HeaderText="Data de Ativação" DataFormatString="{0:d}" />
                                                  <asp:BoundField DataField="PERIODO_FLUXO_DIAS" HeaderText="Período até Ativação" DataFormatString="{0:d}" />

                                                </Columns>
                                            </asp:GridView>

                                        </div>


                                    </div>
                                </div>
                            </div>




                            <div class="control-group">
                                <label class="control-label">Descrição:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtOCR_DESCRICAO" Height="100px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label">Anexo:</label>
                                <div class="controls" id="divNovoAnexo" runat="server" visible="true">
                                    <asp:FileUpload ID="fuAnexo" runat="server" />
                                </div>
                                <div id="divAnexoOcorrencia" runat="server" visible="false" class="control-group">
                                    <div class="controls">
                                        <asp:LinkButton ID="lnkAnexoOcorrencia" runat="server" OnClick="lnkAnexoOcorrencia_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label">Unid. Gerencial Responsável pela Resolução:</label>
                                <div class="controls">
                                    <asp:DropDownList ID="ddlUnidadeRespResolucao" runat="server" Style="width: auto"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="accordion" id="collapse-group">

                                <div class="accordion-group widget-box" id="divAcaoImediata" runat="server" visible="false">
                                    <div class="accordion-heading">
                                        <div class="widget-title">
                                            <a data-parent="#collapse-group" href="#collapseGAcaoImeditada" data-toggle="collapse">
                                                <span class="icon"><i class="icon-magnet"></i></span>
                                                <h5>Ação Imediata/Remoção do Sintoma</h5>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="collapse in accordion-body" id="collapseGAcaoImeditada">
                                        <div class="widget-title">
                                            <ul class="nav nav-tabs">
                                                <h3>
                                                    <div class="alert alert-success">
                                                        <li class="active"><a data-toggle="tab" href="#tab2d">Ação Imediata/Remoção do Sintoma</a></li>
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
                                                            <label class="control-label">Data Registro:</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtSNTAC_DATA" runat="server" Enabled="false"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender1_txtSNTAC_DATA" runat="server" Enabled="True"
                                                                    TargetControlID="txtSNTAC_DATA" Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <label class="control-label">
                                                                Ação:
                                                            </label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtSNTAC_DESCRICAO" runat="server" Height="100px" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="form-actions">
                                                            <asp:Button ID="btnOkAcaoImediata" runat="server" CssClass="btn btn-success" OnClick="btnOkAcaoImediata_Click"
                                                                Text="Salvar Ação Imediata/Remoção do Sintoma" />

                                                            <asp:Button ID="btnCancelarAcaoImediata" runat="server" CssClass="btn btn-success" OnClick="btnCancelarAcaoImediata_Click"
                                                                Text="Fechar" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="accordion-group widget-box" id="divAnáliseCritica" runat="server" visible="false">
                                    <div class="accordion-heading">
                                        <div class="widget-title">
                                            <a data-parent="#collapse-group" href="#collapseGAnaliseCritica" data-toggle="collapse">
                                                <span class="icon"><i class="icon-magnet"></i></span>
                                                <h5>Análise Crítica NQ</h5>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="collapse in accordion-body" id="collapseGAnaliseCritica">

                                        <div class="widget-title">
                                            <ul class="nav nav-tabs">
                                                <h3>
                                                    <div class="alert alert-success">
                                                        <li class="active"><a data-toggle="tab" href="#tab2d">Análise Crítica NQ</a></li>
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
                                                            <label class="control-label">
                                                                Resultado:
                                                            </label>
                                                            <div class="controls">
                                                                <asp:RadioButtonList ID="rblANC_SITUACAO" RepeatDirection="Horizontal" runat="server">
                                                                    <asp:ListItem Text="Aprovada" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Cancelada" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Devolver" Value="2"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <label class="control-label">Data Análise:</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtANC_DATA" runat="server"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="txtANC_DATA_CalendarExtender1" runat="server" Enabled="True"
                                                                    TargetControlID="txtANC_DATA" Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <label class="control-label">Análise:</label>
                                                            <div class="controls">
                                                                <asp:DropDownList ID="ddlTIPOANALISE" runat="server" Style="width: auto"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <label class="control-label">Responsável pelo PA:</label>
                                                            <div class="controls">
                                                                <asp:DropDownList ID="ddlRespPA" runat="server" Style="width: auto"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <label class="control-label">Justificativa:</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtANC_JUSTIVACANCELAMENTO" Height="100px" MaxLength="2000" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="form-actions">
                                                            <asp:Button ID="btnOkAnaliseCritica" runat="server" CssClass="btn btn-success" OnClick="btnOkAnaliseCritica_Click"
                                                                Text="Salvar Análise Crítica" />

                                                            <asp:Button ID="btnCancelarAnaliseCritica" runat="server" CssClass="btn btn-success" OnClick="btnCancelarAnaliseCritica_Click"
                                                                Text="Fechar" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="accordion-group widget-box" id="divCausaEfeito" runat="server" visible="false">
                                    <div class="accordion-heading">
                                        <div class="widget-title">
                                            <a data-parent="#collapse-group" href="#collapseGCausaEfeito" data-toggle="collapse">
                                                <span class="icon"><i class="icon-magnet"></i></span>
                                                <h5>Análise de Causa</h5>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="collapse in accordion-body" id="collapseGCausaEfeito">

                                        <div class="widget-title">
                                            <ul class="nav nav-tabs">
                                                <h3>
                                                    <div class="alert alert-success">
                                                        <li class="active"><a data-toggle="tab" href="#tab2d">Análise de Causa</a></li>
                                                    </div>
                                                </h3>
                                            </ul>
                                        </div>
                                        <div class="control-group">
                                            <label class="control-label">Data Registro:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtANCE_DATA" runat="server" Enabled="false"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1_txtANCE_DATA" runat="server" Enabled="True"
                                                    TargetControlID="txtANCE_DATA" Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="controls">
                                            <asp:RadioButtonList ID="rblAnaliseCausa" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblAnaliseCausa_SelectedIndexChanged" runat="server">
                                                <asp:ListItem Text="Diagrama de Ishikawa" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Outras Ferramentas da Qualidade" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanelCausaEfeito" runat="server">
                                            <ContentTemplate>
                                                <div class="widget-content tab-content" runat="server" id="divDiagramaIshikawa" visible="false">
                                                    <div id="tab2d" class="tab-pane active">
                                                        <br />
                                                        <div class="widget-content nopadding">
                                                            <div class="form-horizontal">
                                                                O Diagrama de Ishikawa, também conhecido como Diagrama de Causa e Efeito, Diagrama Espinha-de-peixe ou Diagrama 6M, é uma ferramenta gráfica
                                                                utilizada pela Administração para o gerenciamento e o Controle da Qualidade (CQ) em processos diversos de manipulação das fórmulas.
                                                                <br />
                                                                - Método: toda a causa envolvendo o método que estava sendo executado o trabalho;
                                                                <br />
                                                                - Matéria-prima: toda causa que envolve o material que estava sendo utilizado no trabalho;
                                                                <br />
                                                                - Mão-de-obra: toda causa que envolve uma atitude do colaborador (ex. procedimento inadequado, pressa, imprudência, ato inseguro, etc.);
                                                                <br />
                                                                - Máquinas: toda causa envolvendo a máquina que estava sendo operada;
                                                                <br />
                                                                - Medida: toda causa que envolve uma medida tomada anteriormente para modificar o processo, etc;
                                                                <br />
                                                                - Meio ambiente: toda causa que envolve o meio ambiente em si (poluição, calor, poeira, etc.) e o ambiente de trabalho (layout, falta de espaço, 
                                                                dimensionamento inadequado dos equipamentos, etc.);
                                                                <br />
                                                                - Gestão: Descumprimentos das normas preconizadas pelo Sistema de Gestão da Qualidade e legislações.
                                                                <br />

                                                                <div class="control-group">
                                                                    <label class="control-label"></label>
                                                                    <div class="controls">
                                                                        <asp:Image runat="server" ImageUrl="~/img/img_causaefeito.png" ID="imgCausaEfeito" />
                                                                    </div>
                                                                </div>
                                                                <div class="control-group">
                                                                    <label class="control-label"></label>
                                                                    <div class="controls">
                                                                        <asp:CheckBoxList ID="chkANCE_DIAGRAMA" RepeatDirection="Horizontal" CellPadding="6" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chkANCE_DIAGRAMA_SelectedIndexChanged">
                                                                            <asp:ListItem Text="MEDIDA" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="MÃO DE OBRA" Value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="MÉTODO" Value="3"></asp:ListItem>
                                                                            <asp:ListItem Text="MEIO AMBIENTE" Value="4"></asp:ListItem>
                                                                            <asp:ListItem Text="MÁQUINAS" Value="5"></asp:ListItem>
                                                                            <asp:ListItem Text="MATÉRIA PRIMA" Value="6"></asp:ListItem>
                                                                            <asp:ListItem Text="GESTÃO" Value="7"></asp:ListItem>
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                                <div class="widget-box">
                                                                    <div class="widget-title">
                                                                        <ul class="nav nav-tabs">
                                                                            <li id="TituloabaMedida" visible="false" runat="server" class="active"><a data-toggle="tab" href="#ctl00_ContentPlaceHolder1_abaMedida">Medida</a></li>
                                                                            <li id="TituloabaMaoDeObra" visible="false" runat="server"><a data-toggle="tab" href="#ctl00_ContentPlaceHolder1_abaMaoDeObra">Mão de Obra</a></li>
                                                                            <li id="TituloabaMetodo" visible="false" runat="server"><a data-toggle="tab" href="#ctl00_ContentPlaceHolder1_abaMetodo">Método</a></li>
                                                                            <li id="TituloabaMeioAmbiente" visible="false" runat="server"><a data-toggle="tab" href="#ctl00_ContentPlaceHolder1_abaMeioAmbiente">Meio Ambiente</a></li>
                                                                            <li id="TituloabaMaquinas" visible="false" runat="server"><a data-toggle="tab" href="#ctl00_ContentPlaceHolder1_abaMaquinas">Máquinas</a></li>
                                                                            <li id="TituloabaMateriaPrima" visible="false" runat="server"><a data-toggle="tab" href="#ctl00_ContentPlaceHolder1_abaMateriaPrima">Matéria Prima</a></li>
                                                                            <li id="TituloabaGestao" visible="false" runat="server"><a data-toggle="tab" href="#ctl00_ContentPlaceHolder1_abaGestao">Gestão</a></li>
                                                                        </ul>
                                                                    </div>
                                                                    <div class="widget-content tab-content" runat="server" visible="false" id="divAbas">
                                                                        <div id="abaMedida" runat="server" class="tab-pane active">
                                                                            <div class="control-group">
                                                                                <label class="control-label">Descrição:</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtMedida" runat="server" Height="100px" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div id="abaMaoDeObra" runat="server" class="tab-pane">
                                                                            <div class="control-group">
                                                                                <label class="control-label">Descrição:</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtMaoDeObra" runat="server" Height="100px" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div id="abaMetodo" runat="server" class="tab-pane">
                                                                            <div class="control-group">
                                                                                <label class="control-label">Descrição:</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtMetodo" runat="server" Height="100px" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div id="abaMeioAmbiente" runat="server" class="tab-pane">
                                                                            <div class="control-group">
                                                                                <label class="control-label">Descrição:</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtMeioAmbiente" runat="server" Height="100px" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div id="abaMaquinas" runat="server" class="tab-pane">
                                                                            <div class="control-group">
                                                                                <label class="control-label">Descrição:</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtMaquinas" runat="server" Height="100px" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div id="abaMateriaPrima" runat="server" class="tab-pane">
                                                                            <div class="control-group">
                                                                                <label class="control-label">Descrição:</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtMateriaPrima" runat="server" Height="100px" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div id="abaGestao" runat="server" class="tab-pane">
                                                                            <div class="control-group">
                                                                                <label class="control-label">Descrição:</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtGestao" runat="server" Height="100px" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="form-actions">
                                                                    <asp:Button ID="btnOkCausaEfeito" runat="server" CssClass="btn btn-success" OnClick="btnOkCausaEfeito_Click"
                                                                        Text="Salvar Análise Causa" />

                                                                    <asp:Button ID="btnCancelarCausaEfeito" runat="server" CssClass="btn btn-success" OnClick="btnCancelarCausaEfeito_Click"
                                                                        Text="Fechar" />
                                                                </div>
                                                                <uc1:MessageBox ID="MessageBox2" runat="server" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <div class="widget-content tab-content" runat="server" id="divOutrosAnaliseCausa" visible="false">
                                            <div id="tab2d" class="tab-pane active">
                                                <br />
                                                <div class="widget-content nopadding">
                                                    <div class="form-horizontal">

                                                        <div class="control-group" id="divArquivoCausaEfeito" runat="server">
                                                            <label class="control-label">Arquivo:</label>
                                                            <div class="controls">
                                                                <asp:FileUpload ID="fuCausaEfeito" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div id="divAnexoCausa" runat="server" visible="false" class="control-group">
                                                            <div class="controls">
                                                                <asp:LinkButton ID="lnkAnexoCausa" runat="server" OnClick="lnkAnexoCausa_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>

                                                        <div class="form-actions">
                                                            <asp:Button ID="btnOutrosCausaEfeito" runat="server" CssClass="btn btn-success" OnClick="btnOkCausaEfeito_Click"
                                                                Text="Salvar Análise Causa" />

                                                            <asp:Button ID="btnOutrosCancelarCausaEfeito" runat="server" CssClass="btn btn-success" OnClick="btnCancelarCausaEfeito_Click"
                                                                Text="Fechar" />
                                                        </div>
                                                        <uc1:MessageBox ID="MessageBox3" runat="server" />

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="accordion-group widget-box" id="divPlanoAcao" runat="server" visible="false">
                                    <div class="accordion-heading">
                                        <div class="widget-title">
                                            <a data-parent="#collapse-group" href="#collapseGPlanoAcao" data-toggle="collapse">
                                                <span class="icon"><i class="icon-magnet"></i></span>
                                                <h5>Plano de Ação</h5>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="collapse in accordion-body" id="collapseGPlanoAcao">
                                        <div class="widget-title">
                                            <ul class="nav nav-tabs">
                                                <h3>
                                                    <div class="alert alert-success">
                                                        <li class="active"><a data-toggle="tab" href="#tab2d">Plano de Ação</a></li>
                                                    </div>
                                                </h3>
                                            </ul>
                                        </div>
                                        <div class="widget-content tab-content">
                                            <div id="tab2d" class="tab-pane active">
                                                <br />
                                                <div class="widget-content nopadding">
                                                    <div class="form-horizontal">

                                                        <div class="widget-content nopadding">
                                                            <asp:GridView ID="grdPlanoAcao" Width="100%" runat="server" EmptyDataText="Nenhum registro encontrado" AutoGenerateColumns="False" PageSize="10"
                                                                CssClass="table table-bordered table-striped table-hover"
                                                                OnRowCommand="grdPlanoAcao_RowCommand"
                                                                OnPageIndexChanging="grdPlanoAcao_PageIndexChanging" AllowPaging="true">
                                                                <Columns>
                                                                    <asp:BoundField DataField="PLNAC_NOME" HeaderText="Nome do Plano" />
                                                                    <%--<asp:BoundField DataField="PLNAC_DATAREGISTRO" HeaderText="Data Registro" DataFormatString="{0:d}" />--%>
                                                                    <asp:BoundField DataField="STPLNAC_DESCRICAO" HeaderText="Situação" />
                                                                    <asp:ButtonField CommandName="Visualizar" ButtonType="Image" ImageUrl="~/Skin/Default/Img/Icons/ico_lista.gif"
                                                                        ItemStyle-HorizontalAlign="Center" HeaderText="Visualizar" />
                                                                </Columns>

                                                            </asp:GridView>
                                                        </div>

                                                        <div class="widget-content nopadding">
                                                            <asp:GridView ID="grdMain" Width="100%" Visible="false" runat="server" EmptyDataText="Nenhum registro encontrado" AutoGenerateColumns="False" PageSize="10"
                                                                CssClass="table table-bordered table-striped table-hover"
                                                                OnRowDataBound="grdMain_RowDataBound" OnRowCommand="grdMain_RowCommand"
                                                                OnPageIndexChanging="grdMain_PageIndexChanging" AllowPaging="true">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ACS_OQUE" HeaderText="O que" />
                                                                    <asp:BoundField DataField="RESPONSAVEL" HeaderText="Responsável" />
                                                                    <asp:BoundField DataField="PRIORIDADE" HeaderText="Prioridade" />
                                                                    <asp:BoundField DataField="SITUACAO" HeaderText="Situação" />
                                                                    <asp:BoundField DataField="ACS_DATAINICIOPREVISAO" HeaderText="Início Previsão" DataFormatString="{0:d}" />
                                                                    <asp:BoundField DataField="ACS_DATATERMINOPREVISAO" HeaderText="Término Previsão" DataFormatString="{0:d}" />
                                                                    <asp:BoundField DataField="ACS_DATAEXECUCAO" HeaderText="Data Execução" DataFormatString="{0:d}" />
                                                                    <asp:ButtonField CommandName="Visualizar" ButtonType="Image" ImageUrl="~/Skin/Default/Img/Icons/ico_lista.gif"
                                                                        ItemStyle-HorizontalAlign="Center" HeaderText="Visualizar" />
                                                                </Columns>

                                                            </asp:GridView>
                                                        </div>

                                                        <%--<div class="form-actions">
                                                        <asp:Button ID="btnGerenciarPlanoAcao" runat="server" CssClass="btn btn-success" OnClick="btnGerenciarPlanoAcao_Click"
                                                            Text="Gerenciar Plano de Açao" />
                                                    </div>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="accordion-group widget-box" id="divVerificacaoEficacia" runat="server" visible="false">
                                    <div class="accordion-heading">
                                        <div class="widget-title">
                                            <a data-parent="#collapse-group" href="#collapseGVerificacaoEficacia" data-toggle="collapse">
                                                <span class="icon"><i class="icon-magnet"></i></span>
                                                <h5>Verificação de Eficácia</h5>
                                            </a>
                                        </div>
                                    </div>
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
                                                                    <%-- Modiicado por Angelo Matos em 02032020 --%>
                                                                    <asp:RadioButtonList ID="rblVRFEFC_SITUACAO" runat="server">
                                                                        <%--<asp:ListItem Text="Aprovado" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Não Aprovado (Elaborar novo plano de ação)" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Não Aprovado" Value="3"></asp:ListItem>--%>
                                                                        <asp:ListItem Text="Eficaz" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Não Eficaz (Elaborar novo plano de ação)" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Não Eficaz (Cancelar RNC)" Value="3"></asp:ListItem>
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
                                                                    <asp:BoundField DataField="STPLNAC_DESCRICAO" HeaderText="Situação" />
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

                            </div>
                            <div class="form-actions">
                                <asp:Button ID="btnOk" runat="server" Text="Salvar" CssClass="btn btn-success" OnClick="btnOk_Click" />
                                <asp:Button ID="btnExcluir" runat="server" Text="Excluir" Visible="false" CssClass="btn btn-success" OnClick="btnExcluir_Click" />
                                <asp:Button ID="btnAcaoImediata" runat="server" Visible="false" Text="Ação Imediata/Remoção do Sintoma" CssClass="btn btn-success" OnClick="btnAcaoImediata_Click" />
                                <asp:Button ID="btnAnaliseCritica" runat="server" Visible="false" Text="Análise Crítica" CssClass="btn btn-success" OnClick="btnAnaliseCritica_Click" />
                                <asp:Button ID="btnCausaEfeito" runat="server" Visible="false" Text="Análise de Causa" CssClass="btn btn-success" OnClick="btnCausaEfeito_Click" />
                                <asp:Button ID="btnPlanoAcao" runat="server" Visible="false" Text="Elaborar Plano de Ação" CssClass="btn btn-success" OnClick="btnPlanoAcao_Click" />
                                <asp:Button ID="btnVerificacaoEficacia" runat="server" Visible="false" Text="Verificação da Eficácia" CssClass="btn btn-success" OnClick="btnVerificacaoEficacia_Click" />
                            </div>
                            <div class="control-group">
                                <div class="controls">
                                    <asp:HyperLink ID="lnkPrint" runat="server" Visible="false" CssClass="btnPrint" Target="_blank" meta:resourcekey="lnkPrint" NavigateUrl="~/Aut/Reports/LoadReportRV.aspx?ReportName=Ocorrencia.rdlc">Imprimir Ocorrência</asp:HyperLink>
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
    <asp:HiddenField ID="hidSTCOCR_ID" runat="server" />
    <asp:HiddenField ID="hidMATRICULA_RESPRESOLUCAO" runat="server" />
    <asp:HiddenField ID="hidMATRICULA_NQ" runat="server" />
    <asp:HiddenField ID="hidANXOCR_ID" runat="server" />
    <asp:HiddenField ID="hidANCE_ID" runat="server" />
    <asp:HiddenField ID="hidPLNAC_ID" runat="server" />
    <asp:HiddenField ID="hidSNTAC_ID" runat="server" />
</asp:Content>

