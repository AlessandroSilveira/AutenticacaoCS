using System;
using System.Collections.Generic;
using Autenticacao.Domain.Entities;

namespace Autenticacao.Application.Interfaces
{
	public interface IUsuarioAppService : IDisposable
	{
		Usuario Adicionar(Usuario obj);
		Usuario ObterPorId(Guid id);
		IEnumerable<Usuario> ObterTodos();
		Usuario Atualizar(Usuario obj);
		void Remover(Guid id);
		int SaveChanges();
	}
}