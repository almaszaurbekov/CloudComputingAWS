using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.JsonModels;
using DataAccess.Models;
using DataAccess.Services;
using DataAccess.Static;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace ContainerRDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserService service;
        private readonly IMapper mapper;

        public UserController(ILogger<UserController> logger, 
            IUserService service, IMapper mapper)
        {
            this.logger = logger;
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<OkObjectResult> GetUserList()
        {
            var users = await service.GetAll();
            return Ok(mapper.Map<List<User>, List<UserJsonModel>>(users));
        }

        [HttpGet("email/{email}")]
        public async Task<OkObjectResult> GetUserByEmail(string email)
        {
            if (SDHelper.IsValueNotNull(email))
            {
                var user = await service
                    .Find(s => s.NormalizedEmail.Contains(email.ToUpper()));
                if(user != null)
                    return Ok(mapper.Map<User, UserJsonModel>(user));
                return Ok(new UserJsonModel() { Error = "There is no user with this email", IsSuccess = false });
            }
            return Ok(new UserJsonModel("Email field is empty", false));
        }

        [HttpGet("{id}")]
        public async Task<OkObjectResult> GetUserById(string id)
        {
            if (SDHelper.IsValueNotNull(id))
            {
                var user = await service.Find(s => s.Id == id);
                if (user != null)
                    return Ok(mapper.Map<User, UserJsonModel>(user));
                return Ok(new UserJsonModel() { Error = "There is no user with this email", IsSuccess = false });
            }
            return Ok(new UserJsonModel("Email field is empty", false));
        }
    }
}
