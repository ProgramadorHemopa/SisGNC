using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{                                                                                     
	public static class NC_VerificarEficaciaQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "NC_VerificarEficacia";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gVRFEFC_ID = new DataField("VRFEFC_ID", 0);

		public static DataField _VRFEFC_ID
		{                                                                          
			get { return gVRFEFC_ID; }                                           
		}

		private static DataField  gVRFEFC_DATA = new DataField("VRFEFC_DATA", 2);

		public static DataField _VRFEFC_DATA
		{                                                                          
			get { return gVRFEFC_DATA; }                                           
		}

		private static DataField  gVRFEFC_SITUACAO = new DataField("VRFEFC_SITUACAO", 1);

		public static DataField _VRFEFC_SITUACAO
		{                                                                          
			get { return gVRFEFC_SITUACAO; }                                           
		}

		private static DataField  gVRFEFC_OBSERVACAO = new DataField("VRFEFC_OBSERVACAO", 1);

		public static DataField _VRFEFC_OBSERVACAO
		{                                                                          
			get { return gVRFEFC_OBSERVACAO; }                                           
		}

		private static DataField  gMATRICULA_REGISTROU = new DataField("MATRICULA_REGISTROU", 0);

		public static DataField _MATRICULA_REGISTROU
		{                                                                          
			get { return gMATRICULA_REGISTROU; }                                           
		}

		private static DataField  gPLNAC_ID = new DataField("PLNAC_ID", 0);

		public static DataField _PLNAC_ID
		{                                                                          
			get { return gPLNAC_ID; }                                           
		}

		private static DataField  gVRFEFC_REGDATE = new DataField("VRFEFC_REGDATE", 2);

		public static DataField _VRFEFC_REGDATE
		{                                                                          
			get { return gVRFEFC_REGDATE; }                                           
		}

		private static DataField  gVRFEFC_REGUSER = new DataField("VRFEFC_REGUSER", 0);

		public static DataField _VRFEFC_REGUSER
		{                                                                          
			get { return gVRFEFC_REGUSER; }                                           
		}

		private static DataField  gVRFEFC_STATUS = new DataField("VRFEFC_STATUS", 1);

		public static DataField _VRFEFC_STATUS
		{                                                                          
			get { return gVRFEFC_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from NC_VerificarEficacia  WHERE VRFEFC_ID = {0}
		/// </summary>                                                             
		public static string qLoadNC_VerificarEficacia
		{                                                                          
			get { return " select * from NC_VerificarEficacia  WHERE VRFEFC_ID = {0} "; }
		}                                                                          

		public static string qNC_VerificarEficaciaList
		{                                                                          
			get { return @" 
			                SELECT PLNAC.PLNAC_ID, PLNAC.PLNAC_NOME,STPLNAC.STPLNAC_DESCRICAO,
                            CASE PLNAC.PLNAC_SITUACAO 
                            WHEN 1 THEN 'Em Elaboração'
                            WHEN 2 THEN 'Em Execução'
                            WHEN 3 THEN 'Executado'
                            WHEN 4 THEN 'Concluído'
                            WHEN 5 THEN 'Não Aprovado'
                            ELSE '' END SITUACAO_PLANO,
                             VRFEFC.*
                            FROM  NC_VERIFICAREFICACIA VRFEFC, NC_PLANOACAO PLNAC                            
                            LEFT JOIN nc_situacaoplanodeacao STPLNAC
                            ON PLNAC.STPLNAC_ID = STPLNAC.STPLNAC_ID                            
							WHERE PLNAC.PLNAC_ID = VRFEFC.PLNAC_ID
                            AND PLNAC.PLNAC_STATUS = 'A' AND VRFEFC.VRFEFC_STATUS = 'A' ";
		        }                                                                    
		}                                                                          

		public static string qNC_VerificarEficaciaCount
		{                                                                          
			get { 
                            return @" select count(*) from NC_VerificarEficacia";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
