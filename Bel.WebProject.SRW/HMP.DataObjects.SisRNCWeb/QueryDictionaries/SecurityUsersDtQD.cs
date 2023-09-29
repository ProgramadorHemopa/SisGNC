using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{                                                                                     
	public static class SecurityUsersDtQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "SecurityUsersDt";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gSUD_PERMISSION = new DataField("SUD_PERMISSION", 1);

		public static DataField _SUD_PERMISSION
		{                                                                          
			get { return gSUD_PERMISSION; }                                           
		}

		private static DataField  gSUD_REGDATE = new DataField("SUD_REGDATE", 2);

		public static DataField _SUD_REGDATE
		{                                                                          
			get { return gSUD_REGDATE; }                                           
		}

		private static DataField  gSUD_REGUSER = new DataField("SUD_REGUSER", 1);

		public static DataField _SUD_REGUSER
		{                                                                          
			get { return gSUD_REGUSER; }                                           
		}

		private static DataField  gSUD_STATUS = new DataField("SUD_STATUS", 1);

		public static DataField _SUD_STATUS
		{                                                                          
			get { return gSUD_STATUS; }                                           
		}

		private static DataField  gSU_ID = new DataField("SU_ID", 0);

		public static DataField _SU_ID
		{                                                                          
			get { return gSU_ID; }                                           
		}

		private static DataField  gSO_OBJECTID = new DataField("SO_OBJECTID", 0);

		public static DataField _SO_OBJECTID
		{                                                                          
			get { return gSO_OBJECTID; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from SecurityUsersDt  WHERE SU_ID = {0}
		/// </summary>                                                             
		public static string qLoadSecurityUsersDt
		{                                                                          
			get { return " select * from SecurityUsersDt  WHERE SU_ID = {0} "; }
		}                                                                          

		public static string qSecurityUsersDtList
		{                                                                          
			get { return @" 
			                select * 
			                    from SecurityUsersDt SUD, SECURITYOBJECTS SO, PESSOAFUNCAO PESF, PESSOA PES
                            WHERE SUD.SO_OBJECTID = SO.SO_OBJECTID
                            AND SUD.SUD_STATUS = 'A' AND SO.SO_STATUS = 'A'
                            AND SUD.SU_ID = PESF.PESF_ID
                            AND PESF.PES_ID = PES.PES_ID
                          ";
		        }                                                                    
		}                                                                          

		public static string qSecurityUsersDtCount
		{                                                                          
			get { 
                            return @" select count(*) from SecurityUsersDt";
	 	        }                                                                    
		}                                                                          

		public static string qPessoaFuncao
		{                                                                          
			get { return @" 
			                select  PESF_ID,PES_NOME 
			                    from PessoaFuncao";
		        }                                                                    
		}                                                                          

		public static string qSecurityObjects
		{                                                                          
			get { return @" 
			                select  SO_OBJECTID,SO_DESC 
			                    from SecurityObjects";
		        }                                                                    
		}

        public static string qSecurityObjectsUserPermission
        {
            get
            {
                return @" 
                            SELECT SO.*
                            , (SELECT SU.SUD_PERMISSION FROM SECURITYUSERSDT SU 
                                WHERE SU.SO_OBJECTID = SO.SO_OBJECTID AND SU.SUD_STATUS='A' AND SU.SU_ID = {0}) SUD_PERMISSION
                            FROM SECURITYOBJECTS SO
                            WHERE SO.SO_TYPE = 3 AND SO.SO_STATUS = 'A'";
            }
        }                                                                

		#endregion
    }
}
