using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
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
		string ValidarToken(string token, string id);
		bool VerificarEmail(object email);
		bool VerificarEmailESenha(string loginEmail, object hash);
		bool Autenticar(string loginEmail, object hash);
		Usuario Get(Func<Usuario,bool> func);
		Usuario EnviarToken(string loginEmail);
		Usuario NovaSenha(Usuario usuario);
		string ObterToken(Usuario usuario);
	}
}