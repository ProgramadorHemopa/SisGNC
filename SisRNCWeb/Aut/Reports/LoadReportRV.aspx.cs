using System;
using System.Data;
using System.Web;

using APB.Framework.DataBase;


using HMP.DataObjects.SisRNCWeb.QueryDictionaries;
using HMP.WebInterface.SisRNCWeb.Www.DataAccess;
using APB.Mercury.Exceptions;
using HMP.DataObjects.SisRNCWeb;


using System.Web.UI.WebControls;
using System.Configuration;

using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.Common;
using System.Web.UI;

namespace RPA.WebInterface.WebRelatorioLotus.Www.Pages
{
    public partial class Aut_Reports_LoadReportRV : System.Web.UI.Page
    {
        #region Private Methods

        private void loadReport()
        {
            string lLocalRptFiles;
            DataTable lData;

            string pTipo = Request.QueryString["TIPO"];

            if (pTipo == "INCLUSAOPRONTUARIO")
            {
                if (Session["WRK_TABLE"] != null)
                {
                    lData = ((DataTable)Session["WRK_TABLE"]);

                    if (lData.Rows.Count > 0)
                    {
                        lLocalRptFiles = ConfigurationManager.AppSettings["SourceRPTFiles"];

                        ReportParameter pParametroTipoRelatorio = new ReportParameter();
                        ReportParameter pParametroDefensor = new ReportParameter();
                        ReportParameter pParametroDefensoria = new ReportParameter();
                        ReportParameter pParametroCompetencia = new ReportParameter();
                        ReportParameter pParametroObservacao = new ReportParameter();
                        ReportDataSource dsReport = new ReportDataSource("DsInclusaoProntuario", lData);




                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.Reset();
                        ReportViewer1.LocalReport.ReportPath = lLocalRptFiles + hidReportName.Value;
                        ReportViewer1.LocalReport.DataSources.Add(dsReport);

                        ReportViewer1.LocalReport.Refresh();


                        Warning[] warnings;
                        string[] streamids;
                        string encoding;
                        string mimeType;
                        string extension;

                        string deviceInfo =
                            "<DeviceInfo>" +
                            "  <OutputFormat>PDF</OutputFormat>" +
                            "  <PageWidth>21cm</PageWidth>" +
                            "  <PageHeight>29cm</PageHeight>" +
                            "  <MarginTop>0.1in</MarginTop>" +
                            "  <MarginLeft>0in</MarginLeft>" +
                            "  <MarginRight>0in</MarginRight>" +
                            "  <MarginBottom>0.1in</MarginBottom>" +
                            "</DeviceInfo>";

                        byte[] bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo,
                            out mimeType, out encoding, out extension, out streamids, out warnings);

                        HttpContext.Current.Response.Buffer = true;
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.ContentType = mimeType;
                        HttpContext.Current.Response.AddHeader("content‐disposition", ("inline; filename=ExportedReport." + "PDF"));
                        HttpContext.Current.Response.BinaryWrite(bytes);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();

                    }
                }

            }
            else if (pTipo == "FollowUp")
            {
                if (Session["WRK_TABLE"] != null)
                {
                    lData = ((DataTable)Session["WRK_TABLE"]);

                    if (lData.Rows.Count > 0)
                    {
                        lLocalRptFiles = ConfigurationManager.AppSettings["SourceRPTFiles"];

                        ReportParameter pParametroTipoRelatorio = new ReportParameter();
                        ReportParameter pParametroDefensor = new ReportParameter();
                        ReportParameter pParametroDefensoria = new ReportParameter();
                        ReportParameter pParametroCompetencia = new ReportParameter();
                        ReportParameter pParametroObservacao = new ReportParameter();
                        ReportDataSource dsReport = new ReportDataSource("dsFOLLOWUP", lData);


                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.Reset();
                        ReportViewer1.LocalReport.ReportPath = lLocalRptFiles + hidReportName.Value;
                        ReportViewer1.LocalReport.DataSources.Add(dsReport);

                        if (hidUnidadePrisional.Value != "")
                        {
                            ReportParameter pParametroUnidadePrisional = new ReportParameter();
                            pParametroUnidadePrisional = new ReportParameter("prmUnidadePrisional", hidUnidadePrisional.Value);
                            ReportViewer1.LocalReport.SetParameters(pParametroUnidadePrisional);
                        }

                        ReportViewer1.LocalReport.Refresh();


                        Warning[] warnings;
                        string[] streamids;
                        string encoding;
                        string mimeType;
                        string extension;

                        string deviceInfo =
                            "<DeviceInfo>" +
                            "  <OutputFormat>PDF</OutputFormat>" +
                            "  <PageWidth>21cm</PageWidth>" +
                            "  <PageHeight>29cm</PageHeight>" +
                            "  <MarginTop>0.1in</MarginTop>" +
                            "  <MarginLeft>0in</MarginLeft>" +
                            "  <MarginRight>0in</MarginRight>" +
                            "  <MarginBottom>0.1in</MarginBottom>" +
                            "</DeviceInfo>";

                        byte[] bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo,
                            out mimeType, out encoding, out extension, out streamids, out warnings);

                        HttpContext.Current.Response.Buffer = true;
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.ContentType = mimeType;
                        HttpContext.Current.Response.AddHeader("content‐disposition", ("inline; filename=ExportedReport." + "PDF"));
                        HttpContext.Current.Response.BinaryWrite(bytes);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();

                    }
                }

            }
            else if (hidReportName.Value == "Ocorrencia.rdlc")
            {

                DataTable lDataOcorrencia = ((DataTable)Session["WRK_TABLE_OCORRENCIA"]);
                DataTable lDataNormas = new DataTable();
                DataTable lDataSintomasAcao = new DataTable();
                DataTable lDataAnaliseCritica = new DataTable();
                DataTable lDataAnaliseCausaEfeito = new DataTable();
                DataTable lDataDiagramaCausaEfeito = new DataTable();
                DataTable lDataAcoes = new DataTable();
                DataTable lDataVerificaEficacia = new DataTable();

                if (lDataOcorrencia.Rows.Count > 0)
                {
                    lLocalRptFiles = ConfigurationManager.AppSettings["SourceRPTFiles"];

                    //ReportParameter pParametroTipoRelatorio = new ReportParameter();
                    ReportDataSource dsReportOcorrencia = new ReportDataSource("DataSetOcorrencia", lDataOcorrencia);

                    if (((DataTable)Session["WRK_TABLE_NORMAS"]) != null)
                        lDataNormas = ((DataTable)Session["WRK_TABLE_NORMAS"]);

                    if (((DataTable)Session["WRK_TABLE_SINTOMASACAO"]) != null)
                        lDataSintomasAcao = ((DataTable)Session["WRK_TABLE_SINTOMASACAO"]);

                    if (((DataTable)Session["WRK_TABLE_ANALISECRITICA"]) != null)
                        lDataAnaliseCritica = ((DataTable)Session["WRK_TABLE_ANALISECRITICA"]);

                    if (((DataTable)Session["WRK_TABLE_ANALISECAUSAEFEITO"]) != null)
                        lDataAnaliseCausaEfeito = ((DataTable)Session["WRK_TABLE_ANALISECAUSAEFEITO"]);

                    if (((DataTable)Session["WRK_TABLE_DIAGRAMACAUSAEFEITO"]) != null)
                        lDataDiagramaCausaEfeito = ((DataTable)Session["WRK_TABLE_DIAGRAMACAUSAEFEITO"]);

                    if (((DataTable)Session["WRK_TABLE_ACOES"]) != null)
                        lDataAcoes = ((DataTable)Session["WRK_TABLE_ACOES"]);

                    if (((DataTable)Session["WRK_TABLE_VERIFICAREFICACIA"]) != null)
                        lDataVerificaEficacia = ((DataTable)Session["WRK_TABLE_VERIFICAREFICACIA"]);

                    
                    ReportDataSource dsReportNormas = new ReportDataSource("DataSetNormas", lDataNormas);
                    ReportDataSource dsReportSintomasAcao = new ReportDataSource("DataSetSintomasAcao", lDataSintomasAcao);
                    ReportDataSource dsReportAnaliseCritica = new ReportDataSource("DataSetAnaliseCritica", lDataAnaliseCritica);
                    ReportDataSource dsReportAnaliseCausaEfeito = new ReportDataSource("DataSetAnaliseCausaEfeito", lDataAnaliseCausaEfeito);
                    ReportDataSource dsReportDiagramaCausaEfeito = new ReportDataSource("DataSetDiagramaCausaEfeito", lDataDiagramaCausaEfeito);
                    ReportDataSource dsReportAcoes = new ReportDataSource("DataSetAcoes", lDataAcoes);
                    ReportDataSource dsReportVerificaEficacia = new ReportDataSource("DataSetVerificaEficacia", lDataVerificaEficacia);


                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.Reset();
                    ReportViewer1.LocalReport.ReportPath = lLocalRptFiles + hidReportName.Value;
                    ReportViewer1.LocalReport.DataSources.Add(dsReportOcorrencia);
                    ReportViewer1.LocalReport.DataSources.Add(dsReportNormas);
                    ReportViewer1.LocalReport.DataSources.Add(dsReportSintomasAcao);
                    ReportViewer1.LocalReport.DataSources.Add(dsReportAnaliseCritica);
                    ReportViewer1.LocalReport.DataSources.Add(dsReportAnaliseCausaEfeito);
                    ReportViewer1.LocalReport.DataSources.Add(dsReportDiagramaCausaEfeito);
                    ReportViewer1.LocalReport.DataSources.Add(dsReportAcoes);
                    ReportViewer1.LocalReport.DataSources.Add(dsReportVerificaEficacia);


                    /*ReportParameter pParametros = new ReportParameter();
                    pParametros = new ReportParameter("prmParametros", hidParametros.Value);
                    ReportViewer1.LocalReport.SetParameters(pParametros);
                    */

                    ReportViewer1.LocalReport.Refresh();


                    Warning[] warnings;
                    string[] streamids;
                    string encoding;
                    string mimeType;
                    string extension;

                    string deviceInfo =
                        "<DeviceInfo>" +
                        "  <OutputFormat>PDF</OutputFormat>" +
                        "  <PageWidth>21cm</PageWidth>" +
                        "  <PageHeight>29cm</PageHeight>" +
                        "  <MarginTop>0.1in</MarginTop>" +
                        "  <MarginLeft>0in</MarginLeft>" +
                        "  <MarginRight>0in</MarginRight>" +
                        "  <MarginBottom>0.1in</MarginBottom>" +
                        "</DeviceInfo>";

                    byte[] bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo,
                        out mimeType, out encoding, out extension, out streamids, out warnings);

                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = mimeType;
                    HttpContext.Current.Response.AddHeader("content‐disposition",
                        ("inline; filename=ExportedReport." + "PDF"));
                    HttpContext.Current.Response.BinaryWrite(bytes);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
            else if (hidReportName.Value == "RelatorioFluxo.rdlc")
            {

                lData = ((DataTable)Session["WRK_TABLE"]);

                if (lData.Rows.Count > 0)
                {
                    lLocalRptFiles = ConfigurationManager.AppSettings["SourceRPTFiles"];

                    ReportParameter pParametroTipoRelatorio = new ReportParameter();
                    ReportParameter pParametroDefensor = new ReportParameter();
                    ReportParameter pParametroDefensoria = new ReportParameter();
                    ReportParameter pParametroCompetencia = new ReportParameter();
                    ReportParameter pParametroObservacao = new ReportParameter();
                    ReportDataSource dsReport = new ReportDataSource("DataSet1", lData);




                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.Reset();
                    ReportViewer1.LocalReport.ReportPath = lLocalRptFiles + hidReportName.Value;
                    ReportViewer1.LocalReport.DataSources.Add(dsReport);



                    ReportViewer1.LocalReport.Refresh();


                    Warning[] warnings;
                    string[] streamids;
                    string encoding;
                    string mimeType;
                    string extension;

                    string deviceInfo =
                        "<DeviceInfo>" +
                        "  <OutputFormat>PDF</OutputFormat>" +
                        "  <PageWidth>29cm</PageWidth>" +
                        "  <PageHeight>21cm</PageHeight>" +
                        "  <MarginTop>0.1in</MarginTop>" +
                        "  <MarginLeft>0in</MarginLeft>" +
                        "  <MarginRight>0in</MarginRight>" +
                        "  <MarginBottom>0.1in</MarginBottom>" +
                        "</DeviceInfo>";

                    byte[] bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo,
                        out mimeType, out encoding, out extension, out streamids, out warnings);

                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = mimeType;
                    HttpContext.Current.Response.AddHeader("content‐disposition",
                        ("inline; filename=ExportedReport." + "PDF"));
                    HttpContext.Current.Response.BinaryWrite(bytes);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }

            else if (hidReportName.Value == "RelatorioOcorrencia.rdlc")
            {

                lData = ((DataTable)Session["WRK_TABLE"]);

                if (lData.Rows.Count > 0)
                {
                    lLocalRptFiles = ConfigurationManager.AppSettings["SourceRPTFiles"];

                    ReportParameter pParametroTipoRelatorio = new ReportParameter();
                    ReportParameter pParametroDefensor = new ReportParameter();
                    ReportParameter pParametroDefensoria = new ReportParameter();
                    ReportParameter pParametroCompetencia = new ReportParameter();
                    ReportParameter pParametroObservacao = new ReportParameter();
                    ReportDataSource dsReport = new ReportDataSource("DataSet1", lData);




                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.Reset();
                    ReportViewer1.LocalReport.ReportPath = lLocalRptFiles + hidReportName.Value;
                    ReportViewer1.LocalReport.DataSources.Add(dsReport);


                    ReportParameter pParametros = new ReportParameter();
                    pParametros = new ReportParameter("prmParametros", hidParametros.Value);
                    ReportViewer1.LocalReport.SetParameters(pParametros);


                    ReportViewer1.LocalReport.Refresh();


                    Warning[] warnings;
                    string[] streamids;
                    string encoding;
                    string mimeType;
                    string extension;

                    string deviceInfo =
                        "<DeviceInfo>" +
                        "  <OutputFormat>PDF</OutputFormat>" +
                        "  <PageWidth>29cm</PageWidth>" +
                        "  <PageHeight>21cm</PageHeight>" +
                        "  <MarginTop>0.1in</MarginTop>" +
                        "  <MarginLeft>0in</MarginLeft>" +
                        "  <MarginRight>0in</MarginRight>" +
                        "  <MarginBottom>0.1in</MarginBottom>" +
                        "</DeviceInfo>";

                    byte[] bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo,
                        out mimeType, out encoding, out extension, out streamids, out warnings);

                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = mimeType;
                    HttpContext.Current.Response.AddHeader("content‐disposition",
                        ("inline; filename=ExportedReport." + "PDF"));
                    HttpContext.Current.Response.BinaryWrite(bytes);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
            else if (hidReportName.Value == "RelatorioPlanoDeAcao.rdlc")
            {

                lData = ((DataTable)Session["WRK_TABLE"]);

                if (lData.Rows.Count > 0)
                {
                    lLocalRptFiles = ConfigurationManager.AppSettings["SourceRPTFiles"];

                    ReportParameter pParametroTipoRelatorio = new ReportParameter();
                    ReportParameter pParametroDefensor = new ReportParameter();
                    ReportParameter pParametroDefensoria = new ReportParameter();
                    ReportParameter pParametroCompetencia = new ReportParameter();
                    ReportParameter pParametroObservacao = new ReportParameter();
                    ReportDataSource dsReport = new ReportDataSource("DataSet1", lData);




                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.Reset();
                    ReportViewer1.LocalReport.ReportPath = lLocalRptFiles + hidReportName.Value;
                    ReportViewer1.LocalReport.DataSources.Add(dsReport);


                    ReportParameter pParametros = new ReportParameter();
                    pParametros = new ReportParameter("prmParametros", hidParametros.Value);
                    ReportViewer1.LocalReport.SetParameters(pParametros);


                    ReportViewer1.LocalReport.Refresh();


                    Warning[] warnings;
                    string[] streamids;
                    string encoding;
                    string mimeType;
                    string extension;

                    string deviceInfo =
                        "<DeviceInfo>" +
                        "  <OutputFormat>PDF</OutputFormat>" +
                        "  <PageWidth>29cm</PageWidth>" +
                        "  <PageHeight>21cm</PageHeight>" +
                        "  <MarginTop>0.1in</MarginTop>" +
                        "  <MarginLeft>0in</MarginLeft>" +
                        "  <MarginRight>0in</MarginRight>" +
                        "  <MarginBottom>0.1in</MarginBottom>" +
                        "</DeviceInfo>";

                    byte[] bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo,
                        out mimeType, out encoding, out extension, out streamids, out warnings);

                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = mimeType;
                    HttpContext.Current.Response.AddHeader("content‐disposition",
                        ("inline; filename=ExportedReport." + "PDF"));
                    HttpContext.Current.Response.BinaryWrite(bytes);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
            else
            {
                if (Session["WRK_TABLE"] != null)
                {
                    lData = ((DataTable)Session["WRK_TABLE"]);

                    if (lData.Rows.Count > 0)
                    {
                        lLocalRptFiles = ConfigurationManager.AppSettings["SourceRPTFiles"];

                        ReportParameter pParametroTipoRelatorio = new ReportParameter();
                        ReportParameter pParametroDefensor = new ReportParameter();
                        ReportParameter pParametroDefensoria = new ReportParameter();
                        ReportParameter pParametroCompetencia = new ReportParameter();
                        ReportParameter pParametroObservacao = new ReportParameter();
                        ReportDataSource dsReport = new ReportDataSource("DataSet1", lData);




                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.Reset();
                        ReportViewer1.LocalReport.ReportPath = lLocalRptFiles + hidReportName.Value;
                        ReportViewer1.LocalReport.DataSources.Add(dsReport);


                        if (hidReportName.Value == "RMA.rdlc")
                        {

                            pParametroTipoRelatorio = new ReportParameter("prmTipoRelatorio", hidTipo.Value);
                            ReportViewer1.LocalReport.SetParameters(pParametroTipoRelatorio);

                            pParametroDefensor = new ReportParameter("prmDefensor", hidDefensor.Value);
                            ReportViewer1.LocalReport.SetParameters(pParametroDefensor);

                            pParametroDefensoria = new ReportParameter("prmDefensoria", hidDefensoria.Value);
                            ReportViewer1.LocalReport.SetParameters(pParametroDefensoria);

                            pParametroCompetencia = new ReportParameter("prmCompetencia", hidCompetencia.Value);
                            ReportViewer1.LocalReport.SetParameters(pParametroCompetencia);

                            pParametroObservacao = new ReportParameter("prmObservacao", hidObservacao.Value);
                            ReportViewer1.LocalReport.SetParameters(pParametroObservacao);
                        }

                        if (hidReportName.Value == "RelatAnomaliaUnidadePrisional.rdlc")
                        {
                            ReportParameter pParametroUnidadePrisional = new ReportParameter();
                            pParametroUnidadePrisional = new ReportParameter("prmUnidadePrisional", hidUnidadePrisional.Value);
                            ReportViewer1.LocalReport.SetParameters(pParametroUnidadePrisional);

                        }

                        if (hidReportName.Value == "RelatQuadroGeralVisitas.rdlc")
                        {
                            ReportParameter pParametroUnidadePrisional = new ReportParameter();
                            pParametroUnidadePrisional = new ReportParameter("prmUnidadePrisional", hidUnidadePrisional.Value);
                            ReportViewer1.LocalReport.SetParameters(pParametroUnidadePrisional);

                            ReportParameter pParametroPeriodo = new ReportParameter();
                            pParametroPeriodo = new ReportParameter("prmPeriodo", hidPeriodo.Value);
                            ReportViewer1.LocalReport.SetParameters(pParametroPeriodo);

                        }

                        if (hidReportName.Value == "RelatResultadosPenal.rdlc")
                        {

                            pParametroDefensor = new ReportParameter("prmDefensor", hidDefensor.Value);
                            ReportViewer1.LocalReport.SetParameters(pParametroDefensor);

                            ReportParameter pParametroPeriodo = new ReportParameter();
                            pParametroPeriodo = new ReportParameter("prmPeriodo", hidPeriodo.Value);
                            ReportViewer1.LocalReport.SetParameters(pParametroPeriodo);

                        }

                        if (hidReportName.Value == "RelatorioCidadao.rdlc")
                        {

                            ReportParameter pParametros = new ReportParameter();
                            pParametros = new ReportParameter("prmParametros", hidParametros.Value);
                            ReportViewer1.LocalReport.SetParameters(pParametros);

                        }

                        ReportViewer1.LocalReport.Refresh();


                        Warning[] warnings;
                        string[] streamids;
                        string encoding;
                        string mimeType;
                        string extension;

                        string deviceInfo =
                            "<DeviceInfo>" +
                            "  <OutputFormat>PDF</OutputFormat>" +
                            "  <PageWidth>21cm</PageWidth>" +
                            "  <PageHeight>29cm</PageHeight>" +
                            "  <MarginTop>0.1in</MarginTop>" +
                            "  <MarginLeft>0in</MarginLeft>" +
                            "  <MarginRight>0in</MarginRight>" +
                            "  <MarginBottom>0.1in</MarginBottom>" +
                            "</DeviceInfo>";

                        byte[] bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo,
                            out mimeType, out encoding, out extension, out streamids, out warnings);

                        HttpContext.Current.Response.Buffer = true;
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.ContentType = mimeType;
                        HttpContext.Current.Response.AddHeader("content‐disposition",
                            ("inline; filename=ExportedReport." + "PDF"));
                        HttpContext.Current.Response.BinaryWrite(bytes);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();

                    }
                }
            }
        }

        #endregion

        #region Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hidReportName.Value = Request.QueryString["ReportName"].ToString();
                if (Request.QueryString["Tipo"] != null)
                    hidTipo.Value = Request.QueryString["Tipo"].ToString();
                else
                    hidTipo.Value = " ";

                if (Request.QueryString["Defensor"] != null)
                    hidDefensor.Value = Request.QueryString["Defensor"].ToString();
                else
                    hidDefensor.Value = " ";

                if (Request.QueryString["Defensoria"] != null)
                    hidDefensoria.Value = Request.QueryString["Defensoria"].ToString();
                else
                    hidDefensoria.Value = " ";

                if (Request.QueryString["Competencia"] != null)
                    hidCompetencia.Value = Request.QueryString["Competencia"].ToString();
                else
                    hidCompetencia.Value = " ";

                if (Request.QueryString["Observacao"] != null)
                    hidObservacao.Value = Request.QueryString["Observacao"].ToString();
                else
                    hidObservacao.Value = " ";

                if (Request.QueryString["UnidadePrisional"] != null)
                    hidUnidadePrisional.Value = Request.QueryString["UnidadePrisional"].ToString();
                else
                    hidUnidadePrisional.Value = "";

                if (Request.QueryString["Periodo"] != null)
                    hidPeriodo.Value = Request.QueryString["Periodo"].ToString();
                else
                    hidPeriodo.Value = "";

                if (Request.QueryString["Parametros"] != null)
                    hidParametros.Value = Request.QueryString["Parametros"].ToString();
                else
                    hidParametros.Value = "";

                loadReport();
            }
            catch (WebManagerException err)
            {
                err.TratarExcecao(true);
            }
            catch (Exception err)
            {
                (new UnknownException(err)).TratarExcecao(true);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            //myReportDocument.Close();
            //myReportDocument.Dispose();
        }
        #endregion

    }

}