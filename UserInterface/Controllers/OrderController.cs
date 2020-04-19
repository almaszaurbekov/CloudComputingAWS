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
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IMapper mapper;

        public OrderController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            this.httpClientFactory = httpClientFactory;
            this.mapper = mapper;
        }

        // GET: Order
        public async Task<ActionResult> Index()
        {
            var response = await GetResponse("AWS", "order");
            var model = JsonConvert.DeserializeObject<OrderListJsonModel>(response);
            var orders = mapper.Map<List<Order>, List<OrderViewModel>>(model.Orders);
            return View(orders);
        }

        // GET: Order/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var response = await GetResponse("AWS", "order", id);
            var model = JsonConvert.DeserializeObject<OrderJsonModel>(response);
            var order = mapper.Map<OrderJsonModel, OrderViewModel>(model);
            return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await PostResponse("AWS", "order", model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Order/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var response = await GetResponse("AWS", "order", id);
            var model = JsonConvert.DeserializeObject<OrderJsonModel>(response);
            var order = mapper.Map<OrderJsonModel, OrderViewModel>(model);
            return View(order);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await PutResponse("AWS", $"order/{id}", model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Order/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var response = await GetResponse("AWS", "order", id);
            var model = JsonConvert.DeserializeObject<OrderJsonModel>(response);
            var order = mapper.Map<OrderJsonModel, OrderViewModel>(model);
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, OrderViewModel model)
        {
            try
            {
                var response = await DeleteResponse("AWS", $"order/{id}");
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