using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.IO;
using System.Text;

//using com.lowagie.text;
//using com.lowagie.text.pdf;
//using com.lowagie.text.html;
//using javax.xml.parsers;

using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;

using APB.Mercury.Exceptions;

using HMP.DataObjects.SisRNCWeb;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;
using HMP.WebInterface.SisRNCWeb.Www.DataAccess;


public partial class Aut_Defensor_VisualizarPDF : System.Web.UI.Page
{

    private Document document;
    string nomeSession;
    string nomeArqPdf;
    string nomeArqHtml;
    string nomeArqImg;
    string nomeArqImg2;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["WRK_PTC_ID"] != null)
            {
                //GerarPDF(Session["WRK_PTC_ID"].DBToDecimal());
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


}
