using System.Collections.Generic;
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

        public PostController(PostService service)
        {
            this.service = service;
        }
    }
}
