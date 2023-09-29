using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{                                                                                     
	public static class NC_MotivoOcorrenciaQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "NC_MotivoOcorrencia";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gMTV_ID = new DataField("MTV_ID", 0);

		public static DataField _MTV_ID
		{                                                                          
			get { return gMTV_ID; }                                           
		}

		private static DataField  gMTV_DESCRICAO = new DataField("MTV_DESCRICAO", 1);

		public static DataField _MTV_DESCRICAO
		{                                                                          
			get { return gMTV_DESCRICAO; }                                           
		}

		private static DataField  gMTV_REGDATE = new DataField("MTV_REGDATE", 2);

		public static DataField _MTV_REGDATE
		{                                                                          
			get { return gMTV_REGDATE; }                                           
		}

		private static DataField  gMTV_REGUSER = new DataField("MTV_REGUSER", 0);

		public static DataField _MTV_REGUSER
		{                                                                          
			get { return gMTV_REGUSER; }                                           
		}

		private static DataField  gMTV_STATUS = new DataField("MTV_STATUS", 1);

		public static DataField _MTV_STATUS
		{                                                                          
			get { return gMTV_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from NC_MotivoOcorrencia  WHERE MTV_ID = {0}
		/// </summary>                                                             
		public static string qLoadNC_MotivoOcorrencia
		{                                                                          
			get { return " select * from NC_MotivoOcorrencia  WHERE MTV_ID = {0} "; }
		}                                                                          

		public static string qNC_MotivoOcorrenciaList
		{                                                                          
			get { return @" 
			                select * 
			                    from NC_MotivoOcorrencia";
		        }                                                                    
		}                                                                          

		public static string qNC_MotivoOcorrenciaCount
		{                                                                          
			get { 
                            return @" select count(*) from NC_MotivoOcorrencia";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
