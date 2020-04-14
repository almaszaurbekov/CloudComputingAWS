using System.Threading.Tasks;
using ContainerRDS.JsonModels;
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

        public UserController(ILogger<UserController> logger, IUserService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpGet]
        public async Task<OkObjectResult> GetUserList()
        {
            var users = await service.GetAll();
            return Ok(users);
        }

        [HttpGet("email")]
        public async Task<OkObjectResult> GetUserByEmail(string email)
        {
            if (SDHelper.IsValueNotNull(email))
            {
                var user = await service
                    .Find(s => s.NormalizedEmail.Contains(email.ToUpper()));
                if(user != null)
                    return Ok(new UserJsonModel() { Id = user.Id, Email = user.Email, IsSuccess = true });
                return Ok(new UserJsonModel() { Error = "There is no user with this email", IsSuccess = false });
            }
            return Ok(new UserJsonModel("Email field is empty", false));
        }
    }
}
