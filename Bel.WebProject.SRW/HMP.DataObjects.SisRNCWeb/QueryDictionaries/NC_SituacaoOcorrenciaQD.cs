using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{                                                                                     
	public static class NC_SituacaoOcorrenciaQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "NC_SituacaoOcorrencia";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gSTCOCR_ID = new DataField("STCOCR_ID", 0);

		public static DataField _STCOCR_ID
		{                                                                          
			get { return gSTCOCR_ID; }                                           
		}

		private static DataField  gSTCOCR_DESCRICAO = new DataField("STCOCR_DESCRICAO", 1);

		public static DataField _STCOCR_DESCRICAO
		{                                                                          
			get { return gSTCOCR_DESCRICAO; }                                           
		}

		private static DataField  gSTCOCR_REGDATE = new DataField("STCOCR_REGDATE", 2);

		public static DataField _STCOCR_REGDATE
		{                                                                          
			get { return gSTCOCR_REGDATE; }                                           
		}

		private static DataField  gSTCOCR_REGUSER = new DataField("STCOCR_REGUSER", 0);

		public static DataField _STCOCR_REGUSER
		{                                                                          
			get { return gSTCOCR_REGUSER; }                                           
		}

		private static DataField  gSTCOCR_STATUS = new DataField("STCOCR_STATUS", 1);

		public static DataField _STCOCR_STATUS
		{                                                                          
			get { return gSTCOCR_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from NC_SituacaoOcorrencia 
		/// </summary>                                                             
		public static string qLoadNC_SituacaoOcorrencia
		{                                                                          
			get { return " select * from NC_SituacaoOcorrencia  "; }
		}                                                                          

		public static string qNC_SituacaoOcorrenciaList
		{                                                                          
			get { return @" 
			                select * 
			                    from NC_SituacaoOcorrencia";
		        }                                                                    
		}                                                                          

		public static string qNC_SituacaoOcorrenciaCount
		{                                                                          
			get { 
                            return @" select count(*) from NC_SituacaoOcorrencia";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
