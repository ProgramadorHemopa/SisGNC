using System;                                                                         

using System.Collections.Generic;                                                     
using System.Data;                                                                    
using System.Xml;                                                                     

using RPA.DataBase;                                                         
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

namespace HMP.DataObjects.SisRNCWeb
{
    [Serializable]
    public class NC_AnaliseCausaEfeitoDo
    {

        #region Private Methods

        private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateConversion(pValues, pResult);
        }


        private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateRequired(NC_AnaliseCausaEfeitoQD._ANCE_ID, pValues, pResult);
        }

        #endregion

        #region Public Methods
        public static OperationResult Insert
        (
           DataFieldCollection pValues,
           List<DataFieldCollection> pListDiagrama,
           DataFieldCollection pValuesOcorrencia,
           ConnectionInfo pInfo
        )
        {
            Transaction lTransaction;

            lTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (lTransaction != null);

            InsertCommand lInsert;

            OperationResult lReturn = new OperationResult(NC_AnaliseCausaEfeitoQD.TableName, NC_AnaliseCausaEfeitoQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {

                    lInsert = new InsertCommand(NC_AnaliseCausaEfeitoQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }
                    decimal lSequence;
                    lSequence = DataBaseSequenceControl.GetNext(pInfo, "ANCE_ID");
                    lInsert.Fields.Add(NC_AnaliseCausaEfeitoQD._ANCE_ID.Name, lSequence, (ItemType)NC_AnaliseCausaEfeitoQD._ANCE_ID.DBType);

                    lInsert.Execute(lTransaction);

                    if (!lReturn.HasError)
                    {
                        //Incluir Diagrama
                        foreach (DataFieldCollection lField in pListDiagrama)
                        {
                            lField.Add(NC_DiagramaCausaEfeitoQD._ANCE_ID, lSequence);
                            lReturn = NC_DiagramaCausaEfeitoDo.Insert(lField, lTransaction, pInfo);

                            if (lReturn.HasError)
                            {
                                lTransaction.Rollback();
                                return lReturn;
                            }
                        }


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
            ConnectionInfo pInfo
        )
        {

            Transaction pTransaction;

            pTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (pTransaction != null);

            UpdateCommand lUpdate;

            OperationResult lReturn = new OperationResult(NC_AnaliseCausaEfeitoQD.TableName, NC_AnaliseCausaEfeitoQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lUpdate = new UpdateCommand(NC_AnaliseCausaEfeitoQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de update");
                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_AnaliseCausaEfeitoQD._ANCE_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", NC_AnaliseCausaEfeitoQD._ANCE_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(NC_AnaliseCausaEfeitoQD._ANCE_ID.Name, pValues[NC_AnaliseCausaEfeitoQD._ANCE_ID].DBToDecimal());

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

        public static DataTable GetNC_AnaliseCausaEfeitoByOCR_ID
        (
            decimal pOCR_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_AnaliseCausaEfeitoQD.qNC_AnaliseCausaEfeitoList;
            lQuery += string.Format(" WHERE ANCE_STATUS='A' AND OCR_ID = {0}", pOCR_ID);

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetNC_AnaliseCausaEfeitoById
        (
            decimal pANCE_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_AnaliseCausaEfeitoQD.qNC_AnaliseCausaEfeitoList;
            lQuery += string.Format(" WHERE ANCE_STATUS='A' AND ANCE_ID = {0}", pANCE_ID);

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        #endregion
    }
}
