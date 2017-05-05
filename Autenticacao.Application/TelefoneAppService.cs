using System;
using System.Collections.Generic;
using Autenticacao.Application.Interfaces;
using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Repository;

namespace Autenticacao.Application
{
	public class TelefoneAppService : ITelefoneAppService
	{

		private readonly ITelefoneRepository _telefoneRepository;

		public TelefoneAppService(ITelefoneRepository telefoneRepository)
		{
			_telefoneRepository = telefoneRepository;
		}

		public void Dispose()
		{
			_telefoneRepository.Dispose();
		}

		public Telefone Adicionar(Telefone obj)
		{
			return _telefoneRepository.Adicionar(obj);
		}

		public Telefone ObterPorId(Guid id)
		{
			return _telefoneRepository.ObterPorId(id);
		}

		public IEnumerable<Telefone> ObterTodos()
		{
			return	_telefoneRepository.ObterTodos();
		}

		public Telefone Atualizar(Telefone obj)
		{
			return _telefoneRepository.Atualizar(obj);
		}

		public void Remover(Guid id)
		{
			_telefoneRepository.Remover(id);
		}

		public int SaveChanges()
		{
			return	_telefoneRepository.SaveChanges();
		}
	}
}