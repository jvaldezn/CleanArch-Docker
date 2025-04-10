using System.Data;
using Application.DTOs;
using Application.Interface;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Configuration.Context;
using Infrastructure.Interface;
using Infrastructure.Messaging.Contract;
using Infrastructure.Messaging.Publisher;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using Transversal.Common;
using Transversal.Common.Interfaces;

namespace Application.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IUnitOfWork<AppDbContext>> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<ILogger<ProductService>> _mockLogger;
        private readonly IProductService _productService;
        private readonly Mock<IEventPublisher> _mockEventPublisher;

        public ProductServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork<AppDbContext>>();
            _mockMapper = new Mock<IMapper>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockLogger = new Mock<ILogger<ProductService>>();
            _mockEventPublisher = new Mock<IEventPublisher>();
            _productService = new ProductService(_mockUnitOfWork.Object, _mockMapper.Object, _mockProductRepository.Object, _mockLogger.Object, _mockEventPublisher.Object);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsEmptyList()
        {
            _mockProductRepository.Setup(u => u.GetAllAsync()).ReturnsAsync(new List<Product>());
            var result = await _productService.GetAllProducts();

            Assert.True(result.IsSuccess);
            Assert.Equal(Messages.NoProductsFound, result.Message);
        }

        [Fact]
        public async Task GetProductById_ReturnsProduct()
        {
            var product = new Product { Id = 1, Name = "Test Product", Price = 100, Stock = 5 };
            var productDto = new ProductDto { Id = 1, Name = "Test Product", Price = 100, Stock = 5 };

            _mockMapper.Setup(m => m.Map<ProductDto>(product)).Returns(productDto);
            _mockProductRepository.Setup(u => u.GetByIdAsync(1)).ReturnsAsync(product);

            var result = await _productService.GetProductById(1);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(productDto.Name, result.Data?.Name);
        }

        [Fact]
        public async Task CreateProduct_ReturnsSuccess()
        {
            var productDto = new ProductDto { Name = "Test Product", Price = 100, Stock = 5 };
            var product = new Product { Id = 1, Name = "Test Product", Price = 100, Stock = 5 };

            var mockTransaction = new Mock<IDbContextTransaction>();

            _mockUnitOfWork.Setup(u => u.BeginTransactionAsync()).ReturnsAsync(mockTransaction.Object);
            _mockMapper.Setup(m => m.Map<Product>(productDto)).Returns(product);
            _mockProductRepository.Setup(r => r.AddAsync(product)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.SaveAsync()).ReturnsAsync(1);
            _mockMapper.Setup(m => m.Map<ProductDto>(product)).Returns(() => { productDto.Id = 1; return productDto; });

            var result = await _productService.CreateProduct(productDto);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.Id);
            Assert.Equal(Messages.ProductCreated, result.Message);

            _mockUnitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);
            mockTransaction.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);

            _mockEventPublisher.Verify(p => p.Publish(It.IsAny<LogCreatedEvent>()), Times.Once);
        }
    }
}