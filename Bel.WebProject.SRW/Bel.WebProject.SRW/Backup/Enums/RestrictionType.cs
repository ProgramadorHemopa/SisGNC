using System;
using System.Collections.Generic;
using System.Text;

namespace APB.Framework.Security.Enums
{
    /// <summary>
    /// Modo de restrição aplicavel a uma lista
    /// </summary>
    [Serializable()]
    public enum RestrictionType
    {
        /// <summary>
        /// Todos os items originais são mantidos, exceto os encontrados
        /// na lista de restrição
        /// </summary>
        Group,

        /// <summary>
        /// Todos os items originais são removidos exceto os encontrados
        /// na lista de restrição
        /// </summary>
        User
    }
}
