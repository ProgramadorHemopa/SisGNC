<%@ WebHandler Language="C#" Class="AutoCompleteProcessoExtern" %>

using System;
using System.Web;
using APB.Mercury.WebInterface.SCPWeb.Www.Authorization;
using APB.Mercury.WebInterface.SCPWeb.Www.MasterPages;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;
using HMP.DataObjects.SisRNCWeb;

using System.Web.UI.WebControls;
using System.Web.UI;


using HMP.WebInterface.SisRNCWeb.Www.DataAccess;
using APB.Mercury.Exceptions;
using System.Configuration;
using System.Data;

public class AutoCompleteProcessoExtern : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string pName;
        DataTable lTable;

       /*
        * if (context.Request.QueryString["q"] != null)
        {
            pName = context.Request.QueryString["q"].ToUpper();
            lTable = DocumentoDo.GetProcessoExternoForAuto(LocalInstance.ConnectionInfo, pName);
            string json = arraier_to_autocomplete(lTable);
            context.Response.Write(json);
        }
        * */
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public string arraier_to_autocomplete(DataTable dt)
    {
        System.Text.StringBuilder ArrayString = new System.Text.StringBuilder();
        ArrayString.Append("{" + @"""" + "items" + @"""" + ":[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ArrayString.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                if (j < dt.Columns.Count - 1)
                {
                    ArrayString.Append(@"""" + "d" + j + @"""" + ":\"" + dt.Rows[i][j].ToString().Replace("\"", "\\\"") + "\",");
                }
                else if (j == dt.Columns.Count - 1)
                {
                    ArrayString.Append(@"""" + "d" + j + @"""" + ":\"" + dt.Rows[i][j].ToString().Replace("\"", "\\\"") + "\"");
                }
            }
            if (i == dt.Rows.Count - 1)
            {
                ArrayString.Append("}");
            }
            else
            {
                ArrayString.Append("},");
            }
        }
        ArrayString.Append("]}");
        return ArrayString.ToString();
    }
    
}