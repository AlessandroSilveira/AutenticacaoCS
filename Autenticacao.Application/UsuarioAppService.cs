using System;
using System.Collections.Generic;
using Autenticacao.Application.Interfaces;
using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Repository;
using Autenticacao.Domain.Interfaces.Service;
using Autenticacao.Infra.Data.Interfaces;

namespace Autenticacao.Application
{
	public class UsuarioAppService : ApplicationService, IUsuarioAppService
	{
		private readonly IUsuarioService _usuarioService;

		public UsuarioAppService(IUsuarioService usuarioService, IUnitOfWork uow) : base(uow)
		{
			_usuarioService = usuarioService;
		}

		public void Dispose()
		{
			_usuarioService.Dispose();
		}

		public Usuario Adicionar(Usuario obj)
		{
			BeginTansaction();
			var objreturn =  _usuarioService.Adicionar(obj);
			Commit();
			return objreturn;
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