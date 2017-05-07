using Autenticacao.API.Models;
using Autenticacao.Application.Interfaces;
using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Web.Http;
using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Service;
using Autenticacao.Infra.Data.Interfaces;

namespace Autenticacao.API.Controllers
{
	public class ProfileController : ApiController, IUsuarioAppService
	{
		private readonly IUsuarioService _usuarioService;
		private readonly IUnitOfWork _uow;
		public ProfileController(IUsuarioService usuarioService, IUnitOfWork uow)
		{
			_uow = uow;
			_usuarioService = usuarioService;
		}

		// GET: api/Profile/5
		public IHttpActionResult Get(string id)
		{
			var headers = Request.Headers;
			var token = GetToken(headers);
			return ValidateToken(id, token);
		}

		private IHttpActionResult ValidateToken(string id, string token)
		{
			try
			{
				var retorno = _usuarioService.ValidarTokenDoUsuario(token, id);

				return !string.IsNullOrWhiteSpace(retorno)
					? (IHttpActionResult)CustomMessage.Create(HttpStatusCode.Unauthorized, retorno)
					: Ok(_usuarioService.ObterPorId(id));
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		private static string GetToken(HttpRequestHeaders headers, string token = "")
		{
			if (!headers.Contains("Authorization")) return token;
			token = headers.GetValues("Authorization").FirstOrDefault();
			return token;
		}

		public bool VerificarEmail(object email)
		{
			return _usuarioService.VerificarEmail(email);
		}

		public bool VerificarEmailESenha(string loginEmail, object hash)
		{
			return _usuarioService.VerificarEmailESenha(loginEmail, hash);
		}

		public object Autenticar(string loginEmail, object hash)
		{
			return _usuarioService.Autenticar(loginEmail, hash);
		}

		public string ValidarTokenDoUsuario(string token, string id)
		{
			return _usuarioService.ValidarTokenDoUsuario(token, id);
		}

		public object Get(Func<object, object> func)
		{
			throw new NotImplementedException();
		}
	}
}
