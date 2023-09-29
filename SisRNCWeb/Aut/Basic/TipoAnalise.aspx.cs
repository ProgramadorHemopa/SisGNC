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
    public partial class TipoAnalise : BaseAutPage
    {
        #region LoadInfo


        #endregion

        #region Private Methods

        private void Clear()
        {
            txtTPANL_DESCRICAO.Text = "";
        }


        private void InterfaceInclude()
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                OperationResult lReturn = new OperationResult();

                lFields.Add(NC_TipoAnaliseQD._TPANL_DESCRICAO, txtTPANL_DESCRICAO.Text);
                lFields.Add(NC_TipoAnaliseQD._TPANL_REGDATE, DateTime.Now);
                lFields.Add(NC_TipoAnaliseQD._TPANL_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_TipoAnaliseQD._TPANL_STATUS, LocalInstance.StatusAtivo);

                lReturn = NC_TipoAnaliseDo.Insert(lFields, LocalInstance.ConnectionInfo);

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

        private void InterfaceUpdate(decimal pTPANL_ID, string pStatus)
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                OperationResult lReturn = new OperationResult();

                lFields.Add(NC_TipoAnaliseQD._TPANL_ID, pTPANL_ID);

                if (pStatus == "A")
                {
                    lFields.Add(NC_TipoAnaliseQD._TPANL_DESCRICAO, txtTPANL_DESCRICAO.Text);
                }

                lFields.Add(NC_TipoAnaliseQD._TPANL_REGDATE, DateTime.Now);
                lFields.Add(NC_TipoAnaliseQD._TPANL_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_TipoAnaliseQD._TPANL_STATUS, pStatus);

                lReturn = NC_TipoAnaliseDo.Update(lFields, LocalInstance.ConnectionInfo);

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
            grdMain.PagerStyle.HorizontalAlign = HorizontalAlign.Center;

            ViewState["WRK_TABLE"] = NC_TipoAnaliseDo.GetAllNC_TipoAnalise(LocalInstance.ConnectionInfo);
            grdMain.DataSource = ((DataTable)ViewState["WRK_TABLE"]);
            grdMain.DataBind();
        }


        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            InterfacePageLoad();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (hidTPANL_ID.Value != "")
                InterfaceUpdate(decimal.Parse(hidTPANL_ID.Value.ToString()), "A");
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
                            txtTPANL_DESCRICAO.Text = lTable.Rows[iIndice][NC_TipoAnaliseQD._TPANL_DESCRICAO.Name].ToString();

                            hidTPANL_ID.Value = lTable.Rows[iIndice][NC_TipoAnaliseQD._TPANL_ID.Name].ToString();
                        }
                    }
                    else if (e.CommandName == "Excluir")
                    {
                        DataTable lTable = (DataTable)ViewState["WRK_TABLE"];

                        InterfaceUpdate(decimal.Parse(lTable.Rows[iIndice][NC_TipoAnaliseQD._TPANL_ID.Name].ToString()), "I");
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
