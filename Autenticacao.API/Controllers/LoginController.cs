using Autenticacao.API.Models;
using Autenticacao.Application.Interfaces;
//using Autenticacao.Infrastructure.Security;
using System;
using System.Net;
using System.Text;
using System.Web.Http;
using Autenticacao.Domain.Interfaces.Service;
using Autenticacao.Domain.Services;
using Autenticacao.Infra.Data.Interfaces;


namespace Autenticacao.API.Controllers
{
	[RoutePrefix("api/login")]
	public class LoginController : ApiController
	{
		private readonly IUsuarioService _usuarioService;
		private readonly ICustomMessage _customMessasge;
		private readonly ICriptografia _criptografia;
		public LoginController(IUsuarioService usuarioService, ICustomMessage customMessasge, ICriptografia criptografia)
		{
			_customMessasge = customMessasge;
			_criptografia = criptografia;
			_usuarioService = usuarioService;
		}

		// POST: api/login
		[HttpPost]
		public IHttpActionResult Autenticar(Login login)
		{
			try
			{
				return !_usuarioService.VerificarEmail(login.Email)
					? _customMessasge.Create(HttpStatusCode.Unauthorized, "E-mail informado é inválido.")
					: (_usuarioService.VerificarEmailESenha(login.Email, _criptografia.Hash(login.Senha))
						? (IHttpActionResult) Ok(_usuarioService.Autenticar(login.Email, _criptografia.Hash(login.Senha)))
						: _customMessasge.Create(HttpStatusCode.Unauthorized, "Usuário e/ou senha inválidos."));
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}
	}
}
