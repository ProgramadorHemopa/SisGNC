using System;                                                                         

using System.Collections.Generic;                                                     
using System.Data;                                                                    
using System.Xml;                                                                     

using RPA.DataBase;                                                         
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

namespace HMP.DataObjects.SisRNCWeb
{                                                                                     
     [Serializable] 
     public class NC_TipoAnaliseDo
	    {                                                                                    

      #region Private Methods

      private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult) 
      {                                                                                        
          GenericDataObject.ValidateConversion(pValues, pResult);                              
      }                                                                                        


      private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult) 
      {                                                                                        
        GenericDataObject.ValidateRequired(NC_TipoAnaliseQD._TPANL_ID, pValues, pResult);
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
                                                                                               
     OperationResult lReturn = new OperationResult(NC_TipoAnaliseQD.TableName, NC_TipoAnaliseQD.TableName);      
                                                                                               
     if (!lReturn.HasError)                                                                    
     {                                                                                         
         try                                                                                   
         {                                                                                     
             if (lLocalTransaction)                                                            
             {                                                                                 
                 lReturn.Trace("Transa��o local, instanciando banco...");               
             }                                                                                 
                                                                                               
             lInsert = new InsertCommand(NC_TipoAnaliseQD.TableName);                                   
                                                                                               
             lReturn.Trace("Adicionando campos ao objeto de insert");                   
                                                                                               
             foreach (DataField lField in pValues.Keys)                                        
             {                                                                                 
                 lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);    
             }                                                                                 
             decimal lSequence;
             lSequence = DataBaseSequenceControl.GetNext(pInfo, "TPANL_ID");
             lInsert.Fields.Add(NC_TipoAnaliseQD._TPANL_ID.Name, lSequence, (ItemType)NC_TipoAnaliseQD._TPANL_ID.DBType);
                                                                                               
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
                                                                                                                                           
         OperationResult lReturn = new OperationResult(NC_TipoAnaliseQD.TableName, NC_TipoAnaliseQD.TableName);                                              
                                                                                                                                           
         ValidateUpdate(pValues, lReturn);                                                                                            
                                                                                                                                           
         if (lReturn.IsValid)                                                                                                              
         {                                                                                                                                 
             try                                                                                                                           
             {                                                                                                                             
                 if (lLocalTransaction)                                                                                                    
                 {                                                                                                                         
                     lReturn.Trace("Transa��o local, instanciando banco...");                                                       
                 }                                                                                                                         
                                                                                                                                           
                 lUpdate = new UpdateCommand(NC_TipoAnaliseQD.TableName);                                                                           
                                                                                                                                           
                 lReturn.Trace("Adicionando campos ao objeto de update");                                                           
                 foreach (DataField lField in pValues.Keys)
                 {
                    if (  (lField.Name != NC_TipoAnaliseQD._TPANL_ID.Name) )
                       lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                 }
                                                                                                                                           
                 string lSql  = "";                                                                                                              
                 lSql = String.Format("WHERE {0} = <<{0}", NC_TipoAnaliseQD._TPANL_ID.Name);
                 lUpdate.Condition = lSql;
                 lUpdate.Conditions.Add(NC_TipoAnaliseQD._TPANL_ID.Name, pValues[NC_TipoAnaliseQD._TPANL_ID].DBToDecimal());
                                                                                                                                           
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

         public static DataTable GetAllNC_TipoAnalise
         (                                                           
             ConnectionInfo pInfo                                    
         )                                                           
         {                                                           
             string lQuery = "";
             DataTable lTable = new DataTable();
                                                                     
             lQuery = NC_TipoAnaliseQD.qNC_TipoAnaliseList;
             lQuery += " WHERE TPANL_STATUS='A'";

             MySqlDo lMySqlDo = new MySqlDo();
             lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

                      
             lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);  
                                                                     
             return lTable;                                                
         }                                                           


             #endregion
     }
}
