using System;
using System.Collections.Generic;
using System.Text;
using APB.Framework.DataBase;

namespace APB.Framework.Security
{
	public static class Instance
	{
		public static APB.Framework.DataBase.DataBase CreateDatabase()
		{

			string configFile = null;
			configFile = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"\DataAccess.config";
			ConfigurationReader config = ConfigurationCache.Fetch(configFile);
			APB.Framework.DataBase.DataBaseType ldbType = (config.Provider.ToLower() == "oracle") ? APB.Framework.DataBase.DataBaseType.Oracle : APB.Framework.DataBase.DataBaseType.SqlServer;
			return new APB.Framework.DataBase.DataBase(ldbType, config.ConnectionString);
		}
	}
}
