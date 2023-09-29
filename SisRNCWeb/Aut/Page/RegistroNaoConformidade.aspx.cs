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

using System.Drawing;
using System.Net.Mail;
using System.Net.Configuration;
using System.Net;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Web;


namespace HMP.WebInterface.SisRNCWeb.Www.Pages
{
    public partial class RegistroNaoConformidade : BaseAutPage//System.Web.UI.Page
    {

        #region [Variaveis]

        #endregion

        #region [Metodos]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Destinatario">1-Resp. Resolucao; 2-Resp. Abertura; 3-Resp. NQ</param>
        /// <param name="Assunto"></param>
        /// <param name="enviaMensagem"></param>
        /// <param name="pNumero"></param>
        private void EnviaMensagemEmail(decimal Destinatario, string Assunto, string enviaMensagem, string pNumero)
        {
            try
            {
                //Mensagem padrão
                if (lblOCR_NUMERO.Text == "")
                    lblOCR_NUMERO.Text = DateTime.Now.Year.ToString() + "/" + pNumero;
                string lMensagemPadrao = "A " + ddlTipoOcorrencia.SelectedItem.Text + " Nº " + lblOCR_NUMERO.Text
                    + ", Resp. Abertura: " + ddlFuncionario.SelectedItem.Text
                    + ", Resp. Resolução: " + ddlUnidadeRespResolucao.SelectedItem.Text
                    + ", " + enviaMensagem;

                string pDestinatario = "";
                pDestinatario = GetEmailResponsavel(Destinatario);

                //pDestinatario = "alexandre.melo@hemopa.pa.gov.br";

                //obtem os valores smtp do arquivo de configuração . Não vou usar estes valores estou apenas mostrando como obtê-los
                Configuration configurationFile = WebConfigurationManager.OpenWebConfiguration("~");
                MailSettingsSectionGroup mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;

                string host = mailSettings.Smtp.Network.Host;
                string password = mailSettings.Smtp.Network.Password;
                string remetente = mailSettings.Smtp.Network.UserName;
                int port = mailSettings.Smtp.Network.Port;


                // valida o email
                bool bValidaEmail = ValidaEnderecoEmail(pDestinatario);

                // Se o email não é validao retorna uma mensagem
                if (bValidaEmail == false)
                    return;

                // cria uma mensagem
                MailMessage mensagemEmail = new MailMessage(remetente, pDestinatario, Assunto, lMensagemPadrao);
                //mensagemEmail.CC.Add("andrea.vasconcelos@hemopa.pa.gov.br");
                //mensagemEmail.CC.Add("celeste.lobo@hemopa.pa.gov.br");
                mensagemEmail.CC.Add("nao.conformidade@hemopa.pa.gov.br");

                SmtpClient client = new SmtpClient(host, port);
                //Modificado por Angelo Matos em 18052020
                //client.EnableSsl = false;
                client.EnableSsl = true;

                // inclui as credenciais
                client.UseDefaultCredentials = true;
                NetworkCredential cred = new NetworkCredential(remetente, password);
                client.Credentials = cred;

                // envia a mensagem
                client.Send(mensagemEmail);

            }
            catch (Exception ex)
            {
            }
        }

        public static bool ValidaEnderecoEmail(string enderecoEmail)
        {
            try
            {
                //define a expressão regulara para validar o email
                string texto_Validar = enderecoEmail;
                Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

                // testa o email com a expressão
                if (expressaoRegex.IsMatch(texto_Validar))
                {
                    // o email é valido
                    return true;
                }
                else
                {
                    // o email é inválido
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetEmailResponsavel(decimal pResponsavel)
        {
            string lEmail = "";
            DataTable lTable;

            switch (pResponsavel.ToString())
            {
                case "1"://Resp. Resolução
                    lTable = (DataTable)ViewState["WRK_TABLE_UNIDADES"];
                    lEmail = lTable.Rows[ddlUnidadeRespResolucao.SelectedIndex - 1]["EMAIL"].ToString();
                    break;
                case "2"://Resp. Abertura
                    lTable = (DataTable)ViewState["WRK_TABLE_FUNCIONARIOS"];
                    lEmail = lTable.Rows[0]["EMAIL"].ToString();
                    break;
                case "3"://NQ
                    lTable = NC_OcorrenciaDo.GetEmailGestorUnidade(60, LocalInstance.ConnectionInfo);
                    lEmail = lTable.Rows[0]["EMAIL"].ToString();
                    break;
            }

            return lEmail;
        }

        private void LoadRespAbertura(decimal pMATRICULA)
        {
            DataTable ltable;

            if (((LoginUserDo)Session["_SessionUser"]).LoginName == "ADMIN.GNC")
            {
                ltable = NC_OcorrenciaDo.GetAllGestor(LocalInstance.ConnectionInfo);
                ViewState["WRK_TABLE_FUNCIONARIOS"] = ltable;
                ddlFuncionario.DataSource = ltable;
                ddlFuncionario.DataTextField = "UNIDADE_RESPONSAVEL";
                ddlFuncionario.DataValueField = "MATRICULA_RESP";
                ddlFuncionario.DataBind();
                ddlFuncionario.Items.Insert(0, new ListItem("--Selecione--", "0"));

            }
            else
            {
                ltable = NC_OcorrenciaDo.GetAllFuncionariosByMatricula(pMATRICULA, LocalInstance.ConnectionInfo);
                ViewState["WRK_TABLE_FUNCIONARIOS"] = ltable;
                ddlFuncionario.DataSource = ltable;
                ddlFuncionario.DataTextField = "NOME";
                ddlFuncionario.DataValueField = "MATRICULA";
                ddlFuncionario.DataBind();

            }

            /*if (pMATRICULA == 0)
                ddlFuncionario.Items.Insert(0, new ListItem(((LoginUserDo)Session["_SessionUser"]).NOME, ((LoginUserDo)Session["_SessionUser"]).MATRICULA.ToString()));
                */
        }

        private void LoadRespOcorrencia(decimal pMATRICULA)
        {
            DataTable ltable;
            ltable = NC_OcorrenciaDo.GetAllFuncionariosByMatricula(pMATRICULA, LocalInstance.ConnectionInfo);
            ViewState["WRK_TABLE_FUNCIONARIOS"] = ltable;
            ddlFuncionario.DataSource = ltable;
            ddlFuncionario.DataTextField = "NOME";
            ddlFuncionario.DataValueField = "MATRICULA";
            ddlFuncionario.DataBind();
        }

        private void LoadRespPA(decimal pMATRICULA)
        {
            DataTable ltable = NC_OcorrenciaDo.GetAllFuncionariosByMatricula(pMATRICULA, LocalInstance.ConnectionInfo);
            ddlRespPA.DataSource = ltable;
            ddlRespPA.DataTextField = "NOME";
            ddlRespPA.DataValueField = "MATRICULA";
            ddlRespPA.DataBind();
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

        private void LoadLocalOcorrencia()
        {
            DataTable ltable = NC_OcorrenciaDo.GetAllLocalOcorrencia(LocalInstance.ConnectionInfo);

            ddlUnidadeOcorrencia.DataSource = ltable;
            ddlUnidadeOcorrencia.DataTextField = "UNIDADES";
            ddlUnidadeOcorrencia.DataValueField = "ID";
            ddlUnidadeOcorrencia.DataBind();
            ddlUnidadeOcorrencia.Items.Insert(0, new ListItem("--Selecione--", "0"));
        }

        private void LoadUnidades()
        {
            //DataTable ltable = NC_OcorrenciaDo.GetAllUnidades(LocalInstance.ConnectionInfo);
            DataTable ltable = NC_OcorrenciaDo.GetAllUnidade(LocalInstance.ConnectionInfo);

            ViewState["WRK_TABLE_UNIDADES"] = ltable;
            ddlUnidadeRespResolucao.DataSource = ltable;
            ddlUnidadeRespResolucao.DataTextField = "UNIDADES";
            ddlUnidadeRespResolucao.DataValueField = "ID";
            ddlUnidadeRespResolucao.DataBind();
            ddlUnidadeRespResolucao.Items.Insert(0, new ListItem("--Selecione--", "0"));

            ddlUnidadeAbertura.DataSource = ltable;
            ddlUnidadeAbertura.DataTextField = "UNIDADES";
            ddlUnidadeAbertura.DataValueField = "ID";
            ddlUnidadeAbertura.DataBind();

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


        private void LoadNormas()
        {
            DataTable ltable = NC_NormasDo.GetAllNC_Normas(LocalInstance.ConnectionInfo);
            chkNormas.DataSource = ltable;
            chkNormas.DataTextField = "NRM_DESCRICAO";
            chkNormas.DataValueField = "NRM_ID";
            chkNormas.DataBind();

        }

        private void LoadTipoAnalise()
        {
            DataTable ltable = NC_TipoAnaliseDo.GetAllNC_TipoAnalise(LocalInstance.ConnectionInfo);
            ddlTIPOANALISE.DataSource = ltable;
            ddlTIPOANALISE.DataTextField = "TPANL_DESCRICAO";
            ddlTIPOANALISE.DataValueField = "TPANL_ID";
            ddlTIPOANALISE.DataBind();
            ddlTIPOANALISE.Items.Insert(0, new ListItem("--Selecione--", "0"));

        }

        private void LoadDocumentosPendentes(decimal pUnidade)
        {
            DataTable lTable = NC_OcorrenciaDo.Get_DocumentoNaoConformeByUnidade(pUnidade, LocalInstance.ConnectionInfo);
            grdDoc.AllowPaging = true;
            grdDoc.PageSize = 6;
            grdDoc.PagerStyle.HorizontalAlign = HorizontalAlign.Center;

            ViewState["WRK_TABLE_DOC"] = lTable;

            grdDoc.DataSource = lTable;
            grdDoc.DataBind();

            divDocumentos.Visible = true;

            if (lTable.Rows.Count > 0)
            {
                Session["WRK_TABLE_DOC"] = lTable;
                divDocumentos.Visible = true;
                //divVerificacaoEficacia.Visible = true;
            }
            else
            {
                Session["WRK_TABLE_DOC"] = null;
                //divVerificacaoEficacia.Visible = false;
            }
        }


        private void Clear()
        {
            ddlTipoOcorrencia.SelectedValue = "0";
            txtOCR_DATAOCORRENCIA.Text = "";
            txtOCR_DESCRICAO.Text = "";
            ddlUnidadeOcorrencia.SelectedValue = "0";

            ddlMotivoOcorrencia.SelectedValue = "0";
            for (int i = 0; i < chkNormas.Items.Count; i++)
            {
                chkNormas.Items[i].Selected = false;
            }

            ddlUnidadeRespResolucao.SelectedValue = "0";
        }

        private bool ValidarInsert()
        {
            bool lValido = true;
            string lMensagem = "";

            if (ddlFuncionario.SelectedValue.DBToDecimal() == 0)
            {
                lMensagem += "-Responsável pela abertura <br/>";
                lValido = false;
            }

            if (txtOCR_DATAABERTURA.Text == "")
            {
                lMensagem += "-Data Abertura <br/>";
                lValido = false;
            }

            if (ddlTipoOcorrencia.SelectedValue.DBToDecimal() == 0)
            {
                lMensagem += "-Tipo <br/>";
                lValido = false;
            }

            if (txtOCR_DATAOCORRENCIA.Text == "")
            {
                lMensagem += "-Data Ocorrência <br/>";
                lValido = false;
            }

            if (ddlUnidadeOcorrencia.SelectedValue.DBToDecimal() == 0)
            {
                lMensagem += "-Local de Ocorrência <br/>";
                lValido = false;
            }

            if (ddlMotivoOcorrencia.SelectedValue.DBToDecimal() == 0)
            {
                lMensagem += "-Motivo <br/>";
                lValido = false;
            }


            bool lSelected = false;
            for (int i = 0; i < chkNormas.Items.Count; i++)
            {
                if (chkNormas.Items[i].Selected)
                {
                    lSelected = true;
                    break;
                }
            }

            if (!lSelected)
            {
                lMensagem += "-Requisitos não atendidos (normas) <br/>";
                lValido = false;
            }


            /*
            lSelected = false;
            //Docs -- se o motivo da ocorrencia for  
            if (ddlMotivoOcorrencia.SelectedValue.DBToDecimal() == 35 && ddlUnidadeOcorrencia.SelectedValue.DBToDecimal() != 0)
            {
                foreach (GridViewRow linha in grdDoc.Rows)
                {
                    CheckBox cbx = linha.FindControl("chkDoc") as CheckBox;

                    if (cbx != null)
                    {
                        if (cbx.Checked)
                        {
                            lSelected = true;
                            break;
                        }
                    }
                }
            }


            if (!lSelected)
            {
                lMensagem += "- Documentos <br/>";
                lValido = false;
            }

            */

            if (txtOCR_DESCRICAO.Text == "")
            {
                lMensagem += "-Descrição <br/>";
                lValido = false;
            }

            if (ddlUnidadeRespResolucao.SelectedValue.DBToDecimal() == 0)
            {
                lMensagem += "-Unid. Gerencial Responsável pela Resolução <br/>";
                lValido = false;
            }

            if (!lValido)
            {
                MessageBox1.wuc_ShowMessage("Registro não incluído. Informe os seguintes campos: <br/>" + lMensagem, 3);
            }
            else
            {
                /*if (((LoginUserDo)Session["_SessionUser"]).LoginName != "ADMIN.GNC" && txtOCR_DATAOCORRENCIA.Text.DBToDateTime() < DateTime.Now.AddDays(-3))
                {
                    MessageBox1.wuc_ShowMessage("Registro não incluído. O Prazo para abertura da ocorrência é de até 72h.", 3);
                    lValido = false;
                }
                else */
                if (txtOCR_DATAOCORRENCIA.Text.DBToDateTime() > DateTime.Now)
                {
                    MessageBox1.wuc_ShowMessage("Registro não incluído. Data da Ocorrência não pode ser maior que a data atual.", 3);
                    lValido = false;
                }
            }

            return lValido;
        }

        private bool ValidarInsertAnaliseCritica()
        {
            bool lValido = true;
            string lMensagem = "";

            if (rblANC_SITUACAO.SelectedValue == "")
            {
                lMensagem += "-Resultado <br/>";
                lValido = false;
                MessageBox1.wuc_ShowMessage("Registro não incluído. Informe os seguintes campos: <br/>" + lMensagem, 3);
                return lValido;
            }
            else if (rblANC_SITUACAO.SelectedValue == "0")//Se Aprovado
            {
                if (txtANC_DATA.Text == "")
                {
                    lMensagem += "-Data Análise <br/>";
                    lValido = false;
                }

                if (ddlTIPOANALISE.SelectedValue.DBToDecimal() == 0)
                {
                    lMensagem += "-Análise <br/>";
                    lValido = false;
                }

                if (ddlRespPA.SelectedValue.DBToDecimal() == 0)
                {
                    lMensagem += "-Responsável pelo PA <br/>";
                    lValido = false;
                }
            }
            else //Se Cancelado
            {
                if (ddlTIPOANALISE.SelectedValue.DBToDecimal() == 0)
                {
                    lMensagem += "-Análise <br/>";
                    lValido = false;
                }

                if (txtANC_JUSTIVACANCELAMENTO.Text == "")
                {
                    lMensagem += "-Justificativa <br/>";
                    lValido = false;
                }
            }


            if (!lValido)
            {
                MessageBox1.wuc_ShowMessage("Registro não incluído. Informe os seguintes campos: <br/>" + lMensagem, 3);
            }


            return lValido;
        }

        private bool ValidarInsertCausaEfeito()
        {
            bool lValido = true;
            string lMensagem = "";

            bool lSelected = false;

            if (rblAnaliseCausa.SelectedValue == "0")
            {
                for (int i = 0; i < chkANCE_DIAGRAMA.Items.Count; i++)
                {
                    if (chkANCE_DIAGRAMA.Items[i].Selected)
                    {
                        lSelected = true;

                        switch (i.ToString())
                        {
                            case "0":
                                if (txtMedida.Text == "")
                                {
                                    lMensagem += "-Descrição Medida <br/>";
                                    lValido = false;
                                }
                                break;
                            case "1":
                                if (txtMaoDeObra.Text == "")
                                {
                                    lMensagem += "-Descrição Mão de Obra <br/>";
                                    lValido = false;
                                }
                                break;
                            case "2":
                                if (txtMetodo.Text == "")
                                {
                                    lMensagem += "-Descrição Método <br/>";
                                    lValido = false;
                                }
                                break;
                            case "3":
                                if (txtMeioAmbiente.Text == "")
                                {
                                    lMensagem += "-Descrição Meio Ambiente <br/>";
                                    lValido = false;
                                }
                                break;
                            case "4":
                                if (txtMaquinas.Text == "")
                                {
                                    lMensagem += "-Descrição Máquinas <br/>";
                                    lValido = false;
                                }
                                break;
                            case "5":
                                if (txtMateriaPrima.Text == "")
                                {
                                    lMensagem += "-Descrição Matéria Prima <br/>";
                                    lValido = false;
                                }
                                break;
                            case "6":
                                if (txtGestao.Text == "")
                                {
                                    lMensagem += "-Descrição Gestão <br/>";
                                    lValido = false;
                                }
                                break;
                        }
                    }
                }

                if (!lSelected)
                {
                    lMensagem += "-Diagrama <br/>";
                    lValido = false;
                }
            }
            else
            {
                if (!fuCausaEfeito.HasFile)
                {
                    lMensagem += "-Arquivo <br/>";
                    lValido = false;
                }
            }

            if (!lValido)
            {
                MessageBox2.wuc_ShowMessage("Registro não incluído. Informe os seguintes campos: <br/>" + lMensagem, 3);
            }


            return lValido;
        }

        private bool ValidarInsertVerificacaoEficacia()
        {
            bool lValido = true;
            string lMensagem = "";

            if (txtVRFEFC_DATA.Text == "")
            {
                lMensagem += "-Data Verificação <br/>";
                lValido = false;
            }

            if (rblVRFEFC_SITUACAO.SelectedValue == "")
            {
                lMensagem += "-Situação <br/>";
                lValido = false;
            }

            if (txtVRFEFC_OBSERVACAO.Text == "")
            {
                lMensagem += "-Observação <br/>";
                lValido = false;
            }

            if (!lValido)
            {
                MessageBox1.wuc_ShowMessage("Registro não incluído. Informe os seguintes campos: <br/>" + lMensagem, 3);
            }


            return lValido;
        }


        private void InterfaceInclude()
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                DataFieldCollection lFieldsNormas = new DataFieldCollection();
                DataFieldCollection lFieldsDocs = new DataFieldCollection();
                List<DataFieldCollection> lListNormas = new List<DataFieldCollection>();
                DataFieldCollection lFieldsAnexo = new DataFieldCollection();
                List<DataFieldCollection> lListAnexo = new List<DataFieldCollection>();
                List<DataFieldCollection> lListDocs = new List<DataFieldCollection>();


                if (!ValidarInsert())
                    return;

                OperationResult lReturn = new OperationResult();

                if (trNumeroAntigo.Visible && txtOCR_NUMEROANTIGO.Text != "")
                    lFields.Add(NC_OcorrenciaQD._OCR_NUMEROANTIGO, " (" + txtOCR_NUMEROANTIGO.Text + ")");

                lFields.Add(NC_OcorrenciaQD._MATRICULA_RESPABERTURA, ddlFuncionario.SelectedValue);
                lFields.Add(NC_OcorrenciaQD._OCR_DATAABERTURA, txtOCR_DATAABERTURA.Text.DBToDateTime());
                lFields.Add(NC_OcorrenciaQD._TPOCR_ID, ddlTipoOcorrencia.SelectedValue);
                lFields.Add(NC_OcorrenciaQD._OCR_DATAOCORRENCIA, txtOCR_DATAOCORRENCIA.Text.DBToDateTime());
                lFields.Add(NC_OcorrenciaQD._UNIDADE_LOCALOCORRENCIA, ddlUnidadeOcorrencia.SelectedValue);
                lFields.Add(NC_OcorrenciaQD._MTV_ID, ddlMotivoOcorrencia.SelectedValue);
                lFields.Add(NC_OcorrenciaQD._OCR_DESCRICAO, txtOCR_DESCRICAO.Text);
                lFields.Add(NC_OcorrenciaQD._UNIDADE_RESPRESOLUCAO, ddlUnidadeRespResolucao.SelectedValue);
                lFields.Add(NC_OcorrenciaQD._UNIDADE_ABERTURA, ddlUnidadeAbertura.SelectedValue);
                lFields.Add(NC_OcorrenciaQD._STCOCR_ID, (decimal)SituacaoOcorrencia.Nova);
                lFields.Add(NC_OcorrenciaQD._OCR_REGDATE, DateTime.Now);
                lFields.Add(NC_OcorrenciaQD._OCR_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_OcorrenciaQD._OCR_STATUS, LocalInstance.StatusAtivo);

                /*
                //Docs -- se o motivo da ocorrencia for  
                if (ddlMotivoOcorrencia.SelectedValue.DBToDecimal() == 35 && ddlUnidadeOcorrencia.SelectedValue.DBToDecimal() != 0)
                {
                    for (int i = 0; i < grdDoc.Rows.Count; i++)
                    {
                        GridViewRow row = grdDoc.Rows[i];
                        CheckBox cbx = grdDoc.Rows[i].FindControl("chkDoc") as CheckBox;

                        if (cbx != null)
                        {
                            if (cbx.Checked)
                            {
                                //lFieldsDocs.Add(NC_OcorrenciaxDocumentoQD._DOC_ID, grdDoc.Rows[i].FindControl("DOC_ID").DBToDecimal());
                                lFieldsDocs.Add(NC_OcorrenciaxDocumentoQD._DOC_ID, row.Cells[1].Text);
                                lFieldsDocs.Add(NC_OcorrenciaxDocumentoQD._OCRDOC_REGDATE, DateTime.Now);
                                lFieldsDocs.Add(NC_OcorrenciaxDocumentoQD._OCRDOC_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                                lFieldsDocs.Add(NC_OcorrenciaxDocumentoQD._OCRDOC_STATUS, LocalInstance.StatusAtivo);
                                lListDocs.Add(lFieldsDocs);
                            }
                        }
                    }
                }
                */

                //normas
                for (int i = 0; i < chkNormas.Items.Count; i++)
                {
                    if (chkNormas.Items[i].Selected)
                    {
                        lFieldsNormas = new DataFieldCollection();
                        lFieldsNormas.Add(NC_NormasxOcorrenciaQD._NRM_ID, chkNormas.Items[i].Value);
                        lFieldsNormas.Add(NC_NormasxOcorrenciaQD._NRMOCR_REGDATE, DateTime.Now);
                        lFieldsNormas.Add(NC_NormasxOcorrenciaQD._NRMOCR_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                        lFieldsNormas.Add(NC_NormasxOcorrenciaQD._NRMOCR_STATUS, LocalInstance.StatusAtivo);
                        lListNormas.Add(lFieldsNormas);
                    }
                }

                if (fuAnexo.HasFile && fuAnexo.FileName != "")
                {
                    if (fuAnexo.FileBytes.Length > 0)
                    {
                        lFieldsAnexo.Add(NC_AnexoOcorrenciaQD._ANXOCR_DESCRICAO, fuAnexo.FileName);
                        lFieldsAnexo.Add(NC_AnexoOcorrenciaQD._ANXOCR_ARQUIVO, fuAnexo.FileBytes);
                        lFieldsAnexo.Add(NC_AnexoOcorrenciaQD._ANXOCR_REGDATE, DateTime.Now);
                        lFieldsAnexo.Add(NC_AnexoOcorrenciaQD._ANXOCR_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                        lFieldsAnexo.Add(NC_AnexoOcorrenciaQD._ANXOCR_STATUS, LocalInstance.StatusAtivo);

                        lListAnexo.Add(lFieldsAnexo);
                    }
                }

                lReturn = NC_OcorrenciaDo.Insert(lFields, lListNormas,lListDocs, lListAnexo, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    EnviaMensagemEmail(1, "Gestão de Não Conformidade", " Foi aberta para sua unidade. \r\r Faça a Ação Imediata/Remoção do Sintoma. \r\r\r Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/ConsultaRNC.aspx?OCR_ID=" + lReturn.SequenceControl.ToString(), lReturn.SequenceControl.ToString());
                    MessageBox1.wuc_ShowMessage("Registro salvo com sucesso.", "ConsultaRNC.aspx?OCR_ID=" + lReturn.SequenceControl.ToString(), 1);
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

        private void InterfaceDelete(decimal pOCR_ID)
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                DataFieldCollection lFieldsNormas = new DataFieldCollection();
                DataFieldCollection lFieldsAnexo = new DataFieldCollection();


                OperationResult lReturn = new OperationResult();


                lFields.Add(NC_OcorrenciaQD._OCR_ID, pOCR_ID);
                lFields.Add(NC_OcorrenciaQD._OCR_REGDATE, DateTime.Now);
                lFields.Add(NC_OcorrenciaQD._OCR_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_OcorrenciaQD._OCR_STATUS, LocalInstance.StatusInativo);

                //normas

                lFieldsNormas.Add(NC_NormasxOcorrenciaQD._OCR_ID, pOCR_ID);
                lFieldsNormas.Add(NC_NormasxOcorrenciaQD._NRMOCR_REGDATE, DateTime.Now);
                lFieldsNormas.Add(NC_NormasxOcorrenciaQD._NRMOCR_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsNormas.Add(NC_NormasxOcorrenciaQD._NRMOCR_STATUS, LocalInstance.StatusInativo);

                //Anexos
                lFieldsAnexo.Add(NC_AnexoOcorrenciaQD._OCR_ID, pOCR_ID);
                lFieldsAnexo.Add(NC_AnexoOcorrenciaQD._ANXOCR_REGDATE, DateTime.Now);
                lFieldsAnexo.Add(NC_AnexoOcorrenciaQD._ANXOCR_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsAnexo.Add(NC_AnexoOcorrenciaQD._ANXOCR_STATUS, LocalInstance.StatusAtivo);





                lReturn = NC_OcorrenciaDo.Delete(lFields, lFieldsNormas, lFieldsAnexo, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    MessageBox1.wuc_ShowMessage("Registro excluído com sucesso.", "ConsultaRNC.aspx", 1);

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


        private void InterfaceIncludeAcaoImediata()
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                DataFieldCollection lFieldsOcorrencia = new DataFieldCollection();

                if (txtSNTAC_DESCRICAO.Text == "")
                {
                    MessageBox1.wuc_ShowMessage("Informe uma descrição.", 3);
                    return;
                }


                OperationResult lReturn = new OperationResult();

                //Atualizar Situação da Ocorrencia
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_ID, hidOCR_ID.Value.DBToDecimal());
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._STCOCR_ID, (decimal)SituacaoOcorrencia.EmAnaliseCritica);
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_REGDATE, DateTime.Now);
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_STATUS, LocalInstance.StatusAtivo);

                lFields.Add(NC_SintomasAcaoQD._OCR_ID, hidOCR_ID.Value.DBToDecimal());
                lFields.Add(NC_SintomasAcaoQD._SNTAC_DESCRICAO, txtSNTAC_DESCRICAO.Text);
                lFields.Add(NC_SintomasAcaoQD._SNTAC_DATA, txtSNTAC_DATA.Text.DBToDateTime());
                lFields.Add(NC_SintomasAcaoQD._SNTAC_REGDATE, DateTime.Now);
                lFields.Add(NC_SintomasAcaoQD._SNTAC_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_SintomasAcaoQD._SNTAC_STATUS, LocalInstance.StatusAtivo);


                lReturn = NC_SintomasAcaoDo.Insert(lFields, lFieldsOcorrencia, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    EnviaMensagemEmail(3, "Gestão de Não Conformidade", " Foi aberta. \r\r Faça a Análise Crítica. \r\r\r Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, "");
                    MessageBox1.wuc_ShowMessage("Ação Imediata/Remoção do Sintoma salva com sucesso.", "ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, 1);
                    btnCancelarAcaoImediata.Visible = false;
                    btnOkAcaoImediata.Visible = false;
                    btnAcaoImediata.Visible = false;

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


        private void InterfaceUpdateAcaoImediata(decimal pSNTAC_ID)
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                DataFieldCollection lFieldsOcorrencia = new DataFieldCollection();

                if (txtSNTAC_DESCRICAO.Text == "")
                {
                    MessageBox1.wuc_ShowMessage("Informe uma descrição.", 3);
                    return;
                }

                OperationResult lReturn = new OperationResult();

                //Atualizar Situação da Ocorrencia
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_ID, hidOCR_ID.Value.DBToDecimal());
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._STCOCR_ID, (decimal)SituacaoOcorrencia.EmAnaliseCritica);
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_REGDATE, DateTime.Now);
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_STATUS, LocalInstance.StatusAtivo);

                lFields.Add(NC_SintomasAcaoQD._SNTAC_ID, pSNTAC_ID);
                lFields.Add(NC_SintomasAcaoQD._SNTAC_DESCRICAO, txtSNTAC_DESCRICAO.Text);
                lFields.Add(NC_SintomasAcaoQD._SNTAC_DATA, txtSNTAC_DATA.Text.DBToDateTime());
                lFields.Add(NC_SintomasAcaoQD._SNTAC_REGDATE, DateTime.Now);
                lFields.Add(NC_SintomasAcaoQD._SNTAC_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_SintomasAcaoQD._SNTAC_STATUS, LocalInstance.StatusAtivo);

                lReturn = NC_SintomasAcaoDo.Update(lFields, lFieldsOcorrencia, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    EnviaMensagemEmail(3, "Gestão de Não Conformidade", " Foi aberta. \r\r Faça a Análise Crítica. \r\r\r Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, "");
                    MessageBox1.wuc_ShowMessage("Ação Imediata/Remoção do Sintoma salva com sucesso.", "ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, 1);
                    btnCancelarAcaoImediata.Visible = false;
                    btnOkAcaoImediata.Visible = false;
                    btnAcaoImediata.Visible = false;

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


        private void InterfaceIncludeAnaliseCritica()
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                DataFieldCollection lFieldsOcorrencia = new DataFieldCollection();

                if (!ValidarInsertAnaliseCritica())
                    return;


                OperationResult lReturn = new OperationResult();

                //Atualizar Situação da Ocorrencia
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_ID, hidOCR_ID.Value.DBToDecimal());

                if (rblANC_SITUACAO.SelectedValue == "0")
                    lFieldsOcorrencia.Add(NC_OcorrenciaQD._STCOCR_ID, (decimal)SituacaoOcorrencia.EmAnaliseCausa);
                else if (rblANC_SITUACAO.SelectedValue == "2")
                    lFieldsOcorrencia.Add(NC_OcorrenciaQD._STCOCR_ID, (decimal)SituacaoOcorrencia.Nova);
                else
                    lFieldsOcorrencia.Add(NC_OcorrenciaQD._STCOCR_ID, (decimal)SituacaoOcorrencia.Cancelado);

                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_REGDATE, DateTime.Now);
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_STATUS, LocalInstance.StatusAtivo);

                //Análise Crítica
                lFields.Add(NC_AnaliseCriticaQD._OCR_ID, hidOCR_ID.Value.DBToDecimal());
                lFields.Add(NC_AnaliseCriticaQD._ANC_SITUACAO, rblANC_SITUACAO.SelectedValue);
                lFields.Add(NC_AnaliseCriticaQD._ANC_DATA, txtANC_DATA.Text.DBToDateTime());
                lFields.Add(NC_AnaliseCriticaQD._TPANL_ID, ddlTIPOANALISE.SelectedValue);
                lFields.Add(NC_AnaliseCriticaQD._MATRICULA_RESPPA, ddlRespPA.SelectedValue);
                lFields.Add(NC_AnaliseCriticaQD._MATRICULA_NQ, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_AnaliseCriticaQD._ANC_JUSTIFICATIVACANCELAMENTO, txtANC_JUSTIVACANCELAMENTO.Text);
                lFields.Add(NC_AnaliseCriticaQD._ANC_REGDATE, DateTime.Now);
                lFields.Add(NC_AnaliseCriticaQD._ANC_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_AnaliseCriticaQD._ANC_STATUS, LocalInstance.StatusAtivo);


                lReturn = NC_AnaliseCriticaDo.Insert(lFields, lFieldsOcorrencia, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    if (rblANC_SITUACAO.SelectedValue == "0")
                    {
                        EnviaMensagemEmail(1, "Gestão de Não Conformidade", " Foi aprovada. \r\r Faça a Análise de Causa e Elabore o Plano de Ação. \r\r\r Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, "");

                    }
                    else if (rblANC_SITUACAO.SelectedValue == "1")
                    {
                        EnviaMensagemEmail(1, "Gestão de Não Conformidade", " Foi cancelada. \r\r\r Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, "");
                        EnviaMensagemEmail(2, "Gestão de Não Conformidade", " Foi cancelada. \r\r\r Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, "");
                    }
                    else
                    {
                        EnviaMensagemEmail(1, "Gestão de Não Conformidade", " Foi Devolvida. \r\r Com a seguinte justificativa: \r " + txtANC_JUSTIVACANCELAMENTO.Text + " \r\r\r Faça a Análise critica. \r\r\r Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, "");
                        EnviaMensagemEmail(2, "Gestão de Não Conformidade", " Foi Devolvida. \r\r Com a seguinte justificativa: \r " + txtANC_JUSTIVACANCELAMENTO.Text + " \r\r\r Para acompanhar o andamento da RNC. Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, "");

                    }

                    MessageBox1.wuc_ShowMessage("Análise Crítica salva com sucesso.", "ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, 1);
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

        private void InterfaceIncludeCausaEfeito()
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                DataFieldCollection lFieldsOcorrencia = new DataFieldCollection();
                DataFieldCollection lFieldsDiagrama = new DataFieldCollection();
                List<DataFieldCollection> lListlDiagramas = new List<DataFieldCollection>();

                if (!ValidarInsertCausaEfeito())
                    return;


                OperationResult lReturn = new OperationResult();

                //Atualizar Situação da Ocorrencia
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_ID, hidOCR_ID.Value.DBToDecimal());
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._STCOCR_ID, (decimal)SituacaoOcorrencia.EmElaboracaoPlanoAcao);
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_REGDATE, DateTime.Now);
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_STATUS, LocalInstance.StatusAtivo);

                //Análise Causa Efeito
                lFields.Add(NC_AnaliseCausaEfeitoQD._OCR_ID, hidOCR_ID.Value.DBToDecimal());
                lFields.Add(NC_AnaliseCausaEfeitoQD._ANCE_DATA, txtANCE_DATA.Text.DBToDateTime());
                lFields.Add(NC_AnaliseCausaEfeitoQD._ANCE_REGDATE, DateTime.Now);
                lFields.Add(NC_AnaliseCausaEfeitoQD._ANCE_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_AnaliseCausaEfeitoQD._ANCE_STATUS, LocalInstance.StatusAtivo);

                if (rblAnaliseCausa.SelectedValue == "0")
                {
                    //Diagrama
                    for (int i = 0; i < chkANCE_DIAGRAMA.Items.Count; i++)
                    {
                        if (chkANCE_DIAGRAMA.Items[i].Selected)
                        {
                            lFieldsDiagrama = new DataFieldCollection();
                            lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_TIPO, chkANCE_DIAGRAMA.Items[i].Value);

                            switch (i.ToString())
                            {
                                case "0":
                                    if (chkANCE_DIAGRAMA.Items[0].Selected)
                                        lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO, txtMedida.Text);
                                    break;
                                case "1":
                                    if (chkANCE_DIAGRAMA.Items[1].Selected)
                                        lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO, txtMaoDeObra.Text);
                                    break;
                                case "2":
                                    if (chkANCE_DIAGRAMA.Items[2].Selected)
                                        lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO, txtMetodo.Text);
                                    break;
                                case "3":
                                    if (chkANCE_DIAGRAMA.Items[3].Selected)
                                        lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO, txtMeioAmbiente.Text);
                                    break;
                                case "4":
                                    if (chkANCE_DIAGRAMA.Items[4].Selected)
                                        lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO, txtMaquinas.Text);
                                    break;
                                case "5":
                                    if (chkANCE_DIAGRAMA.Items[5].Selected)
                                        lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO, txtMateriaPrima.Text);
                                    break;
                                case "6":
                                    if (chkANCE_DIAGRAMA.Items[6].Selected)
                                        lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO, txtGestao.Text);
                                    break;
                            }

                            lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_REGDATE, DateTime.Now);
                            lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                            lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_STATUS, LocalInstance.StatusAtivo);
                            lListlDiagramas.Add(lFieldsDiagrama);
                        }
                    }
                }
                else
                {
                    if (fuCausaEfeito.HasFile && fuCausaEfeito.FileName != "")
                    {
                        if (fuCausaEfeito.FileBytes.Length > 0)
                        {

                            lFields.Add(NC_AnaliseCausaEfeitoQD._ANCE_ARQUIVODESCRICAO, fuCausaEfeito.FileName);
                            lFields.Add(NC_AnaliseCausaEfeitoQD._ANCE_ARQUIVO, fuCausaEfeito.FileBytes);
                        }
                    }
                }

                lReturn = NC_AnaliseCausaEfeitoDo.Insert(lFields, lListlDiagramas, lFieldsOcorrencia, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    MessageBox2.wuc_ShowMessage("Análise de Causa salva com sucesso.", "ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, 1);
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


        private void InterfaceUpdateDiagramaCausaEfeito()
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                DataFieldCollection lFieldsDelete = new DataFieldCollection();
                DataFieldCollection lFieldsOcorrencia = new DataFieldCollection();
                DataFieldCollection lFieldsDiagrama = new DataFieldCollection();
                List<DataFieldCollection> lListlDiagramas = new List<DataFieldCollection>();

                if (!ValidarInsertCausaEfeito())
                    return;


                OperationResult lReturn = new OperationResult();

                if (rblAnaliseCausa.SelectedValue == "0")
                {
                    //Diagrama
                    for (int i = 0; i < chkANCE_DIAGRAMA.Items.Count; i++)
                    {
                        if (chkANCE_DIAGRAMA.Items[i].Selected)
                        {
                            lFieldsDiagrama = new DataFieldCollection();
                            lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_TIPO, chkANCE_DIAGRAMA.Items[i].Value);

                            switch (i.ToString())
                            {
                                case "0":
                                    if (chkANCE_DIAGRAMA.Items[0].Selected)
                                        lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO, txtMedida.Text);
                                    break;
                                case "1":
                                    if (chkANCE_DIAGRAMA.Items[1].Selected)
                                        lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO, txtMaoDeObra.Text);
                                    break;
                                case "2":
                                    if (chkANCE_DIAGRAMA.Items[2].Selected)
                                        lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO, txtMetodo.Text);
                                    break;
                                case "3":
                                    if (chkANCE_DIAGRAMA.Items[3].Selected)
                                        lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO, txtMeioAmbiente.Text);
                                    break;
                                case "4":
                                    if (chkANCE_DIAGRAMA.Items[4].Selected)
                                        lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO, txtMaquinas.Text);
                                    break;
                                case "5":
                                    if (chkANCE_DIAGRAMA.Items[5].Selected)
                                        lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO, txtMateriaPrima.Text);
                                    break;
                                case "6":
                                    if (chkANCE_DIAGRAMA.Items[6].Selected)
                                        lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO, txtGestao.Text);
                                    break;
                            }

                            lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._ANCE_ID, hidANCE_ID.Value.DBToDecimal());
                            lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_REGDATE, DateTime.Now);
                            lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                            lFieldsDiagrama.Add(NC_DiagramaCausaEfeitoQD._DGRCE_STATUS, LocalInstance.StatusAtivo);
                            lListlDiagramas.Add(lFieldsDiagrama);
                        }
                    }
                }
                else
                {
                    if (fuCausaEfeito.HasFile && fuCausaEfeito.FileName != "")
                    {
                        if (fuCausaEfeito.FileBytes.Length > 0)
                        {

                            lFields.Add(NC_AnaliseCausaEfeitoQD._ANCE_ARQUIVODESCRICAO, fuCausaEfeito.FileName);
                            lFields.Add(NC_AnaliseCausaEfeitoQD._ANCE_ARQUIVO, fuCausaEfeito.FileBytes);
                        }
                    }
                }

                lFieldsDelete.Add(NC_DiagramaCausaEfeitoQD._ANCE_ID, hidANCE_ID.Value.DBToDecimal());
                lFieldsDelete.Add(NC_DiagramaCausaEfeitoQD._DGRCE_REGDATE, DateTime.Now);
                lFieldsDelete.Add(NC_DiagramaCausaEfeitoQD._DGRCE_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsDelete.Add(NC_DiagramaCausaEfeitoQD._DGRCE_STATUS, LocalInstance.StatusInativo);

                //Apaga todos os existentes e salva os atuais
                lReturn = NC_DiagramaCausaEfeitoDo.Insert(lListlDiagramas, lFieldsDelete, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    MessageBox2.wuc_ShowMessage("Análise de Causa salva com sucesso.", "ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, 1);
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


        private void InterfaceUpdateSituacaoAcao(decimal pACS_ID, decimal pACS_SITUACAO)
        {
            try
            {
                DataFieldCollection lFieldsAcoes = new DataFieldCollection();
                OperationResult lReturn = new OperationResult();


                lFieldsAcoes.Add(NC_AcoesQD._ACS_ID, pACS_ID);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_SITUACAO, pACS_SITUACAO);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_REGDATE, DateTime.Now);
                //lFieldsAcoes.Add(NC_AcoesQD._ACS_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_STATUS, LocalInstance.StatusAtivo);


                lReturn = NC_AcoesDo.Update(lFieldsAcoes, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {

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


        private void InterfaceIncludeVerificacaoEficacia(decimal pPLNAC_ID)
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                DataFieldCollection lFieldsOcorrencia = new DataFieldCollection();
                DataFieldCollection lFieldsPlanoAcao = new DataFieldCollection();
                DataFieldCollection lFieldsAcoes = new DataFieldCollection();

                if (!ValidarInsertVerificacaoEficacia())
                    return;


                OperationResult lReturn = new OperationResult();

                //Atualizar Situação da Ocorrencia
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_ID, hidOCR_ID.Value.DBToDecimal());

                if (rblVRFEFC_SITUACAO.SelectedValue == "1")//Concluida
                    lFieldsOcorrencia.Add(NC_OcorrenciaQD._STCOCR_ID, (decimal)SituacaoOcorrencia.Concluida);
                else if (rblVRFEFC_SITUACAO.SelectedValue == "2")//Nao aprovado elaborar novo plano
                    lFieldsOcorrencia.Add(NC_OcorrenciaQD._STCOCR_ID, (decimal)SituacaoOcorrencia.EmElaboracaoPlanoAcao);
                else if (rblVRFEFC_SITUACAO.SelectedValue == "3")//Nao aprovado
                    lFieldsOcorrencia.Add(NC_OcorrenciaQD._STCOCR_ID, (decimal)SituacaoOcorrencia.Concluida);

                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_REGDATE, DateTime.Now);
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_STATUS, LocalInstance.StatusAtivo);

                //PlanoDeAcao
                lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_ID, pPLNAC_ID);
                if (rblVRFEFC_SITUACAO.SelectedValue == "1")
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_SITUACAO, (decimal)SituacaoPlanoAcao.Aprovado);
                else if (rblVRFEFC_SITUACAO.SelectedValue == "2")
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_SITUACAO, (decimal)SituacaoPlanoAcao.NaoAprovado);
                else if (rblVRFEFC_SITUACAO.SelectedValue == "3")
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_SITUACAO, (decimal)SituacaoPlanoAcao.NaoAprovado);

                if (rblVRFEFC_SITUACAO.SelectedValue == "1")
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._STPLNAC_ID, (decimal)SituacaoPlanoAcaoFLUXO.Concluído);
                else if (rblVRFEFC_SITUACAO.SelectedValue == "2")
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._STPLNAC_ID, (decimal)SituacaoPlanoAcaoFLUXO.ConcluídoNãoEficaz);
                else if (rblVRFEFC_SITUACAO.SelectedValue == "3")
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._STPLNAC_ID, (decimal)SituacaoPlanoAcaoFLUXO.ConcluídoNãoEficaz);

                lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_REGDATE, DateTime.Now);
                lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_STATUS, LocalInstance.StatusAtivo);




                //Ações
                lFieldsAcoes.Add(NC_AcoesQD._PLNAC_ID, pPLNAC_ID);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_SITUACAO, (decimal)SituacaoAcao.Ineficaz);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_REGDATE, DateTime.Now);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_STATUS, LocalInstance.StatusAtivo);

                //VerificacaoEficacia
                lFields.Add(NC_VerificarEficaciaQD._PLNAC_ID, pPLNAC_ID);
                lFields.Add(NC_VerificarEficaciaQD._VRFEFC_DATA, DateTime.Now);
                lFields.Add(NC_VerificarEficaciaQD._VRFEFC_OBSERVACAO, txtVRFEFC_OBSERVACAO.Text);
                lFields.Add(NC_VerificarEficaciaQD._MATRICULA_REGISTROU, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_VerificarEficaciaQD._VRFEFC_REGDATE, DateTime.Now);
                lFields.Add(NC_VerificarEficaciaQD._VRFEFC_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_VerificarEficaciaQD._VRFEFC_STATUS, LocalInstance.StatusAtivo);





                lReturn = NC_VerificarEficaciaDo.Insert(lFields, lFieldsOcorrencia, lFieldsPlanoAcao, lFieldsAcoes, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    if (rblVRFEFC_SITUACAO.SelectedValue == "1")//Aprovada
                        EnviaMensagemEmail(1, "Gestão de Não Conformidade", " Foi Aprovada. \r\r\r Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, "");
                    else if (rblVRFEFC_SITUACAO.SelectedValue == "2")//Nao aprovada
                        EnviaMensagemEmail(1, "Gestão de Não Conformidade", " Foi Reprovada. \r\r Realize novo Plano de Ação para as etapas reprovadas. \r\r\r Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, "");
                    else if (rblVRFEFC_SITUACAO.SelectedValue == "3")//Cancelada
                        EnviaMensagemEmail(1, "Gestão de Não Conformidade", " Foi Cancelda. \r\r \r\r\r Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, "");

                    MessageBox1.wuc_ShowMessage("Verificação da Eficácia salva com sucesso.", "ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, 1);
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




        private void LoadDadosOcorrencia(decimal pOCR_ID)
        {
            try
            {
                DataTable lTable = NC_OcorrenciaDo.GetOcorrenciaById(pOCR_ID, LocalInstance.ConnectionInfo);
                Session["WRK_TABLE_OCORRENCIA"] = lTable;
                Session.Remove("WRK_TABLE_NORMAS");
                Session.Remove("WRK_TABLE_SINTOMASACAO");
                Session.Remove("WRK_TABLE_ANALISECRITICA");
                Session.Remove("WRK_TABLE_ANALISECAUSAEFEITO");
                Session.Remove("WRK_TABLE_DIAGRAMACAUSAEFEITO");
                Session.Remove("WRK_TABLE_ACOES");
                Session.Remove("WRK_TABLE_VERIFICAREFICACIA");


                if (lTable.Rows.Count > 0)
                {
                    hidSTCOCR_ID.Value = lTable.Rows[0][NC_OcorrenciaQD._STCOCR_ID.Name].ToString();
                    txtOCR_DESCRICAO.Text = lTable.Rows[0][NC_OcorrenciaQD._OCR_DESCRICAO.Name].ToString();
                    ddlTipoOcorrencia.SelectedValue = lTable.Rows[0][NC_OcorrenciaQD._TPOCR_ID.Name].ToString();
                    txtOCR_DATAABERTURA.Text = lTable.Rows[0][NC_OcorrenciaQD._OCR_DATAABERTURA.Name].DBToDateTime().ToShortDateString();
                    ddlMotivoOcorrencia.SelectedValue = lTable.Rows[0][NC_OcorrenciaQD._MTV_ID.Name].ToString();
                    txtOCR_DATAOCORRENCIA.Text = lTable.Rows[0][NC_OcorrenciaQD._OCR_DATAOCORRENCIA.Name].DBToDateTime().ToShortDateString();
                    ddlUnidadeRespResolucao.SelectedValue = lTable.Rows[0][NC_OcorrenciaQD._UNIDADE_RESPRESOLUCAO.Name].ToString();



                    if (lTable.Rows[0][NC_OcorrenciaQD._UNIDADE_ABERTURA.Name].ToString() != "")
                    {
                        ddlUnidadeAbertura.SelectedValue = lTable.Rows[0][NC_OcorrenciaQD._UNIDADE_ABERTURA.Name].ToString();
                    }
                    else
                    {
                        divRespAbertura.Visible = false;
                    }


                    lblOCR_NUMERO.Text = lTable.Rows[0]["OCR_NUMEROS"].ToString();
                    ddlUnidadeOcorrencia.SelectedValue = lTable.Rows[0][NC_OcorrenciaQD._UNIDADE_LOCALOCORRENCIA.Name].ToString();
                    LoadRespOcorrencia(lTable.Rows[0][NC_OcorrenciaQD._MATRICULA_RESPABERTURA.Name].DBToDecimal());
                    LoadMatriculaRespResolucao_NQ(ddlUnidadeRespResolucao.SelectedValue);

                    txtOCR_DATAABERTURA.Enabled = false;
                    ddlFuncionario.Enabled = false;
                    ddlTipoOcorrencia.Enabled = false;
                    txtOCR_DATAOCORRENCIA.Enabled = false;
                    ddlUnidadeOcorrencia.Enabled = false;
                    ddlMotivoOcorrencia.Enabled = false;
                    chkNormas.Enabled = false;
                    txtOCR_DESCRICAO.Enabled = false;
                    fuAnexo.Enabled = false;
                    ddlUnidadeRespResolucao.Enabled = false;

                    trEdicao.Visible = true;
                    LoadNormasOcorrencia(hidOCR_ID.Value.DBToDecimal());
                    LoadAnexoOcorrencia(hidOCR_ID.Value.DBToDecimal());
                    LoadDocumentosOcorrencia(hidOCR_ID.Value.DBToDecimal());

                    LoadSituacaoOcorrencia(hidSTCOCR_ID.Value);
                    lnkPrint.Visible = true;


                }
                else
                {
                    MessageBox1.wuc_ShowMessage("Ocorrência não encontrada.", 3);
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

        private void LoadDocumentosOcorrencia(decimal pOCR_ID)
        {
            DataTable lTable = NC_OcorrenciaDo.Get_DocumentoNaoConformeByOcorrencia(pOCR_ID, LocalInstance.ConnectionInfo);
            grdDoc.AllowPaging = true;
            grdDoc.PageSize = 6;
            grdDoc.PagerStyle.HorizontalAlign = HorizontalAlign.Center;

            ViewState["WRK_TABLE_DOC"] = lTable;

            grdDoc.DataSource = lTable;
            grdDoc.DataBind();

            if (lTable.Rows.Count > 0)
            {
                grdDoc.Columns[0].Visible = false;
                grdDoc.Columns[1].Visible = false;
                Session["WRK_TABLE_DOC"] = lTable;
                divDocumentos.Visible = true;
                grdDoc.Enabled = false;
                
                //divVerificacaoEficacia.Visible = true;
            }
            else
            {
                Session["WRK_TABLE_DOC"] = null;
                divDocumentos.Visible = false;
                
                //divVerificacaoEficacia.Visible = false;
            }
        }



        private void LoadMatriculaRespResolucao_NQ(string pIdUnidade)
        {
            DataTable lTable = NC_OcorrenciaDo.GetAllUnidadesGestor(LocalInstance.ConnectionInfo);
            hidMATRICULA_RESPRESOLUCAO.Value = "";

            if (lTable.Rows.Count > 0)
            {
                for (int i = 0; i < lTable.Rows.Count; i++)
                {
                    if (lTable.Rows[i]["ID"].ToString() == pIdUnidade)
                        hidMATRICULA_RESPRESOLUCAO.Value = lTable.Rows[i]["MATRICULA_RESP"].ToString();

                    if (lTable.Rows[i]["ID"].ToString() == "60") // Resp NQ.
                        hidMATRICULA_NQ.Value = lTable.Rows[i]["MATRICULA_RESP"].ToString();
                }
            }
        }

        private void LoadSituacaoOcorrencia(string pSTCOCR_ID)
        {
            switch (pSTCOCR_ID)
            {
                case "1":
                    lblSTCOCR_ID.Text = "Nova";
                    btnExcluir.Visible = false;
                    btnOk.Visible = false;
                    btnAcaoImediata.Visible = false;
                    btnCancelarAcaoImediata.Visible = false;
                    txtSNTAC_DESCRICAO.Enabled = false;
                    txtSNTAC_DATA.Enabled = false;
                    txtANCE_DATA.Enabled = false;



                    LoadTipoAnalise();
                    LoadAcaoImediata();
                    LoadAnaliseCritica();
                    if (((LoginUserDo)Session["_SessionUser"]).MATRICULA.ToString() == ddlFuncionario.SelectedValue || ((LoginUserDo)Session["_SessionUser"]).LoginName.ToString() == "ADMIN.GNC")//quem abriu
                    {
                        btnExcluir.Visible = true;
                    }

                    if (((LoginUserDo)Session["_SessionUser"]).MATRICULA.ToString() == hidMATRICULA_RESPRESOLUCAO.Value || ((LoginUserDo)Session["_SessionUser"]).LoginName.ToString() == "ADMIN.GNC" || ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID == 60) //quem recebeu
                    {
                        btnAcaoImediata.Visible = true;

                        // txtSNTAC_DESCRICAO.Enabled = true;

                    }
                    break;
                case "2":
                    lblSTCOCR_ID.Text = "Em Análise Crítica";
                    btnExcluir.Visible = false;
                    btnOk.Visible = false;
                    btnAcaoImediata.Visible = false;
                    divAcaoImediata.Visible = true;
                    btnOkAcaoImediata.Visible = false;
                    btnCancelarAcaoImediata.Visible = false;
                    LoadAcaoImediata();
                    btnAnaliseCritica.Visible = false;
                    LoadTipoAnalise();
                    LoadRespPA(hidMATRICULA_RESPRESOLUCAO.Value.DBToDecimal());

                    if (((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID == 60 || ((LoginUserDo)Session["_SessionUser"]).LoginName.ToString() == "ADMIN.GNC")//Usuário do NQ || Usuário de Admin
                    {
                        btnAnaliseCritica.Visible = true;
                    }
                    break;
                case "3":
                    lblSTCOCR_ID.Text = "Cancelada";
                    btnExcluir.Visible = false;
                    btnOk.Visible = false;
                    btnAcaoImediata.Visible = false;
                    divAcaoImediata.Visible = true;
                    btnOkAcaoImediata.Visible = false;
                    btnCancelarAcaoImediata.Visible = false;
                    LoadAcaoImediata();
                    btnAnaliseCritica.Visible = false;
                    LoadTipoAnalise();
                    LoadRespPA(hidMATRICULA_RESPRESOLUCAO.Value.DBToDecimal());
                    btnCausaEfeito.Visible = false;
                    btnOkAnaliseCritica.Visible = false;
                    btnCancelarAnaliseCritica.Visible = false;
                    divAnáliseCritica.Visible = true;
                    LoadAnaliseCritica();
                    LoadCausaEfeito();
                    LoadPlanoDeAcao();
                    break;
                case "4":
                    lblSTCOCR_ID.Text = "Em Análise de Causa";
                    btnExcluir.Visible = false;
                    btnOk.Visible = false;
                    btnAcaoImediata.Visible = false;
                    divAcaoImediata.Visible = true;
                    btnOkAcaoImediata.Visible = false;
                    btnCancelarAcaoImediata.Visible = false;
                    LoadAcaoImediata();
                    btnAnaliseCritica.Visible = false;
                    LoadTipoAnalise();
                    divAnáliseCritica.Visible = true;
                    LoadAnaliseCritica();
                    btnCausaEfeito.Visible = false;
                    btnOkAnaliseCritica.Visible = false;
                    btnCancelarAnaliseCritica.Visible = false;
                    chkANCE_DIAGRAMA.Enabled = false;
                    if (((LoginUserDo)Session["_SessionUser"]).MATRICULA.ToString() == hidMATRICULA_RESPRESOLUCAO.Value || ((LoginUserDo)Session["_SessionUser"]).LoginName.ToString() == "ADMIN.GNC") //quem recebeu
                    {
                        btnCausaEfeito.Visible = true;
                        chkANCE_DIAGRAMA.Enabled = true;
                    }
                    break;
                case "5":
                    lblSTCOCR_ID.Text = "Em Elaboração Plano de Ação";
                    btnExcluir.Visible = false;
                    btnOk.Visible = false;
                    btnAcaoImediata.Visible = false;
                    divAcaoImediata.Visible = true;
                    btnOkAcaoImediata.Visible = false;
                    btnCancelarAcaoImediata.Visible = false;
                    LoadAcaoImediata();
                    btnAnaliseCritica.Visible = false;
                    LoadTipoAnalise();
                    divAnáliseCritica.Visible = true;
                    LoadAnaliseCritica();
                    btnCausaEfeito.Visible = false;
                    btnOkAnaliseCritica.Visible = false;
                    btnCancelarAnaliseCritica.Visible = false;
                    btnCausaEfeito.Visible = false;
                    btnOkCausaEfeito.Visible = false;
                    btnCancelarCausaEfeito.Visible = false;
                    btnOutrosCausaEfeito.Visible = false;
                    btnOutrosCancelarCausaEfeito.Visible = false;
                    LoadCausaEfeito();
                    divCausaEfeito.Visible = true;
                    LoadPlanoDeAcao();
                    LoadVerificacaoEficacia();
                    btnOkCausaEfeito.Visible = false;
                    btnOutrosCausaEfeito.Visible = false;
                    if (((LoginUserDo)Session["_SessionUser"]).MATRICULA.ToString() == hidMATRICULA_RESPRESOLUCAO.Value || ((LoginUserDo)Session["_SessionUser"]).LoginName.ToString() == "ADMIN.GNC") //quem recebeu
                    {
                        btnOkCausaEfeito.Visible = true;
                        btnOutrosCausaEfeito.Visible = true;
                        chkANCE_DIAGRAMA.Enabled = true;
                    }
                    break;
                case "6":
                    lblSTCOCR_ID.Text = "Em Execução do Plano de Ação";
                    btnExcluir.Visible = false;
                    btnOk.Visible = false;
                    btnAcaoImediata.Visible = false;
                    divAcaoImediata.Visible = true;
                    btnOkAcaoImediata.Visible = false;
                    btnCancelarAcaoImediata.Visible = false;
                    LoadAcaoImediata();
                    btnAnaliseCritica.Visible = false;
                    LoadTipoAnalise();
                    divAnáliseCritica.Visible = true;
                    LoadAnaliseCritica();
                    btnCausaEfeito.Visible = false;
                    btnOkAnaliseCritica.Visible = false;
                    btnCancelarAnaliseCritica.Visible = false;
                    btnCausaEfeito.Visible = false;
                    btnOkCausaEfeito.Visible = false;
                    btnCancelarCausaEfeito.Visible = false;
                    btnOutrosCausaEfeito.Visible = false;
                    btnOutrosCancelarCausaEfeito.Visible = false;
                    LoadCausaEfeito();
                    divCausaEfeito.Visible = true;
                    LoadPlanoDeAcao();
                    LoadVerificacaoEficacia();
                    btnOkCausaEfeito.Visible = false;
                    btnOutrosCausaEfeito.Visible = false;
                    if (((LoginUserDo)Session["_SessionUser"]).MATRICULA.ToString() == hidMATRICULA_RESPRESOLUCAO.Value || ((LoginUserDo)Session["_SessionUser"]).LoginName.ToString() == "ADMIN.GNC") //quem recebeu
                    {
                        btnOkCausaEfeito.Visible = true;
                        btnOutrosCausaEfeito.Visible = true;
                        chkANCE_DIAGRAMA.Enabled = true;
                    }
                    break;
                case "7":
                    lblSTCOCR_ID.Text = "Em Verificação da Eficácia";
                    btnExcluir.Visible = false;
                    btnOk.Visible = false;
                    btnAcaoImediata.Visible = false;
                    divAcaoImediata.Visible = true;
                    btnOkAcaoImediata.Visible = false;
                    btnCancelarAcaoImediata.Visible = false;
                    LoadAcaoImediata();
                    btnAnaliseCritica.Visible = false;
                    LoadTipoAnalise();
                    divAnáliseCritica.Visible = true;
                    LoadAnaliseCritica();
                    btnCausaEfeito.Visible = false;
                    btnOkAnaliseCritica.Visible = false;
                    btnCancelarAnaliseCritica.Visible = false;
                    btnCausaEfeito.Visible = false;
                    btnOkCausaEfeito.Visible = false;
                    btnCancelarCausaEfeito.Visible = false;
                    btnOutrosCausaEfeito.Visible = false;
                    btnOutrosCancelarCausaEfeito.Visible = false;
                    LoadCausaEfeito();
                    divCausaEfeito.Visible = true;
                    LoadPlanoDeAcao();
                    LoadVerificacaoEficacia();
                    if (((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID == 60)//Usuário do NQ
                    {
                        btnVerificacaoEficacia.Visible = true;
                    }
                    break;
                case "8":
                    lblSTCOCR_ID.Text = "Concluída";
                    btnExcluir.Visible = false;
                    btnOk.Visible = false;
                    btnAcaoImediata.Visible = false;
                    divAcaoImediata.Visible = true;
                    btnOkAcaoImediata.Visible = false;
                    btnCancelarAcaoImediata.Visible = false;
                    LoadAcaoImediata();
                    btnAnaliseCritica.Visible = false;
                    LoadTipoAnalise();
                    divAnáliseCritica.Visible = true;
                    LoadAnaliseCritica();
                    btnCausaEfeito.Visible = false;
                    btnOkAnaliseCritica.Visible = false;
                    btnCancelarAnaliseCritica.Visible = false;
                    btnCausaEfeito.Visible = false;
                    btnOkCausaEfeito.Visible = false;
                    btnCancelarCausaEfeito.Visible = false;
                    btnOutrosCausaEfeito.Visible = false;
                    btnOutrosCancelarCausaEfeito.Visible = false;
                    LoadCausaEfeito();
                    divCausaEfeito.Visible = true;
                    LoadPlanoDeAcao();
                    btnVerificacaoEficacia.Visible = false;
                    divVerificacaoEficacia.Visible = true;
                    btnOkVerificacao.Visible = false;
                    LoadVerificacaoEficacia();
                    break;
            }
            if (((LoginUserDo)Session["_SessionUser"]).LoginName.ToString() == "ADMIN.GNC")
            {
                txtSNTAC_DATA.Enabled = true;
                txtANCE_DATA.Enabled = true;
            }
        }

        private void LoadNormasOcorrencia(decimal pOCR_ID)
        {
            DataTable lTable = NC_NormasxOcorrenciaDo.GetNormasxOcorrenciaByOCR_ID(pOCR_ID, LocalInstance.ConnectionInfo);
            Session["WRK_TABLE_NORMAS"] = lTable;

            if (lTable.Rows.Count > 0)
            {
                chkNormas.DataSource = lTable;
                chkNormas.DataTextField = "NRM_DESCRICAO";
                chkNormas.DataValueField = "NRM_ID";
                chkNormas.DataBind();

                for (int x = 0; x < chkNormas.Items.Count; x++)
                {
                    chkNormas.Items[x].Selected = true;
                }
            }
        }


        private void LoadAnexoOcorrencia(decimal pOCR_ID)
        {
            DataTable lTable = NC_AnexoOcorrenciaDo.GetNC_AnexoOcorrenciaByOCR_ID(pOCR_ID, LocalInstance.ConnectionInfo);
            if (lTable.Rows.Count > 0)
            {
                hidANXOCR_ID.Value = lTable.Rows[0][NC_AnexoOcorrenciaQD._ANXOCR_ID.Name].ToString();
                lnkAnexoOcorrencia.Text = lTable.Rows[0][NC_AnexoOcorrenciaQD._ANXOCR_DESCRICAO.Name].ToString();
                divAnexoOcorrencia.Visible = true;
            }
        }

        private void LoadAcaoImediata()
        {
            DataTable lTable = NC_SintomasAcaoDo.GetNC_SintomasAcaoByOCR_ID(hidOCR_ID.Value.DBToDecimal(), LocalInstance.ConnectionInfo);

            if (lTable.Rows.Count > 0)
            {

                Session["WRK_TABLE_SINTOMASACAO"] = lTable;

                txtSNTAC_DESCRICAO.Text = lTable.Rows[0][NC_SintomasAcaoQD._SNTAC_DESCRICAO.Name].ToString();
                txtSNTAC_DATA.Text = lTable.Rows[0][NC_SintomasAcaoQD._SNTAC_DATA.Name].DBToDateTime().ToShortDateString();
                hidSNTAC_ID.Value = lTable.Rows[0][NC_SintomasAcaoQD._SNTAC_ID.Name].ToString();

                if (hidSTCOCR_ID.Value.DBToDecimal() == 1)
                {
                    divAcaoImediata.Visible = true;

                    txtSNTAC_DESCRICAO.Enabled = false;
                    txtSNTAC_DATA.Enabled = false;
                    btnOkAcaoImediata.Visible = false;

                }
            }
            else
            {
                Session["WRK_TABLE_SINTOMASACAO"] = null;
                hidSNTAC_ID.Value = "";
            }

            txtSNTAC_DESCRICAO.Enabled = false;
            txtSNTAC_DATA.Enabled = false;
        }

        private void LoadAnaliseCritica()
        {
            DataTable lTable = NC_AnaliseCriticaDo.GetNC_AnaliseCriticaByOCR_ID(hidOCR_ID.Value.DBToDecimal(), LocalInstance.ConnectionInfo);

            if (lTable.Rows.Count > 0)
            {
                Session["WRK_TABLE_ANALISECRITICA"] = lTable;

                rblANC_SITUACAO.SelectedValue = lTable.Rows[0][NC_AnaliseCriticaQD._ANC_SITUACAO.Name].ToString();
                txtANC_DATA.Text = lTable.Rows[0][NC_AnaliseCriticaQD._ANC_DATA.Name].DBToDateTime().ToShortDateString();
                ddlTIPOANALISE.SelectedValue = lTable.Rows[0][NC_AnaliseCriticaQD._TPANL_ID.Name].ToString();
                LoadRespPA(lTable.Rows[0][NC_AnaliseCriticaQD._MATRICULA_RESPPA.Name].DBToDecimal());
                txtANC_JUSTIVACANCELAMENTO.Text = lTable.Rows[0][NC_AnaliseCriticaQD._ANC_JUSTIFICATIVACANCELAMENTO.Name].ToString();

                rblANC_SITUACAO.Enabled = false;
                txtANC_DATA.Enabled = false;
                ddlTIPOANALISE.Enabled = false;
                ddlRespPA.Enabled = false;
                txtANC_JUSTIVACANCELAMENTO.Enabled = false;

                if (hidSTCOCR_ID.Value.DBToDecimal() == 1)
                {
                    btnOkAnaliseCritica.Visible = false;
                    divAnáliseCritica.Visible = true;
                    btnCancelarAnaliseCritica.Visible = false;

                }
            }
            else
                Session["WRK_TABLE_ANALISECRITICA"] = null;

        }



        private void LoadCausaEfeito()
        {
            DataTable lTable = NC_AnaliseCausaEfeitoDo.GetNC_AnaliseCausaEfeitoByOCR_ID(hidOCR_ID.Value.DBToDecimal(), LocalInstance.ConnectionInfo);

            if (lTable.Rows.Count > 0)
            {
                Session["WRK_TABLE_ANALISECAUSAEFEITO"] = lTable;
                txtANCE_DATA.Text = lTable.Rows[0][NC_AnaliseCausaEfeitoQD._ANCE_DATA.Name].DBToDateTime().ToShortDateString();

                rblAnaliseCausa.Enabled = false;
                divArquivoCausaEfeito.Visible = false;

                if (lTable.Rows[0][NC_AnaliseCausaEfeitoQD._ANCE_ARQUIVODESCRICAO.Name].ToString() != "")
                {
                    rblAnaliseCausa.SelectedValue = "1";
                    divAnexoCausa.Visible = true;
                    divOutrosAnaliseCausa.Visible = true;
                    divDiagramaIshikawa.Visible = false;

                    hidANCE_ID.Value = lTable.Rows[0][NC_AnaliseCausaEfeitoQD._ANCE_ID.Name].ToString();
                    lnkAnexoCausa.Text = lTable.Rows[0][NC_AnaliseCausaEfeitoQD._ANCE_ARQUIVODESCRICAO.Name].ToString();
                }
                else
                {
                    rblAnaliseCausa.SelectedValue = "0";
                    divAnexoCausa.Visible = false;
                    divDiagramaIshikawa.Visible = true;
                    hidANCE_ID.Value = lTable.Rows[0][NC_AnaliseCausaEfeitoQD._ANCE_ID.Name].ToString();
                    LoadDiagraCausaEfeito(lTable.Rows[0][NC_AnaliseCausaEfeitoQD._ANCE_ID.Name].DBToDecimal());
                }
            }
            else
            {
                hidANCE_ID.Value = "";
                Session["WRK_TABLE_ANALISECAUSAEFEITO"] = null;
            }
        }

        private void LoadDiagraCausaEfeito(decimal pANCE_ID)
        {
            DataTable lTable = NC_DiagramaCausaEfeitoDo.GetNC_DiagramaCausaEfeitoByANCE_ID(pANCE_ID, LocalInstance.ConnectionInfo);
            if (lTable.Rows.Count > 0)
            {
                Session["WRK_TABLE_DIAGRAMACAUSAEFEITO"] = lTable;

                divAbas.Visible = true;
                for (int x = 0; x < lTable.Rows.Count; x++)
                {
                    for (int y = 0; y < chkANCE_DIAGRAMA.Items.Count; y++)
                    {
                        if (lTable.Rows[x][NC_DiagramaCausaEfeitoQD._DGRCE_TIPO.Name].ToString() == chkANCE_DIAGRAMA.Items[y].Value)
                        {
                            chkANCE_DIAGRAMA.Items[y].Selected = true;

                            switch (lTable.Rows[x][NC_DiagramaCausaEfeitoQD._DGRCE_TIPO.Name].ToString())
                            {
                                case "1":
                                    TituloabaMedida.Visible = true;
                                    txtMedida.Text = lTable.Rows[x][NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO.Name].ToString();
                                    //txtMedida.Enabled = false;
                                    ManageTabsPostBackDiagrama("abaMedida");
                                    break;
                                case "2":
                                    TituloabaMaoDeObra.Visible = true;
                                    txtMaoDeObra.Text = lTable.Rows[x][NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO.Name].ToString();
                                    //txtMaoDeObra.Enabled = false;
                                    ManageTabsPostBackDiagrama("abaMaoDeObra");
                                    break;
                                case "3":
                                    TituloabaMetodo.Visible = true;
                                    txtMetodo.Text = lTable.Rows[x][NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO.Name].ToString();
                                    //txtMetodo.Enabled = false;
                                    ManageTabsPostBackDiagrama("abaMetodo");
                                    break;
                                case "4":
                                    TituloabaMeioAmbiente.Visible = true;
                                    txtMeioAmbiente.Text = lTable.Rows[x][NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO.Name].ToString();
                                    //txtMeioAmbiente.Enabled = false;
                                    ManageTabsPostBackDiagrama("abaMeioAmbiente");
                                    break;
                                case "5":
                                    TituloabaMaquinas.Visible = true;
                                    txtMaquinas.Text = lTable.Rows[x][NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO.Name].ToString();
                                    //txtMaquinas.Enabled = false;
                                    ManageTabsPostBackDiagrama("abaMaquinas");
                                    break;
                                case "6":
                                    TituloabaMateriaPrima.Visible = true;
                                    txtMateriaPrima.Text = lTable.Rows[x][NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO.Name].ToString();
                                    //txtMateriaPrima.Enabled = false;
                                    ManageTabsPostBackDiagrama("abaMateriaPrima");
                                    break;
                                case "7":
                                    TituloabaGestao.Visible = true;
                                    txtGestao.Text = lTable.Rows[x][NC_DiagramaCausaEfeitoQD._DGRCE_DESCRICAO.Name].ToString();
                                    //txtGestao.Enabled = false;
                                    ManageTabsPostBackDiagrama("abaGestao");
                                    break;
                            }
                        }
                    }
                }
            }
            else
                Session["WRK_TABLE_DIAGRAMACAUSAEFEITO"] = null;


        }


        private void LoadPlanoDeAcao()
        {
            DataTable lTable = NC_PlanoAcaoDo.GetNC_PlanoAcaoByOCR_ID(hidOCR_ID.Value.DBToDecimal(), LocalInstance.ConnectionInfo);

            if (lTable.Rows.Count > 0)
            {
                //Para Impressão
                Session["WRK_TABLE_ACOES"] = NC_AcoesDo.GetRelatorioNC_AcoesByUnidadeId(0, lblOCR_NUMERO.Text, LocalInstance.ConnectionInfo);

                divPlanoAcao.Visible = true;
                btnPlanoAcao.Visible = false;
                //btnGerenciarPlanoAcao.Visible = false;


                //Paginação do Grid                                                                               
                grdPlanoAcao.AllowPaging = true;
                grdPlanoAcao.PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
                grdPlanoAcao.PagerStyle.HorizontalAlign = HorizontalAlign.Center;

                ViewState["WRK_TABLE_PLANOACAO"] = lTable;

                grdPlanoAcao.DataSource = ((DataTable)ViewState["WRK_TABLE_PLANOACAO"]);
                grdPlanoAcao.DataBind();


                //LoadAcoes(lTable.Rows[0][NC_PlanoAcaoQD._PLNAC_ID.Name].DBToDecimal());

                int lPlanoNaoAprovado = 0;
                for (int i = 0; i < lTable.Rows.Count; i++)
                {
                    //Seleciona Plano para Verificação da Eficácia
                    if (lTable.Rows[i][NC_PlanoAcaoQD._PLNAC_SITUACAO.Name].DBToDecimal() == (decimal)SituacaoPlanoAcao.Executado || lTable.Rows[i][NC_PlanoAcaoQD._STPLNAC_ID.Name].DBToDecimal() == (decimal)SituacaoPlanoAcaoFLUXO.EmVerificacaoEficacia)
                    {
                        txtPLNAC_NOME.Text = lTable.Rows[i][NC_PlanoAcaoQD._PLNAC_NOME.Name].ToString();
                        hidPLNAC_ID.Value = lTable.Rows[i][NC_PlanoAcaoQD._PLNAC_ID.Name].ToString();
                    }

                    //Verifica Se os planos ja existentes estão com a situação de Não Aprovado para poder permitir criar um novo plano
                    if (lTable.Rows[i][NC_PlanoAcaoQD._PLNAC_SITUACAO.Name].DBToDecimal() == (decimal)SituacaoPlanoAcao.NaoAprovado || lTable.Rows[i][NC_PlanoAcaoQD._STPLNAC_ID.Name].DBToDecimal() == (decimal)SituacaoPlanoAcaoFLUXO.Cancelado || lTable.Rows[i][NC_PlanoAcaoQD._STPLNAC_ID.Name].DBToDecimal() == (decimal)SituacaoPlanoAcaoFLUXO.ConcluídoNãoEficaz)
                    {
                        lPlanoNaoAprovado = lPlanoNaoAprovado + 1;
                    }
                }

                if (((LoginUserDo)Session["_SessionUser"]).MATRICULA.ToString() == hidMATRICULA_RESPRESOLUCAO.Value //quem recebeu
                    && hidSTCOCR_ID.Value.DBToDecimal() == (decimal)SituacaoOcorrencia.EmElaboracaoPlanoAcao)
                {
                    //btnGerenciarPlanoAcao.Visible = true;

                    //Todos os planos são Não Aprovados
                    if (lPlanoNaoAprovado == lTable.Rows.Count)
                        btnPlanoAcao.Visible = true;
                }

            }
            else
            {
                Session["WRK_TABLE_ACOES"] = null;

                if (((LoginUserDo)Session["_SessionUser"]).MATRICULA.ToString() == hidMATRICULA_RESPRESOLUCAO.Value || ((LoginUserDo)Session["_SessionUser"]).LoginName.ToString() == "ADMIN.GNC") //quem recebeu
                    btnPlanoAcao.Visible = true;
            }
        }

        private void LoadVerificacaoEficacia()
        {
            DataTable lTable = NC_VerificarEficaciaDo.GetNC_VerificarEficaciaByOCR_ID(hidOCR_ID.Value.DBToDecimal(), LocalInstance.ConnectionInfo);
            grdEficacia.AllowPaging = true;
            grdEficacia.PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            grdEficacia.PagerStyle.HorizontalAlign = HorizontalAlign.Center;

            ViewState["WRK_TABLE_VERIFICAREFICACIA"] = lTable;

            grdEficacia.DataSource = ((DataTable)ViewState["WRK_TABLE_VERIFICAREFICACIA"]);
            grdEficacia.DataBind();

            if (lTable.Rows.Count > 0)
            {
                Session["WRK_TABLE_VERIFICAREFICACIA"] = lTable;
                divVerificacaoEficacia.Visible = true;
            }
            else
            {
                Session["WRK_TABLE_VERIFICAREFICACIA"] = null;
                divVerificacaoEficacia.Visible = false;
            }
        }


        private void LoadAcoes(decimal pPLNAC_ID)
        {
            //Paginação do Grid                                                                               
            grdMain.AllowPaging = true;
            grdMain.PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            grdMain.PagerStyle.HorizontalAlign = HorizontalAlign.Center;

            ViewState["WRK_TABLE"] = NC_AcoesDo.GetNC_AcoesByPLNAC_Id(pPLNAC_ID, 0, LocalInstance.ConnectionInfo);

            grdMain.DataSource = ((DataTable)ViewState["WRK_TABLE"]);
            grdMain.DataBind();

        }


        private void ManageTabsPostBackDiagrama(string grid)
        {

            TituloabaMedida.Attributes.Add("class", "");
            abaMedida.Attributes.Add("class", "tab-pane");

            TituloabaMaoDeObra.Attributes.Add("class", "");
            abaMaoDeObra.Attributes.Add("class", "tab-pane");

            TituloabaMetodo.Attributes.Add("class", "");
            abaMetodo.Attributes.Add("class", "tab-pane");

            TituloabaMeioAmbiente.Attributes.Add("class", "");
            abaMeioAmbiente.Attributes.Add("class", "tab-pane");

            TituloabaMaquinas.Attributes.Add("class", "");
            abaMaquinas.Attributes.Add("class", "tab-pane");

            TituloabaMateriaPrima.Attributes.Add("class", "");
            abaMateriaPrima.Attributes.Add("class", "tab-pane");

            TituloabaGestao.Attributes.Add("class", "");
            abaGestao.Attributes.Add("class", "tab-pane");

            //activate the the tab and pane the user was viewing
            switch (grid)
            {
                case "abaGestao":
                    TituloabaGestao.Attributes.Add("class", "active");
                    abaGestao.Attributes.Add("class", "tab-pane active");
                    break;
                case "abaMateriaPrima":
                    TituloabaMateriaPrima.Attributes.Add("class", "active");
                    abaMateriaPrima.Attributes.Add("class", "tab-pane active");
                    break;
                case "abaMedida":
                    TituloabaMedida.Attributes.Add("class", "active");
                    abaMedida.Attributes.Add("class", "tab-pane active");
                    break;
                case "abaMaoDeObra":
                    TituloabaMaoDeObra.Attributes.Add("class", "active");
                    abaMaoDeObra.Attributes.Add("class", "tab-pane active");
                    break;
                case "abaMetodo":
                    TituloabaMetodo.Attributes.Add("class", "active");
                    abaMetodo.Attributes.Add("class", "tab-pane active");
                    break;
                case "abaMeioAmbiente":
                    TituloabaMeioAmbiente.Attributes.Add("class", "active");
                    abaMeioAmbiente.Attributes.Add("class", "tab-pane active");
                    break;
                case "abaMaquinas":
                    TituloabaMaquinas.Attributes.Add("class", "active");
                    abaMaquinas.Attributes.Add("class", "tab-pane active");
                    break;
            }
        }




        #endregion

        #region [Eventos]

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!Page.IsPostBack)
            {

                LoadRespAbertura(((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                txtOCR_DATAABERTURA.Text = DateTime.Now.ToShortDateString();


                LoadTipoOcorrencia();
                LoadUnidades();
                LoadLocalOcorrencia();
                LoadMotivosOcorrencia();
                LoadNormas();


                if (((LoginUserDo)Session["_SessionUser"]).LoginName == "ADMIN.GNC")
                {
                    txtOCR_DATAABERTURA.Enabled = true;
                    trNumeroAntigo.Visible = true;
                    ddlUnidadeAbertura.Enabled = true;
                }
                else
                {
                    txtOCR_DATAABERTURA.Enabled = false;
                    trNumeroAntigo.Visible = false;
                    ddlUnidadeAbertura.SelectedValue = ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID.ToString();
                }

                if (Request["OCR_ID"] != null)
                {
                    hidOCR_ID.Value = Request["OCR_ID"].ToString();
                    LoadDadosOcorrencia(decimal.Parse(hidOCR_ID.Value));




                }
            }
        }

        private bool LoadPermissaoAcesso()
        {
            bool lValido = false;

            //Se Elaborador
            if (((LoginUserDo)Session["_SessionUser"]).MATRICULA.ToString() == ddlFuncionario.SelectedValue || ((LoginUserDo)Session["_SessionUser"]).LoginName.ToString() == "ADMIN.GNC")//quem abriu
            {
                lValido = true;
            }

            //Se resp Resolução
            if (((LoginUserDo)Session["_SessionUser"]).MATRICULA.ToString() == hidMATRICULA_RESPRESOLUCAO.Value || ((LoginUserDo)Session["_SessionUser"]).LoginName.ToString() == "ADMIN.GNC" || ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID == 60) //quem recebeu
            {
                lValido = true;
            }

            //Se NQ
            if (((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID == 60 || ((LoginUserDo)Session["_SessionUser"]).LoginName.ToString() == "ADMIN.GNC")//Usuário do NQ || Usuário de Admin
            {
                lValido = true;
            }


            //Se Etapa Plano
            DataTable lTable = NC_AcoesDo.GetNC_AcoesByUnidadeOcorrencia(((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID, hidOCR_ID.Value.DBToDecimal(), LocalInstance.ConnectionInfo);
            if (lTable.Rows.Count > 0)
                lValido = true;




            return lValido;

        }


        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID != 60 && !LoadRespUnidade(((LoginUserDo)Session["_SessionUser"]).MATRICULA) && ((LoginUserDo)Session["_SessionUser"]).LoginName.ToString() != "ADMIN.GNC")
            {
                MessageBox1.wuc_ShowMessage("Você não tem permissão para acessar esta página.", "ConsultaRNC.aspx", 3);
            }

            if (Request["OCR_ID"] != null)
            {
                if (!LoadPermissaoAcesso())
                {
                    divDadosOcorrencia.Visible = false;
                    MessageBox1.wuc_ShowMessage("Você não tem permissão para acessar esta não conformidade.", "ConsultaRNC.aspx", 3);

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



        #region Grid


        protected void grdEficacia_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                ((GridView)sender).PageIndex = e.NewPageIndex;
                ((GridView)sender).DataSource = ((DataTable)ViewState["WRK_TABLE_VERIFICAREFICACIA"]);
                ((GridView)sender).DataBind();
            }
            catch (Exception err)
            {
                (new UnknownException(err)).TratarExcecao(true);
            }
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


        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (DataBinder.Eval(e.Row.DataItem, "ACS_SITUACAO").DBToDecimal() != (decimal)SituacaoAcao.NãoIniciada)
                    {
                        //e.Row.Cells[9].Enabled = false;
                        //e.Row.Cells[10].Enabled = false;

                        if (DataBinder.Eval(e.Row.DataItem, "ACS_SITUACAO").DBToDecimal() == (decimal)SituacaoAcao.Atrasado)
                        {
                            e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
                            e.Row.Cells[5].BackColor = System.Drawing.Color.Red;
                        }
                        else if (DataBinder.Eval(e.Row.DataItem, "ACS_SITUACAO").DBToDecimal() == (decimal)SituacaoAcao.Concluído)
                        {
                            e.Row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                            e.Row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                        }
                        else if (DataBinder.Eval(e.Row.DataItem, "ACS_SITUACAO").DBToDecimal() == (decimal)SituacaoAcao.EmExecução)
                        {
                            e.Row.Cells[4].BackColor = System.Drawing.Color.LightBlue;
                            e.Row.Cells[5].BackColor = System.Drawing.Color.LightBlue;

                            if (DataBinder.Eval(e.Row.DataItem, "ACS_DATATERMINOPREVISAO").DBToDateTime() < DateTime.Now.ToShortDateString().DBToDateTime())
                            {
                                //Atualiza status para atrasado
                                InterfaceUpdateSituacaoAcao(DataBinder.Eval(e.Row.DataItem, "ACS_ID").DBToDecimal(), (decimal)SituacaoAcao.Atrasado);
                                e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
                                e.Row.Cells[5].BackColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                    else
                    {
                        //e.Row.Cells[9].Enabled = true;
                        //e.Row.Cells[10].Enabled = true;
                    }


                }
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
                            Response.Redirect("PlanoDeAcao.aspx?OCR_ID=" + hidOCR_ID.Value + "&PLNAC_ID=" + lTable.Rows[iIndice][NC_AcoesQD._PLNAC_ID.Name].ToString() + "&ACS_ID=" + lTable.Rows[iIndice][NC_AcoesQD._ACS_ID.Name].ToString(), true);
                        }
                    }

                }

            }
            catch (Exception err)
            {
                (new UnknownException(err)).TratarExcecao(true);
            }
        }


        protected void grdPlanoAcao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                ((GridView)sender).PageIndex = e.NewPageIndex;
                ((GridView)sender).DataSource = ((DataTable)ViewState["WRK_TABLE_PLANOACAO"]);
                ((GridView)sender).DataBind();
            }
            catch (Exception err)
            {
                (new UnknownException(err)).TratarExcecao(true);
            }
        }

        protected void grdPlanoAcao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName != "Page") //Paginação                                                                          
                {
                    int iIndice = (((GridView)sender).PageIndex * ((GridView)sender).PageSize) + int.Parse(e.CommandArgument.ToString());

                    if (e.CommandName == "Visualizar")
                    {
                        DataTable lTable = (DataTable)ViewState["WRK_TABLE_PLANOACAO"];

                        if (lTable.Rows.Count > 0)
                        {
                            if (((LoginUserDo)Session["_SessionUser"]).LoginName == "ADMIN.GNC")
                                Response.Redirect("PlanoDeAcaoAdmin.aspx?OCR_ID=" + hidOCR_ID.Value + "&PLNAC_ID=" + lTable.Rows[iIndice][NC_PlanoAcaoQD._PLNAC_ID.Name].ToString(), true);
                            else
                                Response.Redirect("PlanoDeAcao.aspx?OCR_ID=" + hidOCR_ID.Value + "&PLNAC_ID=" + lTable.Rows[iIndice][NC_PlanoAcaoQD._PLNAC_ID.Name].ToString(), true);
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

        #region [Btn]

        protected void btnOk_Click(object sender, EventArgs e)
        {
            InterfaceInclude();

        }

        protected void lnkAnexoOcorrencia_Click(object sender, EventArgs e)
        {
            if (hidANXOCR_ID.Value != "")
                Response.Redirect("~/Aut/Documento/LoadArquivo.aspx?ANXOCR_ID=" + hidANXOCR_ID.Value.DBToDecimal());
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            InterfaceDelete(hidOCR_ID.Value.DBToDecimal());
        }
        protected void btnAcaoImediata_Click(object sender, EventArgs e)
        {
            if (hidSTCOCR_ID.Value.DBToDecimal() == 1)
            {
                hidSNTAC_ID.Value = "";
                txtSNTAC_DESCRICAO.Text = "";
                divAnáliseCritica.Visible = false;
                txtSNTAC_DESCRICAO.Enabled = true;
                btnOkAcaoImediata.Visible = true;
                btnCancelarAcaoImediata.Visible = true;

            }


            divAcaoImediata.Visible = true;
            txtSNTAC_DATA.Text = DateTime.Now.ToShortDateString();

            SetFocus(txtSNTAC_DESCRICAO);
            btnAcaoImediata.Visible = false;
        }

        protected void btnOkAcaoImediata_Click(object sender, EventArgs e)
        {
            if (hidSNTAC_ID.Value == "")
                InterfaceIncludeAcaoImediata();
            else
                InterfaceUpdateAcaoImediata(hidSNTAC_ID.Value.DBToDecimal());
        }

        protected void btnCancelarAcaoImediata_Click(object sender, EventArgs e)
        {
            divAcaoImediata.Visible = false;
            btnAcaoImediata.Visible = true;
        }

        protected void btnAnaliseCritica_Click(object sender, EventArgs e)
        {
            divAnáliseCritica.Visible = true;
            txtANC_DATA.Text = DateTime.Now.ToShortDateString();

            SetFocus(btnOkAnaliseCritica);
            btnAnaliseCritica.Visible = false;

            if (((LoginUserDo)Session["_SessionUser"]).LoginName.ToString() == "ADMIN.GNC")
                txtANC_DATA.Enabled = true;
            else
                txtANC_DATA.Enabled = false;
        }

        protected void btnOkAnaliseCritica_Click(object sender, EventArgs e)
        {
            InterfaceIncludeAnaliseCritica();
        }
        protected void btnCancelarAnaliseCritica_Click(object sender, EventArgs e)
        {
            divAnáliseCritica.Visible = false;
            btnAnaliseCritica.Visible = true;
        }

        protected void btnCausaEfeito_Click(object sender, EventArgs e)
        {
            divCausaEfeito.Visible = true;
            divAbas.Visible = false;
            btnCausaEfeito.Visible = false;
            txtANCE_DATA.Text = DateTime.Now.ToShortDateString();
            SetFocus(rblAnaliseCausa);
        }

        protected void btnOkCausaEfeito_Click(object sender, EventArgs e)
        {
            if (hidANCE_ID.Value == "")
                InterfaceIncludeCausaEfeito();
            else
                InterfaceUpdateDiagramaCausaEfeito();
        }

        protected void btnCancelarCausaEfeito_Click(object sender, EventArgs e)
        {
            divCausaEfeito.Visible = false;
            btnCausaEfeito.Visible = true;
        }

        protected void btnPlanoAcao_Click(object sender, EventArgs e)
        {
            Response.Redirect("PlanoDeAcao.aspx?OCR_ID=" + hidOCR_ID.Value, true);

        }

        protected void btnGerenciarPlanoAcao_Click(object sender, EventArgs e)
        {
            Response.Redirect("PlanoDeAcao.aspx?OCR_ID=" + hidOCR_ID.Value + "&PLNAC_ID=" + hidPLNAC_ID.Value, true);
        }

        protected void btnVerificacaoEficacia_Click(object sender, EventArgs e)
        {
            divVerificacaoEficacia.Visible = true;
            divNovaVerificaoEficacia.Visible = true;
            txtVRFEFC_DATA.Text = DateTime.Now.ToShortDateString();
            txtVRFEFC_DATA.Enabled = false;
            SetFocus(btnOkVerificacao);
            btnVerificacaoEficacia.Visible = false;
        }

        protected void btnOkVerificacao_Click(object sender, EventArgs e)
        {
            InterfaceIncludeVerificacaoEficacia(hidPLNAC_ID.Value.DBToDecimal());
        }

        protected void btnCancelarVerificacao_Click(object sender, EventArgs e)
        {
            divVerificacaoEficacia.Visible = false;
            divNovaVerificaoEficacia.Visible = false;
            btnVerificacaoEficacia.Visible = true;
        }

        protected void chkANCE_DIAGRAMA_SelectedIndexChanged(object sender, EventArgs e)
        {
            divAbas.Visible = true;
            int lAlgumSelecionado = 0;

            for (int i = 0; i < chkANCE_DIAGRAMA.Items.Count; i++)
            {
                switch (i.ToString())
                {
                    case "0":
                        if (chkANCE_DIAGRAMA.Items[0].Selected)
                        {
                            TituloabaMedida.Visible = true;
                            lAlgumSelecionado += lAlgumSelecionado + 1;
                            SetFocus(txtMedida);
                            ManageTabsPostBackDiagrama("abaMedida");
                        }
                        else
                            TituloabaMedida.Visible = false;
                        break;
                    case "1":
                        if (chkANCE_DIAGRAMA.Items[1].Selected)
                        {
                            TituloabaMaoDeObra.Visible = true;
                            lAlgumSelecionado += lAlgumSelecionado + 1;
                            SetFocus(txtMaoDeObra);
                            ManageTabsPostBackDiagrama("abaMaoDeObra");
                        }
                        else
                            TituloabaMaoDeObra.Visible = false;
                        break;
                    case "2":
                        if (chkANCE_DIAGRAMA.Items[2].Selected)
                        {
                            TituloabaMetodo.Visible = true;
                            lAlgumSelecionado += lAlgumSelecionado + 1;
                            SetFocus(txtMetodo);
                            ManageTabsPostBackDiagrama("abaMetodo");
                        }
                        else
                            TituloabaMetodo.Visible = false;
                        break;
                    case "3":
                        if (chkANCE_DIAGRAMA.Items[3].Selected)
                        {
                            TituloabaMeioAmbiente.Visible = true;
                            lAlgumSelecionado += lAlgumSelecionado + 1;
                            SetFocus(txtMeioAmbiente);
                            ManageTabsPostBackDiagrama("abaMeioAmbiente");
                        }
                        else
                            TituloabaMeioAmbiente.Visible = false;
                        break;
                    case "4":
                        if (chkANCE_DIAGRAMA.Items[4].Selected)
                        {
                            TituloabaMaquinas.Visible = true;
                            lAlgumSelecionado += lAlgumSelecionado + 1;
                            SetFocus(txtMaquinas);
                            ManageTabsPostBackDiagrama("abaMaquinas");
                        }
                        else
                            TituloabaMaquinas.Visible = false;
                        break;
                    case "5":
                        if (chkANCE_DIAGRAMA.Items[5].Selected)
                        {
                            TituloabaMateriaPrima.Visible = true;
                            lAlgumSelecionado += lAlgumSelecionado + 1;
                            SetFocus(txtMateriaPrima);
                            ManageTabsPostBackDiagrama("abaMateriaPrima");
                        }
                        else
                            TituloabaMateriaPrima.Visible = false;
                        break;
                    case "6":
                        if (chkANCE_DIAGRAMA.Items[6].Selected)
                        {
                            TituloabaGestao.Visible = true;
                            lAlgumSelecionado += lAlgumSelecionado + 1;
                            SetFocus(txtGestao);
                            ManageTabsPostBackDiagrama("abaGestao");
                        }
                        else
                            TituloabaGestao.Visible = false;
                        break;
                }

            }

            if (lAlgumSelecionado == 0)
                divAbas.Visible = false;
            else
                divAbas.Visible = true;



        }


        protected void rblAnaliseCausa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblAnaliseCausa.SelectedValue == "0")
            {
                divDiagramaIshikawa.Visible = true;
                divOutrosAnaliseCausa.Visible = false;
                SetFocus(btnOkCausaEfeito);
            }
            else
            {
                divDiagramaIshikawa.Visible = false;
                divOutrosAnaliseCausa.Visible = true;
                SetFocus(btnOutrosCausaEfeito);
            }


        }

        protected void lnkAnexoCausa_Click(object sender, EventArgs e)
        {
            if (hidANCE_ID.Value != "")
                Response.Redirect("~/Aut/Documento/LoadArquivo.aspx?ANCE_ID=" + hidANCE_ID.Value.DBToDecimal());
        }

        #endregion

        #endregion





        protected void grdDoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                ((GridView)sender).PageIndex = e.NewPageIndex;
                ((GridView)sender).DataSource = ((DataTable)ViewState["WRK_TABLE_DOC"]);
                ((GridView)sender).DataBind();
                SetFocus(grdDoc.Page);
            }
            catch (Exception err)
            {
                (new UnknownException(err)).TratarExcecao(true);
            }
        }

        protected void ddlMotivoOcorrencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMotivoOcorrencia.SelectedValue.DBToDecimal() == 35)
            {
                divDocumentos.Visible = false;

                if (ddlUnidadeOcorrencia.SelectedValue.DBToDecimal() == 0)
                {
                    MessageBox1.wuc_ShowMessage("Você deverá selecionar o Local da Ocorrência", 1);
                    ddlMotivoOcorrencia.SelectedValue = "0";
                }
                else
                {
                    LoadDocumentosPendentes(ddlUnidadeOcorrencia.SelectedValue.DBToDecimal());
                }
            }
            else
            {
                divDocumentos.Visible = false;
                SetFocus(chkNormas);
            }
        }

        protected void ddlUnidadeOcorrencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMotivoOcorrencia.SelectedValue.DBToDecimal() == 35)
            {
                LoadDocumentosPendentes(ddlUnidadeOcorrencia.SelectedValue.DBToDecimal());
            }
        }
    }
}