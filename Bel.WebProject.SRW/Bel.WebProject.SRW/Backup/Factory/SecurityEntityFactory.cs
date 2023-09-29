using System;
using System.Collections.Generic;
using System.Text;
using APB.Framework.Security.Internal;
using APB.Framework.Security.DAL;

namespace APB.Framework.Security.Factory
{

    /// <summary>
    /// Factory de objetos derivados de entidades de segurança (Grupo ou Usuário);
    /// </summary>
    internal static class SecurityEntityFactory
    {

        #region Methods

        /// <summary>
        /// Cria um SecurityGroup com as permissões já carregadas
        /// </summary>
        /// <param name="groupid">Código do Grupo</param>        
        internal static SecurityEntity CreateGroup(int groupid)
        {
            SecurityGroup group = new SecurityGroup(groupid);

            group.Permissions = DataAccess.Groups.GetPermissions(groupid);

            return group;
        }

        /// <summary>
        /// Cria um SecurityUser com as permissões já carregadas
        /// </summary>
        /// <param name="userid">Código do usuário</param>        
        internal static SecurityEntity CreateUser(int userid)
        {
            SecurityUser user = new SecurityUser(userid);

            user.Permissions = DataAccess.Users.GetPermissions(userid);

            return user;
        }

        #endregion

    }
}
