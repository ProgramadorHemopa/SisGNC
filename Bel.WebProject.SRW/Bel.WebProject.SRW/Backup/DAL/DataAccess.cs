using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using APB.Framework.Security.Enums;
using APB.Framework.DataBase;
using APB.Framework.Security;

namespace APB.Framework.Security.DAL
{
    public static class DataAccess
    {
        #region Constants

        private const string GENERIC_SELECT = @"SELECT {0}, {1} FROM {2} ORDER BY {3}";
        
        private const string SELECT_COUNT_SECURITYUSERSDT = @"SELECT COUNT(*) FROM SECURITYUSERSDT WHERE SU_ID = >>SU_ID AND  SO_OBJECTID = >>SO_OBJECTID";

        private const string SELECT_COUNT_SECURITYUSERSGROUPSDT = @"SELECT COUNT(1) FROM SECURITYUSERSGROUPSDT WHERE SG_GROUPID = >>SG_GROUPID AND  SO_OBJECTID =  >>SO_OBJECTID";

        private const string ORACLE_SELECT_TABLES = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES ORDER BY TABLE_NAME";

        private const string SQL_SELECT_TABLES = "SELECT TABLE_NAME FROM USER_TABLES ORDER BY TABLE_NAME";

        private const string ORACLE_SELECT_COLUMNS = "SELECT COLUMN_NAME FROM USER_TAB_COLUMNS WHERE TABLE_NAME = >>TABLE_NAME ORDER BY COLUMN_NAME";

        private const string SQL_SELECT_COLUMNS = "";
        
        private const string DELETE_SECURITYUSERSDT = @"DELETE FROM SECURITYUSERSDT WHERE SU_ID = >>SU_ID ";
        
        private const string DELETE_SECURITYUSERSGROUPSDT = @"DELETE FROM SECURITYUSERSGROUPSDT WHERE SG_GROUPID = >>SG_GROUPID";
        
        private const string DELETE_SECURITYUSERSGROUPS = @"DELETE FROM SECURITYUSERSGROUPS WHERE SG_GROUPID = >>SG_GROUPID";

        private const string DELETE_SECURITYGROUPSXUSERS = @"DELETE FROM SECURITYGROUPSXUSERS WHERE GXU_SGID = >>GXU_SGID AND GXU_USERID  = >>GXU_USERID";

        private const string SELECT_SECURITYOBJECTS = @"SELECT SO_OBJECTID, SO_DESC, SO_PARENT,SO_TYPE FROM SECURITYOBJECTS  WHERE SO_STATUS =>>SO_STATUS ORDER BY SO_PARENT, SO_DESC";

        private const string SELECT_SECURITYPARENTOBJECTS = @"SELECT SO_PARENT FROM SECURITYOBJECTS  WHERE SO_STATUS = 'A' AND SO_OBJECTID = >>SO_OBJECTID";

        private const string SELECT_SECURITYCHILDOBJECTS = @"SELECT SO_OBJECTID FROM SECURITYOBJECTS  WHERE SO_STATUS = 'A' AND SO_PARENT = >>SO_PARENT";

        private const string SELECT_SECURITYPERMISSIONUSERMASTER = @"SELECT * FROM SECURITYUSERSDT WHERE SU_ID NOT IN (>>SU_ID) AND SO_OBJECTID IN (>>SO_OBJECTID) AND SUD_PERMISSION = '1111'";

        private const string SELECT_SECURITYPERMISSIONGROUPMASTER = @"SELECT * FROM SECURITYUSERSGROUPSDT WHERE SG_GROUPID NOT IN (>>SG_GROUPID) AND SO_OBJECTID IN (>>SO_OBJECTID) AND SGD_PERMISSION = '1111'";

        private const string SELECT_SECURITYUSERSDT = @"SELECT * FROM SECURITYUSERSDT WHERE SUD_STATUS = >>SUD_STATUS";
        
        private const string SELECT_SECURITYUSERSDT_2 = @"SELECT SECURITYUSERSDT.SO_OBJECTID, SUD_PERMISSION FROM  SECURITYUSERSDT 
                							JOIN SECURITYOBJECTS ON SECURITYUSERSDT.SO_OBJECTID =SECURITYOBJECTS.SO_OBJECTID WHERE SUD_STATUS = >>SUD_STATUS AND SU_ID = >>SU_ID";

        private const string SELECT_SECURITYUSERSGROUPSDT = @"SELECT * FROM SECURITYUSERSGROUPSDT  WHERE SGD_STATUS='A' AND SG_GROUPID = >>SG_GROUPID";

        private const string SELECT_SYSTEMUSERS = @"SELECT SU_ID, SU_LOGINNAME FROM SYSTEMUSERS WHERE
SU_ID NOT IN (SELECT A.GXU_USERID FROM SECURITYGROUPSXUSERS A JOIN SYSTEMUSERS B ON A.GXU_USERID = B.SU_ID 
WHERE A.GXU_SGID = >>GXU_SGID)  ORDER BY SU_LOGINNAME";

        private const string SELECT_SECURITYGROUPSXUSERS = @"SELECT A.GXU_USERID SU_ID, B.SU_LOGINNAME FROM SECURITYGROUPSXUSERS A
JOIN SYSTEMUSERS B ON A.GXU_USERID = B.SU_ID WHERE A.GXU_SGID = >>GXU_SGID ORDER BY SU_LOGINNAME";
        
        private const string SELECT_SECURITYGROUPSXUSERS_2 = @"SELECT A.GXU_USERID, B.SU_LOGINNAME FROM SECURITYGROUPSXUSERS A
JOIN SYSTEMUSERS B ON A.GXU_USERID = B.SU_ID WHERE A.GXU_SGID = >>GXU_SGID ";
        
        private const string SELECT_SECURITYGROUPSXUSERS_3 = @"SELECT GXU_SGID, GXU_USERID FROM SECURITYGROUPSXUSERS WHERE GXU_USERID = >>GXU_USERID";

        #endregion

        public static class Users
        {

            public static DataTable GetUsers()
            {
                SelectCommand lSC = new SelectCommand(SELECT_SECURITYUSERSDT);
                lSC.Fields.Add("SUD_STATUS", 'A');

                return lSC.ReturnData(Instance.CreateDatabase());
            }

            /// <summary>
            /// Verificar se após alteração de permissão, ficará algum usuário com permissão de acesso às funcionalidades do menu de segurança.
            /// </summary>
            /// <param name="pUserId"></param>
            /// <param name="pGroupId"></param>
            /// <param name="pObjectIdList"></param>
            /// <returns></returns>
            public static bool VerifySecurityPermission(Int32 pUserId, Int32 pGroupId, String pObjectIdList)
            {

                DataTable dt = null;                
                SelectCommand lSC = null;
                String lSql = null;

                try
                {
                   
                    //consultar permisões para usuário
                    if (pUserId > -1)
                    {
                        lSql = SELECT_SECURITYPERMISSIONUSERMASTER;
                        lSql = lSql.Replace(">>SU_ID", String.Concat(pUserId.ToString(),",1")); //é adicionar o id 1, pois esse é o do usuário ADMIN
                        lSql = lSql.Replace(">>SO_OBJECTID", pObjectIdList);
                    }
                    else if (pGroupId > -1)
                    {
                        lSql = SELECT_SECURITYPERMISSIONGROUPMASTER;
                        lSql = lSql.Replace(">>SG_GROUPID", pGroupId.ToString());
                        lSql = lSql.Replace(">>SO_OBJECTID", pObjectIdList);
                    }
                    
                    //Verificar se foi encontrado algum registro. Caso tenha sido, signfica que algum outro usuário também possui permissão de acesso 
                    //às funcionalidade de sgurança.
                    lSC = new SelectCommand(lSql);
                    dt = lSC.ReturnData(Instance.CreateDatabase());

                    if (dt.Rows.Count > 0)
                        return true;
                    else
                        return false;

                }
                catch (Exception pErr)
                {
                    throw pErr;
                }
                
            }



            /// <summary>
            /// Verificar se o objectid passado como parâmetro pertence às opções de segurança, ou possui "pais" que possuem.
            /// </summary>
            /// <param name="pObjectId"></param>
            /// <returns></returns>
            public static bool VerifySecurityMenu(Int32 pObjectId)
            {

                DataTable dt;                
                SelectCommand lSC;
                Int32 lObjectId;

                try
                {

                    if (pObjectId == 424)
                    {
                        return true;
                    }
                    
                    //consultar pai
                    lSC = new SelectCommand(SELECT_SECURITYPARENTOBJECTS);
                    lSC.Fields.Add("SO_OBJECTID", pObjectId);

                    dt = lSC.ReturnData(Instance.CreateDatabase());

                    foreach (DataRow drw in dt.Rows)
                    {

                        if (Int32.TryParse(drw["SO_PARENT"].ToString(), out lObjectId) == true)
                        {
                            if (VerifySecurityMenu(lObjectId))
                            {
                                return true;
                            }

                        }

                    }

                }
                catch (Exception pErr)
                {
                    throw pErr;
                }
                
                return false;

            }


            /// <summary>
            /// Verificar se o objectid passado como parâmetro pertence às opções de segurança, ou possui filhos que pertence.
            /// </summary>
            /// <param name="pObjectId"></param>
            /// <returns></returns>
            public static bool VerifySecurityChildMenu(Int32 pObjectId)
            {

                DataTable dt;
                SelectCommand lSC;
                Int32 lObjectId;

                try
                {

                    if (pObjectId == 424)
                    {
                        return true;
                    }

                    //consultar pai
                    lSC = new SelectCommand(SELECT_SECURITYCHILDOBJECTS);
                    lSC.Fields.Add("SO_PARENT", pObjectId);

                    dt = lSC.ReturnData(Instance.CreateDatabase());

                    foreach (DataRow drw in dt.Rows)
                    {

                        if (Int32.TryParse(drw["SO_OBJECTID"].ToString(), out lObjectId) == true)
                        {
                            if (VerifySecurityChildMenu(lObjectId))
                            {
                                return true;
                            }

                        }

                    }

                }
                catch (Exception pErr)
                {
                    throw pErr;
                }

                return false;

            }



            public static Hashtable GetPermissions(int userid)
            {
                DataTable dt;
                Hashtable result = new Hashtable();
                SelectCommand lSC = new SelectCommand(SELECT_SECURITYUSERSDT_2);

                lSC.Fields.Add("SUD_STATUS", 'A');
                lSC.Fields.Add("SU_ID", userid);

                dt = lSC.ReturnData(Instance.CreateDatabase());

                foreach (DataRow drw in dt.Rows)
                {

                    //A implementação abaixo foi realizada, para que o usuário ADMIN (SU_ID = 1), sempre tenha acesso às telas de manutenção de segurança
                    //do sistema. Isso foi definido em reunião realizada entre Felipe, Javier e Marcos H., no dia 23/09/2008.
                    //  ObjectId 427 = Grupos incluir
                    //  ObjectId 428 = Grupos listar
                    //  ObjectId 429 = Usuários listar

                    //if (userid == 1 && (drw["SO_OBJECTID"].ToString() == "427" || drw["SO_OBJECTID"].ToString() == "428" || drw["SO_OBJECTID"].ToString() == "429"))
                    if (userid == 1 && (VerifySecurityMenu(int.Parse(drw["SO_OBJECTID"].ToString()))))
                        result.Add(Convert.ToInt32(drw["SO_OBJECTID"]), "1111");
                    else
                        result.Add(Convert.ToInt32(drw["SO_OBJECTID"]), drw["SUD_PERMISSION"].ToString());
                }

                return result;
            }

            private static void InsertPermission(string permission, int userid, int objectid, string regUser)
            {
                InsertCommand lIns = new InsertCommand("SECURITYUSERSDT");
                lIns.Fields.Add("SU_ID", userid);
                lIns.Fields.Add("SO_OBJECTID", objectid);
                lIns.Fields.Add("SUD_PERMISSION", permission);
                lIns.Fields.Add("SUD_STATUS", 'A');
                lIns.Fields.Add("SUD_REGUSER", regUser);

                lIns.Execute(Instance.CreateDatabase());
            }

            private static void UpdatePermissionInternal(int userid, int objectid, string permission, string regUser)
            {
                UpdateCommand lUpd;
                SelectCommand lSC = new SelectCommand(SELECT_COUNT_SECURITYUSERSDT);

                lSC.Fields.Add("SU_ID", userid);
                lSC.Fields.Add("SO_OBJECTID", objectid);

                if (Convert.ToInt32(lSC.ReturnScalar(Instance.CreateDatabase())) == 0)
                    InsertPermission(permission, userid, objectid, regUser);
                else
                {
                    lUpd = new UpdateCommand("SECURITYUSERSDT");

                    lUpd.Fields.Add("SUD_PERMISSION", permission);
                    lUpd.Fields.Add("SUD_REGUSER", regUser);

                    lUpd.Condition = "WHERE SU_ID = >>SU_ID AND SO_OBJECTID = >>SO_OBJECTID";
                    lUpd.Conditions.Add("SU_ID", userid);
                    lUpd.Conditions.Add("SO_OBJECTID", objectid);

                    lUpd.Execute(Instance.CreateDatabase());
                }
            }

            public static void CallUpdatePermissionInternal(int userid, int objectid, string permission, string regUser)
            {
                UpdatePermissionInternal(userid, objectid, permission, regUser);
            }

            public static void UpdatePermission(int userid, int objectid, string permission, string regUser)
            {
                
                List<int> Family = SecurityObjects.GetObjectFamily(objectid);

                foreach (int fid in Family)
                    UpdatePermissionInternal(userid, fid, permission, regUser);
            }

            public static List<int> GetGroups(int userid)
            {
                List<int> Result = new List<int>();
                SelectCommand lSC = new SelectCommand(SELECT_SECURITYGROUPSXUSERS_3);                
                lSC.Fields.Add("GXU_USERID", userid);

                DataTable dt = lSC.ReturnData(Instance.CreateDatabase());

                foreach (DataRow drw in dt.Rows)
                {
                    Result.Add(Convert.ToInt32(drw["GXU_SGID"]));
                }

               return Result;
            }

            internal static void ResetPermissions(int userid)
            {
                SqlQuery lQuery = new SqlQuery(Instance.CreateDatabase(), DELETE_SECURITYUSERSDT);
                lQuery.AddParameter("SU_ID", userid);

                lQuery.ExecuteNonQuery();
            }
        }        

        public static class Groups
        {
            public static void DeleteGroup(int groupid, string regUser)
            {
                // Para excluir um grupo, tem que des-associar todos os usuarios antes.

                DataTable dt = Groups.GetUsers(groupid);
                foreach (DataRow row in dt.Rows)
                    RemoveFromUser(Convert.ToInt32(row["GXU_USERID"]), groupid, regUser);

                SqlQuery lQuery = new SqlQuery(Instance.CreateDatabase(), DELETE_SECURITYUSERSGROUPSDT);
                lQuery.AddParameter("SG_GROUPID", groupid);

                lQuery.ExecuteNonQuery();

                lQuery = new SqlQuery(Instance.CreateDatabase(), DELETE_SECURITYUSERSGROUPS);
                lQuery.AddParameter("SG_GROUPID", groupid);

                lQuery.ExecuteNonQuery();
            }

            public static class CrossUsers
            {
                public static DataTable GetNas(int groupid)
                {
                    SelectCommand lSC = new SelectCommand(SELECT_SYSTEMUSERS);
                    lSC.Fields.Add("GXU_SGID",groupid);
                    return lSC.ReturnData(Instance.CreateDatabase());
                }

                public static DataTable GetAss(int groupid)
                {
                    SelectCommand lSC = new SelectCommand(SELECT_SECURITYGROUPSXUSERS);
                    lSC.Fields.Add("GXU_SGID", groupid);
                    return lSC.ReturnData(Instance.CreateDatabase());
                }
            }

            public static void ApplyToUser(int userid, int groupid, string regUser)
            {
                Hashtable CurrentUserPermissions = Users.GetPermissions(userid);
                Hashtable GroupPermissions = Groups.GetPermissions(groupid);

                foreach (int key in GroupPermissions.Keys)
                {
                    if (CurrentUserPermissions[key] == null)
                        CurrentUserPermissions.Add(key, "0000");

                    string combined = PermissionQuery.Combine(CurrentUserPermissions[key].ToString(), GroupPermissions[key].ToString());

                    Users.CallUpdatePermissionInternal(userid, key, combined, regUser);
                }

                SqlQuery lQuery = new SqlQuery(Instance.CreateDatabase(), DELETE_SECURITYGROUPSXUSERS);
                lQuery.AddParameter("GXU_SGID", groupid);
                lQuery.AddParameter("GXU_USERID", userid);

                lQuery.ExecuteNonQuery();                

                InsertCommand lIns = new InsertCommand("SECURITYGROUPSXUSERS");
                lIns.Fields.Add("GXU_SGID",groupid);
                lIns.Fields.Add("GXU_USERID",userid);

                lIns.Execute(Instance.CreateDatabase());
            }

            public static void RemoveFromUser(int userid, int groupid, string regUser)
            {

                SqlQuery lQuery = new SqlQuery(Instance.CreateDatabase(), DELETE_SECURITYGROUPSXUSERS);
                lQuery.AddParameter("GXU_SGID", groupid);
                lQuery.AddParameter("GXU_USERID", userid);

                lQuery.ExecuteNonQuery();        

                Users.ResetPermissions(userid);

                foreach (int gmid in Users.GetGroups(userid))
                {
                    Groups.ApplyToUser(userid, gmid, regUser);
                }

            }

            public static void ResetPermissions(int groupid)
            {
            }

            public static Hashtable GetPermissions(int groupid)
            {
               Hashtable result = new Hashtable();
               SelectCommand lSC = new SelectCommand(SELECT_SECURITYUSERSGROUPSDT);
               //lSC.Fields.Add("SGD_STATUS",'A');
               lSC.Fields.Add("SG_GROUPID",groupid);

               DataTable dt = lSC.ReturnData(Instance.CreateDatabase());
                
               foreach (DataRow drw in dt.Rows)
               {
                   result.Add(Convert.ToInt32(drw["SO_OBJECTID"]), drw["SGD_PERMISSION"].ToString());
               }

                return result;
            }

            private static void InsertPermission(int groupid, int objectid, string permission, string regUser)
            {
                InsertCommand lIns = new InsertCommand("SECURITYUSERSGROUPSDT");
                lIns.Fields.Add("SGD_PERMISSION",permission);
                lIns.Fields.Add("SG_GROUPID",groupid);
                lIns.Fields.Add("SO_OBJECTID",objectid);
                lIns.Fields.Add("SGD_REGUSER",regUser);

                lIns.Execute(Instance.CreateDatabase());
            }

            private static void UpdatePermissionInternal(int groupid, int objectid, string permission, string regUser)
            {
                foreach (DataRow row in Groups.GetUsers(groupid).Rows)
                    Users.UpdatePermission(Convert.ToInt32(row["GXU_USERID"]), objectid, permission, regUser);

                SelectCommand lSC = new SelectCommand(SELECT_COUNT_SECURITYUSERSGROUPSDT);
                lSC.Fields.Add("SG_GROUPID",groupid);
                lSC.Fields.Add("SO_OBJECTID", objectid);                

                if (Convert.ToInt32(lSC.ReturnScalar(Instance.CreateDatabase())) == 0)
                {
                    InsertPermission(groupid, objectid, permission, regUser);
                }
                else
                {
                    UpdateCommand lUpd = new UpdateCommand("SECURITYUSERSGROUPSDT");
                    lUpd.Fields.Add("SGD_PERMISSION",permission);
                    lUpd.Fields.Add("SGD_REGUSER",regUser);

                    lUpd.Condition="WHERE SG_GROUPID = >>SG_GROUPID AND SO_OBJECTID = >>SO_OBJECTID";
                    lUpd.Conditions.Add("SG_GROUPID",groupid);
                    lUpd.Conditions.Add("SO_OBJECTID",objectid);

                    lUpd.Execute(Instance.CreateDatabase());
                }
            }

            public static void UpdatePermission(int groupid, int objectid, string permission,string regUser)
            {
                List<int> Family = SecurityObjects.GetObjectFamily(objectid);

                foreach (int fid in Family)
                    UpdatePermissionInternal(groupid, fid, permission, regUser);
            }

            public static DataTable GetRestrictions(int groupid)
            {
                return null;
            }


            public static DataTable GetUsers(int groupid)
            {
                SelectCommand lSC = new SelectCommand(SELECT_SECURITYGROUPSXUSERS_2);
                lSC.Fields.Add("GXU_SGID", groupid);

                return lSC.ReturnData(Instance.CreateDatabase());
            }

        }

        public static class SecurityObjects
        {
            public static DataTable GetTreeData()
            {                
                SelectCommand lSC = new SelectCommand(SELECT_SECURITYOBJECTS);
                lSC.Fields.Add("SO_STATUS",'A');

                return lSC.ReturnData(Instance.CreateDatabase());
            }

            public static List<int> GetObjectFamily(int objectid)
            {

                List<int> family = new List<int>();

                Hashtable allobjects = new Hashtable();
                DataTable objectsdt = GetTreeData();
                foreach (DataRow row in objectsdt.Rows)
                    allobjects.Add(Convert.ToInt32(row["SO_OBJECTID"]), row["SO_PARENT"] == DBNull.Value ? -1 : Convert.ToInt32(row["SO_PARENT"]));

                FillFamilyRecursivelly(allobjects, family, objectid);


                return family;

            }

            public static List<int> GetChildren(Hashtable allobjects, int objectid)
            {
                List<int> children = new List<int>();

                foreach (int key in allobjects.Keys)

                    if (Convert.ToInt32(allobjects[key]) == objectid)
                        children.Add(key);

                return children;
            }

            private static void FillFamilyRecursivelly(Hashtable allobjects, List<int> list, int objectid)
            {
                list.Add(objectid);

                foreach (int child in GetChildren(allobjects, objectid))
                    FillFamilyRecursivelly(allobjects, list, child);
            }
        }

        public static class DatabaseSchema
        {
            public static string[] GetTableNames()
            {                
                List<string> tlist = new List<string>();
                SelectCommand lSC = (Instance.CreateDatabase().DataBaseType == DataBaseType.Oracle)
                    ? new SelectCommand(ORACLE_SELECT_TABLES)
                    : new SelectCommand(SQL_SELECT_TABLES);
  
                DataTable tables = lSC.ReturnData(Instance.CreateDatabase());

                foreach (DataRow row in tables.Rows)
                    tlist.Add(row["TABLE_NAME"].ToString());

                return tlist.ToArray();
            }

            public static string[] GetFieldNames(string tablename)
            {
                List<string> tlist = new List<string>();
                SelectCommand lSC = (Instance.CreateDatabase().DataBaseType == DataBaseType.Oracle)
                                    ? new SelectCommand(ORACLE_SELECT_COLUMNS)
                                    : new SelectCommand(SQL_SELECT_COLUMNS);

                lSC.Fields.Add("TABLE_NAME", tablename.ToUpper());

                DataTable tables = lSC.ReturnData(Instance.CreateDatabase());

                foreach (DataRow row in tables.Rows)
                    tlist.Add(row["COLUMN_NAME"].ToString());

                return tlist.ToArray();

            }
        }

        public static class Restrictions
        {

            //        public static void UpdateASSListFromRestrictionSet(System.Web.UI.WebControls.ListControl control, int holderid, int objectId)
            //        {
            //            RestrictionSet restrictionmarshal = new RestrictionSet(holderid, objectId);
            //        }

            public static void FillASSListFromRestrictionSet(System.Web.UI.WebControls.ListControl control, int holderid, int objectId)
            {
                RestrictionSet res = new RestrictionSet(holderid, objectId, RestrictionType.Group);                

                SelectCommand lSC = new SelectCommand(String.Format(GENERIC_SELECT,res.TextField, res.ValueField, res.TableName, res.TextField));
                DataTable dt = lSC.ReturnData(Instance.CreateDatabase());

                if (res.RestrictionListItems != null)
                {
                    foreach (string item in res.RestrictionListItems)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr[res.ValueField].ToString() == item)
                            {
                                control.Items.Add(new System.Web.UI.WebControls.ListItem(dr[res.TextField].ToString(), dr[res.ValueField].ToString()));
                                break;
                            }
                        }

                    }
                }
            }

            public static void FillNASListFromRestrictionSet(System.Web.UI.WebControls.ListControl control, int holderid, int objectId)
            {
                RestrictionSet res = new RestrictionSet(holderid, objectId, RestrictionType.Group);

                SelectCommand lSC = new SelectCommand(String.Format(GENERIC_SELECT, res.TextField, res.ValueField, res.TableName, res.TextField));
                control.DataSource = lSC.ReturnData(Instance.CreateDatabase());

                control.DataTextField = res.TextField;
                control.DataValueField = res.ValueField;
                control.DataBind();

                if (res.RestrictionListItems != null)
                {
                    foreach (string item in res.RestrictionListItems)
                    {
                        if (control.Items.FindByValue(item) != null)
                            control.Items.Remove(control.Items.FindByValue(item));
                    }
                }
            }

            //        public static void CreateRestrictionList(int objectid, int userid, string tablename, string textfield, string valuefield, int listmode, int valuetype)
            //        {
            //        }

            //        public static Nullable<int> UpdateRestrictionList(int objectid, int userid, string tablename, string textfield, string valuefield, int listmode, int valuetype)
            //        {
            //            return 1;
            //        }
        }

    }
}
