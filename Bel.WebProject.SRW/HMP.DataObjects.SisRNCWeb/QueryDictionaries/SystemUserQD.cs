using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries                                   
{                                                                                     
	public static class SystemUserQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "geapedb.SystemUser";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gSUSR_ID = new DataField("SUSR_ID", 0);

		public static DataField _SUSR_ID
		{                                                                          
			get { return gSUSR_ID; }                                           
		}

        private static DataField gMATRICULA = new DataField("MATRICULA", 0);

        public static DataField _MATRICULA
        {
            get { return gMATRICULA; }
        }

		private static DataField  gSUSR_NAME = new DataField("SUSR_NAME", 1);

		public static DataField _SUSR_NAME
		{                                                                          
			get { return gSUSR_NAME; }                                           
		}

		private static DataField  gSUSR_LOGIN = new DataField("SUSR_LOGIN", 1);

		public static DataField _SUSR_LOGIN
		{                                                                          
			get { return gSUSR_LOGIN; }                                           
		}

		private static DataField  gSUSR_PASSWORD = new DataField("SUSR_PASSWORD", 1);

		public static DataField _SUSR_PASSWORD
		{                                                                          
			get { return gSUSR_PASSWORD; }                                           
		}

		private static DataField  gSUSR_REGDATE = new DataField("SUSR_REGDATE", 2);

		public static DataField _SUSR_REGDATE
		{                                                                          
			get { return gSUSR_REGDATE; }                                           
		}

		private static DataField  gSUSR_REGUSR = new DataField("SUSR_REGUSR", 1);

		public static DataField _SUSR_REGUSR
		{                                                                          
			get { return gSUSR_REGUSR; }                                           
		}

		private static DataField  gSUSR_STATUS = new DataField("SUSR_STATUS", 1);

		public static DataField _SUSR_STATUS
		{                                                                          
			get { return gSUSR_STATUS; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from SystemUser  WHERE SUSR_ID = {0}
		/// </summary>                                                             
		public static string qLoadSystemUser
		{                                                                          
			get { return " select * from SystemUser  WHERE SUSR_ID = {0} "; }
		}                                                                          

		public static string qSystemUserList
		{                                                                          
			get { return @" 
			                    SELECT  SUSR_ID
	  		                       , SUSR_NAME as NOME
	  		                       , SUSR_LOGIN
	  		                       , SUSR_PASSWORD
                                   , MATRICULA
	  		                       , SUSR_REGDATE
	  		                       , SUSR_REGUSR
	  		                       , SUSR_STATUS
			                    from geapedb.SystemUser";
		        }                                                                    
		}

        public static string qSystemUserPessoa
        {
            get
            {
                return @" 
			                 SELECT  SUSR_ID
	  		                       , SUSR_NAME
	  		                       , SUSR_LOGIN
	  		                       , SUSR_PASSWORD
                                   , PESF.PESF_ID
                                   , PES.PES_NOME, PESF.PRF_ID, PESF.FUNC_ID
	  		                       , SUSR_REGDATE
	  		                       , SUSR_REGUSR
	  		                       , SUSR_STATUS
			                    from SystemUser SUSR, PESSOA PES, PESSOAFUNCAO PESF
                                WHERE SUSR.PES_ID = PES.PES_ID AND PES.PES_ID = PESF.PES_ID
                                AND PESF.PESF_STATUS NOT IN ('I','E')
                         ";
            }
        }

        public static string qSystemUserFuncionario
        {
            get
            {
                return @" 
			                   SELECT  SUSR_ID
	  		                       , SUSR_NAME
	  		                       , SUSR_LOGIN
	  		                       , SUSR_PASSWORD
                                   , FUN.FUN_MATRICULA AS MATRICULA
                                   , FUN.FUN_NOME AS NOME
                                   , SUSR_TREINAMENTO
			                    from GEAPEDB.SystemUser SUSR,GEAPEDB.AP_FUNCIONARIO FUN
                                WHERE SUSR.MATRICULA = FUN.FUN_MATRICULA
                                AND SUSR.SUSR_STATUS = 'A'
                                AND FUN.FUN_STATUS='A'
                         ";
            }
        }                                                          

		public static string qSystemUserCount
		{                                                                          
			get { 
                            return @" select count(*) from geapedb.SystemUser";
	 	        }                                                                    
		}

        public static string qUnidadesVinculosLogin
        {
            get
            {
                return @" 
                          select und.und_id as id, und.und_sigla as sigla, und.und_nome UNIDADE, concat(und.und_sigla, ' - ', und.und_nome) AS UNIDADES, fun.fun_nome FUNCIONARIO,
                            fun.fun_email as email
                            , fun.fun_matricula AS MATRICULA_RESP
                            from geapedb.ap_funcionario fun, geapedb.ap_vinculo vnc, 
                            geapedb.ap_vinculoxunidade vncu, geapedb.ap_unidade und
                            where fun.fun_id = vnc.fun_id and vnc.vnc_id = vncu.vnc_id
                            and vncu.und_id = und.und_id 
                            and vncu.VNCU_DATAFIM is null
                            and vnc.vncst_id in (2,4)
                            and fun.fun_status='A'
                            and vnc.vnc_status='A'
                            and vncu.vncu_status='A'
                            and und.UND_STATUS='A'
                             ";
            }
        }
        #endregion
    }
}
