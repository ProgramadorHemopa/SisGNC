using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using System.Xml.XPath;
using FreeTextBoxControls.Support.Sgml;

using System.Data;
using APB.Mercury.WebInterface.SCPWeb.Www.Authorization;
using APB.Mercury.WebInterface.SCPWeb.Www.MasterPages;
using HMP.DataObjects;

using System.Web.UI.WebControls;
using System.Web.UI;

using HMP.DataObjects.SisRNCWeb;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

using HMP.WebInterface.SisRNCWeb.Www.DataAccess;
using APB.Mercury.Exceptions;
using System.Web.UI.WebControls;
using System.Web.UI;

using APB.Mercury.Exceptions;
using System.Configuration;
/// Summary description for Class1
/// </summary>

namespace HMP.WebInterface.SisRNCWeb.Www.Pages
{

    public class ValidacaoPermissao
    {
        public ValidacaoPermissao()
        {
        }

        public bool LeituraKeys()
        {

            bool pKeys = true;

            try
            {

                string arquivo = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @"App_Data\ItensPermissao.xml");
                DataSet myData = null;
                System.IO.FileStream myStream = null;

                myStream = new System.IO.FileStream(arquivo, System.IO.FileMode.Open);
                myData = new DataSet();
                myData.ReadXml(myStream);

                DataTable lTable = myData.Tables["PerfilEstagiario"];

            }
            catch (Exception)
            {

                throw;
            }

            return pKeys;
        }


        /// <summary>
        /// Metodo que carrega o perfil dos logins
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public DataTable LeituraKeysId(string pId)
        {

            DataSet myData = null;
            DataTable lData = null;

            try
            {
                string arquivo = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @"App_Data\ItensPermissao.xml");

                System.IO.FileStream myStream = null;
                myStream = new System.IO.FileStream(arquivo, System.IO.FileMode.Open);
                myData = new DataSet();
                myData.ReadXml(myStream);
                myStream.Close();

                int i = 0;

                if (int.TryParse(pId, out i))
                {

                    DataTable lTableperfil = PessoaFuncaoDo.GetAllFuncaoPerfil(LocalInstance.ConnectionInfo, pId);

                    if (lTableperfil.Rows.Count > 0)
                    {
                        if (lTableperfil.Rows[0]["PRF_ID"].ToString() == "5")
                        {
                            lData = myData.Tables["PerfilEstagiario"];
                        }
                        else if (lTableperfil.Rows[0]["PRF_ID"].ToString() == "6")
                        {
                            lData = myData.Tables["PerfilAtendente"];
                        }
                        else if (lTableperfil.Rows[0]["PRF_ID"].ToString() == "4")
                        {
                            lData = myData.Tables["PerfilDefensor"];
                        }
                        else if (lTableperfil.Rows[0]["PRF_ID"].ToString() == "8")
                        {
                            lData = myData.Tables["ParametroAdmin"];
                        }
                        else if (lTableperfil.Rows[0]["PRF_ID"].ToString() == "2")
                        {
                            lData = myData.Tables["ParametroCoord"];
                        }
                    }

                }

                return lData;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
