using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Autenticacao.API.Controllers;
using Autenticacao.API.Models;
using Autenticacao.Domain.Interfaces.Service;
using Autenticacao.Domain.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Autenticacao.Testes
{
	[TestFixture]
	public class LoginControllerTestes
	{
		private MockRepository _repository;
		private Mock<IUsuarioService> _mockUsuarioService;
		
		private Mock<ICriptografia> _mockCriptografia;
		private LoginController _loginController;

		[SetUp]
		public void Setup()
		{
			_repository = new MockRepository(MockBehavior.Strict);
			_mockCriptografia = _repository.Create<ICriptografia>();
		
			_mockUsuarioService = _repository.Create<IUsuarioService>();
			_loginController = new LoginController(_mockUsuarioService.Object, _mockCriptografia.Object );
		}

		[Test]
		public void LoginAutenticar()
		{
			//Arrange
			var login = new Login()
			{
				Senha = "1234567890",
				Email = "teste@teste.com"
			};

			
			_mockUsuarioService.Setup(a=>a.VerificarEmail(login.Email)).Returns(It.IsAny<bool>()).Verifiable();
			

			//Act
			//_loginController.Autenticar(login);

			//Assert
			_repository.VerifyAll();

		}
	}
}