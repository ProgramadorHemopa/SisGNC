using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{             
    public enum SituacaoPlanoAcaoFLUXO
    {
        EmElaboracao = 1,
        EmExecucao = 2,
        Executado = 3,
        EmVerificacaoEficacia = 6,
        Concluído = 4,
        ConcluídoNãoEficaz = 5,
        Cancelado = 7,
    }
    public enum SituacaoPlanoAcao
    {
        EmElaboracao = 1,
        EmExecucao = 2,
        Executado = 3,
        Aprovado = 4,
        NaoAprovado = 5,
    }

    public static class NC_PlanoAcaoQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "NC_PlanoAcao";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gPLNAC_ID = new DataField("PLNAC_ID", 0);

		public static DataField _PLNAC_ID
		{                                                                          
			get { return gPLNAC_ID; }                                           
		}

		private static DataField  gPLNAC_NOME = new DataField("PLNAC_NOME", 1);

		public static DataField _PLNAC_NOME
		{                                                                          
			get { return gPLNAC_NOME; }                                           
		}

        private static DataField gMATRICULA = new DataField("MATRICULA", 0);

        public static DataField _MATRICULA
        {
            get { return gMATRICULA; }
        }

		private static DataField  gPLNAC_DATAREGISTRO = new DataField("PLNAC_DATAREGISTRO", 2);

		public static DataField _PLNAC_DATAREGISTRO
		{                                                                          
			get { return gPLNAC_DATAREGISTRO; }                                           
		}

        private static DataField gPLNAC_SITUACAO = new DataField("PLNAC_SITUACAO", 0);

        public static DataField _PLNAC_SITUACAO
        {
            get { return gPLNAC_SITUACAO; }
        }

        private static DataField STPLNAC_ID = new DataField("STPLNAC_ID", 0);

        public static DataField _STPLNAC_ID
        {
            get { return STPLNAC_ID; }
        }

        private static DataField  gOCR_ID = new DataField("OCR_ID", 0);

		public static DataField _OCR_ID
		{                                                                          
			get { return gOCR_ID; }                                           
		}

		private static DataField  gPLNAC_REGDATE = new DataField("PLNAC_REGDATE", 2);

		public static DataField _PLNAC_REGDATE
		{                                                                          
			get { return gPLNAC_REGDATE; }                                           
		}

        private static DataField gPLNAC_DATAJUSTIFICATIVANQ = new DataField("PLNAC_DATAJUSTIFICATIVANQ", 2);

        public static DataField _PLNAC_DATAJUSTIFICATIVANQ
        {
            get { return gPLNAC_DATAJUSTIFICATIVANQ; }
        }

        private static DataField gPLNAC_JUSTIFICATIVANQ = new DataField("PLNAC_JUSTIFICATIVANQ", 1);

        public static DataField _PLNAC_JUSTIFICATIVANQ
        {
            get { return gPLNAC_JUSTIFICATIVANQ; }
        }



        private static DataField  gPLNAC_REGUSER = new DataField("PLNAC_REGUSER", 0);

		public static DataField _PLNAC_REGUSER
		{                                                                          
			get { return gPLNAC_REGUSER; }                                           
		}

		private static DataField  gPLNAC_STATUS = new DataField("PLNAC_STATUS", 1);

		public static DataField _PLNAC_STATUS
		{                                                                          
			get { return gPLNAC_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from NC_PlanoAcao  WHERE PLNAC_ID = {0}
		/// </summary>                                                             
		public static string qLoadNC_PlanoAcao
		{                                                                          
			get { return " select * from NC_PlanoAcao  WHERE PLNAC_ID = {0} "; }
		}

        public static string qLoadNC_SituacaoPlanoAcao
        {
            get { return " select * from NC_SituacaoPlanodeAcao"; }
        }

        public static string qNC_PlanoAcaoList
		{                                                                          
			get { return @" 
			                select PLNAC.* , STPLNAC.STPLNAC_DESCRICAO,
                            CASE PLNAC.PLNAC_SITUACAO 
                            WHEN 1 THEN 'Em Elaboração'
                            WHEN 2 THEN 'Em Execução'
                            WHEN 3 THEN 'Executado'
                            WHEN 4 THEN 'Aprovado'
                            WHEN 5 THEN 'Não Aprovado'
                            ELSE '' END SITUACAO_PLANO
                            , (SELECT MAX(acs_dataterminoprevisao) FROM nc_acoes ACS WHERE ACS.plnac_id = PLNAC.plnac_id) DATA_PREVISAO_CONCLUSAO
			                from NC_PlanoAcao PLNAC
                            LEFT JOIN nc_situacaoplanodeacao STPLNAC
                            ON PLNAC.STPLNAC_ID = STPLNAC.STPLNAC_ID   ";
		        }                                                                    
		}                                                                          

		public static string qNC_PlanoAcaoCount
		{                                                                          
			get { 
                            return @" select count(*) from NC_PlanoAcao";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
