using System;
using System.Data;

using APB.Mercury.WebInterface.SCPWeb.Www.Authorization;
using APB.Mercury.WebInterface.SCPWeb.Www.MasterPages;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;
using HMP.DataObjects.SisRNCWeb;

using System.Web.UI.WebControls;                              
using System.Web.UI;                                          


using HMP.WebInterface.SisRNCWeb.Www.DataAccess;     
using APB.Mercury.Exceptions;
using System.Configuration;


namespace HMP.WebInterface.SisRNCWeb.Www.Pages       
{
    [APB.Framework.Security.Attributes.SecurityObjectId(999)]
    public partial class Default : BaseAutPage
    {

        #region Global
        bool lError = false;
        wsAtualizacao.Atualizacao wsAtualizacao = new wsAtualizacao.Atualizacao();

        #endregion

        #region LoadInfo


        private void LoadGrid()
        {

        }

        #endregion

        #region Private Methods

        private void InterfacePageLoad()
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    litUser.Text = (string)Application["Defensor"];
                }
            }
            catch (WebManagerException e)
            {
                e.TratarExcecao(true);
            }
            catch (Exception err)
            {
                (new UnknownException(err)).TratarExcecao(true);
            }
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            this.HasValidation = true;
            this.HasMenu = "menuCadastro";
            base.Page_Load(sender, e);
            this.MasterPage.PageH3 = "Administrador";
            InterfacePageLoad();
        }


        #endregion

    }                                                    
}                                                             
