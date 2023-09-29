using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{                                                                                     
	public static class NC_AnaliseCriticaQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "NC_AnaliseCritica";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gANC_ID = new DataField("ANC_ID", 0);

		public static DataField _ANC_ID
		{                                                                          
			get { return gANC_ID; }                                           
		}

		private static DataField  gANC_SITUACAO = new DataField("ANC_SITUACAO", 0);

		public static DataField _ANC_SITUACAO
		{                                                                          
			get { return gANC_SITUACAO; }                                           
		}

		private static DataField  gANC_DATA = new DataField("ANC_DATA", 2);

		public static DataField _ANC_DATA
		{                                                                          
			get { return gANC_DATA; }                                           
		}

		private static DataField  gANC_SAC = new DataField("ANC_SAC", 0);

		public static DataField _ANC_SAC
		{                                                                          
			get { return gANC_SAC; }                                           
		}

		private static DataField  gMATRICULA_RESPPA = new DataField("MATRICULA_RESPPA", 0);

		public static DataField _MATRICULA_RESPPA
		{                                                                          
			get { return gMATRICULA_RESPPA; }                                           
		}

		private static DataField  gANC_JUSTIFICATIVACANCELAMENTO = new DataField("ANC_JUSTIFICATIVACANCELAMENTO", 1);

		public static DataField _ANC_JUSTIFICATIVACANCELAMENTO
		{                                                                          
			get { return gANC_JUSTIFICATIVACANCELAMENTO; }                                           
		}

		private static DataField  gOCR_ID = new DataField("OCR_ID", 0);

		public static DataField _OCR_ID
		{                                                                          
			get { return gOCR_ID; }                                           
		}

		private static DataField  gTPANL_ID = new DataField("TPANL_ID", 0);

		public static DataField _TPANL_ID
		{                                                                          
			get { return gTPANL_ID; }                                           
		}

		private static DataField  gMATRICULA_NQ = new DataField("MATRICULA_NQ", 0);

		public static DataField _MATRICULA_NQ
		{                                                                          
			get { return gMATRICULA_NQ; }                                           
		}

		private static DataField  gANC_REGDATE = new DataField("ANC_REGDATE", 2);

		public static DataField _ANC_REGDATE
		{                                                                          
			get { return gANC_REGDATE; }                                           
		}

		private static DataField  gANC_REGUSER = new DataField("ANC_REGUSER", 0);

		public static DataField _ANC_REGUSER
		{                                                                          
			get { return gANC_REGUSER; }                                           
		}

		private static DataField  gANC_STATUS = new DataField("ANC_STATUS", 1);

		public static DataField _ANC_STATUS
		{                                                                          
			get { return gANC_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from NC_AnaliseCritica  WHERE ANC_ID = {0}
		/// </summary>                                                             
		public static string qLoadNC_AnaliseCritica
		{                                                                          
			get { return " select * from NC_AnaliseCritica  WHERE ANC_ID = {0} "; }
		}                                                                          

		public static string qNC_AnaliseCriticaList
		{                                                                          
			get { return @" 
			                 select ANC.*, 
                                CASE ANC.ANC_SITUACAO
                                WHEN 0 THEN 'Aprovada' 
                                WHEN 1 THEN  'Cancelada' 
                                ELSE '' END ANC_SITUACAODESCRICAO
                                , TPANL.TPANL_DESCRICAO
                                ,(SELECT FUN_NOME FROM GEAPEDB.AP_FUNCIONARIO FUN WHERE FUN.FUN_STATUS='A' AND FUN.FUN_MATRICULA = ANC.MATRICULA_RESPPA) AS RESPONSAVEL_PA 
			                    FROM NC_AnaliseCritica ANC, NC_TIPOANALISE TPANL
                                WHERE ANC.TPANL_ID = TPANL.TPANL_ID";
		        }                                                                    
		}                                                                          

		public static string qNC_AnaliseCriticaCount
		{                                                                          
			get { 
                            return @" select count(*) from NC_AnaliseCritica";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
