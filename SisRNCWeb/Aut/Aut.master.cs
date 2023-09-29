using System;
using System.Data;
using System.Net;

using HMP.WebInterface.SisRNCWeb.Www.DataAccess;

using HMP.DataObjects.SisRNCWeb;

namespace APB.Mercury.WebInterface.SCPWeb.Www.MasterPages
{
	public partial class AutMaster : BaseAutMaster
	{

        //public void BuilHelp()
        //{
        //    DataTable lTable;
        //    string lHelp = this.BaseDocument;
        //    lTable = lTable = WebContentTo.GetHelp(decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["SOFTWARES"].ToString()),
        //            ConstantWebContenType.Help, ConstantWebContenGroup.HELP_PORTAL_GARAGEM, lHelp, LocalInstance.ConnectionInfo);

        //    if (lTable.Rows.Count > 0)
        //    {
        //        Response.Write("<p id='toplinks'>");
        //        Response.Write("<a id='Ajuda' href='" + this.BaseURL + "Help.aspx?FormName=" + this.BaseDocument + "&SF_ID=100&Type=help' target='_blank'>Ajuda</a>");
        //        Response.Write("</p>");
        //    }
        //}

	}
}