using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{                                                                                     
	public static class NC_NormasQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "NC_Normas";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gNRM_ID = new DataField("NRM_ID", 0);

		public static DataField _NRM_ID
		{                                                                          
			get { return gNRM_ID; }                                           
		}

		private static DataField  gNRM_DESCRICAO = new DataField("NRM_DESCRICAO", 1);

		public static DataField _NRM_DESCRICAO
		{                                                                          
			get { return gNRM_DESCRICAO; }                                           
		}

		private static DataField  gNRM_REGDATE = new DataField("NRM_REGDATE", 2);

		public static DataField _NRM_REGDATE
		{                                                                          
			get { return gNRM_REGDATE; }                                           
		}

		private static DataField  gNRM_REGUSER = new DataField("NRM_REGUSER", 0);

		public static DataField _NRM_REGUSER
		{                                                                          
			get { return gNRM_REGUSER; }                                           
		}

		private static DataField  gNRM_STATUS = new DataField("NRM_STATUS", 1);

		public static DataField _NRM_STATUS
		{                                                                          
			get { return gNRM_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from NC_Normas 
		/// </summary>                                                             
		public static string qLoadNC_Normas
		{                                                                          
			get { return " select * from NC_Normas  "; }
		}                                                                          

		public static string qNC_NormasList
		{                                                                          
			get { return @" 
			                select * 
			                    from NC_Normas";
		        }                                                                    
		}                                                                          

		public static string qNC_NormasCount
		{                                                                          
			get { 
                            return @" select count(*) from NC_Normas";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
