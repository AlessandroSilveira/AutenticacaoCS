using System;
using System.Collections.Generic;
using Autenticacao.API.Controllers;
using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Repository;
using Autenticacao.Domain.Interfaces.Service;
using Moq;
using NUnit.Framework;

namespace Autenticacao.Testes
{
	[TestFixture]
	public class ProfileControllerTestes
	{
		private MockRepository _repository;
		private Mock<IUsuarioService> _mockUsuarioService;
		private Mock<ICustomMessage> _mockCustomMessage;
		private Mock<IUsuarioRepository> _mockUsuarioRepository;
		private ProfileController _profileController;

		[SetUp]
		public void Setup()
		{
			_repository = new MockRepository(MockBehavior.Strict);
			_mockCustomMessage = _repository.Create<ICustomMessage>();
			_mockUsuarioService = _repository.Create<IUsuarioService>();
			_mockUsuarioRepository = _repository.Create<IUsuarioRepository>();
			_profileController = new ProfileController(_mockUsuarioService.Object, _mockCustomMessage.Object);
		}

		[Test]
		public void TestGet()
		{
			Guid id = new Guid("a566c99b-1ca7-48f9-a85e-68efd6ce2c2f");
			var usuario = new Usuario()
			{
				UsuarioId = id,
				Nome = "Ale",
				Senha = "1234567890",
				Email = "teste@teste.com",
				Token = "123",
				Telefones = new List<Telefone>()
			};
			
			_mockUsuarioService.Setup(a=>a.ObterPorId(id)).Returns(It.IsAny<Usuario>()).Verifiable();
			_mockUsuarioRepository.Setup(a=>a.ObterPorId(id)).Returns(It.IsAny<Usuario>()).Verifiable();
			_mockUsuarioService.Setup(a=>a.ObterToken(usuario)).Returns(It.IsAny<string>()).Verifiable();
		
			_mockUsuarioService.Setup(a=>a.Autenticar(usuario.Email,usuario.Senha)).Returns(It.IsAny<bool>()).Verifiable();

			_profileController.Get(id);

			_repository.VerifyAll();
		}
		[Test]
		public void TestValidaToken()
		{
			
			

			
		}

	}
}
