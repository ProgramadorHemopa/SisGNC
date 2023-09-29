using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.IO;
using System.Text;

using APB.Mercury.Exceptions;

using HMP.DataObjects.SisRNCWeb;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;
using HMP.WebInterface.SisRNCWeb.Www.DataAccess;

using Winnovative.WnvHtmlConvert;
using Winnovative.WnvHtmlConvert.PdfDocument;

public partial class Aut_Defensor_HtmlToPdf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["WRK_MDPT_ID"] != null)
            {
                GerarPDFModeloTermosEDeclaracoes(Session["WRK_MDPT_ID"].DBToDecimal());

            }

   

        }
        catch (WebManagerException em)
        {
            em.TratarExcecao(true);
        }
        catch (Exception err)
        {
            (new UnknownException(err)).TratarExcecao(true);
        }
    }

    // METODO PARA IMPRIMIR CAPA DO PROCESSO - 12/05/2010 - PAULO
    /*
        private void GerarPDFCapa(DataTable pTableDoc, List<string> pAssunto, DataTable pTableProcAnexo)
        {
            string lhtml = "";
            string lprocInterno = "";
            string lprocExterno = "";

            if (pTableProcAnexo.Rows.Count > 0)
            {
                for (int i = 0; i < pTableProcAnexo.Rows.Count; i++)
                {
                    if (pTableProcAnexo.Rows[i][DocumentoAnexoQD._TDOC_ID.Name].DBToDecimal() == (decimal)TipoDocumento.ProcessoExterno)
                        lprocExterno += pTableProcAnexo.Rows[i][DocumentoQD._DOC_NUMERO.Name].ToString() + "   ,   ";
                    else
                        lprocInterno += pTableProcAnexo.Rows[i][DocumentoQD._DOC_NUMERO.Name].ToString() + "   ,   ";
                }
            }

            lhtml = @"<table width=""700"" border=""0"" cellpadding=""0"" cellspacing=""0"">";
            lhtml += @"<tr><td width=""170"" height=""28"">Nome:&nbsp;</td>";
            lhtml += @"<td height=""28"" colspan=""3"">" + pTableDoc.Rows[0][PessoaQD._PES_NOME.Name].ToString() + @"</td>";
            lhtml += @"</tr><tr>";
            lhtml += @"<td height=""28"">M&atilde;e:&nbsp;</td>";
            lhtml += @"<td height=""28"" colspan=""3"">" + pTableDoc.Rows[0][PessoaQD._PES_MAE.Name].ToString() + @"</td>";
            lhtml += @"</tr><tr><td height=""28""><p>Pasta Interna:&nbsp;</p></td>";
            lhtml += @"<td width=""195"" height=""28"">" + pTableDoc.Rows[0][DocumentoQD._DOC_NUMERO.Name].ToString() + "</td>";
            lhtml += @"<td width=""67"">Situa&ccedil;&atilde;o:</td>";
            lhtml += @"<td width=""268"">" + pTableDoc.Rows[0]["SIT_DESCRICAO"].ToString() + "</td></tr><tr>";
            lhtml += @"<td height=""28"">Processo Externo:&nbsp;</td><td height=""28"" bgcolor=""#FFFFFF"" colspan=""3"">" + lprocExterno + "</td></tr><tr>";
            lhtml += @"<td height=""28"">Pastas Internas Anexadas:&nbsp;</td>";
            lhtml += @"<td height=""28"" colspan=""3"">" + lprocInterno + @"</td></tr></table><br>";
            lhtml += @"<table width=""700"" border=""0"" cellpadding=""0"" cellspacing=""0""><tr>";
            lhtml += @"<td align=""center"" height=""28"">Assunto</td></tr>";

            if (pAssunto.Count > 0)
            {
                for (int i = 0; i < pAssunto.Count; i++)
                {
                    lhtml += @"<tr><td height=""28"" valign=""middle"">" + pAssunto[i] + "</td></tr>";
                }
            }

            lhtml += @"</table>";


            string textToConvert;
            textToConvert = "<html><body><div style='margin-left:113pt; margin-right:56pt;margin-top:1pt;'>";
            textToConvert += lhtml;
            textToConvert += "</div></body></html>";

            PdfConverter pdf = new PdfConverter();
            pdf.LicenseKey = "VX5kdWR1Z2dsdWB7ZXVmZHtkZ3tsbGxs";
            pdf.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
            pdf.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
            pdf.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;

            pdf.PdfDocumentOptions.TopMargin = 5;
            pdf.PdfDocumentOptions.GenerateSelectablePdf = true;
            pdf.PdfDocumentOptions.ShowHeader = false;

            pdf.PdfDocumentOptions.ShowFooter = false;

            pdf.PdfFooterOptions.DrawFooterLine = false;


            #region new
            byte[] pdfBytes = null;

            pdfBytes = pdf.GetPdfBytesFromHtmlString(textToConvert);

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;

            response.Clear();
            response.AddHeader("Content-Type", "binary/octet-stream");
            response.AddHeader("Content-Disposition", "attachment; filename=capa" + Session.SessionID + DateTime.Now.Millisecond.ToString() + ".pdf; size=" + pdfBytes.Length.ToString());
            response.Flush();
            response.BinaryWrite(pdfBytes);
            response.Flush();
            response.End();

            #endregion

        }

        //---------------

        private void GerarPDFPeticao(decimal pPTC_ID)
        {
            DataTable lTable = PeticaoTo.GetTextoPeticaoByID(pPTC_ID, LocalInstance.ConnectionInfo);

            if (lTable.Rows.Count > 0)
            {
                string nomeSession = Session.SessionID + DateTime.Now.Millisecond.ToString();

                //Arquivo
                string nomeArqHtml = Server.MapPath("~") + "\\_pdf\\" + nomeSession + ".html";
                string nomeArqImg = Server.MapPath("~") + "\\Skin\\Default\\Img\\LogoGov1.jpg";
                string nomeArqImg2 = Server.MapPath("~") + "\\Skin\\Default\\Img\\LogoDp1.png";

                //Corpo
                string textToConvert;
                textToConvert = "<html><body><div style='margin-left:113pt; margin-right:56pt;margin-top:1pt;'>";
                textToConvert += lTable.Rows[0][PeticaoQD._PTC_TEXTO.Name].ToString();
                textToConvert += "</div></body></html>";


                PdfConverter pdf = new PdfConverter();
                pdf.LicenseKey = "VX5kdWR1Z2dsdWB7ZXVmZHtkZ3tsbGxs";

                //Cabeçalho
                pdf.PdfDocumentOptions.ShowHeader = true;
                pdf.PdfHeaderOptions.HeaderImage = System.Drawing.Image.FromFile(nomeArqImg);
                pdf.PdfHeaderOptions.HeaderHeight = 100;
                pdf.PdfHeaderOptions.HeaderImageWidth = 600;
                pdf.PdfHeaderOptions.DrawHeaderLine = false;

                //Opções documento
                pdf.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
                pdf.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
                pdf.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;
                pdf.PdfDocumentOptions.TopMargin = 5;
                pdf.PdfDocumentOptions.GenerateSelectablePdf = true;

                //Rodapé
                pdf.PdfDocumentOptions.ShowFooter = true;
                pdf.PdfFooterOptions.FooterHeight = 70;
                pdf.PdfFooterOptions.FooterText += "                                         " + ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID;
                pdf.PdfFooterOptions.FooterTextFontSize = 8;
                pdf.PdfFooterOptions.DrawFooterLine = false;


                #region new
                byte[] pdfBytes = null;

                pdfBytes = pdf.GetPdfBytesFromHtmlString(textToConvert);

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;

                response.Clear();
                response.AddHeader("Content-Type", "binary/octet-stream");

                response.AddHeader("Content-Disposition", "attachment; filename=" + lTable.Rows[0][PeticaoQD._PTC_NUMERO.Name].ToString() + ".pdf; size=" + pdfBytes.Length.ToString());
                //response.AddHeader("Content-Disposition", "attachment; filename=" + Session["WRK_PARAMETER_PETICAO"].ToString() + ".pdf; size=" + pdfBytes.Length.ToString());
                response.Flush();
                response.BinaryWrite(pdfBytes);
                response.Flush();
                response.End();

                #endregion
            }
        }

        private void GerarPDFModeloPeticao(decimal pMDPT_ID)
        {
            DataTable lTable = ModeloPeticaoTo.GetModeloPeticaoByID(pMDPT_ID, LocalInstance.ConnectionInfo);

            if (lTable.Rows.Count > 0)
            {
                string nomeSession = Session.SessionID + DateTime.Now.Millisecond.ToString();

                //CABEÇALHO HTML
                string nomeArqHtml = Server.MapPath("~") + "\\_pdf\\" + nomeSession + ".html";
                string nomeArqImg = Server.MapPath("~") + "\\Skin\\Default\\Img\\LogoGov1.jpg";
                string nomeArqImg2 = Server.MapPath("~") + "\\Skin\\Default\\Img\\LogoDp1.png";


                string textToConvert;
                textToConvert = "<html><body><div style='margin-left:113pt; margin-right:56pt;margin-top:1pt;'>";
                textToConvert += lTable.Rows[0][ModeloPeticaoQD._MDPT_TEXTO.Name].ToString();
                textToConvert += "</div></body></html>";


                PdfConverter pdf = new PdfConverter();
                pdf.LicenseKey = "VX5kdWR1Z2dsdWB7ZXVmZHtkZ3tsbGxs";

                //Cabeçalho
                pdf.PdfDocumentOptions.ShowHeader = true;
                pdf.PdfHeaderOptions.HeaderImage = System.Drawing.Image.FromFile(nomeArqImg);
                pdf.PdfHeaderOptions.HeaderHeight = 100;
                pdf.PdfHeaderOptions.HeaderImageWidth = 600;
                pdf.PdfHeaderOptions.DrawHeaderLine = false;

                //Opções documento
                pdf.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
                pdf.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
                pdf.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;
                pdf.PdfDocumentOptions.TopMargin = 5;
                pdf.PdfDocumentOptions.GenerateSelectablePdf = true;

                //Rodapé
                pdf.PdfDocumentOptions.ShowFooter = true;
                pdf.PdfFooterOptions.FooterHeight = 70;
                pdf.PdfFooterOptions.FooterText += "                                         " + ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID;
                pdf.PdfFooterOptions.FooterTextFontSize = 8;
                pdf.PdfFooterOptions.DrawFooterLine = false;


                #region new
                byte[] pdfBytes = null;

                pdfBytes = pdf.GetPdfBytesFromHtmlString(textToConvert);

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;

                response.Clear();
                response.AddHeader("Content-Type", "binary/octet-stream");
                response.AddHeader("Content-Disposition", "attachment; filename=" + lTable.Rows[0][ModeloPeticaoQD._MDPT_DESCRICAO.Name].ToString() + ".pdf; size=" + pdfBytes.Length.ToString());
                response.Flush();
                response.BinaryWrite(pdfBytes);
                response.Flush();
                response.End();

                #endregion
            }
        }

        private void GerarPDFTermosEDeclaracoes(decimal pTRMDC_ID)
        {
            DataTable lTable = TermosEDeclaracoesTo.GetTermosEDeclaracoesById(pTRMDC_ID, LocalInstance.ConnectionInfo);

            if (lTable.Rows.Count > 0)
            {
                string nomeSession = Session.SessionID + DateTime.Now.Millisecond.ToString();

                //CABEÇALHO HTML
                string nomeArqHtml = Server.MapPath("~") + "\\_pdf\\" + nomeSession + ".html";
                string nomeArqImg = Server.MapPath("~") + "\\Skin\\Default\\Img\\LogoGov1.jpg";
                string nomeArqImg2 = Server.MapPath("~") + "\\Skin\\Default\\Img\\LogoDp1.png";

                string textToConvert;
                textToConvert = "<html><body><div style='margin-left:90pt; margin-right:56pt;margin-top:1pt;'>";
                textToConvert += lTable.Rows[0][TermosEDeclaracoesQD._TRMDC_TEXTO.Name].ToString();
                textToConvert += "</div></body></html>";


                PdfConverter pdf = new PdfConverter();
                pdf.LicenseKey = "VX5kdWR1Z2dsdWB7ZXVmZHtkZ3tsbGxs";


                //Cabeçalho
                pdf.PdfDocumentOptions.ShowHeader = true;
                pdf.PdfHeaderOptions.HeaderImage = System.Drawing.Image.FromFile(nomeArqImg);
                pdf.PdfHeaderOptions.HeaderHeight = 100;
                pdf.PdfHeaderOptions.HeaderImageWidth = 600;
                pdf.PdfHeaderOptions.DrawHeaderLine = false;

                //Opções documento
                pdf.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
                pdf.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
                pdf.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;
                pdf.PdfDocumentOptions.TopMargin = 5;
                pdf.PdfDocumentOptions.GenerateSelectablePdf = true;

                //Rodapé
                pdf.PdfDocumentOptions.ShowFooter = true;
                pdf.PdfFooterOptions.FooterHeight = 70;
                pdf.PdfFooterOptions.FooterText += "                                         " + ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID;
                pdf.PdfFooterOptions.FooterTextFontSize = 8;
                pdf.PdfFooterOptions.DrawFooterLine = false;


                #region new
                byte[] pdfBytes = null;

                pdfBytes = pdf.GetPdfBytesFromHtmlString(textToConvert);

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;

                response.Clear();
                response.AddHeader("Content-Type", "binary/octet-stream");
                response.AddHeader("Content-Disposition", "attachment; filename=TermosEDeclaracoes" + pTRMDC_ID + ".pdf; size=" + pdfBytes.Length.ToString());
                response.Flush();
                response.BinaryWrite(pdfBytes);
                response.Flush();
                response.End();

                #endregion
            }
        }

    */
    private void GerarPDFModeloTermosEDeclaracoes(decimal pMODETRM_ID)
    {
        DataTable lTable = new DataTable();//MODELOTERMOSDECLARACAODo.GetAllMODELODECLARACAOID(LocalInstance.ConnectionInfo, pMODETRM_ID);

        if (lTable.Rows.Count > 0)
        {
            string nomeSession = Session.SessionID + DateTime.Now.Millisecond.ToString();

            //CABEÇALHO HTML
            string nomeArqHtml = Server.MapPath("~") + "\\_pdf\\" + nomeSession + ".html";
            string nomeArqImg = Server.MapPath("~") + "\\Skin\\Default\\Img\\LogoGov1.jpg";
            string nomeArqImg2 = Server.MapPath("~") + "\\Skin\\Default\\Img\\LogoDp1.png";

            string textToConvert;
            textToConvert = "<html><body><div style='margin-left:113pt; margin-right:56pt;margin-top:1pt;'>";
            //textToConvert += lTable.Rows[0][MODELOTERMOSDECLARACAOQD._MODETRM_TEXTO.Name].ToString();
            textToConvert += "</div></body></html>";


            PdfConverter pdf = new PdfConverter();
            pdf.LicenseKey = "VX5kdWR1Z2dsdWB7ZXVmZHtkZ3tsbGxs";


            //Cabeçalho
            pdf.PdfDocumentOptions.ShowHeader = true;
            pdf.PdfHeaderOptions.HeaderImage = System.Drawing.Image.FromFile(nomeArqImg);
            pdf.PdfHeaderOptions.HeaderHeight = 100;
            pdf.PdfHeaderOptions.HeaderImageWidth = 600;
            pdf.PdfHeaderOptions.DrawHeaderLine = false;

            //Opções documento
            pdf.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
            pdf.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
            pdf.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;
            pdf.PdfDocumentOptions.TopMargin = 5;
            pdf.PdfDocumentOptions.GenerateSelectablePdf = true;

            //Rodapé
            pdf.PdfDocumentOptions.ShowFooter = true;
            pdf.PdfFooterOptions.FooterHeight = 70;
            pdf.PdfFooterOptions.FooterText += "                                         " + ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID;
            pdf.PdfFooterOptions.FooterTextFontSize = 8;
            pdf.PdfFooterOptions.DrawFooterLine = false;


            #region new
            byte[] pdfBytes = null;

            pdfBytes = pdf.GetPdfBytesFromHtmlString(textToConvert);

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;

            response.Clear();
            response.AddHeader("Content-Type", "binary/octet-stream");
            response.AddHeader("Content-Disposition", "attachment; filename=ModeloTermosEDeclaracoes" + pMODETRM_ID + ".pdf; size=" + pdfBytes.Length.ToString());
            response.Flush();
            response.BinaryWrite(pdfBytes);
            response.Flush();
            response.End();

            #endregion
        }
    }

}
