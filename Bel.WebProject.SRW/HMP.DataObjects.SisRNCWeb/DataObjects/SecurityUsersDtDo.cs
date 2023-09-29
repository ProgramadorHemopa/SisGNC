using System;                                                                         

using System.Collections.Generic;                                                     
using System.Data;                                                                    
using System.Xml;

using RPA.DataBase;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

namespace HMP.DataObjects.SisRNCWeb
{
    [Serializable]
    public class SecurityUsersDtDo
    {

        #region Private Methods

        private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateConversion(pValues, pResult);
        }


        private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateRequired(SecurityUsersDtQD._SU_ID, pValues, pResult);
            GenericDataObject.ValidateRequired(SecurityUsersDtQD._SUD_REGDATE, pValues, pResult);
            GenericDataObject.ValidateRequired(SecurityUsersDtQD._SUD_REGUSER, pValues, pResult);
            GenericDataObject.ValidateRequired(SecurityUsersDtQD._SUD_STATUS, pValues, pResult);
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

            OperationResult lReturn = new OperationResult(SecurityUsersDtQD.TableName, SecurityUsersDtQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lInsert = new InsertCommand(SecurityUsersDtQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de insert");

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }
                    //decimal lSequence;
                    //lSequence = DataBaseSequenceControl.GetNext(pInfo, "SU_ID");
                    //lInsert.Fields.Add(SecurityUsersDtQD._SU_ID.Name, lSequence, (ItemType)SecurityUsersDtQD._SU_ID.DBType);

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

            OperationResult lReturn = new OperationResult(SecurityUsersDtQD.TableName, SecurityUsersDtQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lInsert = new InsertCommand(SecurityUsersDtQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de insert");

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }
                    //decimal lSequence;
                    //lSequence = DataBaseSequenceControl.GetNext(pInfo, "SU_ID");
                    //lInsert.Fields.Add(SecurityUsersDtQD._SU_ID.Name, lSequence, (ItemType)SecurityUsersDtQD._SU_ID.DBType);

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

            OperationResult lReturn = new OperationResult(SecurityUsersDtQD.TableName, SecurityUsersDtQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lUpdate = new UpdateCommand(SecurityUsersDtQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de update");
                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != SecurityUsersDtQD._SU_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format(" WHERE {0} = <<{0}", SecurityUsersDtQD._SU_ID.Name);
                    lSql += String.Format(" AND {0} = <<{0}", SecurityUsersDtQD._SO_OBJECTID.Name);
                    lUpdate.Condition = lSql;

                    lUpdate.Conditions.Add(SecurityUsersDtQD._SU_ID.Name, pValues[SecurityUsersDtQD._SU_ID].DBToDecimal());
                    lUpdate.Conditions.Add(SecurityUsersDtQD._SO_OBJECTID.Name, pValues[SecurityUsersDtQD._SO_OBJECTID].DBToDecimal());

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

        public static DataTable GetAllSecurityUsersDt
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = SecurityUsersDtQD.qSecurityUsersDtList;
            lQuery += " ORDER BY PES.PES_NOME ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetSecurityUsersDtByUser_Object
        (
            decimal pSU_ID,
            decimal pSO_OBJECTID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = SecurityUsersDtQD.qSecurityUsersDtList;
            lQuery += string.Format(" AND SUD.SU_ID = {0} AND SUD.SO_OBJECTID = {1}", pSU_ID, pSO_OBJECTID);

            MySqlDo lMySqlDo = new MySqlDo();

            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetAllPessoaFuncao
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = SecurityUsersDtQD.qPessoaFuncao;
            lQuery += " WHERE PESF_STATUS='A'";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }
        public static DataTable GetAllSecurityObjects
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = SecurityUsersDtQD.qSecurityObjects;
            lQuery += " WHERE SO_STATUS='A'";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetSecurityObjectsNivel3
        (
            decimal pPESF_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = string.Format(SecurityUsersDtQD.qSecurityObjectsUserPermission, pPESF_ID);
            lQuery += " ORDER BY SO_OBJECTID";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }
        #endregion
    }
}
