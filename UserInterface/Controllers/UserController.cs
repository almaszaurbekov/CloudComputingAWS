﻿using System;
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
            var client = httpClientFactory.CreateClient("AWS");
            var query = await client.GetStringAsync("user");
            var result = JsonConvert.DeserializeObject<string>(query);
            var users = JsonConvert.DeserializeObject<List<UserJsonModel>>(result);
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var client = httpClientFactory.CreateClient("AWS");
            var query = await client.GetStringAsync($"user/{id}");
            var result = JsonConvert.DeserializeObject<string>(query);
            var entity = JsonConvert.DeserializeObject<UserJsonModel>(result);

            if (!entity.IsSuccess)
            {
                return NotFound();
            }

            return View(entity);
        }
    }
}