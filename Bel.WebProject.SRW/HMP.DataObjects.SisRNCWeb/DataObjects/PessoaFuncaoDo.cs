using System;

using System.Collections.Generic;
using System.Data;
using System.Xml;

using RPA.DataBase;// APB.Framework.DataBase;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

namespace HMP.DataObjects.SisRNCWeb
{
    [Serializable]
    public class PessoaFuncaoDo
    {

        #region Private Methods

        private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateConversion(pValues, pResult);
        }


        private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateRequired(PessoaFuncaoQD._PESF_ID, pValues, pResult);
            GenericDataObject.ValidateRequired(PessoaFuncaoQD._PESF_REGDATE, pValues, pResult);
            GenericDataObject.ValidateRequired(PessoaFuncaoQD._PESF_REGUSER, pValues, pResult);
            GenericDataObject.ValidateRequired(PessoaFuncaoQD._PESF_STATUS, pValues, pResult);
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

            OperationResult lReturn = new OperationResult(PessoaFuncaoQD.TableName, PessoaFuncaoQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lInsert = new InsertCommand(PessoaFuncaoQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de insert");

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }
                    decimal lSequence;
                    lSequence = DataBaseSequenceControl.GetNext(pInfo, "PESF_ID");
                    lInsert.Fields.Add(PessoaFuncaoQD._PESF_ID.Name, lSequence,
                                       (ItemType)PessoaFuncaoQD._PESF_ID.DBType);

                    lReturn.SequenceControl = lSequence;

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


        public static OperationResult Insert
            (
            DataFieldCollection pValuesPESF,
            List<DataFieldCollection> pListValuesATUA,
            DataFieldCollection pValuesSUSR,
            List<DataFieldCollection> pListValuesPERMISSAO,
            ConnectionInfo pInfo
            )
        {
            Transaction lTransaction;

            lTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (lTransaction != null);

            InsertCommand lInsert;

            OperationResult lReturn = new OperationResult(PessoaFuncaoQD.TableName, PessoaFuncaoQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {
                    lInsert = new InsertCommand(PessoaFuncaoQD.TableName);

                    foreach (DataField lField in pValuesPESF.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValuesPESF[lField], (ItemType)lField.DBType);
                    }
                    decimal lSequencePESF_ID;
                    lSequencePESF_ID = DataBaseSequenceControl.GetNext(pInfo, "PESF_ID");
                    lInsert.Fields.Add(PessoaFuncaoQD._PESF_ID.Name, lSequencePESF_ID,
                                       (ItemType)PessoaFuncaoQD._PESF_ID.DBType);

                    


                    lInsert.Execute(lTransaction);


                    if (!lReturn.HasError)
                    {

                        

                        //pValuesSUSR.Add(SystemUserQD._PESF_ID, lSequencePESF_ID);
                        lReturn = SystemUserDo.Insert(pValuesSUSR, lTransaction, pInfo);

                        if (lReturn.HasError)
                        {
                            lTransaction.Rollback();
                            return lReturn;
                        }

                        if (pListValuesPERMISSAO.Count > 0)
                        {
                            foreach (DataFieldCollection lFields in pListValuesPERMISSAO)
                            {
                                lFields.Add(SecurityUsersDtQD._SU_ID, lSequencePESF_ID);

                                lReturn = SecurityUsersDtDo.Insert(lFields, lTransaction, pInfo);

                                if (lReturn.HasError)
                                {
                                    lTransaction.Rollback();
                                    return lReturn;
                                }
                            }
                        }

                        if (!lReturn.HasError)
                        {
                            lReturn.SequenceControl = lSequencePESF_ID;
                            lTransaction.Commit();
                        }
                        else
                        {
                            lTransaction.Rollback();
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

            OperationResult lReturn = new OperationResult(PessoaFuncaoQD.TableName, PessoaFuncaoQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lUpdate = new UpdateCommand(PessoaFuncaoQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de update");
                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != PessoaFuncaoQD._PESF_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", PessoaFuncaoQD._PESF_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(PessoaFuncaoQD._PESF_ID.Name, pValues[PessoaFuncaoQD._PESF_ID].DBToDecimal());

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

        public static OperationResult Update
            (
            DataFieldCollection pValues,
            List<DataFieldCollection> pListAtuaInc,
            List<DataFieldCollection> pListAtuaUpd,
            DataFieldCollection pValuesAtuaExcluir,
            ConnectionInfo pInfo
            )
        {

            Transaction pTransaction;

            pTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (pTransaction != null);

            UpdateCommand lUpdate;

            OperationResult lReturn = new OperationResult(PessoaFuncaoQD.TableName, PessoaFuncaoQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    lUpdate = new UpdateCommand(PessoaFuncaoQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != PessoaFuncaoQD._PESF_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", PessoaFuncaoQD._PESF_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(PessoaFuncaoQD._PESF_ID.Name, pValues[PessoaFuncaoQD._PESF_ID].DBToDecimal());

                    lUpdate.Execute(pTransaction);

                    if (!lReturn.HasError)
                    {

                        if (!lReturn.HasError)
                        {
                            pTransaction.Commit();
                        }
                        else
                        {
                            pTransaction.Rollback();
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

        public static OperationResult Update
            (
            DataFieldCollection pValues,
            List<DataFieldCollection> pListAtuaInc,
            List<DataFieldCollection> pListAtuaUpd,
            DataFieldCollection pValuesAtuaExcluir,
            List<DataFieldCollection> pListPermissao,
            ConnectionInfo pInfo
            )
        {

            Transaction pTransaction;

            pTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (pTransaction != null);

            UpdateCommand lUpdate;

            OperationResult lReturn = new OperationResult(PessoaFuncaoQD.TableName, PessoaFuncaoQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    lUpdate = new UpdateCommand(PessoaFuncaoQD.TableName);

                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != PessoaFuncaoQD._PESF_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", PessoaFuncaoQD._PESF_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(PessoaFuncaoQD._PESF_ID.Name, pValues[PessoaFuncaoQD._PESF_ID].DBToDecimal());

                    lUpdate.Execute(pTransaction);

                    if (!lReturn.HasError)
                    {

                        if (pListPermissao.Count > 0)
                        {
                            foreach (DataFieldCollection lFields in pListPermissao)
                            {
                                lReturn = SecurityUsersDtDo.Insert(lFields, pTransaction, pInfo);

                                if (lReturn.HasError)
                                {
                                    pTransaction.Rollback();
                                    return lReturn;
                                }
                            }
                        }

                        if (!lReturn.HasError)
                        {
                            pTransaction.Commit();
                        }
                        else
                        {
                            pTransaction.Rollback();
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


        public static DataTable GetAllPessoaFuncao
            (
            ConnectionInfo pInfo
            )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaFuncaoQD.qPessoaFuncaoPessoa;
            lQuery += " ORDER BY PES.PES_NOME";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetAllPessoaFuncaoByPOLICAD
        (
            decimal pPLC_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaFuncaoQD.qPessoaFuncaoPessoa;
            lQuery += string.Format(" AND PESF.PLC_ID = {0}", pPLC_ID);
            lQuery += " ORDER BY PESF.PESF_NOME";

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetAllPOLICAD
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaFuncaoQD.qPolicadList;
            lQuery += " ORDER BY PLC_NOME";

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetAllPessoaFuncaoSemAtuacao
            (
            ConnectionInfo pInfo
            )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaFuncaoQD.qPessoaFuncaoPessoa;
            lQuery +=
                " AND NOT EXISTS(SELECT 0 FROM ATUACAO ATUA WHERE ATUA.PESF_ID = PESF.PESF_ID AND ATUA.ATUA_STATUS = 'A')";
            lQuery += " ORDER BY PES.PES_NOME";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetPessoaFuncaoPessoaByCondicao
            (
            string lCondicao,
            ConnectionInfo pInfo
            )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaFuncaoQD.qPessoaFuncaoPessoa;
            lQuery += lCondicao;
            lQuery += " ORDER BY PES.PES_NOME";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }



        public static DataTable GetPessoaFuncaoByCondicao
            (
            string lCondicao,
            ConnectionInfo pInfo
            )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaFuncaoQD.qAllPessoaFuncao;
            lQuery += lCondicao;
            lQuery += " ORDER BY PES.PES_NOME";

            //MySqlDo lMySqlDo = new MySqlDo();
            //lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetPessoaFuncaoServidorByCondicao
            (
            string lCondicao,
            ConnectionInfo pInfo
            )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaFuncaoQD.qPessoaFuncaoServidores;
            lQuery += lCondicao;
            lQuery += " ORDER BY PES.PES_NOME";

            //MySqlDo lMySqlDo = new MySqlDo();
            //lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetAllFuncao
            (
            ConnectionInfo pInfo
            )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaFuncaoQD.qFuncao;
            lQuery += " WHERE FUNC_STATUS='A' ORDER BY FUNC_DESC";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetAllFuncaoPerfil
            (
            ConnectionInfo pInfo, string pPesfId
            )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery +=
                @"    SELECT * FROM PESSOAFUNCAO PF 
                            INNER JOIN PERFIL PER ON PER.PRF_ID = PF.PRF_ID
                            WHERE 1=1
                            AND PF.PESF_STATUS='A'
                            AND PF.PESF_ID = " +
                pPesfId;

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }



        public static DataTable GetAllFuncaoPerfilEstagiario(ConnectionInfo pInfo, string pPesfId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery +=
                @"    SELECT * FROM PESSOAFUNCAO PF
                            INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID
                            INNER JOIN PERFIL PER ON PER.PRF_ID = PF.PRF_ID
                            WHERE 1=1
                            AND P.PES_STATUS='A'
                            AND PF.PESF_STATUS='A'
                            AND PF.PESF_ID = " +
                pPesfId;
            lQuery += "     AND PER.PRF_DESCRICAO IN ('ESTAGIÁRIO')";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetAllPerfil
            (
            ConnectionInfo pInfo
            )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaFuncaoQD.qPerfil;

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static decimal GetCountPES_ID
            (
            decimal pes_id,
            ConnectionInfo pInfo
            )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();
            decimal qtdPesf_id;
            lQuery = PessoaFuncaoQD.qPessoaFuncaoList;
            lQuery += " Where PES_ID =" + pes_id;
            lQuery += " and pesf_status = 'A'";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            qtdPesf_id = lTable.Rows.Count;
            // teste
            return qtdPesf_id;
        }



        public static DataTable GetPessoaFuncaoBuscaPerfil(ConnectionInfo pInfo)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery =
                @" SELECT P.PES_NOME ||' --> '||PER.PRF_DESCRICAO AS PESSOAXCARGO,PF.PESF_ID FROM PESSOAFUNCAO PF
                        INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID
                        INNER JOIN PERFIL PER ON PER.PRF_ID = PF.PRF_ID
                        WHERE 1=1
                        AND PF.PESF_STATUS='A'
                        AND P.PES_STATUS='A'
                        AND PER.PRF_DESCRICAO IN ('COORDENADOR','CORREGEDORIA','DEFENSOR','ASSISTENTE PSICO-SOCIAL')
                        ORDER BY P.PES_NOME ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetAllPessoaFuncaoPesId(ConnectionInfo pInfo, string pPesId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery += "SELECT * FROM PESSOAFUNCAO PF " +
                      " INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID ";
            lQuery += "WHERE 1=1 " +
                      " AND P.PES_STATUS='A' " +
                      " AND PF.PESF_STATUS='A' ";
            lQuery += "AND P.PES_ID = " + pPesId;

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetAllPessoaFuncaoPerfil
        (
            string pPESF_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery += PessoaFuncaoQD.qPessoaFuncaoPessoaPerfil;
            if (pPESF_ID != "")
                lQuery += string.Format(" AND PF.PESF_ID = {0} ", pPESF_ID);

            lQuery += " ORDER BY PF.PESF_NOME ";

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetPessoaFuncaoByPerfil
        (
            string pPRF_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = string.Format(PessoaFuncaoQD.qPessoaFuncaoPerfil, pPRF_ID);

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetPessoaFuncaoByPerfilEPesfId
      (
          decimal pPRF_ID,
          string pPesfId,
          ConnectionInfo pInfo
      )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = string.Format(PessoaFuncaoQD.qPessoaFuncaoPerfilPesfId, pPRF_ID, pPesfId);

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetPessoaLocalDefensoriaEVinculo(ConnectionInfo pInfo, string pPesfId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = @" 
                        SELECT PFL.PESFLOT_ID,PFL.LOT_ID,PFL.VINC_ID,PFL.NUC_ID,
                        V.VINC_DESCRICAO,L.LOT_DESCRICAO,L.NUC_ID,PF.PESF_ID,PER.PRF_ID,PER.PRF_DESCRICAO,N.NUC_DESCRICAO,AR.ARATUA_ID,AR.ARATUA_DESCRICAO,P.PES_NOME,AR.ARATUA_ID,
                       'VINCULO:'|| V.VINC_DESCRICAO ||'-> ATRIBUIÇÃO :'||AR.ARATUA_DESCRICAO AS VINCULO, 
                       'UNIDADE: '||N.NUC_DESCRICAO || ' VINCULO:'|| V.VINC_DESCRICAO ||'-> ATRIBUIÇÃO :'||AR.ARATUA_DESCRICAO||' -> DEFENSORIA:'||L.LOT_DESCRICAO AS UNIDADE
                       FROM PESSOAXFUNCAOXLOTACAO PFL
                       LEFT JOIN VINCULO V ON V.VINC_ID = PFL.VINC_ID
                       INNER JOIN LOTACAO L ON PFL.LOT_ID = L.LOT_ID
                       INNER JOIN AREAATUACAO AR ON AR.LOT_ID = L.LOT_ID
                       INNER JOIN NUCLEO N ON N.NUC_ID = L.NUC_ID
                       INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID
                       INNER JOIN PERFIL PER ON PER.PRF_ID = PF.PRF_ID
                       INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID
                       WHERE 1=1
                       AND N.NUC_STATUS='A'
                       AND L.LOT_STATUS='A'
                       AND PFL.PESFLOT_REGSTATUS='A'
                       AND PFL.PESF_ID =  " + pPesfId;

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetPessoaLocalDefensoriaEVinculoAratuaId(ConnectionInfo pInfo, string pPesfId, string pAratuaId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery =
                @"  SELECT PFL.PESFLOT_ID,PFL.LOT_ID,PFL.VINC_ID,PFL.NUC_ID,
                         V.VINC_DESCRICAO,L.LOT_DESCRICAO,L.NUC_ID,PF.PESF_ID,PER.PRF_ID,PER.PRF_DESCRICAO,N.NUC_DESCRICAO,AR.ARATUA_ID,AR.ARATUA_DESCRICAO,P.PES_NOME,AR.ARATUA_ID,
                        'VINCULO:'|| V.VINC_DESCRICAO ||'-> ATRIBUIÇÃO :'||AR.ARATUA_DESCRICAO AS VINCULO, 
                        'UNIDADE: '||N.NUC_DESCRICAO || ' -> DEFENSORIA:'||L.LOT_DESCRICAO AS UNIDADE
                        FROM PESSOAXFUNCAOXLOTACAO PFL
                        INNER JOIN VINCULO V ON V.VINC_ID = PFL.VINC_ID
                        INNER JOIN LOTACAO L ON PFL.LOT_ID = L.LOT_ID
                        INNER JOIN AREAATUACAO AR ON AR.LOT_ID = L.LOT_ID
                        INNER JOIN NUCLEO N ON N.NUC_ID = L.NUC_ID
                        INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID
                        INNER JOIN PERFIL PER ON PER.PRF_ID = PF.PRF_ID
                        INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID
                        WHERE 1=1
                        AND N.NUC_STATUS='A'
                        AND L.LOT_STATUS='A'
                        AND PFL.PESFLOT_REGSTATUS='A'
                        AND PFL.PESF_ID =  " + pPesfId;

            if (pAratuaId != "")
            {

                lQuery += " AND AR.ARATUA_ID = " + pAratuaId;
            }

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetPessoaLocalDefensoriaEVinculoPesfLotId(ConnectionInfo pInfo, string pPesfLotId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery =
                @"  SELECT PFL.PESFLOT_ID,PFL.LOT_ID,PFL.VINC_ID,PFL.NUC_ID,
                         V.VINC_DESCRICAO,L.LOT_DESCRICAO,L.NUC_ID,PF.PESF_ID,PER.PRF_ID,PER.PRF_DESCRICAO,N.NUC_DESCRICAO,AR.ARATUA_ID,AR.ARATUA_DESCRICAO,P.PES_NOME,AR.ARATUA_ID,
                        'VINCULO:'|| V.VINC_DESCRICAO ||'-> ATRIBUIÇÃO :'||AR.ARATUA_DESCRICAO AS VINCULO, 
                        'UNIDADE: '||N.NUC_DESCRICAO || ' -> DEFENSORIA:'||L.LOT_DESCRICAO AS UNIDADE
                        FROM PESSOAXFUNCAOXLOTACAO PFL
                        INNER JOIN VINCULO V ON V.VINC_ID = PFL.VINC_ID
                        INNER JOIN LOTACAO L ON PFL.LOT_ID = L.LOT_ID
                        INNER JOIN AREAATUACAO AR ON AR.LOT_ID = L.LOT_ID
                        INNER JOIN NUCLEO N ON N.NUC_ID = L.NUC_ID
                        INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID
                        INNER JOIN PERFIL PER ON PER.PRF_ID = PF.PRF_ID
                        INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID
                        WHERE 1=1
                        AND N.NUC_STATUS='A'
                        AND L.LOT_STATUS='A'
                        AND PFL.PESFLOT_REGSTATUS='A'";

            if (pPesfLotId != "")
            {

                lQuery += " AND PFL.PESFLOT_ID = " + pPesfLotId;
            }


            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetLocalDefensorByPesfId
        (
            string pPESF_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery =
                @"  SELECT PFL.PESFLOT_ID,PFL.LOT_ID, L.LOT_DESCRICAO, L.NUC_ID, PF.PESF_ID, N.NUC_DESCRICAO, P.PES_NOME,
                        'UNIDADE: '||N.NUC_DESCRICAO || ' -> DEFENSORIA: '||L.LOT_DESCRICAO AS UNIDADE
                        FROM PESSOAXFUNCAOXLOTACAO PFL
                        LEFT JOIN LOTACAO L ON PFL.LOT_ID = L.LOT_ID
                        INNER JOIN NUCLEO N ON N.NUC_ID = L.NUC_ID
                        INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID
                        INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID
                        WHERE N.NUC_STATUS='A'
                        AND L.LOT_STATUS='A'
                        AND PFL.PESFLOT_REGSTATUS='A'
                        AND PFL.PESF_ID =  " + pPESF_ID;


            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetPessoaLocalDefensoriaEVinculoPesfLotId(ConnectionInfo pInfo, string pPesfId, string pPesfLotId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery =
                @"  SELECT PFL.PESFLOT_ID,PFL.LOT_ID,PFL.VINC_ID,PFL.NUC_ID,
                         V.VINC_DESCRICAO,L.LOT_DESCRICAO,L.NUC_ID,PF.PESF_ID,PER.PRF_ID,PER.PRF_DESCRICAO,N.NUC_DESCRICAO,AR.ARATUA_ID,AR.ARATUA_DESCRICAO,P.PES_NOME,AR.ARATUA_ID,
                        'VINCULO: '|| V.VINC_DESCRICAO ||'-> ATRIBUIÇÃO : '||AR.ARATUA_DESCRICAO AS VINCULO, 
                        'UNIDADE: '||N.NUC_DESCRICAO || ' -> DEFENSORIA: '||L.LOT_DESCRICAO AS UNIDADE
                        FROM PESSOAXFUNCAOXLOTACAO PFL
                        LEFT JOIN VINCULO V ON V.VINC_ID = PFL.VINC_ID
                        INNER JOIN LOTACAO L ON PFL.LOT_ID = L.LOT_ID
                        INNER JOIN AREAATUACAO AR ON AR.LOT_ID = L.LOT_ID
                        INNER JOIN NUCLEO N ON N.NUC_ID = L.NUC_ID
                        INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID
                        INNER JOIN PERFIL PER ON PER.PRF_ID = PF.PRF_ID
                        INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID
                        WHERE N.NUC_STATUS='A'
                        AND L.LOT_STATUS='A'
                        AND PFL.PESFLOT_REGSTATUS='A'
                        AND PFL.PESF_ID =  " + pPesfId;

            if (pPesfLotId != "")
            {

                lQuery += " AND PFL.PESFLOT_ID = " + pPesfLotId;
            }


            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetPessoaLocalDefensoriaEVinculoPesfLotIdGet(ConnectionInfo pInfo, string pPesfId, string pPesfLotId, string pLotId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery =
                @"  SELECT PFL.PESFLOT_ID,PFL.LOT_ID,PFL.VINC_ID,
                         V.VINC_DESCRICAO,L.LOT_DESCRICAO,L.NUC_ID,PF.PESF_ID,PER.PRF_ID,PER.PRF_DESCRICAO,N.NUC_DESCRICAO,AR.ARATUA_ID,AR.ARATUA_DESCRICAO,P.PES_NOME,AR.ARATUA_ID,
                        'VINCULO:'|| V.VINC_DESCRICAO ||'-> ATRIBUIÇÃO :'||AR.ARATUA_DESCRICAO AS VINCULO, 
                        'UNIDADE: '||N.NUC_DESCRICAO || ' -> DEFENSORIA:'||L.LOT_DESCRICAO AS UNIDADE
                        FROM PESSOAXFUNCAOXLOTACAO PFL
                        LEFT JOIN VINCULO V ON V.VINC_ID = PFL.VINC_ID
                        INNER JOIN LOTACAO L ON PFL.LOT_ID = L.LOT_ID
                        INNER JOIN AREAATUACAO AR ON AR.LOT_ID = L.LOT_ID
                        INNER JOIN NUCLEO N ON N.NUC_ID = L.NUC_ID
                        INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID
                        INNER JOIN PERFIL PER ON PER.PRF_ID = PF.PRF_ID
                        INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID
                        WHERE N.NUC_STATUS='A'
                        AND L.LOT_STATUS='A'
                        AND PFL.PESFLOT_REGSTATUS='A'
                        AND PFL.PESF_ID =  " + pPesfId;
            lQuery += " AND L.LOT_ID = " + pLotId;

            if (pPesfLotId != "")
            {

                lQuery += " AND PFL.PESFLOT_ID = " + pPesfLotId;
            }


            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetPessoaLocalDefensoriaEVinculoAratua(ConnectionInfo pInfo, string pPesfId, string pAratuaId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = @"  SELECT PFL.PESFLOT_ID,PFL.LOT_ID,PFL.VINC_ID,PFL.NUC_ID,
                         V.VINC_DESCRICAO,L.LOT_DESCRICAO,L.NUC_ID,PF.PESF_ID,PER.PRF_ID,PER.PRF_DESCRICAO,N.NUC_DESCRICAO,AR.ARATUA_ID,AR.ARATUA_DESCRICAO,P.PES_NOME,AR.ARATUA_ID,
                        'VINCULO:'|| V.VINC_DESCRICAO ||'-> ATRIBUIÇÃO :'||AR.ARATUA_DESCRICAO AS VINCULO, 
                        'UNIDADE: '||N.NUC_DESCRICAO || ' VINCULO:'|| V.VINC_DESCRICAO ||'-> ATRIBUIÇÃO :'||AR.ARATUA_DESCRICAO||' -> DEFENSORIA:'||L.LOT_DESCRICAO AS UNIDADE
                        FROM PESSOAXFUNCAOXLOTACAO PFL
                        INNER JOIN VINCULO V ON V.VINC_ID = PFL.VINC_ID
                        INNER JOIN LOTACAO L ON PFL.LOT_ID = L.LOT_ID
                        INNER JOIN AREAATUACAO AR ON AR.LOT_ID = L.LOT_ID
                        INNER JOIN NUCLEO N ON N.NUC_ID = L.NUC_ID
                        INNER JOIN PESSOAFUNCAO PF ON PF.PESF_ID = PFL.PESF_ID
                        INNER JOIN PERFIL PER ON PER.PRF_ID = PF.PRF_ID
                        INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID
                        WHERE 1=1
                        AND N.NUC_STATUS='A'
                        AND L.LOT_STATUS='A'
                        AND PFL.PESFLOT_REGSTATUS='A'
                        AND PFL.PESF_ID =  " + pPesfId;
            lQuery += " AND AR.ARATUA_ID = " + pAratuaId;

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetPessoaFuncaoBuscaPerfilDefensor(ConnectionInfo pInfo)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = @" SELECT P.PES_NOME ||' --> '||PER.PRF_DESCRICAO AS PESSOAXCARGO,PF.PESF_ID FROM PESSOAFUNCAO PF
                        INNER JOIN PESSOA P ON P.PES_ID = PF.PES_ID
                        INNER JOIN PERFIL PER ON PER.PRF_ID = PF.PRF_ID
                        WHERE PF.PESF_STATUS='A'
                        AND P.PES_STATUS='A'
                        AND PER.PRF_DESCRICAO IN ('DEFENSOR')
                        ORDER BY P.PES_NOME ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        // select referente aos defensores que tem a segunda instancia cadastrada para eles
        // Marcelo nascimento.
        public static DataTable GetPessoaFuncaoSegundaIntancia(ConnectionInfo pInfo)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = @"     SELECT PFL.PESFLOT_ID, PFL.PESFLOT_TIPO, PF.PESF_ID, P.PES_ID,PF.PESF_FERIAS, P.PES_NOME, L.LOT_ID, L.LOT_DESCRICAO, PER.PRF_ID, PER.PRF_DESCRICAO, N.NUC_ID, N.NUC_DESCRICAO,
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
                            LEFT OUTER JOIN AREAATUACAO ARATUA ON ARATUA.LOT_ID = L.LOT_ID
                            WHERE PFL.PESFLOT_REGSTATUS='A'
                            AND PF.PESF_STATUS='A'
                            AND P.PES_STATUS='A'
                            AND PFL.PESFLOT_TIPO IN ('D')
                            AND L.NUC_ID IN(" + 254 + ")";
            lQuery += "     AND L.LOT_STATUS='A' ORDER BY PES_NOME  ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetPessoaFuncaoSegundaIntanciaSemFerias(ConnectionInfo pInfo)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = @"     SELECT PFL.PESFLOT_ID, PFL.PESFLOT_TIPO, PF.PESF_ID, P.PES_ID,PF.PESF_FERIAS, P.PES_NOME, L.LOT_ID, L.LOT_DESCRICAO, PER.PRF_ID, PER.PRF_DESCRICAO, N.NUC_ID, N.NUC_DESCRICAO,
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
                            LEFT OUTER JOIN AREAATUACAO ARATUA ON ARATUA.LOT_ID = L.LOT_ID
                            WHERE PFL.PESFLOT_REGSTATUS='A'
                            AND PF.PESF_STATUS='A'
                            AND P.PES_STATUS='A'
                            AND PFL.PESFLOT_TIPO IN ('D')
                            AND L.NUC_ID IN(" + 152 + ") AND PF.PESF_FERIAS is null ";
            lQuery += "     AND L.LOT_STATUS='A' ORDER BY PES_NOME  ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetPessoaSemFerias(ConnectionInfo pInfo)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = @"     SELECT PFL.PESFLOT_ID, PFL.PESFLOT_TIPO, PF.PESF_ID, P.PES_ID,PF.PESF_FERIAS, P.PES_NOME, L.LOT_ID, L.LOT_DESCRICAO, PER.PRF_ID, PER.PRF_DESCRICAO, N.NUC_ID, N.NUC_DESCRICAO,
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
                            LEFT OUTER JOIN AREAATUACAO ARATUA ON ARATUA.LOT_ID = L.LOT_ID
                            WHERE PFL.PESFLOT_REGSTATUS='A'
                            AND PF.PESF_STATUS='A'
                            AND P.PES_STATUS='A'
                            AND PFL.PESFLOT_TIPO IN ('D')
                            AND PF.PESF_FERIAS is null ";
            lQuery += "     AND L.LOT_STATUS='A' ORDER BY PES_NOME  ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetPessoaFuncaoSegundaIntanciaFerias(ConnectionInfo pInfo)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = @"     SELECT PFL.PESFLOT_ID, PFL.PESFLOT_TIPO, PF.PESF_ID, P.PES_ID,PF.PESF_FERIAS, P.PES_NOME, L.LOT_ID, L.LOT_DESCRICAO, PER.PRF_ID, PER.PRF_DESCRICAO, N.NUC_ID, N.NUC_DESCRICAO,
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
                            LEFT OUTER JOIN AREAATUACAO ARATUA ON ARATUA.LOT_ID = L.LOT_ID
                            WHERE PFL.PESFLOT_REGSTATUS='A'
                            AND PF.PESF_STATUS='A'
                            AND P.PES_STATUS='A'
                            AND PFL.PESFLOT_TIPO IN ('D')
                            AND L.NUC_ID IN(" + 254 + ")  AND PF.PESF_FERIAS='F' ";
            lQuery += "     AND L.LOT_STATUS='A' ORDER BY PES_NOME  ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetPessoaFuncaoSegundaIntanciaFerias(ConnectionInfo pInfo, string pPesfId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery =
                @"     SELECT PFL.PESFLOT_ID, PFL.PESFLOT_TIPO, PF.PESF_ID, P.PES_ID,PF.PESF_FERIAS, P.PES_NOME, L.LOT_ID, L.LOT_DESCRICAO, PER.PRF_ID, PER.PRF_DESCRICAO, N.NUC_ID, N.NUC_DESCRICAO,
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
                            LEFT OUTER JOIN AREAATUACAO ARATUA ON ARATUA.LOT_ID = L.LOT_ID
                            WHERE PFL.PESFLOT_REGSTATUS='A'
                            AND PF.PESF_STATUS='A'
                            AND P.PES_STATUS='A'
                            AND PFL.PESFLOT_TIPO IN ('D') ";
            lQuery += "     AND PF.PESF_ID = " + pPesfId;
            lQuery += "     AND L.NUC_ID IN(" + 254 + ")  AND PF.PESF_FERIAS='F' ";
            lQuery += "     AND L.LOT_STATUS='A' ORDER BY PES_NOME  ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetPessoaFuncaoFerias(ConnectionInfo pInfo, string pPesfId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery =
                @"     SELECT PFL.PESFLOT_ID, PFL.PESFLOT_TIPO, PF.PESF_ID, P.PES_ID,PF.PESF_FERIAS, P.PES_NOME, L.LOT_ID, L.LOT_DESCRICAO, PER.PRF_ID, PER.PRF_DESCRICAO, N.NUC_ID, N.NUC_DESCRICAO,
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
                            LEFT OUTER JOIN AREAATUACAO ARATUA ON ARATUA.LOT_ID = L.LOT_ID
                            WHERE PFL.PESFLOT_REGSTATUS='A'
                            AND PF.PESF_STATUS='A'
                            AND P.PES_STATUS='A'
                            AND PFL.PESFLOT_TIPO IN ('D') ";
            lQuery += "     AND PF.PESF_ID = " + pPesfId;
            lQuery += "      AND PF.PESF_FERIAS='F' ";
            lQuery += "     AND L.LOT_STATUS='A' ORDER BY PES_NOME  ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetPessoaFerias(ConnectionInfo pInfo, string pPesfId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery =
                @"     SELECT PFL.PESFLOT_ID, PFL.PESFLOT_TIPO, PF.PESF_ID, P.PES_ID,PF.PESF_FERIAS, P.PES_NOME, L.LOT_ID, L.LOT_DESCRICAO, PER.PRF_ID, PER.PRF_DESCRICAO, N.NUC_ID, N.NUC_DESCRICAO,
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
                            LEFT OUTER JOIN AREAATUACAO ARATUA ON ARATUA.LOT_ID = L.LOT_ID
                            WHERE PFL.PESFLOT_REGSTATUS='A'
                            AND PF.PESF_STATUS='A'
                            AND P.PES_STATUS='A'
                            AND PFL.PESFLOT_TIPO IN ('D') ";
            lQuery += "     AND PF.PESF_ID = " + pPesfId;
            lQuery += "     AND PF.PESF_FERIAS='F' ";
            lQuery += "     AND L.LOT_STATUS='A' ORDER BY PES_NOME  ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetPessoaFerias(ConnectionInfo pInfo)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery =
                @"     SELECT PFL.PESFLOT_ID, PFL.PESFLOT_TIPO, PF.PESF_ID, P.PES_ID,PF.PESF_FERIAS, P.PES_NOME, L.LOT_ID, L.LOT_DESCRICAO, PER.PRF_ID, PER.PRF_DESCRICAO, N.NUC_ID, N.NUC_DESCRICAO,
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
                            LEFT OUTER JOIN AREAATUACAO ARATUA ON ARATUA.LOT_ID = L.LOT_ID
                            WHERE PFL.PESFLOT_REGSTATUS='A'
                            AND PF.PESF_STATUS='A'
                            AND P.PES_STATUS='A'
                            AND PFL.PESFLOT_TIPO IN ('D') ";
            lQuery += "     AND PF.PESF_FERIAS='F' ";
            lQuery += "     AND L.LOT_STATUS='A' ORDER BY PES_NOME  ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetPessoaFuncaoSegundaIntanciaSemFerias(ConnectionInfo pInfo, string pPesfId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery =
                @"     SELECT PFL.PESFLOT_ID, PFL.PESFLOT_TIPO, PF.PESF_ID, P.PES_ID,PF.PESF_FERIAS, P.PES_NOME, L.LOT_ID, L.LOT_DESCRICAO, PER.PRF_ID, PER.PRF_DESCRICAO, N.NUC_ID, N.NUC_DESCRICAO,
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
                            LEFT OUTER JOIN AREAATUACAO ARATUA ON ARATUA.LOT_ID = L.LOT_ID
                            WHERE PFL.PESFLOT_REGSTATUS='A'
                            AND PF.PESF_STATUS='A'
                            AND P.PES_STATUS='A'
                            AND PFL.PESFLOT_TIPO IN ('D') ";
            lQuery += "     AND PF.PESF_ID = " + pPesfId;
            lQuery += "     AND L.NUC_ID IN(" + 254 + ")  AND PF.PESF_FERIAS IS NULL ";
            lQuery += "     AND L.LOT_STATUS='A' ORDER BY PES_NOME  ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetPessoaFuncaoAllDefensores(ConnectionInfo pInfo)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaFuncaoQD.qPessoaFuncaoAllDefensores;

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        #endregion
    }
}
