using System;
using System.Data;
using System.Net;

using HMP.WebInterface.SisRNCWeb.Www.DataAccess;

using HMP.DataObjects.SisRNCWeb;

namespace APB.Mercury.WebInterface.SCPWeb.Www.MasterPages
{
    public partial class AutAdmin : BaseAutMaster
	{

        public string DataUser()
        {
            return "Usuário: " + ((LoginUserDo)Session["_SessionUser"]).LoginName.ToString() +
                "&nbsp;&nbsp;&nbsp;&nbsp;" + "Conexão: " + System.Configuration.ConfigurationManager.AppSettings["ApplicationMode"].ToString() +
                "&nbsp;&nbsp;&nbsp;&nbsp;" + "IP: " + Request.UserHostAddress.ToString();
            
        }
	
	}
}