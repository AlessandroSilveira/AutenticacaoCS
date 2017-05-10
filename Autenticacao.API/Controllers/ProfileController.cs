using System;
using System.Net;
using System.Web.Http;
using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Service;

namespace Autenticacao.API.Controllers
{
	[RoutePrefix("api/profile")]
	public class ProfileController : ApiController
	{
		private readonly IUsuarioService _usuarioService;
		private readonly ICustomMessage _customMessasge;

		public ProfileController(IUsuarioService usuarioService, ICustomMessage customMessasge)
		{
			_customMessasge = customMessasge;
			_usuarioService = usuarioService;
		}

		// GET: api/Profile/5
		public IHttpActionResult Get(Guid id)
		{
			var usuario = _usuarioService.ObterPorId(id);
			var token = _usuarioService.ObterToken(usuario);
			return ValidateToken(id, token, usuario);
		}

		public IHttpActionResult ValidateToken(Guid id, string token, Usuario usuario)
		{
			var retorno = _usuarioService.Autenticar(usuario.Email, usuario.Senha);
			return retorno
				? _customMessasge.Create(HttpStatusCode.Unauthorized, "Token  Inválido") as IHttpActionResult
				: Ok(_usuarioService.ObterPorId(id));
		}
	}
}
