using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Service;

namespace Autenticacao.Domain.Services
{
	public class GerenciadorEmail : IGerenciadorEmail
	{
		public EnviaEmailBuilder Builder { get; }
		private readonly IConfiguration _configuration;

		public GerenciadorEmail(IConfiguration configuration, EnviaEmailBuilder builder)
		{
			_configuration = configuration;
			Builder = builder;
		}

		//public GerenciadorEmail(Usuario usuario, string token)
		//{
		//	this._usuario = usuario;
		//	this._token = token;
		//}

		public EnviaEmailBuilder EnviarEmail(Usuario usuario, string token)
		{
			Builder.BuildBody("");
			Builder.BuildBcc("");
			Builder.BuildBody(_configuration.GetBodyEmailRecuperarSenha(token, usuario.UsuarioId.ToString()));
			Builder.BuildCc("");
			Builder.BuildFrom(_configuration.ObterEmailFrom());
			Builder.BuildPort(_configuration.ObterPortaServidorEmail());
			Builder.BuildSmtpServer(_configuration.ObterSmtp());
			Builder.BuildTo(usuario.Email);

			//var email = Builder.GetEmail();
			return Builder;
		}
	}
}
