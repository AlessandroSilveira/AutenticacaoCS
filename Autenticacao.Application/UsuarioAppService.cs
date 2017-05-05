using System;
using System.Collections.Generic;
using Autenticacao.Application.Interfaces;
using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Repository;

namespace Autenticacao.Application
{
	public class UsuarioAppService : IUsuarioAppService
	{
		private readonly IUsuarioRepository UsuarioRepository;

		public UsuarioAppService(IUsuarioRepository usuarioRepository)
		{
			UsuarioRepository = usuarioRepository;
		}

		public void Dispose()
		{
			UsuarioRepository.Dispose();
		}

		public Usuario Adicionar(Usuario obj)
		{
			return UsuarioRepository.Adicionar(obj);
		}

		public Usuario ObterPorId(Guid id)
		{
			return UsuarioRepository.ObterPorId(id);
		}

		public IEnumerable<Usuario> ObterTodos()
		{
			return UsuarioRepository.ObterTodos();
		}

		public Usuario Atualizar(Usuario obj)
		{
			return UsuarioRepository.Atualizar(obj);
		}

		public void Remover(Guid id)
		{
			UsuarioRepository.Remover(id);
		}

		public int SaveChanges()
		{
			return UsuarioRepository.SaveChanges();
		}
	}
}