using System;
using System.Collections.Generic;
using System.Text;

namespace APB.Framework.Security.Attributes
{

    /// <summary>
    /// Atributo de identificação de um objeto de segurança
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class SecurityObjectId :Attribute
    {

        #region Members

        public readonly int ObjectId;
        public readonly bool EditRequired = false;
        public readonly bool InsertRequired = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Construtor padrão do atributo de identificação de um objeto de segurança
        /// </summary>
        /// <param name="objectId">Código Id do Objeto</param>
        public SecurityObjectId(int objectId)
        {
            this.ObjectId = objectId;
        }

        /// <summary>
        /// Construtor padrão do atributo de identificação de um objeto de segurança
        /// </summary>
        /// <param name="objectId">Código Id do Objeto</param>
        public SecurityObjectId(int objectId, bool editrequired, bool insertrequired)
        {
            this.ObjectId = objectId;
            this.EditRequired = editrequired;
            this.InsertRequired = insertrequired;
        }


        #endregion
        
    }

}
