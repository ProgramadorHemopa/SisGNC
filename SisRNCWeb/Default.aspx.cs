using System;
using System.Data;
using System.Collections;

using APB.Mercury.WebInterface.SCPWeb.Www.Authorization;
using APB.Mercury.WebInterface.SCPWeb.Www.MasterPages;
using System.Web.UI.WebControls;
using System.Web.UI;

using HMP.DataObjects;
using HMP.DataObjects.SisRNCWeb;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

using HMP.WebInterface.SisRNCWeb.Www.DataAccess;
using APB.Mercury.Exceptions;

namespace HMP.WebInterface.SisRNCWeb.Www.Pages
{
    public partial class Default : BasePage
    {

        #region [Metodos]

        private void LogUser()
        {
            if (txtLogin.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                MessageBox1.wuc_ShowMessage(MSG001.Text);
            }
            else if (txtLogin.Text.Trim().Length < 4 || txtPassword.Text.Trim().Length < 4)
            {
                MessageBox1.wuc_ShowMessage(MSG002.Text);
            }
            else
            {
                LoginUserDo lUser;
                //string lDECODE = APB.Framework.Encryption.Crypto.Decode(txtPassword.Text);
                lUser = SystemUserDo.VerifyLogin(txtLogin.Text.ToUpper().Trim(), txtPassword.Text, "1", LocalInstance.ConnectionInfo);
                if (lUser != null)
                {

                    Session["_SessionUser"] = lUser;

                    //Session.Add("SecuritySet", APB.Framework.Security.PermissionQuery.GetUserSecuritySet(Convert.ToInt32(lUser.PESF_ID)));
                    if(Session["RedirectAfterLogin"] != null)
                        Response.Redirect(Session["RedirectAfterLogin"].ToString(), false);
                    else
                        Response.Redirect("Aut/AutDefault.aspx", false);

                }
                else
                {
                    MessageBox1.wuc_ShowMessage("Usuário não encontrado.", 2);
                }
            }
        }

        #endregion

        #region [Eventos]

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    MSG001.Visible = false;
                    MSG002.Visible = false;

                    if (Request["LOGIN"] != null)
                    {
                        txtLogin.Text = Request["LOGIN"].ToString();
                        txtPassword.Text = APB.Framework.Encryption.Crypto.Decode(Request["SENHA"].ToString());

                        LogUser();
                    }
                    else
                        SetFocus(txtLogin);
                }
            }
            catch (WebManagerException err)
            {
                err.TratarExcecaoLogin(true);
            }
            catch (Exception err)
            {
                (new UnknownException(err)).TratarExcecaoLogin(true);
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                LogUser();

            }
            catch (WebManagerException err)
            {
                err.TratarExcecaoLogin(true);
            }
            catch (Exception err)
            {
                //(new UnknownException(err)).TratarExcecaoLogin(true);
                MessageBox1.wuc_ShowMessage(err.Message.ToString(), 3);
            }
        }

        protected void lnkPasswordRecovery_Click(object sender, EventArgs e)
        {
            Response.Redirect("PasswordRecovery.aspx?Login=" + txtLogin.Text);
        }

        #endregion

    }
}
