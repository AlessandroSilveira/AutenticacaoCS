using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace Autenticacao.API
{
	public class AuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
	{
		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
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

				var identity = new ClaimsIdentity(context.Options.AuthenticationType);
				var roles = new List<string> {"User"};
				foreach (var role in roles)
				{
					identity.AddClaim(new Claim(ClaimTypes.Role,role));
				}

				GenericPrincipal principal = new GenericPrincipal(identity, roles.ToArray());
				Thread.CurrentPrincipal = principal;

				context.Validated(identity);
			}
			catch (Exception e)
			{
				context.SetError("invalid_grant","Falha ao Autenticar");
			}
		}

	}
}