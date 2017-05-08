using System;
using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Repository;
using Autenticacao.Domain.Services;
using Autenticacao.Infra.Data.Context;

namespace Autenticacao.Infra.Data.Repository
{
	public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
	{
		public UsuarioRepository(AutenticacaoContext db) : base(db)
		{
		}

	}
}