using DataAccess.Models;
using MongoDB.Driver;
namespace DataAccess.Context
{
    public class MdbContext
    {
        private readonly IMongoDatabase database;
        public MdbContext()
        {
            var server = new MongoServerAddress("ec2-52-28-121-13.eu-central-1.compute.amazonaws.com", 27017);
            database = new MongoClient("mongodb://foxychmoxy:123qweAS1!@52.28.121.13").GetDatabase("development");
        }

        public IMongoCollection<Order> Orders
        {
            get
            {
                return database.GetCollection<Order>("Orders");
            }
        }

        public IMongoCollection<Post> Posts
        {
            get
            {
                return database.GetCollection<Post>("Posts");
            }
        }
    }
}
