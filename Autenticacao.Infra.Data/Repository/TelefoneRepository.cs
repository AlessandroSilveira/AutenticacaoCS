using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Repository;
using Autenticacao.Infra.Data.Context;

namespace Autenticacao.Infra.Data.Repository
{
	public class TelefoneRepository : Repository<Telefone>, ITelefoneRepository
	{
		public TelefoneRepository(AutenticacaoContext db) : base(db)
		{
		}
	}
}