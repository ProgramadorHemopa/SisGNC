using System;

using APB.Mercury.WebInterface.SCPWeb.Www.Authorization;

namespace APB.Mercury.WebInterface.SCPWeb.Www.MasterPages
{
	/// <summary>
	/// Summary description for BaseAutMaster
	/// </summary>
	public class BaseAutMaster : BaseMaster
	{
		#region Properties

		private string _PageH3 = "Propriedade PageH3 do Master";

		public string PageH3
		{
			get { return _PageH3; }
			set { _PageH3 = value; }
		}

		#endregion
	}
}