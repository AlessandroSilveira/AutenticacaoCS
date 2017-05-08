using System.Net;
using System.Web.Http;
using Autenticacao.API.Models;
using Autenticacao.Domain.Interfaces.Service;
using Autenticacao.Infra.Data.Interfaces;

namespace Autenticacao.API.Controllers
{
	public class RecoverPassword : ApiController
	{
		private readonly IUsuarioService _usuarioService;
		private readonly IUnitOfWork _uow;
		private readonly ICustomMessage _customMessasge;
		private readonly ICriptografia _criptografia;

		public RecoverPassword(IUsuarioService usuarioService, IUnitOfWork uow, ICustomMessage customMessasge,
			ICriptografia criptografia)
		{
			_uow = uow;
			_customMessasge = customMessasge;
			_criptografia = criptografia;
			_usuarioService = usuarioService;
		}

		[HttpPost]
		public IHttpActionResult RecuperarSenha(Login login)
		{
			return !_usuarioService.VerificarEmail(login.Email)
				? _customMessasge.Create(HttpStatusCode.Unauthorized, "E-mail informado é inválido.")
				: (_usuarioService.VerificarEmailESenha(login.Email, _criptografia.Hash(login.Senha))
					? Ok(_usuarioService.EnviarToken(login.Email, _criptografia.Hash(login.Senha))) as IHttpActionResult
					: _customMessasge.Create(HttpStatusCode.Unauthorized, "Usuário e/ou senha inválidos."));
		}

		[HttpPost]
		public IHttpActionResult NovaSenha(string token, string id, string senha)
		{
			var dadosToken = _usuarioService.ValidarToken(token, id);
			var usuario = _usuarioService.Get(f => f.UsuarioId.ToString().Equals(id));

			if (!string.IsNullOrEmpty(dadosToken))
				return usuario == null
					? (IHttpActionResult) _customMessasge.Create(HttpStatusCode.Unauthorized, "Usuário Invalido")
					: Ok(_usuarioService.NovaSenha(token, id, senha));
			return _customMessasge.Create(HttpStatusCode.Unauthorized, "Token Invalido");
		}
	}
}
