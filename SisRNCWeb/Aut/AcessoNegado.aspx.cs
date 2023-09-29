using System;
using HMP.DataObjects;
using System.Globalization;
using System.Data;
using HMP.DataObjects.SisRNCWeb;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;
using HMP.WebInterface.SisRNCWeb.Www.DataAccess;

using APB.Mercury.Exceptions;

namespace HMP.WebInterface.SisRNCWeb.Www.Pages
{
    public partial class Aut_AcessoNegado : BaseAutPage
	{
        #region Properties

        public string  lLogin = "";
        public string  lcountOper = "";
        public string  lcountPrsTemp = "";
        public string  lData = "";
        public string  lDiaSemana = "";
        public decimal lOperatorsCount;
        public decimal lTempOperatorsCount;
        public string  lTrasProviderCount = "";
        public string  lTP_ID = "";
        private DateTime lDataSistema;


        #endregion

        #region Private Methods

        protected void UserResume()
        {
            lLogin = ((LoginUserDo)Session["_SessionUser"]).LoginName;
            lDataSistema = DateTime.Now;
            lData = lDataSistema.ToString("dd/MM/yyyy");
            lDiaSemana = DateTimeFormatInfo.CurrentInfo.GetDayName(lDataSistema.DayOfWeek);

        }

        public void Password()
        {

            if (((LoginUserDo)Session["_SessionUser"]).AlterarSenha)
            {
                Response.Write("<p>");
                Response.Write("<a id='Ajuda' href='" + "../Aut/Admin/PasswordChange.aspx" + "'>Clique aqui para alterar sua senha.</a>");
                Response.Write("</p>");
            }
        }

        #endregion

        #region Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.HasGrid = false;

                base.Page_Load(sender, e);

                this.MasterPage.PageH3 = "Acesso Negado";
            }
            catch (WebManagerException err)
            {
                err.TratarExcecao(true);
            }
            catch (Exception err)
            {
                (new UnknownException(err)).TratarExcecao(true);
            }
        }

        protected void lnkOperador_Click(object sender, EventArgs e)
        {
            Response.Redirect("Operators/Active.aspx");
        }

        protected void lnkTempOperador_Click(object sender, EventArgs e)
        {
            Response.Redirect("Operators/PersonnelTemp.aspx");
        }

        #endregion
    }
}