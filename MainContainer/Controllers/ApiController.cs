using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess.JsonModels;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MainContainer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> logger;
        private readonly IHttpClientFactory httpClientFactory;

        public ApiController(ILogger<ApiController> logger,
            IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public OkObjectResult Get()
        {
            return Ok(new { response = 200 });
        }

        [HttpGet("user")]
        public async Task<OkObjectResult> GetUserList()
        {
            var response = await GetResponse("RDS", "user");
            return Ok(response);
        }

        [HttpGet("user/email/{email}")]
        public async Task<OkObjectResult> GetUserByEmail(string email)
        {
            var response = await GetResponse("RDS", $"user/email/{email}");
            return Ok(response);
        }

        [HttpGet("user/{id}")]
        public async Task<OkObjectResult> GetUserById(string id)
        {
            var response = await GetResponse("RDS", $"user/{id}");
            return Ok(response);
        }

        [HttpPost("user/edit")]
        public async Task<OkObjectResult> EditUser(UserJsonModel user)
        {
            var response = await PostResponse("RDS", "user/edit", user);
            return Ok(response);
        }

        [HttpGet("product")]
        public async Task<OkObjectResult> GetProductList()
        {
            var response = await GetResponse("RDS", "product");
            return Ok(response);
        }

        private async Task<string> GetResponse(string httpClient, string action)
        {
            var client = httpClientFactory.CreateClient(httpClient);
            var response = await client.GetAsync(action);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> PostResponse(string httpClient, string action, object model)
        {
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = httpClientFactory.CreateClient(httpClient);
            var response = await client.PostAsync(action, data);
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}