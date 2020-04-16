﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            var client = httpClientFactory.CreateClient("RDS");
            var response = await client.GetAsync("user");
            var result = await response.Content.ReadAsStringAsync();
            return Ok(result);
        }

        [HttpGet("user/{email}")]
        public async Task<OkObjectResult> GetUserByEmail(string email)
        {
            var client = httpClientFactory.CreateClient("RDS");
            var response = await client.GetAsync($"user/{email}");
            var result = await response.Content.ReadAsStringAsync();
            return Ok(result);
        }

        [HttpGet("product")]
        public async Task<OkObjectResult> GetProductList()
        {
            var client = httpClientFactory.CreateClient("RDS");
            var response = await client.GetAsync("product");
            var result = await response.Content.ReadAsStringAsync();
            return Ok(result);
        }

        [HttpPost("account/login")]
        public async Task<OkObjectResult> Login(string email, string password)
        {
            var client = httpClientFactory.CreateClient("RDS");
            var parameters = new Dictionary<string, string>()
            {
                { "email", email },
                { "password", password }
            };
            var response = await client.PostAsync("account/login", 
                new FormUrlEncodedContent(parameters)).ConfigureAwait(false);
            var result = await response.Content.ReadAsStringAsync();
            return Ok(result);
        }
    }
}