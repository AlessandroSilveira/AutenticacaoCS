using Autenticacao.API.Models;
using Autenticacao.Application.Interfaces;
using Autenticacao.Domain.Entities;
//using Autenticacao.Infrastructure.Security;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Web.Http;
using Autenticacao.Domain.Interfaces.Service;
using Autenticacao.Infra.Data.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Autenticacao.API.Controllers
{
	[RoutePrefix("api/signup")]
	public class SignUpController : ApiController, IUsuarioAppService
	{
		private readonly IUsuarioService _usuarioService;
		private readonly IUnitOfWork _uow;

		public SignUpController(IUsuarioService usuarioService, IUnitOfWork uow)
		{
			_uow = uow;
			_usuarioService = usuarioService;
		}

		// POST: api/SignUp
		[HttpPost]
		public IHttpActionResult Registrar(Usuario usuario)
		{
			try
			{
				if (_usuarioService.VerificarEmail(usuario.Email))
					return CustomMessage.Create(HttpStatusCode.Conflict, "E-mail já cadastrado.");

				var novoUsuario = new Usuario(usuario.Nome, usuario.Email, Criptografia.Hash(usuario.Senha),
					usuario.Telefones, Jwt.GenerateToken(usuario.Email));

				_usuarioService.Adicionar(novoUsuario);

				return Created("Usuario", novoUsuario);
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		public bool VerificarEmail(object email)
		{
			return _usuarioService.VerificarEmail(email);
		}

		public bool VerificarEmailESenha(string loginEmail, object hash)
		{
			return _usuarioService.VerificarEmailESenha(loginEmail, hash);
		}

		public object Autenticar(string loginEmail, object hash)
		{
		return	_usuarioService.Autenticar(loginEmail, hash);
		}

		public string ValidarTokenDoUsuario(string token, string id)
		{
		return	_usuarioService.ValidarTokenDoUsuario(token, id);
		}

		
	}

	public class Jwt
	{
		public const string Secret = "856FECBA3B06519C8DDDBC80BB080553";

		public static string GenerateToken(string username, int expireMinutes = 30)
		{
			var symmetricKey = Convert.FromBase64String(Secret);
			var tokenHandler = new JwtSecurityTokenHandler();

			var now = DateTime.UtcNow;
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, username)
				}),

				Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
			};

			var stoken = tokenHandler.CreateToken(tokenDescriptor);
			var token = tokenHandler.WriteToken(stoken);

			return token;
		}
	}


}
