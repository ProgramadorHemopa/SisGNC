using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{                                                                                     
	public static class NC_SintomasAcaoQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "NC_SintomasAcao";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gSNTAC_ID = new DataField("SNTAC_ID", 0);

		public static DataField _SNTAC_ID
		{                                                                          
			get { return gSNTAC_ID; }                                           
		}

		private static DataField  gSNTAC_DESCRICAO = new DataField("SNTAC_DESCRICAO", 1);

		public static DataField _SNTAC_DESCRICAO
		{                                                                          
			get { return gSNTAC_DESCRICAO; }                                           
		}

		private static DataField  gSNTAC_DATA = new DataField("SNTAC_DATA", 2);

		public static DataField _SNTAC_DATA
		{                                                                          
			get { return gSNTAC_DATA; }                                           
		}

		private static DataField  gOCR_ID = new DataField("OCR_ID", 0);

		public static DataField _OCR_ID
		{                                                                          
			get { return gOCR_ID; }                                           
		}

		private static DataField  gSNTAC_REGDATE = new DataField("SNTAC_REGDATE", 2);

		public static DataField _SNTAC_REGDATE
		{                                                                          
			get { return gSNTAC_REGDATE; }                                           
		}

		private static DataField  gSNTAC_REGUSER = new DataField("SNTAC_REGUSER", 0);

		public static DataField _SNTAC_REGUSER
		{                                                                          
			get { return gSNTAC_REGUSER; }                                           
		}

		private static DataField  gSNTAC_STATUS = new DataField("SNTAC_STATUS", 1);

		public static DataField _SNTAC_STATUS
		{                                                                          
			get { return gSNTAC_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from NC_SintomasAcao  WHERE SNTAC_ID = {0}
		/// </summary>                                                             
		public static string qLoadNC_SintomasAcao
		{                                                                          
			get { return " select * from NC_SintomasAcao  WHERE SNTAC_ID = {0} "; }
		}                                                                          

		public static string qNC_SintomasAcaoList
		{                                                                          
			get { return @" 
			                select * 
			                    from NC_SintomasAcao";
		        }                                                                    
		}                                                                          

		public static string qNC_SintomasAcaoCount
		{                                                                          
			get { 
                            return @" select count(*) from NC_SintomasAcao";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
