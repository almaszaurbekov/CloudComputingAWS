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
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IMapper mapper;

        public ProductController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            this.httpClientFactory = httpClientFactory;
            this.mapper = mapper;
        }

        // GET: Product
        public async Task<ActionResult> Index()
        {
            var response = await GetResponse("AWS", "product");
            var model = JsonConvert.DeserializeObject<ProductListJsonModel>(response);
            var products = mapper.Map<List<Product>, List<ProductViewModel>>(model.Products);
            return View(products);
        }

        // GET: Product/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var response = await GetResponse("AWS", "product", id.ToString());
            var model = JsonConvert.DeserializeObject<ProductJsonModel>(response);
            var product = mapper.Map<ProductJsonModel, ProductViewModel>(model);
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var response = await PostResponse("AWS", "product", product);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var response = await GetResponse("AWS", "product", id.ToString());
            var model = JsonConvert.DeserializeObject<ProductJsonModel>(response);
            var product = mapper.Map<ProductJsonModel, ProductViewModel>(model);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var response = await PutResponse("AWS", "product", id, product);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var response = await GetResponse("AWS", "product", id.ToString());
            var model = JsonConvert.DeserializeObject<ProductJsonModel>(response);
            var product = mapper.Map<ProductJsonModel, ProductViewModel>(model);
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ProductViewModel product)
        {
            try
            {
                var response = await DeleteResponse("AWS", $"product/{id}");
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

        private async Task<string> PutResponse(string httpClient, string action, object id, object model)
        {
            Dictionary<string, object> set = new Dictionary<string, object>() { { "id", id }, { "model", model } };
            var json = JsonConvert.SerializeObject(set);
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