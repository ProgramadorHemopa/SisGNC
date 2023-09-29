using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace APB.Framework.Security.Internal
{
	/// <summary>
	/// Entidade que representa um usu�rio e suas permiss�es atribuidas
	/// </summary>
    [Serializable()]
    internal sealed class SecurityUser : SecurityEntity
	{


		#region Constructos


		/// <summary>
		/// Construtor padr�o do usu�rio, aceita apenas o c�digo ID.
		/// </summary>
		/// <param name="userid">C�digo ID</param>
		public SecurityUser(int userid)
		{
			this.ID = userid;
		}

		#endregion


	}

}
