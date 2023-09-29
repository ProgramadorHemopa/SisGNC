using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{                                                                                     
	public static class NC_TipoAnaliseQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "NC_TipoAnalise";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gTPANL_ID = new DataField("TPANL_ID", 0);

		public static DataField _TPANL_ID
		{                                                                          
			get { return gTPANL_ID; }                                           
		}

		private static DataField  gTPANL_DESCRICAO = new DataField("TPANL_DESCRICAO", 1);

		public static DataField _TPANL_DESCRICAO
		{                                                                          
			get { return gTPANL_DESCRICAO; }                                           
		}

		private static DataField  gTPANL_REGDATE = new DataField("TPANL_REGDATE", 2);

		public static DataField _TPANL_REGDATE
		{                                                                          
			get { return gTPANL_REGDATE; }                                           
		}

		private static DataField  gTPANL_REGUSER = new DataField("TPANL_REGUSER", 0);

		public static DataField _TPANL_REGUSER
		{                                                                          
			get { return gTPANL_REGUSER; }                                           
		}

		private static DataField  gTPANL_STATUS = new DataField("TPANL_STATUS", 1);

		public static DataField _TPANL_STATUS
		{                                                                          
			get { return gTPANL_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from NC_TipoAnalise  WHERE TPANL_ID = {0}
		/// </summary>                                                             
		public static string qLoadNC_TipoAnalise
		{                                                                          
			get { return " select * from NC_TipoAnalise  WHERE TPANL_ID = {0} "; }
		}                                                                          

		public static string qNC_TipoAnaliseList
		{                                                                          
			get { return @" 
			                select * 
			                    from NC_TipoAnalise";
		        }                                                                    
		}                                                                          

		public static string qNC_TipoAnaliseCount
		{                                                                          
			get { 
                            return @" select count(*) from NC_TipoAnalise";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
