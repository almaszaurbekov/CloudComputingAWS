using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UserInterface.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient();
            var url = ApiDestinations.MAIN_CONTAINER + "user";
            var result = await client.GetStringAsync(url);
            var users = JsonConvert.DeserializeObject<List<User>>(result);
            return View(users);
        }
    }
}