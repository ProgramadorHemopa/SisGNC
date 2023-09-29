using System;

using System.Collections.Generic;
using System.Data;
using System.Xml;

using RPA.DataBase;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;

namespace HMP.DataObjects.SisRNCWeb
{
    [Serializable]
    public class SystemUserDo
    {

        #region Private Methods

        private static void ValidateInsert(DataFieldCollection pValues, OperationResult pResult)
        {

            GenericDataObject.ValidateConversion(pValues, pResult);

        }

        private static void ValidateUpdate(DataFieldCollection pValues, OperationResult pResult)
        {

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

            OperationResult lReturn = new OperationResult(SystemUserQD.TableName, SystemUserQD.TableName);

            ValidateInsert(pValues, lReturn);

            if (!lReturn.HasError)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lInsert = new InsertCommand(SystemUserQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de insert");

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    decimal lSequence;

                    lSequence = DataBaseSequenceControl.GetNext(pInfo, "SUSR_ID");

                    lInsert.Fields.Add(SystemUserQD._SUSR_ID.Name, lSequence, (ItemType)SystemUserQD._SUSR_ID.DBType);

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
           DataFieldCollection pValues,
           Transaction pTransaction,
           ConnectionInfo pInfo
        )
        {
            Transaction lTransaction;

            bool lLocalTransaction = (pTransaction == null);

            if (lLocalTransaction)
                lTransaction = new Transaction(Instance.CreateDatabase(pInfo));
            else
                lTransaction = pTransaction;

            InsertCommand lInsert;

            OperationResult lReturn = new OperationResult(SystemUserQD.TableName, SystemUserQD.TableName);

            ValidateInsert(pValues, lReturn);

            if (!lReturn.HasError)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lInsert = new InsertCommand(SystemUserQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de insert");

                    foreach (DataField lField in pValues.Keys)
                    {
                        lInsert.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    decimal lSequence;

                    lSequence = DataBaseSequenceControl.GetNext(pInfo, "SUSR_ID");

                    lInsert.Fields.Add(SystemUserQD._SUSR_ID.Name, lSequence, (ItemType)SystemUserQD._SUSR_ID.DBType);

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

            OperationResult lReturn = new OperationResult(SystemUserQD.TableName, SystemUserQD.TableName);

            ValidateUpdate(pValues, lReturn);

            if (lReturn.IsValid)
            {
                try
                {
                    if (lLocalTransaction)
                    {
                        lReturn.Trace("Transação local, instanciando banco...");
                    }

                    lUpdate = new UpdateCommand(SystemUserQD.TableName);

                    lReturn.Trace("Adicionando campos ao objeto de update");

                    foreach (DataField lField in pValues.Keys)
                    {
                        if ((lField.Name != SystemUserQD._SUSR_ID.Name))
                            lUpdate.Fields.Add(lField.Name, pValues[lField], (ItemType)lField.DBType);
                    }

                    string lSql = "";
                    lSql = String.Format("WHERE {0} = <<{0}", SystemUserQD._SUSR_ID.Name);

                    lUpdate.Condition = lSql;

                    lUpdate.Conditions.Add(SystemUserQD._SUSR_ID.Name, pValues[SystemUserQD._SUSR_ID].DBToDecimal());

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

            
        public static DataTable GetAllSystemUser(ConnectionInfo pInfo)
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = SystemUserQD.qSystemUserList;
            lQuery += " WHERE SUSR_STATUS = 'A'";

            MySqlDo lMySqlDo = new MySqlDo();

            lTable = lMySqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        public static LoginUserDo VerifyLogin(string pSUSR, string pSUSR_PASSWORD, string pSUSR_TYPE, ConnectionInfo pConnectionInfo)
        {
            bool lReturn;
            string lQuery;
            DataTable lTable;

            //Select.AlterSessionBinary(pConnectionInfo);

            lTable = new DataTable();

            if (pSUSR_TYPE == "1")//
            {
                pSUSR_PASSWORD = APB.Framework.Encryption.Crypto.Encode(pSUSR_PASSWORD);

                if (pSUSR == "ADMIN.GNC")
                {
                    lQuery = SystemUserQD.qSystemUserList;
                    lQuery += String.Format(" WHERE SUSR_LOGIN = '{0}' AND SUSR_PASSWORD = '{1}' ", pSUSR, pSUSR_PASSWORD);
                }
                else
                {
                    lQuery = SystemUserQD.qSystemUserFuncionario;
                    lQuery += String.Format(" AND SUSR_LOGIN = '{0}' AND SUSR_PASSWORD = '{1}' ", pSUSR, pSUSR_PASSWORD);

                }


                /*lSelect.Fields.Add(SystemUserQD._SUSR_LOGIN.Name, pSUSR, (ItemType)SystemUserQD._SUSR_LOGIN.DBType);
                lSelect.Fields.Add(SystemUserQD._SUSR_PASSWORD.Name, pSUSR_PASSWORD, (ItemType)SystemUserQD._SUSR_PASSWORD.DBType);
                */


                MySqlDo lOra = new MySqlDo();
                lTable = lOra.Consulta(lQuery, pConnectionInfo.ConnectionString);

                //lTable = lSelect.ReturnData(Instance.CreateDatabase(pConnectionInfo));
            }

            lReturn = (lTable.Rows.Count > 0) ? true : false;


            if (lReturn)
            {
                decimal lMatricula = 0;
                decimal lUnidade = 0;
                decimal lPerfil = 0;



                //lFUNC_ID = lTable.Rows[0][PessoaFuncaoQD._FUNC_ID.Name].DBToDecimal();
                lMatricula = lTable.Rows[0]["MATRICULA"].DBToDecimal();
                //lPerfil = lTable.Rows[0][PessoaFuncaoQD._PRF_ID.Name].DBToDecimal();

                if (pSUSR != "ADMIN.GNC")
                {
                    DataTable lTableUnidadeResp = NC_OcorrenciaDo.GetUnidadeResp(lMatricula, pConnectionInfo);
                    lUnidade = lTableUnidadeResp.Rows[0]["ID"].DBToDecimal();
                }

                return new LoginUserDo((pSUSR_PASSWORD == "64676661"), lTable.Rows[0]["SUSR_ID"].DBToDecimal(), (string)lTable.Rows[0]["SUSR_LOGIN"], (string)lTable.Rows[0]["NOME"], lMatricula, lUnidade, lPerfil);
            }

            return null;
        }

        public static string ChangePassword(decimal pSusr_Id, string pSusr_Login, string pSusr_Password_old, string pSusr_Password_new, ConnectionInfo pConnectionInfo)
        {
            bool lPasswordOK;
            string lPassword;
            string lResult = "";
            OperationResult lReturn;

            Transaction pTransaction;

            pTransaction = new Transaction(Instance.CreateDatabase(pConnectionInfo));

            // Validando a Senha Digitada.
            lPassword = GetPassword(pSusr_Login, pConnectionInfo);
            lPasswordOK = (lPassword == pSusr_Password_old) ? true : false;
            if (!lPasswordOK)
                lResult = "Senha Atual Incorreta";

            // Verificando Transação
            bool lLocalTransaction = (pTransaction != null);
            UpdateCommand lUpdate;
            lReturn = new OperationResult(SystemUserQD.TableName, SystemUserQD.TableName);

            if (lPasswordOK)
            {
                // Iniciando processo de update...
                if (lReturn.IsValid)
                {
                    try
                    {
                        if (lLocalTransaction)
                        {
                            lReturn.Trace("ExternalUser: Transação local, instanciando banco...");
                        }

                        lUpdate = new UpdateCommand(SystemUserQD.TableName);

                        lReturn.Trace("ExternalUser: Adicionando campos ao objeto de update");

                        lUpdate.Fields.Add(SystemUserQD._SUSR_PASSWORD.Name, APB.Framework.Encryption.Crypto.Encode(pSusr_Password_new), (ItemType)SystemUserQD._SUSR_PASSWORD.DBType);

                        string lSql = "";

                        //TODO: Condição where customizada
                        lSql = String.Format("WHERE {0} = >>{0}", SystemUserQD._SUSR_ID.Name);

                        lUpdate.Condition = lSql;

                        lUpdate.Conditions.Add(SystemUserQD._SUSR_ID.Name, pSusr_Id);

                        lReturn.Trace("ExternalUser: Executando o Update");

                        lUpdate.Execute(pTransaction);

                        if (!lReturn.HasError)
                        {
                            if (lLocalTransaction)
                            {
                                if (!lReturn.HasError)
                                {
                                    lReturn.Trace("ExternalUser: Update finalizado, executando commit");

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
            }
            else
            {
                return "Senha atual incorreta";
            }

            if (lReturn.IsValid)
                lResult = "";
            else
                lResult = lReturn.ToString();

            return lResult;
        }


        public static string GetPassword(string pSusr_Login, ConnectionInfo pConnectionInfo)
        {
            bool lReturn;
            string lQuery = "";
            string lPassWord = "";
            DataTable lTable;

            lQuery = SystemUserQD.qSystemUserList;
            lQuery += String.Format(" WHERE SUSR_LOGIN = '{0}' ", pSusr_Login);
            lQuery += " AND SUSR_STATUS='A' ";

            MySqlDo lMySqlDo = new MySqlDo();



            lTable = lMySqlDo.Consulta(lQuery, pConnectionInfo.ConnectionString);

            lReturn = (lTable.Rows.Count > 0) ? true : false;

            // User Accept, Get Password
            if (lReturn)
            {
                // Decodificar Senha para Envio
                lPassWord = APB.Framework.Encryption.Crypto.Decode(lTable.Rows[0]["SUSR_PASSWORD"].ToString());
            }

            return lPassWord;
        }


        public static DataTable GetUnidadeResp
       (
           decimal pMATRICULA,
           ConnectionInfo pInfo
       )
        {
            string lQuery = "";
            DataTable lTable = new DataTable();

            lQuery = SystemUserQD.qUnidadesVinculosLogin;
            lQuery += string.Format(" and fun.fun_matricula = {0} ", pMATRICULA);

            MySqlDo lSqlDo = new MySqlDo();
            lTable = lSqlDo.Consulta(lQuery, pInfo.ConnectionString);

            return lTable;
        }

        #endregion


    }
}
