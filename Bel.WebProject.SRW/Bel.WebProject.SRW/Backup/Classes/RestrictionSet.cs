using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using APB.Framework.Security.Enums;
using APB.Framework.DataBase;

namespace APB.Framework.Security
{

    /// <summary>
    /// Classe que encapsula uma restrição de lista especifica.
    /// </summary>
    [Serializable()]
    public sealed class RestrictionSet
    {
        #region Constants

        private const string SELECT_SECURITYRESLISTSDT = @"SELECT SRD_ITEM, SRD_TYPE FROM SECURITYRESLISTSDT A JOIN SECURITYRESLISTS B 
                            ON A.SRL_LISTID = B.SRL_LISTID WHERE SO_OBJECTID = >>SO_OBJECTID
                            AND SRD_GROUPID = >>SRD_GROUPID AND SRL_STATUS = 'A' order by CAST(SRD_ITEM AS DECIMAL)";

        private const string SELECT_SECURITYRESLISTSDT_RESTRICTIONTYPE_USER = @"SELECT SRD_ITEM, SRD_TYPE FROM SECURITYRESLISTSDT A JOIN SECURITYRESLISTS B 
                            ON A.SRL_LISTID = B.SRL_LISTID WHERE SO_OBJECTID = >>SO_OBJECTID 
                            AND SRD_GROUPID IN( SELECT C.GXU_SGID FROM SECURITYGROUPSXUSERS C WHERE C.GXU_USERID = >>SRD_GROUPID) AND SRL_STATUS = 'A' order by CAST(SRD_ITEM AS DECIMAL)";

        private const string SELECT_SECURITYRESLISTS = @"SELECT SRL_LISTID, SRL_TABLE,  SRL_TEXTFIELD,SRL_VALUEFIELD FROM SECURITYRESLISTS WHERE SO_OBJECTID = >>SO_OBJECTID";

        private const string DELETE_SECURITYRESLISTSDT = "DELETE FROM SECURITYRESLISTSDT WHERE SRL_LISTID = >>SRL_LISTID AND SRD_GROUPID = >>SRD_GROUPID";

        #endregion

        #region Members

        public RestrictionMode Mode = new RestrictionMode();
        public string[] RestrictionListItems = null;
        //private static Helper mercury = Helpers.Helper.Mercury;

        public int ListType;

        public string TableName = string.Empty;
        public string TextField = string.Empty;
        public string ValueField = string.Empty;
        public int ListId;
        private int HolderId;
        private int ObjectID;

        #endregion

        #region Constructors

        /// <summary>
        /// Construtor interno.
        /// </summary>
        /// <param name="objectid">Código do usuárioo</param>
        public RestrictionSet(int holderId, int objectId, RestrictionType type)
        {
            SelectCommand lSC = new SelectCommand(SELECT_SECURITYRESLISTS);
            lSC.Fields.Add("SO_OBJECTID",objectId,APB.Framework.DataBase.ItemType.Decimal); 

            DataRow dr = null;
            DataTable dt;

            dr = lSC.ReturnDataRow(Instance.CreateDatabase());

            if (dr != null)
            {
                this.ListType = 1;
                this.TableName = dr["SRL_TABLE"].ToString();
                this.TextField = dr["SRL_TEXTFIELD"].ToString();
                this.ValueField = dr["SRL_VALUEFIELD"].ToString();
                this.ListId = Convert.ToInt32(dr["SRL_LISTID"]);

                HolderId = holderId;
                ObjectID = objectId;

                List<string> list = new List<string>();

                lSC = (type == RestrictionType.User)
                    ? new SelectCommand(SELECT_SECURITYRESLISTSDT_RESTRICTIONTYPE_USER)
                    : new SelectCommand(SELECT_SECURITYRESLISTSDT);

                lSC.Fields.Add("SO_OBJECTID", ObjectID);
                lSC.Fields.Add("SRD_GROUPID", HolderId);

                dt = lSC.ReturnData(Instance.CreateDatabase());

                foreach (DataRow drw in dt.Rows)
                {
                    this.ListType = Convert.ToInt32(drw["SRD_TYPE"]);

                    if (drw["SRD_ITEM"] != DBNull.Value)
                        list.Add(drw["SRD_ITEM"].ToString());
                }

                if (list.Count != 0)
                    RestrictionListItems = list.ToArray();
            }
        }

        #endregion 

        #region Methods

        /// <summary>
        /// Aplica as regras de restrição ao campo, removendo os items não autorizados.
        /// </summary>
        /// <param name="listBox">Controle a ser processado</param>
        public void ApplyToList(System.Web.UI.WebControls.ListControl control)
        {
            System.Web.UI.WebControls.ListItemCollection col = new System.Web.UI.WebControls.ListItemCollection();
            foreach (string item in RestrictionListItems)
            {
                if (ListType == 1)
                {
                    if (control.Items.FindByValue(item) != null)
                    {
                        control.Items.Remove(control.Items.FindByValue(item));
                    }
                }
                else
                {
                    if (control.Items.FindByValue(item) != null)
                    {
                        col.Add(control.Items.FindByValue(item));
                    }
                }
            }
            if (ListType != 1)
            {
                control.Items.Clear();
                foreach (ListItem item in col)
                {
                    control.Items.Add(item);
                }
            }

        }

        public void UpdateRestrictionList(System.Web.UI.WebControls.ListControl control)
        {
            InsertCommand lIns = new InsertCommand("SECURITYRESLISTSDT");
            SqlQuery lQuery = new SqlQuery(Instance.CreateDatabase(), DELETE_SECURITYRESLISTSDT);
            lQuery.AddParameter("SRL_LISTID", this.ListId);
            lQuery.AddParameter("SRD_GROUPID", this.HolderId);

            lQuery.ExecuteNonQuery();

            foreach (ListItem item in control.Items)
            {
                lIns.ClearFields_And_Conditions();
                lIns.Fields.Add("SRL_LISTID",this.ListId);
                lIns.Fields.Add("SRD_ITEM",item.Value);
                lIns.Fields.Add("SRD_GROUPID",this.HolderId);
                lIns.Fields.Add("SRD_TYPE", this.ListType);

                lIns.Execute(Instance.CreateDatabase());
            }
        }

        #endregion

    }
}
