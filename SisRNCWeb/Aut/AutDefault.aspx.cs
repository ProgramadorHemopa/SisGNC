using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using HMP.DataObjects;
using System.Globalization;
using System.Data;
using HMP.DataObjects.SisRNCWeb;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;
using HMP.WebInterface.SisRNCWeb.Www.DataAccess;

using APB.Mercury.Exceptions;

namespace HMP.WebInterface.SisRNCWeb.Www.Pages
{
    public partial class Aut_Default : BaseAutPage
    {

        #region [Variaveis]

        public string lLogin = "";
        public string lData = "";
        public string lDiaSemana = "";
        private DateTime lDataSistema;

        #endregion

        #region [Metodos]

        #region [Metodos_Load]
        protected void UserResume()
        {
            lLogin = ((LoginUserDo)Session["_SessionUser"]).LoginName;
            lDataSistema = DateTime.Now;
            lData = lDataSistema.ToString("dd/MM/yyyy");
            lDiaSemana = DateTimeFormatInfo.CurrentInfo.GetDayName(lDataSistema.DayOfWeek);
            DataTable lTableUnidadeResp = SystemUserDo.GetUnidadeResp(((LoginUserDo)Session["_SessionUser"]).MATRICULA, LocalInstance.ConnectionInfo);
            if (lTableUnidadeResp.Rows.Count > 1)
            {
                divAtuacao.Visible = true;
                ddlUnidades.DataSource = lTableUnidadeResp;
                ddlUnidades.DataTextField = "UNIDADES";
                ddlUnidades.DataValueField = "ID";
                ddlUnidades.DataBind();

                ddlUnidades.SelectedValue = ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID.ToString();
            }
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

        
        #endregion

        #region [Eventos]

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    UserResume();
                    TITLE.Visible = false;
                }
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


        #endregion


        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            DataTable lTable = SystemUserDo.GetAllSystemUser(LocalInstance.ConnectionInfo);
            DataFieldCollection lFields = new DataFieldCollection();
            
            OperationResult lReturn = new OperationResult();
            string pSUSR_PASSWORD = "";

            if (lTable.Rows.Count > 0)
            {
                for (int i = 0; i < lTable.Rows.Count; i++)
                {
                    if (lTable.Rows[i][SystemUserQD._SUSR_PASSWORD.Name].ToString() == "")
                    {
                        //string[] lLogin = lTable.Rows[i][SystemUserQD._SUSR_LOGIN.Name].ToString().Split(' ');
                        pSUSR_PASSWORD = APB.Framework.Encryption.Crypto.Encode(lTable.Rows[i][SystemUserQD._MATRICULA.Name].ToString());

                        lFields = new DataFieldCollection();
                        lFields.Add(SystemUserQD._SUSR_ID, lTable.Rows[i][SystemUserQD._SUSR_ID.Name].DBToDecimal());
                        //lFields.Add(SystemUserQD._SUSR_LOGIN, lLogin[0].ToUpper() + "." + lLogin[lLogin.Length - 1].ToUpper());
                        lFields.Add(SystemUserQD._SUSR_PASSWORD, pSUSR_PASSWORD);

                        lReturn = SystemUserDo.Update(lFields, LocalInstance.ConnectionInfo);

                        if (lReturn.HasError)
                        {

                        }
                    }
                }
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID = ddlUnidades.SelectedValue.DBToDecimal();
            UserResume();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}