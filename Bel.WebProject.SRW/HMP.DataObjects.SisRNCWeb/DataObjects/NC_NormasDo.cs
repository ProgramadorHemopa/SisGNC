using System;                                                                         

using System.Collections.Generic;                                                     
using System.Data;                                                                    
using System.Xml;                                                                     

using RPA.DataBase;                                                         
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

namespace HMP.DataObjects.SisRNCWeb
{
    [Serializable]
    public class NC_NormasDo
    {

        #region Private Methods

        private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateConversion(pValues, pResult);
        }


        private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateRequired(NC_NormasQD._NRM_ID, pValues, pResult);
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

            OperationResult lReturn = new OperationResult(NC_NormasQD.TableName, NC_NormasQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {
                    
                    lInsert = new InsertCommand(NC_NormasQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }
                    decimal lSequence;
                    lSequence = DataBaseSequenceControl.GetNext(pInfo, "NRM_ID");
                    lInsert.Fields.Add(NC_NormasQD._NRM_ID.Name, lSequence, (ItemType)NC_NormasQD._NRM_ID.DBType);

                    lInsert.Execute(lTransaction, false);

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
                                                                                                                                           
         OperationResult lReturn = new OperationResult(NC_NormasQD.TableName, NC_NormasQD.TableName);                                              
                                                                                                                                           
         ValidateUpdate(pValues, lReturn);                                                                                            
                                                                                                                                           
         if (lReturn.IsValid)                                                                                                              
         {                                                                                                                                 
             try                                                                                                                           
             {                                                                                                                             
                                                                                                                                           
                 lUpdate = new UpdateCommand(NC_NormasQD.TableName);                                                                           
                                                                                                                                           
                 foreach (DataField lField in pValues.Keys)
                 {
                     if ((lField.Name != NC_NormasQD._NRM_ID.Name))
                       lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                 }
                                                                                                                                           
                 string lSql  = "";
                 lSql = String.Format("WHERE {0} = <<{0}", NC_NormasQD._NRM_ID.Name);
                 lUpdate.Condition = lSql;
                 lUpdate.Conditions.Add(NC_NormasQD._NRM_ID.Name, pValues[NC_NormasQD._NRM_ID].DBToDecimal());
                                                                                                                                           
                 lUpdate.Execute(pTransaction, false);                                                                                            
                                                                                                                                           
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

        public static DataTable GetAllNC_Normas
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = NC_NormasQD.qNC_NormasList;
            lQuery += " WHERE NRM_STATUS='A' ORDER BY NRM_DESCRICAO";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        #endregion
    }
}
