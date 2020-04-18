using System;
using System.Collections.Generic;
using AutoMapper;
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
        private readonly IMapper mapper;

        public OrderController(OrderService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
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

        [HttpPost]
        public OkObjectResult Insert(OrderJsonModel model)
        {
            try
            {
                var order = mapper.Map<OrderJsonModel, Order>(model);
                service.Insert(order);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return Ok(new OrderJsonModel(false, ex.Message));
            }
        }
    }
}
