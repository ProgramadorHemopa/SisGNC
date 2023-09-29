using System;

using System.Collections.Generic;
using System.Data;
using System.Xml;

using RPA.DataBase;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

namespace HMP.DataObjects.SisRNCWeb
{
    [Serializable]
    public class NC_OcorrenciaxDocumentoDo
    {

        #region Private Methods

        private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateConversion(pValues, pResult);
        }


        private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateRequired(NC_OcorrenciaxDocumentoQD._OCR_ID, pValues, pResult);
            GenericDataObject.ValidateRequired(NC_OcorrenciaxDocumentoQD._DOC_ID, pValues, pResult);
            GenericDataObject.ValidateRequired(NC_OcorrenciaxDocumentoQD._OCRDOC_REGDATE, pValues, pResult);
            GenericDataObject.ValidateRequired(NC_OcorrenciaxDocumentoQD._OCRDOC_REGUSER, pValues, pResult);
            GenericDataObject.ValidateRequired(NC_OcorrenciaxDocumentoQD._OCRDOC_STATUS, pValues, pResult);
        }
        #endregion

        #region Public Methods
        public static OperationResult Insert
         (
            DataFieldCollection pValues,
             Transaction pTransaction,
            ConnectionInfo pInfo
         )
        {
            Transaction lTransaction;

            bool lLocalTransaction = (pTransaction == null);

            if (lLocalTransaction)
                lTransaction = new Transaction(Instance.CreateDatabase(pInfo));
            else
                lTransaction = pTransaction;

            InsertCommand lInsert;

            OperationResult lReturn = new OperationResult(NC_OcorrenciaxDocumentoQD.TableName, NC_OcorrenciaxDocumentoQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {


                    lInsert = new InsertCommand(NC_OcorrenciaxDocumentoQD.TableName);


                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    lInsert.Execute(lTransaction);

                    if (!lReturn.HasError)
                    {
                        if (lLocalTransaction)
                        {
                            if (!lReturn.HasError)
                            {
                                lTransaction.Commit();
                            }
                            else
                            {
                                lTransaction.Rollback();
                            }
                        }
                    }
                    else
                    {
                        if (lLocalTransaction)
                            lTransaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    lReturn.OperationException = new SerializableException(ex);

                    if (lLocalTransaction)
                        lTransaction.Rollback();
                }
            }

            return lReturn;
        }

        public static OperationResult Update
              (
                  DataFieldCollection pValues,
                  ConnectionInfo pInfo
              )
        {

            Transaction pTransaction;

            pTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (pTransaction != null);

            UpdateCommand lUpdate;

            OperationResult lReturn = new OperationResult(NC_NormasxOcorrenciaQD.TableName, NC_NormasxOcorrenciaQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lUpdate = new UpdateCommand(NC_NormasxOcorrenciaQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de update");
                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_NormasxOcorrenciaQD._NRMOCR_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lUpdate.Condition = lSql;

                    lReturn.Trace("Executando o Update");

                    lUpdate.Execute(pTransaction);

                    if (!lReturn.HasError)
                    {
                        if (lLocalTransaction)
                        {
                            if (!lReturn.HasError)
                            {
                                lReturn.Trace("Update finalizado, executando commit");

                                pTransaction.Commit();
                            }
                            else
                            {
                                pTransaction.Rollback();
                            }
                        }
                    }
                    else
                    {
                        if (lLocalTransaction)
                            pTransaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    lReturn.OperationException = new SerializableException(ex);

                    if (lLocalTransaction)
                        pTransaction.Rollback();
                }
            }

            return lReturn;
        }

        public static OperationResult UpdateSituacao
        (
            DataFieldCollection pValues,
            Transaction pTransaction,
           ConnectionInfo pInfo
        )
        {
            Transaction lTransaction;

            bool lLocalTransaction = (pTransaction == null);

            if (lLocalTransaction)
                lTransaction = new Transaction(Instance.CreateDatabase(pInfo));
            else
                lTransaction = pTransaction;

            UpdateCommand lUpdate;

            OperationResult lReturn = new OperationResult(NC_OcorrenciaQD.TableName, NC_OcorrenciaQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {

                    lUpdate = new UpdateCommand(NC_OcorrenciaQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_OcorrenciaQD._OCR_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", NC_OcorrenciaQD._OCR_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(NC_OcorrenciaQD._OCR_ID.Name, pValues[NC_OcorrenciaQD._OCR_ID].DBToDecimal());

                    lUpdate.Execute(lTransaction);

                    if (!lReturn.HasError)
                    {
                        if (lLocalTransaction)
                        {
                            if (!lReturn.HasError)
                            {
                                lTransaction.Commit();
                            }
                            else
                            {
                                lTransaction.Rollback();
                            }
                        }
                    }
                    else
                    {
                        if (lLocalTransaction)
                            lTransaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    lReturn.OperationException = new SerializableException(ex);

                    if (lLocalTransaction)
                        lTransaction.Rollback();
                }
            }

            return lReturn;
        }


        public static OperationResult Delete
        (
            DataFieldCollection pValues,
            DataFieldCollection pValuesNormas,
            DataFieldCollection pValuesAnexos,
            ConnectionInfo pInfo
        )
        {

            Transaction pTransaction;

            pTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (pTransaction != null);

            UpdateCommand lUpdate;

            OperationResult lReturn = new OperationResult(NC_OcorrenciaQD.TableName, NC_OcorrenciaQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {

                    lUpdate = new UpdateCommand(NC_OcorrenciaQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_OcorrenciaQD._OCR_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", NC_OcorrenciaQD._OCR_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(NC_OcorrenciaQD._OCR_ID.Name, pValues[NC_OcorrenciaQD._OCR_ID].DBToDecimal());

                    lUpdate.Execute(pTransaction);

                    if (!lReturn.HasError)
                    {
                        lReturn = NC_NormasxOcorrenciaDo.Delete(pValuesNormas, pTransaction, pInfo);

                        if (lReturn.HasError)
                        {
                            pTransaction.Rollback();
                            return lReturn;
                        }

                        lReturn = NC_AnexoOcorrenciaDo.Delete(pValuesAnexos, pTransaction, pInfo);

                        if (lReturn.HasError)
                        {
                            pTransaction.Rollback();
                            return lReturn;
                        }


                        if (lLocalTransaction)
                        {
                            if (!lReturn.HasError)
                            {
                                pTransaction.Commit();
                            }
                            else
                            {
                                pTransaction.Rollback();
                            }
                        }
                    }
                    else
                    {
                        if (lLocalTransaction)
                            pTransaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    lReturn.OperationException = new SerializableException(ex);

                    if (lLocalTransaction)
                        pTransaction.Rollback();
                }
            }

            return lReturn;
        }

        public static DataTable GetAllNC_Ocorrencia
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qNC_OcorrenciaList;
            lQuery += " ORDER BY OCR.OCR_ID DESC ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetAllMotivo_Ocorrencia
       (
           ConnectionInfo pInfo
       )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qMotivosOcorrencia;
            lQuery += " ORDER BY MTV_ID ASC ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetOcorrenciaById
        (
            decimal pOCR_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qNC_OcorrenciaList;
            lQuery += string.Format(" AND OCR.OCR_ID = {0}", pOCR_ID);


            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetCountOcorrenciaByResponsavelUnidades
        (
            decimal pUNIDADERESOLUCAO,
            decimal pUNIDADENQ,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qNC_OcorrenciaPendentes;
            if (pUNIDADERESOLUCAO != 0)
            {
                lQuery += " AND OCR.STCOCR_ID IN (1,4,5,6,9) ";
                lQuery += string.Format(@" AND ( UND.ID = {0}
                                            OR ( OCR.OCR_STATUS = 'A' 
	                                            AND OCR.STCOCR_ID = 6
	                                            AND exists (SELECT 1 FROM NC_PLANOACAO PLNAC, NC_ACOES ACS 
				                                            WHERE OCR.OCR_ID = PLNAC.OCR_ID 
				                                            AND PLNAC.PLNAC_ID = ACS.PLNAC_ID 
				                                            AND PLNAC.PLNAC_STATUS = 'A'
                                                            AND ACS.UNIDADES_QUEM = {0} 
                                                            AND ACS.ACS_STATUS = 'A' 
                                                            AND ACS.ACS_SITUACAO NOT IN (3,6))
                                               )
                                            )
                                            ", pUNIDADERESOLUCAO);
            }
            else if (pUNIDADENQ != 0)
            {
                lQuery += string.Format(@" AND ((OCR.STCOCR_ID IN (1,4,5,6,9) 
                                            AND (UND.ID = {0} 
                                            OR ( OCR.OCR_STATUS = 'A' 
	                                            AND OCR.STCOCR_ID = 6
	                                            AND exists (SELECT 1 FROM NC_PLANOACAO PLNAC, NC_ACOES ACS 
				                                            WHERE OCR.OCR_ID = PLNAC.OCR_ID 
				                                            AND PLNAC.PLNAC_ID = ACS.PLNAC_ID 
				                                            AND PLNAC.PLNAC_STATUS = 'A'
                                                            AND ACS.UNIDADES_QUEM = {0} 
                                                            AND ACS.ACS_STATUS = 'A' 
                                                            AND ACS.ACS_SITUACAO NOT IN (3,6))
                                               )
                                            )
                                            ) 
                                            or OCR.STCOCR_ID IN (2,7)  
                                               or  exists (SELECT 1 FROM NC_PLANOACAO PLNAC, NC_ACOES ACS 
				                                            WHERE PLNAC.OCR_ID = OCR.OCR_ID 
				                                            AND PLNAC.PLNAC_ID = ACS.PLNAC_ID 
				                                            AND PLNAC.PLNAC_STATUS = 'A'                                                            
                                                            AND ACS.ACS_STATUS = 'A'
														    AND ACS.acs_dataterminoprevisao < curdate()
                                                            AND PLNAC.STPLNAC_ID = 2 AND OCR.STCOCR_ID = 6 
                                                            AND (SELECT COUNT(*) FROM nc_reprogramacaoacoes RPG1 WHERE RPG1.rpgac_status= 'A'
															AND RPG1.ACS_ID = (SELECT MAX(ACS1.ACS_ID) FROM NC_PLANOACAO PLNAC1, NC_ACOES ACS1 
				                                            WHERE PLNAC1.OCR_ID = OCR.OCR_ID
				                                            AND PLNAC1.PLNAC_ID = ACS1.PLNAC_ID 
				                                            AND PLNAC1.PLNAC_STATUS = 'A'  
                                                            AND ACS1.ACS_STATUS='A'
                                                           
                                                            ) 
                                                         
                                             )>1
                                             
                                             )
                                            ) ", pUNIDADENQ);
            }

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetOcorrenciaByResponsavelUnidadeId
        (
            decimal pUNIDADERESOLUCAO,
            decimal pUNIDADENQ,
            decimal pOCR_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qNC_OcorrenciaList;
            if (pUNIDADERESOLUCAO != 0)
            {
                lQuery += " AND OCR.STCOCR_ID IN (1,4,5,6) ";
                lQuery += string.Format(@" AND (UNDR.ID = {0}
                                            OR ( OCR.OCR_STATUS = 'A' 
	                                            AND OCR.STCOCR_ID = 6
	                                            AND exists (SELECT 1 FROM NC_PLANOACAO PLNAC, NC_ACOES ACS 
				                                            WHERE OCR.OCR_ID = PLNAC.OCR_ID 
				                                            AND PLNAC.PLNAC_ID = ACS.PLNAC_ID 
				                                            AND PLNAC.PLNAC_STATUS = 'A'
                                                            AND ACS.UNIDADES_QUEM = {0} 
                                                            AND ACS.ACS_STATUS = 'A' 
                                                            AND ACS.ACS_SITUACAO NOT IN (3,6))
                                               )
                                            )
                                            ", pUNIDADERESOLUCAO);
            }
            else if (pUNIDADENQ != 0)
            {
                lQuery += string.Format(@" AND ((OCR.STCOCR_ID IN (1,4,5,6) 
                                            AND (UNDR.ID = {0}
                                            OR ( OCR.OCR_STATUS = 'A' 
	                                            AND OCR.STCOCR_ID = 6
	                                            AND exists (SELECT 1 FROM NC_PLANOACAO PLNAC, NC_ACOES ACS 
				                                            WHERE OCR.OCR_ID = PLNAC.OCR_ID 
				                                            AND PLNAC.PLNAC_ID = ACS.PLNAC_ID 
				                                            AND PLNAC.PLNAC_STATUS = 'A'
                                                            AND ACS.UNIDADES_QUEM = {0} 
                                                            AND ACS.ACS_STATUS = 'A' 
                                                            AND ACS.ACS_SITUACAO NOT IN (3,6))
                                               )
                                            )
                                            ) 
                                            or OCR.STCOCR_ID IN (2,7)
                                        or  exists (SELECT 1 FROM NC_PLANOACAO PLNAC, NC_ACOES ACS 
				                                            WHERE PLNAC.OCR_ID = OCR.OCR_ID 
				                                            AND PLNAC.PLNAC_ID = ACS.PLNAC_ID 
				                                            AND PLNAC.PLNAC_STATUS = 'A'                                                            
                                                            AND ACS.ACS_STATUS = 'A'
														    AND ACS.acs_dataterminoprevisao < curdate()
                                                            AND PLNAC.STPLNAC_ID = 2 AND OCR.STCOCR_ID = 6 
                                                            AND (SELECT COUNT(*) FROM nc_reprogramacaoacoes RPG1 WHERE RPG1.rpgac_status= 'A'
															AND RPG1.ACS_ID = (SELECT MAX(ACS1.ACS_ID) FROM NC_PLANOACAO PLNAC1, NC_ACOES ACS1 
				                                            WHERE PLNAC1.OCR_ID = OCR.OCR_ID
				                                            AND PLNAC1.PLNAC_ID = ACS1.PLNAC_ID 
				                                            AND PLNAC1.PLNAC_STATUS = 'A'  
                                                            AND ACS1.ACS_STATUS='A'
                                                           
                                                            ) 
                                                         
                                             )>1
                                             
                                             )
                                            
                                            

                                             
                                             
                                            

                                            ) ", pUNIDADENQ);
            }
            else if (pOCR_ID != 0)
            {
                lQuery += string.Format(" AND OCR.OCR_ID = {0} ", pOCR_ID);
            }

            lQuery += " ORDER BY OCR.OCR_ID ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetOcorrenciaByParametros
        (
            decimal pMatriculaAbertura,
            string pDataAbertura,
            decimal pTipo,
            string pDataOcorrencia,
            decimal pUnidadeOcorrencia,
            decimal pMotivo,
            string pDescricao,
            decimal pUnidadeResolucao,
            decimal pSituacao,
            string pOCR_NUMERO,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qNC_OcorrenciaList;

            if (pMatriculaAbertura != 0)
                lQuery += string.Format(" AND OCR.MATRICULA_RESPABERTURA = {0}", pMatriculaAbertura);

            if (pDataAbertura != "" && pDataAbertura != "0001-01-01")
                lQuery += string.Format(" AND OCR.OCR_DATAABERTURA = '{0}'", pDataAbertura);

            if (pTipo != 0)
                lQuery += string.Format(" AND OCR.TPOCR_ID = {0}", pTipo);

            if (pDataOcorrencia != "" && pDataOcorrencia != "0001-01-01")
                lQuery += string.Format(" AND OCR.OCR_DATAOCORRENCIA = '{0}'", pDataOcorrencia);

            if (pUnidadeOcorrencia != 0)
                lQuery += string.Format(" AND OCR.UNIDADE_LOCALOCORRENCIA = {0}", pUnidadeOcorrencia);

            if (pMotivo != 0)
                lQuery += string.Format(" AND OCR.MTV_ID = {0}", pMotivo);

            if (pDescricao != "")
                lQuery += string.Format(" AND OCR.OCR_DESCRICAO LIKE '%{0}%'", pDescricao);

            if (pUnidadeResolucao != 0)
                lQuery += string.Format(" AND OCR.UNIDADE_RESPRESOLUCAO = {0}", pUnidadeResolucao);

            if (pSituacao != 0)
                lQuery += string.Format(" AND OCR.STCOCR_ID = {0}", pSituacao);

            if (pOCR_NUMERO != "")
                lQuery += string.Format(" AND OCR.OCR_NUMERO = '{0}'", pOCR_NUMERO);


            lQuery += " ORDER BY OCR.OCR_ID DESC ";


            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetRelatorioOcorrenciaByPeriodoUnidade
        (
            string pDataInicio,
            string pDataFim,
            decimal pUnidade,
            decimal pMotivo,
            decimal pSituacao,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qNC_OcorrenciaRelatorioList;


            if (pDataInicio != "" && pDataFim != "" && pDataInicio != "0001-01-01" && pDataFim != "0001-01-01")
                lQuery += string.Format(" AND OCR.OCR_DATAABERTURA BETWEEN '{0}' AND '{1}'", pDataInicio, pDataFim);

            if (pUnidade != 0)
                lQuery += string.Format(" AND UNDR.ID = {0}", pUnidade);

            if(pSituacao == 99)
                lQuery += @" AND OCR.STCOCR_ID IN (8,3) AND EXISTS(SELECT 0 FROM NC_PLANOACAO PLNAC WHERE PLNAC.OCR_ID = OCR.OCR_ID AND PLNAC.PLNAC_STATUS = 'A' 
                            AND (PLNAC.STPLNAC_ID = 5 OR PLNAC.PLNAC_SITUACAO = 5) AND PLNAC.PLNAC_ID = (SELECT MAX(PLNAC.PLNAC_ID) FROM NC_PLANOACAO PLNAC WHERE PLNAC.OCR_ID = OCR.OCR_ID AND PLNAC.PLNAC_STATUS = 'A'))  ";
            else if (pSituacao != 0)
                lQuery += string.Format(" AND OCR.STCOCR_ID = {0}", pSituacao);

            if (pMotivo != 0)
                lQuery += string.Format(" AND MTV.MTV_ID = {0}", pMotivo);

            lQuery += " ORDER BY OCR.OCR_DATAABERTURA ";


            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }



        public static DataTable GetOcorrenciaSolicitacaoReprogramacaoByParametros
        (
            decimal pUnidadeResolucao,
            string pOCR_NUMERO,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qNC_OcorrenciaList;

            //Ação com situação de Solicitado Reprogramação
            lQuery += @" AND EXISTS (SELECT 0 FROM NC_PLANOACAO PLNAC, NC_ACOES ACS
										WHERE OCR.OCR_ID = PLNAC.OCR_ID AND PLNAC.PLNAC_ID = ACS.PLNAC_ID
                                        AND PLNAC.PLNAC_STATUS = 'A' AND ACS.ACS_STATUS = 'A'
                                        AND ACS.ACS_SITUACAO = 5) ";

            if (pUnidadeResolucao != 0)
                lQuery += string.Format(" AND OCR.UNIDADE_RESPRESOLUCAO = {0}", pUnidadeResolucao);

            if (pOCR_NUMERO != "")
                lQuery += string.Format(" AND OCR.OCR_NUMERO = '{0}'", pOCR_NUMERO);

            lQuery += " ORDER BY OCR.OCR_ID ";


            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable Get_DocumentoNaoConformeByUnidade
      (
 
          decimal pUNIDADE_RESPONSAVEL,          
          ConnectionInfo pInfo
      )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qNC_OcorrenciaDocumentosPendentes;
       
            if (pUNIDADE_RESPONSAVEL != 0)
                lQuery += string.Format("  AND UNIDADE_RESPONSAVEL = {0} ", pUNIDADE_RESPONSAVEL);
          
            lQuery += " order by DOC_CODIGO,doc_dataelaboracao ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetAllUnidades
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qUnidades;
            lQuery += " ORDER BY SIGLA ";

            MySqlDo lSqlDo = new MySqlDo();
            lTable = lSqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetAllUnidadesGestor
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qUnidadesVinculos;
            lQuery += " ORDER BY und.sigla ";

            MySqlDo lSqlDo = new MySqlDo();
            lTable = lSqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetUnidadesGestor
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qUnidadesDistinct;
            lQuery += " ORDER BY und.sigla ";

            MySqlDo lSqlDo = new MySqlDo();
            lTable = lSqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetAllLocalOcorrencia
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qUnidadesLocal;
            lQuery += " ORDER BY und.sigla ";

            MySqlDo lSqlDo = new MySqlDo();
            lTable = lSqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetAllGestor
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qUnidadesVinculos;
            lQuery += " ORDER BY fun.nome ";

            MySqlDo lSqlDo = new MySqlDo();
            lTable = lSqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetEmailGestorUnidade
        (
            decimal pUNIDADE_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qUnidadesVinculos;
            lQuery += string.Format(" AND UND.ID = {0}", pUNIDADE_ID);

            MySqlDo lSqlDo = new MySqlDo();
            lTable = lSqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }



        public static DataTable GetUnidadeResp
        (
            decimal pMATRICULA,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qUnidadesVinculosLogin;
            lQuery += string.Format(" and fun.matricula = {0} ", pMATRICULA);

            MySqlDo lSqlDo = new MySqlDo();
            lTable = lSqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetUnidadeRespByMatricula
        (
            decimal pMATRICULA,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qUnidadesVinculos;
            lQuery += string.Format(" and fun.matricula = {0} ", pMATRICULA);

            MySqlDo lSqlDo = new MySqlDo();
            lTable = lSqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetFuncionariosByMatricula
        (
            decimal pMATRICULA,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qFuncionarios;
            lQuery += string.Format("  AND FUN.MATRICULA = {0}", pMATRICULA);

            MySqlDo lSqlDo = new MySqlDo();
            lTable = lSqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetOcorrenciaByUnidadeeSituacao
        (
            decimal pUnidadeResolucao,
            decimal pSituacao,
            string pOCR_NUMERO,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_OcorrenciaQD.qNC_OcorrenciaList;


            if (pUnidadeResolucao != 0)
                lQuery += string.Format(" AND OCR.UNIDADE_RESPRESOLUCAO = {0}", pUnidadeResolucao);

            if (pOCR_NUMERO != "")
                lQuery += string.Format(" AND OCR.OCR_NUMERO = '{0}'", pOCR_NUMERO);

            if (pSituacao != 0)
                lQuery += string.Format(" AND OCR.STCOCR_ID = {0}", pSituacao);

            else if (pSituacao == 1)
                lQuery += "and adddate(ocr_dataabertura, INTERVAL 5 DAY) < curdate()";

            else if (pSituacao == 4)
                lQuery += "and (exists(select 0 from nc_analisecritica anc where anc.ocr_id = ocr.ocr_id and adddate(anc.anc_data, INTERVAL 5 DAY) < CURDATE()) and not exists(select 0 from nc_analisecausaefeito ance where ance.ocr_id = ocr.ocr_id))";

            else if (pSituacao == 5)
                lQuery += "and (exists(select 0 from nc_analisecausaefeito ance where ance.ocr_id = ocr.ocr_id and adddate(ance.ance_data, INTERVAL 5 DAY) < CURDATE()) and not exists(select 0 from nc_planoacao plnac where plnac.ocr_id = ocr.ocr_id))";

            else if (pSituacao == 6)
                lQuery += "AND exists (SELECT MAX(ACS.ACS_DATATERMINOPREVISAO) < curdate() FROM nc_acoes ACS, nc_planoacao PLNAC WHERE ACS.plnac_id = PLNAC.plnac_id AND PLNAC.ocr_id = OCR.ocr_id)";


            lQuery += " ORDER BY OCR.OCR_DATAABERTURA DESC ";


            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        #endregion
    }
}
