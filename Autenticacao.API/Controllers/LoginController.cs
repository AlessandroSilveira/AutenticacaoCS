using System.Net;
using System.Web.Http;
using Autenticacao.API.Models;
using Autenticacao.Domain.Interfaces.Service;

namespace Autenticacao.API.Controllers
{
	[RoutePrefix("api/login")]
	public class LoginController : ApiController
	{
		private readonly IUsuarioService _usuarioService;
		private readonly ICriptografia _criptografia;
		public LoginController(IUsuarioService usuarioService, ICriptografia criptografia)
		{
			_criptografia = criptografia;
			_usuarioService = usuarioService;
		}

		// POST: api/login
		[HttpPost]
		public IHttpActionResult Autenticar(string Email, string Senha)
		{
			var login = new Login()
			{
				Email = Email,
				Senha = Senha
			};

			return _usuarioService.VerificarEmail(login.Email)
				? (_usuarioService.VerificarEmailESenha(login.Email, _criptografia.Hash(login.Senha))
					? (IHttpActionResult) Ok(_usuarioService.Autenticar(login.Email, _criptografia.Hash(login.Senha)))
					: CustomMessage.Create(HttpStatusCode.Unauthorized, "Usuário e/ou senha inválidos."))
				: CustomMessage.Create(HttpStatusCode.Unauthorized, "E-mail informado é inválido.");
		}
	}
}

