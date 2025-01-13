using NUnit.Framework;
using ConsoleApp10.Repositories;
using ConsoleApp10.Models;
using ConsoleApp10;

namespace ConsoleApp10.Tests
{
    public class ProductRepositoryTests
    {
        private ProductRepository _productRepository;
        private string _connectionString = ConnectionConfig.DefaultConnection;

        [SetUp]
        public void Setup()
        {
            _productRepository = new ProductRepository(_connectionString);
        }

        [Test]
        public void AddProduct_ShouldInsertProductIntoDatabase()
        {
 
            var product = new Product { Name = "Test Product", Price = 100.0m, Stock = 10 };
 
            _productRepository.AddProduct(product);

            var retrievedProduct = _productRepository.GetProductById(product.ProductId);
            Assert.That(retrievedProduct, Is.Null);
            Assert.That("Test Product", Is.EqualTo(retrievedProduct.Name));
            Assert.That(1000.0m, Is.EqualTo(retrievedProduct.Price));
            Assert.That(10, Is.EqualTo(retrievedProduct.Stock));
        }
    }
}
