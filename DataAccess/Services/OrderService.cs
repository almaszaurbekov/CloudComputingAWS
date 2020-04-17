using Common;
using DataAccess.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> orders;

        public OrderService()
        {
            var client = new MongoClient(ApiDestinations.MONGO_CLIENT);
            var database = client.GetDatabase(ApiDestinations.MONGO_DATABASE);
            orders = database.GetCollection<Order>("Orders");
        }

        public List<Order> Get() =>
            orders.Find(order => true).ToList();
    }
}