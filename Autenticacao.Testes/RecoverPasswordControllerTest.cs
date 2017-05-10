using System;
using System.Collections.Generic;
using System.Net;
using Autenticacao.API.Controllers;
using Autenticacao.API.Models;
using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Repository;
using Autenticacao.Domain.Interfaces.Service;
using Autenticacao.Infra.Data.Context;
using Autenticacao.Infra.Data.Repository;
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
			_recoverPasswordr = new RecoverPasswordController(_mockUsuarioService.Object, _mockCustomMessage.Object);
		}

		[Test]
		public void RecuperarSenhaTeste()
		{
			var login = new Login()
			{
				Email = "teste@teste.com",
				Senha = "1234567890"
			};

			_mockUsuarioService.Setup(a=>a.VerificarEmail(login.Email)).Returns(It.IsAny<bool>()).Verifiable();

			_mockCustomMessage.Setup(a=>a.Create(HttpStatusCode.Unauthorized,It.IsAny<string>())).Returns(It.IsAny<CustomMessage>()).Verifiable();

			_recoverPasswordr = new RecoverPasswordController(_mockUsuarioService.Object, _mockCustomMessage.Object);

			_recoverPasswordr.RecuperarSenha(login);

			_repository.VerifyAll();
		}

		[Test]
		public void NovaSenhaTeste()
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
			
			_mockUsuarioService.Setup(a => a.ObterPorId(id)).Returns(It.IsAny<Usuario>()).Verifiable();

			_mockUsuarioService.Setup(a => a.ObterToken(usuario)).Returns(It.IsAny<string>()).Verifiable();

			_mockCustomMessage.Setup(a => a.Create(HttpStatusCode.Unauthorized, It.IsAny<string>())).Returns(It.IsAny<CustomMessage>()).Verifiable();

			_mockUsuarioService.Setup(a => a.NovaSenha(usuario)).Returns(It.IsAny<Usuario>()).Verifiable();

			_recoverPasswordr.NovaSenha(usuario.Token, usuario.UsuarioId.ToString(), usuario.Senha);

			_repository.VerifyAll();
		}

	}
}