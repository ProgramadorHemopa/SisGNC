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

using System.Net.Mail;
using System.Net.Configuration;
using System.Net;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Web;


namespace HMP.WebInterface.SisRNCWeb.Www.Pages
{
    public partial class PlanoDeAcao : BaseAutPage
    {

        #region [Variaveis]

        #endregion

        #region [Metodos]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Destinatario">1-Resp. Resolucao; ou UNIDADE_ID</param>
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
                    + ", Resp. Abertura: " + ddlRespAbertura.SelectedItem.Text
                    + ", Resp. Resolução: " + ddlFuncionario.SelectedItem.Text
                    + ", " + enviaMensagem;

                string pDestinatario = "";
                if (Destinatario > 1)
                {
                    DataTable lTable = NC_OcorrenciaDo.GetEmailGestorUnidade(Destinatario, LocalInstance.ConnectionInfo);
                    pDestinatario = lTable.Rows[0]["EMAIL"].ToString();
                }
                else
                {
                    pDestinatario = GetEmailResponsavel(Destinatario);
                }

                //pDestinatario = "ricardo.paes@hemopa.pa.gov.br";

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
                client.EnableSsl = false;
                NetworkCredential cred = new NetworkCredential(remetente, password);
                client.Credentials = cred;

                // inclui as credenciais
                client.UseDefaultCredentials = true;

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
                    lEmail = lTable.Rows[ddlFuncionario.SelectedIndex]["EMAIL"].ToString();
                    break;
            }

            return lEmail;
        }



        private void LoadRespResolucaoPA()
        {
            DataTable ltable = NC_OcorrenciaDo.GetAllGestor(LocalInstance.ConnectionInfo);
            ViewState["WRK_TABLE_FUNCIONARIOS"] = ltable;
            ddlFuncionario.DataSource = ltable;
            ddlFuncionario.DataTextField = "UNIDADE_RESPONSAVEL";
            ddlFuncionario.DataValueField = "MATRICULA_RESP";
            ddlFuncionario.DataBind();
        }

        private void LoadRespAcao()
        {
            DataTable ltable = NC_OcorrenciaDo.GetAllUnidadesGestor(LocalInstance.ConnectionInfo);
            ddlUNIDADES_QUEM.DataSource = ltable;
            ddlUNIDADES_QUEM.DataTextField = "UNIDADE_RESPONSAVEL";
            ddlUNIDADES_QUEM.DataValueField = "ID";
            ddlUNIDADES_QUEM.DataBind();
            ddlUNIDADES_QUEM.Items.Insert(0, new ListItem("--Selecione--", "0"));
        }


        private void Clear()
        {
            txtACS_DATAINICIOPREVISAO.Text = "";
            txtACS_DATATERMINOPREVISAO.Text = "";
            txtACS_OQUE.Text = "";
            txtACS_PORQUE.Text = "";
            txtACS_COMO.Text = "";
            txtACS_ONDE.Text = "";
            txtACS_QUANTO.Text = "";
            txtACS_DESCRICAOEXECUCAO.Text = "";
            txtACS_DATAEXECUCAO.Text = "";
            txtACS_DESCRICAOEXECUCAO.Text = "";
            txtRPGAC_JUSTIFICATIVA.Text = "";
            txtRPGAC_JUSTIFICATIVACANCELAMENTO.Text = "";
            txtRPGAC_OBSERVACAONQ.Text = "";
            txtNovaDataInicio.Text = "";
            txtNovaDataFim.Text = "";
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

            if (!lValido)
            {
                MessageBox1.wuc_ShowMessage("Registro não incluído. Informe os seguintes campos: <br/>" + lMensagem, 3);
            }


            return lValido;
        }

        private bool ValidarConclusaoPlano()
        {
            bool lValido = true;
            string lMensagem = "";

            if (((DataTable)ViewState["WRK_TABLE"]).Rows.Count == 0)
            {
                MessageBox1.wuc_ShowMessage("Plano de Ação não pode ser concluído. Adicione ao menos uma etapa do plano de ação." + lMensagem, 3);
                lValido = false;
            }


            return lValido;
        }


        private bool ValidarInsertAcoes()
        {
            bool lValido = true;
            string lMensagem = "";

            if (ddlFuncionario.SelectedValue.DBToDecimal() == 0)
            {
                lMensagem += "-Responsável pela abertura <br/>";
                lValido = false;
            }

            if (txtACS_OQUE.Text == "")
            {
                lMensagem += "-O quê <br/>";
                lValido = false;
            }


            if (txtACS_COMO.Text == "")
            {
                lMensagem += "-Como <br/>";
                lValido = false;
            }

            if (txtACS_ONDE.Text == "")
            {
                lMensagem += "-Onde <br/>";
                lValido = false;
            }

            if (txtACS_DATAINICIOPREVISAO.Text == "")
            {
                lMensagem += "-Data Início Previsão <br/>";
                lValido = false;
            }

            if (txtACS_DATATERMINOPREVISAO.Text == "")
            {
                lMensagem += "-Data Término Previsão <br/>";
                lValido = false;
            }

            if (txtACS_DATAINICIOPREVISAO.Text != "" && txtACS_DATATERMINOPREVISAO.Text != "")
            {
                if (txtACS_DATAINICIOPREVISAO.Text.DBToDateTime() > txtACS_DATATERMINOPREVISAO.Text.DBToDateTime())
                {
                    lMensagem += "-Data Início não pode ser maior que a Data Término de Previsão <br/>";
                    lValido = false;
                }

                if (hidOCR_NUMEROANTIGO.Value == "" && (txtACS_DATATERMINOPREVISAO.Text.DBToDateTime() < DateTime.Now.ToShortDateString().DBToDateTime()))
                {
                    lMensagem += "-Data Fim não pode ser menor que a data de hoje <br/>";
                    lValido = false;
                }
                else
                {
                    if (int.Parse(hidSTCOCR_ID.Value) != (int)SituacaoOcorrencia.EmElaboracaoPlanoAcao && txtACS_DATATERMINOPREVISAO.Text.DBToDateTime() > lblDataConclusao.Text.DBToDateTime())
                    {
                        lMensagem += "-Data Fim não pode ser maior que a Data Conclusão do PA <br/>";
                        lValido = false;
                    }
                }
            }

            if (txtACS_PORQUE.Text == "")
            {
                lMensagem += "-Por quê <br/>";
                lValido = false;
            }

            if (ddlUNIDADES_QUEM.SelectedValue == "0")
            {
                lMensagem += "-Quem <br/>";
                lValido = false;
            }

            if (!lValido)
            {
                MessageBox1.wuc_ShowMessage("Registro não incluído. Informe os seguintes campos: <br/>" + lMensagem, 3);
            }


            return lValido;
        }


        private bool ValidarInsertExecucao()
        {
            bool lValido = true;
            string lMensagem = "";

            if (txtACS_DATAEXECUCAO.Text == "")
            {
                lMensagem += "-Data de Execução <br/>";
                lValido = false;
            }
            else
            {

                if (txtACS_DATAEXECUCAO.Text.DBToDateTime() > lblDataConclusao.Text.DBToDateTime())
                {
                    lMensagem += "-Data de Execução não pode ser maior que a Data de Conclusão do Plano de Ação <br/>";
                    lValido = false;
                }

                if (txtACS_DATAEXECUCAO.Text.DBToDateTime() > DateTime.Now.ToShortDateString().DBToDateTime())
                {
                    lMensagem += "-Data de Execução não pode ser maior que a data corrente <br/>";
                    lValido = false;
                }
            }

            if (txtACS_DESCRICAOEXECUCAO.Text == "")
            {
                lMensagem += "-Descrição da Execução <br/>";
                lValido = false;
            }



            if (!lValido)
            {
                MessageBox1.wuc_ShowMessage("Registro não incluído. Informe os seguintes campos: <br/>" + lMensagem, 3);
            }


            return lValido;
        }


        private bool ValidarReprogramacaoAcoesPA()
        {
            bool lValido = true;

            DataTable lTable = (DataTable)ViewState["WRK_TABLE"];


            if (lTable.Rows.Count > 0)
            {
                for (int i = 0; i < lTable.Rows.Count; i++)
                {
                    if (lTable.Rows[i][NC_AcoesQD._ACS_SITUACAO.Name].DBToDecimal() == (decimal)SituacaoAcao.DevolvidoReprogramacao || lTable.Rows[i][NC_AcoesQD._ACS_SITUACAO.Name].DBToDecimal() == (decimal)SituacaoAcao.SolicitadoReprogamacao)
                        lValido = false;
                }


            }
            return lValido;
        }


        private bool ValidarExecutarUltimaAcao()
        {
            bool lValido = true;

            DataTable lTable = (DataTable)ViewState["WRK_TABLE"];


            if (lTable.Rows.Count > 0)
            {
                for (int i = 0; i < lTable.Rows.Count; i++)
                {
                   
                    if (lTable.Rows[i][NC_AcoesQD._ACS_SITUACAO.Name].DBToDecimal() == (decimal)SituacaoAcao.DevolvidoReprogramacao || lTable.Rows[i][NC_AcoesQD._ACS_SITUACAO.Name].DBToDecimal() == (decimal)SituacaoAcao.SolicitadoReprogamacao || lTable.Rows[i][NC_AcoesQD._ACS_SITUACAO.Name].DBToDecimal() == (decimal)SituacaoAcao.Atrasado)
                        lValido = false;

                  
                }


            }
            return lValido;
        }



        private bool ValidarSolicitacaoReprogramacaoAcoes()
        {
            bool lValido = true;
            string lMensagem = "";


            if (txtNovaDataInicio.Text == "")
            {
                lMensagem += "-Nova Data Início Previsão <br/>";
                lValido = false;
            }

            if (txtNovaDataFim.Text == "")
            {
                lMensagem += "-Nova Data Término Previsão <br/>";
                lValido = false;
            }

            if (txtNovaDataInicio.Text != "" && txtNovaDataFim.Text != "")
            {
                if (txtNovaDataInicio.Text.DBToDateTime() > txtNovaDataFim.Text.DBToDateTime())
                {
                    lMensagem += "-Data Início não pode ser maior que a Data Término de Previsão <br/>";
                    lValido = false;
                }

                if (txtNovaDataFim.Text.DBToDateTime() < DateTime.Now.ToShortDateString().DBToDateTime())
                {
                    lMensagem += "-Data Fim não pode ser menor que a data de hoje <br/>";
                    lValido = false;
                }
            }

            if (txtRPGAC_JUSTIFICATIVA.Text == "")
            {
                lMensagem += "-Justificativa Reprogramação <br/>";
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

                //if (!ValidarInsert())
                //    return;

                OperationResult lReturn = new OperationResult();

                DataTable lTable = NC_PlanoAcaoDo.GetNC_PlanoAcaoByOCR_ID(hidOCR_ID.Value.DBToDecimal(), LocalInstance.ConnectionInfo);
                txtPLAC_NOME.Text = "PA" + (lTable.Rows.Count + 1).ToString();

                lFields.Add(NC_PlanoAcaoQD._OCR_ID, hidOCR_ID.Value);
                lFields.Add(NC_PlanoAcaoQD._PLNAC_NOME, txtPLAC_NOME.Text);
                lFields.Add(NC_PlanoAcaoQD._MATRICULA, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_PlanoAcaoQD._PLNAC_SITUACAO, (decimal)SituacaoPlanoAcao.EmElaboracao);
                lFields.Add(NC_PlanoAcaoQD._PLNAC_DATAREGISTRO, DateTime.Now);
                lFields.Add(NC_PlanoAcaoQD._PLNAC_REGDATE, DateTime.Now);
                lFields.Add(NC_PlanoAcaoQD._PLNAC_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_PlanoAcaoQD._PLNAC_STATUS, LocalInstance.StatusAtivo);


                lReturn = NC_PlanoAcaoDo.Insert(lFields, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    hidPLNAC_ID.Value = lReturn.SequenceControl.ToString();
                    //MessageBox1.wuc_ShowMessage("Registro salvo com sucesso.", 1);
                    btnNovaAcao.Visible = true;
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


        private void InterfaceUpdateAcoesPlano(decimal pPLNAC_ID, decimal pOCR_ID)
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                DataFieldCollection lFieldsOcorrencia = new DataFieldCollection();
                DataFieldCollection lFieldsPlanoAcao = new DataFieldCollection();


                if (!ValidarConclusaoPlano())
                    return;

                OperationResult lReturn = new OperationResult();

                //Atualizar Situação da Ocorrencia 
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_ID, pOCR_ID);
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._STCOCR_ID, (decimal)SituacaoOcorrencia.EmExecucaoPlanoAcao);
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_REGDATE, DateTime.Now);
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_STATUS, LocalInstance.StatusAtivo);

                //Atualiza Situação Plano de Ação
                lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_ID, pPLNAC_ID);
                lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_SITUACAO, (decimal)SituacaoPlanoAcao.EmExecucao);
                lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_REGDATE, DateTime.Now);
                lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_STATUS, LocalInstance.StatusAtivo);



                //Atualiza todas ações para EmExecução
                lFields.Add(NC_AcoesQD._PLNAC_ID, pPLNAC_ID);
                lFields.Add(NC_AcoesQD._ACS_SITUACAO, (decimal)SituacaoAcao.EmExecução);
                lFields.Add(NC_AcoesQD._ACS_REGDATE, DateTime.Now);
                lFields.Add(NC_AcoesQD._ACS_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_AcoesQD._ACS_STATUS, LocalInstance.StatusAtivo);


                lReturn = NC_AcoesDo.UpdateSituacaoAcoes(lFields, lFieldsOcorrencia, lFieldsPlanoAcao, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    MessageBox1.wuc_ShowMessage("Registro salvo com sucesso.", 1);
                    btnNovaAcao.Visible = false;
                    btnExcluir.Visible = false;
                    btnConcluirPlano.Visible = false;
                    hidSTCOCR_ID.Value = ((decimal)SituacaoOcorrencia.EmExecucaoPlanoAcao).ToString();
                    LoadSituacaoOcorrencia(((decimal)SituacaoOcorrencia.EmExecucaoPlanoAcao).ToString());
                    LoadPlanoDeAcao(hidPLNAC_ID.Value.DBToDecimal());

                    DataTable lTable = (DataTable)ViewState["WRK_TABLE"];
                    string lUnidadeID = " ";
                    for (int i = 0; i < lTable.Rows.Count; i++)
                    {
                        if (!lUnidadeID.Contains(" " + lTable.Rows[i][NC_AcoesQD._UNIDADES_QUEM.Name].ToString() + ","))
                        {
                            lUnidadeID += lTable.Rows[i][NC_AcoesQD._UNIDADES_QUEM.Name].ToString() + ", ";
                            EnviaMensagemEmail(lTable.Rows[i][NC_AcoesQD._UNIDADES_QUEM.Name].DBToDecimal(), "Gestão de Não Conformidade", " está em fase de execução do plano de ação. \r\r Execute a(s) etapa(s) sob sua responsabilidade. \r\r\r Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, "");
                        }
                    }

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


        private void InterfaceDeletePlano(decimal pPLNAC_ID)
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();
                DataFieldCollection lFieldsAcoes = new DataFieldCollection();


                OperationResult lReturn = new OperationResult();


                lFields.Add(NC_PlanoAcaoQD._PLNAC_ID, pPLNAC_ID);
                lFields.Add(NC_PlanoAcaoQD._PLNAC_REGDATE, DateTime.Now);
                lFields.Add(NC_PlanoAcaoQD._PLNAC_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_PlanoAcaoQD._PLNAC_STATUS, LocalInstance.StatusInativo);


                lFieldsAcoes.Add(NC_AcoesQD._PLNAC_ID, pPLNAC_ID);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_REGDATE, DateTime.Now);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_STATUS, LocalInstance.StatusInativo);



                lReturn = NC_PlanoAcaoDo.Delete(lFields, lFieldsAcoes, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    MessageBox1.wuc_ShowMessage("Registro excluído com sucesso.", "RegistroNaoConformidade.aspx?OCR_ID=" + hidOCR_ID.Value, 1);

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


        private void InterfaceDeleteAcao(decimal pACS_ID, string pACS_STATUS)
        {
            try
            {
                DataFieldCollection lFieldsAcoes = new DataFieldCollection();
                OperationResult lReturn = new OperationResult();


                lFieldsAcoes.Add(NC_AcoesQD._ACS_ID, pACS_ID);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_REGDATE, DateTime.Now);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_STATUS, pACS_STATUS);


                lReturn = NC_AcoesDo.Update(lFieldsAcoes, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    MessageBox1.wuc_ShowMessage("Registro excluído com sucesso.", 1);
                    LoadAcoes();

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


        private void InterfaceUpdateAcao(decimal pACS_ID, decimal pACS_SITUACAO)
        {
            try
            {
                DataFieldCollection lFieldsAcoes = new DataFieldCollection();
                DataFieldCollection lFieldsAnexoAcoes = new DataFieldCollection();
                DataFieldCollection lFieldsOcorrencia = new DataFieldCollection();
                DataFieldCollection lFieldsPlanoAcao = new DataFieldCollection();
                OperationResult lReturn = new OperationResult();

                if (pACS_SITUACAO == (decimal)SituacaoAcao.NãoIniciada && !ValidarInsertAcoes())
                    return;
                else if (pACS_SITUACAO == (decimal)SituacaoAcao.Concluído && !ValidarInsertExecucao())
                    return;

                lFieldsAcoes.Add(NC_AcoesQD._ACS_ID, pACS_ID);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_SITUACAO, pACS_SITUACAO);

                if (pACS_SITUACAO == (decimal)SituacaoAcao.Concluído)
                {
                    lFieldsAcoes.Add(NC_AcoesQD._ACS_DATAEXECUCAO, txtACS_DATAEXECUCAO.Text.DBToDateTime());
                    lFieldsAcoes.Add(NC_AcoesQD._ACS_DESCRICAOEXECUCAO, txtACS_DESCRICAOEXECUCAO.Text);
                    lFieldsAcoes.Add(NC_AcoesQD._PLNAC_ID, hidPLNAC_ID.Value);

                    if (fuANXACS_ARQUIVO.HasFile && fuANXACS_ARQUIVO.FileName != "")
                    {
                        if (fuANXACS_ARQUIVO.FileBytes.Length > 0)
                        {
                            lFieldsAnexoAcoes.Add(NC_AnexoAcoesQD._ACS_ID, pACS_ID);
                            lFieldsAnexoAcoes.Add(NC_AnexoAcoesQD._ANXACS_DESCRICAO, fuANXACS_ARQUIVO.FileName);
                            lFieldsAnexoAcoes.Add(NC_AnexoAcoesQD._ANXACS_ARQUIVO, fuANXACS_ARQUIVO.FileBytes);
                            lFieldsAnexoAcoes.Add(NC_AnexoAcoesQD._ANXACS_REGDATE, DateTime.Now);
                            lFieldsAnexoAcoes.Add(NC_AnexoAcoesQD._ANXACS_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                            lFieldsAnexoAcoes.Add(NC_AnexoAcoesQD._ANXACS_STATUS, LocalInstance.StatusAtivo);
                        }
                    }

                    //Atualizar Situação da Ocorrencia 
                    lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_ID, hidOCR_ID.Value);
                    lFieldsOcorrencia.Add(NC_OcorrenciaQD._STCOCR_ID, (decimal)SituacaoOcorrencia.EmVerificacaoEficacia);
                    lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_REGDATE, DateTime.Now);
                    lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                    lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_STATUS, LocalInstance.StatusAtivo);

                    //Atualiza Situação Plano de Ação
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_ID, hidPLNAC_ID.Value.DBToDecimal());
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_SITUACAO, (decimal)SituacaoPlanoAcao.Executado);
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_REGDATE, DateTime.Now);
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_STATUS, LocalInstance.StatusAtivo);

                }
                else if (pACS_SITUACAO == (decimal)SituacaoAcao.Cancelado) // Cancelado Reprogramação
                {

                    //Atualiza Situação Plano de Ação - Cancelado
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_ID, hidPLNAC_ID.Value.DBToDecimal());
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_SITUACAO, (decimal)SituacaoPlanoAcao.NaoAprovado);
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_REGDATE, DateTime.Now);
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_STATUS, LocalInstance.StatusAtivo);
                }
                else
                {
                    if (int.Parse(hidSTCOCR_ID.Value) != (decimal)SituacaoOcorrencia.EmElaboracaoPlanoAcao && txtACS_DATATERMINOPREVISAO.Text.DBToDateTime() > lblDataConclusao.Text.DBToDateTime())
                    {
                        MessageBox1.wuc_ShowMessage("Data Término Previsão não pode ser maior que a data de Conclusão do Plano de Ação.", 3);
                        return;
                    }

                    lFieldsAcoes.Add(NC_AcoesQD._ACS_DATAINICIOPREVISAO, txtACS_DATAINICIOPREVISAO.Text.DBToDateTime());
                    lFieldsAcoes.Add(NC_AcoesQD._ACS_DATATERMINOPREVISAO, txtACS_DATATERMINOPREVISAO.Text.DBToDateTime());
                    lFieldsAcoes.Add(NC_AcoesQD._ACS_OQUE, txtACS_OQUE.Text);
                    lFieldsAcoes.Add(NC_AcoesQD._ACS_PORQUE, txtACS_PORQUE.Text);
                    lFieldsAcoes.Add(NC_AcoesQD._ACS_COMO, txtACS_COMO.Text);
                    lFieldsAcoes.Add(NC_AcoesQD._ACS_ONDE, txtACS_ONDE.Text);
                    lFieldsAcoes.Add(NC_AcoesQD._ACS_QUANTO, txtACS_QUANTO.Text);
                    lFieldsAcoes.Add(NC_AcoesQD._UNIDADES_QUEM, ddlUNIDADES_QUEM.SelectedValue);

                }

                lFieldsAcoes.Add(NC_AcoesQD._ACS_REGDATE, DateTime.Now);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_STATUS, LocalInstance.StatusAtivo);


                lReturn = NC_AcoesDo.UpdateExecucaoEtapa(lFieldsAcoes, lFieldsAnexoAcoes, lFieldsOcorrencia, lFieldsPlanoAcao, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    MessageBox1.wuc_ShowMessage("Registro atualizado com sucesso.", 1);
                    Clear();
                    LoadDadosOcorrencia(hidOCR_ID.Value.DBToDecimal());
                    LoadPlanoDeAcao(hidPLNAC_ID.Value.DBToDecimal());
                    divAcoes.Visible = false;
                    divExecutarAcao.Visible = false;
                    btnOkExecucaoAcao.Visible = false;
                    hidACS_ID.Value = "";
                    if (lReturn.SequenceControl.ToString() != "")//Notifica NQ
                        EnviaMensagemEmail(60, "Gestão de Não Conformidade", " Finalizou o Plano de Ação. \r\r Faça a Verificação da Eficácia. \r\r\r Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, "");

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


        private void InterfaceUpdateValidarReprogramacaoAcao(decimal pACS_ID, decimal pACS_SITUACAO)
        {
            try
            {
                DataFieldCollection lFieldsAcoes = new DataFieldCollection();
                DataFieldCollection lFieldsReprogramacao = new DataFieldCollection();
                DataFieldCollection lFieldsPlanoAcao = new DataFieldCollection();
                DataFieldCollection lFieldsOcorrencia = new DataFieldCollection();
                OperationResult lReturn = new OperationResult();

                if (pACS_SITUACAO == (decimal)SituacaoAcao.Cancelado && txtRPGAC_JUSTIFICATIVACANCELAMENTO.Text.Trim() == "")
                {
                    MessageBox1.wuc_ShowMessage("Informe a justificativa de Cancelamento", 2);
                    return;
                }

                if (pACS_SITUACAO == (decimal)SituacaoAcao.DevolvidoReprogramacao && txtRPGAC_OBSERVACAONQ.Text.Trim() == "")
                {
                    MessageBox1.wuc_ShowMessage("Informe a uma observação", 2);
                    return;
                }

                lFieldsAcoes.Add(NC_AcoesQD._ACS_ID, pACS_ID);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_SITUACAO, pACS_SITUACAO);

                if (pACS_SITUACAO == (decimal)SituacaoAcao.EmExecução) // ok Reprogramação
                {
                    lFieldsAcoes.Add(NC_AcoesQD._ACS_DATAINICIOPREVISAO, txtNovaDataInicio.Text.DBToDateTime());
                    lFieldsAcoes.Add(NC_AcoesQD._ACS_DATATERMINOPREVISAO, txtNovaDataFim.Text.DBToDateTime());

                    //Atualiza Reprogramação Datas
                    lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_ID, hidRPGAC_ID.Value.DBToDecimal());
                    lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_DATAINICIOANTERIOR, txtACS_DATAINICIOPREVISAO.Text.DBToDateTime());
                    lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_DATAFIMANTERIOR, txtACS_DATATERMINOPREVISAO.Text.DBToDateTime());
                    lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_REGDATE, DateTime.Now);
                    lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                    lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_STATUS, LocalInstance.StatusAtivo);
                }
                else if (pACS_SITUACAO == (decimal)SituacaoAcao.DevolvidoReprogramacao) // Devolvido Reprogramação
                {
                    //Atualiza Reprogramação Datas
                    lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_ID, hidRPGAC_ID.Value.DBToDecimal());
                    lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_OBSERVACAONQ, txtRPGAC_OBSERVACAONQ.Text.Trim());
                    lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_REGDATE, DateTime.Now);
                    lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                    lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_STATUS, LocalInstance.StatusAtivo);
                }
                else if (pACS_SITUACAO == (decimal)SituacaoAcao.Cancelado) // Cancelado Reprogramação
                {
                    //Atualiza Reprogramação Justificativa
                    lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_ID, hidRPGAC_ID.Value.DBToDecimal());
                    lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_JUSTIFICATIVACANCELAMENTO, txtRPGAC_JUSTIFICATIVACANCELAMENTO.Text);
                    lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_REGDATE, DateTime.Now);
                    lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                    lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_STATUS, LocalInstance.StatusAtivo);


                    //Atualizar Situação da Ocorrencia e do Plano de Ação Se todas as outras ações Concluídas ou Canceladas
                    lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_ID, hidOCR_ID.Value);
                    lFieldsOcorrencia.Add(NC_OcorrenciaQD._STCOCR_ID, (decimal)SituacaoOcorrencia.EmVerificacaoEficacia);
                    lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_REGDATE, DateTime.Now);
                    lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                    lFieldsOcorrencia.Add(NC_OcorrenciaQD._OCR_STATUS, LocalInstance.StatusAtivo);

                    //Atualiza Situação Plano de Ação
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_ID, hidPLNAC_ID.Value.DBToDecimal());
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_SITUACAO, (decimal)SituacaoPlanoAcao.Executado);
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_REGDATE, DateTime.Now);
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                    lFieldsPlanoAcao.Add(NC_PlanoAcaoQD._PLNAC_STATUS, LocalInstance.StatusAtivo);

                }

                lFieldsAcoes.Add(NC_AcoesQD._ACS_REGDATE, DateTime.Now);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_STATUS, LocalInstance.StatusAtivo);


                lReturn = NC_AcoesDo.UpdateValidarReprogramacao(lFieldsAcoes, lFieldsPlanoAcao, lFieldsReprogramacao, lFieldsOcorrencia, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    MessageBox1.wuc_ShowMessage("Registro atualizado com sucesso.", 1);
                    Clear();
                    LoadDadosOcorrencia(hidOCR_ID.Value.DBToDecimal());
                    LoadPlanoDeAcao(hidPLNAC_ID.Value.DBToDecimal());
                    divAcoes.Visible = false;
                    divReprogramacao.Visible = false;
                    hidACS_ID.Value = "";
                    divValidarReprogramacao.Visible = false;

                    //Notifica Resp. pelo PA
                    if (pACS_SITUACAO == (decimal)SituacaoAcao.EmExecução) // ok Reprogramação
                        EnviaMensagemEmail(1, "Gestão de Não Conformidade", " Foi aprovado a Reprogramação. \r\rExecute a Etapa. \r\r\r Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, "");
                    else if (pACS_SITUACAO == (decimal)SituacaoAcao.DevolvidoReprogramacao) // Devolvido Reprogramação
                        EnviaMensagemEmail(1, "Gestão de Não Conformidade", " Foi Devolvido para Reprogramação. \r\rRefaça a Solicitação. \r\r\r Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, "");
                    else if (pACS_SITUACAO == (decimal)SituacaoAcao.Cancelado) // Cancelado Reprogramação
                        EnviaMensagemEmail(1, "Gestão de Não Conformidade", " Foi Cancelado a Etapa. \r\r\r Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, "");

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



        private void InterfaceIncludeAcoes()
        {
            try
            {
                DataFieldCollection lFields = new DataFieldCollection();

                if (!ValidarInsertAcoes())
                    return;


                OperationResult lReturn = new OperationResult();

                lFields.Add(NC_AcoesQD._PLNAC_ID, hidPLNAC_ID.Value.DBToDecimal());

                if ((int)SituacaoOcorrencia.EmElaboracaoPlanoAcao == int.Parse(hidSTCOCR_ID.Value))
                    lFields.Add(NC_AcoesQD._ACS_SITUACAO, (decimal)SituacaoAcao.NãoIniciada);
                else
                    lFields.Add(NC_AcoesQD._ACS_SITUACAO, (decimal)SituacaoAcao.EmExecução);

                lFields.Add(NC_AcoesQD._ACS_DATAINICIOPREVISAO, txtACS_DATAINICIOPREVISAO.Text.DBToDateTime());
                lFields.Add(NC_AcoesQD._ACS_DATATERMINOPREVISAO, txtACS_DATATERMINOPREVISAO.Text.DBToDateTime());
                lFields.Add(NC_AcoesQD._ACS_OQUE, txtACS_OQUE.Text);
                lFields.Add(NC_AcoesQD._ACS_PORQUE, txtACS_PORQUE.Text);
                lFields.Add(NC_AcoesQD._ACS_COMO, txtACS_COMO.Text);
                lFields.Add(NC_AcoesQD._ACS_ONDE, txtACS_ONDE.Text);
                lFields.Add(NC_AcoesQD._ACS_QUANTO, txtACS_QUANTO.Text);
                lFields.Add(NC_AcoesQD._UNIDADES_QUEM, ddlUNIDADES_QUEM.SelectedValue);
                lFields.Add(NC_AcoesQD._ACS_REGDATE, DateTime.Now);
                lFields.Add(NC_AcoesQD._ACS_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFields.Add(NC_AcoesQD._ACS_STATUS, LocalInstance.StatusAtivo);


                lReturn = NC_AcoesDo.Insert(lFields, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    MessageBox1.wuc_ShowMessage("Etapa salva com sucesso.", 1);
                    Clear();
                    LoadAcoes();
                    divAcoes.Visible = false;

                    if ((int)SituacaoOcorrencia.EmElaboracaoPlanoAcao == int.Parse(hidSTCOCR_ID.Value))
                        btnConcluirPlano.Visible = true;
                    else if ((int)SituacaoOcorrencia.EmExecucaoPlanoAcao == int.Parse(hidSTCOCR_ID.Value))
                        EnviaMensagemEmail(ddlUNIDADES_QUEM.SelectedValue.DBToDecimal(), "Gestão de Não Conformidade", " está em fase de execução do plano de ação. \r\r Execute a(s) etapa(s) sob sua responsabilidade. \r\r\r Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/ConsultaRNC.aspx?OCR_ID=" + hidOCR_ID.Value, "");
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
                    LoadAcoes();
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


        private void InterfaceIncludeReprogramarAcao(decimal pACS_ID, decimal pACS_SITUACAO)
        {
            try
            {
                DataFieldCollection lFieldsAcoes = new DataFieldCollection();
                DataFieldCollection lFieldsReprogramacao = new DataFieldCollection();
                OperationResult lReturn = new OperationResult();

                if (!ValidarSolicitacaoReprogramacaoAcoes())
                    return;

                lFieldsAcoes.Add(NC_AcoesQD._ACS_ID, pACS_ID);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_SITUACAO, pACS_SITUACAO);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_DATAINICIOPREVISAO, txtACS_DATAINICIOPREVISAO.Text.DBToDateTime());
                lFieldsAcoes.Add(NC_AcoesQD._ACS_DATATERMINOPREVISAO, txtACS_DATATERMINOPREVISAO.Text.DBToDateTime());
                lFieldsAcoes.Add(NC_AcoesQD._ACS_REGDATE, DateTime.Now);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_STATUS, LocalInstance.StatusAtivo);


                //Salva Historico reprogramação
                lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._ACS_ID, pACS_ID);
                lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_DATAINICIOANTERIOR, txtNovaDataInicio.Text.DBToDateTime());
                lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_DATAFIMANTERIOR, txtNovaDataFim.Text.DBToDateTime());
                lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_JUSTIFICATIVA, txtRPGAC_JUSTIFICATIVA.Text);
                lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_REGDATE, DateTime.Now);
                lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_STATUS, LocalInstance.StatusAtivo);


                lReturn = NC_AcoesDo.UpdateReprogramacao(lFieldsAcoes, lFieldsReprogramacao, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    MessageBox1.wuc_ShowMessage("Registro atualizado com sucesso.", 1);
                    Clear();
                    LoadDadosOcorrencia(hidOCR_ID.Value.DBToDecimal());
                    LoadPlanoDeAcao(hidPLNAC_ID.Value.DBToDecimal());
                    divAcoes.Visible = false;
                    hidACS_ID.Value = "";
                    divNovaReprogramacao.Visible = false;
                    btnOkReprogramar.Visible = false;
                    btnCancelarReprogramacao.Visible = false;
                    //Notifica NQ
                    EnviaMensagemEmail(60, "Gestão de Não Conformidade", " Necessita de Reprogramação. \r\rAvalie a Solicitação. \r\r\r Acesse http://10.95.3.2/SisGNCWeb/Aut/Page/SolicitacaoReprogramacao.aspx", "");

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


        private void InterfaceUpdateReprogramarAcao(decimal pRPGAC_ID, decimal pACS_ID, decimal pACS_SITUACAO)
        {
            try
            {
                DataFieldCollection lFieldsAcoes = new DataFieldCollection();
                DataFieldCollection lFieldsReprogramacao = new DataFieldCollection();
                OperationResult lReturn = new OperationResult();

                if (!ValidarSolicitacaoReprogramacaoAcoes())
                    return;

                lFieldsAcoes.Add(NC_AcoesQD._ACS_ID, pACS_ID);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_SITUACAO, pACS_SITUACAO);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_REGDATE, DateTime.Now);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsAcoes.Add(NC_AcoesQD._ACS_STATUS, LocalInstance.StatusAtivo);


                //Salva Historico reprogramação
                lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_ID, pRPGAC_ID);
                lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_DATAINICIOANTERIOR, txtNovaDataInicio.Text.DBToDateTime());
                lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_DATAFIMANTERIOR, txtNovaDataFim.Text.DBToDateTime());
                lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_JUSTIFICATIVA, txtRPGAC_JUSTIFICATIVA.Text);
                lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_REGDATE, DateTime.Now);
                lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_REGUSER, ((LoginUserDo)Session["_SessionUser"]).MATRICULA);
                lFieldsReprogramacao.Add(NC_ReprogramacaoAcoesQD._RPGAC_STATUS, LocalInstance.StatusAtivo);


                lReturn = NC_ReprogramacaoAcoesDo.Update(lFieldsReprogramacao, lFieldsAcoes, LocalInstance.ConnectionInfo);

                if (!lReturn.IsValid)
                {
                    Exception err = new Exception(lReturn.OperationException.Message.ToString());
                    throw err;
                }
                else
                {
                    MessageBox1.wuc_ShowMessage("Registro atualizado com sucesso.", 1);
                    Clear();
                    LoadDadosOcorrencia(hidOCR_ID.Value.DBToDecimal());
                    LoadPlanoDeAcao(hidPLNAC_ID.Value.DBToDecimal());
                    divAcoes.Visible = false;
                    hidACS_ID.Value = "";
                    divNovaReprogramacao.Visible = false;
                    btnOkReprogramar.Visible = false;
                    btnCancelarReprogramacao.Visible = false;
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

        private void LoadTipoOcorrencia()
        {
            DataTable ltable = NC_TipoOcorrenciaDo.GetAllNC_TipoOcorrencia(LocalInstance.ConnectionInfo);
            ddlTipoOcorrencia.DataSource = ltable;
            ddlTipoOcorrencia.DataTextField = "TPOCR_DESCRICAO";
            ddlTipoOcorrencia.DataValueField = "TPOCR_ID";
            ddlTipoOcorrencia.DataBind();
            ddlTipoOcorrencia.Items.Insert(0, new ListItem("--Selecione--", "0"));

        }

        private void LoadRespAberturaOcorrencia(decimal pMATRICULA)
        {
            DataTable ltable = NC_OcorrenciaDo.GetFuncionariosByMatricula(pMATRICULA, LocalInstance.ConnectionInfo);
            ddlRespAbertura.DataSource = ltable;
            ddlRespAbertura.DataTextField = "NOME";
            ddlRespAbertura.DataValueField = "MATRICULA";
            ddlRespAbertura.DataBind();
        }

        private void LoadDadosOcorrencia(decimal pOCR_ID)
        {
            try
            {
                DataTable lTable = NC_OcorrenciaDo.GetOcorrenciaById(pOCR_ID, LocalInstance.ConnectionInfo);

                if (lTable.Rows.Count > 0)
                {

                    lblOCR_NUMERO.Text = lTable.Rows[0][NC_OcorrenciaQD._OCR_NUMERO.Name].ToString();
                    hidOCR_NUMEROANTIGO.Value = lTable.Rows[0][NC_OcorrenciaQD._OCR_NUMEROANTIGO.Name].ToString();
                    ddlTipoOcorrencia.SelectedValue = lTable.Rows[0][NC_OcorrenciaQD._TPOCR_ID.Name].ToString();
                    LoadRespAberturaOcorrencia(lTable.Rows[0][NC_OcorrenciaQD._MATRICULA_RESPABERTURA.Name].DBToDecimal());
                    trEdicao.Visible = true;
                    hidSTCOCR_ID.Value = lTable.Rows[0][NC_OcorrenciaQD._STCOCR_ID.Name].ToString();

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

        private void LoadPlanoDeAcao(decimal pPLNAC_ID)
        {
            DataTable lTable = NC_PlanoAcaoDo.GetNC_PlanoAcaoById(pPLNAC_ID, LocalInstance.ConnectionInfo);

            if (lTable.Rows.Count > 0)
            {
                txtPLAC_NOME.Text = lTable.Rows[0][NC_PlanoAcaoQD._PLNAC_NOME.Name].ToString();
                txtPLAC_NOME.Enabled = false;
                lblDataConclusao.Text = lTable.Rows[0]["DATA_PREVISAO_CONCLUSAO"].DBToDateTime().ToShortDateString();
                ddlFuncionario.SelectedValue = lTable.Rows[0][NC_PlanoAcaoQD._MATRICULA.Name].ToString();
                ddlFuncionario.Enabled = false;
                LoadAcoesAtrasadas(); //Salva o ID da ultima ação atrasada 
                LoadAcoes();
            }
        }

        private void LoadSituacaoOcorrencia(string pSTCOCR_ID)
        {
            switch (pSTCOCR_ID)
            {
                case "5":
                    lblSTCOCR_ID.Text = "Em Elaboração Plano de Ação";
                    btnExcluir.Visible = false;
                    btnNovaAcao.Visible = false;
                    btnExcluir.Visible = false;
                    btnConcluirPlano.Visible = false;
                    grdMain.Columns[7].Visible = true;
                    grdMain.Columns[8].Visible = true;
                    if (((LoginUserDo)Session["_SessionUser"]).MATRICULA.ToString() == ddlFuncionario.SelectedValue || ((LoginUserDo)Session["_SessionUser"]).LoginName.ToString() == "ADMIN.GNC") //Resp. PA
                    {
                        btnNovaAcao.Visible = true;

                        if (hidACS_COUNT.Value.DBToDecimal() > 0)
                            btnConcluirPlano.Visible = true;
                    }
                    break;
                case "6":
                    lblSTCOCR_ID.Text = "Em Execução do Plano de Ação";
                    btnExcluir.Visible = false;
                    btnNovaAcao.Visible = false;
                    btnExcluir.Visible = false;
                    btnConcluirPlano.Visible = false;

                    //  if (((LoginUserDo)Session["_SessionUser"]).MATRICULA.ToString() == ddlFuncionario.SelectedValue || ((LoginUserDo)Session["_SessionUser"]).LoginName.ToString() == "ADMIN.GNC") //Resp. PA
                    if (((LoginUserDo)Session["_SessionUser"]).LoginName.ToString() == "ADMIN.GNC") //Resp. PA
                    {
                        btnNovaAcao.Visible = true;
                    }
                    break;
                case "7":
                    lblSTCOCR_ID.Text = "Em Verificação da Eficácia";
                    btnExcluir.Visible = false;
                    btnNovaAcao.Visible = false;
                    btnExcluir.Visible = false;
                    btnConcluirPlano.Visible = false;

                    break;
                case "8":
                    lblSTCOCR_ID.Text = "Concluída";
                    btnExcluir.Visible = false;
                    btnNovaAcao.Visible = false;
                    btnExcluir.Visible = false;
                    btnConcluirPlano.Visible = false;

                    break;
            }
        }

        private void LoadAcoes()
        {
            //Paginação do Grid                                                                               
            grdMain.AllowPaging = true;
            grdMain.PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            grdMain.PagerStyle.HorizontalAlign = HorizontalAlign.Center;

            DataTable lTable;
            lTable = NC_AcoesDo.GetNC_AcoesByPLNAC_Id(hidPLNAC_ID.Value.DBToDecimal(), 0, LocalInstance.ConnectionInfo);



            ViewState["WRK_TABLE"] = lTable;

            hidACS_COUNT.Value = ((DataTable)ViewState["WRK_TABLE"]).Rows.Count.ToString();

            grdMain.DataSource = lTable;
            grdMain.DataBind();

        }

        private void LoadAcoesAtrasadas()
        {

            DataTable lTableAcoesAtrasadas;
            lTableAcoesAtrasadas = NC_AcoesDo.GetNC_AcoesByPLNAC_Id(hidPLNAC_ID.Value.DBToDecimal(), (decimal)SituacaoAcao.Atrasado, LocalInstance.ConnectionInfo);

            if (lTableAcoesAtrasadas.Rows.Count > 0)
            {
                hidACS_ID_MAIORATRASADO.Value = lTableAcoesAtrasadas.Rows[0][NC_AcoesQD._ACS_ID.Name].ToString(); //ultima acao atrasada 
            }
            //  ViewState["WRK_TABLE_ACOESATRASADA"] = lTable;

        }


        private void LoadReprogramacao(decimal pACS_ID, string pACAOGRD, decimal pACS_SITUACAO)
        {
            //Paginação do Grid                                                                               
            grdReprogramacao.AllowPaging = true;
            grdReprogramacao.PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            grdReprogramacao.PagerStyle.HorizontalAlign = HorizontalAlign.Center;

            DataTable lTable;
            lTable = NC_ReprogramacaoAcoesDo.GetNC_ReprogramacaoAcoesByACS_Id(pACS_ID, LocalInstance.ConnectionInfo);

            ViewState["WRK_TABLE_REPROGRAMACAO"] = lTable;
            grdReprogramacao.DataSource = lTable;
            grdReprogramacao.DataBind();

            if (lTable.Rows.Count > 0)
            {
                divReprogramacao.Visible = true;
                txtNovaDataInicio.Text = lTable.Rows[0][NC_ReprogramacaoAcoesQD._RPGAC_DATAINICIOANTERIOR.Name].DBToDateTime().ToShortDateString();
                txtNovaDataFim.Text = lTable.Rows[0][NC_ReprogramacaoAcoesQD._RPGAC_DATAFIMANTERIOR.Name].DBToDateTime().ToShortDateString();
                txtRPGAC_JUSTIFICATIVA.Text = lTable.Rows[0][NC_ReprogramacaoAcoesQD._RPGAC_JUSTIFICATIVA.Name].ToString();
                txtRPGAC_OBSERVACAONQ.Text = lTable.Rows[0][NC_ReprogramacaoAcoesQD._RPGAC_OBSERVACAONQ.Name].ToString();
                hidRPGAC_ID.Value = lTable.Rows[0][NC_ReprogramacaoAcoesQD._RPGAC_ID.Name].ToString();

                if (pACAOGRD == "Reprogramar")
                {
                    if (pACS_SITUACAO == (decimal)SituacaoAcao.SolicitadoReprogamacao)
                    {
                        divValidarReprogramacao.Visible = true;
                        divNovaReprogramacao.Visible = false;
                        btnOkReprogramar.Visible = false;
                        btnOkValidacaoReprogramacao.Visible = true;
                        if (txtRPGAC_OBSERVACAONQ.Text == "")
                        {
                            txtRPGAC_OBSERVACAONQ.Enabled = true;
                            divJustificativaCancelamentoAcao.Visible = false;
                            btnDevolverReprogramacao.Visible = true;
                            btnCancelarReprogramacao.Visible = false;
                        }
                        else
                        {
                            txtRPGAC_OBSERVACAONQ.Enabled = false;
                            divJustificativaCancelamentoAcao.Visible = true;
                            btnDevolverReprogramacao.Visible = false;
                            btnCancelarReprogramacao.Visible = true;
                        }

                    }
                    else if (pACS_SITUACAO == (decimal)SituacaoAcao.DevolvidoReprogramacao)
                    {
                        divValidarReprogramacao.Visible = false;
                        divNovaReprogramacao.Visible = true;
                        btnOkReprogramar.Visible = true;
                    }
                    else if (pACS_SITUACAO == (decimal)SituacaoAcao.Atrasado)
                    {

                        btnNovaReprogramacao.Visible = true;
                        divReprogramacao.Visible = true;
                        /* if (lTable.Rows.Count == 1)
                         {
                             btnNovaReprogramacao.Visible = true;
                             divReprogramacao.Visible = true;

                         }*/
                    }

                }
            }
            else
            {
                if (pACAOGRD == "Reprogramar")
                {
                    divReprogramacao.Visible = true;
                    divNovaReprogramacao.Visible = true;
                    btnOkReprogramar.Visible = true;
                }
                else
                {
                    divReprogramacao.Visible = false;
                }
                hidRPGAC_ID.Value = "";
            }


        }

        private void LoadAcoes(decimal pACS_ID)
        {
            DataTable lTable = (DataTable)ViewState["WRK_TABLE"];
            int iIndice = 0;

            if (lTable.Rows.Count > 0)
            {
                for (int i = 0; i < lTable.Rows.Count; i++)
                {
                    if (lTable.Rows[i][NC_AcoesQD._ACS_ID.Name].DBToDecimal() == pACS_ID)
                        iIndice = i;
                }

                txtACS_DATAINICIOPREVISAO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_DATAINICIOPREVISAO.Name].DBToDateTime().ToShortDateString();
                txtACS_DATATERMINOPREVISAO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_DATATERMINOPREVISAO.Name].DBToDateTime().ToShortDateString();
                txtACS_OQUE.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_OQUE.Name].ToString();
                txtACS_PORQUE.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_PORQUE.Name].ToString();
                txtACS_COMO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_COMO.Name].ToString();
                txtACS_ONDE.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_ONDE.Name].ToString();
                txtACS_QUANTO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_QUANTO.Name].ToString();

                ddlUNIDADES_QUEM.SelectedValue = lTable.Rows[iIndice][NC_AcoesQD._UNIDADES_QUEM.Name].ToString();
                divAcoes.Visible = true;
                btnOkAcoes.Visible = false;

                txtACS_DATAINICIOPREVISAO.Enabled = false;
                txtACS_DATATERMINOPREVISAO.Enabled = false;
                txtACS_OQUE.Enabled = false;
                txtACS_PORQUE.Enabled = false;
                txtACS_COMO.Enabled = false;

                LoadAnexoAcoes(pACS_ID);

                if (lTable.Rows[iIndice][NC_AcoesQD._ACS_SITUACAO.Name].DBToDecimal() == (decimal)SituacaoAcao.Concluído)
                {
                    txtACS_DATAEXECUCAO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_DATAEXECUCAO.Name].DBToDateTime().ToShortDateString();
                    txtACS_DESCRICAOEXECUCAO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_DESCRICAOEXECUCAO.Name].ToString();
                    divExecutarAcao.Visible = true;
                    txtACS_DATAEXECUCAO.Enabled = false;
                    txtACS_DESCRICAOEXECUCAO.Enabled = false;


                }
                else
                {
                    divExecutarAcao.Visible = false;
                }
                btnOkExecucaoAcao.Visible = false;
            }
        }

        private void LoadAnexoAcoes(decimal pACS_ID)
        {
            DataTable lTable = NC_AnexoAcoesDo.GetNC_AnexoAcoesByACS_ID(pACS_ID, LocalInstance.ConnectionInfo);
            if (lTable.Rows.Count > 0)
            {
                hidANXACS_ID.Value = lTable.Rows[0][NC_AnexoAcoesQD._ANXACS_ID.Name].ToString();
                lnkAnexoAcao.Text = lTable.Rows[0][NC_AnexoAcoesQD._ANXACS_DESCRICAO.Name].ToString();
                divAnexoAcao.Visible = true;
            }
            else
                divAnexoAcao.Visible = false;
        }


        #endregion

        #region [Eventos]

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!Page.IsPostBack)
            {
                LoadRespResolucaoPA();
                LoadRespAcao();
                LoadTipoOcorrencia();

                if (Request["OCR_ID"] != null)
                {
                    hidOCR_ID.Value = Request["OCR_ID"].ToString();
                    LoadDadosOcorrencia(hidOCR_ID.Value.DBToDecimal());
                }



                if (Request["PLNAC_ID"] != null)
                {
                    hidPLNAC_ID.Value = Request["PLNAC_ID"].ToString();
                    LoadPlanoDeAcao(hidPLNAC_ID.Value.DBToDecimal());

                    if (Request["ACS_ID"] != null)
                    {
                        LoadAcoes(Request["ACS_ID"].DBToDecimal());
                    }
                }
                else
                {
                    ddlFuncionario.SelectedValue = ((LoginUserDo)Session["_SessionUser"]).MATRICULA.ToString();
                    InterfaceInclude();
                }

                LoadSituacaoOcorrencia(hidSTCOCR_ID.Value);
            }
        }

        #region Grid
        protected void grdReprogramacao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                ((GridView)sender).PageIndex = e.NewPageIndex;
                ((GridView)sender).DataSource = ((DataTable)ViewState["WRK_TABLE_REPROGRAMACAO"]);
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

                            txtACS_DATAINICIOPREVISAO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_DATAINICIOPREVISAO.Name].DBToDateTime().ToShortDateString();
                            txtACS_DATATERMINOPREVISAO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_DATATERMINOPREVISAO.Name].DBToDateTime().ToShortDateString();
                            txtACS_OQUE.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_OQUE.Name].ToString();
                            txtACS_PORQUE.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_PORQUE.Name].ToString();
                            txtACS_COMO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_COMO.Name].ToString();
                            txtACS_ONDE.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_ONDE.Name].ToString();
                            txtACS_QUANTO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_QUANTO.Name].ToString();

                            LoadRespAcao();
                            ddlUNIDADES_QUEM.SelectedValue = lTable.Rows[iIndice][NC_AcoesQD._UNIDADES_QUEM.Name].ToString();
                            divAcoes.Visible = true;
                            btnOkAcoes.Visible = false;

                            txtACS_DATAINICIOPREVISAO.Enabled = false;
                            txtACS_DATATERMINOPREVISAO.Enabled = false;
                            txtACS_OQUE.Enabled = false;
                            txtACS_PORQUE.Enabled = false;
                            txtACS_COMO.Enabled = false;
                            txtACS_ONDE.Enabled = false;
                            txtACS_QUANTO.Enabled = false;
                            ddlUNIDADES_QUEM.Enabled = false;

                            if (lTable.Rows[iIndice][NC_AcoesQD._ACS_SITUACAO.Name].DBToDecimal() == (decimal)SituacaoAcao.Concluído)
                            {
                                txtACS_DATAEXECUCAO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_DATAEXECUCAO.Name].DBToDateTime().ToShortDateString();
                                txtACS_DESCRICAOEXECUCAO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_DESCRICAOEXECUCAO.Name].ToString();
                                divExecutarAcao.Visible = true;
                                txtACS_DATAEXECUCAO.Enabled = false;
                                txtACS_DESCRICAOEXECUCAO.Enabled = false;
                                LoadAnexoAcoes(lTable.Rows[iIndice][NC_AcoesQD._ACS_ID.Name].DBToDecimal());
                            }
                            else
                            {
                                divExecutarAcao.Visible = false;
                            }

                            LoadReprogramacao(lTable.Rows[iIndice][NC_AcoesQD._ACS_ID.Name].DBToDecimal(), "Visualizar", 0);

                            btnOkExecucaoAcao.Visible = false;
                            SetFocus(txtACS_QUANTO);
                        }
                    }
                    else if (e.CommandName == "Executar")
                    {
                        DataTable lTable = (DataTable)ViewState["WRK_TABLE"];

                        if (lTable.Rows.Count > 0)
                        {
                            if (hidOCR_NUMEROANTIGO.Value == "" && lTable.Rows[iIndice][NC_AcoesQD._ACS_SITUACAO.Name].DBToDecimal() == (decimal)SituacaoAcao.Atrasado)
                            {
                                MessageBox1.wuc_ShowMessage("Ação atrasada. O Responsável pela Elaboração do Plano de Ação deve solicitar a reprogramação desta ação ao NQ ou alterar a data termíno de previsão.", 2);
                                return;
                            }

                            if (hidOCR_NUMEROANTIGO.Value == "" && lTable.Rows[iIndice][NC_AcoesQD._ACS_SITUACAO.Name].DBToDecimal() == (decimal)SituacaoAcao.EmExecução && !ValidarExecutarUltimaAcao() && iIndice.DBToDecimal() == (lTable.Rows.Count.DBToDecimal()-1) )
                            {
                                MessageBox1.wuc_ShowMessage("A ultima ação do PA não poderá ser executada. Pois o plano de ação ainda possui ações intermediarias", 2);
                                return;
                            }

                            hidACS_ID.Value = lTable.Rows[iIndice][NC_AcoesQD._ACS_ID.Name].ToString();
                            txtACS_DATAINICIOPREVISAO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_DATAINICIOPREVISAO.Name].DBToDateTime().ToShortDateString();
                            txtACS_DATATERMINOPREVISAO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_DATATERMINOPREVISAO.Name].DBToDateTime().ToShortDateString();
                            txtACS_OQUE.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_OQUE.Name].ToString();
                            txtACS_PORQUE.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_PORQUE.Name].ToString();
                            txtACS_COMO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_COMO.Name].ToString();
                            txtACS_ONDE.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_ONDE.Name].ToString();
                            txtACS_QUANTO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_QUANTO.Name].ToString();
                            ddlUNIDADES_QUEM.SelectedValue = lTable.Rows[iIndice][NC_AcoesQD._UNIDADES_QUEM.Name].ToString();
                            divAcoes.Visible = true;
                            btnOkAcoes.Visible = false;

                            txtACS_DATAINICIOPREVISAO.Enabled = false;
                            txtACS_DATATERMINOPREVISAO.Enabled = false;
                            txtACS_OQUE.Enabled = false;
                            txtACS_PORQUE.Enabled = false;
                            txtACS_COMO.Enabled = false;
                            txtACS_ONDE.Enabled = false;
                            txtACS_QUANTO.Enabled = false;
                            ddlUNIDADES_QUEM.Enabled = false;

                            txtACS_DATAEXECUCAO.Text = DateTime.Now.ToShortDateString();
                            txtACS_DESCRICAOEXECUCAO.Text = "";
                            txtACS_DESCRICAOEXECUCAO.Enabled = true;
                            txtACS_DATAEXECUCAO.Enabled = false;
                            lnkAnexoAcao.Text = "";

                            divExecutarAcao.Visible = true;
                            btnOkExecucaoAcao.Visible = true;

                            LoadReprogramacao(lTable.Rows[iIndice][NC_AcoesQD._ACS_ID.Name].DBToDecimal(), "Executar", 0);
                            SetFocus(btnOkExecucaoAcao);
                        }
                    }
                    else if (e.CommandName == "Reprogramar")
                    {
                        DataTable lTable = (DataTable)ViewState["WRK_TABLE"];

                        if (lTable.Rows.Count > 0)
                        {
                            hidACS_ID.Value = lTable.Rows[iIndice][NC_AcoesQD._ACS_ID.Name].ToString();
                            txtACS_DATAINICIOPREVISAO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_DATAINICIOPREVISAO.Name].DBToDateTime().ToShortDateString();
                            txtACS_DATATERMINOPREVISAO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_DATATERMINOPREVISAO.Name].DBToDateTime().ToShortDateString();
                            txtACS_OQUE.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_OQUE.Name].ToString();
                            txtACS_PORQUE.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_PORQUE.Name].ToString();
                            txtACS_COMO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_COMO.Name].ToString();
                            txtACS_ONDE.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_ONDE.Name].ToString();
                            txtACS_QUANTO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_QUANTO.Name].ToString();
                            ddlUNIDADES_QUEM.SelectedValue = lTable.Rows[iIndice][NC_AcoesQD._UNIDADES_QUEM.Name].ToString();
                            divAcoes.Visible = true;
                            btnOkAcoes.Visible = false;

                            txtACS_DATAINICIOPREVISAO.Enabled = false;
                            txtACS_DATATERMINOPREVISAO.Enabled = false;
                            txtACS_OQUE.Enabled = false;
                            txtACS_PORQUE.Enabled = false;
                            txtACS_COMO.Enabled = false;
                            txtACS_ONDE.Enabled = false;
                            txtACS_QUANTO.Enabled = false;
                            ddlUNIDADES_QUEM.Enabled = false;

                            LoadReprogramacao(lTable.Rows[iIndice][NC_AcoesQD._ACS_ID.Name].DBToDecimal(), "Reprogramar", lTable.Rows[iIndice][NC_AcoesQD._ACS_SITUACAO.Name].DBToDecimal());
                            divReprogramacao.Visible = true;
                            SetFocus(btnCancelarReprogramacao);
                        }
                    }
                    else if (e.CommandName == "Alterar")
                    {
                        DataTable lTable = (DataTable)ViewState["WRK_TABLE"];

                        if (lTable.Rows.Count > 0)
                        {
                            hidACS_ID.Value = lTable.Rows[iIndice][NC_AcoesQD._ACS_ID.Name].ToString();
                            txtACS_DATAINICIOPREVISAO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_DATAINICIOPREVISAO.Name].DBToDateTime().ToShortDateString();
                            txtACS_DATATERMINOPREVISAO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_DATATERMINOPREVISAO.Name].DBToDateTime().ToShortDateString();

                            if (txtACS_DATATERMINOPREVISAO.Text.DBToDateTime() < lblDataConclusao.Text.DBToDateTime())
                                txtACS_DATATERMINOPREVISAO.Enabled = true;
                            else
                                txtACS_DATATERMINOPREVISAO.Enabled = false;

                            txtACS_OQUE.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_OQUE.Name].ToString();
                            txtACS_PORQUE.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_PORQUE.Name].ToString();
                            txtACS_COMO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_COMO.Name].ToString();
                            txtACS_ONDE.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_ONDE.Name].ToString();
                            txtACS_QUANTO.Text = lTable.Rows[iIndice][NC_AcoesQD._ACS_QUANTO.Name].ToString();
                            ddlUNIDADES_QUEM.SelectedValue = lTable.Rows[iIndice][NC_AcoesQD._UNIDADES_QUEM.Name].ToString();
                            divAcoes.Visible = true;
                            btnOkAcoes.Visible = true;
                            SetFocus(btnOkAcoes);
                        }
                    }
                    else if (e.CommandName == "Excluir")
                    {
                        DataTable lTable = (DataTable)ViewState["WRK_TABLE"];

                        if (lTable.Rows.Count > 0)
                        {
                            InterfaceDeleteAcao(decimal.Parse(lTable.Rows[iIndice][NC_AcoesQD._ACS_ID.Name].ToString()), LocalInstance.StatusInativo);
                        }
                    }

                }

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
                    /*
                    DateTime lDataTermino = DataBinder.Eval(e.Row.DataItem, "ACS_DATATERMINOPREVISAO").DBToDateTime();

                    if (lDataTermino > lblDataConclusao.Text.DBToDateTime())
                        lblDataConclusao.Text = lDataTermino.ToShortDateString();
                    */

                    switch (int.Parse(DataBinder.Eval(e.Row.DataItem, "ACS_SITUACAO").ToString()))
                    {
                        case (int)SituacaoAcao.NãoIniciada:
                            e.Row.Cells[7].Enabled = false;
                            e.Row.Cells[8].Enabled = false;
                            e.Row.Cells[9].Enabled = false;
                            e.Row.Cells[10].Enabled = false;
                            if (ddlFuncionario.SelectedValue.DBToDecimal() == ((LoginUserDo)Session["_SessionUser"]).MATRICULA)// Resp. pelo PA pode alterar e excluir
                            {
                                e.Row.Cells[7].Enabled = true;
                                e.Row.Cells[8].Enabled = true;
                            }
                            break;
                        case (int)SituacaoAcao.Atrasado:
                            e.Row.Cells[3].BackColor = System.Drawing.Color.Red;
                            e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
                            e.Row.Cells[7].Enabled = false;
                            e.Row.Cells[8].Enabled = false;
                            if (DataBinder.Eval(e.Row.DataItem, "UNIDADES_QUEM").DBToDecimal() == ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID)
                                e.Row.Cells[9].Enabled = true; //Se tiver atrasado não pode executar ação. Exibe msg
                            else
                                e.Row.Cells[9].Enabled = false;

                            if (ddlFuncionario.SelectedValue.DBToDecimal() == ((LoginUserDo)Session["_SessionUser"]).MATRICULA)//Se tiver atrasado o Resp. pelo PA pode solicitar a reprogramação
                            {
                                //Se ação atrasado, mas dentro do prazo PA    
                                if (DataBinder.Eval(e.Row.DataItem, "ACS_DATATERMINOPREVISAO").DBToDateTime() < lblDataConclusao.Text.DBToDateTime() && lblDataConclusao.Text.DBToDateTime() > DateTime.Today.DBToDateTime())
                                {
                                    e.Row.Cells[7].Enabled = true;
                                    e.Row.Cells[10].Enabled = false;
                                    //se for o ultima ação atrasada, libera reprogramação
                                    if (e.Row.RowIndex.DBToDecimal() == (hidACS_COUNT.Value.DBToDecimal() - 1))
                                        e.Row.Cells[10].Enabled = true;

                                }
                                else// Se ação e PA atrasado
                                {
                                    e.Row.Cells[7].Enabled = false;
                                    e.Row.Cells[10].Enabled = false;
                                    if (ValidarReprogramacaoAcoesPA())//se não existe nenhuma reprogramação ou devolvido reprogramação
                                    {

                                        //se for o ultima ação atrasada, libera reprogramação
                                        if (e.Row.RowIndex.DBToDecimal() == (hidACS_COUNT.Value.DBToDecimal() - 1))
                                            e.Row.Cells[10].Enabled = true;

                                    }
                                    // e.Row.Cells[10].Enabled = true;
                                }
                            }
                            else
                                e.Row.Cells[10].Enabled = false;
                            break;
                        case (int)SituacaoAcao.EmExecução:
                            e.Row.Cells[3].BackColor = System.Drawing.Color.LightBlue;
                            e.Row.Cells[4].BackColor = System.Drawing.Color.LightBlue;
                            e.Row.Cells[7].Enabled = false;
                            e.Row.Cells[8].Enabled = false;
                            e.Row.Cells[10].Enabled = false;

                            if (ddlFuncionario.SelectedValue.DBToDecimal() == ((LoginUserDo)Session["_SessionUser"]).MATRICULA)//O Resp. PA pode Editar
                            {
                                e.Row.Cells[7].Enabled = true;
                            }


                            if (DataBinder.Eval(e.Row.DataItem, "UNIDADES_QUEM").DBToDecimal() == ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID)
                                e.Row.Cells[9].Enabled = true;
                            else
                                e.Row.Cells[9].Enabled = false;

                            //Verifica se não ta atrasada e alterar Situação
                            if (DataBinder.Eval(e.Row.DataItem, "ACS_DATATERMINOPREVISAO").DBToDateTime() < DateTime.Now.ToShortDateString().DBToDateTime())
                            {
                                //Atualiza status para atrasado
                                InterfaceUpdateSituacaoAcao(DataBinder.Eval(e.Row.DataItem, "ACS_ID").DBToDecimal(), (decimal)SituacaoAcao.Atrasado);
                                e.Row.Cells[3].BackColor = System.Drawing.Color.Red;
                                e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
                            }
                            break;
                        case (int)SituacaoAcao.Concluído:
                            e.Row.Cells[3].BackColor = System.Drawing.Color.LightGreen;
                            e.Row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                            e.Row.Cells[7].Enabled = false;
                            e.Row.Cells[8].Enabled = false;
                            e.Row.Cells[9].Enabled = false;
                            e.Row.Cells[10].Enabled = false;
                            break;
                        case (int)SituacaoAcao.SolicitadoReprogamacao:
                            e.Row.Cells[3].BackColor = System.Drawing.Color.LightSalmon;
                            e.Row.Cells[4].BackColor = System.Drawing.Color.LightSalmon;
                            e.Row.Cells[7].Enabled = false;
                            e.Row.Cells[8].Enabled = false;
                            e.Row.Cells[9].Enabled = false;

                            if (((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID == 60)//Se for NQ
                                e.Row.Cells[10].Enabled = true;
                            else
                                e.Row.Cells[10].Enabled = false;
                            break;
                        case (int)SituacaoAcao.Cancelado:
                            e.Row.Cells[3].BackColor = System.Drawing.Color.Yellow;
                            e.Row.Cells[4].BackColor = System.Drawing.Color.Yellow;
                            e.Row.Cells[7].Enabled = false;
                            e.Row.Cells[8].Enabled = false;
                            e.Row.Cells[9].Enabled = false;
                            e.Row.Cells[10].Enabled = false;
                            break;
                        case (int)SituacaoAcao.DevolvidoReprogramacao:
                            e.Row.Cells[3].BackColor = System.Drawing.Color.LightSlateGray;
                            e.Row.Cells[4].BackColor = System.Drawing.Color.LightSlateGray;
                            e.Row.Cells[7].Enabled = false;
                            e.Row.Cells[8].Enabled = false;
                            e.Row.Cells[9].Enabled = false;
                            //Verifica se não ta atrasada e alterar Situação
                            if (DataBinder.Eval(e.Row.DataItem, "ACS_DATATERMINOPREVISAO").DBToDateTime() < DateTime.Now.ToShortDateString().DBToDateTime())
                            {
                                //Atualiza status para atrasado
                                InterfaceUpdateSituacaoAcao(DataBinder.Eval(e.Row.DataItem, "ACS_ID").DBToDecimal(), (decimal)SituacaoAcao.Atrasado);
                                e.Row.Cells[3].BackColor = System.Drawing.Color.Red;
                                e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
                            }
                            if (ddlFuncionario.SelectedValue.DBToDecimal() == ((LoginUserDo)Session["_SessionUser"]).MATRICULA)//Se tiver atrasado o Resp. pelo PA pode solicitar a reprogramação
                                e.Row.Cells[10].Enabled = true;
                            else
                                e.Row.Cells[10].Enabled = false;
                            break;
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

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            InterfaceDeletePlano(hidPLNAC_ID.Value.DBToDecimal());
        }

        protected void btnNovaAcao_Click(object sender, EventArgs e)
        {
            txtACS_DATAINICIOPREVISAO.Enabled = true;
            txtACS_DATATERMINOPREVISAO.Enabled = true;
            txtACS_OQUE.Enabled = true;
            txtACS_PORQUE.Enabled = true;
            txtACS_COMO.Enabled = true;
            txtACS_ONDE.Enabled = true;
            txtACS_QUANTO.Enabled = true;
            ddlUNIDADES_QUEM.Enabled = true;
            divAcoes.Visible = true;
            hidACS_ID.Value = "";
            Clear();
            btnOkAcoes.Visible = true;
            ddlUNIDADES_QUEM.SelectedValue = ((LoginUserDo)Session["_SessionUser"]).UNIDADE_ID.ToString();
        }


        protected void btnOkAcoes_Click(object sender, EventArgs e)
        {
            if (hidACS_ID.Value == "")
                InterfaceIncludeAcoes();
            else
            {
                if (hidSTCOCR_ID.Value == ((decimal)SituacaoOcorrencia.EmElaboracaoPlanoAcao).ToString())
                    InterfaceUpdateAcao(hidACS_ID.Value.DBToDecimal(), (decimal)SituacaoAcao.NãoIniciada);
                else
                    InterfaceUpdateAcao(hidACS_ID.Value.DBToDecimal(), (decimal)SituacaoAcao.EmExecução);
            }

        }
        protected void btnCancelarAcoes_Click(object sender, EventArgs e)
        {
            hidACS_ID.Value = "";
            divAcoes.Visible = false;
            divExecutarAcao.Visible = false;
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistroNaoConformidade.aspx?OCR_ID=" + hidOCR_ID.Value, true);
        }

        protected void btnConcluirPlano_Click(object sender, EventArgs e)
        {
            InterfaceUpdateAcoesPlano(hidPLNAC_ID.Value.DBToDecimal(), hidOCR_ID.Value.DBToDecimal());
        }

        protected void btnOkExecucaoAcao_Click(object sender, EventArgs e)
        {
            InterfaceUpdateAcao(hidACS_ID.Value.DBToDecimal(), (decimal)SituacaoAcao.Concluído);
        }

        protected void btnOkReprogramar_Click(object sender, EventArgs e)
        {
            if (hidRPGAC_ID.Value == "")
                InterfaceIncludeReprogramarAcao(hidACS_ID.Value.DBToDecimal(), (decimal)SituacaoAcao.SolicitadoReprogamacao);
            else
                InterfaceUpdateReprogramarAcao(hidRPGAC_ID.Value.DBToDecimal(), hidACS_ID.Value.DBToDecimal(), (decimal)SituacaoAcao.SolicitadoReprogamacao);
        }

        protected void btnOkValidacaoReprogramacao_Click(object sender, EventArgs e)
        {
            InterfaceUpdateValidarReprogramacaoAcao(hidACS_ID.Value.DBToDecimal(), (decimal)SituacaoAcao.EmExecução);
        }

        protected void btnDevolverReprogramacao_Click(object sender, EventArgs e)
        {
            InterfaceUpdateValidarReprogramacaoAcao(hidACS_ID.Value.DBToDecimal(), (decimal)SituacaoAcao.DevolvidoReprogramacao);
        }

        protected void btnCancelarReprogramacao_Click(object sender, EventArgs e)
        {
            InterfaceUpdateValidarReprogramacaoAcao(hidACS_ID.Value.DBToDecimal(), (decimal)SituacaoAcao.Cancelado);
        }

        protected void lnkAnexoAcao_Click(object sender, EventArgs e)
        {
            if (hidANXACS_ID.Value != "")
                Response.Redirect("~/Aut/Documento/LoadArquivo.aspx?ANXACS_ID=" + hidANXACS_ID.Value.DBToDecimal());
        }
        #endregion

        #endregion




        protected void btnNovaReprogramacao_Click(object sender, EventArgs e)
        {
            divNovaReprogramacao.Visible = true;
            hidRPGAC_ID.Value = "";
            btnOkReprogramar.Visible = true;
            txtNovaDataInicio.Text = "";
            txtNovaDataFim.Text = "";
            txtRPGAC_JUSTIFICATIVA.Text = "";
            btnNovaReprogramacao.Visible = false;
            SetFocus(btnOkReprogramar);
        }
    }
}