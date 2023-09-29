using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_WuGraficoBar : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public void Mensagens(string pMensagem)
    {

        var page = HttpContext.Current.CurrentHandler as Page;


        page.ClientScript.RegisterStartupScript(this.GetType(), "dialog", " ShowGrafico('" + pMensagem + "');", true);



    }
}