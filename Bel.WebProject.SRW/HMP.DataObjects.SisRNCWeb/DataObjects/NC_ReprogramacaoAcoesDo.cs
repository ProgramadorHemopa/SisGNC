using System;                                                                         

using System.Collections.Generic;                                                     
using System.Data;                                                                    
using System.Xml;                                                                     

using RPA.DataBase;                                                         
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

namespace HMP.DataObjects.SisRNCWeb
{
    [Serializable]
    public class NC_ReprogramacaoAcoesDo
    {

        #region Private Methods

        private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateConversion(pValues, pResult);
        }


        private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult)
        {
            //GenericDataObject.ValidateRequired(NC_ReprogramacaoAcoesQD._RPGAC_ID, pValues, pResult);
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

            OperationResult lReturn = new OperationResult(NC_ReprogramacaoAcoesQD.TableName, NC_ReprogramacaoAcoesQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {

                    lInsert = new InsertCommand(NC_ReprogramacaoAcoesQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }
                    decimal lSequence;
                    lSequence = DataBaseSequenceControl.GetNext(pInfo, "RPGAC_ID");
                    lInsert.Fields.Add(NC_ReprogramacaoAcoesQD._RPGAC_ID.Name, lSequence, (ItemType)NC_ReprogramacaoAcoesQD._RPGAC_ID.DBType);

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

            OperationResult lReturn = new OperationResult(NC_ReprogramacaoAcoesQD.TableName, NC_ReprogramacaoAcoesQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    lUpdate = new UpdateCommand(NC_ReprogramacaoAcoesQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_ReprogramacaoAcoesQD._RPGAC_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", NC_ReprogramacaoAcoesQD._RPGAC_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(NC_ReprogramacaoAcoesQD._RPGAC_ID.Name, pValues[NC_ReprogramacaoAcoesQD._RPGAC_ID].DBToDecimal());

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


        public static OperationResult Update
        (
            DataFieldCollection pValues,
            DataFieldCollection pValuesAcao,
           ConnectionInfo pInfo
        )
        {
            Transaction lTransaction;
            
            lTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (lTransaction != null);

            UpdateCommand lUpdate;

            OperationResult lReturn = new OperationResult(NC_ReprogramacaoAcoesQD.TableName, NC_ReprogramacaoAcoesQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    lUpdate = new UpdateCommand(NC_ReprogramacaoAcoesQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_ReprogramacaoAcoesQD._RPGAC_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", NC_ReprogramacaoAcoesQD._RPGAC_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(NC_ReprogramacaoAcoesQD._RPGAC_ID.Name, pValues[NC_ReprogramacaoAcoesQD._RPGAC_ID].DBToDecimal());

                    lUpdate.Execute(lTransaction);

                    if (!lReturn.HasError)
                    {

                        lReturn = NC_AcoesDo.Update(pValuesAcao, lTransaction, pInfo);

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


        public static DataTable GetAllNC_ReprogramacaoAcoes
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_ReprogramacaoAcoesQD.qNC_ReprogramacaoAcoesList;
            lQuery += " WHERE RPGAC_STATUS='A'";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetNC_ReprogramacaoAcoesByACS_Id
        (
            decimal pACS_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_ReprogramacaoAcoesQD.qNC_ReprogramacaoAcoesList;
            lQuery += string.Format(" WHERE RPGAC_STATUS='A' AND ACS_ID = {0} ORDER BY RPGAC_ID DESC", pACS_ID);

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        //Verifica se a Reprograma Ação está atrasada 2x e não possui verificacao de eficacia
        public static DataTable GetNC_ReprogramacaoAcoesByACS_Id_Eficacia
      (
          decimal pACS_ID,
          ConnectionInfo pInfo
      )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_ReprogramacaoAcoesQD.qNC_ReprogramacaoAcoesList;
            lQuery += string.Format(@" WHERE RPGAC_STATUS='A' AND NOT exists(SELECT 0 FROM NC_VERIFICAREFICACIA VRFEFC WHERE VRFEFC.vrfefc_status='A' 
                                       AND VRFEFC.plnac_id = (SELECT plnac_id FROM nc_acoes ACS WHERE ACS.ACS_STATUS = 'A' AND ACS.ACS_ID = RPGAC.acs_id)) ");

            lQuery += string.Format(" AND ACS_ID = {0}", pACS_ID);



            lQuery += string.Format(" ORDER BY RPGAC_ID DESC");
            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        #endregion
    }
}
