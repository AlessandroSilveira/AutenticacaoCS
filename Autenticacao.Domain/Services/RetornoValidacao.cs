using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Service;

namespace Autenticacao.Domain.Services
{
	public class RetornoValidacao : IItensValidacao
	{
		public string Validacao(string token, Usuario usuario, string retorno, int tempologado)
		{
			return retorno;
		}

		public IItensValidacao Proximo { get; set; }
	}
}