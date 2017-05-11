using System;
using System.Net;
using System.Web.Http;
using Autenticacao.Domain.Interfaces.Service;

namespace Autenticacao.API.Controllers
{
	[RoutePrefix("api/newpassword")]
	public class NewPasswordController : ApiController
	{
		private readonly IUsuarioService _usuarioService;
		//private readonly ICustomMessage _customMessasge;

		public NewPasswordController(IUsuarioService usuarioService)
		{
			//_customMessasge = customMessasge;
			_usuarioService = usuarioService;
		}


		[HttpPost]
		public IHttpActionResult NovaSenha(string token, string id, string senha)
		{
			var usuarioid = new Guid(id);
			var usuario = _usuarioService.ObterPorId(usuarioid);
			

			if (token.Equals(usuario.Token))
				return usuario == null
					? (IHttpActionResult)CustomMessage.Create(HttpStatusCode.Unauthorized, "Usuário Invalido")
					: Ok(_usuarioService.NovaSenha(usuario));
			return CustomMessage.Create(HttpStatusCode.Unauthorized, "Token Invalido");
		}
	}
}