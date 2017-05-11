using System;
using System.Collections.Generic;
using System.Linq;
using Autenticacao.Domain.Entities;
using Autenticacao.Domain.Interfaces.Repository;
using Autenticacao.Infra.Data.Context;
using Moq;
using NUnit.Framework;

namespace Autenticacao.Testes
{
	///<summary>
	/// Summary description for UnitTest1
	///</summary>
	[TestFixture]
	public class UnitTest1
	{
		///<summary>
		/// Constructor
		///</summary>
		public UnitTest1()
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

			// Mock the Products Repository using Moq
			Mock<IUsuarioRepository> mockUsuarioRepository = new Mock<IUsuarioRepository>();

			// return a product by Id
			mockUsuarioRepository.Setup(mr => mr.ObterPorId(id)).Returns((Guid i) => usuarios.Single(x => x.UsuarioId == i));

			// Complete the setup of our Mock Product Repository
			this.MockUsuarioRepository = mockUsuarioRepository.Object;
		}

		///<summary>
		/// Gets or sets the test context which provides
		/// information about and functionality for the current test run.
		///</summary>
		public AutenticacaoContext Context { get; set; }

		///<summary>
		/// Our Mock Products Repository for use in testing
		///</summary>
		public readonly IUsuarioRepository MockUsuarioRepository;

		///<summary>
		/// Can we return a product By Id?
		///</summary>
		[Test]
		public void CanReturnProductById()
		{
			Guid id = new Guid("a566c99b-1ca7-48f9-a85e-68efd6ce2c2f");
			// Try finding a product by id
			Usuario testUsuario = this.MockUsuarioRepository.ObterPorId(id);

			Assert.IsNotNull(testUsuario); // Test if null
			//Assert.IsInstanceOfType(testUsuario, typeof(Usuario)); // Test type
			Assert.AreEqual("Ale", testUsuario.Nome); // Verify it is the right product
		}

		///<summary>
		/// Can we return a product By Name?
		///</summary>
		//[TestMethod]
		//public void CanReturnProductByName()
		//{
		//	// Try finding a product by Name
		//	Product testProduct = this.MockProductsRepository.FindByName("Silverlight Unleashed");

		//	Assert.IsNotNull(testProduct); // Test if null
		//	Assert.IsInstanceOfType(testProduct, typeof(Product)); // Test type
		//	Assert.AreEqual(3, testProduct.ProductId); // Verify it is the right product
		//}

		///<summary>
		/// Can we return all products?
		///</summary>
		//[TestMethod]
		//public void CanReturnAllProducts()
		//{
		//	// Try finding all products
		//	IList<Product> testProducts = this.MockProductsRepository.FindAll();

		//	Assert.IsNotNull(testProducts); // Test if null
		//	Assert.AreEqual(3, testProducts.Count); // Verify the correct Number
		//}

		///<summary>
		/// Can we insert a new product?
		///</summary>
		//[TestMethod]
		//public void CanInsertProduct()
		//{
		//	// Create a new product, not I do not supply an id
		//	Product newProduct = new Product
		//	{ Name = "Pro C#", Description = "Short description here", Price = 39.99 };

		//	int productCount = this.MockProductsRepository.FindAll().Count;
		//	Assert.AreEqual(3, productCount); // Verify the expected Number pre-insert

		//	// try saving our new product
		//	this.MockProductsRepository.Save(newProduct);

		//	// demand a recount
		//	productCount = this.MockProductsRepository.FindAll().Count;
		//	Assert.AreEqual(4, productCount); // Verify the expected Number post-insert

		//	// verify that our new product has been saved
		//	Product testProduct = this.MockProductsRepository.FindByName("Pro C#");
		//	Assert.IsNotNull(testProduct); // Test if null
		//	Assert.IsInstanceOfType(testProduct, typeof(Product)); // Test type
		//	Assert.AreEqual(4, testProduct.ProductId); // Verify it has the expected productid
		//}

		///<summary>
		/// Can we update a prodict?
		///</summary>
		//[TestMethod]
		//public void CanUpdateProduct()
		//{
		//	// Find a product by id
		//	Product testProduct = this.MockProductsRepository.FindById(1);

		//	// Change one of its properties
		//	testProduct.Name = "C# 3.5 Unleashed";

		//	// Save our changes.
		//	this.MockProductsRepository.Save(testProduct);

		//	// Verify the change
		//	Assert.AreEqual("C# 3.5 Unleashed", this.MockProductsRepository.FindById(1).Name);
		//}
	}
}