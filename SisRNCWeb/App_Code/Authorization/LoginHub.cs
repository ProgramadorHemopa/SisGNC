using System;
using HMP.DataObjects.SisRNCWeb;

namespace APB.Mercury.WebInterface.SCPWeb.Www.Authorization
{
	/// <summary>
	/// Fun��es de Login / Logout e Autoriza��o
	/// </summary>
	public static class LoginHub
	{
		/// <summary>
		/// Loga um usu�rio no site.
		/// </summary>
		/// <param name="pDocument">Documento, pode ser CPF ou CNPJ.</param>
		/// <param name="pPassword">Senha, sem criptografia.</param>
		/// <returns>Usu�rio se logar, ou null se n�o.</returns>
		public static LoginUserDo LogUser(string pDocument, string pPassword)
		{
			return null;
		}

	}
}