using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{                                                                                     
	public static class NC_OcorrenciaxDocumentoQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "NC_ocorrenciaxdocumento";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gOCRDOC_ID = new DataField("OCRDOC_ID", 0);

		public static DataField _OCRDOC_ID
        {                                                                          
			get { return gOCRDOC_ID; }                                           
		}

		private static DataField  gDOC_ID= new DataField("DOC_ID", 0);

		public static DataField _DOC_ID
		{                                                                          
			get { return gDOC_ID; }                                           
		}

		private static DataField  gOCR_ID = new DataField("OCR_ID", 0);

		public static DataField _OCR_ID
		{                                                                          
			get { return gOCR_ID; }                                           
		}

		private static DataField  gOCRDOC_REGDATE = new DataField("OCRDOC_REGDATE", 2);

		public static DataField _OCRDOC_REGDATE
		{                                                                          
			get { return gOCRDOC_REGDATE; }                                           
		}

		private static DataField  gOCRDOC_REGUSER = new DataField("OCRDOC_REGUSER", 0);

		public static DataField _OCRDOC_REGUSER
		{                                                                          
			get { return gOCRDOC_REGUSER; }                                           
		}

		private static DataField  gOCRDOC_STATUS = new DataField("OCRDOC_STATUS", 1);

		public static DataField _OCRDOC_STATUS
		{                                                                          
			get { return gOCRDOC_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from NC_NormasxOcorrencia 
		/// </summary>                                                             
		public static string qLoadNC_NormasxOcorrencia
		{                                                                          
			get { return " select * from nc_ocorrenciaxdocumento  "; }
		}                                                                          

	                                                                      

		public static string qNC_NormasxOcorrenciaCount
		{                                                                          
			get { 
                            return @" select count(*) from nc_ocorrenciaxdocumento";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
