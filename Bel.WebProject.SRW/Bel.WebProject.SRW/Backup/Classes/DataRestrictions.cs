using System;
using System.Collections.Generic;
using System.Text;
using APB.Framework.Security.Enums;

namespace APB.Framework.Security
{

    /// <summary>
    /// Classe de acesso aos metodos de restrição de dados na tela (listas restritas)
    /// </summary>
    public static class DataRestrictions
    {

        #region Pubic Methods

        /// <summary>
        /// Obtem um conjunto de restriçoes aplicaveis ao usuário atual no objeto especificado
        /// </summary>
        /// <param name="userid">Código do usuário</param>
        /// <param name="objectid">Código do objeto</param>        
        public static RestrictionSet GetRestrictionSet(int userid, int objectid)
        {
            return new RestrictionSet(userid, objectid, RestrictionType.User);
        }


        /// <summary>
        /// Aplica as restrições de dados cabiveis a lista especificada para o usuário atual
        /// </summary>
        /// <param name="control">Controle derivado de um Listcontrol (DropdownList, Listbox, etc..)</param>
        /// <param name="userid">Código do usuário</param>
        /// <param name="objectid">Código do objeto</param>     
        public static void ApplyDataRestriction(System.Web.UI.WebControls.ListControl control, int userId, int objectId)
        {
            RestrictionSet restrictionmarshal = new RestrictionSet(userId, objectId, RestrictionType.User);
            restrictionmarshal.ApplyToList(control);
        }

        #endregion

    }
}
