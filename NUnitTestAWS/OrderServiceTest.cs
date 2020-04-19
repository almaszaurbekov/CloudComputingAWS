using Common;
using DataAccess.JsonModels;
using DataAccess.Models;
using DataAccess.Services;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;
namespace NUnitTestAWS
{
    public class OrderServiceTest
    {
        private OrderService service { get; set; }

        [SetUp]
        public void Setup()
        {
            service = new OrderService();
        }

        [Test]
        public void GetOrderList()
        {
            var orders = service.Get();
            Assert.IsNotNull(orders);
        }

        [Test]
        public void InsertOrder()
        {
            const string userId = "31a7971d-aa5a-4dd1-a1e8-e50d68b8db7a";
            const string userName = "foxxychmoxy@gmail.com";

            const int productId = 1;
            const string productName = "Meat";
            const double productPrice = 2000;
            const int productCount = 2;

            const string createdBy = "system";

            var order = new Order()
            {
                UserId = userId,
                UserName = userName,
                ProductId = productId,
                ProductName = productName,
                ProductPrice = productPrice,
                ProductCount = productCount,
                CreatedBy = createdBy,
                CreatedDate = DateTime.UtcNow
            };

            var model = service.Insert(order);
            Assert.IsTrue(model.IsSuccess);
        }

        [Test]
        public void GetOrderById()
        {
            const string id = "5e9b8d8e55a9e84ab8654e05";
            const string productName = "Meat";

            var order = service.GetOrderById(id);
            Assert.AreEqual(productName, order.ProductName);
        }

        [Test]
        public void RemoveOrder()
        {
            const string id = "5e9b8d8e55a9e84ab8654e05";

            var result = service.Remove(id);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void UpdateOrder()
        {
            const string id = "5e9b8d8e55a9e84ab8654e05";

            var order = service.GetOrderById(id);

            order.UserName = "foxychmoxy";

            var result = service.Update(order);
            Assert.AreEqual(true, result);
        }
    }
}