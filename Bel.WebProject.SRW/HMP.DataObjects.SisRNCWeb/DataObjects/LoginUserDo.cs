using System;
using System.Collections.Generic;

namespace HMP.DataObjects.SisRNCWeb
{
	/// <summary>
	/// Usuário que está logado no sistema
	/// </summary>
	[Serializable]
	public class LoginUserDo
	{
		#region Properties

        public decimal  LoginId { get; set; }
		public string   LoginName { get; set; }
        public string   NOME { get; set; }
        public decimal  UNIDADE_ID { get; set; }
        public decimal  MATRICULA { get; set; }
        public bool     AlterarSenha { get; set; }
        public decimal  Perfil { get; set; }
        public string   StatusAtivo { get; set; }
        public string   StatusInativo { get; set; }

		#endregion

		#region Constructors
		
		/// <summary>
		/// Construtor padrão
		/// </summary>
		public LoginUserDo()
		{
		}

		/// <summary>
		/// Construtor passando a propriedade Name
		/// </summary>
		/// <param name="pName">Nome do usuário</param>
        public LoginUserDo(decimal pLoginId, string pLoginName)
		{
            this.LoginId = pLoginId;
			this.LoginName = pLoginName;
		}

        public LoginUserDo(bool pAlterarSenha, decimal pLoginId, string pLoginName, string pNome, decimal pMatricula)
        {
            this.AlterarSenha = pAlterarSenha;
            this.LoginId = pLoginId;
            this.LoginName = pLoginName;
            this.NOME = pNome;
            this.MATRICULA = pMatricula;
        }

        public LoginUserDo(bool pAlterarSenha, decimal pLoginId, string pLoginName, string pNome, decimal pMatricula, decimal pUnidade, decimal pPERFIL)
        {
            this.AlterarSenha = pAlterarSenha;
            this.LoginId = pLoginId;
            this.LoginName = pLoginName;
            this.NOME = pNome;
            this.MATRICULA = pMatricula;
            this.UNIDADE_ID = pUnidade;
            this.Perfil = pPERFIL;

            if (pPERFIL == 5)//Estagiario
            {
                this.StatusAtivo = "S";
                this.StatusInativo = "N";
            }
            else
            {
                this.StatusAtivo = "A";
                this.StatusInativo = "I";
            }
        }


        

		#endregion
	}
}