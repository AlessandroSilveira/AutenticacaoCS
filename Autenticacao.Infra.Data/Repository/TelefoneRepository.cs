using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Repository;

namespace Autenticacao.Infra.Data.Repository
{
	public class TelefoneRepository : Repository<Telefone>, ITelefoneRepository
	{
	}
}