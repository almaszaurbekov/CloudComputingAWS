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

        [HttpGet("{id}")]
        public OkObjectResult GetOrderById(string id)
        {
            var order = service.GetOrderById(id);
            if (order != null)
            {
                var model = mapper.Map<Order, OrderJsonModel>(order);
                return Ok(model);
            }
            return Ok(new OrderJsonModel(false, "No order with this id"));
        }

        [HttpPost]
        public OkObjectResult AddOrder(OrderJsonModel model)
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

        [HttpPut("{id}")]
        public OkObjectResult UpdateOrder(string id, OrderJsonModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return Ok(new OrderJsonModel(false, "Bad request"));
                }

                var order = mapper.Map<OrderJsonModel, Order>(model);
                var result = service.Update(order);
                if (result)
                {
                    return Ok(model);
                }
                return Ok(new OrderJsonModel(false, "Something went wrong"));
            }
            catch (Exception ex)
            {
                return Ok(new OrderJsonModel(false, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public OkObjectResult DeleteOrder(string id)
        {
            var order = service.GetOrderById(id);
            if (order == null)
            {
                Ok(new OrderJsonModel(false, "Not found"));
            }

            var result = service.Remove(id);
            if (result)
            {
                return Ok(mapper.Map<Order, OrderJsonModel>(order));
            }

            return Ok(new OrderJsonModel(false, "Something went wrong"));
        }
    }
}
