using System;
using System.Collections.Generic;
using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Repository;
using Autenticacao.Domain.Interfaces.Service;

namespace Autenticacao.Domain.Services
{
	public class UsuarioService : IUsuarioService
	{

		private readonly IUsuarioRepository _usuarioRepository;

		public UsuarioService(IUsuarioRepository usuarioRepository)
		{
			_usuarioRepository = usuarioRepository;
		}

		public void Dispose()
		{
			_usuarioRepository.Dispose();
			GC.SuppressFinalize(this);
		}

		public Usuario Adicionar(Usuario obj)
		{
			return _usuarioRepository.Adicionar(obj);
		}

		public Usuario ObterPorId(Guid id)
		{
			return _usuarioRepository.ObterPorId(id);
		}

		public IEnumerable<Usuario> ObterTodos()
		{
			return _usuarioRepository.ObterTodos();
		}

		public Usuario Atualizar(Usuario obj)
		{
			return _usuarioRepository.Atualizar(obj);
		}

		public void Remover(Guid id)
		{
			_usuarioRepository.Remover(id);
		}

		public int SaveChanges()
		{
			return _usuarioRepository.SaveChanges();
		}

		public string ValidarTokenDoUsuario(string token, string id)
		{
			var usuario= _usuarioRepository.ValidarTokenDoUsuario(token, id);
			 return ValidadorToken(usuario.Token, usuario);
		}
		private static string ValidadorToken(string token, Usuario usuario)
		{
			var retorno = "";

			var verificadoNaoAutorizado = new VerificaNaoAutorizado();
			var verificaSessaoInvalida = new VerificaSessaoInvalida();
			var retornaValidacao = new RetornoValidacao();

			verificadoNaoAutorizado.Proximo = verificaSessaoInvalida;
			verificaSessaoInvalida.Proximo = retornaValidacao;

			return verificadoNaoAutorizado.Validacao(token, usuario, retorno);
		}
		public bool VerificarEmail(object email)
		{
			return _usuarioRepository.VerificarEmail(email);
		}

		public bool VerificarEmailESenha(string loginEmail, object hash)
		{
			return _usuarioRepository.VerificarEmailESenha(loginEmail, hash);
		}

		public object Autenticar(string loginEmail, object hash)
		{
			return _usuarioRepository.Autenticar(loginEmail, hash);
		}
	}
}