using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{
    public enum SituacaoOcorrencia
    {
        Nova = 1,
        EmAnaliseCritica = 2,
        Cancelado = 3,
        EmAnaliseCausa = 4,
        EmElaboracaoPlanoAcao = 5,
        EmExecucaoPlanoAcao = 6,
        EmVerificacaoEficacia = 7,
        Concluida = 8,
        EmPlanoDeAcao = 9,
    }

    public static class NC_OcorrenciaQD
    {
        #region Table Name

        public static string TableName
        {
            get { return "NC_Ocorrencia"; }
        }

        #endregion

        #region Database Fields

        private static DataField gOCR_ID = new DataField("OCR_ID", 0);

        public static DataField _OCR_ID
        {
            get { return gOCR_ID; }
        }

        private static DataField gOCR_DESCRICAO = new DataField("OCR_DESCRICAO", 1);

        public static DataField _OCR_DESCRICAO
        {
            get { return gOCR_DESCRICAO; }
        }

        private static DataField gTPOCR_ID = new DataField("TPOCR_ID", 0);

        public static DataField _TPOCR_ID
        {
            get { return gTPOCR_ID; }
        }

        private static DataField gOCR_DATAABERTURA = new DataField("OCR_DATAABERTURA", 2);

        public static DataField _OCR_DATAABERTURA
        {
            get { return gOCR_DATAABERTURA; }
        }

        private static DataField gMTV_ID = new DataField("MTV_ID", 0);

        public static DataField _MTV_ID
        {
            get { return gMTV_ID; }
        }

        private static DataField gOCR_DATAOCORRENCIA = new DataField("OCR_DATAOCORRENCIA", 2);

        public static DataField _OCR_DATAOCORRENCIA
        {
            get { return gOCR_DATAOCORRENCIA; }
        }

        private static DataField gUNIDADE_RESPRESOLUCAO = new DataField("UNIDADE_RESPRESOLUCAO", 0);

        public static DataField _UNIDADE_RESPRESOLUCAO
        {
            get { return gUNIDADE_RESPRESOLUCAO; }
        }


        private static DataField gUNIDADE_ABERTURA = new DataField("UNIDADE_ABERTURA", 0);

        public static DataField _UNIDADE_ABERTURA
        {
            get { return gUNIDADE_ABERTURA; }
        }

        private static DataField gSTCOCR_ID = new DataField("STCOCR_ID", 0);

        public static DataField _STCOCR_ID
        {
            get { return gSTCOCR_ID; }
        }

        private static DataField gOCR_NUMERO = new DataField("OCR_NUMERO", 1);

        public static DataField _OCR_NUMERO
        {
            get { return gOCR_NUMERO; }
        }

        private static DataField gOCR_NUMEROANTIGO = new DataField("OCR_NUMEROANTIGO", 1);

        public static DataField _OCR_NUMEROANTIGO
        {
            get { return gOCR_NUMEROANTIGO; }
        }

        private static DataField gMATRICULA_RESPABERTURA = new DataField("MATRICULA_RESPABERTURA", 0);

        public static DataField _MATRICULA_RESPABERTURA
        {
            get { return gMATRICULA_RESPABERTURA; }
        }

        private static DataField gUNIDADE_LOCALOCORRENCIA = new DataField("UNIDADE_LOCALOCORRENCIA", 0);

        public static DataField _UNIDADE_LOCALOCORRENCIA
        {
            get { return gUNIDADE_LOCALOCORRENCIA; }
        }

        private static DataField gOCR_REGDATE = new DataField("OCR_REGDATE", 2);

        public static DataField _OCR_REGDATE
        {
            get { return gOCR_REGDATE; }
        }

        private static DataField gOCR_REGUSER = new DataField("OCR_REGUSER", 0);

        public static DataField _OCR_REGUSER
        {
            get { return gOCR_REGUSER; }
        }

        private static DataField gOCR_STATUS = new DataField("OCR_STATUS", 1);

        public static DataField _OCR_STATUS
        {
            get { return gOCR_STATUS; }
        }
        #endregion

        #region Queries

        /// <summary>                                                              
        /// select * from NC_Ocorrencia  WHERE OCR_ID = {0}
        /// </summary>                                                             
        public static string qLoadNC_Ocorrencia
        {
            get { return " select * from NC_Ocorrencia  WHERE OCR_ID = {0} "; }
        }

        public static string qNC_OcorrenciaList
        {
            get
            {
                return @" 
			                SELECT OCR.* , STCOCR.stcocr_descricao, TPOCR.TPOCR_DESCRICAO,  MTV.MTV_DESCRICAO, CONCAT(UNDL.UND_SIGLA, ' - ', UNDL.UND_NOME) UNIDADE_OCORRENCIA
                            , CONCAT(UNDR.UND_SIGLA, ' - ', UNDR.UND_NOME) UNIDADE_RESPONSAVEL
                            , (select fun_nome as nome from geapedb.ap_funcionario fun where fun.fun_status='A' and ocr.matricula_respabertura = fun.fun_matricula) RESPABERTURA
                            , CONCAT(OCR.OCR_NUMERO,COALESCE(OCR.OCR_NUMEROANTIGO, '')) OCR_NUMEROS
                            FROM NC_OCORRENCIA OCR, NC_SITUACAOOCORRENCIA STCOCR, NC_TIPOOCORRENCIA TPOCR, NC_MOTIVOOCORRENCIA MTV, GEAPEDB.AP_unidade UNDL, geapedb.ap_unidade UNDR
                            WHERE OCR.STCOCR_ID = STCOCR.STCOCR_ID
                            AND OCR.TPOCR_ID = TPOCR.TPOCR_ID
                            AND OCR.MTV_ID = MTV.MTV_ID
                            AND OCR.UNIDADE_LOCALOCORRENCIA = UNDL.UND_ID
                            AND OCR.UNIDADE_RESPRESOLUCAO = UNDR.UND_ID
                            AND OCR.OCR_STATUS = 'A' 
                            AND UNDR.UND_STATUS='A'    
                            AND UNDL.UND_STATUS='A' ";
            }
        }


        //Adicionado por Angelo Matos em 29092020
        public static string qNC_OcorrenciaListNQ
        {
            get
            {
                return @"SELECT * FROM
                    (
	                    SELECT 
		                    OCR.* , STCOCR.stcocr_descricao, TPOCR.TPOCR_DESCRICAO,  MTV.MTV_DESCRICAO, CONCAT(UNDL.UND_SIGLA, ' - ', UNDL.UND_NOME) UNIDADE_OCORRENCIA
		                    , CONCAT(UNDR.UND_SIGLA, ' - ', UNDR.UND_NOME) UNIDADE_RESPONSAVEL
		                    , (select fun_nome as nome from geapedb.ap_funcionario fun where fun.fun_status='A' and ocr.matricula_respabertura = fun.fun_matricula) RESPABERTURA
		                    , CONCAT(OCR.OCR_NUMERO,COALESCE(OCR.OCR_NUMEROANTIGO, '')) OCR_NUMEROS
	                    FROM 
		                    NC_OCORRENCIA OCR, NC_SITUACAOOCORRENCIA STCOCR, NC_TIPOOCORRENCIA TPOCR, NC_MOTIVOOCORRENCIA MTV, GEAPEDB.AP_unidade UNDL, geapedb.ap_unidade UNDR
	                    WHERE OCR.STCOCR_ID = STCOCR.STCOCR_ID
		                    AND OCR.TPOCR_ID = TPOCR.TPOCR_ID
		                    AND OCR.MTV_ID = MTV.MTV_ID
		                    AND OCR.UNIDADE_LOCALOCORRENCIA = UNDL.UND_ID
		                    AND OCR.UNIDADE_RESPRESOLUCAO = UNDR.UND_ID
		                    AND OCR.OCR_STATUS = 'A' 
		                    AND UNDR.UND_STATUS='A'    
		                    AND UNDL.UND_STATUS='A'
		                    AND OCR.STCOCR_ID IN (1,2,4,5,6,7)
		                    AND UNDR.UND_ID = {0}
	                    UNION
                        SELECT 
		                    OCR.* , STCOCR.stcocr_descricao, TPOCR.TPOCR_DESCRICAO,  MTV.MTV_DESCRICAO, CONCAT(UNDL.UND_SIGLA, ' - ', UNDL.UND_NOME) UNIDADE_OCORRENCIA
		                    , CONCAT(UNDR.UND_SIGLA, ' - ', UNDR.UND_NOME) UNIDADE_RESPONSAVEL
		                    , (select fun_nome as nome from geapedb.ap_funcionario fun where fun.fun_status='A' and ocr.matricula_respabertura = fun.fun_matricula) RESPABERTURA
		                    , CONCAT(OCR.OCR_NUMERO,COALESCE(OCR.OCR_NUMEROANTIGO, '')) OCR_NUMEROS
	                    FROM 
		                    NC_OCORRENCIA OCR, NC_SITUACAOOCORRENCIA STCOCR, NC_TIPOOCORRENCIA TPOCR, NC_MOTIVOOCORRENCIA MTV, GEAPEDB.AP_unidade UNDL, geapedb.ap_unidade UNDR
	                    WHERE OCR.STCOCR_ID = STCOCR.STCOCR_ID
		                    AND OCR.TPOCR_ID = TPOCR.TPOCR_ID
		                    AND OCR.MTV_ID = MTV.MTV_ID
		                    AND OCR.UNIDADE_LOCALOCORRENCIA = UNDL.UND_ID
		                    AND OCR.UNIDADE_RESPRESOLUCAO = UNDR.UND_ID
		                    AND OCR.OCR_STATUS = 'A' 
		                    AND UNDR.UND_STATUS='A'    
		                    AND UNDL.UND_STATUS='A'
		                    AND OCR.STCOCR_ID IN (2,7)
                    ) sql1";
            }
        }

        //Modificado por Angelo Matos em 15012020
        public static string qNC_FluxoOcorrenciaList
        {
            get
            {
                return @" 
			              SELECT OCR.OCR_ID,CONCAT(OCR.OCR_NUMERO,COALESCE(OCR.OCR_NUMEROANTIGO, '')) OCR_NUMEROS,ocr.ocr_numero,IF((SELECT LENGTH(ocr.ocr_descricao)>120),
                            CONCAT(SUBSTRING(ocr.ocr_descricao, 1, 120),'...'),ocr.ocr_descricao) AS OCR_DESCRICAO, mtv.mtv_descricao, stocr.stcocr_descricao
                            ,(select fun.fun_nome from geapedb.ap_funcionario fun where fun.fun_matricula = matricula_respabertura and fun.fun_status='A') as respabertura,
                            (select und.und_sigla from geapedb.ap_unidade und where und.und_id = unidade_localocorrencia and und.und_status='A') as local_ocorrencia,
                            (select und.und_sigla from geapedb.ap_unidade und where und.und_id = unidade_respresolucao and und.und_status='A') as unidade_responsavel
                            , ocr.ocr_dataocorrencia, ocr.ocr_dataabertura
                            ,IF(ocr.stcocr_id!=1 and exists (select 0 from nc_sintomasacao st where st.sntac_status='A' and st.ocr_id = ocr.ocr_id ), DATEDIFF((select max(st.sntac_data) from nc_sintomasacao st where st.sntac_status='A' and st.ocr_id = ocr.ocr_id ), ocr.ocr_dataabertura),DATEDIFF(SYSDATE(),ocr.ocr_dataabertura)) AS ATE_IMEDIATA
                            ,(select max(st.sntac_data) from nc_sintomasacao st where st.sntac_status='A' and st.ocr_id = ocr.ocr_id and ocr.stcocr_id!=1 ) AS ACAO_IMEDIATA
                            ,IF(exists (select 0 from nc_analisecritica anc where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id and anc_situacao = 0 ), DATEDIFF((select max(anc.anc_data) from nc_analisecritica anc where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id and anc_situacao = 0), (select max(st.sntac_data) from nc_sintomasacao st where st.sntac_status='A' and st.ocr_id = ocr.ocr_id )),null) AS ATE_CRITICA
                            ,(select  if( anc_situacao = 0, DATE_FORMAT(max(anc.anc_data), '%d/%m/%Y'), if(anc_situacao = 1, 'Cancelado', null)) from sisrnc.nc_analisecritica anc where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id ) AS ANALISE_CRITICA,

                            (select ance.ance_data from sisrnc.nc_analisecausaefeito ance where ance.ance_status='A' and ance.ance_id =(select max(ance.ance_id) 
                            from sisrnc.nc_analisecausaefeito ance where ance.ance_status='A' and ance.ocr_id = ocr.ocr_id)) AS ANALISECAUSA_PA, 
                            IF(exists (select 0 from 
                            sisrnc.nc_analisecausaefeito ance where ance.ance_status='A' and ance.ance_id =(select max(ance.ance_id) 
                            from sisrnc.nc_analisecausaefeito ance where ance.ance_status='A' and ance.ocr_id = ocr.ocr_id ) ), 
                            DATEDIFF((select plnac.ance_data from sisrnc.nc_analisecausaefeito plnac where plnac.ance_status='A' and 
                            plnac.ance_id =(select max(plnac.ance_id) from sisrnc.nc_analisecausaefeito plnac where plnac.ance_status='A' 
                            and plnac.ocr_id = ocr.ocr_id ) ), (select max(anc.anc_data) from sisrnc.nc_analisecritica anc 
                            where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id and anc_situacao = 0 ) ), IF(exists (select if( anc_situacao = 0, max(anc.anc_data), if(anc_situacao = 1, 'Cancelado', null)) from sisrnc.nc_analisecritica anc where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id),
                            (DATEDIFF(SYSDATE(), (select if (anc_situacao = 0, max(anc.anc_data), if (anc_situacao = 1, 'Cancelado', null)) from sisrnc.nc_analisecritica anc where anc.anc_status = 'A' and anc.ocr_id = ocr.ocr_id))), null) ) AS ATE_ANALISECAUSA,

                            IF(exists (select 0 from sisrnc.nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(select max(plnac.plnac_id) 
                            from sisrnc.nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id ) ), 
                            DATEDIFF((select plnac.plnac_dataregistro from sisrnc.nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(
                            select max(plnac.plnac_id) from sisrnc.nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id ) ), (
                            select max(ance.ance_data) from sisrnc.nc_analisecausaefeito ance where ance.ance_status='A' and ance.ocr_id = ocr.ocr_id ) ), null ) AS ATE_ELABORACAO_PA,

                            IF(exists (select 0 from nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id)), DATEDIFF((select plnac.plnac_dataregistro from nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id)),(select max(anc.anc_data) from nc_analisecritica anc where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id and anc_situacao = 0)),null) AS PA
                            ,(select plnac.plnac_dataregistro from nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id)) AS ELABORACAO_PA
                            ,(select if(acs_situacao = 4, if(ocr.stcocr_id = 6, '0001-01-02', '0001-01-03'), if(ocr.stcocr_id != 6, max(acs.acs_dataexecucao),null)) from nc_acoes acs where acs.acs_status='A' and acs.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id )) AS DATA_FINAL_PA,

                            (select  if(acs.plnac_id is not null, if( acs_situacao = 4, if(ocr.stcocr_id = 6, 'Atrasado - Em execução','Atrasado'), DATE_FORMAT(max(acs.acs_dataexecucao), '%d/%m/%Y')), null) from sisrnc.nc_acoes acs 
                            where acs.acs_status = 'A' and acs.plnac_id = (select max(plnac.plnac_id) from sisrnc.nc_planoacao plnac  where plnac.plnac_status = 'A' and plnac.ocr_id = ocr.ocr_id )) AS DATA_FINAL_PA_GRID
    

                            , IF(exists (select 0 from nc_verificareficacia ef where ef.vrfefc_status='A' and ef.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id and ocr.stcocr_id!=6) ), DATEDIFF((select max(ef.vrfefc_data) from nc_verificareficacia ef where ef.vrfefc_status='A' and ef.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id)),(select max(acs.acs_dataexecucao) from nc_acoes acs where acs.acs_status='A' and acs.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id))),null) AS ATE_VERIFICACAO
                            ,(select max(ef.vrfefc_data) from nc_verificareficacia ef where ef.vrfefc_status='A' and ef.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id and ocr.stcocr_id!=6)) as VERIFICACAO

                            FROM sisrnc.nc_ocorrencia ocr, nc_motivoocorrencia mtv, nc_situacaoocorrencia stocr
                            where ocr.mtv_id= mtv.mtv_id
                            and ocr.stcocr_id = stocr.stcocr_id
                            and ocr.ocr_status ='A' 
                            ";

                //BKP 29012020
                /*
                 * return @" 
			              SELECT OCR.OCR_ID,CONCAT(OCR.OCR_NUMERO,COALESCE(OCR.OCR_NUMEROANTIGO, '')) OCR_NUMEROS,ocr.ocr_numero,IF((SELECT LENGTH(ocr.ocr_descricao)>120),
                            CONCAT(SUBSTRING(ocr.ocr_descricao, 1, 120),'...'),ocr.ocr_descricao) AS OCR_DESCRICAO, mtv.mtv_descricao, stocr.stcocr_descricao
                            ,(select fun.fun_nome from geapedb.ap_funcionario fun where fun.fun_matricula = matricula_respabertura and fun.fun_status='A') as respabertura,
                            (select und.und_sigla from geapedb.ap_unidade und where und.und_id = unidade_localocorrencia and und.und_status='A') as local_ocorrencia,
                            (select und.und_sigla from geapedb.ap_unidade und where und.und_id = unidade_respresolucao and und.und_status='A') as unidade_responsavel
                            , ocr.ocr_dataocorrencia, ocr.ocr_dataabertura
                            ,IF(ocr.stcocr_id!=1 and exists (select 0 from nc_sintomasacao st where st.sntac_status='A' and st.ocr_id = ocr.ocr_id ), DATEDIFF((select max(st.sntac_data) from nc_sintomasacao st where st.sntac_status='A' and st.ocr_id = ocr.ocr_id ), ocr.ocr_dataabertura),DATEDIFF(SYSDATE(),ocr.ocr_dataabertura)) AS ATE_IMEDIATA
                            ,(select max(st.sntac_data) from nc_sintomasacao st where st.sntac_status='A' and st.ocr_id = ocr.ocr_id and ocr.stcocr_id!=1 ) AS ACAO_IMEDIATA
                            ,IF(exists (select 0 from nc_analisecritica anc where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id and anc_situacao = 0 ), DATEDIFF((select max(anc.anc_data) from nc_analisecritica anc where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id and anc_situacao = 0), (select max(st.sntac_data) from nc_sintomasacao st where st.sntac_status='A' and st.ocr_id = ocr.ocr_id )),null) AS ATE_CRITICA
                            ,(select  if( anc_situacao = 0, DATE_FORMAT(max(anc.anc_data), '%d/%m/%Y'), if(anc_situacao = 1, 'Cancelado', DATE_FORMAT(max(anc.anc_data), '%d/%m/%Y'))) from sisrnc.nc_analisecritica anc where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id ) AS ANALISE_CRITICA,

                            (select ance.ance_data from sisrnc.nc_analisecausaefeito ance where ance.ance_status='A' and ance.ance_id =(select max(ance.ance_id) 
                            from sisrnc.nc_analisecausaefeito ance where ance.ance_status='A' and ance.ocr_id = ocr.ocr_id)) AS ANALISECAUSA_PA, IF(exists 
                            (select 0 from sisrnc.nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(select max(plnac.plnac_id) from 
                            sisrnc.nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id ) ) AND exists (select 0 from 
                            sisrnc.nc_analisecausaefeito ance where ance.ance_status='A' and ance.ance_id =(select max(ance.ance_id) 
                            from sisrnc.nc_analisecausaefeito ance where ance.ance_status='A' and ance.ocr_id = ocr.ocr_id ) ), 
                            DATEDIFF((select plnac.ance_data from sisrnc.nc_analisecausaefeito plnac where plnac.ance_status='A' and 
                            plnac.ance_id =(select max(plnac.ance_id) from sisrnc.nc_analisecausaefeito plnac where plnac.ance_status='A' 
                            and plnac.ocr_id = ocr.ocr_id ) ), (select max(anc.anc_data) from sisrnc.nc_analisecritica anc 
                            where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id and anc_situacao = 0 ) ), null ) AS ATE_ANALISECAUSA,

                            IF(exists (select 0 from sisrnc.nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(select max(plnac.plnac_id) 
                            from sisrnc.nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id ) ), 
                            DATEDIFF((select plnac.plnac_dataregistro from sisrnc.nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(
                            select max(plnac.plnac_id) from sisrnc.nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id ) ), (
                            select max(ance.ance_data) from sisrnc.nc_analisecausaefeito ance where ance.ance_status='A' and ance.ocr_id = ocr.ocr_id ) ), null ) AS ATE_ELABORACAO_PA,

                            IF(exists (select 0 from nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id)), DATEDIFF((select plnac.plnac_dataregistro from nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id)),(select max(anc.anc_data) from nc_analisecritica anc where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id and anc_situacao = 0)),null) AS PA
                            ,(select plnac.plnac_dataregistro from nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id)) AS ELABORACAO_PA
                            ,(select  if(acs_situacao = 4, if(ocr.stcocr_id = 6, '0001-01-02', '0001-01-03'), if(ocr.stcocr_id != 6, max(acs.acs_dataexecucao),null)) from nc_acoes acs where acs.acs_status='A' and acs.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id )) AS DATA_FINAL_PA

                            ,IF(exists (select 0 from nc_verificareficacia ef where ef.vrfefc_status='A' and ef.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id and ocr.stcocr_id!=6) ), DATEDIFF((select max(ef.vrfefc_data) from nc_verificareficacia ef where ef.vrfefc_status='A' and ef.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id)),(select max(acs.acs_dataexecucao) from nc_acoes acs where acs.acs_status='A' and acs.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id))),null) AS ATE_VERIFICACAO
                            ,(select max(ef.vrfefc_data) from nc_verificareficacia ef where ef.vrfefc_status='A' and ef.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id and ocr.stcocr_id!=6)) as VERIFICACAO

                            FROM sisrnc.nc_ocorrencia ocr, nc_motivoocorrencia mtv, nc_situacaoocorrencia stocr
                            where ocr.mtv_id= mtv.mtv_id
                            and ocr.stcocr_id = stocr.stcocr_id
                            and ocr.ocr_status ='A' 
                            ";
                 */

                //BKP 27012020
                //(select if(acs.plnac_id is not null, if( acs_situacao = 4, if(ocr.stcocr_id = 6, 'Atrasado - Em execução','Atrasado'), DATE_FORMAT(max(acs.acs_dataexecucao), '%d/%m/%Y')), DATE_FORMAT(max(acs.acs_dataexecucao), '%d/%m/%Y')) from nc_acoes acs where acs.acs_status='A' and acs.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id)) AS DATA_FINAL_PA

                //return @" 
                // SELECT OCR.OCR_ID,CONCAT(OCR.OCR_NUMERO,COALESCE(OCR.OCR_NUMEROANTIGO, '')) OCR_NUMEROS,ocr.ocr_numero,IF((SELECT LENGTH(ocr.ocr_descricao)>120),
                //            CONCAT(SUBSTRING(ocr.ocr_descricao, 1, 120),'...'),ocr.ocr_descricao) AS OCR_DESCRICAO, mtv.mtv_descricao, stocr.stcocr_descricao
                //            ,(select fun.fun_nome from geapedb.ap_funcionario fun where fun.fun_matricula = matricula_respabertura and fun.fun_status='A') as respabertura,
                //            (select und.und_sigla from geapedb.ap_unidade und where und.und_id = unidade_localocorrencia and und.und_status='A') as local_ocorrencia,
                //            (select und.und_sigla from geapedb.ap_unidade und where und.und_id = unidade_respresolucao and und.und_status='A') as unidade_responsavel
                //            , ocr.ocr_dataocorrencia, ocr.ocr_dataabertura
                //            ,IF(ocr.stcocr_id!=1 and exists (select 0 from nc_sintomasacao st where st.sntac_status='A' and st.ocr_id = ocr.ocr_id ), DATEDIFF((select max(st.sntac_data) from nc_sintomasacao st where st.sntac_status='A' and st.ocr_id = ocr.ocr_id ), ocr.ocr_dataabertura),DATEDIFF(SYSDATE(),ocr.ocr_dataabertura)) AS ATE_IMEDIATA
                //            ,(select max(st.sntac_data) from nc_sintomasacao st where st.sntac_status='A' and st.ocr_id = ocr.ocr_id and ocr.stcocr_id!=1 ) AS ACAO_IMEDIATA
                //            ,IF(exists (select 0 from nc_analisecritica anc where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id and anc_situacao = 0 ), DATEDIFF((select max(anc.anc_data) from nc_analisecritica anc where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id and anc_situacao = 0), (select max(st.sntac_data) from nc_sintomasacao st where st.sntac_status='A' and st.ocr_id = ocr.ocr_id )),null) AS ATE_CRITICA
                //            ,(select max(anc.anc_data) from nc_analisecritica anc where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id and anc_situacao = 0 ) AS ANALISE_CRITICA,

                //            (select ance.ance_data from sisrnc.nc_analisecausaefeito ance where ance.ance_status='A' and ance.ance_id =(select max(ance.ance_id) 
                //            from sisrnc.nc_analisecausaefeito ance where ance.ance_status='A' and ance.ocr_id = ocr.ocr_id)) AS ANALISECAUSA_PA, IF(exists 
                //            (select 0 from sisrnc.nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(select max(plnac.plnac_id) from 
                //            sisrnc.nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id ) ) AND exists (select 0 from 
                //            sisrnc.nc_analisecausaefeito ance where ance.ance_status='A' and ance.ance_id =(select max(ance.ance_id) 
                //            from sisrnc.nc_analisecausaefeito ance where ance.ance_status='A' and ance.ocr_id = ocr.ocr_id ) ), 
                //            DATEDIFF((select plnac.ance_data from sisrnc.nc_analisecausaefeito plnac where plnac.ance_status='A' and 
                //            plnac.ance_id =(select max(plnac.ance_id) from sisrnc.nc_analisecausaefeito plnac where plnac.ance_status='A' 
                //            and plnac.ocr_id = ocr.ocr_id ) ), (select max(anc.anc_data) from sisrnc.nc_analisecritica anc 
                //            where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id and anc_situacao = 0 ) ), null ) AS ATE_ANALISECAUSA,

                //            IF(exists (select 0 from sisrnc.nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(select max(plnac.plnac_id) 
                //            from sisrnc.nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id ) ), 
                //            DATEDIFF((select plnac.plnac_dataregistro from sisrnc.nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(
                //            select max(plnac.plnac_id) from sisrnc.nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id ) ), (
                //            select max(ance.ance_data) from sisrnc.nc_analisecausaefeito ance where ance.ance_status='A' and ance.ocr_id = ocr.ocr_id ) ), null ) AS ATE_ELABORACAO_PA,

                //            IF(exists (select 0 from nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id)), DATEDIFF((select plnac.plnac_dataregistro from nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id)),(select max(anc.anc_data) from nc_analisecritica anc where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id and anc_situacao = 0)),null) AS PA
                //            ,(select plnac.plnac_dataregistro from nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id)) AS ELABORACAO_PA
                //            ,(select max(acs.acs_dataexecucao) from nc_acoes acs where acs.acs_status='A' and acs.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id and ocr.stcocr_id!=6)) AS DATA_FINAL_PA

                //            ,IF(exists (select 0 from nc_verificareficacia ef where ef.vrfefc_status='A' and ef.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id and ocr.stcocr_id!=6) ), DATEDIFF((select max(ef.vrfefc_data) from nc_verificareficacia ef where ef.vrfefc_status='A' and ef.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id)),(select max(acs.acs_dataexecucao) from nc_acoes acs where acs.acs_status='A' and acs.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id))),null) AS ATE_VERIFICACAO
                //            ,(select max(ef.vrfefc_data) from nc_verificareficacia ef where ef.vrfefc_status='A' and ef.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id and ocr.stcocr_id!=6)) as VERIFICACAO

                //            FROM sisrnc.nc_ocorrencia ocr, nc_motivoocorrencia mtv, nc_situacaoocorrencia stocr
                //            where ocr.mtv_id= mtv.mtv_id
                //            and ocr.stcocr_id = stocr.stcocr_id
                //            and ocr.ocr_status ='A' 
                //            ";


                //BKP 15012020
                //              return @" 
                //	              SELECT OCR.OCR_ID,CONCAT(OCR.OCR_NUMERO,COALESCE(OCR.OCR_NUMEROANTIGO, '')) OCR_NUMEROS,ocr.ocr_numero,IF((SELECT LENGTH(ocr.ocr_descricao)>120),
                //CONCAT(SUBSTRING(ocr.ocr_descricao, 1, 120),'...'),ocr.ocr_descricao) AS OCR_DESCRICAO, mtv.mtv_descricao, stocr.stcocr_descricao
                //                          ,(select fun.fun_nome from geapedb.ap_funcionario fun where fun.fun_matricula = matricula_respabertura and fun.fun_status='A') as respabertura,
                //                          (select und.und_sigla from geapedb.ap_unidade und where und.und_id = unidade_localocorrencia and und.und_status='A') as local_ocorrencia,
                //                          (select und.und_sigla from geapedb.ap_unidade und where und.und_id = unidade_respresolucao and und.und_status='A') as unidade_responsavel
                //                          , ocr.ocr_dataocorrencia, ocr.ocr_dataabertura
                //                          ,IF(ocr.stcocr_id!=1 and exists (select 0 from nc_sintomasacao st where st.sntac_status='A' and st.ocr_id = ocr.ocr_id ), DATEDIFF((select max(st.sntac_data) from nc_sintomasacao st where st.sntac_status='A' and st.ocr_id = ocr.ocr_id ), ocr.ocr_dataabertura),DATEDIFF(SYSDATE(),ocr.ocr_dataabertura)) AS ATE_IMEDIATA
                //                          ,(select max(st.sntac_data) from nc_sintomasacao st where st.sntac_status='A' and st.ocr_id = ocr.ocr_id and ocr.stcocr_id!=1 ) AS ACAO_IMEDIATA
                //                          ,IF(exists (select 0 from nc_analisecritica anc where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id and anc_situacao = 0 ), DATEDIFF((select max(anc.anc_data) from nc_analisecritica anc where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id and anc_situacao = 0), (select max(st.sntac_data) from nc_sintomasacao st where st.sntac_status='A' and st.ocr_id = ocr.ocr_id )),null) AS ATE_CRITICA
                //                          ,(select max(anc.anc_data) from nc_analisecritica anc where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id and anc_situacao = 0 ) AS ANALISE_CRITICA
                //                          ,IF(exists (select 0 from nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id)), DATEDIFF((select plnac.plnac_dataregistro from nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id)),(select max(anc.anc_data) from nc_analisecritica anc where anc.anc_status='A' and anc.ocr_id = ocr.ocr_id and anc_situacao = 0)),null) AS PA
                //                          ,(select plnac.plnac_dataregistro from nc_planoacao plnac where plnac.plnac_status='A' and plnac.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id)) AS ELABORACAO_PA
                //                          ,(select max(acs.acs_dataexecucao) from nc_acoes acs where acs.acs_status='A' and acs.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id and ocr.stcocr_id!=6)) AS DATA_FINAL_PA

                //                          ,IF(exists (select 0 from nc_verificareficacia ef where ef.vrfefc_status='A' and ef.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id and ocr.stcocr_id!=6) ), DATEDIFF((select max(ef.vrfefc_data) from nc_verificareficacia ef where ef.vrfefc_status='A' and ef.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id)),(select max(acs.acs_dataexecucao) from nc_acoes acs where acs.acs_status='A' and acs.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id))),null) AS ATE_VERIFICACAO
                //                          ,(select max(ef.vrfefc_data) from nc_verificareficacia ef where ef.vrfefc_status='A' and ef.plnac_id =(select max(plnac.plnac_id) from nc_planoacao plnac where plnac.plnac_status='A' and plnac.ocr_id = ocr.ocr_id and ocr.stcocr_id!=6)) as VERIFICACAO

                //                          FROM sisrnc.nc_ocorrencia ocr, nc_motivoocorrencia mtv, nc_situacaoocorrencia stocr
                //                          where ocr.mtv_id= mtv.mtv_id
                //                          and ocr.stcocr_id = stocr.stcocr_id
                //                          and ocr.ocr_status ='A' 
                //                          ";
            }
        }


        public static string qNC_OcorrenciaRelatorioList
        {
            get
            {
                return @" 
                            SELECT OCR.OCR_ID, OCR.OCR_NUMERO, OCR.OCR_DATAOCORRENCIA, OCR.OCR_DATAABERTURA, OCR.OCR_DESCRICAO , STCOCR.STCOCR_ID, STCOCR.STCOCR_DESCRICAO
							, CONCAT(UNDL.UND_SIGLA, ' - ', UNDL.UND_NOME) UNIDADE_OCORRENCIA
                            , CONCAT(UNDR.UND_SIGLA, ' - ', UNDR.UND_NOME) UNIDADE_RESPONSAVEL
                            , (select fun.fun_nome FUNCIONARIO
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
								and und.und_id = ocr.unidade_respresolucao
								) FUNCIONARIO_RESPONSAVEL, MTV.MTV_DESCRICAO
                            FROM NC_OCORRENCIA OCR,NC_MOTIVOOCORRENCIA MTV, NC_SITUACAOOCORRENCIA STCOCR, geapedb.ap_unidade UNDL, geapedb.ap_unidade UNDR
                            WHERE OCR.STCOCR_ID = STCOCR.STCOCR_ID
                            AND OCR.UNIDADE_LOCALOCORRENCIA = UNDL.UND_ID
                            AND OCR.MTV_ID = MTV.MTV_ID
                            AND OCR.UNIDADE_RESPRESOLUCAO = UNDR.UND_ID
                            AND OCR.OCR_STATUS = 'A'
                            AND UNDR.UND_STATUS='A'
                             AND UNDL.UND_STATUS='A' ";
            }
        }


        public static string qNC_OcorrenciaPendentes
        {
            get
            {
                return @" SELECT count(0) QTD
                            FROM NC_OCORRENCIA OCR, geapedb.ap_unidade UND
                            WHERE OCR.UNIDADE_RESPRESOLUCAO = UND.UND_ID
                            AND OCR.OCR_STATUS = 'A'
                            AND UND.UND_STATUS='A'
                            ";
            }
        }

        //Adicionado por Angelo Matos em 29092020
        public static string qNC_OcorrenciaPendentesNQ
        {
            get 
            {
                return @"
                            SELECT count(0) QTD FROM
                            (
	                            SELECT 
		                            *
                                FROM
		                            NC_OCORRENCIA OCR, geapedb.ap_unidade UND
	                            WHERE
		                            OCR.UNIDADE_RESPRESOLUCAO = UND.UND_ID
		                            AND OCR.OCR_STATUS = 'A'
		                            AND UND.UND_STATUS='A'
		                            AND OCR.STCOCR_ID IN (1,2,4,5,6,7)
                                    AND UND.UND_ID = {0}
	                            UNION
                                SELECT 
		                            *
	                            FROM
		                            NC_OCORRENCIA OCR, geapedb.ap_unidade UND
	                            WHERE
		                            OCR.UNIDADE_RESPRESOLUCAO = UND.UND_ID
                                    AND OCR.OCR_STATUS = 'A'
		                            AND UND.UND_STATUS='A'
		                            AND OCR.STCOCR_ID IN (2,7)
                            ) sql1
                        ";
            }
        }

        public static string qUnidades
        {
            get
            {
                return @" 
			                SELECT ID, SIGLA, NOME, concat(SIGLA, ' - ' , NOME) AS UNIDADES, MATRICULA_RESP FROM sisgeape2.unidades ";
            }
        }

        public static string qUnidadesVinculos
        {
            get
            {
                return @" 
                          select und.und_id as id, und.und_sigla as sigla, und.und_nome as UNIDADE, concat(und.und_sigla, ' - ', und.und_nome) AS UNIDADES, fun.fun_nome FUNCIONARIO,
                             fun.fun_email as email,
                             fun.fun_matricula AS MATRICULA_RESP, func.fnc_nome FUNCAO, funcv.FNCVNC_DATAINICIO, funcv.FNCVNC_DATAFIM
                            , concat(und.und_sigla, ' - ', fun.fun_nome) AS UNIDADE_RESPONSAVEL
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
                            
                             ";
            }
        }


        public static string qUnidadesDistinct
        {
            get
            {
                return @" 
                            select DISTINCT und.id, und.sigla, und.nome UNIDADE, concat(und.sigla, ' - ', und.nome) AS UNIDADES
                            from sisgeape2.funcionarios fun, sisgeape2.vinculos vnc, 
                            sisgeape2.vinculos_unidades vncu, sisgeape2.unidades und, sisgeape2.funcoes_vinculos funcv, sisgeape2.funcoes func
                            where fun.matricula = vnc.funcionarios_matricula and vnc.id = vncu.vinculos_id
                            and vncu.unidades_id = und.id and vnc.id = funcv.vinculos_id and funcv.funcoes_id = func.id
                            and vncu.dataFim is null and funcv.dataFim is null
                            
                             ";
            }
        }

        public static string qAllUnidades
        {
            get
            {
                //Modificado por Angelo Matos em 22052020
                //return @" 
                //          select und.und_id as id, und.und_sigla as sigla, und.und_nome as UNIDADE, concat(und.und_sigla, ' - ', und.und_nome) AS UNIDADES
                //             from geapedb.ap_unidade und
                //            where und.und_status='A'
                //            and UND.UND_DATAFIM IS NULL
                //            AND UND.UND_ID NOT IN(76)     

                //             ";
                return @"
                    select und.* from (
                    select 
	                    und.und_id as id, und.und_sigla, und.und_sigla as sigla, und.und_nome as UNIDADE, concat(und.und_sigla, ' - ', und.und_nome) AS UNIDADES, email.FUN_EMAIL as EMAIL from
	                    (select *
	                    from geapedb.ap_unidade unid
	                    where unid.und_status='A' and unid.UND_DATAFIM IS NULL AND unid.UND_ID NOT IN(76) ) und
	                    LEFT JOIN
	                    (select distinct vxu.UND_ID, fxv.*, vnc.FUN_ID, fun.FUN_NOME, fun.FUN_EMAIL
	                    from geapedb.ap_vinculoxunidade vxu, geapedb.ap_funcaoxvinculo fxv, geapedb.ap_vinculo vnc, geapedb.ap_funcionario fun
	                    where vxu.VNCU_DATAFIM is null and vxu.VNCU_STATUS = 'A' and fxv.VNC_ID = vxu.VNC_ID and FNCVNC_STATUS = 'A' and FNCVNC_DATAFIM is null and FNC_ID in (1, 2, 3, 6) and
		                    vnc.VNC_ID = vxu.VNC_ID AND vnc.VNC_STATUS = 'A' and fun.FUN_ID = vnc.FUN_ID) email
	                    ON und.und_id = email.UND_ID
	                    group by id) und
                ";
            }
        }


        public static string qUnidadesLocal
        {
            get
            {
                return @" 
                            select und.und_id AS ID, und.und_sigla AS SIGLA, und.und_nome UNIDADE, concat(und.und_sigla, ' - ', und.und_nome) AS UNIDADES
                            from geapedb.ap_unidade und
                            where und.und_status='A'
                            and und.und_datafim is null
                             ";
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

        public static string qFuncionarios
        {
            get
            {
                return @" 
                            SELECT FUN.FUN_MATRICULA AS MATRICULA, FUN.FUN_NOME AS NOME, FUN.FUN_EMAIL AS EMAIL, FUN.FUN_NOME AS FUNC FROM GEAPEDB.ap_funcionario FUN, GEAPEDB.AP_VINCULO vnc,
                             GEAPEDB.ap_vinculoxunidade vncu,  GEAPEDB.ap_unidade und
                             WHERE FUN.FUN_ID= VNC.FUN_ID
                             AND VNC.VNC_ID = VNCU.VNC_ID
                             AND VNC.VNCST_ID IN (2,4)
                             AND VNCU.UND_ID = UND.UND_ID 
                             AND VNCU.VNCU_DATAFIM IS NULL
                             AND FUN.FUN_STATUS='A'
                             AND VNC.VNC_STATUS='A'
                             AND VNCU.VNCU_STATUS='A'
                             AND UND.UND_STATUS = 'A'
                             ";
            }
        }
        public static string qAllFuncionarios
        {
            get
            {
                return @" 
                          SELECT fun.FUN_MATRICULA AS MATRICULA, fun.FUN_NOME AS NOME, fun.FUN_EMAIL AS EMAIL FROM geapedb.ap_funcionario fun
                            where fun.FUN_STATUS='A'
                            and fun.FUN_MATRICULA !=0 
                             ";
            }
        }

        public static string qMotivosOcorrencia
        {
            get
            {
                return @" 
			               SELECT * FROM sisrnc.nc_motivoocorrencia where MTV_STATUS = 'A'";
            }
        }


        public static string qNC_OcorrenciaDocumentosPendentes
        {
            get
            {
                return @" SELECT DOC_ID, DOC_CODIGO, DOC_NOME,DOC_VERSAO, DOC_DATAELABORACAO,DOC_DATAAPROVACAO1, DOC_DATAAPROVACAO2, DOC_DATAATIVACAO,
                                (SELECT FUN.FUN_NOME from geapedb.ap_funcionario fun WHERE FUN.FUN_STATUS='A' AND FUN.FUN_MATRICULA = DOC.MATRICULA_ELABORADOR)AS ELABORADOR,
                                (SELECT FUN.FUN_NOME from geapedb.ap_funcionario fun WHERE FUN.FUN_STATUS='A' AND FUN.FUN_MATRICULA = DOC.MATRICULA_APROVADOR1)AS APROVADOR1,
                                (SELECT FUN.FUN_NOME from geapedb.ap_funcionario fun WHERE FUN.FUN_STATUS='A' AND FUN.FUN_MATRICULA = DOC.MATRICULA_APROVADOR2)AS APROVADOR2,
                                STDOC.STDOC_DESCRICAO,
                                IF(DOC.DOC_DATAAPROVACAO1 != '', DATEDIFF(DOC.DOC_DATAAPROVACAO1, doc_dataelaboracao),null) AS ELAB_APROV1,
                                IF(DOC.DOC_DATAAPROVACAO2 != '', DATEDIFF(DOC.DOC_DATAAPROVACAO2, DOC.DOC_DATAAPROVACAO1),null) AS APROV1_APROV2,
                                IF(DOC.DOC_DATAATIVACAO != '', DATEDIFF(DOC.DOC_DATAATIVACAO, DOC.DOC_DATAAPROVACAO2),null) AS APROV2_TREINAMENTO_ATIVACAO,
                                IF(DOC.DOC_SITUACAO in (7,8,9), DATEDIFF(doc.doc_dataativacao, doc_dataelaboracao),DATEDIFF(CURDATE(), doc_dataelaboracao)) AS PERIODO_FLUXO_DIAS

                                FROM sisgd.gd_documento doc, sisgd.gd_situacaodocumento stdoc

                                where doc.doc_situacao = stdoc.stdoc_id
                                AND (DOC.DOC_SITUACAO in (7,8) and DATEDIFF(doc.doc_dataativacao, doc.doc_dataelaboracao) > 60
                                or (DOC.DOC_SITUACAO not in (7,8,9) and DATEDIFF(curdate(), doc.doc_dataelaboracao) > 60 )
                                )
                                and doc.doc_status= 'A'
                            ";
            }
        }

        #endregion
    }
}
