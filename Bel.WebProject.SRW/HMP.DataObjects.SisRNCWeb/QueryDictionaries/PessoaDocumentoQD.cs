using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{
    public enum TipoPessoaDocumento
    {
        RG = 1,
        CPF = 2,
    }

    public static class PessoaDocumentoQD
    {
        #region Table Name

        public static string TableName
        {
            get { return "PessoaDocumento"; }
        }

        #endregion

        #region Database Fields

        private static DataField gPDOC_ID = new DataField("PDOC_ID", 0);

        public static DataField _PDOC_ID
        {
            get { return gPDOC_ID; }
        }

        private static DataField gPDOC_NUMERODOCUMENTO = new DataField("PDOC_NUMERODOCUMENTO", 1);

        public static DataField _PDOC_NUMERODOCUMENTO
        {
            get { return gPDOC_NUMERODOCUMENTO; }
        }

        private static DataField gPDCO_ORGAOEXPEDIDOR = new DataField("PDCO_ORGAOEXPEDIDOR", 1);

        public static DataField _PDCO_ORGAOEXPEDIDOR
        {
            get { return gPDCO_ORGAOEXPEDIDOR; }
        }


        private static DataField gTPDOC_ID = new DataField("TPDOC_ID", 0);

        public static DataField _TPDOC_ID
        {
            get { return gTPDOC_ID; }
        }

        private static DataField gTPDOC_DESCRICAO = new DataField("TPDOC_DESCRICAO", 1);

        public static DataField _TPDOC_DESCRICAO
        {
            get { return gTPDOC_DESCRICAO; }
        }

        private static DataField gPES_ID = new DataField("PES_ID", 0);

        public static DataField _PES_ID
        {
            get { return gPES_ID; }
        }

        private static DataField gPDOC_REGDATE = new DataField("PDOC_REGDATE", 2);

        public static DataField _PDOC_REGDATE
        {
            get { return gPDOC_REGDATE; }
        }

        private static DataField gPDOC_REGUSER = new DataField("PDOC_REGUSER", 1);

        public static DataField _PDOC_REGUSER
        {
            get { return gPDOC_REGUSER; }
        }

        private static DataField gPDOC_STATUS = new DataField("PDOC_STATUS", 1);

        public static DataField _PDOC_STATUS
        {
            get { return gPDOC_STATUS; }
        }

        private static DataField gSRV_ID = new DataField("SRV_ID", 0);

        public static DataField _SRV_ID
        {
            get { return gSRV_ID; }
        
        }

        private static DataField gPDOC_DATA = new DataField("PDOC_DATA", 2);

        public static DataField _PDOC_DATA
        {
            get { return gPDOC_DATA; }
        }

        private static DataField gPDOC_ANEXO = new DataField("PDOC_ANEXO", 3);

        public static DataField _PDOC_ANEXO
        {
            get { return gPDOC_ANEXO; }
        }

        private static DataField gPDOC_ANEXODESCRICAO = new DataField("PDOC_ANEXODESCRICAO", 1);

        public static DataField _PDOC_ANEXODESCRICAO
        {
            get { return gPDOC_ANEXODESCRICAO; }
        }
        #endregion

        #region Queries

        /// <summary>                                                              
        /// select * from PessoaDocumento  WHERE PDOC_ID = {0}
        /// </summary>                                                             
        public static string qLoadPessoaDocumento
        {
            get { return " select * from PessoaDocumento  WHERE PDOC_ID = {0} "; }
        }

        public static string qPessoaDocumentoList
        {
            get
            {
                return @" 
			                select * 
			                    from PessoaDocumento";
            }
        }

        public static string qPessoaDocumentoCount
        {
            get
            {
                return @" select count(*) from PessoaDocumento";
            }
        }

        public static string qPessoaDocumentoTipoDocumento
        {
            get
            {
                return @" 
                            SELECT PDOC.PDOC_ID, PDOC.PDOC_NUMERODOCUMENTO, PDOC.PDOC_STATUS,PDOC.PDCO_ORGAOEXPEDIDOR, PDOC.PDOC_DATA, PDOC.PDOC_ANEXODESCRICAO,
                                   TPDOC.TPDOC_ID, TPDOC.TPDOC_DESCRICAO, TPDOC.TPDOC_STATUS
                            FROM PESSOADOCUMENTO PDOC, TIPOPESSOADOCUMENTO TPDOC
                            WHERE PDOC.TPDOC_ID = TPDOC.TPDOC_ID
                            AND PDOC.PDOC_STATUS NOT IN ('I','E')
                            AND TPDOC.TPDOC_STATUS NOT IN ('I','E')
                        ";
            }
        }

        #endregion
    }
}
