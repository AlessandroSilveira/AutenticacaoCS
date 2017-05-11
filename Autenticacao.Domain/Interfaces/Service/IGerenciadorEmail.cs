using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Services;

namespace Autenticacao.Domain.Interfaces.Service
{
	public interface IGerenciadorEmail
	{
		EnviaEmailBuilder EnviarEmail(Usuario usuario, string token);
	}
}
