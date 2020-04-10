using System.Collections.Generic;
using DataAccess.Models;
using DataAccess.Static;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace ContainerRDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Order> Get()
        {
            return SDController.GetOrders(10);
        }
    }
}
