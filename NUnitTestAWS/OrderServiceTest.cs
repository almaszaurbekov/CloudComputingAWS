using DataAccess.Services;
using NUnit.Framework;

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
            Assert.IsEmpty(orders);
        }

    }
}