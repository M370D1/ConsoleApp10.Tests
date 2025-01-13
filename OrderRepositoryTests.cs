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
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            _orderRepository = new OrderRepository(_connectionString);
            _productRepository = new ProductRepository(_connectionString);
            _productService = new ProductService(_productRepository);
            _orderService = new OrderService(_orderRepository, _productRepository);
        }

        [Test]
        public void PlaceOrder_ShouldUpdateStock()
        {
            const int productId = 3; // Specify productId that you want to order
            const int orderQuantity = 2; // Specify order quantity
            const int quantityLeft = 8; // Specify quantity that will left after the order

            var newOrder = new Order { ProductId = productId, Quantity = orderQuantity, OrderDate = DateTime.Now };
            _orderService.PlaceOrder(newOrder);

            var getProductById = _productService.GetProductById(productId);

            Assert.That(quantityLeft, Is.EqualTo(getProductById.Stock));

            _productService.DeleteProduct(productId);
        }
    }
}
