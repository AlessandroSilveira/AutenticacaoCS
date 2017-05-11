using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Reflection;
using Autenticacao.Application;
using Autenticacao.Application.Interfaces;
using Autenticacao.Domain.Interfaces.Repository;
using Autenticacao.Domain.Interfaces.Service;
using Autenticacao.Domain.Services;
using Autenticacao.Infra.Data.Repository;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;

[assembly: OwinStartup(typeof(Autenticacao.API.Startup))]

namespace Autenticacao.API
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var config = new HttpConfiguration();
			ConfigureWebApi(config);
			ConfigureOAuth(app);
			app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
			app.UseWebApi(config);
			app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);
		}

		private static StandardKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			kernel.Load(Assembly.GetExecutingAssembly());
			RegisterServices(kernel);
			GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
			return kernel;
		}
		private static void RegisterServices(StandardKernel kernel)
		{
			kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
			kernel.Bind<IUsuarioAppService>().To<UsuarioAppService>();
			kernel.Bind<IUsuarioService>().To<UsuarioService>().InTransientScope();
			kernel.Bind<IUsuarioRepository>().To<UsuarioRepository>();
			kernel.Bind<ITelefoneAppService>().To<TelefoneAppService>();
			kernel.Bind<ITelefoneService>().To<TelefoneService>();
			kernel.Bind<ITelefoneRepository>().To<TelefoneRepository>();
			kernel.Bind<ICriptografia>().To<Criptografia>();
			kernel.Bind<IJwt>().To<Jwt>().InRequestScope();
			kernel.Bind<IConfiguration>().To<Configuration>();
			kernel.Bind<ICustomMessage>().To<CustomMessage>();
			kernel.Bind<IEmailBuilder>().To<EmailBuilder>();
			kernel.Bind<IEmailSender>().To<EnviaEmailBuilder>();
			kernel.Bind<IGerenciadorEmail>().To<GerenciadorEmail>();
			kernel.Bind<IHttpActionResult>().To<CustomMessage>();
			kernel.Bind<HttpStatusCode>();
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
			OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
			{
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString("/api/token"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
				Provider = new AuthAuthorizationServerProvider()
			};

			// Token Generation
			app.UseOAuthAuthorizationServer(oAuthServerOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
		}
	}
}
