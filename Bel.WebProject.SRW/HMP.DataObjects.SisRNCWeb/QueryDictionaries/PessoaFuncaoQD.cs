using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{
    public enum AreaAtuacao
    {
        Cível = 1,
        Criminal = 2,
        ExecuçãoPenal = 3,
        Naeca = 4,
        MediaçãoArbitragem = 5,
        DireitosHumanos = 6,
        EntranciaEspecial = 7,
        Secretaria = 8,
        AssistentePsicoSocial = 9,
        CívelFamilia = 10,
        CívelFazenda = 11,
        DiretoriaInterior = 12,
        CentralFlagrante = 13,
        CentralCriminal = 14,
        PsicossocialPlantao = 65,
        ViolenciaDomestica = 33,
    }

    public enum PerfilSistema
    {
        AssistentePsicoSocial = 1,
        Coordenador = 2,
        Corregedoria = 3,
        Defensor = 4,
        Estagiário = 5,
        Secretaria = 6,
        //Atendente = 7,
        Administrador = 8,
        SegundaInstancia = 9,
        RH = 10,
        Assessor = 11,
        Estagiário129 = 12,
        SedeAdministrativa = 13,
        DPG = 14,
    }


    public static class PessoaFuncaoQD
    {
        #region Table Name

        public static string TableName
        {
            get { return "PessoaFuncao"; }
        }

        #endregion

        #region Database Fields

        private static DataField gPESF_ID = new DataField("PESF_ID", 0);

        public static DataField _PESF_ID
        {
            get { return gPESF_ID; }
        }

        private static DataField gPESF_NOME = new DataField("PESF_NOME", 1);

        public static DataField _PESF_NOME
        {
            get { return gPESF_NOME; }
        }


        private static DataField gPLC_ID = new DataField("PLC_ID", 0);

        public static DataField _PLC_ID
        {
            get { return gPLC_ID; }
        }

        private static DataField gPESF_ID_PAI = new DataField("PESF_ID_PAI", 0);

        public static DataField _PESF_ID_PAI
        {
            get { return gPESF_ID_PAI; }
        }



        private static DataField gPES_ID = new DataField("PES_ID", 0);

        public static DataField _PES_ID
        {
            get { return gPES_ID; }
        }

        private static DataField gFUNC_ID = new DataField("FUNC_ID", 0);

        public static DataField _FUNC_ID
        {
            get { return gFUNC_ID; }
        }

        private static DataField gLOT_ID = new DataField("LOT_ID", 0);

        public static DataField _LOT_ID
        {
            get { return gLOT_ID; }
        }

        //10/10/2012 Ricardo Almeida. retirada do campo ARATUA_ID. A indentificação da área do funcionário será armazenada na tabela ATUACAO
        //private static DataField gARATUA_ID = new DataField("ARATUA_ID", 0);

        //public static DataField _ARATUA_ID
        //{
        //    get { return gARATUA_ID; }
        //}

        private static DataField gPESF_MATRICULA = new DataField("PESF_MATRICULA", 1);

        public static DataField _PESF_MATRICULA
        {
            get { return gPESF_MATRICULA; }
        }

        private static DataField gPESF_OAB = new DataField("PESF_OAB", 1);

        public static DataField _PESF_OAB
        {
            get { return gPESF_OAB; }
        }

        private static DataField gPESF_STATUS = new DataField("PESF_STATUS", 1);

        public static DataField _PESF_STATUS
        {
            get { return gPESF_STATUS; }
        }

        private static DataField gPESF_REGDATE = new DataField("PESF_REGDATE", 2);

        public static DataField _PESF_REGDATE
        {
            get { return gPESF_REGDATE; }
        }

        private static DataField gPESF_REGUSER = new DataField("PESF_REGUSER", 1);

        public static DataField _PESF_REGUSER
        {
            get { return gPESF_REGUSER; }
        }

        private static DataField gPRF_ID = new DataField("PRF_ID", 0);

        public static DataField _PRF_ID
        {
            get { return gPRF_ID; }
        }

        private static DataField gSCT_ID = new DataField("SCT_ID", 0);

        public static DataField _SCT_ID
        {
            get { return gSCT_ID; }
        }

        private static DataField gPESF_FERIAS = new DataField("PESF_FERIAS", 1);

        public static DataField _PESF_FERIAS
        {
            get { return gPESF_FERIAS; }
        }


        private static DataField gPESF_FERIASXDATA = new DataField("PESF_FERIASXDATA", 2);

        public static DataField _PESF_FERIASXDATA
        {
            get { return gPESF_FERIASXDATA; }
        }

        private static DataField gPESF_OBSERVACAOXFERIAS = new DataField("PESF_OBSERVACAOXFERIAS", 1);

        public static DataField _PESF_OBSERVACAOXFERIAS
        {
            get { return gPESF_OBSERVACAOXFERIAS; }
        }

        private static DataField gPESF_PRAZO = new DataField("PESF_PRAZO", 0);

        public static DataField _PESF_PRAZO
        {
            get { return gPESF_PRAZO; }
        }


        private static DataField gPESF_TITULOELEITOR = new DataField("PESF_TITULOELEITOR", 1);

        public static DataField _PESF_TITULOELEITOR
        {
            get { return gPESF_TITULOELEITOR; }
        }


        private static DataField gPESF_ZONA = new DataField("PESF_ZONA", 1);

        public static DataField _PESF_ZONA
        {
            get { return gPESF_ZONA; }
        }

        private static DataField gPESF_SECAO = new DataField("PESF_SECAO", 1);

        public static DataField _PESF_SECAO
        {
            get { return gPESF_SECAO; }
        }

        private static DataField gPESF_PAI = new DataField("PESF_PAI", 1);

        public static DataField _PESF_PAI
        {
            get { return gPESF_PAI; }
        }

        private static DataField gPESF_MAE = new DataField("PESF_MAE", 1);

        public static DataField _PESF_MAE
        {
            get { return gPESF_MAE; }
        }

        private static DataField gPESF_TELEFONE = new DataField("PESF_TELEFONE", 1);

        public static DataField _PESF_TELEFONE
        {
            get { return gPESF_TELEFONE; }
        }



        #endregion


        #region Queries

        /// <summary>                                                              
        /// select * from PessoaFuncao  WHERE PESF_ID = {0}
        /// </summary>                                                             
        public static string qLoadPessoaFuncao
        {
            get { return " select * from PessoaFuncao  WHERE PESF_ID = {0} "; }
        }

        public static string qPessoaFuncaoList
        {
            get
            {
                return @" 
			                select * 
			                    from PessoaFuncao";
            }
        }

        public static string qPolicadList
        {
            get
            {
                return @" 
			                select * 
			                    from POLICAD";
            }
        }

        public static string qPessoaFuncaoCount
        {
            get
            {
                return @" select count(*) from PessoaFuncao";
            }
        }

        public static string qFuncao
        {
            get
            {
                return @" 
			                select  FUNC_ID, FUNC_DESC 
			                    from Funcao";
            }
        }

        //        public static string qAllPessoaFuncao
        //        {
        //            get
        //            {
        //                return @"
        //                            SELECT PESF.PESF_ID, PESF.PESF_MATRICULA, PESF.PESF_OAB, PESF.ARATUA_ID,
        //                                   PES.PES_ID, PES.PES_NOME, FUNC.FUNC_ID, FUNC.FUNC_DESC,
        //                                   LOT.LOT_ID, LOT.LOT_DESCRICAO, NUC.NUC_ID, NUC.NUC_DESCRICAO, NUC.NUC_ENDERECO, SUSR.SUSR_LOGIN
        //                            FROM PESSOAFUNCAO PESF, PESSOA PES, FUNCAO FUNC, LOTACAO LOT, NUCLEO NUC, SYSTEMUSER SUSR
        //                            WHERE PESF.PES_ID = PES.PES_ID
        //                            AND PESF.FUNC_ID = FUNC.FUNC_ID
        //                            AND PESF.LOT_ID = LOT.LOT_ID
        //                            AND LOT.NUC_ID = NUC.NUC_ID
        //                            AND PES.PES_ID = SUSR.PES_ID
        //                            AND PESF.PESF_STATUS = 'A'
        //                            AND PES.PES_STATUS NOT IN ('I','E')
        //                            AND FUNC.FUNC_STATUS = 'A'
        //                            AND LOT.LOT_STATUS = 'A'
        //                            AND NUC.NUC_STATUS = 'A'
        //                            AND SUSR.SUSR_STATUS = 'A'
        //                        ";
        //            }
        //        }
        //22/10/2012 Ricardo Almeida - Mudança na query devido à nova estrutura das tabelas
        public static string qAllPessoaFuncao
        {
            get
            {
                return @"
                            SELECT DISTINCT PESF.PESF_ID, PESF.PESF_MATRICULA, PESF.PESF_OAB, PES.PES_ID, PES.PES_NOME, PESF.FUNC_ID,
                                   LOT.LOT_ID, LOT.LOT_DESCRICAO, NUC.NUC_ID, NUC.NUC_DESCRICAO, NUC.NUC_ENDERECO, SUSR.SUSR_LOGIN
                            FROM PESSOAFUNCAO PESF, PESSOA PES, LOTACAO LOT, ATUACAO ATUA, NUCLEOXAREAATUACAO NUCARATUA, NUCLEO NUC, SYSTEMUSER SUSR
                            WHERE PESF.PES_ID = PES.PES_ID
                            AND PESF.LOT_ID = LOT.LOT_ID
                            AND PES.PES_ID = SUSR.PES_ID
                            AND PESF.PESF_ID = ATUA.PESF_ID
                            AND ATUA.NUCARATUA_ID  = NUCARATUA.NUCARATUA_ID
                            AND NUCARATUA.NUC_ID = NUC.NUC_ID
                            AND PESF.PESF_STATUS = 'A'
                            AND PES.PES_STATUS = 'A'
                            AND LOT.LOT_STATUS = 'A'
                            AND ATUA.ATUA_STATUS = 'A'
                            AND NUCARATUA.NUCARATUA_STATUS = 'A'
                            AND NUC.NUC_STATUS = 'A'
                            AND SUSR.SUSR_STATUS = 'A'
                        ";
            }
        }

        public static string qPessoaFuncaoServidores
        {
            get
            {
                return @"
                            SELECT DISTINCT PESF.PESF_ID, PESF.PESF_MATRICULA, PES.PES_ID, PES.PES_NOME, PESF.FUNC_ID,
                                   NUC.NUC_ID, NUC.NUC_DESCRICAO, NUC.NUC_ENDERECO, SUSR.SUSR_LOGIN
                            FROM PESSOAFUNCAO PESF, PESSOA PES, ATUACAO ATUA, NUCLEOXAREAATUACAO NUCARATUA, NUCLEO NUC, SYSTEMUSER SUSR
                            WHERE PESF.PES_ID = PES.PES_ID
                            AND PES.PES_ID = SUSR.PES_ID
                            AND PESF.PESF_ID = ATUA.PESF_ID
                            AND ATUA.NUCARATUA_ID  = NUCARATUA.NUCARATUA_ID
                            AND NUCARATUA.NUC_ID = NUC.NUC_ID
                            AND PESF.PESF_STATUS = 'A'
                            AND PES.PES_STATUS = 'A'
                            AND ATUA.ATUA_STATUS = 'A'
                            AND NUCARATUA.NUCARATUA_STATUS = 'A'
                            AND NUC.NUC_STATUS = 'A'
                            AND SUSR.SUSR_STATUS = 'A'
                        ";
            }
        }

        public static string qPessoaFuncaoNucleoAtucao
        {
            get
            {
                return @"   
                            SELECT DISTINCT PESF.PESF_ID, PESF.FUNC_ID, NUC.NUC_ID, NUC.NUC_DESCRICAO, NUC.NUC_QTDAGENDAMENTO, NUC.NUC_QTDATENDIMENTO, NUC.NUC_ENDERECO,
                                   NUC.NUC_ESCALADEF, NUC.NUC_RETORNODEF, NUC.NUC_DESCPETICAO,
                                   (SELECT LOT.NUC_ID FROM LOTACAO LOT WHERE LOT.LOT_ID = PESF.LOT_ID AND LOT.LOT_STATUS = 'A') LOTACAO_NUCLEO
                            FROM PESSOAFUNCAO PESF,  ATUACAO ATUA, NUCLEOXAREAATUACAO NUCARATUA, NUCLEO NUC
                            WHERE PESF.PESF_ID = ATUA.PESF_ID
                            AND ATUA.NUCARATUA_ID = NUCARATUA.NUCARATUA_ID
                            AND NUCARATUA.NUC_ID = NUC.NUC_ID
                            AND PESF.PESF_STATUS = 'A'
                            AND ATUA.ATUA_STATUS = 'A'
                            AND NUCARATUA.NUCARATUA_STATUS = 'A'
                        ";
            }
        }

        //12/03/2013 - Ricardo Almeida - Retirada de método não usado
        //        public static string qPessoaFuncaoNucleoAtucaoPessoa
        //        {
        //            get
        //            {
        //                return @"   
        //                            SELECT PESF.*, ATUA.NUC_ID, NUC.NUC_DESCRICAO, PES.PES_ID, PES.PES_NOME,
        //                                   FUNC.FUNC_DESC
        //                            , (SELECT SUSR.SUSR_ID FROM SYSTEMUSER SUSR WHERE PES.PES_ID = SUSR.PES_ID AND SUSR.SUSR_STATUS = 'A' ) SUSR_ID
        //                            FROM PESSOAFUNCAO PESF, PESSOA PES,  ATUACAO ATUA, NUCLEO NUC, FUNCAO FUNC
        //                            WHERE PESF.PES_ID = PES.PES_ID
        //                            AND PESF.PESF_ID = ATUA.PESF_ID
        //                            AND ATUA.NUC_ID = NUC.NUC_ID
        //                            AND PESF.FUNC_ID = FUNC.FUNC_ID
        //                            AND NUC.NUC_STATUS = 'A'                            
        //                            AND PESF.PESF_STATUS NOT IN ('I','E')                            
        //                            AND PES.PES_STATUS NOT IN ('I','E')                            
        //                            AND ATUA.ATUA_STATUS NOT IN ('I','E')
        //                            
        //                        ";
        //            }
        //        }



        public static string qPessoaFuncaoPessoa
        {
            get
            {
                return @"   
                            SELECT PESF.*, FUNC.FUNC_ID, FUNC.FUNC_DESC, SUSR.SUSR_LOGIN, SUSR.SUSR_ID
                            FROM PESSOAFUNCAO PESF, FUNCAO FUNC, SYSTEMUSER SUSR
                            WHERE PESF.FUNC_ID = FUNC.FUNC_ID
                            AND PESF.PESF_ID = SUSR.PESF_ID
                            AND PESF.PESF_STATUS = 'A'
                            AND FUNC.FUNC_STATUS = 'A'
                            AND SUSR.SUSR_STATUS = 'A'
                        ";
            }
        }

        public static string qPerfil
        {
            get
            {
                return @"   
                            select * from perfil
                        ";
            }
        }


        public static string qPessoaFuncaoAtucao
        {
            get
            {
                return @"   
                            SELECT PESF.*
                            FROM PESSOAFUNCAO PESF, ATUACAO ATUA, NUCLEOXAREAATUACAO NUCARATUA
                            WHERE PESF.PESF_ID = ATUA.PESF_ID
                            AND ATUA.NUCARATUA_ID = NUCARATUA.NUCARATUA_ID      
                            AND PESF.PESF_STATUS = 'A'
                            AND ATUA.ATUA_STATUS = 'A'
                            AND NUCARATUA.NUCARATUA_STATUS = 'A'
                        ";
            }
        }

        //17/10/2012 Ricardo Almeida
        public static string qPessoaFuncaoPessoaNucleo
        {
            get
            {
                return @"   
                            SELECT DISTINCT PESF.*, PES.PES_NOME, NUCARATUA.NUC_ID, FUNC.FUNC_DESC, SUSR.SUSR_ID
                            FROM ATUACAO ATUA, NUCLEOXAREAATUACAO NUCARATUA, PESSOAFUNCAO PESF, PESSOA PES, FUNCAO FUNC, SYSTEMUSER SUSR
                            WHERE ATUA.NUCARATUA_ID = NUCARATUA.NUCARATUA_ID      
                            AND ATUA.PESF_ID = PESF.PESF_ID
                            AND PESF.PES_ID = PES.PES_ID
                            AND PESF.FUNC_ID = FUNC.FUNC_ID
                            AND PES.PES_ID = SUSR.PES_ID
                            AND ATUA.ATUA_STATUS = 'A'
                            AND NUCARATUA.NUCARATUA_STATUS = 'A'
                            AND PESF.PESF_STATUS = 'A'
                            AND PES.PES_STATUS = 'A'
                            AND SUSR.SUSR_STATUS = 'A'
                        ";
            }
        }

        //18/10/2012 Ricardo Almeida - Alteração na query para atendenter nova estrutura de tabela
        public static string qPessoaFuncaoNucAratuaPessoaEscala
        {
            get
            {
                return @"   
                            SELECT PESF.*, PES.PES_NOME
                            FROM ATUACAO ATUA, NUCLEOXAREAATUACAO NUCARATUA, PESSOAFUNCAO PESF, PESSOA PES, ESCALA ESC
                            WHERE ATUA.NUCARATUA_ID = NUCARATUA.NUCARATUA_ID      
                            AND ATUA.PESF_ID = PESF.PESF_ID
                            AND PESF.PES_ID = PES.PES_ID
                            AND ATUA.ATUA_ID = ESC.ATUA_ID
                            AND ATUA.ATUA_STATUS = 'A'
                            AND NUCARATUA.NUCARATUA_STATUS = 'A'
                            AND PESF.PESF_STATUS = 'A'
                            AND PES.PES_STATUS = 'A'
                            AND ESC.ESC_STATUS = 'A'
                            AND ESC.ESC_FALTA = 'N'                            
                        ";
            }
        }

        public static string qPessoaFuncaoPerfil
        {
            get
            {
                return @"   
                            SELECT P.PES_NOME ||' --> '||PER.PRF_DESCRICAO AS PESSOAXCARGO,PF.PESF_ID FROM PESSOAFUNCAO PF
                            INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID
                            INNER JOIN PERFIL PER ON PER.PRF_ID = PF.PRF_ID
                            WHERE PF.PESF_STATUS='A'
                            AND P.PES_STATUS='A'
                            AND PER.PRF_ID IN ({0})
                            ORDER BY P.PES_NOME                           
                        ";
            }
        }

        public static string qPessoaFuncaoPerfilPesfId
        {
            get
            {
                return @"   
                            SELECT P.PES_NOME ||' --> '||PER.PRF_DESCRICAO AS PESSOAXCARGO,PF.PESF_ID FROM PESSOAFUNCAO PF
                            INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID
                            INNER JOIN PERFIL PER ON PER.PRF_ID = PF.PRF_ID
                            WHERE PF.PESF_STATUS='A'
                            AND P.PES_STATUS='A'
                            AND PER.PRF_ID = {0}
                            AND PF.PESF_ID = {1}
                            ORDER BY P.PES_NOME                           
                        ";
            }
        }

        // Por Cristovam dos Reis em 25/01/2013:
        public static string qPessoaFuncaoAllDefensores
        {
            get
            {

                return @"
                                select 
                                    pf.pesf_id,
                                    p.pes_id,
                                    (p.pes_nome || ' (' || coalesce(pf.pesf_matricula, 'matrícula não informada') || ')') as pes_nome
                                  from 
                                    pessoa p join pessoafuncao pf on (p.pes_id = pf.pes_id)
                                  where 
                                    (pf.pesf_status = 'A') and
                                    (p.pes_status = 'A') and
                                    (pf.func_id in (1, 12)) and
                                    (not p.pes_nome like 'DEFENSOR 129%') and
                                    (not p.pes_nome like 'ELIEZER SIQUEIRA DE SOUZA JÚNIOR%') and     
                                    (not p.pes_nome like 'ELISEU VICTOR SOUSA%') and
                                    (not p.pes_nome like 'ELISIO DE OLIVEIRA LOPES%') and                                    
                                    (not p.pes_nome like 'GUILHERME RABBI BORTOLINI%') and            
                                    (not p.pes_nome like 'GUSTAVO CIVES SEABRA%') and            
                                    (not p.pes_nome like 'MANOEL LUIZ FERREIRA%') and            
                                    (not p.pes_nome like 'RUBENS PEDREIRO LOPES%')                        
                                  order by p.pes_nome
                                 ";
            }
        }

        public static string qPessoaFuncaoPessoaPerfil
        {
            get
            {

                return @"
                            SELECT PF.*, PER.PRF_DESCRICAO, SUSR.SUSR_ID, FUNC.FUNC_DESC
                            FROM PESSOAFUNCAO PF 
                            INNER JOIN PERFIL PER ON PER.PRF_ID = PF.PRF_ID
                            INNER JOIN SYSTEMUSER SUSR ON PF.PESF_ID = SUSR.PESF_ID
                            INNER JOIN FUNCAO FUNC ON FUNC.FUNC_ID = PF.FUNC_ID
                            WHERE PF.PESF_STATUS='A' AND SUSR.SUSR_STATUS = 'A'
                                 ";
            }
        }
         

        #endregion
    }
}
