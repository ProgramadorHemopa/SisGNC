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
    public partial class SecurityObjects : BaseAutPage
    {
        #region LoadInfo

        private void LoadSecurityObjects()
        {
            ddlSO_PARENT.DataSource = SecurityObjectsDo.GetAllSecurityObjects(LocalInstance.ConnectionInfo);
            ddlSO_PARENT.DataTextField = "SO_DESC";
            ddlSO_PARENT.DataValueField = "SO_OBJECTID";
            ddlSO_PARENT.DataBind();
            ddlSO_PARENT.Items.Insert(0, new ListItem("", "-1"));
        }

        #endregion

        #region Private Methods

        private void Clear()
        {
            ddlSO_PARENT.SelectedIndex = 0;
            txtSO_TYPE.Text = "";
            txtSO_DESC.Text = "";
        }


        private void InterfaceInclude()
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                OperationResult lReturn = new OperationResult();
                
                if(ddlSO_PARENT.SelectedValue != "-1")
                    lFields.Add(SecurityObjectsQD._SO_PARENT, decimal.Parse(ddlSO_PARENT.SelectedValue));
                lFields.Add(SecurityObjectsQD._SO_TYPE, txtSO_TYPE.Text);
                lFields.Add(SecurityObjectsQD._SO_DESC, txtSO_DESC.Text);
                lFields.Add(SecurityObjectsQD._SO_REGDATE, DateTime.Now);
                lFields.Add(SecurityObjectsQD._SO_REGUSER, "USER");
                lFields.Add(SecurityObjectsQD._SO_STATUS, "A");

                lReturn = SecurityObjectsDo.Insert(lFields, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    MessageBox1.wuc_ShowMessage("Registro salvo com sucesso.", 1);
                    LoadGrid();
                    LoadSecurityObjects();
                    Clear();
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

        private void InterfaceUpdate(decimal pSO_OBJECTID, string pStatus)
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                OperationResult lReturn = new OperationResult();

                lFields.Add(SecurityObjectsQD._SO_OBJECTID, pSO_OBJECTID);

                if (pStatus == "A")
                {
                    lFields.Add(SecurityObjectsQD._SO_PARENT, decimal.Parse(ddlSO_PARENT.SelectedValue));
                    lFields.Add(SecurityObjectsQD._SO_TYPE, txtSO_TYPE.Text);
                    lFields.Add(SecurityObjectsQD._SO_DESC, txtSO_DESC.Text);
                }

                lFields.Add(SecurityObjectsQD._SO_REGDATE, DateTime.Now);
                lFields.Add(SecurityObjectsQD._SO_REGUSER, "USER");
                lFields.Add(SecurityObjectsQD._SO_STATUS, pStatus);

                lReturn = SecurityObjectsDo.Update(lFields, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    MessageBox1.wuc_ShowMessage("Registro salvo com sucesso.", 1);
                    LoadGrid();
                    Clear();
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
                    LoadGrid();
                    LoadSecurityObjects();

                    MSG001.Visible = false;
                    MSG002.Visible = false;
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


        private void LoadGrid()
        {
            //Paginação do Grid                                                                               
            grdMain.AllowPaging = true;
            grdMain.PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            grdMain.PagerStyle.CssClass = "dgHeader";
            grdMain.PagerStyle.HorizontalAlign = HorizontalAlign.Center;

            ViewState["WRK_TABLE"] = SecurityObjectsDo.GetAllSecurityObjects(LocalInstance.ConnectionInfo);
            grdMain.DataSource = ((DataTable)ViewState["WRK_TABLE"]);
            grdMain.DataBind();
        }


        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            this.HasMenu = "menuCadastro";
            base.Page_Load(sender, e);
            this.MasterPage.PageH3 = "Cadastro de Páginas Menu";
            InterfacePageLoad();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (hidSO_OBJECTID.Value != "")
                InterfaceUpdate(decimal.Parse(hidSO_OBJECTID.Value.ToString()), "A");
            else
                InterfaceInclude();
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AutDefault.aspx");
        }

        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                ((GridView)sender).PageIndex = e.NewPageIndex;
                ((GridView)sender).DataSource = ((DataTable)ViewState["WRK_TABLE"]);
                ((GridView)sender).DataBind();
            }
            catch (Exception err)
            {
                (new UnknownException(err)).TratarExcecao(true);
            }
        }

        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName != "Page") //Paginação                                                                          
                {
                    int iIndice = (((GridView)sender).PageIndex * ((GridView)sender).PageSize) + int.Parse(e.CommandArgument.ToString());

                    if (e.CommandName == "Alterar")
                    {
                        DataTable lTable = (DataTable)ViewState["WRK_TABLE"];

                        if (lTable.Rows.Count > 0)
                        {
                            ddlSO_PARENT.SelectedValue = lTable.Rows[iIndice][SecurityObjectsQD._SO_PARENT.Name].ToString();
                            txtSO_TYPE.Text = lTable.Rows[iIndice][SecurityObjectsQD._SO_TYPE.Name].ToString();
                            txtSO_DESC.Text = lTable.Rows[iIndice][SecurityObjectsQD._SO_DESC.Name].ToString();

                            hidSO_OBJECTID.Value = lTable.Rows[iIndice][SecurityObjectsQD._SO_OBJECTID.Name].ToString();
                        }
                    }
                    else if (e.CommandName == "Excluir")
                    {
                        DataTable lTable = (DataTable)ViewState["WRK_TABLE"];

                        InterfaceUpdate(decimal.Parse(lTable.Rows[iIndice][SecurityObjectsQD._SO_OBJECTID.Name].ToString()), "I");
                    }
                }

            }
            catch (Exception err)
            {
                (new UnknownException(err)).TratarExcecao(true);
            }
        }

        #endregion

    }                                         
}                                                             
