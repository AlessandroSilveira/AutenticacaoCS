﻿using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(Autenticacao.API.Startup))]

namespace Autenticacao.API
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var config = new HttpConfiguration();

			ConfigureWebApi(config);
			
		}

		public static void ConfigureWebApi(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			var formatters = config.Formatters;
			formatters.Clear();
			formatters.Add(new JsonMediaTypeFormatter());

			var jsonFormatter = formatters.JsonFormatter;
			var settings = jsonFormatter.SerializerSettings;
			settings.Formatting = Formatting.Indented;
			settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}

	

		

		public void ConfigureOAuth(IAppBuilder app)
		{
			var oAuthServerOptions = new OAuthAuthorizationServerOptions()
			{
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString("/token"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
				Provider = null // new SimpleAuthorizationServerProvider()
			};

			// Token Generation
			app.UseOAuthAuthorizationServer(oAuthServerOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
		}
	}
}
