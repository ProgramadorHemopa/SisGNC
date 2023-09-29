using System;
using System.Collections.Generic;
using System.Text;

namespace APB.Framework.Security.Enums
{
    /// <summary>
    /// Modo de restri��o aplicavel a uma lista
    /// </summary>
    [Serializable()]
    public enum RestrictionType
    {
        /// <summary>
        /// Todos os items originais s�o mantidos, exceto os encontrados
        /// na lista de restri��o
        /// </summary>
        Group,

        /// <summary>
        /// Todos os items originais s�o removidos exceto os encontrados
        /// na lista de restri��o
        /// </summary>
        User
    }
}
