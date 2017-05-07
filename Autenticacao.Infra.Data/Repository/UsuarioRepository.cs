using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Repository;
using Autenticacao.Infra.Data.Context;

namespace Autenticacao.Infra.Data.Repository
{
	public class UsuarioRepository :Repository<Usuario> , IUsuarioRepository
	{

		

		public UsuarioRepository(AutenticacaoContext db) : base(db)
		{
		}

		public Usuario ValidarTokenDoUsuario(string token, string id)
		{
			
				var usuario =Get(f => f.UsuarioId.ToString().Equals(id));
			return usuario;


		}

		public bool VerificarEmail(object email)
		{
			return Get(f => f.Email.Equals(email)) != null;
		}

		public bool VerificarEmailESenha(string loginEmail, object hash)
		{
			return Get(f => f.Email.Equals(loginEmail) && f.Senha.Equals(hash)) != null;
		}

		public object Autenticar(string loginEmail, object hash)
		{
			throw new System.NotImplementedException();
		}
	}
}