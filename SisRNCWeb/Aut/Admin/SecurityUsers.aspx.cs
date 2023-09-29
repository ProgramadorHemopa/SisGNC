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
    [APB.Framework.Security.Attributes.SecurityObjectId(31)]
    public partial class SecurityUsers : BaseAutPage
    {
        #region LoadInfo


        private void LoadPessoaFuncao()
        {
            ddlSU_ID.DataSource = PessoaFuncaoDo.GetAllPessoaFuncao(LocalInstance.ConnectionInfo);// GetPessoaFuncaoPessoaByNucleo(ddlNUC_ID.SelectedValue.DBToDecimal(), LocalInstance.ConnectionInfo);
            ddlSU_ID.DataTextField = "PES_NOME";
            ddlSU_ID.DataValueField = "PESF_ID";
            ddlSU_ID.DataBind();

            LoadSecurityObjects();
        }
        private void LoadSecurityObjects()
        {
            grdObjects.PageSize = int.Parse("50");
            ViewState["WRK_TABLE"] = SecurityUsersDtDo.GetSecurityObjectsNivel3(ddlSU_ID.SelectedValue.DBToDecimal(), LocalInstance.ConnectionInfo);
            grdObjects.DataSource = (DataTable)ViewState["WRK_TABLE"];
            grdObjects.DataBind();

            
        }

        #endregion

        #region Private Methods

        private void Clear()
        {
            hidSU_ID.Value = "";
        }

        private bool VerificarInclusao(decimal pSO_OBJECTID, string pSUD_PERMISSION)
        {
            DataTable lTable;
            bool lReturn = false;
            try
            {
                lTable = SecurityUsersDtDo.GetSecurityUsersDtByUser_Object(ddlSU_ID.SelectedValue.DBToDecimal(), pSO_OBJECTID,
                    LocalInstance.ConnectionInfo);

                if (lTable.Rows.Count > 0)
                {
                    lReturn = false;
                    InterfaceUpdate(ddlSU_ID.SelectedValue.DBToDecimal(), pSO_OBJECTID, pSUD_PERMISSION);
                }
                else
                {
                    lReturn = true;
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

            return lReturn;
        }

        private void InterfaceInclude()
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                OperationResult lReturn = new OperationResult();


                DataTable lTable = (DataTable)ViewState["WRK_TABLE"];
                CheckBox chkPermissao = new CheckBox();
                for (int i = 0; i < lTable.Rows.Count; i++)
                {
                    chkPermissao = (CheckBox)grdObjects.Rows[i].FindControl("chkPermissao");

                    string lPERMISSION = "";
                    if (chkPermissao.Checked)
                        lPERMISSION = "1111";
                    else
                        lPERMISSION = "0000";



                    if (VerificarInclusao(lTable.Rows[i][SecurityObjectsQD._SO_OBJECTID.Name].DBToDecimal(), lPERMISSION))
                    {
                        lFields.Clear();
                        lFields.Add(SecurityUsersDtQD._SUD_PERMISSION, lPERMISSION);
                        lFields.Add(SecurityUsersDtQD._SU_ID, decimal.Parse(ddlSU_ID.SelectedValue));
                        lFields.Add(SecurityUsersDtQD._SO_OBJECTID, lTable.Rows[i][SecurityObjectsQD._SO_OBJECTID.Name].DBToDecimal());
                        lFields.Add(SecurityUsersDtQD._SUD_REGDATE, DateTime.Now);
                        lFields.Add(SecurityUsersDtQD._SUD_REGUSER, ((LoginUserDo)Session["_SessionUser"]).LoginName);
                        lFields.Add(SecurityUsersDtQD._SUD_STATUS, "A");

                        lReturn = SecurityUsersDtDo.Insert(lFields, LocalInstance.ConnectionInfo);

                        if (!lReturn.IsValid)
                        {
                            Exception err = new Exception(lReturn.OperationException.Message.ToString());
                            throw err;
                        }
                    }
                }


                MessageBox1.wuc_ShowMessage("Registro salvo com sucesso.", 1);
                Clear();
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

        private void InterfaceUpdate(decimal pSU_ID, decimal pSO_OBJECTID, string pSUD_PERMISSION)
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                OperationResult lReturn = new OperationResult();

                lFields.Add(SecurityUsersDtQD._SU_ID, pSU_ID);
                lFields.Add(SecurityUsersDtQD._SUD_PERMISSION, pSUD_PERMISSION);
                lFields.Add(SecurityUsersDtQD._SO_OBJECTID, pSO_OBJECTID);
                lFields.Add(SecurityUsersDtQD._SUD_REGDATE, DateTime.Now);
                lFields.Add(SecurityUsersDtQD._SUD_REGUSER, ((LoginUserDo)Session["_SessionUser"]).LoginName);
                lFields.Add(SecurityUsersDtQD._SUD_STATUS, "A");

                lReturn = SecurityUsersDtDo.Update(lFields, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
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


        private void InterfacePageLoad()
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    //LoadNucleo();
                    LoadPessoaFuncao();
                    

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
            InterfacePageLoad();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (hidSU_ID.Value != "")
                InterfaceUpdate(decimal.Parse(hidSU_ID.Value.ToString()), 0, "A");
            else
                InterfaceInclude();
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AutDefault.aspx");
        }

        protected void grdObjects_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkPermissao = (CheckBox)e.Row.FindControl("chkPermissao");

                    if (DataBinder.Eval(e.Row.DataItem, "SUD_PERMISSION").ToString() == "1111")
                        chkPermissao.Checked = true;
                    else
                        chkPermissao.Checked = false;
                }
            }
            catch (Exception err)
            {
                (new UnknownException(err)).TratarExcecao(true);
            }
        }

        protected void ddlSU_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadSecurityObjects();
            }
            catch (Exception err)
            {
                (new UnknownException(err)).TratarExcecao(true);
            }

        }

        protected void ddlNUC_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadPessoaFuncao();
            }
            catch (Exception err)
            {
                (new UnknownException(err)).TratarExcecao(true);
            }
        }


        #endregion

        protected void grdObjects_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
}                                              
}                                                             
