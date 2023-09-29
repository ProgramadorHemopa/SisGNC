using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

using HMP.DataObjects.SisRNCWeb.QueryDictionaries;
using HMP.DataObjects.SisRNCWeb;
using HMP.WebInterface.SisRNCWeb.Www.DataAccess;
using APB.Mercury.Exceptions;



namespace HMP.WebInterface.SisRNCWeb.Www.Pages
{
    public partial class ConsultaRNC : BaseAutPage
    {

        #region [Variaveis]

        #endregion

        #region [Metodos]

        private void LoadRespAbertura()
        {
            DataTable ltable = NC_OcorrenciaDo.GetAllGestor(LocalInstance.ConnectionInfo);
            ddlFuncionario.DataSource = ltable;
            ddlFuncionario.DataTextField = "FUNCIONARIO";
            ddlFuncionario.DataValueField = "MATRICULA_RESP";
            ddlFuncionario.DataBind();
            ddlFuncionario.Items.Insert(0, new ListItem("--Selecione--", "0"));
        }

        private void LoadTipoOcorrencia()
        {
            DataTable ltable = NC_TipoOcorrenciaDo.GetAllNC_TipoOcorrencia(LocalInstance.ConnectionInfo);
            ddlTipoOcorrencia.DataSource = ltable;
            ddlTipoOcorrencia.DataTextField = "TPOCR_DESCRICAO";
            ddlTipoOcorrencia.DataValueField = "TPOCR_ID";
            ddlTipoOcorrencia.DataBind();
            ddlTipoOcorrencia.Items.Insert(0, new ListItem("--Selecione--", "0"));

        }

        private void LoadUnidades()
        {
            DataTable ltable = NC_OcorrenciaDo.GetAllUnidade(LocalInstance.ConnectionInfo);
            ddlUnidadeOcorrencia.DataSource = ltable;
            ddlUnidadeOcorrencia.DataTextField = "UNIDADES";
            ddlUnidadeOcorrencia.DataValueField = "ID";
            ddlUnidadeOcorrencia.DataBind();
            ddlUnidadeOcorrencia.Items.Insert(0, new ListItem("--Selecione--", "0"));

            ddlUnidadeRespResolucao.DataSource = ltable;
            ddlUnidadeRespResolucao.DataTextField = "UNIDADES";
            ddlUnidadeRespResolucao.DataValueField = "ID";
            ddlUnidadeRespResolucao.DataBind();
            ddlUnidadeRespResolucao.Items.Insert(0, new ListItem("--Selecione--", "0"));

        }


        private void LoadMotivosOcorrencia()
        {
            DataTable ltable = NC_MotivoOcorrenciaDo.GetAllNC_MotivoOcorrencia(LocalInstance.ConnectionInfo);
            ddlMotivoOcorrencia.DataSource = ltable;
            ddlMotivoOcorrencia.DataTextField = "MTV_DESCRICAO";
            ddlMotivoOcorrencia.DataValueField = "MTV_ID";
            ddlMotivoOcorrencia.DataBind();
            ddlMotivoOcorrencia.Items.Insert(0, new ListItem("--Selecione--", "0"));

        }

        private void LoadSituacaoOcorrencia()
        {
            DataTable ltable = NC_SituacaoOcorrenciaDo.GetAllNC_SituacaoOcorrencia(LocalInstance.ConnectionInfo);
            ddlSTCOCR_ID.DataSource = ltable;
            ddlSTCOCR_ID.DataTextField = "STCOCR_DESCRICAO";
            ddlSTCOCR_ID.DataValueField = "STCOCR_ID";
            ddlSTCOCR_ID.DataBind();
            ddlSTCOCR_ID.Items.Insert(0, new ListItem("--Selecione--", "0"));

        }

        private void InterfacePesquisa()
        {
            //Paginação do Grid                                                                               
            grdMain.AllowPaging = true;
            grdMain.PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            grdMain.PagerStyle.HorizontalAlign = HorizontalAlign.Center;

            ViewState["WRK_TABLE"] = NC_OcorrenciaDo.GetOcorrenciaByParametros(ddlFuncionario.SelectedValue.DBToDecimal(), txtOCR_DATAABERTURA.Text.DBToDateTime().ToString("yyyy-MM-dd"), ddlTipoOcorrencia.SelectedValue.DBToDecimal(),
                txtOCR_DATAOCORRENCIA.Text.DBToDateTime().ToString("yyyy-MM-dd"), ddlUnidadeOcorrencia.SelectedValue.DBToDecimal(), ddlMotivoOcorrencia.SelectedValue.DBToDecimal(), 
                txtOCR_DESCRICAO.Text, ddlUnidadeRespResolucao.SelectedValue.DBToDecimal(),
                ddlSTCOCR_ID.SelectedValue.DBToDecimal(), txtOCR_NUMERO.Text, LocalInstance.ConnectionInfo);

            grdMain.DataSource = ((DataTable)ViewState["WRK_TABLE"]);
            grdMain.DataBind();

            litTotal.Text = ((DataTable)ViewState["WRK_TABLE"]).Rows.Count.ToString();
            SetFocus(grdMain);
        }

        private void LoadGrid(decimal pUNIDADERESP, decimal pUNIDADENQ, decimal pOCR_ID)
        {
            //Paginação do Grid                                                                               
            grdMain.AllowPaging = true;
            grdMain.PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            grdMain.PagerStyle.HorizontalAlign = HorizontalAlign.Center;

            if (pUNIDADERESP != 0 || pUNIDADENQ != 0 || pOCR_ID != 0)
                ViewState["WRK_TABLE"] = NC_OcorrenciaDo.GetOcorrenciaByResponsavelUnidadeId(pUNIDADERESP, pUNIDADENQ, pOCR_ID, LocalInstance.ConnectionInfo);
            else
                ViewState["WRK_TABLE"] = NC_OcorrenciaDo.GetAllNC_Ocorrencia(LocalInstance.ConnectionInfo);

            grdMain.DataSource = ((DataTable)ViewState["WRK_TABLE"]);
            grdMain.DataBind();

            litTotal.Text = ((DataTable)ViewState["WRK_TABLE"]).Rows.Count.ToString();
        }

        #endregion

        #region [Eventos]

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            this.Page.Form.DefaultButton = btnPesquisar.UniqueID;

            if (!Page.IsPostBack)
            {
                LoadRespAbertura();
                LoadTipoOcorrencia();
                LoadUnidades();
                LoadMotivosOcorrencia();
                LoadSituacaoOcorrencia();

                if (Request["UNIDADERESP"] != null)
                {
                    LoadGrid(Request["UNIDADERESP"].DBToDecimal(), 0, 0);
                }
                else if (Request["UNIDADENQ"] != null)
                {
                    LoadGrid(0, Request["UNIDADENQ"].DBToDecimal(), 0);
                }
                else if (Request["OCR_ID"] != null)
                {
                    LoadGrid(0, 0, Request["OCR_ID"].DBToDecimal());
                }
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
                            if (((LoginUserDo)Session["_SessionUser"]).LoginName == "ADMIN.GNC")
                                Response.Redirect("RegistroNaoConformidadeAdmin.aspx?OCR_ID=" + lTable.Rows[iIndice][NC_OcorrenciaQD._OCR_ID.Name].ToString());
                            else
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