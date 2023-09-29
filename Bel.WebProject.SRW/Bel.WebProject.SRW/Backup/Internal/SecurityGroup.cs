using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace APB.Framework.Security.Internal
{
	/// <summary>
	/// Entidade que representa um grupo e suas permissões atribuidas
	/// </summary>
    [Serializable()]
    internal sealed class SecurityGroup : SecurityEntity 
	{


		#region Constructos


        public SecurityGroup(int groupid)
		{
			this._ID = groupid;
		}

		#endregion


	}

}
