using System;
using System.Collections.Generic;
using System.Text;

namespace APB.Framework.Security.Structs
{

    /// <summary>
    /// Representa uma permissão de acesso.
    /// </summary>
	[Serializable()]
	public struct Permission 
	{               

        #region Members
        
        /// <summary>
        /// Acesso a visualização da tela ou item
        /// </summary>
        public bool CanView;

        /// <summary>
        /// Acesso a edição do item
        /// </summary>
		public bool CanEdit;

        /// <summary>
        /// Acesso a inclusão do item
        /// </summary>
		public bool CanInsert;

        /// <summary>
        /// Acesso a exclusão do item
        /// </summary>
		public bool CanDelete;

        /// <summary>
        /// Variavel de controle para representar acesso negado
        /// </summary>
        private bool isNull;
        #endregion

        #region Struct Values

        /// <summary>
        /// Representa um acesso nulo - equivalente a negado
        /// </summary>
        public static Permission Denied = new Permission(null);

        #endregion


        #region Constructors

        public Permission(bool canView, bool canEdit, bool canInsert, bool canDelete)
        {
            this.CanView = canView;
            this.CanEdit = canEdit;
            this.CanInsert = canInsert;
            this.CanDelete = canDelete;
            this.isNull = false;
        }

        public Permission(string bitmask)
        {
            if (bitmask == null)
            {
                this.CanView = false;
                this.CanEdit = false;
                this.CanInsert = false;
                this.CanDelete = false;
                this.isNull = true;
            }
            else
            {
                this.CanView    = (bitmask.Substring(0, 1) == "1"); 
                this.CanEdit    = (bitmask.Substring(1, 1) == "1");
                this.CanInsert  = (bitmask.Substring(2, 1) == "1");
                this.CanDelete  = (bitmask.Substring(3, 1) == "1");
                this.isNull = false;             

            }
        }

        #endregion

        #region Methods

        private void FromBitMask(string bitmask)
		{
            this.CanView = (bitmask.Substring(0, 1) == "1");
            this.CanEdit = (bitmask.Substring(1, 1) == "1");
            this.CanInsert = (bitmask.Substring(2, 1) == "1");
            this.CanDelete = (bitmask.Substring(3, 1) == "1");
            this.isNull = false;         

		}

        
		private string ToBitMask()
		{
			StringBuilder sb = new StringBuilder();
            sb.Append((CanView ? "1" : "0"));
            sb.Append((CanEdit ? "1" : "0"));
            sb.Append((CanInsert ? "1" : "0"));
            sb.Append((CanDelete ? "1" : "0"));
			return sb.ToString();
		}

        #endregion
        
        #region Overrides 

        
        /// <summary>
        /// Converte para string, no formado proprio para ser armazenada
        /// no banco de dados.
        /// </summary>        
        public override string ToString()
        {
            if (isNull)
                return null;
            else
                return ToBitMask();
        }
        

        /// <summary>
        /// Compara uma permissão a outra - Igualdade
        /// </summary>
        /// <param name="p1">Permissão 1</param>
        /// <param name="p2">Permissão 2</param>        
        public static bool operator ==(Permission p1, Permission p2)
        {
            return (p1.ToString() == p2.ToString());
        }

        /// <summary>
        /// Compara uma permissão a outra - Diferença
        /// </summary>
        /// <param name="p1">Permissão 1</param>
        /// <param name="p2">Permissão 2</param>
        public static bool operator !=(Permission p1, Permission p2)
        {
            return (p1.ToString() != p2.ToString());
        }

        /// <summary>
        /// Override passthrough
        /// </summary>        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Override passthrough
        /// </summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }


        #endregion

    }

}
