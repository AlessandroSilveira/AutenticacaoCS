using Autenticacao.Application;
using Autenticacao.Application.Interfaces;
using Autenticacao.Domain.Interfaces.Repository;
using Autenticacao.Domain.Interfaces.Service;
using Autenticacao.Domain.Services;
using Autenticacao.Infra.Data.Repository;
using SimpleInjector;

namespace Autenticacao.Infra.CrossCutting.IoC
{
	public class BootStrapper
	{
		public static void RegisterServices(Container container)
		{
			container.Register<IUsuarioAppService,UsuarioAppService>(Lifestyle.Scoped);
			container.Register<ITelefoneAppService, TelefoneAppService>(Lifestyle.Scoped);

			container.Register<IUsuarioService, UsuarioService>(Lifestyle.Scoped);
			container.Register<ITelefoneService, TelefoneService>(Lifestyle.Scoped);

			container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Scoped);
			container.Register<ITelefoneRepository, TelefoneRepository>(Lifestyle.Scoped);

			container.Register(typeof(IRepository<>),typeof(Repository<>));
		}
	}
}