using System;                                                                         

using System.Collections.Generic;                                                     
using System.Data;                                                                    
using System.Xml;                                                                     

using RPA.DataBase;                                                         
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

namespace HMP.DataObjects.SisRNCWeb
{
    [Serializable]
    public class NC_AcoesDo
    {

        #region Private Methods

        private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateConversion(pValues, pResult);
        }


        private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult)
        {
            //GenericDataObject.ValidateRequired(NC_AcoesQD._ACS_ID, pValues, pResult);
        }

        #endregion

        #region Public Methods
        public static OperationResult Insert
        (
           DataFieldCollection pValues,
           ConnectionInfo pInfo
        )
        {
            Transaction lTransaction;

            lTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (lTransaction != null);

            InsertCommand lInsert;

            OperationResult lReturn = new OperationResult(NC_AcoesQD.TableName, NC_AcoesQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lInsert = new InsertCommand(NC_AcoesQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de insert");

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }
                    decimal lSequence;
                    lSequence = DataBaseSequenceControl.GetNext(pInfo, "ACS_ID");
                    lInsert.Fields.Add(NC_AcoesQD._ACS_ID.Name, lSequence, (ItemType)NC_AcoesQD._ACS_ID.DBType);

                    lReturn.Trace("Executando o Insert");

                    lInsert.Execute(lTransaction);

                    if (!lReturn.HasError)
                    {
                        if (lLocalTransaction)
                        {
                            if (!lReturn.HasError)
                            {
                                lReturn.Trace("Insert finalizado, executando commit");

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

            OperationResult lReturn = new OperationResult(NC_AcoesQD.TableName, NC_AcoesQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    lUpdate = new UpdateCommand(NC_AcoesQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_AcoesQD._ACS_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", NC_AcoesQD._ACS_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(NC_AcoesQD._ACS_ID.Name, pValues[NC_AcoesQD._ACS_ID].DBToDecimal());

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

        public static OperationResult Update
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

            OperationResult lReturn = new OperationResult(NC_AcoesQD.TableName, NC_AcoesQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    lUpdate = new UpdateCommand(NC_AcoesQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_AcoesQD._ACS_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", NC_AcoesQD._ACS_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(NC_AcoesQD._ACS_ID.Name, pValues[NC_AcoesQD._ACS_ID].DBToDecimal());

                    lUpdate.Execute(pTransaction);

                    if (!lReturn.HasError)
                    {

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


        public static OperationResult UpdateExecucaoEtapa
         (
            DataFieldCollection pValues,
            DataFieldCollection pValuesAnexo, 
            DataFieldCollection pValuesOcorrencia,
            DataFieldCollection pValuesPlanoAcao,
            ConnectionInfo pInfo
         )
        {

            Transaction pTransaction;

            pTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (pTransaction != null);

            UpdateCommand lUpdate;

            OperationResult lReturn = new OperationResult(NC_AcoesQD.TableName, NC_AcoesQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    lUpdate = new UpdateCommand(NC_AcoesQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_AcoesQD._ACS_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", NC_AcoesQD._ACS_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(NC_AcoesQD._ACS_ID.Name, pValues[NC_AcoesQD._ACS_ID].DBToDecimal());

                    lUpdate.Execute(pTransaction);
                    if (!lReturn.HasError)
                    {
                        if (pValues[NC_AcoesQD._ACS_SITUACAO].DBToDecimal() == (decimal)SituacaoAcao.Concluído && pValuesPlanoAcao.Count != 0  || pValues[NC_AcoesQD._ACS_SITUACAO].DBToDecimal() == (decimal)SituacaoAcao.ConcluídoJustificado && pValuesPlanoAcao.Count != 0)
                        {

                            if (pValuesAnexo.Count != 0)
                            {
                                //Atualiza Situacao PlanoDeAção
                                lReturn = NC_AnexoAcoesDo.Insert(pValuesAnexo, pTransaction, pInfo);

                                if (lReturn.HasError)
                                {
                                    pTransaction.Rollback();
                                    return lReturn;
                                }
                            }

                            DataTable lTable = NC_AcoesDo.GetNC_AcoesByPLNAC_IdConcluido(pValuesPlanoAcao[NC_PlanoAcaoQD._PLNAC_ID].DBToDecimal(), pInfo);

                            if (lTable.Rows.Count == 1)
                            { 
                                //Se todas ações Concluídas ou Canceladas, atualiza Situação da Ocorrência e Situação do Plano

                                lReturn = NC_OcorrenciaDo.UpdateSituacao(pValuesOcorrencia, pTransaction, pInfo);

                                if (lReturn.HasError)
                                {
                                    pTransaction.Rollback();
                                    return lReturn;
                                }

                                //Atualiza Situacao PlanoDeAção
                                lReturn = NC_PlanoAcaoDo.UpdateSituacao(pValuesPlanoAcao, pTransaction, pInfo);

                                if (lReturn.HasError)
                                {
                                    pTransaction.Rollback();
                                    return lReturn;
                                }

                                lReturn.SequenceControl = 1;
                            }
                        }
                        /*
                        else
                        {
                            if(pValuesPlanoAcao.Count != 0)
                            {                     
                                                                                         
                                //Atualiza Situacao PlanoDeAção
                                lReturn = NC_PlanoAcaoDo.UpdateSituacao(pValuesPlanoAcao, pTransaction, pInfo);

                                if (lReturn.HasError)
                                {
                                    pTransaction.Rollback();
                                    return lReturn;
                                }
                            }

                        }*/

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


        public static OperationResult UpdateValidarReprogramacao
        (
             DataFieldCollection pValues,
             DataFieldCollection pValuesPlanoAcao,
             DataFieldCollection pValuesReprogramacao,
             DataFieldCollection pValuesOcorrencia,
             ConnectionInfo pInfo
        )
        {

            Transaction pTransaction;

            pTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (pTransaction != null);

            UpdateCommand lUpdate;

            OperationResult lReturn = new OperationResult(NC_AcoesQD.TableName, NC_AcoesQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    lUpdate = new UpdateCommand(NC_AcoesQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_AcoesQD._ACS_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", NC_AcoesQD._ACS_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(NC_AcoesQD._ACS_ID.Name, pValues[NC_AcoesQD._ACS_ID].DBToDecimal());

                    lUpdate.Execute(pTransaction);

                    if (!lReturn.HasError)
                    {

                        if (pValuesReprogramacao.Count != 0)
                        {
                            lReturn = NC_ReprogramacaoAcoesDo.Update(pValuesReprogramacao, pTransaction, pInfo);

                            if (lReturn.HasError)
                            {
                                pTransaction.Rollback();
                                return lReturn;
                            }
                        }

                        if (pValues[NC_AcoesQD._ACS_SITUACAO].DBToDecimal() == (decimal)SituacaoAcao.Cancelado && pValuesPlanoAcao.Count != 0)
                        {

                            DataTable lTable = NC_AcoesDo.GetNC_AcoesByPLNAC_IdConcluido(pValuesPlanoAcao[NC_PlanoAcaoQD._PLNAC_ID].DBToDecimal(), pInfo);

                            if (lTable.Rows.Count == 1)
                            {
                                //Se todas ações Concluídas ou Canceladas, atualiza Situação da Ocorrência e Situação do Plano

                                lReturn = NC_OcorrenciaDo.UpdateSituacao(pValuesOcorrencia, pTransaction, pInfo);

                                if (lReturn.HasError)
                                {
                                    pTransaction.Rollback();
                                    return lReturn;
                                }

                                //Atualiza Situacao PlanoDeAção
                                lReturn = NC_PlanoAcaoDo.UpdateSituacao(pValuesPlanoAcao, pTransaction, pInfo);

                                if (lReturn.HasError)
                                {
                                    pTransaction.Rollback();
                                    return lReturn;
                                }
                            }
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


        public static OperationResult UpdateReprogramacao
         (
             DataFieldCollection pValues,
             DataFieldCollection pValuesReprogramacao,
             ConnectionInfo pInfo
         )
        {

            Transaction pTransaction;

            pTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (pTransaction != null);

            UpdateCommand lUpdate;

            OperationResult lReturn = new OperationResult(NC_AcoesQD.TableName, NC_AcoesQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    lUpdate = new UpdateCommand(NC_AcoesQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_AcoesQD._ACS_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", NC_AcoesQD._ACS_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(NC_AcoesQD._ACS_ID.Name, pValues[NC_AcoesQD._ACS_ID].DBToDecimal());

                    lUpdate.Execute(pTransaction);

                    if (!lReturn.HasError)
                    {
                        if (pValuesReprogramacao.Count != 0)
                        {

                            lReturn = NC_ReprogramacaoAcoesDo.Insert(pValuesReprogramacao, pTransaction, pInfo);

                            if (lReturn.HasError)
                            {
                                pTransaction.Rollback();
                                return lReturn;
                            }

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


        public static OperationResult UpdateSituacaoAcoes_Ineficaz
        (
            DataFieldCollection pValues,
            //DataFieldCollection pValuesOcorrencia,
            DataFieldCollection pValuesPlanoAcao,
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

            OperationResult lReturn = new OperationResult(NC_AcoesQD.TableName, NC_AcoesQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    lUpdate = new UpdateCommand(NC_AcoesQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_AcoesQD._PLNAC_ID.Name) && (lField.Name != NC_AcoesQD._ACS_STATUS.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0} AND {1} = <<{1} AND ACS_SITUACAO IN(4,6)", NC_AcoesQD._PLNAC_ID.Name, NC_AcoesQD._ACS_STATUS.Name);
                    lUpdate.Condition = lSql;

                    lUpdate.Conditions.Add(NC_AcoesQD._PLNAC_ID.Name, pValues[NC_AcoesQD._PLNAC_ID].DBToDecimal());
                    lUpdate.Conditions.Add(NC_AcoesQD._ACS_STATUS.Name, pValues[NC_AcoesQD._ACS_STATUS].ToString());

                    lUpdate.Execute(pTransaction);

                    if (!lReturn.HasError)
                    {

                        /*Atualiza Situacao Ocorrencia
                        lReturn = NC_OcorrenciaDo.UpdateSituacao(pValuesOcorrencia, pTransaction, pInfo);

                        if (lReturn.HasError)
                        {
                            pTransaction.Rollback();
                            return lReturn;
                        }*/

                        //Atualiza Situacao PlanoDeAção
                        lReturn = NC_PlanoAcaoDo.UpdateSituacao(pValuesPlanoAcao, pTransaction, pInfo);

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


        public static OperationResult UpdateSituacaoAcoes
        (
            DataFieldCollection pValues,
            DataFieldCollection pValuesOcorrencia,
            DataFieldCollection pValuesPlanoAcao,
            ConnectionInfo pInfo
        )
        {

            Transaction pTransaction;

            pTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (pTransaction != null);

            UpdateCommand lUpdate;

            OperationResult lReturn = new OperationResult(NC_AcoesQD.TableName, NC_AcoesQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    lUpdate = new UpdateCommand(NC_AcoesQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_AcoesQD._PLNAC_ID.Name) && (lField.Name != NC_AcoesQD._ACS_STATUS.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0} AND {1} = <<{1}", NC_AcoesQD._PLNAC_ID.Name, NC_AcoesQD._ACS_STATUS.Name);
                    lUpdate.Condition = lSql;

                    lUpdate.Conditions.Add(NC_AcoesQD._PLNAC_ID.Name, pValues[NC_AcoesQD._PLNAC_ID].DBToDecimal());
                    lUpdate.Conditions.Add(NC_AcoesQD._ACS_STATUS.Name, pValues[NC_AcoesQD._ACS_STATUS].ToString());

                    lUpdate.Execute(pTransaction);

                    if (!lReturn.HasError)
                    {

                        //Atualiza Situacao Ocorrencia
                        lReturn = NC_OcorrenciaDo.UpdateSituacao(pValuesOcorrencia, pTransaction, pInfo);

                        if (lReturn.HasError)
                        {
                            pTransaction.Rollback();
                            return lReturn;
                        }

                        //Atualiza Situacao PlanoDeAção
                        lReturn = NC_PlanoAcaoDo.UpdateSituacao(pValuesPlanoAcao, pTransaction, pInfo);

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

        public static OperationResult Delete
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

            OperationResult lReturn = new OperationResult(NC_AcoesQD.TableName, NC_AcoesQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {

                    lUpdate = new UpdateCommand(NC_AcoesQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_AcoesQD._PLNAC_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", NC_AcoesQD._PLNAC_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(NC_AcoesQD._PLNAC_ID.Name, pValues[NC_AcoesQD._PLNAC_ID].DBToDecimal());


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

        public static DataTable GetAllNC_Acoes
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_AcoesQD.qNC_AcoesList;
            lQuery += " WHERE ACS_STATUS='A'";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetNC_AcoesByPLNAC_Id
        (
            decimal pPLNAC_ID,
            decimal pACS_SITUACAO,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_AcoesQD.qNC_AcoesList;
            lQuery += string.Format(" WHERE ACS_STATUS='A' AND PLNAC_ID = {0} ", pPLNAC_ID);

            if (pACS_SITUACAO != 0)
                lQuery += string.Format("AND ACS_SITUACAO = {0} ", pACS_SITUACAO);

            
            if (pACS_SITUACAO == 4)
                lQuery += " ORDER BY ACS_ID DESC";
            else
                lQuery += " ORDER BY ACS_DATAINICIOPREVISAO";



            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetNC_AcoesByPLNAC_Id_
       (
           decimal pPLNAC_ID,
           decimal pACS_SITUACAO,
           ConnectionInfo pInfo
       )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_AcoesQD.qNC_AcoesList;
            lQuery += string.Format(" WHERE ACS_STATUS='A' AND PLNAC_ID = {0} ", pPLNAC_ID);

            if (pACS_SITUACAO != 0)
                lQuery += string.Format("AND ACS_SITUACAO = {0} ", pACS_SITUACAO);


            if (pACS_SITUACAO == 4)
                lQuery += " ORDER BY ACS_ID DESC";
            else
                lQuery += " ORDER BY ACS_DATAINICIOPREVISAO";



            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetRelatorioNC_AcoesByUnidadeId
        (
            decimal pUNIDADE_ID,
            string pOCR_NUMERO,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_AcoesQD.qNC_AcoesRelatorioList;
            if(pUNIDADE_ID != 0)
                lQuery += string.Format(" and ocr.unidade_respresolucao = {0} ", pUNIDADE_ID);

            if(pOCR_NUMERO != "")
                lQuery += string.Format(" and ocr.ocr_numero = '{0}' ", pOCR_NUMERO);

            lQuery += " ORDER BY OCR_ID";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetNC_AcoesAnexoByID
        (
            decimal pACS_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_AcoesQD.qNC_AcoesAnexo;
            lQuery += string.Format(" WHERE ACS_STATUS='A' AND ACS_ID = {0} ", pACS_ID);

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetNC_AcoesByPLNAC_IdConcluido
        (
            decimal pPLNAC_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_AcoesQD.qNC_AcoesList;//Ação diferente de concluído e cancelado
            lQuery += string.Format(" WHERE ACS_STATUS='A' AND PLNAC_ID = {0} AND ACS_SITUACAO not in (3,6,8) ", pPLNAC_ID);

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetNC_AcoesByUnidadeOcorrencia
        (
            decimal pUNIDADE_ID,
            decimal pOCR_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_AcoesQD.qNC_AcoesPlanoAcaoCount;
            lQuery += string.Format(" AND PLNAC.OCR_ID = {0} AND ACS.UNIDADES_QUEM = {1} ", pOCR_ID, pUNIDADE_ID);

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }



        #endregion
    }
}
