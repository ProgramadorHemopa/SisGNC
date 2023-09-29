<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WuIconesInformativosTop.ascx.cs"
    Inherits="UserControl_WuIconesInformativosTop" %>
<h1>Sistema de Gestão de Não Conformidades</h1>
<h5>
    <asp:Label runat="server" ID="lblNomePerfil"></asp:Label></h5>
<div class="btn-group">
    <a id="A1" runat="server" href="~/Aut/Page/ConsultaRNC.aspx" class="btn btn-large tip-bottom" title="Busca Ocorrências">
        <i class="icon-search"></i>
    </a>
    <a runat="server" href="~/Aut/Page/SolicitacaoReprogramacao.aspx" id="lnkReprogramacao" class="btn btn-large tip-bottom" title="Reprogramações">
        <i class="icon-tag"></i>
        <span class="label label-important"><%=lOcorrenciaReprogramacao %></span>
    </a> 
    <a runat="server" href="~/Aut/Page/ConsultaRNC.aspx" id="lnkAcompanhamento" class="btn btn-large tip-bottom" title="Acompanhamento">
        <i class="icon-tasks"></i>
        <span class="label label-important"><%=lOcorrenciaAcompanhamento %></span>
    </a> 
    <a runat="server" href="~/Aut/Page/ConsultaRNC.aspx" id="lnkPendencias" class="btn btn-large tip-bottom" title="Pendências">
        <i class="icon-comment"></i>
        <span class="label label-important"><%=lOcorrenciaPendente %></span>
    </a>
<%--     <a runat="server" href="~/Aut/Areadetrabalho/Acompanhamentodefensores.aspx" class="btn btn-large tip-bottom" title="Visualização de Atendimentos"><i class="icon-file"></i><span class="label label-important"><%=lTotalAtendimento %></span></a>--%>
    <a class="btn btn-large tip-bottom" runat="server" href="../Logout.aspx" title="Sair">
        <i class="icon-user"></i>
    </a>
</div>
