using System;
using System.Configuration;
using HMP.DataObjects.SisRNCWeb;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        object lRedirect = Session["RedirectAfterLogin"];

        Session.Remove("WRK_TABLE");
        Session.Remove("_SessionUser");
        //Session.Abandon();
        //Session.Clear();

        Session["RedirectAfterLogin"] = lRedirect;
        string PageRedirect = "";

        PageRedirect = ConfigurationManager.AppSettings["Logout"].ToString();
        string[] sURL = System.Web.HttpContext.Current.Request.Url.ToString().Split('/');

        Response.Redirect("~/");
        //Response.Redirect("http://" + sURL[2].ToString() + "/" + PageRedirect);

    }
}
