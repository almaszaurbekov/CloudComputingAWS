using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using DataAccess.JsonModels;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserInterface.Models;

namespace UserInterface.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient("AWS");
            var query = await client.GetStringAsync("user");
            var result = JsonConvert.DeserializeObject<string>(query);
            var users = JsonConvert.DeserializeObject<List<UserJsonModel>>(result);
            return View(users);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = httpClientFactory.CreateClient("AWS");
                var parameters = new Dictionary<string, string>()
                {
                    { "email", model.Email },
                    { "password", model.Password }
                };
                var response = await client.PostAsync("account/login",
                    new FormUrlEncodedContent(parameters)).ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync();
                return Ok(result);
            }

            return View(model);
        }
    }
}