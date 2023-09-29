using System;                                                                         

using System.Collections.Generic;                                                     
using System.Data;                                                                    
using System.Xml;                                                                     

using RPA.DataBase;                                                         
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

namespace HMP.DataObjects.SisRNCWeb
{
    [Serializable]
    public class NC_DiagramaCausaEfeitoDo
    {

        #region Private Methods

        private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateConversion(pValues, pResult);
        }


        private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult)
        {
            //GenericDataObject.ValidateRequired(NC_DiagramaCausaEfeitoQD._DGRCE_ID, pValues, pResult);
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

            OperationResult lReturn = new OperationResult(NC_DiagramaCausaEfeitoQD.TableName, NC_DiagramaCausaEfeitoQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {

                    lInsert = new InsertCommand(NC_DiagramaCausaEfeitoQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }
                    decimal lSequence;
                    lSequence = DataBaseSequenceControl.GetNext(pInfo, "DGRCE_ID");
                    lInsert.Fields.Add(NC_DiagramaCausaEfeitoQD._DGRCE_ID.Name, lSequence, (ItemType)NC_DiagramaCausaEfeitoQD._DGRCE_ID.DBType);

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

        public static OperationResult Insert
        (
           List<DataFieldCollection> pListDiagrama,
           DataFieldCollection pValuesDelete,
           ConnectionInfo pInfo
        )
        {
            Transaction lTransaction;

            lTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            OperationResult lReturn = new OperationResult(NC_DiagramaCausaEfeitoQD.TableName, NC_DiagramaCausaEfeitoQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {
                    //Excluir todos existentes
                    lReturn = DeleteTodos(pValuesDelete, pInfo);
                    if (lReturn.HasError)
                    {
                        lTransaction.Rollback();
                        return lReturn;
                    }


                    //Incluir Diagrama
                    foreach (DataFieldCollection lField in pListDiagrama)
                    {
                        lReturn = NC_DiagramaCausaEfeitoDo.Insert(lField, lTransaction, pInfo);

                        if (lReturn.HasError)
                        {
                            lTransaction.Rollback();
                            return lReturn;
                        }
                    }


                    if (!lReturn.HasError)
                    {
                        lTransaction.Commit();
                    }
                    else
                    {
                        lTransaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    lReturn.OperationException = new SerializableException(ex);
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

            OperationResult lReturn = new OperationResult(NC_DiagramaCausaEfeitoQD.TableName, NC_DiagramaCausaEfeitoQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lUpdate = new UpdateCommand(NC_DiagramaCausaEfeitoQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de update");
                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_DiagramaCausaEfeitoQD._DGRCE_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", NC_DiagramaCausaEfeitoQD._DGRCE_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(NC_DiagramaCausaEfeitoQD._DGRCE_ID.Name, pValues[NC_DiagramaCausaEfeitoQD._DGRCE_ID].DBToDecimal());

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


        public static OperationResult DeleteTodos
        (
            DataFieldCollection pValues,
            ConnectionInfo pInfo
        )
        {

            Transaction pTransaction;

            pTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (pTransaction != null);

            UpdateCommand lUpdate;

            OperationResult lReturn = new OperationResult(NC_DiagramaCausaEfeitoQD.TableName, NC_DiagramaCausaEfeitoQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {

                    lUpdate = new UpdateCommand(NC_DiagramaCausaEfeitoQD.TableName);
                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_DiagramaCausaEfeitoQD._ANCE_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", NC_DiagramaCausaEfeitoQD._ANCE_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(NC_DiagramaCausaEfeitoQD._ANCE_ID.Name, pValues[NC_DiagramaCausaEfeitoQD._ANCE_ID].DBToDecimal());

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


        public static DataTable GetNC_DiagramaCausaEfeitoByANCE_ID
        (
            decimal pANCE_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_DiagramaCausaEfeitoQD.qNC_DiagramaCausaEfeitoList;
            lQuery += string.Format(" WHERE DGRCE_STATUS='A' AND ANCE_ID = {0}", pANCE_ID);

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        #endregion
    }
}
