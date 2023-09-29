using System;
using HMP.DataObjects.SisRNCWeb;

namespace APB.Mercury.WebInterface.SCPWeb.Www.Authorization
{
	/// <summary>
	/// Funções de Login / Logout e Autorização
	/// </summary>
	public static class LoginHub
	{
		/// <summary>
		/// Loga um usuário no site.
		/// </summary>
		/// <param name="pDocument">Documento, pode ser CPF ou CNPJ.</param>
		/// <param name="pPassword">Senha, sem criptografia.</param>
		/// <returns>Usuário se logar, ou null se não.</returns>
		public static LoginUserDo LogUser(string pDocument, string pPassword)
		{
			return null;
		}

	}
}