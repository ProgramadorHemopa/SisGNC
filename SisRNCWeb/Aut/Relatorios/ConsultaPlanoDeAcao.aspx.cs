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
    public partial class ConsultaPlanoDeAcao : BaseAutPage
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
        
        private void LoadGrid()
        {
            try
            {

                DataTable lTable = NC_AcoesDo.GetRelatorioNC_AcoesByUnidadeId(ddlUnidadeRespResolucao.SelectedValue.DBToDecimal(), txtNumeroOcorrencia.Text.Trim(), LocalInstance.ConnectionInfo);

                Session["WRK_TABLE"] = lTable;
                grdMain.DataSource = (DataTable)Session["WRK_TABLE"];
                grdMain.DataBind();

                if (lTable.Rows.Count > 0)
                {
                    string lParametros = "";
                    
                    if(ddlUnidadeRespResolucao.SelectedValue != "0")
                        lParametros += "  Unidade: " + ddlUnidadeRespResolucao.SelectedItem.Text;

                    if (txtNumeroOcorrencia.Text != "")
                        lParametros += "  Nº Ocorrência: " + txtNumeroOcorrencia.Text;

                    litTotal.Text = lTable.Rows.Count.ToString();
                    lnkPrint.Visible = true;
                    lnkPrint.NavigateUrl = "~/Aut/Reports/LoadReportRV.aspx?ReportName=RelatorioPlanoDeAcao.rdlc&Parametros=" + lParametros;

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
            }
        }

        #region [Btn]

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (ddlUnidadeRespResolucao.SelectedValue == "" && txtNumeroOcorrencia.Text == "")
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

        #endregion

        #endregion

    }
}