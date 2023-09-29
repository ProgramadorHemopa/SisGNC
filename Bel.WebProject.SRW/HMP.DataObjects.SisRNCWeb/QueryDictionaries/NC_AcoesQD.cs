using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{
    public enum SituacaoAcao
    {
        NãoIniciada = 1,
        EmExecução = 2,
        Concluído = 3,
        Atrasado = 4,
        SolicitadoReprogamacao = 5,
        Cancelado = 6,
        DevolvidoReprogramacao = 7,
        ConcluídoJustificado = 8,
        Ineficaz = 9,
    }


    public static class NC_AcoesQD
    {
        #region Table Name

        public static string TableName
        {
            get { return "NC_Acoes"; }
        }

        #endregion

        #region Database Fields

        private static DataField gACS_ID = new DataField("ACS_ID", 0);

        public static DataField _ACS_ID
        {
            get { return gACS_ID; }
        }

        private static DataField gACS_NOME = new DataField("ACS_NOME", 1);

        public static DataField _ACS_NOME
        {
            get { return gACS_NOME; }
        }


        private static DataField gACS_SITUACAO = new DataField("ACS_SITUACAO", 0);

        public static DataField _ACS_SITUACAO
        {
            get { return gACS_SITUACAO; }
        }

        private static DataField gACS_OQUE = new DataField("ACS_OQUE", 1);

        public static DataField _ACS_OQUE
        {
            get { return gACS_OQUE; }
        }

        private static DataField gACS_PORQUE = new DataField("ACS_PORQUE", 1);

        public static DataField _ACS_PORQUE
        {
            get { return gACS_PORQUE; }
        }

        private static DataField gACS_COMO = new DataField("ACS_COMO", 1);

        public static DataField _ACS_COMO
        {
            get { return gACS_COMO; }
        }

        private static DataField gACS_ONDE = new DataField("ACS_ONDE", 1);

        public static DataField _ACS_ONDE
        {
            get { return gACS_ONDE; }
        }

        private static DataField gACS_QUANTO = new DataField("ACS_QUANTO", 1);

        public static DataField _ACS_QUANTO
        {
            get { return gACS_QUANTO; }
        }

        private static DataField gUNIDADES_QUEM = new DataField("UNIDADES_QUEM", 0);

        public static DataField _UNIDADES_QUEM
        {
            get { return gUNIDADES_QUEM; }
        }

        private static DataField gACS_JUSTIFICATIVA = new DataField("ACS_JUSTIFICATIVA", 1);

        public static DataField _ACS_JUSTIFICATIVA
        {
            get { return gACS_JUSTIFICATIVA; }
        }

        private static DataField gACS_DATAEXECUCAO = new DataField("ACS_DATAEXECUCAO", 2);

        public static DataField _ACS_DATAEXECUCAO
        {
            get { return gACS_DATAEXECUCAO; }
        }

        private static DataField gACS_DESCRICAOEXECUCAO = new DataField("ACS_DESCRICAOEXECUCAO", 1);

        public static DataField _ACS_DESCRICAOEXECUCAO
        {
            get { return gACS_DESCRICAOEXECUCAO; }
        }

        private static DataField gACS_DATAINICIOPREVISAO = new DataField("ACS_DATAINICIOPREVISAO", 2);

        public static DataField _ACS_DATAINICIOPREVISAO
        {
            get { return gACS_DATAINICIOPREVISAO; }
        }

        private static DataField gACS_DATATERMINOPREVISAO = new DataField("ACS_DATATERMINOPREVISAO", 2);

        public static DataField _ACS_DATATERMINOPREVISAO
        {
            get { return gACS_DATATERMINOPREVISAO; }
        }

        private static DataField gPLNAC_ID = new DataField("PLNAC_ID", 0);

        public static DataField _PLNAC_ID
        {
            get { return gPLNAC_ID; }
        }

        private static DataField gACS_REGDATE = new DataField("ACS_REGDATE", 2);

        public static DataField _ACS_REGDATE
        {
            get { return gACS_REGDATE; }
        }

        private static DataField gACS_REGUSER = new DataField("ACS_REGUSER", 0);

        public static DataField _ACS_REGUSER
        {
            get { return gACS_REGUSER; }
        }

        private static DataField gACS_STATUS = new DataField("ACS_STATUS", 1);

        public static DataField _ACS_STATUS
        {
            get { return gACS_STATUS; }
        }

        private static DataField gACS_JUSTIFICATIVAREPROGAMACAO = new DataField("ACS_JUSTIFICATIVAREPROGAMACAO", 1);

        public static DataField _ACS_JUSTIFICATIVAREPROGAMACAO
        {
            get { return gACS_JUSTIFICATIVAREPROGAMACAO; }
        }



        private static DataField gACS_JUSTIFICATIVAATRASO = new DataField("ACS_JUSTIFICATIVAATRASO", 1);

        public static DataField _ACS_JUSTIFICATIVAATRASO
        {
            get { return gACS_JUSTIFICATIVAATRASO; }
        }
        #endregion

        #region Queries

        /// <summary>                                                              
        /// select * from NC_Acoes  WHERE ACS_ID = {0}
        /// </summary>                                                             
        public static string qLoadNC_Acoes
        {
            get { return " select * from NC_Acoes  WHERE ACS_ID = {0} "; }
        }

        public static string qNC_AcoesList
        {
            get
            {
                return @" 
			               
			                SELECT ACS.ACS_ID, ACS.ACS_SITUACAO, ACS.ACS_OQUE, ACS.ACS_PORQUE, ACS.ACS_COMO
                                , ACS.UNIDADES_QUEM, ACS.ACS_JUSTIFICATIVA, ACS.ACS_JUSTIFICATIVAATRASO, ACS.ACS_DATAEXECUCAO, ACS.ACS_DESCRICAOEXECUCAO
                                , ACS.ACS_DATAINICIOPREVISAO, ACS.ACS_DATATERMINOPREVISAO
                                , ACS.ACS_ONDE, ACS.ACS_QUANTO
                                , CASE ACS.acs_situacao 
                                WHEN 1 THEN 'Não Iniciada' 
                                WHEN 2 THEN  'Em Execução' 
                                WHEN 3 THEN 'Concluído'
                                WHEN 4 THEN 'Atrasado'
                                WHEN 5 THEN 'Solicitado Reprogamação'
                                WHEN 6 THEN 'Cancelado'
                                WHEN 7 THEN 'Devolvido Reprogramação'
                                WHEN 8 THEN 'Concluído - Justificado'
                                ELSE '' END SITUACAO
                                ,   (
                                
						   select concat(und.und_sigla, ' - ', fun.fun_nome)
                            from 
                            geapedb.ap_funcionario fun,
                            geapedb.ap_vinculo vnc, 
                            geapedb.ap_vinculoxunidade vncu,
                            geapedb.ap_unidade und,
                            geapedb.ap_funcaoxvinculo funcv,
                            geapedb.ap_funcao func
                            where fun.fun_id= vnc.fun_id 
                            and vnc.vnc_id = vncu.vnc_id
                            and vncu.und_id = und.und_id
                            and (vnc.vnc_id = funcv.vnc_id)
                            and funcv.fnc_id = func.fnc_id
                           and fun.fun_status='A'
                           and vnc.vnc_status='A'
                           and vncu.vncu_status='A'
                           and und.und_status='A'
                           and funcv.FNCVNC_STATUS='A'
                           and func.FNC_STATUS='A'                           
                           and vncu.vncu_dataFim is null
                           and funcv.fncvnc_dataFim is null
                            and funcv.fnc_id !=2
                             
                            and und.und_id =acs.unidades_quem) 
                            
                            
                            UNIDADENOME_QUEM
							from NC_Acoes ACS ";
            }
        }

        public static string qNC_AcoesRelatorioList
        {
            get
            {
                return @" 
			                        select ocr.ocr_id, ocr.ocr_numero, ACS.acs_id, acs.acs_oque, acs.acs_como, acs.acs_porque, acs.acs_quanto, acs.acs_datainicioprevisao, acs.acs_dataterminoprevisao,  acs.acs_onde, acs.acs_dataexecucao
                                , CASE ACS.acs_situacao 
                                WHEN 1 THEN 'Não Iniciada' 
                                WHEN 2 THEN  'Em Execução' 
                                WHEN 3 THEN 'Concluído'
                                WHEN 4 THEN 'Atrasado'
                                WHEN 5 THEN 'Solicitado Reprogamação'
                                WHEN 6 THEN 'Cancelado'
                                WHEN 7 THEN 'Devolvido Reprogramação'
                                ELSE '' END SITUACAO
                                , PLNAC.PLNAC_NOME, PLNAC.PLNAC_ID 
                                , CASE PLNAC.PLNAC_SITUACAO
                                    WHEN 1 THEN 'Em Elaboração'
                                    WHEN 2 THEN 'Em Execução'
                                    WHEN 3 THEN 'Executado'
                                    WHEN 4 THEN 'Aprovado'
                                    WHEN 5 THEN 'Não Aprovado'
                                    ELSE '' END PLNAC_SITUACAODESCRICAO
                                , 
                                
                                
                                (
                                
						   select concat(und.und_sigla, ' - ', fun.fun_nome)
                            from 
                            geapedb.ap_funcionario fun,
                            geapedb.ap_vinculo vnc, 
                            geapedb.ap_vinculoxunidade vncu,
                            geapedb.ap_unidade und,
                            geapedb.ap_funcaoxvinculo funcv,
                            geapedb.ap_funcao func
                            where fun.fun_id= vnc.fun_id 
                            and vnc.vnc_id = vncu.vnc_id
                            and vncu.und_id = und.und_id
                            and (vnc.vnc_id = funcv.vnc_id)
                            and funcv.fnc_id = func.fnc_id
                           and fun.fun_status='A'
                           and vnc.vnc_status='A'
                           and vncu.vncu_status='A'
                           and und.und_status='A'
                           and funcv.FNCVNC_STATUS='A'
                           and func.FNC_STATUS='A'                           
                           and vncu.vncu_dataFim is null
                           and funcv.fncvnc_dataFim is null
                            and funcv.fnc_id !=2
                             
                            and und.und_id =acs.unidades_quem) 
                            
                            
                            UNIDADENOME_QUEM
			                from nc_acoes acs, NC_PlanoAcao PLNAC, nc_ocorrencia ocr
                            where acs.plnac_id = plnac.plnac_id and plnac.ocr_id = ocr.ocr_id
                            and acs.acs_status = 'A' and plnac.plnac_status = 'A' and ocr.ocr_status = 'A' ";
            }
        }

        public static string qNC_AcoesAnexo
        {
            get
            {
                return @" 
			                select ACS.ACS_ANEXOEXECUCAO, ACS.ACS_ANEXODESCRICAO
			                    from NC_Acoes ACS";
            }
        }

        public static string qNC_AcoesCount
        {
            get
            {
                return @" select count(*) from NC_Acoes";
            }
        }

        public static string qNC_AcoesPlanoAcaoCount
        {
            get
            {
                return @" select  ACS.ACS_ID
                            from NC_PLANOACAO PLNAC, NC_ACOES ACS
                            WHERE PLNAC.PLNAC_ID = ACS.PLNAC_ID
                            AND PLNAC.PLNAC_STATUS = 'A'
                            AND ACS.ACS_STATUS = 'A'";
            }
        }

        #endregion
    }
}
