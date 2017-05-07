using System;
using Autenticacao.Domain.Entities;

namespace Autenticacao.Application.Interfaces
{
	public interface IUsuarioAppService : IDisposable
	{
		bool VerificarEmail(object email);
		bool VerificarEmailESenha(string loginEmail, object hash);
		object Autenticar(string loginEmail, object hash);
		string ValidarTokenDoUsuario(string token, string id);
	}
}