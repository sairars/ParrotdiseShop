using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.Repositories;
using ParrotdiseShop.Web.Areas.Admin.Controllers.api;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ParrotdiseShop.Tests.Areas.Admin.Controllers.api
{
	public class ProductsControllerTests
	{
		private ProductsController _controller;
		private Mock<IMapper> _mockAutoMapper;
		private Mock<IWebHostEnvironment> _mockWebHostEnvironment;
		private Mock<IProductRepository> _mockProductsRepository;

		[SetUp]
		public void Setup()
		{
			var mockUOfWork = new Mock<IUnitOfWork>();

			_mockAutoMapper = new Mock<IMapper>();
			_mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
			_mockProductsRepository = new Mock<IProductRepository>();

			mockUOfWork
				.SetupGet(u => u.Products)
				.Returns(_mockProductsRepository.Object);

			_controller = new ProductsController(mockUOfWork.Object,
													_mockAutoMapper.Object,
													_mockWebHostEnvironment.Object);
		}

		[Test]
		public void GetProducts_WhenCalled_ShouldReturnOKResult()
		{
			var products = new List<Product>
			{
				new Product { Id = 1 },
				new Product { Id = 2 }
			};

			var productDtos = new List<ProductDto> 
			{ 
				new ProductDto { Id = 1 },
				new ProductDto { Id = 2 } 
			};

			_mockProductsRepository.Setup(p => p.GetAllProductsWithCategory()).Returns(products);

			var result = _controller.GetProducts();

			result.Should().BeOfType<OkObjectResult>();
		}

		[Test]
		public void Delete_NoProductWithGivenIdExists_ShouldReturnNotFound()
		{
			var result = _controller.Delete(1);

			result.Should().BeOfType<NotFoundObjectResult>();
		}

		[Test]
		public void Delete_ProductIsFound_ShouldReturnOK()
		{
			var product = new Product { Id = 1, ImagePath = "test.png" };

			_mockWebHostEnvironment.Setup(e => e.WebRootPath).Returns("testpath");
			_mockProductsRepository.Setup(p => p.Get(It.IsAny<Expression<Func<Product, bool>>>()))
				.Returns(product);
			
			var result = _controller.Delete(1);

			result.Should().BeOfType<OkObjectResult>();
		}
	}
}
