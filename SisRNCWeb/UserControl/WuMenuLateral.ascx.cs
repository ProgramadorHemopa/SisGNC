using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HMP.DataObjects.SisRNCWeb;
using HMP.WebInterface.SisRNCWeb.Www.DataAccess;
using HMP.WebInterface.SisRNCWeb.Www.Pages;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;
public partial class UserControl_WuMenuLateral : System.Web.UI.UserControl
{
    public string lLink;
    public string lTotal;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            DataTable lTable;
            ValidacaoPermissao la = new ValidacaoPermissao();
            UserControl_WuMenuLateral lControl = new UserControl_WuMenuLateral();

            if(((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID == 60 || ((LoginUserDo)Session["_SessionUser"]).LoginName == "ADMIN.GNC")//nq
            {
                menuCadastroBasico.Visible = true;
                menuRelatorios.Visible = true;
                menuReprogramacao.Visible = true;
            }

        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }
    }
}
