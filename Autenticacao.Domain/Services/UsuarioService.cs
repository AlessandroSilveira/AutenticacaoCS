using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web.Security;
using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Repository;
using Autenticacao.Domain.Interfaces.Service;
using RestSharp;

namespace Autenticacao.Domain.Services
{
	public class UsuarioService : IUsuarioService
	{
		private readonly IUsuarioRepository _usuarioRepository;
		private readonly ICriptografia _criptografia;
		private readonly IGerenciadorEmail _gerenciadorEmail;

		public UsuarioService(IUsuarioRepository usuarioRepository, ICriptografia criptografia, IGerenciadorEmail gerenciadorEmail)
		{
			_usuarioRepository = usuarioRepository;
			_criptografia = criptografia;
			_gerenciadorEmail = gerenciadorEmail;
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

		public string ValidarToken(string token, string id)
		{
			var usuario = _usuarioRepository.Get(f => f.UsuarioId.ToString().Equals(id));
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
			return _usuarioRepository.Get(f => f.Email.Equals(email)) != null;
		}

		public bool VerificarEmailESenha(string loginEmail, object hash)
		{
			return _usuarioRepository.Get(f => f.Email.Equals(loginEmail) && f.Senha.Equals(hash)) != null;
		}

		public bool Autenticar(string loginEmail, object hash)
		{
			var usuario = _usuarioRepository.Get(f => f.Email.Equals(loginEmail) && f.Senha.Equals(hash));
			if (usuario != null)
			{
				var token = AutalizarToken(usuario);
				if (string.IsNullOrEmpty(token))
					return false;
				FormsAuthentication.SetAuthCookie(token, false);
				return true;
			}
			else
			{
				return false;
			}
		}

		private string AutalizarToken(Usuario usuario)
		{
			var token = ObterToken(usuario);
			usuario.Token = token;
			_usuarioRepository.Atualizar(usuario);
			return token;
		}

		public string ObterToken(Usuario usuario)
		{
			var client = new RestClient("http://localhost:56490/");
			var request = new RestRequest("/api/token", Method.POST);
			request.AddParameter("grant_type", "password");
			request.AddParameter("username", usuario.Nome);
			request.AddParameter("password", usuario.Senha);
			IRestResponse<TokenData> response = client.Execute<TokenData>(request);
			var token = response.Data.AccessToken;
			return token;
		}

		public Usuario Get(Func<Usuario, bool> func)
		{
			return _usuarioRepository.Get(func);
		}

		public Usuario EnviarToken(string loginEmail)
		{
			var usuario = _usuarioRepository.Get(f => f.Email.Equals(loginEmail));
			 usuario.Token = ObterToken(usuario);
			var dadosEmail = _gerenciadorEmail.EnviarEmail(usuario, usuario.Token);
			_usuarioRepository.Atualizar(usuario);
			EnviarTokenPorEmail(dadosEmail);
			return usuario;
		}

		private static void EnviarTokenPorEmail(EnviaEmailBuilder dadosEmail)
		{
			using (
				var message = new MailMessage(dadosEmail.From, dadosEmail.To,
					dadosEmail.Subject, dadosEmail.Body))
			{
				var client = new SmtpClient(dadosEmail.SmtpServer) { UseDefaultCredentials = true };
				client.Send(message);
			}
		}

		public bool NovaSenha(Usuario usuario)
		{
			try
			{
				var usuario2 = CriarSenhaHash(usuario.UsuarioId.ToString(), usuario.Senha);
				_usuarioRepository.Adicionar(usuario2);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private Usuario CriarSenhaHash(string id, string senha)
		{
			var usuario = _usuarioRepository.Get(f => f.UsuarioId.ToString().Equals(id));
			usuario.Senha = _criptografia.Hash(senha);
			return usuario;
		}
	}
}