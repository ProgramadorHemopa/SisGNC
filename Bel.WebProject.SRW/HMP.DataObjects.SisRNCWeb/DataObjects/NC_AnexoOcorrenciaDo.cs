using System;                                                                         

using System.Collections.Generic;                                                     
using System.Data;                                                                    
using System.Xml;                                                                     

using RPA.DataBase;                                                         
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

namespace HMP.DataObjects.SisRNCWeb
{
    [Serializable]
    public class NC_AnexoOcorrenciaDo
    {

        #region Private Methods

        private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateConversion(pValues, pResult);
        }


        private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateRequired(NC_AnexoOcorrenciaQD._OCR_ID, pValues, pResult);
            GenericDataObject.ValidateRequired(NC_AnexoOcorrenciaQD._ANXOCR_REGDATE, pValues, pResult);
            GenericDataObject.ValidateRequired(NC_AnexoOcorrenciaQD._ANXOCR_REGUSER, pValues, pResult);
            GenericDataObject.ValidateRequired(NC_AnexoOcorrenciaQD._ANXOCR_STATUS, pValues, pResult);
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

            OperationResult lReturn = new OperationResult(NC_AnexoOcorrenciaQD.TableName, NC_AnexoOcorrenciaQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {


                    lInsert = new InsertCommand(NC_AnexoOcorrenciaQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }
                    decimal lSequence;
                    lSequence = DataBaseSequenceControl.GetNext(pInfo, "ANXOCR_ID");
                    lInsert.Fields.Add(NC_AnexoOcorrenciaQD._ANXOCR_ID.Name, lSequence, (ItemType)NC_AnexoOcorrenciaQD._ANXOCR_ID.DBType);

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

            OperationResult lReturn = new OperationResult(NC_AnexoOcorrenciaQD.TableName, NC_AnexoOcorrenciaQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lUpdate = new UpdateCommand(NC_AnexoOcorrenciaQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de update");
                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_AnexoOcorrenciaQD._ANXOCR_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", NC_AnexoOcorrenciaQD._ANXOCR_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(NC_AnexoOcorrenciaQD._ANXOCR_ID.Name, pValues[NC_AnexoOcorrenciaQD._ANXOCR_ID].DBToDecimal());

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

            OperationResult lReturn = new OperationResult(NC_AnexoOcorrenciaQD.TableName, NC_AnexoOcorrenciaQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    lUpdate = new UpdateCommand(NC_AnexoOcorrenciaQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_AnexoOcorrenciaQD._OCR_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", NC_AnexoOcorrenciaQD._OCR_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(NC_AnexoOcorrenciaQD._OCR_ID.Name, pValues[NC_AnexoOcorrenciaQD._OCR_ID].DBToDecimal());

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

        public static DataTable GetNC_AnexoOcorrenciaByOCR_ID
        (
            decimal pOCR_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_AnexoOcorrenciaQD.qNC_AnexoOcorrenciaList;
            lQuery += " WHERE ANXOCR_STATUS='A'";
            lQuery += string.Format(" AND OCR_ID = {0}", pOCR_ID);

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetArquivoAnexoOcorrenciaById
        (
            decimal pANXOCR_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_AnexoOcorrenciaQD.qNC_AnexoOcorrenciaArquivo;
            lQuery += " WHERE ANXOCR_STATUS='A'";
            lQuery += string.Format(" AND ANXOCR_ID = {0}", pANXOCR_ID);

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        #endregion
    }
}
