using System;                                                                         

using System.Collections.Generic;                                                     
using System.Data;                                                                    
using System.Xml;                                                                     

using RPA.DataBase;                                                         
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

namespace HMP.DataObjects.SisRNCWeb
{
    [Serializable]
    public class NC_SintomasAcaoDo
    {

        #region Private Methods

        private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateConversion(pValues, pResult);
        }


        private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateRequired(NC_SintomasAcaoQD._SNTAC_ID, pValues, pResult);
        }

        #endregion

        #region Public Methods
        public static OperationResult Insert
        (
           DataFieldCollection pValues,
            DataFieldCollection pValuesOcorrencia,
           ConnectionInfo pInfo
        )
        {
            Transaction lTransaction;

            lTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (lTransaction != null);

            InsertCommand lInsert;

            OperationResult lReturn = new OperationResult(NC_SintomasAcaoQD.TableName, NC_SintomasAcaoQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {
                    lInsert = new InsertCommand(NC_SintomasAcaoQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }
                    decimal lSequence;
                    lSequence = DataBaseSequenceControl.GetNext(pInfo, "SNTAC_ID");
                    lInsert.Fields.Add(NC_SintomasAcaoQD._SNTAC_ID.Name, lSequence, (ItemType)NC_SintomasAcaoQD._SNTAC_ID.DBType);

                    lInsert.Execute(lTransaction);

                    if (!lReturn.HasError)
                    {

                        //Atualiza Situacao Ocorrencia
                        lReturn = NC_OcorrenciaDo.UpdateSituacao(pValuesOcorrencia, lTransaction, pInfo);

                        if (lReturn.HasError)
                        {
                            lTransaction.Rollback();
                            return lReturn;
                        }

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
            DataFieldCollection pValuesOcorrencia,
            ConnectionInfo pInfo
        )
        {

            Transaction pTransaction;

            pTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (pTransaction != null);

            UpdateCommand lUpdate;

            OperationResult lReturn = new OperationResult(NC_SintomasAcaoQD.TableName, NC_SintomasAcaoQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lUpdate = new UpdateCommand(NC_SintomasAcaoQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de update");
                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_SintomasAcaoQD._SNTAC_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", NC_SintomasAcaoQD._SNTAC_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(NC_SintomasAcaoQD._SNTAC_ID.Name, pValues[NC_SintomasAcaoQD._SNTAC_ID].DBToDecimal());

                    lReturn.Trace("Executando o Update");

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

        public static DataTable GetNC_SintomasAcaoByOCR_ID
        (
            decimal pOCR_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_SintomasAcaoQD.qNC_SintomasAcaoList;
            lQuery += string.Format(" WHERE SNTAC_STATUS='A' AND OCR_ID = {0} ORDER BY SNTAC_ID DESC", pOCR_ID);

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        #endregion
    }
}
