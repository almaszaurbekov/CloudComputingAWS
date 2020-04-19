using System;
using System.Collections.Generic;
using AutoMapper;
using DataAccess.JsonModels;
using DataAccess.Models;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace ContainerMDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostService service;
        private readonly IMapper mapper;

        public PostController(PostService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }


        [HttpGet]
        public OkObjectResult Get()
        {
            var posts = service.Get();
            if (posts.Count > 0)
            {
                var model = new PostListJsonModel(posts);
                return Ok(model);
            }
            return Ok(new PostListJsonModel(false, "Order list is empty"));
        }

        [HttpGet("{id}")]
        public OkObjectResult GetPostById(string id)
        {
            var post = service.GetPostById(id);
            if (post != null)
            {
                var model = mapper.Map<Post, PostJsonModel>(post);
                return Ok(model);
            }
            return Ok(new PostJsonModel(false, "No order with this id"));
        }

        [HttpPost]
        public OkObjectResult AddPost(PostJsonModel model)
        {
            try
            {
                var post = mapper.Map<PostJsonModel, Post>(model);
                service.Insert(post);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return Ok(new PostJsonModel(false, ex.Message));
            }
        }

        [HttpPut("{id}")]
        public OkObjectResult UpdatePost(string id, PostJsonModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return Ok(new PostJsonModel(false, "Bad request"));
                }

                var post = mapper.Map<PostJsonModel, Post>(model);
                var result = service.Update(post);
                if (result)
                {
                    return Ok(model);
                }
                return Ok(new PostJsonModel(false, "Something went wrong"));
            }
            catch (Exception ex)
            {
                return Ok(new PostJsonModel(false, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public OkObjectResult DeletePost(string id)
        {
            var post = service.GetPostById(id);
            if (post == null)
            {
                Ok(new PostJsonModel(false, "Not found"));
            }

            var result = service.Remove(id);
            if (result)
            {
                return Ok(mapper.Map<Post, PostJsonModel>(post));
            }

            return Ok(new PostJsonModel(false, "Something went wrong"));
        }
    }
}
