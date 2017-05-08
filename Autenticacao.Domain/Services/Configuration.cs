using System;
using Autenticacao.Domain.Interfaces.Service;
using static System.Configuration.ConfigurationSettings;

namespace Autenticacao.Domain.Services
{
	public class Configuration : IConfiguration
	{
		public int ObterTempoLogado()
		{
			return Convert.ToInt32(AppSettings["tempoLogado"]);
		}

		public string ObterSmtp()
		{
			return AppSettings["host"];
		}

		public string ObterEmailFrom()
		{
			return AppSettings["From"];
		}

		public string ObterPortaServidorEmail()
		{
			return AppSettings["port"];
		}

		public string GetBodyEmailRecuperarSenha(string token,string id)
		{
			return "Link para alteração de senha http://localhost:55345/api/RecuperarSenha/NovaSenha/?token="+token+"&id="+id+"";
		}
	}
}
