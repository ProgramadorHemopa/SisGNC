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
    public partial class ConsultaOcorrencia : BaseAutPage
    {

        #region [Variaveis]

        #endregion

        #region [Metodos]

        private void LoadUnidades()
        {
            DataTable ltable = NC_OcorrenciaDo.GetAllUnidade(LocalInstance.ConnectionInfo);

            ddlUnidadeRespResolucao.DataSource = ltable;
            ddlUnidadeRespResolucao.DataTextField = "UNIDADES";
            ddlUnidadeRespResolucao.DataValueField = "ID";
            ddlUnidadeRespResolucao.DataBind();
            ddlUnidadeRespResolucao.Items.Insert(0, new ListItem("--Selecione--", "0"));

        }


        private void LoadMotivoOcorrencia()
        {
            DataTable ltable = NC_OcorrenciaDo.GetAllMotivo_Ocorrencia(LocalInstance.ConnectionInfo);

            ddlMTV_ID.DataSource = ltable;
            ddlMTV_ID.DataTextField = "MTV_DESCRICAO";
            ddlMTV_ID.DataValueField = "MTV_ID";
            ddlMTV_ID.DataBind();
            ddlMTV_ID.Items.Insert(0, new ListItem("--Selecione--", "0"));

        }

        private void LoadSituacaoOcorrencia()
        {
            DataTable ltable = NC_SituacaoOcorrenciaDo.GetAllNC_SituacaoOcorrencia(LocalInstance.ConnectionInfo);
            ddlSTCOCR_ID.DataSource = ltable;
            ddlSTCOCR_ID.DataTextField = "STCOCR_DESCRICAO";
            ddlSTCOCR_ID.DataValueField = "STCOCR_ID";
            ddlSTCOCR_ID.DataBind();
            ddlSTCOCR_ID.Items.Insert(0, new ListItem("--Selecione--", "0"));
            ddlSTCOCR_ID.Items.Insert(9, new ListItem(" CONCLUÍDA-NÃO EFICAZ", "99"));

        }

        private void LoadGrid()
        {
            try
            {
                //Modificado por Angelo Matos em 24072020
                //Backup 24072020
                //DataTable lTable = NC_OcorrenciaDo.GetRelatorioOcorrenciaByPeriodoUnidade(txtDataInicio.Text.DBToDateTime().ToString("yyyy-MM-dd"), txtDataFim.Text.DBToDateTime().ToString("yyyy-MM-dd"),
                //    ddlUnidadeRespResolucao.SelectedValue.DBToDecimal(), ddlMTV_ID.SelectedValue.DBToDecimal(), ddlSTCOCR_ID.SelectedValue.DBToDecimal(), LocalInstance.ConnectionInfo);
                DataTable lTable = NC_OcorrenciaDo.GetRelatorioOcorrenciaByPeriodoUnidadeRegDate(txtDataInicio.Text.DBToDateTime().ToString("yyyy-MM-dd"), txtDataFim.Text.DBToDateTime().ToString("yyyy-MM-dd"),
                    ddlUnidadeRespResolucao.SelectedValue.DBToDecimal(), ddlMTV_ID.SelectedValue.DBToDecimal(), ddlSTCOCR_ID.SelectedValue.DBToDecimal(), LocalInstance.ConnectionInfo);

                Session["WRK_TABLE"] = lTable;
                grdMain.DataSource = (DataTable)Session["WRK_TABLE"];
                grdMain.DataBind();

                if (lTable.Rows.Count > 0)
                {
                    string lParametros = "";

                    if (txtDataInicio.Text != "" || txtDataFim.Text != "")
                        lParametros += "  PERÍODO DE REFERÊNCIA " + txtDataInicio.Text + " a " + txtDataFim.Text;

                    if (ddlUnidadeRespResolucao.SelectedValue.DBToDecimal() != 0)
                        lParametros += " Unidade Responsável: " + ddlUnidadeRespResolucao.SelectedItem.Text;

                    litTotal.Text = lTable.Rows.Count.ToString();
                    lnkPrint.Visible = true;
                    lnkPrint.NavigateUrl = "~/Aut/Reports/LoadReportRV.aspx?ReportName=RelatorioOcorrencia.rdlc&Parametros=" + lParametros;

                }
                else
                {
                    litTotal.Text = "0";
                    Session.Remove("WRK_TABLE");

                    lnkPrint.Visible = false;
                }
            }
            catch (Exception err)
            {
                (new UnknownException(err)).TratarExcecao(true);
            }
        }

        #endregion

        #region [Eventos]

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!Page.IsPostBack)
            {
                LoadUnidades();
                LoadSituacaoOcorrencia();
                LoadMotivoOcorrencia();
            }
        }

        #region [Btn]

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtDataInicio.Text == "" && txtDataFim.Text == "" && ddlUnidadeRespResolucao.SelectedValue == "0" && ddlSTCOCR_ID.SelectedValue == "0" && ddlMTV_ID.SelectedValue == "0")
            {
                MessageBox1.wuc_ShowMessage("Informe um parâmetro de pesquisa.", 3);
            }
            else
            {
                LoadGrid();
            }
        }

        #endregion

        #region [Grd]

        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                ((GridView)sender).PageIndex = e.NewPageIndex;
                ((GridView)sender).DataSource = (DataTable)Session["WRK_TABLE"];
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
                        DataTable lTable = (DataTable)Session["WRK_TABLE"];

                        if (lTable.Rows.Count > 0)
                        {
                            if (((LoginUserDo)Session["_SessionUser"]).LoginName == "ADMIN.GNC")
                                Response.Redirect("~/Aut/Page/RegistroNaoConformidadeAdmin.aspx?OCR_ID=" + lTable.Rows[iIndice][NC_OcorrenciaQD._OCR_ID.Name].ToString());
                            else
                                Response.Redirect("~/Aut/Page/RegistroNaoConformidade.aspx?OCR_ID=" + lTable.Rows[iIndice][NC_OcorrenciaQD._OCR_ID.Name].ToString());

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