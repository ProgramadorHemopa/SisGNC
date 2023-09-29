using System;

using APB.Mercury.WebInterface.SCPWeb.Www.MasterPages;
using System.Web;

using HMP.DataObjects.SisRNCWeb;


namespace HMP.WebInterface.SisRNCWeb.Www.Pages
{
	/// <summary>
	/// Summary description for BasePage
	/// </summary>
	public class BasePageConsulta : System.Web.UI.Page
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

		public BasePageConsulta()
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

            this.MasterPage.AddJavaScript("Validation.js");
			this.MasterPage.AddJavaScript("lib/prototype.js");
			this.MasterPage.AddJavaScript("scriptaculous/scriptaculous.js");

			this.MasterPage.AddCSS("base.css");


            #region Atualizacao Rodrigo - 29/03/2010 
            //A atualizaçao foi realizada para atender a solicitaçao de consulta de processos sem 
            //a necessidade de se validar o usuário.

                string lBrowser = Request.Browser.Browser;
                string lVersion = Request.Browser.Version;

                if (lBrowser == "IE")
                {
                    if (lVersion.DBToDecimal() > 70)
                        this.MasterPage.AddCSS(this.MasterPage.SkinFolder + "/Style2.css");
                    else
                        this.MasterPage.AddCSS(this.MasterPage.SkinFolder + "/Style2.css");
                }
                else if (lBrowser.ToUpper() == "FIREFOX")
                    this.MasterPage.AddCSS(this.MasterPage.SkinFolder + "/StyleFirefox.css");
                else if (lBrowser.ToUpper() == "APPLEMAC-SAFARI")
                    this.MasterPage.AddCSS(this.MasterPage.SkinFolder + "/StyleChrome.css");

                //this.MasterPage.AddCSS(this.MasterPage.SkinFolder + "/toolbar.css");
                //this.MasterPage.AddCSS(this.MasterPage.SkinFolder + "/menu.css");
                //this.MasterPage.AddCSS(this.MasterPage.SkinFolder + "/combobox.css");

                if (this.HasCalendar)
                {
                    this.MasterPage.AddJavaScript("Calendar.js");
                    this.MasterPage.AddCSS(this.MasterPage.SkinFolder + "/calendarStyle.css");
                }

                this.MasterPage.AddJavaScript("Base.js");

                if (this.HasMaskedTextBox)
                {
                    this.MasterPage.AddJavaScript("mascaras.js");
                }

                //this.MasterPage.AddJavaScript("jquery-1.3.1.min.js");
                //this.MasterPage.AddJavaScript("show.js");


                if (this.HasGrid)
                {
                    this.MasterPage.AddJavaScript("WebGrid.js");
                    this.MasterPage.AddCSS(this.MasterPage.SkinFolder + "/WebGrid.css");
                }

                if (this.HasAddress)
                {
                    this.MasterPage.AddJavaScript("AddressPhone.js");
                    this.MasterPage.AddCSS(this.MasterPage.SkinFolder + "/AddressPhone.css");
                }

                if (this.HasPhone)
                {
                    this.MasterPage.AddJavaScript("AddressPhone.js");
                    this.MasterPage.AddCSS(this.MasterPage.SkinFolder + "/AddressPhone.css");

                }

            #endregion





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