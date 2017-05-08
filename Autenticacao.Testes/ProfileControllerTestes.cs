using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Autenticacao.API.Controllers;
using Autenticacao.Domain.Interfaces.Service;
using Moq;
using NUnit.Framework;

namespace Autenticacao.Testes
{
	[TestFixture]
	class ProfileControllerTestes
	{
		private MockRepository _repository;
		private Mock<IUsuarioService> _mockUsuarioService;
		private Mock<ICustomMessage> _mockCustomMessage;
		
		private ProfileController _profileController;

		[SetUp]
		public void Setup()
		{
			_repository = new MockRepository(MockBehavior.Strict);
			_mockCustomMessage = _repository.Create<ICustomMessage>();
			_mockUsuarioService = _repository.Create<IUsuarioService>();
			_profileController = new ProfileController(_mockUsuarioService.Object, _mockCustomMessage.Object);
			
		}

		//[Test]
		//public void TestGet()
		//{
		//	Guid UsuarioId = new Guid();
			
		//	var Request = new HttpRequestMessage();
		//	var headers = Request.Headers;

		//	var token = _profileController.GetToken(headers);
		//	_profileController.ValidateToken(UsuarioId.ToString(), token);

		//	_profileController.Get(UsuarioId.ToString());

		//}
		[Test]
		public void TestValidaToken()
		{
			
			

			
		}

	}
}
