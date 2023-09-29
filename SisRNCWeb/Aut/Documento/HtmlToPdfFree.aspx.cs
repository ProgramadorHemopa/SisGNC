using System;
using System.Collections;
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

using DuoDimension;

public partial class Aut_Defensor_HtmlToPdfFree : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Request["TYPE"] != null)
            {
                GerarHtml(Session["WRK_PTC_ID"].DBToDecimal());
            }
            else if (Session["WRK_PTC_ID"] != null)
            {
                GerarPDF(Session["WRK_PTC_ID"].DBToDecimal());
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

    private void GerarHtml(decimal pPTC_ID)
    {
        DataTable lTable = new DataTable();//PeticaoTo.GetTextoPeticaoByID(pPTC_ID, LocalInstance.ConnectionInfo);

        if (lTable.Rows.Count > 0)
        {
            string nomeSession = Session.SessionID + DateTime.Now.Millisecond.ToString();

            //CABEÇALHO HTML
            string nomeArqHtml = Server.MapPath("~") + "\\_pdf\\" + nomeSession + ".doc";
            string nomeArqImg = Server.MapPath("~") + "\\Skin\\Default\\Img\\LogoGov1.jpg";
            string nomeArqImg2 = Server.MapPath("~") + "\\Skin\\Default\\Img\\LogoDpCompac1.jpg";
            string defensoria = "";

            if (((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID.ToString() != "")
                defensoria = ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID.ToString();
            else
                defensoria = "DEFENSORIA PÚBLICA DE " + ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID;



            string lHtml = @" <table border='0' width='830px' align='center'>
                                <tr>
                                    <td align = 'left'><img src='" + nomeArqImg + @"' height='100' width='100'/></td>
                                    <td align = 'center'><p><span style='font-size:14pt;'>DEFENSORIA PÚBLICA DO PARÁ <br> " + defensoria +
                                        @"</span> <br>" + ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID + @" </p></td>
                                    <td align = 'right'><img src='" + nomeArqImg2 + @"' height='100' width='100'/></td>
                                </tr>
                              </table>
                              
                            ";


            //DOCUMENTO HTML
            StreamWriter sWriter = new StreamWriter(nomeArqHtml, false, Encoding.UTF8);
            //sWriter.Write("<html><body><div style='margin-top:0pt; margin-left:99pt; margin-right:45pt; margin-bottom:200pt;'>" + lHtml + lTable.Rows[0][PeticaoQD._PTC_TEXTO.Name].ToString() + "</div></body></html>");
            sWriter.Close();
            sWriter.Dispose();


            Response.Redirect("~/_pdf/" + nomeSession + ".doc");
        }
    }

    private void GerarPDF(decimal pPTC_ID)
    {
        DuoDimension.HtmlToPdf.SetLicenseCode("771500037994826");

        DataTable lTable = new DataTable();//PeticaoTo.GetTextoPeticaoByID(pPTC_ID, LocalInstance.ConnectionInfo);
        DuoDimension.HtmlToPdf conv = new DuoDimension.HtmlToPdf();

        if (lTable.Rows.Count > 0)
        {
            string defensoria = "";

            if (((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID.ToString() != "")
                defensoria = ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID.ToString();
            else
                defensoria = "DEFENSORIA PÚBLICA DE " + ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID.ToString();


            conv.BasePath = Server.MapPath(@"~\Skin\Default\Img\");
            conv.PageInfo.PageTopMargin = 45;
            conv.PageInfo.PageBottomMargin = 45;
            conv.PageInfo.PageLeftMargin = 99;
            

            string lFooter = "<em>Processo Interno Defensoria: " + Session["WRK_PARAMETER_PI"].ToString() + "                                                                                                                       ";
            lFooter += "<br>Petição: " + Session["WRK_PARAMETER_PETICAO"].ToString() + "</em>";
            conv.Footer = lFooter;

            //conv.OpenHTML(lStringBuilder);
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;

            response.Clear();
            //response.AddHeader("Content-Type", "binary/octet-stream");
            //response.AddHeader("Content-Disposition", "attachment; filename=" + Session["WRK_PARAMETER_PETICAO"].ToString() + ".pdf;");
            conv.ShowPDF(conv.SavePDF(), response);
        }
    }

 
}
