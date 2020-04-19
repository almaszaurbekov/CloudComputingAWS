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
    public class PostService
    {
        private readonly IMongoCollection<Post> posts;

        public PostService()
        {
            var client = new MongoClient(ApiDestinations.MONGO_CLIENT);
            var database = client.GetDatabase(ApiDestinations.MONGO_DATABASE);
            posts = database.GetCollection<Post>("Posts");
        }

        public List<Post> Get() => posts.Find(post => true).ToList();

        public Post GetPostById(string id)
        {
            try
            {
                var post = this.posts
                    .Find(new BsonDocument("_id", new ObjectId(id)))
                    .FirstOrDefault();
                return post;
            }
            catch
            {
                return null;
            }
        }

        public PostJsonModel Insert(Post order)
        {
            try
            {
                posts.InsertOne(order);
                return new PostJsonModel()
                {
                    Id = order.Id,
                    UserId = order.UserId
                };
            }
            catch (Exception ex)
            {
                return new PostJsonModel(false, ex.Message);
            }
        }

        public bool Update(Post post)
        {
            try
            {
                var result = posts.ReplaceOne(new BsonDocument("_id", new ObjectId(post.Id)), post);
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
                var result = posts.DeleteOne(new BsonDocument("_id", new ObjectId(id)));
                return result.IsAcknowledged;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}