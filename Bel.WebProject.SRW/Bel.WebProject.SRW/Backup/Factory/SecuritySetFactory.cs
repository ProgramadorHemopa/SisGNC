using System;
using System.Collections.Generic;
using System.Text;
using APB.Framework.Security;

namespace APB.Framework.Security.Factory
{
    /// <summary>
    /// Factory de SecuritySets baseados em um usu�rio ou grupo
    /// </summary>
    internal static class SecuritySetFactory
    {

        #region Methods

        /// <summary>
        /// Cria o SecuritySet a partir do usu�rio
        /// </summary>
        /// <param name="userID">C�digo do usu�rio</param>        
        internal static SecuritySet GetSecuritySet(int userID)
        {   
            return new SecuritySet(SecurityEntityFactory.CreateUser(userID));            
        }

        /// <summary>
        /// Cria o SecuritySet a partir do grupo
        /// </summary>
        /// <param name="groupid">C�digo do grupo</param>        
        internal static SecuritySet GetGroupSecuritySet(int groupid)
        {
            return new SecuritySet(SecurityEntityFactory.CreateGroup(groupid));
        }

        #endregion

    }
}
