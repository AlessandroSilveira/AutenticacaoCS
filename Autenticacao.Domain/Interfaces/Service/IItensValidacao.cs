using Autenticacao.Domain.Entities;

namespace Autenticacao.Domain.Interfaces.Service
{
	public interface IItensValidacao
	{
		string Validacao(string token, Usuario usuario, string retorno);
		IItensValidacao Proximo { get; set; }
	}
}