using ConsoleApp10.Interfaces;
using ConsoleApp10.Models;
using ConsoleApp10.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp10.Tests
{
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _mockProductRepository;
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockProductRepository.Object);
        }

        [Test]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, Name = "Product1", Price = 10.0m, Stock = 5 },
                new Product { ProductId = 2, Name = "Product2", Price = 20.0m, Stock = 3 }
            };
            _mockProductRepository.Setup(repo => repo.GetAllProducts()).Returns(products);

            // Act
            var result = _productService.GetAllProducts();

            // Assert
            Assert.That(2, Is.EqualTo(result.Count()));
            Assert.That("Product1", Is.EqualTo(result.First().Name));
            //Assert.AreEqual(2, result.Count());
            //Assert.AreEqual("Product1", result.First().Name);
        }

        [Test]
        public void AddProduct_ValidProduct_ShouldCallRepositoryAdd()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "New Product", Price = 15.0m, Stock = 10 };

            // Act
            _productService.AddProduct(product);

            // Assert
            _mockProductRepository.Verify(repo => repo.AddProduct(It.Is<Product>(p => p.Name == "New Product")), Times.Once);
        }

        [Test]
        public void AddProduct_NegativeStock_ShouldThrowArgumentException()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Invalid Product", Price = 15.0m, Stock = -5 };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _productService.AddProduct(product));
        }

        [Test]
        public void UpdateProduct_ValidProduct_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Updated Product", Price = 25.0m, Stock = 8 };
            _mockProductRepository.Setup(repo => repo.GetProductById(product.ProductId)).Returns(product);

            // Act
            _productService.UpdateProduct(product);

            // Assert
            _mockProductRepository.Verify(repo => repo.UpdateProduct(product), Times.Once);
        }
    }
}
