using System;
using System.Collections.Generic;
using System.Text;

namespace APB.Framework.Security.Attributes
{

    /// <summary>
    /// Atributo de identifica��o de um objeto de seguran�a
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
        /// Construtor padr�o do atributo de identifica��o de um objeto de seguran�a
        /// </summary>
        /// <param name="objectId">C�digo Id do Objeto</param>
        public SecurityObjectId(int objectId)
        {
            this.ObjectId = objectId;
        }

        /// <summary>
        /// Construtor padr�o do atributo de identifica��o de um objeto de seguran�a
        /// </summary>
        /// <param name="objectId">C�digo Id do Objeto</param>
        public SecurityObjectId(int objectId, bool editrequired, bool insertrequired)
        {
            this.ObjectId = objectId;
            this.EditRequired = editrequired;
            this.InsertRequired = insertrequired;
        }


        #endregion
        
    }

}
