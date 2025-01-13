using ConsoleApp10.Interfaces;
using ConsoleApp10.Models;
using ConsoleApp10.Services;
using Moq;
using NUnit.Framework;
using System;

namespace ConsoleApp10.Tests
{
    public class OrderServiceTests
    {
        private Mock<IOrderRepository> _mockOrderRepository;
        private Mock<IProductRepository> _mockProductRepository;
        private OrderService _orderService;

        [SetUp]
        public void Setup()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockProductRepository = new Mock<IProductRepository>();
            _orderService = new OrderService(_mockOrderRepository.Object, _mockProductRepository.Object);
        }

        [Test]
        public void PlaceOrder_SufficientStock_ShouldPlaceOrderAndUpdateStock()
        {    
            var product = new Product { ProductId = 1, Name = "Product1", Stock = 10 };
            var order = new Order { ProductId = 1, Quantity = 2, OrderDate = DateTime.Now };

            _mockProductRepository.Setup(service => service.GetProductById(order.ProductId)).Returns(product);
          
            _orderService.PlaceOrder(order);

            _mockOrderRepository.Verify(repo => repo.PlaceOrder(order), Times.Once);
            _mockProductRepository.Verify(service => service.UpdateProduct(It.Is<Product>(p => p.Stock == 8)), Times.Once);
        }

        [Test]
        public void PlaceOrder_InsufficientStock_ShouldThrowInvalidOperationException()
        {
            var product = new Product { ProductId = 1, Name = "Product1", Stock = 1 };
            var order = new Order { ProductId = 1, Quantity = 2, OrderDate = DateTime.Now };

            _mockProductRepository.Setup(service => service.GetProductById(order.ProductId)).Returns(product);

            Assert.Throws<InvalidOperationException>(() => _orderService.PlaceOrder(order));
            _mockOrderRepository.Verify(repo => repo.PlaceOrder(It.IsAny<Order>()), Times.Never);
        }
    }
}
