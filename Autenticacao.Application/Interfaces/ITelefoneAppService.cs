using System;
using System.Collections.Generic;
using Autenticacao.Domain.Entities;

namespace Autenticacao.Application.Interfaces
{
	public interface ITelefoneAppService : IDisposable
	{
		Telefone Adicionar(Telefone obj);
		Telefone ObterPorId(Guid id);
		IEnumerable<Telefone> ObterTodos();
		Telefone Atualizar(Telefone obj);
		void Remover(Guid id);
		int SaveChanges();
	}
}