using System;
using System.Collections.Generic;
using System.Text;

namespace APB.Framework.Security.Enums
{

    /// <summary>
    /// Modo de restri��o aplicavel a uma lista
    /// </summary>
    [Serializable()]
    public enum RestrictionMode
    {
        /// <summary>
        /// Todos os items originais s�o mantidos, exceto os encontrados
        /// na lista de restri��o
        /// </summary>
        AllExcept,

        /// <summary>
        /// Todos os items originais s�o removidos exceto os encontrados
        /// na lista de restri��o
        /// </summary>
        OnlyIncluded
    }
}
