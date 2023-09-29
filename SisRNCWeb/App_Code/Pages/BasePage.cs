using System;

using APB.Mercury.WebInterface.SCPWeb.Www.MasterPages;
using System.Web;

using HMP.DataObjects.SisRNCWeb;


namespace HMP.WebInterface.SisRNCWeb.Www.Pages
{
	/// <summary>
	/// Summary description for BasePage
	/// </summary>
	public class BasePage : System.Web.UI.Page
	{
		#region Properties


		public BaseMaster MasterPage
		{
			get
			{
				return (BaseMaster)this.Master;
			}
		}

        public bool HasGrid { get; set; }

        public bool HasAddress { get; set; }

        public bool HasPhone { get; set; }

        public bool HasCalendar { get; set; }

        public bool HasMaskedTextBox { get; set; }

        
        /// <summary>
        /// Retorna o Nome do WebForm da aplicação
        /// </summary>
        private string _BaseDocument;

        public string BaseDocument
        {
            get { return _BaseDocument; }
        }


		#endregion

		#region Constructor

		public BasePage()
		{
            string[] sURL = System.Web.HttpContext.Current.Request.Url.ToString().Split('/');
            _BaseDocument = sURL[sURL.Length - 1];
			this.Load+=new EventHandler(Page_Load);
		}

		#endregion


		#region Event Handlers

		public void Page_Load(object sender, EventArgs e)
		{
            ClearCache();

            //this.MasterPage.AddJavaScript("Validation.js");
            //this.MasterPage.AddJavaScript("lib/prototype.js");
            //this.MasterPage.AddJavaScript("scriptaculous/scriptaculous.js");

            //this.MasterPage.AddCSS("base.css");
        }


        private void ClearCache()
        {
            Response.Cache.SetExpires(DateTime.Parse(DateTime. Now.ToString()));
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
        }


		#endregion
	}
}