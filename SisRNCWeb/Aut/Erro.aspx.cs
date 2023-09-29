using System;
using System.Data;

using APB.Mercury.WebInterface.SCPWeb.Www.Authorization;
using APB.Mercury.WebInterface.SCPWeb.Www.MasterPages;
using HMP.DataObjects.SisRNCWeb;
using System.Web.UI.WebControls;
using System.Web.UI;

using APB.Mercury.Exceptions;

using HMP.WebInterface.SisRNCWeb.Www.DataAccess;

namespace HMP.WebInterface.SisRNCWeb.Www.Pages
{
    public partial class Erro : BaseAutPage
	{
		#region Private Methods

        private void AddErrorMessage(int iExcNumber, Exception exc)
        {
            iExcNumber++;
            string sAux = "<hr /><strong> (" + iExcNumber.ToString() + ") " + exc.Message + "</strong>";
            if (exc is WebManagerException)
                if (((WebManagerException)exc).DescricaoDetalhada != null)
                    sAux = sAux + "<br />Informação adicional: " + ((WebManagerException)exc).DescricaoDetalhada;

            divDetalhesErro.InnerHtml = divDetalhesErro.InnerHtml + sAux;

            if (exc.InnerException != null)
                AddErrorMessage(iExcNumber, exc.InnerException);
        }


		#endregion

		#region Event Handlers

		protected void Page_Load(object sender, EventArgs e)
		{
            try
            {

                this.HasGrid = false;

                base.Page_Load(sender, e);

                this.MasterPage.PageH3 = "Erro no Sistema";

                if (!IsPostBack)
                {
                    if ((Session["ExceptionMessage"] == null) || (Session["Exception"] == null))
                    {
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        AddErrorMessage(0, (Exception)Session["Exception"]);
                        Session.Remove("ExceptionMessage");
                        Session.Remove("Exception");
                    }
                }
            }
            catch (WebManagerException err)
            {
                err.TratarExcecao(false);
            }
            catch (Exception err)
            {
                UnknownException newErr = new UnknownException(err);
                newErr.TratarExcecao(false);
            }
		}

		#endregion
	}
}
