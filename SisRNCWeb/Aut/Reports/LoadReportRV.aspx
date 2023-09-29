<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoadReportRV.aspx.cs"
    Inherits="RPA.WebInterface.WebRelatorioLotus.Www.Pages.Aut_Reports_LoadReportRV" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Relatórios</title>
</head>
<body>
    <form id="form1" runat="server">

        <asp:HiddenField ID="hidReportName" runat="server" />
        <asp:HiddenField ID="hidTipo" runat="server" />
        <asp:HiddenField ID="hidDefensor" runat="server" />
        <asp:HiddenField ID="hidDefensoria" runat="server" />
        <asp:HiddenField ID="hidCompetencia" runat="server" />
        <asp:HiddenField ID="hidObservacao" runat="server" />
        <asp:HiddenField ID="hidUnidadePrisional" runat="server" />
        <asp:HiddenField ID="hidPeriodo" runat="server" />
        <asp:HiddenField ID="hidParametros" runat="server" />

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server"></rsweb:ReportViewer>



    </form>
</body>
</html>
