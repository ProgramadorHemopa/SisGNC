using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace APB.Framework.Security.Internal
{
	/// <summary>
	/// Entidade que representa um usuário e suas permissões atribuidas
	/// </summary>
    [Serializable()]
    internal sealed class SecurityUser : SecurityEntity
	{


		#region Constructos


		/// <summary>
		/// Construtor padrão do usuário, aceita apenas o código ID.
		/// </summary>
		/// <param name="userid">Código ID</param>
		public SecurityUser(int userid)
		{
			this.ID = userid;
		}

		#endregion


	}

}
