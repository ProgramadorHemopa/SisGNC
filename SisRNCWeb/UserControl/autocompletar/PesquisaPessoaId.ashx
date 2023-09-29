<%@ WebHandler Language="C#" Class="PesquisaPessoaId" %>

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

public class PesquisaPessoaId : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string pName;
        DataTable lTable;
        try
        {
            if (context.Request.QueryString["q"] != null)
            {
                pName = context.Request.QueryString["q"].ToUpper();
                lTable = PessoaDo.GetPesForAutoId(LocalInstance.ConnectionInfo, pName);
                string json = arraier_to_autocomplete(lTable);
                context.Response.Write(json);
            }
        }
        catch { }
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


        //Caso não tenha registro na consulta mostrar a mensagem Nenhum registro encontrado
        if (dt.Rows.Count <= 0)
        {
            ArrayString.Append("{");

            ArrayString.Append("\"d0\":\"0\",\"d1\":\"Nenhum registro encontrado\"");


            ArrayString.Append("}");
        }

        ArrayString.Append("]}");
        return ArrayString.ToString();
    }

}