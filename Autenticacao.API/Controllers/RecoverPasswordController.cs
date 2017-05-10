using System;
using System.Net;
using System.Web.Http;
using Autenticacao.API.Models;
using Autenticacao.Domain.Interfaces.Service;

namespace Autenticacao.API.Controllers
{
	[RoutePrefix("api/recoverpassword")]
	public class RecoverPasswordController : ApiController
	{
		private readonly IUsuarioService _usuarioService;
		private readonly ICustomMessage _customMessasge;

		public RecoverPasswordController(IUsuarioService usuarioService, ICustomMessage customMessasge)
		{
			_customMessasge = customMessasge;
			_usuarioService = usuarioService;
		}

		[HttpPost]
		public IHttpActionResult RecuperarSenha(Login login)
		{
			return _usuarioService.VerificarEmail(login.Email)
				? Ok(_usuarioService.EnviarToken(login.Email)) as IHttpActionResult
				: _customMessasge.Create(HttpStatusCode.Unauthorized, "Usuario ou senhas ");
		}

		[HttpPost]
		public IHttpActionResult NovaSenha(string token, string id, string senha)
		{
			var usuarioid=new Guid(id);
			var usuario = _usuarioService.ObterPorId(usuarioid);
			var dadosToken = _usuarioService.ObterToken(usuario);

			if (token.Equals(dadosToken))
				return usuario == null
					? (IHttpActionResult)_customMessasge.Create(HttpStatusCode.Unauthorized, "Usuário Invalido")
					: Ok(_usuarioService.NovaSenha(usuario));
			return _customMessasge.Create(HttpStatusCode.Unauthorized, "Token Invalido");
		}
	}
}
