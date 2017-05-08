using System;
using Autenticacao.Domain.Entities;

namespace Autenticacao.Domain.Interfaces.Repository
{
	public interface IUsuarioRepository : IRepository<Usuario>
	{
		Usuario Get(Func<Usuario, bool> func);
	}
}