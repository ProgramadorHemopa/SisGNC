<%@ Page Language="C#" MasterPageFile="~/Aut/AutAdmin.master" AutoEventWireup="true"
    CodeFile="PasswordChange.aspx.cs" Inherits="HMP.WebInterface.SisRNCWeb.Www.Pages.Aut_Admin_PasswordChange"
    Title="SisGNCWeb - Alterar Senha" Culture="auto" meta:resourcekey="PageResource" UICulture="auto" %>

<%@ Register Src="../../UserControl/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelPrincipal" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row-fluid">
                    <div class="span12">
                        <div class="widget-box">
                            <div class="widget-title">
                                <span class="icon"><i class="icon-align-justify"></i></span>
                                <h5>
                                    Trocar Senha
                                </h5>
                            </div>
                            <div class="widget-content nopadding">
                                <div class="form-horizontal">
                                    <div class="control-group">
                                        <label class="control-label">
                                            <asp:Literal ID="litPasswordOld" runat="server" meta:resourcekey="litPasswordOld">Senha Atual:</asp:Literal></label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtPasswordOld" MaxLength="20" runat="server" TextMode="Password"
                                                meta:resourcekey="txtPasswordOld"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label">
                                            <asp:Literal ID="litPasswordNew1" runat="server" meta:resourcekey="litPasswordNew1">Senha Nova:</asp:Literal></label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtPasswordNew1" TextMode="Password" MaxLength="20" runat="server"
                                                meta:resourcekey="txtPasswordNew1"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label">
                                            <asp:Literal ID="litPasswordNew2" runat="server" meta:resourcekey="litPasswordNew2">Repetir Senha:</asp:Literal></label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtPasswordNew2" TextMode="Password" MaxLength="20" runat="server"
                                                CssClass="valOb" meta:resourcekey="txtPasswordNew2"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <asp:Button runat="server" ID="btnOk" CssClass="btn btn-primary" Text="Salvar"
                                            OnClientClick="return pValidation_checkSubmit()" OnClick="btnOk_Click" />
                                        <asp:Button runat="server" ID="btnVoltar" CssClass="btn btn-primary" Text="Voltar"
                                            meta:resourcekey="btnVoltar" OnClick="btnVoltar_Click" />
                                    </div>
                                    <uc1:MessageBox ID="MessageBox1" runat="server" />
                                    <p>
                                        <asp:Literal ID="TITLE" runat="server" Text="Alterar Senha" meta:resourcekey="TITLE"></asp:Literal>
                                        <asp:Literal ID="MSG001" runat="server" Text="Dados inválidos." meta:resourcekey="MSG001"></asp:Literal>
                                        <asp:Literal ID="MSG002" runat="server" Text="Senha alterada com sucesso." meta:resourcekey="MSG002"></asp:Literal>
                                        <asp:Literal ID="MSG003" runat="server" Text="Senha não alterada." meta:resourcekey="MSG003"></asp:Literal>
                                        <asp:Literal ID="MSG004" runat="server" Text="A nova senha deve conter no mínimo 4 caracteres."
                                            meta:resourcekey="MSG004"></asp:Literal>
                                        <asp:Literal ID="MSG005" runat="server" Text="Confirmação da senha incorreta." meta:resourcekey="MSG005"></asp:Literal>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
