using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace APB.Mercury.WebInterface.SCPWeb.Www.Globalization
{
	public enum BuildMsgMessage
	{
		/// <summary>
		/// Espera mais 2 par�metros. Mensagem: Par�metro 'Skin' configurado como '{0}' por�m pasta '{1}' n�o existe, passando para o padr�o 'Default'
		/// </summary>
		Error_Config_BadSkinFolder
	}

	/// <summary>
	/// Classe para pegar mensagens globalizadas
	/// </summary>
	public static class CultureHub
	{
		#region Public Methods

		public static string BuldValidationMessage(string pMessageKey, params object[] pParams)
		{
			string lReturn = "";
			string lRescMsg;

			try
			{
				lRescMsg = Resources.ValidationMessages.ResourceManager.GetString(
																				pMessageKey,
																				System.Globalization.CultureInfo.CurrentCulture
																				);

				if(pParams != null)
					lReturn = string.Format(lRescMsg, pParams);
				else
					lReturn = lRescMsg;

			}
			catch (FormatException)
			{
				lReturn = "Erro do BuldValidationMessage, os par�metros passados para formatar a string devem estar errados: ";

				lReturn += "BuldValidationMessage: " + pMessageKey + " Params: ";

				foreach (object lParam in pParams)
				{
					lReturn += lParam.ToString() + " | ";
				}
			}
			catch (Exception ex)
			{
				lReturn = "Erro n�o esperado do BuldValidationMessage. Mensagem: '{0}' \r\n Stack: {1}";

				lReturn = string.Format(lReturn, ex.Message, ex.StackTrace);
			}

			return lReturn;
		}

		public static string BuildMessage(BuildMsgMessage pMessage, params object[] pParams)
		{
			string lReturn = "";

			try
			{
				switch (pMessage)
				{
					case BuildMsgMessage.Error_Config_BadSkinFolder:
                        
						lReturn = string.Format(Resources.ErrorMessages.Config_BadSkinFolder, pParams);

						break;

					default:
						break;
				}
			}
			catch (FormatException)
			{
				lReturn = "Erro do GetMessage, os par�metros passados para formatar a string devem estar errados: ";

				lReturn += "BuildMsgMessage: " + pMessage.ToString() + " Params: ";

				foreach (object lParam in pParams)
				{
					lReturn += lParam.ToString() + " | ";
				}
			}
			catch (Exception ex)
			{
				lReturn = "Erro n�o esperado do GetMessage. Mensagem: '{0}' \r\n Stack: {1}";

				lReturn = string.Format(lReturn, ex.Message, ex.StackTrace);
			}

			return lReturn;
		}

		#endregion
	}
}