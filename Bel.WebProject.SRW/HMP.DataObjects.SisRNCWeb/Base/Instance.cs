using System;

using RPA.DataBase;

namespace HMP.DataObjects.SisRNCWeb
{
	/// <summary>
	/// Summary description for Instance
	/// </summary>
	public class Instance
	{
		public static RPA.DataBase.DataBase CreateDatabase(ConnectionInfo pInfo)
		{
			//TODO: Fazer direito, a connstring tem que ser criptografada
			return new RPA.DataBase.DataBase((DataBaseType)pInfo.DataBaseType, pInfo.ConnectionString);
		}

	}
}