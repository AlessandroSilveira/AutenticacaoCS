using Autenticacao.API.Controllers;
using Autenticacao.Domain.Interfaces.Service;
using Moq;
using NUnit.Framework;

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
			var Senha = "1234567890";
			var Email = "teste@teste.com";
			
			_mockUsuarioService.Setup(a=>a.VerificarEmail(Email)).Returns(It.IsAny<bool>()).Verifiable();
			

			//Act
			_loginController.Autenticar(Email,Senha);

			//Assert
			_repository.VerifyAll();
		}
	}
}