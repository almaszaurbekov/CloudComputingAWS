using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.JsonModels;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserInterface.Models;

namespace UserInterface.Controllers
{
    public class PostController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IMapper mapper;

        public PostController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            this.httpClientFactory = httpClientFactory;
            this.mapper = mapper;
        }

        // GET: Post
        public async Task<ActionResult> Index()
        {
            var response = await GetResponse("AWS", "post");
            var model = JsonConvert.DeserializeObject<PostListJsonModel>(response);
            var products = mapper.Map<List<Post>, List<PostViewModel>>(model.Posts);
            return View(products);
        }

        // GET: Post/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var response = await GetResponse("AWS", "post", id);
            var model = JsonConvert.DeserializeObject<PostJsonModel>(response);
            var post = mapper.Map<PostJsonModel, PostViewModel>(model);
            return View(post);
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await PostResponse("AWS", "post", model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Post/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var response = await GetResponse("AWS", "post", id);
            var model = JsonConvert.DeserializeObject<PostJsonModel>(response);
            var post = mapper.Map<PostJsonModel, PostViewModel>(model);
            return View(post);
        }

        // POST: Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await PutResponse("AWS", $"post/{id}", model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Post/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var response = await GetResponse("AWS", "post", id);
            var model = JsonConvert.DeserializeObject<PostJsonModel>(response);
            var post = mapper.Map<PostJsonModel, PostViewModel>(model);
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, PostViewModel model)
        {
            try
            {
                var response = await DeleteResponse("AWS", $"post/{id}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<string> GetResponse(string httpClient, string action,
            params string[] param)
        {
            if (param.Length > 0)
                foreach (var par in param)
                    action += "/" + par;

            var client = httpClientFactory.CreateClient(httpClient);
            var response = await client.GetStringAsync(action);
            return JsonConvert.DeserializeObject<string>(response);
        }

        private async Task<string> PostResponse(string httpClient, string action, object model)
        {
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = httpClientFactory.CreateClient(httpClient);
            var response = await client.PostAsync(action, data);
            return response.Content.ReadAsStringAsync().Result;
        }

        private async Task<string> PutResponse(string httpClient, string action, object model)
        {
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = httpClientFactory.CreateClient(httpClient);
            var response = await client.PutAsync(action, data);
            return response.Content.ReadAsStringAsync().Result;
        }

        private async Task<string> DeleteResponse(string httpClient, string action)
        {
            var client = httpClientFactory.CreateClient(httpClient);
            var response = await client.DeleteAsync(action);
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}