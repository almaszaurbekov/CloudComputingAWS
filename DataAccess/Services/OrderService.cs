using Common;
using DataAccess.JsonModels;
using DataAccess.Models;
using MongoDB.Bson;
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

        public List<Order> Get() => orders.Find(order => true).ToList();

        public Order GetOrderById(string id)
        {
            try
            {
                var order = this.orders
                    .Find(new BsonDocument("_id", new ObjectId(id)))
                    .FirstOrDefault();
                return order;
            }
            catch
            {
                return null;
            }
        } 

        public OrderJsonModel Insert(Order order)
        {
            try
            {
                orders.InsertOne(order);
                return new OrderJsonModel()
                {
                    Id = order.Id,
                    ProductId = order.ProductId,
                    UserId = order.UserId
                };
            }
            catch (Exception ex)
            {
                return new OrderJsonModel(false, ex.Message);
            }
        }

        public bool Update(Order order)
        {
            try
            {
                var result = orders.ReplaceOne(new BsonDocument("_id", new ObjectId(order.Id)), order);
                return result.IsAcknowledged;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Remove(string id)
        {
            try
            {
                var result = orders.DeleteOne(new BsonDocument("_id", new ObjectId(id)));
                return result.IsAcknowledged;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}