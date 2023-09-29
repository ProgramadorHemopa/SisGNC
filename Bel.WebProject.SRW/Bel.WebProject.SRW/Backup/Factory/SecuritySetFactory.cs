using System;
using System.Collections.Generic;
using System.Text;
using APB.Framework.Security;

namespace APB.Framework.Security.Factory
{
    /// <summary>
    /// Factory de SecuritySets baseados em um usuário ou grupo
    /// </summary>
    internal static class SecuritySetFactory
    {

        #region Methods

        /// <summary>
        /// Cria o SecuritySet a partir do usuário
        /// </summary>
        /// <param name="userID">Código do usuário</param>        
        internal static SecuritySet GetSecuritySet(int userID)
        {   
            return new SecuritySet(SecurityEntityFactory.CreateUser(userID));            
        }

        /// <summary>
        /// Cria o SecuritySet a partir do grupo
        /// </summary>
        /// <param name="groupid">Código do grupo</param>        
        internal static SecuritySet GetGroupSecuritySet(int groupid)
        {
            return new SecuritySet(SecurityEntityFactory.CreateGroup(groupid));
        }

        #endregion

    }
}
