using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper mapper;

        public UserController(IHttpClientFactory httpClientFactory,
            IMapper mapper)
        {
            this.httpClientFactory = httpClientFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await GetResponse("AWS", "user");
            var model = JsonConvert.DeserializeObject<UserListJsonModel>(response);
            var users = mapper.Map<List<User>, List<UserViewModel>>(model.Users);
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var response = await GetResponse("AWS", "user", id);
            var model = JsonConvert.DeserializeObject<UserJsonModel>(response);
            if (!model.IsSuccess) return NotFound();
            var user = mapper.Map<UserJsonModel, UserViewModel>(model);
            return View(user);
        }

        private async Task<string> GetResponse(string httpClient, string action, params string[] param)
        {
            if(param.Length > 0)
                foreach(var par in param)
                    action += "/" + par;

            var client = httpClientFactory.CreateClient(httpClient);
            var query = await client.GetStringAsync(action);
            return JsonConvert.DeserializeObject<string>(query);
        }
    }
}