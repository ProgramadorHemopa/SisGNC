using System;
using System.Data;
using System.Collections.Generic;

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
    public partial class SolicitacaoReprogramacao : BaseAutPage
    {

        #region [Variaveis]

        #endregion

        #region [Metodos]

        private void LoadUnidades()
        {
            DataTable ltable = NC_OcorrenciaDo.GetAllUnidadesGestor(LocalInstance.ConnectionInfo);
            ddlUnidadeRespResolucao.DataSource = ltable;
            ddlUnidadeRespResolucao.DataTextField = "UNIDADES";
            ddlUnidadeRespResolucao.DataValueField = "ID";
            ddlUnidadeRespResolucao.DataBind();
            ddlUnidadeRespResolucao.Items.Insert(0, new ListItem("--Selecione--", "0"));

        }


        private void InterfacePesquisa()
        {
            //Paginação do Grid                                                                               
            grdMain.AllowPaging = true;
            grdMain.PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            grdMain.PagerStyle.HorizontalAlign = HorizontalAlign.Center;

            ViewState["WRK_TABLE"] = NC_OcorrenciaDo.GetOcorrenciaSolicitacaoReprogramacaoByParametros(ddlUnidadeRespResolucao.SelectedValue.DBToDecimal(), txtOCR_NUMERO.Text, LocalInstance.ConnectionInfo);

            grdMain.DataSource = ((DataTable)ViewState["WRK_TABLE"]);
            grdMain.DataBind();
        }

        #endregion

        #region [Eventos]

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!Page.IsPostBack)
            {
                LoadUnidades();
                InterfacePesquisa();
            }
        }

        #region [Btn]

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            InterfacePesquisa();
        }

        #endregion

        #region Grid

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

                    if (e.CommandName == "Visualizar")
                    {
                        DataTable lTable = (DataTable)ViewState["WRK_TABLE"];

                        if (lTable.Rows.Count > 0)
                        {
                            Response.Redirect("RegistroNaoConformidade.aspx?OCR_ID=" + lTable.Rows[iIndice][NC_OcorrenciaQD._OCR_ID.Name].ToString());

                        }
                    }
                  
                }

            }
            catch (Exception err)
            {
                (new UnknownException(err)).TratarExcecao(true);
            }
        }

        #endregion

        #endregion

    }
}