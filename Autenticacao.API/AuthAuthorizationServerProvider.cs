using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Autenticacao.Domain.Entities;
using Microsoft.Owin.Security.OAuth;

namespace Autenticacao.API
{
	public class AuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
	{
		public async Task ValidateClientAutentication(OAuthValidateClientAuthenticationContext context)
		{
			context.Validated();
		}

		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin",new []{"*"});

			try
			{
				var user = context.UserName;
				var password = context.Password;

				if (user != "Alessandro" || password != "Alessandro")
				{
					context.SetError("invalid_grant","Usuário ou senhas inválidos");
					return;
				}

				var Identity = new ClaimsIdentity(context.Options.AuthenticationType);
				
				Identity.AddClaim(new Claim(ClaimTypes.Name,user));

				var roles  = new List<string>();
				roles.Add("User");

				foreach (var role in roles)
				{
					Identity.AddClaim(new Claim(ClaimTypes.Role,role));
				}

				GenericPrincipal principal = new GenericPrincipal(Identity, roles.ToArray());
				Thread.CurrentPrincipal = principal;

				context.Validated(Identity);
			}
			catch (Exception e)
			{
				context.SetError("invalid_grant","Falha ao Autenticar");
			}
		}

	}
}