using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using HMP.DataObjects.SisRNCWeb;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;
using System.Data;
using HMP.WebInterface.SisRNCWeb.Www.DataAccess;    

/// <summary>
/// Summary description for WSPessoa
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WSPessoa : System.Web.Services.WebService {

    public WSPessoa () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public string ListaPessoa(string pNome)
    {
        string lNomePessoa = "RA";
        DataTable lTable = new DataTable();

        lTable = PessoaDo.GetPessoaByNome(pNome, LocalInstance.ConnectionInfo);

        if(lTable.Rows.Count > 0)
        {
            lNomePessoa = lTable.Rows[0][PessoaQD._PES_NOME.Name].ToString();
        }


        return lNomePessoa;

    }
    
}
