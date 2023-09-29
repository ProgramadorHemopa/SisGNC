using System;
using System.Collections;


namespace APB.Framework.Security.Internal
{

    /// <summary>
    /// Classe base de entidades de segurança.
    /// </summary>
    [Serializable()]
    internal abstract class SecurityEntity
    {

        #region Members

        public Hashtable Permissions = new Hashtable();
        protected int _ID;

        #endregion

        #region Properties

        /// <summary>
        /// Codigo ID do usuário ou grupo
        /// </summary>
        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        #endregion


    }
}
