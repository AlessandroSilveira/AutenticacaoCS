using System;
using System.Collections.Generic;
using Autenticacao.API.Controllers;
using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Repository;
using Autenticacao.Domain.Interfaces.Service;
using Autenticacao.Infra.Data.Interfaces;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Autenticacao.Testes
{
	[TestFixture]
	public class SingUpControllerTest
	{
		private MockRepository _repository;
		private Mock<IUsuarioService> _mockUsuarioService;
		private Mock<IUnitOfWork> _mockUow;
		private Mock<ICustomMessage> _mockCustomMessage;
		private Mock<ICriptografia> _mockCriptografia;
		private Mock<IJwt> _mockJwt;
		private Mock<IUsuarioRepository> _mockUsuarioRepository;
		private SignUpController _signUpController;

		[SetUp]
		public void Setup()
		{
			_repository = new MockRepository(MockBehavior.Strict);
			_mockCustomMessage = _repository.Create<ICustomMessage>();
			_mockUsuarioService = _repository.Create<IUsuarioService>();
			_mockUow = _repository.Create<IUnitOfWork>();
			_mockCriptografia = _repository.Create<ICriptografia>();
			_mockJwt = _repository.Create<IJwt>();
			_mockUsuarioRepository = _repository.Create<IUsuarioRepository>();
			_signUpController = new SignUpController(_mockUsuarioService.Object, _mockCustomMessage.Object, _mockCriptografia.Object,_mockJwt.Object,_mockUow.Object);
		}

		[Test]
		public void RegisterTestError()
		{
			Guid id = new Guid();
			var usuario = new Usuario()
			{
				Nome = "Ale",
				Senha = "1234567890",
				Email = "teste@teste.com",
				Token = "123",
				Telefones = new List<Telefone>()
			};
			
			_mockUsuarioService.Setup(a=>a.VerificarEmail(usuario.Email)).Returns(It.IsAny<bool>()).Verifiable();
		
			_signUpController.Registrar(usuario);

			_repository.VerifyAll();

		}

		[Test]
		public void RegisterTest()
		{
			Guid id = new Guid("a566c99b-1ca7-48f9-a85e-68efd6ce2c2f");
			var usuario = new Usuario()
			{
				Nome = "Ale",
				Senha = "1234567890",
				Email = "teste@teste.com",
				Token = "123",
				Telefones = new List<Telefone>()
			};
			
			_mockJwt.Setup(a=>a.GenerateToken(usuario.Email)).Returns(It.IsAny<string>()).Verifiable();

			_mockCriptografia.Setup(a=>a.Hash(usuario.Senha)).Returns(It.IsAny<string>()).Verifiable();

			_mockUsuarioService.Setup(a => a.VerificarEmail(usuario.Email)).Returns(It.IsAny<bool>()).Verifiable();

			//_mockUsuarioService.Setup(a=>a.Adicionar(usuario)).Returns(It.IsAny<Usuario>()).Verifiable();

			_mockUow.Setup(a=>a.BeginTransaction()).Verifiable();


			//_mockUow.Setup(a => a.Commit()).Verifiable();

			_signUpController.Registrar(usuario);

		

			_repository.VerifyAll();

		}
	}
}
