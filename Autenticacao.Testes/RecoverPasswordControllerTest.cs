using Autenticacao.API.Controllers;
using Autenticacao.Domain.Interfaces.Repository;
using Autenticacao.Domain.Interfaces.Service;
using Moq;
using NUnit.Framework;

namespace Autenticacao.Testes
{
	[TestFixture]
	public class RecoverPasswordControllerTest
	{
		private MockRepository _repository;
		private Mock<IUsuarioService> _mockUsuarioService;
		private Mock<ICustomMessage> _mockCustomMessage;
		private Mock<IUsuarioRepository> _mockusuarioRepository;
		private RecoverPasswordController _recoverPasswordr;

		[SetUp]
		public void Setup()
		{
			_repository = new MockRepository(MockBehavior.Strict);
			_mockCustomMessage = _repository.Create<ICustomMessage>();
			_mockUsuarioService = _repository.Create<IUsuarioService>();
			_mockusuarioRepository = _repository.Create<IUsuarioRepository>();
			_recoverPasswordr = new RecoverPasswordController(_mockUsuarioService.Object);
		}

		[Test]
		public void RecuperarSenhaTeste()
		{
			var Email = "teste@teste.com";
			var Senha = "1234567890";

			_mockUsuarioService.Setup(a=>a.VerificarEmail(Email)).Returns(It.IsAny<bool>()).Verifiable();

			_recoverPasswordr = new RecoverPasswordController(_mockUsuarioService.Object);

			_recoverPasswordr.Index(Email);

			_repository.VerifyAll();
		}
	}
}