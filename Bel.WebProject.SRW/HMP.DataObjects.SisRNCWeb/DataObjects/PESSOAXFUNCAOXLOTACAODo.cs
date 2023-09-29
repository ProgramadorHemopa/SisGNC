using System;

using System.Collections.Generic;
using System.Data;
using System.Xml;

using RPA.DataBase;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

namespace HMP.DataObjects.SisRNCWeb
{
    [Serializable]
    public class PESSOAXFUNCAOXLOTACAODo
    {

        #region Private Methods

        private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateConversion(pValues, pResult);
        }


        private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateRequired(PESSOAXFUNCAOXLOTACAOQD._PESFLOT_ID, pValues, pResult);
            GenericDataObject.ValidateRequired(PESSOAXFUNCAOXLOTACAOQD._PESFLOT_REGDATE, pValues, pResult);
            GenericDataObject.ValidateRequired(PESSOAXFUNCAOXLOTACAOQD._PESFLOT_REGUSER, pValues, pResult);
            GenericDataObject.ValidateRequired(PESSOAXFUNCAOXLOTACAOQD._PESFLOT_REGSTATUS, pValues, pResult);
        }

        #endregion

        #region Public Methods
        public static OperationResult Insert
        (
           DataFieldCollection pValues,
           ConnectionInfo pInfo
        )
        {
            Transaction lTransaction;

            lTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (lTransaction != null);

            InsertCommand lInsert;

            OperationResult lReturn = new OperationResult(PESSOAXFUNCAOXLOTACAOQD.TableName, PESSOAXFUNCAOXLOTACAOQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lInsert = new InsertCommand(PESSOAXFUNCAOXLOTACAOQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de insert");

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }
                    decimal lSequence;
                    lSequence = DataBaseSequenceControl.GetNext(pInfo, "PESFLOT_ID");
                    lInsert.Fields.Add(PESSOAXFUNCAOXLOTACAOQD._PESFLOT_ID.Name, lSequence, (ItemType)PESSOAXFUNCAOXLOTACAOQD._PESFLOT_ID.DBType);

                    lReturn.Trace("Executando o Insert");

                    lInsert.Execute(lTransaction);

                    if (!lReturn.HasError)
                    {
                        if (lLocalTransaction)
                        {
                            if (!lReturn.HasError)
                            {
                                lReturn.Trace("Insert finalizado, executando commit");

                                lTransaction.Commit();
                            }
                            else
                            {
                                lTransaction.Rollback();
                            }
                        }
                    }
                    else
                    {
                        if (lLocalTransaction)
                            lTransaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    lReturn.OperationException = new SerializableException(ex);

                    if (lLocalTransaction)
                        lTransaction.Rollback();
                }
            }

            return lReturn;
        }
        public static OperationResult Update
        (
            DataFieldCollection pValues,
            ConnectionInfo pInfo
        )
        {

            Transaction pTransaction;

            pTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (pTransaction != null);

            UpdateCommand lUpdate;

            OperationResult lReturn = new OperationResult(PESSOAXFUNCAOXLOTACAOQD.TableName, PESSOAXFUNCAOXLOTACAOQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lUpdate = new UpdateCommand(PESSOAXFUNCAOXLOTACAOQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de update");
                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != PESSOAXFUNCAOXLOTACAOQD._PESFLOT_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", PESSOAXFUNCAOXLOTACAOQD._PESFLOT_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(PESSOAXFUNCAOXLOTACAOQD._PESFLOT_ID.Name, pValues[PESSOAXFUNCAOXLOTACAOQD._PESFLOT_ID].DBToDecimal());

                    lReturn.Trace("Executando o Update");

                    lUpdate.Execute(pTransaction);

                    if (!lReturn.HasError)
                    {
                        if (lLocalTransaction)
                        {
                            if (!lReturn.HasError)
                            {
                                lReturn.Trace("Update finalizado, executando commit");

                                pTransaction.Commit();
                            }
                            else
                            {
                                pTransaction.Rollback();
                            }
                        }
                    }
                    else
                    {
                        if (lLocalTransaction)
                            pTransaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    lReturn.OperationException = new SerializableException(ex);

                    if (lLocalTransaction)
                        pTransaction.Rollback();
                }
            }

            return lReturn;
        }


        public static DataTable GetDefensorByPESF_IDLOT_ID
        (
            string pPESF_ID,
            string pLOT_ID,
            ConnectionInfo pInfo
         )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PESSOAXFUNCAOXLOTACAOQD.qPESSOAXFUNCAOXLOTACAOListDefensor;
            lQuery += @" WHERE PFL.PESFLOT_REGSTATUS='A'
                         AND PF.PESF_STATUS='A'
                         AND P.PES_STATUS='A'
                         AND PFL.PESFLOT_TIPO IN ('D') AND L.LOT_STATUS='A' ";

            if (pPESF_ID != "")
                lQuery += string.Format(" AND PF.PESF_ID = {0}", pPESF_ID);

            if (pLOT_ID != "")
                lQuery += string.Format(" AND L.LOT_ID = {0}", pLOT_ID);
            
            lQuery += " ORDER BY PES_NOME";

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetAllDefensor(ConnectionInfo pInfo)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PESSOAXFUNCAOXLOTACAOQD.qPESSOAXFUNCAOXLOTACAODefensores;
            lQuery += @" WHERE PF.PESF_STATUS='A'
                         AND P.PES_STATUS='A'
                         AND PF.PRF_ID = 4
                         ORDER BY PES_NOME";

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }



        public static DataTable GetAllPESSOAXFUNCAOXLOTACAOESTAGIARIOOUATENDENTE(decimal pPESF_ID, ConnectionInfo pInfo)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PESSOAXFUNCAOXLOTACAOQD.qPESSOAXFUNCAOXLOTACAOList;

            lQuery += @" WHERE PFL.PESFLOT_REGSTATUS='A'
                         AND PF.PESF_STATUS='A'
                         AND P.PES_STATUS='A'
                         AND PFL.PESFLOT_TIPO IN ('A','E','S')
                         ";

            if (pPESF_ID != 0)
                lQuery += " AND PF.PESF_ID = " + pPESF_ID;

            lQuery += " ORDER BY P.PES_NOME";

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetEstagiarioAssesorAtendenteByLocal
        (
            decimal pNUC_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PESSOAXFUNCAOXLOTACAOQD.qPESSOAXFUNCAOXLOTACAOList;
            lQuery += string.Format(@" WHERE PFL.PESFLOT_REGSTATUS='A'
                         AND PF.PESF_STATUS='A'
                         AND P.PES_STATUS='A'
                         AND PFL.PESFLOT_TIPO IN ('A','E')
                         AND (L.NUC_ID = {0} OR N.NUC_ID = {0})
                         ORDER BY P.PES_NOME", pNUC_ID);

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetAllPESSOAXFUNCAOXLOTACAOESTAGIARIOOUATENDENTEByLocalFuncao
        (
            decimal pNUC_ID,
            string pListFUNC_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PESSOAXFUNCAOXLOTACAOQD.qPESSOAXFUNCAOXLOTACAOList;
            lQuery += string.Format(@" WHERE PFL.PESFLOT_REGSTATUS='A'
                         AND PF.PESF_STATUS='A'
                         AND P.PES_STATUS='A'
                         AND PFL.PESFLOT_TIPO IN ('A','E')
                         AND (PFL.NUC_ID = {0} OR L.NUC_ID = {0})
                         AND PF.FUNC_ID IN ({1})
                         ORDER BY P.PES_NOME", pNUC_ID, pListFUNC_ID);

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetAllPessoaxFuncaoByLocal
        (
            decimal pNUC_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PESSOAXFUNCAOXLOTACAOQD.qPESSOAXFUNCAOXLOTACAONome;
            lQuery += string.Format(@" WHERE PFL.PESFLOT_REGSTATUS='A'
                         AND PF.PESF_STATUS='A'
                         AND P.PES_STATUS='A'
                         AND (PFL.NUC_ID = {0} OR L.NUC_ID = {0})
                         ORDER BY P.PES_NOME", pNUC_ID);

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetAllPessoaxFuncaoByLocalFerias
        (
            decimal pNUC_ID,
            string pArea,
            string pPESF_FERIAS,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PESSOAXFUNCAOXLOTACAOQD.qPESSOAXFUNCAOXLOTACAONome;
            lQuery += string.Format(@" WHERE PFL.PESFLOT_REGSTATUS='A'
                         AND PF.PESF_STATUS='A'
                         AND P.PES_STATUS='A'
                         AND PFL.PESFLOT_TIPO='D'
                         AND (PFL.NUC_ID = {0} OR L.NUC_ID = {0}) ", pNUC_ID);

            if (pPESF_FERIAS != "")
                lQuery += string.Format(" AND PF.PESF_FERIAS = '{0}' ", pPESF_FERIAS);
            else
                lQuery += " AND PF.PESF_FERIAS IS NULL ";

            if (pArea != "")
                lQuery += string.Format(" AND L.LOT_DESCRICAO LIKE '%{0}%'", pArea);

            lQuery += " ORDER BY P.PES_NOME";

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetDefensorQuantidadeByLocalData
        (
            decimal pNUC_ID,
            string pData,
            string pArea,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = string.Format(PESSOAXFUNCAOXLOTACAOQD.qPESSOAXFUNCAOXLOTACAOQuantidadeDistribuicao, pData);
            lQuery += string.Format(" AND L.NUC_ID = {0}", pNUC_ID);

            lQuery += " AND PF.PESF_FERIAS IS NULL ";

            if (pArea != "")
                lQuery += string.Format(" AND L.LOT_DESCRICAO LIKE '%{0}%'", pArea);

            lQuery += " ORDER BY P.PES_NOME ASC";

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetListaDefensorAlfabeticaByLocal
        (
            decimal pNUC_ID,
            string pArea,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PESSOAXFUNCAOXLOTACAOQD.qPESSOAXFUNCAOXLOTACAOLista;
            lQuery += string.Format(" AND L.NUC_ID = {0}", pNUC_ID);

            if (pArea != "")
                lQuery += string.Format(" AND L.LOT_DESCRICAO LIKE '%{0}%'", pArea);

            lQuery += " ORDER BY P.PES_NOME ASC";

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetDefensorByLocal
        (
            decimal pNUC_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PESSOAXFUNCAOXLOTACAOQD.qPESSOAXFUNCAOXLOTACAONome;
            lQuery += string.Format(@" WHERE PFL.PESFLOT_REGSTATUS='A'
                         AND PF.PESF_STATUS='A'
                         AND P.PES_STATUS='A'
                         AND (PFL.NUC_ID = {0} OR L.NUC_ID = {0})
                         AND PF.FUNC_ID = 1
                         ORDER BY P.PES_NOME", pNUC_ID);

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetAllPESSOAFUNCAO
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PESSOAXFUNCAOXLOTACAOQD.qPESSOAFUNCAO;
            lQuery += " WHERE PES_STATUS='A'";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }
        public static DataTable GetAlllotacao
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PESSOAXFUNCAOXLOTACAOQD.qlotacao;
            lQuery += " WHERE lot_STATUS='A'";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetAllDefensoriaDefensor(ConnectionInfo pInfo, string pLotId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = "  SELECT PFL.*,PF.*,L.*,P.*,V.*,'DEFENSOR - ' ||P.PES_NOME || ' VINCULO: - ' ||V.VINC_DESCRICAO AS DEFENSOR  FROM PESSOAXFUNCAOXLOTACAO PFL  " +
                      " INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID  " +
                      " INNER JOIN LOTACAO L ON L.LOT_ID = PFL.LOT_ID " +
                      " INNER JOIN PESSOA P ON P.PES_ID  = PF.PES_ID " +
                     "  INNER JOIN VINCULO V ON V.VINC_ID = PFL.VINC_ID ";
            lQuery += "  WHERE 1=1" +
                       " AND L.LOT_ID =" + pLotId;
            lQuery += "  AND L.LOT_STATUS='A' ";
            lQuery += "  AND PF.PESF_STATUS='A' ";
            lQuery += "  AND PESFLOT_REGSTATUS='A' ";
            lQuery += "  AND PFL.PESFLOT_TIPO = 'D' ";


            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetAllDefensoriaDefensoriD(ConnectionInfo pInfo, string pLotId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery =
                "  SELECT DISTINCT PFL.PESF_ID,P.PES_NOME  FROM PESSOAXFUNCAOXLOTACAO PFL  " +
                " INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID  " +
                " INNER JOIN LOTACAO L ON L.LOT_ID = PFL.LOT_ID " +
                " INNER JOIN PESSOA P ON P.PES_ID  = PF.PES_ID ";
            lQuery += "  WHERE 1=1" +
                       " AND L.LOT_ID =" + pLotId;
            lQuery += "  AND L.LOT_STATUS='A' ";
            lQuery += "  AND PF.PESF_STATUS='A' ";
            lQuery += "  AND PESFLOT_REGSTATUS='A' ";
            lQuery += "  AND PFL.PESFLOT_TIPO = 'D' ";


            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }



        public static DataTable GetAllDefensoriaDefensoriDRecupera(ConnectionInfo pInfo, string pPesflotId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery =
                "  SELECT DISTINCT PFL.PESF_ID,P.PES_NOME  FROM PESSOAXFUNCAOXLOTACAO PFL  " +
                " INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID  " +
                " INNER JOIN LOTACAO L ON L.LOT_ID = PFL.LOT_ID " +
                " INNER JOIN PESSOA P ON P.PES_ID  = PF.PES_ID ";
            lQuery += "  WHERE 1=1" +
                       " AND PFL.PESF_ID =" + pPesflotId;
            lQuery += "  AND L.LOT_STATUS='A' ";
            lQuery += "  AND PF.PESF_STATUS='A' ";
            lQuery += "  AND PESFLOT_REGSTATUS='A' ";
            lQuery += "  AND PFL.PESFLOT_TIPO = 'D' ";


            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetAllDefensoriaDefensor(ConnectionInfo pInfo, string pLotId, string pPesfId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = "  SELECT * FROM PESSOAXFUNCAOXLOTACAO PFL  " +
                      " INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID  " +
                      " INNER JOIN LOTACAO L ON L.LOT_ID = PFL.LOT_ID " +
                      " INNER JOIN PESSOA P ON P.PES_ID  = PF.PES_ID " +
                     "  LEFT JOIN VINCULO V ON V.VINC_ID = PFL.VINC_ID ";
            lQuery += "  WHERE " +
                       " L.LOT_ID =" + pLotId;
            lQuery += "  AND L.LOT_STATUS='A' ";
            lQuery += "  AND PF.PESF_STATUS='A' ";
            lQuery += "  AND PESFLOT_REGSTATUS='A' ";
            lQuery += "  AND PFL.PESFLOT_TIPO = 'D' ";
            lQuery += "  AND PF.PESF_ID = " + pPesfId;


            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetAllDefensoriaDefensorQuantidade(ConnectionInfo pInfo, string pLotId, string pAratuaId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery =
                @"  SELECT  AGD.AGAT_ID, PF.PESF_ID,L.LOT_ID,L.LOT_DESCRICAO,L.LOT_STATUSDEFENSORIA,P.PES_ID,P.PES_NOME,(COUNT(AGD.AGAT_ID)) AS QTD_TOTALATENDIMENTO,
                    (TO_NUMBER(E.ESC_QTDVAGAS) - (COUNT(AGD.AGAT_ID))) AS QTD_RESTANTE 
                    FROM PESSOAXFUNCAOXLOTACAO PFL  
                    INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID   
                    INNER JOIN LOTACAO L ON L.LOT_ID = PFL.LOT_ID 
                    INNER JOIN PESSOA P ON P.PES_ID  = PF.PES_ID
                    INNER JOIN ESCALA E ON E.PESF_ID = PFL.PESF_ID   
                    INNER JOIN AGENDAMENTOATENDIMENTO AGD ON AGD.ARATUA_ID = E.ARATUA_ID
                    WHERE 1=1 
                    AND L.LOT_ID = " + pLotId;
            lQuery += "  AND E.ARATUA_ID = " + pAratuaId;
            lQuery += @" AND L.LOT_STATUS='A'  
                     AND PF.PESF_STATUS='A' 
                     AND PESFLOT_REGSTATUS='A'
                     AND AGD.AGAT_DATAAGENDADA = TO_CHAR(SYSDATE)  
                     AND TO_CHAR(AGD.AGAT_DATAAGENDADA )= TO_CHAR( E.ESC_DIA)
                     GROUP BY AGD.AGAT_ID, PF.PESF_ID,L.LOT_ID,L.LOT_DESCRICAO,L.LOT_STATUSDEFENSORIA,P.PES_ID,P.PES_NOME,E.ESC_QTDVAGAS ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetDefensoriaDefensorByNUC_ID
        (
            decimal pNUC_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PESSOAXFUNCAOXLOTACAOQD.qPessoaFuncaoLotacaoNucleoDefensor;
            lQuery += string.Format("  AND N.NUC_ID = {0}", pNUC_ID);
            lQuery += " AND PF.PRF_ID = 4";
            lQuery += " ORDER BY P.PES_NOME";


            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetDefensoriaDefensorByPESF_ID
        (
            decimal pPESF_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PESSOAXFUNCAOXLOTACAOQD.qPessoaFuncaoLotacaoNucleoDefensoriaDefensor;
            lQuery += string.Format("  AND PF.PESF_ID = {0}", pPESF_ID);


            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetDefensoriaAtendenteByPESF_ID
        (
            decimal pPESF_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PESSOAXFUNCAOXLOTACAOQD.qPessoaFuncaoNucleoAtendente;
            lQuery += string.Format("  AND PF.PESF_ID = {0}", pPESF_ID);


            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetCidadeLocalAtendenteByPESF_ID
        (
            decimal pPESF_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PESSOAXFUNCAOXLOTACAOQD.qPessoaFuncaoNucleoCidadeAtendente;
            lQuery += string.Format(" AND PFL.PESF_ID = {0} ORDER BY COMARCALOCAL ", pPESF_ID);

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        #endregion
    }
}
