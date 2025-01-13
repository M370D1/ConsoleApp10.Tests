using NUnit.Framework;
using ConsoleApp10.Repositories;
using ConsoleApp10.Models;
using ConsoleApp10;
using ConsoleApp10.Services;

namespace ConsoleApp10.Tests
{
    public class ProductRepositoryTests
    {
        private ProductRepository _productRepository;
        private ProductService _productService;
        private string _connectionString = ConnectionConfig.DefaultConnection;

        [SetUp]
        public void Setup()
        {
            _productRepository = new ProductRepository(_connectionString);
            _productService = new ProductService(_productRepository);
        }

        [Test]
        public void AddProduct_ShouldInsertProductIntoDatabase()
        {
            const int productId = 5; // Specify productId that will be validated for existing

            var product = new Product { Name = "Test Product", Price = 100.0m, Stock = 10 };
            _productService.AddProduct(product);

            var retrievedProduct = _productService.GetProductById(productId);
            Assert.That(retrievedProduct, Is.Not.Null);
            Assert.That("Test Product", Is.EqualTo(retrievedProduct.Name));
            Assert.That(100.0m, Is.EqualTo(retrievedProduct.Price));
            Assert.That(10, Is.EqualTo(retrievedProduct.Stock));
        }
    }
}
