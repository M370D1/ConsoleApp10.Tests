using NUnit.Framework;
using ConsoleApp10.Repositories;
using ConsoleApp10.Models;
using ConsoleApp10.Services;

namespace ConsoleApp10.Tests
{
    public class OrderRepositoryTests
    {
        private OrderRepository _orderRepository;
        private ProductRepository _productRepository;
        private string _connectionString = ConnectionConfig.DefaultConnection;
        private OrderService _orderService;

        [SetUp]
        public void Setup()
        {
            _orderRepository = new OrderRepository(_connectionString);
            _productRepository = new ProductRepository(_connectionString);
            _orderService = new OrderService(_orderRepository, _productRepository);
        }

        [Test]
        public void PlaceOrder_ShouldUpdateStock()
        {
            
            var product = new Product { ProductId = 1, Name = "Product1", Stock = 10, Price = 50.0m };
            var order = new Order { ProductId = 1, Quantity = 2, OrderDate = DateTime.Now };

            _productRepository.UpdateProduct(product);

            
            _orderService.PlaceOrder(order);

            
            var updatedProduct = _productRepository.GetProductById(product.ProductId);
            Assert.That(6, Is.EqualTo(updatedProduct.Stock)); 
        }
    }
}
