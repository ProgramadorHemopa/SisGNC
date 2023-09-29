using System;
using System.Collections.Generic;
using System.Text;
using APB.Framework.Security.Enums;

namespace APB.Framework.Security
{

    /// <summary>
    /// Classe de acesso aos metodos de restri��o de dados na tela (listas restritas)
    /// </summary>
    public static class DataRestrictions
    {

        #region Pubic Methods

        /// <summary>
        /// Obtem um conjunto de restri�oes aplicaveis ao usu�rio atual no objeto especificado
        /// </summary>
        /// <param name="userid">C�digo do usu�rio</param>
        /// <param name="objectid">C�digo do objeto</param>        
        public static RestrictionSet GetRestrictionSet(int userid, int objectid)
        {
            return new RestrictionSet(userid, objectid, RestrictionType.User);
        }


        /// <summary>
        /// Aplica as restri��es de dados cabiveis a lista especificada para o usu�rio atual
        /// </summary>
        /// <param name="control">Controle derivado de um Listcontrol (DropdownList, Listbox, etc..)</param>
        /// <param name="userid">C�digo do usu�rio</param>
        /// <param name="objectid">C�digo do objeto</param>     
        public static void ApplyDataRestriction(System.Web.UI.WebControls.ListControl control, int userId, int objectId)
        {
            RestrictionSet restrictionmarshal = new RestrictionSet(userId, objectId, RestrictionType.User);
            restrictionmarshal.ApplyToList(control);
        }

        #endregion

    }
}
