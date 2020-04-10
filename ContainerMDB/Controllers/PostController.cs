using System.Collections.Generic;
using DataAccess.Models;
using DataAccess.Static;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace ContainerRDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;

        public PostController(ILogger<PostController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Post> Get()
        {
            return SDController.GetPosts(10);
        }
    }
}
