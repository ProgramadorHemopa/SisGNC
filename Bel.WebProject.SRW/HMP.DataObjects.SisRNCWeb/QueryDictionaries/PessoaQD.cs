using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{
    public static class PessoaQD
    {
        #region Table Name

        public static string TableName
        {
            get { return "Pessoa"; }
        }

        #endregion

        #region Database Fields

        private static DataField gPES_ID = new DataField("PES_ID", 0);

        public static DataField _PES_ID
        {
            get { return gPES_ID; }
        }

        //16/10/2012 Ricardo Almeida - Solicitação reunião criminal
        private static DataField gPES_TRATAMENTO = new DataField("PES_TRATAMENTO", 1);

        public static DataField _PES_TRATAMENTO
        {
            get { return gPES_TRATAMENTO; }
        }

        //03/01/2012 Ricardo Almeida - Solicitação Dra. Paula Denadai
        private static DataField gPES_DEFICIENCIA = new DataField("PES_DEFICIENCIA", 1);

        public static DataField _PES_DEFICIENCIA
        {
            get { return gPES_DEFICIENCIA; }
        }

        private static DataField gPES_CIDADE = new DataField("PES_CIDADE", 1);

        public static DataField _PES_CIDADE
        {
            get { return gPES_CIDADE; }
        }


        private static DataField gPES_NOME = new DataField("PES_NOME", 1);

        public static DataField _PES_NOME
        {
            get { return gPES_NOME; }
        }

        private static DataField gPES_SEXO = new DataField("PES_SEXO", 1);

        public static DataField _PES_SEXO
        {
            get { return gPES_SEXO; }
        }

        private static DataField gPES_MAE = new DataField("PES_MAE", 1);

        public static DataField _PES_MAE
        {
            get { return gPES_MAE; }
        }

        private static DataField gPES_PAI = new DataField("PES_PAI", 1);

        public static DataField _PES_PAI
        {
            get { return gPES_PAI; }
        }

        private static DataField gPES_FOTO = new DataField("PES_FOTO", 3);

        public static DataField _PES_FOTO
        {
            get { return gPES_FOTO; }
        }

        private static DataField gPES_PSEUDONIMO = new DataField("PES_PSEUDONIMO", 1);

        public static DataField _PES_PSEUDONIMO
        {
            get { return gPES_PSEUDONIMO; }
        }

        private static DataField gPES_NOMEFANTASIA = new DataField("PES_NOMEFANTASIA", 1);

        public static DataField _PES_NOMEFANTASIA
        {
            get { return gPES_NOMEFANTASIA; }
        }

        private static DataField gPES_NASCIMENTO = new DataField("PES_NASCIMENTO", 2);

        public static DataField _PES_NASCIMENTO
        {
            get { return gPES_NASCIMENTO; }
        }

        private static DataField gPES_EMAIL = new DataField("PES_EMAIL", 1);

        public static DataField _PES_EMAIL
        {
            get { return gPES_EMAIL; }
        }

        private static DataField gPES_OBSERVACAO = new DataField("PES_OBSERVACAO", 1);

        public static DataField _PES_OBSERVACAO
        {
            get { return gPES_OBSERVACAO; }
        }

        private static DataField gPES_ESTADOCIVIL = new DataField("PES_ESTADOCIVIL", 1);

        public static DataField _PES_ESTADOCIVIL
        {
            get { return gPES_ESTADOCIVIL; }
        }

        private static DataField gPES_PROFISSAO = new DataField("PES_PROFISSAO", 1);

        public static DataField _PES_PROFISSAO
        {
            get { return gPES_PROFISSAO; }
        }

        private static DataField gPES_NACIONALIDADE = new DataField("PES_NACIONALIDADE", 1);

        public static DataField _PES_NACIONALIDADE
        {
            get { return gPES_NACIONALIDADE; }
        }

        private static DataField gPES_FAIXASALARIAL = new DataField("PES_FAIXASALARIAL", 1);

        public static DataField _PES_FAIXASALARIAL
        {
            get { return gPES_FAIXASALARIAL; }
        }
         
        private static DataField gPES_STATUS = new DataField("PES_STATUS", 1);

        public static DataField _PES_STATUS
        {
            get { return gPES_STATUS; }
        }

        private static DataField gPES_REGDATE = new DataField("PES_REGDATE", 2);

        public static DataField _PES_REGDATE
        {
            get { return gPES_REGDATE; }
        }

        private static DataField gPES_REGUSER = new DataField("PES_REGUSER", 0);

        public static DataField _PES_REGUSER
        {
            get { return gPES_REGUSER; }
        }

        private static DataField gPES_CPF = new DataField("PES_CPF", 1);

        public static DataField _PES_CPF
        {
            get { return gPES_CPF; }
        }
        private static DataField gPES_RG = new DataField("PES_RG", 1);

        public static DataField _PES_RG
        {
            get { return gPES_RG; }
        }


        private static DataField gPES_TITULOELEITOR = new DataField("PES_TITULOELEITOR", 1);

        public static DataField _PES_TITULOELEITOR
        {
            get { return gPES_TITULOELEITOR; }
        }

        private static DataField gPES_ZONA = new DataField("PES_ZONA", 1);

        public static DataField _PES_ZONA
        {
            get { return gPES_ZONA; }
        }

        private static DataField gPES_SECAO = new DataField("PES_SECAO", 1);

        public static DataField _PES_SECAO
        {
            get { return gPES_SECAO; }
        }

        private static DataField gGRP_ID = new DataField("GRP_ID", 0);

        public static DataField _GRP_ID
        {
            get { return gGRP_ID; }
        }

        private static DataField gGRT_ID = new DataField("GRT_ID", 0);

        public static DataField _GRT_ID
        {
            get { return gGRT_ID; }
        }

        private static DataField gPDOC_NUMERODOCUMENTO = new DataField("PDOC_NUMERODOCUMENTO", 1);

        public static DataField _PDOC_NUMERODOCUMENTO
        {
            get { return gPDOC_NUMERODOCUMENTO; }
        }

        private static DataField gENDE_ID = new DataField("ENDE_ID", 0);

        public static DataField _ENDE_ID
        {
            get { return gENDE_ID; }
        }

        private static DataField gENDE_DESCRICAO = new DataField("ENDE_DESCRICAO", 1);

        public static DataField _ENDE_DESCRICAO
        {
            get { return gENDE_DESCRICAO; }
        }

        private static DataField gENDE_COMPLEMENTO = new DataField("ENDE_COMPLEMENTO", 1);

        public static DataField _ENDE_COMPLEMENTO
        {
            get { return gENDE_COMPLEMENTO; }
        }

        private static DataField gBRR_ID = new DataField("BRR_ID", 0);

        public static DataField _BRR_ID
        {
            get { return gBRR_ID; }
        }

        private static DataField gBRR_DESC = new DataField("BRR_DESC", 1);

        public static DataField _BRR_DESC
        {
            get { return gBRR_DESC; }
        }

        private static DataField gSRV_ID = new DataField("SRV_ID", 0);

        public static DataField _SRV_ID
        {
            get { return gSRV_ID; }
        }

        private static DataField gPES_UNIAOESTAVEL = new DataField("PES_UNIAOESTAVEL", 1);

        public static DataField _PES_UNIAOESTAVEL
        {
            get { return gPES_UNIAOESTAVEL; }
        }

        private static DataField gPES_TELEFONE = new DataField("PES_TELEFONE", 1);

        public static DataField _PES_TELEFONE
        {
            get { return gPES_TELEFONE; }
        }

        private static DataField gPES_RELIGIAO = new DataField("PES_RELIGIAO", 1);

        public static DataField _PES_RELIGIAO
        {
            get { return gPES_RELIGIAO; }
        }

        private static DataField gPES_DATAXENTRADA = new DataField("PES_DATAXENTRADA", 2);

        public static DataField _PES_DATAXENTRADA
        {
            get { return gPES_DATAXENTRADA; }
        }


        private static DataField gPES_ALCUNHA = new DataField("PES_ALCUNHA", 1);

        public static DataField _PES_ALCUNHA
        {
            get { return gPES_ALCUNHA; }
        }

        private static DataField gPES_MATRICULA = new DataField("PES_MATRICULA", 1);

        public static DataField _PES_MATRICULA
        {
            get { return gPES_MATRICULA; }
        }


        #endregion

        #region Queries

        /// <summary>                                                              
        /// select * from Pessoa  WHERE PES_ID = {0}
        /// </summary>                                                             
        public static string qLoadPessoa
        {
            get { return " select * from Pessoa  WHERE PES_ID = {0} "; }
        }

        public static string qPessoaList
        {
            get
            {
                return @" 
			                select * 
			                    from Pessoa";
            }
        }

        public static string qPessoaListBairro
        {
            get
            {
                return @" 
			                SELECT TRIM(EMDE_BAIRRO) BAIRRO
                            FROM ENDERECO 
                            GROUP BY TRIM(EMDE_BAIRRO)
                            ORDER BY TRIM(EMDE_BAIRRO)";
            }
        }


        public static string qPessoaConsultaList
        {
            get
            {
                return @" 
			                 SELECT PES.PES_ID, PES.PES_NOME, PES.PES_CPF, PES.PES_MAE, PES.PES_NASCIMENTO, PES_TELEFONE
                             , (SELECT CID.CID_DESC FROM PESSOAENDERECO PEND, ENDERECO ENDE, CIDADE CID WHERE rownum=1 AND  PEND.PES_ID = PES.PES_ID AND PEND.ENDE_ID = ENDE.ENDE_ID AND ENDE.TPEND_ID = 1 AND ENDE.CID_ID = CID.CID_ID AND PEND.PEND_STATUS = 'A' AND ENDE.ENDE_STATUS = 'A') ENDE_CIDADE
                             FROM PESSOA PES ";
            }
        }

        public static string qPessoaConsultaDetalhes
        {
            get
            {
                return @" 
			                 SELECT PES.PES_ID, PES.PES_NOME, PES.PES_CPF, PES.PES_MAE, PES.PES_NASCIMENTO, PES_TELEFONE, PES_FOTO, PES_PROFISSAO, PES_FAIXASALARIAL, PES_PAI, PES_ESTADOCIVIL, PES_SEXO, PES.PES_RG
                             , PES.PES_TITULOELEITOR, PES.PES_ZONA, PES.PES_SECAO, PES.GRP_ID, PES.PES_TRATAMENTO, PES.GRT_ID, PES.PES_RELIGIAO
                             , (SELECT CID.CID_DESC FROM PESSOAENDERECO PEND, ENDERECO ENDE, CIDADE CID WHERE rownum=1 AND  PEND.PES_ID = PES.PES_ID AND PEND.ENDE_ID = ENDE.ENDE_ID AND ENDE.TPEND_ID = 1 AND ENDE.CID_ID = CID.CID_ID AND PEND.PEND_STATUS = 'A' AND ENDE.ENDE_STATUS = 'A') ENDE_CIDADE
                             FROM PESSOA PES ";
            }
        }

        /// <summary>
        /// TIRAR ISSO DEPOIS QUE É SO PARA VISUALIZAÇÃO
        /// </summary>
        public static string qPessoaListPesquisaAssistido
        {
            get
            {
                return
                    @"      SELECT P.*,C.CID_DESC,E.ENDE_DESCRICAO,E.ENDE_COMPLEMENTO,E.ENDE_CEP,E.ENDE_CELULAR,E.ENDE_EMAIL FROM PESSOA P
                            LEFT OUTER JOIN PESSOAENDERECO PE ON  P.PES_ID = PE.PES_ID AND PE.PEND_STATUS='A'
                            LEFT OUTER JOIN ENDERECO E ON E.ENDE_ID = PE.ENDE_ID AND E.ENDE_STATUS='A'
                            LEFT OUTER JOIN BAIRRO B ON B.BRR_ID = E.BRR_ID AND B.BRR_STATUS='A'
                            LEFT OUTER JOIN CIDADE C ON C.CID_ID = B.CID_ID AND C.CID_STATUS='A' ";
            }
        }

        public static string qPessoaAtendimentoList
        {
            get
            {
                return @" 
			                SELECT PES.PES_ID, PES.PES_NOME, PES.PES_MAE, PES.PES_NASCIMENTO 
			                FROM PESSOA PES
                            WHERE PES.PES_STATUS = 'A' 
                        ";
            }
        }

        public static string qPessoaEndereco
        {
            get
            {
                return @" 
                            SELECT PES.PES_ID, PES.PES_NOME, PES.PES_MAE, PES.PES_EMAIL, PES.PES_NASCIMENTO, 
                                   ENDE.ENDE_ID, ENDE.ENDE_DESCRICAO, ENDE.ENDE_COMPLEMENTO,
                                   ENDE.ENDE_TELEFONE, ENDE.ENDE_CEP, BRR.BRR_ID, BRR.BRR_DESC
                            FROM PESSOA PES, ENDERECO ENDE, PESSOAENDERECO PEND, BAIRRO BRR
                            WHERE PES.PES_ID = PEND.PES_ID
                                    AND PEND.ENDE_ID = ENDE.ENDE_ID
                                    AND ENDE.TPEND_ID = 1 --ENDEREÇO RESIDENCIAL
                                    AND ENDE.BRR_ID = BRR.BRR_ID
                                    AND PES.PES_STATUS NOT IN ('I','E')
                                    AND ENDE.ENDE_STATUS NOT IN ('I','E')
                                    AND PEND.PEND_STATUS NOT IN ('I','E')
                                    AND BRR.BRR_STATUS = 'A'
                        ";
            }
        }

        public static string qPessoaDocumentoEnderecoCpf
        {
            get
            {
                return @" 
                            SELECT PES.PES_ID, PES.PES_NOME, PES.PES_MAE, PES.PES_EMAIL, PES.PES_NASCIMENTO, 
                                   PDOC.PDOC_ID, PDOC.PDOC_NUMERODOCUMENTO, ENDE.ENDE_ID, ENDE.ENDE_DESCRICAO, ENDE.ENDE_COMPLEMENTO,
                                   ENDE.ENDE_TELEFONE, ENDE.ENDE_CEP, BRR.BRR_ID, BRR.BRR_DESC
                            FROM PESSOA PES, PESSOADOCUMENTO PDOC, ENDERECO ENDE, PESSOAENDERECO PEND, BAIRRO BRR
                            WHERE PES.PES_ID = PDOC.PES_ID
                                    AND PDOC.TPDOC_ID = 2 -- CPF
                                    AND PES.PES_ID = PEND.PES_ID
                                    AND PEND.ENDE_ID = ENDE.ENDE_ID
                                    AND ENDE.TPEND_ID = 1 --ENDEREÇO RESIDENCIAL
                                    AND ENDE.BRR_ID = BRR.BRR_ID
                                    AND PES.PES_STATUS NOT IN ('I','E')
                                    AND PDOC.PDOC_STATUS NOT IN ('I','E')
                                    AND ENDE.ENDE_STATUS NOT IN ('I','E')
                                    AND PEND.PEND_STATUS NOT IN ('I','E')
                                    AND BRR.BRR_STATUS = 'A'
                        ";
            }
        }

        public static string qPessoaDocumentoEnderecoId
        {
            get
            {
                return @" 
                            SELECT PES.PES_ID, PES.PES_NOME, PES.PES_MAE, PES.PES_EMAIL, PES.PES_NASCIMENTO, 
                                   ENDE.ENDE_ID, ENDE.ENDE_DESCRICAO, ENDE.ENDE_COMPLEMENTO,
                                   ENDE.ENDE_TELEFONE, ENDE.ENDE_CEP, BRR.BRR_ID, BRR.BRR_DESC,
                                   PDOC.PDOC_ID, PDOC.PDOC_NUMERODOCUMENTO,
                                   TPDOC.TPDOC_ID, TPDOC.TPDOC_DESCRICAO
                            FROM PESSOA PES, PESSOADOCUMENTO PDOC, ENDERECO ENDE, PESSOAENDERECO PEND, BAIRRO BRR, TIPOPESSOADOCUMENTO TPDOC
                            WHERE PES.PES_ID = PDOC.PES_ID
                                    AND PDOC.TPDOC_ID = TPDOC.TPDOC_ID
                                    AND PES.PES_ID = PEND.PES_ID
                                    AND PEND.ENDE_ID = ENDE.ENDE_ID
                                    AND ENDE.TPEND_ID = 1 --ENDEREÇO RESIDENCIAL
                                    AND ENDE.BRR_ID = BRR.BRR_ID
                                    AND PES.PES_STATUS NOT IN ('I','E')
                                    AND PDOC.PDOC_STATUS NOT IN ('I','E')
                                    AND TPDOC.TPDOC_STATUS NOT IN ('I','E')
                                    AND ENDE.ENDE_STATUS NOT IN ('I','E')
                                    AND PEND.PEND_STATUS NOT IN ('I','E')
                                    AND BRR.BRR_STATUS = 'A'
                        ";
            }
        }

        public static string qPessoaCount
        {
            get
            {
                return @" select count(*) from Pessoa";
            }
        }


        public static string qUnidadeFederativa
        {
            get
            {
                return @" SELECT * FROM UNIDADEFEDERATIVA";
            }
        }

        public static string qCidade
        {
            get
            {
                return @" SELECT CID_ID, UPPER(CID_DESC) CID_DESC FROM CIDADE";
            }
        }

        public static string qBairro
        {
            get
            {
                return @" SELECT * FROM BAIRRO";
            }
        }

        public static string qTipoPessoa
        {
            get
            {
                return @" 
			                select  TPPES_ID,TPPES_DESC 
			                    from TIPOSPESSOA";
            }
        }

        public static string qTipoDocumentoPessoa
        {
            get
            {
                return @" select * from tipopessoadocumento ";
            }
        }

        public static string qPessoaUsing
        {
            get
            {
                return @" select inte.inte_id 
                            from interessado inte, documento doc
                           where inte.doc_id = doc.doc_id
                           and   inte.inte_status NOT IN ('I','E')
                           and   doc.doc_status NOT IN ('I','E')
                        
                        ";
            }
        }

        public static string qPessoaParente
        {
            get
            {
                return @" 
			                SELECT PFAM.PFAM_ID, PFAM.PFAM_TIPOREQUERENTE,
                                (SELECT PES.PES_ID FROM PESSOA PES WHERE PES.PES_ID = PFAM.PFAM_PAI AND PES.PES_STATUS = 'A') PES_PAI,
                                (SELECT PES.PES_NOME FROM PESSOA PES WHERE PES.PES_ID = PFAM.PFAM_PAI AND PES.PES_STATUS = 'A') NOME_PAI,
                                (SELECT PES.PES_ID FROM PESSOA PES WHERE PES.PES_ID = PFAM.PFAM_MAE AND PES.PES_STATUS = 'A') PES_MAE,
                                (SELECT PES.PES_NOME FROM PESSOA PES WHERE PES.PES_ID = PFAM.PFAM_MAE AND PES.PES_STATUS = 'A') NOME_MAE,
                                (SELECT PES.PES_ID FROM PESSOA PES WHERE PES.PES_ID = PFAM.PFAM_REQUERENTE AND PES.PES_STATUS = 'A') PES_REQUERENTE,
                                (SELECT PES.PES_NOME FROM PESSOA PES WHERE PES.PES_ID = PFAM.PFAM_REQUERENTE AND PES.PES_STATUS = 'A') NOME_REQUERENTE
                            FROM PESSOAFAMILIA PFAM
                            WHERE PFAM.PFAM_STATUS = 'A'
                            AND PFAM.PES_ID = {0}";
            }
        }

        public static string qDadosCompletoPessoa
        {
            get
            {
                return @" 
                            SELECT PES.PES_NASCIMENTO, PES.PES_TELEFONE, PES.PES_MAE, PES.PES_PAI, PES.PES_TRATAMENTO, PES.PES_NOME, PES.PES_ESTADOCIVIL, PES.PES_PROFISSAO, PES.PES_NACIONALIDADE, PES.PES_ID,
                                (
                                SELECT ENDE.ENDE_DESCRICAO || ' ' || ENDE.ENDE_COMPLEMENTO || ', BAIRRO ' || BRR.BRR_DESC || ', CIDADE ' || CID.CID_DESC || ',  CEP ' || ENDE.ENDE_CEP
                                || '\r\n TELEFONE(S): ' || ENDE.ENDE_TELEFONE || '    ' || ENDE.ENDE_CELULAR
                                FROM ENDERECO ENDE, PESSOAENDERECO PEND, BAIRRO BRR, CIDADE CID
                                WHERE PES.PES_ID = PEND.PES_ID
                                AND PEND.ENDE_ID = ENDE.ENDE_ID
                                AND ENDE.BRR_ID = BRR.BRR_ID
                                AND BRR.CID_ID = CID.CID_ID
                                AND ENDE.TPEND_ID = 1
                                AND ENDE.ENDE_STATUS NOT IN ('I','E')
                                ) ENDERECO,
                                (
                                 SELECT PDOC.PDOC_NUMERODOCUMENTO
                                 FROM PESSOADOCUMENTO PDOC 
                                 WHERE PDOC.PES_ID = PES.PES_ID
                                 AND PDOC.PDOC_STATUS NOT IN ('I','E')
                                 AND PDOC.TPDOC_ID = 1
                                ) RG,
                                (
                                 SELECT PDOC.PDCO_ORGAOEXPEDIDOR
                                 FROM PESSOADOCUMENTO PDOC 
                                 WHERE PDOC.PES_ID = PES.PES_ID
                                 AND PDOC.PDOC_STATUS NOT IN ('I','E')
                                 AND PDOC.TPDOC_ID = 1
                                ) OE_RG,
                                (
                                 SELECT PDOC.PDOC_NUMERODOCUMENTO
                                 FROM PESSOADOCUMENTO PDOC 
                                 WHERE PDOC.PES_ID = PES.PES_ID
                                 AND PDOC.PDOC_STATUS NOT IN ('I','E')
                                 AND PDOC.TPDOC_ID = 2
                                ) CPF                                
                            FROM PESSOA PES
                            WHERE PES.PES_STATUS NOT IN ('I','E') AND PES.PES_ID = {0}
                            
			            ";
            }
        }

        public static string qCidadao
        {
            get
            {
                return @" 
                            SELECT PES.PES_NASCIMENTO, PES.PES_TELEFONE, PES.PES_MAE, PES.PES_PAI, PES.PES_TRATAMENTO, PES.PES_NOME, 
                                    PES.PES_ESTADOCIVIL, PES.PES_PROFISSAO, PES.PES_NACIONALIDADE, PES.PES_ID,
                                    PES.PES_RELIGIAO, PES.PES_ZONA, 
                                (
                                SELECT ENDE.ENDE_DESCRICAO || ' ' || ENDE.ENDE_COMPLEMENTO || ', BAIRRO ' || ENDE.EMDE_BAIRRO || ', CIDADE ' || CID.CID_DESC || ',  CEP ' || ENDE.ENDE_CEP
                                || '\r\n TELEFONE(S): ' || ENDE.ENDE_TELEFONE || '    ' || ENDE.ENDE_CELULAR
                                FROM ENDERECO ENDE, PESSOAENDERECO PEND, CIDADE CID
                                WHERE PES.PES_ID = PEND.PES_ID
                                AND PEND.ENDE_ID = ENDE.ENDE_ID
                                AND ENDE.CID_ID = CID.CID_ID
                                AND ENDE.TPEND_ID = 1
                                AND ENDE.ENDE_STATUS NOT IN ('I','E')
                                ) ENDERECO,           
                                ( SELECT GRP.GRP_DESCRICAO FROM GRUPO GRP WHERE GRP.GRP_ID = PES.GRP_ID) GRP_DESCRICAO,
                                ( SELECT GRT.GRT_DESCRICAO FROM GERENTE GRT WHERE GRT.GRT_ID = PES.GRT_ID) GRT_DESCRICAO
                            FROM PESSOA PES
                            WHERE PES.PES_STATUS NOT IN ('I','E') 
                            
			            ";
            }
        }

        #endregion
    }
}
