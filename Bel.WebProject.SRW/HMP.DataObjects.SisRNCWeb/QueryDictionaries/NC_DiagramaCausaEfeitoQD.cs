using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{
    public static class NC_DiagramaCausaEfeitoQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "NC_DiagramaCausaEfeito";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gDGRCE_ID = new DataField("DGRCE_ID", 0);

		public static DataField _DGRCE_ID
		{                                                                          
			get { return gDGRCE_ID; }                                           
		}

		private static DataField  gDGRCE_TIPO = new DataField("DGRCE_TIPO", 0);

		public static DataField _DGRCE_TIPO
		{                                                                          
			get { return gDGRCE_TIPO; }                                           
		}

		private static DataField  gDGRCE_DESCRICAO = new DataField("DGRCE_DESCRICAO", 1);

		public static DataField _DGRCE_DESCRICAO
		{                                                                          
			get { return gDGRCE_DESCRICAO; }                                           
		}

		private static DataField  gANCE_ID = new DataField("ANCE_ID", 0);

		public static DataField _ANCE_ID
		{                                                                          
			get { return gANCE_ID; }                                           
		}

		private static DataField  gDGRCE_REGDATE = new DataField("DGRCE_REGDATE", 2);

		public static DataField _DGRCE_REGDATE
		{                                                                          
			get { return gDGRCE_REGDATE; }                                           
		}

		private static DataField  gDGRCE_REGUSER = new DataField("DGRCE_REGUSER", 0);

		public static DataField _DGRCE_REGUSER
		{                                                                          
			get { return gDGRCE_REGUSER; }                                           
		}

		private static DataField  gDGRCE_STATUS = new DataField("DGRCE_STATUS", 1);

		public static DataField _DGRCE_STATUS
		{                                                                          
			get { return gDGRCE_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from NC_DiagramaCausaEfeito  WHERE DGRCE_ID = {0}
		/// </summary>                                                             
		public static string qLoadNC_DiagramaCausaEfeito
		{                                                                          
			get { return " select * from NC_DiagramaCausaEfeito  WHERE DGRCE_ID = {0} "; }
		}                                                                          

		public static string qNC_DiagramaCausaEfeitoList
		{                                                                          
			get { return @" 
			                SELECT DGRCE.*
                            , CASE DGRCE.DGRCE_TIPO
                            WHEN 1 THEN 'Medida'
                            WHEN 2 THEN 'Mão de Obra'
                            WHEN 3 THEN 'Método'
                            WHEN 4 THEN 'Meio Ambiente'
                            WHEN 5 THEN 'Máquinas'
                            WHEN 6 THEN 'Matéria Prima'
                            WHEN 7 THEN 'Gestão'
                            ELSE '' END DGRCE_TIPODESCRICAO 
                            FROM NC_DIAGRAMACAUSAEFEITO DGRCE ";
		        }                                                                    
		}                                                                          

		public static string qNC_DiagramaCausaEfeitoCount
		{                                                                          
			get { 
                            return @" select count(*) from NC_DiagramaCausaEfeito";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
