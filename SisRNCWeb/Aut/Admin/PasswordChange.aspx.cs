using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;

using APB.Framework.DataBase;
using HMP.DataObjects.SisRNCWeb;
using APB.Mercury.Exceptions;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;
using HMP.WebInterface.SisRNCWeb.Www.DataAccess;


namespace HMP.WebInterface.SisRNCWeb.Www.Pages
{
    public partial class Aut_Admin_PasswordChange : BaseAutPage
    {
        #region [Varivaies]

        #endregion

        #region [Metodos]

        private void Configuration()
        {
            MSG001.Visible = false;
            MSG002.Visible = false;
            MSG003.Visible = false;
            MSG004.Visible = false;
            MSG005.Visible = false;
            TITLE.Visible = false;
        }

        private bool ValidPassword()
        {
            if (txtPasswordOld.Text.Trim().ToString() == "" || txtPasswordNew1.Text.Trim() == "" || txtPasswordNew2.Text.Trim() == "")
            {
                MessageBox1.wuc_ShowMessage(MSG001.Text, 2);
                return false;
            }
            else if (txtPasswordNew1.Text.Trim().Length < 4)
            {
                MessageBox1.wuc_ShowMessage(MSG004.Text, 2);
                return false;
            }
            else if (txtPasswordNew1.Text.Trim() != txtPasswordNew2.Text.Trim())
            {
                MessageBox1.wuc_ShowMessage(MSG005.Text, 2);
                return false;
            }

            return true;
        }

        private void Interface_Salvar()
        {
            if (ValidPassword())
            {
                string lResult;

                lResult = SystemUserDo.ChangePassword(((LoginUserDo)Session["_SessionUser"]).LoginId,
                    ((LoginUserDo)Session["_SessionUser"]).LoginName, txtPasswordOld.Text, txtPasswordNew1.Text, LocalInstance.ConnectionInfo);

                if (lResult == "")
                    MessageBox1.wuc_ShowMessage(MSG002.Text, "../AutDefault.aspx", 1);
                else
                    MessageBox1.wuc_ShowMessage(MSG003.Text + "  " + lResult, 2);
            }
        }

        #endregion

        #region [Eventos]

        protected void Page_Load(object sender, EventArgs e)
        {
            Configuration();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Interface_Salvar();
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

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AutDefault.aspx");
        }

        #endregion

    }
}
