using System;
using System.Collections;

using APB.Mercury.WebInterface.SCPWeb.Www.MasterPages;
using APB.Mercury.WebInterface.SCPWeb.Www.Authorization;
using HMP.DataObjects.SisRNCWeb;

using APB.Framework.Security;
using APB.Framework.Security.Structs;

namespace HMP.WebInterface.SisRNCWeb.Www.Pages
{
    /// <summary>
    /// Summary description for BaseAutPage
    /// </summary>
    public class BaseAutPage : BasePage
    {
        #region Globals

        protected readonly SecurityInterface Security = new SecurityInterface();

        protected Permission Permission = new Permission("0000");

        #endregion

        #region Properties

        private bool _HasValidation;

        public bool HasValidation
        {
            get { return _HasValidation; }
            set { _HasValidation = value; }
        }

        public bool HasGrafico { get; set; }

        public bool HasGrid { get; set; }

        public bool HasAddress { get; set; }

        public bool HasPhone { get; set; }

        public bool HasCalendar { get; set; }

        public bool HasMaskedTextBox { get; set; }

        public bool HasCKEDITOR { get; set; }

        public string HasMenu { get; set; }

        public new BaseAutMaster MasterPage
        {
            get
            {
                return (BaseAutMaster)base.MasterPage;
            }
        }

        public LoginUserDo SessionUser
        {
            get
            {
                if (Session["_SessionUser"] == null)
                {
                    return null;
                }
                else
                {
                    return (LoginUserDo)Session["_SessionUser"];
                }
            }
            set
            {
                Session["_SessionUser"] = value;
            }
        }


        #endregion

        #region Constructors

        public BaseAutPage()
        {
        }

        #endregion

        #region Private Methods



        #endregion

        #region Event Handlers

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            try
            {
                //((Hashtable)Application["USERS"])[Session["LoginDesc"].ToString().ToUpper()] = DateTime.Now;
            }
            catch { }

            Security.SetParent(this);

            if (!this.Security.ObjectId.HasValue) return;

            //if (Session["SecuritySet"] == null || Session["_SessionUser"] == null)
            if (Session["_SessionUser"] == null)
            {
                Response.Redirect(this.MasterPage.BaseURL + "Logout.aspx");
                //Sem sessão, logout.aspx
            }
            else
            {
                if (((LoginUserDo)Session["_SessionUser"]).LoginName == "ADMIN.WEB")
                    return;


               /* Permission = ((SecuritySet)Session["SecuritySet"]).QueryPermission((int)this.Security.ObjectId);

                if (Permission.ToString() == "0000" || Permission.ToString() == null)
                {
                    //Server.Transfer("/AcessoNegado.aspx");
                    Response.Redirect(this.MasterPage.BaseURL + "Aut/AcessoNegado.aspx");
                }*/
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.SessionUser == null)
            {
                Session["RedirectAfterLogin"] = Request.Url.PathAndQuery;

                Response.Redirect(this.MasterPage.BaseURL + "Logout.aspx");
            }
            else
            {
                Session["RedirectAfterLogin"] = null;
            }

            string lBrowser = Request.Browser.Browser;
            string lVersion = Request.Browser.Version;

            //Adicionar js e css para páginas que utilizam gráficos
            if (this.HasGrafico)
            {
                this.MasterPage.AddJavaScript("raphael-min.js");
                this.MasterPage.AddJavaScript("morris.js");
                this.MasterPage.AddJavaScript("morris.min.js");
                this.MasterPage.AddJavaScript("prettify.min.js");
                this.MasterPage.AddCSS(this.MasterPage.SkinFolder + "/prettify.min.css");
                this.MasterPage.AddCSS(this.MasterPage.SkinFolder + "/morris.css");
            }

            //this.MasterPage.AddJavaScript("Base.js");

            if (this.HasMaskedTextBox)
            {
                this.MasterPage.AddJavaScript("mascaras.js");
            }



            if (HasMenu != null)
                this.MasterPage.AddJavaScriptBodyOnLoad("PageInit();  var vTeste = pBase_Select(document.getElementById('" + HasMenu + "'));");
            else
                this.MasterPage.AddJavaScriptBodyOnLoad("PageInit();");
        }

        #endregion
    }
}