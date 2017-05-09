using System.Net;
using Autenticacao.Application;
using Autenticacao.Application.Interfaces;
using Autenticacao.Domain.Interfaces.Repository;
using Autenticacao.Domain.Interfaces.Service;
using Autenticacao.Domain.Services;
using Autenticacao.Infra.Data.Context;
using Autenticacao.Infra.Data.Interfaces;
using Autenticacao.Infra.Data.Repository;
using Autenticacao.Infra.Data.UoW;
using SimpleInjector;

namespace Autenticacao.Infra.CrossCutting.IoC
{
	public class BootStrapper
	{
		public static void RegisterServices(Container container)
		{
			HttpStatusCode StatusCode = HttpStatusCode.Unauthorized;
			string Message = "teste"; 
			container.Register<IUsuarioAppService,UsuarioAppService>(Lifestyle.Scoped);
			container.Register<ITelefoneAppService, TelefoneAppService>(Lifestyle.Scoped);
			container.Register<IUsuarioService, UsuarioService>(Lifestyle.Scoped);
			container.Register<ITelefoneService, TelefoneService>(Lifestyle.Scoped);
			container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Scoped);
			container.Register<ITelefoneRepository, TelefoneRepository>(Lifestyle.Scoped);
			container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
			container.Register<ICriptografia, Criptografia>(Lifestyle.Scoped);
			container.Register<IJwt,Jwt>(Lifestyle.Scoped);
			container.Register<IConfiguration,Configuration>(Lifestyle.Scoped);
			container.Register<ICustomMessage>(()=> new CustomMessage(StatusCode,Message));
			container.Register<AutenticacaoContext>(Lifestyle.Scoped);
			container.Register(typeof(IRepository<>),typeof(Repository<>));
		}
	}
}