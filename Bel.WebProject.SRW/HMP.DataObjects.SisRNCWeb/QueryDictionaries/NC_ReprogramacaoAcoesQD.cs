using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{

    public static class NC_ReprogramacaoAcoesQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "NC_ReprogramacaoAcoes";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gRPGAC_ID = new DataField("RPGAC_ID", 0);

		public static DataField _RPGAC_ID
		{                                                                          
			get { return gRPGAC_ID; }                                           
		}

		private static DataField  gRPGAC_NOME = new DataField("RPGAC_NOME", 1);

		public static DataField _RPGAC_NOME
		{                                                                          
			get { return gRPGAC_NOME; }                                           
		}

		private static DataField  gRPGAC_DATAINICIOANTERIOR = new DataField("RPGAC_DATAINICIOANTERIOR", 2);

		public static DataField _RPGAC_DATAINICIOANTERIOR
		{                                                                          
			get { return gRPGAC_DATAINICIOANTERIOR; }                                           
		}

		private static DataField  gRPGAC_DATAFIMANTERIOR = new DataField("RPGAC_DATAFIMANTERIOR", 2);

		public static DataField _RPGAC_DATAFIMANTERIOR
		{                                                                          
			get { return gRPGAC_DATAFIMANTERIOR; }                                           
		}


        private static DataField gRPGAC_JUSTIFICATIVA = new DataField("RPGAC_JUSTIFICATIVA", 1);

        public static DataField _RPGAC_JUSTIFICATIVA
        {
            get { return gRPGAC_JUSTIFICATIVA; }
        }


        private static DataField gRPGAC_JUSTIFICATIVACANCELAMENTO = new DataField("RPGAC_JUSTIFICATIVACANCELAMENTO", 1);

        public static DataField _RPGAC_JUSTIFICATIVACANCELAMENTO
        {
            get { return gRPGAC_JUSTIFICATIVACANCELAMENTO; }
        }

        private static DataField gRPGAC_OBSERVACAONQ = new DataField("RPGAC_OBSERVACAONQ", 1);

        public static DataField _RPGAC_OBSERVACAONQ
        {
            get { return gRPGAC_OBSERVACAONQ; }
        }

        
		private static DataField  gACS_ID = new DataField("ACS_ID", 0);

		public static DataField _ACS_ID
		{                                                                          
			get { return gACS_ID; }                                           
		}

		private static DataField  gRPGAC_REGDATE = new DataField("RPGAC_REGDATE", 2);

		public static DataField _RPGAC_REGDATE
		{                                                                          
			get { return gRPGAC_REGDATE; }                                           
		}

		private static DataField  gRPGAC_REGUSER = new DataField("RPGAC_REGUSER", 0);

		public static DataField _RPGAC_REGUSER
		{                                                                          
			get { return gRPGAC_REGUSER; }                                           
		}

		private static DataField  gRPGAC_STATUS = new DataField("RPGAC_STATUS", 1);

		public static DataField _RPGAC_STATUS
		{                                                                          
			get { return gRPGAC_STATUS; }                                           
		}

		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from NC_ReprogramacaoAcoes  WHERE RPGAC_ID = {0}
		/// </summary>                                                             
		public static string qLoadNC_ReprogramacaoAcoes
		{                                                                          
			get { return " select * from NC_ReprogramacaoAcoes  WHERE RPGAC_ID = {0} "; }
		}                                                                          

		public static string qNC_ReprogramacaoAcoesList
		{                                                                          
			get { return @" 
			                select RPGAC.*
			                    from NC_ReprogramacaoAcoes RPGAC";
		        }                                                                    
		}                                                                          

		public static string qNC_ReprogramacaoAcoesCount
		{                                                                          
			get { 
                            return @" select count(*) from NC_ReprogramacaoAcoes";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
