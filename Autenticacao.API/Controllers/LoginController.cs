using Autenticacao.API.Models;
using System.Web.Http;
using System.Web.Security;
using Autenticacao.API.ViewModel;
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
		[HttpPost]
		public IHttpActionResult Autenticar(Login login)
		{
			var usuario = _usuarioService.Get(f => f.Email.Equals(login.Email) && f.Senha.Equals(_criptografia.Hash(login.Senha)));
			var client = new RestClient("http://localhost:56490/");

			var request = new RestRequest("/api/security/token", Method.POST);
			request.AddParameter("grant_type", "password");
			request.AddParameter("username","Alessandro");
			request.AddParameter("password","123" );

			IRestResponse<TokenViewModel> response = client.Execute<TokenViewModel>(request);
			var token = response.Data.AccessToken;

			if (!string.IsNullOrEmpty(token))
				FormsAuthentication.SetAuthCookie(token, false);

			return Ok(token);
		}
	}



}

