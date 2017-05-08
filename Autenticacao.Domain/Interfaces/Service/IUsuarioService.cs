using System;
using System.Collections.Generic;
using Autenticacao.Domain.Entities;

namespace Autenticacao.Domain.Interfaces.Service
{
	public interface IUsuarioService : IDisposable
	{
		Usuario Adicionar(Usuario obj);
		Usuario ObterPorId(Guid id);
		IEnumerable<Usuario> ObterTodos();
		Usuario Atualizar(Usuario obj);
		void Remover(Guid id);
		int SaveChanges();
		string ValidarTokenDoUsuario(string token, string id);
		bool VerificarEmail(object email);
		bool VerificarEmailESenha(string loginEmail, object hash);
		object Autenticar(string loginEmail, object hash);
		object Get(Func<Usuario,bool> func);
		object EnviarToken(string loginEmail, string hash);
	}
}