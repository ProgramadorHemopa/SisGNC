using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{
    public static class PESSOAXFUNCAOXLOTACAOQD
    {
        #region Table Name

        public static string TableName
        {
            get { return "PESSOAXFUNCAOXLOTACAO"; }
        }

        #endregion

        #region Database Fields

        private static DataField gPESFLOT_ID = new DataField("PESFLOT_ID", 0);

        public static DataField _PESFLOT_ID
        {
            get { return gPESFLOT_ID; }
        }

        private static DataField gPESF_ID = new DataField("PESF_ID", 0);

        public static DataField _PESF_ID
        {
            get { return gPESF_ID; }
        }

        private static DataField gLOT_ID = new DataField("LOT_ID", 0);

        public static DataField _LOT_ID
        {
            get { return gLOT_ID; }
        }


        private static DataField gVINC_ID = new DataField("VINC_ID", 0);

        public static DataField _VINC_ID
        {
            get { return gVINC_ID; }
        }


        private static DataField gPESFLOT_REGDATE = new DataField("PESFLOT_REGDATE", 2);

        public static DataField _PESFLOT_REGDATE
        {
            get { return gPESFLOT_REGDATE; }
        }

        private static DataField gPESFLOT_REGUSER = new DataField("PESFLOT_REGUSER", 1);

        public static DataField _PESFLOT_REGUSER
        {
            get { return gPESFLOT_REGUSER; }
        }

        private static DataField gPESFLOT_REGSTATUS = new DataField("PESFLOT_REGSTATUS", 1);

        public static DataField _PESFLOT_REGSTATUS
        {
            get { return gPESFLOT_REGSTATUS; }
        }

        private static DataField gPESFLOT_TIPO = new DataField("PESFLOT_TIPO", 1);

        public static DataField _PESFLOT_TIPO
        {
            get { return gPESFLOT_TIPO; }
        }

        private static DataField gNUC_ID = new DataField("NUC_ID", 0);

        public static DataField _NUC_ID
        {
            get { return gNUC_ID; }
        }

        #endregion

        #region Queries

        /// <summary>                                                              
        /// select * from PESSOAXFUNCAOXLOTACAO  WHERE PESFLOT_ID = {0}
        /// </summary>                                                             
        public static string qLoadPESSOAXFUNCAOXLOTACAO
        {
            get { return " select * from PESSOAXFUNCAOXLOTACAO  WHERE PESFLOT_ID = {0} "; }
        }

        public static string qPESSOAXFUNCAOXLOTACAOList
        {
            get
            {
                return @" 
			                 SELECT PFL.PESFLOT_ID, PFL.PESFLOT_TIPO, PF.PESF_ID, P.PES_ID, P.PES_NOME, L.LOT_ID, L.LOT_DESCRICAO, PER.PRF_ID, PER.PRF_DESCRICAO, N.NUC_ID, N.NUC_DESCRICAO,
                            V.VINC_ID, V.VINC_DESCRICAO,
                            (P.PES_NOME || '-->' || PER.PRF_DESCRICAO) PESSOA_PERFIL
                            ,'Comarca:'|| C.CID_DESC || ' -> Local :' || N.NUC_DESCRICAO || '-> Defensoria:'||L.LOT_DESCRICAO || ' -> Area atuação:' || ARATUA.ARATUA_DESCRICAO || '-> Vinculo:'|| V.VINC_DESCRICAO AS DESCRICAO
                            FROM PESSOAXFUNCAOXLOTACAO PFL
                            INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID
                            INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID 
                            LEFT OUTER JOIN LOTACAO L ON L.LOT_ID = PFL.LOT_ID
                            LEFT OUTER JOIN PERFIL PER ON PER.PRF_ID = PF.PRF_ID
                            LEFT OUTER JOIN VINCULO V ON V.VINC_ID = PFL.VINC_ID 
                            LEFT OUTER JOIN NUCLEO N ON N.NUC_ID = PFL.NUC_ID
                            LEFT OUTER JOIN CIDADE C ON C.CID_ID = N.CID_ID
                            LEFT OUTER JOIN AREAATUACAO ARATUA ON ARATUA.LOT_ID = L.LOT_ID";
            }
        }

        public static string qPESSOAXFUNCAOXLOTACAOListDefensor
        {
            get
            {
                return @" 
			                 SELECT PFL.PESFLOT_ID, PFL.PESFLOT_TIPO, PF.PESF_ID, P.PES_ID, P.PES_NOME, L.LOT_ID, L.LOT_DESCRICAO, PER.PRF_ID, PER.PRF_DESCRICAO, N.NUC_ID, N.NUC_DESCRICAO,
                            V.VINC_ID, V.VINC_DESCRICAO,
                            (P.PES_NOME || '-->' || PER.PRF_DESCRICAO) PESSOA_PERFIL
                            ,'Comarca:'|| C.CID_DESC || ' -> Local :' || N.NUC_DESCRICAO || '-> Defensoria:'||L.LOT_DESCRICAO || ' -> Area atuação:' || ARATUA.ARATUA_DESCRICAO || '-> Vinculo:'|| V.VINC_DESCRICAO AS DESCRICAO
                            FROM PESSOAXFUNCAOXLOTACAO PFL
                            INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID
                            INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID 
                            LEFT OUTER JOIN LOTACAO L ON L.LOT_ID = PFL.LOT_ID
                            LEFT OUTER JOIN PERFIL PER ON PER.PRF_ID = PF.PRF_ID
                            LEFT OUTER JOIN VINCULO V ON V.VINC_ID = PFL.VINC_ID 
                            LEFT OUTER JOIN NUCLEO N ON N.NUC_ID = L.NUC_ID
                            LEFT OUTER JOIN CIDADE C ON C.CID_ID = N.CID_ID
                            LEFT OUTER JOIN AREAATUACAO ARATUA ON ARATUA.LOT_ID = L.LOT_ID";
            }
        }

        public static string qPESSOAXFUNCAOXLOTACAONome
        {
            get
            {
                return @" 
			                SELECT DISTINCT PFL.PESFLOT_ID, PF.PESF_ID, P.PES_ID,PF.PESF_OBSERVACAOXFERIAS, P.PES_NOME, PER.PRF_DESCRICAO,
                            (P.PES_NOME || '-->' || PER.PRF_DESCRICAO) PESSOA_PERFIL
                            FROM PESSOAXFUNCAOXLOTACAO PFL
                            INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID
                            INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID 
                            LEFT OUTER JOIN LOTACAO L ON L.LOT_ID = PFL.LOT_ID
                            LEFT OUTER JOIN PERFIL PER ON PER.PRF_ID = PF.PRF_ID
                            ";
            }
        }

        public static string qPESSOAXFUNCAOXLOTACAOQuantidadeDistribuicao
        {
            get
            {
                return @" 
			                SELECT DISTINCT PFL.PESFLOT_ID, PF.PESF_ID, P.PES_ID, P.PES_NOME
                            , (SELECT  COUNT(0) FROM NUMEROXPROCESSO NUMP WHERE NUMP.NUMP_STATUS = 'A' AND NUMP.NUMP_DATAXSORTEIO = TO_DATE('{0}','DD/MM/YYYY') 
                                AND NUMP.PESF_ID = PF.PESF_ID) QTD_PROCESSOS
                            FROM PESSOAXFUNCAOXLOTACAO PFL
                            INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID
                            INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID 
                            LEFT OUTER JOIN LOTACAO L ON L.LOT_ID = PFL.LOT_ID
                            WHERE PFL.PESFLOT_REGSTATUS='A'
                            AND PF.PESF_STATUS='A'
                            AND P.PES_STATUS='A'
                            AND PFL.PESFLOT_TIPO = 'D'
                            ";
            }
        }

        public static string qPESSOAXFUNCAOXLOTACAOLista
        {
            get
            {
                return @" 
			                SELECT DISTINCT PFL.PESFLOT_ID, PF.PESF_ID, P.PES_ID, P.PES_NOME
                            FROM PESSOAXFUNCAOXLOTACAO PFL
                            INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID
                            INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID 
                            LEFT OUTER JOIN LOTACAO L ON L.LOT_ID = PFL.LOT_ID
                            WHERE PFL.PESFLOT_REGSTATUS='A'
                            AND PF.PESF_STATUS='A'
                            AND P.PES_STATUS='A'
                            AND PFL.PESFLOT_TIPO = 'D'
                            ";
            }
        }

        public static string qPESSOAXFUNCAOXLOTACAODefensores
        {
            get
            {
                return @" 
			                SELECT DISTINCT PF.PESF_ID, P.PES_ID, P.PES_NOME
                            FROM PESSOAFUNCAO PF
                            INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID 
                            ";
            }
        }


        public static string qPESSOAXFUNCAOXLOTACAOCount
        {
            get
            {
                return @" select count(*) from PESSOAXFUNCAOXLOTACAO";
            }
        }

        public static string qPESSOAFUNCAO
        {
            get
            {
                return @" 
			                select  pesf_id,pesf_id 
			                    from PESSOAFUNCAO";
            }
        }

        public static string qlotacao
        {
            get
            {
                return @" 
			                select  lot_id,lot_id 
			                    from lotacao";
            }
        }

        public static string qPessoaFuncaoLotacaoNucleoDefensoriaDefensor
        {
            get
            {
                return @" 
			                SELECT PFL.PESFLOT_ID, PFL.PESFLOT_TIPO, PF.PESF_ID, P.PES_ID, P.PES_NOME, L.LOT_ID, L.LOT_DESCRICAO, N.NUC_ID, N.NUC_DESCRICAO, N.NUC_ENDERECO,
                            V.VINC_ID, V.VINC_DESCRICAO, AR.ARATUA_ID, AR.ARATUA_DESCRICAO
                            FROM PESSOAXFUNCAOXLOTACAO PFL
                            INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID
                            INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID 
                            LEFT OUTER JOIN LOTACAO L ON L.LOT_ID = PFL.LOT_ID
                            LEFT OUTER JOIN VINCULO V ON V.VINC_ID = PFL.VINC_ID 
                            LEFT OUTER JOIN NUCLEO N ON N.NUC_ID = L.NUC_ID
                            LEFT OUTER JOIN AREAATUACAO AR ON AR.LOT_ID = L.LOT_ID
                            WHERE L.LOT_STATUS='A'
                            AND PF.PESF_STATUS='A' 
                            AND PFL.PESFLOT_REGSTATUS='A' 
                            AND N.NUC_STATUS='A' 
                        ";
            }
        }

        public static string qPessoaFuncaoLotacaoNucleoDefensor
        {
            get
            {
                return @" 
			                SELECT PFL.PESFLOT_ID, PFL.PESFLOT_TIPO, PF.PESF_ID, P.PES_ID, P.PES_NOME, L.LOT_ID, L.LOT_DESCRICAO, N.NUC_ID, N.NUC_DESCRICAO, N.NUC_ENDERECO
                            FROM PESSOAXFUNCAOXLOTACAO PFL
                            INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID
                            INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID 
                            LEFT OUTER JOIN LOTACAO L ON L.LOT_ID = PFL.LOT_ID
                            LEFT OUTER JOIN NUCLEO N ON N.NUC_ID = L.NUC_ID
                            WHERE L.LOT_STATUS='A'
                            AND PF.PESF_STATUS='A' 
                            AND PFL.PESFLOT_REGSTATUS='A' 
                            AND N.NUC_STATUS='A' 
                        ";
            }
        }

        public static string qPessoaFuncaoNucleoAtendente
        {
            get
            {
                return @" 
			                SELECT PFL.PESFLOT_ID, PFL.PESFLOT_TIPO, PF.PESF_ID, P.PES_ID, P.PES_NOME, N.NUC_ID, N.NUC_DESCRICAO, N.NUC_ENDERECO
                            FROM PESSOAXFUNCAOXLOTACAO PFL
                            INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID
                            INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID 
                            LEFT OUTER JOIN NUCLEO N ON N.NUC_ID = PFL.NUC_ID
                            WHERE PF.PESF_STATUS='A' 
                            AND PFL.PESFLOT_REGSTATUS='A' 
                            AND N.NUC_STATUS='A' 
                        ";
            }
        }

        public static string qPessoaFuncaoNucleoCidadeAtendente
        {
            get
            {
                return @" 
			                    SELECT N.NUC_ID, 'COMARCA: '||C.CID_DESC || ' -> LOCAL: '|| N.NUC_DESCRICAO AS COMARCALOCAL 
                                FROM PESSOAXFUNCAOXLOTACAO PFL, NUCLEO N, CIDADE C
                                WHERE PFL.NUC_ID = N.NUC_ID
                                AND N.CID_ID = C.CID_ID
                                AND N.NUC_STATUS='A'
                                AND C.CID_STATUS='A' 
                                AND PFL.PESFLOT_REGSTATUS = 'A'
                               
                        ";
            }
        }                                                                          



        #endregion
    }
}
