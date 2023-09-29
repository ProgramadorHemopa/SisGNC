using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

public partial class UserControl_WuIconesInformativosTop : System.Web.UI.UserControl
{
    #region [Variaveis]

    public string gQuantidadeAtendimento = "";
    public string lTotalAtendimento = "";
    public string lOcorrenciaPendente = "";
    public string lOcorrenciaAcompanhamento = "";
    public string lOcorrenciaReprogramacao = "";

    #endregion

    
    #region [Eventos]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (((LoginUserDo)Session["_SessionUser"]) != null)
        {

            lblNomePerfil.Text = ((LoginUserDo)Session["_SessionUser"]).NOME;

            //Verifica pendencias
            DataTable lTable;


            //Pendencias NQ
            if(((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID == 60)
            {
                lnkReprogramacao.Visible = true;
                DataTable lTableReprogramacao = NC_OcorrenciaDo.GetOcorrenciaSolicitacaoReprogramacaoByParametros(0, "", LocalInstance.ConnectionInfo);
                lOcorrenciaReprogramacao = lTableReprogramacao.Rows.Count.ToString();


                lTable = NC_OcorrenciaDo.GetCountOcorrenciaByResponsavelUnidades(0, ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID, LocalInstance.ConnectionInfo);
                lOcorrenciaPendente = lTable.Rows[0]["QTD"].ToString();
                if (lOcorrenciaPendente != "0")
                    lnkPendencias.HRef = "~/Aut/Page/ConsultaRNC.aspx?UNIDADENQ=" + ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID.ToString();

                DataTable lTableAcomp = NC_OcorrenciaDo.GetCountOcorrenciaByResponsavelUnidades(0, 99, LocalInstance.ConnectionInfo);
                lOcorrenciaAcompanhamento = lTableAcomp.Rows[0]["QTD"].ToString();
                if (lOcorrenciaAcompanhamento != "0")
                    lnkAcompanhamento.HRef = "~/Aut/Page/ConsultaRNC.aspx?UNIDADENQ=" + 99.ToString();
            }
            else
            {
                lnkReprogramacao.Visible = false;
                if (LoadRespUnidade(((LoginUserDo)Session["_SessionUser"]).MATRICULA))
                {
                    lTable = NC_OcorrenciaDo.GetCountOcorrenciaByResponsavelUnidades(((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID, 0, LocalInstance.ConnectionInfo);
                    lOcorrenciaPendente = lTable.Rows[0]["QTD"].ToString();
                    if (lOcorrenciaPendente != "0")
                        lnkPendencias.HRef = "~/Aut/Page/ConsultaRNC.aspx?UNIDADERESP=" + ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID.ToString();
                }
            }
        }
    }


    private bool LoadRespUnidade(decimal pMATRICULA)
    {
        bool lValido = false;

        DataTable ltable = NC_OcorrenciaDo.GetUnidadeRespByMatricula(pMATRICULA, LocalInstance.ConnectionInfo);
        if (ltable.Rows.Count > 0)
        {
            lValido = true;
        }

        return lValido;
    }


    #endregion
}