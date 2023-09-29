using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{                                                                                     
	public static class NC_AnaliseCausaEfeitoQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "NC_AnaliseCausaEfeito";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gANCE_ID = new DataField("ANCE_ID", 0);

		public static DataField _ANCE_ID
		{                                                                          
			get { return gANCE_ID; }                                           
		}

		private static DataField  gANCE_DATA = new DataField("ANCE_DATA", 2);

		public static DataField _ANCE_DATA
		{                                                                          
			get { return gANCE_DATA; }                                           
		}

		private static DataField  gANCE_DIAGRAMA = new DataField("ANCE_DIAGRAMA", 0);

		public static DataField _ANCE_DIAGRAMA
		{                                                                          
			get { return gANCE_DIAGRAMA; }                                           
		}

		private static DataField  gANCE_OBSERVACAO = new DataField("ANCE_OBSERVACAO", 1);

		public static DataField _ANCE_OBSERVACAO
		{                                                                          
			get { return gANCE_OBSERVACAO; }                                           
		}

        private static DataField gANCE_ARQUIVO = new DataField("ANCE_ARQUIVO", 3);

        public static DataField _ANCE_ARQUIVO
        {
            get { return gANCE_ARQUIVO; }
        }

        private static DataField gANCE_ARQUIVODESCRICAO = new DataField("ANCE_ARQUIVODESCRICAO", 1);

        public static DataField _ANCE_ARQUIVODESCRICAO
        {
            get { return gANCE_ARQUIVODESCRICAO; }
        }


        private static DataField  gOCR_ID = new DataField("OCR_ID", 0);

		public static DataField _OCR_ID
		{                                                                          
			get { return gOCR_ID; }                                           
		}

		private static DataField  gANCE_REGDATE = new DataField("ANCE_REGDATE", 2);

		public static DataField _ANCE_REGDATE
		{                                                                          
			get { return gANCE_REGDATE; }                                           
		}

		private static DataField  gANCE_REGUSER = new DataField("ANCE_REGUSER", 0);

		public static DataField _ANCE_REGUSER
		{                                                                          
			get { return gANCE_REGUSER; }                                           
		}

		private static DataField  gANCE_STATUS = new DataField("ANCE_STATUS", 1);

		public static DataField _ANCE_STATUS
		{                                                                          
			get { return gANCE_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from NC_AnaliseCausaEfeito  WHERE ANCE_ID = {0}
		/// </summary>                                                             
		public static string qLoadNC_AnaliseCausaEfeito
		{                                                                          
			get { return " select * from NC_AnaliseCausaEfeito  WHERE ANCE_ID = {0} "; }
		}                                                                          

		public static string qNC_AnaliseCausaEfeitoList
		{                                                                          
			get { return @" 
			                select * 
			                    from NC_AnaliseCausaEfeito";
		        }                                                                    
		}                                                                          

		public static string qNC_AnaliseCausaEfeitoCount
		{                                                                          
			get { 
                            return @" select count(*) from NC_AnaliseCausaEfeito";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
