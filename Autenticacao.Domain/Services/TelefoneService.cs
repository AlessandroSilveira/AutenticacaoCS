using System;
using System.Collections.Generic;
using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Repository;
using Autenticacao.Domain.Interfaces.Service;

namespace Autenticacao.Domain.Services
{
	public class TelefoneService : ITelefoneService
	{
		private readonly ITelefoneRepository _telefoneRepository;

		public TelefoneService(ITelefoneRepository telefoneRepository)
		{
			_telefoneRepository = telefoneRepository;
		}

		public void Dispose()
		{
			_telefoneRepository.Dispose();
			GC.SuppressFinalize(this);
		}

		public Telefone Adicionar(Telefone obj)
		{
			return _telefoneRepository.Adicionar(obj);
		}

		public Telefone ObterPorId(Guid id)
		{
		return	_telefoneRepository.ObterPorId(id);
		}

		public IEnumerable<Telefone> ObterTodos()
		{
			return _telefoneRepository.ObterTodos();
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
			return  _telefoneRepository.SaveChanges();
		}
	}
}