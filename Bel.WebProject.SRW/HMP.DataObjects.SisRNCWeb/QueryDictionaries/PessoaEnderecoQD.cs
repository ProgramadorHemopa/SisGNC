using System;                                                                         
using System.Collections.Generic;                                                     
using System.Text;                                                                    
using System.Reflection;                                                              

namespace HMP.DataObjects.SisRNCWeb.QueryDictionaries
{                                                                                     
	public static class PessoaEnderecoQD
	{                                                                                    
		#region Table Name
                                                                                      
		public static string TableName
		{                                                                          
			get{return "PessoaEndereco";}                                            
		}                                                                          

		#endregion                                                                         

		#region Database Fields

		private static DataField  gPEND_ID = new DataField("PEND_ID", 0);

		public static DataField _PEND_ID
		{                                                                          
			get { return gPEND_ID; }                                           
		}

		private static DataField  gPES_ID = new DataField("PES_ID", 0);

		public static DataField _PES_ID
		{                                                                          
			get { return gPES_ID; }                                           
		}

		private static DataField  gENDE_ID = new DataField("ENDE_ID", 0);

		public static DataField _ENDE_ID
		{                                                                          
			get { return gENDE_ID; }                                           
		}

		private static DataField  gPEND_REGDATE = new DataField("PEND_REGDATE", 2);

		public static DataField _PEND_REGDATE
		{                                                                          
			get { return gPEND_REGDATE; }                                           
		}

		private static DataField  gPEND_REGUSER = new DataField("PEND_REGUSER", 1);

		public static DataField _PEND_REGUSER
		{                                                                          
			get { return gPEND_REGUSER; }                                           
		}


        private static DataField gPEND_STATUS = new DataField("PEND_STATUS", 1);

        public static DataField _PEND_STATUS
        {
            get { return gPEND_STATUS; }
        }

        private static DataField gSRV_ID = new DataField("SRV_ID", 0);

        public static DataField _SRV_ID
        {
            get { return gSRV_ID; }
        }
		#endregion

		#region Queries

		/// <summary>                                                              
		/// select * from PessoaEndereco  WHERE PEND_ID = {0}
		/// </summary>                                                             
		public static string qLoadPessoaEndereco
		{                                                                          
			get { return " select * from PessoaEndereco  WHERE PEND_ID = {0} "; }
		}                                                                          

		public static string qPessoaEnderecoList
		{                                                                          
			get { return @" 
			                select * 
			                    from PessoaEndereco";
		        }                                                                    
		}                                                                          

		public static string qPessoaEnderecoCount
		{                                                                          
			get { 
                            return @" select count(*) from PessoaEndereco";
	 	        }                                                                    
		}                                                                          

		#endregion
    }
}
