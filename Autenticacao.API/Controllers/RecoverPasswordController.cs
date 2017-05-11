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
		//private readonly ICustomMessage _customMessasge;

		public RecoverPasswordController(IUsuarioService usuarioService)
		{
			//_customMessasge = customMessasge;
			_usuarioService = usuarioService;
		}

		[HttpPost]
		public IHttpActionResult Index(string Email)
		{
			var login = new Login()
			{
				Email = Email
			};

			return _usuarioService.VerificarEmail(login.Email)
			? Ok(_usuarioService.EnviarToken(login.Email)) as IHttpActionResult
			: CustomMessage.Create(HttpStatusCode.Unauthorized, "Usuario ou senhas invalido");
		}
	}
}
