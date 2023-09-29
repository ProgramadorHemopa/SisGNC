<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" 
    Inherits="HMP.WebInterface.SisRNCWeb.Www.Pages.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/UserControl/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SisGNCWeb - Login do Sistema</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <link href="css/bootstrap.min.css" webstripperwas="./css/bootstrap.min.css" rel="stylesheet">
    <link href="css/bootstrap-responsive.min.css" webstripperwas="./css/bootstrap-responsive.min.css"
        rel="stylesheet">
    <link href="LoginEstilo/fonts.googleapis.com/css.7.css" webstripperwas="http://fonts.googleapis.com/css?family=Open+Sans:400italic,600italic,400,600"
        rel="stylesheet">
    <link href="LoginEstilo/font-awesome.css" webstripperwas="./css/font-awesome.css"
        rel="stylesheet">
    <link href="LoginEstilo/adminia.css" webstripperwas="./css/adminia.css" rel="stylesheet">
    <link href="LoginEstilo/adminia-responsive.css" webstripperwas="./css/adminia-responsive.css"
        rel="stylesheet">
    <link href="LoginEstilo/pages/login.css" webstripperwas="./css/pages/login.css" rel="stylesheet">
    <script src="LoginEstilo/js/jquery-1.7.2.min.js" webstripperwas="./js/jquery-1.7.2.min.js"></script>
    <script src="LoginEstilo/js/bootstrap.js" webstripperwas="./js/bootstrap.js"></script>
    <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="LoginEstilo/html5shim.googlecode.com/svn/trunk/html5.js"  webstripperwas="http://html5shim.googlecode.com/svn/trunk/html5.js" ></script>
    <![endif]-->
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="navbar navbar-fixed-top">
        <div class="navbar-inner">
            <h3>
                    
                <asp:Image runat="server" ID="img" ImageUrl="~/Skin/Default/Img/logoHemopa.png" width="70px" 
                    Height="83px"  ></asp:Image>
                SISTEMA DE GESTÃO DE NÃO CONFORMIDADE
                </h3>
            <div class="container">
                <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse"><span
                    class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span>
                </a>
                <div class="nav-collapse">
                </div>
                <!-- /nav-collapse -->
            </div>
            <!-- /container -->
        </div>
        <!-- /navbar-inner -->
    </div>
    <!-- /navbar -->
    <div id="login-container">
        <div id="login-header" class="style1">
            <strong>SisGNCWeb</strong>
        </div>
        <!-- /login-header -->
        <div id="login-content" class="clearfix">
            <fieldset>
                <div class="control-group">
                    <label class="control-label" for="username">
                        Login</label>
                    <div class="controls">
                        <asp:TextBox class="" ID="txtLogin" runat="server" />
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label" for="password">
                        Senha</label>
                    <div class="controls">
                        <asp:TextBox class="" ID="txtPassword" TextMode="Password" runat="server" />
                    </div>
                </div>
            </fieldset>
            <div class="pull-right">
                <asp:Button class="btn btn-warning btn-large" Width="100px" ID="btnOk" runat="server"
                    Text="Acessar" OnClick="btnOk_Click" />
            </div>
            
        </div>
        <!-- /login-content -->
        <!-- /login-extra -->
    </div>
    <uc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:Literal ID="MSG001" Text="Favor preencher usuário e senha de acesso ao sistema."
        runat="server"></asp:Literal>
    <asp:Literal ID="MSG002" Text="Dados inválidos." runat="server"></asp:Literal>
    </form>
</body>
</html>
