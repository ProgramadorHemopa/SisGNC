using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{                                                                                     
	public static class NC_AnexoOcorrenciaQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "NC_AnexoOcorrencia";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gANXOCR_ID = new DataField("ANXOCR_ID", 0);

		public static DataField _ANXOCR_ID
		{                                                                          
			get { return gANXOCR_ID; }                                           
		}

		private static DataField  gOCR_ID = new DataField("OCR_ID", 0);

		public static DataField _OCR_ID
		{                                                                          
			get { return gOCR_ID; }                                           
		}

		private static DataField  gANXOCR_ARQUIVO = new DataField("ANXOCR_ARQUIVO", 3);

		public static DataField _ANXOCR_ARQUIVO
		{                                                                          
			get { return gANXOCR_ARQUIVO; }                                           
		}

		private static DataField  gANXOCR_DESCRICAO = new DataField("ANXOCR_DESCRICAO", 1);

		public static DataField _ANXOCR_DESCRICAO
		{                                                                          
			get { return gANXOCR_DESCRICAO; }                                           
		}

		private static DataField  gANXOCR_REGDATE = new DataField("ANXOCR_REGDATE", 2);

		public static DataField _ANXOCR_REGDATE
		{                                                                          
			get { return gANXOCR_REGDATE; }                                           
		}

		private static DataField  gANXOCR_REGUSER = new DataField("ANXOCR_REGUSER", 0);

		public static DataField _ANXOCR_REGUSER
		{                                                                          
			get { return gANXOCR_REGUSER; }                                           
		}

		private static DataField  gANXOCR_STATUS = new DataField("ANXOCR_STATUS", 1);

		public static DataField _ANXOCR_STATUS
		{                                                                          
			get { return gANXOCR_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from NC_AnexoOcorrencia  WHERE ANXOCR_ID = {0}
		/// </summary>                                                             
		public static string qLoadNC_AnexoOcorrencia
		{                                                                          
			get { return " select * from NC_AnexoOcorrencia  WHERE ANXOCR_ID = {0} "; }
		}                                                                          

		public static string qNC_AnexoOcorrenciaList
		{                                                                          
			get { return @" 
			                select ANXOCR_ID, ANXOCR_DESCRICAO
			                    from NC_AnexoOcorrencia";
		        }                                                                    
		}

        public static string qNC_AnexoOcorrenciaArquivo
        {
            get
            {
                return @" 
			                select * 
			                    from NC_AnexoOcorrencia";
            }
        }                                                              

		public static string qNC_AnexoOcorrenciaCount
		{                                                                          
			get { 
                            return @" select count(*) from NC_AnexoOcorrencia";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
