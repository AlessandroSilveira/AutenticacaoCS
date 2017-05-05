using System;
using System.Collections.Generic;
using Autenticacao.Application.Interfaces;
using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Repository;
using Autenticacao.Domain.Interfaces.Service;

namespace Autenticacao.Application
{
	public class TelefoneAppService : ITelefoneAppService
	{

		private readonly ITelefoneService _telefoneService;

		public TelefoneAppService(ITelefoneService telefoneService)
		{
			_telefoneService = telefoneService;
		}

		public void Dispose()
		{
			_telefoneService.Dispose();
		}

		public Telefone Adicionar(Telefone obj)
		{
			return _telefoneService.Adicionar(obj);
		}

		public Telefone ObterPorId(Guid id)
		{
			return _telefoneService.ObterPorId(id);
		}

		public IEnumerable<Telefone> ObterTodos()
		{
			return	_telefoneService.ObterTodos();
		}

		public Telefone Atualizar(Telefone obj)
		{
			return _telefoneService.Atualizar(obj);
		}

		public void Remover(Guid id)
		{
			_telefoneService.Remover(id);
		}

		public int SaveChanges()
		{
			return	_telefoneService.SaveChanges();
		}
	}
}