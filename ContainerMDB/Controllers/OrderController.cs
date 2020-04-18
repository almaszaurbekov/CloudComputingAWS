using System.Collections.Generic;
using DataAccess.Context;
using DataAccess.JsonModels;
using DataAccess.Models;
using DataAccess.Services;
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
        private readonly OrderService service;

        public OrderController(OrderService service)
        {
            this.service = service;
        }


        [HttpGet]
        public OkObjectResult Get()
        {
            var orders = service.Get();
            if (orders.Count > 0)
            {
                var model = new OrderListJsonModel(orders);
                return Ok(model);
            }
            return Ok(new OrderListJsonModel(false, "Order list is empty"));
        }
    }
}
