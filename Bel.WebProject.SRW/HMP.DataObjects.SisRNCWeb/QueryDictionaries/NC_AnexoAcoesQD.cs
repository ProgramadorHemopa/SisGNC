using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{                                                                                     
	public static class NC_AnexoAcoesQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "NC_AnexoAcoes";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gANXACS_ID = new DataField("ANXACS_ID", 0);

		public static DataField _ANXACS_ID
		{                                                                          
			get { return gANXACS_ID; }                                           
		}

		private static DataField  gACS_ID = new DataField("ACS_ID", 0);

		public static DataField _ACS_ID
		{                                                                          
			get { return gACS_ID; }                                           
		}

		private static DataField  gANXACS_ARQUIVO = new DataField("ANXACS_ARQUIVO", 3);

		public static DataField _ANXACS_ARQUIVO
		{                                                                          
			get { return gANXACS_ARQUIVO; }                                           
		}

		private static DataField  gANXACS_DESCRICAO = new DataField("ANXACS_DESCRICAO", 1);

		public static DataField _ANXACS_DESCRICAO
		{                                                                          
			get { return gANXACS_DESCRICAO; }                                           
		}

		private static DataField  gANXACS_REGDATE = new DataField("ANXACS_REGDATE", 2);

		public static DataField _ANXACS_REGDATE
		{                                                                          
			get { return gANXACS_REGDATE; }                                           
		}

		private static DataField  gANXACS_REGUSER = new DataField("ANXACS_REGUSER", 0);

		public static DataField _ANXACS_REGUSER
		{                                                                          
			get { return gANXACS_REGUSER; }                                           
		}

		private static DataField  gANXACS_STATUS = new DataField("ANXACS_STATUS", 1);

		public static DataField _ANXACS_STATUS
		{                                                                          
			get { return gANXACS_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from NC_AnexoAcoes  WHERE ANXACS_ID = {0}
		/// </summary>                                                             
		public static string qLoadNC_AnexoAcoes
		{                                                                          
			get { return " select * from NC_AnexoAcoes  WHERE ANXACS_ID = {0} "; }
		}                                                                          

		public static string qNC_AnexoAcoesList
		{                                                                          
			get { return @" 
			                select ANXACS_ID, ANXACS_DESCRICAO
			                    from NC_AnexoAcoes";
		        }                                                                    
		}

        public static string qNC_AnexoAcoesArquivo
        {
            get
            {
                return @" 
			                select * 
			                    from NC_AnexoAcoes";
            }
        }                                                              

		public static string qNC_AnexoAcoesCount
		{                                                                          
			get { 
                            return @" select count(*) from NC_AnexoAcoes";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
