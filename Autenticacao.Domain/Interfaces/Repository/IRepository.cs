using System;
using System.Collections.Generic;

namespace Autenticacao.Domain.Interfaces.Repository
{
	public interface IRepository<TEntity> :IDisposable where  TEntity : class
	{
		TEntity Adicionar(TEntity obj);
		TEntity ObterPorId(Guid id);
		IEnumerable<TEntity> ObterTodos();
		TEntity Atualizar(TEntity obj);
		void Remover(Guid id);
		int SaveChanges();
	}
}