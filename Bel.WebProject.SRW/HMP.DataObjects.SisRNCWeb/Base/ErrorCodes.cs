using System;
using System.Collections.Generic;
using System.Text;

namespace HMP.DataObjects.SisRNCWeb
{
	public static class ErrorCodes
	{
		public static string DataBase_CannotBeNull
		{
			get
			{
				return "Database_Cannot_Be_Null";
			}
		}

		public static string Transaction_CannotBeNull
		{
			get
			{
				return "Transaction_Cannot_Be_Null";
			}
		}
	}
}
