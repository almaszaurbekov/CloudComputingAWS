using DataAccess.Models;
using System;
using System.Collections.Generic;
namespace DataAccess.Static
{
    public class SDController
    {
        /// <summary>
        /// Get user list
        /// </summary>
        /// <param name="userCount">minimal user count</param>
        public static List<User> GetUsers(int userCount) => GenerateUserList(userCount);

        private static List<User> GenerateUserList(int userCount)
        {
            List<User> items = new List<User>();
            for(int i = 1; i < userCount + 1; i++)
                items.Add(CreateUser(i));
            return items;
        }

        private static User CreateUser(int id) => new User() { Id = id, CreatedBy = "system", CreatedDate = DateTime.Now };

        /// <summary>
        /// Get order list
        /// </summary>
        /// <param name="orderCount">minimal order count</param>
        public static List<Order> GetOrders(int orderCount) => GenerateOrderList(orderCount);

        private static List<Order> GenerateOrderList(int orderCount)
        {
            List<Order> items = new List<Order>();
            for (int i = 1; i < orderCount + 1; i++)
                items.Add(CreateOrder(i));
            return items;
        }

        private static Order CreateOrder(int id) => new Order() { Id = id, CreatedBy = "system", CreatedDate = DateTime.Now };

        /// <summary>
        /// Get post list
        /// </summary>
        /// <param name="postCount">minimal post count</param>
        public static List<Post> GetPosts(int postCount) => GeneratePostList(postCount);

        private static List<Post> GeneratePostList(int postCount)
        {
            List<Post> items = new List<Post>();
            for (int i = 1; i < postCount + 1; i++)
                items.Add(CreatePost(i));
            return items;
        }

        private static Post CreatePost(int id) => new Post() { Id = id, CreatedBy = "system", CreatedDate = DateTime.Now };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userCount"></param>
        /// <returns></returns>
        public static List<Product> GetProducts(int productCount) => GenerateProductList(productCount);

        private static List<Product> GenerateProductList(int productCount)
        {
            List<Product> items = new List<Product>();
            for (int i = 1; i < productCount + 1; i++)
                items.Add(CreateProduct(i));
            return items;
        }

        private static Product CreateProduct(int id) => new Product() { Id = id, CreatedBy = "system", CreatedDate = DateTime.Now };
    }
}
