using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{                                                                                     
	public static class NC_NormasxOcorrenciaQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "NC_NormasxOcorrencia";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gNRMOCR_ID = new DataField("NRMOCR_ID", 0);

		public static DataField _NRMOCR_ID
		{                                                                          
			get { return gNRMOCR_ID; }                                           
		}

		private static DataField  gNRM_ID = new DataField("NRM_ID", 0);

		public static DataField _NRM_ID
		{                                                                          
			get { return gNRM_ID; }                                           
		}

		private static DataField  gOCR_ID = new DataField("OCR_ID", 0);

		public static DataField _OCR_ID
		{                                                                          
			get { return gOCR_ID; }                                           
		}

		private static DataField  gNRMOCR_REGDATE = new DataField("NRMOCR_REGDATE", 2);

		public static DataField _NRMOCR_REGDATE
		{                                                                          
			get { return gNRMOCR_REGDATE; }                                           
		}

		private static DataField  gNRMOCR_REGUSER = new DataField("NRMOCR_REGUSER", 0);

		public static DataField _NRMOCR_REGUSER
		{                                                                          
			get { return gNRMOCR_REGUSER; }                                           
		}

		private static DataField  gNRMOCR_STATUS = new DataField("NRMOCR_STATUS", 1);

		public static DataField _NRMOCR_STATUS
		{                                                                          
			get { return gNRMOCR_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from NC_NormasxOcorrencia 
		/// </summary>                                                             
		public static string qLoadNC_NormasxOcorrencia
		{                                                                          
			get { return " select * from NC_NormasxOcorrencia  "; }
		}                                                                          

		public static string qNC_NormasxOcorrenciaList
		{                                                                          
			get { return @" 
			                select nrmocr.*, nrm.nrm_descricao
                            from nc_normasxocorrencia nrmocr, nc_normas nrm
                            where nrmocr.nrm_id = nrm.nrm_id ";
		        }                                                                    
		}                                                                          

		public static string qNC_NormasxOcorrenciaCount
		{                                                                          
			get { 
                            return @" select count(*) from NC_NormasxOcorrencia";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
