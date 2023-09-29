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
    /// Classe de acesso/query as permiss�es dos usu�rios.
    /// </summary>
	public static class PermissionQuery
    {

        #region Internal Methods

        /// <summary>
        /// Combina duas permiss�es em uma, herdando sempre a mais completa,
        /// independente da ordem especificada.
        /// </summary>
        /// <param name="a">Permiss�o A</param>
        /// <param name="b">Permiss�o B</param>        
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
        /// Obtem um security set(Conjunto de permiss�es) de um usu�rio.
        /// </summary>
        /// <param name="userID">C�digo do usu�rio</param>        
		public static SecuritySet GetUserSecuritySet(int userID)
		{
			return SecuritySetFactory.GetSecuritySet(userID);
		}

        /// <summary>
        /// Obtem um security set(Conjunto de permiss�es) de um grupo.
        /// </summary> 
        /// <param name="groupid">C�digo do grupo</param>        
        public static SecuritySet GetGroupSecuritySet(int groupid)
        {
            return SecuritySetFactory.GetGroupSecuritySet(groupid);
        }

		#endregion

    }
}
