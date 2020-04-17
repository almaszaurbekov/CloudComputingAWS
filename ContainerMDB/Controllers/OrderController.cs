using System.Collections.Generic;
using DataAccess.Context;
using DataAccess.JsonModels;
using DataAccess.Models;
using DataAccess.Static;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace ContainerMDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private MdbContext mongo { get; set; }

        public OrderController()
        {
            mongo = new MdbContext();
        }

        [HttpGet]
        public OkObjectResult Get()
        {
            var orders = mongo.Orders.Find(_ => true).ToList();
            if (orders.Count > 0)
            {
                var model = new OrderListJsonModel(orders);
                return Ok(model);
            }
            return Ok(new OrderListJsonModel(false, "Order list is empty"));
        }
    }
}
