using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Service;

namespace Autenticacao.Domain.Services
{
	public class GerenciadorEmail
	{
		
		private readonly EnviaEmailBuilder _enviaEmailBuilder;
		private readonly IConfiguration _configuration;
		private Usuario _usuario;
		private string _token;

		public GerenciadorEmail(Usuario usuario, EnviaEmailBuilder enviaEmailBuilder, IConfiguration configuration, string token)
		{
			_usuario = usuario;
			_enviaEmailBuilder = enviaEmailBuilder;
			_configuration = configuration;
			
		}

		public GerenciadorEmail(Usuario usuario, string token)
		{
			this._usuario = usuario;
			this._token = token;
		}

		public IEmailBuilder EnviaEmail()
		{
			_enviaEmailBuilder.BuildBody("");
			_enviaEmailBuilder.BuildBcc("");
			_enviaEmailBuilder.BuildBody(_configuration.GetBodyEmailRecuperarSenha(_token, _usuario.UsuarioId.ToString()));
			_enviaEmailBuilder.BuildCc("");
			_enviaEmailBuilder.BuildFrom(_configuration.ObterEmailFrom());
			_enviaEmailBuilder.BuildPort(_configuration.ObterPortaServidorEmail());
			_enviaEmailBuilder.BuildSmtpServer(_configuration.ObterSmtp());
			_enviaEmailBuilder.BuildTo(_usuario.Email);

			var email =  _enviaEmailBuilder.GetEmail();
			return email;
		}

	}
}
