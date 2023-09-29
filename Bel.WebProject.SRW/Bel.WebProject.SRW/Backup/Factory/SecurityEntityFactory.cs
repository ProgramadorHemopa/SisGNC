using System;
using System.Collections.Generic;
using System.Text;
using APB.Framework.Security.Internal;
using APB.Framework.Security.DAL;

namespace APB.Framework.Security.Factory
{

    /// <summary>
    /// Factory de objetos derivados de entidades de seguran�a (Grupo ou Usu�rio);
    /// </summary>
    internal static class SecurityEntityFactory
    {

        #region Methods

        /// <summary>
        /// Cria um SecurityGroup com as permiss�es j� carregadas
        /// </summary>
        /// <param name="groupid">C�digo do Grupo</param>        
        internal static SecurityEntity CreateGroup(int groupid)
        {
            SecurityGroup group = new SecurityGroup(groupid);

            group.Permissions = DataAccess.Groups.GetPermissions(groupid);

            return group;
        }

        /// <summary>
        /// Cria um SecurityUser com as permiss�es j� carregadas
        /// </summary>
        /// <param name="userid">C�digo do usu�rio</param>        
        internal static SecurityEntity CreateUser(int userid)
        {
            SecurityUser user = new SecurityUser(userid);

            user.Permissions = DataAccess.Users.GetPermissions(userid);

            return user;
        }

        #endregion

    }
}
