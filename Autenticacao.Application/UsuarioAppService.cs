using System;
using System.Collections.Generic;
using Autenticacao.Application.Interfaces;
using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Repository;
using Autenticacao.Domain.Interfaces.Service;

namespace Autenticacao.Application
{
	public class UsuarioAppService : IUsuarioAppService
	{
		private readonly IUsuarioService _usuarioService;

		public UsuarioAppService(IUsuarioService usuarioService)
		{
			_usuarioService = usuarioService;
		}

		public void Dispose()
		{
			_usuarioService.Dispose();
		}

		public Usuario Adicionar(Usuario obj)
		{
			return _usuarioService.Adicionar(obj);
		}

		public Usuario ObterPorId(Guid id)
		{
			return _usuarioService.ObterPorId(id);
		}

		public IEnumerable<Usuario> ObterTodos()
		{
			return _usuarioService.ObterTodos();
		}

		public Usuario Atualizar(Usuario obj)
		{
			return _usuarioService.Atualizar(obj);
		}

		public void Remover(Guid id)
		{
			_usuarioService.Remover(id);
		}

		public int SaveChanges()
		{
			return _usuarioService.SaveChanges();
		}
	}
}