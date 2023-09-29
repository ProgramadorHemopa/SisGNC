using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{
    public static class NC_TipoOcorrenciaQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "NC_TipoOcorrencia";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gTPOCR_ID = new DataField("TPOCR_ID", 0);

		public static DataField _TPOCR_ID
		{                                                                          
			get { return gTPOCR_ID; }                                           
		}

		private static DataField  gTPOCR_DESCRICAO = new DataField("TPOCR_DESCRICAO", 1);

		public static DataField _TPOCR_DESCRICAO
		{                                                                          
			get { return gTPOCR_DESCRICAO; }                                           
		}

		private static DataField  gTPOCR_REGDATE = new DataField("TPOCR_REGDATE", 2);

		public static DataField _TPOCR_REGDATE
		{                                                                          
			get { return gTPOCR_REGDATE; }                                           
		}

		private static DataField  gTPOCR_REGUSER = new DataField("TPOCR_REGUSER", 0);

		public static DataField _TPOCR_REGUSER
		{                                                                          
			get { return gTPOCR_REGUSER; }                                           
		}

		private static DataField  gTPOCR_STATUS = new DataField("TPOCR_STATUS", 1);

		public static DataField _TPOCR_STATUS
		{                                                                          
			get { return gTPOCR_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
        /// select * from NC_TipoOcorrencia  WHERE TPOCR_ID = {0}
		/// </summary>                                                             
        public static string qLoadNC_TipoOcorrencia
		{
            get { return " select * from NC_TipoOcorrencia  WHERE TPOCR_ID = {0} "; }
		}

        public static string qNC_TipoOcorrenciaList
		{                                                                          
			get { return @" 
			                select * 
			                    from NC_TipoOcorrencia";
		        }                                                                    
		}

        public static string qNC_TipoOcorrenciaCount
		{                                                                          
			get {
                return @" select count(*) from NC_TipoOcorrencia";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
