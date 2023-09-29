using System;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using APB.Framework.Security.Factory;

namespace APB.Framework.Security
{

    /// <summary>
    /// Classe de acesso/query as permissões dos usuários.
    /// </summary>
	public static class PermissionQuery
    {

        #region Internal Methods

        /// <summary>
        /// Combina duas permissões em uma, herdando sempre a mais completa,
        /// independente da ordem especificada.
        /// </summary>
        /// <param name="a">Permissão A</param>
        /// <param name="b">Permissão B</param>        
        internal static string Combine(string a, string b)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append((((a.Substring(0, 1) == "1") || (b.Substring(0, 1) == "1")) ? "1" : "0"));
            sb.Append((((a.Substring(1, 1) == "1") || (b.Substring(1, 1) == "1")) ? "1" : "0"));
            sb.Append((((a.Substring(2, 1) == "1") || (b.Substring(2, 1) == "1")) ? "1" : "0"));
            sb.Append((((a.Substring(3, 1) == "1") || (b.Substring(3, 1) == "1")) ? "1" : "0"));

            return sb.ToString();

        }

        #endregion

        #region Pubic Methods

        /// <summary>
        /// Obtem um security set(Conjunto de permissões) de um usuário.
        /// </summary>
        /// <param name="userID">Código do usuário</param>        
		public static SecuritySet GetUserSecuritySet(int userID)
		{
			return SecuritySetFactory.GetSecuritySet(userID);
		}

        /// <summary>
        /// Obtem um security set(Conjunto de permissões) de um grupo.
        /// </summary> 
        /// <param name="groupid">Código do grupo</param>        
        public static SecuritySet GetGroupSecuritySet(int groupid)
        {
            return SecuritySetFactory.GetGroupSecuritySet(groupid);
        }

		#endregion

    }
}
