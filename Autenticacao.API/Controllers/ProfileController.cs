using System.Net;
using System.Web.Http;
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
		public IHttpActionResult Get(string id)
		{
			var usuario = _usuarioService.Get(f => f.UsuarioId.ToString().Equals(id.ToString()));
			var token = _usuarioService.ObterToken(usuario);
			return ValidateToken(id, token);
		}

		public IHttpActionResult ValidateToken(string id, string token)
		{
			var usuario = _usuarioService.Get(f => f.UsuarioId.ToString().Equals(id.ToString()));
			var retorno = _usuarioService.Autenticar(usuario.Email, usuario.Senha);
			return retorno
				? _customMessasge.Create(HttpStatusCode.Unauthorized, "Token  Inválido") as IHttpActionResult
				: Ok(_usuarioService.Get(f => f.UsuarioId.ToString().Equals(id)));
		}
	}
}
