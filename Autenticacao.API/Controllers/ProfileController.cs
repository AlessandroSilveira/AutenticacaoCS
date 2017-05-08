using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Web.Http;
using Autenticacao.Domain.Interfaces.Service;

namespace Autenticacao.API.Controllers
{
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
					? _customMessasge.Create(HttpStatusCode.Unauthorized, retorno) as IHttpActionResult				
					: Ok(_usuarioService.Get(f=>f.UsuarioId.ToString().Equals(id)));
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
	}
}
