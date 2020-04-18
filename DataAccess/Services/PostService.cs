using Common;
using DataAccess.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Services
{
    public class PostService
    {
        private readonly IMongoCollection<Post> posts;

        public PostService()
        {
            var client = new MongoClient(ApiDestinations.MONGO_CLIENT);
            var database = client.GetDatabase(ApiDestinations.MONGO_DATABASE);
            posts = database.GetCollection<Post>("Orders");
        }

        public List<Post> Get() =>
            posts.Find(post => true).ToList();
    }
}
