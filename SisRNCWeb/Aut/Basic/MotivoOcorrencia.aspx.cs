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
    public partial class MotivoOcorrencia : BaseAutPage
    {
        #region LoadInfo


        #endregion

        #region Private Methods

        private void Clear()
        {
            txtMTV_DESCRICAO.Text = "";
        }


        private void InterfaceInclude()
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                OperationResult lReturn = new OperationResult();

                lFields.Add(NC_MotivoOcorrenciaQD._MTV_DESCRICAO, txtMTV_DESCRICAO.Text);
                lFields.Add(NC_MotivoOcorrenciaQD._MTV_REGDATE, DateTime.Now);
                lFields.Add(NC_MotivoOcorrenciaQD._MTV_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_MotivoOcorrenciaQD._MTV_STATUS, LocalInstance.StatusAtivo);

                lReturn = NC_MotivoOcorrenciaDo.Insert(lFields, LocalInstance.ConnectionInfo);

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

        private void InterfaceUpdate(decimal pMTV_ID, string pStatus)
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                OperationResult lReturn = new OperationResult();

                lFields.Add(NC_MotivoOcorrenciaQD._MTV_ID, pMTV_ID);

                if (pStatus == "A")
                {
                    lFields.Add(NC_MotivoOcorrenciaQD._MTV_DESCRICAO, txtMTV_DESCRICAO.Text);
                }

                lFields.Add(NC_MotivoOcorrenciaQD._MTV_REGDATE, DateTime.Now);
                lFields.Add(NC_MotivoOcorrenciaQD._MTV_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_MotivoOcorrenciaQD._MTV_STATUS, pStatus);

                lReturn = NC_MotivoOcorrenciaDo.Update(lFields, LocalInstance.ConnectionInfo);

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

            ViewState["WRK_TABLE"] = NC_MotivoOcorrenciaDo.GetAllNC_MotivoOcorrencia(LocalInstance.ConnectionInfo);
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
            if (hidMTV_ID.Value != "")
                InterfaceUpdate(decimal.Parse(hidMTV_ID.Value.ToString()), "A");
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
                            txtMTV_DESCRICAO.Text = lTable.Rows[iIndice][NC_MotivoOcorrenciaQD._MTV_DESCRICAO.Name].ToString();

                            hidMTV_ID.Value = lTable.Rows[iIndice][NC_MotivoOcorrenciaQD._MTV_ID.Name].ToString();
                        }
                    }
                    else if (e.CommandName == "Excluir")
                    {
                        DataTable lTable = (DataTable)ViewState["WRK_TABLE"];

                        InterfaceUpdate(decimal.Parse(lTable.Rows[iIndice][NC_MotivoOcorrenciaQD._MTV_ID.Name].ToString()), "I");
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
