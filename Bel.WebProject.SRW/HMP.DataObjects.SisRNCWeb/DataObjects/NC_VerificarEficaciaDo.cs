using System;                                                                         

using System.Collections.Generic;                                                     
using System.Data;                                                                    
using System.Xml;                                                                     

using RPA.DataBase;                                                         
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

namespace HMP.DataObjects.SisRNCWeb
{
    [Serializable]
    public class NC_VerificarEficaciaDo
    {

        #region Private Methods

        private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateConversion(pValues, pResult);
        }


        private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateRequired(NC_VerificarEficaciaQD._VRFEFC_ID, pValues, pResult);
        }

        #endregion

        #region Public Methods
        public static OperationResult Insert
        (
           DataFieldCollection pValues,
           DataFieldCollection pValuesOcorrencia,
            DataFieldCollection pValuesPlanoAcao,
            DataFieldCollection pValuesAcao,
           ConnectionInfo pInfo
        )
        {
            Transaction lTransaction;

            lTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (lTransaction != null);

            InsertCommand lInsert;

            OperationResult lReturn = new OperationResult(NC_VerificarEficaciaQD.TableName, NC_VerificarEficaciaQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {
                    lInsert = new InsertCommand(NC_VerificarEficaciaQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }
                    decimal lSequence;
                    lSequence = DataBaseSequenceControl.GetNext(pInfo, "VRFEFC_ID");
                    lInsert.Fields.Add(NC_VerificarEficaciaQD._VRFEFC_ID.Name, lSequence, (ItemType)NC_VerificarEficaciaQD._VRFEFC_ID.DBType);


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

                        //Atualiza Situacao PlanoDeAção
                        //lReturn = NC_PlanoAcaoDo.UpdateSituacao(pValuesPlanoAcao, lTransaction, pInfo);
                        lReturn = NC_AcoesDo.UpdateSituacaoAcoes_Ineficaz(pValuesAcao,pValuesPlanoAcao,lTransaction, pInfo);

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

            OperationResult lReturn = new OperationResult(NC_VerificarEficaciaQD.TableName, NC_VerificarEficaciaQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lUpdate = new UpdateCommand(NC_VerificarEficaciaQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de update");
                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != NC_VerificarEficaciaQD._VRFEFC_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", NC_VerificarEficaciaQD._VRFEFC_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(NC_VerificarEficaciaQD._VRFEFC_ID.Name, pValues[NC_VerificarEficaciaQD._VRFEFC_ID].DBToDecimal());

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

        public static DataTable GetNC_VerificarEficaciaByOCR_ID
        (
            decimal pOCR_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_VerificarEficaciaQD.qNC_VerificarEficaciaList;
            lQuery += string.Format(" AND PLNAC.OCR_ID = {0}  ORDER BY VRFEFC.VRFEFC_ID ", pOCR_ID);

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        #endregion
    }
}
