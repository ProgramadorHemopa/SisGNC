using System;

using System.Collections.Generic;
using System.Data;
using System.Xml;

using RPA.DataBase;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

namespace HMP.DataObjects.SisRNCWeb
{
    [Serializable]
    public class PessoaDo
    {

        #region Private Methods

        private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateConversion(pValues, pResult);
        }


        private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult)
        {
            GenericDataObject.ValidateRequired(PessoaQD._PES_ID, pValues, pResult);
            //GenericDataObject.ValidateRequired(PessoaQD._PES_REGDATE, pValues, pResult);
            //GenericDataObject.ValidateRequired(PessoaQD._PES_REGUSER, pValues, pResult);
            GenericDataObject.ValidateRequired(PessoaQD._PES_STATUS, pValues, pResult);
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

            OperationResult lReturn = new OperationResult(PessoaQD.TableName, PessoaQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lInsert = new InsertCommand(PessoaQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de insert");

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }
                    decimal lSequence;
                    lSequence = DataBaseSequenceControl.GetNext(pInfo, "PES_ID");
                    lInsert.Fields.Add(PessoaQD._PES_ID.Name, lSequence, (ItemType)PessoaQD._PES_ID.DBType);

                    lReturn.Trace("Executando o Insert");

                    lReturn.SequenceControl = lSequence;

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
           DataFieldCollection pValuesPessoa,
           DataFieldCollection pValuesEndereco,
           DataFieldCollection pValuesPessoaEndereco,
           DataFieldCollection pValuesRG,
           DataFieldCollection pValuesCPF,
           DataFieldCollection pValuesCNPJ,
           ConnectionInfo pInfo
        )
        {
            Transaction lTransaction;

            lTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (lTransaction != null);

            InsertCommand lInsert;

            OperationResult lReturn = new OperationResult(PessoaQD.TableName, PessoaQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {
                    lInsert = new InsertCommand(PessoaQD.TableName);

                    foreach (DataField lField in pValuesPessoa.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValuesPessoa[lField], (ItemType)lField.DBType);
                    }
                    decimal lSequencePessoa;
                    lSequencePessoa = DataBaseSequenceControl.GetNext(pInfo, "PES_ID");
                    lInsert.Fields.Add(PessoaQD._PES_ID.Name, lSequencePessoa, (ItemType)PessoaQD._PES_ID.DBType);

                    lInsert.Execute(lTransaction);

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
             DataFieldCollection pValuesPessoa,
             DataFieldCollection pValuesPai,
             DataFieldCollection pValuesMae,
             DataFieldCollection pValuesRequerente,
             DataFieldCollection pValuesPFamilia,
             DataFieldCollection pValuesSituacaoEscolar,
             DataFieldCollection pValuesEnderecoPai,
             DataFieldCollection pValuesPessoaEnderecoPai,
             DataFieldCollection pValuesEnderecoMae,
             DataFieldCollection pValuesPessoaEnderecoMae,
             DataFieldCollection pValuesEnderecoRequerente,
             DataFieldCollection pValuesPessoaEnderecoRequerente,
             DataFieldCollection pValuesRG,
             DataFieldCollection pValuesCPF,
             DataFieldCollection pValuesRGPai,
             DataFieldCollection pValuesCPFPai,
             DataFieldCollection pValuesRGMae,
             DataFieldCollection pValuesCPFMae,
             DataFieldCollection pValuesRGRequerente,
             DataFieldCollection pValuesCPFRequerente,
             ConnectionInfo pInfo
        )
        {
            Transaction lTransaction;

            lTransaction = new Transaction(Instance.CreateDatabase(pInfo));

            bool lLocalTransaction = (lTransaction != null);

            InsertCommand lInsert;

            OperationResult lReturn = new OperationResult(PessoaQD.TableName, PessoaQD.TableName);

            if (!lReturn.HasError)
            {
                try
                {
                    lInsert = new InsertCommand(PessoaQD.TableName);

                    foreach (DataField lField in pValuesPessoa.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValuesPessoa[lField], (ItemType)lField.DBType);
                    }
                    decimal lSequencePessoa;
                    decimal lSequencePai = 0;
                    decimal lSequenceMae = 0;
                    decimal lSequenceRequerente = 0;
                    lSequencePessoa = DataBaseSequenceControl.GetNext(pInfo, "PES_ID");
                    lInsert.Fields.Add(PessoaQD._PES_ID.Name, lSequencePessoa, (ItemType)PessoaQD._PES_ID.DBType);

                    lInsert.Execute(lTransaction);

                    if (!lReturn.HasError)
                    {
                        if (pValuesRG.Count > 0)
                        {
                            pValuesRG.Add(PessoaDocumentoQD._PES_ID, lSequencePessoa);
                            lReturn = PessoaDocumentoDo.Insert(pValuesRG, lTransaction, pInfo);
                            if (lReturn.HasError)
                            {
                                lTransaction.Rollback();
                                return lReturn;
                            }
                        }

                        if (pValuesCPF.Count > 0)
                        {
                            pValuesCPF.Add(PessoaDocumentoQD._PES_ID, lSequencePessoa);
                            lReturn = PessoaDocumentoDo.Insert(pValuesCPF, lTransaction, pInfo);
                            if (lReturn.HasError)
                            {
                                lTransaction.Rollback();
                                return lReturn;
                            }
                        }

                        if (!lReturn.HasError)
                        {
                            lReturn.SequenceControl = lSequencePessoa;
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

            OperationResult lReturn = new OperationResult(PessoaQD.TableName, PessoaQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lUpdate = new UpdateCommand(PessoaQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de update");
                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != PessoaQD._PES_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", PessoaQD._PES_ID.Name);
                    lUpdate.Condition = lSql;
                    lUpdate.Conditions.Add(PessoaQD._PES_ID.Name, pValues[PessoaQD._PES_ID].DBToDecimal());

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

        public static DataTable GetAllPessoa
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaQD.qPessoaList;
            lQuery += " WHERE PES_STATUS NOT IN ('I','E')";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetAllBairro
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaQD.qPessoaListBairro;

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetPessoaByID
        (
            decimal pPES_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaQD.qPessoaList;
            lQuery += string.Format(" WHERE PES_ID = {0} AND PES_STATUS NOT IN ('I','E')", pPES_ID);

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetPessoaByPESID(decimal pPES_ID, ConnectionInfo pInfo)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaQD.qPessoaListPesquisaAssistido;
            lQuery += @"   WHERE 1=1
                           AND P.PES_STATUS='A' ";
            lQuery += "    AND P.PES_ID = " + pPES_ID;

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetPessoaByPESID2(decimal pPES_ID, ConnectionInfo pInfo)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaQD.qPessoaConsultaDetalhes;
            lQuery += "   WHERE PES_STATUS='A' ";
            lQuery += "    AND PES_ID = " + pPES_ID;

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetPessoaByNome
        (
            string pPES_NOME, 
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();
            Select.AlterSessionBinary(pInfo);

            lQuery = PessoaQD.qPessoaConsultaList;
            lQuery += "   WHERE PES_STATUS='A' ";
            lQuery += string.Format(" AND TRANSLATE(PES_NOME,'ÁÇÉÍÓÚÀÈÌÒÙÂÊÎÔÛÃÕËÜáçéíóúàèìòùâêîôûãõëü','ACEIOUAEIOUAEIOUAOEUaceiouaeiouaeiouaoeu') LIKE TRANSLATE('%{0}%','ÁÇÉÍÓÚÀÈÌÒÙÂÊÎÔÛÃÕËÜáçéíóúàèìòùâêîôûãõëü','ACEIOUAEIOUAEIOUAOEUaceiouaeiouaeiouaoeu') ORDER BY PES_NOME", pPES_NOME);
            
            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetPessoaByNomeMaeCpfGrupoGerente
        (
            string pPES_NOME,
            string pPES_MAE,
            string pPES_CPF,
            decimal pGRP_ID,
            decimal pGRT_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();
            Select.AlterSessionBinary(pInfo);

            lQuery = PessoaQD.qPessoaConsultaList;
            lQuery += "   WHERE PES_STATUS='A' ";
            if(pPES_NOME != "")
                lQuery += string.Format(" AND TRANSLATE(PES_NOME,'ÁÇÉÍÓÚÀÈÌÒÙÂÊÎÔÛÃÕËÜáçéíóúàèìòùâêîôûãõëü','ACEIOUAEIOUAEIOUAOEUaceiouaeiouaeiouaoeu') LIKE TRANSLATE('%{0}%','ÁÇÉÍÓÚÀÈÌÒÙÂÊÎÔÛÃÕËÜáçéíóúàèìòùâêîôûãõëü','ACEIOUAEIOUAEIOUAOEUaceiouaeiouaeiouaoeu') ORDER BY PES_NOME", pPES_NOME);

            if (pPES_MAE != "")
                lQuery += string.Format(" AND PES.PES_MAE = '{0}'", pPES_MAE);

            if (pPES_MAE != "")
                lQuery += string.Format(" AND PES.PES_MAE = '{0}'", pPES_MAE);

            if (pGRP_ID != 0)
                lQuery += string.Format(" AND PES.GRP_ID = {0}", pGRP_ID);

            if (pGRT_ID != 0)
                lQuery += string.Format(" AND PES.GRT_ID = {0}", pGRT_ID);

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetPessoaByPesMae(string pPesMae, ConnectionInfo pInfo)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();
            
            Select.AlterSessionBinary(pInfo);

            lQuery = PessoaQD.qPessoaConsultaList;
            lQuery += "   WHERE PES_STATUS='A' ";
            lQuery += string.Format(" AND PES_MAE LIKE '%{0}%' ORDER BY PES_NOME", pPesMae);

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetPessoaByPesCPF(string pPesCPF, ConnectionInfo pInfo)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaQD.qPessoaConsultaList;
            lQuery += "   , PESSOADOCUMENTO PDOC2 WHERE PDOC2.PES_ID = PES.PES_ID AND PDOC2.TPDOC_ID = 2 AND PDOC2.PDOC_STATUS = 'A' AND PES.PES_STATUS='A' ";
            lQuery += "    AND PDOC2.PDOC_NUMERODOCUMENTO = '" + pPesCPF + "'";

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetAllPessoaByCondicao
        (
            string pCondicao,
            ConnectionInfo pInfo
        )
        {
            Select.AlterSessionBinary(pInfo);

            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaQD.qPessoaList;
            lQuery += " WHERE PES_STATUS NOT IN ('I','E')";
            lQuery += pCondicao;
            lQuery += " ORDER BY PES_NOME";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetPessoaAtendimentoByCondicao
        (
            string pCondicao,
            ConnectionInfo pInfo
        )
        {
            Select.AlterSessionBinary(pInfo);

            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaQD.qPessoaAtendimentoList;
            lQuery += pCondicao;
            lQuery += " ORDER BY PES.PES_NOME";

            //08/11/2012 - Ricardo Almeida
            //MySqlDo lMySqlDo = new MySqlDo();
            //lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetPessoaByParametros
        (
            string pDataInicio,
            string pDataFim,
            string pDataAniversario,
            string pBairro,
            string pZona,
            string pReligiao,
            decimal pGrupo,
            decimal pGerente,
            ConnectionInfo pInfo
        )
        {
            Select.AlterSessionBinary(pInfo);

            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaQD.qCidadao;
            if (pDataInicio != "" && pDataFim != "")
                lQuery += string.Format(" AND EXISTS (SELECT 0 FROM RELATOXATENDIMENTO RELATD WHERE RELATD.PES_ID = PES.PES_ID AND RELATD.RELATD_STATUS = 'A' AND RELATD.RELATD_DATA BETWEEN '{0}' AND '{1}')", pDataInicio, pDataFim);
            else if(pDataInicio != "")
                lQuery += string.Format(" AND EXISTS (SELECT 0 FROM RELATOXATENDIMENTO RELATD WHERE RELATD.PES_ID = PES.PES_ID AND RELATD.RELATD_STATUS = 'A' AND RELATD.RELATD_DATA = '{0}')", pDataInicio);
            else if (pDataFim != "")
                lQuery += string.Format(" AND EXISTS (SELECT 0 FROM RELATOXATENDIMENTO RELATD WHERE RELATD.PES_ID = PES.PES_ID AND RELATD.RELATD_STATUS = 'A' AND RELATD.RELATD_DATA = '{0}')", pDataFim);

            if(pDataAniversario != "")
                lQuery += string.Format(" AND TO_CHAR(PES.PES_NASCIMENTO,'dd/MM') = '{0}' ", pDataAniversario);

            if(pBairro != "")
                lQuery += string.Format(" AND EXISTS (SELECT 0 FROM ENDERECO ENDE, PESSOAENDERECO PEND WHERE ENDE.EMDE_BAIRRO = '{0}' AND ENDE.ENDE_ID = PEND.ENDE_ID AND PEND.PES_ID = PES.PES_ID AND PEND.PEND_STATUS = 'A' AND ENDE.ENDE_STATUS = 'A') ", pBairro);

            if (pZona != "")
                lQuery += string.Format(" AND PES.PES_ZONA = '{0}' ", pZona);

            if (pReligiao != "")
                lQuery += string.Format(" AND PES.PES_RELIGIAO = '{0}' ", pReligiao);

            if (pGrupo != 0)
                lQuery += string.Format(" AND PES.GRP_ID = {0} ", pGrupo);

            if (pGerente != 0)
                lQuery += string.Format(" AND PES.GRT_ID = {0} ", pGerente);


            lQuery += " ORDER BY PES.PES_NOME";

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }



        public static DataTable GetPessoaByCondition
        (
            string pWhere,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaQD.qPessoaEndereco;
            lQuery += pWhere;

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetPessoaCpfByCondition
        (
            string pWhere,
            ConnectionInfo pInfo
        )
        {
            Select.AlterSessionBinary(pInfo);

            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaQD.qPessoaDocumentoEnderecoCpf;
            lQuery += pWhere;

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }
        public static DataTable GetAllUnidadeFederativa
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaQD.qUnidadeFederativa;
            lQuery += " WHERE UF_STATUS='A' ORDER BY UF_DESC";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetCidadeByUF
        (
            decimal pUF_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaQD.qCidade;
            lQuery += string.Format(" WHERE UF_ID = {0} AND CID_STATUS = 'A' ORDER BY CID_DESC ", pUF_ID);

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetBairroByCID
        (
            decimal pCID_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaQD.qBairro;
            lQuery += string.Format(" WHERE CID_ID = {0} AND BRR_STATUS = 'A' ORDER BY BRR_DESC ", pCID_ID);

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetAllTipoPessoa
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaQD.qTipoPessoa;

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetTipoPessoaDocumento
        (
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaQD.qTipoDocumentoPessoa;

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

       


        public static DataTable GetPesForAuto(ConnectionInfo pInfo, String pPes_Nome)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();
            //Select.AlterSessionBinary(pInfo);

            lQuery += " select pes_id, pes_nome , to_char('')  ";
            lQuery += " from pessoa p ";
            lQuery += " where pes_status = 'A' ";
            lQuery += " and TRANSLATE(pes_nome,'ÁÇÉÍÓÚÀÈÌÒÙÂÊÎÔÛÃÕËÜáçéíóúàèìòùâêîôûãõëü','ACEIOUAEIOUAEIOUAOEUaceiouaeiouaeiouaoeu') like TRANSLATE('%" + pPes_Nome + "%','ÁÇÉÍÓÚÀÈÌÒÙÂÊÎÔÛÃÕËÜáçéíóúàèìòùâêîôûãõëü','ACEIOUAEIOUAEIOUAOEUaceiouaeiouaeiouaoeu') And ROWNUM <50 ";
            lQuery += " order by p.pes_nome ASC";

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }



        public static DataTable GetPesForAutoId(ConnectionInfo pInfo, String pId)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery += " select pes_id, pes_nome , to_char('')  ";
            lQuery += " from pessoa p ";
            lQuery += " where pes_status = 'A' ";
            lQuery += " and pes_id =" + pId + " And ROWNUM <25 ";
            lQuery += " order by p.pes_nome ";

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        //public static DataTable GetPesForAuto( ConnectionInfo pInfo,String pPes_Nome)
        //{
        //    string lQuery = "";
        //    DataTable lTable = new DataTable();

        //    lQuery += " select pes_id, pes_nome, to_char(pes_nascimento) ";
        //    lQuery += " from pessoa p ";
        //    lQuery += " where pes_status = 'A' ";
        //    lQuery += " and pes_nome like '%" + pPes_Nome + "%' And ROWNUM <25 ";
        //    lQuery += " order by p.pes_nome ";

        //    MySqlDo lMySqlDo = new MySqlDo();
        //    lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

        //    return lTable;
        //}

        public static DataTable GetPesForAutoRg(ConnectionInfo pInfo, String pPesRg)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery += " select pes_id, PES_RG,' NOME: ' || PES_NOME ";
            lQuery += " from pessoa p ";
            lQuery += " where pes_status = 'A' ";
            lQuery += " and PES_RG like '%" + pPesRg + "%' And ROWNUM <25 ";
            lQuery += " order by p.pes_nome ";

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetPesForAutoCPF(ConnectionInfo pInfo, String pPesCpf)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery += " select pes_id, PES_CPF,' NOME: ' || PES_NOME ";
            lQuery += " from pessoa p ";
            lQuery += " where pes_status = 'A' ";
            lQuery += " and PES_CPF like '%" + pPesCpf + "%' And ROWNUM <25 ";
            lQuery += " order by p.pes_nome ";

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetParentePessoaByID
        (
            decimal pPES_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = string.Format(PessoaQD.qPessoaParente, pPES_ID);

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static DataTable GetDadosPessoaByID
        (
            decimal pPES_ID,
            ConnectionInfo pInfo
        )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = string.Format(PessoaQD.qDadosCompletoPessoa, pPES_ID);

            MySqlDo lOra = new MySqlDo();
            lTable = lOra.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        public static DataTable GetCidadeByUFParametro(decimal pUF_ID, string pNomeCidade, ConnectionInfo pInfo)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = PessoaQD.qCidade;
            lQuery += string.Format(" WHERE UF_ID = {0} AND TRIM(cid_desc) = UPPER('" + pNomeCidade + "')  AND CID_STATUS = 'A' ORDER BY CID_DESC ", pUF_ID);

            MySqlDo lMySqlDo = new MySqlDo();
            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }


        #endregion
    }
}
