using System;
using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Service;
using static System.Configuration.ConfigurationSettings;

namespace Autenticacao.Domain.Services
{
	public class VerificaNaoAutorizado :  IItensValidacao
	{
		public IItensValidacao Proximo { get; set; }
		public string Validacao(string token, Usuario usuario, string retorno)
		{
			var tempoLogado = Convert.ToInt32(AppSettings["tempoLogado"]);
			retorno = !string.IsNullOrWhiteSpace(token) &&(usuario.Token.Equals(token) || usuario.DataUltimoLogin.Minute >= tempoLogado)? "Não autorizado.": "";
			return Proximo.Validacao(token, usuario, retorno);
		}
	}
}