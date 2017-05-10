using System;
using System.Net;
using System.Web.Http;
using Autenticacao.API.Models;
using Autenticacao.Domain.Interfaces.Service;
using RestSharp;

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
		public IHttpActionResult Autenticar(Login login)
		{
			if (_usuarioService.VerificarEmail(login.Email))
			{
				return _usuarioService.VerificarEmailESenha(login.Email, _criptografia.Hash(login.Senha))
					? (IHttpActionResult) Ok(_usuarioService.Autenticar(login.Email, _criptografia.Hash(login.Senha)))
					: CustomMessage.Create(HttpStatusCode.Unauthorized, "Usuário e/ou senha inválidos.");
			}
			return CustomMessage.Create(HttpStatusCode.Unauthorized, "E-mail informado é inválido.");
			//https://github.com/prrandrade/Estudos_AspNetWebApi
			//https://www.youtube.com/watch?v=RwpaFVS60fc
		}
	}
}

