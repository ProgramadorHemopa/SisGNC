using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{                                                                                     
	public static class SecurityObjectsQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "SecurityObjects";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gSO_OBJECTID = new DataField("SO_OBJECTID", 0);

		public static DataField _SO_OBJECTID
		{                                                                          
			get { return gSO_OBJECTID; }                                           
		}

		private static DataField  gSO_PARENT = new DataField("SO_PARENT", 0);

		public static DataField _SO_PARENT
		{                                                                          
			get { return gSO_PARENT; }                                           
		}

		private static DataField  gSO_TYPE = new DataField("SO_TYPE", 0);

		public static DataField _SO_TYPE
		{                                                                          
			get { return gSO_TYPE; }                                           
		}

		private static DataField  gSO_DESC = new DataField("SO_DESC", 1);

		public static DataField _SO_DESC
		{                                                                          
			get { return gSO_DESC; }                                           
		}

		private static DataField  gSO_STATUS = new DataField("SO_STATUS", 1);

		public static DataField _SO_STATUS
		{                                                                          
			get { return gSO_STATUS; }                                           
		}

		private static DataField  gSO_REGDATE = new DataField("SO_REGDATE", 2);

		public static DataField _SO_REGDATE
		{                                                                          
			get { return gSO_REGDATE; }                                           
		}

		private static DataField  gSO_REGUSER = new DataField("SO_REGUSER", 1);

		public static DataField _SO_REGUSER
		{                                                                          
			get { return gSO_REGUSER; }                                           
		}
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from SecurityObjects  WHERE SO_OBJECTID = {0}
		/// </summary>                                                             
		public static string qLoadSecurityObjects
		{                                                                          
			get { return " select * from SecurityObjects  WHERE SO_OBJECTID = {0} "; }
		}                                                                          

		public static string qSecurityObjectsList
		{                                                                          
			get { return @" 
			                select * 
			                    from SecurityObjects";
		        }                                                                    
		}                                                                          

		public static string qSecurityObjectsCount
		{                                                                          
			get { 
                            return @" select count(*) from SecurityObjects";
	 	        }                                                                    
		}                                                                          

		public static string qSecurityObjects
		{                                                                          
			get { return @" 
			                select  SO_OBJECTID,SO_DESC 
			                    from SecurityObjects";
		        }                                                                    
		}                                                                          

		#endregion
    }
}
