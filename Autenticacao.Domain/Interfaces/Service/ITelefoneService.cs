using System;
using System.Collections.Generic;
using Autenticacao.Domain.Entities;

namespace Autenticacao.Domain.Interfaces.Service
{
	public interface ITelefoneService : IDisposable
	{
		Telefone Adicionar(Telefone obj);
		Telefone ObterPorId(Guid id);
		IEnumerable<Telefone> ObterTodos();
		Telefone Atualizar(Telefone obj);
		void Remover(Guid id);
		int SaveChanges();
	}
}