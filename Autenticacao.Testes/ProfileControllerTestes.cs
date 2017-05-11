using System;
using System.Collections.Generic;
using System.Linq;
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
			Guid id2 = new Guid("c3158adf-158e-407d-9c16-6f3cbab8a524");
			Guid id3 = new Guid("48839961-0d93-4556-9408-cc7007d64125");

			// create some mock products to play with
			IList<Usuario> usuarios = new List<Usuario>
			{

			new Usuario()
				{
					UsuarioId =id,
					Nome = "Ale",
					Senha = "1234567890",
					Email = "teste@teste.com",
					Token = "123",
					Telefones = new List<Telefone>()
				},
				new Usuario()
				{
					UsuarioId =id2,
					Nome = "Ale2",
					Senha = "1234567890",
					Email = "teste1@teste.com",
					Token = "123",
					Telefones = new List<Telefone>()
				},
				new Usuario()
				{
					UsuarioId =id3,
					Nome = "Ale3",
					Senha = "1234567890",
					Email = "teste3@teste.com",
					Token = "123",
					Telefones = new List<Telefone>()
				}

			};

			_mockUsuarioService.Setup(a => a.ObterPorId(id)).Returns((Guid i) => usuarios.Single(x => x.UsuarioId == i));
			
			_mockUsuarioService.Setup(a=>a.ObterToken(It.IsAny<Usuario>())).Returns(It.IsAny<string>()).Verifiable();
		
			_mockUsuarioService.Setup(a=>a.Autenticar(It.IsAny<string>(),It.IsAny<string>())).Returns(It.IsAny<bool>()).Verifiable();

			//_mockUsuarioRepository.Setup(a => a.ObterPorId(id)).Returns((Guid i) => usuarios.Single(x => x.UsuarioId == i));

			_profileController.Get(id);

			_repository.VerifyAll();
		}
		[Test]
		public void TestValidaToken()
		{
			
			

			
		}

	}
}
