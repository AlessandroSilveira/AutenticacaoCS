using Autenticacao.Domain.Entities;

namespace Autenticacao.Domain.Interfaces.Repository
{
	public interface IUsuarioRepository : IRepository<Usuario>
	{
		Usuario ValidarTokenDoUsuario(string token, string id);
		bool VerificarEmail(object email);
		bool VerificarEmailESenha(string loginEmail, object hash);
		object Autenticar(string loginEmail, object hash);
	}
}