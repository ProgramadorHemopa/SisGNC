using System;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Web.SessionState;

using APB.Mercury.WebInterface.SCPWeb.Www;
using APB.Mercury.WebInterface.SCPWeb.Www.Globalization;

using System.Web.UI.WebControls;

namespace APB.Mercury.WebInterface.SCPWeb.Www.MasterPages
{	
	/// <summary>
	/// Classe base para páginas Master
	/// </summary>
	public class BaseMaster : MasterPage
	{

		#region Properties


        /// <summary>
        /// Retorna o Nome do WebForm da aplicação
        /// </summary>
        private string _BaseDocument;

        public string BaseDocument
        {
            get { return _BaseDocument; }
        }

        //private string _PageH3 = "Propriedade PageH3 do Master";

        //public string PageH3
        //{
        //    get { return _PageH3; }
        //    set { _PageH3 = value; }
        //}

        
		/// <summary>
		/// Retorna a Url da aplicação
		/// </summary>
		public string BaseURL
		{
			get
			{
				try
				{
					return string.Format("http://{0}{1}/", HttpContext.Current.Request.ServerVariables["HTTP_HOST"],
										(VirtualFolder.Equals("/")) ? string.Empty : VirtualFolder);
				}
				catch
				{
					return null;
				}
			}
		}

		/// <summary>
		/// Recupera a cultura corrente configurada no Web.config
		/// </summary>
		public string UICurrentCulture
		{
			get
			{
				return System.Globalization.CultureInfo.CurrentCulture.Name;
			}
		}

		/// <summary>
		/// Pasta raíz na qual a aplicação se localiza
		/// </summary>
		/// <returns></returns>
		protected string BaseApplicationPath
		{
			get
			{
				return Server.MapPath(string.Empty);
			}
		}

		public string SkinFolder
		{
			get
			{
				object lSkinFolder;
				string lLocalFolder;

				lSkinFolder = Session["_SkinFolder"];

				if(lSkinFolder == null)
				{
					lSkinFolder = ConfigurationManager.AppSettings["Skin"];

					if(lSkinFolder == null) lSkinFolder = "Default";

					lLocalFolder = Server.MapPath("Skin//" + lSkinFolder.ToString());

					if(!System.IO.Directory.Exists(lLocalFolder))
					{
						TraceWrite(
									TraceCategories.ExpectedConfigError, 
									BuildMsgMessage.Error_Config_BadSkinFolder, 
									lSkinFolder, 
									lLocalFolder
									);

						lSkinFolder = "Default";
					}

					Session["_SkinFolder"] = lSkinFolder;
				}

				return lSkinFolder.ToString();
			}
		}

		/// <summary>
		/// Retorna o diretório virtual onde o projeto está localizado
		/// </summary>
		private static string VirtualFolder
		{
			get { return HttpContext.Current.Request.ApplicationPath; }
		}

		/// <summary>
		/// Retorna o item do menu esquerdo que está espandido
		/// </summary>
		public int SelectedMenuItem
		{
			get
			{
				return _SelectedMenuItem;
			}
			set
			{
				_SelectedMenuItem = value;
			}
		}

		private int _SelectedMenuItem = 0;

		#endregion

        #region Constructor

        public BaseMaster()
		{
            string[] sURL = System.Web.HttpContext.Current.Request.Url.ToString().Split('/');
            _BaseDocument = sURL[sURL.Length - 1];			
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Adiciona um link para um arquivo de css, com base na pasta ~/skin/
		/// </summary>
		/// <param name="pCss">Caminho para o arquivo, se não tiver a extensão ela é adicionada.</param>
		public void AddCSS(string pCss)
		{
			if (!pCss.EndsWith(".css")) pCss += ".css";

			Literal lNewCss = new Literal();

			lNewCss.Text = "\n<link href=\"" + this.BaseURL + "Skin/" + pCss + "\"  rel=\"stylesheet\" type=\"text/css\" />";
			
			Page.Header.Controls.Add(lNewCss);

            
		}

		/// <summary>
		/// Adiciona um link para um arquivo de js, com base na pasta ~/js/
		/// </summary>
		/// <param name="pJavaScriptFileName">Caminho para o arquivo, se não tiver a extensão ela é adicionada.</param>
		public void AddJavaScript(string pJavaScriptFileName)
		{
			if (!pJavaScriptFileName.EndsWith(".js")) pJavaScriptFileName += ".js";

			Literal lNewJs = new Literal();

			lNewJs.Text = "\n<script type=\"text/javascript\" src=\"" + this.BaseURL + "Js/" + pJavaScriptFileName + "\"></script>";
			
			Page.Header.Controls.Add(lNewJs);
		}

        public void AddMeta(string pValue)
        {
            Literal lNewJs = new Literal();

            lNewJs.Text = "\n<meta http-equiv=\"refresh\" content=\"60; URL=" + this.BaseURL + "Aut/Atendimento/Atendimento.aspx"  + "\">";

            Page.Header.Controls.Add(lNewJs);
        }

		/// <summary>
		/// Adiciona javascript dentro do corpo de uma função que será carregada no PageLoad
		/// </summary>
		/// <param name="pJavaScriptFunctionBody">Corpo da função que irá rodar no page load</param>
		public void AddJavaScriptBodyOnLoad(string pJavaScriptFunctionBody)
		{
			Literal lNewJs = new Literal();

			string lFunctionUniqueID = Guid.NewGuid().ToString().Replace('-', '_');

			lNewJs.Text = "\n<script type=\"text/javascript\">    \r\n\r\n";
			lNewJs.Text += "function AfterPageLoad_" + lFunctionUniqueID + "() {  \r\n";
			lNewJs.Text += "//essa funcao foi incluida via AddJavaScriptBodyOnLoad do master. \r\n\r\n";
			lNewJs.Text += pJavaScriptFunctionBody;
			lNewJs.Text += "\r\n    }  \r\n\r\n";
			lNewJs.Text += "addLoadEvent(AfterPageLoad_" + lFunctionUniqueID + ");  \r\n\r\n";
			lNewJs.Text += "</script>   \r\n\r\n";

			Page.Header.Controls.Add(lNewJs);
		}

		public void JavaScriptAlert(string pMessage)
		{
			pMessage = pMessage.Replace('"', '\'');

			pMessage = pMessage.Replace("\r\n", "\\n");

			AddJavaScriptBodyOnLoad(string.Format("alert(\"{0}\");", pMessage));
		}

		/// <summary>
		/// Adiciona uma mensagem ao trace da página, com suporte a string. format
		/// </summary>
		/// <param name="pTraceCategory">Categoria da mensagem do Trace</param>
		/// <param name="pBaseMessage">Mensagem a ser exibida (enum com suporte a localização no CultureHub)</param>
		/// <param name="pParams">Outros parâmetros para o string.format que é feito na mensagem pBaseMessage</param>
		public void TraceWrite(TraceCategories pTraceCategory, BuildMsgMessage pBaseMessage, params object[] pParams)
		{
			if (Trace.IsEnabled)
			{
				Page.Trace.Write(pTraceCategory.ToString(), CultureHub.BuildMessage(pBaseMessage, pParams));
			}
		}

		/// <summary>
		/// Adiciona uma mensagem ao trace da página, com suporte a string. format
		/// </summary>
		/// <param name="pTraceCategory">Categoria da mensagem do Trace</param>
		/// <param name="pMessage">Mensagem a ser exibida (enum com suporte a localização no CultureHub)</param>
		/// <param name="pParams">Outros parâmetros para o string.format que é feito na mensagem pMessage</param>
		public void TraceWrite(TraceCategories pTraceCategory, string pMessage, params object[] pParams)
		{
			if (Trace.IsEnabled)
			{
				Page.Trace.Write(pTraceCategory.ToString(), string.Format(pMessage, pParams));
			}
		}




		#endregion

	}

}
