<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WuMenuLateral.ascx.cs"
    Inherits="UserControl_WuMenuLateral" %>
<a href="#" class="visible-phone"><i class="icon icon-home"></i>Inicio</a>
<ul>
    <li class="active"><a runat="server" href="~/Aut/AutDefault.aspx"><i class="icon icon-home"></i><span class="titulo">Início</span></a></li>
    <li class=""><a runat="server" href="~/Aut/Page/RegistroNaoConformidade.aspx"><i class="icon icon-file"></i><span class="titulo">Registrar SAC ou SAP</span></a></li>
    <li class=""><a runat="server" href="~/Aut/Page/ConsultaRNC.aspx"><i class="icon icon-th-list"></i><span class="titulo">Consultar SAC ou SAP</span></a></li>
    <li class="" runat="server" visible="false" id="menuReprogramacao"><a runat="server" href="~/Aut/Page/SolicitacaoReprogramacao.aspx"><i class="icon icon-th-list"></i><span class="titulo">Reprogramação</span></a></li>
    
    
   
    <%--MENU DO NQ--%>
    <li class="submenu" runat="server" id="menuCadastroBasico" visible="false"><a href="#"><i class="icon icon-th-list"></i><span class="titulo">Cadastros Básicos</span> <span class="label">4</span></a>
        <ul>
            <li><a id="A33" runat="server" title=""
                href="~/Aut/Basic/TipoAnalise.aspx">Tipos de Análise</a>
            </li>
            <li><a id="A3" runat="server" title=""
                href="~/Aut/Basic/TipoOcorrencia.aspx">Tipos de Ocorrência</a>
            </li>
            <li><a id="A1" runat="server" title=""
                href="~/Aut/Basic/MotivoOcorrencia.aspx">Motivos da Ocorrência</a>
            </li>
            <li><a id="A2" runat="server" title=""
                href="~/Aut/Basic/Normas.aspx">Normas</a>
            </li>


        </ul>
    </li>
    <li class="submenu" runat="server" id="menuRelatorios" visible="false"><a href="#"><i class="icon icon-th-list"></i><span class="titulo">Relatórios</span> <span class="label">2</span></a>
        <ul>
              <li><a id="A6" runat="server" title=""
                href="~/Aut/Relatorios/ConsultaFluxoOcorrencia.aspx">Fluxo Ocorrências</a>
            </li>
            <li><a id="A4" runat="server" title=""
                href="~/Aut/Relatorios/ConsultaOcorrencia.aspx">Ocorrências</a>
            </li>
            <li><a id="A5" runat="server" title=""
                href="~/Aut/Relatorios/ConsultaPlanoDeAcao.aspx">Plano de Ação</a>
            </li>
       <%--     <li><a id="A6" runat="server" title=""
                href="~/Aut/Relatorios/ConsultaRelatoriosVencidos.aspx">Prazos Vencidos</a>
            </li>--%>

        </ul>
    </li>

    <li class="" id="menuCadastroUsuario" runat="server" visible="false"><a runat="server" href="~/Aut/Basic/PessoaFuncao.aspx"><i
        class="icon icon-cog"></i><span class="titulo">Cadastro Usuário</span></a></li>
    <li class="" id="Li1" runat="server" ><a runat="server" href="~/Aut/Admin/PasswordChange.aspx"><i
        class="icon icon-cog"></i><span class="titulo">Alterar Senha</span></a></li>
    <li class="" id="Li2" runat="server" ><a runat="server" target="_blank" href="~/Arquivos/Manual Sistema de Gestão de não Conformidade.pdf"><i
        class="icon icon-file"></i><span class="titulo">Manual do Usuário</span></a></li>
    <li class=""><a runat="server" href="~/Logout.aspx"><i
        class="icon icon-cog"></i><span class="titulo">Sair</span></a></li>
</ul>
