using System;                                                                         

using System.Collections.Generic;                                                     
using System.Data;                                                                    
using System.Xml;

using RPA.DataBase;                                                         
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

namespace HMP.DataObjects.SisRNCWeb
{                                                                                     
     [Serializable] 
     public class SecurityObjectsDo
	    {                                                                                    

      #region Private Methods

      private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult) 
      {                                                                                        
          GenericDataObject.ValidateConversion(pValues, pResult);                              
      }                                                                                        


      private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult) 
      {                                                                                        
        GenericDataObject.ValidateRequired(SecurityObjectsQD._SO_OBJECTID, pValues, pResult);
        GenericDataObject.ValidateRequired(SecurityObjectsQD._SO_REGDATE, pValues, pResult);
        GenericDataObject.ValidateRequired(SecurityObjectsQD._SO_REGUSER, pValues, pResult);
        GenericDataObject.ValidateRequired(SecurityObjectsQD._SO_STATUS , pValues, pResult);
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
                                                                                               
     OperationResult lReturn = new OperationResult(SecurityObjectsQD.TableName, SecurityObjectsQD.TableName);      
                                                                                               
     if (!lReturn.HasError)                                                                    
     {                                                                                         
         try                                                                                   
         {                                                                                     
             if (lLocalTransaction)                                                            
             {                                                                                 
                 lReturn.Trace("Transação local, instanciando banco...");               
             }                                                                                 
                                                                                               
             lInsert = new InsertCommand(SecurityObjectsQD.TableName);                                   
                                                                                               
             lReturn.Trace("Adicionando campos ao objeto de insert");                   
                                                                                               
             foreach (DataField lField in pValues.Keys)                                        
             {                                                                                 
                 lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);    
             }                                                                                 
             decimal lSequence;
             lSequence = DataBaseSequenceControl.GetNext(pInfo, "SO_OBJECTID");
             lInsert.Fields.Add(SecurityObjectsQD._SO_OBJECTID.Name, lSequence, (ItemType)SecurityObjectsQD._SO_OBJECTID.DBType);
                                                                                               
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
                                                                                                                                           
         OperationResult lReturn = new OperationResult(SecurityObjectsQD.TableName, SecurityObjectsQD.TableName);                                              
                                                                                                                                           
         ValidateUpdate(pValues, lReturn);                                                                                            
                                                                                                                                           
         if (lReturn.IsValid)                                                                                                              
         {                                                                                                                                 
             try                                                                                                                           
             {                                                                                                                             
                 if (lLocalTransaction)                                                                                                    
                 {                                                                                                                         
                     lReturn.Trace("Transação local, instanciando banco...");                                                       
                 }                                                                                                                         
                                                                                                                                           
                 lUpdate = new UpdateCommand(SecurityObjectsQD.TableName);                                                                           
                                                                                                                                           
                 lReturn.Trace("Adicionando campos ao objeto de update");                                                           
                 foreach (DataField lField in pValues.Keys)
                 {
                    if (  (lField.Name != SecurityObjectsQD._SO_OBJECTID.Name) )
                       lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                 }
                                                                                                                                           
                 string lSql  = "";                                                                                                              
                 lSql = String.Format("WHERE {0} = <<{0}", SecurityObjectsQD._SO_OBJECTID.Name);
                 lUpdate.Condition = lSql;
                 lUpdate.Conditions.Add(SecurityObjectsQD._SO_OBJECTID.Name, pValues[SecurityObjectsQD._SO_OBJECTID].DBToDecimal());
                                                                                                                                           
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

         public static DataTable GetAllSecurityObjects
         (                                                           
             ConnectionInfo pInfo                                    
         )                                                           
         {                                                           
             string lQuery = "";
             DataTable lTable = new DataTable();
                                                                     
             lQuery = SecurityObjectsQD.qSecurityObjectsList;
             lQuery += " WHERE SO_STATUS='A' ORDER BY SO_OBJECTID";
                                                                     
             MySqlDo lMySqlDo = new MySqlDo();         
             lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);  
                                                                     
             return lTable;                                                
         }                                                           

             #endregion
     }
}
