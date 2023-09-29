using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using APB.Framework.Security.Internal;
using APB.Framework.Security.Structs;

namespace APB.Framework.Security
{

    /// <summary>
    /// Classe que encapsula um conjunto de permiss�es para um usu�rio ou grupo.
    /// </summary>
    [Serializable()]
    public sealed class SecuritySet
    {

        #region Members

        private SecurityEntity Entity;

        #endregion

        #region Constructors

        /// <summary>
        /// Construtor interno (chamado pela factory) que cria um SecuritySet a partir do usu�rio
        /// </summary>
        /// <param name="user">Usu�rio do security set</param>
        internal SecuritySet(SecurityEntity entity)
        {
            this.Entity = entity;
        }  

        #endregion

        #region Public Methods

        /// <summary>
        /// Resolve a permiss�o do usu�rio ao objeto especificado
        /// </summary>
        /// <param name="objectId">C�digo do objeto</param>        
        public Permission QueryPermission(int objectId)
        {

            // Padr�o � permiss�o negada
            Permission Result = Permission.Denied;

            object query = this.Entity.Permissions[objectId];

            if (query != null)
                Result = new Permission(query.ToString());

            //System.Diagnostics.Debug.WriteLine("Permiss�o : " + Result.ToString(), "QueryPermission( objectid=" + objectId.ToString() + ", User: " + Entity.ID.ToString() + ") ");

            return Result;


        }

        #endregion



    }
}
