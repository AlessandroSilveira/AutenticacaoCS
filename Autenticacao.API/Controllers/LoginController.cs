using Autenticacao.API.Models;
using Autenticacao.Application.Interfaces;
//using Autenticacao.Infrastructure.Security;
using System;
using System.Net;
using System.Text;
using System.Web.Http;



namespace Autenticacao.API.Controllers
{
	[RoutePrefix("api/login")]
	public class LoginController : ApiController
	{
		private readonly IUsuarioAppService _usuarioApplication;

		public LoginController(IUsuarioAppService usuarioApplication)
		{
			_usuarioApplication = usuarioApplication;
		}

		// POST: api/login
		[HttpPost]
		public IHttpActionResult Autenticar(Login login)
		{
			try
			{
				return !_usuarioApplication.VerificarEmail(login.Email)
					? CustomMessage.Create(HttpStatusCode.Unauthorized, "E-mail informado é inválido.")
					: (_usuarioApplication.VerificarEmailESenha(login.Email, Criptografia.Hash(login.Senha))
						? (IHttpActionResult) Ok(_usuarioApplication.Autenticar(login.Email, Criptografia.Hash(login.Senha)))
						: CustomMessage.Create(HttpStatusCode.Unauthorized, "Usuário e/ou senha inválidos."));
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}
	}

	public class Criptografia
	{
		public static string Hash(string senha)
		{
			var bytes = new UTF8Encoding().GetBytes(senha);
			byte[] hashBytes;
			using (var algorithm = new System.Security.Cryptography.SHA512Managed())
			{
				hashBytes = algorithm.ComputeHash(bytes);
			}

			return Convert.ToBase64String(hashBytes);
		}
	}
}
